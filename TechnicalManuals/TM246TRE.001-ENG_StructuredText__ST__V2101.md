## Page 1

TM246

Structured Text

## Page 2

2 STRUCTURED TEXT TM246
Requirements
Training modules: TM210 – Working with Automation Studio
TM213 - Automation Runtime
TM223 – Automation Studio diagnostics
Software Automation Studio 3.0.90 or later
Hardware None

## Page 3

TABLE OF CONTENTS 3
Table of contents
1 Introduction.........................................................................................................................................................4
1.1 Learning objectives..............................................................................................................................4
2 General information...........................................................................................................................................5
2.1 Structured Text features.....................................................................................................................5
2.2 Editor functions....................................................................................................................................5
3 Basic elements....................................................................................................................................................7
3.1 Expressions............................................................................................................................................7
3.2 Assignments..........................................................................................................................................7
3.3 Source code documentation - Comments......................................................................................7
3.4 Operator priorities..............................................................................................................................8
3.5 Reserved keywords..............................................................................................................................9
4 Command groups............................................................................................................................................10
4.1 Boolean operators.............................................................................................................................10
4.2 Arithmetic operations.......................................................................................................................11
4.3 Data type conversion........................................................................................................................12
4.4 Comparison operators and decisions...........................................................................................14
4.5 State machines - CASE statement.................................................................................................16
4.6 Loops....................................................................................................................................................17
5 Functions, function blocks and actions......................................................................................................23
5.1 Calling functions and function blocks...........................................................................................23
5.2 Calling actions....................................................................................................................................24
6 State diagrams.................................................................................................................................................26
6.1 Types of logic.....................................................................................................................................26
6.2 State diagrams...................................................................................................................................29
6.3 Implementation..................................................................................................................................30
6.4 Summary..............................................................................................................................................33
7 Exercises............................................................................................................................................................34
7.1 Exercise - Box lift................................................................................................................................34
7.2 Exercise - Dispersion mixer..............................................................................................................35
8 Summary............................................................................................................................................................36
9 Appendix............................................................................................................................................................37
9.1 Exercise solutions..............................................................................................................................37
9.2 Further information..........................................................................................................................43

## Page 4

4STRUCTURED TEXT  TM246

1Introduction

Structured Text is a high-level programming language. Elements from the languages BASIC, PASCAL and ANSI C were

used for the concept. Thanks to its easily comprehensible standard constructs, Structured Text (ST) is a fast and effi-

cient type of programming for the automation sector.

The following chapters provide information about commands, keywords and the syntax that can be used in Standard

Text. These functionalities can be applied in simple examples to make them easier to understand.

1.1Learning objectives

This training module uses selected exercises to help participants learn the basics of high-level language programming

with Structured Text (ST).

Using the high-level language editor and Automation Studio's SmartEdit features.

•

Learning to work with basic elements of high-level language programming and apply commands in structured

•

text.

Learning to recognize reserved keywords and use them during programming.

•

Learning to program command groups and arithmetic functions.

•

Learning to use comparison operators and Boolean links.

•

Applying elements to program flow control.

•

Using actions, functions and function blocks and learning how to use them in different ways.

•

Creating state diagrams and implement them in source code.

•

## Page 5

GENERAL INFORMATION5

2General information

2.1Structured Text features

General information

ST is a textual high-level programming language for programming automation systems. Simple standard constructs

allow a fast and efficient way of programming. ST makes use of many traditional characteristics of high-level program-

ming languages, including the use of variables, operators, functions and elements for program flow control.

The programming language ST is standardized according to IEC 1

Properties

Structured Text is characterized by the following properties:

Textual high-level programming language

•

Structured programming

•

Easy-to-use standard constructs

•

Fast and efficient programming

•

Self-explanatory and flexible use

•

Conforms to the IEC 61131-3 standard

•

Problems structuring applications and software?

If you need help with structuring or naming applications and variables, our guidelines offer a structured

approach:

TM231 - B&R Coding Guidelines

TM233 - B&R Application Design Guidelines

2.2Editor functions

The editor is a text editor with many additional functions. Commands and keywords are displayed in color. Areas can

be opened and closed. Autocomplete is integrated for variables and constructs (SmartEdit).

Figure 1: Application program in the ST editor

The editor has the following functions and features:

Differentiates between upper and lower case letters (case sensitive).

•

Autocomplete (SmartEdit, <CTRL> <SPACE>, <TAB>)

•

Manages and inserts code snippets (<CTRL> +q, k).

•

Identifies corresponding pairs of brackets.

•

Collapses or expands the construct (outlining).

•

Inserts block comments.

•

URL detection

•

Markers for modified rows

•

1The IEC 61131-3 standard is a worldwide valid standard for programming languages of programmable logic controllers. In addition to Structured Text, the programming languages

Sequential Function Chart, Ladder Diagram, Instruction List and Function Block Diagram are defined.

## Page 6

6 STRUCTURED TEXT TM246
Programming \ Editors \ General operations \ SmartEdit
Programming \ Editors \ General operations \ Smart Edit \ Code snippets
Programming \ Programs \ Structured Text (ST)
The variable declaration editor supports the initialization of variables, constants, user data types and structures. In
addition, the variables used can be commented on and thus documented. The declaration editors also support the
SmartEdit function.
Programming \ Editors \ Table editors - General \ Declaration editors

## Page 7

BASIC ELEMENTS7

3Basic elements

The following chapter describes the basic elements of ST in more detail. Among other things, expressions, assign-

ments, the use of comments in the program code and reserved keywords are explained.

3.1Expressions

An expression is a construct that returns a value after it has been calculated. Expressions are made up of operators and

operands. An operand can be a variable, constant or function call. The operators connect the operands (3.4 "Operator

priorities"). Every expression, regardless of whether it is a function call or an assignment, must end with a semicolon

(";").

Various different expressions

b + c;

(a - b + c) * COS(b);

SIN(a) + COS(b);

3.2Assignments

The assignment consists of a variable on the left side, which is assigned with the assignment operator ":=", the result

of a calculation or an expression on the right side. All statements must end with a semicolon (";").

The assignment is done from right to left.

←

// Result (ProcessValue * 2)

Result := ProcessValue * 2;

When the line of code has been processed, the value of the variable "Result" is twice the value of the

variable "ProcessValue".

Bitwise access

With assignments, individual bits of variables can also be addressed. A dot (".") is placed behind the variable. The

access is then carried out via the bit number, beginning with 0. Constants can also be used in place of the bit number.

Access to the second bit of "ProcessValue"

Result := ProcessValue.1;

3.3Source code documentation - Comments

Comments are an important part of the source code. They describe the code and make it easier to understand and read.

Comments allow you or others to understand a program even after a long time. They are not compiled and have no

influence on the program execution. Comments must be placed between a pair of parentheses and asterisks "(*com-

ment*)".

An additional comment variant is introduced with "//". Multiple lines can be marked in the editor and commented out

using an icon from the editor bar. This variant represents an extension to the existing IEC standard.

Figure 2: Setting text block as a comment

