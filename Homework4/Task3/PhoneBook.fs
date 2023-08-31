module PhoneBook

open Task3
open System

let commands =
    "here's what you can do:
    0 - exit
    1 - add contact
    2 - find number by name
    3 - find name by number
    4 - print all contacts
    5 - write all contacts to file
    6 - read all contacts from file"

let getName () =
    printfn "enter the name:"
    Console.ReadLine()

let getNumber () =
    printfn "enter the number:"
    Console.ReadLine()

let getFilePath () =
    printfn "enter the path to the file:"
    Console.ReadLine()

let launchCLI () =
    printfn "hi!! welcome to the coolest phone book you've ever seen :)"

    let rec getCommand phoneBook =
        printfn $"%s{commands}"

        try
            match Console.ReadLine() with
            | "0" -> ()
            | "1" ->
                let name = getName ()
                let number = getNumber ()

                let phoneBook =
                    addContact name number phoneBook

                printfn "done!"
                getCommand phoneBook
            | "2" ->
                let name = getName ()
                let result = getContactNumber name phoneBook

                printfn "%A"
                <| match result with
                   | None -> "there's no such contact :("
                   | _ -> "search result: " + result.Value
            | "3" ->
                let number = getNumber ()
                let result = getContactName number phoneBook

                printfn "%A"
                <| match result with
                   | None -> "there's no such contact :("
                   | _ -> "search result: " + result.Value
            | "4" ->
                printfn "here are all contacts:"
                printAllContacts stdout phoneBook
            | "5" ->
                let filePath = getFilePath ()
                writeContactsToFile filePath phoneBook
                printfn "done!"

            | "6" ->
                let filePath = getFilePath ()

                let phoneBook =
                    readContactsFromFile filePath phoneBook

                printfn "done!"
                getCommand phoneBook
            | _ -> printfn "i don't know this command :("
        with
        | Failure msg -> printfn $"%s{msg}"

        getCommand phoneBook

    getCommand []
