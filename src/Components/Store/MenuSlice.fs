module Components.Store.MenuSlice

type MenuItemSrc = {
    Name: string
    NavType: obj
    Url: string
    Script: obj
    Description: obj
    Icon: obj
    Acl: obj
    Properties: obj option
    UrlKey: obj option
}

type MenuState = {
    mutable menu: MenuItemSrc[]
}

let initialState = {
    menu= Array.empty
}

let menuSlice: StoreSchema.Reducer<MenuState> = 
    StoreSchema.Imports.createSlice {|
            name= "menu"
            initialState= initialState
            reducers= {|


            |}
    |}