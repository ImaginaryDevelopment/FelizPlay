module Components.Links

open Feliz

open Reusable

let card_height_sm = 150
let card_height_lg = 75

type MenuItem = {
    name: string
    navType: obj
    url: string
    script: obj
    description: obj
    icon: obj
    acl: obj
    properties: obj option
    urlKey: obj option
}

type IItem = {
    loading: bool
    data: MenuItem[]
    hasBlendedLearning: bool
}

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

let getIcon name =
    "test"
module Views =
    let loadingView =
        Html.div [
            Html.text "loading..."
        ]
    let loadedView =  ()

let linksView (input: {| category : Schema.LinkMenuType |} ) =
    let initialState = {loading= true; data = Array.empty; hasBlendedLearning = false}
    let (gridTemplateOption, setGridTemplateOption) = React.useState "1fr 3fr"
    let itemState, setItemState = React.useState(initialState)
    let original, setOriginal = React.useState<MenuItem[]>(Array.empty)
    let filter, setFilter = React.useState("")
    // useTranslation
    // let navigate = Feliz.Router.Router
    let getItems (menu: {| Name: string ; Items: MenuItemSrc[] |}[]) =
        let mutable hasBlended = false
        let items =
            menu
            // |> Seq.takeWhileInc(fun item -> item <> "Blended Learning")
            |> Seq.filter(fun child -> child.Name = string input.category)
            |> Seq.collect(fun child -> child.Items)
            |> Seq.map(fun child ->
                    let item = {
                        name = if child.Name = "Bus Schedule" then "Bus Registration" else child.Name
                        navType = child.NavType
                        url = if child.Name = "Bus Schedule" then "/Page/31291" else child.Url 
                        script = child.Script
                        description = child.Description
                        icon = getIcon child.Icon
                        acl = child.Acl
                        properties = child.Properties
                        urlKey = child.UrlKey
                    }
                    if child.Name = "Blended Learning" then
                        hasBlended <- true
                    item
            )
            |> Array.ofSeq
            
        setItemState({loading = false; data = items; hasBlendedLearning = hasBlended})
        setOriginal items
        if hasBlended then
            ""
        else "1fr 3fr"
        |> setGridTemplateOption
        items
    let getItemsCallback = React.useCallback(getItems, Array.singleton input.category)
    // React.useEffect( fun () ->
    //     setItemState(initialState)
    //     getItemsCallback(menu)
    // )
    Html.div [


    ]

[<ReactComponent>]
let linksComponent = React.functionComponent(linksView)