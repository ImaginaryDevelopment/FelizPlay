module Components.Adapters.Msal
open Fable.Core.JsInterop

import "PublicClientApplication" "@azure/msal-browser"

let inline private importMsalReact selector = import "MsalProvider" "@azure/msal-react"

module Props =
    let instance x = Feliz.Interop.mkAttr "instance" x
let PublicClientApplication (constructorArgs:obj) : obj =
    let t = import "PublicClientApplication" "@azure/msal-browser"
    createNew t (constructorArgs)
// [<ImportAll("@azure/msal-browser")>]
// module Util =
//     let 
let MsalProvider props =
        Feliz.Interop.createElement (import "MsalProvider" "@azure/msal-react") props
[<AbstractClass>]
type IMSalContext =
    abstract member instance: obj
    abstract member inProgress: obj
    abstract member accounts: obj[]
    abstract member logger: obj

// type MsalObj =
//     abstract instance: ipublic with get
//     abstract account: obj with get
//     abstract inProgress: obj with get
let useMsal (): IMSalContext = import "useMsal" "@azure/msal-react" ()

let useIsAuthenticated () : bool = importMsalReact "useIsAuthenticated" ()