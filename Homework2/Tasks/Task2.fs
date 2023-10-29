module Tasks.Task2

type Tree<'a> =
    | Node of 'a * Tree<'a> * Tree<'a>
    | Empty
    
let treeMap func tree =
    let rec transformTree tree =
        match tree with
        | Empty -> Tree.Empty
        | Node (elem, left, right) -> Node(func elem, transformTree left, transformTree right)
    transformTree tree