module Tasks.Task1

let isBracket symbol =
    match symbol with
    | ')'
    | ']'
    | '}' -> true
    | _ -> false

let getMatchingBracket bracket =
    match bracket with
    | ')' -> '('
    | ']' -> '['
    | '}' -> '{'

let isSeqCorrect string =
    let rec check list stack =
        match list with
        | [] -> stack |> List.isEmpty
        | head :: _ ->
            match head with
            | '('
            | '['
            | '{' -> check list.Tail (head :: stack)
            | _ ->
                if isBracket head then
                    if getMatchingBracket head = stack.Head then check list.Tail stack.Tail
                    else false
                else check list.Tail stack

    check (Seq.toList string) []
