module Reusable
open System.Collections.Generic

module Seq =
    let takeWhileInc f (items: _ seq) =
        let rec loop (en: IEnumerator<_>) = seq {
            if en.MoveNext() then
                yield en.Current
            if not <| f en.Current then
                yield! loop en
        }
        seq {
            use en = items.GetEnumerator()
            yield! loop en
        }