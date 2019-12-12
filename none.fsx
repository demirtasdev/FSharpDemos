let fizzBuzz x =
    match (x % 3), (x % 5) with
    | 0, 0 -> "FizzBuzz"
    | 0, _ -> "Fizz"
    | _, 0 -> "Buzz"
    | _ -> x.ToString()

let fizzBuzzAll () =
    [ 0 .. 100 ]
    |> List.map fizzBuzz

fizzBuzzAll () |> List.iter (printfn "%s")