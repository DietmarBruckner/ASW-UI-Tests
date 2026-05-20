## Page 1

TM210

Working with

Automation Studio

## Page 2

2 WORKING WITH AUTOMATION STUDIO TM210
Requirements
Necessary basic Basic technical training
knowledge Basic knowledge of how a control system functions
Training modules –
Automation Studio 6.0
Software
Automation Runtime 6.0 and later
X20 controller and X20 I/O modules
Hardware

## Page 3

TABLE OF CONTENTS 3
Table of contents
1 Introduction.........................................................................................................................................................4
1.1 Learning objectives..............................................................................................................................4
1.2 Safety notices and symbols...............................................................................................................4
1.3 B&R online courses...............................................................................................................................5
2 Installation and licensing.................................................................................................................................6
2.1 Installation wizard................................................................................................................................6
2.2 Automation Studio licensing.............................................................................................................7
3 My first project...................................................................................................................................................8
4 Automation Studio............................................................................................................................................9
4.1 Basic concept of Automation Studio..............................................................................................9
4.2 Automation Help – The integrated help documentation............................................................9
4.3 The workspace....................................................................................................................................11
5 Software, hardware and configurations.....................................................................................................17
5.1 Software management in the Logical View..................................................................................17
5.2 Hardware organization - Physical View and System Designer.................................................17
5.3 Organizing configurations in the Configuration View...............................................................19
5.4 Assigning programs - Software configuration............................................................................19
6 Configure the hardware.................................................................................................................................23
6.1 Testing in simulation mode.............................................................................................................24
6.2 Adding I/O modules from the Hardware Catalog......................................................................25
6.3 Mapping process variables to I/O channels................................................................................26
6.4 Editing an I/O configuration...........................................................................................................27
6.5 Configure the network interface of the controller.....................................................................27
6.6 Compiling the project.......................................................................................................................28
7 Initial installation of the controller..............................................................................................................30
7.1 Establishing a connection to the target system.........................................................................30
7.2 Project installation.............................................................................................................................32
8 programming....................................................................................................................................................42
8.1 Programming languages..................................................................................................................42
8.2 Initialization and cyclic program section.....................................................................................42
8.3 Variables and data types.................................................................................................................43
8.4 Creating an application...................................................................................................................45
8.5 Import, export and team functions...............................................................................................46
9 Updates and licenses......................................................................................................................................48
9.1 Hardware and software upgrade...................................................................................................48
9.2 Technology Guarding........................................................................................................................49
10 Summary..........................................................................................................................................................50

## Page 4

4WORKING WITH AUTOMATION STUDIO TM210

1Introduction

Automation Studio is the configuration environment used for B&R automation components. This includes controllers,

motion control components, safety modules and HMI applications. Automation Studio offers the perfect environment

for creating different variants of a machine and managing them neatly. Projects can be structured clearly and facilitate

team work in this way.

Users can choose from a wide range of programming languages, diagnostics tools and editors to assist them at every

stage of engineering. Standard libraries provided by B&R and the integrated IEC programming languages allow a highly

efficient workflow. Extensive simulation options make it easier to configure and test applications independently of

the hardware.

Figure 1: Automation Studio – One engineering tool for the machine's entire lifecycle

1.1Learning objectives

This training module contains simple tasks to demonstrate the wide range of tools available in Automation Studio. In

addition, it will frequently refer to the extensive Automation Help, an invaluable reference for completing the exercises

in this training module.

Participants will learn how to create and configure projects in Automation Studio.

•

Participants will learn how to develop small programs and declare process variables.

•

Participants will learn how to use Automation Help.

•

Participants will learn how to set up hardware configurations and use the simulation features in Automation Stu-

•

dio.

Participants will learn the steps required to commission a B&R controller.

•

Participants will learn how to use the Automation Studio user interface and the various editors available.

•

Participants will learn about the options for configuring modules with Automation Studio's hardware manage-

•

ment features.

1.2Safety notices and symbols

Safety notices in this manual are organized as follows:

Disregarding these safety guidelines and notices can result in severe injury, death or substantialDanger:

damage to property.

Disregarding these safety guidelines and notices can result in severe injury or substantial dam-Warning:

age to property.

## Page 5

INTRODUCTION 5
Caution: Disregarding these safety guidelines and notices can result in injury or damage to property.
These instructions are important for avoiding malfunctions.
Additional notices and information in this manual are organized as follows:
Note: Provides important tips and additional information.
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
1.3 B&R online courses
The B&R online courses provide lessons for a wide range of topics. Because the courses are
interactive, they allow content to be learned effectively.
To help you find the B&R online courses you need more quickly, the various courses on the
website are assigned to different training categories and are based on the modular training
concept.
B&R online courses https://www.br-automation.com/en/academy/online-courses/)

## Page 6

6WORKING WITH AUTOMATION STUDIO TM210

2Installation and licensing

Automation Studio is available for download on the B&R website.

All Automation Studio software versions are available at Downloads, "Product groups = Software", "Software = Au-

tomation Studio".

Once Automation Studio has been installed, it must be licensed.

2.1Installation wizard

The installation wizard guides the user through the installation process. After selecting the language for installation, a

number of installation options are available. These include viewing version information, calling system requirements,

etc.

Figure 2: Automation Studio installation: Selecting the desired languageFigure 3: Automation Studio installation: Installation options and version

information

In the following dialogs, the license agreement is accepted and a selection is made as to which components are to

be installed.

Automation software \ Software installation \ Automation Studio

2.1.1Start Automation Studio.

The Automation Studio installation automatically adds the necessary short-

cuts to the Start menu and desktop in Windows. It is then possible to launch

Automation Studio from the Windows Start menu or using the shortcut on

the desktop.

Figure 4: Automation Studio desktop icon

## Page 7

INSTALLATION AND LICENSING7

When Automation Studio is started for the first time, the Automation Studio start page is displayed.

Figure 5: The Automation Studio home screen

From the home screen, you can create a new project or open an existing one.

It also includes section "Getting started" to show users how to work with Automation Studio.

The home screen is displayed automatically after closing a project. It can also be opened via menu "Help" \ "Show

start page".

2.1.2Version information

For an overview of the functions of the new Automation Studio version, see the section in Automation Help with version

information. The differences to the previous versions as well as new functions are explained briefly here.

Automation software \ Version information

2.2Automation Studio licensing

Once Automation Studio has been installed, it needs to be licensed. If Automation Studio has not yet been licensed,

the licensing dialog box will be displayed automatically.

Depending on the selected license type, a Technology Guard is required as license storage, see 9.2 "Technology Guard-

ing" on page 49. Evaluation licenses and student licenses can be requested directly on the B&R web page.

For a complete description of the licensing dialog box, see Automation Help.

Automation software \ Software installation \ Automation Studio

www.br-automation.com  Service  Software registration→→

## Page 8

8WORKING WITH AUTOMATION STUDIO TM210

