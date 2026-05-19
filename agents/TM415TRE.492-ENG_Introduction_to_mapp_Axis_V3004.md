## Page 1

TM415

Introduction to mapp

Axis

## Page 2

2 INTRODUCTION TO MAPP AXIS TM415
Requirements
Training modules TM210 – Working with Automation Studio
TM400 – Introduction to motion control
Software Automation Studio 4.9
Automation Runtime 4.91 and later
mapp Motion Technology Package
Hardware X20 controller
ACOPOS devices / X20 stepper motor module

## Page 3

TABLE OF CONTENTS 3
Table of contents
1 Introduction.........................................................................................................................................................4
1.1 Learning objectives..............................................................................................................................4
1.2 Symbols and safety notices...............................................................................................................4
1.3 B&R online courses...............................................................................................................................5
2 The hardware......................................................................................................................................................6
3 The software.......................................................................................................................................................8
3.1 Drive parameters and drive communication..................................................................................8
3.2 Software concept and software interfaces...................................................................................9
4 My first project.................................................................................................................................................11
4.1 Configuring the single axis...............................................................................................................11
4.2 Preparing a drive and performing movements...........................................................................16
5 Components of the motion control system..............................................................................................18
5.1 Configuration modules.....................................................................................................................18
5.2 Configuration management.............................................................................................................21
6 Commissioning and diagnostics..................................................................................................................25
6.1 Commands...........................................................................................................................................26
6.2 Drive parameters and homing........................................................................................................28
6.3 Diagnostics.........................................................................................................................................30
6.4 Determine the controller settings using autotuning.................................................................35
6.5 Commissioning checklist.................................................................................................................36
6.6 Speed-torque characteristic curve................................................................................................38
7 Further drive functions...................................................................................................................................40
8 Simulation options..........................................................................................................................................41
8.1 Simulation of controller and drive..................................................................................................41
9 Summary............................................................................................................................................................44

## Page 4

4INTRODUCTION TO MAPP AXIS TM415

1Introduction

The B&R drive solution is . This enables consistent configuration and commis-fully integrated into Automation Studio

sioning of all drive components.

This training module describes the steps in Automation Studio to develop and implement a motion control solution.

Figure 1: The integrated automation solution from B&R

During this training course, participants will take a closer look at the role played by the various elements of a motion

control solution. The diagnostic tools included in Automation Studio provide an ideal environment for efficient testing

and commissioning. The exercises in this training module are designed to help participants become familiar with the

fundamentals of working with integrated motion control.  Automation Help provides support during configuration

and detailed descriptions of all elements.

1.1Learning objectives

Participants will get an overview of B&R motion products.

•

Participants will gain basic knowledge about wiring and troubleshooting drive hardware, displayed on the hard-

•

ware.

Participants will know where to find required information in Automation Help.

•

Participants will be able to add and configure an axis in Automation Studio.

•

Participants will be able to operate and diagnose an axis.

•

Participants will know the necessary parameters required to operate a motor with a B&R drive.

•

Participants will know the concept of regulation and understand how autotuning is carried out. Participants will

•

also be familiar with controller parameters that can be readjusted manually.

Participants will be able to execute various options for simulations for drives in Automation Studio.

•

Participants will categorize important information for a support request at B&R.

•

Participants will know possibilities for displaying axis information without Automation Studio.

•

1.2Symbols and safety notices

Unless otherwise specified, the symbol descriptions and safety notices listed in "TM210 - Working with Automation

Studio" apply.

## Page 5

INTRODUCTION 5
1.3 B&R online courses
The B&R online courses provide lessons for a wide range of topics. Because the courses are
interactive, they allow content to be learned effectively.
To help you find the B&R online courses you need more quickly, the various courses on the
website are assigned to different training categories and are based on the modular training
concept.
B&R online courses https://www.br-automation.com/en/academy/online-courses/)

## Page 6

6INTRODUCTION TO MAPP AXIS TM415

2The hardware

At the beginning, it is important to deal with the hardware that is used. Which connections does the device have? How

should it be wired?

Hardware information sources

Product-related information are available in the  and in .user's manualAutomation Help

The user's manual is available on the B&R website in the product area. A device can be quickly found by entering its

serial number (see nameplate) in the search field on the website.

In Automation Help, product-related information is stored in chapter "Hardware".

Further information:

www.br-automation.com

•

Wiring

There is an overview for each B&R hardware that shows all connections and labels. The pinout of the individual con-

nectors are clearly displayed in a separate section.

The hardware includes labels and stickers with clear instructions for wiring the pins. This makes troubleshooting eas-

ier.

Figure 2: The ACOPOS P3 user's manual, technical data, pinout

If a motor is connected to an ACOPOS servo device, the order of the cable ends must be observed. Motor phases U, V

and W must be correctly connected to the ACOPOS terminal.

Failure to adhere to the motor cable pinout may result in incorrect direction of rotation or irreparable damage to motor

components.

## Page 7

THE HARDWARE7

An improperly connected motor may perform uncontrolled movements!

This behavior cannot be compensated by the software!

The wiring must be carried out properly according to the official B&R description!

Figure 3: The ACOPOS P3 user's manual, technical data, motor cable 0.75 mm²

Hardware \ Motion control \ <Device> \ Technical data \ <Device type> \ Wiring

Hardware \ Motion control \ <Device> \ Technical data \ Accessories \ Cables or motor cables

Exercise: Obtaining information about the hardware

The goal of this exercise is to help participants to use Automation Help and the hardware section of the user's manual.

Participants should find the cable diagram and the color coding of the motor cable and the pinout of the device used.

In addition, the hardware used should be checked for wiring errors.

1)Open the B&R website and search for the device used (e.g. ACOPOS P3 user's manual, serial number for related in-

formation)

2)Open the hardware section of the device being used in Automation Help.

3)Find the cable diagram and color coding of the B&R motor cable.

4)Find the pinout of the device used.

## Page 8

8INTRODUCTION TO MAPP AXIS TM415

3The software

This chapter describes the communication between controller and drive and the configuration of the various drive

parameters. It also explains which interface the motion control application and diagnostic tools use to communicate

with a drive axis.

Figure 4: A motion control system as seen in System Designer. Data exchange between the controller and ACOPOS

servo drive via POWERLINK. The ACOPOS servo drive is connected to the motor via a motor cable and an encoder cable.

The System Designer image shows the components required for a drive configuration.

The X20 controller is connected to the ACOPOS servo drive via POWERLINK. The ACOPOS servo drive is connected to

the motor using a motor cable and an encoder cable.

The motor cable transfers the power to the motor. The encoder cable is used for position feedback to the ACOPOS

servo drive.

Configuration in Automation Studio

ProcedureDescription

ConfigurationThe hardware for the motion control system is configured in Automation Studio. This in-

volves, for example, adding an ACOPOS servo drive in the Physical View or in System De-

signer.

ConfigurationThe drive configuration must be adapted to match the actual components being used.

