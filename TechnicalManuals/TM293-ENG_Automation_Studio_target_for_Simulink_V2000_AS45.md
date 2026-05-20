## Page 1

TM293

Automation Studio

Target for Simulink

## Page 2

2 AUTOMATION STUDIO TARGET FOR SIMULINK TM293
Requirements
TM210 - Working with Automation Studio
TM213 - Automation Runtime
Training modules TM223 - Automation Studio diagnostics
Starting with MATLAB R2017b
C/C++ compiler
Software Automation Studio 4.x.x or later
Hardware None

## Page 3

TABLE OF CONTENTS 3
Table of contents
1 Introduction.........................................................................................................................................................5
1.1 Learning objectives..............................................................................................................................6
1.2 Symbols and safety notices...............................................................................................................6
2 General information...........................................................................................................................................7
2.1 Simulation..............................................................................................................................................7
2.2 Model-based development................................................................................................................8
2.3 Virtual commissioning......................................................................................................................10
3 MATLAB...............................................................................................................................................................11
3.1 User interface......................................................................................................................................11
3.2 Help documentation..........................................................................................................................11
3.3 Simulink................................................................................................................................................13
4 Automation Studio Target for Simulink......................................................................................................15
4.1 Installation and registration............................................................................................................16
4.2 Help documentation..........................................................................................................................19
5 My first project.................................................................................................................................................23
6 Principle of automatic code generation.....................................................................................................24
7 Interface to Automation Studio - B&R config block.................................................................................26
7.1 Basic settings......................................................................................................................................26
7.2 Automatic transfer.............................................................................................................................27
7.3 External mode.....................................................................................................................................28
7.4 Cycle time and task class.................................................................................................................30
7.5 Dependence on additional files.......................................................................................................31
7.6 Additional functionality.....................................................................................................................31
8 Variables, constants and arrays...................................................................................................................33
8.1 Code generation.................................................................................................................................33
8.2 B&R input block..................................................................................................................................35
8.3 B&R output block...............................................................................................................................37
8.4 Creating internal variables and constants...................................................................................37
8.5 Creating arrays...................................................................................................................................39
9 Creating user libraries....................................................................................................................................43
10 Structures........................................................................................................................................................46
10.1 B&R bus block...................................................................................................................................46
10.2 Example: Bus block..........................................................................................................................47
10.3 B&R struct block...............................................................................................................................47
10.4 Example: Structure block...............................................................................................................49
10.5 B&R extended struct block............................................................................................................49
10.6 Example: Extended Structure block............................................................................................50
11 Automation Studio hot plugging interface..............................................................................................52
12 Example: Automation Studio Hot-Plug Interface block.........................................................................56
13 Model structuring and data management in MATLAB...........................................................................57
13.1 Subsystems........................................................................................................................................57

## Page 4

4 AUTOMATION STUDIO TARGET FOR SIMULINK TM293
13.2 Example: Subsystems.....................................................................................................................58
13.3 Model referencing............................................................................................................................58
13.4 Model referencing example...........................................................................................................60
13.5 Data dictionary.................................................................................................................................60
13.6 Data dictionary example................................................................................................................62
14 Summary..........................................................................................................................................................63
15 Appendix..........................................................................................................................................................64
15.1 Programming interfaces (API).......................................................................................................64
15.2 Data type conversions....................................................................................................................64
15.3 Diagnostics........................................................................................................................................66

## Page 5

INTRODUCTION5

1Introduction

For years, the  software package from  (http://www.mathworks.com) has served as a powerfulMathWorksMATLAB®

tool for solving engineering, mathematical and business problems and is widely used in the industrial world. MATLAB®

is a numerical computing environment and programming language. The greatest strength of the program is its ability

to handle large matrices, as the name rix oratory suggests. MATLAB® can be extended with various add-onMATLAB

packages, such as . This program package enables graphical creation of simulation models that allow com-Simulink®

plex technical processes to be adapted under realistic conditions.

Automatic implementation of Simulink models in C/C++ code, which has been specially optimized for use in B&R target

systems, offers developers new possibilities for designing sophisticated simulation models and control structures

that would otherwise be impossible or very time consuming to implement.

The greatest advantages of  are available to developers who already use MATLAB® andautomatic code generation

Simulink® for simulation and solution design, as well as to developers who, in the past, have worked hard to revise

implemented structures in a language supported by Automation Studio. In the procedures listed below, the tool for

automatic code generation provided by B&R represents an innovation with endless possibilities that contribute to

productive transformation of control system development.

The basic principle is simple: The module created in Simulink® is automatically translated into the optimal language

for the B&R target system using  or  (optional) to ensure maximum performanceSimulink Coder®Embedded Coder®

of the generated source code. Seamless integration into an  helps to achieve the idealAutomation Studio project

development process.

ControllerDevelopment

HMISimulation

Motion controlCommissioning

Safety technologyDiagnostics and

Service

Figure 1:  Automation Studio: One engineering tool for the machine's entire lifecycle

## Page 6

6 AUTOMATION STUDIO TARGET FOR SIMULINK TM293
1.1 Learning objectives
This training module gives participants insight into the basics of simulation and how to use the Automation Studio
Target for Simulink interface.
Participants will learn the following:
How to use Automation Studio Target for Simulink.
•
How to use the user interface for the B&R Simulink blocks.
•
How to create smaller projects in Simulink.
•
How to use automatic code generation.
•
About the relationship between the Simulink model and the program generated in Automation Studio.
•
How to configure communication between a Simulink model and an Automation Studio project.
•
1.2 Symbols and safety notices
Unless otherwise specified, the symbol descriptions and safety notices listed in "TM210 - Working with Automation
Studio" apply.

## Page 7

GENERAL INFORMATION7

2General information

The sharp increase in production requirements, the expanding complexity of systems and the development of greater

numbers of technical products with software all make  an essential component of the development process.simulation

The following image shows the typical B&R , which differ in their level of detail. A distinction is madesimulation levels

between the , the   and the  .automation hardware levelcomponentand machine levelprocessand plant level

Starting at the base, you have simulation of hardware and software components. Automation Studio supports all

possible simulation options.

In the higher level, dynamic processes of machines and systems can be simulated. For this, B&R offers connection to

the most popular simulation tools on the market. The third level enables simulation of complex system processes.

Even for process simulation, B&R offers corresponding interfaces for external software in Automation Studio.

Figure 2: Simulation levels - Plants and processes, machines and components, hardware

2.1Simulation

is an experimental approach to determine static and dynamic properties of a system based on a model. TheSimulation

model makes it possible to gain knowledge about the system and its behavior. This knowledge can then be transferred

to reality. For the digital representation of the original system, it is irrelevant whether the system to be examined

already exists or is still in planning stage. The image below shows a simplified procedure for creating a simulation

model.

## Page 8

8AUTOMATION STUDIO TARGET FOR SIMULINK TM293

Figure 3: Simplified representation of a simulation.

Customized Simulink models are used as the simulation model in the following.

Advantages of simulation with MATLAB/Simulink for software and machine development

Possibility to analyze extremely extensive or complex systems

•

Generation of highly efficient program code

•

More precise adjustment when presetting system parameters

•

Minimization and early detection of errors

•

High efficiency and productivity

•

High product quality

•

Time and place for innovation

•

Rapid prototyping

•

Reusability

•

Accelerated time to market

•

2.2Model-based development

is based the principle of simulation. A complex process is first represented in an abstract,Model-based development

realistic model. It is important that all processes necessary for development are contained in the simulation model

since any further development is based on it. During the test phase, continuous improvements can be made based on

the test results from the model. This process is displayed in the following diagram.

## Page 9

GENERAL INFORMATION 9
Validation
Functional
testing
and
system
testing
Integration
testing
Test
and
verification
Requirements
Design
Functions and properties
System model
Implementation
C, C++
Rapid prototyping
Figure 4: Procedure of model-based development

## Page 10

10 AUTOMATION STUDIO TARGET FOR SIMULINK TM293
2.3 Virtual commissioning
Virtual commissioning refers to the testing of the simulation model in a virtual environment in order to simulate real
commissioning as accurately as possible. In most cases of virtual commissioning, the simulation model is a virtual
image of a real machine. During virtual commissioning, data is imported, tested and changed on the virtual image of
the machine before the software is transferred to the real machine. An executable model enables the software to be
tested on a workstation computer or on a real-time system at an early stage.
An important aspect during virtual commissioning of mechanical processes is the 3D representation that simulates
machine behavior and thus provides visual feedback for the tester.
It should be noted, however, that virtual commissioning does not replace real commissioning completely. In mod-
el-based development, two test procedures have proven to be effective: Software-in-the-loop (SiL) and hardware-in-
the-loop (HiL).
2.3.1 Software-in-the-loop (SiL)
Software-in-the-loop is a test procedure used for models in a simulation environment. During a software-in-the-loop
simulation, the software that has been developed and the simulation environment are executed on the same hardware.
The software communicates with the simulation, which is running on the same processor, via global variables or vari-
able mapping.
2.3.2 Hardware-in-the-loop (HiL)
Hardware-in-the-loop is a test procedure for evaluating a simulation model of a machine on the real hardware used
during commissioning. A real-time simulation is used for the test system in order to represent a controlled system
for a closed-loop controller as accurately as possible. An HiL system provides the controller with all input and output
signals that would exist in the real environment.

## Page 11

MATLAB11

3MATLAB

MATLAB (rix oratory) offers a programming language that is primarily de-MATLAB

signed for numerical calculations based on matrix-based mathematics. A wide

range of applications is provided by the combination of this development process

and a desktop environment for iterative analysis. MATLAB can be extended with a

variety of toolboxes, such as Simulink.

For further information, see MathWorks.

https://mathworks.com

3.1User interface

The MATLAB user interface consists of five windows. At this point, only a rudimentary description will be provided. For

more detailed information, see Matlab Getting started on the MATLAB website:

mathworks.com/help/matlab/getting-started-with-matlab.html

User interface

1): Displays the content of the folder at the current path.Current folder

2): The command window is used to execute functions and statements.Command window

3): The workspace lists the currently used variables of all data types until the next clean up.Workspace

4): Displays the current path and offers recently used paths through the drop-down menu.Current path