## Page 8

8STRUCTURED TEXT  TM246

Single-line comment

(*This is a single line of comment.*)

(*

These

Multi-line comment

are several

lines of comment.

*)

// This is a general

Comment with "//"

// text block.

// It contains comment.

Table 1: Comment variants

Programming \ Editors \ Text editors \ Commenting and uncommenting a selection/row

Programming \ Structured software development \ Program layout

3.4Operator priorities

The use of different operators raises the question of precedence. The resolution of an expression is determined by

the precedence (binding) of the operators.

Expressions are resolved according to the operators, starting with the highest precedence. Operators with the same

precedence are resolved from left to right as written in the expression.

OperatorSyntax

ParenthesesHighest precedence

()

Function call

Call(Argument)

Exponent

**

Negation

NOT

Multiplication, division, modulo division

*, /, MOD

Addition, subtraction

+, -

Comparisons

<, >, <=, >=

Equality, inequality

=, <>

Boolean AND

AND

Boolean XOR

XOR

Boolean ORLowest precedence

OR

The expression is already being resolved by the compiler. The following examples show that different results can be

achieved using parameters.

## Page 9

BASIC ELEMENTS 9
Resolution of an expression without parentheses:
// Result equals 38
Result := 6 + 7 * 5 - 3;
The multiplication is performed first, followed by the addition. The subtraction is performed last.
Resolution of an expression with parentheses:
// Result equals 26
Result := (6 + 7) * (5 - 3);
The expression is resolved from left to right. The operations in parentheses are calculated first, followed
by the multiplication; this is because the content in the parentheses has a higher precedence than the
multiplication. You can see that parentheses lead to different results.
3.5 Reserved keywords
All variables must follow the naming conventions in the programming. Furthermore, there are reserved
keywords that are already recognized as such by the editor and are colored. These cannot be used as
variables.
The OPERATOR and AsIecCon libraries are part of the standard scope of a new project. The functions
contained therein are IEC functions; these are interpreted as keywords.
In addition, the standard also defines literals for numbers and character strings. This makes it possible
to represent numbers in different formats.
Programming \ Structured software development \ Naming conventions
Programming \ Standards \ Literals in IEC languages
Programming \ Programs \ Structured Text (ST) \ Keywords
Programming \ Libraries \ IEC 61131-3 functions

## Page 10

10STRUCTURED TEXT  TM246

4Command groups

The following command groups represent the basic constructs of high-level language programming. These constructs

can be flexibly combined and nested within one another.

A differentiation is made between the following command groups:

4.1 "Boolean operators"

•

4.2 "Arithmetic operations"

•

4.4 "Comparison operators and decisions"

•

4.5 "State machines - CASE statement"

•

4.6 "Loops"

•

4.1Boolean operators

Boolean operators can be used for the binary linking of variable values. A distinction is made between NOT, AND, OR

and XOR. The operands do not necessarily have to be of data type BOOL. Operator priorities should be taken into

consideration. Parentheses can be used.

SymbolLogical operationExamples are:

Binary negation

a := NOT b;NOT

Logical AND

a := b AND c;AND

Logical OR

a := b OR c;OR

Exclusive OR

a := b XOR c;XOR

Table 2: Overview of Boolean operators

The truth table for the operations looks like this:

InputANDORXOR

00000

01011

10011

11110

Table 3: Truth table for Boolean operators

Boolean operators can be combined in any way. Additional sets of parentheses increase program readability and en-

sure that the expression is solved correctly. The only possible results of the expression are TRUE (logical 1) or FALSE

(logical 0).

Boolean operator - Comparison between Ladder Diagram and Structured Text

Figure 3: Linking normally open and closed contacts

Implementation with Boolean operators

doValveSilo1 := diSilo1Up AND NOT doValveSilo2 AND NOT doValveSilo3;

No parentheses are needed since the NOT has a higher binding than the AND. For clearer programming,

however, it is important to use parentheses.

## Page 11

COMMAND GROUPS11

Exercise: Lighting control

The output "DoLight" should be ON when the "ButtonLightOn" button is pressed and should remain ON until the "But-

tonLightOff" button is pressed. Complete this exercise using Boolean operators.

Figure 4: On-Off switch, relay with latch

4.2Arithmetic operations

A decisive advantage of high-level programming languages is the simple handling of arithmetic operations.

Overview of arithmetic operations

Structured Text provides basic arithmetic operations for the application. It is important to pay attention to the oper-

ator precedences in the application. Multiplication is performed before addition, for example. The desired behavior

can be achieved by using parentheses.

SymbolArithmetic operationExample

Assignment

:=a := b;

Addition

+a := b + c;

Subtraction

-a := b - c;

Multiplication

*a:= b *c;

Division

/a := b / c;

Modulo, integer division remainder

MODa := b MOD c;

Table 4: Overview of arithmetic operations

The data type of variables and values is always decisive for calculations. The result is calculated on the right side of

the expression and then assigned to the result variable. The result depends on the data types used and the syntax

(notation). The following table illustrates this fact.

Data types

Expression/SyntaxResult

ResultOperand1Operand2

INTINTINT2

Result := 8 / 3;

REALINTINT2.0

Result := 8 / 3;

REALREALINT2.66667

Result := 8.0 / 3;

INTREALINT*Error

Result := 8.0 / 3;

Table 5: Conversion implicitly performed by the compiler

The result value "*Error" stands for the compiler error message "

Error 1140: Incompatible data types:

" since it is not possible to assign the expression to the data type for the result.

Cannot convert REAL to INT.

## Page 12

12 STRUCTURED TEXT TM246
4.3 Data type conversion
When programming, one is inevitably confronted with different types of data. In principle, you can mix the data types
in the program. Different types of assignments are also possible. Caution is required, however.
Implicit data type conversion
Whenever an assignment should be made in the program code, the compiler checks the data types. Assignments are
always made from right to left in the statement. The result must therefore find room in the result variable. A conversion
from a small data type to a larger one is thus performed implicitly by the compiler without a request from the user.
If an attempt is made to assign a large data type to a small data type, a compiler error occurs. An explicit data type
conversion is required.
Depending on the compiler and constellation, an incorrect result can be obtained despite implicit data
type conversion. A hazard may occur when assigning unsigned data types to signed data types.
A value overflow may occur during addition or multiplication. This is platform dependent. No warning is
issued by the compiler.
Expression Data types Note
UINT, USINT Can easily be converted implicitly
// UINT USINT
Result := Value;
INT, UINT Be careful with negative num-
// INT UINT
bers!
Result := Value;
UINT, USINT, USINT Risk of range overflow during ad-
// UINT USINT USINT
dition
Result := Value1 + Value2;
Table 6: Examples of implicit data type conversions
Explicit data type conversion
Although implicit data type conversion is often the more convenient method, it should not always be the first choice.
Clean programming necessitates that types are handled correctly using explicit data type conversion. The examples
below highlight some of the cases where explicit conversion is necessary.
All variable declarations are listed in IEC format. The declaration must be entered in this format in the
text view for the program's VAR file.