3My first project

In this section, you will use Automation Help as a reference to create a new project, transfer it to Automation Runtime

Simulation and test it using Automation Studio.

Getting Started \ Creating programs in Automation Studio

To help you start using Automation Studio, B&R offers a "Getting started" tutorial.

With the help of step-by-step instructions, you will become familiar with the basics of Automation Studio

and learn how to configure, program and operate both hardware and software.

https://www.br-automation.com/learn-as

Exercise: Create a new project with the support of Automation Help

The corresponding section of Automation Help can be opened directly from the Automation Studio home screen.

Figure 7: Procedure for creating your first project

Figure 6: Automation Desktop home screen

1)Open the Automation Studio home screen.

2)Click on "How do I create an control project?"

3)Click on "Example project with Automation Runtime Simulation" in Automation Help

4)Work through the topics listed

Automation Help is a useful aid for creating a new project, designing a program and then transferring

the project to the Automation Runtime Simulation where it can be tested.

Some of the elements of Automation Studio have already been introduced during the course of this first

project. The next few sections will explain the functionality of Automation Studio in detail.

## Page 9

AUTOMATION STUDIO9

4Automation Studio

4.1Basic concept of Automation Studio

In an Automation Studio project, the application software is managed and structured in the Logical View. The machine

design and functions determine what the structure looks like. This direct relationship between the application and the

machine makes the software structure intuitive and easy to navigate.

Completed software components are assigned to their respective configurations in the Configuration View. For each

configuration, there is a corresponding hardware configuration that is managed in the Physical View.

Automation Studio allows automation projects to be structured modularly to support distributed development ap-

proaches and makes working in project teams more efficient.

Figure 8: Structure and principle of Automation Studio

4.2Automation Help – The integrated help documentation

Automation Help is an invaluable resource throughout the development, configuration and commissioning of a

project. Automation Help is a comprehensive resource and the first place to look for answers to any questions that

might arise.

Automation Help as a reference:

Working with Automation Studio and its editors

•

Creating programs

•

Configuring an HMI application

•

Configuring drives

•

Complete B&R hardware documentation

•

If new hardware or software upgrades are installed in Automation Studio, the corresponding documentation is auto-

matically extended in Automation Help (see 9.1 "Hardware and software upgrade" on page 48).

How can the language in Automation Help be changed?

The language in Automation Help is set via menu option "View \ Language".

## Page 10

10WORKING WITH AUTOMATION STUDIO TM210

Exercise: Use Automation Help

Press the  in Automation Studio to open Automa-<F1 key>

tion Help and view information about the selected ele-

ment. There is also a search function to look for informa-

tion about a specific topic. The functions of Automation

Help are described in the section Automation Software""

.\ "How do I use the help documentation?"

The technical data for controller X20CP1686X can be

found via the .Search option

Use the technical data to get information about the pow-

er consumption of the selected controller.

Figure 9: Automation Help

4.2.1Manage help documentation favorites

Help favorites make it possible to manage and store helpful pages within a personalized structure. It is possible to save

the Help favorites in a file. In this way, Help favorites can be managed as needed and even passed on to other people.

Figure 10: Managing Help favorites

1)Save

Help Favorites can be saved to a file via "File" \ "Save as". Any newly added entries are saved automatically.

2)Help favorites

The Help favorites are managed in a tree structure. Entries can be moved and renamed.

3)Toolbar

Buttons "Help contents" and "Help favorites" make it possible to switch between the Favorites view and the navi-

gation tree. The navigation tree is synchronized at the same time.

4)Adding Help favorites

Add the currently open help page to your Help favorites by right-clicking on the document and then selecting "Add

to help favorites". This will open a dialog box for selecting the position in the favorites list.

Automation software \ How do I use the help documentation? \ Help favorites

## Page 11

AUTOMATION STUDIO11

4.3The workspace

Automation Studio is divided into several sections with different functions.

Figure 11: The Automation Studio workspace

1)Project Explorer

This is where we can manage and edit the hardware and software objects that make up a project.

2)Section for open documents

Programs are edited here, for example.

3)Toolbox

Depending on what object you are currently working on, the Toolbox window allows you to select and add hard-

ware modules, program functions or software objects.

4)Output window

Compiler warnings, for example, are displayed here.

5)Properties window

This window displays configuration options for whichever object or hardware module is currently selected. It can

also be used to edit the properties of the selected object.

Project management \ Workspace

4.3.1Repositioning the windows

When a project is opened in Automation Studio, the various section windows are all docked to the main program

window. All of the section windows can be rearranged by clicking and dragging the title bar.

## Page 12

12WORKING WITH AUTOMATION STUDIO TM210

Figure 12: Docking and undocking windows

Project management \ Workspace \ Project Explorer

4.3.2Hiding windows

To create more space on the screen, for example when working with visual

programming editors, the Project Explorer and all other dockable windows

can be hidden. The windows are hidden and shown by clicking on the pin icon

Figure 13: Click on the pin icon to dock or hide

in the title bar.

windows.

How do I get back to the original window layout?

The default window layout in Automation Studio can be restored under "Window" \ "Reset window lay-

out".

Project management \ Workspace \ Project Explorer

4.3.3Workbooks

The Workbook mode presents a clear display of open windows and a convenient way to switch between them. Windows

can be overlapping or arranged above or next to one another.

Figure 14: Workbooks in Automation Studio

## Page 13

AUTOMATION STUDIO13

If several editors are open at the same time, each one is displayed in a separate workbook. For an overview of all open

workbooks, click on the drop-down symbol on the right side of the title bar.

Figure 15: Overview of open workbooks

Project management \ Workspace

Workbook mode

•

Workbook mode \ Configuring workbooks

•

Exercise: Work with Automation Studio

The aim of this task is to introduce participants to the Automation Studio workspace.

1)Identify individual work areas.

2)Open context-sensitive Automation Help for the individual work areas by pressing the .<F1 key>

3)Rearrange, show and hide windows.

4)Restore original window layout.

4.3.4Menus and toolbox

The main menu provides access to all of the functions in Automation Studio.

Depending on the active editor, individual menu options are shown, hidden, enabled or disabled. In this way, only the

functions available for the current context can be selected.

## Page 14

14WORKING WITH AUTOMATION STUDIO TM210

Toolbox - Catalog

The Toolbox provides access to program and configuration elements, programming symbols or hardware modules

depending on the editor that is selected.

Figure 17: Toolbox - Hardware Catalog: HardwareFigure 16: Toolbox - Ladder Diagram:Figure 18: Toolbox - Object Catalog: Programs

modules, infrastructure componentsProgramming functions, commands, functionsand configuration files

and function blocks

Project management \ Workspace

Main menu

•

Toolbars

•

Keyboard shortcuts

•

Catalogs

•

Project management \ Hardware management \ Physical View \ Editing operations

4.3.5The convenience of SmartEdit