5): Quick start for possible functions in MATLABToolbar

Figure 5: MATLAB user interface

3.2Help documentation

The MATLAB help documentation can be opened via the toolbar. The help documentation contains information about

functions, blocks and toolboxes.

## Page 12

12AUTOMATION STUDIO TARGET FOR SIMULINK TM293

Figure 6: MATLAB's help documentation can be opened via the toolbar.

Figure 7: MATLAB help documentation

## Page 13

MATLAB13

3.3Simulink

adds graphical programming to MATLAB, providing the foundation for multi-domain simulation and mod-Simulink

el-based development with MATLAB. This supports system-level design and simulation and enables automatic code

generation and continuous testing and verification of embedded systems. In addition, existing and custom MATLAB

algorithms can be used in Simulink and the simulation results can be analyzed and processed in MATLAB.

Figure 8: Opening Simulink from the toolbar

Figure 9: Simulink start page

For further information, see Simulink - Getting Started.

https://mathworks.com/products/simulink.html

3.3.1Toolbox system

Simulink consists of a  and includes a graphics editor, custom block libraries and solvers for modelingtoolbox system

and simulating dynamic systems. Simulink libraries intended for specific application areas can be added to Simulink

individually. Automation Studio Target for Simulink is another toolbox with blocks that are added to the Simulink

library.

## Page 14

14AUTOMATION STUDIO TARGET FOR SIMULINK TM293

Figure 10: Simulink library

## Page 15

AUTOMATION STUDIO TARGET FOR SIMULINK15

4Automation Studio Target for Simulink

is a toolbox for extending Simulink and serves as an interface between MAT-Automation Studio Target for Simulink

LAB/Simulink and Automation Studio.

Installing Automation Studio Target for Simulink adds a B&R library to the Simulink library. It provides blocks that allow

a connection to Automation Studio and seamless integration of the generated program code. A detailed explanation

of the individual blocks is provided in the following sections. Automation Studio Target for Simulink supports the

creation of variables, constants, one-dimensional and multidimensional fields and structures.

In addition to B&R-specific blocks, Automation Studio Target for Simulink also supports all standard input/output

blocks from Simulink. In addition to the standard MATLAB setting options, however, the B&R blocks also offer settings

adapted to Automation Studio.

Figure 11: Automation Studio Target for Simulink library

## Page 16

16AUTOMATION STUDIO TARGET FOR SIMULINK TM293

B&R Automation Studio Target for Simulink blocks

The B&R CONFIG block is required to configure the Automation Studio connection and to select the coder and

•

programming language (block cannot be generated).

The B&R IN block / B&R OUT block creates a process variable in Automation Studio that can be linked to inputs or

•

outputs.

The B&R PARAMETER block creates a process variable in Automation Studio to make an internal parameter visible

•

externally.

The B&R WORKSPACE_VAR block enables variables created in MATLAB/Simulink to be used in the Simulink model

•

and generated as process variables in the Automation Studio project.

The B&R EXT_IN block / B&R EXT_OUT block creates process variables in Automation Studio to adapt Simulink

•

variables to the hardware inputs and outputs.

The B&R STRUCT_IN block / B&R STRUCT_OUT block allows individual data types of existing Automation Studio

•

structures to be used in the Simulink model. The structures are read from a specified .typ file.

The B&R EXT STRUCT Block makers it possible to use entire existing Automation Studio structures in the

•

Simulink model. The structures are read from a specified .typ file. The output/input of the block represents a

Simulink.Bus. The *.typ file can be used to initialize the bus signals with values.

The B&R BUS IN / B&R BUS OUT block generates a corresponding structure in the Automation Studio .typ file

•

from a Simulink bus variable as well as a corresponding process variable of the structure type.

The Automation Studio Hot-Plug Interface block establishes a live ("Hot") connection, i.e. without code genera-

•

tion, to the controller/ARsim via the PVI protocol. Here, you have direct read/write access to PVI variables.

4.1Installation and registration

Installation of Automation Studio Target for Simulink is started via the installation package (.exe). During installation,

Automation Studio Target for Simulink must be licensed. The product can also be licensed later in MATLAB.

4.1.1Installation

Requirements

IMPORTANT

A C/C++ compiler is required to compile Simulink models. This can be configured in MATLAB

with command line command "mex -setup". For compatible compilers, see de.mathworks.com/sup-

port/requirements/supported-compilers.html

Example with Visual Studio C++ 2017 compiler:

C/C++ compiler (see info box above)

•

MATLAB/Simulink

•

Simulink Coder and/or Embedded Coder Toolbox (usually automatically included with the license when MAT-

•

LAB/Simulink is installed)

For integration in Automation Studio 3.0.90 or later

•

.NET Framework V4.5 or later (latest version automatically installed if not found)

•

Technology Guard software (latest version automatically installed if not found)

•

## Page 17

AUTOMATION STUDIO TARGET FOR SIMULINK17

Installation wizard

Figure 12: Automation Studio Target for Simulink - Welcome screen

Depending on which language is configured on the PC, the wizard is displayed in German or English.

Figure 13: Specification of the MATLAB path where matlab.exe is located

Enter the MATLAB path to matlab.exe. MATLAB must be closed.

## Page 18

18AUTOMATION STUDIO TARGET FOR SIMULINK TM293

Figure 14: Specification of the Automation Studio Target for Simulink installation path

Enter the path for installing the B&R Automation Studio Target for Simulink files.

MATLAB will open in the background. Please wait until the licensing dialog box or a licensing message appears.

4.1.2Uninstall procedure

The file for uninstalling is located in the installation directory of Automation Studio Target for Simulink or can be

launched from .Control Panel / Programs and functions

4.1.3Licensing

Licenses

Automation Studio Target for SimulinkTechnology Guarding / License name

AS Target for Simulink - Single license (hardware dongle)1TGSIMTS.SNG.00-01

AS Target for Simulink - Site license1TGSIMTS.STE.00-01

AS Target for Simulink - Corporate license1TGSIMTS.CRP.00-01

AS Target for Simulink - Education license1TGSIMTS.STE.E0-01

AS Target for Simulink - Evaluation license1TGSIMTS.TRL.00-01

AS Target for Simulink - Student license1TGSIMTS.STE.E1-01

AS Target for Simulink - Single-user software license1TGSIMTS.SNG.00-02

Licensing procedure

Once Automation Studio Target for Simulink has been installed, it must be licensed. If Automation Studio Target for

Simulink has not yet been licensed, the licensing dialog box is automatically displayed to enter a license key. If a Tech-

nology Guard dongle containing a license file exists, the license is activated automatically.

## Page 19

AUTOMATION STUDIO TARGET FOR SIMULINK19

Depending on the selected license type, a Technology Guard for storing the license is needed (Technology Guarding) or

a licensing key must be entered in the licensing dialog box. Evaluation licenses and student licenses can be requested

directly on the B&R website.

There are 2 options for licensing Automation Studio Target for Simulink.

Online activation with a valid license key

•

Hardware dongle

•

Online activation

Enter the supplied B&R license key in the input field. Clicking button Activate will automatically license the B&R Au-

tomation Studio Target for Simulink version online over an existing Internet connection.

A 90-day evaluation license can also be requested. The link in the licensing dialog box directs to the B&R website. When

selecting an evaluation license, an email is sent to the specified address after the form has been filled out. This email

contains a license key that can be activated in the licensing dialog box. The remaining evaluation period is shown each

time Automation Studio Target for Simulink is started. After 90 calendar days, Automation Studio Target for Simulink

must be licensed again.

Hardware dongle

Connect the supplied hardware dongle to an available USB slot and load the license onto the dongle via the Technology

Guarding interface.

4.1.4Upgrades

The installer and latest version can be downloaded from the Downloads section of the B&R website.

Link: B&R Downloads section

4.2Help documentation

During installation of Automation Studio Target for Simulink, multiple help options are also installed. The burhelp

folder, which contains the TM140 user's manual in German and English for Automation Studio Target for Simulink, can

be found in the installation path. The auxiliary functions (commands) described below are also located here.

## Page 20

20AUTOMATION STUDIO TARGET FOR SIMULINK TM293

Figure 15: Automation Studio Target for Simulink burhelp folder

List of help functions

bur_ver

•

Returns the currently installed version of Automation Studio Target for Simulink for the current MATLAB version.

bur_path

•

Specifies the path where Automation Studio Target for Simulink is installed.

bur_setup

•

Allows you to switch between Automation Studio Target for Simulink versions starting with Automation Studio

Target for Simulink version 5.3.0.

bur_relicense

•

Opens a window to relicense Automation Studio Target for Simulink.

bur_getting_started

•

Opens the Automation Studio Target for Simulink help documentation.

bur_demo (example_number)

•

provides a list of all available B&R demo models to the command win-bur_demo

dow.

The number in parentheses opens the respective demo Simulink model in a separate folder, e.g. bur_demo(6)

opens "Example 6 - PID controller with bus objects". (write permissions must be available)

bur_info

•

Opens a window with general information about Automation Studio Target for Simulink.

bur_homepage

•

Opens the B&R website.

## Page 21

AUTOMATION STUDIO TARGET FOR SIMULINK21

In addition to the help options mentioned above, there is also a section in Automation Help called Simulation - Au-

.tomation Studio Target for Simulink

Figure 16: Automation Studio Target for Simulink in Automation Help

This essentially has the same content as the TM140, but appears in updated form with the respective Automation

Studio version (not updated by an installation of Automation Studio Target for Simulink).

4.2.1Demo models

During installation of Automation Studio Target for Simulink, demo models are also installed. These are located in the

installation folder under  and described in the Automation Studio Target for Simulink help documentation.burdemos

Figure 17: Demo models can be found in the installation folder under "burdemos".

## Page 22

22 AUTOMATION STUDIO TARGET FOR SIMULINK TM293
The demo models can be opened by entering the demo model number in the command window.

## Page 23

MY FIRST PROJECT23

5My first project

This section covers creating a new model in Simulink, generating the code, importing it into an Automation Studio

project, transferring it to Automation Runtime Simulation and testing it in Automation Studio.

Exercise 1: My first project

The room temperature of a house is measured at two different

points. Create a Simulink model called ,WeatherStationSimple

which calculates the average temperature and outputs it to an