## Page 13

COMMAND GROUPS13

There is already a risk of an overflow when the addition is carried out:

VAR

TotalWeight: INT;

Declaration:

Weight1: INT;

Weight2: INT;

END_VAR

Program code:

TotalWeight := Weight1 + Weight2;

Table 7: An overflow can occur to the right of the assignment operator

In the case described, the result data type must be able to record the range of values that was increased

by the addition. A larger result data type is therefore necessary. During addition, the data type must be a

larger data type for at least one operand. The second operand is then implicitly converted by the compiler.

VAR

TotalWeight: DINT;

Declaration:

Weight1: INT;

Weight2: INT;

END_VAR

Program code:

TotalWeight :=  INT_TO_DINT (Weight1) + Weight2;

Table 8: With explicit conversion an overflow cannot happen

On 32-bit platforms, the compiler converts the operands to 32-bit for calculation. In this case, no value

overflow occurs during the addition.

Programming \ Variables and data types \ Data types \ Basic data types

Programming \ Libraries \ IEC 61131-3 functions \ AsIecCon

Programming \ Editors \ Text editors

Programming \ Editors \ Table editors - General \ Declaration editors \ Table editor for variable decla-

ration

Exercise: Aquarium

Using analog sensors (data type INT), the temperature of

an aquarium is measured at two different points. The sen-

sors are calibrated so that the maximum value of the sen-

sor (32767) corresponds to a temperature of 100° Celsius.

Create a program that calculates the average temperature

value in degrees Celsius and stores it in a variable of type

REAL with the correct decimal places.

Figure 5: Aquarium

VAR

aiTemperatureTop: INT;

Declaration:

aiTemperatureBottom: INT;

AverageTemperatureCelsius: REAL;

END_VAR

## Page 14

14STRUCTURED TEXT  TM246

4.4Comparison operators and decisions

Structured Text provides simple constructs for comparing variables. These return the value TRUE or FALSE. The com-

parison operators and logical operations are mainly used as conditions for statements such as IF, ELSIF, WHILE and

UNTIL.

SymbolComparative expressionExample

Equal to

IF a = b THEN=

Not equal to

IF a <> b THEN<>

Greater than

IF a > b THEN>

Greater than or equal to

IF a >= b THEN≥

Less than

IF a < b THEN<

Less than or equal to

IF a <= b THEN≤

Table 9: Overview of the logical comparison operators

Decisions

The IF statement is used for decisions in the program. You are already familiar with the comparison operators. These

can be used here.

KeywordsSyntaxDescription

1. Compare

IF a > b THENIF .. THEN

Instruction if 1. Comparison TRUE

Result := 1;

2. Compare

ELSIF a > c THENELSIF .. THEN

Instruction if 2. Comparison TRUE

Result := 2;

Alternate branch when no comparison is

ELSEELSE

TRUE

Statement of the alternate branch

Result := 3;

End of the decision

END_IFEND_IF

Table 10: Syntax of the IF statement

Decisions are mapped in the program using IF statements. Comparison operators are used for this purpose. If a con-

dition is true, the associated source code is executed. All other conditions are then no longer queried.

Figure 6: IF - ELSE statement

Figure 7: IF - ELSIF - Else statement

## Page 15

COMMAND GROUPS15

The comparison expressions can be linked with boolean operators so that several conditions can be queried simulta-

neously.

If "a" is greater than "b" and if "a" is less than "c", then "Result" equals 100Explanation:

IF (a > b) AND (a < c) THEN

Program code:

Result := 100;

END_IF;

Table 11: Using multiple comparison expressions

A new IF statement can be embedded in any IF statement. It is important to ensure that there are not too many nesting

levels; otherwise, the program becomes confusing.

Further notes and guidelines can be found in TM231 - C310 rule.

The editor's SmartEdit function can be used to simplify entering the code. If an IF statement is required,

just type "IF" and press the <> key. The basic framework of the IF statement is automatically addedTAB

to the editor.

Programming \ Programs \ Structured text (ST) \ IF statement

Programming \ Editors \ General operation \ SmartEdit settings

Exercise: Weather station - Part 1

A temperature sensor measures the outside temperature. The temperature

is read via an analog input and should be displayed in text form in the house.

1)If the temperature is below 18°C, "Cold" should be displayed.

2)If the temperature is between 18°C and 25°, "Optimal" should be dis-

played.

3)If the temperature is above 25°C, then "Hot" should be displayed.

Create a solution for this application by using IF, ELSIF and ELSE statements.

Figure 8: Thermometers

To output a text, you need a variable with the data type STRING. The assignment can look like this:

Show-

Text := 'COLD';

Exercise: Weather station - Part 2

Evaluate the humidity in addition to the temperature.

The text "Optimal" should only appear if the humidity is between 40% and 75% and the temperature is between 18°C

and 25°C; otherwise, "Temp. OK" should be displayed.

Extend your statement from Part I with a nested IF statement.

## Page 16

16STRUCTURED TEXT  TM246

If several IF statements check the same variable value, it must be checked whether the request cannot

be solved mo re elegantly and clearly with the CASE statement.

Compared to the IF statement, the CASE statement has the further advantage that comparisons are only

made once, thus creating more effective program code.

4.5State machines - CASE statement

The CASE statement compares a variable with several values. If one of these comparisons is true, the statements

associated with the step in question are executed. If none of these comparisons apply, there is an ELSE branch, similar

to the IF statement, whose program code is processed in this case.

Depending on the application, the CASE statement is also used as a construct for the implementation of state ma-

chines.

KeywordsSyntaxDescription

Start of CASE statement

CASE Step OFCASE .. OF

For 1 and 5

1,5:

Show := MATERIAL;

For 2

2:

Show := TEMP;

For 3, 4, 6, 7, 8, 9 and 10

3, 4, 6..10:

(6..10 = range from 6 to 10)         Show := OPERATION;

Alternate branch

ELSEELSE

(*...*)

End of CASE

END_CASEEND_CASE

Only one step of the CASE statement is processed in a program cycle.

The step variable must be an integer data type.

Figure 9: Overview - CASE statement

Constants or elements of enumeration data types should be used in the program code rather than fixed

numerical values. Because a text is used in the program code instead of a value, it is easier to read. If

values have to be changed in the program, a change in the declaration is necessary, but not in the program

code.

## Page 17

COMMAND GROUPS17

Programming \ Programs \ Structured Text (ST) \ CASE statement

Programming \ Variables and data types \ Variables \ Constants

Programming \ Variables and data types \ Data types \ Derived data types \ Enumerators

Exercise: Fill-level control

The level in a container should be monitored in three

ranges: Low, ok and high.

The fill level of the tank is indicated on the outside by

three lamps. The corresponding lamp lights up depend-

ing on the fill level. If the content drops below 1%, an

acoustic alarm should also sound.

The level is read via an analog value (0 - 32767) and

should be converted internally to 0-100%.

Create a solution for this application by using the CASE

statement.

Figure 10: The fill level of the tank should be monitored.

