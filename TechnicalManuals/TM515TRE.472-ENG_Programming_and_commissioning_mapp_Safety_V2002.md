## Page 1

TM515

Programming and

commissioning safety

applications with mapp

Safety

## Page 2

2 PROGRAMMING AND COMMISSIONING SAFETY APPLICATIONS WITH MAPP SAFETY TM515
Requirements
Training modules TM210 - Working with Automation Studio
Software Automation Studio 4.8.2
Automation Runtime 4.82
mapp Safety 5.10 (contains SafeDESIGNER 5.10.1.0)
Hardware upgrades ≥ V2.0.0.1
mapp View 5.10
Hardware X20CPU with at least 256 MB RAM
SafeLOGIC/SafeLOGIC-X

## Page 3

TABLE OF CONTENTS 3
Table of contents
1 Introduction.........................................................................................................................................................5
1.1 Learning objectives..............................................................................................................................5
1.2 Safety notices and symbols...............................................................................................................6
2 mapp Safety concept and scope of installation.........................................................................................8
2.1 Installation and licensing....................................................................................................................9
2.2 mapp Safety help documentation....................................................................................................9
3 Presentation of example project "Saw".......................................................................................................11
3.1 Safety requirements specification (SRS).......................................................................................11
4 Configuration in Automation Studio...........................................................................................................14
4.1 Preparing an Automation Studio project......................................................................................14
4.2 Adding a safety controller...............................................................................................................15
4.3 Configuring mapp Safety.................................................................................................................16
4.4 Adding SafeIO modules....................................................................................................................17
5 Getting started in SafeDESIGNER................................................................................................................21
5.1 Password protection.........................................................................................................................21
5.2 SafeDESIGNER layout........................................................................................................................21
5.3 Editor functions.................................................................................................................................22
5.4 Linking I/O channels.........................................................................................................................28
6 Commissioning the safety application.......................................................................................................33
6.1 Compiling a project...........................................................................................................................33
6.2 Download via the Remote Control dialog box............................................................................34
6.3 Download via the mapp Safety HMI application........................................................................34
6.4 Commissioning checklist.................................................................................................................37
7 Implementing the safety application..........................................................................................................39
7.1 Using PLCopen function blocks......................................................................................................39
7.2 Dual-channel evaluation for safe inputs........................................................................................39
7.3 Pulse source........................................................................................................................................40
7.4 Switching a safe output...................................................................................................................41
7.5 Switch-on and switch-off filter.......................................................................................................42
7.6 Communication channels between CPU and SafeLOGIC controller........................................43
7.7 Creating a user function block........................................................................................................47
8 SafeCOMMISSIONING.....................................................................................................................................50
9 Diagnostics and service for safety applications.....................................................................................54
9.1 Diagnostics for the SafeAPPLICATION..........................................................................................54
9.2 Operating and status elements......................................................................................................61
9.3 Module replacement and update...................................................................................................64
10 Further information.......................................................................................................................................70
10.1 Project documentation and printing...........................................................................................70
10.2 How to connect to a safety controller.........................................................................................71
10.3 Support for third-party devices with openSAFETY...................................................................73
10.4 Example projects and solutions...................................................................................................74

## Page 4

4 PROGRAMMING AND COMMISSIONING SAFETY APPLICATIONS WITH MAPP SAFETY TM515
11 Summary...........................................................................................................................................................81

## Page 5

INTRODUCTION5

1Introduction

With this training module, participants will learn how to create a sample project and they will familiarize themselves

with the most important functions and configurations in Automation Studio and SafeDESIGNER.

The course provides information about the installation of the software and Automation Help, followed by a detailed

explanation of sample application "Saw".

Step by step, the individual sensors and actuators are commissioned and the safety controller and its associated safe

I/O modules are parameterized to get "Saw" running.

The training module also contains the documentation for the safety application. A sample project in the safety appli-

cation is used for illustration.

Figure 1: Technology Package "mapp Safety"

Technology Package "mapp Safety" is used to install and provide the required hardware upgrades for the safety com-

ponents as well as programming interface SafeDESIGNER including all functionalities for the safety controller and mo-

tion control.

The example code in this documentation is exclusively for implementing the test setup for this training.

This example code is not permitted to be adopted for safety applications under any circumstances.

Each safe machine application must be subjected to a separate risk analysis and a safety concept must

be created.

1.1Learning objectives

This training module is designed to help participants learn how to use SafeDESIGNER and develop a safety application.

## Page 6

6 PROGRAMMING AND COMMISSIONING SAFETY APPLICATIONS WITH MAPP SAFETY TM515
Overview of the most important objectives:
Participants will learn how individual configuration tasks are divided up between Automation Studio and
•
SafeDESIGNER.
Participants will learn how to add safe modules to a project and complete the I/O configuration in Automation
•
Studio.
Participants will learn how to open SafeDESIGNER, log in to a project and use the various features of the
•
SafeDESIGNER interface.
Participants will learn how to edit the safe module configuration in SafeDESIGNER and set the parameters for the
•
safety equipment being used.
Participants will learn about the documentation requirements for a safety application and how to create docu-
•
mentation for a project using SafeDESIGNER.
Participants will learn how to use the integrated help documentation to assist you in working with SafeDESIGN-
•
ER and the PLCopen Safety library.
Participants will learn how to access and configure function blocks from the PLCopen Safety library in
•
SafeDESIGNER.
Participants can create your own function block and configure the inputs and outputs.
•
Participants will learn about the various diagnostic tools and procedures for commissioning the safety con-
•
troller.
Participants will learn how to transfer a safety application to the SafeLOGIC and SafeLOGIC-X controller and test
•
it with the tools provided.
Participants can transfer the application to the simulation and test it.
•
Participants can create SafeCOMMISSIONING files and activate the machine options via the mapp Safety HMI ap-
•
plication.
In order to correctly implement a safety application, it is important that applicable regulations and stan-
dards are observed in all phases of the safety application's lifecycle. This training module is limited ex-
clusively to use of Automation Studio, SafeDESIGNER and the mapp Safety Technology Package. This
training manual can therefore never replace sound training in safety-related topics.
Safety technology \ Intended use
Organization of notices
•
Qualified personnel
•
1.2 Safety notices and symbols
Safety notices in this manual are organized as follows:
Danger: Disregarding these safety guidelines and notices can result in severe injury, death or substantial
damage to property.
Warning: Disregarding these safety guidelines and notices can result in severe injury or substantial dam-
age to property.
Caution: Disregarding these safety guidelines and notices can result in injury or damage to property.
These instructions are important for avoiding malfunctions.
Additional notices and information in this manual are organized as follows:
Note: Provides important tips and additional information.

## Page 7

INTRODUCTION 7
Help: Refers to an Automation Help entry that contains further information, data sheets or user's man-
uals.
Example: Programming \ Variables and data types \ Data types \ Basic data types
Click on the link to open Automation Help.
Example: Shows an example that reinforces what you have learned.
Result: Briefly summarizes the result of a completed task.
Organization of safety notices in external manuals:
This manual contains references to other manuals. How safety notices are organized in external manuals is listed in
the respective manual.
Exercise: Task definitions and exercises
The sections highlighted in gray describe exercises and the respective steps to be performed. The exercises are de-
signed to provide a deeper understanding of the information provided.

## Page 8

8PROGRAMMING AND COMMISSIONING SAFETY APPLICATIONS WITH MAPP SAFETY TM515

2mapp Safety concept and scope of in-

stallation

Concept

In the context of mapp Safety, a SafeLOGIC (or SafeL-

OGIC-X) controller including all associated SafeNODEs,

project and option definitions is referred to as SafeDO-

.MAIN

Figure 2: Symbolic representation of SafeDOMAIN

A differentiation is made between the following terms:

SafeLOGIC

•

The SafeLOGIC controller is the central processing unit responsible for the cyclic processing of the SafeAPPLI-

CATION as well as configuration and parameter management.

SafeNODE

•

The term SafeNODE refers to a safe device that exchanges openSAFETY data with a SafeLOGIC controller. These

include both safe I/O modules and safe drives. It is important to note that a SafeNODE must always be uniquely

assigned to a SafeDOMAIN. (4 "Configuration in Automation Studio" on page 14)

SafeDESIGNER project

•

The SafeDESIGNER project is created using SafeDESIGNER. There, the safety functions are programmed, the I/O

channels are linked and the SafeNODEs are configured. (5 "Getting started in SafeDESIGNER" on page 21)

SafeAPPLICATION

•

A SafeAPPLICATION is a compiled SafeDESIGNER project. The SafeAPPLICATION is the object that can be trans-

ferred to the SafeLOGIC controller. During compilation, each SafeDESIGNER project is automatically entered as a

download object in the "Configuration View" underneath SafeAPPLICATION and available for selection on the tar-

get system. (6 "Commissioning the safety application" on page 33)

SafeOPTION

•

SafeCOMMISSIONING files make it possible to implement safe machine options. (8 "SafeCOMMISSIONING" on page

50)

Safety technology \ mapp Safety \ Concept \ Organization of mapp Safety

## Page 9

MAPP SAFETY CONCEPT AND SCOPE OF INSTALLATION9

Scope of the mapp Safety Technology Package

The mapp Safety Technology Package contains the following contents:

mapp Safety help documentation

•

SafeDESIGNER

•

mapp Safety HMI

•

Automation Studio hardware upgrades

•

B&R tutorials

•

Figure 3: Contents of the mapp Safety

Technology Package

2.1Installation and licensing

Installing the mapp Safety Technology Package

The mapp Safety Technology Package is downloaded and

installed via dialog box "Upgrades" in Automation Studio.

Alternatively, the Technology Package is available in the

Downloads section of the B&R website.

Figure 4: Automation Studio upgrades - mapp Safety Technology Package

Licensing for SafeDESIGNER

For SafeDESIGNER, the licensing mechanism of Automation Studio is used. No separate license is required.

Validity period of mapp Safety license

If the required licenses are not available on the SafeKEY, the Technology Guarding system searches for the corre-1

sponding licenses. If they are also not available in the Technology Guarding system, a corresponding entry is made

in the logbook. This situation is indicated by blinking signals on both the safety controller as well as the standard

controller. For information about the required licenses, see Automation Help.

Safety technology \ mapp Safety \ Licensing

2.2mapp Safety help documentation

Installing the mapp Safety Technology Package integrates all contents required for programming B&R safety technol-

ogy into Automation Help.

It contains information about the safety hardware, the use of sensors and actuators as well as the engineering of

Smart Safe Reaction using mapp Safety and SafeDESIGNER.

1Storage medium for the SafeLOGIC controller

## Page 10

10PROGRAMMING AND COMMISSIONING SAFETY APPLICATIONS WITH MAPP SAFETY TM515

Figure 6: Navigating the start page of the mapp Safety help documentation

Figure 5: Contents of the mapp Safety help documentation

Safety technology \ mapp Safety

Concept

•

Getting started

•

Engineering

•

Programming \ SafeDESIGNER

•

Service and diagnostics \ Maintenance scenarios

•

Safety technology \ Hardware

Safety technology \ Using sensors and actuators

Safety technology \ Intended use

## Page 11

PRESENTATION OF EXAMPLE PROJECT "SAW"11

3Presentation of example project "Saw"

Controlling a saw is an example of a safety application. The image shows a sketch of the hardware structure and the

associated safety devices. There are two possible configurations depending on the ETA light system used.

