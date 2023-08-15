module Components.Links

open Feliz
open Components.Adapters

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

    let useStyles = MuiStyles.makeStyles_get (fun theme ->
            {|
                grid= Fable.Core.JsInterop.createObj [

                        "display", "grid"
                        "gridTemplateColumns", "1fr 1fr 1fr 1fr"
                        "paddingTop", 40
                        "paddingBottom", 20
                        theme.breakpoints.down Mui.MaterialUI.BreakpointKey.Sm, {| gridTemplateColumns = "1fr" |}
                ]
                button= {| margin= 5 |}
                skeleton= {|

                |}

            |}
    )
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
        if hasBlended then ""
        else "1fr 3fr"
        |> setGridTemplateOption
        items
    let getItemsCallback = React.useCallback(getItems, Array.singleton input.category)
    // React.useEffect( fun () ->
    //     setItemState(initialState)
    //     getItemsCallback(menu)
    // )
    Html.div [
        prop.children [
            let focusedElement = Html.div []
            focusedElement
            if itemState.loading then
                Mui.Skeleton.Skeleton2 (Some Mui.Skeleton.Rectangular) (Some Mui.Skeleton.Wave) [ prop.width (length.percent 50.)]
            else
                Mui.Box.Box [
                    Mui.sx {| display="flex"; alignItems="flex-end" |}
                    prop.children [
                        Html.text "I am a box"
                        Mui.Icons.SearchIcon [ Mui.sx {| color="action.active"; mr=1; my=0.5 |}]
                        Mui.TextField.TextField [ prop.id "input-with-sx"; Mui.sx {| width=350 |}]
                    ]
                ]
            if not itemState.hasBlendedLearning && input.category = Schema.BlendedLearning then
                Html.div [
                    Html.p [
                        prop.style [
                            style.textAlign.center
                        ]
                    ]
                ]
            Mui.Box.Box [
                prop.classes []
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

    ]

[<ReactComponent>]
let LinksComponent args = React.functionComponent(linksView) args