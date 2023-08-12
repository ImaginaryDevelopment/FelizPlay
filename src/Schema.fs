module Schema

let blendedLearningText = "Blended Learning"
type LinkMenuType = 
    | BlendedLearning
    | HowTo
    | MyLinks
    with
        override x.ToString() =
            match x with
            | BlendedLearning -> blendedLearningText
            | HowTo -> "How To"
            | MyLinks -> "My Links"