VAR

aiLevel : INT;

PercentLevel : UINT;

doLevelLow : BOOL;

Declaration:

doLevelOk : BOOL;

doLevelHigh : BOOL;

doAlarm : BOOL;

END_VAR

4.6Loops

In many applications, code parts must be processed repeatedly in the same cycle. This type of processing is also called

"loop". The code in the loop is executed until a defined terminating condition is met.

Loops serve to make programs clearer and shorter. The expandability of programs is also an issue here.

Depending on how a program is structured, it could happen that an error in the program does not leave the loop until

CPU time monitoring responds. In order to avoid such endless loops, a path must always be provided that terminates

the loop after a defined number of repetitions.

Among other things, head and foot-controlled loops are often referred to.

Loops where control begins at the top (FOR, WHILE) check the terminating condition before entering the loop. Loops

where control begins at the bottom (REPEAT) check the condition at the end of the loop. These will always be cycled

through at least once.

## Page 18

18STRUCTURED TEXT  TM246

4.6.1FOR statement

The FOR statement is used to execute a limited number of repetitions of a program part. The WHILE and REPEAT loops

are used for applications where the number of cycles cannot be permanently defined.

KeywordsSyntax

2FOR .. TO .. BY .. DO

FOR i:= StartValue TO StopValue BY Step DO

Result := Result + 1;

END_FOREND_FOR

Table 12: Elements of the FOR statement

The loop counter "Index (abbr.: i)" is preinitialized with the start value "Start-

Value". The loop is repeated until the end value "StopValue" is reached. The

loop counter is always increased by 1, or by "BY step". If a negative numerical

value is used as step size "Step", the loop counter counts backwards.

The loop counter, the start value and the end value must have the same whole

number data type. This can also be achieved by explicit data type conversion

(4.3 "Data type conversion").

If the start and end values are already the same at the beginning,

this loop type is always run through at least once! (for example,

if the start and end value are equal to 0)

Programming \ Programs \ Structured Text (ST) \ FOR state-

ment

Figure 11: Overview - FOR statement

2Specifying the keyword "BY" is optional.

## Page 19

COMMAND GROUPS19

Exercise: Total crane load

There are five load receptors on one crane. The load receptors are each connected to an analog input and provide

values in the range from 0 to 32767. To determine the total load and the average value, the individual loads must first

be totaled and divided by the number of load receptors. Complete the exercise using a FOR statement.

Figure 12: Crane with five load receptors

VAR

Weights : ARRAY [0..4] OF INT;

Counter : USINT;

Declaration:

TotalWeight : DINT;

AverageWeight : INT;

END_VAR

Arrays are required for completing this exercise. For additional information about fields, see the Automa-

tion Help or in TM251.

If possible, use constants for field declarations and for limiting loop end values. This improves the read-

ability of declarations and programs. Changes can be made more easily.

Programming \ Variables and data types \ Data types \ Derived data types \ Arrays

Exercise: Total crane load - Improve the program code

As a result of the previous task, the sum of the individual loads could be calculated using a loop. Up to now, fixed

numerical values have been included in the variable declaration and in the program code. The purpose of this exercise

is to replace as many fixed numerical values as possible (from declaration and program code) with constants.

VAR CONSTANT

Declaration:

MAX_INDEX : USINT := 4;

END_VAR

## Page 20

20STRUCTURED TEXT  TM246

4.6.2WHILE statement

Unlike the FOR statement, the WHILE loop does not have a loop counter. This loop type is called as long as a condition or

expression is TRUE. It is important to ensure that the loop has an end so that no cycle time violation occurs at runtime.

KeywordsSyntax

WHILE i < 4 DOWHILE .. DO

Result := Value + 1;

i := i + 1;

END_WHILEEND_WHILE

Table 13: Calling the WHILE statement

The statements are executed repeatedly as long as the condition is TRUE. If

the condition is already FALSE during the first evaluation, the statements are

never executed.

Figure 13: Overview - WHILE statement

Programming \ Programs \ Structured Text (ST) \ WHILE statement

## Page 21

COMMAND GROUPS21

4.6.3REPEAT statement

The REPEAT loop differs from the WHILE loop in that the terminating condition is checked only after the loop has been

executed. This means that the loop runs at least once, regardless of the terminating condition.

KeywordsSyntax

REPEATREPEAT

// program code

i := i + 1;

UNTIL i > 4UNTIL

END_REPEATEND_REPEAT

Table 14: Calling the REPEAT statement

The statements are executed until the UNTIL condition is TRUE. If the UNITIL

condition already returns TRUE during the first evaluation, the statements

are executed one time only.

If the UNTIL condition never assumes the value TRUE, the state-

ments are repeated endlessly, causing a runtime error.

Figure 14: Overview of the REPEAT statement

Programming \ Programs \ Structured Text (ST) \ REPEAT statement

## Page 22

22STRUCTURED TEXT  TM246

4.6.4EXIT statement

The EXIT statement can be used for all loop types before their terminating condition applies. If EXIT is called, the loop

is aborted.

KeywordsSyntax

REPEAT

IF SetExit = TRUE THEN

EXIT;EXIT

END_IF

UNTIL i > 5

END_REPEAT

As soon as the EXIT statement is called in the loop, the loop is terminated,

regardless of whether the terminating condition or the end value of the loop

was reached. In nested loops, the system terminates the loop in which the

EXIT statement occurs.

Figure 15: EXIT statement terminates the inner

loop.

Programming \ Programs \ Structured Text (ST) \ EXIT statement

Exercise: Search with abort

A certain number should be selected from a list of 100 numbers. The list contains random numbers. If the number 10

is found, the search is aborted. It is possible, however, that the number is not available in the list.

Use the REPEAT and EXIT statements for the solution. Pay attention to the two terminating conditions.

VAR

Declaration:

Values : ARRAY[0..99] OF INT;

END_VAR

The individual elements of fields can be preinitialized with values in the program code or in the variable

declaration.

## Page 23

FUNCTIONS, FUNCTION BLOCKS AND ACTIONS23

5Functions, function blocks and actions

Various functions and function blocks add system-specific functionality to a programming language. Actions are used

to give the program a better structure. Functions and function blocks can be added using the toolbar.

Figure 16: Adding functions and function blocks via the menu bar

5.1Calling functions and function blocks

Functions

Functions are like subroutines that return a value when called. A function can be called in an expression, for example.

The command line parameters, also called arguments, are the values that are passed to a function. They are transferred

separated by commas.

VAR

SineResult : REAL;

Declaration:

Value : REAL;

END_VAR

Value := 3.14159265;

Program code:

SineResult := SIN(Value);

Table 15: Calling the SIN() function with transfer parameter "Value"

Programming \ Programs \ Structured Text (ST) \ Calling functions

Function blocks

A function block is distinguished by the fact that they have multiple command line parameters and can return multiple

results.

Unlike a function, a function block requires the declaration of an instance vari-

able that corresponds to the data type of the function block. This has the ad-

vantage that a function block can also calculate a result over multiple cycles

