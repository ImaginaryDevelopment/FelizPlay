module Components.Adapters.Mui
// see migration at https://mui.com/material-ui/migration/migration-v4/
// see example mappings at https://github.com/Shmew/Feliz.MaterialUI
// older example at https://github.com/mvsmal/fable-material-ui
open Feliz
open Fable.Core.JsInterop

let sx obj =
    Interop.mkAttr "sx" obj

/// Layout
module Box =

    let Box props =
        Feliz.Interop.createElement(importDefault "@mui/material/Box") props

module Skeleton =


    type SkeletonVariant =
        | Circular // of FelizSchema.SizeOpt
        | Rectangular // of FelizSchema.SizeOpt
        | Rounded // of FelizSchema.SizeOpt
        | Text // of fontSize: string option

    type SkeletonAnimation =
        | Pulse
        | Wave
        | NoAnimation

    let variant sv =
        let makeV (name: string) = Interop.mkAttr "variant" name
        match sv with
        | Circular -> makeV "circular"
        | Rectangular -> makeV "rectangular"
        | Rounded -> makeV "rounded"
        | Text -> makeV "text"

    let animation a =
        let makeA (name: string) = Interop.mkAttr "animation" name
        match a with
        | Pulse -> makeA "pulse"
        | Wave -> makeA "wave"
        | NoAnimation -> makeA "false"


    let Skeleton props =
        Feliz.Interop.createElement(importDefault "@mui/material/Skeleton") props

    let Skeleton2 variantOpt animationOpt props =
        Skeleton [
            match variantOpt with
            | None -> ()
            | Some v -> yield variant v
            match animationOpt with
            | None -> ()
            | Some a -> yield animation a
            yield! props
        ]

module TextField = // https://mui.com/material-ui/react-text-field/

    /// the examples suggest variant and label attributes all matching, might not be required
    type TextVariant =
        | Outlined
        | Filled
        | Standard // semi-unsupported

    let variant tv =
        let makeV (name: string) = Interop.mkAttr "variant" name
        match tv with
        | Outlined -> makeV "outlined"
        | Filled -> makeV "Filled"
        | Standard -> makeV "Standard"
    let label (tv: TextVariant) =
        let makeL (name: string) = Interop.mkAttr "label" name
        string tv |> makeL

    let TextField props =
        Feliz.Interop.createElement (importDefault "@mui/material/TextField") props
// fun () ->
//     Html.span [
//         prop.classes [
//             "MuiSkeleton-root MuiSkeleton-rectangular MuiSkeleton-wave css-1kcnzpi-MuiSkeleton-root"
//         ]
//         prop.style [
//             style.width (length.percent 50)
//         ]
//     ]



// <span class="MuiSkeleton-root MuiSkeleton-rectangular MuiSkeleton-wave css-1kcnzpi-MuiSkeleton-root" style="width: 50%;"></span>
module Icons = // @mui/icons-material
    /// import SearchIcon from '@mui/icons-material/Search';
    let SearchIcon props =
        Feliz.Interop.createElement (importDefault "@mui/icons-material/Search") props

