let factorial n =
    let rec factorial n acc =
        if n = 0UL then acc else factorial (n - 1UL) (acc * n)
    factorial n 1UL
    
printf $"{factorial 11UL}"