The SmartEdit feature combines a range of intelligent functions that provide input support with Automation Studio

editors.

Autocomplete

Keyboard shortcut  automatically complete partially entered terms. Characters already entered<CTRL> + <SPACE>

serve as a filter.

The following elements are supported:

Variable names, structure elements, constants and

•

enumerators

Function names and function blocks

•

Figure 19: SmartEdit - Code completion

## Page 15

AUTOMATION STUDIO15

Code snippets

Keyboard shortcutcan be used to <CTRL> + <q>, <k>

add code snippets. Code snippets are ready-made bits of

source code that can be managed by the user in the code

snippet manager.

Figure 20: Adding a code snippet

Automatic declaration

Automatic declaration of new variables can be helpful depending on prefer-

ence. In SmartEdit settings, the editors can be adjusted in various user-spe-

cific ways.

Open the configuration dialog box by selecting "Extras" \ "Options" from the

main menu. Activate the setting for automatically declaring variables in tab

"SmartEdit".

Figure 21: Setting for automatic declaration of

new variables

Additional editor functions

The editors in Automation Studio offer additional functions that can help improve your overview of the program code.

Tooltips

•

Syntax coloring

•

Coloring for modified lines of code

•

Collapse or expand code segments

•

Display corresponding parentheses pairs

•

Automatic indenting

•

Opening variable declarations and function implementations directly from the program code

•

FunctionKeyboard shortcut

Complete a code snippet<TAB>

Auto-complete a variable or function name<CTRL> + <SPACE>

Jump to a variable declaration<CTRL> + <d>

Jump to the declaration of a variable's data type<CTRL> + <t>

Comment line or selection<CTRL> + <k>

Table 1: Useful SmartEdit keyboard shortcuts

## Page 16

16 WORKING WITH AUTOMATION STUDIO TM210
Programming \ Editors
General operation \ SmartEdit
•
General operation \ SmartEdit \ Code snippets
•
General operation \ Dialog boxes for input support \ Automatic declaration of variables
•
Text editors
•
Table editors - General
•
Project Management \ Workspace \ Keyboard shortcuts

## Page 17

SOFTWARE, HARDWARE AND CONFIGURATIONS17

5Software, hardware and configura-

tions

The Project Explorer is a central element of the Automation Studio interface. The Project Explorer contains the Logical

View for organizing software, the Physical View for organizing hardware and the Configuration View for managing

configurations of multiple machine variants in a single project.

5.1Software management in the Logical View

POUs (program organization units) are arranged in the  in a treeLogical View

structure.

Each of these units is organized into packages, which are comparable to di-

rectories. A package might include all of the software elements needed for a

functional unit as well as accompanying documentation.

In this view, there is no direct relationship between the software and the ac-

tual hardware being used. It only serves to organize and manage the different

POUs.

Project management \ Logical View

Figure 22: Logical View

5.2Hardware organization - Physical View and System Designer

The hardware required for the machine can be managed either in a hierarchical view or in a graphical view.

Hardware management features in Automation Studio are used to perform the following tasks:

Adding and configuring hardware modules

•

Mapping variables to I/O data points

•

Configuring fieldbus modules and interfaces

•

## Page 18

18WORKING WITH AUTOMATION STUDIO TM210

Physical View

The Physical View is a hierarchical

topology overview of the configured

hardware.

When creating a project or a new sys-

tem configuration, a CPU is selected.

The required hardware modules are

then added into the topology from the

Toolbox.

Figure 23: Physical View in Automation Studio

The configuration of individual hardware components and interfaces is opened in Physical View via the shortcut menu.

This allows you to make module-specific settings, network configurations and configuration settings for system be-

havior.

System designer

System Designer provides a visual representation of the Physical View. It allows control components to be arranged

just as they would be in the actual machine. Hardware modules can be added both in System Designer and in the

Physical View, see 6.2 "Adding I/O modules from the Hardware Catalog" on page 25. The two views are synchronized

with each other.

Figure 24: Opening System Designer via the toolbar of the Physical View and adding hardware modules from the Toolbox.

How can System Designer content be arranged clearly?

The System Designer editor allows objects to be laid out automatically. The ability to add text boxes

makes it possible to group hardware components and assign them to a control cabinet, for example.

Project management \ Hardware management

Physical View

•

System Designer

•

Edit operations \ Text boxes

°

View operations \ Automatic layout

°

## Page 19

SOFTWARE, HARDWARE AND CONFIGURATIONS19

5.3Organizing configurations in the Configuration View

All the different variants of a given machine are managed in the . The configurations typically differConfiguration View

in terms of software scope and the exact hardware used.

You can add a new configuration using the toolbox.

Figure 25: Configuration View with active configuration [Active]; adding a new configuration via drag-and-drop.

When you double-click on a configuration to activate it, the corresponding hardware assigned is displayed in the Phys-

ical View as well as in System Designer. Only one configuration can be active  at a time.[Active]

The following configuration elements are managed in the Configuration View:

Software configuration and I/O mapping

•

Configuration of user role system, certificates, text system and OPC UA server.

•

Configuration of mapp Technology components

•

Configuration of motion control

•

Project management \ Configuration View

5.4Assigning programs - Software configuration

The software elements to be transferred to the target system for the active configuration are assigned in the software

configuration.

There are several different ways to assign software to the software configuration:

5.4.1 "Automatic assignment when creating a program" on page 20

•

5.4.2 "Adding existing programs manually" on page 21

•

To open the software configuration in the Physical View,Alternatively, file "CPU.sw" in the Configuration

double-click on the controller or select "Software" from theView can be used to open the software configura-

controller's shortcut menu.tion.

## Page 20

20WORKING WITH AUTOMATION STUDIO TM210

Figure 26: Opening the software configuration from the controller's shortcut

menu.

Figure 27: Opening the software configuration by selecting

"CPU.sw" in the Configuration View.

Project management \ Logical View

Programming \ Editors \ Configuration editors \ Software configuration

5.4.1Automatic assignment when creating a program

If a program is added to the Logical View via the toolbox,

then it is automatically included in the software configu-

ration for the active configuration.

Task class #4 is used as the default task class. The default

task class is changed in the settings of the active config-

uration.

Figure 28: When added, the software object is automatically assigned to

the software configuration of the active configuration.

How is the execution order of the software defined?

The execution order and priority of programs can be changed by moving the corresponding objects as

necessary directly in the software configuration.

Real-time operating system \ Mode of operation \ Runtime behavior

Project management \ Configuration View \ Properties of the objects in the Configuration View

## Page 21

SOFTWARE, HARDWARE AND CONFIGURATIONS21

5.4.2Adding existing programs manually

If a new hardware configuration is added to the Configuration View, the software elements must be assigned manually

to the software configuration.

Open the software configuration, then select the Logical View in the Project Explorer. An object can then be assigned

by moving it from the Logical View to the desired position in the software configuration.

Figure 29: Assigning software elements to the software configuration