Diagnostic and test-System Diagnostics Manager and mapp Cockpit offer various diagnostic and commis-

ing toolssioning aids. They can be used to start positioning sequences, issue other commands and

record data for detailed analysis, see 6 "Commissioning and diagnostics" on page 25.

SimulationAutomation Studio allows you to simulate motors and servo drives, see 8 "Simulation op-

tions" on page 41.

Software develop-Drive solutions from B&R are implemented with mapp Motion. For more information, see

menttraining modules "TM416 – Motion control: Basic functions" and "TM417 – Motion control:

Multi-axis functions".

Table 1: Overview of configuration options in Automation Studio

3.1Drive parameters and drive communication

Drive parameters

Drive parameters can generally be divided into two general groups.

The first group includes all the parameters required for the . These are device-specific para-hardware configuration

meters required for real motion and real hardware operation.

The second group includes all the parameters required for the . These are generally adjustedpositioning sequences

to the movement and can be homed and processed on any device. The combined structure is referred to as the axis

or axis group reference.

## Page 9

THE SOFTWARE9

Parameters for the hardware configurationParameters for positioning sequences

Motor characteristic valuesSystem of units

Encoder interfacesMotion parameters

Supply voltageSpeed/acceleration values

Controller valuesAxis limit values

Table 2: Example listing of how drive parameters can be grouped

Drive communication

The controller communicates with the ACOPOS servo drive via POWERLINK. The data transmitted by the controller

controls the drive.

Module McAcpDrv, a software object called the ACOPOS driver, provides libraries (e.g. McAxis) with function blocks.

McAcpDrv is used to operate ACOPOS drive hardware and to provide services for cyclic and acyclic communication

with the ACOPOS hardware.

The ACOPOS servo drive provides the motor with setpoint values that are returned via an encoder interface.

The data needed for drive communication can be divided into two general categories.

Drive parameters and commandsStatus data

The drive parameters are transferred from the controllerDevices in the ACOPOS devices use status data to re-

to the devices in the ACOPOS devices.port the state of the drive control and positioning proce-

Positioning commands are transferred to the devices indures back to the controller.

the ACOPOS devices.

Table 3: Differentiating between parameters, commands and status data

The operating system and the parameters needed to configure the drive are transferred to the drive automatically.

This also happens after the connection to a drive is lost or when a drive is replaced by a new device. No other steps

are required to update the drive.

mapp function blocks (MpAxis library) and PLCopen function blocks (McAxis library) serve as programming interfaces

in the drive application.

3.2Software concept and software interfaces

Regardless of which particular drive components are used, axes are all addressed in the same way in the software.

A  is used to manage each axis (later linked to a servo drive, frequency inverter, stepper motor drive orcomponent

hydraulic drive). Several configuration objects in addition to the component are available for expansions. Since it is

independent of the motion control technology used, the same motion control application can always be used to control

an axis.

The corresponding component is used by both the motion control application and the diagnostic environment to

address the corresponding drive axis. Real and virtual axes are available for single-axis applications, couplings via

electronic gearbox and cams.

TM1111 describes path-controlled movements, i.e. when individual axes are interdependent or affect each other (e.g.

robots with linear kinematics, CNC machines).

We will take a closer look at the single-axis concept in the following pages.

Real axis

This component is used to operate a real servo drive with a motor and posi-

tion encoder, for example. The servo drive generates setpoints for the motor,

and motor movement is monitored using the position encoder.

Figure 5: Motor driving a winder drive

## Page 10

10INTRODUCTION TO MAPP AXIS TM415

Virtual axis

In addition to real axes, ACOPOS also offers the option of operating a virtual

drive. This drive works solely as a type of "setpoint generator" that generates

values for position and speed. It is operated in the same way as a real axis. A

real axis can be coupled to the position of a virtual axis.

Figure 6: Virtual axis e.g. in an ACOPOS P3 device

Purely virtual axis

Purely virtual axes are axes without physical characteristics. The position,

speed and acceleration values are calculated on the controller using a set-

point generator (McProfGen).

A purely virtual axis can be integrated in an Automation Studio project in the

Configuration View using configuration file PureVAx.

Programming the purely virtual axis can be done using PLCopen function

blocks from the McAxis library.

Figure 7: Purely virtual axis on the PLC

A purely virtual axis must be switched on like a real axis. It does not have to be homed to start a movement (the position

value is initially set to 0 when the controller starts up).

Purely virtual axes can, for example, be used as:

Coupling master for one or more hardware axes

•

Simulation on the controller without drive hardware

•

Centralized setpoint generation on the controller for various hardware

•

Motion control \ mapp Motion \ Concept \

Motion control \ mapp Motion \ Programming \ Application program \ Libraries \ Core \ McPureVAx

## Page 11

MY FIRST PROJECT 11
4 My first project
This chapter will help participants create their projects step by step – from adding a drive to Automation Studio to
performing the first movements in mapp Cockpit. At the end, they will complete an exercise.
The following steps must be carried out:
1) Create an Automation Studio project with an X20 controller.
2) "Getting started" axis, see 4.1 "Configuring the single axis" on page 11
1) 4.1.1 "Adding configuration file" on page 11
2) 4.1.2 "Adjusting the axis object" on page 12
3) 4.1.3 "Adding ACOPOS servo drive" on page 12
4) 4.1.4 "Performing a hardware-specific configuration" on page 15
5) 4.1.5 "Performing an offline installation using CompactFlash" on page 16
3) "Getting started" mapp Cockpit
Diagnostics and Service \ mapp Cockpit \ Getting started with mapp Cockpit \
Preparing to use mapp Cockpit
•
Accessing the web-based mapp Cockpit HMI application
•
Interacting with mapp components in the web-based HMI application
•
4.1 Configuring the single axis
The following steps can also be performed together with "Getting started" in Automation Help or with the "Getting
started" tutorial.
Motion control \ mapp Motion \ Getting started \ Axis
4.1.1 Adding configuration file
The required configuration files for an axis can be added in the motion section of the Configuration View in Automation
Studio.
By setting the filters in the Toolbox, the required files can be located quickly and efficiently.

## Page 12

12INTRODUCTION TO MAPP AXIS TM415

Figure 8: Adding an axis from the Toolbox by double-clicking (mapp Motion section must be selected beforehand)

4.1.2Adjusting the axis object

When an axis object is added in the Configuration View, it is given a default name. Name "gAxis_1" can be changed

as needed.

Figure 9: Double-clicking on Config_1.axis (in the Configuration View) brings you to the configuration for an axis

Multiple axis objects with different names can be created here. A single axis is needed for this "Getting started" section.

Further information

5.1 "Configuration modules" on page 18

•

4.1.3Adding ACOPOS servo drive

After creating an Automation Studio project with an X20 controller, an ACOPOS servo drive is connected via POWER-

LINK.1

1Other devices from the B&R motion control portfolio are also added to the Physical View via hardware access.

