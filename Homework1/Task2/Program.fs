let fibonacci n =
    let rec fibonacci n first second =
        match n with
        | 0UL -> first
        | 1UL -> second
        | _ -> fibonacci (n - 1UL) second (first + second)
    fibonacci n 0UL 1UL
    
printf $"{fibonacci 10UL}"