By double-clicking on an entry in the software configuration, the Logical View with the corresponding entry is selected.

Can more than one software of a package be assigned to the software configuration?

Entire packages from the Logical View can be added to the software configuration. To do this, drag the

selected package from the Logical View to the highest position in the software configuration.

## Page 22

22WORKING WITH AUTOMATION STUDIO TM210

5.4.3Creating variants by assigning software packages

The ability to manage multiple configurations in one project allows you to work with the different variants of a given

machine type all in the same project. These configurations can vary with regard to the extent of software being used

or in their hardware design.

The Logical View is shown in the image on the left. This view is used to manage libraries and programs. The image on

the right shows a schematic representation of the different variants of a machine. In Automation Studio, each variant

is represented by a corresponding configuration. The configurations differ from each other in terms of the hardware

used and the software packages that are assigned to them.

Figure 30: Different machine variants can be created by assigning software packages to them.

Project management

## Page 23

CONFIGURE THE HARDWARE23

6Configure the hardware

This section is based on the previously created project, see 3 "My first project" on page 8.

This project already contains a configuration based on the "Automation Runtime Simulation" and a Ladder Diagram

program with two variables.

Exercise: Configure the hardware and commission the system

Assign program  to task class "Cyclic#1"."LampTest"

1)Create a new configuration.

2)Assign program "LampTest" to the software configuration.

5.4.2 "Adding existing programs manually" on page 21

3)6.1 "Testing in simulation mode" on page 24

4)6.2 "Adding I/O modules from the Hardware Catalog" on page 25

5)6.3 "Mapping process variables to I/O channels" on page 26

6)6.5 "Configure the network interface of the controller" on page 27

7)6.6 "Compiling the project" on page 28

8)7.1.1 "Connection via target system search" on page 31

9)7.2.1 "Online installation" on page 33

Add a new configuration using the toolbox. To do this, open the Configuration View and add a new configuration via

drag-and-drop or double-click.

Figure 31: Adding a new configuration in the Configuration View using the toolbox

Then, select the desired controller in the wizard.

## Page 24

24WORKING WITH AUTOMATION STUDIO TM210

Exercise: Create a new configuration and assign programs

In this task, a new configuration with an X20 controller (e.g. X20CP1686X) is added to the project.

1)Add a new configuration.

2)Select the desired control in the wizard.

3)Rename the configuration.

4)Open a software configuration.

5)Assign program "LampTest".

Program "LampTest" was already created using the "Getting Started" section.

6.1Testing in simulation mode

Automation Studio provides extensive simulation options for the con-

troller, HMI application, drive controller and motors. In essence, all com-

ponents of an integrated automation solution from B&R can be simulated.

If it is not possible or desirable to operate the actual motor on the ma-

chine, it can be simulated instead. Movement profiles can be carried out

on the controller or PC, even if the entire drive system is not available.

The platform-independent Automation Runtime system allows control

programs to be created and tested directly on the PC. This function is also

available for the safety application. Control applications can be executed

in slow motion or time lapse in order to hone in on different phases of the

machine's lifecycle.

Integrated VNC and web server functionality makes it possible to operate

HMI applications not just remotely, but also directly on the PC.

The integrated WinIO interface makes it possible to fully simulate I/O

points.

Figure 32: Complete simulation at every level

Simulation of a controller can be started by selecting the simulation icon in

Automation Studio. All control programs run directly on the PC. This means

that all of the software functions in the control application can be configured

and tested independently of the hardware.

Figure 33: Activating CPU simulation via the

Automation Studio toolbar

When you switch to simulation mode, the project is rebuilt, the simulation environment is automatically started and

an online connection to Automation Runtime Simulation is established.

The Automation Studio status bar indicates when a CPU simulation is running.

Figure 34: Automation Studio status bar – Simulation running

Simulation \ CPU simulation

## Page 25

CONFIGURE THE HARDWARE25

Exercise: Activate a simulation of the control system

Automation Studio offers comprehensive simulation functions for hardware and software. The goal of this task is to

put the active configuration into simulation mode and test it.

1)Activate the simulation mode via the toolbar.

2)Transfer the configuration.

Further information:

7.2.1 "Online installation" on page 33

°

7.2.2 "Offline installation" on page 38

°

3)Test program "LampTest".

After the configuration has been transferred, all the diagnostics tools become available. Program se-1

quences can be tested and further developed without having to connect to the machine. To transfer a

project to the real controller, simulation mode is ended via the menu. It is possible to check whether the

configuration is running in simulation mode using the simulation icon in the status bar.

Figure 35: Automation Studio status bar - Simulation mode active

Figure 36: Automation Studio status bar - Connection to the real hardware

6.2Adding I/O modules from the Hardware Catalog

When using an X20 system, I/O modules can be added directly to the X2X+ Link interface.

I/O modules can be added either in the Physical View or in System Designer. I/O modules can be dragged and dropped

from the Hardware Catalog to the desired position.

Figure 37: Assigning an I/O module via drag-and-drop

How are modules suggested for selection in System Designer?

Selecting an interface (e.g. X2X+, POWERLINK) in the control system filters the Hardware Catalog to show

only the modules that match this interface. The results list can be filtered by setting further filter cate-

gories.

It is also possible to filter the results by entering parts of the model ID in the search field.

1Further information about the topic of diagnostics can be found in training module "TM223 - Automation Studio Diagnostics" and in Automation Help.

## Page 26

26WORKING WITH AUTOMATION STUDIO TM210

Exercise: Add a digital input and output module

In this task, two X20 modules will be added in the Physical View using the "Getting started" section of Automation

Help as a reference. A digital input module and a digital output module (corresponding to the modules inserted in the

target system) should be added in each case.

Example for ETAL210: X20DI6371 and X20DO6322

1)Open Automation Help:

Getting started \ Creating programs with Automation Studio \ Assigning variables to I/Os

2)Select the X2X+ Link interface in the Physical View.

3)Drag and drop the inserted modules to the X2X+ Link interface.

Communication \ X2X Link

Communication \ POWERLINK

6.3Mapping process variables to I/O channels

I/O mapping is another way of referring to the assignment of variables being used in the control program to a hardware

module's I/O channels.

Variables are allocated to an I/O channel by selecting "Open" / "I/O Mapping" in the I/O mapping editor that is opened

using the shortcut menu in the relevant module, or by double-clicking on the relevant I/O module.

Figure 38: Open the I/O mapping for a module

Exercise: Assign process variables to I/O channels

In this task, two variables will be mapped (one to a digital input channel and one to a digital output channel) using the

"Getting started" section in Automation Help.

1)Open Automation Help:

Getting started \ Creating programs with Automation Studio \ Assigning variables to I/Os

2)Assign variable "Lamp" to a digital output channel.

Programming \ I/O handling \ I/O mapping

## Page 27

CONFIGURE THE HARDWARE27

6.4Editing an I/O configuration

