## Page 1

TM251

Memory organization

and IEC programming

## Page 2

2 MEMORY ORGANIZATION AND IEC PROGRAMMING TM251
Requirements
TM210 - Automation Studio
Training modules
TM246 - Structured Text (ST)
Automation Runtime 6.0
Software
Automation Studio 6.1
Hardware ArSim / X20CP1686x

## Page 3

TABLE OF CONTENTS 3
Table of contents
1 Introduction.........................................................................................................................................................4
1.1 Learning objectives..............................................................................................................................4
1.2 Symbols and safety notices...............................................................................................................4
2 Memory.................................................................................................................................................................5
2.1 Number systems...................................................................................................................................5
2.2 Memory organization..........................................................................................................................8
2.3 Data formats.........................................................................................................................................9
3 Variables, constants and data types............................................................................................................11
3.1 Basic information................................................................................................................................11
3.2 Derived data types.............................................................................................................................16
3.3 Memory requirements of variables................................................................................................21
3.4 Dynamic memory access (pointers)..............................................................................................24
4 Arrays and strings...........................................................................................................................................29
4.1 Arrays....................................................................................................................................................29
4.2 Strings..................................................................................................................................................34
4.3 Operations on memory areas.........................................................................................................39
4.4 Error handling with IEC Check........................................................................................................41
5 Advanced memory management.................................................................................................................48
5.1 Dynamic memory...............................................................................................................................48
6 Summary............................................................................................................................................................51

## Page 4

4MEMORY ORGANIZATION AND IEC PROGRAMMING TM251

1Introduction

For correct use of variables and data types, it is important to have a basic understanding of how the memory on a

controller is managed. Knowledge of how the memory is organized and the concept of addressing memory cells, blocks

or whole areas is indispensable to be able to create an error-free application.

The use of user data types and constants improves the flexibility and consistency of the application. The use of dynamic

memory ensures optimum use of resources and high-performance.1

Figure 1: Correct memory organization is the basis of every executable application

This training module covers IEC data types, applying them correctly and getting an understanding of how memory

can be used in and by an application. Topics range from the representation of binary information to data types and

the use of dynamic memory.

The focus is on avoiding errors and being able to analyze and eliminate any software errors that may occur.

1.1Learning objectives

This training module uses selected application examples and exercises to help participants learn the basics of memory

organization and management on the controller.

Detailed learning objectives:

Learning about the memory organization of B&R controllers

•

Learning to use both simple and complex data types correctly and confidently.

•

Explaining the dangers that can occur when using arrays, strings and pointers.

•

Deliberately creating error situations through the improper use of arrays, strings and pointers.

•

Using different tools to find the causes of memory access violations.

•

1.2Symbols and safety notices

Unless otherwise specified, the symbol descriptions and safety notices listed in "TM210 - Working with Automation

Studio" apply.

1The term "dynamic memory" should be qualified in this context because on B&R control systems only the use of the memory is dynamic and not its allocation.

## Page 5

MEMORY5

2Memory

Like most computer systems, a control system also has different types of memory for storing data. A distinction is2

made between   (nonvolatile) and  (volatile).3ROMRAM

Data can be managed on the controller in many different ways. It can be stored as process variables in RAM, on a USB

flash drive, as a file in a file system or as a network file.

During startup of the controller, Automation Runtime copies all system and user tasks, as well as configured compo-

nents, to DRAM (a sub-area of RAM) and executes them there. Accessing DRAM is faster than accessing flash memory

(ROM).

Automation Runtime \ Method of operation \ Memory

The examples and exercises shown in this document refer to information that is in DRAM at runtime (Automation

Runtime). The memory addresses mentioned as well as their values also originate from the DRAM.

2.1Number systems

A computer system can only store and process data (files, programs, variables, recipes, measurement data) in binary

form, i.e. as a sequence of zeros and ones in memory. The zeros and ones can be represented in hardware either by

two distinguishable magnetic states (hard disks) or two electrical states (flash memory, RAM).

2.1.1Binary system

In a binary computer system, information is represented as ones and zeros. Understanding the basics of binary logic

makes programming and debugging a system easier.

The decimal system (base-10 system) used by people in everyday life has a digit set of ten digits (0 to 9). In contrast,

the binary system (base-2 system) only has two digits (0 and 1). As with the decimal system, each position (place) in

a binary number has a value.4

Representation of a binary number

A binary number has a certain number of positions. The

example shows a number with eight positions. The posi-

tions are always counted from the right starting with in-

dex 0.

Each position has a certain value. The value of a position

is calculated from 2.index

Figure 2: Correlation between the position (place) and value of a binary

number

Index 0 (2) thus has the value 1. And Index 4 has the value0

2 = 16.4

Each position of a binary number can contain either a 0 or a 1. If there is a 1, the value of the position is included. If

there is a 0, the value of the position is not added to the result.

2A digital system that contains hardware and software and can therefore perform complex tasks.

3Although today, with the use of flash memory, it is no longer strictly speaking ROM (read-only memory).

4Known from the decimal system by the terms "ones place", "tens place", "hundreds place" and so on.

## Page 6

6MEMORY ORGANIZATION AND IEC PROGRAMMING TM251

Example of a binary number

To obtain the decimal value of a binary number, all values

of positions containing a 1 are added up.

In the example, this means 128 + 16 + 8 + 4 + 1 = 157

Figure 3: Binary representation of the decimal number 157

Length of a binary number

The maximum value of an eight-digit binary number is 255. A total of 256 different numbers can be represented (0-255).

A binary digit corresponds to the smallest possible unit of information on a computer system. This smallest unit of

information is called a bit and can only contain the values 0 and 1.

In computer systems, information is always stored in multiples of 8 bits (i.e. 1 byte), which is described in 2.2 "Memory

organization" on page 8 in more detail.

Therefore, it may happen that more digits (bits) are used

to represent the binary number than would be necessary.

These digits are filled with leading zeros.

Figure 4: Binary representation of the decimal number 13

Sign of a binary number

In binary, the bit with the highest value (i.e. the bit on the

far left) is used as the sign bit. As a result, 7 bits are left

to represent the value. (In the figure on the left, a binary

number is represented with only 4 bits for reasons of clar-

ity, so 3 bits are left to represent the value.)

Negative numbers are formed using two's complement.

1)The bit pattern is created from the positive decimal

number.

2)This bit pattern is then inverted.

3)1 is added. 5

The result is then the bit pattern for the negative number.

Figure 5: Representation of negative numbers in the binary system (4-bit)

Binary representation of -3 (8-bit)

1.0000 0011Binary representation of +3

2.1111 1100All bits are inverted

3.1111 1101Binary addition of 0000 0001

1111 1101Binary representation of -3

5The number zero is the only number that has no sign. For this reason, 1 must be added after inverting.

## Page 7

MEMORY7

Using a sign bit shifts the value range of a number.

For the example of an 8-bit number, this means:

Value range without signValue range with sign

0 to 255-128 to +127

The total quantity of different numbers that can be represented is always 256 in both cases.

This is especially important when signed and unsigned data types are mixed in programs: 2.3 "Data for-

mats" on page 9

2.1.2Hexadecimal system

Binary data or bit patterns become difficult to read especially with longer sequences of numbers. The hexadecimal

system is used instead because it provides a compact and clear representation of binary information.

Unlike the binary system, the hexadecimal system has a set of 16 digits: From 0 to 9 and A to F.

Hexadecimal digits0123456789ABCDEF

0123456789101112131415Decimal equivalent

Representation of a hexadecimal number

In the hexadecimal system, a value is assigned for each position (as with the

binary system). The value is calculated from 16.index

Example of a hexadecimal number

The total value is calculated by multiplying the value of the position by the

digit there (for all positions).

In the example this means

(  *  ) + ( D= *  ) = 157916131

The common representation of hexadecimal numbers for computer systems and most programming languages is

a leading "0x". For the example above, the representation is 0x9D. This prefix tells the compiler: "Here comes a hex

number".6

Correlation between binary and hexadecimal numbers

A hexadecimal position can be one of 16 digits (0-F). For a binary number to represent 16 different values, it needs four

positions. This means that 4 binary positions can be combined into one hexadecimal position.

Binary0101110000111111

Hexadecimal5C3F

6In the ST programming language, a "16#" is required instead of the preceding "0x"

## Page 8

8MEMORY ORGANIZATION AND IEC PROGRAMMING TM251

Conversion table for 4-bit

BinaryHexadecimalDecimalBinaryHexadecimalDecimal

000000100088

000111100199

0010221010A10

0011331011B11

0100441100C12

0101551101D13

0110661110E14

0111771111F15

A byte consists of 8 bits, i.e. 8 binary positions, and a byte can also be represented by 2 hexadecimal digits.

Hexadecimal representation of numbers is primarily used when Logger entries or addresses are dis-

played.

The Watch window offers the option of displaying variable values in binary, octal, decimal or hexadecimal.

IEC 61131-3 specifies the prefixes 2# (e.g. 2#0000_1001) for binary and 16# (e.g. 16#09) for hexadecimal.

Programming \ Standards \ Literals in IEC languages

2.2Memory organization

The memory of a system is divided into memory cells, each containing 1

byte (i.e. 8 bits). The system can address each memory cell by its unique

address.

Figure 6: Memory cell with address

## Page 9

MEMORY9

Correlation between memory cell, address, variable name and value

Variable names are used so that program-

mers do not have to worry about addresses

of memory cells. A variable name is an easily

readable term for a human, but in the back-

ground it is always translated into an address.

The binding of a variable name to an address

at runtime is unchangeable. The addresses

change after a restart or after transfer and

thus reloading of data into the DRAM.

The value of a variable, i.e. the content of the

memory cell, can change at runtime.

Addresses are usually represented in hexadec-

imal.

Figure 7: Correlation between memory cell, variable name, address and value

On a 32-bit system, addresses are 32 bits long. The maximum amount of memory that can be managed

by the system automatically results from the length of the addresses, since each memory cell must have

a unique address. This limits the maximum possible number of memory cells.

If an address must be stored, this requires a variable with a data type that can hold 32 bits, i.e. 4 bytes.

According to IEC 61131-3, this applies to UDINT.7

2.3Data formats

The binary data in memory alone is not enough to generate correct information from it. In addition, knowledge of how

to read or interpret the binary data is required. This is called data format.

In the simplest case, this means knowing the data type.

89Interpretation as an unsigned variable (USINT)Interpretation as a signed variable (SINT)

Returns the value Returns the value 139-117

Table 1: Different interpretation of the same memory content

7IEC 61131-3 is abbreviated to IEC in the following sections.

