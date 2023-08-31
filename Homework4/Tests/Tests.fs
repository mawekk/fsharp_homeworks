module Tests

open System.IO
open Tasks.Task1
open Tasks.Task2
open Task3

open NUnit.Framework
open FsUnit
open FsCheck

//tests for task 1
let bracketData () =
    [ "(({}))", true
      "([)]", false
      "((hi!!)){[]}{}", true
      "{[)]:(", false
      "([{}](){[]})", true
    ]
    |> List.map TestCaseData

[<TestCaseSource(nameof bracketData)>]
let ``Bracket sequence test`` string result = isSeqCorrect string |> should equal result

//tests for task 2
[<Test>]
let ``First step is correct``() =
    Check.QuickThrowOnFailure(fun x l -> (func x l) = (func1 x l))
    
[<Test>]
let ``Second step is correct``() =
    Check.QuickThrowOnFailure(fun x l -> (func x l) = (func2 x l))

[<Test>]
let ``Third step is correct``() =
    Check.QuickThrowOnFailure(fun x l -> (func x l) = (func3 x l))
    
[<Test>]
let ``Fourth step is correct``() =
    Check.QuickThrowOnFailure(fun x l -> (func x l) = (func4 x l))
   
[<Test>]
let ``Fifth step is correct``() =
    Check.QuickThrowOnFailure(fun x l -> (func x l) = (func5 x l))
    
//tests for task 3    
[<Test>]
let ``Add correct contact``() =
    let name = "bestie"
    let number = "11111111111"
    addContact name number [] |> should equal [{Name = name; Number = number}]

[<Test>]
let ``Add incorrect contact``() =
    let name = "bestie"
    let number = "oneoneone"
    (fun () -> addContact name number [] |> ignore) |> should throw typeof<System.Exception>

[<Test>]
let ``Find existing contact``() =
    let name = "bestie"
    let number = "11111111111"
    let phoneBook = [{Name = name; Number = number}]
    getContactName number phoneBook |> should equal (Some name)
    
[<Test>]
let ``Find not existing contact``() =
    let name = "bestie"
    let number = "11111111111"
    let phoneBook = [{Name = name; Number = number}]
    getContactNumber "nobody" phoneBook |> should equal None
    
[<Test>]
let ``File test``() =
    let phoneBook = [{Name = "mom"; Number = "1"}; {Name = "dad"; Number = "2"}]
    let file = "./test.txt"
    writeContactsToFile file phoneBook
    let anotherPhoneBook = [{Name = "bro!!"; Number = "3"}]
    readContactsFromFile file anotherPhoneBook |> should equal (anotherPhoneBook @ phoneBook)
    File.Delete(file)