## Page 13

MY FIRST PROJECT13

Codian or Comau robots with ACOPOS drive hardware can be added via the specific robot wizard starting

with Automation Studio 4.10 and mapp Motion 5.16. The required configuration objects and the drive

hardware are automatically added to the project and the robot is fully parameterized. This step can be

skipped! Continue with Structured Text motion commands to create a program.

The hardware can be selected in the Hardware Catalog and then dragged and dropped to the appropriate interface

in the Physical View (or in System Designer).

ACOPOSmicro is used as an example here.

However, thanks to the system's extensive scalability ("Scalability+"), this "Getting started" tutorial can also be applied

to ACOPOS drive, ACOPOSmulti, ACOPOS P3, ACOPOSmotor, etc.

Figure 10: View before hardware is added from the Toolbox

## Page 14

14INTRODUCTION TO MAPP AXIS TM415

Figure 11: Attaching hardware to the POWERLINK interface by double-clicking

After working through the Getting started documentation, the drive is visible in the Physical View and in System De-

signer. It is possible to add or replace encoder interfaces by selecting the slots on the drive.2

After adding the drive in the Physical View, Automation Studio automatically created all the necessary libraries in the

Logical View. For a description on this, see 5 "Components of the motion control system" on page 18.

Embedded parameter chip

The EnDat encoder system includes nonvolatile, maintenance-free data memory to store all of the data required to

operate the drive motor. B&R stores motor data, among other things, so that the correct motor parameters and limit

values are always automatically available to the ACOPOS drive system. This preprogrammed data is automatically

transferred to the servo drive when the system is started.

In addition to assistance during commissioning, routine service work is also simplified, and motors can be replaced

without having to take extra time to set parameters.

Motion control \ mapp Motion \ Concept \ Drive technology \ Components of a drive system \ Position

encoder \ EnDat absolute encoder

Setting the POWERLINK node number

The node number on the ACOPOS must match the node number in Automation Studio.

2Additional encoder options and plug-in cards can be connected to available slots on ACOPOS and ACOPOSmulti systems.

## Page 15

MY FIRST PROJECT15

The devices connected via POWERLINK receive node num-

bers from Automation Studio in ascending order. These

node numbers can be changed in the shortcut menu in the

Physical View or in System Designer.

Figure 12: Changing the node number in the Physical View

4.1.4Performing a hardware-specific configuration

This is where the previously created axis object is assigned to the hardware and the first necessary configuration

settings are made. These settings can be adjusted as needed (e.g. homing method, simulation, limit values, etc.).

Figure 13: Double-clicking the hardware in the Physical View brings you to the hardware-specific configuration. The axis is assigned here.

If axes are connected to actual hardware, you must check the wiring ("Digital inputs"). If a system such as

ETAlight 410 is used, the quick stop function should be disabled on all channels as the reference switch

for homing would otherwise unintentionally trigger this emergency stop function.

The project is prepared and can be generated. After restarting the CPU, you can check in the Logger whether the

system was started correctly.

The program can be tested using the Watch window and 6 "Commissioning and diagnostics" on page 25.

Further information:

5.1 "Configuration modules" on page 18

•

## Page 16

16INTRODUCTION TO MAPP AXIS TM415

4.1.5Performing an offline installation using CompactFlash

The modified hardware configuration can be loaded to the target system us-

ing a CompactFlash card (offline installation) or via the online connection.

When the X20 controller is restarted, all parameters will then be transferred

to the drive. The "R/E" LED lights up solid green on the drive once the transfer

and a restart have taken place.

Further information:

In the respective user's manual

•

Under the "Hardware" section in Automation Help

•

Figure 14: ACOPOS micro servo drive LED status

indicators

Project management \ Hardware management \ Physical View \ Editing operations \ Changing the node

number

Getting started \ Creating programs in Automation Studio \ Example project for a target system with

CompactFlash

4.2Preparing a drive and performing movements

The hardware for the X20 controller and drive was configured in Automation Studio and transferred to the controller.

Now the first motor movement can be started.

Open mapp Cockpit

mapp Cockpit can be used to put any drive axis or axis groups in Automation Studio.

Diagnostics and service \ mapp Cockpit \ Getting started with mapp Cockpit

Preparing the project for use with mapp Cockpit

•

Accessing the web-based mapp Cockpit HMI application

•

Switch on controller AutoTune and perform homing procedure

After opening mapp Cockpit, several commands are available. The corresponding command is selected and executed.

The controller parameters for initial use can be determined using the autotuning procedure. If this step is omitted, a

lag error can occur when starting a movement at high loads or instability can occur at low loads and with small motors.

The sequence should be observed, see see "Determine the controller settings using autotuning" on page 35.

Speed controller (possibly with filter for encoder feedback: "Filter time" or resonances: "Notch")

•

Position controller

•

Testing the controller

•

Feed-forward control (optional)

•

Command "" is used to enable the position control and the motor holds the current position.Power on

Command "" is used to create a reference to the zero point and the system of units. This then makes it possibleHome

to move to a position or move a certain distance.

Switching on and homing only have to take place once; it is then possible to perform any number of movements.

Perform a movement

After preparation (Tune, Power, Home) has been completed, the drive is ready to perform movements. For example,

commands "Move Absolute", "-Additive" and "-Velocity" can be used to rotate the motor clockwise and "Abort" can be

used to stop a movement.

A detailed description of all options in mapp Cockpit is covered later in section, see 6 "Commissioning and diagnostics"

on page 25.

## Page 17

MY FIRST PROJECT 17
Motion control \ mapp Motion \ Diagnostics
Exercise: First movement with "Getting started"
Objectives:
Project creation
•
Configure the ACOPOS servo drive.
•
Opening mapp Cockpit
•
Move the axis.
•
The individual steps are listed at the beginning of this chapter. The exact procedure is described in the subchapters.
In addition, 2 "Getting started" chapters can be consulted.
Motion control \ mapp Motion \ Getting started \ Axis
Diagnostics and Service \ mapp Cockpit \ Getting started with mapp Cockpit
Preparing to use mapp Cockpit
•
Accessing the web-based mapp Cockpit HMI application
•
Interacting with mapp components in the web-based HMI application
•

## Page 18

18INTRODUCTION TO MAPP AXIS TM415

5Components of the motion control

system

Configuration objects are managed and accessed in the same way, regardless of the hardware being used. As a result,

it makes little difference what type of motion control technology is in use.

The image below illustrates where individual configuration modules are stored during runtime, as well as where

changes made to parameter values will have an effect.

Figure 15: Overview of drive configuration components

The part of the PLC with the application (in the image) stretches over not just the programs but also the configuration

objects in the Configuration View as well as the libraries used in the Logical View.

General device parameters are transferred to the ACOPOS product family devices as hardware configuration parame-

ters (e.g. via the shortcut menu in System Designer). In addition, the movement configuration parameters from the

motion function blocks for single-axis control, for example, are transferred at runtime.