When using a SafeLOGIC controller, it is connected to a CPU via POWERLINK. The safe input and output modules com-

municate with the SafeLOGIC controller via the X2X interface. Alternatively, this exercise can also be implemented with

a SafeLOGIC-X controller.

Figure 7: Topology of the hardware used

The SRS (Safety Requirements Specification) defines the function of the individual safety devices and

their interaction. (3.1 "Safety requirements specification (SRS)" on page 11)

Throughout the entire content of this training module, the individual safety functions in the sample ap-

plication are implemented and put into operation.

3.1Safety requirements specification (SRS)

The following safety functions must be implemented in sample project "Saw":

## Page 12

12PROGRAMMING AND COMMISSIONING SAFETY APPLICATIONS WITH MAPP SAFETY TM515

3.1.1Emergency stop switch function

The following function must be implemented:

Saw is stopped when triggered.

•

No acknowledgment is required after startup.

•

Acknowledgment is required via the green button after reseting the

•

emergency stop switch.

The simultaneity for multi-channel evaluation must be within .200 ms

•

Figure 8: Emergency stop switch

3.1.2Light curtain function

The following function must be implemented:

Saw stops when triggered.

•

No acknowledgment is required after startup.

•

Acknowledgment is required via the green button after an intervention

•

in the light curtain.

The simultaneity for multi-channel evaluation must be within .300 ms

•

To support a flexible machine concept, an extension of the safety ap-

•

plication is being considered. A safe machine option makes it possible

to configure the light curtain as available or not available. SafeCommis-

sioningOption receives the name "S_Disable_Lightcurtain" and can ac-

cept values SAFETRUE and SAFEFALSE (8 "SafeCOMMISSIONING" on

page 50).

Figure 9: Light curtains

A hard-wired light curtain returns an OSSD signal (Output Signal Switching Device). This OSSD signal

must be filtered at the safe input using the switch-off filter.

3.1.3Mode selector switch function

The following function must be implemented:

Saw stops when switching.

•

No acknowledgment is required after startup.

•

Two possible operating modes are used:

•

Manual - Switch position right

°

Automatic - Switch position left

°

Impermissible state - Switch position middle  An error is triggered→

°

It is not possible to switch the operating modes directly without ac-

•

knowledgment.

The maximum time for an invalid state is permitted to be 500 ms.

•Figure 10: Mode selector switch

The invalid state is acknowledged via the green button.

•

## Page 13

PRESENTATION OF EXAMPLE PROJECT "SAW"13

3.1.4Operating modes and their interaction

Two operating modes are implemented:

Manual operation (switch position right)

The output is only enabled by the safety application while the green but-

•

ton is pressed and no violation of the safety equipment occurs.

The light curtain is disabled in this operating mode.

•

A violation of the safety equipment must be acknowledged via a rising

•

edge of the green button.

Automatic operation (switch position left)

With a positive edge of the green button, the output is enabled by the

•

safety application if no violation of the safety equipment (emergency

stop, light curtain) occurs.

The light curtain is active in this operating mode.

•

A violation of the safety equipment must be acknowledged via a rising

•

Figure 11: Green button

edge of the green button.

Transferring diagnostic data to the standard application

The diagnostic codes of the individual safe function blocks must be transferred to the standard application via the I/

O mapping and passed on to the corresponding variables of data type UINT.

The following variables must be declared:

diagCode_EStop (DiagCode of "SF_EmergencyStop")

•

diagCode_LightCurtain (DiagCode of "SF_ESPE")

•

diagCode_ModeSelector (DiagCode of "SF_ModeSelector")

•

If an error occurs in at least one function block, set one bit to TRUE in Automation Studio.

The following variable must be declared:

errorInSafetyApplication

•

## Page 14

14PROGRAMMING AND COMMISSIONING SAFETY APPLICATIONS WITH MAPP SAFETY TM515

4Configuration in Automation Studio

Automation Studio is the basis for working with mapp Safety. After creating a project, the hardware for the safety

functions must first be created. All hardware modules combined in a SafeDOMAIN must have the same SafeDOMAIN

ID defined in their configuration.

Module management in Automation Studio includes the following:

Adding the SafeNODEs (safety controller and safe modules)

•

Assigning the SafeNODEs to a SafeDOMAIN

•

Determining the project name of the safety application

•

Defining the SafeKEY password

•

Assigning a user role to access the mapp Safety HMI application

•

Accessing disclosed I/O data of the safety components

•

Exchanging data between the default CPU and safety controller via communication channels

•

Each SafeNODE that is added has a SafeNODE ID. This ID must be assigned to a SafeDOMAIN. At least one SafeLOGIC

or SafeLOGIC-X controller must be configured for the SafeDOMAIN ID.

SafeDOMAIN IDSafeNODE ID

SL112

SI 123

SC 134

Table 1: Assigning the SafeNODE ID to a SafeDOMAIN ID

Figure 12: Example hardware configuration

4.1Preparing an Automation Studio project

To use the mapp Safety HMI application, the  must be enabled in the CPU configuration.OPC UA system

In addition, the  and  versions used must be  in the Automation Studio project undermapp Safetymapp Viewconfigured

"Project \ Change runtime version".

Role "SafeDEV" and user "Safety" are created with any password in section "Access and security \ UserRole system"

of the Configuration View.

2SafeLOGIC

3Safe input module

4Safe digital mixed module

## Page 15

CONFIGURATION IN AUTOMATION STUDIO15

Figure 13: Creating a role and a user

Safety technology \ mapp Safety \ Getting started \ Preparing the Automation Studio project

4.2Adding a safety controller

The SafeLOGIC controller is the central processing unit responsible for the cyclic processing of the SafeAPPLICATION

as well as configuration and parameter management. The SafeLOGIC controller is added in Automation Studio to a

POWERLINK interface and the SafeLOGIC-X controller is added to an X2X interface.

Figure 15: Hardware design with an X20 controller, a SafeLOGIC-X

controller and an openSAFETY light curtain

Figure 14: Hardware design with an X20 controller and a SafeLOGIC

controller

The safety controller is assigned to a POWERLINK interface (SafeLOGIC) or X2X interface (SafeLOGIC-X) via drag-and-

drop from the Hardware Catalog.

## Page 16

16PROGRAMMING AND COMMISSIONING SAFETY APPLICATIONS WITH MAPP SAFETY TM515

Figure 16: Adding the safety controller from the Hardware Catalog

Setting the POWERLINK node number

The POWERLINK node number of the SafeLOGIC con-

troller can be configured in the Physical View.

Figure 17: Setting the POWERLINK node number

4.3Configuring mapp Safety

When using mapp Safety, it is necessary to add configuration files "General"

and "SafeDOMAIN" from the Toolbox to the Configuration View and configure

them.

Figure 18: Folder "mapp Safety"

General configuration

The settings in file "Settings.sfcfg" apply to all SafeDOMAINs. The settings are used to manage which mapp Safety

HMI application is displayed and which roles have access to it. (6.3 "Download via the mapp Safety HMI application"

on page 34)

## Page 17

CONFIGURATION IN AUTOMATION STUDIO17

Configuring the SafeDOMAIN

File "Config.sfdomain" is used to define the SafeKEY password and assign a

name to the SafeDESIGNER project.

Figure 19: SafeDOMAIN configuration

Safety technology \ mapp Safety \

Concept \ Configuration of mapp Safety

•

Getting started \ Configuring mapp Safety

•

4.4Adding SafeIO modules

The SafeIO modules can be added directly to the X2X Link interface of a controller or a POWERLINK bus controller.

SafeIO modules can be combined with standard I/O modules as needed.

The SafeNODE ID is used to identify the individual SafeIO modules in a SafeDOMAIN.

The safe I/O modules can be added from the Hardware Catalog at the desired position via drag-and-drop.

Figure 20: Adding SafeIO modules via drag-and-drop

Module configuration

The shortcut menu of the safe I/O module is used to open the module configuration. This can be used to configure

different settings depending on the module type.

## Page 18

18PROGRAMMING AND COMMISSIONING SAFETY APPLICATIONS WITH MAPP SAFETY TM515

Safety technology \

Hardware

•

Using sensors and actuators

•

4.4.1Configuring the enabling principle

The following 2 settings can be used to configure the enabling principle:

"" (default value): Output channel is visible in the I/O mapping in Automation Studiodirect

•

The "direct" mode makes it possible to use the safe output like a default output from the point of view of the

standard application. This means that as long as there is no safety request, the output is switched on and off via

the standard application.

"": Output channel is hidden in the I/O mappingvia SafeLOGIC

•

The "via SafeLOGIC" mode is used to control the output via the safety controller only.

Figure 22: Enabling principle: Mode "via SafeLOGIC"Figure 21: Enabling principle: Mode "direct"

The enabling principle can be config-

ured individually for each channel of a

safe output module. Depending on the

selection, the output channel is shown

or hidden in the I/O mapping in Au-

tomation Studio.

Figure 23: Settings for the enabling principle in Automation Studio

Safety technology \ mapp Safety \ Engineering \ SafeIO \ Safe digital outputs \ Enabling principle

## Page 19

CONFIGURATION IN AUTOMATION STUDIO19

4.4.2Status of "start interlock on error" on the safe output module

The I/O configuration of safe output modules can be used to define the visibility of the channel status information

and the state numbers of "start interlock on error".

For detailed information about the states of "start interlock on error" and the associated state diagram, see Automa-

tion Help.

Channel "FBOutputState" can be used to read the state number of "start interlock on error". To make this input visible

in the I/O mapping, it must be enabled in the module configuration.

Figure 24: Settings for "FBOutputState"

Safety engineering \ mapp Safety \ Engineering \ SafeIO \

Restart behavior

•

Safe digital outputs \ "start interlock on error" state diagram

•

4.4.3Status inputs for multi-channel evaluation

Safe digital input modules are equipped with integrat-

ed multi-channel evaluation. The multi-channel evaluation

state in the standard application can be evaluated via the

I/O mapping in Automation Studio.

Figure 25: I/O configuration of a SafeLOGIC-X controller - Status of multi-

channel evaluation

## Page 20

20PROGRAMMING AND COMMISSIONING SAFETY APPLICATIONS WITH MAPP SAFETY TM515

Depending on whether the equivalence or antivalence be-

tween two safe inputs has been defined in SafeDESIGN-

ER, a status is created in the safe input module and made

available in the I/O mapping in Automation Studio.

Figure 26: Multi-channel evaluation: Equivalence - Antivalence

SafeDESIGNER provides dual-channel evaluation for each input pair. The setting of the equivalence or

antivalence and the discrepancy time must be defined in the device configuration.

Safety technology \ mapp Safety \ Engineering \ SafeIO \ Safe digital inputs \ PLCopen state diagrams

"Antivalent" / "Equivalent"

Hardware \ X20 system \ X20 modules \ Digital input modules \ X20(c)SIx10x \ Register description

X20SIx1x0 \ Channel list

Exercise: Create a new Automation Studio project, add modules and edit the configuration

The objective of this exercise is to create a new Automation Studio project and to plan and configure the hardware.

This project is then transferred to the controller.

1)Create an Automation Studio project.

2)Add CPU and I/O

3)Configure the SafeLOGIC controller

4)Create new role "SafeDEV" and new user "Safety"

5)Insert and configure the Settings.sfcfg and Config.sfdomain file in Config View

6)Configure SafeIOs