8USINT stands for Unsigned Short INT and is a 1-byte IEC data type

9SINT stands for Short INT and is also a 1-byte IEC data type

## Page 10

10 MEMORY ORGANIZATION AND IEC PROGRAMMING TM251
Interpretation as an unsigned variable (USINT8) Interpretation as a signed variable (SINT9)
The result is obtained by adding all the values The bit on the far left is the sign bit. The (negative) dec-
(128+8+2+1). imal value can be derived by forming the two's comple-
ment.
Table 1: Different interpretation of the same memory content
Even more information is needed on a higher level (e.g. files) to interpret the stored (binary) data correctly. Without
knowing the appropriate data format, the data is just raw binary data that cannot be processed.
Data format Binary data (hexadecimal notation)
Binary 2B 34 33 37 37 34 38 36 35 38 2B 34 33 37 37 34 38 36 35 38 36
36
Data type REAL 12:34 41 43 D7 0A
ASCII 'Hello World!' 48 65 6C 6C 6F 20 57 6F 72 6C 64
21 00
XML <?xml version="1.0"> 3C 3F 78 6D 6C 20 76 65 72 73 69
<ComboBox> 6F 6E 3D 22 31 2E 30 22 3E 0D
<Item ID="off"/> 0A 3C 43 6F 6D 62 6F 42 6F 78
</ComboBox> 3E 0D 0A 3C 49 74 65 6D 20 49
44 3D 22 6F 66 66 22 2F 3E 0D
0A 3C 2F 43 6F 6D 62 6F 42 6F
78 3E 0D 0A
Table 2: Comparison of data format and binary representation
As can be seen in the table, all data is always stored in binary. It can only be represented differently if the
data format is known. A file extension tells nothing about the contents of a file; it simply provides a clue
about how the data should be interpreted.
8 USINT stands for Unsigned Short INT and is a 1-byte IEC data type
9 SINT stands for Short INT and is also a 1-byte IEC data type

## Page 11

VARIABLES, CONSTANTS AND DATA TYPES 11
3 Variables, constants and data types
Programming is not carried out by accessing fixed memory addresses, but via symbolic elements that have names.
These elements are called process variables.
3.1 Basic information
If data is managed in the form of variables on a system, both the identifier (variable name) and the data type must
be made known to the compiler. Only this allows correct transformation of the source code and the corresponding
memory organization on the target system.
3.1.1 Overview of IEC 61131-3 base data types
A data type must be specified at the declaration for each variable and constant. Basic data types are also called base
data types. These data types form the basis for all other derived data types.
The following list represents all available base data types according to IEC 61131-3 as well as their possible uses. The
defined memory space according to the IEC standard is listed for each data type.
Binary / Signed integer Unsigned Floating-point Charac- Time, date
Bit string integers number ter string
BOOL 1-bit 10 SINT 8-bit USINT 8-bit REAL 32-bit STRING 11 TIME 12
BYTE 8-bit INT 16-bit UINT 16-bit LREAL 64-bit WSTRING DATE_AND_TIME (DT)
WORD 16-bit DINT 32-bit UDINT 32-bit DATE
DWORD32-bit TIME_OF_DAY (TOD)
Table 3: Overview of IEC data types
A complete list of base data types, their areas of use and range of values can be found in Automation Help.
The data types BYTE, WORD and DWORD are pure bit strings. Arithmetic operations are therefore not
allowed by the compiler.
For floating-point data types (REAL, LREAL), an exact comparison of two values is not possible because
for these data types only approximate values and never exact values can be stored.
Programming \ Variables and data types \ Data types
3.1.2 Declaring variables and constants
Declaration
The variable declaration tells the compiler the name of a variable and its data type.
Variables and constants are generally only created in files with the extension .var. When adding a new program, a file
for variable declaration is automatically created as well.
10 Since the smallest possible memory unit is 8 bits, 8 bits are also used by a BOOL in memory.
11 Each single character of a STRING uses 8 bits of memory, one character of a WSTRING uses 16 bits, see chapter 4.2.1 "Declaring strings" on page 34.
12 All time and date data types are managed in the background as UDINT and thus using 32 bits.

## Page 12

12MEMORY ORGANIZATION AND IEC PROGRAMMING TM251

In addition, the Toolbox offers the possibility to insert further .var files at the desired position in the project structure.

Initialization

Initializing is the process of assigning an initial value to a variable.

Initializing variables can take place either directly in the declaration editors or in the initialization subroutine of the

program. Caution is advised when using remanent data. The initialization of a remanent variable in the initialization

subroutine means that the remanent data is also reinitialized during a warm restart. For this reason, remanent vari-

ables should only be assigned a start value in the declaration editor.

Constants can only be "initialized" in the variable declaration file (.var file).

Programming \ Editors \ Table editors \ Declaration editors \ Table editor for variable declaration

Programming \ Editors \ General operation \ Smart Edit

Scope

Variables (and self-created data types) are only known within their scope. The scope initially depends on the position

of the declaration file in the Logical View.

1)Global

Variables from "Global.var" are known in all programs and pack-

ages.

2)Package-global

The validity of "PackageVariables.var" initially refers only to all

programs from "Package" as well as all underlying packages.

By right-clicking on the corresponding declaration file, the

scope of "Package-global" can also be extended to "Global" un-

der "Properties". This is of great advantage when structuring

projects.

3)Local

A local variable from "Program" cannot be used outside the lo-

cal task. The same also applies to "Program1" and "Program2".

If a variable is declared locally with a name that already exists

Figure 8: Declaration files with different scopes

globally, the program only has access to the locally declared

variable. However, this should be avoided for reasons of trace-

ability.

Programming \ Variables and data types \ Variables \ Scope of variables

## Page 13

VARIABLES, CONSTANTS AND DATA TYPES13

Declaring and initializing in Automation Studio

Variable declarations are displayed in this training mod-

ule using the IEC text format.

Automation Studio users can choose between open-

ing declarations in the table editor or text editor. When

double-clicking on a declaration file (.var), the table edi-

tor is opened by default.

Figure 9: Open declaration as text

Depending on the selected method of representation, the declaration is displayed as a table (left) or as text (right).

Figure 10: Variable declaration in table format

Figure 11: Variable declaration in text format

Constants

Like variables, constants have a name and a data type and are also stored in memory like variables.

Variables can get new values at runtime. Some examples of variables include digital and analog inputs/outputs as well

as process data.

In contrast, the value of a constant cannot be modified by its name at runtime. Constants must already be initialized

during the declaration. They are used in the program code as limit values, for example.

Using constants makes the source code easier to read and maintain.

Exercise: Declaring and using variables and constants

1)Declaring variables and a constant

Declare the variables  and  with the appropriate data types. Initialize

VariableUSINTVariableSINTVari-

with the value 137. Also declare the constant  of type USINT and assign the value 10.

ableUSINTMAX_INDEX

2)Representing values in binary and hexadecimal

Assign the value -119 to  in the INIT part of the program or in the Watch window. Now view the

VariableSINT

two variables in the Watch window in binary and hexadecimal view.

3)Assigning a value to the constant

Assign the new value 42 to the constant in the program code.

4)Evaluating the output of the compiler

Compile the program and analyze the output from the compiler.

## Page 14

14MEMORY ORGANIZATION AND IEC PROGRAMMING TM251

Assigning values to constants is not possible in the program code. The compiler will report the following

error in the message window:

Constants can subsequently be used for initializing variables or array limits. In order to do this, however, the project

setting "Allow extension of IEC standards" needs to be enabled.

Programming \ Variables and data types \ Variables \ Constants

Project management \ Workspace \ General project settings \ Settings for IEC compliance

3.1.3Data type conversion

During programming, it may become necessary to convert one data type to another. In general, this is always the case

when two variables or values of different data types occur in the same expression. Caution is also advised if the value

range of a data type would be exceeded by an addition.

Figure 12: Different data types occurring in one expression

A distinction is made between implicit and explicit data type conversion.

Implicit data type conversion

Implicit data type conversion is a type of conversion that is performed automatically by the compiler. If the compiler

finds data types with different accuracy or different range of values in a statement, it performs a conversion to the

data type with the larger range of values.

If the data type of the result variable is too small to accommodate the result of the expression, implicit conversion is

not possible and a compilation error is output.

## Page 15

VARIABLES, CONSTANTS AND DATA TYPES15

Assigning a data type with a smaller range of values to a larger data type is possible and is executed

implicitly by the compiler:

Declaration

VAR

VariableINT  : INT;

VariableREAL : REAL;

END_VAR

Program code

VariableINT  := 32767;

VariableREAL := VariableINT + 100;

There are no problems when assigning an INT variable to a REAL variable, since REAL has a larger range

of values.

Assigning a data type with a larger range of values to a smaller data type is not possible and is displayed

with a corresponding error message:

Declaration

VAR

VariableINT  : INT;

VariableREAL : REAL;

END_VAR

Program code

VariableREAL  := 7.0;

VariableINT   := VariableREAL / 2;

Error message

When assigning a REAL variable to an INT variable, 32 bits must be reduced to 16 bits. This is not easily

possible.

Implicit type conversions should be avoided if possible. Where necessary, any type conversion should be

explicit in the code.

Explicit type conversions make the code easier to understand and maintain.

Explicit data type conversion

Explicit data type conversions are deliberately performed by the programmer using the corresponding function from

the AsIecCon library.

It must always be taken into account that type conversions can mean losing accuracy (e.g. omitting decimal places)

or limiting/shifting a value range.

## Page 16

16 MEMORY ORGANIZATION AND IEC PROGRAMMING TM251
The error shown above can be prevented by using function REAL_TO_INT().
Declaration VAR
VariableINT : INT;
VariableREAL : REAL;
END_VAR
Program code VariableREAL := 7.0;
VariableINT := REAL_TO_INT(VariableREAL) / 2;
Result The value 3 can be stored in VariableINT.
Explanation: The value of VariableREAL is "shortened" to INT (7.0 becomes
7). Then an integer division (7/2) is performed. The result (3) can be stored in
VariableINT.
Sometimes an explicit type conversion can be used to get the maximum possible accuracy in a calculation.
Declaration VAR
VariableINT : INT;
VariableREAL : REAL;
END_VAR
Program code VariableINT := 7;
VariableREAL := INT_TO_REAL(VariableINT) / 2;
If no explicit type conversion were performed in the example shown, the result stored in Variable-
REAL would be 3.0, since an integer division would first be performed on the right side.
Programming \ Libraries \ IEC 61131-3 functions \ AsIecCon \ Function blocks and functions
Exercise: Converting data types
Explicit data type conversion
1) Create one variable of type INT and one of type REAL.
2) Divide the REAL type variable (after assigning a value) by 2 and try to store the result in the INT type variable.
What error message is generated?
3) Now use the correct conversion function for an explicit type conversion to prevent the error.
Consequences of type conversions
1) Find out about the behavior of the decimal places when converting a REAL variable into an INT value.
2) What happens if the value of the REAL variable exceeds the value range of the INT variable being converted to?
3.2 Derived data types
User-defined data types can be created that are based on the different base data types. This procedure is called de-
rivation. User-defined data types consist of elements from the base data types.
User-defined data types, enumerations and derivations are always stored in files with the extension .typ. When adding
a new program, a data type declaration is automatically created. If this file is not needed, it can be removed again.

