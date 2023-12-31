module Components.Adapters.MuiStyles // https://github.com/Shmew/Feliz.MaterialUI/blob/master/src/Feliz.MaterialUI/Styles.fs

open Components.Adapters.Mui.MaterialUI

open Fable.Core
open Fable.Core.JsInterop
open Feliz

//let ThemeProvider = import "ThemeProvider" "@mui/material/styles"
// node_modules\@mui\material\styles\makeStyles.d.ts
// https://mui.com/material-ui/migration/v5-style-changes/#%E2%9C%85-update-makestyles-import
let makeStyles_get (getStyles: Theme -> 't) : ('props -> 't) =
    import "makeStyles" "@mui/styles" getStyles
// let makeStyles_getWithOpts (getStyles: Theme -> obj) (opts: MakeStylesOptions) : ('props -> 'a) =
//     import "makeStyles" "@mui/base/styles" getStyles opts

let createTheme obj : obj = import "createTheme" "@mui/material/styles"

// attempt to import ThemeProvider
// let ThemeProvider : IReactProperty list -> ReactElement = import "ThemeProvider" "@mui/material/styles"
// this should have used createElement, not import
let ThemeProvider : obj -> ReactElement = import "ThemeProvider" "@mui/material/styles"
// let ThemeProvider : ReactElement list -> ReactElement = import "ThemeProvider" "@mui/material/styles"