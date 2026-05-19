## Page 1

TM545

Integrated safe motion

control

## Page 2

2 INTEGRATED SAFE MOTION CONTROL TM545
Requirements
Training modules: TM210 - Working with Automation Studio
TM415 - Introduction to mapp Axis
TM515 - Programming and commissioning safety applications with mapp Safety
Software Automation Studio ≥ V4.8
Automation Runtime ≥ 4.8x
mapp Safety ≥ 5.12
mapp Motion ≥ 5.12
HW upgrade ≥ 2.1.0.0
Hardware SG4 CPU with POWERLINK V2 interface / interface card
SafeLOGIC (X20SL8101, X20SLX806)
ACOPOS P3 with SafeMOTION
Display module 8EAD0000.000-1

## Page 3

TABLE OF CONTENTS 3
Table of contents
1 Introduction.........................................................................................................................................................4
1.1 Learning objectives..............................................................................................................................4
2 Operating principle of "safe motion control"..............................................................................................6
2.1 Comparison to motion control without integrated safety technology....................................6
2.2 Safe power transmission system.....................................................................................................7
2.3 The idle current principle...................................................................................................................8
2.4 Pulse disabling and motor holding brake output.........................................................................9
2.5 Error states with SafeMOTION.........................................................................................................9
3 Configuration.....................................................................................................................................................11
3.1 Safety functions in the sample application..................................................................................12
3.2 Configuration in Automation Studio.............................................................................................13
3.3 Use in SafeDESIGNER........................................................................................................................18
4 Error handling...................................................................................................................................................25
4.1 Status indicators on the drive........................................................................................................25
4.2 Logger..................................................................................................................................................25
4.3 mapp Cockpit.....................................................................................................................................26
5 Safety functions and their application.......................................................................................................28
5.1 Safe Torque Off (STO).......................................................................................................................28
5.2 Safe Stop 1 (SS1)................................................................................................................................30
5.3 Safe homing........................................................................................................................................34
5.4 Safe Stop 2 (SS2)...............................................................................................................................35
5.5 Safely Limited Speed (SLS).............................................................................................................38
5.6 Safely Limited Increment (SLI).......................................................................................................40
5.7 Other SafeMOTION functions..........................................................................................................43
6 Example projects and solutions...................................................................................................................44
6.1 Example solution - SafeDESIGNER project...................................................................................44
6.2 Example solution - Automation Studio project..........................................................................46
6.3 Sample solution - Variables.............................................................................................................48
6.4 Sample solution - Parameters for the safe hardware modules...............................................49
7 Summary.............................................................................................................................................................51

## Page 4

4INTEGRATED SAFE MOTION CONTROL TM545

1Introduction

Training module "TM545 - Safe motion control with mapp Safety" is designed to become familiar with the SafeMOTION

safety functions and demonstrate how they can be used in Automation Studio and SafeDESIGNER.

This training course will explore the relationship between safety-oriented and non-safety-oriented (standard) appli-

cations. It will introduce the safety functions and explain how to use them.

Figure 1: mapp Safety offers the full range of functions for the entire hardware portfolio

The safety functions integrated in the drive open up entirely new possibilities for guaranteeing the safety of personnel

while maintaining maximum machine availability.

Training module "TM545 – Safe motion control with mapp Safety" is designed to accompany training

seminars but does not represent a full documentation.

The complete documentation is available in Automation Help.

1.1Learning objectives

The goal of this training module is to become familiar with the SafeMOTION safety functions and to learn how they

are used.

Participants will learn the principles on which safe integrated motion control operates.

•

Participants will learn about the available safety functions and how they are used (STO, SS1, etc.).

•

Participants will learn how to add and configure a safe drive in Automation Studio.

•

Participants will learn about the relationship between standard and safety applications.

•

Participants will learn about the function blocks in the openSAFETY library and the procedure for developing

•

safety functions.

Participants will learn the procedure for commissioning and maintenance.

•

## Page 5

INTRODUCTION 5
In order to correctly implement a safety application, it is important that applicable regulations and stan-
dards are observed in all phases of the safety application's lifecycle. This training module only covers
the use of the integrated safety functions from SafeMOTION functions in SafeDESIGNER. This training
manual can therefore never replace sound training in safety-related topics.

## Page 6

6INTEGRATED SAFE MOTION CONTROL TM545

2Operating principle of "safe motion

control"

From the perspective of the standard application, the ACOPOS servo family exhibits the same behavior in the version

with and without integrated safety technology. The drive can therefore be integrated in the POWERLINK network as

usual and operated in Automation Studio via mapp Motion function blocks.

What makes the ACOPOS servo family with SafeMOTION unique is the additional software and hardware. Here, the

encoder signal is evaluated with regard to safety, pulse disabling is checked and the motor holding brake output is

controlled.

The purpose of this section is to explain the fundamental characteristics of motion control with integrated safety

technology as well as to present its main advantages.

Figure 2: mapp Safety

2.1Comparison to motion control without integrated safety technology

The fundamental purpose of safe motion control is to interrupt the power supply to the motor. This interruption is

performed using safe pulse disabling.

Motion control without SafeMOTION

Pulse disabling is controlled externally via two inputs

•

Motion control with SafeMOTION

is controlled by the SafeMOTION module within the module.Safepulse disabling

•

The  is switched and monitored on the inverter modulesafe motor holding brake output

•

The  is evaluated in order to monitor the speed and position limits as neededsafe encoder signal

•

SafeMOTION makes it possible to monitor the safe speeds and positions. A moving axis can be used to

restore a safe state!

The integrated safety technology does not actively intervene in control processes and therefore only handles safety

and monitoring functions.

## Page 7

OPERATING PRINCIPLE OF "SAFE MOTION CONTROL"7

Stopping an axis

The advantage of integrated safety technology is the possibility to respond to safety-related events. The drive does

not necessarily have to be powered off, but can functionally be brought to a standstill in a controlled manner by the

standard application and monitored by the SafeMOTION module.

Commands to stop or switch off the axis are already known and are also called from a program on the PLC. The safety

application can now set up a time or ramp window and handle standstill monitoring, for example.

Figure 3: Hardware topology with integrated safety technology

If a safe state of the machine is no longer given and an unauthorized movement is executed, the safety application

enables safe pulse disabling of the inverter and the holding brake. This ensures that no additional energy is introduced

into the system.

Safety violations should not be the standard case. Safe motion control technology is purely used for monitoring a safe

state. Interaction with the standard application is therefore required at all times.

The response times achieved minimize residual movement in the event of error resulting in a significant increase in

safety.

2.2Safe power transmission system

The safe power transmission system consists of the following components:

SafeMOTION-compatible device

•

Encoder cable

•

Motor cable

•

Motor with position encoder

•

The safe drive module basically consists of a standard ACOPOS system with additional SafeMOTION hardware and

firmware. The encoder interface / SafeMOTION module is permanently installed on the inverter module and cannot

be replaced.

Depending on the SafeMOTION safety functions required, a license package is required to enable them.

In the first steps, the drive is commissioned as usual as a familiar ACOPOS device with all configuration settings and

test options in Automation Studio. In addition, the SafeLOGIC controller must enable the controller via the "Enable"

signal.

## Page 8

8INTEGRATED SAFE MOTION CONTROL TM545

SafeMOTION

ElectronicsSafeMOTION

Electronics

POWERLINKEPL-SafetyCommunication

Monitoring,Control

