namespace FunctionalToolkit

module ChainingWorldCrossingFunctionsWithBIND =
    
    // examples of world-crossing functions:
    let range max = [ 1 .. max ]

    let getCustomer id =
        if customerFound then
            Some customerData
        else
            None  

    // chaining world-crossing OPTION types together
    let optBind f opt =
        match f with
        | Some s -> opt s
        | None -> None        

    let optEx input =
        doSomething input
        |> optBind doSomethingElse
        |> optBind doAThirdThing
        |> optionBind ...    

    // chaining world-crossing TASK types together
    let tskBind f tsk =
        tsk.WhenFinished ( fun tskRes -> f tskRes )   

    let tskEx input =
        startTask input
        |> taskBind startAnotherTask
        |> taskBind startThirdTask    

// BIND is very important in Functional Programming. It makes world-crossing functions composable.
// After we BIND, the function is horizontal and doesn't keep going back and forth the two worlds.
// This allows the linear functions to become horizontal in the effect world and their composing possible.

// ######### This is called a MONAD
