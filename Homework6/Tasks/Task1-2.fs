module Tasks

type RoundingBuilder(accuracy: int) =
    member this.Bind(x: float, f) = f <| System.Math.Round(x, accuracy)
    member this.Return(x: float) = System.Math.Round(x, accuracy)

type CalculateBuilder() =
    member this.Bind(x: string, f) =
        match System.Int32.TryParse x with
        | true, int -> f int
        | _ -> None

    member this.Return(x) = Some x