Encoder position

DiagnosticCurrent/ Speed / Postition / Brake

functions

Motor controlSafe pulse disabling

Brake controlBrake control

Safe current measurement*Powerstage

lnna

oon

iiergttocc

kitseea

o nnr

M.Bcnn

noo

Ecc

B&R Safety Motor

Encoder shaftMotorBrakeEncoderMotor shaft

* Only valid for ACOPOSmulti SafeMOTION SinCos & ACOPOS P3 SafeMOTION

A functinal safety encoder, safeencoder mounting and/or corresponding cables

may be requireddepending on the safety function being used.

Figure 4: A safe power transmission system demonstrated via an ACOPOSmulti SafeMOTION example

Safety technology \ mapp Safety \

General information \ Licensing

•

Engineering \ SafeMOTION \ The safe power transmission system

•

2.3The idle current principle

Integrated safety technology with SafeMOTION uses the idle current principle. When there is a logical 0 at a controller

input, the corresponding safety function or error response is executed.

The idle current principle ensures that the system changes to the safe state and safe pulse disabling is enabled. No

more energy can be introduced into the system to cause additional movement. If the axis is in motion at the time the

error occurs, it will coast to a stop.

In engineering, the generalization of this principle is referred to as "" (see "FAIL SAFE state" on page 9).FAIL SAFE

This is why cutting off the drive's energy and torque is the only safe function that can be executed at any time.

As a last consequence, safe pulse disabling is always enabled in the event of a safety violation, since this initiates the

safe state. The axis is thus switched to a torqueless state, even if a movement is performed.

Situations involving external forces (e.g. suspended loads) can result in dangerous movements! If this

poses a safety risk, the user must implement the necessary measures to eliminate the risk (e.g. mechan-

ical brakes)! These measures must correspond to the required safety level!

Safety technology \ Intended use \ Safe state \ The idle current principle

## Page 9

OPERATING PRINCIPLE OF "SAFE MOTION CONTROL" 9
2.4 Pulse disabling and motor holding brake output
SafeMOTION does not actively intervene in open and closed control loops of the inverter module. Only safe pulse dis-
abling and the safe motor holding brake output are operated directly.
Safe pulse disabling
Safe pulse disabling prevents control pulses of the processor from reaching the power unit of the inverter and ensures
that the power supply to the motor is interrupted so that it loses all torque/power.
No external wiring is required for SafeMOTION. Pulse disabling is connected by the SafeMOTION module via two chan-
nels within the module. The request for the STO safety function is made via the SafeLOGIC controller and is transferred
via the openSAFETY protocol.
Safety technology \ mapp Safety \ Engineering \ SafeMOTION \ Safe actuators \ Safe pulse disabling
Safe motor holding brake output
The safe motor holding brake output can enable the motor brake output independently of the active controller on the
inverter module. A transistor interrupts the flow of current, the magnetic field in the coil is weakened and the motor
brake engages.
The voltage on the motor brake output is evaluated with regard to safety by SafeMOTION, with the transistors tested
cyclically.
Safety technology \ mapp Safety \ Engineering \ SafeMOTION \ Safe actuators \ Sensors \ Safe motor
holding brake output
2.5 Error states with SafeMOTION
There are essentially two error states, whereby pulse disabling and the safe motor holding brake output are not en-
abled.
The logbook entry in Automation Studio provides additional information about the pending error.
04_Betriebszustände_FailSafe
•
04_Betriebszustände_FunctionalFailSafe
•
2.5.1 FAIL SAFE state
If a hardware or firmware error occurs, the safe inverter module switches to the non-acknowledgeable error state FAIL
SAFE.
A logbook entry in Automation Studio provides more detailed information regarding a pending error, which can also
be evaluated in the standard application. In addition, the FAIL SAFE state is indicated by the corresponding LEDs on
the device used.
The error may have been triggered by defective hardware or a configuration mistake.
The following behavior results from the FAIL SAFE state:
Safe pulse disabling is always active, i.e. the motor is no longer supplied with power and does not generate
•
torque.
In this state, the motor holding brake output is always set to 0 V and the motor holding brake is thus engaged. If
•
the drive was in motion beforehand, the motor holding brake will suffer mechanical wear.
To return the drive to the OPERATIONAL state, a power off / power on cycle must be performed.
Safety technology \ mapp Safety \ Engineering \ SafeMOTION \ Integrated safety functions \ State FAIL
SAFE

## Page 10

10 INTEGRATED SAFE MOTION CONTROL TM545
2.5.2 FUNCTIONAL FAIL SAFE state
If a monitored limit is exceeded or an encoder error occurs during operation, then the device changes to an acknowl-
edgeable error state – the FUNCTIONAL FAIL SAFE state – as long as safe evaluation of the encoder signal is required
for the safety functions being used. Information about any errors that occur can be found in the logbook entry in Au-
tomation Studio.
If the module switches to the FUNCTIONAL FAIL SAFE state:
The S_NotErrFUNC output on the function block is reset.
•
The drive loses all torque/power and coasts to a stop!
•
The motor holding brake output is set to 0 V, which engages a connected motor holding brake.
•
In the event of an error, a synchronous axis will no longer be synchronous.
•
If cutting off torque and coasting to a stop on the machine is a problem, an STO1 with delayed STO can also be set in
the SafeMOTION parameters. This provides the possibility for the standard application to initiate a short circuit stop
with the motor. This results in increased braking of the motor – and therefore the axis – using values that go beyond
the defined limits.
The temperature of the motor windings will increase, and this should be taken into account. If there is a risk of motor
overheating, the ACOPOS drive automatically switches off and the motor coasts to a stop.
As an alternative, some type of external brake could be installed on the mechanical system.

## Page 11

CONFIGURATION11

3Configuration

For the realization of the application and analysis of the different behavior patterns of the SafeMOTION functions, the

following hardware setup serves as an exercise:

X20 controller with I/Os

•

SL8101 with SafeIOs

•

Light curtains

°

Acknowledgement button

°

Mode selector switch

°

Emergency stop switch

°

POWERLINK bus controller with DI module

•

ACOPOS P3 SafeMOTION with two axes

•

Figure 5: Training hardware

ModuleChannelFunctionSafeDESIGNER configuration

X20SI2100SafeTwoChannelInput0102Light curtainsPulse Source = No Pulse

- SI_LightcurtainFilter off = 1000 µs

X20SC0806SafeTwoChannelInput0102Emergency stop

- SI_EStop

X20SC0806SafeDigitalInput03Automatic modePulse source = Pulse 3

- SI_Automatic

X20SC0806SafeDigitalInput04Manual modePulse source = Pulse 3

- SI_Manual

X20SC0806SafeDigitalInput05Green buttonPulse source = Pulse 4

- SI_Reset

Table 1: Cable pinout

Configuration and programming are split into two areas:

Automation Studio

Configuring hardware

•

Controlling an axis with mapp Motion

•

## Page 12

12INTEGRATED SAFE MOTION CONTROL TM545

SafeDESIGNER

Configuring safe modules and axes

•

Using function block "openSAFETY_BuR_Motion_SF"

•

3.1Safety functions in the sample application

The following safety functions must be implemented in sample project "Saw":

Emergency stop

The following function must be implemented:

Functionally stop the movement when triggered.

•

Enable STO with a delay.

•

Acknowledge by pressing the green button after startup.

•

Acknowledge by pressing the green button after resetting the emergency stop