7)Configure safe output "via SafeLOGIC"

8)Establish a connection to the CPU

9)Compile and transfer the project.

Getting Started \ Creating programs in Automation Studio

Safety technology \ mapp Safety \ Guides \ Getting started

## Page 21

GETTING STARTED IN SAFEDESIGNER21

5Getting started in SafeDESIGNER

SafeDESIGNER can be used to create the safety application, which is cyclically

processed by the safety controller. The SafeLOGIC controller and the SafeIOs

are also configured in SafeDESIGNER. For this, only the safety-relevant hard-

ware from the Automation Studio configuration is applied.

The safety application can be opened via the shortcut menu of the SafePLC.

Before SafeDESIGNER can be opened, the project name for a SafeDOMAIN

must be defined in file "config.sfdomain". (4.3 "Configuring mapp Safety" on

page 16)

Figure 27: Open SafeDESIGNER.

Safety technology \ mapp Safety \ Getting started \ Creating a SafeAPPLICATION \ Launching

SafeDESIGNER for the first time

5.1Password protection

When a SafeDESIGNER project is opened for the first time, a password must be entered. Passwords must contain at

least 6 alphanumeric characters.

This password must be entered each time you wish to access the SafeDESIGNER project.

SafeDESIGNER distinguishes between 3 user levels.

Development:

•

All rights

Commissioning:

•

Open project, change commissioning parameters and download

Maintenance or "Cancel":

•

Open project and download - no password required

Figure 28: Entering the password in SafeDESIGNER

After successfully logging in, SafeDESIGNER functions are available based on the user level.

The passwords are set as project specific. If a new project is created, new passwords must be set.

Safety technology \ mapp Safety \ Programming \ SafeDESIGNER \ Introduction \ Password protection

for projects and the safety controller

5.2SafeDESIGNER layout

SafeDESIGNER has multiple toolbars, configuration editors and editing windows. For more information about using

SafeDESIGNER, see Automation Help.

## Page 22

22PROGRAMMING AND COMMISSIONING SAFETY APPLICATIONS WITH MAPP SAFETY TM515

Figure 29: The SafeDESIGNER workspace

Safety technology \ mapp Safety \ Programming \ SafeDESIGNER \

1)User interface \ Menu bar

2)User interface \ Toolbars

3)User Interface \ Edit wizard

4)User interface \ Project tree - Overview

5)User interface \ Toolbars

6)User interface \ Workspace

7)Programming a project \ Device configuration

8)Programming a project \ Device configuration

9)User interface \ Message window

10)User interface \ Cross-references window

11)User interface \ Watch window

5.3Editor functions

The main window in SafeDESIGNER features the Edit wizard, the project tree and the workspace.

Edit wizard: Used to add functions and function modules in code worksheets

•

Project tree: Used to manage and edit project structure

•

Workspace: Used to open programs, variable declarations and descriptions

•

## Page 23

GETTING STARTED IN SAFEDESIGNER23

Figure 30: Programming interface in SafeDESIGNER

Safety technology \ mapp Safety \ Programming \ SafeDESIGNER \

User interface

•

EC 61131 implementation \ Data types

•

Developing a project

•

Limited variability language (LVL)

SafeDESIGNER uses the limited variability languages (LVL) Function Block Diagram and Ladder Diagram. This protects

the user from errors, such as the creation of endless loops, memory corruption due to corrupted pointers or invalid

access to global variables.

Software development is also simplified by ISO 13849-1.

5.3.1Edit wizard

The Edit wizard can be used to add available function blocks to the worksheet.

This view can be grouped by function groups and libraries.

Safe function / function block per IEC 61131-3

Non-safe function / function block per IEC 61131-3

Figure 31: Edit wizard

PLCopen Safety function block

Self-created function / function block

Table 2: Color legend of the function blocks in SafeDESIGNER

Safety technology \ mapp Safety \ Programming \ SafeDESIGNER \ User Interface \ Edit wizard

## Page 24

24PROGRAMMING AND COMMISSIONING SAFETY APPLICATIONS WITH MAPP SAFETY TM515

5.3.2Programs and libraries

The programming languages available for the main code are Ladder Diagram

and Function Block Diagram. These can be combined within the worksheets.

It is possible to structure the safety application by using multiple worksheets.

The user can also create his own function blocks and reuse them within the

program. The function blocks are implemented either using Ladder Diagram,

Function Block Diagram or Structured Text.

Figure 32: Project tree

Safety technology \ mapp Safety \ Programming \ SafeDESIGNER \ EC 61131 implementation \ Program-

ming languages

5.3.3Adding functions and function blocks

Functions and function blocks can be added either via drag-and-drop or by double-clicking on the respective function

or function block.

Figure 33: Adding a function in worksheet via drag-and-drop

Adding a function block opens a dialog box that is used to create an in-

stance variable with the corresponding data type.

Figure 34: Declaring an instance variable for a

function block

Safety technology \ mapp Safety \ Programming \ SafeDESIGNER \

User Interface \ Edit wizard

•

Developing a project \ Developing visual code

•

## Page 25

GETTING STARTED IN SAFEDESIGNER25

5.3.4Creating a variable

There are precise definitions for the use and scope of local and global variables as well as data types when program-

ming with programming languages with limited variability language (see 5.3 "Editor functions" on page 22).

There are two options for adding a new variable. It is either added via the "Variable"

button in the vertical toolbar or by double-clicking on the input or output of a function

or function block.

Figure 35: Adding a

new variable

A wizard opens to set the variable name, data type and the scope for the variable.

Figure 36: Wizard for the new variable

The following scopes are available for selection:

Local: For the current worksheet

•

Global: Only for communication channels and I/O channels or for the use in multiple worksheets

•

Constant: Variable with a constant value

•

Local variable and constants

Local variables and constants are only used in the current worksheet.

The following button is used to switch between the code view and the variable declara-

tion of the local variables and constants:

Figure 37: Displaying

local variables

Global variable

Global variables are exclusively used for communication channels and I/O channels or

for the use in several worksheets.

The following button is used to open the variable declaration of the global variables:

Figure 38: Displaying

global variables

Safe and non-safe data types

Data types define the properties for the values of a variable. They define the initial value, range of possible values and

number of bits. SafeDESIGNER contains elementary data types defined in IEC 61131-3 and special safety-relevant data

types.

Safe data typeNon-safe data type

SAFEBOOLBOOL

Table 3: Safe and non-safe data types

## Page 26

26PROGRAMMING AND COMMISSIONING SAFETY APPLICATIONS WITH MAPP SAFETY TM515

Safe data typeNon-safe data type

SAFEINTINT

SAFEDINTDINT

SAFETIMETIME

SAFEBYTEBYTE

SAFEWORDWORD

SAFEDWORDDWORD

Table 3: Safe and non-safe data types

Variable declaration

The global or local variables are displayed in the variable declaration depending on the selection.

Figure 39: Global variable declaration

Constants can be added via the entries from DATA TYPE#VALUE. Example: "SAFETIME#10s" for a time

constant with a duration of 10 seconds.

Safety technology \ mapp Safety \ Programming \ SafeDESIGNER \

User interface \ Variables worksheets

•

User interface \ Dialog boxes for code editing \ Dialog box 'Variable"

•

User interface \ Visual code objects \ Variables

•

Developing a project \ Developing visual code \ Adding and editing constants (literals)

•

5.3.5Linking a function / function block

There are two ways to link a variable to an input or output of a function or function block.

The variable can be set directly to the input or output by moving it.

Figure 40: Variable linked to function input

## Page 27

GETTING STARTED IN SAFEDESIGNER27

Button "Connect" can also be used to connect a variable with a function / function

block or two functions / function blocks.

Figure 41: Button

"Connect"

A connecting line between two con-

tacts is drawn via clicking and drag-

ging.

Figure 42: Drawing a connecting line

Safety technology \ mapp Safety \ Programming \ SafeDESIGNER \ User interface

Keyboard shortcuts

•

Visual code editor (general description)

•

Handling objects in the code editor

°

Connecting objects in the code editor

°

5.3.6Adding a Ladder Diagram network

A network can be added via a button in the vertical toolbar.

Figure 43: Adding

Ladder Diagram

network

A network is added at the current cursor position in the worksheet.

Figure 44: New network in worksheet

Local or global variables must now be connected to the network elements. A double click on the element opens a wizard

for selecting a variable.

Adding function blocks to networks

It is also possible to create networks with a function / function block.

Figure 45: Network with function block

## Page 28

28PROGRAMMING AND COMMISSIONING SAFETY APPLICATIONS WITH MAPP SAFETY TM515

Safety technology \ mapp Safety \ Programming \ SafeDESIGNER \ User interface \ Graphical code ob-

jects

Contact and coil

•

Function and function blocks

•

Variable

•

5.3.7Adding a comment in the worksheet

Button "Comment" can be used to add a comment at the current cursor position. A win-

dow will open to enter the text and configure various formatting settings.

Figure 46: Button to

add a comment

After the window has been acknowl-

edged, the text is added at the current

cursor position.

Figure 47: Comment in the worksheet

Safety technology \ mapp Safety \ Programming \ SafeDESIGNER \ User interface \ Visual code editor

(general description) \ Adding comments - Dialog box "Comment"

5.4Linking I/O channels

The I/O mapping in the Safety View serves as an interface between the channels of the SafeNODEs and SafeDESIGNER.

Global variables are connected to the safe inputs and outputs via the global variable declaration in SafeDESIGNER.

Figure 48: Safety View

Column "Slot" displays the physical position of the safe modules. This is for information only and cannot be changed.

Column "Variable" contains the name of the I/O data point in the safety application.

Column "CPU variable" contains the name that was configured for the I/O data point in Automation Studio.

All channels with a yellow arrow are safety-related and must reference to variables with safe data types.

## Page 29

GETTING STARTED IN SAFEDESIGNER29

Safety technology \ mapp Safety \ Programming \ SafeDESIGNER \ Developing a project \ Connect-

ing/Disconnecting process data elements and global I/O variables

5.4.1Channels of the SafeLOGIC controller

Configurable communication channels and SafeCOMMISSIONING channels

are available for the SafeLOGIC controller.

The communication channels can be used, for example, to transmit the Di-

agCodes of the PLCopen blocks to the standard application. In addition, the

channels of the SafeLOGIC controller can be used to transmit a non-safe sig-

nal of an acknowledgment button from the standard application to the safety

application.

For more information about SafeCOMMISSIONING, see chapter 8 "SafeCOM-

MISSIONING" on page 50.

Figure 49: I/O mapping - SafeLOGIC

Safety technology \ mapp Safety \ Hardware \ X20 system \ Module overview: Alphabetical

5.4.2Safe input and output modules

The individual input and output channels and channels for status information are available for connecting global vari-

ables on the safe I/O modules.

In addition, it is possible to use mul-

ti-channel evaluation for the safe inputs

