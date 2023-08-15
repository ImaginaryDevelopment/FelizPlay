module Main

open Feliz
open App
open Browser.Dom
open Fable.Core.JsInterop

open Components.Adapters

importAll "./Components/pages/BlendedLearning.module.css"

// let createTheme (props: IReactProperty list) : ReactElement = import "createTheme" "@mui/material/styles"
// attempt to import ThemeProvider
// let tp : IReactProperty list -> ReactElement = import "ThemeProvider" "@mui/material/styles"
importAll "@mui/icons-material"
// let theme : = import "Theme" "@mui/material/styles"
let root = ReactDOM.createRoot(document.getElementById "feliz-app")
let defaultTheme = MuiStyles.createTheme {| |}
console.log("defaultTheme", defaultTheme)
// root.render(Components.SampleComponents.App())

// ThemeProvider did not work passing a list of props, seems to work with anonymous record
let tpProps = {| theme = defaultTheme; children = [Components.SampleComponents.App()]|}
// root.render(MuiStyles.ThemeProvider [ Interop.mkAttr "theme" defaultTheme; prop.children [ Components.SampleComponents.App()]])
MuiStyles.ThemeProvider tpProps
|> root.render


Components.SampleElmish.run()
