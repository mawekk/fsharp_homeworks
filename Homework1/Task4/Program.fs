let listOfPowersOfTwo n m =
    let rec listOfPowersOfTwo list m acc =
        if m = 0 then list else listOfPowersOfTwo (acc :: list) (m - 1) (acc * 2)
    List.rev <| listOfPowersOfTwo [] (m + 1) (pown 2 n)
    
printf "%A" <| listOfPowersOfTwo 10 5