•

function.

The simultaneity for multi-channel evaluation must be within .200 ms

•

Figure 6: Emergency stop

switch

Light curtains

The following function must be implemented:

3 seconds after triggering the light curtain, switch the axis to a torqueless state.

•

After startup and triggering the light curtain, acknowledge by pressing the green

•

button.

The simultaneity for multi-channel evaluation must be within .300 ms

•

Figure 7: Light curtains

Mode selector switch

The following function must be implemented:

Stop the movement when switching.

•

3 switch positions are evaluated:

•

Manual - Switch position right

°

Automatic - Switch position left

°

Neutral - Switch position middle

°

Figure 8: Mode selector

switch - Switch position right

## Page 13

CONFIGURATION13

Green button

The following function must be implemented:

Start a movement depending on the selected mode of operation.

•

Acknowledge the error in the standard application as well as in the safety applica-

•

tion.

Figure 9: Green button

Operating modes

Automatic operation (switch position left)

The positive edge of the green button starts a movement with a constant velocity

•

The light curtain is active when triggered: Axis stops, SS1 is immediately active, STO after 3 seconds

•

A violation of the safety equipment must be acknowledged via a positive edge of the green button.

•

Manual operation (switch position right)

As long as the green button is pressed, the axis is in motion

•

If the light curtain is triggered, the speed is reduced and monitored with SLS

•

Standstill of the axis is monitored with SS2

•

Movement monitored with SLI

•

Neutral operation (Switch position middle)

Axis is moved using mapp Cockpit

•

Acceleration and braking procedure is monitored with SLA according to specified acceleration or deceleration

•

limit values

3.2Configuration in Automation Studio

At the beginning, all hardware is configured in the Physical View in Automation Studio. The necessary mapp configu-

ration packages are added to the Configuration View. Subsequently, the SafeMOTION axis is controlled via function

block MpAxisBasic.

## Page 14

14INTEGRATED SAFE MOTION CONTROL TM545

The difference for a device with

SafeMOTION is that a safe node is addi-

tionally added to the Physical View. The

status of the various safety functions is

available here via the I/O mapping.

Figure 10: I/O mapping for the SafeMOTION module

Management in Automation Studio includes the following:

Adding hardware to the Physical View

•

Configuring mapp Safety and mapp Motion

•

Assigning the axis object to the inverter module

•

Controlling an axis in the standard application

•

Motion control \ mapp Motion

Safety technology \ mapp Safety

Exercise: Configuration and implementation in Automation Studio

The aim of this exercise is to configure the hardware, add the mapp configurations and control the axis in the standard

application.

1)Create an Automation Studio project.

2)Configure the controller, the SafeLOGIC, the I/Os and the ACOPOS drive.

3)Add the mapp Safety and mapp Motion configurations.

4)Add the ST program, move it to the 10 ms task class and implement function block MpAxisBasic.

5)Compile and transfer the project.

3.2.1Adding the hardware

The hardware modules required are added to the Physical View and connected as needed via POWERLINK or X2X Link.

## Page 15

CONFIGURATION15

Figure 11: Physical View & System Designer

The configuration of a SafeMOTION axis does not differ from devices without integrated safety technology. However,

a SafeLOGIC or SafeLOGIC-X controller is required; otherwise, controller enable cannot be controlled.

The node number on the ACOPOS drive, which must be set via the display module (see image), must match the node

number in Automation Studio.

Figure 12: Display module ACOPOS P3 8EAD0000.000-1

Hardware \ Motion control \ ACOPOS P3 \ Technical data \ Accessories \ 8EAD display modules \ Op-

eration

## Page 16

16INTEGRATED SAFE MOTION CONTROL TM545

I/O mapping

The status of individual safety functions can be read out via the

I/O mapping for the SafeMOTION module.

These are available in the form of status bits and can be evalu-

ated with the connection to a process variable in the application

in Automation Studio.

Figure 13: I/O mapping for the SafeMOTION module

3.2.2Configuration of mapp components

mapp Safety

The configuration for the SafeDOMAIN (Config.sfdomain) is added to

the mapp Safety folder in the Configuration View. The name and pass-

word are defined for the SafeDESIGNER project.

Figure 14: mapp Safety configuration

mapp Motion

To control an axis in Automation Studio, a single axis configuration is re-

quired in the mapp Motion folder (Config_1.axis). The name of the axis

object is defined in this file (gAxis_1).

Figure 15: mapp Motion configuration

It is important to ensure that the same system of units is para-

meterized for mapp Motion as is used in SafeDESIGNER; other-

wise, it is difficult to perform trace analyses.

Figure 16: mapp Motion system of units

To ensure that the axis object and the hardware are connected and the power supply is correctly defined, the config-

uration of the ACOPOS drive is opened in the Physical View.

The following settings must be made:

Set the power supply to "ETA system (for training only)".

•

Select the axis object that has been added under "Axis reference".

•

## Page 17

CONFIGURATION17

Figure 17: ACOPOS configuration

Select Runtime versions + OPC UA

When using SafeMOTION, the appropriate versions of the fol-

lowing Technology Packages are required:

mapp View

•

mapp Motion

•

mapp Safety

•

Figure 18: mapp View version

Enabling the OPC UA server

If mapp View is used in a project, the OPC UA server must be en-

abled.

Figure 19: mapp View version

3.2.3Implementing MpAxisBasic

Function block MpAxisBasic is used to control the axis and read out the status during runtime.

To do this, an ST program is added to the Logical View, an instance of the block is created and the following inputs

are connected:

MpLink  Address of the axis object→

•

Enable  TRUE→

•

Parameters  Parameter structure (variable with data type "MpAxisBasicParType")→

•

Figure 20: Implementing mpAxisBasic

## Page 18

18INTEGRATED SAFE MOTION CONTROL TM545

To enable a faster execution of the program, the task is moved to task class 1 [10 ms].

Figure 21: Task class 1

Building and transferring

All changes can now be compiled and transferred to the controller.

Do not forget to enable or disable the SNMP server and the correct IP settings!

Motion control / mapp Motion / Guides / "Getting started" / Axis

3.3Use in SafeDESIGNER

The entire safety application is created and the individual modules are parameterized in the SafeDESIGNER project.

Only devices with SafeMOTION and safety-related components appear in the Safety View. The content of the parameter

list on the right side changes with the selected safe module.

Figure 22: Safety View and parameter list in SafeDESIGNER

Safety applications can be easily created using openSAFETY function blocks. These function blocks are already certi-

fied and thus reduce the time required in all phases of the safety application lifecycle.

Safety technology \ mapp Safety \ Programming \ SafeDESIGNER libraries \ openSAFETY_BuR_Mo-

tion_SF

Exercise: Parameterization and programming in SafeDESIGNER

The objective of this exercise is to parameterize the axis correctly and to use the openSAFETY function block so that

the axis moves.

1)Open SafeDESIGNER.

2)Set parameter "EUS - Encoder type" to "Encoder used".

3)Add library "openSAFETY_BuR_Motion_SF".

## Page 19

CONFIGURATION19

4)Add function block "SF_oS_MOTION_BR".

5)Connect inputs "Activate", "S_AxisID", "S_Control_Reset", "S_Control_Activate".

6)Compile and then transfer using the Remote Control dialog box.

7)Start the axis movement in Automation Studio.

3.3.2Parameterization of the safe axis in SafeDESIGNER

Encoder type