(4.4.3 "Status inputs for multi-channel

evaluation" on page 19).

Figure 50: Input channels

Any safe output module is equipped

with an internal "start interlock on er-

ror" to switch on the channel after an

error on the module and/or network.

The error is acknowledged via a rising

edge on channel "ReleaseOutput".

Figure 51: Output channels

## Page 30

30PROGRAMMING AND COMMISSIONING SAFETY APPLICATIONS WITH MAPP SAFETY TM515

If no data points have been linked in SafeDESIGNER by a safe module, the SE LED on the safe module

blinks (9.2.1 "Status elements" on page 62).

Safety technology \ mapp Safety \ Hardware \ X20 system \ Module overview: Alphabetical

5.4.3Linking an I/O channel to a new variable

To define an I/O channel as a new variable, the channel is first selected in the Safety View and then dragged and

dropped onto the worksheet.

The declaration dialog box for a new global variable appears. After the variable name has been specified, the dialog

box can be confirmed and the new variable can be placed on the worksheet as needed. The new variable name is now

assigned to the corresponding channel in the Safety View.

Figure 52: Dragging an I/O channel onto the worksheet - Filling in the declaration dialog box

This button is used to switch to the global variable declaration.

Figure 53: Opening

global declarations

Column "Terminal" contains information about which I/O channel the variable is connected to.

## Page 31

GETTING STARTED IN SAFEDESIGNER31

Figure 54: Global declarations

Safety technology \ mapp Safety \ Programming \ SafeDESIGNER \ Developing a project \ Connect-

ing/Disconnecting process data elements and global I/O variables

5.4.4Linking an I/O channel with an existing variable

To establish a connection between a global variable and an I/O channel, the I/O channel must first be selected in the

Safety View and then dragged and dropped onto the variable in the global variable declaration. The reverse is not

permitted.

Figure 55: Selecting an I/O channel and assign it to the global variable via drag-and-drop

Safety technology \ mapp Safety \ Programming \ SafeDESIGNER \ Developing a project \ Connect-

ing/Disconnecting process data elements and global I/O variables

Exercise: First safety application

The objective of this exercise is to link two safe inputs to one safe output using an AND operation.

## Page 32

32PROGRAMMING AND COMMISSIONING SAFETY APPLICATIONS WITH MAPP SAFETY TM515

1)Open a SafeDESIGNER project

2)Assign the project password

3)Add function AND_S to the worksheet

4)Declare input "SafeModuleOK" on the SI module as global variable "SM2_SafeModuleOK"

5)Declare input "SafeModuleOK" on the SC module as global variable "SM3_SafeModuleOK"

6)Declaring local variable "AllSafeModulesOK"

7)Connect all variables with an AND_S function

Result:

Figure 56: Query the module status

## Page 33

COMMISSIONING THE SAFETY APPLICATION33

6Commissioning the safety application

6.1Compiling a project

The project must be compiled before you can download it. Only projects compiled error-free can be transferred to the

SafeLOGIC controller or simulation. The cause of warnings should be identified and corrected.

The "Compile" button is used to compile the safety application and provide it with a

unique CRC value.5

Figure 57: Compiling

During the build process, messages, errors and warnings

are displayed in the message window. If no errors are re-

ported, the safety application can be transferred to the

SafeLOGIC controller.

Figure 58: Message window

There are 2 ways to transfer a SafeAPPLICATION:

Remote Control in SafeDESIGNER (see "Download via the Remote Control dialog box" on page 34)

•

mapp Safety HMI application (Download via the mapp Safety HMI application)

•

Download via "" is a fast method to transfer code changes to the SafeLOGIC controllerRemote control

and test them.

Before commissioning, it is necessary to ensure that a complete and final transfer via the "mapp Safety

" is carried out.HMI

For the first or final transfer, the "" must be selected.mapp Safety HMI

SafeKEY / Safety section

The safety-related data on the SafeLOGIC controller is transferred directly to the SafeKEY and loaded from there.

Since there is no storage medium available on the SafeLOGIC-X controller, a safety section is created in the flash mem-

ory of the controller in which the data for the SafeLOGIC-X controller is stored.

This includes the following data on the SafeKEY / Safety section:

SafeDESIGNER application (application and all SafeDESIGNER parameters for the modules)

•

Configuration (unique module ID (UDID), firmware versions of modules)

•

SafeOPTION (Safe Commissioning Options, Safe Commissioning Tables, etc.)

•

Safety technology \ mapp Safety \ Programming \ SafeDESIGNER \ Developing a project \ Compiling

a project

5Cyclic redundancy check (CRC for short) is a procedure for calculating a data checksum

## Page 34

34PROGRAMMING AND COMMISSIONING SAFETY APPLICATIONS WITH MAPP SAFETY TM515

6.2Download via the Remote Control dialog box

The application can be transferred, messages can be acknowledged and the

SafeLOGIC controller can be reset via the Remote Control dialog box.

The Remote Control dialog box is opened in the SafeLOGIC controller short-

cut menu in the Safety View.

Figure 59: Opening the Remote Control dialog

box

The SafeKEY password that was defined in the "Config.sfdomain" file in

Automation Studio must be entered first.

After the Remote Control dialog box has been opened, the safety applica-

tion can be transferred to the SafeLOGIC controller via command "Down-

load".

After the download has been completed successfully, the SafeLOGIC con-

troller restarts and the buttons in the Remote Control dialog box appear

in orange. This indicates that acknowledgment is required.

Figure 60: Remote Control dialog box

For detailed information about the operating and status elements of the Remote Control dialog box, see

9.2 "Operating and status elements" on page 61.

Safety technology \ mapp Safety \ Programming \ SafeDESIGNER \ Dialog boxes for controlling the

safety controller \ Remote Control

6.3Download via the mapp Safety HMI application

After the SafeAPPLICATION has been created and compiled in SafeDESIGNER, it is stored in the mappSafety folder in

the Configuration View in Automation Studio and is stored on the controller when the standard application is down-

loaded in Automation Studio.

The SafeAPPLICATION can now be selected as the standard application in the .sfdomain file. The SafeAPPLICATION is

thus automatically preselected in the "Transfer" area of the mapp Safety HMI application.

URL  can be used to call the mapp Safety HMI application via a<IP Address>:81/index.html?visuId=mappSAFETY

browser.6

6The latest version of Google Chrome is preferred

## Page 35

COMMISSIONING THE SAFETY APPLICATION35

Login

To establish a connection with the

SafeLOGIC controller, it is necessary to

log in to Automation Studio with the

user data from the user configuration.

Figure 61: mapp Safety HMI application - Login

Connection

A connection can then be established

to the desired SafeDOMAIN by select-

ing the corresponding entry from the

table and confirming via "Connect".

When connected, the correct status of

the SafeLOGIC controller is displayed.

Figure 62: mapp Safety HMI application - Establishing a connection to the SafeLOGIC controller

Downloading the safety application via the mapp Safety HMI application only works if the SafeLOGIC

controller is in setup mode. This can be done as follows:

Enable setup mode in the Remote Control dialog box.

•

Empty or formatted SafeKEY (only for SafeLOGIC controller, not for SafeLOGIC-X controller)

•

Set the selector switch on the SafeLOGIC controller to an unlabeled position between FW-ACKN and

•

SK-COPY and press and hold the acknowledgment button for approx. 20 seconds (only for SafeL-

OGIC controller, not for SafeLOGIC-X controller).

Setup mode is only permitted to be enabled during the commissioning of the machine/system. Setup

mode must be disabled during operation.

## Page 36

36PROGRAMMING AND COMMISSIONING SAFETY APPLICATIONS WITH MAPP SAFETY TM515

Transfer

The configured standard

SafeAPPLICATION is displayed in the

"Transfer" area. If it should be trans-

ferred, button "Transfer to SafeDO-

MAIN" is selected directly. Otherwise,

another SafeAPPLICATION can be used

via "Browse for data".

Figure 63: mapp Safety HMI application - Transferring SafeAPPLICATION

Completion

To complete commissioning and ex-

it setup mode, the correctness of the

transmitted data must be confirmed.

This is done in the "Completion" area.

The items to be confirmed are high-

lighted in white and must be completed

one after the other before setup mode

can be exited.

Figure 64: mapp Safety HMI application - Acknowledgment

Afterwards, a complete test of the application is required!

Safety technology \ mapp Safety \ Getting started \ Commissioning the application \ Using the precon-

figured HMI application

Exercise: Compiling and downloading

The objective of this exercise is to successfully compile the project and to test the two download options in order to

put the SafeLOGIC controller into operation.

Remote Control dialog box

1)Compile the safety application.

2)Opening the Remote Control dialog box

3)Download the safety application to the SafeLOGIC controller.

4)Complete the necessary acknowledgments

5)Check the LED status on the SafeLOGIC controller / the status messages of the Remote Control dialog box.

6)Formats the SafeKEY

## Page 37

COMMISSIONING THE SAFETY APPLICATION 37
mapp Safety HMI
1) Compile the safety application.
2) Compile and download the Automation Studio project.
3) Open the mapp Safety HMI application
4) Logging on and establishing a connection
5) Download the safety application to the SafeLOGIC controller.
6) Complete the necessary acknowledgments
7) Checking the LED status of the SafeLOGIC controller
6.4 Commissioning checklist
The following is a list of the most important steps for commissioning the SafeLOGIC controller / SafeLOGIC-X con-
troller, the SafeIOs and the safety application.
Procedure Note
Download the Automation Download configuration of the modules and mapp Safety
Studio project When formatting the CPU memory, reset the password for the SLX module.
Wait for startup The system performs the following steps:
Start default CPU
•
Start SafeLOGIC controller / SafeLOGIC-X controller
•
Install firmware updates on the modules
•
Initialize the hardware
•
Download the program Transfer via "Download" button in the Remote Control dialog box
Safety controller restarts
In mapp Safety HMI application:
Select SafeAPPLICATION and SafeCOMMISSIONING file
•
Transfer via button "Transfer to SafeDOMAIN"
•
Safety controller restarts
Acknowledge SafeKEY LED FW_ACKN lights up permanently
Acknowledge via SK_XCHG
Acknowledge new modules LED MXCHG blinks
Acknowledge new modules with 1, 2, 3, 4 or n
Acknowledge firmware LED FW_ACKN blinks
Acknowledge new firmware via FW_ACKN
Test application Status LEDs light up green
Test application
Table 4: Commissioning checklist
Commissioning in setup mode
Setup mode facilitates commissioning since the user can stabilize the machine when setup mode is active, test
firmware updates or make changes. In addition, acknowledgment requirements "SafeKEY exchange", "Firmware ac-
knowledge" and "UDID mismatch" are no longer required.
When commissioning is complete, setup mode is exited and a final overall test of the machine is required.
Active setup mode is indicated by both the FAILSAFE LED (X20SL81xx series) or SE LED (X20SLXxxx series) as well as
an entry in the logbook.
When setup mode is active, acknowledgment requests "SafeKEY Exchange", "Firmware Acknowledge" and "UDID Mis-
match" are no longer necessary.

## Page 38

38 PROGRAMMING AND COMMISSIONING SAFETY APPLICATIONS WITH MAPP SAFETY TM515
Setup mode can be enabled and disabled using the operating elements of the "Remote Control" in SafeDESIGNER
(X20SL81xx and X20SLXxxx series) or using the selector switch and acknowledgment button (X20SL81xx series). (9.2.2
"Operating elements" on page 63)
Safety technology \ mapp Safety \ Programming \ SafeDESIGNER \ Commissioning the safety controller
Safety technology \ mapp Safety \ Engineering \ SafeLOGIC \ Software functions \ Setup mode

## Page 39

IMPLEMENTING THE SAFETY APPLICATION39

7Implementing the safety application

7.1Using PLCopen function blocks

SafeDESIGNER provides various PLCopen function blocks that are used to implement the SafeAPPLICATION. In these

