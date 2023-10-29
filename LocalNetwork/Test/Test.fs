module Test

open LocalNetwork
open Computer

open NUnit.Framework
open FsUnit

[<Test>]
let ``The probability of infection is always 1.0`` () =
    let comp1 = Computer(1, "other", true)
    let comp2 = Computer(2, "other", false)
    let comp3 = Computer(3, "other", false)
    let comp4 = Computer(4, "other", false)
    let comp5 = Computer(5, "other", false)
    let comp6 = Computer(6, "other", false)

    let computers =
        [ comp1
          comp2
          comp3
          comp4
          comp5
          comp6 ]

    let matrix =
        [ [ comp2; comp3 ]
          [ comp1; comp4; comp5 ]
          [ comp1; comp6 ]
          [ comp2 ]
          [ comp2 ]
          [ comp3 ] ]

    let net = LocalNetwork(computers, matrix)
    net.Start()

    let results =
        computers
        |> List.map (fun computer -> computer.IsInfected)

    let expected = [ for _ in 1..6 -> true ]

    results |> should equal expected

[<Test>]
let ``The probability of infection is always 0.0`` () =
    let comp1 = Computer(1, "macOS", true)
    let comp2 = Computer(2, "macOS", false)
    let comp3 = Computer(3, "macOS", false)
    let comp4 = Computer(4, "macOS", false)
    let comp5 = Computer(5, "macOS", false)
    let comp6 = Computer(6, "macOS", false)

    let computers =
        [ comp1
          comp2
          comp3
          comp4
          comp5
          comp6 ]

    let matrix =
        [ [ comp2; comp3 ]
          [ comp1; comp4; comp5 ]
          [ comp1; comp6 ]
          [ comp2 ]
          [ comp2 ]
          [ comp3 ] ]

    let net = LocalNetwork(computers, matrix)
    net.Start()

    let results =
        computers
        |> List.map (fun computer -> computer.IsInfected)

    let expected =
        true :: [ for _ in 1..5 -> false ]

    results |> should equal expected

[<Test>]
let ``Some test`` () =
    let comp1 = Computer(1, "Windows", false)
    let comp2 = Computer(2, "macOS", false)
    let comp3 = Computer(3, "macOS", true)
    let comp4 = Computer(4, "other", false)
    let comp5 = Computer(5, "Linux", false)
    let comp6 = Computer(6, "Windows", false)

    let computers =
        [ comp1
          comp2
          comp3
          comp4
          comp5
          comp6 ]

    let matrix =
        [ [ comp2; comp5 ]
          [ comp1 ]
          [ comp4; comp6 ]
          [ comp3; comp5 ]
          [ comp1 ]
          [ comp3 ] ]

    let net = LocalNetwork(computers, matrix)
    net.Start()

    let results =
        computers
        |> List.map (fun computer -> computer.IsInfected)

    let expected =
       [ true; false ] @ [ for _ in 1..4 -> true ]

    results |> should equal expected
