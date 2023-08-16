module Main

open Feliz
open App
open Browser.Dom
open Fable.Core.JsInterop

open Components.Adapters

importAll "./Styles/main.chunk.css"
importAll "./Styles/App.css"
importAll "./Components/pages/BlendedLearning.module.css"
// let createTheme (props: IReactProperty list) : ReactElement = import "createTheme" "@mui/material/styles"
// attempt to import ThemeProvider
// let tp : IReactProperty list -> ReactElement = import "ThemeProvider" "@mui/material/styles"
importAll "@mui/icons-material"

type EnvLoginInfo = {
    domainHint: string
    scope: string
    oldScope: string
}

type EnvVars = {
    msalClientId: string
    msalAuthority: string
    login: EnvLoginInfo
}

// let theme : = import "Theme" "@mui/material/styles"
let root = ReactDOM.createRoot(document.getElementById "feliz-app")
let defaultTheme = MuiStyles.createTheme {| |}
console.log("defaultTheme", defaultTheme)
// root.render(Components.SampleComponents.App())
let envVars: EnvVars = import "msal" "../local.env"
console.log("envVars", envVars)

type AppCallbackArg = {
    accessToken: obj
    expiresOn: obj
}

module App =
    let readScope= "user.readbasic.all"
    let createLoginConfig authority = {|
        scopes= [
            "openid"
            authority
            readScope
        ]
        extraQueryParameters= {| domain_hint= envVars.login.domainHint; prompt= "select_account" |}
    |}
    let loginConfigOld= createLoginConfig envVars.login.oldScope
    let loginConfigNew= createLoginConfig envVars.login.scope

    [<ReactComponent>]
    let App() =
        let msal = Msal.useMsal()
        let dispatch = Redux.useDispatch()
        let isAuthenticated = Msal.useIsAuthenticated()
        let ApisToConnect = React.useCallback(fun (res: AppCallbackArg) ->
            let expiration = createDate(res.expiresOn)
            let payload = {|
                token= res.accessToken
                expiration= expiration.toLocaleString()
            |}
            dispatch(Components.Store.TokenSlice.tokenActions.updateTokenNew(unbox payload))
        )
        ()

let AppCreate() =

    // ThemeProvider did not work passing a list of props, seems to work with anonymous record
    let tpProps = {| theme = defaultTheme; children = [Components.SampleComponents.App()]|}
    // root.render(MuiStyles.ThemeProvider [ Interop.mkAttr "theme" defaultTheme; prop.children [ Components.SampleComponents.App()]])
    MuiStyles.ThemeProvider tpProps

let msalInstance = Msal.PublicClientApplication( {|
    auth= {|
        clientId= envVars.msalClientId
        authority= envVars.msalAuthority
        redirectUri= window.location.origin
    |}
    cache= {|
        cacheLocation= "sessionStorage"
        storeAuthStateInCookie= false
    |}
|})

console.log("msal", msalInstance)
Msal.MsalProvider [
    Msal.Props.instance msalInstance
    prop.children [
        Redux.Provider [
            Feliz.Interop.mkAttr "store" Components.Store.store
            prop.children [
                React.suspense([
                    AppCreate()
                ],unbox "loading")
            ]
        ]
    ]
]
|> root.render


Components.SampleElmish.run()
