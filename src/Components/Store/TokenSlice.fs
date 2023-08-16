module Components.Store.TokenSlice

open Components.Store.StoreSchema

type TokenInterface = {
    token: string
    expiration: string
}

type TokenState = {
    mutable token: string
    mutable tokenExpiration: string
    mutable tokenNew: string
    mutable tokenNewExpiration: string
}

let tokenSlice: StoreSchema.Reducer<TokenState> = 
    StoreSchema.Imports.createSlice {|
        name= "token"
        initialState= {
            token= ""
            tokenExpiration= ""
            tokenNew= ""
            tokenNewExpiration= ""
        }
        reducers = {|
            updateTokenOld= fun (state,action: PayloadAction<TokenInterface>) ->
                state.token <- action.payload.token
                state.tokenExpiration <- action.payload.expiration
            updateTokenNew= fun (state, action: PayloadAction<TokenInterface>) ->
                state.tokenNew <- action.payload.token
                state.tokenNewExpiration <- action.payload.expiration
        |}
    |}

[<AbstractClass>]
type TokenActions =
    abstract member updateTokenOld: TokenInterface -> unit
    abstract member updateTokenNew: TokenInterface -> unit
let tokenActions : TokenActions = tokenSlice.actions :?> TokenActions