Motion control \ mapp Motion \ Concept \ mapp Motion \

mapp Axis

•

mapp Motion components

•

Motion control \ mapp Motion \ Configuration \ Basic components \ Axis

5.1Configuration modules

The modules added for the drive will be described in more detail in the following sections.

5.1.1Axis configuration

The configuration file contains basic parameters for operation and defining the axis reference. The reference is later

used by the software to access the axis component, e.g. for motion control and diagnostics.

The configuration file is accessed from the Configuration View. Double-clicking on the corresponding file with exten-

sion "" opens a window where the initialization parameters (e.g. speed/acceleration values) of the axis compo-.axis

nents can be changed.

## Page 19

COMPONENTS OF THE MOTION CONTROL SYSTEM19

Figure 16: Configuration file of an axis component

The parameters of the axis component are arranged in clearly structured groups. These groups, the configuration

options and the associated elements are described in greater detail in Automation Help.

Motion control \ mapp Motion \ Configuration \ Basic components \ Axis

5.1.2Hardware-specific configuration

The hardware-specific configuration contains parameters that are required for the respective device used. Dou-

ble-clicking on the device takes you to its configuration, as is the case with other B&R devices (e.g. X20 module).

A few examples are listed below, for a complete list see Automation Help or the Automation Studio project.

Axis reference of the axis to be operated (real axis, motor control)

•

Controller settings

•

Ratio of the gearbox

•

Stop reactions

•

Digital inputs

•

Axis features

•

## Page 20

20INTRODUCTION TO MAPP AXIS TM415

Figure 17: Hardware-specific configuration of an ACOPOSmicro

Motion control \ mapp Motion \ Configuration \ Hardware \

## Page 21

COMPONENTS OF THE MOTION CONTROL SYSTEM21

5.1.3Overall configuration

The hardware-specific settings and the axis configuration together are a functioning unit. The parameters for oper-

ation are divided into 2 clearly structured groups: Software object axis and hardware. Together they enable motor

movement.

An overview in Automation Help shows which configu-

ration options are available. An axis only needs an axis

configuration to be put into operation; this configuration

must be assigned to a hardware device later on.

Axes can also be combined to form an axis group so they

can be operated together.

Figure 18: Overview of the mapp Motion axis configuration files

Motion control \ mapp Motion \ Configuration \ Basic components

Motion control \ mapp Motion \ Concept \ mapp Motion \ mapp Motion components \ Axis group

5.2Configuration management

The required parameters are configured at the appropriate places in Automation Studio.

Axis component-relevant parameters are managed in the Configuration View.

Hardware-specific parameters are managed in the hardware configuration of the respective device.

When initializing mapp components after controller startup, all necessary parameters are written to the drive.

5.2.1Axis initialization and configuration

Initialization parameters

A mapp Axis configuration file already contains default values when added. These default values, which configure the

axis component, can be adjusted individually. All changes to the configuration are loaded onto the drive after startup

of the controller.

## Page 22

22INTRODUCTION TO MAPP AXIS TM415

Example rotary table application:

A pivoting carrier must move a product to different stations for processing (specific angular positions

within a full rotation of 360°).

Positioning accuracy must be within 0.1°. MpAxisBasic is used together with command "MoveAbsolute"

to approach the positions.

To make this procedure a little easier, the position is specified in degrees (with one decimal place):

BasicParameters.Distance := 135.0; // Target position 135°

The carrier is driven by a gear (gear ratio= 5:1) using a servo motor.

Figure 19: Sketch of the mechanical structure

In the configuration of the axis and the device-specific configuration, the unit of measurement, measure-

ment resolution, gear ratio and axis period must be specified.

The following configuration serves as a solution for the example:

Figure 20: mapp Motion configuration for the rotary table example (axis and device-specific configuration)

Commissioning this configuration results in the following correlations:

Degrees [°] are defined as the unit of measurement. The accuracy of the resolution is 0.1°. One period

(rotation) equals 360°. The gear ratio was specified as 5:1. A rotation on the gear output equals a rotation

on the rotary table.

The reference distance remains unchanged because it is not taken into account when "Degrees", "Gradi-

ans" or "Revolutions" are used as the measurement unit.

In this case, the following value is used internally:

Degrees: 360

Gradians: 400

Revolutions: 1

## Page 23

COMPONENTS OF THE MOTION CONTROL SYSTEM23

Exercise: Configure the unit system for a spindle drive

A gripper is positioned using a spindle drive connected to a motor via a gearbox. The gear ratio is 5:1 (i=5). For each

motor rotation on the gearbox output, there is a feed rate of 0.6 mm. The positioning should be exact to 0.1 mm. The

position setpoint is entered in mm.*

Determine the settings for unit of measurement, measurement precision, gear ratio and reference distance in the

mapp Motion configuration.

Figure 21: Schematic illustration of a spindle drive

1)Determine and configure suitable units of measurement.

2)Configure the measurement precision.

3)Configure the gear ratio.

4)Configure the reference distance.

5)Configure the limit values and pay attention to the maximum speed of the motor, for example.

6)Configure the software limits in a range from -5 mm to 205 mm.

7)Add MpAxisBasic.

8)Save and transfer the changes.

9)Execute movements and check results.

It should be taken into consideration that the parameters for maximum speed, lag error, acceleration and

software limits should also be changed in the appropriate ratio when changing the axis parameter units.

Entry – Unit for traversing distanceNumber of motor revolutions

0.6 mm5 revolutions

2.4 mm20 revolutions

90.0 mm750 revolutions

Table 4: Correlation between the input value of the traversing distance and the number of motor revolutions

*Entries such as 0.1 mm or 240 mm traversing distance are permitted.

## Page 24

24INTRODUCTION TO MAPP AXIS TM415

Settings:

Base type = Linear bounded

•

Measurement unit = Millimeters

•

Measurement resolution = 0.1

•

Lower limit = -5

•

Upper limit = 205

•

Gearbox input = 5

•

Gearbox output = 1

•

Reference distance = 0.6

•

Figure 22: mapp Motion configuration for the spindle drive (axis and device-specific configuration)

The following correlations result from the settings:

## Page 25

COMMISSIONING AND DIAGNOSTICS25

6Commissioning and diagnostics

Automation Studio and web-based applications both offer several tools for diagnostics.

SDM – System Diagnostics Manager

•

mapp Cockpit

•

B&R Scene Viewer

•

Standalone program that can be downloaded from the B&R website, e.g.

to view complex movements (such as those of a robot system)

6.3.1 "Logger" on page 30

•

6.3.2 "Network command trace" on page 30

•

6.3.3 "Trace" on page 31

•

Figure 23: Commissioning and diagnostics

System Diagnostics Manager

System Diagnostics Manager is accessed via a web browser (preferably Google Chrome) and via the IP address of the

controller. The integrated web server must be enabled in Automation Runtime. In SDM, for example, the Logger can

be read out and the status of the hardware checked.

