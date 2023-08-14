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
                        name = child.Name
                        navType = child.NavType
                        url = child.Url 
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
        if itemState.loading then
            Components.Adapters.Mui.Skeleton()
        else 
            Html.div [
                prop.classes [ "MuiBox-root"; "css-1nylpq2" ]
                prop.children [
                    // <svg class="MuiSvgIcon-root MuiSvgIcon-fontSizeMedium css-oe0os1-MuiSvgIcon-root" focusable="false" aria-hidden="true" viewBox="0 0 24 24" data-testid="SearchIcon"><path d="M15.5 14h-.79l-.28-.27C15.41 12.59 16 11.11 16 9.5 16 5.91 13.09 3 9.5 3S3 5.91 3 9.5 5.91 16 9.5 16c1.61 0 3.09-.59 4.23-1.57l.27.28v.79l5 4.99L20.49 19l-4.99-5zm-6 0C7.01 14 5 11.99 5 9.5S7.01 5 9.5 5 14 7.01 14 9.5 11.99 14 9.5 14z"></path></svg>

                    Html.input [
                        prop.title "Filter Links"
                    ]
                ]
            ]
        // <div class="makeStyles-grid-155 MuiBox-root css-0" id="linkscomponentbox"></div>
        Html.div [
            prop.id "linksComponentBox"
            prop.classes [ "makeStyles-grid-155 MuiBox-root css-0"]
            prop.children [
                Html.text "Hello Blended"
            ]
        ]

    ]

[<ReactComponent>]
let LinksComponent = React.functionComponent(linksView)