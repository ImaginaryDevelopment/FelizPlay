module Components.Store

open Fable.Core.JsInterop

open Components.Store
open Components.Store.StoreSchema

// import { configureStore } from "@reduxjs/toolkit";

type RootState = {
    user: Components.Store.UserSlice.UserState
    menu: Components.Store.MenuSlice.MenuState
    token: obj
    student: obj
    error: obj
}
[<AbstractClass>]
type Store<'t> =
    // getState
    abstract member getState: unit -> 't

// pretty wild function here, hack in any/obj for now
let configureStore : obj -> Store<RootState> = import "configureStore" "@reduxjs/toolkit"

let store = configureStore({| reducer= {|
    user= UserSlice.userSlice.reducer
    menu= MenuSlice.menuSlice.reducer
|}|})


module internal Impl = // https://github.com/fable-compiler/fable-browser/tree/master/src/WebStorage
    let localStorage = Browser.WebStorage.localStorage
    let inline deserialize<'t>(text: string) =
        Fable.Core.JS.JSON.parse(text) :?> 't

open Impl

// https://github.com/ImaginaryDevelopment/SkyblockHelper/blob/master/src/Client/CodeHelpers/BrowserStorage.fs
type Internal =
    static member inline TryGet< 't when 't : equality > (key) : 't option =
        localStorage.getItem key
        |> Option.ofObj
        |> Option.bind (fun x ->
            let result: 't option =
                let v = deserialize x
                if System.Object.ReferenceEquals(v,null) then
                    None
                else Some v
            result
        )

// a store with hooks that determine how to update state
//let store =