function blocks, the input signal is processed, the restart behavior is configured and the reset signal is connected.

The function blocks can be added to the worksheet via drag-and-drop using the Edit wizard and a window is automat-

ically opened in which the instance is created.

Figure 65: Adding a PLCopen function block for an emergency stop switch

Safety technology \ mapp Safety \ \ Programming \ SafeDESIGNER libraries \ PLCopen_SF \ Function

blocks

7.2Dual-channel evaluation for safe inputs

The safe digital inputs are used for safe sensors and additionally support

functions such as dual-channel evaluation and simultaneity monitoring.

The input signals of the signal pairs (e.g. channel 1 and 2) are monitored in

the module for simultaneity. The maximum permissible discrepancy time of

inputs of a signal pair is configurable.

The safe signal of a dual-channel sensor, such as an emergency stop device or

a light curtain, can thus be processed in the safety application via a variable.

Figure 66: Dual-channel evaluation being

performed directly on the safe module

Hardware \ X20 system \ X20 modules \

Digital input modules \ X20(c)SIx1x0

•

Digital mixed modules \ X20SC0xxx

•

Safety technology \ mapp Safety \ \ Engineering \ SafeIO \ Safe digital inputs

## Page 40

40PROGRAMMING AND COMMISSIONING SAFETY APPLICATIONS WITH MAPP SAFETY TM515

7.3Pulse source

The module provides pulse signals for diagnosing the sensor line. By default, each pulse signal provides a unique pulse

pattern derived from the module's serial number and pulse channel number. This allows any pulse signals to be com-

bined in one signal cable and still cover any cross fault combinations in the cable.

Internal pulse (pulse 1, 2, 3 or 4)

When connecting single-channel or dual-channel electromagnetic switches,

each input channel is assigned a dedicated clock output.

It is also possible to select a pulse of another channel in the configuration

depending on the wiring.

Figure 67: Single-channel connection

No pulse

The pulse check can be disabled to connect electronic sensors with separate

line monitoring (OSSD signals).

Figure 68: Single-channel connection - Sensors

with their own OSSD signal

If a clock pattern from the module is not used, the safe input module itself has reduced error detection.

All potential errors in the wiring must be detected through supplementary measures or by the connected

devices.

Safety technology \ mapp Safety \ Engineering \ SafeIO \ Safe digital inputs \ Connection examples

## Page 41

IMPLEMENTING THE SAFETY APPLICATION41

7.4Switching a safe output

To be able to set an output on the safe output module,

"start interlock on error" must first be removed via a posi-

tive edge of "ReleaseOutput". This must be done once per

module after a module or network error.

After that, the output can be switched using the "SafeDig-

italOutputxx" outputs in SafeDESIGNER and/or "Digi-

talOutputxx" in Automation Studio.

If this sequence is not observed, the output channel re-

mains inactive.

Figure 69: Sequence for switching a safe output

The release signal is a non-safe data type and can also be a signal from a digital input module in Automation Studio,

for example.

Figure 70: ReleaseOutput

Safety technology \ mapp Safety \ SafeIO \ Safe digital outputs \ "start interlock on error" state diagram

Exercise: Implement emergency stop function

The objective of this exercise is to use a PLCopen function block. This requires configuration of dual-channel evaluation,

switching of a safe output and selection of the correct pulse source for the safe channel.

1)Add function block "SF_EmergencyStop_V1_00"

2)Declare SafeTwoChannelInput on the SC module as global variable "SI_EStop"

3)Configure dual-channel evaluation (equivalent and discrepancy time = 50000 µs)

## Page 42

42PROGRAMMING AND COMMISSIONING SAFETY APPLICATIONS WITH MAPP SAFETY TM515

4)Declare SafeInput on the SC module as global variable "SI_Start"

5)"Change "PulseSource" from "SI_Start" to "Pulse 4"

6)Declare SafeOutput on the SC module as global variable "SO_Saw"

7)Connect function block "SF_EmergencyStop_V1_00"

8)Connect ReleaseOutput on the SC module to "SI_Start"

Result:

Figure 71: Implementation of the emergency stop function

7.5Switch-on and switch-off filter

Safe digital inputs are equipped with filters that are individually configurable for switch-on and switch-off behavior.

Switch-on filters are used to filter out signal disturbances.

Switch-off filters are used to smooth testing gaps in external signal sources – i.e. OSSD signals – so that unintended

cutoffs can be avoided.

Figure 72: Testing gap

Figure 73: Filter settings for safe inputs

## Page 43

IMPLEMENTING THE SAFETY APPLICATION43

Safety technology \ mapp Safety \ Engineering \ SafeIO \ Safe digital inputs

Exercise: Implement light curtain function

The objective of this exercise is to use a PLCopen function block, configure dual-channel evaluation, make settings for

an OSSD signal and link the emergency stop and light curtain signals.

1)Add function block "SF_ESPE_V1_00"

2)Declare SafeTwoChannelInput0102 on the SI module as global variable "SI_Lightcurtain"

3)Configure dual-channel evaluation (equivalent and discrepancy time = 50000 µs)

4)"Change "PulseSource" to "No Pulse"

5)Change the "Filter_off" setting to value 1000 µs

6)Connect function block "SF_ESPE_V1_00"

7)Link the output signal of the emergency stop and light curtain with an AND_S function and connect to "SO_Saw"

Result:

Figure 74: Implementing the light curtain function

7.6Communication channels between CPU and SafeLOGIC controller

To exchange data between the CPU and the SafeLOGIC controller, communication channels with different data types

are available.

The status of the inputs and outputs of the safe modules can be read directly in Automation Studio and evaluated in

a program using connected variables. The communication channels are thus only used to exchange internal data of

the safety application.

## Page 44

44PROGRAMMING AND COMMISSIONING SAFETY APPLICATIONS WITH MAPP SAFETY TM515

The number of channels is defined in the SafeLOGIC configuration in Automa-

tion Studio. The corresponding interfaces are available as inputs and outputs

and can be connected to variables in the I/O mapping in Automation Studio

and in the Safety View in SafeDESIGNER.

Figure 75: Setting for communication channels

in Automation Studio

These interfaces are non-safe data types and are used, for example, to exchange diagnostic codes of PLCopen function

blocks or to use a non-safe input for the reset signal.

Figure 76: Using communication channels in SafeDESIGNER

Hardware \ X20 System \ X20 modules\ CPUs \ X20(c)SL81xx \ Register description \ Parameters in the

I/O configuration

Exercise: Use communication channels and implement mode selector switch

The goal of this exercise is to implement communication channels between the CPU and SafeLOGIC controller to ex-

change the diagnostic codes and to evaluate the result of these.

Automation Studio

1)Add a new ST program

2)Declare the following variables and call them in the ST program:

Variable nameData typeDescription

diagCode_EStopUINT

PLCopen diagnostics code

Function blocks in SafeDESIGNERdiagCode_LightcurtainUINT

Table 5: Variables in Automation Studio

## Page 45

IMPLEMENTING THE SAFETY APPLICATION45

Variable nameData typeDescription

diagCode_ModeSelectorUINT

errorInSafetyApplicationBOOLResult of diagnostic code query

Table 5: Variables in Automation Studio

3)In the SafeLOGIC configuration, enable three "Communication from SafeLOGIC to CPU" channels with data type

UINT.

4)Connect variables for diagnostic codes with communication channels in the I/O mapping for the SafeLOGIC con-

troller.

5)Compile and transfer the project.

Result:

Figure 78: SafeLOGIC controller I/O mapping

Figure 77: Program in Automation Studio

SafeDESIGNER

1)Add function block "SF_ModeSelector_V1_00"

2)Declare SafeDigitalInputs on the SC module as global variable "SI_AutoMode" and "SI_ManualMode"

3)Change PulseSource from "SI_ManualMode" to "Pulse3"

4)Connect function block "SF_ModeSelector_V1_00"

5)Declare "ToCPU_UINT" channels on the SafeLOGIC controller as global variables

6)Connect variables for the diagnostic codes of all function blocks with output "DiagCode"

7)Implement automatic and manual mode

## Page 46

46PROGRAMMING AND COMMISSIONING SAFETY APPLICATIONS WITH MAPP SAFETY TM515

Result:

Figure 79: Implementing the mode selector switch function

## Page 47

IMPLEMENTING THE SAFETY APPLICATION47

7.7Creating a user function block

It is possible to create user function blocks via the shortcut menu of the

project tree in SafeDESIGNER.

Figure 80: Creating a new function block

A dialog box appears in which the name and programming language must

be defined. Ladder Diagram, Function Block Diagram and Structured Text

are available for selection. After the dialog box has been confirmed, the

function block can be opened and edited in the project tree.

Figure 81: Selecting the programming language for

the function block

A worksheet and a variable declaration are created for each function block.

VAR, VAR_INPUT and VAR_OUTPUT usage can be selected for local variables. Variables used as VAR_INPUT and

VAR_OUTPUT represent the inputs and outputs of the function block. The function block is supplied with data via

these inputs and outputs.

Figure 82: Defining instance variables 'VAR_INPUT, VAR_OUTPUT"

Each function block is programmed in a separate worksheet. It is possible to directly access I/O variables.

I/O variables must be declared globally.

Using a user function block

After the function block code has been compiled, it can be added to the work-

sheet using the Edit wizard. To facilitate the search function of the Edit wiz-

ard, the project name can be selected during grouping.

Figure 83: Adding a function block in the Edit

wizard via drag-and-drop

## Page 48

48PROGRAMMING AND COMMISSIONING SAFETY APPLICATIONS WITH MAPP SAFETY TM515

Locking a function block

After the functionality of the self-created function block has been tested suc-

cessfully, the function block should be locked in the shortcut menu via com-

mand "Set verification". A CRC value is thus created and the block can no

longer be changed.

Figure 84: Setting verification

Safety technology \ mapp Safety \ Programming \ SafeDESIGNER \

User interface \ Project tree - Overview

•

Developing a project \ Adding, deleting and renaming function block POUs

•

Exercise: Create a user function block

The objective of this exercise is to simplify the code in worksheet "Main" and to implement the functions as user

function blocks.

1)Implement function block "Safety_Function" for emergency stop, light curtain and mode selector switch

2)Implement function block "Auto_Manual_Mode" for the various operating modes

ParameterIN/OUTDescription

S_EStopINEquivalent signal from SC module

S_LightcurtainINEquivalent signal from SI module

S_AutoModeINSignal from SC module

S_ManualModeINSignal from SC module

S_StartINStart signal from SC module

S_EStop_SetOUTStatus of emergency stop

S_Lightcurtain_SetOUTStatus of light curtains

S_AutoMode_SetOUTStatus indicating if automatic mode was selected

S_ManualMode_SetOUTStatus indicating if manual mode has been selected

DiagCode_EStopOUTDiagnostic code for emergency stop block

DiagCode_LightcurtainOUTDiagnostic code for light curtain block

DiagCode_ModeSelectorOUTDiagnostic code for operating mode block

Table 6: Overview of the function block parameters for "Safety_Function"

ParameterIN/OUTDescription

S_EStop_SetINStatus of emergency stop

S_Lightcurtain_SetINStatus of light curtains

S_AutoMode_SetINStatus indicating if automatic mode was selected

S_ManualMode_SetINStatus indicating if manual mode has been selected

S_StartINCPU communication channel

Table 7: Overview of the function block parameters for "Auto_Manual_Mode"

