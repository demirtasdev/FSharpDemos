open System

let numbers = [40;5;23;2;34;5;5;1;546;356;63;145;12;]

let denominator = numbers |> Seq.head

numbers
|> Seq.filter (fun n -> n < denominator)
|> Seq.map Console.WriteLine()