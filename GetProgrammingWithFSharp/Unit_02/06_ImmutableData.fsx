// NOW YOU TRY: MUTABLES
// 1. Initial state
let mutable petrol1 = 100.0

// 2. Modify state through mutation
let drive1(distance) =
   if distance = "far" then petrol1 <- petrol1 / 2.0
   elif distance = "medium" then petrol1 <- petrol1 - 10.0
   else petrol1 <- petrol1 - 1.0

// 3. Repeatedly modify state
drive1("far")                                 
drive1("medium")
drive1("short")

// 4. Current state
petrol1


// ABOVE BLOCK WRITTEN AS IMMUTABLE
// 1. Function explicitly dependent on state—takes 
//    in petrol and distance, and returns new petrol
let drive2 (petrol, distance) =
   if distance = "far" then petrol / 2.0
   elif distance = "medium" then petrol - 10.0
   else petrol - 1.0

// 2. Initial state
let petrol = 100.0
// 3. Storing output state in a value
let firstState = drive2(petrol, "far")
let secondState = drive2(firstState, "medium")
// 4. Chaining calls together manually
let finalState = drive2(secondState, "short")


// ABOVE BLOCK MODIFIED AS REQUIRED
let drive3 (petrol, distance) =
   if ( distance > 50 ) then petrol / 2.0
   elif ( distance > 25 && distance <= 50 ) then petrol - 10.0
   elif ( distance > 0 && distance <= 25 ) then petrol - 1.0
   else petrol

// 2. Initial state
let petrol3 = 100.0
// 3. Storing output state in a value
let firstState3 = drive3(petrol, 400)
let secondState3 = drive3(firstState3, 200)
// 4. Chaining calls together manually
let finalState3 = drive3(secondState3, 70)


// TRY THIS
// 1. State machine with immutable data:
let kettle (amountWithin, operation) =
   match operation with
   | "pourToPot" -> amountWithin - 500.0
   | "pourToCup" -> amountWithin - 200.0
   | "fillKettle" -> amountWithin + 1000.0
   | _ -> amountWithin

let initialAmountOfWater = 1500.0

let firstState4 = kettle(initialAmountOfWater, "pourToPot")
let secondState4 = kettle(initialAmountOfWater, "pourToCup")
let finalState4 = kettle(initialAmountOfWater, "fillKettle")