## Page 49

IMPLEMENTING THE SAFETY APPLICATION49

ParameterIN/OUTDescription

S_SawOUTStatus indicating if all security relevant conditions are fulfilled

Table 7: Overview of the function block parameters for "Auto_Manual_Mode"

Result:

Figure 85: Creating a user function block

## Page 50

50PROGRAMMING AND COMMISSIONING SAFETY APPLICATIONS WITH MAPP SAFETY TM515

8SafeCOMMISSIONING

SafeCOMMISSIONING makes it possible to adapt the characteristics of the machine at runtime. The following steps

are required for this:

Preparing the safety application

For the SafeLOGIC controller, the availability of individual modules can be influenced via parameter "Availability source"

and for SafeNODEs via parameter "Availability".

Depending on which SafeCOMMISSIONING file is active, the "Safe Commissioning Options" parameters can be used

to change the values of variables according to the configuration.

Figure 86: SafeCOMMISSIONING parameters in the "Safety" view

Creating the SafeCOMMISSIONING file

Various SafeCOMMISSIONING files are created and edited via the configura-

tor in the "Project \ SafeCOMMISSIONING" area.

Figure 87: Opening the SafeCOMMISSIONING

configurator

Any number of SafeCOMMISSIONING files can be created per application with

different configurations. This makes it possible to configure different vari-

ants of a machine. In this example, two new SafeCOMMISIONING files are cre-

ated, one with and one without light curtain evaluation.

Figure 88: SafeCOMMISSIONING configurator

## Page 51

SAFECOMMISSIONING51

The parameters of a SafeCOM-

MISSIONING file can be named and

grouped freely. Parameter "SafeOp-

tionBool" is set to either SAFETRUE or

SAFEFALSE via property "Enabled". An

analog limit value is defined for an inte-

ger parameter, for example.

Figure 89: SafeOptionBool

SafeNodeAvailability is used to define

for each safe module whether or not

the module is available in this machine

variant.

The "Lock" button is used to assign a

CRC value to the SafeCOMMISSIONING

file and create the XML files.

Figure 90: Availability of the SafeNODEs

Configuration in Automation Studio

There are now two .sfopt files in folder "SafeCOMMISSIONING" in the Configuration View. A default SafeCOM-

MISSIONING file can be selected in the .sfdomin configuration. These changes are then transferred to the controller.

Figure 91: Configuration in Automation Studio

## Page 52

52PROGRAMMING AND COMMISSIONING SAFETY APPLICATIONS WITH MAPP SAFETY TM515

Selection and transfer

The SafeCOMMISSIONING.xml file is

available for download in the "Trans-

fer" area of the mapp Safety HMI

application. Alternatively, a different

SafeCOMMISSIONING.xml file can be

selected from the transferred files.

Figure 92: SafeCOMMISSIONING file being transferred via the mapp Safety HMI application

Safety technology \ mapp Safety \ Getting started \ Creating a SafeAPPLICATION \ SafeCOM-

MISSIONING

Exercise: Implement SafeCOMMISSIONING

The objective of this exercise is to define two different hardware expansion stages and to select these via the mapp

Safety HMI application (3.1.2 "Light curtain function" on page 12).

1)Declare SafeCommissioningOptionBIT on the SafeLOGIC controller as "Disable_Lightcurtain"

2)SafeCOMMISSIONING configuration

BasicWithLightcurtain

SafeOptionBool  Disable→

°

SafeNodeAvailability SL1.SM2  Present→

°

SafeNodeAvailability SL1.SM3  Present→

°

NoLightcurtain

SafeOptionBool  Enabled→

°

SafeNodeAvailability SL1.SM2  NotPresent→

°

SafeNodeAvailability SL1.SM3  Present→

°

3)Function block "SafetyFunction" - Create new input variable "S_DisableLightcurtain" and adapt code

4)Download changes to the SafeLOGIC controller via the Remote Control dialog box

5)Change default SafeCOMMISSIONING file in the .sfdomain configuration in Automation Studio

6)Download the Automation Studio project.

7)Open the Remote Control dialog box in SafeDESIGNER and enable setup mode

8)Open the mapp Safety HMI application (http://:81/index.html?visuId=mappSAFETY)IP address

9)Connect to SafeLOGIC controller

10)Click the "Browse for data" button, select the SafeCOMMISSIONING file and transfer it to the SafeDOMAIN

11)Acknowledge SafeCOMMISSIONING and SafeNODEs and exit setup mode

Now you can choose between two SafeCOMMISSIONING files via the mapp Safety HMI application and,

depending on which file is active, the function of the light curtain is enabled or disabled.

## Page 53

SAFECOMMISSIONING53

Result:

## Page 54

54PROGRAMMING AND COMMISSIONING SAFETY APPLICATIONS WITH MAPP SAFETY TM515

9Diagnostics and service for safety ap-

plications

9.1Diagnostics for the SafeAPPLICATION

9.1.1Checking I/O status bits

There is one bit of information about the state of the channel or multi-channel evaluation for each I/O channel. Safe

output modules provide additional information about the physical switching state.

Figure 93: Status information for a safe input module

Figure 94: Status information for a safe output

module

To receive the status information, the status information channels in the worksheet must be used.

## Page 55

DIAGNOSTICS AND SERVICE FOR SAFETY APPLICATIONS55

Automation Studio's I/O mapping al-

so provides status information about

the safe modules for diagnostics in the

form of non-safe data points.

Figure 95: I/O status bits in Automation Studio

9.1.2Checking the variable status

SafeDESIGNER offers a function to make current values of the variables visible. This

function is enabled via the following button:

Figure 96: Button

"Variable status"

Variable status in the variable declaration

Button "Global declaration" or "Toggle WS" can be used to switch to the variable declaration. Button "Variable status"

is used to display the current values of the variables in column "Online value".

Figure 97: Variable status in the global declaration window

Variable status in the visual editor

The visual editor displays the current value for each variable. The connecting lines between the function blocks are

color-coded according to the variable value.

## Page 56

56PROGRAMMING AND COMMISSIONING SAFETY APPLICATIONS WITH MAPP SAFETY TM515

ColorMeaning

RedSignal connected through

BlueSignal not connected

through

GreenConstants and information

Table 8: Color legend in the variable status

Figure 98: Variable status in the worksheet

Variable status in the Watch window

Each variable can be added to the Watch window via the shortcut menu when the variable status is enabled. In debug

mode, variables can be forced in the Watch window. (9.1.3 "Forcing variables" on page 56)

Figure 99: Variable status in the Watch window

Safety technology \ mapp Safety \ Programming \ SafeDESIGNER \ Commissioning the safety controller

\

Monitoring: Displaying the variable status

•

Monitoring: Using the Watch window

•

9.1.3Forcing variables

To force variable values, debug mode must be enabled on the SafeLOGIC con-

troller. First, the SafePLC dialog box is opened (SafeAPPLICATION password

required) and then, debug mode is switched on via button "Debug".

Figure 100: SafePLC dialog box

Forced variables can result in dangerous situations on the machine. Therefore, it must always be ensured

that the machine is secured appropriately.

Double-clicking on the variable opens a wizard for enabling the force operation. The action must be acknowledged by

the user, whereby an information window draws attention to the possible dangers.

## Page 57

DIAGNOSTICS AND SERVICE FOR SAFETY APPLICATIONS57

Figure 101: Forcing the variable

In the visual editor, the forced variables

are distinguished by a pink background

color.

Figure 102: Forced variables

All forced variables are reset when switching to safe mode via the communication window and a message window

appears, which must be confirmed.

Safety technology \ mapp Safety \ Programming \ SafeDESIGNER \ Commissioning the safety controller

\ Debugging: Forcing, overwriting, single-cycle mode

9.1.4Cyclic execution of the safety application

SafeDESIGNER allows the safety application to be executed in single cycles

for testing purposes. To do so, it is necessary to first switch to debug mode

and then to the stop state by clicking on the "Stop" button.

Clicking on the "Single cycle" button will execute the safety application one

time.

Figure 103: Stop state

To return to safe mode, first select the "Next" button and then "Safe".

Safety technology \ mapp Safety \ Programming \ SafeDESIGNER \ Commissioning the safety controller

Debugging: Forcing, overwriting, single-cycle mode

•

Dialog boxes for setting the SafePLC \ Dialog box "Debug"

•

## Page 58

58PROGRAMMING AND COMMISSIONING SAFETY APPLICATIONS WITH MAPP SAFETY TM515

9.1.5Simulating the application

Simulation can be used to test the safety application independently of the hardware.

This virtual SafeLOGIC controller is started via the "Simulate" button.

Figure 104: Starting

the simulation

A compilation procedure starts automatically and the simulated SafeLOGIC

controller is started. An icon in the "Information" area of the Windows taskbar

indicates that the simulation is running.

Figure 105: Symbol in system tray

If the simulation is active, the download procedure can only be started via the SafePLC dialog box. For this, it is nec-

essary to first switch to debug mode, then stop the simulation using the "Stop" button and finally start the download.

The safety application can now be tested.

Safety technology \ mapp Safety \ Programming \ SafeDESIGNER \ EasySim controller simulation

Exercise: Observe and manipulate application in a simulation and on real hardware

The objective of this exercise is to transfer the application to the simulation, enable the variable status and force values.

Afterwards, the simulation is disabled again and the program is observed and manipulated on the real hardware using

the variable status.

Diagnostics in simulation

1)Enable simulation (project is compiled automatically)

2)Open "SafePLC" dialog box

3)Select debug mode

4)Stop the SafeLOGIC controller

5)Download project

6)Enable variable status

7)Force values

8)Disable simulation

Diagnostics on real hardware

1)Enable variable status

2)Open "SafePLC" dialog box

3)Select debug mode

4)Force values

## Page 59

DIAGNOSTICS AND SERVICE FOR SAFETY APPLICATIONS59

9.1.6Automation Studio Logger window

The system keeps a logbook for safety-related components and for safe communication. The Logger data can be

accessed in Automation Studio via main menu "Open \ Logger".

Figure 106: Safety entries in the Automation Studio Logger

The following entries are recorded in this logbook:

Replacing safety modules

•

Configuring safety modules

•

Downloading the application

•

Updating the firmware

•

Changing to the FAIL SAFE state

•

Warnings for channel errors

•

...

•

Diagnostics and service \ Diagnostics tools \ Logger

## Page 60

60PROGRAMMING AND COMMISSIONING SAFETY APPLICATIONS WITH MAPP SAFETY TM515

9.1.7Project comparison

SafeDESIGNER provides the user with the opportunity to compare projects

to each other. This means that the safety application, configuration and pa-

rameters of the selected projects are compared.

Two different methods can be selected for project comparison:

Compare projects...

•

Compare to project on the safety controller...

•

Figure 107: Starting project comparison

Compare projects...

This option is used to compare the project currently opened in SafeDESIGNER with a locally stored project. Window

"Project Comparer" opens in which the project to be compared is defined. A file with the extension .swt must be se-

lected.

Compare to project on the safety controller...

The second option is to compare it with a project from the safety controller.

To do so, it must be connected and the project sources must have been stored.

Under "Online \ Communication parameters" in the lower part of the window,

there is an entry that must be selected to download the code to the SafeLOGIC

controller. The entry must be deselected if storing is not desired.

Figure 108: Storing project sources on the safety

controller

Project comparison is divided into two areas. In the lower area, the differences are displayed in a tree structure. Select-

