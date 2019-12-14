namespace FunctionalToolkit

open System.Collections.Generic

module MovingFunctionsBetweenWorldsWithM =

    let add1 x = x + 1

    let add1ToOption (opt:int option) =
        if opt.IsSome then
            let newVal = add1 opt.Value
            Some newVal
        else
            None

    // IF WE WERE TO MAKE THE ABOVE FUNCTION GENERIC...

    let optionMap f (x:int option) =
        if x.IsSome then
            Some(f x)
        else
            None  
    // Action is parameterized

    // LETS TAKE A STEP FURTHER BY RETURNING A LAMBDA EXPRESSION

    let optMap f =              
        fun (opt:int option) ->
            if opt.IsSome then
                Some (f opt.Value)
            else
                None      
    // The lambda expression that this function returns is an "Option -> Option" function
    // optMap function turns a normal world function (f) to option world function

    // This is what map is.
    
    // MAP TURNS NORMAL WORLD FUNCTIONS INTO OPTION WORLD FUNCTIONS
    printfn "%A" (optMap add1 (Some 11))


    // SAME FUNCTION FOR A LIST:
    let listMap f =
        fun aList ->
            let newList = new List<int>()
            for item in aList do
                let newItem = f item
                newList.Add(newItem)
            //return            
            newList
    // This is a List -> List function!  
    // Again it takes in a normal world function and returs a list world function 

    
    let res = (listMap add1) [1;2;3]


    // FP TERMINOLOGY
    // i. An effect type
    // `-> e.g. Option<>, List<>, Async<>
    // ii. Plus a "map" function that "lifts" a function to the effects world
    // `-> e.g. select, lift

        