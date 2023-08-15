module Components.Adapters.Redux

open Fable.Core.JsInterop

let inline private importUseSelector (args:obj) = import "useSelector" "react-redux" args
let useSelector (selector: 't -> 'tSelected) : 'tSelected =
    importUseSelector (selector)
// let useSelector2 (selector: 't -> 'tSelected, equalityFn: 'tSelected * 'tSelected -> bool) : 'tSelected =
//     importUseSelector () (selector, equalityFn)

let Provider props = Feliz.Interop.createElement (import "Provider" "react-redux") props