analog output. Only use the standard I/O blocks from Simulink

and test the result using Simulink Simulation.

## Page 24

24AUTOMATION STUDIO TARGET FOR SIMULINK TM293

6Principle of automatic code genera-

tion

The basic principle of  from MATLAB/Simulink into Automation Studio consists of threeautomatic code generation

elements. Creating a Simulink model in MATLAB/SIMULINK, integrating the generated program code in Automation

Studio and running the project on the target system.

The model created in Simulink is automatically converted to the C/C++ programming language by Simulink Coder

or Embedded Coder (optional). The Automation Studio Target for Simulink interface is automatically adapted and

integrated with Automation Studio.

The project can then be transferred to the target system. The target system can now communicate not only with

Automation Studio but also with the model in Simulink.

This allows simple transfer of complex and sophisticated Simulink models to the target system.

Five steps to executable program code

Create a Simulink model

•

Add a B&R config block to the Simulink model.

•

Create an Automation Studio project

•

Configure the Automation Studio Target for Simulink parameters

•

Start automatic code generation

•

Figure 18: Workflow for Automation Studio Target for Simulink

Exercise 2: Code generation

After successful simulation, the code for the preceding exam-

ple in exercise 1 should be generated in an Automation Studio

project.

1)Create a new Automation Studio project. Hardware: e.g. PC - Automation Runtime Simulation

2)Add a B&R config block to the Simulink model from the  exercise.My first project

## Page 25

PRINCIPLE OF AUTOMATIC CODE GENERATION 25
3) Apart from that, only use standard Simulink I/O blocks.
4) Configure the B&R Config block to generate standard Simulink I/O blocks.
5) Configure the B&R config block so a task is created in Automation Studio.
6) Start code generation. What information is output during code generation? What files are created in MATLAB in
the background?
7) Transfer the Automation Studio project to the Automation Runtime Simulation and check the functionality of the
generated program code in monitor mode.

## Page 26

26AUTOMATION STUDIO TARGET FOR SIMULINK TM293

7Interface to Automation Studio - B&R

config block

The  is the interface between the Simulink model and Automation Studio. Each Simulink model thatB&R config block

should be integrated into an Automation Studio project via automatic code generation must include an instance of

the B&R config block. The block is used to set B&R-specific parameters as well as important Simulink-specific model

parameters and to select the coder and language to be generated.

The B&R config block user interface consists of the five tabs , , Model configurationAutomation Studio settingsAd-

,  and . The primary settings are explained in the following chapters. Forvanced settingsAdditional filesInformation

additional information, see the Automation Studio Target for Simulink help documentation.

Only one instance of the B&R config block is allowed and necessary per Simulink model. The B&R config

block must always be created in the highest hierarchy level of the Simulink model.

Automation Studio Target for Simulink help documentation

Chapter 2.1 B&R config block

•

Chapter 3 Configuration settings

•

7.1Basic settings

The information and parameters necessary for automatic code generation are set in the basic settings for the B&R

config block. The basic settings include tabs  and .Model configurationAutomation Studio settings

Model configuration

Tab  can be used to set important Simulink-specific model parameters and the code generationModel configuration

mode.

Four code generation modes are available for a Simulink model.

Simulink Coder C

•

Simulink Coder C++

•

Embedded Coder C

•

Embedded Coder C++

•

Figure 19: B&R config block user interface - Model configuration

## Page 27

INTERFACE TO AUTOMATION STUDIO - B&R CONFIG BLOCK27

In the model workspace, a separate configuration is created in the background for each code generation mode. It is

possible to switch between modes by first using the radio buttons for selection and then clicking on button "OK" or

"Apply".

Selection of the Embedded Coder as a configuration option only appears if it is available on the system.

Automation Studio settings

Important information for automatic integration of the generated code into an Automation Studio project can be set

in the . If the generated program code should be Automation Studio compatible but notAutomation Studio settings

directly integrated, it can be saved as a  . This can be enabled with the  option.ZIP packageCreate ZIP package

In the further course of action, a decision must be made between  and  generation. When generatingtaskfunction block

a function block, the generated program code is encapsulated in a function block and a library is created that includes

this function block. Task generation, however, uses the generated program code to create a stand-alone program

unit in Automation Studio. In both cases, the program code is automatically added to the Logical View in Automation

Studio.

The generated code requires B&R standard libraries  and . These are automatically addedbrsystemsys_lib

to the Automation Studio project during task or function block generation. When creating a ZIP package,

these libraries must be manually added to the Automation Studio project. Additionally, it is necessary to

make the settings defined in task_properties.txt / lib_properties.txt (a file that is also generated).

Figure 20: User interface B&R config block - Automation Studio settings

You must also specify the path where the Automation Studio project is located or the ZIP package should be created.

The path can be entered using the  field. The name of the task to be created,Automation Studio project path / ZIP path

the library and the function block must also be specified ( /  / ).Task nameLibrary nameFunction block name

The name of the model and the task or library and function block are not permitted to be identical.

7.2Automatic transfer

Setting  is located in tab . If enabled, the generated program is com-Automatic transferAutomation Studio settings

piled and transferred to the target system after automatic code generation.

## Page 28

28AUTOMATION STUDIO TARGET FOR SIMULINK TM293

Figure 21: Enabling automatic transfer in the B&R config block

Ethernet connection

Automation Help: Programming \ Build & Transfer \ Establishing a connection to the target system \

Ethernet connection

7.3External mode

The Simulink  configuration setting enables communication at runtime between the Simulink modelexternal mode

and the code-generated model running on the target system.

External mode establishes one communication service each on the system on which MATLAB is installed (PC) and the

target system (controller or PC). These two services on the respective systems establish a communication channel

between the Simulink engine and the generated code provided on the target system. The communication service iso-

lates the model process on the target system from the code and transport layer. The tasks of the transport layer are to

format, transmit and receive data packets. The communication service on the host computer receives the data pack-

ets through the transport layer and updates the Simulink model display. The diagram shows the connection that the

communication service establishes in external mode between Simulink on the host computer and the provided code

on the target system.

Figure 22: External mode communication scheme MATLAB/Simulink

Configuring external mode

To use external mode, the feature must be enabled in the B&R config block in tab . In addition, threeAdvanced settings

settings must be made:

IP address of the target system. The "localhost" IP address 127.0.0.1 is entered as the default valueIP address:

•

(e.g. when using ARsim).

External mode uses the port for communication with Automation Studio. The port number is an integer val-Port:

•

ue between 1024 and 65535. The MATLAB default value is port number 17725.

Buffer size that is preallocated on the target system. Default value: 1000000 bytes (1 MB)Buffer size:

•

## Page 29

INTERFACE TO AUTOMATION STUDIO - B&R CONFIG BLOCK29

B&R libraries  and  are necessary for external mode in Automation Studio. They are automatically addedAsArLogAsTCP

to the Automation Studio project when a task or function block is created. If a ZIP package is created, these libraries

must be added manually in the Automation Studio project.

Figure 23: External mode settings in the B&R config block

Enabling external mode generates additional code during model code generation that affects perfor-

mance. For additional information, see the documentation for External mode from MATLAB.

https://de.mathworks.com/help/rtw/ug/set-up-and-use-hosttarget-communication-chan-Link:

nel.html

Starting external mode

1)Enable and specify external mode features in the B&R Config block.

2)Generate code and start the target system.

3)Configure the Simulink model for external mode and start the external mode connection.

External mode can also be started and stopped via the  with the con-External Mode Control Panel

nect/disconnect button.

## Page 30

30AUTOMATION STUDIO TARGET FOR SIMULINK TM293

Manipulating variables on the target system from Simulink

While communication between the Simulink model and target system is taking place, process variables can be ob-

served and manipulated in the Simulink model.

Displaying process variables in the Simulink model

To check current values in the Simulink model at runtime, blocks from Simulink library "Sinks" can be used (e.g. "Scope"

or "Display").

Manipulating process variables in the Simulink model

There are two ways to manipulate parameters from Simulink. The first is modifying the values of Simulink blocks like

or  directly during the external mode connection. The other is to use the .GainConstantExternal Mode Control Panel

The External Mode Control Panel allows values of workspace variables to be changed locally in Simulink and then down-

loaded to the target system using command . This can be used with the B&R Parameter block or B&RBatch download

Workspace Variable block, for example. Only parameters that are located in the MATLAB workspace require the batch

download. (B&R input/output parameters cannot be changed because they are not in the workspace, for example)

The External Mode Control Panel is located under Code > External Mode Control Panel > Batch download > Download.

7.4Cycle time and task class

If a program in Automation Studio is added to the Logical View via the toolbox, then it is automatically included in the

software configuration for the active configuration.

In the  tab, the B&R config block provides the possibility to automatically assign the taskAutomation Studio settings

to be generated to a configuration using the  option in the Automation Studio project. SelectingAdd task to hardware

this option enables options  and . The "Target task class" option makes itTarget task classChange cyclic task class

possible to assign the task to a certain task class. Task class #1 is defined as the standard task class.

Figure 24: Enabling options "Add task to hardware", "Target task class" and "Change cyclic task class"

## Page 31

INTERFACE TO AUTOMATION STUDIO - B&R CONFIG BLOCK31

If the generated task is automatically assigned to the correct configuration and task class, the additional

compiler switches and additional include directories are also specified. These entries are always stored

again in the generated code for manual integration in an additional text file.

In addition to the task class assignment, changes can also be made to the selected task class by activating the Change

option.cyclic task class

Duration [s]:

Duration of the . Configurable range: 400 µs to 60 s. The value must be a multiple of the system tick. Allcycle time

software objects for this resource are processed exactly once within this cycle time.

Tolerance [s]:

Maximum allowed cycle time violation. This value must be a multiple of the cycle time. This allows the cycle time to

be exceeded by a predefined amount.

System tick [s]:

Influences the timing characteristics of the system. A system tick is the time base for the clock generator used to

derive the cycle time (also for the task classes). By default, the system tick is set to 0.001 seconds. The system tick is

adjusted in the CPU configuration in the "Timing" subsection.

For more information, see Automation Help:

Real-time operating system \ Method of operation \ Runtime performance \ Task classes

Project management \ Configuration View \ Properties of the objects in the Configuration View