## Page 17

VARIABLES, CONSTANTS AND DATA TYPES 17
Derived data types include:
Direct derivatives and subranges
•
Enumerations
•
Structures
•
Arrays and multidimensional arrays
•
Programming \ Variables and data types \ Data types \ Derived data types
(Multidimensional) arrays play an important role in programming and are therefore covered in a separate chapter (4.1
"Arrays" on page 29).
3.2.1 Direct derivatives and subranges
It is possible to derive basic data types directly. A new data type with a new name is created that has the same prop-
erties as the basic data type. An initial value can also be assigned to the new data type. All variables that use these
data types therefore have the configured value.
In the case of direct derivation, a range of values can also be specified; this is then referred to as a subrange. Variables
declared with this "new" data type can then only receive values that are in the specified range. Otherwise, an error
message is generated informing that an invalid value assignment has been attempted.
Variables can also be assigned a subrange.
Data type with a subrange TYPE
TemperatureType : UINT(250..500);
END_TYPE
Variable with a subrange VAR
Temperature : UINT(200..550);
END_VAR
Table 4: Declaration of a data type and a variable with a subrange
Programming \ Variables and data types \ Data types \ Derived data types \ Direct derivatives
Programming \ Variables and data types \ Data types \ Derived data types \ SubrangesProgramming \
Variables and data types \ Data types \ Derived data types \ Subranges
Attempting to assign a value outside the subrange to a variable whose value range is restricted by a
subrange in the program is prevented by the compiler and an error message is generated.
If the invalid assignment is made at runtime by another variable, the value is still written. Resulting mal-
functions can be detected e.g. with IEC Check (4.4.2 "IEC Check library" on page 43).
Exercise: Declaring a direct derivative with a subrange
The data type "TemperatureType" should be derived directly. The base data type should be INT. The valid value range
is between 200 and 500. Use numeric constants to define this value range.
1) Declaring a directly derived type
Open the data type declaration, select symbol "Add directly derived type", then assign name "TemperatureType"
and base data type "INT".
2) Declaring the constants
Open the variable declaration, create constants MIN_TEMP and MAX_TEMP and assign the values.
3) Using the constants as a subrange of TemperatureType

## Page 18

18MEMORY ORGANIZATION AND IEC PROGRAMMING TM251

Now use the two constants as the value range for the newly created data type.

4)Testing the result

Declare a new variable of data type  in the variable declaration. Then try to assign a value

TemperatureType

outside the valid value range in the program code and view the compiler output in the message window.

5)Optional: Invalid assignment at runtime

Try assigning a value outside the allowed subrange at runtime using another variable.

3.2.2Enumerations

An enumeration can be described as a list of values. This is also called "enumeration". After the declaration, the enu-

merated data type can be used as data type of a variable. Instead of the numerical value of the enumerated element,

the variable contains the corresponding text. The numerical value of the enumeration element can still be read and

written, however.

The values of the enumerated elements are numbered automatically in ascending order beginning with 0. Negative

initial values can also be assigned.

Enumerated data types and their elements can be used for step sequences in PLC programming and are a way to make

the source code more readable.

Enumeration declara-

TYPE

tion  PizzaEnum:

(

MARGHERITA,

SALAMI,

FUNGHI,

TUNA

);

END_TYPE

Figure 13: Types.typ

Declaring the variables

VAR

Pizza : PizzaEnum;

Price : REAL;

END_VAR

Figure 14: Variables.var

Program code

CASE Pizza OF

MARGHERITA:

Price := 7.5;

SALAMI:

Price := 9;

Figure 15: Watch window  FUNGHI:

Price := 8.5;

TUNA:

Price := 10.2;

END_CASE

Table 5: Declaration of an enumeration and use in the program code

Enumerated types are managed in the background as DINT. Thus, a variable of the type of any user-

defined enumeration always requires 4 bytes in memory.

Programming \ Variables and data types \ Data types \ Derived data types \ Enumerations

## Page 19

VARIABLES, CONSTANTS AND DATA TYPES19

Exercise: Enumerated data type "Pizza"

1)Declare a global enumeration named  with at least three entries.

PizzaEnum

2)Declare a variable of type  and a variable for the price of type REAL.

PizzaEnum

3)Use both variables in a CASE statement in the cyclic section of the program to set the price per type of pizza.

4)Observe the value of the price in the Watch window when a pizza is changed.

3.2.3Structure types

A structure is composed of individual elements, those being basic data types, arrays or other structures. The entire

structure is addressed via a common name. Each of the individual elements also has its own name.

Declaring structures is done either locally or globally in the corresponding .typ file, depending on the required scope.

Structures are primarily used to group

together data and values that have a

relationship to each other. An exam-

ple would be a recipe that always uses

the same ingredients, but in different

amounts.

Default values for the individual struc-

ture elements can be set in the struc-

ture declaration. The default values can

be overwritten in the variable declara-

tion if necessary.

Figure 16: Declaration of a structure in Automation Studio

Structure declaration

TYPE

PizzaType : STRUCT

Price : REAL;

Size : USINT;

Topping : PizzaEnum;

LeaveCheese : BOOL;

BakingTemperature : TemperatureType := 430;

END_STRUCT;

END_TYPE

Variable declaration

VAR

Pizza : PizzaType;

END_VAR

Use in the program code

// Access to elements of Pizza

Pizza.Topping := FUNGHI;

Pizza.Price := 8.5;

Pizza.BakingTemperature := 400;

Table 6: Declaration of a structure and use in the program code

Programming \ Variables and data types \ Data types \ Derived data types \ Structures

Programming \ Editors \ Table editors \ Declaration editors \ Table editor for data type declaration

Exercise: Declaring structure "PizzaType"

Declare a global structure named .

PizzaType

## Page 20

20 MEMORY ORGANIZATION AND IEC PROGRAMMING TM251
This structure should include the following elements:
Price (REAL)
•
Size (USINT)
•
Topping (PizzaEnum)
•
LeaveCheese (BOOL)
•
BakingTemperature (TemperatureType)
•
1) Structure declaration
Assign the name PizzaType in the declaration editor. Add the elements with the corresponding data types ac-
cording to the list provided.
2) Setting default values
Set the BakingTemperature in the declaration of the structure to the default value 430 for all variables.
3) Initializing the elements in the program code
Declare a new variable named Pizza and apply the new data type. Use the variable in the program code and
initialize the elements with values. Then view the result in the Watch window.
Nested structures
The elements of a structure can also be a structure data type. This is also referred to as structural nesting. In practice,
nesting depths of up to four levels are common. This improves the structuring and readability of the source code by
better representing the dependencies or hierarchy of otherwise individual variables.
The above example is now adapted so that the element Price of the structure PizzaType is no longer
a simple REAL, but a separate structure variable with the elements Euro and USD.
Structure declaration
To declare a nested structure, TYPE
PriceType : STRUCT
a structure data type declared
Euro : REAL;
in the project is used for the de-
Usd : REAL;
sired structure element instead
END_STRUCT;
of a base data type.
PizzaType : STRUCT
Price : PriceType;
The order of the structure decla- Size : USINT;
rations PriceType and Piz- Topping : PizzaEnum;
zaType is not important, but LeaveCheese : BOOL;
attention should be paid to the BakingTemperature : TemperatureType := 430;
scope of PriceType. END_STRUCT;
END_TYPE
Variable declaration
The declaration for the struc- VAR
ture variable Pizza does not Pizza : PizzaType;
ExchangeRate : REAL := 1.11;
change.
END_VAR
Use in the program code
The individual elements are de-
scribed or read in the case of Pizza.Price.Euro := 8.5;
Pizza.Price.Usd := Pizza.Price.Euro * ExchangeRate;
nested structures via repeated
use of the "." operator.
Table 7: Declaration of a nested structure and use in the program code

## Page 21

VARIABLES, CONSTANTS AND DATA TYPES 21
Task: Nested structure
Modify the existing structure PizzaType so that the price for a pizza can be specified in three different currencies.
1) Declare the structure PriceType with three elements for different currencies (e.g. Euro, USD, Forint).
2) Adjust the data type of the Price element of PizzaType.
3) Create additional variables for the exchange rates.
4) Set the euro price of Pizza in the program and calculate the prices in the other currencies using the exchange
rates.
5) Observe the change in value of the structure elements for the different currencies in the Watch window when
changing the exchange rates or the euro price.
3.3 Memory requirements of variables
Each variable or constant uses a certain amount of memory at runtime. The required amount of memory depends on
the data type and partly also on the order in which the variables are stored in memory.
SIZEOF for determining the memory requirements // DECLARATIONS
The memory requirement of a variable can be determined with the VAR
function SIZEOF(). Pizza : PizzaType;
Length : UDINT;
The function receives the name of the variable as parameter and re- END_VAR
turns the used memory in bytes as return value.
// CYCLIC
Length := SIZEOF(Pizza);
Programming \ Libraries \ IEC 61131-3 functions \ OPERATOR \ Functions \ Address and Size Functions
\ SIZEOF()
For the configuration of an error-free application it is necessary to have an understanding of how variables can be
stored and localized in memory.
3.3.1 Variable addresses
Access to the actual location in the hardware is based on the address.
The addresses of variables and constants are unchangeable at runtime of the system. These variables are therefore
referred to as "static". A change to the address of a static variable happens when the controller is restarted or when
processes and data are reloaded into the DRAM during transfer.
The addresses of remanent and permanent variables also change when the controller is restarted, since the data is
stored during restart not in DRAM but in the battery-backed SRAM (or FRAM).
Determining the addresses of variables
Declaring the VAR In ST, the address of a variable can be deter-
variables Variable1 : INT := 123; mined with the function ADR(). Since an ad-
Variable2 : INT := 77; dress is a 32-bit binary number, the return val-
Address1 : UDINT; ue of ADR() corresponds to a UDINT.
Address2 : UDINT;
END_VAR Therefore, the address variables in the exam-
Determining Address1 := ADR(Variable1); ple (Address1 and Address2) have the da-
the addresses Address2 := ADR(Variable2);
ta type UDINT.