Possibilities of SDM:

Read-only access to the controller

•

Download network command trace.

•

Download the Logger.

•

Complete diagnostic image with System Dump.

•

Diagnostics and service \ Diagnostics tools \ System Diagnostics Manager

mapp Cockpit

mapp Cockpit is delivered as a Technology Package upgrade. The version of mapp Cockpit must match

the mapp Motion version. The version can be downloaded from the B&R website or via dialog box "Up-

grades" in Automation Studio.

A connection between Automation Studio, controller and drive is required in order to use mapp Cockpit.

mapp Cockpit offers a wide range of functions that are very helpful when commissioning or troubleshooting a drive

axis or axis group.

Functions of mapp Cockpit:

Preparing a drive and issuing commands

•

Accessing and managing drive parameters

•

Viewing drive status values

•

Recording drive parameters and process variables

•

Viewing the command sequence to and status information from the drive

•

## Page 26

26INTRODUCTION TO MAPP AXIS TM415

Figure 24: mapp Cockpit start page.

Start page

The start page is the entrance area of mapp Cockpit. The version used is shown in the lower left corner

with the mapp Cockpit in the background.

Components

Area "Components" provides an overview of the components available on the target system. It is possi-

ble to access the various available views of the components.

Trace

The trace area provides options for configuring and analyzing traces. Existing traces are listed in the

trace overview.

Tools

The Tools area provides access to additional functions that cannot be directly assigned to any compo-

nent.

Login and logout

The Login/Logout area is accessible from any point in the web-based mapp Cockpit HMI application

via its icon. In this area, you can log in and out with the users configured in the user role system. To be

granted write access, your user must be assigned the role with write permission.

Diagnostics and service \ mapp Cockpit

6.1Commands

An axis is controlled via commands.

## Page 27

COMMISSIONING AND DIAGNOSTICS27

Commands for different applications:

Autotuning

•

Preparation

•

Movements and coupling

•

Read and write parameters

•

Autotuning

The controller parameters are determined via integrated autotuning. This means that the ACOPOS servo family device

and the motor are adjusted to the load. Higher load also means higher controller values, which are necessary to be

able to perform a proper movement and to prevent lag errors (difference between setpoint and actual value of the

position) when starting the movement.

Preparation and movement

Before you can move an axis, you must first activate the drive's controller using command "". The axis isPower On

homed using command "". Movements (single axis) or couplings (one axis depending on others) can then beHome

performed.

Read and write parameters

Command "Process ParID" can be used to read individual parameters from the drive or write them to it.

However, it is recommend to always change the configurations within the Configuration View.

Exercise: Read ParIDs

Determine the following drive parameters:

Motor ambient temperature (ParID: 668, TEMP_MOTOR_AMB)

•

DC bus voltage (ParID: 390; UDC_NOMINAL)

•

Actual position (ParID: 111, PCTRL_S_ACT)

•

Current torque (ParID: 277; TORQUE_ACT)

•

Lag error (ParID: 112; PCTRL_LAG_ERROR)

•

1)Search for ParIDs in Automation Help

Motion control \ ACP10/ARNC0 \ Reference manual \ ACP10 \ ACOPOS parameter IDs \ Overview

2)Enter and confirm the ParID in "Read ParID" in mapp Cockpit

The value of the parameters is calculated directly by the drive and displayed in the message area of mapp

Cockpit.

Figure 25: Reading lag errors in mapp Cockpit

Exercise: Work out movement types

The goal of this exercise is to move an axis and independently work out the differences.

1)Open mapp Cockpit

2)Prepare axis (switch on and perform homing)

3)Identify commands for movement types and perform testing

4)Use Automation Help to work out differences for discussion

## Page 28

28 INTRODUCTION TO MAPP AXIS TM415
Exercise: Change speed during movement
The goal of this exercise is to have an axis rotate at different speeds without stopping the active movement.
1) Open mapp Cockpit
2) Prepare axis (switch on and perform homing)
3) Starting the movement
4) Change speed using commands and parameters
Which parameters?
°
What is the override function and how should it be used?
°
6.2 Drive parameters and homing
6.2.1 Drive parameters
The drive parameters are grouped by individual drive functions. If parameters were changed, then they can be applied
and stored in the drive configuration.
See Automation Help for detailed descriptions of the grouped parameters and elements of the data structure.
The drive parameters can be managed both in mapp Cockpit and Automation Studio.
mapp Cockpit Configuration area
•
Automation Studio Hardware-specific drive configuration
•
Configuration file (.axis)
•
Table 5: Management of drive parameters
Diagnostics and service \ mapp Cockpit \ Web-based HMI application \ Components \ Common view
Motion control \ mapp Motion \ Diagnostics \ mapp Cockpit for mapp Motion components
Motion control \ mapp Motion \ Configuration \ Hardware
Exercise: Configure the axis
The goal of this exercise is to define suitable values for operation.
24 V are available in most training situations, which results in performance restrictions. The limits are higher in a real
environment. It is important to adjust the preset default values to the application.
The limit values should be defined in such a way that the lag error is not aborted when the movement starts.
Open testing environment
•
Increase limit values and execute controlled movement
•
Optional: Check using a speed-torque characteristic curve based on the applied voltage of the inverter and mo-
•
tor components.
Save values in the project and transfer them again
•
6.2.2 Homing
The homing mode for the motor can be configured based on the encoder system (e.g. multi-turn/single-turn) or con-
nected sensors (e.g. inductive switch for homing procedure). The corresponding parameters and different homing
mode options are described in Automation Help.
The homing mode can be managed both in mapp Cockpit and in Automation Studio.
mapp Cockpit Command area
•
Automation Studio Hardware-specific drive configuration
•
Table 6: Management of homing modes

## Page 29

COMMISSIONING AND DIAGNOSTICS 29
Motion control \ mapp Motion \ Concept \ Controller \ Homing
Homing methods
The goal of this exercise is to become familiar with the most common homing variants. The homing variants that
execute a movement are used to start a movement via "Start velocity" to search for the switch. After the switch has
been found, the axis moves more slowly and precisely to the configured switching edge via "Homing velocity".
Exercise: Homing with a movement
1) Opening mapp Cockpit
2) Prepare axis (switch on and perform homing)
3) Set required parameters for switch search (either in mapp Cockpit under "InitHome" or in Automation Studio \
ACOPOS configuration \ Drive configuration \ Channel \ Homing)
4) Start homing
5) Training situation: Manually trigger the reference switch (since the torque may not be sufficient with 24 VDC
power supply)
ETA light: Wait until the axis has found the sensor
6) Observe behavior based on help description
7) Complete homing
Example – Switch gate homing:
1) Edit the hardware-specific drive configuration.
Drive configuration \ Channel 2 \ Real axis \ Homing \ Mode = Switch gate
°
Drive configuration \ Channel 2 \ Real axis \ Digital inputs \ Homing switch \ Source = Digital in
°
X2.Trigger 1
2) Edit the mapp Cockpit parameters.
Under "Init home", edit the parameters and execute Init home.
°
Under "Home", edit the parameters and execute Home.
°
Exercise: Homing without a movement
1) Opening mapp Cockpit
2) Set the required parameters for the switch search
3) Follow the circuit diagram described in Automation Help
4) After completion of the homing procedure, set the homing mode
5) Switch off hardware
6) Turn the axis by hand by a quarter turn (if the motor does not have a holding brake)
7) Switch on hardware
8) Perform homing and check whether the position is set correctly.
9) Repeat procedure starting at point 5

