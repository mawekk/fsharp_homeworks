module Tests

open PointFree

open NUnit.Framework
open FsUnit
open FsCheck
open System.Threading

open LazyFactory

[<Test>]
let ``Point-free test`` () =
    Check.QuickThrowOnFailure(fun g l -> (f g l) = (f4 g l))

[<Test>]
let ``Lazy test`` () =
    let random = System.Random()

    let _lazy =
        LazyFactory.CreateLazy(random.NextInt64)

    let first = _lazy.Get()
    let second = _lazy.Get()
    first |> should equal second

[<Test>]
let ``Lazy with lock test`` () =
    let x = ref 1

    let _lazy =
        LazyFactory.CreateLazyWithLock(fun () -> Interlocked.Increment(x))

    let task =
        async { return _lazy.Get() |> should equal 2 }

    let tasks = Seq.init 10 (fun _ -> task)

    tasks
    |> Async.Parallel
    |> Async.RunSynchronously
    |> ignore

    x.Value |> should equal 2

[<Test>]
let ``Lazy free lock test`` () =
    let x = ref 0

    let _lazy =
        LazyFactory.CreateLazyFreeLock(fun () -> Interlocked.Increment(x))

    let tasks =
        Seq.init 10 (fun _ -> async { return _lazy.Get() |> should equal 1 })

    tasks
    |> Async.Parallel
    |> Async.RunSynchronously
    |> ignore