Programming \ Editors \ Configuration editors \ Hardware configuration \ CPU configuration \ SG4 \

CPU properties - Timing

Programming \ Editors \ Configuration editors \ Hardware configuration \ CPU configuration \ SG4 \

CPU properties - Resources

7.5Dependence on additional files

The B&R config block offers two possibilities in the  tab.Additional files

Adding additional source files or other files to the generated task or function block.

•

Specification of the type of files whose structures are read and then available in the Simulink model. For more in-

•

formation, see chapters 10.3 "B&R struct block" and B&R extended struct block.

7.6Additional functionality

In addition to the options described above, the B&R config block also offers other options in the "Advanced settings"

tab. Some of the important ones are described below.

## Page 32

32 AUTOMATION STUDIO TARGET FOR SIMULINK TM293
Create Simulink I/Os/ Simulink.Parameter / Simulink.Signal
•
Creates all I/O variables defined by standard Simulink blocks, Simulink.Parameter variables or Simulink.Signal
variables that are defined in the base workspace or data dictionary as local/global variables in the generated
code and creates them in the VAR file.
Enable pre/post processing
•
Before/After the code generation has been completed, an M-file can be called up for a pre/post processing rou-
tine. The file must be located in a MATLAB path.
Create global var files
•
Creates a global_modelName.var and global_modelName.typ file with all IO variables defined globally in the
Simulink model.
Simulink web interface
•
Creates a web service on the target system for accessing the Simulink model via a web browser. Intended for di-
agnostic purposes.
For additional information, see the Automation Studio Target for Simulink help documentation.
Chapter 2.1 B&R config block
Chapter 3 Configuration settings

## Page 33

VARIABLES, CONSTANTS AND ARRAYS 33
8 Variables, constants and arrays
Programming in Automation Studio is not done by accessing fixed memory addresses, but instead via symbolic ele-
ments that have names. These elements are called process variables. Process variables compatible with Automation
Studio are created with B&R Simulink blocks that are added to the Simulink library when Automation Studio Target
for Simulink is installed.
The following sections explain how to create variables, constants and arrays.
Declaration
In Automation Studio, variables, constants and arrays are only created in files with the *.var file extension. Accordingly,
when a program with newly generated code is added, a variable declaration with the name of the program is created
so it is available locally.
Initialization
In Automation Studio, variables, constants and arrays can be initialized directly in the declaration editors. As a result,
an initialization value can be assigned in Simulink, which is then automatically applied and displayed in the declaration
editor.
Scope
The scope of declared process variables depends on the position of the declaration file in the Logical View. For each
variable, constant or array, you can define whether it is available globally or locally. When available globally, a global
variable declaration is created.
Basic data types
Basic data types form the basis for all other derived data types. Basic data types determine the value range of a variable
in addition to how much space it needs in memory. Data types also establish whether values are signed or unsigned,
whether they include decimal places, text or even dates or times.
The following list shows all basic data types standardized according to IEC 61131-3 that are supported by Automation
Studio Target for Simulink and their counterparts in Simulink.
Automation Studio Simulink Range of values
Binary / Bit string BOOL Boolean FALSE, TRUE
SINT int8 -128 … 127
INT Int16 -32768 … 32767
Signed integers
DINT int32 -2,147,483,648 …
2,147,483,647
USINT uint8 0 … 255
Unsigned integers UINT uint16 0 … 65535
UDINT uint32 0 … 4,294,967,295
REAL Single -3.4E+38 … 3.4E+38
Floating point
LREAL double -1.7E+308 … 1.7E+308
Table 1: Overview of supported IEC data types
8.1 Code generation
After successful simulation of the Simulink model, the model should be generated in an Automation Studio project
using the B&R Config block. To do this, the B&R Config block is added to the model, and the project path and task
name are set in the block.
Afterwards, the variables for the inputs and output should be created manually in the project so that the generated
code can be tested on ARsim.

## Page 34

34AUTOMATION STUDIO TARGET FOR SIMULINK TM293

Exercise 3: Code generation with B&R I/Os

The standard Simulink I/Os from the previous example in exer-

cise 2 should now be replaced by B&R blocks and code should be

generated again.

Declaration

VAR

aiSummand1: REAL;(*erster Temperatursensor*)

aiSummand2: REAL;(*zweiter Temperatursensor*)

aoAvgTemp: REAL;(*Durchschnittstemperatur*)

END_VAR

Table 2: Variable declaration of process variables created with B&R blocks

1)Use the model from the previous exercise (exercise 2) and add the appropriate B&R Input blocks and a B&R Out-

put block. Check the model with the simulation.

2)Start the code generation.

3)Check the variables in the VAR file and check the functionality of the generated program code in the Watch win-

dow.

## Page 35

VARIABLES, CONSTANTS AND ARRAYS35

Exercise 4: Change the task class and cycle time

Use the example from exercise 3  to generateCode generation

the task in a specific task class. The cycle time for this task class

should also be adapted.

1)Change the task class.

2)Modify the selected task class. Change the cycle time to 0.01 s.

3)Generate the model in an Automation Studio project and

check the modifications.

4)Determine what happens if the cycle time of the task class

does not match the step size of the Simulink model.

The cycle time of the task class must match the step size of the Simulink model.

8.2B&R input block

The B&R input block creates a process variable per IEC in Automation Studio that can be linked to inputs.

The interface allows you to specify all the information that is also possible in the .var file in Automation Studio.

Figure 25: B&R input block - User interface

Name of the process variable in Automation Studio.Variable name:

Comment entry for the description of process variables.Variable description:

Availability and visibility of the process variable within the entire project. Values: "Global" or "Local".Variable scope:

Data type of the process variable per IEC 61131-3.Variable data type:

Decides whether the process variable is battery-backed (value retained after the controller is switched off).Memory:

Values: "Retain" for battery-backed or "Standard" for not battery-backed.

Initial value of the process variable. This value is used for all fields of an array. To initialize an n-dimen-Initial value:

sional array with different initial values, square brackets must be written in the field. (For example: [1,2,3;4,5,6] for a 2-

dimensional array with the arrangement [2,3], i.e. 2 rows and 3 columns)

If the value is 1, a process variable is created as a variable. If the value is greater than 1, a process variable isArray size:

created as a one-dimensional array. To create an n-dimensional array, square brackets must be written in the field (e.g.

[3,5] for a 2-dimensional array with the arrangement 3 rows and 5 columns). It is also possible to write variables instead

of numeric values into the field, e.g. [rowSize,colSize] if rowSize and colSize are declared accordingly in the workspace.

## Page 36

36AUTOMATION STUDIO TARGET FOR SIMULINK TM293

IMPORTANT: If multidimensional arrays are used, array layout  must be selected in the config-Row-major

uration panel and "External functions compatibility" must be set to .none

Figure 26: Displaying a declaration as a table

Figure 27: Displaying a declaration as text

## Page 37

VARIABLES, CONSTANTS AND ARRAYS37

8.3B&R output block

The B&R output block creates a process variable per IEC in Automation Studio that can be linked to outputs.

Figure 28: B&R output block - User interface

The same setting options are available as for the B&R input block.

8.4Creating internal variables and constants

Variables that are used internally in the model can be made visible in Automation Studio. These variables can also be

defined as constants using the . The value cannot change at runtime. Constants are used in theB&R parameter block

program code, for example, as limit values.

There is also the option " in the , which generates code for vari-"Create Simulink.Parameter/SignalB&R Config block

ables that have already been created in MATLAB. It is important to ensure that "" is set to "Storage classImportedEx-

".ternal

Figure 29: Setting the storage class for the code generation on ImportedExtern

Declaration

When a program with newly generated code is added, a variable declaration with the name of the variable is created

so it is available locally.

## Page 38

38AUTOMATION STUDIO TARGET FOR SIMULINK TM293

For each constant, it is also possible to define individually that it is available globally. In this case, a global variable

declaration is created.

The B&R parameter block can be used to create constants in Automation Studio.

Exercise 5: Make internal variables visible

The value of the gain block should remain variable. Accordingly,

the value of the gain block should be visible in Automation Studio

as a process variable.

1)Add a B&R parameter block from the Create process variables

exercise to the model.

2)Start the code generation.

3)Try to change the value of the variable using the External

Mode Control Panel

4)In the second step, it was decided that the variable for the

gain block would be created as a constant. Change the set-

tings in the Simulink model so the variable is created as a con-

stant in Automation Studio.

5)Now, remove the B&R parameter block and create a Simulink

parameter variable. Repeat step 2.

8.4.1B&R parameter block

The B&R parameter block offers the option of making internal variables of the Simulink model that are not inputs or

outputs visible in Automation Studio as IEC process variables in the .var file.

A special feature of the B&R input/output block is that the process variable can be defined as a constant.

Figure 30: B&R parameter block - User interface

Name of the process variable in Automation Studio.Variable name:

Comment entry for the description of process variables.Variable description:

Availability and visibility of the process variable within the entire project. Values: "Global" or "Local".Variable scope:

Data type of the process variable per IEC 61131-3.Variable data type:

Decides whether the process variable is battery-backed (value retained after the controller is switched off).Memory:

Values: "Retain" for battery-backed, "Standard" for not battery-backed, "Constant" for defining a constant.

Initial value of the process variable. This value is used for all fields of an array. To initialize an n-dimen-Initial value:

sional array with different initial values, square brackets must be written in the field. (For example: [1,2,3;4,5,6] for a 2-

dimensional array with the arrangement [2,3], i.e. 2 rows and 3 columns)

If the value is 1, a process variable is created as a variable. If the value is greater than 1, a process variableArray size:

is created as a one-dimensional array. To create an n-dimensional array, square brackets must be written in the field

(e.g. [3,5] for a 2-dimensional array with 3 rows and 5 columns). It is also possible to write variables instead of numeric

values into the field, e.g. [rowSize,colSize] if rowSize and colSize are declared accordingly in the workspace.

## Page 39

