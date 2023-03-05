let findFirstEntrance element list =
    let rec findFirstEntrance element list index =
        match list with
        | [] -> None
        | head::_ when head = element -> Some index
        | _::tail -> findFirstEntrance element tail (index + 1)
    findFirstEntrance element list 0

printf $"{(findFirstEntrance 5 [1; 2; 5; 3; 5]).Value}"