## Page 22

22MEMORY ORGANIZATION AND IEC PROGRAMMING TM251

For data types that require more than

one byte of memory (e.g. INT), the cor-

responding amount of consecutive

memory cells is used.

In the case of , the sys-

Variable1

tem knows that it must always read (or

write) two consecutive cells because of

the declaration of the data type (INT).

In the example, both  and

Variable1

use 2 bytes (i.e. 16 bits)

Variable2

of memory each, which corresponds to

two memory cells each.

The variables  and

Address1Ad-

use 4 memory cells each,

dress2

since the data type UDINT requires 32

bits (i.e. 4 bytes).

Figure 17: Addresses and memory requirements of variables

A memory address can be identified in the program code but is not permitted to be used as a fixed

value because a new address can already be assigned by Automation Runtime during the next controller

startup or project transfer.

If new variables are created in the project or existing ones are deleted, address changes occur after a

transfer of the project even without a restart. Therefore, a restart is recommended after such a transfer.

Programming \ Libraries \ IEC 61131-3 functions \ OPERATOR \ Functions \ Address and Size Functions

\ ADR()

3.3.2Alignment

Different process architectures follow different rules when it comes to data storage in memory. In very few cases,

the user has to take this into account. If data is to be transferred between systems that have different architectures,

however, it is a good idea to think about how that data is to be stored.

The arrangement of variables in memory is called alignment. Depending on the system, the data type used and the

order in which the variables are declared, there may be pad bytes, i.e. unusable memory cells.

Pad bytes

is declared with type SINT in this example, which requires only 1 byte of memory according to IEC.

Variable1

## Page 23

VARIABLES, CONSTANTS AND DATA TYPES23

Although a variable of the type SINT

uses only 1 byte of memory according

to IEC, it turns out that the next used

memory cell is only two addresses fur-

ther.

The reason for this is that the system

can only assign addresses to variables

that are divisible by the memory re-

quirements of the data type (two bytes

in the example). This can result in mem-

ory cells that remain unused. These are

called pad bytes.

If data types with less than 2 bytes of

memory are combined in structures or

arrays, odd memory cells are also used

for the individual elements that follow

one another in memory.

Figure 18: Pad bytes between variables of different data type

Exercise: Determining the addresses of variables

1)Identify the addresses of the variables existing in the program. To do this, create address variables of type

UDINT to record the addresses and then display their values in the Watch window.

2)Change the declaration order of the variables. Does this have an effect on the addresses of the variables?

3)Restart the controller and view the addresses and the order of the variables again. What can be seen?

4)Now try to use the findings and the declaration of a variable with a memory requirement of only one byte to gen-

erate a pad byte in memory.

Alignment of structural elements

When declaring structures, the rules of alignment also apply to the individual structure elements. This can lead to pad

bytes between the individual structural elements.

The declaration order of the elements of a structure thus has an influence on how much memory a variable of this data

type uses in the system.

Declaration order without pad bytesDeclaration order with pad bytes

TYPETYPE

PizzaType : STRUCT  PizzaType : STRUCT

Price : REAL;    Size : USINT;

Topping : PizzaEnum;    Price : REAL;

Size : USINT;     LeaveCheese : BOOL;

LeaveCheese : BOOL;    Topping : PizzaEnum;

BakingTemperature : TemperatureType;    BakingTemperature : TemperatureType;

END_STRUCT;  END_STRUCT;

END_TYPE  END_TYPE

Table 8: Effects of the declaration order on the memory requirements of a structure variable

## Page 24

24MEMORY ORGANIZATION AND IEC PROGRAMMING TM251

Declaration order without pad bytesDeclaration order with pad bytes

Figure 19: Declaration order without pad bytes

Figure 20: Declaration order with pad bytes

Table 8: Effects of the declaration order on the memory requirements of a structure variable

Simulation \ ARsim \ Runtime behavior \ Alignment

Exercise: Memory requirement of PizzaType

1)Use the function SIZEOF() to find out how much memory a variable of type  with the declaration

PizzaType

from the previous exercise needs.

2)Consider between which elements the pad bytes, if any, are located in the structure and how they could be re-

arranged to make them disappear.

3.4Dynamic memory access (pointers)

All variables shown so far were static variables. For static variables, the correlation between the address (memory cell)

and the variable name cannot be changed at runtime. This means that the name of a variable is always the identifier

for a specific memory cell (for the entire runtime duration).

In contrast, there are also variables that are not always linked to the same address (and therefore the same memory

cell) at runtime, however. These are reference variables. Reference variables, references for short, are often also called

pointers.13

13In programming, there are, in fact, a few small differences between pointers and references. They can be considered identical, however, for general functionality and the memory

view.

## Page 25

VARIABLES, CONSTANTS AND DATA TYPES 25
The terms pointer and reference are used synonymously in the following sections. References are described in the
"Dynamic variables" section of Automation Help.14
A pointer (or reference) is itself also a variable, but of the type REFERENCE TO. While variables as they are normally
know store values (e.g. an integer), a pointer stores an address. The value of a pointer is thus an address. This address
refers to the memory cell that the pointer is "pointing to" (referencing). A pointer can be used to read or overwrite the
value of the memory cell being referenced. This is also possible if there is simultaneously a link between this memory
cell and a static variable name.
3.4.1 Declaring and using pointers
Declaration
Pointers (references) are marked in ST with the keyword REFERENCE TO during the declaration. The data type to
which the reference (the pointer) is to refer later must already be specified during the declaration. It is not possible to
initialize a reference during the declaration; this can only be done at runtime.
At runtime, a pointer can refer to changing addresses and therefore different variables. A pointer has access to a
specific address (and therefore a specific memory cell) via the keyword ACCESS, which is followed by the address of
the variable to be accessed.
The example shows the declaration and use of three static variables and two pointers.
Declaring the variables VAR
Variable1 : INT := 123;
Variable2 : INT := 77;
Variable3 : REAL := 12.3
Pointer1 : REFERENCE TO INT;
Pointer2 : REFERENCE TO REAL;
END_VAR
Program code Pointer1 ACCESS ADR(Variable1);
Pointer1 := 33;
Variable2 := Variable2 + Pointer1;
// Move pointer to another memory cell
Pointer1 ACCESS ADR(Variable2);
Instead of the identifier Variable1, Pointer1 can now also be used in the program. For both read
and write accesses, the memory cell currently pointed to by Pointer1 is accessed directly.
14 From a programming point of view, a reference is not a dynamic variable. For a real dynamic variable, the memory required by the variable would be requested dynamically and
reserved by the system at runtime.

## Page 26

26MEMORY ORGANIZATION AND IEC PROGRAMMING TM251

View in memory

A pointer references a static variable if its address is

specified after the keyword ACCESS.

Pointer1 ACCESS ADR(Variable1);

This is also termed " points to

Pointer1Vari-

".

able1

The pointer itself only "remembers" the address of the

variable it is supposed to point to. The pointer provides

access to the contents of the referenced memory cell.

In ST, the normal assignment operator ":=" can be used

for this purpose; no dereferencing operator is needed

as it is the case in ANSI C.

Pointer1 := 33;

This assigns the new value 33 to  because

Variable1

currently points to .

Figure 21: Reference of  to Pointer1Variable1Pointer1Variable1

A pointer can also be used to read the value of the vari-

able (content of the memory cell) that is being refer-

enced:

Variable2 := Variable2 + Pointer1;

The value of the memory cell to which  cur-

Pointer1

rently points is read. In the example, 33 is thus read and

added to .

variable2

A pointer can also be "moved" to another memory cell

at runtime:

Pointer1 ACCESS ADR(Variable2);

This makes it possible to access different locations in

memory with a pointer at different locations in the pro-

gram code.

Figure 22: Reference of  to Pointer1Variable2

Programming \ Variables and data types \ Variables \ Dynamic variables

3.4.2Significance of data types for pointers

A pointer actually "stores" an address, namely the address of the variable it is referencing. An address is 32 bits long,

so a pointer requires 32 bits in memory, which corresponds to a UDINT.15

A data type must nevertheless be specified when declaring a pointer. The reason is that only the start address of a

variable is retrieved with ADR(). The size of the data type then determines how many subsequent bytes are read in

the memory.

15Addresses are 32 bits (i.e. 4 bytes) long, never negative and always an integer. Of the 4-byte IEC data types (DINT, UDINT, REAL), only UDINT has all those properties.

## Page 27

VARIABLES, CONSTANTS AND DATA TYPES27

With a pointer to INT, for example, only two memory cells

need to be used, while with a pointer to REAL, four mem-

ory cells belong to the variable and must therefore be

read or written.

If the data type of the pointer does not correspond to

the data type of the variable, the memory areas are inter-

preted incorrectly and may be overwritten if four bytes

are written instead of two bytes, for example.

Pointer1 ACCESS ADR(Variable3);

Pointer1 := 100;

With this invalid access due to the mismatching data

type of the pointer (INT) and the variable (REAL), the

statement  will not have the de-

Pointer1 := 100;

sired effect.

The value of  does not change to 100, and

Variable3

instead it can be observed in the Watch window that

12.3 is overwritten by 12.250004. The reason for this is

that  (due to its declaration REFERENCE TO

Pointer1

INT) can only manipulate the lower two memory cells of

.

Variable3

Pointer2 ACCESS ADR(Variable3);

Only a pointer to REAL can read and store the informa-

tion correctly. Even an integer data type with 4 bytes

(e.g. REFERENCE TO UDINT) will not deliver a correct re-

sult here, because the interpretation of the binary data

in memory is different for the two data types (REAL and

UDINT)!

Figure 23: Significance of the data type for pointers

This concept becomes especially important with a pointer to a structure variable. Due to the type declaration of the

structure, the compiler knows the organization of the data in memory belonging to this structure and can take this

into account accordingly when accessing it by means of a pointer.  also only provides the  for astart address

ADR()

structure variable in memory  all elements belonging to the structure variable are stored.from which point

Errors in programming can cause the start or end addresses of the read or write operation of memory cells that are

actually belonging together to be selected incorrectly. Then data is read or written incorrectly.

This can lead to very hard to find and sporadic errors, e.g. if this unintentionally overwrites the value of another vari-

able. Certain memory addresses are protected by the system so that writing or reading will cause a system crash (EX-

CEPTION Page fault), thus allowing the error to be detected. This is described in more detail in the chapter 4.4 "Error

handling with IEC Check" on page 41.

Optional task: Pointer to sub-area of an array

