namespace FunctionalToolkit

module FunctionTransformers =
    // i: WE USE FUNCTION TRANSFORMERS TO COMPOSE DIFFERENT SHAPED FUNCTIONS
    // THEY TURN DIFFERENT SHAPED FUNCTIONS INTO SAME SHAPED FUNCTIONS

    // COMPOSING FUNCTIONS THAT HAS ONE INPUT AND TWO OUTPUTS USING TWO-TRACK APPROACH
    let TwoTrackApproach nextFunction twoTrackInput =
        match twoTrackInput with
        | Ok success -> nextFunction success
        | Error err -> Error err

    type Input = { name:string ; email:string }

    // SOME FUNCTIONS TO COMBINE
    let nameNotBlank input =
        if input.name = "" then
            Error "Name must not be blank"
        else Ok input

    let nameLessThan50 input =
        if input.name.Length > 50 then  
            Error "Name cannot be longer than 50 characters"
        else Ok input

    let emailNotBlank input =
        if input.email = "" then
            Error "Email must not be blank"    
        else
            Ok input

    // BIND to COMPOSE
    let validateInput input =
        input
        |> nameNotBlank
        |> Result.bind nameLessThan50
        |> Result.bind emailNotBlank
    // Note that we're not using Bind on the first function.
    // This is because it expects a single input, which is what we're giving it.    

    // COMPOSE ONE INPUT ONE OUPTUT FUNCTIONS with ONE INPUT TWO OUTPUT FUNCTIONS

    let map singleTrackFunction twoTrackInput =
        match twoTrackInput with
        | Ok s -> Ok (singleTrackFunction s)
        | Error e -> Error e
    

             