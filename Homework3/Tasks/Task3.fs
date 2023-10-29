module Tasks.Task3

open Microsoft.FSharp.Quotations

type Variable = string

type LambdaTerm =
    | Variable of Variable
    | Application of LambdaTerm * LambdaTerm
    | LambdaAbstraction of Variable * LambdaTerm

let rec getFreeVariables term =
    match term with
    | Variable var -> Set.singleton var
    | Application (first, second) -> getFreeVariables first + getFreeVariables second
    | LambdaAbstraction (var, term) -> getFreeVariables(term).Remove(var)

let isFree var term = (getFreeVariables term).Contains(var)

let getNewName (freeVars: Set<string>) =
    let rec getNext name =
        match freeVars.Contains(name) with
        | true -> getNext (name + "'")
        | false -> name

    getNext "a"

let rec substitute firstTerm variable secondTerm =
    match firstTerm with
    | Variable var when var = variable -> secondTerm
    | Variable _ -> firstTerm
    | Application (first, second) ->
        Application(substitute first variable secondTerm, substitute second variable secondTerm)
    | LambdaAbstraction (var, _) when var = variable -> firstTerm
    | LambdaAbstraction (var, term) ->
        if not ((isFree var secondTerm) && (isFree variable term)) then
            LambdaAbstraction(var, substitute term variable secondTerm)
        else
            let newVariable =
                getFreeVariables term
                + getFreeVariables secondTerm
                |> getNewName

            let newTerm =
                substitute (substitute term var (Variable(newVariable))) variable secondTerm

            LambdaAbstraction(newVariable, newTerm)

let rec betaReduce lambdaTerm =
    match lambdaTerm with
    | Variable _ -> lambdaTerm
    | Application (first, second) ->
        match first with
        | LambdaAbstraction (var, term) -> substitute term var second
        | _ -> Application(betaReduce first, betaReduce second)
    | LambdaAbstraction (var, term) -> LambdaAbstraction(var, betaReduce term)

let rec reduce lambdaTerm =
    match lambdaTerm with
    | Application(LambdaAbstraction _, _) -> reduce (betaReduce lambdaTerm)
    | _ -> lambdaTerm