Three elements of an array with a total of 10 elements () of type  should be read using a pointer

BaseArray1USINT

() in response to a start command (). The position of the three elements to read

PointerArrayReadSelection

can be specified via .

SelectedIndex

## Page 28

28MEMORY ORGANIZATION AND IEC PROGRAMMING TM251

Figure 24: Schematic representation of the reading process with pointers

1)Create an array with 10 elements of type USINT and create a pointer to a three-element array of type USINT.

2)Create the program code that executes the read process using the pointer after setting start command

ReadS-

. It should be possible to change the index from which the three values of  are read at

electionBaseArray1

runtime via variable .

SelectedIndex

3)Now try creating a second array with 10 elements (), which directly follows  in mem-

BaseArray2BaseArray1

ory in order to observe a read limit violation if you set  to 9.

SelectedIndex

Dynamic memory access and the use of pointers are particularly important when using arrays or trans-

ferring large data structures to a function or function block.

## Page 29

ARRAYS AND STRINGS29

4Arrays and strings

While arrays also belong to the derived data types, a string is a base data type according to IEC 61131-3. Because of

their identical management in memory and the resulting identical risks in use, they are discussed together here.

4.1Arrays

Unlike structures, arrays can only contain elements of the same data type. When declaring the array, the common data

type of the elements is specified. The individual elements are not addressed via their own element names, as is the

case with structures, but via the common identifier (array name) and their respective index.

Arrays can also be called fields.

The array consists of 8 elements. Each ele-

ment has a value that can be accessed with

the corresponding index.

Figure 25: Schematics of an array

4.1.1Declaring and using arrays

Declaration

Arrays are declared in the .var file via the keyword ARRAY. The number of elements and the data type must be specified

in the declaration. The number of elements is also called the size of the array.

Declaration

VAR

ArrayNumbers : ARRAY[0..7] OF USINT;

Value : USINT;

index : SINT;

END_VAR

Program code

// Assign values to single elements of array

ArrayNumbers[0] := 13;

ArrayNumbers[1] := 0;

ArrayNumbers[6] := 23;

// Read a value from array

Value := ArrayNumbers[6];

Table 9: Declaration of an array with 8 elements of type USINT

Individual elements of the array are accessed via the respective index, which is enclosed in square brackets after the

name. Both read and write access is possible.

The index used can be (as in the example) a fixed number, a variable, a constant or an enumerated element. Especially

when using variables as indices, care must be taken that only the valid indices according to the array declaration are

used; otherwise memory access violations may occur. In the example, this would be all numbers from 0-7.

## Page 30

30MEMORY ORGANIZATION AND IEC PROGRAMMING TM251

Array in memory

In memory, the elements of an array are

stored in sequence. There are no pad

bytes between the elements if the data

type of an element (as in the example

USINT) requires only one byte of mem-

ory.

In principle, the access to the individual

elements can be imagined as "continu-

ous" variable names.

is thus the iden-

ArrayNumbers[2]

tifier for the memory cell with address

0x0471DC72 and the value 7.

would

ADR(ArrayNumbers[2])

return the address 0x0471DC72 accord-

ingly.

Figure 26: Storage of an array in memory

With  or , you would get 0x0471DC70.

ADR(ArrayNumbers[0])ADR(ArrayNumbers)

This shows what happens if the index used is outside the valid range:

ArrayNumbers[9] := 255;

This statement attempts to execute an access to a memory cell (with address 0x0471DC79) that no longer

belongs to the array. If the invalid index is a fixed number in the source code, this is already addressed

with an error message during compiling. However, if the invalid access occurs only at runtime through a

variable used as an index, a memory access violation occurs that may result in a system error (see 4.4.1

"Causes of errors " on page 41).

Using arrays

Arrays are often used to collect data that requires identical actions or calculations to be performed.

In this case, the index is not a fixed number, but a variable that can be used to loop "over the array" (iterate). Here,

too, both read and write access is possible.

FOR index := 0 TO 7 DO Each of the 8 elements (index 0-7) is assigned twice the value of its

ArrayNumbers[index] := index * 2;

own index. The last element thus has the value 14.

END_FOR

Arrays with structure elements

Array elements can also be structures with respect to data type. They are used in the same way as arrays with elements

of basic data types. The access to single elements of the array is still done with the index, the access to the elements

of the structure behind it with a dot.

Declaration

VAR

PizzaList : ARRAY[0..9] OF PizzaType;

END_VAR

Program code

PizzaList[0].Price.Euro := 7.5;

References as elements of arrays are not possible in Automation Studio and generate an error message

during compiling.

## Page 31

ARRAYS AND STRINGS 31
It is important to ensure that array elements are only accessed with indices that are in the valid index
range of the declared array!
4.1.2 Range specifications with constants
Since using fixed numerical values in the declarations and program code usually leads to programming that is unman-
ageable and difficult to maintain, the use of numerical constants is recommended.
The upper index of the array can be specified by a constant. The same constant can be used in the program code for
limiting the index variable (index) in loops.
Declaration VAR CONSTANT
MAX_INDEX : USINT := 99;
END_VAR
VAR
ArrayREAL : ARRAY[0..MAX_INDEX] OF REAL;
index : USINT;
END_VAR
Program code FOR index := 0 TO MAX_INDEX DO
// Operation on element of array
ArrayREAL[index] := index * 5.0;
END_FOR
With this program code, the size of the array can be adjusted by changing the value of the constants only
when they are declared. The rest of the code works unchanged.
Programming \ Variables and data types \ Data types \ Derived data types \ Arrays
Correlation between MAX_INDEX and number of elements
As already seen when declaring arrays, MAX_INDEX must always be one less than the number of elements in an array,
since the first element of the array has the index 0.
In principle, you can also find the size of an array via the source code. To do this, SIZEOF() is used to determine the
total size (i.e. memory requirement) of the array, which is then divided by the size of any element in the array. This
provides the number of elements in the array.
NumberOfElements := SIZEOF(ArrayREAL) / SIZEOF(ArrayReal[0]);
As an alternative to the example above, which uses MAX_INDEX to limit the loop, the following can also be done:
FOR index := 0 TO SIZEOF(ArrayREAL) / SIZEOF(ArrayREAL[0]) - 1 DO
// Operation on element of array
ArrayREAL[index] := index * 5.0;
END_FOR
This variant of a loop also remains valid without change when the size of ArrayREAL in the declaration file is changed
and does not require any adaptation of the source code in the cyclic section of the program.
4.1.3 Initial values of arrays
In some cases it is necessary or desirable to fill an array with values already during declaration. This can be done in
the variable editor in both text and table view.

## Page 32

32MEMORY ORGANIZATION AND IEC PROGRAMMING TM251

Filling with different initial values

VAR

ArrayNumbers : ARRAY[0..7] OF SINT := [13,0,7,100,2,0,23,17];

END_VAR

The known  with 8 elements of

ArrayNumbers

type SINT is declared here and initialized with the

values shown in the example.

This method is only useful and manageable for ar-

rays of small size. Test arrays are often created in

this way during program development.

Filling with identical initial values for all array elements

If all elements of an array should be initialized with the same initial value, there is another way to specify this when

declaring the array.

VAR

ArrayREAL : ARRAY[0..99] OF REAL := [100(12.0)];

END_VAR

In the example, 100  elements are ini-

ArrayREAL

tialized with the value 12.0 using this type of initial-

ization. Since the array has only 100 elements, all of

them are initialized.

With the specification

[50(12.0), 20(24.0), 30(230.0)]

the first 50 elements of the array would be initial-

ized with the value 12.0, the next 20 elements with

the value 24.0 and the last 30 elements of the array

with the value 230.0.

Exercise: Declaring and using an array

Declare an array with elements of type . The array should contain 10 elements.

PizzaType

Use a constant instead of a fixed value when declaring the array. Calculate the average price of a pizza.

Declaring and initializing

1)Declare a variable  as an array with 10 elements of type .

PizzaListPizzaType

2)Initialize all array elements by default with  32 and the  MARGHERITA.

SizeTopping

3)Manually assign values to the price of all array elements in the INIT part of the program or in the Watch window.

Calculating the average value of "Price"

1)Go through all array elements and form the sum of the  or  elements.

Price.EuroPrice.Usd

2)From this, calculate the average price of a pizza.

## Page 33

ARRAYS AND STRINGS33

Test if the average price changes by changing the  element of the first and last array elements. This is an easy

Price

way to make sure that the program is working properly.

Use the constant used in the array declaration for both the summation and the averaging.

Exercise: Using a pointer to an array element

Individual elements of an array can be accessed using a pointer. This is frequently used, especially when transferring

arrays to functions or function blocks.

The value of an array element should be read using a pointer. At runtime, which element of the array is read can be

selected by adjusting an index variable .

IndexToRead

1)Use array  from the previous task or alternatively, create an array with 10 elements of type .

PizzaListREAL

Initialize all elements with different values. Declare a pointer to the data type that the elements of your array

have.

2)In the cyclical part of the program, ensure that the pointer reads the element of the array specified via

Index-

if a  trigger command has been set.

ToReadReadElement

3)Test your program by changing the value of  in the Watch window and observing the value of the

IndexToRead

pointer.

4.1.4Multidimensional arrays

Arrays can also be composed of several dimensions. Accessing an element of the array is then no longer done via a

single index, but via as many indices as the array has dimensions.

In the case of two-dimensional arrays, this is called a row index and a col-

umn index.

In the case of a two-dimensional array, access to a particular element is

done with

Array2D[Zeilenindex,Spaltenindex]

In the example, this would be:

Array2D[2,1] := 77;

Array2D[4,2] := 3;

Figure 27: Schematics of a two-dimensional array

The use of constants to set array limits is also recommended for multidimensional arrays, both when declaring and

when using index limits in loops.

## Page 34

