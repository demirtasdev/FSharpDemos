// Now you try...
let sayHello(someValue) =
   let innerFunction(number) =
      if number > 1 then "Isaac"
      elif number > 20 then "Fred"
      else "Sara"

   let resultOfInner =
      if someValue < 10.0 then innerFunction(5)
      else innerFunction(15)

   "Hello " + resultOfInner

let result = sayHello(10.5)


let nums = [ 0 ; 1 ; 2 ; 3 ; 4 ]
let squareNums =
   nums
   |> List.map (fun i -> i * i )

printf "%A" squareNums