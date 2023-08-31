module Task3

open System
open System.IO

type Contact =
    { Name: string
      Number: string }
    override this.ToString() =
        $"%s{this.Name}; %s{this.Number}"

    member this.isNumberCorrect() = this.Number |> Seq.forall Char.IsDigit

let addContact name number phoneBook =
    let contact =
        { Name = name; Number = number }

    if contact.isNumberCorrect () then
        phoneBook @ [ contact ]
    else
        failwith "incorrect number :("

let getContactName number phoneBook =
    match phoneBook
          |> List.tryFind (fun contact -> contact.Number = number)
        with
    | Some contact -> Some contact.Name
    | _ -> None

let getContactNumber name phoneBook =
    match phoneBook
          |> List.tryFind (fun contact -> contact.Name = name)
        with
    | Some contact -> Some contact.Number
    | _ -> None

let printAllContacts (stream: TextWriter) phoneBook =
    phoneBook
    |> List.iter (fun contact -> stream.WriteLine $"%s{contact.ToString()}")

let readContactsFromFile (filePath: string) phoneBook =
    if not (File.Exists filePath) then
        failwith "incorrect path :("

    use stream = new StreamReader(filePath)

    let rec readLine phoneBook =
        let line = stream.ReadLine()

        if line = null then
            phoneBook
        else
            let data = line.Split "; "

            if data.Length = 2 then
                try
                    readLine (addContact data[0] data[1] phoneBook)
                with
                | Failure _ -> readLine phoneBook
            else
                readLine phoneBook

    readLine phoneBook

let writeContactsToFile (filePath: string) phoneBook =
    try
        use stream = new StreamWriter(filePath)
        printAllContacts stream phoneBook
    with
    | :? UnauthorizedAccessException -> printfn "incorrect path :("