To commission the safe axis, the correct parameters must be set.

Since an EnDat2.2 encoder set up for safety must be used for the training hardware, "Encoder used" must be selected

in setting "EUS - Encoder type".

Figure 23: Parameter of the SafeMOTION axis

General settings

The system of units and the direction of rotation of the safe position are defined in this group.

Safety response time

This parameter block concerns the response time of the SafeMOTION. These settings can be defined for all safe nodes

in the SafeLOGIC parameters in SafeDESIGNER.

Safe monitoring of the motor/encoder shaft connection

To be able to safely exclude mechanical errors, parameter "Encoder monitoring - Safe encoder mounting" has two

setting options:

""From motor data record

•

For B&R motors with EnDat2.2 FS encoders, the encoders are mounted strictly according to Heidenhain specifi-

cations to avoid mechanical connection errors (breakage, slip, offset). Information about encoder mounting is

stored in the motor data, so no further measures are required to monitor the connection between the motor and

the encoder shaft. If the encoder is not mounted securely, additional monitoring measures must be enabled.

""Approved by user

•

This setting can be used for third-party motors where the encoder is securely attached, but this information is

not stored in the motor data.

Safety technology \ mapp Safety \ Engineering \ SafeMOTION \

Register description SafeMOTION / Parameters in SafeDESIGNER

•

Safe actuators \ Sensors \ Safe encoder input \ Mechanical mounting

•

3.3.3Adding a library and a function block

Library "openSAFETY_BuR_Motion_SF" is used to implement the SafeMOTION function.

## Page 20

20INTEGRATED SAFE MOTION CONTROL TM545

Figure 24: Adding the library to the project tree

Figure 25: Selecting the library

All function blocks have PLCopen-specified inputs or outputs:

Activate  Enabling the function block→

•

Ready  Function block is executed→

•

Error  Boolean error message→

•

DiagCode  Error code→

•

In addition, input "S_AxisID" is used to reference to the axis used.

Function blocks

Function block "SF_oS_MOTION_BR" is used to map most of the avail-

able safety functions.

For clear programming, various safety functions are also grouped in-

to smaller blocks, according to the openSafety drive profile:

SF_oS_MOTION_Basic_BR

•

SF_oS_MOTION_Speed_BR

•

SF_oS_MOTION_Advanced_BR

•

SF_oS_MOTION_AbsOos_BR

•

Simultaneous use of an axis with one of the named function blocks

and function block SF_oS_MOTION_BR is not permitted

Figure 26: Function block SF_oS_MOTION_BR

The following function blocks are available to additionally use the cyclic data such as safe speed, safe position or safe

torque in the safety application:

SF_oS_MOTION_Data_Speed_BR

•

SF_oS_MOTION_Position_BR

•

SF_oS_MOTION_UserData_BR

•

Safety technology \ mapp Safety \ Programming \ SafeDESIGNER libraries \ openSAFETY_BuR_Mo-

tion_SF

## Page 21

CONFIGURATION21

Connecting the function block

To enable the movement of the axis and to send controller enable from SafeLOGIC to SafeMOTION, function block

"SF_oS_MOTION_BR" is added to the worksheet and the following inputs are connected:

Activate  Constant "TRUE" or BOOL variable→

•

S_AxisID  Connection to the real axis→

•

S_Control_Reset  Connection to green button→

•

S_Control_Activate  Constant "SAFETRUE" or BOOL variable→

•

Figure 27: Connecting the inputs

The pulse source of SafeDigitalInput05 must be set to "Pulse 4" so

that the signal of the green button can be read out correctly.

Figure 28: Pulse source

The connection to the real axis is added via

drag-and-drop from the Safety View.

Figure 29: Axis in the Safety View

Safety technology \ mapp Safety \ Programming \ SafeDESIGNER libraries \ openSAFETY_BuR_Mo-

tion_SF\ SF_oS_MOTION_BR

3.3.4Compiling, transferring and starting an axis

Compiling

The project is compiled before it is transferred to the safety controller.

Figure 30: Compiling

3.3.4.1Transfer to the SafeLOGIC controller

The safety application can be transferred and commissioned either via the mapp Safety HMI application or the Remote

Control dialog box as described in "TM515 - Programming and commissioning a safety application with mapp Safety".

## Page 22

22INTEGRATED SAFE MOTION CONTROL TM545

mapp Safety HMI

The following link is used to access the mapp Safety HMI application: <IP address>:81/mappSafety

Figure 31: mapp Safety HMI

After compiling the SafeCOMMISSIONING.xml file in SafeDESIGNER, the .swt file is updated in Automation Studio.

These changes are now compiled in Automation Studio and transferred to the gray controller.

The mapp Safety HMI application is called via a browser and the SafeAPPLICATION is now loaded from the gray con-

troller to the SafeLOGIC controller.

Transfer via the mapp Safety HMI application must be used for the first and last step of commissioning.

This ensures that the most current SafeAPPLICATION file is stored on the gray controller.

Safety technology \ mapp Safety \ "Getting started" \ Commissioning the preconfigured HMI application

## Page 23

CONFIGURATION23

Remote Control dialog box

The changes to the SafeAPPLICATION are compiled in

SafeDESIGNER and transferred directly to SafeLOGIC con-

troller using the Remote Control dialog box.

The setup mode is enabled in this dialog box to be able to

use the mapp Safety HMI application.

Figure 32: Remote Control dialog box

If the SafeAPPLICATION is transferred via the Remote Control dialog box, only the SafeLOGIC file is up-

dated, but not the file on the gray controller.

This mode is recommended only during implementation for testing the SafeAPPLICATION.

The user must ensure a complete transfer via the mapp Safety HMI application!

Safety technology \ mapp Safety \ Programming \ SafeDESIGNER \ Dialog boxes for controlling the

safety controller \ Remote Control

Starting an axis movement

To be able to perform the first movement, the axis must first be enabled pressing the green button.

This is necessary to perform a start reset on the SafeMOTION module and to switch the internal state

machine to the "Operational" state.

Only when output "MpAxisBasic_0.Info.ReadyToPowerOn" is set to TRUE, the following inputs can be set one after the

other via the monitor mode or the Watch window:

## Page 24

24INTEGRATED SAFE MOTION CONTROL TM545

1)Set input "Power" to TRUE.

2)Set input "Home" to TRUE.

3)Set input "MoveVelocity" to TRUE.

Figure 33: Monitor mode

## Page 25

ERROR HANDLING25

4Error handling

During development and commissioning, various errors can occur on the drive or in SafeMOTION.

Diagnostics possibilities are presented in the following chapter.

4.1Status indicators on the drive

LED status indicators on the drive

One method of obtaining the status of the drive is via the LED indica-

tors on the drive itself.

Figure 34: Axis status display module

Display module

With the ACOPOS P3, additional information, such as the axis status,

can be read out via the display module. Automation Help describes the

menu navigation and function of the display module in more detail.

Figure 35: Axis status display module

Hardware \ Motion control \ ACOPOS P3 \

..\ Technical data \ 8EI servo drives \ Display

..\ Accessories \ 8EAD display module \ 8EAD0000.000-1 display module \ Operation

4.2Logger

During development, the module may start with the FAIL SAFE state or switch to the FUNCTIONAL FAIL SAFE state

unexpectedly. The most common reason for this is an incorrect parameterization in SafeDESIGNER. The Logger can

be used to identify the cause of the error.

