module Components.SampleElmish
open Feliz
open Feliz.UseElmish
open Elmish

type Msg =
    | Increment
    | Decrement

type State = { Count : int }

let init() = { Count = 0 }, Cmd.none

let update msg state =
    match msg with
    | Increment -> { state with Count = state.Count + 1 }, Cmd.none
    | Decrement -> { state with Count = state.Count - 1 }, Cmd.none

[<ReactComponent>]
let Counter() =
    let state, dispatch = React.useElmish(init, update, [| |])
    Html.div [
        Html.h1 [
            sprintf "Elmish counter: %i" state.Count |> Html.text]
        Html.button [
            prop.text "Increment"
            prop.onClick (fun _ -> dispatch Increment)
        ]

        Html.button [
            prop.text "Decrement"
            prop.onClick (fun _ -> dispatch Decrement)
        ]
    ]

open Browser.Dom

// https://github.com/Zaid-Ajaj/Feliz
let private getDomEl () = document.getElementById "elmish-app"

[<System.Obsolete>]
let old () = ReactDOM.render(Counter(), getDomEl())

let run() =
    let root = ReactDOM.createRoot(getDomEl())
    root.render(Counter())