for more complex tasks. By using different instances, multiple function blocks

of the same type can be called and supplied with different transfer parame-

ters.

Figure 17: Passing parameters (orange) and

results (black) of the TON() function block

When calling, it is possible to choose to transfer only some of the transfer parameters or all of them. The parameters

and results can be accessed in the program code using the elements of the instance variable.

VAR

diButton : BOOL;

Declaration:

doBell : BOOL;

Timer : TON;

END_VAR

Timer(IN := diButton, PT := T#1s);

Call variant 1:

doBell := Timer.Q;

Table 16: Debouncing a button with the TON() function block

## Page 24

24 STRUCTURED TEXT TM246
// parameters
Timer.IN := diButton;
Timer.PT := T#1s;
Call variant 2:
// call function block
Timer();
// read results
doBell := Timer.Q;
Table 16: Debouncing a button with the TON() function block
In call variant 1, all parameters are transferred directly when the function block is called. In call variant 2, the parameters
are assigned to the elements of the instance variable. In both cases, the desired result must be read from the instance
variable after the call has been made.
Programming \ Programs \ Structured Text (ST) \ Calling function blocks
Exercise: Function blocks
Call some of the function blocks in the STANDARD library. Before doing so, have a look at the function and parameter
descriptions found in Automation Help.
1) Call TON switch-on delay.
Setting the variable "diSwitch" should start the timer.
2) Call the CTU upward counter.
Each rising edge that results from setting the variable "diCountlmpuls" should increase the upward counter by 1.
Setting the variable "diReset" should reset the CV output to 0.
VAR
TestTimer : TON;
TestCounter : CTU;
Declaration:
diSwitch : BOOL;
diCountImpulse : BOOL;
diReset : BOOL;
END_VAR
For a more detailed description of the library function, see Automation Help. Pressing <F1> opens the
help documentation for the selected function block.
There are application examples for many libraries. These can be imported directly into the Automation
Studio project.
Programming \ Libraries \ IEC 61131-3 functions \ STANDARD
Programming \ Examples
5.2 Calling actions
An action is a program section that can be added to programs and libraries. They represent another way to structure
programs and can be created in a programming language different from the program being called. Actions are iden-
tified by their name.
Calling an action is very similar to calling a function. There are no transfer parameters and no return value, however.
If a CASE statement is used to control a more complex process, the content of the individual CASE steps
can be outsourced to actions. This keeps the main program compact. If the same functionality is required
again in another place, it simply needs to be called again.

## Page 25

FUNCTIONS, FUNCTION BLOCKS AND ACTIONS 25
CASE Sequence OF
WAIT:
IF CmdStartProcess = 1 THEN
Sequence := START_PROCESS;
END_IF
START_PROCESS:
// machine startup
StartProcess;
Program:
IF ProcessDone = 1 THEN
Sequence := END_PROCESS;
END_IF
END_PROCESS:
// machine shutdown
EndProcess;
// ...
END_CASE
ACTION StartProcess:
Action: // add your sequence code here
ProcessDone := 1;
END_ACTION
Table 17: Calling actions in the main program
Programming \ Actions

## Page 26

26STRUCTURED TEXT  TM246

6State diagrams

One of the most common tasks for software engineers in the field of automation is programming a machine or system

logic that defines the general function of the machine or system. Machine or system logic is normally described on a

textual level, which is often imprecise.

State diagrams can be used to precisely describe machine or system logic in a formal way. Additionally, state diagrams

are often a practical way to formally discuss machine logic. After the state diagram for the desired logic is ready,

implementing it is no longer a major problem.

This chapter should introduce you to state diagrams as a powerful method for analyzing and describing machine se-

quences and system procedures.

All program sections are shown in ST. A state diagram can be converted into source code analogously in any high-level

textual language.

Example: Water tank with temperature control

For the example, a water tank should be filled via . Digital sensor  provides a

doPumpdiLevelSwitch

signal when a specified fill level is exceeded. A heater can be switched on with . The current

doHeater

temperature is reported using a sensor .

aiCurrentTemp

Figure 18: Water tank with temperature control

6.1Types of logic

6.1.1Combinational logic

Combinational logic always generates output values directly from a logical combination of the current input values.

Combinational logic has no "memory" of previous events or sensor values.

## Page 27

STATE DIAGRAMS 27
Example: Water tank with temperature control
Water is drained when an external valve is opened. The water level in the tank should be kept constant
by a pump that is switched on as soon as the water falls below a certain level. This level is monitored
by the level sensor.
doPump := NOT diLevelSwitch;
The temperature of the water should be kept at value SetTemp using a heating element. The current
water temperature aiCurrentTemp is measured with a temperature sensor. Heating element do-
Heater is switched on when the water level is OK and the temperature is below value SetTemp:
doHeater := diLevelSwitch AND (aiCurrentTemp < SetTemp);
Disadvantages:
If the water level falls below the level sensor, the pump is switched on. The pump is then switched
•
off when the water rises. This means that the pump is continuously switched on and off, which has
a negative effect on the service life of the pump over the long term.
The same applies to the heating element: If it is switched on, the water temperature rises, which
•
means that the heating element is switched off again. The water cools down and the heating ele-
ment is switched on again. This in turn, reduces the service life of the heating element.
In order to eliminate these disadvantages, which result from insufficient "memory" of the water tank temperature
control history, the sequential logic is introduced in the next section.
6.1.2 Sequential logic
When using sequential logic, the state of the output signals no longer depends only on the current state of the input
signals but on their history as well. There are "internal memories" (e.g. in the form of variables) that contain this history.

## Page 28

28STRUCTURED TEXT  TM246

Example: Water tank with temperature control

If the fill level sensor responds, the pump is not switched off immediately, but only after a defined delay

time .

DelayTime

Figure 19: Delay time when switching off the pump

A hysteresis is also added to the switching threshold for switching off the heating element. It is not

switched off immediately when  reaches the value of , instead, only when

aiCurrentTempSetTemp

.

aiCurrentTemp > (SetTemp + DeltaTemp)

Figure 20: Temperature hysteresis

Result:

Outputs  and  are no longer derived solely from the current value of inputs

doPumpdoHeater

and .

diLevelSwitchaiCurrentTemp

The pump can now also be switched on if . Whether the pump is switched

diLevelSwitch = 1•

on or off depends on the history of . If the time elapsed since the last positive

diLevelSwitch

edge at input  is less than , the pump is switched on.

diLevelSwitchDelayTime

If the value of  is in temperature corridor , whether or not the heat-

aiCurrentTempDeltaTemp•

ing is switched on depends on the previous history of . If

aiCurrentTempaiCurrentTemp

has entered the temperature corridor "from below" (), the heating is switched on; oth-

< SetTemp

erwise, it is not.

In order to describe the behavior of the sequential logic, additional variables are required that contain the stored

information. These variables are called status variables and they contain the state in which the logic is currently located.

## Page 29

STATE DIAGRAMS29

6.2State diagrams