## Page 26

26INTEGRATED SAFE MOTION CONTROL TM545

Figure 36: Logger

The short description of the Logger only provides basic information. The complete error text and details are available

in Automation Help.

Diagnostics and service \ Diagnostics tools \ Logger

4.3mapp Cockpit

The web-based mapp Cockpit HMI application can be reached using Google Chrome via the following URL: <IP ad-

dress>:8084/mappCockpit

There is a wide range of functions that are helpful when commissioning or troubleshooting a drive axis or axis group:

Testing the behavior by executing a command

•

Monitoring the behavior (observing live values, recording a trace and checking logged events)

•

Changing the configuration to achieve the desired behavior and, if necessary, testing the behavior once again

•

## Page 27

ERROR HANDLING27

Figure 37: mapp Cockpit

A "Getting started" tutorial in mapp Cockpit as well as detailed information are available in Automation Help and in

training module "TM415 - Basic information mapp Axis ".

Diagnostics and service \ mapp Cockpit

## Page 28

28 INTEGRATED SAFE MOTION CONTROL TM545
5 Safety functions and their application
The following section describes selected integrated safety functions and how they are used.
A list of all available safety functions and their safety level are available in Automation Help:
Safety technology \ mapp Safety \ Engineering
Safety level overview for safety functions \
Safety level overview for ACOPOS product family safety functions\
SafeMOTION integrated safety technology ("network-based safety technology")
The following applies to all monitoring safety functions:
If a current speed or position limit is violated, then SafeMOTION switches to the acknowledgeable FUNCTIONAL FAIL
SAFE error state.
Detailed information is available under "FUNCTIONAL FAIL SAFE state" on page
5.1 Safe Torque Off (STO)
STO is the fundamental safety function for SafeMOTION since it represents the implementation of the idle current
principle.
A request from the STO enables safe pulse disabling and switches off the torque and power to the drive. Safe pulse
disabling is enabled via a signal that is sent directly to the ACOPOS drive via the openSAFETY protocol.
Controlbit
STO
t
Statusbit
STO
t
Speed
t
Torque on
motor
t
Figure 38: Safe Torque Off, STO
A STO request causes synchronized axes to no longer be synchronous.
If the drive is in motion at the time STO is requested, it will coast to a stop.
The resulting residual movement and time depends on the properties of the machine and must always
be considered when dimensioning the safety equipment.
The maximum possible movement (worst case) must be assumed!
Application of the safety function
When STO is triggered, the drive is immediately prevented from supplying torque-generating power. This function can
be used for motors that are able to reach standstill in a sufficiently short amount of time on their own (e.g. due to
friction) or where coasting to a standstill is irrelevant from a safety point of view.

## Page 29

SAFETY FUNCTIONS AND THEIR APPLICATION29

Figure 39: Safe Torque Off, STO

Safety technology \ mapp Safety \ Engineering \ SafeMOTION \ Integrated safety functions \ Safe

Torque Off, STO

Exercise: Emergency stop function

The aim of this exercise is to stop the movement via the emergency stop button and to enable pulse disabling using

the STO safety function.

:SafeDESIGNER

1)Add and connect SF_EmergencyStop_V1.

2)Connect output "S_EStopOut" with input "S_Control_STO".

3)Compiling

:Automation Studio

1)Connect the emergency stop signal with global variable "gdiEStop".

2)Switch off input "Power" when the emergency stop button is pressed.

3)Compiling

Transferring the SafeAPPLICATION

:Testing

1)Start the axis movement in monitor mode.

2)Press the emergency stop button  The axis is stopped.→

3)Check DriveEnable of output mpAxisBasic.

4)Check the axis status on the ACOPOS display module.

5)Acknowledge function block "SF_EmergencyStop" by pressing the green button.

6)Acknowledge the error in the standard application with "ErrorReset".

7)Restart the movement.

## Page 30

30INTEGRATED SAFE MOTION CONTROL TM545

PROGRAM _INIT

MpAxisBasic_0.Enable := TRUE;

END_PROGRAM

PROGRAM _CYCLIC

MpAxisBasic_0.MpLink := ADR(gAxis_1);

MpAxisBasic_0.Parameters := ADR(parAxis);

IF (gdiEStop = FALSE) THEN

MpAxisBasic_0.Power := FALSE;

END_IF;

MpAxisBasic_0();

END_PROGRAM

Table 2: Example code for the emergency stop function - Automation Studio

Figure 40: Example code for the emergency stop function - SafeDESIGNER

5.2Safe Stop 1 (SS1)

When requesting the SS1 safety function, the deceleration process of the axis is monitored until standstill after ramp

delay time "SS1 - Ramp monitoring - Time" has passed. After decelerating, safe pulse disabling is enabled and switches

off the torque/power to the drive.

If a violation occurs during ramp monitoring, safe pulse disabling is enabled immediately and the drive switches to

acknowledgeable error state FUNCTIONAL FAIL SAFE.

Deceleration is controlled by the non-safety-related standard application.

## Page 31

SAFETY FUNCTIONS AND THEIR APPLICATION31

tt

SS1_RMRM_ED

Controlbit

SS1

t

Statusbit

SDC

t

Statusbit

SS1

t

Torque on

motor

t

Speedoptional

t

Figure 41: Safe Stop 1

The monitored ramp always starts at the currently monitored limit and is calculated using the parameterized slope.

If no speed limit is specified, the monitored ramp starts at the .maximum possible speed 2^31 - 1 = 2147483647 [unit/s]

This does not make sense in normal cases! In order to start the ramp at a physically reasonable value, it is recommended

to limit the currently monitored speed limit using safety function Safe Maximum Speed (SMS), for example siehe "Safe

Maximum Speed (SMS)" auf Seite 39.

Application of the safety function

The SS1 function enables a controlled stop function for the axes. This allows active deceleration thereby preventing

dangers that could otherwise occur due to axes spinning out.

Figure 42: Safe Stop 1

The SS1 safety function does not include monitoring the axis standstill. It simply enables pulse disabling

once the axis has been brought to a standstill.

Safety technology \ mapp Safety \ Engineering \ SafeMOTION \ Integrated safety functions \ Safe Stop

1, SS1

## Page 32

32 INTEGRATED SAFE MOTION CONTROL TM545
Exercise: Safe standstill and pulse disabling (SS1)
The goal of this exercise is to automatically set the axis in motion using the green button, implement the light curtain
function and trigger SS1.
Automation Studio:
1) Create variables "gdiReset" and "gdiLightcurtain" and connect them with the input.
2) Program an automatic procedure using CASE.
3) Steps: enINIT, enSTART, enPOWERON, enHOME, enOPERATION, enERROR
4) Compiling
SafeDESIGNER:
1) Parameterize "SafeDigitalInput01/ 02" with "No Pulse" and "Filter off".
2) Add "SF_ESPE_V1_00" and connect it.
3) Connect output "S_ESPE_Out" with input "S_Control_SS1.
4) Enable SS1 and set "SS1 - Ramp monitoring - Time" to 3 seconds.
5) Compiling
Transferring the SafeAPPLICATION
Testing:
1) Start the axis using the green button.
2) Trigger the light curtain.
3) Check DriveEnable of output mpAxisBasic.
4) Check the axis status on the ACOPOS display module.
5) Observe the status after 3 seconds.

## Page 33