## Page 30

30INTRODUCTION TO MAPP AXIS TM415

6.3Diagnostics

6.3.1Logger

In Automation Studio, the drive configuration can be diagnosed via the Logger. The Logger is the central point of

contact for information and sorts error messages and status entries chronologically.

Information from mapp Motion is entered in the "Motion" logbook. This contains information about the system, func-

tion blocks, libraries and errors that have occurred.

The Logger can be saved with an online connection to the controller. It can then be sent by email, for example, to

provide further information about the current application in the event of support requests.

Diagnostics and service \ Diagnostic tools \ Logger

Motion control \ mapp Motion \ Guides \ Diagnostics

6.3.2Network command trace

The NC trace logs commands sent to and from the drive at the network level. Since purely formal commands are record-

ed, the recording is practically independent of the actual protocol. The status of the ACOPOS product is also recorded.

Figure 26: Cutout of a network command trace

An instantaneous recording of the network command trace can be created and loaded from the target system under

"Motion \ "NW command trace" in SDM. The evaluation is performed with Automation Studio.

In mapp Cockpit, the network command trace can be exported in the Tools area. This can then be analyzed in Automa-

tion Studio.

Motion control \ ACP10/ARNC0 \ NC Diagnosis \ NC Trace

Diagnostics and Service \ Diagnostics tools \ System Diagnostics Manager (SDM) \ Accessing System

Diagnostics Manager \ SDM – SVG pages \ SDM page – Motion / NW Command trace

## Page 31

COMMISSIONING AND DIAGNOSTICS 31
6.3.3 Trace
mapp Cockpit contains the trace and live value sections for data acquisition.
With the Trace function, drive parameters on the drive and variables on the PLC can be recorded and displayed simul-
taneously.
The live value area contains a selection of relevant variables of the component. The current values of these variables
are displayed along with their units and trends.
Drive parameters are recorded in real time directly on the drive. The data collected from the ACOPOS servo drive is
loaded into mapp Cockpit via the controller and displayed graphically. A number of different tools such as reference
cursors and measurement cursors can be used for detailed analysis of this data.
The Trace function offers the following options:
Records speed, acceleration and current values
•
Checks the motor load
•
Checks the thermal load on the ACOPOS
•
Checking the positioning processes
•
Synchronous recording of parameters from multiple axes
•
Configure a trace.
The Trace settings are adjusted in the Trace Configuration View. Data points to be recorded, recording duration and
triggering behavior are configured.
Exporting trace data
The trace data obtained can be exported in the trace analysis view. The data is stored as ".csv" on the terminal device.
Diagnostics and service \ mapp Cockpit \ Web-based HMI application \ Trace
Motion control \ ACP10/ARNC0 \ Reference manual \ ACP10 \ ACOPOS parameter IDs \ Overview
Exercise: Trace recording
The goal of this exercise is to become familiar with working with Trace. Some parameters are included and can be
viewed in the mapp Cockpit Trace.
1) Opening mapp Cockpit
2) Configure trace (data points, timing and trigger)
Data points:
Actual position
°
Actual speed
°
Current motor torque
°
Lag error
°
3) Start movement, e.g. additive movement
4) Evaluate trace

## Page 32

32INTRODUCTION TO MAPP AXIS TM415

The figure shows that an additive movement was carried out with 4 revolutions. The speed slowly in-

creases to 500 deg/s because the user has set acceleration to 500 deg/s². The lag error occurs between

-0.004 and 0.09.3

Figure 27: Orange: current torque; green: current lag error; red: current position; blue: current speed

Exercise: Record lag error using Trace function

The goal of this exercise is to abort a motion by exceeding the maximum lag error. The lag error should be recorded

via the Trace function. In addition to the Trace function, other B&R diagnostic devices will also be discussed in detail.

There are several ways to generate a lag error in a training situation. The axis can be blocked by holding it in place, for

example. Furthermore, if the speed or acceleration is too high, this can result in a lag error.4

1)Prepare axis (switch on and perform homing)

2)Configure the Trace function

Data points:

Actual speed

°

Speed setpoint

°

Lag error

°

3)Enable trace recording.

4)Perform positive movement

5)Block axis by hand

6)Check for errors:

Trace in mapp Cockpit

°

EventLog

°

SDM

°

7)Acknowledge axis errors

3Measurement resolution = 0.01

4Example: The speed is increased gradually so that the maximum lag error is exceeded. Right before movement stops, the speed can be measured and saved as the maximum

speed in the Automation Studio project.

## Page 33

COMMISSIONING AND DIAGNOSTICS33

The figure shows that the axis was blocked approx. 2 seconds after the movement was started. The lag

error increases up to the "maximum position error" configured. The movement is aborted and the drive

enters an error state.

Figure 28: Red: lag error; green: speed setpoint; yellow: actual speed

Exercise: Configure the jerk time, record the movement, calculate acceleration

A jerk filter is configured in the hardware configuration to protect the drive mechanics during acceleration and braking

processes.

The goal of this exercise is to compare the results with and without the jerk filter configured.

For Axis 1, a jerk filter is configured; for Axis 2, no jerk filter is configured. The same movement is executed and recorded

on the two axes one after the other. The acceleration is calculated from the recorded speed. Finally, the graphs are

arranged into charts and analyzed.

1)Configuring a jerk filter

For example, Channel 1: Jerk filter – used, Jerk time – e.g. 0.02 seconds

Channel 2: Jerk filter – not used

2)Switching on and homing both axes

3)Configure and enable the Trace function in Automation Studio or mapp Cockpit, e.g.

for the data points *ACP:IF3.ST1_Axis1:114 and *ACP:IF3.ST1_Axis2:114.

4)Perform additive movement for both axes in succession.

5)Calculate acceleration with "Algorithm – Differentiate dy/dt".

6)Arrange diagrams in mapp Cockpit.

Chart 1 with gAxis_1:PCTRL_V_SET and gAxis_2:PCTRL_V_SET, for example

Chart 2, gradient 1 and gradient 2

7)Analyze the recording results.

## Page 34

34INTRODUCTION TO MAPP AXIS TM415

Here you can see the difference in results when a configured jerk filter is used and when no jerk filter

is used.

Green = Jerk filter for Axis 1, 0.024 seconds – Speed

Red = Jerk filter "not used" for Axis 2 – Speed