State diagrams offer the possibility to visualize sequential logic simply and clearly. There are different states (different

values of the state variable). A state transitions to different state when a defined event (e.g. a sensor signal) occurs.

For each state, there are defined actions that are executed in the respective state.

Figure 21: General illustration of a state diagram (, black: Action, , )blue: Stategreen: Transitionred: Event

## Page 30

30STRUCTURED TEXT  TM246

Example: Water tank with temperature control

In the case of tank regulators , there are two independent state variables:

PumpState

•

TemperatureState

•

Independent state diagrams must be created for both variables:

Pump control

The state diagram for controlling the pump consists of three states (,  and

PUMP_OFFPUMP_ON

). The events that trigger the state transitions are the switching  on or

TIMER_ONdiLevelSwitch

off and the expiration of a delay timer.

Figure 22: State diagram for pump control

Temperature control

The state diagram for temperature control has two states ( and ). The events

HEATER_OFFHEATER_ON

for the transitions between the two states are, as described in the text above, depending on which side

enters temperature corridor .

aiCurrentTempDeltaTemp

Figure 23: State diagram for temperature control

6.3Implementation

A state diagram is implemented using an enumeration data type for the state variable and a CASE statement in the

program code:

## Page 31

STATE DIAGRAMS 31
Declaration TYPE
StateEnum:
(
STATE_ONE,
STATE_TWO,
STATE_THREE
);
END_TYPE
VAR
State : StateEnum;
END_VAR
Program code CASE State OF
STATE_ONE:
//actions for STATE_ONE
IF Event1 THEN
State := STATE_TWO;
END_IF
STATE_TWO:
//actions for STATE_TWO
IF Event2 THEN
State := STATE_THREE;
END_IF
STATE_THREE:
//actions for STATE_THREE
IF Event3 THEN
State := STATE_ONE;
END_IF
END_CASE

## Page 32

32 STRUCTURED TEXT TM246
Example: Water tank with temperature control
Implementing a pump controller
Declaration TYPE
PumpStateEnum:
(
PUMP_OFF,
PUMP_ON,
TIMER_ON
);
END_TYPE
VAR
PumpState : PumpStateEnum;
END_VAR
Program code CASE PumpState OF
PUMP_OFF:
//switch off pump
IF NOT diLevelSwitch THEN
PumpState := PUMP_ON;
END_IF
PUMP_ON:
//switch on pump
IF diLevelSwitch THEN
PumpState := TIMER_ON;
END_IF
TIMER_ON:
//start DelayTimer
IF DelayTimer elapsed THEN
PumpState := PUMP_OFF;
END_IF
END_CASE
Implementing temperature control
Declaration TYPE
TemperatureStateEnum:
(
HEATER_OFF,
HEATER_ON,
);
END_TYPE
VAR
HeaterState : HeaterStateEnum;
END_VAR
Program code CASE HeaterState OF
HEATER_OFF:
//switch heating off
IF aiCurrentTemp < SetTemp THEN
HeaterState := HEATER_ON;
END_IF
PUMP_ON:
//switch on heating
IF aiCurrentTemp > (SetTemp + DeltaTemp) THEN
HeaterState := HEATER_OFF;
END_IF
END_CASE

## Page 33

STATE DIAGRAMS 33
6.4 Summary
Using state diagrams makes it possible to display even complex system logic in a modular, clear and structured way.
State diagrams also make it easier to implement the system logic.
The following list provides a brief overview of the steps required for a clean implementation of sequential system logic:
1) Define input variables
2) Define output variables
3) Create state diagram (define state variables, states and events for state transitions)
4) Implementation of the state diagram (declare StateEnum, declare state variable, declare input & output vari-
ables, program CASEstructure )

## Page 34

34STRUCTURED TEXT  TM246

7Exercises

7.1Exercise - Box lift

Task: Box lift

The objective of the exercise is to analyze the process of the box lift and then to program it step by step in Structured

Text.

Two conveyor belts (, ) transport boxes to a lift.doInfeed1ConveyordoInfeed2Conveyor

If a light barrier at the end of a conveyor belt ( or ) detects a box, thediInfeed1BoxDetectiondiInfeed2BoxDetection

corresponding conveyor belt stops and the lift is requested.

If the lift has not yet received a request, it is moved to the corresponding position ).(doLiftUp

When the lift is in the requested position (, ), the lift conveyor diLiftPositionInfeed1diLiftPositionInfeed2(doLiftCon-

) is switched on until the box is positioned correctly on the lift ).veyor(diLiftBoxDetection

Then the lift moves to the unloading position ). Once it has reached the position (),(doLiftDowndiLiftPositionUnload

the box is lifted to the unloading conveyor.

As soon as the box is removed from the lift, it is ready for the next request.

:The following steps must be carried out

1)Outline the procedure, e.g. with steps, states and actions in the program structure

2)Implement the requirement in a Structured Text program.

Figure 24: Boxlift

A RobotStudio simulation model for the BoxLift is available in the download area of the website, which

communicates the above-mentioned variables to ArSim or a real controller via OPC UA.

Download BoxLift simulation package (RobotStudio)

## Page 35

EXERCISES35

7.2Exercise - Dispersion mixer

A mixing plant must be configured. The elements water and paint will be mixed into a dispersion.

Figure 25: Schematics of a paint-mixing plant

The mixing program will run according to the following procedure:

The mixing program waits until the start button is pressed ().diStart

•

Water () is filled into the container until the sensor "" is triggered.doWaterValvediWaterOK

•

The mixing unit () is started and paint () is filled into the container until the sensordoMixerdoColorValve

•

"" is triggered.diLevelHigh

It takes 30 seconds for the mixing time to elapse.

•

The drain valve () and drain pump () are switched on for the filling process.doDrainValvedoDrainPump

•

The filling process ends when signal "" is triggered.diLevelLow

•

The starting situation is restored.

•

Exercise: Implement the dispersion mixer

The exercise is to analyze the process of the dispersion mixer and then to program it step by step in Structured Text.

The following steps must be carried out for this:

1)Outline the procedure, e.g. with steps, states and actions in the program structure

2)Implement the requirement in a Structured Text program.

A timer block from the STANDARD library must be used to implement the mixing time. It is important to

ensure that the timer is reset after the time has expired.

## Page 36

36STRUCTURED TEXT  TM246

8Summary

Structured Text is a high-level programming language that offers a wide range of functionalities. It contains everything

necessary to create an application for handling a particular task. You now have an overview of the constructs and

possibilities of ST.

Automation Help contains a description of all of these constructs. This programming language is especially powerful

when using arithmetic functions and formulating mathematical calculations.

## Page 37

