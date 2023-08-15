namespace Components.Store.StoreSchema


type PayloadAction<'t> =
    abstract member payload: 't
type Reducer<'t> =
    abstract member reducer: obj


module Imports =

    open Fable.Core.JsInterop
    let createSlice<'t> (x:obj) : Reducer<'t> = import "createSlice" "@reduxjs/toolkit" x