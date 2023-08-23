let reverseList list =
    let rec reverse list reversed =
        match list with
        | [] -> reversed
        | head::tail -> reverse tail (head :: reversed)
    reverse list []
    
let list = reverseList [1..5]
printf $"%A{list}"