APPENDIX 37
9 Appendix
9.1 Exercise solutions
Exercise: Lighting control
VAR
ButtonLightOn: BOOL;
Declaration:
ButtonLightOff: BOOL;
doLight: BOOL;
END_VAR
Program code: doLight := (ButtonLightOn OR doLight) AND NOT ButtonLightOff;
Table 18: On/Off button, relay with latch
Exercise: Aquarium
VAR
aiTemperatureTop : INT;
Declaration:
aiTemperatureBottom : INT;
aoAverageTemperature : INT;
END_VAR
Program code: aoAverageTemperature := DINT_TO_INT((INT_TO_DINT
(aiTemperatureTop) + aiTemperatureBottom) / 2);
Table 19: Explicit data type coding before addition, after division
On 32-bit platforms (e.g. X20CP1586), the compiler converts the operands to 32-bit for calculation. In this
case, no value overflow occurs during an addition.
This is the case here, for example. On an X20CP1586, it will make no difference whether the explicit data
type conversion is performed or not.
For the calculation "aiTemperatureTop := (aiTemperatureTop + aiTemperatureBottom) / 2;", the inter-
mediate result of the content in parentheses is stored in the variable "aiTemperatureTop" before being
divided by 2.
This could not work on a 16-bit system, however. An explicit data type conversion would therefore have
to be performed in order to obtain the correct result.
Exercise: Weather station - Part 1
VAR
Declaration: aiOutsideTemperature : INT;
ShowText : STRING[80];
END_VAR
IF aiOutsideTemperature < 18 THEN
ShowText := 'Cold';
ELSIF (aiOutsideTemperature >= 18) AND (aiOutsideTemperature <= 25) THEN
Program code:
ShowText := 'Optimal';
ELSE
ShowText := 'Hot';
END_IF;
Table 20: IF statement

## Page 38

38 STRUCTURED TEXT TM246
Exercise: Weather station - Part 2
VAR
aiOutsideTemperature : INT;
Declaration:
aiHumidity: INT;
ShowText : STRING[80];
END_VAR
IF aiOutsideTemperature < 18 THEN
ShowText := 'Cold';
ELSIF (aiOutsideTemperature >= 18) AND (aiOutsideTemperature <= 25) THEN
IF (aiHumidity >= 40) AND (aiHumidity <= 75) THEN
ShowText := 'Optimal';
Program code:
ELSE
ShowText := 'Temperature Ok';
END_IF
ELSE
ShowText := 'Hot';
END_IF;
Table 21: Nested IF statement
Exercise: Fill-level control exercise
VAR
aiLevel : INT;
PercentLevel : UINT;
Declaration: doLevelLow : BOOL;
doLevelOk : BOOL;
doLevelHigh : BOOL;
doAlarm : BOOL;
END_VAR
// scaling the analog input to percent
PercentLevel := INT_TO_UINT(aiLevel / 327);
// reset all outputs
doAlarm := FALSE;
doLevelLow := FALSE;
doLevelOk := FALSE;
doLevelHigh := FALSE;
CASE PercentLevel OF
Program code:
0: // -- level alarm
doAlarm := TRUE;
doLevelLow := TRUE;
1..24: // -- level is low
doLevelLow := TRUE;
25..90:// -- level is ok
doLevelOk := TRUE;
ELSE // -- level is high
doLevelHigh := TRUE;
END_CASE
Table 22: CASE statement for querying values and value ranges

## Page 39