ing an entry opens the corresponding file in the upper area. The differences are shown here and labeled in different

colors.

## Page 61

DIAGNOSTICS AND SERVICE FOR SAFETY APPLICATIONS61

Figure 109: Project comparison in SafeDESIGNER with the current project on the left, the project to be compared on the right

Safety technology \ mapp Safety \ Programming \ SafeDESIGNER \ Introduction \ Comparing projects

9.2Operating and status elements

SafeLOGIC

The SafeLOGIC controller is equipped with an application interface that con-

tains LED status indicators, a selector switch and a confirmation button. The

user can view states via this application interface and perform various ac-

tions.

Figure 110: Operating and status elements on

the SafeLOGIC controller

## Page 62

62PROGRAMMING AND COMMISSIONING SAFETY APPLICATIONS WITH MAPP SAFETY TM515

Remote Control dialog box

The Remote Control dialog box is an interface that can be used to operate a

SafeLOGIC controller and SafeLOGIC-X controller. The current status of the

safety controller and various buttons are displayed. The buttons are locked,

highlighted or neutral depending on the operating possibility. This prevents

incorrect entry.

Figure 111: Remote Control dialog box

9.2.1Status elements

LED status indicators of the SafeLOGIC controller

The LED status indicators of the safety processor are used to indicate the

status of the SafeLOGIC controller, safety application and safe modules.

The LED status indicators indicate different operating and error states via

different blinking states or continuous illumination.

For a detailed breakdown, see the manual for the SafeLOGIC controller and

Automation Help.

Figure 112: SafeLOGIC

Hardware \ X20 system \ X20 modules \ CPUs \ X20(c)SL81xx \ Operating and connection elements \

Safety processor \ LED status indicators of the safety processor

Status element of the Remote Control dialog box

The status of the SafeLOGIC controller / SafeLOGIC-X controller is displayed in the corresponding text fields in the

Remote Control dialog box.

Figure 113: SafeLOGIC

1)SafeOS status  Status of the operating system of the SafeLOGIC controller→

2)Module status  Message about the state of the module configuration ("OK", "Acknowledgment required" or→

"Modules missing")

3)System status  Status of the system ("Firmware / SafeKEY OK" or "Acknowledgment required")→

4)Operation mode  Mode of the SafeLOGIC controller ("Operational", "Pre-operational" or "FailSafe")→

Safety technology \ mapp Safety \ Programming \ SafeDESIGNER \ Dialog boxes for controlling the

safety controller \ Remote Control \ Status indicators for Remote Control

## Page 63

DIAGNOSTICS AND SERVICE FOR SAFETY APPLICATIONS63

9.2.2Operating elements

Actions can either be performed directly on the SafeLOGIC controller or via the Remote Control dialog box in

SafeDESIGNER.

Entry via SafeLOGIC controller

Confirmation key ENTER on the SafeLOGIC controller must be pressed be-

tween 500 milliseconds and 4 seconds later.

This does not apply when the setup mode is enabled or disabled and when

the SafeKEY is formatted. In these cases, the button must be pressed for 20

to 30 seconds.

If the entry is correct, the ENTER LED blinks red, whereas blinking tree times

indicates an incorrect entry.

Figure 114: Operating elements on the

SafeLOGIC controller

Hardware \ X20 system \ X20 modules \ X20 CPUs \ X20(c)SL81xx \ Operating and connection elements

\ Safety processor \ Selector switch and confirmation button

Entry via Remote Control dialog box

Using the Remote Control dialog box prevents a wrong entry. All buttons that are not permitted to be selected at the

respective time are disabled.

Safety technology \ mapp Safety \ Programming \ SafeDESIGNER \ Commissioning the safety controller

\ Dialog boxes for controlling the safety controller \ Remote Control

Description of operating elements

SafeLOGICRemote ControlDescription

FW-ACKNFW-ACKNConfirms the firmware

Between FW-ACKN and SK-Setup Mode on/offEnables and disables the setup mode8

COPY7

TESTTESTPerforms an LED test

-CLEAR DATADeletes subsequently loadable data from the

SafeKEY

1, 2, 3, 4, n1, 2, 3, 4, nAcknowledges new modules

SCANSCANTriggers a module scan

SK-XCHGSK-XCHGAcknowledges the SafeKEY

Between SK-XCHG and FW-SK-FORMATFormats the SafeKEY

ACKN1

Table 9: Operating elements for the SafeLOGIC controller and Remote Control dialog box

7The acknowledgment button must be pressed for 20 to 30 seconds.

8The setup mode must be disabled again after commissioning has been completed.

## Page 64

64PROGRAMMING AND COMMISSIONING SAFETY APPLICATIONS WITH MAPP SAFETY TM515

SafeLOGICRemote ControlDescription

-Change passwordChanges the SafeKEY password

-DownloadDownloads the safety application

-Reset SafeLOGICRestarts the SafeLOGIC controller

Table 9: Operating elements for the SafeLOGIC controller and Remote Control dialog box

9.2.3LED status indicators on SafeIO modules

The LED status indicators of the safe I/O modules are used for initial diagnos-

tics of the input and output channels and the operating state of the module.

An error in the dual-channel evaluation can be indicated via the 00 and 0C

LEDs, for example.

The LED status indicators indicate warnings and errors via unique blinking

codes and permanent illumination.

For additional information, see the manual or Automation Help.

Figure 115: LED status indicators of a safe input

module

Hardware \ X20 system \ X20 modules \ Analog input modules \ X20(c)SA4430 \ LED status indicators

Hardware \ X20 system \ X20 modules \ Digital output modules \ X20(c)S0x1x0 \ LED status indicators

Hardware \ X20 system \ X20 modules \ Digital input modules \ X20(c)SIx1x0 \ LED status indicators

Hardware \ X20 system \ X20 modules \ Temperature modules \ X20ST4492 \ LED status indicators

Hardware \ X20 system \ X20 modules \ Counter modules \ X20(c)SD1207 \ LED status indicators

9.3Module replacement and update

9.3.1Replacing a module

The system checks the safety-related hardware configuration at a time inter-

val defined by the system. If a new module is detected, this is indicated via

the SafeLOGIC controller.

Figure 116: Replacing a module

Remote Control dialog box:

1)"(Number of new modules) Module exchange" is displayed as the module status of in the Remote Control dialog

box.

2)Confirm with orange marked button 1, 2, 3, 4 or n.

3)Perform a test of the affected portion of the machine.

## Page 65

DIAGNOSTICS AND SERVICE FOR SAFETY APPLICATIONS65

Figure 117: Confirming module replacement via the Remote Control dialog

box

mapp Safety HMI application:

1)"Safenodes acknowledge required" is displayed in the "State" field in the top right corner.

2)Confirm with button "Acknowledge exchange" in the "Exchange" area.

3)Perform a test of the affected portion of the machine.

Figure 118: Confirming module replacement in the mapp Safety HMI application

The Exchange dialog box is there to exchange SafeNODEs.

A SafeLOGIC or SafeLOGIC-X controller exchange is therefore not possible.

## Page 66

66PROGRAMMING AND COMMISSIONING SAFETY APPLICATIONS WITH MAPP SAFETY TM515

SafeLOGIC:

1)LED MXCHG blinks (indicates the number of new modules)

2)Set 1, 2, 3, 4 or n for the selector switch and confirm with ENTER.

3)Perform a test of the affected portion of the machine.

If more modules were replaced than indicated, a manual scan can be started via the SCAN button. This

might be necessary for large machines on which the automatic scan takes more time.

9.3.2Replacing the SafeLOGIC controller

A SafeLOGIC controller is replaced in the same way as a normal module.

When replacing a SafeLOGIC controller, the SafeKEY from the SafeLOGIC con-

troller being replaced must be applied in order to avoid enabling an old safe-

ty-related application.

The safety application and the device parameters of a SafeLOGIC-X controller

are stored in the safety section of the flash memory of a controller. When the

SafeLOGIC-X controller is replaced, the control system is automatically ac-

cessed and the current SafeAPPLICATION downloaded.

Figure 119: Replacing the SafeLOGIC controller

1)Replace the SafeLOGIC controller / SafeLOGIC-X controller.

2)Only with SafeLOGIC: Connect the old SafeKEY.

3)Acknowledge the new SafeKEY after booting:

Remote Control dialog box: Confirm with button SK-XCHG.

°

SafeLOGIC: Set SK-XCHG for the selector switch and confirm with ENTER.

°

4)The module to be acknowledged is displayed.

5)Acknowledge the new SafeLOGIC controller:

Remote Control dialog box: Confirm with orange marked button "1".

°

SafeLOGIC: Set "1" for the selector switch and confirm with ENTER.

°

6)No test is required.

With new hardware, it may be that a firmware update is performed on the SafeLOGIC controller / SafeL-

OGIC-X controller. This extends the startup time and an entry is created in the Logger in Automation

Studio.

It is currently not yet possible to perform a SafeLOGIC or SafeLOGIC-X exchange via the mapp Safety

HMI application.

In this case, commissioning must be performed via the HMI application starting with a "SafeKEY format".

## Page 67

DIAGNOSTICS AND SERVICE FOR SAFETY APPLICATIONS67

9.3.3Confirming a firmware update

After a module has been replaced, the safety application has been extended

by further modules or a firmware update has been installed, it may be neces-

sary that the system performs a firmware update on a module.

Figure 120: Updating the firmware

Remote Control dialog box:

1)After a firmware update has been completed, "FW updated" is displayed in the system status of the Remote Con-

trol dialog box.

2)Acknowledge with orange marked button "FW-ACKN".

3)The modules are updated and started.

4)Perform a complete test of the safety application.

SafeLOGIC:

1)The FW-ACKN LED blinks after the firmware update.

2)Set FW-ACKN for the selector switch and confirm with ENTER.

3)The modules are updated and started.

4)Perform a complete test of the safety application.

9.3.4Module is missing

The system checks the safety-related hardware configuration at a defined

time interval. If a missing module is detected, this is indicated in the Remote

Control dialog box, in System Diagnostics Manager and/or via blinking LED

status indicators on the SafeLOGIC controller.

Figure 121: Module is missing

Remote Control dialog box:

Displayed in the module status "(Number of missing modules)  modules missing"

•

System Diagnostics Manager:

Hardware error  Displayed in detail in the hardware tree→

•

## Page 68

68PROGRAMMING AND COMMISSIONING SAFETY APPLICATIONS WITH MAPP SAFETY TM515

SafeLOGIC:

LED MXCHG blinks fast and LED FAILSAFE blinks twice.

•

9.3.5Updating the safety application

The safety application can be updated either via the mapp Safety HMI appli-

cation, the Remote Control dialog box or by connecting a preprogrammed

SafeKEY (only with SafeLOGIC).

For a download via the mapp Safety HMI application, it must be ensured that

the most current file is available on the controller.

To ensure that the safe configuration and the safe parameters are not lost

when the SafeKEY is replaced, the SK-COPY function of the SafeLOGIC con-

troller can be used to copy the settings.

Figure 122: Update via the SafeKEY

:mapp Safety HMI application

1)Download the latest version of SafeAPPLICATION to the controller via Automation Studio.

2)Open the mapp Safety HMI application (http://<IP-ADRESSE>:81/index.html?visuId=mappSAFETY).

3)Log in and connect to the SafeLOGIC controller.