VARIABLES, CONSTANTS AND ARRAYS 39
8.5 Creating arrays
In contrast to basic data type variables, values with the same data type are combined in arrays. The individual elements
can be addressed with the array name and an index.
The value of the array index is not allowed to exceed the array size. The size of an array is defined by the variable
declaration. In the program, the index can be a fixed value, a variable, a constant or an enumerated element.
Declaring and using arrays
When an array is declared, it must be given a data type and a dimension. In most programming languages, the smallest
index of an array is set to 0 (MATLAB is an exception and starts with index 1). For this reason, take note that an array
with 10 elements has a maximum index of 9 (apart from MATLAB).
Declaration VAR
aPressure : ARRAY[0..9] OF INT := [10(0)];
END_VAR
Program code (*Assigning value 123 to index 0*)
aPressure[0] := 123;
Table 3: Declaring an array of 10 elements in Automation Studio, starting index = 0
Declaring an array using constants
Since using fixed numeric values in declarations and the program code itself (so-called magic numbers) usually leads
to programming that is unmanageable and difficult to maintain, it is a much better idea to use numeric constants. The
upper and lower indexes of an array can be defined using these constants. These constants can then be used in the
program code to limit the changing array index.
Declaration VAR CONSTANT
MAX_INDEX : USINT := 9;
END_VAR
VAR
aPressure : ARRAY[0..MAX_INDEX] OF INT ;
index : USINT := 0;
END_VAR
Program code IF index > MAX_INDEX THEN
index := MAX_INDEX;
END_IF
aPressure[index] := 75;
Table 4: Declaring an array in Automation Studio using a constant
In C/C++, the first element of an array is addressed with 0; in MATLAB, arrays always start with 1. Con-
sequently, an array with 5 elements in C/C++ has an index from 0 to 4. In MATLAB, however, the array
has an index from 1 to 5.
8.5.1 Creating one-dimensional arrays
The B&R input/output block and the B&R parameter block allow process variables to be created as one-dimensional
arrays in Automation Studio. The value in option Array size is all that needs to be adjusted.
If the array size is set to 5, a process variable is created in Automation Studio as a one-dimensional array with 5 ele-
ments that can be addressed using the index from 0 to 4.
There are various ways to initialize the elements of the one-dimensional field. In "Initial value", it is pos-
sible to enter either a single number (all fields are then initialized with this value) or square brackets and
a comma (the individual field values can then be determined, e.g. [1,2,3,4] for a field with 4 elements).

## Page 40

40AUTOMATION STUDIO TARGET FOR SIMULINK TM293

Exercise 6: Create one-dimensional arrays

House temperature control gets an update. The temperatures of

the last three days should be stored in order to calculate the av-

erage temperature of these days.

Modify the example from exercise 5 Make internal variables vis-

to meet the new requirements.ible

Change the array size of all B&R blocks to the corresponding

•

days.

Start the code generation.

•

Try to change the values of the B&R parameter variable again

•

using the External Mode Control Panel.

8.5.2Creating multidimensional arrays

Arrays can also be composed of several dimensions. The declaration and usage in this case can look something like this:

Declaration

VAR

Array2Dim : ARRAY [0..6,0..6]  OF INT;

END_VAR

Program code

(*Zählweise mit 0 beginnend*)

Array2Dim[2,2] := 11;

Figure 31: Accessing the value in

Column 3, Row 3

Table 5: Declaring and accessing a 7x7 two-dimensional array

The  and the  allow process variables to be created as multidimensionalB&R input/output blockB&R parameter block

arrays in Automation Studio. The value in option  is all that needs to be adjusted.Array size

If the array size is set to [2,3], a process variable is created in Automation Studio as a 2-dimensional field with 6 ele-

ments.

There are various ways to initialize the elements of the multidimensional field. In the item "Initial value", it

is possible to enter either a single number (all fields are then initialized with this value) or square brackets,

a comma and a semicolon (the individual field values can then be determined, e.g. [1,2,3,4,5,6] for a [2,3]

field with 6 elements).

By default, MATLAB generates one-dimensional arrays in C from multidimensional arrays in Simulink

(the Embedded Coder provides advanced settings that preserve multidimensionalities in the generated

code). The  and the  take this conversion into account, butB&R input/output blockB&R parameter block

still create the arrays as multidimensional in the VAR file in order to retain the desired structure. This

conversion was not implemented for the .B&R workspace block

8.5.2.1B&R workspace variable block

The B&R workspace block makes it possible to create existing variables created on a workspace as process variables

in Automation Studio. A common use case is to create fields with different initial values as process variables.

The block supports the following:

## Page 41

VARIABLES, CONSTANTS AND ARRAYS41

Variables of data type "Double"

•

One-dimensional and multidimensional arrays of data type "Double"

•

Structures

•

Simulink.Parameter

•

Mpt.Parameter

•

Figure 32: B&R workspace variable block - User interface

8.5.2.2Creating 2-dimensional fields

Creating multidimensional fields can be done in several ways. In this example, the input and output will be created as

2-dimensional fields.

Exercise 7: Create 2-dimensional fields

Using the model from the previous exercise, create a 2-dimen-

sional array using the B&R Input and Output blocks.

The problem now is that one input should be used to specify the

setpoint for the indoor and outdoor temperature for the last 3

days, and the second input supplies noise values for the respec-

tive temperatures. The average temperature should therefore be

calculated again for the output for the respective days, taking in-

to account the noise values.

The initial values for setpoint temperature or noise values are

specified in the tables below.

Day 1Day 2Day 3

Indoor temperature202225

Outdoor temperature10149

Table 6: Initial values for setpoint temperatures

Day 1Day 2Day 3

Noise values for the indoor1.42.11.8

temperature

Noise values for the out-3.74.33.9

door temperature

Table 7: Initial values for noise values

## Page 42

42AUTOMATION STUDIO TARGET FOR SIMULINK TM293

1)Use the model from the previous exercise and create the 2-dimensional fields. Enter the initial values from the ta-

bles above into the two B&R Input blocks (see B&R input block). Check the model with the simulation.

2)Start the code generation.

3)Check the functionality with the external mode.

Exercise 8: Use the B&R Workspace block

Use the model from the previous exercise, "Exercise 7: Create 2-

dimensional fields" and then use the B&R Workspace block. This

was implemented for variables that are already in the MATLAB

workspace. The existing variables can easily be selected and used

with the help of this block.

For this, you should recreate the temperature input matrix from

the previous example in the MATLAB workspace and then select it

using the Workspace block. It is important to note the data type

for this.

Note:

>> myMatrix = [20,22,25;10,14,9]

myMatrix =

20 22 25

10 14 9

>> myMatrix = single(myMatrix)

myMatrix =

2×3 single matrix

20 22 25

10 14 9

1)Use the model from the previous exercise and use the B&R Workspace block for the temperature input. Check the

model with the simulation.

2)Start the code generation.

3)Determine the difference in the variable declaration in the VAR file of the Automation Studio project.

4)Check the functionality with the external mode.

## Page 43

CREATING USER LIBRARIES43

9Creating user libraries

Libraries are used to package software into reusable and compact units. A

library is a collection of functions and function blocks, constants and data

types. The following section briefly explains the definitions and shows how

the function blocks are used. Customized user libraries can then also be cre-

ated.

Figure 33: Working with libraries

Library

A library is a collection of functions, function blocks, constants and data types. Any existing library can be inserted

into the Logical View at any time. Either B&R standard libraries or libraries created by the user can be selected. In order

to be able to reuse program code, it must first be split up into self-contained modules that can be maintained. User

libraries are suitable for this purpose. Before the design phase begins, it is important to give some thought to the

scope of the individual functional units, i.e. the functions and function blocks. Once this has been done, the interfaces

can be declared. Finally, the range of functions can be implemented.

The following questions must be taken into consideration when creating a library:

What is the function of this library?

•

Which functions and function blocks are necessary?

•

How should the interfaces for the functional units look?

•

Will constants and structures be used?

•

Are certain things necessary from other libraries to handle certain tasks?

•

How will the library be passed on or stored?

•

Components of a library

A library consists of several components and properties. A distinction is made between the following components:

- Contains the interface or the structure of the instance of a function or function block.*.fun file

•

- Contains numeric constants, including the modes that a function block can report, but also numeric*.var file

•

parameters that are expected from a function or function block.

- Structures that the function block needs internally or structures that must be transferred to the func-*.type file

•

tion block in the application.

Function block

A function block can have multiple return values. The declaration of an instance is necessary. In addition, it is possible

for a function block to perform a task while being called multiple times.

Figure 34: (1) Instance variable, (2) Inputs and (3) Outputs on a function block

## Page 44

44AUTOMATION STUDIO TARGET FOR SIMULINK TM293

1)Instance structure, must be declared in the variable declaration editor with a unique name.

2)Input parameters are passed directly before or during the function block call.

3)Output parameters are written while the function block is being called and can then be used in the program

code.

Different instances make it possible to make calculations in tasks using different parameters. An instance can actually

be thought of as a structure. The function block takes in the input parameters at the moment it is called and then

passes the output parameters on to the instance after the program routine is processed.

Code generation for a library with a function block

To generate a library with a function block from a Simulink model, select  as target in the B&R configFunction block

block in the  tab. Then it is necessary to specify the name of the library ()Automation Studio settingsLibrary name

and of the function block ().Function block name

Figure 35: User interface B&R config block - Automation Studio settings

Figure 36: Creating a user library with a function block

The library name is not permitted to be the same as the function block name or the model name.

Likewise, the function block name is not permitted to be the same as the library name or model name.

By default, an  integer variable is created for the inputs and outputs of each function block. This variablessMethodType

determines the status of the function block. The following values can be assigned to the variable:

## Page 45

CREATING USER LIBRARIES45

This means that each standard function block must be initialized with ssMethodType = SS_INITIALIZE and started with

ssMethodType = SS_OUTPUT. In addition, the block can be paused/terminated with ssMethodType = SS_TERMINATE,

i.e. if the block is previously in the state SS_OUTPUT and is then changed to SS_TERMINATE, the block can be resumed

again with SS_OUTPUT. The block is reset with SS_INITIALIZE.

The  option causes the function block to be instantiated only once in an Automation Studio project.Legacy mode

The option  prevents any  variables from beingCreate B&R library guidelines compliant function blockssMethodType

created; instead the block runs immediately after starting the program. A  boolean variable is created insteadbur_init

of ssMethodType. The block can be reset by this variable; this variable is edge controlled.

Exercise 9: Create a user library