APPENDIX 39
Exercise: Total crane load
VAR CONSTANT
MAX_INDEX: USINT := 4;
END_VAR
VAR
Declaration:
Weights : ARRAY[0..MAX_INDEX] OF INT;
Counter : USINT;
TotalWeight : DINT;
AverageWeight : INT;
END_VAR
TotalWeight := 0;
FOR Counter := 0 TO MAX_INDEX DO
Program code:
TotalWeight := TotalWeight + Weights[Counter];
END_FOR
AverageWeight := DINT_TO_INT (TotalWeight / (MAX_INDEX + 1));
Table 23: FOR - Statement, totaling the weights
Exercise: Search with abort
VAR CONSTANT
MAX_NUMBERS : UINT := 99;
END_VAR
Declaration:
VAR
Numbers : ARRAY[0..MAX_NUMBERS] OF INT;
Counter : INT;
END_VAR
Counter := 0;
REPEAT
IF Numbers[Counter] = 10 THEN
// found the number 10
Program code:
EXIT;
END_IF
Counter := Counter + 1;
UNTIL Counter > MAX_NUMBERS
END_REPEAT
Table 24: REPEAT statement, aborting via search result, limitation
Exercise: Function blocks
VAR
TestTimer : TON;
TestCounter : CTU;
Declaration:
diSwitch : BOOL;
diCountImpuls : BOOL;
diReset : BOOL;
END_VAR
Program code: TestTimer(IN := diSwitch, PT := T#5s);
TestCounter(CU := diCountImpuls, RESET := diReset);
Table 25: Calling TON and CTU

## Page 40

40 STRUCTURED TEXT TM246
Task: Box lift
TYPE
LiftStateEnum:
(
WAIT,
TO_INFEED1_POSITION,
TO_INFEED2_POSITION,
GET_BOX_INFEED1,
GET_BOX_INFEED2,
TO_UNLOAD_POSITION,
UNLOAD_BOX
);
END_TYPETYPE
(*Digital outputs*)
VAR
doInfeed1Conveyor : BOOL;
doInfeed2Conveyor : BOOL;
doLiftConveyor : BOOL;
Declaration:
doLiftUp : BOOL;
doLiftDown : BOOL;
END_VAR
(*Digital inputs*)
VAR
diInfeed1BoxDetection : BOOL;
diInfeed2BoxDetection : BOOL;
diLiftBoxDetection : BOOL;
diUndladBoxDetection : BOOL;
diLiftPositionInfeed1 : BOOL;
diLiftPositionInfeed2 : BOOL;
diLiftPositionUnload : BOOL;
END_VAR
(*Status variables*)
VAR
LiftState: LiftStateEnum;
END_VAR
Table 26: Suggested solution for the box lift

## Page 41

APPENDIX 41
(*Stop conveyors IF a box is waiting FOR lift*)
IF diInfeed1BoxDetection AND (LiftState <> GET_BOX_INFEED1) THEN
doInfeed1Conveyor := FALSE;
ELSE
doInfeed1Conveyor := TRUE;
END_IF
IF diInfeed2BoxDetection AND (LiftState <> GET_BOX_INFEED2) THEN
doInfeed2Conveyor := FALSE;
ELSE
doInfeed2Conveyor := TRUE;
END_IF
(*state machine for boxlift*)
CASE LiftState OF
WAIT://WAIT
IF diInfeed1BoxDetection THEN
LiftState := TO_INFEED1_POSITION;
ELSIF diInfeed2BoxDetection THEN
LiftState := TO_INFEED2_POSITION;
END_IF
TO_INFEED1_POSITION: //Move lift to Infeed1
doLiftUp := 1;
doLiftDown := 0;
IF diLiftPositionInfeed1 THEN
doLiftUp := 0;
LiftState := GET_BOX_INFEED1;
END_IF
TO_INFEED2_POSITION://Move lift to Infeed2
doLiftUp := 1;
doLiftDown := 0;
Program code: IF diLiftPositionInfeed2 THEN
doLiftUp := 0;
LiftState := GET_BOX_INFEED2;
END_IF
GET_BOX_INFEED1://get box from infeed1 onto lift
doLiftConveyor := 1;
doInfeed1Conveyor := 1;
IF diLiftBoxDetection THEN
doLiftConveyor := 0;
LiftState := TO_UNLOAD_POSITION;
END_IF
GET_BOX_INFEED2://get box from infeed2 onto lift
doLiftConveyor := 1;
doInfeed2Conveyor := 1;
IF diLiftBoxDetection THEN
doLiftConveyor := 0;
LiftState := TO_UNLOAD_POSITION;
END_IF
TO_UNLOAD_POSITION://move lift to unload position
doLiftUp := 0;
doLiftDown := 1;
IF diLiftPositionUnload THEN
doLiftDown := 0;
LiftState := UNLOAD_BOX;
END_IF
UNLOAD_BOX://move box from lift to unload conveyor
doLiftConveyor := 1;
IF diUnloadBoxDetection THEN
doLiftConveyor := 0;
LiftState := WAIT;
END_IF
END_CASE
Table 26: Suggested solution for the box lift

## Page 42

42 STRUCTURED TEXT TM246
Exercise: Implement the dispersion mixer
Declaration TYPE
MixerStateEnum:
(
WAIT_FOR_START,
FILL_WATER,
FILL_COLOR,
MIX_TIME,
BOTTLE_DISPERSION
);
END_TYPE
(*Digital inputs*)
VAR
diStart : BOOL := FALSE;
diWaterOk : BOOL;
diLevelHigh : BOOL := FALSE;
diLevelLow : BOOL := FALSE;
END_VAR
(*Digital outputs*)
VAR
doWaterValve : BOOL := FALSE;
doColorValve : BOOL := FALSE;
doMixer : BOOL := FALSE;
doDrainPump : BOOL := FALSE;
doDrainValve : BOOL := FALSE;
END_VAR
(*Function block instances*)
VAR
MixerTimer : TON;
END_VAR
(*State machine variables*)
VAR
MixerState : MixerStateEnum;
END_VAR
Program code // Reset all outputs - will be set in the individual states
doWaterValve := FALSE;
doColorValve := FALSE;
doMixer := FALSE;
doDrainValve := FALSE;
doDrainPump := FALSE;
MixerTimer.IN := FALSE;
// Implementation of dispersion mixer state machine
CASE MixerState OF
// Wait until operator starts process by start button
WAIT_FOR_START:
IF diStart THEN
MixerState := FILL_WATER;
END_IF
// Fill water into the reservoir until limit is reached
FILL_WATER:
IF diWaterOk THEN
MixerState := FILL_COLOR;
END_IF
doWaterValve := TRUE

## Page 43

APPENDIX 43
// Add color and mix dispersion until reservoir is full
FILL_COLOR:
IF diLevelHigh THEN
MixerState := MIX_TIME;
END_IF
doColorValve := TRUE;
doMixer := TRUE;
// Mix color and water until time is elapsed
MIX_TIME:
MixerTimer.IN := TRUE;
MixerTimer.PT := T#30s;
IF MixerTimer.Q THEN
MixerState := BOTTLE_DISPERSION;
END_IF
doMixer := TRUE;
// Continue mixing and bottle dispersion until reservoir is empty
BOTTLE_DISPERSION:
IF diLevelLow = TRUE THEN
MixerState := WAIT_FOR_START;
END_IF
doDrainValve := TRUE;
doDrainPump := TRUE;
doMixer := TRUE;
END_CASE
// Call timer function block
MixerTimer();
9.2 Further information
Diagnostic functions
Only comprehensive diagnostic tools make programming efficient. Automation Studio provides several tools for pro-
gram diagnostics of high-level languages, which are introduced in detail in TM223:
Monitor mode
•
Watch window
•
Line coverage
•
Tooltips
•
Debugger
•
Cross-reference list
•
Diagnostics and service \ Diagnostics tool \ Debugger
Diagnostics and service \ Diagnostics tool \ Watch window
Diagnostics and Service \ Diagnostics tools \ Monitor mode \ Programming languages in monitor mode
\ Line coverage
Diagnostics and Service \ Diagnostics tools \ Monitor mode \ Programming languages in monitor mode
\ Powerflow
Project management \ Workspace \ Output window \ Cross reference
Pointers and references
B&R offers pointers in ST as an extension to the existing IEC standard. A dynamic variable can be assigned a memory
address at runtime. This process is called referencing or initializing a dynamic variable.
As soon as the dynamic variable is initialized, it can be used to access the memory content that it now "points" to. The
keyword ACCESS is used for this process.

## Page 44

44 STRUCTURED TEXT TM246
VAR
Declaration: Source : INT;
PointerDynamic : REFERENCE TO INT;
END_VAR
Program code: // PointerDynamic references to iSource
PointerDynamic ACCESS ADR(Source);
Table 27: Referencing a pointer
IEC standard extensions can be enabled in the project settings of Automation Studio.
Programming \ Variables and data types \ Variables \ Dynamic variables
Preprocessor for IEC programs
Using preprocessor directives is possible in text-oriented programming languages. The syntax of the implemented
directives corresponds to a large extent to that of the ANSI C preprocessor.
The preprocessor directives are an IEC extension. This must be enabled in the project settings.
Preprocessor directives are used for the conditional compilation of programs or entire configurations. Compiler op-
tions can be used to enable options that have an influence on the compilation.
A description and complete list of all available commands can be found in Automation Help.
Project management \ Workspace \ General project settings \ Settings for IEC compliance
Programming \ Programs \ Preprocessor for IEC programs

## Page 45

AUTOMATION ACADEMY45

Automation Academy

Gain additional knowledge

The Automation Academy provides  courses for our customers as well as for our own employees.targeted training

Expand your skills in the field of automation technology and learn to independently implement efficient automation

using B&R systems.solutions

Decide for yourself which  you want to follow!learning concept

Classroom trainingVirtual classroomOnline courses

An experienced trainer guides youA location-independent distanceYou acquire your knowledge inde-

through the learning program.learning program complementspendently and determine the pace

On-site at the desired B&R loca-the other learning options we of-and content yourself. Online cours-

tion. Learn individually or in smallfer. An online tutor accompanieses are available at any time and are

groups.you virtually. The emphasis is onindependent of duration and loca-

self-study.tion.

Contact

Would you like additional training? Are you interested in finding out what the B&R Automation Academy has to offer?

If so, this is the right place.

Access additional information here:

https://www.br-automation.com/de/academy/

Enjoy your next training course!

## Page 46

46 STRUCTURED TEXT TM246

## Page 47

AUTOMATION ACADEMY 47

## Page 48

B&R

Industrial Automation GmbH

A member of the ABB Group

B&R Straße 1

5142 Eggelsberg, Austria

office@br-automation.com

t +43 7748 6586-0

f +43 7748 6586-26

br-automation.com

V2.1.0.1 ©2024/11/12 by B&R, All rights reserved.

All registered trademarks are the property of their respective owners.

Subject to technical changes without notice.