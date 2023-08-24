module Tasks.Task3

type Operation =
    | Operand of float
    | Addition of Operation * Operation
    | Subtraction of Operation * Operation
    | Multiplication of Operation * Operation
    | Division of Operation * Operation
    
let rec parse operation =
    match operation with
    | Operand operand -> operand
    | Addition(first, second) -> parse first + parse second
    | Subtraction(first, second) -> parse first - parse second
    | Multiplication(first, second) -> parse first * parse second
    | Division(first, second) -> parse first / parse second
    