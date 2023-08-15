module Components.Adapters.Msal
open Fable.Core.JsInterop

import "PublicClientApplication" "@azure/msal-browser"
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