4)Select the SafeAPPLICATION in the "Transfer" area.

5)Update the SafeAPPLICATION via button "Transfer to SafeDOMAIN".

6)Perform a complete test of the safety application.

:Remote Control dialog box

1)Compile a SafeDESIGNER project.

2)Opening the Remote Control dialog box

3)Transfer the project via the "Download" button.

4)Perform a complete test of the safety application.

:SafeLOGIC

1)Old SafeKEY is connected.

2)Set SK-COPY for the selector switch and confirm with ENTER.

3)Configuration data from the SafeKEY are copied into the RAM of the SafeLOGIC controller.

4)Remove the old SafeKEY and connect the new one.

5)Confirm with ENTER; SafeLOGIC is rebooted.

6)Perform a complete test of the safety application.

Safety technology \ mapp Safety \ Diagnostics and service \ Maintenance scenarios \ SafeKEY or

safety section of the CompactFlash card \ Changing the application on the SafeLOGIC controller by

replacing the SafeKEY (X20SL8xxx series only)

## Page 69

DIAGNOSTICS AND SERVICE FOR SAFETY APPLICATIONS 69
Exercise: Replace safe I/O module
The objective of this exercise is to disable module monitoring for the safe modules, to replace a safe module with
another during operation and to acknowledge the module replacement.
1) Disable module monitoring in Automation Studio.
2) Compile the changes and transfer them to the controller.
3) Disconnect the SI module.
4) Connect the SI module with a different serial number.
5) Acknowledge module replacement on the SafeLOGIC controller or in the Remote Control dialog box.
6) Perform a test of the affected portion of the machine.

## Page 70

70PROGRAMMING AND COMMISSIONING SAFETY APPLICATIONS WITH MAPP SAFETY TM515

10Further information

Further sources of information are available to deepen participants' understanding of the previous top-

ics.

TopicSource

B&R website \ Technology \ Safety technology

www.br-automation.com/en-gb/technologies/safe-

Safety technology

•ty-technology/

openSAFETY

•

B&R website \ Products \ Safety technologywww.br-automation.com/en-gb/products/safety-tech-

nology/

mapp Safety in Automation Help

Safety technology \ mapp Safety

Figure 123: mapp Safety help documentation - Start page

Integrated safe motion controlTraining module TM540

10.1Project documentation and printing

SafeDESIGNER can be used to create the documentation of the safety application. The documentation interface can

be opened via main menu "Project \ Project information".

Figure 124: Project documentation

## Page 71

FURTHER INFORMATION71

The following information belongs to the project information:

Manufacturer

•

Contact details of the machine manufacturer, such as name and address, are entered here.

Project

•

Data about the machine and the safety application are entered here. The project data is automatically maintained

by the system and the unique CRC number of the safety application is also available.

Persons in charge

•

This tab is used to enter the persons who are in charge of the project, such as the project manager, the programmer

of the safety application and the person who tests the safety application.

Safety functions

•

The safety functions configured for the machine are entered here. Information about a safety function that was

successfully tested during integration can be entered here.

Commissioning checklist

•

This list serves as a support for validation. The programmer of the safety application enters data, for example for

the network connections and wiring, here. This data must be observed later during commissioning.

History

•

The history of the safety application is managed here. For each revision, the corresponding CRC number and revi-

sion history is entered.

Yellow fields should always be filled in. Grey fields can be filled in optionally.

Project printout

After the project information has been filled in, the project documentation

can be printed. The selection dialog box is displayed via main menu "File \

Printing project".

Additional settings for the documentation can be made using button "Page

layout texts".

Figure 125: Print selection for the

documentation

Safety technology \ mapp Safety \ Programming \ SafeDESIGNER \ Developing a project \

Dialog box "Project info"

•

Printing a project

•

10.2How to connect to a safety controller

There are several ways to connect to a safety controller. The following table shows which of the connection types work

with the various safety controllers. The following pages explain the connection options in more detail.

Connection optionsSafeLOGICSafeLOGIC-X

Online connection via a standard CPU

Online via direct connection

Manual connection

Table 10: Options for connecting to the SafeLOGIC controller / SafeLOGIC-X controller

## Page 72

72PROGRAMMING AND COMMISSIONING SAFETY APPLICATIONS WITH MAPP SAFETY TM515

An online connection via a standard CPU is used in this training module.

Online connection via a standard CPU

The existing connection with the standard CPU can be used to establish on-

line communication with the safety controller. The standard CPU automati-

cally routes the data for this.

When SafeDESIGNER is opened, the current online settings of Automation

Studio are called. If there is an online connection to a controller at this time,

this IP address is used for the online settings in SafeDESIGNER and a connec-

tion can be made to the safety controller.

Figure 126: Online via a standard CPU

In SafeDESIGNER, communication to the SafeLOGIC controller can be con-

figured via menu option . The preset de-Online / Communication settings

fault value is the connection "SL - communication through BR-CPU".

Figure 127: Communication settings "SL -

communication through BR-CPU"

Online via direct connection

Using the direct connection, the PC is connected directly to the SafeLOGIC

controller via a network cable. An IP address from the address range of the

POWERLINK network (192.168.100.xxx) is set on the PC. The SafeLOGIC con-

troller always uses the set node number as the last position of the IP address.

Figure 128: Online via direct connection

## Page 73

FURTHER INFORMATION73

The dialog box for the connection settings in SafeDESIGNER is opened un-

der "Online \ TCPIP communication parameters". Option "SL directly con-

nected" must be selected for the direct connection.

Figure 129: Communication settings "SL directly

connected"

Manual connection

When connecting manually, the user can set all communication parameters, e.g. the IP address and port number.

Safety technology \ mapp Safety \ Programming \ SafeDESIGNER \ Commissioning the safety controller

\ Communication settings

10.3Support for third-party devices with openSAFETY

Operating an openSAFETY third-party device in Automation Studio requires an .xosdd

and .xdd file from the device manufacturer. These files contain information about the

configuration and functionality of the device.

New devices can be added via menu item "Extras \ Managing 3rd-party devices".

Figure 130: Adding 3rd-party

devices

With "Importing fieldbus device(s)", a

selection dialog box is opened in the

current dialog box where the .xosdd file

must be selected first and then the .xdd

file.

Figure 131: Selecting a fieldbus device

## Page 74

74PROGRAMMING AND COMMISSIONING SAFETY APPLICATIONS WITH MAPP SAFETY TM515

After the files have been imported, the

device can be added and configured via

the Hardware Catalog.

Figure 132: Adding a 3rd-party device

10.4Example projects and solutions

The example code in this documentation is exclusively for implementing the test setup for this training.

This example code is not permitted to be adopted for safety applications under any circumstances.

Each safe machine application must be subjected to a separate risk analysis and a safety concept must

be created.

10.4.1Sample solution - Project

Main code

The main code contains the query for the "ModuleOk" parameters, the self-created function blocks and the connection

between the reset signal and ReleaseOutput.

Figure 133: Code: Main

## Page 75

FURTHER INFORMATION75

Function block: Auto_Manual_Mode

Functionality for automatic and manual mode is implemented in this function block.

Figure 134: Auto_Manual_Mode function block

Function block: Safety_Function

The PLCopen function blocks for emergency stop, light curtain and mode selector switch are called and connected in

this function block. Enabling and disabling the light curtain is also implemented via the SafeCOMMISSIONING file.

## Page 76

76PROGRAMMING AND COMMISSIONING SAFETY APPLICATIONS WITH MAPP SAFETY TM515

Figure 135: Safety_Function function block

10.4.2Sample solution - Variables

Global variables

This file contains all variables that are connected to a safe input or output as well as the variables for the communi-

cation channels.

## Page 77

FURTHER INFORMATION77

Figure 136: Global variable declaration

Local variables: Main code

Created user function blocks and the variable "AllSafeModulesOK" for the module status of all safe modules are here.

Figure 137: Local variable declaration for main code

Local variables: SafetyFunction

The inputs on this function block serve as an interface for the safe inputs and for the reset signal. The instances of the

PLCopen function blocks are declared as internal variables and the status of the safety functions and the diagnostic

codes of the PLCopen function blocks are declared as outputs.

## Page 78

78PROGRAMMING AND COMMISSIONING SAFETY APPLICATIONS WITH MAPP SAFETY TM515

Figure 138: Local variable declaration for SafetyFunction function block

Local variables: Auto_Manual_Mode

In the Auto_Manual_Mode function block, the information of the PLCopen safety functions (emergency stop, light

curtain and mode selector switch) and the green button are declared as inputs and the signal for the safe output as

output.

Figure 139: Local variable declaration for the Auto_Manual_Mode function block

## Page 79

FURTHER INFORMATION79

10.4.3Sample solution - Parameters for the safe hardware modules

Parameter SafeLOGIC

With the SafeLOGIC controller,

"SafeCOMMISSIONING" only needs to

be set as "Availability Source".

Figure 140: Parameter X20SL8101

Parameter X20SI2100

The inputs from the safe input module

are wired to the light curtain. This sen-

sor already has its own test pulse, so it

has to be disabled on the module and a

filter must be configured. The light cur-

tain is evaluated via multi-channel eval-

uation using an equivalent signal.

Figure 141: Parameter X20SI2100

## Page 80

80PROGRAMMING AND COMMISSIONING SAFETY APPLICATIONS WITH MAPP SAFETY TM515

Parameter X20SC0806

The safe mixed module is wired to

the emergency stop, the mode selector

switch, the green button and the "Saw"

output. The emergency stop signal is

read out equivalently. The pulse for the

mode selector switch and green button

must be changed.

Figure 142: Parameter X20SC0806

Parameter openSAFETY Datalogic

light curtain

With the Datalogic light curtain, the

parameters for resolution and length

must be changed depending on the

model type.

Figure 143: Parameter SG4-x-x-OP-B

## Page 81

SUMMARY81

11Summary

The first steps for a safety application are done in Automation Studio by configuring all safe hardware components

and mapp Safety. After this has been completed, the next step is to start the SafeAPPLICATION in SafeDESIGNER.

SafeDESIGNER is used for the safety-related configuration of the safety controller and safe modules. The safety appli-

cation can be configured using the visual editor and a large number of PLCopen Safety function blocks. A convenient

user interface and numerous diagnostic options facilitate commissioning of the safety controller.

The mapp Safety HMI application makes it possible to download a SafeAPPLICATION and select the SafeCOM-

MISSIONING file even without SafeDESIGNER. In addition to the advantages already mentioned, SafeDESIGNER pro-

vides complete project documentation and an integrated simulation environment.

Figure 144: mapp Safety

## Page 82

82PROGRAMMING AND COMMISSIONING SAFETY APPLICATIONS WITH MAPP SAFETY TM515

Automation Academy

Gain additional knowledge

The Automation Academy provides  courses for our customers as well as for our own employees.targeted training

Expand your skills in the field of automation technology and learn to independently implement efficient automation

using B&R systems.solutions

Decide for yourself which  you prefer!learning concept

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

## Page 83

AUTOMATION ACADEMY 83

## Page 84

B&R

Industrial Automation GmbH

A member of the ABB Group

B&R Straße 1

5142 Eggelsberg, Austria

office@br-automation.com

t +43 7748 6586-0

f +43 7748 6586-26

br-automation.com

V2.0.0.2 ©2025/03/10 by B&R, All rights reserved.

All registered trademarks are the property of their respective owners.

Subject to technical changes without notice.