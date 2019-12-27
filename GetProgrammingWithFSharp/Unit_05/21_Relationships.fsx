// Listing 21.1
// -> Composition with record in F#

//Define two record types. Computer depends on the Disk.
type Disk = { SizeGb: int }

type Computer =
    { Manufacturer: string 
      Disks: Disk list}

// Create an instance of a Computer
let myPc =
    { Manufacturer = "Computers Inc."
      Disks =
        [ { SizeGb = 100 } 
          { SizeGb = 250 }
          { SizeGb = 500 } ] }
  


// Listing 21.2
// -> Discriminated Unions in F#

// base type:
type DiskDU =
// HardDisk subtype, containing two custom fields as metadata
| HardDisk of RPM:int * Platters:int
// SolidState - no custom fields
| SolidState
// MMC - single custom field as metadata
| MMC of NumberOfPins:int

// explicitly named arguments:
let hd = HardDisk(RPM = 250, Platters = 7)
// lightweight syntax
let hd2 = HardDisk(250, 7)

let args = 250, 7
// passing all values as a single argument, omitting the brackets
let myHardDiskTupled = HardDisk args

let mmc = MMC(NumberOfPins = 5)
// Creating a DU case without metadata
let ssd = SolidState

// i: notice that the values are typed as DiskDU and not the specific cases.



// Listing 21.4
// -> Writing functions for a discriminated union
let seek disk =
    match disk with
    // matches on any type of hard disk:
    | HardDisk _ -> "Seeking loudly at a reasonable speed."
    // Matches on any type of MMc:
    | MMC _ -> "Seeking quietly but slowly."
    | SolidState -> "Already found it!"
// Returns "Already found it!":
seek ssd


// Listing 21.5
// -> Pattern matching on values
let seekValues disk =
    match disk with
    // matches on any type of hard disk:
    | HardDisk (5400, 5) -> "Seeking very slowly."
    // Matches on any type of MMc:
    | HardDisk (rpm, 7) -> sprintf "I have 7 spindles and RPM %d" rpm
    | MMC 3 -> "Seeking... I have three pins. :("
    | _ -> ""
// i: This is incrediply powerful, in that the type of the object is checked
// before an attempt is made to read its values.


//NOW YOU TRY:
let describe disk =
    match disk with
    | SolidState -> "I'm a newfangled SSD."
    | MMC 1 -> "I have only 1 pin."
    | MMC pins when pins < 5 -> "I'm an MMC with a few pins."
    | MMC pins -> sprintf "I'm an MMC with %d pins." pins
    | HardDisk (5400, _) -> "I'm a slow hard disk."
    | HardDisk (_, 7) -> "I have 7 spindles."
    | HardDisk _ -> "I'm a hard disk"



// Listing 21.6
// -> Nested discriminated unions
// Nested DU with associated cases:
type MMCDisk =
| RsMmc
| MmcPlus
| SecureMMC
// Adding the nested DU to your parent case in the Disk DU:
type Disk1 =
| MMC of MMCDisk * NumberOfPins:int

let disk1 = MMC(RsMmc, 1)

match disk1 with
// Matching on both top-level and nested DUs simultaneously:
| MMC(MmcPlus, 3) -> "Seeking quietly but slowly."
| MMC(SecureMMC, 6) -> "Seeking quietly with 6 pins."
| _ -> ""



// Listing 21.7
// Shared fields using a combination of records and discriminated unions
// Composite record:
type DiskInfo =
    // Starting with common fields:
    { Manufacturer: string
      SizeGb: int
      // Varrying data with field as DU
      DiskData: DiskDU }

// Computer record---contains the manufacturer and a list of disks
type Computerr = 
    { Manufacturer: string
      Disks: DiskInfo list }

let myPC =
    { Manufacturer = "Computers Inc."
      Disks =
        // Creating a list of disks using square brackets
        [ { Manufacturer = "HardDisks Inc."
            SizeGb = 100
            // Common fields in the varying DU as a HardDisk
            DiskData = HardDisk(5400, 7) }
          { Manufacturer = "SuperDisks Corp."
            SizeGb = 250
            DiskData = SolidState } ] }



// Listing 21.8
// -> Creating an enum in F#
// Enum type:
type Printer =
// Enum cases with explicit ordinal values
| Injket = 0
| Laserjet = 1
| DotMatrix = 3
// i: Only difference to DUs is that we must give each case an explicit ordinal,
// and that we can't associate metadata with any case.
