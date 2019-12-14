namespace FunctionalToolkit

module MoveValuesBetweenWorldsWithRETURN =
    
    // normal world value:
    let x = 42
    // lift to option world value:
    let intOption = Some x

    // normal world value:
    let y = 42
    // lift to list world value:
    let intList = [y]