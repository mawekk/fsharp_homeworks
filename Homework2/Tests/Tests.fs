module Tests

open Tasks.Task1
open Tasks.Task2
open Tasks.Task3
open Tasks.Task4

open NUnit.Framework
open FsCheck
open FsUnit

[<Test>]
let ``Even numbers test``() =
    let allElementsAreEqual list =
        list |> List.forall ((=) (List.head list))

    let resultsAreEqual test =
        [ filterList; foldList; mapList ]
        |> List.map (fun func -> func test)
        |> allElementsAreEqual

    Check.QuickThrowOnFailure resultsAreEqual

let func = (fun x -> x * 10)

[<Test>]
let ``Empty tree test``() =
    treeMap func Tree.Empty
    |> should equal (Tree.Empty: Tree<int>)

[<Test>]
let ``Tree test``() =
    let tree =
        Node(
            1,
            Node(2, Node(4, Tree.Empty, Tree.Empty), Tree.Empty),
            Node(3, Node(5, Tree.Empty, Tree.Empty), Tree.Empty)
        )

    let newTree =
        Node(
            10,
            Node(20, Node(40, Tree.Empty, Tree.Empty), Tree.Empty),
            Node(30, Node(50, Tree.Empty, Tree.Empty), Tree.Empty)
        )

    treeMap func (tree: Tree<int>)
    |> should equal (newTree: Tree<int>)

let operationData () =
    [ Operand(5), 5
      Addition(Operand(20), Operand(22)), 42
      Subtraction(Multiplication(Operand(10), Operand(10)), Operand(1)), 99
      Multiplication(Operand(13), Addition(Operand(4), Operand(-1))), 39
      Division(Operand(80), Subtraction(Operand(27), Operand(7))), 4 ]
    |> List.map (TestCaseData)

[<TestCaseSource(nameof operationData)>]
let ``Parse test`` operation expected =
    parse operation |> should equal expected

let seqData () = 
    [   
        1, seq{2}
        3, seq{2; 3; 5}
        5, seq{2; 3; 5; 7; 11}
        10, seq{2; 3; 5; 7; 11; 13; 17; 19; 23; 29}
    ] |> List.map (TestCaseData)

[<TestCaseSource(nameof seqData)>]
let ``Generate seq test`` lenght seq =
    generateSeq() |> Seq.take lenght |> should equal seq