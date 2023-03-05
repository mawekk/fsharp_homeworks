let listOfPowersOfTwo n m =
    let rec listOfPowersOfTwo list m currentPowerOfTwo =
        if m = 0 then list else listOfPowersOfTwo (currentPowerOfTwo :: list) (m - 1) (currentPowerOfTwo * 2)
    List.rev <| listOfPowersOfTwo [] (m + 1) (pown 2 n)
    
printf "%A" <| listOfPowersOfTwo 10 5