SAFETY FUNCTIONS AND THEIR APPLICATION 33
IF gdiEStop = FALSE OR gdiLightcurtain = FALSE THEN
MpAxisBasic_0.Power := FALSE;
sStep := enERROR;
END_IF;
CASE sStep OF
enINIT:
IF (gdiReset = TRUE) THEN
sStep := enSTART;
END_IF;
enSTART:
IF (MpAxisBasic_0.Info.ReadyToPowerOn = TRUE
AND MpAxisBasic_0.Active = TRUE) THEN
sStep := enPOWERON;
END_IF;
enPOWERON:
MpAxisBasic_0.Power := TRUE;
IF (MpAxisBasic_0.PowerOn = TRUE) THEN
sStep := enHOME;
END_IF;
enHOME:
MpAxisBasic_0.Home := TRUE;
IF (MpAxisBasic_0.IsHomed = TRUE) THEN
MpAxisBasic_0.Home := FALSE;
sStep := enOPERATION;
END_IF;
enOPERATION:
MpAxisBasic_0.MoveVelocity := TRUE;
IF (MpAxisBasic_0.Error = TRUE) THEN
sStep := enERROR;
END_IF;
enERROR:
MpAxisBasic_0.MoveVelocity := FALSE;
IF (MpAxisBasic_0.Error = FALSE
AND MpAxisBasic_0.StatusID = 0) THEN
IF (MpAxisBasic_0.PowerOn = FALSE
AND MpAxisBasic_0.Power = TRUE) THEN
MpAxisBasic_0.Power := FALSE;
END_IF;
sStep := enINIT;
END_IF;
END_CASE;
MpAxisBasic_0.ErrorReset := gdiReset;
MpAxisBasic_0();
Table 3: Example code for the light curtain - Automation Studio

## Page 34

34INTEGRATED SAFE MOTION CONTROL TM545

Figure 43: Ramp monitoring parameter - SafeDESIGNER

Figure 44: Example code for the light curtain - SafeDESIGNER

5.3Safe homing

This safety function is used to establish a reference between the encoder position and the machine position.

Controlbit

Homing

t

tt

HOME_MHOME_M

Statusbit

Position

Valid

t

Homing

Active

t

Homing finishedStart HomingStart HomingHoming aborted

Figure 45: Safe homing

The following safe homing variants are supported:

Direct

•

Reference switch

•

Home offset / Home offset with correction (for SafeMOTION EnDat2.2 only)

•

Safety function "Safe homing" is a prerequisite for implementing safety functions  and  and for using the safeSLPSMP

position. The SafePositionValid status will remain set to SAFEFALSE until safe homing has been performed!

Safety technology \ mapp Safety \ Engineering \ SafeMOTION \ Integrated safety functions \ Safe hom-

ing

Exercise: Safe homing

The goal of this exercise is to directly home an axis in SafeMOTION.

:SafeDESIGNER

1)Define parameter "Homing - Home position".

## Page 35

SAFETY FUNCTIONS AND THEIR APPLICATION35

2)Define "Standstill monitoring - Speed tolerance" of 100.

3)Define "Standstill monitoring - Position tolerance" of 5.

4)Declare local variable "reqHoming".

5)Connect the variable with input "S_Control_Homing".

6)Compiling

Transferring the SafeAPPLICATION

:Test

1)Switch on the axis and home it in Automation Studio.

2)Enable the debug mode in SafeDESIGNER.

3)Force "reqHoming" to TRUE.

4)Check "S_Status_Homing" and "S_Status_ReqHomingOk".

Figure 47: SafeDESIGNER parameters

Figure 46: Safe homing code - SafeDESIGNER

5.4Safe Stop 2 (SS2)

If the SS2 safety function is enabled, the deceleration process of the axis and standstill are monitored after ramp delay

time "SS2 - Ramp monitoring - Time" has passed.

Unlike the SS1 function, the drive must be kept at standstill by the standard application.

If a violation occurs during ramp or standstill monitoring, safe pulse disabling is enabled immediately and the drive

switches to an acknowledgeable FUNCTIONAL FAIL SAFE error state.

## Page 36

36INTEGRATED SAFE MOTION CONTROL TM545

tt

RM_EDSS2_RM

Controlbit

SS2

t

Statusbit

SDC

t

Statusbit

SS2

t

Torque on

motor

t

optional

Speed

v

SM_Tt

v

SM_T

Positions

SM_T

s

SM_T

t

Figure 48: Safe Stop 2

The monitored ramp always starts at the currently monitored limit and is calculated using the parameterized slope.

If no speed limit is specified, the monitored ramp starts at the .maximum possible speed 2^31 - 1 = 2147483647 [unit/s]

This does not make sense in normal cases! In order to start the ramp at a physically reasonable value, it is recommended

to limit the currently monitored speed limit using safety function Safe Maximum Speed (SMS), for example.

Application of the safety function

After being stopped by the standard application, the drive must be actively held at standstill.

Figure 49: Safe Stop 2

Safety technology \ mapp Safety \ Engineering \ SafeMOTION \ Integrated safety functions \ Safe Stop

2, SS2

Exercise: Safe standstill after movement (SS2)

The goal of this exercise is to evaluate the manual and automatic mode and control SS2.

## Page 37

SAFETY FUNCTIONS AND THEIR APPLICATION 37
Automation Studio:
1) Create variables "gdiManual" and "gdiAutomatic" and connect them to inputs.
2) Implement the automatic mode → Movement with constant speed, light curtain is active.
3) Implement the manual mode → Movement when button is pressed, light curtain is not active.
4) Compiling
SafeDESIGNER:
1) Evaluate the switch position (manual and automatic mode).
2) Change "Pulse source" at SafeDigitalInput04 to "Pulse 3".
3) SS2 is enabled in manual mode 1 second after the green button is released.
4) Compiling
Transferring the SafeAPPLICATION
Testing:
1) Enable the manual mode.
2) Move the axis by pressing the green button.
3) Trigger the light curtain → The axis must continue to rotate.
4) Release the green button → The axis must stop but the controller remains switched on.
5) SS2 is active for one second after the green button has been released.
6) Move the axis in monitor mode with "MoveAbsolute" to trigger safety violations.
7) Check DriveEnable of output mpAxisBasic and the axis status on the ACOPOS display module.
IF ((gdiEStop = FALSE) OR
(gdiLightcurtain = FALSE AND gdiAutomatic = TRUE)) THEN
MpAxisBasic_0.Power := FALSE;
sStep := enERROR;
END_IF;
Table 4: Example code for the light curtain and automatic mode - Automation Studio
IF (gdiManual = TRUE) THEN
MpAxisBasic_0.MoveVelocity := FALSE;
MpAxisBasic_0.JogPositive := gdiReset;
ELSIF (gdiAutomatic = TRUE) THEN
MpAxisBasic_0.MoveVelocity := TRUE;
ELSE
MpAxisBasic_0.MoveVelocity := FALSE;
END_IF;
IF (MpAxisBasic_0.Error = TRUE) THEN
sStep := enERROR;
END_IF
Table 5: Example code for the manual and automatic mode - Automation Studio

## Page 38

38INTEGRATED SAFE MOTION CONTROL TM545

Figure 51: Example code for the connection of SS1 and SS2 -

SafeDESIGNER

Figure 52: Parameter SS2 - SafeDESIGNER

Figure 50: Example code for function block ModeSelector - SafeDESIGNER

5.5Safely Limited Speed (SLS)

