module LocalNetwork

open System
open Computer

type LocalNetwork(computers: Computer list, matrix: Computer list list) =
    let mutable needToPrint = true

    let printState step =
        printfn $"Step #%d{step}"

        computers
        |> List.iter (fun computer ->
            let state =
                if computer.IsInfected then
                    "infected"
                else
                    "okay"

            printfn $"Computer %d{computer.Id + 1} is %s{state}")

    let finish () = printfn "Finished"

    let tryToInfect (computer: Computer) =
        if not computer.IsInfected then
            if Random().NextDouble() < computer.ProbabilityOfInfection then
                computer.IsInfected <- true
                needToPrint <- true

    let checkNeighbors (computer: Computer) =
        matrix[computer.Id]
        |> List.filter (fun computer -> not computer.IsInfected && computer.ProbabilityOfInfection > 0.0)

    let findPossibleInfected (computers: Computer list) =
        computers
        |> List.filter (fun x -> x.IsInfected)
        |> List.collect checkNeighbors

    let rec update step =
        if needToPrint then
            printState step
            needToPrint <- false

        let possibleInfected =
            findPossibleInfected computers

        match possibleInfected with
        | [] -> finish ()
        | _ ->
            possibleInfected |> List.iter tryToInfect
            update (step + 1)

    member this.Start() = update 0
