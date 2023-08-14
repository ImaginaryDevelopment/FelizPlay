module Components.Adapters.Mui

open Feliz

// could use our own sans special logic?
let Skeleton = fun () -> 
    Html.span [
        prop.classes [
            "MuiSkeleton-root MuiSkeleton-rectangular MuiSkeleton-wave css-1kcnzpi-MuiSkeleton-root"
        ]
        prop.style [
            style.width (length.percent 50)
        ]
    ]



// <span class="MuiSkeleton-root MuiSkeleton-rectangular MuiSkeleton-wave css-1kcnzpi-MuiSkeleton-root" style="width: 50%;"></span>