module Lazy

open ILazy

type Lazy<'a>(supplier: unit -> 'a) =
    let mutable result = None

    interface ILazy<'a> with
        member this.Get() =
            if result.IsNone then
                result <- Some(supplier ())

            result.Value