34 MEMORY ORGANIZATION AND IEC PROGRAMMING TM251
Declaration VAR CONSTANT
MAX_INDEX_ROW : USINT := 4;
MAX_INDEX_COLUMN : USINT := 2;
END_VAR
VAR
Array2D : ARRAY[0..MAX_INDEX_ROW,0..MAX_INDEX_COLUMN] OF INT;
i : UINT; // Row index
j : UINT; // Column index
END_VAR
Program code // Nested loop to reach all elements of array
// with two indices i and j
FOR i := 0 TO MAX_INDEX_ROW DO
FOR j := 0 TO MAX_INDEX_COLUMN DO
// Do here what ist necessary for each element of the array
// Access with Array2D[i,j]
END_FOR
END_FOR
Optional exercise: Loading a pizza oven
The loading of a pizza oven should be represented by a two-dimensional array. The oven has 4 drawers; each drawer
can hold a maximum of 10 pizzas at the same time.
1) Declare two constants with meaningful identifiers for the maximum number of drawers and the maximum quan-
tity of pizzas per drawer.
2) Using the constants, declare a two-dimensional array of type BOOL named OvenUsage.
3) Assign the value "TRUE" to individual, random elements during declaration.
4) Calculate how many pizzas are currently in the oven in the cyclic program section and store the result in a suitable
variable.
5) Check the functionality of the program by changing the values of individual elements of OvenUsage in the
Watch window and by observing the result variable.
4.2 Strings
The arrangement of the individual elements of a string in memory is identical to that of arrays, but strings belong to
the base data types according to IEC 61131-3.
4.2.1 Declaring strings
Arrays allow free choice of data type for each element, which is not possible with strings. The elements of a STRING
always have a memory requirement of one byte, and the elements of a WSTRING have a memory requirement of two
bytes.
The information stored in binary form in strings is interpreted either by the ASCII character map if it is a STRING, or
by UNICODE if it is a WSTRING.
As with other data types, the declaration is made in the .var file via the name of the data type (STRING or WSTRING)
followed by the required number of characters in square brackets.
VAR
Assigning a value to a STRING is done with single
StringASCII : STRING[9] := 'Hallo!';
StringUNICODE : WSTRING[9] := "Hallo"; quotes, to a WSTRING with double quotes.
END_VAR

## Page 35

ARRAYS AND STRINGS35

A WSTRING uses twice as much memory as a STRING

with the same number of characters, because the in-

dividual elements are of type WORD (double byte = 16

bits).

The end of a string is always indicated with a binary

zero "\0".

If a STRING with 9 characters is declared, the IEC com-

piler reserves 10 bytes in the memory to accommodate

the termination character "\0" after the last usable

character.16

String functions recognize the end of the string by this

termination character.

For uninitialized strings, there is random content in17

the unused memory cells after the termination charac-

ter.

Figure 28: STRING and WSTRING in memory

Individual elements of a string cannot be addressed directly in ST as is the case with arrays. There are various string

functions in different libraries for manipulating strings.

As with arrays, there is a risk of writing beyond the actual end of the string, which can also lead to memory

access violations.

4.2.2String length

In many cases it is necessary to find out how long a string is. However, a distinction must be made between

the total amount of memory reserved for the string in memory and

•

the number of characters (currently) used in this string.

•

Total amount of memory

The total amount of memory reserved for a string in memory is unchangeable at runtime of the program. Memory size

can be determined using the function SIZEOF().

returns the value 10 for the STRING from the above declaration (9 usable char-

SIZEOF(StringASCII)

acters + "\0").

returns the value 20 for the WSTRING from the above declaration (9 usable

SIZEOF(StringUNICODE)

characters of 2 bytes each + "\0" with another 2 bytes).

The function SIZEOF() is given the name of the string to be examined as a parameter. The return value is the number

of bytes used by the string.

The function SIZEOF() can be used for strings of type STRING as well as WSTRING.

16For non-IEC languages (e.g. C), the string is only as large as specified in the declaration for the static string variable.

17If a string is created in a .var file, it is initialized according to the IEC standard. Uninitialized strings can therefore only occur with dynamically allocated memory.

## Page 36

36 MEMORY ORGANIZATION AND IEC PROGRAMMING TM251
Number of characters used
To determine the length of a string, i.e. the number of characters actually used, different functions are required for the
two types of string (STRING and WSTRING). The length of a string can change at runtime if the number of characters
used changes.
Function Library Description
Determines the number of characters used of a STRING specified by
Standard
LEN() name.
Determines the number of characters used of a STRING specified by ad-
AsBrStr
brsstrlen() dress.
Determines the number of characters used of a WSTRING specified by ad-
AsBrWStr
brwcslen() dress.
Table 10: Comparison of string length functions
For the two strings declared above, the function calls look like this:
Function call Description
Returns the value 6 because the STRING was initialized with 6 char-
LEN(StringASCII) acters.
Returns the value 6 like LEN().
brsstrlen(ADR(StringASCII))
Returns the value 5 because the WSTRING was initialized with 5
brwcslen(ADR(StringUNICODE)) characters.
Table 11: Examples of string length functions
The longer a string is, the longer the execution time of these functions, which in the worst case can lead
to a cycle time violation. In a loop, the functions check the current byte for "\0".
For all functions that require an address as a parameter (here: brsstrlen(), brwcslen()), ensure
that a null pointer is not transferred because this leads to a page fault.
Example: Determining string lengths
Comparison of STRING and WSTRING
1) Declaration
Declare a variable of type STRING and WSTRING with 80 characters each. Initialize both strings with different values.
The identifiers should be DescriptionSTRING and DescriptionWSTRING.
2) Determining the string length
Use the appropriate functions to determine the length of both strings in the cyclic section of the program, i.e. how
many characters of the string are actually used. (Note: LEN() or brsstrlen() for STRING, brwcslen() for WSTRING)
In addition, use SIZEOF() to determine the amount of memory occupied by both strings.
3) Extending the PizzaType structure variable by a STRING element
Add an element of type STRING with 100 characters called Description to the declaration of PizzaType.
Initialize either all or a single element of the array PizzaList with any string and then try to determine the length
of the string in the cyclic program.

## Page 37

ARRAYS AND STRINGS 37
4.2.3 String functions
While in ST it is possible to assign strings or compare two strings using the usual assignment or comparison operator,
other programming languages require libraries to handle strings.
Declaration VAR
String1 : STRING[40] := 'Hello ';
String2 : STRING[40] := 'World!';
END_VAR
Program code IF String1 <> String2 THEN
String2 := 'Hello World!';
END_IF
Library functions are also necessary in ST for the advanced handling of strings. The following section provides an
overview of the available libraries and their areas of application. This makes it possible to concatenate strings, search
for substrings in a longer string, replace text or insert strings at any position in other strings. Converting to numerical
values is also possible.
Library String type Description
Standard functions for handling STRINGs, not available in ANSI C, length limita-
Standard STRING
tion for strings
AsIecCon (W)STRING Conversion functions from and to (W)STRING, not available in ANSI C
Conversion and comparison functions (STRING only)
AsBrStr (W)STRING
Operations on memory areas: Compare, copy, set values (type-independent)
AsBrWStr WSTRING Standard functions for handling WSTRINGs
Programming \ Libraries \ IEC 61131-3 functions \ STANDARD \ Function blocks and functions
Programming \ Libraries \ IEC 61131-3 functions \ AsIecCon \ Function blocks and functions
Programming \ Libraries \ Configuration, system Information, runtime control \ AsBrStr
Programming \ Libraries \ Configuration, system information, runtime control \ AsBrWStr
Important functions for handling strings
The following functions for handling strings from the AsBrStr library are often needed when working with strings.
For further information and a complete list of available functions, see Automation Help. In particular, the STANDARD
library contains functions for more complex handling of strings.18
The most common use cases are copying informa- VAR
tion from a source string (SourceString) to a DestString : STRING[100];
destination string (DestString) or appending in- SourceString : STRING[100];
formation to the existing content. NextAddress : UDINT;
Comparing two strings is also a very common use Result : DINT;
case. END_VAR
The following table provides a short overview of the three functions available for these cases from the AsBrStr library:
18 The string handling functions of STANDARD are restricted to strings with a maximum length of 255 characters.

## Page 38

38 MEMORY ORGANIZATION AND IEC PROGRAMMING TM251
Function Short description Source code example
brsstrcat() Appends the contents of the source
string (specified as a pointer) to the
existing contents of the destination
string (also specified as a pointer).19 NextAddress :=
brsstrcat(ADR(DestString), ADR(SourceString));
NextAddress is the address of
the memory cell that follows the last
copied character.20
brsstrcpy() Copies the contents of the source
string (specified as a pointer) to the
destination string (also specified as
NextAddress :=
a pointer). brsstrcpy(ADR(DestString), ADR(SourceString));
NextAddress is the address that
follows the last element described.
brsstrcmp() Compares two character strings.
If the specified strings are equal, Result :=
brsstrcmp(ADR(DestString), ADR(SourceString));
this is displayed with Result=0.
Both brsstrcat() and brsstrcpy() do NOT check21 whether the memory of the source string is
sufficient to hold the data!
The user is responsible to ensure sufficient size of the target memory. Failure to do so will result in over-
writing memory areas that no longer belong to DestString and thus memory access violations.
The corresponding functions for handling WSTRINGs can be found in the AsBrWStr library.
Exercise: Working with string functions
Create multiple string variables of different lengths and initial values for the following exercises.
Copying
1) Copy the contents of one string to another.
2) Try this despite the fact that the destination string might not have sufficient memory. Where or how can it be de-
termined that the string length is beyond the limit?
3) Adjust the program so that copying is only performed if the destination string is large enough.
4) Try to find a way to copy only as many characters from the source string as actually have room in the destination
string.
Concatenating
1) Repeatedly append the contents of one string (or a fixed string) to the contents of another string. Observe the
repeated appending either in the Watch window or in the Debugger.
2) Observe what happens when the destination string is full and consider what risks may result.
3) Can you find a possibility in the source code to prevent appending beyond the limits of the destination string?
19 The end of the existing contents is defined by the first termination character "\0" in the destination string.
20 If the destination string is not large enough, this address is outside the memory area of the destination string!
21 Safe functions for string and memory operations are available in ANSI C.

## Page 39

ARRAYS AND STRINGS 39
Comparing
1) Compare the contents of two strings and find out what the return value of brsstrcmp() means.
2) Create source code to find out if the extension of a filename (stored in a string) is ".txt". If yes, the filename
should be replaced by ".dat".
For strings with a maximum length of 255 characters, there are some useful functions in the STANDARD
library. They can be used, for example, to search, replace or position specific characters or words within
a string.
Optional exercise: Converting and editing strings
The two variables DescriptionSTRING and DescriptionWSTRING should generate a message about the cur-
rent oven temperature Temperature:
"The current temperature of the oven is -xx- degrees."
1) Add the AsBrStr and AsBrWStr libraries to the project.
2) Assign to both strings the beginning of the message: "The current temperature of the oven is ". Either the as-
signment operator or a suitable copy function for the respective string type can be used for this in ST.
3) Converting to STRING or WSTRING
Convert the value of the variable "Temperature" into a STRING or WSTRING and append the information to the
existing strings respectively. The conversion functions required for this can be found in the AsIecCon library. Use
the CONCAT() function to combine the strings and the brwcscat() function to combine Wstrings.
4) Now append the end of the message "degrees" to both strings.
5) View the result in the Watch window when changing the value of "Temperature".
Careless or incorrect use of string functions may result in memory access violations when reading or
writing beyond the limits of a string.
4.3 Operations on memory areas
In addition to the functions brsstrcat(), brsstrcmp() and brsstrcpy(), which are specifically intended for use with
strings, there are other functions that can be used on arbitrary memory areas using the addresses of the memory
areas, regardless of the data type.
These functions are also available in the AsBrStr li- VAR
brary but are independent of the data type of the DestAddress : UDINT;
data to be manipulated. SourceAddress : UDINT;
NextAddress : UDINT;
As with strings, memory areas can be initialized Length : UDINT;
with certain values, data can be copied or moved or Value : USINT;
the contents of memory areas can be compared. Result : DINT;
END_VAR
The following table provides a short overview of the functions available for these use cases from the AsBrStr library:

