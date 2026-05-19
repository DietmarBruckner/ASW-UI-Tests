## Page 1

Naming conventions Readability Coding practices
Meaningful names for variables C001 Indentation C101 Initialize variables C301
True to intention, no interpretation Indent code with one Tab ( = 4 spaces) Deleteunused variables anddatatypes C302
Maximum length for names C002 Whitespaces (□= one whitespace) C102 C303
Global variables
C304
Better readabillity → 24 characters Operators: Addend1□+□Addend2 Limited use only; Assign values within one program
Parenthesis: ADR('RecipeIcecream')
Avoid using abbreviations C003 Assignment: RecipeXml.Enable□:=□TRUE; Functionblockinstances C305
Function call arguments: Sum(Addend1,□Addend2)
Abbreviations used should be documented.
Call only onceper program cycle
Separation of logical blocks visually C103
Don’treuse variable names C004 Deletedead code C306
Consistent empty lines between blocks.
Same names make it difficult to debug
Avoidduplicating code C307
Define the maximum width of code C104
Define the use of case C005
Don’t Repeat Yourself, use functions, FBs, actions, etc.
Define number of characters to be used in a single line
Constants → UPPER_SNAKE_CASE
of code. Suggestion 120characters Avoidimplicit typeconversions C308
Multi-word items → UpperCamelCase
Prefixes and suffixes C006 Define the maximum length of code C105 Avoidcomparing floating points C309
C007
Set a limit for the maximum number of lines for a
Prefix → g, di, do, ai, ao Limitprogramcomplexity C310
block of code.. Goal: avoid scrolling.
Suffix → Type (StatusType), Enum (StateEnum)
Nested IF statements → maximum 4 levels
Parentheses → Consistent C106
Names of functions and function
block (FB) instances C008 Shortenbooleanassignments C311
Comments („Why“ not „How“) NO: IF PendingAlarms= 0 THEN YES: AlarmActive:=
Function: CalculateAverageValue
AlarmActive:= FALSE; (PendingAlarms> 0);
FB instance: TuneTemperatureControllerZone1
ELSE
Describeall elements C201
AlarmActive:= TRUE;
Don’t repeat context name C009 END_IF;
Programs, packages, libraries, configurations
User defined types variables, user types, enumerations Avoid using EDGE, EDGEPOS and EDGENEG C312
NO: Conveyor.Cmd.CmdStart
YES: Conveyor.Cmd.Start Consistent styleand placement C202 Release code with 0 errors and0 warnings C313
//comment instead of (* comment *)
Limit nestingof structures C010 No magic numbers. Define constants. C314
Place comments above code.
Recommended maximum of 4 levels nesting
YES: Heater.Par.PID.Gain Header C203 Use AND instead of nested IF/ELSE C315
File name, copyright, author, created on, description Avoidbooleancomparisons in conditions C316
Don’t comment out code C203 NO: IF Enable = TRUE THEN YES: IF Enable THEN
Use structures to group inputs and outputs C317