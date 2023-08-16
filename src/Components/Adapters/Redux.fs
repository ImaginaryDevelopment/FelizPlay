module Components.Adapters.Redux

open Fable.Core.JsInterop

let inline private importRedux selector = import selector "react-redux"
let inline private importUseSelector (args:obj) = importRedux "useSelector" args
let useSelector (selector: 't -> 'tSelected) : 'tSelected =
    importUseSelector (selector)
// let useSelector2 (selector: 't -> 'tSelected, equalityFn: 'tSelected * 'tSelected -> bool) : 'tSelected =
//     importUseSelector () (selector, equalityFn)

let useDispatch () : obj -> unit = importRedux "useDispatch"

let Provider props = Feliz.Interop.createElement (import "Provider" "react-redux") props