## Page 40

40 MEMORY ORGANIZATION AND IEC PROGRAMMING TM251
Function22 Short description Source code example
memset() Writes NumberBytes times the
value of Value in memory be-
ginning with the start address
NextAddress :=
DestAddress. brsmemset(DestAddress, Value, NumberBytes);
The return value is the address fol-
lowing the last byte written.
memcpy() Copies the contents of Length
bytes in memory starting from
SourceAddress to the memory NextAddress :=
cells following DestAddress. brsmemcpy(DestAddress, SourceAddress, Length);
The return value is the address fol-
lowing the last byte written.
memmove() Like memcpy() but the memory ar-
NextAddress :=
eas may overlap. brsmemmove(DestAddress, SourceAddress, Length);
memcmp() Compares the content of Length
memory cells that follow the two
transferred memory locations Result :=
brsmemcmp(DestAddress, SourceAddress, Length);
DestAddress and SourceAd-
dress byte by byte23.
Care must be taken to use correct addresses during programming when transferring the address para-
meters to the functions. The functions themselves cannot perform any checks in this regard.
Task: Initializing and transferring memory
In the following two tasks you will initialize memory and implement a FIFO buffer.
Initializing memory
1) Initialize the values of all elements with 0 using brsmemset() for your existing PizzaList.
2) Can you find a way to initialize the elements of the PizzaList not with 0, but with defined values of a "Reset-
Pizza"? What problems occur here when using brsmemset()?
Transferring to memory: FIFO buffer
The value change of the State variable of a state machine should be recorded in an array with 20 elements. Each
time the value is changed, all elements in the array are moved back one position and the new value is saved on
index 0.
1) Create the state variable State of type StateEnum (INIT, IDLE, STOPPED, RUN).
2) Create the array in which the value of the state variable can be recorded.
3) Program the FIFO function as described above. If the array is full, the oldest value in the buffer is discarded.
For operations on memory areas, regardless of whether this memory area is realized by a large static array or by dy-
namic memory (see 5.1 "Dynamic memory" on page 48), care must be taken during programming to ensure correct
management of the used and available memory via the pointers used. Otherwise, unwanted overwriting of needed
data or even memory access violations will occur.
22 All functions have the prefix brs, i.e. brsmemset(), etc.
23 This can lead to cycle time violations if the memory area to be compared is too large.

## Page 41

ARRAYS AND STRINGS41

Compared to using dynamic memory, using static arrays has the advantage that there is no need to worry about cor-

rectly reserving and freeing up the memory block. Dynamic memory management is possible. In order to prevent often

difficult and lengthy troubleshooting, the use of static variables is preferable.

4.4Error handling with IEC Check

When using arrays and strings, programming errors can have serious consequences if the memory access violations

mentioned above occur.

4.4.1Causes of errors

EXCEPTION page fault

If memory access violations occur due to impermissible indices or the use of strings that are too long and write beyond

the declared string limits, this may cause the system to restart in SERVICE mode.

The use of dynamic variables that have not yet been initialized is also an error that places the system in SERVICE mode.

The system crash and restart is in all cases caused by a page fault, which is entered in the Logger.

Figure 29: Logger entry after a page fault

The problem is that an EXCEPTION page fault can have many causes that are not listed with separate error numbers.

Better ways to localize the causes need to be explored to find the bugs in the code. A tool for this is "Backtrace" from

the Logger.

Undetected memory access violations

An even bigger problem than the EXCEPTION page fault described above can be undetected memory access violations.

For example, if writing beyond the valid limits of an array or string does not result in a page fault, but "only" overwrites

the value of an "adjacent" variable in memory.

## Page 42

42 MEMORY ORGANIZATION AND IEC PROGRAMMING TM251
Shown here is an example of a simple state machine whose state variable has been declared with an
enumerated data type.
Declarations Program code
TYPE CASE State OF
StatesEnum: INIT:
( Info := 'Init';
INIT, IF Trigger = 1 THEN
WAIT, State := WAIT;
MOVE_UP, END_IF
MOVE_DOWN, WAIT:
ERROR Info := 'Wait';
); IF Trigger = 2 THEN
END_TYPE State := MOVE_UP;
END_IF
VAR MOVE_UP:
ArrayTricky : ARRAY[0..5] OF DINT; Info := 'MoveUp';
State : StatesEnum := INIT; IF Trigger = 3 THEN
index : DINT := 0; State := MOVE_DOWN;
Trigger : DINT := 0; END_IF
Info : STRING[40]; MOVE_DOWN:
END_VAR Info := 'MoveDown';
IF Trigger = 4 THEN
State := WAIT;
The enumerated type StatesEnum has 5 ele-
END_IF
ments whose corresponding values are 0-4. The
ERROR:
element Error thus represents the value 4. Info := 'Error';
IF Trigger = 0 THEN
The last line in the program code is the statement
State := INIT;
that causes an impermissible write access by set- END_IF
ting an index that is outside the limit. END_CASE
ArrayTricky[index] := 4;
Alternatively, the last line of the program code could read:
ArrayTricky[index] := ERROR;
For the given declaration sequence, the variables are stored in memory as shown in the figures below. For clarity, four
memory cells are always grouped together in a row in the memory display. A DINT uses 32 bits and thus 4 memory cells.

## Page 43

ARRAYS AND STRINGS43

(index=2) (index=6)Permitted accessAccess outside array limits

ArrayTricky[2] := 4;ArrayTricky[6] := 4;

As can be seen, a write access to  beyond the array limits causes the  variable to be over-

ArrayTricky[6]State

written. As a result, the manufacturing system, whose state machine is affected here, goes into an error state even

though there is no technical cause for it.

A programming error is the only cause. Finding the error could be difficult, however, because the invalid access does

not show up in the Logger either.

The behavior shown in the figures can also be observed in the Watch window of the program. Permitted memory access

again on the left, access outside array limits on the right.

Figure 31: Values of the variables in the Watch window when

Figure 30: Values of the variables in the Watch window when

index=6

index=2

Other "silent" errors

Also, assigning values to variables that are outside the defined range of the enumerated data type or subrange is

not automatically detected and prevented by the system. Again, however, an invalid value assignment can cause the

application to behave incorrectly.

4.4.2IEC Check library

The functions of the IEC Check library provide the possibility to localize the mentioned memory access violations at

runtime. EXCEPTION page faults, as well as limit or value range overshoots and undershoots of data types with sub-

ranges can be detected.

## Page 44

44MEMORY ORGANIZATION AND IEC PROGRAMMING TM251

The library is inserted into the

Logical View of the project via the

Toolbox and already features im-

plemented functions. The names

and parameters of the functions

cannot be changed, the implemen-

tation of each contained function

can or should be adapted to the

respective needs.

Figure 32: Adding the IEC Check library

IEC Check should only be used for troubleshooting in exceptional cases and only on test machines or

monitored machines. Some errors cannot be found in a simulation because they do not occur there.

Due to the operator overload that is executed in the background, a significant amount of system re-

sources is required!

The use of the library in the project can be disabled again at any time with the compiler option -D _IG-

NORE_CHECKLIB.

Right-click on the CPU folder in the

Configuration View to open the

properties window.

You can enter additional compiler

options in the "Compile" tab.

Figure 33: Setting compiler options in the project

FunctionDescriptionWhat is intercepted?

Checks for limit overshoots in theInvalid  are reported, string lim-indices for arrays

CheckBounds()

user project.it overshoots are  intercepted!NOT

Checks for limit overshoots during

CheckRange()Invalid values for  variables are reported.24enum

write access to enum variables.

Table 12: Overview of IEC Check library functions

24Even without IEC Check, an invalid value assignment to an enum variable can and should be intercepted by appropriate error handling in a DEFAULT case.

## Page 45

ARRAYS AND STRINGS45

FunctionDescriptionWhat is intercepted?

Invalid values for variables with a ; valuesubrange

Checks for an invalid value assign-

CheckSubrange()is not written and is set to the lower or upper lim-

ment to a subrange variable.

it of the subrange.

CheckDiv()Checks for invalid divisions.Division by 0

This function takes effect if a reference does not

Checks the addresses during read

yet have an address assignment.

CheckReadAccess()access with dynamic memory ac-

Invalid read access with references that are ini-

cess.

tialized is not intercepted.

Write access to references that have not yet been

Checks the addresses during write

initialized is prevented.

CheckWriteAccess()access with dynamic memory ac-

Invalid write access to references that are initial-

cess.

ized is not intercepted.

Table 12: Overview of IEC Check library functions

Programming \ Libraries \ IEC Check library

Programming \ Libraries \ Configuration, system information, runtime control \ SYS_Lib \ Functions

and function blocks \ Error handling

By adding the IEC Check library - without any further configuration of the functions defined there - the error generated

for the invalid write attempt of

ArrayTricky[6] := 4;

is entered in the logbook.

Due to the default implementation

of the function ,

CheckBounds()

the controller is simultaneously set to

SERVICE mode.

1)The default error number of all

functions from IEC Check is 55555.

This can (and should) be adjusted

when implementing the function.

2)The ASCII data indicates the task

where the error can be found and

IEC Check function that has been

triggered.

3)The binary data indicates which

value caused the limit overshoot.

0x06 corresponds exactly to index

6, which generates the error.

Figure 34: Logger entry when using the IEC Check library

puts the system into SERVICE mode due to the call of the function . By adapting

CheckBounds()ERRxfatal()

the implementation and using  instead of , you can ensure that only a warning is

ERRxwarning()ERRxfatal()

entered in the Logger instead of a system restart.

Error localization with Backtrace in the Logger

By adapting the implementation of  accordingly, precise error localization in the code can be achieved.

MakeEntry()

is the function that generates a corresponding Logger entry for all IEC Check functions. In

MakeEntry()MakeEn-

