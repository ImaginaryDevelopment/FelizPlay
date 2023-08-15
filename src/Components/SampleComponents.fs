module App.Components.SampleComponents

open Feliz
open Feliz.Router

/// <summary>
/// The simplest possible React component.
/// Shows a header with the text Hello World
/// </summary>
[<ReactComponent>]
let HelloWorld() = Html.h1 "Hello World"

/// <summary>
/// A stateful React component that maintains a counter
/// </summary>
[<ReactComponent>]
let Counter() =
    let (count, setCount) = React.useState(0)
    if false then // https://github.com/Zaid-Ajaj/Feliz/blob/master/public/Feliz/React/EffectfulComponents.md
        React.useEffect(fun () ->
            Browser.Dom.document.title <- sprintf "Count = %d" count
        )
    Html.div [
        Html.h1 count
        Html.button [
            prop.onClick (fun _ -> setCount(count + 1))
            prop.text "Increment"
        ]
    ]

let index(updateUrl) =
    let makeLink (name: string) url =
            Html.li [
                Html.a [
                    prop.href url
                    prop.text name
                    prop.onClick(fun e ->
                        e.preventDefault()
                        updateUrl url)
                ]
            ]

    Html.div [
        Html.h1 "Index"
        Html.ul [
            Html.li [
                makeLink "Counter" "counter"
            ]
            Html.li [ makeLink "Blended" "blended"]
        ]
    ]

/// <summary>
/// A React component that uses Feliz.Router
/// to determine what to show based on the current URL
/// </summary>
let withRouter f = 
    let (currentUrl, updateUrl) =
        React.useState(Router.currentUrl())
    React.router [
        router.onUrlChanged updateUrl
        router.children [
            printfn "currentUrl:%A" currentUrl
            match currentUrl with
            | [ ] ->
            
                printfn "Router: Index"
                index(fun next ->
                    updateUrl (next::currentUrl)
                )
            | [ "hello" ] ->
                printfn "Router: Hello"
                HelloWorld()
            | [ "blended" ] -> f()
            | [ "counter" ] ->
                printfn "Router: Counter"
                Counter()
            | _ -> Html.h1 "Not found"
        ]
    ]

[<ReactComponent>]
let App () = Components.Pages.BlendedLearning.BlendedLearning ()