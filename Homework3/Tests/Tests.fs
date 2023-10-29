module Tests

open Tasks.Task3

open NUnit.Framework
open FsUnit

let testData () =
    [ Application(LambdaAbstraction("c", Application(Variable "c", Variable "b")), LambdaAbstraction("a", Variable "a")),
      Variable "b"

      Application(
          LambdaAbstraction(
              "f",
              LambdaAbstraction("x", Application(Variable "f", Application(Variable "x", Variable "x")))
          ),
          Variable "+"
      ),
      LambdaAbstraction("x", Application(Variable "+", Application(Variable "x", Variable "x")))

      Application(LambdaAbstraction("x", LambdaAbstraction("y", Application(Variable "x", Variable "y"))), Variable "y"),
      LambdaAbstraction("a", Application(Variable "y", Variable "a")) ]
    |> List.map TestCaseData

[<TestCaseSource(nameof testData)>]
let ``Lambda interpreter test`` term result = reduce term |> should equal result
