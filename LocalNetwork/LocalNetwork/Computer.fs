module Computer

type Computer(id: int, operatingSystem: string, isInfected: bool) =
    member this.Id = id - 1
    member this.OS = operatingSystem
    member val IsInfected = isInfected with get, set
    member this.ProbabilityOfInfection =
        match this.OS with
        | "Windows" -> 0.8
        | "Linux" -> 0.5
        | "macOS" -> 0.0
        | _ -> 1.0