module PointFree

let f g l = List.map g (List.tail l)
let f1 g = (List.map g) << List.tail
let f2 g = (<<) (List.map g) List.tail
let f3 g = (>>) List.tail (List.map g)

let f4: (int -> int) -> 'a list -> 'b list =
    ((>>) List.tail) << List.map
