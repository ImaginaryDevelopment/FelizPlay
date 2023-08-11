namespace App

open Feliz
open Feliz.Router

type Components =
    /// <summary>
    /// The simplest possible React component.
    /// Shows a header with the text Hello World
    /// </summary>
    [<ReactComponent>]
    static member HelloWorld() = Html.h1 "Hello World"

    /// <summary>
    /// A stateful React component that maintains a counter
    /// </summary>
    [<ReactComponent>]
    static member Counter() =
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
    static member Index(updateUrl) =
        Html.div [
            Html.h1 "Index"
            Html.ul [
                Html.li [
                    Html.a [ 
                        prop.href "counter"
                        prop.text "Counter"
                        prop.onClick(fun e ->
                            e.preventDefault()
                            updateUrl "counter")
                    ]
                ]
            ]
        ]

    /// <summary>
    /// A React component that uses Feliz.Router
    /// to determine what to show based on the current URL
    /// </summary>
    [<ReactComponent>]
    static member Router() =
        let (currentUrl, updateUrl) =
            React.useState(Router.currentUrl())
        React.router [
            router.onUrlChanged updateUrl
            router.children [
                printfn "currentUrl:%A" currentUrl
                match currentUrl with
                | [ ] ->
                    printfn "Router: Index"
                    Components.Index(fun next ->
                        updateUrl (next::currentUrl)
                    )
                | [ "hello" ] ->
                    printfn "Router: Hello"
                    Components.HelloWorld()
                | [ "counter" ] ->
                    printfn "Router: Counter"
                    Components.Counter()
                | otherwise -> Html.h1 "Not found"
            ]
        ]