, calling the  function is replaced by:

try()ERRxfatal()

## Page 46

46MEMORY ORGANIZATION AND IEC PROGRAMMING TM251

With the parameters for  set in this way,

brsmemset()

ERRxwarning(number,index,ADR(out_text));

a page fault is now deliberately caused when

MakeEn-

brsmemset(0,0,1);

is called.

try()

1)Warning that is entered in

the Logbook by

ERRxwarn-

.

ing()

2)Logger entry via

brsmem-

.

set(0,0,1)

3)View of the Backtrace data for

the selected EXCEPTION page

fault.

4)Trace back to the source code

line that is causing the error by

double-clicking on the entry.

Error numbers that can be assigned by users range from 0xC350 to 0xEA5F (50 000 – 59 999). All other

numbers are already assigned or reserved for future functions.

Using the return values of the IEC Check functions

With the functions , ,  and , the return value

CheckBounds()CheckRange()CheckSubrange()CheckDiv()

of the functions is used in the program at the location where it is checked.

In the case of , there is no access out-

FUNCTION CheckBoundsCheckBounds()

side the valid array limits and the valid lower (first) or up-   IF index < lower THEN

per (last) array element is accessed.      CheckBounds := lower;

MakeEntry(55555, index, 'CheckBounds');

ELSIF index > upper THENIf the index used is within the valid limits, that value is, of

CheckBounds := upper;course, used (ELSE case).

MakeEntry(55555, index, 'CheckBounds');

ELSE

With problematic access:

CheckBounds := index;

ArrayTricky[6] := 4;

END_IF

END_FUNCTION

The invalid index 6 is now set by  to the last valid value for the array – i.e. 5 – and overwriting the

CheckBounds()

variable  from the example is thus prevented.

State

Assigning the function's return value to the checked variables in the source code is independent of the Logger entry

created by .

MakeEntry()

The functions  and  assign the upper or lower valid value to the checked vari-

CheckRange()CheckSubrange()

able. By default,  executes a division by 1 instead of division by 0.

CheckDiv()

## Page 47

ARRAYS AND STRINGS 47
Exercise: Using IEC Check in the project
1) Accessing outside the array limits
Access an element outside the valid array limits on an array used in the program (e.g. PizzaList). Observe the
behavior in the Watch window and in the Logger.
2) Adding the IEC Check library
Now add the IEC Check library in the Logical View under Libraries. Transfer your project again after a rebuild (using
"Rebuild all") and execute unauthorized access outside the array limits again.
Analyze the message appearing in the Logger and the system behavior.
Optional exercise: Individually customizing IEC Check functions
Change the standard implementation of the function MakeEntry() from the IEC Check library so a page fault is
deliberately caused when MakeEntry() is called.
In the event of access outside the valid array limits, analyze the Backtrace data for the Logger entry and try to locate
the exact cause in the source code.
4.4.3 Limitations of IEC Check
Using the functions from the IEC Check library can, in many cases, give an indication of where the cause of an error
might be.
However, not all errors can be found or intercepted even with IEC Check. The following list provides a few examples
where even IEC Check cannot detect or localize the error (cause) more precisely, so special care must be taken when
programming.
Writing beyond string limits cannot be detected.
•
• Invalid access with references that are initialized cannot be intercepted. CheckWriteAccess() and Check-
ReadAccess() only check for invalid, i.e. uninitialized, references.
Overwriting values of local variables with pointers from other tasks remains undetected.
•
• Detecting a division by zero with CheckDiv() may fail when using non-integer data types, since floating-point
numbers can only ever be approximated.
Adjustments to the standard implementation of the IEC Check functions are necessary for exact error localiza-
•
tion.
The system load increases significantly when using IEC-Check because the functions of the library are
executed at each relevant execution of an instruction of the program.
Every time an array element is accessed (read or write), for example, the function CheckBounds() is
always called and executed first.
IEC Check should therefore only be used for debugging and not during productive operation of a system.

## Page 48

48 MEMORY ORGANIZATION AND IEC PROGRAMMING TM251
5 Advanced memory management
5.1 Dynamic memory
It is possible not to allocate memory in static variables for the entire runtime, but to request it dynamically at runtime
and to free it up again.
Dynamic memory can be used for reading files as well as for Ethernet communication, if it is only known at runtime
how much memory is actually required.
Ready-made B&R libraries are available for almost all applications, so in most cases it is no longer
necessary to manually reserve dynamic memory.
Using the user-specific B&R libraries is recommended, since many critical errors can occur when reserving
memory manually, and these errors are often very difficult to find.
Once memory is reserved by a function, the programmer is responsible for freeing it up again using
the corresponding function!
Failure to do so results in memory leaks, which can eventually consume the available memory.
5.1.1 Reserving memory blocks
Sometimes it may be necessary to reserve a memory area in DRAM at runtime. The serial interface of a controller
operates with dynamic memory, for example, which it requires at runtime to maintain error-free communication.
Dynamically reserved memory is available to the user in the program. It can always be accessed via the start address
of the reserved memory. The reserved memory area is no longer available to the system until it is freed up again by
the user.
The memory being reserved must comprise consecutive available memory locations on the system in order for it to
be used by the application program.
The contents of dynamically requested memory must be initialized by the user via a reference of the
appropriate data type. Since the contents of memory reserved are in DRAM, it will be lost each time the
controller is restarted.
Reserving and freeing up dynamic memory is possible via functions and function blocks from the SYS_Lib and AsMem
libraries. These functions should only be called in the program's init and exit subroutines.
5.1.2 Managing memory with AsMem
The AsMem library can be used in the INIT subroutine of a program to reserve a large partition of memory on the
system. Function blocks in the cyclic program section can be used to divert memory blocks from this memory partition
and then free it up again. The term "partition" does not refer to a partition in the sense of "drive" (C: or D:) in the flash
memory but to a contiguous (large) memory block in DRAM.
The AsMem library contains function blocks that can be used to dynamically manage even large amounts of memory
from DRAM at runtime. These are used, for example, when loading files from a USB device so that they can be evaluated
by an application.

## Page 49

ADVANCED MEMORY MANAGEMENT49

Function blockDescription

AsMemPartCreateCreating a memory partition; must be done in INIT

AsMemPartDestroyFreeing up a memory partition; must be done in EXIT

AsMemPartAllocReserving a memory block within the partition

AsMemPartAllocClearReserving a memory block within the partition; contents of the block are initialized

with 0

AsMemPartReallocIncreasing or decreasing the size of the existing memory block within the partition

AsMemPartFreeFreeing up the memory block

AsMemPartInfoProvides information about available memory in an allocated partition, including in-

formation about the largest remaining block of free memory in the partition.

Table 13: Function blocks from AsMem

Using the function blocks

A partition with the desired size must first be allocated with As-

MemPartCreate(). This is only permitted in the initialization subrou-

tine and not in the cyclic section of the program.

In the cyclic section of the program, the functions AsMemPartAl-

loc(), AsMemPartAllocClear(), AsMemPartRealloc() and AsMemPart-

Free() can then be used to request, resize and free up one or more

memory blocks from this partition.

A partition is identified by the "ident" value that is returned when

the partition is created. This is a unique identification number of

the partition that is assigned by the system.

A reserved memory block is accessed via its start address. The

start addresses are returned by the allocation blocks AsMemPartAl-

loc(), AsMemPartAllocClear() and AsMemPartRealloc().

Figure 35: Managing memory with AsMem

For each reserved memory block there is a memory overhead of 8 bytes to manage the block. The minimum size of a

memory block is 16 bytes; if less is requested, 16 bytes + 8 bytes of memory are still reserved within the partition.

Reading and writing data from or to reserved memory blocks is done using the already known functions from the

AsBrStr library (e.g. brsmemcpy()).

For using the function blocks from the AsMem library, the executable sample project LibAsMem1_ST is available, which

can be imported into Automation Studio to show how memory areas in DRAM are dynamically managed. The memory

allocated with AsMem is located in DRAM, but encapsulated within the partition. This prevents a memory leak from

occurring, which would use up all DRAM memory. In this way, only the memory within the partition can be used up.

Programming \ Libraries \ Configuration, system information, runtime control \ AsMem

Programming \ Examples \ Examples - Libraries \ Configuration, system information and runtime con-

trol \ Managing memory areas

Dynamically reserved memory must also be  by the user!freed up again

## Page 50

50MEMORY ORGANIZATION AND IEC PROGRAMMING TM251

5.1.3Risks and error sources

If dynamic memory is used carelessly, there are several errors that can occur. These errors often cause problems with

Ethernet communication or HMI application. This can also result in a system crash followed by SERVICE mode, however.

Memory leaks

The most common cause is forgetting to free up reserved memory. This can lead to the aforementioned memory leaks.

This causes the memory to "overflow" and the system can only restart with the resulting ArException in SERVICE mode.

The corresponding message in the Logger looks like this:

Figure 36: Logger entry after a memory leak

Invalid memory access

The cause of the error here is not incorrectly reserving or failing to release the dynamic memory, but the fact that,

when accessing the reserved memory with pointers or references, addresses are used that refer to a memory area that

is used by Automation Runtime, for example.

These invalid accesses cannot be intercepted or localized more precisely even with IEC-Check, since they are not static

arrays and the pointers or references are already initialized when they are used.

Freeing up already released memory

Trying to free up memory that is no longer reserved will also cause an ArException and put the system into SERVICE

mode.

Figure 37: Logger message when trying to free up memory that is no longer reserved

To prevent this scenario, pointers that are released or no longer used should always be set to zero. This makes it

possible to avoid these "dangling pointers". Every time a pointer is accessed, it must, of course, be secured with a "<

>0" check.

## Page 51

SUMMARY51

6Summary

Anyone who is responsible for designing a control application is constantly confronted with data. Data is managed

by variables and constants, regardless of the programming language. A variety of IEC base data types, arrays and

structures are available for this purpose. Automation Studio features declaration editor for easily initializing constants,

variables and arrays. Function blocks and functions are provided for managing memory blocks in the application. The

basic element of data processing is the storage format of the data on the respective platform. Functions are available

for determining and using memory addresses and memory sizes in the program.

Locating errors and eliminating their cause is an important part of developing and maintaining applications.

## Page 52

B&R

Industrial Automation GmbH

A member of the ABB Group

B&R Straße 1

5142 Eggelsberg, Austria

office@br-automation.com

t +43 7748 6586-0

f +43 7748 6586-26

br-automation.com

V2.0.0.0 ©2025/10/07 by B&R, All rights reserved.

All registered trademarks are the property of their respective owners.

Subject to technical changes without notice.