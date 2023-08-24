module Tasks.Task1

let filterList list =
    list |> List.filter(fun x -> x % 2 = 0) |> List.length
    
let foldList list =
    List.fold(fun acc x -> if x % 2 = 0 then acc + 1 else acc) 0 list
    
let mapList list =
    0 - List.sum (List.map(fun x -> abs x % 2 - 1) list)