All B&R I/O modules have configuration options. The I/O configuration can be used to configure I/O modules without

having to do any programming.

Selecting one or more I/O modules or interfaces in the Physical View or in System Designer updates the Properties

window located at the bottom right of the Automation Studio window. The Properties window shows all of the con-

figuration options that can be applied to the selected components. Individual property categories can be opened di-

rectly from workbooks.

Figure 39: The Properties window is updated immediately when you select one or more modules.

The I/O configuration can only be opened by selecting "Configuration" in the respective I/O module's shortcut menu

or via "Open" \ "Configuration" in the main menu.

Figure 40: Opening a module's I/O configuration

How can the technical data of an I/O module be found quickly?

The data sheet for the selected I/O module can be accessed directly from the Physical View. To do this,

select the I/O module in the Physical View. Press the to open Automation Help. The data sheet<F1 key>

for the selected module appears. The data sheet contains technical information, configuration options,

connection examples and details of the LED status indicators.

Programming \ I/O handling

Hardware \ X20 System \ X20 module

6.5Configure the network interface of the controller

Automation Studio requires a network connection in order to communicate with the controller. PC and controller must

therefore be on the same subnet.

The network properties can be opened using the shortcut menu for the desired Ethernet interface of the controller. It

is then possible to configure the device parameters for the network interface.

## Page 28

28WORKING WITH AUTOMATION STUDIO TM210

Figure 41: Opening and editing a controller's network settings

The network protocol SNMP (Simple Network Management Protocol) is enabled in order to find the controller using

the "Search for target systems" dialog box even if the INA configuration is invalid. If this protocol is enabled, Ethernet

parameters can also be changed temporarily.

How are devices integrated into an existing network?

Please contact your network administrator for information about the integration of devices into an ex-

isting network.

Exercise: Configure the network interface for the controller and PC

Configure the network interfaces on the PC and controller using Automation Help as a guide.

Getting Started \ Creating programs in Automation Studio \ First project for a target system with inte-

grated flash memory\

Ethernet settings on the workstation

•

Ethernet settings on the target system

•

Communication \ Ethernet \ AR configuration \ Ethernet interface configuration

6.6Compiling the project

Once a program has been completed, it must first be compiled before it can be transferred to the target system.

Compiling the configuration

When compiling the project, all of the changes made since the last build are added.

This process is carried out using the toolbar or by pressing the .<F7 key>

A successful build is indicated in the output window.

Figure 43: Dialog box indicating a

successfully compiled configuration

Figure 42: Output window: 0 errors, 0 warnings

Rebuilding the configuration

When you rebuild a configuration by pressing , all the software objects in the active configuration are<CTRL> + <F7>

created again, even if their source files have not changed since the last time they were built.

## Page 29

CONFIGURE THE HARDWARE 29
Programming \ Build & transfer \ Creating a project

## Page 30

30WORKING WITH AUTOMATION STUDIO TM210

7Initial installation of the controller

To commission the controller, it is necessary to transfer Automation Runtime (operating system), the system compo-

nents and the application project. Depending on the target system, an ,  or a online installationoffline installationUSB

is performed.installation

Systems with integrated flash memory are delivered with preinstalled default Automation Runtime. This guarantees

that a connection with Automation Studio is established and an online installation can be performed.

Offline installation is available for systems with CompactFlash and CFast cards.

Alternatively, USB installation is possible for any system.

Online installation

After a connection has been established, the Automation Runtime version

configured in the project is installed in operating mode "BOOT". Subsequent-

ly, the Automation Studio project will be completely transferred.

The following steps are necessary:

7.1.1 "Connection via target system search" on page 31

•

7.2.1 "Online installation" on page 33

•

Installation via data storage medium

For offline installation or to create a project installation package for USB in-

stallation, Automation Runtime, the system components and the application

project are copied to the CompactFlash/CFast card or a transfer module is

copied to a USB flash drive.

The following steps are necessary:

7.2.2 "Offline installation" on page 38 or

•

7.2.3 "Project installation package USB" on page 38

•

Programming \ Build & transfer \ Establishing a connection to the target system \ Ethernet connection

Project management \ Project installation

7.1Establishing a connection to the target system

How to configure the network interface settings for the network controller

has already been explained. These settings only have to be configured once

before a connection can be made.

There are 2 ways to establish a connection:

7.1.1 "Connection via target system search" on page 31

•

7.1.2 "Establishing a connection via manual configuration" on page 32

•

The configuration dialog box for online connection is opened by selecting

"Online" / "Settings" from the main menu.

Figure 44: Menu option "Online"

Programming \ Build & transfer \ Establishing a connection to the target system

Communication \ Online communication

Communication \ Ethernet \ FAQ

## Page 31

INITIAL INSTALLATION OF THE CONTROLLER31

7.1.1Connection via target system search

Clicking on icon "Browse" activates the search function for the network. This will open up a second pane within the

main window. The results of the network search will be displayed a few seconds later. If the CPU could be identified on

the network, it will be shown in result list. The connection is then established by right-clicking on the CPU and selecting

"Connect" from its shortcut menu.

Figure 45: Browsing the network and connecting to the controller

1)Browse the network via toolbar "Browse" in the toolbar. Results will appear in the right pane.

2)Select "Connect'" from the shortcut menu of the connection.

3)The connection can be dragged from the result list to the left pane.

Temporarily change IP parameters

If the network settings of the controller and PC don't

match, the entry is highlighted in red in the search results.

IP parameters can be changed temporarily from an en-

try's shortcut menu. The temporarily changed settings

can be added directly to the project configuration. If the

changes are not transferred, the original settings will be

used again after the controller is restarted.

Figure 46: Temporary IP configuration

Programming \ Build & transfer \ Establishing a connection to the target system \ Ethernet connection

Browsing for target systems

•

Browsing for target systems \ Changing the IP configuration

•

Exercise: Connect to the controller

In this task, the participants will establish a connection to the controller. The participants will identify the controller

on the network by browsing for target systems and then establish the online connection.

## Page 32

32WORKING WITH AUTOMATION STUDIO TM210

1)Open the online settings.

2)Enable function "Browse for target systems".

3)Identify the target system in the result list.

Target systems can be distinguished by the MAC address, node numbers, hostnames and serial numbers.

4)Establish the connection.

7.1.2Establishing a connection via manual configuration

Manual configuration

If it is not possible to browse the network, for example if SNMP broadcasts are blocked, it is possible to set up an

online connection manually.

Figure 47: Adding and configuring a connection manually.

1)Add a new connection configuration using via the toolbar.

2)Specify the connection parameters (IP address, node number).

3)Enable the online connection from the shortcut menu of the new connection configuration.

Programming \ Build & transfer \ Establishing a connection to the target system \ Ethernet connection

As soon as a connection is established between the PC and the controller, its status will be shown in the

Automation Studio status bar.

In operating mode BOOT, Automation Runtime can then be transferred to the controller.

