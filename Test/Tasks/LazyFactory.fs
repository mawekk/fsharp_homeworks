module LazyFactory

open ILazy
open Lazy
open LazyWithLock
open LazyLockFree

type LazyFactory =
    static member CreateLazy supplier = new Lazy<'t>(supplier) :> ILazy<'t>

    static member CreateLazyWithLock supplier =
        new LazyWithLock<'t>(supplier) :> ILazy<'t>

    static member CreateLazyFreeLock supplier =
        new LazyLockFree<'t>(supplier) :> ILazy<'t>
