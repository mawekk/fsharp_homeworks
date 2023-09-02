module Tests

open Tasks

open NUnit.Framework
open FsUnit

//tests for task 1
[<Test>]
let ``Rounding test with positive numbers`` () =
    RoundingBuilder 3 {
        let! a = 2.0 / 12.0
        let! b = 3.5
        return a / b
    }
    |> should equal 0.048

[<Test>]
let ``Rounding test with negative numbers`` () =
    RoundingBuilder 3 {
        let! a = -3.0 / 12.0
        let! b = 4.5
        return a / b
    }
    |> should equal -0.056

[<Test>]
let ``Rounding test with incorrect accuracy`` () =
    (fun () ->
        RoundingBuilder -6 {
            let! a = -3.0 / 12.0
            let! b = -4.5
            return a / b
        }
        |> ignore)
    |> should throw typeof<System.ArgumentOutOfRangeException>
    
//tests for task 2
[<Test>]
let ``Calculate test with correct strings`` () =
    CalculateBuilder() {
        let! x = "1"
        let! y = "3"
        let z = x * y
        return z
    }
    |> should equal (Some 3)

[<Test>]
let ``Calculate test with incorrect strings`` () =
    CalculateBuilder() {
        let! x = "1"
        let! y = "a"
        let z = x + y
        return z
    }
    |> should equal None