•

Figure 48: An online connection to the controller has been established. The controller is in BOOT mode.

In operating mode RUN or SERVICE, the entire project can be transfered to the controller. Automa-

•

tion Studio diagnostics tools are available when there is an active online connection.

Figure 49: An online connection to the controller has been established. The controller is in RUN mode.

7.2Project installation

When the project is compiled, a transfer module is created. This can then be transferred online, installed offline via

a CompactFlash or CFast card or loaded onto the target system via a USB flash drive. Then, the transfer module is

installed on the target system.

## Page 33

INITIAL INSTALLATION OF THE CONTROLLER33

Figure 50: Project installation process

There are several ways to perform the project installation.

The various transfer methods are found under "Project" /

"Project installation".

7.2.1 "Online installation" on page 33

•

7.2.2 "Offline installation" on page 38

•

7.2.3 "Project installation package USB" on page

•

38

Figure 51: Opening the project installation

Can the toolbar be edited?

The entries in the Automation Studio menu can added individually to the Automation Studio toolbar. The

toolbar can be adjusted under "Toolbar options" \ "Add or remove button".

Project management \ Project installation

Overview

•

Scenarios

•

Perform project installation

•

7.2.1Online installation

If the project is compiled and Automation Runtime in-

stalled on the target system, then the next step is to

transfer the project.

Clicking on "Transfer to target" in the project installation

menu starts the process. If need be, the latest changes are

compiled in the project.

Figure 52: Online installation procedure

## Page 34

34WORKING WITH AUTOMATION STUDIO TM210

Initial transfer

An initial transfer is performed the first time a project is installed. During this, the target system is completely initial-

ized. All data from any previous projects is removed. With an initial transfer, the entire project including Automation

Runtime is always transferred to the target system. The memory is partitioned, formatted and the target system is

restarted. RETAIN and permanent variables are deleted during an initial transfer.

An initial transfer is performed under the following conditions:

The configuration ID of the target system is different than the one defined in the project.

•

The configuration ID is established in the system configuration of the controller.

The partitioning of the target system does not match the partitioning required for the project.

•

Transfer to a data storage device, see 7.2.2 "Offline installation" on page 38.

•

The user forces an initial transfer in the transfer settings.

•

Figure 54: Note: The memory is formatted during an initial

transfer

Figure 53: Transfer dialog box: Initial transfer forced

Update transfer

Automation Studio will generally first attempt an update transfer on the target system. If performing an update trans-

fer is not possible (the configuration ID has changed, for example), then an initial transfer is performed instead. During

an update transfer, only data that has been changed is transferred to the target system. Depending on the transfer

settings, the Init and Exit programs are executed or values are received from process variables.

Figure 56: Transfer dialog box: No differences were identified between

Figure 55: Transfer dialog box: Differences require a restart.

the project and the target system.

## Page 35

INITIAL INSTALLATION OF THE CONTROLLER35

What do the icons in the transfer dialog box mean?

In addition to the textual notes, notification icons are shown in the transfer dialog box. These can be

used to open tooltips with detailed information about the differences between the project and the target

system configuration.

Figure 57: Additional information from notification icons in the transfer dialog box

Project management \ Project installation

Perform project installation \ Transfer to target

•

Scenarios \ Online commissioning

•

Scenarios \ Online update

•

Glossary

•

## Page 36

36WORKING WITH AUTOMATION STUDIO TM210

How do I enable Monitor mode?

1)In the Configuration View under Connectivity \ OpcUaCs \ UaConfic.uacfg, enable the "OPC UA

Client/Server" setting.

2)In the Configuration View under AccessAndSecurity \ UserRoleSystem \ User.user, create a new

user and set a password. Then assign a predefined role to the user.

## Page 37

INITIAL INSTALLATION OF THE CONTROLLER37

3)Enable Monitor mode (CTRL + M) and log in with the created user.

Exercise: Perform online project installation

After successful commissioning, the project must be installed on the target system. Any future changes to the project

will be loaded to the target system via the online installation.

1)Compile the project

2)Start the online installation.

3)Follow the instructions in the transfer dialog box.

4)Confirm the online installation.

5)Wait for transfer and optional restarts.

The target system is now up-to-date. The function of the program "LampTest" can now be tested directly

on the target system.

Exercise: Test the program

Program "LampTest" can now be tested on the target system. Setting digital input "Switch" should set output "Lamp".

Check the function in Automation Studio and also on the LED status indicators of the assigned I/O modules.

1)Enable the monitor mode.

Figure 58: Ladder Diagram program "LampTest" in monitor mode with Powerflow

2)Set input "Switch" and observe output "Lamp".

3)Compare the status of the LED status indicators with that of the Automation Studio.

## Page 38

38WORKING WITH AUTOMATION STUDIO TM210

7.2.2Offline installation

For offline installation, the project is installed on a data

storage device. The transfer dialog box for doing this is

slightly different from the one for online installation. In

the transfer dialog box, a storage device is selected for

the transfer.

The dialog box for offline installation is opened by select-

ing "Project" / "Project installation".

Figure 59: Offline installation procedure

Offline installation on CompactFlash or CFast card

An offline installation is performed on a CompactFlash or

CFast card to commission the target system. This process

carries out partitioning, transfers the operating system

and the necessary system settings.

Once the CompactFlash or CFast card has been inserted

into the target system and the supply voltage has been

switched on, the controller starts the installed project.

Figure 60: Dialog box for offline installation on a CompactFlash or CFast

card

Automation Runtime simulation

Offline installation can be used to generate an installation structure for Automation Runtime Simulation (ARsim). Dur-

ing this process, the Automation Runtime files, project configuration and programs are copied to a local folder. The

Automation Runtime Simulation is then started automatically.

Project management \ Project installation

Scenarios \ Offline commissioning

•

Perform project installation \ Offline installation

•

7.2.3Project installation package USB

Automation Runtime and the application software can be

included in an offline installation package and transferred

to the target system on a USB flash drive, CompactFlash

card or via a DHCP server.

The following section deals with project installation via a

USB flash drive.

Figure 61: Procedure for generating project installation package

Description of steps:

1)Compile the Automation Studio project.

2)Generate the project installation package

3)Copy the project installation package to the USB flash drive.

4)Connect the USB flash drive to the target system.

During the next startup, the versions of Automation Runtime and the application software are checked and updated

if necessary. Alternatively, the project installation package can be installed at runtime using library "ArProject".

## Page 39

INITIAL INSTALLATION OF THE CONTROLLER39

Project management \ Project installation \ Overview \ Transfer

Enable USB installation

In order for the control system to continue to support this function after the first update via USB installation, you2

must ensure that this function is enabled in the system configuration.

Figure 62: Enabling USB installation in system properties

The configuration is opened in Physical View via the controller's shortcut menu. The required configuration entry is

stored under "System".

Generate the project installation package

The project installation package is generated via "Project"