Blue = Jerk filter for Axis 1, 0.024 seconds – Acceleration

Pink = Jerk filter "not used" for Axis 2 – Acceleration

Figure 29: Comparison of the braking process of a movement with and without a configured jerk filter.

Optional exercise: Troubleshooting a communication error

The goal of this exercise is to compare it to the previous exercise that deals with lag error abort.

As previously, a movement is started and the effects investigated in an error scenario.

1)Start a slow movement.

2)Disconnect the POWERLINK communication connection.

3)Analysis of status LEDs on the device

4)Reconnect POWERLINK communication

5)Analysis of status LEDs on the device

6)Stop movement

7)Check for errors:

mapp Cockpit

°

EventLog

°

SDM

°

## Page 35

COMMISSIONING AND DIAGNOSTICS35

Hardware \ Motion control \ <ACOPOS device> \ Technical data \ (<Servo drive>) \ Status indicators

6.4Determine the controller settings using autotuning

B&R drive software is based on a cascaded control concept. A position setpoint is provided to the position controller

by a setpoint generator that calculates a path profile upon receiving a positioning command. To achieve this position

setpoint, the position controller specifies a speed profile. The task of the speed controller is to maintain the speed

setpoint as closely as possible.

Figure 30: Simplified illustration of the cascaded control concept

The integrated autotuning procedure makes it possible to calculate the control parameters automatically. When cal-

culating the parameters for closed-loop control, it is recommended to start with the speed controller before the posi-

tion controller. The control settings must then be tested before determining the parameters for feed-forward control.

Further information about cascaded control loops:

TM260 – The basics of closed-loop control

•

Motion control \ mapp Motion \ Use cases \ Autotuning

Motion control \ mapp Motion \ Concept \ Motion control \ Closed-loop control concepts

Motion control \ mapp Motion \ Programming \ Application program \ Libraries \ Core \ McAcpAx \

Function blocks

Preparing autotuning

The drive must be operational before autotuning can be carried out. The functionality of the holding brake must then

be checked. It is also required to check the measured direction of rotation and distance of the encoder. If any deviations

are observed, or if another malfunction of the encoder is detected, the encoder must be checked both mechanically

and electrically. The encoder should then be phased. The tuning parameters can now be entered:5

Tuning the speed controller

The speed controller's job is to determine the difference between the manipulated variable of the position controller

(to which it is subordinate) and the measured speed. This calculates a manipulated variable for the subordinate current

controller that works against a deviation in the speed by accelerating.

Selecting autotuning mode "Speed controller" and restarting the tuning procedure using the corresponding com-6

mand will determine the parameters for the speed controller.

Tuning the position controller

The purpose of the position controller is to compare the position provided by the setpoint generator to the actual

position and to generate a manipulated variable for the subordinate speed controller that works against a position

change by changing the speed.

5Phasing is not normally required for B&R motors. It is required, however, if the encoder has been installed at a later time.

6Various filters (e.g. notch filters) are available when tuning the speed controller to stabilize the system.

## Page 36

36INTRODUCTION TO MAPP AXIS TM415

Selecting autotuning mode "Position controller" and restarting the tuning procedure from the command interface will

determine the parameters for the position controller.

This requires that the underlying speed controller is stable.

Testing the controller settings

Before a movement is executed with the new controller parameters, the control loop should be checked for stability.

For this purpose, the system has the option of applying a short disturbance signal to the control loop ("Auto Tune

Test"). If the controller parameters are correct, this disturbance disappears and the drive does not report an error.

Feed-forward components

The purpose of the feed-forward component is to reduce the load on the controller when the speed changes. The values

used by the feed-forward component take the system's moment of inertia into consideration and are determined

during autotuning.

This requires that the underlying speed and position controllers are stable. To do so, the axis is put into

motion and must be homed.

Exercise: Determine the controller parameters using autotuning

Use the autotuning procedure to determine the controller parameters for an axis. To do so, proceed as follows:

1)Open mapp Cockpit

2)Check the holding brake and encoder signal

3)Perform autotuning for the speed controller

4)Perform autotuning for the position controller

5)Perform autotuning for feed-forward control

6)Test controller parameters

7)Save new controller parameters

6.5Commissioning checklist

This section provides step-by-step instructions for commissioning a motion control system. The following points must

be observed:

Safety

It is particularly important to test the safety features. This includes the emergency stop and the limit switches installed

on the machine.

Figure 31: Emergency stop

Figure 32: Limit switch

The ACOPOS user's manual and Automation Help show how to correctly wire the emergency stop and

limit switch inputs.

## Page 37

COMMISSIONING AND DIAGNOSTICS37

Hardware \ Motion control \

ACOPOS P3 \ Safety technology

•

ACOPOS \ Safety technology

•

ACOPOSmulti \ Safety technology

•

SafeMOTION \ Safety technology

•

ACOPOSmicro \ Safety technology

•

Digital inputs

It is important to check whether the servo drive's digital inputs have been

wired according to the configured parameters. mapp Cockpit can also be

used to enter states for the inputs on the ACOPOS servo drive.

Figure 33: Physical location of inputs for the

ACOPOS product family

Units and movement parameters

The following configured units and settings must be checked:

Encoder resolution and units per motor revolution

•

Maximum lag error

•

Software limits

•

Maximum acceleration / speed values

•

Stop functions

•

Direction of motor rotation

•

Jerk filter

Using a jerk filter is recommended to prevent placing an

unnecessary load on the mechanical components. This is

set up using device-specific parameter "Jerk filter".

It is implemented using a moving average filter over mul-

tiple position setpoints and configured in [s].

Figure 34: Effect of a configured jerk filter (green line in graph)

Homing

To ensure that positioning is accurate, a homing procedure must first be performed on the drive. There are a few

different ways to do this.

Motion control \ mapp Motion \ Concept \ Controller \ Homing

Controller settings and autotuning

Controller parameters must be fine-tuned in order to adjust the control loop settings to the exact mechanical require-

ments. Automation Studio offers an integrated autotuning process for determining closed-loop control parameters.

Further information about control loops:

TM260 – The basics of closed-loop control

•

## Page 38