Open the demo example 1 from the bur_demo examples. Note:

Enter  in the command line and change the gen-

bur_demo(1)

eration from "Task" to "Function block" in the B&R Config block.

In addition, replace a B&R Input block with a Simulink Counter

block:

1)Open the demo example 1 from the bur_demo examples.

2)Change the B&R Config block so that a function block is created.

3)Replace a B&R Input block with a Simulink Counter block (e.g. Counter Free-Running).

4)Start the code generation.

5)Verify the functionality of your function block in Automation Studio by calling the function block in a program.

6)Become familiar with the states of the function block ).(ss_Methodtype

## Page 46

46AUTOMATION STUDIO TARGET FOR SIMULINK TM293

10Structures

A structure is composed of individual elements including basic data types, arrays and other structures. The entire

structure is addressed via a common name. Each of the individual elements also has its own name. Structures are also

known as user data types. Structures are primarily used to group together data and values that have a relationship

to each other.

10.1B&R bus block

The  makes it possible to create a process variable whose data type is a structure type.B&R bus input/output blocks

These structure types are already created in Simulink as bus elements via the . This allows structure typesbus editor

to be defined in Simulink, created and transferred to the Automation Studio project.

Figure 37: B&R bus input blockFigure 38: B&R bus output block

Name of the process variable in Automation Studio.Variable name:

Comment entry for the description of process variables.Variable description:

Availability and visibility of the process variable within the entire project. Values: "Global" or "Local".Variable scope:

Structure type of the process variable. All available bus elements from Simulink are automaticallyVariable data type:

offered in a drop-down box.

Decides whether the process variable is battery-backed. Values: "Retain" for battery-backed or "Standard"Memory:

for not battery-backed.

If the value is 1, a process variable is created as a variable. If the value is greater than 1, a process variableArray size:

is created as a one-dimensional array.

## Page 47

STRUCTURES47

Bus editor

The bus editor in Simulink can be opened via the command window with command . Variousbuseditor

bus elements can then be defined and created here.

Starting with V6.2.1, bus elements can also be defined as a multidimensional array.

Figure 39: Bus editor in MATLAB

10.2Example: Bus block

Exercise 10: Create structure types

Use the example pid_controller_with_bus_object. You can open the example by executing bur_demo in the command

window and specifying the appropriate number as a parameter. You may want to save the newly opened example

with a shorter filename to avoid path length problems.

1)Which bus elements are used in the model?

2)Adjust the B&R Config block.

3)Generate the Simulink model in Automation Studio.

4)Check the functionality by including a trace of the output variable.

5)Which variables are responsible for the setpoint specification, return values and reset?

10.3B&R struct block

The  allows structure types that have already been created in Automation Studio inB&R struct input/output block

a .typ file to be used in the Simulink model. The .typ file is read in via block "B&R CONFIG" and made available to the

B&R struct block.

## Page 48

48AUTOMATION STUDIO TARGET FOR SIMULINK TM293

Figure 40: B&R struct input blockFigure 41: B&R struct output block

Name of the process variable in Automation Studio.Structure name:

Comment entry for the description of process variables.Variable description:

Availability and visibility of the process variable within the entire project. Values: "Global" or "Local".Variable scope:

Decides whether the process variable is battery-backed. Values: "Retain" for battery-backed or "Standard"Memory:

for not battery-backed.

## Page 49

STRUCTURES 49
10.4 Example: Structure block
Exercise 11: Import the structures
Create a model named structure_sample.
Declaration TYPE
InputType:STRUCT
in_Real: REAL;
in_Int:INT;
in_Bool:BOOL;
END_STRUCT;
OutputType:STRUCT
out_Real: REAL;
out_Int:INT;
out_Bool:BOOL;
END_STRUCT;
END_TYPE
Table 8: Structure declaration in the TYP file of the Automation Studio project
1) Create two structures named "InputType" and "OutputType" in an Automation Studio project in the file "Glob-
al.typ".
2) Link the "Global.typ" file with the Simulink model using the B&R Config block.
3) In the model, the respective data types of identical structure elements should be written to each other.
4) Start the code generation.
5) Check the functionality.
10.5 B&R extended struct block
The B&R extended struct input/output block allows entire structures that have already been created in Automation
Studio in a .typ file to be used in the Simulink model. The .typ file is read in via block "B&R CONFIG" and made available
to the B&R extended struct block. The block generates a Simulink.Bus from an Automation Studio structure and a
corresponding MATLAB struct that can contain initial values (the initial values come from the *.typ file). These variables
are created in the base workspace or in a data dictionary. During code generation, a process variable of the type of the
selected structure is created in Automation Studio. The corresponding *.typ file must exist in the Automation Studio
project.

## Page 50

50AUTOMATION STUDIO TARGET FOR SIMULINK TM293

Figure 42: B&R extended struct in blockFigure 43: B&R extended struct out block

Table 9: Extended struct block

Name of the process variable in Automation Studio.Variable name:

Name of the structure/type of the process variable.Structure:

Comment entry for the description of process variables.Description:

Availability and visibility of the process variable within the entire project. Values: 'GLOBAL' or 'LOCAL'.Scope:

Decides whether the process variable is battery-backed. Values: "Retain" for battery-backed or "Standard"Memory:

for not battery-backed.

Limitations

This block is only supported by MATLAB R2018a and later

•

Only supports basic data types SINT, USINT, INT, UINT, DINT, UDINT, REAL, LREAL, BOOL and one-di-

•

mensional arrays

Initialization from data dictionary not directly supported

•

In order to initialize from the data dictionary, a MATLAB function block can be connected

downstream to the B&R extended struct in block. For more information, please contact

B&R Support.

Currently not possible to mark (memory) the structure as a reference (pointer)

•

Initialization currently only possible via .typ file (applies to B&R extended struct in block)

•

10.6Example: Extended Structure block

Exercise 12: Read structures with the Extended Structure block

Use the model from the previous exercise (exercise 11): .Read structures

1)Replace the B&R Structure blocks with B&R Extended Structure blocks.

2)What happens in the MATLAB workspace after applying the settings?

## Page 51

STRUCTURES 51
3) A Bus Selector block or Bus Creator block from the Simulink library is required for a direct connection between a
B&R Extended Struct In block and a B&R Extended Struct Out block. (analog to B&R Bus blocks)
4) Start the code generation.
5) Check the functionality in Automation Studio again.

## Page 52

52AUTOMATION STUDIO TARGET FOR SIMULINK TM293

11Automation Studio hot plugging inter-

face

The  block has been available since V6.1.0. Using this block, Simulink modelsAutomation Studio Hot-Plug Interface

can be connected to existing B&R controllers without having to generate code. This means, for example, that a code-

generated controller on a B&R PLC can be connected to a simulated controlled system in MATLAB and then tested. It

is possible to use multiple instances in a Simulink model.

The connection is made using the protocol PVI. This protocol is not real-time capable.

Figure 44: Automation Studio hot plugging interface

Procedure

1)Button  is used to add a new controller to the left area. If no default settings have been changed, only the cor-[+]

rect IP address must be entered.

2)The block establishes a connection with the controller via PVI and searches all programs for process variables

that are then displayed in the center window on the right.

3)Use the up and down arrows to set individual variables as inputs or outputs.

4)Click the  button to generate and label the outputs and inputs on the block.OK

5)When simulation is started in Simulink, the block cyclically transfers the inputs to the B&R controllers and re-

ceives the outputs again each time a change is made.

## Page 53

AUTOMATION STUDIO HOT PLUGGING INTERFACE53

Variable names can be searched for directly in the preselection using field . The programs can be filtered usingSearch

the  drop-down box.Scope

A number between -1 and 10000 that stands for the update rate in milliseconds can be entered via field . This rateRF

indicates how often the variables are updated.

Starting with Automation Runtime version AR 4.61, it has been possible to enable ANSL authentication

in an Automation Studio project. Here, a password and role is assigned to authenticated users, which is

then assigned to the ANSL authentication under the online parameters in the CPU hardware configura-

tion. For details, see Automation Help. To ensure that the hot plugging interface can now also access the

password-protected connection, a dialog box appears every time a new connection is made. The variable

list is only reloaded after the username and password have been entered correctly. If this safety option

is omitted in the project, i.e. no user is created, the fields in the dialog box are left empty.

In order to be able to test the system appropriately, the time setting in Simulink should be changed to universal re-

al-time.

Figure 45: Simulation - Pacing options

Figure 46: Pacing options - Universal real-time

## Page 54

54AUTOMATION STUDIO TARGET FOR SIMULINK TM293

API hot plugging interface

The hot plugging interface block can be controlled via an API. This makes it possible to automatically communicate

with multiple controllers or APCs, for example. This is advantageous if multiple Simulink models with different target

hardware should exchange data efficiently via PVI. This is how a central computer (e.g. an APC) with Simulink models

and the API of the hot plugging block functions – on the one hand as a distribution node for several end devices, and

on the other, as a connection to a cloud-based network (see the following figure).

Figure 47: Example of a network application for the API hot plugging interface

In order to obtain an instance of the hot plugging block,

myHotPlugBlockInstance = getHotPlugInst('blockName');

creates an instance in the MATLAB workspace that can be used to access all functionsgetHotPlugInst('blockName')

required for controlling the block automatically via script. The name of the Hot-Plug block for which the API should be

enabled must be specified as a parameter. The name of the block can be read from the block's tag (in parentheses).

The name can also be user-defined (direct input of the name above the tag).

All functions at a glance:

Function nameDescription