\ "Project installation" \ "Generate project installation

package".

A USB flash drive is selected in the dialog box. Numer-

ous installation settings make it possible to select exactly

how to update the target system.

The system can now be updated using the configured USB

flash drive.

Figure 63: Generating project installation package on USB flash drive

Are there any other setting options?

The installation settings are used to configure target system identification, installation restrictions and

handling of user data. The installation settings are opened by clicking on the toothed gear symbol on

the right.

Project management \ Project installation

Project management \ Project installationInstalling the project

•

Scenarios

•

Performing a project installation \ Settings

•

Programming \ Libraries \ Configuration, system information, runtime control \ ArProject

2This applies to control systems using the default Automation Runtime.

## Page 40

40WORKING WITH AUTOMATION STUDIO TM210

7.2.4Runtime Utility Center export

The Runtime Utility Center is a system tool that provides a range of utilities for diagnostics and service on B&R con-

trollers. The installation program for the Runtime Utility Center is included in the Automation Studio installation or

can be downloaded separately from the B&R website.

Figure 64: Runtime Utility Center start page

The most important functions are:

Performing service functions via an online connection to the controller

•

Variable functions for backing up and restoring process variables

•

Creating individual Instruction Lists for testing and installation procedures

•

Backing up and restoring a CompactFlash/CFast card

•

Offline installation of a control project on a CompactFlash/CFast card

•

Creating project installation packages for USB installation

•

Custom mode allows the creation of a user-defined user interface

•

How can I open the Runtime Utility Center help documentation?

The Runtime Utility Center contains complete help documentation. This help documentation is opened

by pressing the . The Runtime Utility Center must be opened before doing this. The following<F1 key>

entries provide additional important information about using the Runtime Utility Center.

Diagnostics and service \ Service tool \ Runtime Utility Center \ Home screen

Diagnostics and service \ Service tool \ Runtime Utility Center \ Operation \ Workspace

Diagnostics and service \ Service tools \ Runtime Utility Center \ Operation \ Commands \ Establish

connection, wait for reconnection

Diagnostics and service \ Service tools \ Runtime Utility Center \ Operation \ Commands \ PLC infor-

mation

## Page 41

INITIAL INSTALLATION OF THE CONTROLLER41

Downloading the Runtime Utility Center

The Runtime Utility Center is part of the  and can be downloaded from the B&R website:PVI development setup

www.br-automation.com  Downloads  Product group: Software  Software: Automation NET/PVI →→→→

"PVI Development Setup".

Installing the Runtime Utility Center

The downloaded installation package must be extracted before installation. The installation program can then be

started. No changes have to be made during the installation for use of the Runtime Utility Center.

Create Runtime Utility Center export

The Runtime Utility Center export is started from the Project menu in Automa-

tion Studio. After the destination folder is selected and confirmed, the nec-

essary data is exported as a ZIP file.

The export file can then be processed with the Runtime Utility Center.

Figure 65: Runtime Utility Center (RUC) export in

Automation Studio

Project management \ Project installation \ Installing the project \ Exporting RUC

Loading Runtime Utility Center export package

Select "" to load the Runtime Utility Center export package. Then, the following functions areOpen project (.zip, .pil)

available:

Figure 66: Runtime Utility Center start page with export file already loaded, see header of window

Performing offline installation

•

This function can be used to perform an initial transfer to a CompactFlash/CFast card.

Creating project installation package

•

This function can be used to create a project installation package, e.g. for USB installation.

Diagnostics and service \ Service tool \ Runtime Utility Center \ Creating a list / data storage medium

\ Creating an installation package

## Page 42

42 WORKING WITH AUTOMATION STUDIO TM210
8 programming
A program is a POU (program organization unit) defined in the IEC 61131 standard that possesses the ability to directly
access all global variables, functions and function blocks.
8.1 Programming languages
A variety of different programming languages are available in Automation Studio for creating programs.
It is also possible to combine multiple programming languages within a single project.
Programming languages available in Automation Studio:
Programming language IEC 61131 Description Reference
Ladder Diagram (LD) Yes Visual TM240
Function Block Diagram (FBD) Yes Visual TM241
Sequential Function Chart (SFC) Yes Mixed TM242
Structured Text (ST) Yes Text-based TM246
Instruction List (IL) Yes Text-based Automation Help
Continuous Function Chart (CFC) No Visual Automation Help
ANSI C and C++ No Text-based Automation Help
Table 2: Overview of programming languages
All text-based programming languages in Automation Studio use the same editor. As a result, diagnostics tools always
show the same behavior and are always operated in the same way. This high degree of uniformity simplifies workflows
and increases productivity.
Are there restrictions to B&R standard libraries?
Function blocks included in B&R standard libraries can be called and used in all of the programming lan-
guages.
Programming\ Programs
8.2 Initialization and cyclic program section
When a program is added using the toolbox, the cyclic program sections, the initialization subroutine and the exit
program are automatically added. Program sections that are not needed can be deleted in the Logical View. If the
program is to be executed cyclically, it must have a cyclic program.
Further information:
Training module "TM213 - Automation Runtime"
•

## Page 43

PROGRAMMING43

Figure 67: Image of a newly added program in the Logical View - initialization subroutine, cyclic program and exit program

Task initialization

When the cyclic system is started, each task executes its initialization subroutine. This initialization subroutine can

already contain program code that calculates and describes variable values, for example.

Cyclic subroutine

The program's cyclic subroutine starts once the task's initialization subroutine has completed. Variables that are de-

scribed there retain their values until they are overwritten or until the system is restarted.

Exit subroutine

A task's exit subroutine is only called when the task is uninstalled (deleted). If resources (e.g. memory, interfaces, etc.)

were requested in the initialization or cyclic subroutine, then these resources must be freed up properly.

Automation Runtime \ Operation \ Runtime behavior \ Tasks

8.3Variables and data types

serve as storage for values. Variables are givenVariables

a name and are managed by the operating system in the

controller's memory. In Automation Studio, variables are

declared in files with the extension .var.

Figure 68: Declaring variables

## Page 44

44WORKING WITH AUTOMATION STUDIO TM210

define the properties of variables. They defineData types

things like the range of values, the precision of the num-

ber stored in the variable or the possible operations.

Figure 69: Variable data type

It is also possible for the user to create user-defined data

that are based on standard data types. In Automa-types

tion Studio, these user-defined data types (or derived da-

ta types) are declared in a file with the extension .typ.

Figure 70: Declaring data types

Programming \ Variables and data types

Variables

•

Data types \ Basic data types

•

Data types \ Derived data types

•

8.3.1Variable scope

The software can be structured in the Logical View using packages. The created structure facilitates the simple en-

capsulation of data and functionality.

The scope and visibility of the declared variables and data types is determined by the structure. This makes it possible

to define variables in the appropriate "logical" place in the project.

Automation Studio controls the visibility of variables using the position of the VAR file.