Parameter "" defines the delay time between requesting and starting the mon-Ramp monitoring - Enable delay time

itoring.

The SLS safety function is used to monitor up to 4 specified speed limits "" and, in additionSLS1/2/3/4 - Speed limit

to the time, ramp "" when the SLS function is requested.SLS - Ramp monitoring

All limits can be monitored in parallel. If a request is made to monitor multiple speed limits at the same time, the

lowest limit value is always monitored. To make this possible, the function block includes four different inputs S_Re-

.questSLSX-1/2/3/4

tt

RM_EDSLSX_RM

Controlbit

SLS X

t

Statusbit

SDC

t

Statusbit

SLSX

t

optional

Speed

v

SLSX_L

t

v

SLSX_L

optional

Figure 53: Safe Limited Speed

Application of the safety function

Maintenance tasks and setup mode often require work to be performed on a machine while it is running. This increas-

es the potential for hazardous situations. SLS monitors whether the drive speed is below a limit value. This ensures

greater safety when working on the machine while it is running.

## Page 39

SAFETY FUNCTIONS AND THEIR APPLICATION39

Figure 54: Safe Limited Speed

Safety technology \ mapp Safety \ Engineering \ SafeMOTION \ Integrated safety functions \ Safely

Limited Speed, SLS

Safe Maximum Speed (SMS)

The main difference between safety function Safe Maximum Speed and SLS is that it cannot be actively requested.

It is either enabled (parameter "SMS - Enable = Enabled") or disabled (parameter "SMS - Enable = Disabled") in the

configuration.

When enabled, the current speed is constantly monitored according to a defined limit (parameter "SMS - Speed limit").

Safety technology \ mapp Safety \ Engineering \ SafeMOTION \ Integrated safety functions \ Safe Max-

imum Speed, SMS

Exercise: Manual mode limits speed

The goal of this exercise is to define the maximum speed and to enable a safely limited speed of 500 units/s when the

light curtain is triggered during a movement in manual mode.

:SafeDESIGNER

1)Set "Ramp monitoring - Enable delay time" with 100 milliseconds.

2)Enable "SMS - Enable".

3)Set "SMS - Speed limit" to 5100 units/s.

4)Set "SLS1 - Speed limit" to 550 units/s.

5)Set "SLS1 - Ramp monitoring - Time" to 1 second.

6)Connect the signal from the light curtain, manual mode and the green button with input "S_Control_SLS1".

7)Compiling

:Automation Studio

1)Decrease speed "parAxis.Jog.Velocity" when enabling the light curtain.

2)Update the parameter at runtime using "MpAxisBasic_0".

3)Compiling

Transferring the SafeAPPLICATION

## Page 40

40INTEGRATED SAFE MOTION CONTROL TM545

:Testing

1)Start a manual movement.

2)Trigger the light curtain and observe the changed movement.

3)Check output DriveEnable of mpAxisBasic and the axis status on the ACOPOS display module.

IF (gdiManual := TRUE) THEN

MpAxisBasic_0.MoveVelocity := FALSE;

MpAxisBasic_0.JogPositive := gdiReset;

IF (gdiLightcurtain = TRUE) THEN

parAxis.Jog.Velocity := 2;

MpAxisBasic_0.Update := TRUE;

MpAxisBasic_0();

ELSE

parAxis.Jog.Velocity := 0.5;

MpAxisBasic_0.Update := TRUE;

MpAxisBasic_0();

END_IF;

END_IF;

Table 6: Example code for reducing the speed - Automation Studio

MpAxisBasic_0.Update := FALSE;

MpAxisBasic_0.ErrorReset := gdiReset;

MpAxisBasic_0();

Table 7: Example code for resetting the update command - Automation Studio

Figure 56: Example code for wiring SLS1 - SafeDESIGNER

Figure 55: Parameter SLS1 - SafeDESIGNER

5.6Safely Limited Increment (SLI)

With the SLI safety function, a movement is monitored with regard to a defined position limit.

## Page 41

SAFETY FUNCTIONS AND THEIR APPLICATION 41
Controlbit
SLI
t SLI_DD t SLI_DD t SLI_DD t SLI_DD t
Statusbit
SLI
t
Speed
v
SM_T t
v
SM_T
Position
s ]
SLI_L
s SLI_L s ]
SLI_L
s SLI_L s SLI_L
s t
SLI_L
Figure 57: Safe Limited Speed
The safe axis must be at standstill when this function is enabled. A position window that is safety-monitored is then
generated. This position window depends on the parameterized safe position limit "SLI - Position limit".
The standard application must guarantee that this position window is not exceeded. The evaluation is performed via
the safe encoder position. Mechanical moving or bumping can thus also lead to a safety violation. Safe pulse disabling
is enabled immediately and an acknowledgeable error state is triggered.
After the safety function is disabled, monitoring continues for the configured period of time "SLI - Disable delay time".
This prevents continuous movement caused by constant jogging.
Safety technology \ mapp Safety \ Engineering \ SafeMOTION \ Integrated safety functions \ Safely
Limited Increment, SLI
Exercise: SLI in manual mode
The goal of this exercise is to be permitted to move the axis for a maximum of 20 revolutions in manual mode.
SafeDESIGNER:
1) Connect the inverted status of the manual mode and the green button to SLI.
2) Parameterize "SLI - Position limit" to 20 revolutions.
3) Limit "SLI - Disable delay time" to 2 seconds.
4) Building and transferring
Transferring the SafeAPPLICATION
Testing:
1) Start a manual movement.
2) Check output DriveEnable of mpAxisBasic and the axis status on the ACOPOS display module.

## Page 42

42INTEGRATED SAFE MOTION CONTROL TM545

Figure 58: Parameter SLI - SafeDESIGNER

Figure 59: Example code for SLI - SafeDESIGNER

Exercise: Evaluate limit values in mapp Cockpit

The goal of this exercise is to record and diagnose the safe speed and the change of the limit values in mapp Cockpit.

Automation Studio:

1)Add the mapp Cockpit configuration and assign role "Everyone".

2)Building and transferring

Google Chrome:

1)Open mapp Cockpit (IP address:8084/mappCockpit).

2)Configure a trace.

*ACP:IF3.ST3_Axis1:196 (ParId 196 - Safe speed)

°

*ACP:IF3.ST3_Axis1:224 (ParId 224 - Speed limit value)

°

3)Start a manual movement.

4)Evaluate safe speed and limit values.

Diagnostics and service / Diagnostics tools / Getting started with mapp Cockpit

Figure 60: Record safe speed and limit values of the axis in mapp Cockpit

## Page 43

SAFETY FUNCTIONS AND THEIR APPLICATION 43
5.7 Other SafeMOTION functions
This training module contains an overview of some integrated safety functions. For additional information about the
supported safety functions, see Automation Help:
Hardware \ Motion control \ SafeMOTION \ Safety technology \ Integrated safety functions

## Page 44

44INTEGRATED SAFE MOTION CONTROL TM545

6Example projects and solutions

The example code in this documentation is exclusively for implementing the test setup for this training.

This example code is not permitted to be adopted for safety applications under any circumstances.

Each safe machine application must be subjected to a separate risk analysis and a safety concept must

be created.

6.1Example solution - SafeDESIGNER project

Emergency stop and light curtain

Using function blocks "SF_EmergencyStop_V1" and "SF_ESPE_V1", the signals of the two sensors are evaluated and

acknowledged with the green button.

