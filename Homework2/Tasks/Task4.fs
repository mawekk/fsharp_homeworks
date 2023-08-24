module Tasks.Task4

let isPrime n =
    let upperBound = (float >> sqrt >> int) n

    { 2..upperBound }
    |> Seq.forall (fun x -> n % x <> 0)

let generateSeq() =
    Seq.initInfinite (fun x -> x + 2) |> Seq.filter isPrime