are located at the highest possible level and are visible(1) Global variables

throughout the entire project. They can therefore be used in any program,

regardless of the hierarchical level of the package containing it.

are declared within a specific package and are(2) Package-global variables

only valid in that package as well as in all subordinate packages and programs.

can be defined with a local reference frame for a program(3) Local variables

and are therefore cannot be used in other POUs of the project.3

Programming \ Variables and data types \ Scope of declara-

tions

Figure 71: Scopes in the Logical View

3Program organization units are referred to in the IEC 61131-3 standard as POUs for short. POUs are the programs, functions and function blocks into which the control project

is divided.

## Page 45

PROGRAMMING45

8.3.2Initializing and buffering variable values

A variable always has a data type as one of its properties. The variable declaration can contain additional properties

for the variable.

Figure 72: Example of a variable declaration

are variables with values that are not permitted to be changed while the program is being executed. AConstants

constant is assigned its initial value when the software is created (Value column).

variables are protected in buffered memory so that they can be reloaded after a system restart (warm restart).Retain

Unlike RETAIN variables, permanent variables are also protected against a cold restart. In order for variables to be

stored in the permanent memory area, they have to be defined as RETAIN and Global in the variable declaration window.

Depending on the target system used, a buffer battery is used in the CPU to retain the data. Detailed information is

listed in the data sheet of the respective device.

Programming \ Variables and data types

Automation Runtime \ Mode of operation \ Module / Data security

Nonvolatile memory \ Shutting down

•

Nonvolatile memory \ Startup behavior

•

8.4Creating an application

Automation Studio includes a number of components that enable efficient and platform-independent project config-

uration.

8.4.1Libraries, samples and solutions

The standard libraries in Automation Studio provide access to many system functions. Physical interfaces can be freely

programmed, reports can be used in a targeted manner and hardware can be configured with a high level of flexibility.

It is also possible to import numerous sample programs that demonstrate how the libraries can be used. These en-

compass executable program codes for the controller or the simulation.

With solutions that are installed via the Automation Studio upgrade dialog box, you receive complete project templates

with process control, simulation and visualization. These are imported into the project by the user and adjusted as

required.

Programming

Libraries

•

Examples

•

Solutions \ Technology Solutions

## Page 46

46WORKING WITH AUTOMATION STUDIO TM210

8.4.2mapp Technology

With mapp Technology, we offer an easy-to-use interface and comprehen-4

sive functionality.

Many complex operations, such as loading and saving recipe data, controlling

a drive axis and recording process values, can be implemented quickly and

easily using mapp Technology components.

Figure 73: mapp Technology logo

mapp Technology unites configuration and programming. The functionality itself is implemented in the application

program using standard libraries. In addition, mapp provides configuration interfaces. Similar to hardware modules,

these are used to configure the functionality of the mapp components without programming.

User access to mapp Technology is enabled via independent Technology Packages. They are loaded via the B&R website

or the Upgrades dialog box in Automation Studio.

mapp Technology \ Concept

Project management \ Workspace \ Upgrades

8.5Import, export and team functions

Teamwork requires the division of responsibility and tasks.

Automation Studio supports functions designed to help teams work more efficiently:

Passing on project data via export with small file sizes

•

Applying project data via import

•

Working with version control systems

•

Project management

Automation Studio project \ Importing/Exporting projects

•

Distributed development

•

Using version control systems

•

8.5.1Exporting projects

An export function allows an Automation Studio project

or parts of a project to be shared with other program-

mers.

Projects can be exported by selecting "File" / "Save

project as ZIP" or "File" / "Save project as ZIP without up-

grades" from the main menu.

Additional functions used to export or import hardware

modules for ECAD systems are available.

Figure 74: Exporting software components from the Logical View

4mapp Technology stands for "Modular application technology"

## Page 47

PROGRAMMING47

Project management \

Automation Studio project \ Importing/Exporting projects

•

Hardware management \ ECAD import/export

•

8.5.2Exporting and importing software components

Completed software components can be exported and imported so that they can be shared or reused in other projects.

Exports are performed individually for each package in the Logical View and include any dependencies to libraries.

These are checked during import and automatically resolved.

Preprepared samples are available in Automation Studio

as packages for B&R standard libraries. These can be im-

ported into an existing project as needed.

Importing to the Logical View is carried out using the tool-

box.

Figure 75: Adding samples from the toolbox

Programming \ Examples

Project management \ Automation Studio project \ Importing/Exporting projects

## Page 48

48WORKING WITH AUTOMATION STUDIO TM210

9Updates and licenses

In addition to opening System Diagnostics Manager, Automation Studio's "Tools" menu offers other functions as well.

This section briefly explains Technology Guarding and the function of dialog box "Upgrade".

9.1Hardware and software upgrade

The ability to upgrade components makes it possible to update hardware, libraries, Automation Runtime versions and

Technology Packages.

In order to download components from the Internet, it is necessary to open Automation Studio.

The upgrade process is started by selecting "Tools" \ "Upgrades". A dialog box opens, showing available upgrades.

Once upgrades have been selected, they are downloaded in the background and installed automatically.

Figure 76: Selecting subsequently loadable components in Automation Studio

Project management \ Workspace \ Upgrades

## Page 49

UPDATES AND LICENSES49

9.2Technology Guarding

Technology Guarding is used to license protected software. This technolo-

gy protects against unauthorized duplication of machine software and facil-

itates implementation of machine options. Licenses are stored in encrypted

form to prevent tampering. Licenses can come preinstalled on a Technolo-

gy Guard dongle from B&R or downloaded in the field using the Technology

Guarding function in Automation Studio. The Technology Guarding portal on

the B&R website provides full transparency of available and already activated

licenses.

The Technology Guard provides two manipulation-proof operating hours

counters and permanent data storage. These functions can be used via library

"AsGuard" in the application software.

Figure 77: Technology Guard for license

protection on the machine

Licenses on the inserted Technology Guard are verified automatically by Automation Runtime. The Technology Guard

is used on the controller, in the simulation and as a license memory for Automation Studio single licenses.

Automation software \ Technology Guarding

Programming \ Libraries \ Configuration, system information, runtime control \ AsGuard

## Page 50

50WORKING WITH AUTOMATION STUDIO TM210

10Summary

Automation Studio is more than just a programming tool. It provides support for the user throughout the entire life-

cycle of a machine – from initial testing to finished project.

Structuring software via machine functions or working with different configurations makes it possible to manage

machine variants in a project and facilitates teamwork.

Figure 78: Automation Studio: One engineering tool for the machine's entire lifecycle

Automation Studio is an invaluable resource for programmers, service technicians and maintenance engineers for

every stage of a machine's lifecycle.

## Page 51

AUTOMATION ACADEMY51

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

V4.0.0.3 ©2025/09/16 by B&R, All rights reserved.

All registered trademarks are the property of their respective owners.

Subject to technical changes without notice.