Figure 61: Emergency stop and light grid evaluation

Mode selector switch

Function block "SF_ModeSelector_V1" is used to evaluate the manual mode and the automatic mode.

## Page 45

EXAMPLE PROJECTS AND SOLUTIONS45

Figure 62: ModeSelector block

Function block: Safety_Function

PLCopen function block "SF_oS_MOTION_BR" is used for the active monitoring of four different parameters, such as

speed, position or acceleration.

## Page 46

46INTEGRATED SAFE MOTION CONTROL TM545

Figure 63: SafeMOTION function block

6.2Example solution - Automation Studio project

Init section

In the Init section, input ".Enable" of the mpAxisBasic block is enabled.

MpAxisBasic_0.Enable := TRUE;

Cyclic section

The status of the emergency stop function and light curtain is queried first and the axis is switched off. A step switching

chain is then used to automatically switch on the axis, home it and move it according to the set mode. Error handling

is also part of this code.

MpAxisBasic_0.Paramters := ADR(parAxis);

MpAxisBasic_0.MpLink := ADR(gAxis_1);

IF (gdiEStop = FALSE OR gdiLightcurtain = FALSE

AND gdiAutomatic = TRUE) THEN

MpAxisBasic_0.Power := FALSE;

sStep := enERROR;

END_IF;

CASE sStep OF

enINIT:

IF (gdiReset = TRUE) THEN

sStep := enSTART;

END_IF;

## Page 47

EXAMPLE PROJECTS AND SOLUTIONS 47
enSTART:
IF (MpAxisBasic_0.Info.ReadyToPowerOn = TRUE
AND MpAxisBasic_0.Active = TRUE) THEN
sStep := enPOWERON;
END_IF;
enPOWERON:
MpAxisBasic_0.Power := TRUE;
IF (MpAxisBasic_0.PowerOn = TRUE) THEN
sStep := enHOME;
END_IF;
enHOME:
MpAxisBasic_0.Home := TRUE;
IF (MpAxisBasic_0.IsHomed = TRUE) THEN
MpAxisBasic_0.Home := FALSE;
sStep := enOPERATION;
END_IF;
enOPERATION:
IF (gdiManual = TRUE) THEN
MpAxisBasic_0.MoveVelocity := FALSE;
MpAxisBasic_0.JogPositive := gdiReset;
IF (gdiLightcurtain = TRUE) THEN
parAxis.Jog.Velocity := 2;
MpAxisBasic_0.Update := TRUE;
MpAxisBasic_0();
ELSE
parAxis.Jog.Velocity := 0.4;
MpAxisBasic_0.Update := TRUE;
MpAxisBasic_0();
END_IF
ELSIF (gdiAutomatic = TRUE) THEN
MpAxisBasic_0.MoveVelocity := TRUE;
ELSE
MpAxisBAsic_0.MoveVelocity := FALSE;
END_IF
IF (MpAxisBasic_0.Error = TRUE) THEN
sStep := enERROR;
END_IF
enERROR:
MpAxisBasic_0.MoveVelocity := FALSE;
IF (MpAxisBasic_0.Error = FALSE
AND MpAxisBasic_0.StatusID = 0) THEN
IF (MpAxisBasic_0.PowerOn = FALSE
AND MpAxisBasic_0.Power = TRUE) THEN
MpAxisBasic_0.Power := FALSE;
END_IF;
sStep := enINIT;
END_IF
END_CASE;
MpAxisBasic_0.Update := FALSE;
MpAxisBasic_0.ErrorReset := gdiReset;
MpAxisBasic_0();

## Page 48

48INTEGRATED SAFE MOTION CONTROL TM545

6.3Sample solution - Variables

6.3.1SafeDESIGNER

Global variables

This file contains all variables that are connected to a safe input or output as well as the variables for the communi-

cation channels.

Figure 64: Global variables - SafeDESIGNER

Local variables: Main code

The function blocks and the status variables from the evaluation of the emergency stop function, light curtain and

mode selector switch are declared here.

Figure 65: Local variable declaration for main code

6.3.2Automation Studio

Global variables

The global variable file contains the variables that are directly connected to the inputs.

VAR

gdiEStop : BOOL;

gdiReset : BOOL;

gdiLightcurtain : BOOL;

gdiAutomatic : BOOL;

gdiManual : BOOL;

END_VAR

Text view of the global variables

Figure 66: Global variables - Automation Studio

Local variables and types

The local variable file contains the instance of the MpAxisBasic block, the associated parameter structure and the

variable for the step switching chain. The individual steps are created as an enumeration in the type file. This is done

to improve readability of the code. Simple numerical values can also be used instead of this enumeration.

## Page 49

EXAMPLE PROJECTS AND SOLUTIONS49

VAR

MpAxisBasic_0 : MpAxisBasic;

parAxis : MpAxisBasicParType;

sStep : enum_sequence;

END_VAR

Text view of the local variables

Figure 67: Local variables - Automation Studio

TYPE

enum_sequence :

( enINIT := 0,

enSTART := 10,

enPOWERON := 20,

enHOME := 30,

enOPERATION := 40,

enERROR := 100);

END_TYPE

Text view of the local types

Figure 68: Local types - Automation Studio

6.4Sample solution - Parameters for the safe hardware modules

Safe input module X20SI2100

The inputs from the safe input module

are wired to the light curtain. This sen-

sor already has its own test pulse, so it

has to be disabled on the module and a

filter must be configured. The light cur-

tain is evaluated via multi-channel eval-

uation using an equivalent signal.

Figure 69: Parameter X20SL8101

## Page 50

50INTEGRATED SAFE MOTION CONTROL TM545

Safe mixed module X20SC0806

The safe mixed module is wired to

the emergency stop, the mode selector

switch, the green button and the "Saw"

output. The emergency stop signal is

read out equivalently. The pulse for the

mode selector switch and green button

must be changed.

Figure 70: Parameter X20SC0806

Safe axis module 8ESMC59315

The safe axis must be parameterized

with the correct values depending on

the safety function used. The screen-

shot shows which parameters are nec-

essary to run the sample application

correctly.

Figure 71: Parameter 8ESMC59315

## Page 51

SUMMARY51

7Summary

With safe drive technology SafeMOTION, the drive is controlled via the PLC and only monitored via the safety functions

on the SafeLOGIC controller.

The hardware itself is configured in Automation Studio and commissioned using mapp Motion, mapp Safety and mapp

Cockpit.

SafeDESIGNER uses PLCopen blocks to evaluate sensors such as the light curtain or emergency stop function and to

monitor various movements. However, this application does not actively intervene in the control process of motion

control, but enables safe pulse disabling and disables the holding brake output in the case of a safety violation.

Figure 72: mapp Safety offers the full range of functions for the entire hardware portfolio

## Page 52

52INTEGRATED SAFE MOTION CONTROL TM545

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

## Page 53

AUTOMATION ACADEMY 53

## Page 54

54 INTEGRATED SAFE MOTION CONTROL TM545

## Page 55

AUTOMATION ACADEMY 55

## Page 56

B&R

Industrial Automation GmbH

A member of the ABB Group

B&R Straße 1

5142 Eggelsberg, Austria

office@br-automation.com

t +43 7748 6586-0

f +43 7748 6586-26

br-automation.com

V2.0.0.0 ©2023/10/23 by B&R, All rights reserved.

All registered trademarks are the property of their respective owners.

Subject to technical changes without notice.