38 INTRODUCTION TO MAPP AXIS TM415
Exercise: The effects of commissioning parameters
Change the following parameters and check their functionality.
Parameters:
Jerk filter
•
Lag error (position error)
•
Direction of rotation of motor (count direction)
•
Homing variant (homing mode)
•
1) Find and change the parameters in the parameter window
2) Save and transfer the changed values
3) Switch on the controller and perform a homing procedure
4) Start the movement and Trace
5) Evaluate the trace data
6) Perform autotuning
7) Start the movement and Trace
8) Evaluate the trace data
9) Work with the different homing modes
Explore the homing modes using Automation Help as a guide. Conclude by performing a homing procedure to a
limit switch.
6.6 Speed-torque characteristic curve
It is helpful to take a closer look at information regarding the current requirements under which the machine is being
operated.
This includes the current voltage on the ACOPOS device's DC bus since this affects the speed-torque characteristic
curve of the motor. This can be viewed either on the B&R website, in Automation Help or in Automation Studio with
dynamic content.
Access in Automation Studio
This requires a motor added to a device in the ACOPOS servo family.
•
Right-click on the motor and select "Speed Torque Chart".
•
Enter the voltage and taking reading using the mouse pointer in the calculated diagram.
•
Access via the B&R website
Open the B&R website, www.br-automation.com.
•
• Products → Motion control → <Type> synchronous motors → Cooling type <A, B> → Size <#> → 8LSA... (motor
type)
Select the drive with a suitable DC bus voltage and make readings using the mouse pointer in the static diagram.
•
Access via Automation Help
Open Automation Help
•
Examples are located in the hardware section under the motors (accessed similar to the B&R website).
•
Hardware \ Motion control \ <Motor> \ Technical data \ <Cooling type> \ Technical data <motor ID> \
Speed-torque characteristic curve
Exercise: Speed-torque characteristic curve of a motor
The goal of this exercise is to view a live speed-torque characteristic curve. This clearly demonstrates the limits of the
24 V used in training situations.

## Page 39

COMMISSIONING AND DIAGNOSTICS 39
A motor added to a device in the ACOPOS servo devices is important.
•
Right-click on the motor and select "Speed Torque Chart".
•
In Automation Studio, the notional voltage value 17 V (24 VDC in the DC bus divided by the root (2) in order to
•
specify the notional, not-yet-rectified current value for the calculation) can be entered.
Analyze the motor's torque curve and determine the reasons for the lag error (deviation between actual position
•
and position setpoint) starting from a certain speed.
For information regarding real-world applications with proscribed nominal voltage, see the B&R website or Au-
•
tomation Help.

## Page 40

40 INTRODUCTION TO MAPP AXIS TM415
7 Further drive functions
There are a large number of auxiliary functions provided by the operating system used by the ACOPOS servo devices.
A few of them are listed here. For more information, see Automation Help.
Motion control \ mapp Motion \ Programming \ Application program \ Modules \ McAcpSys
2-encoder control
2-encoder control if position control should be performed on an external position encoder in an application.
reACTION Technology
Predefined function blocks can be configured remotely on the ACOPOS drive to ensure very fast reaction times or the
collection of high-speed signals at very high processing speeds (400 µs).
This includes logical operations (AND, OR, etc.) as well as arithmetic calculations, comparisons, profile generators, the
use of cams in switching networks and much more.
Encoder phasing
If B&R does not deliver the motor and encoder in a housing, phasing (measuring the encoder zero point in relation to
the generated current phasor in the stator) is required to ensure optimal commutation.
Suppression of periodic disturbances
If impacts or resistance occurs on an axis within a motor revolution or within an interval, it is possible to compensate
for these periodically occurring disturbances.
The goal is higher positioning precision and control quality with respect to the setpoint.
Controlling a B&R stepper motor axis
Motion control \ mapp Motion \ Programming \ Application program \ Libraries \ Core \ McStpAx

## Page 41

SIMULATION OPTIONS41

8Simulation options

Automation Studio provides extensive simulation options for the con-

troller, HMI application, drive controller, motors and even loads on the mo-

tor. In essence, all components of an integrated automation solution from

B&R can be simulated.

If it is not possible or desirable to operate the actual motor on the ma-

chine, it can be simulated instead. Movement profiles can be carried out

on the controller or PC, even if the entire drive system is not available. The

load within defined parameters can also be simulated.

The platform-independent Automation Runtime system allows control

programs to be created and tested directly on the PC. This function is also

available for the safety application. Control applications can be executed

in slow motion or time lapse in order to hone in on different phases of the

machine's lifecycle.

Integrated VNC and web server functionality makes it possible to operate

HMI applications not just remotely, but also directly on the PC.

The integrated WinIO interface makes it possible to fully simulate I/O

points.

Figure 35: Complete simulation at every level

8.1Simulation of controller and drive

Controller simulation

Simulation of a controller can be started by selecting the simulation icon in Automation Studio. All control programs

run directly on the PC. This means that all of the software functions in the control application can be configured and

tested independently of the hardware. When you switch to simulation mode, the project is rebuilt, the simulation

environment is automatically started and an online connection to Automation Runtime Simulation is established.

Figure 36: Activating CPU simulation from the Automation Studio toolbar

The active CPU simulation is displayed in the Automation Studio status bar with the same icon as during activation.

Figure 37: Automation Studio status bar – Simulation running

Project management \ Simulation \

Drive simulation

If the drive is simulated on the actual controller CPU, this enables the drive simulation. The simulation mode can be

selected in the drive's hardware configuration.

Depending on the mode, either a complete simulation is performed or only setpoint generation without controller

cascading. After changing the simulation mode, the project must be built and sent to the controller.

## Page 42

42INTRODUCTION TO MAPP AXIS TM415

Figure 38: Enabling simulation in the device's hardware configuration

Motion control \ mapp Motion \ Configuration \ Hardware

Motion control \ mapp Motion \ Concept \ Controller \ Simulation

Simulation of the load or motor

To simulate the motor when only the PLC and ACOPOS drive are available, the appropriate settings must be made in

the device-specific configuration.

Various settings are possible. If a load is not specified, only the motor is simulated.

The load can also be simulated and enabled on devices in the ACOPOS servo family.

Figure 39: Load simulation configuration in the device-specific settings

## Page 43

SIMULATION OPTIONS 43
Exercise: Simulation
The goal of this exercise is to work with the different simulation options.
As a result, the different possibilities should be tried out in this exercise.
Switch motor simulation on/off in the hardware configuration
•
Switch on/off device simulation in the hardware configuration
•
Locate the settings for configuring the load simulation (device-specific parameter)
•
Simulate the controller and the drives in Automation Studio using "Online / Enable simulation".
•

## Page 44

44INTRODUCTION TO MAPP AXIS TM415

9Summary

Drives are added in Automation Studio in the Physical View or in System Designer. The drive configuration can be

added easily to the Configuration View from the Toolbox.

The axis component can be put into operation quickly using mapp Cockpit.

Figure 40: The integrated drive concept in Automation Studio

The Logger makes it possible to monitor drive communication and provides information about called function blocks.

The integrated autotuning function allows controller settings to be determined very quickly. System Diagnostics Man-

ager is used to read basic information about the control system even without Automation Studio. Automation Help

provides extensive support for drive configuration, diagnostics and installing drive components.

The powerful simulation environment provided by Automation Studio allows the implementation and testing of drive

applications right at the workstation, further helping to reduce the amount of time needed for commissioning.

## Page 45

AUTOMATION ACADEMY45

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

## Page 46

46 INTRODUCTION TO MAPP AXIS TM415

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

V3.0.0.4 ©2025/03/11 by B&R, All rights reserved.

All registered trademarks are the property of their respective owners.

Subject to technical changes without notice.