addTarget('targetname', 'ipadress', 'port', 'user-Adds a connection to a target hardware.

name' (optional), 'password' (optional))

removeTarget('targetname')Removes a connection.

getTargetList()Returns a list of all created connections.

refreshTarget()Restarts the connection (e.g. after a timeout)

getVarList()Returns a list of all PVI variables.

Table 10: API functions of the hot plugging block

## Page 55

AUTOMATION STUDIO HOT PLUGGING INTERFACE 55
Function name Description
getVars() Returns a list of all variables that are not assigned to an
Inport or Outport block.
addToInPort('variablename1', 'variablename2',..) Assigns variables to the Inport block.
getInPortVars() Returns a list of all variables assigned to the Inport
block.
addToOutPort('variablename1', 'variablename2',..) Assigns variables to the Outport block.
getOutPortVars() Returns a list of all variables assigned to the Outport
block.
addAllToInPort() Assigns all variables to the Inport block that are current-
ly not assigned to the Outport block.
addAllToOutPort() Assigns all variables to the Outport block that are cur-
rently not assigned to the Inport block.
removeFromInPort('variablename1', 'variablename2',..) Removes variables from the Inport block.
removeFromOutPort('variablename1', 'variablename2',..) Removes variables from the Outport block.
removeAllFromInPort() Removes all variables from the Inport block.
removeAllFromOutPort() Removes all variables from the Outport block.
connectToTarget('targetname') Connects to target hardware 'targetname'
setRF(value) Sets the refresh time for updating the variables in mil-
liseconds (default 10 ms).
getRF() Outputs the refresh time in milliseconds.
Table 10: API functions of the hot plugging block
Example:
myHotPlugBlockInstance = getHotPlugInst('bur_hp');
myHotPlugBlockInstance.addTarget('myHost', '127.0.0.1', '11169');
myVars = myHotPlugBlockInstance.getVars();
myHotPlugBlockInstance.addToInPort(myVars(1).name, myVars(2).name);
myHotPlugBlockInstance.addToOutPort(myVars(3).name, myVars(4).name);

## Page 56

56AUTOMATION STUDIO TARGET FOR SIMULINK TM293

12Example: Automation Studio Hot-Plug

Interface block

Exercise 13: Create a control loop for a temperature-controlled system

This exercise is intended to show a practical ap-

plication of the Hot-Plug Interface block. The

adjacent graphic shows a closed control loop.

The Hot-Plug Interface block should be used

for a co-simulation between the controller (Au-

tomation Studio project) and the DUT (Device

Under Test, MATLAB/Simulink).

The green box marked  is in-Control system

tended to represent a controlled system, i.e.

the DUT (Device Under Test).

The orange box should represent the con-

troller. A PID controller from the demo exam-

ples should be used for this purpose (Note: En-

ter  in the command line). This

bur_demo(2)

controller should be generated in an Automa-

tion Studio project.

Here, the controlled system should be a tem-

perature model from the demo examples (Note:

Enter  in the command line).

bur_demo(3)

This is  code generated.not

1)Open the PID controller model with .

bur_demo(2)

2)Start the code generation of this model.

3)Close the model in Simulink.

4)Open the temperature model with .

bur_demo(3)

5)Add the Hot-Plug Interface block.

6)Configure the Hot-Plug Interface block according to the block diagram above. The output of the block is the in-

put of the temperature model; the input of the block is the output of the temperature model. The output of the

temperature model is the feedback variable to the input of the PID controller.

## Page 57

MODEL STRUCTURING AND DATA MANAGEMENT IN MATLAB57

13Model structuring and data manage-

ment in MATLAB

This section deals with the targeted use of the options available for preparing, managing and structuring Simulink

models and their data.

Figure 50: Data dictionary

Figure 49: Model referencing

Figure 48: Subsystems

Correct usage allows software errors to be avoided and the flexibility and consistency of the application to be im-

proved. The information provided here provides a small insight into which tools and procedures can be used. In ad-

dition, an overview of possible storage formats and locations for data should be a decision-making aid for tasks to

be solved.

13.1Subsystems

You can use  to create a hierarchical model with many levels. A subsystem is a group of blocks that yousubsystems

replace with a single subsystem block. This system structures a complex model and holds together the functionality

of the associated blocks.

Figure 51: Replacing a group of blocks with a subsystem and encapsulation of the functionality.

There are different types of subsystems. For more information about each subsystem, see the MATLAB documenta-

tion.

Figure 52: Selection of subsystem blocks.

## Page 58

58AUTOMATION STUDIO TARGET FOR SIMULINK TM293

The following approaches are available to create a subsystem:

Adding a subsystem block to the model. Blocks can be added to this after it is opened.

•

Marking all blocks that should form a subsystem, right-click and select "Create subsystem from selection".

•

Copying a model directly into a Simulink subsystem window

•

Copying an existing subsystem into a model

•

Figure 53: Creating a subsystem using function .Create subsystem from selection

For additional information about subsystems, see the MathWorks website.

www.mathworks.com/help/simulink/ug/creating-subsystems.html

www.mathworks.com/help/simulink/ug/configure-a-subsystem.html

www.mathworks.com/help/simulink/examples/variant-subsystems.html

13.2Example: Subsystems

Exercise 14: Subsystems

Open the example  using bur_demo(11) and answer the following ques-Inverted_Pendulum_with_variant_Subsystems

tions:

What type of subsystem was used?

•

How is it possible to navigate between the different levels?

•

Generate a hierarchical view of the model.

•

Simulink's  maps the hierarchical structure of a Simulink model.model dependency viewer

13.3Model referencing

You can use  to integrate models into another model using a model block. Simulink offers the twomodel referencing

model blocks "Model" and "Model variants" for referencing models. The difference between the two blocks is that

the user can use "Model variants" to set different references to other models. The user can then switch between the

different referenced models. Block "Model" can only set one model reference.

Model referencing allows you to do the following, for example:

Structure large models hierarchically

•

Reuse the same functionality without having to redefine it

•

Modular development

•

Incremental loading of models

•

Incremental code generation of the model

•

## Page 59

MODEL STRUCTURING AND DATA MANAGEMENT IN MATLAB59

Figure 54: Model referencing

For additional information about model referencing, see the MathWorks website.

www.mathworks.com/help/simulink/model-reference.html

www.mathworks.com/help/simulink/ug/overview-of-model-referencing-1.html

www.mathworks.com/help/simulink/ug/model-referencing-limitations.html

B&R input and output blocks within a referenced model are not created as process variables!

## Page 60

60AUTOMATION STUDIO TARGET FOR SIMULINK TM293

13.4Model referencing example

Exercise 15: Model referencing

Open the example  using bur_demo(10) and answer the following questions:Inverted_Pendulum

What is the difference to the subsystem?

•

How many sub-level models does the top-level model have?

•

Generate a hierarchical view of the model.

•

Simulink's  maps the hierarchical structure of a Simulink model.model dependency viewer

13.5Data dictionary

A  is a persistent repository of data that is relevant for a model. Persistent means that no data must bedata dictionary

reloaded during development. A data dictionary offers more functions than the workspace. A complete list is available

here:

https://www.mathworks.com/help/simulink/ug/what-is-a-data-dictionary.html.

Figure 55: Possible link between the Simulink model and data dictionary.

The main difference to the workspace is the persistence of data, i.e. the data is always available and does not have

to be reloaded during development. The data dictionary stores design data (e.g. parameters) and not simulation data

that is input or output from a model simulation that enters and exits input and output blocks. In the ,Model Explorer

tab "" allows the model to link to either the workspace or a data dictionary:Data

Figure 56: Model Explorer - Setting a link to the data dictionary.

## Page 61

MODEL STRUCTURING AND DATA MANAGEMENT IN MATLAB61

After the setting has been applied, the data dictionary icon appears in the model hierarchy. If you open section "Design

data", all variables that were in the workspace are displayed. New variables can be added there and existing variables

edited and deleted.

Figure 57: Model Explorer - Adding variables to the data dictionary.

MATLAB supports data dictionaries in R2014a and higher. Automation Studio Target for Simulink supports data dictio-

naries in V5.6.0 or later with the B&R parameter block, B&R workspace variable block and B&R bus input/output block.

## Page 62

62 AUTOMATION STUDIO TARGET FOR SIMULINK TM293
By linking a model to a Data Dictionary, the following automatically occurs:
Variables of the B&R parameter block are written to the data dictionary.
•
In the B&R workspace variable block, a list of variables is only offered from the data dictionary.
•
In the B&R bus input/output block, a list of bus elements is only offered from the data dictionary.
•
As long as a model is connected to a data dictionary, the B&R blocks only support the variables created
in the data dictionary.
13.6 Data dictionary example
Exercise 16: Data dictionary
Open the example pid_controller_bus_dd using bur_demo(15).
Link the model to the attached data dictionary.
•
Adjust the B&R config block.
•
Generate the Simulink model in Automation Studio.
•
Check the functionality by including a trace of the output variable. Does this reach the final value?
•

## Page 63

SUMMARY 63
14 Summary
Simulation and model-based development are essential aspects when working with Automation Studio Target for
Simulink. The interface allows easy conversion of existing Simulink models and guarantees seamless integration of
the generated program code into an existing Automation Studio project.
Automation Studio Target for Simulink thus extends the simulation capabilities of Automation Studio by integrating
the MATLAB/Simulink simulation tool.

## Page 64

64AUTOMATION STUDIO TARGET FOR SIMULINK TM293

15Appendix

15.1Programming interfaces (API)

B&R Config block

There is an API for the B&R Config block that is used to control it programmatically, i.e. from the command line or

a .m file.

Example for setting the Automation Studio project path:

bur_set_param('bur_projpath', 'C:\test’);

bur_get_param('bur_projpath’)

>> ans =

‘C:\test’

To do this, the block must be added to the model and the model window must be active. The command  can be

gcs

used to check which model window is active.

For all API commands, see TM140.

B&R Automation Studio Hot-Plug Interface block

There is an API for the B&R Automation Studio Hot-Plug Interface block that is used to control it programmatically, i.e.

from the command line or a .m file.

First, an instance of the block must be created. The block name must be entered here since there can be multiple Hot-

plug blocks in a model:

myHotPlugBlockInstance = getHotPlugInst('bur_hp');

Afterwards, the API functions can be used:

myHotPlugBlockInstance = getHotPlugInst('bur_hp');

myHotPlugBlockInstance.addTarget('myHost', '127.0.0.1', '11169');

myVars = myHotPlugBlockInstance.getVars();

myHotPlugBlockInstance.addToInPort(myVars(1).name, myVars(2).name);

myHotPlugBlockInstance.addToOutPort(myVars(3).name, myVars(4).name);

For all API commands, see TM140.

15.2Data type conversions

During programming, it may become necessary to convert one

data type to another.

When assigning a variable of a data type with a smaller range of

values to one with a larger range, implicit conversion is carried

out. When the opposite is done (the range of values becomes

smaller), the user has to handle the conversion in the program

code itself, i.e. explicitly.

The IEC 61131-3 conversion functions are contained in the  library, which is automatically included in a newAsIecCon

Automation Studio project.

Implicit data type conversion

Implicit data type conversion is type conversion from one data type to another that is performed by the compiler itself.

Explicit data type conversion

If the value a data type with a larger range of values is assigned to a data type with a smaller range, then it's up to the

user to carry out the conversion. The functions in the "AsIecCon" library can be used to carry out the conversion.

Direct composition and subranges

In addition to arrays, other derived data types can also be derived from basic data types. It is possible to derive simple

data types directly. A new data type with a new name is created that has the same properties as the basic data type.

An initial value can also be assigned to the new data type. All variables that use these data types therefore have the

## Page 65

APPENDIX65

configured value. A value range can also be specified for direct derivation. It is therefore only possible to assign values

to variables of this type that lie within the set value range. A subrange can also be assigned to variables.

Variable with a subrange

VAR

varSubRange : USINT(24..48);

END_VAR

Data type with a subrange

TYPE

Voltage_typ : USINT(12..24);

END_TYPE

Table 11: Declaring a variable and data type with a subrange

15.2.1B&R extended I/O block

The  generates a process variable in Automation Studio that is subject to data typeB&R extended input/output block

conversion and/or direct derivation. The conversion or casting is carried out automatically by the block.

One use case would be to convert hardware inputs or outputs (usually INT) to floating-point values (REAL or LREAL)

for more powerful calculations in the controller algorithm and vice versa.

Figure 58: B&R extended input blockFigure 59: B&R extended output block

Name of the process variable in Automation Studio.Variable name:

Comment entry for the description of process variables.Variable description:

Availability and visibility of the process variable within the entire project. Values: "Global" or "Local".Variable scope:

Decides whether the process variable is battery-backed. Values: "Retain" for battery-backed or "Standard"Memory:

for not battery-backed.

## Page 66

66AUTOMATION STUDIO TARGET FOR SIMULINK TM293

If the value is 1, a process variable is created as a variable. If the value is greater than 1, a process variableArray size:

is created as a one-dimensional array.

Data type of the process variable per IEC 61131-3 that is available in Automation Studio.Automation Studio data type:

With reference to the B&R extended output block, the conversion from the Simulink data type to the Automation Studio

data type is calculated as follows:

Minimum value that the Automation Studio variable is permitted to accept.Automation Studio minimum value:

Maximum value that the Automation Studio variable is permitted to accept.Automation Studio maximum value:

Initial value of the process variable, which is limited by the minimum and max-Automation Studio simulation value:

imum values.

Data type of the process variable per IEC 61131-3 that is available in Automation Studio.Simulink data type:

With reference to the B&R extended input block, the conversion from the Automation Studio data type to the Simulink

data type is calculated as follows:

Minimum value that the Automation Studio variable is permitted to accept.Simulink minimum value:

Maximum value that the Automation Studio variable is permitted to accept.Simulink maximum value:

Initial value of the process variable, which is limited by the minimum and maximum values.Simulink simulation value:

15.3Diagnostics

This section explains the different variants of diagnosis the Automation Studio project using the Simulink model.

Automation Studio provides the user with a wide variety of diagnostic tools for installation and localizing errors. They

are described in more detail in section Diagnostic tools.

15.3.1Debugging

When option "Debugging" is enabled, the generated task/library is marked as "debug-bar" in the Automation Studio

project.

## Page 67

APPENDIX67

Figure 60: Advanced settings - Debugging

## Page 68

68AUTOMATION STUDIO TARGET FOR SIMULINK TM293

Figure 61: Automation Studio task properties - Tab compiler

15.3.2External mode

The Simulink  configuration setting enables communication at runtime between the Simulink modelexternal mode

and the code-generated model running on the target system.

External mode establishes one communication service each on the system on which MATLAB is installed (PC) and the

target system (controller or PC). These two services on the respective systems establish a communication channel

between the Simulink engine and the generated code provided on the target system. The communication service iso-

lates the model process on the target system from the code and transport layer. The tasks of the transport layer are to

format, transmit and receive data packets. The communication service on the host computer receives the data pack-

ets through the transport layer and updates the Simulink model display. The diagram shows the connection that the

## Page 69

APPENDIX69

communication service establishes in external mode between Simulink on the host computer and the provided code

on the target system.

Figure 62: External mode communication scheme MATLAB/Simulink

Configuring external mode

To use external mode, the feature must be enabled in the B&R config block in tab . In addition, threeAdvanced settings

settings must be made:

IP address of the target system. The "localhost" IP address 127.0.0.1 is entered as the default valueIP address:

•

(e.g. when using ARsim).

External mode uses the port for communication with Automation Studio. The port number is an integer val-Port:

•

ue between 1024 and 65535. The MATLAB default value is port number 17725.

Buffer size that is preallocated on the target system. Default value: 1000000 bytes (1 MB)Buffer size:

•

B&R libraries  and  are necessary for external mode in Automation Studio. They are automatically addedAsArLogAsTCP

to the Automation Studio project when a task or function block is created. If a ZIP package is created, these libraries

must be added manually in the Automation Studio project.

Figure 63: External mode settings in the B&R config block

Enabling external mode generates additional code during model code generation that affects perfor-

mance. For additional information, see the documentation for External mode from MATLAB.

https://de.mathworks.com/help/rtw/ug/set-up-and-use-hosttarget-communication-chan-Link:

nel.html

## Page 70

70AUTOMATION STUDIO TARGET FOR SIMULINK TM293

Starting external mode

1)Enable and specify external mode features in the B&R Config block.

2)Generate code and start the target system.

3)Configure the Simulink model for external mode and start the external mode connection.

External mode can also be started and stopped via the  with the con-External Mode Control Panel

nect/disconnect button.

Manipulating variables on the target system from Simulink

While communication between the Simulink model and target system is taking place, process variables can be ob-

served and manipulated in the Simulink model.

Displaying process variables in the Simulink model

To check current values in the Simulink model at runtime, blocks from Simulink library "Sinks" can be used (e.g. "Scope"

or "Display").

Manipulating process variables in the Simulink model

There are two ways to manipulate parameters from Simulink. The first is modifying the values of Simulink blocks like

or  directly during the external mode connection. The other is to use the .GainConstantExternal Mode Control Panel

The External Mode Control Panel allows values of workspace variables to be changed locally in Simulink and then down-

loaded to the target system using command . This can be used with the B&R Parameter block or B&RBatch download

Workspace Variable block, for example. Only parameters that are located in the MATLAB workspace require the batch

download. (B&R input/output parameters cannot be changed because they are not in the workspace, for example)

The External Mode Control Panel is located under Code > External Mode Control Panel > Batch download > Download.

## Page 71

APPENDIX71

15.3.ASimulink web interface

It is possible to operate the Simulink model via a web service in the web browser. This feature is only available when

a task is generated.

The web service can be accessed at the following address:

http://<TARGET_IP>/Webservice_<ROOT MODEL NAME>.cgi

Figure 64: Simulink web interface - Code generation warning

The current version supports the following Simulink blocks

#All B&R blocks (except for the B&R Extended Struct block)

#Simulink Inport and Outport blocks

#,  (if not optimized away by the coder)GainSum, Product, Saturation

#Subsystems

#Stateflow

The following data types are supported:

#Standard data types: LREAL, REAL, SINT, USINT, INT, UINT, DINT, UDINT, BOOL, USINT

#One-dimensional arrays of standard data types

#Bus/structures with standard data types as elements

Enabling the Simulink web interface generates additional code during model code generation that af-

fects performance. In addition, all process variables used can be read and written via a non-secure http

access. This feature should therefore only be used for testing and debugging a model.

## Page 72

72AUTOMATION STUDIO TARGET FOR SIMULINK TM293

Figure 65: When activating the Simulink web interface, it is also possible to specify the hierarchical depth up to which referenced models in the system or

subsystems in the referenced models (and also the Top model) should be displayed in the web interface. For the setting of the depth of the subsystems,

the set value always applies starting from the uppermost Top model, i.e. if, as in the figure, the value 3 is set, then, regardless of whether a subsystem or

a referenced model is located in the Top model, the respective system is generated up to a depth of 3.

To be able to calculate the coordinates of individual blocks/objects correctly, there must be a "real" block

at the top left position in each system (no comment, no connecting line, etc.).

## Page 73

AUTOMATION ACADEMY73

Automation Academy

Your knowledge advantage

The Automation Academy provides  courses for our customers as well as for our own employees.targeted training

Expand your skills in the field of automation technology and learn to independently implement efficient automation

using B&R systems.solutions

Decide for yourself which  you prefer!learning concept

Classroom learning

B&R offers  at all B&R locations. Services include seminar documents, effective communication ofstandard seminars

training course content by experienced trainers and an Automation Diploma. A combination of group work and self-

study provides the high level of flexibility needed to maximize the learning experience.

Virtual classroom

supplement B&R's continuing education portfolio with a virtual classroom, offering an alternative toRemote Lectures

our on-site seminars. Selected content from our standard seminars is offered online. In addition to remote learning

methods, powerful simulation tools and secure remote maintenance are used.

Online courses

Take control of the content and learn at your own pace. With B&R , you can take your first steps in theonline courses

world of B&R automation at any time. Based on a comprehensive narrative, you will independently work out how to use

our products. The mix of different media allows a logical sequence to be followed when learning as well as a targeted

choice of information to be used as a reference.

Contact

Would you like additional training? Are you interested in finding out what the B&R Automation Academy has to offer?

If so, this is the right place.

Access additional information here:

https://www.br-automation.com/de/academy/

Enjoy your next training course!

## Page 74

74 AUTOMATION STUDIO TARGET FOR SIMULINK TM293

## Page 75

AUTOMATION ACADEMY 75

## Page 76

B&R

Industrial Automation GmbH

A member of the ABB Group

B&R Straße 1

5142 Eggelsberg, Austria

office@br-automation.com

t +43 7748 6586-0

f +43 7748 6586-26

br-automation.com

V2.0.0.0 ©2023/09/27 by B&R, All rights reserved.

All registered trademarks are the property of their respective owners.

Subject to technical changes without notice.