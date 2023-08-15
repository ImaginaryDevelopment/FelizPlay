module Main

open Feliz
open App
open Browser.Dom
open Fable.Core.JsInterop

importAll "./Components/pages/BlendedLearning.module.css"
importAll "@mui/icons-material"

let root = ReactDOM.createRoot(document.getElementById "feliz-app")
root.render(Components.SampleComponents.App())

Components.SampleElmish.run()
