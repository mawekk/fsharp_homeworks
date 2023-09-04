module LazyWithLock

open ILazy

type LazyWithLock<'a>(supplier: unit -> 'a) =
    let mutable result = None
    let lockObj = obj ()

    interface ILazy<'a> with
        member this.Get() =
            if result.IsSome then
                result.Value
            else
                lock lockObj (fun () ->
                    if result.IsNone then
                        result <- Some(supplier ()))

                result.Value
