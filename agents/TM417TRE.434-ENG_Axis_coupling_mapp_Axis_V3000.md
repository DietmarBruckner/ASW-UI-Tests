## Page 1

TM417

Axis coupling: mapp Axis

## Page 2

2 AXIS COUPLING: MAPP AXIS TM417
Requirements
Training modules: TM416 – Motion control: Basic functions
Software Automation Studio 4.3
Automation Runtime 4.33
mapp Motion Technology Package
Hardware X20 controller
ACOPOS servo family / ARsim

## Page 3

TABLE OF CONTENTS 3
Table of contents
1 Introduction.........................................................................................................................................................4
1.1 Learning objectives..............................................................................................................................4
2 General information about drive coupling...................................................................................................6
2.1 Axis group..............................................................................................................................................7
2.2 Purely Virtual Axis................................................................................................................................9
3 Linear axis coupling.........................................................................................................................................11
3.1 mapp Cockpit: Use without additional programming effort....................................................11
3.2 Axis coupling program......................................................................................................................12
3.3 Introduction to cam automats........................................................................................................14
4 Delay times........................................................................................................................................................23
5 Non-linear axis coupling.................................................................................................................................27
5.1 Introduction.........................................................................................................................................27
5.2 Compensation gear...........................................................................................................................34
6 Dynamic phase shifting..................................................................................................................................36
7 Cam automat design.......................................................................................................................................38
8 Summary............................................................................................................................................................40

## Page 4

4AXIS COUPLING: MAPP AXIS  TM417

1Introduction

The B&R drive solution provides flexible, high-performance tools for coupling drives electronically. This makes it pos-

sible to implement couplings for linear and also for dynamic motion sequences, for example. There are many practical

applications for these solutions, such as synchronous cutting procedures, dynamic transfer processes and flexible

length allocation.

Easy to use configuration objects and function blocks are provided for comprehensive handling of these functionali-

ties.

Figure 1: Labeling bottles

This training module explains how to use a range of functions to configure and control electronically coupled move-

ment sequences.

We will begin with a brief overview to become familiar with the individual options. We will then cover some basic theory,

ask ourselves some important questions and finally, learn how to use multi-axis functions in actual applications.

1.1Learning objectives

This training module uses selected examples to demonstrate the use of mapp Motion-compliant multi-axis functions.

## Page 5

INTRODUCTION 5
Participants will learn about the possibilities available with mapp Motion multi-axis components.
•
Participants will become familiar with the strengths and possibilities of the ACOPOS product family, which en-
•
ables maximum performance and synchronization precision when using POWERLINK.
Participants will learn how to connect drives using simple axis couplings and how to implement a phase shift.
•
Participants will learn about the concept of cams and virtual axes. Participants will be able to configure them in
•
Automation Studio and use them for an axis coupling.
Participants will be able to record the course of movement of master and slave axes and will understand the rela-
•
tionships.
Participants learn the underlying principles behind the cam automat.
•
Participants will learn about the various compensation gears that can be used when switching cams.
•
Participants will learn how to prepare an axis coupling for operation and become acquainted with the process of
•
error evaluation.
Participants will become comfortable in Automation Help and will be able to find there way around.
•

## Page 6

6AXIS COUPLING: MAPP AXIS  TM417

2General information about drive cou-

pling

Coupling drives electronically results in a predefined synchronized movement of drive axes. The coupling is established

using either a specified gear ratio or cams.

Position setpoint coupling is generally assumed. It is also possible to perform coupling using the actual position,

speed, torque or any other signals.

The coupling is defined using axes that are then assigned to hardware.

Coupling across devices in the ACOPOS servo family is therefore not a problem, and the coupling principle can be

applied anytime with any devices in the B&R product landscape. This is the case regardless of the hardware type being

used or the performance reflected by the size of the device.

Drive A is coupled with drive B using the position setpoint. This means that while the drives are actively

coupled, drive A must adjust its position according to the position of drive B. In this case, drive B is the

master, which specifies a reference position, and drive A the slave, which has a position based on the

master position.

Figure 2: Schematic illustration of a coupling

A coupling always requires a master signal, which provides the reference (position, time) and at least one slave drive,

which must follow this reference value using a "rule". However, the master signal does not have to come from a real

drive. In principle, drives can also be coupled to a variety of suitable reference values. Virtual axes, among others, are

available in the B&R drives for this purpose.

The master remains unaffected by the coupling procedure. It is simply used as the basis for the desired

coupling signal. For example, if a drive's position value is used as the master signal, then this master axis

can still be given a command even while coupling is active. In this situation, however, the slave drive is

still completely dependent on the master signal.

The type of position coupling (i.e. the "rule" that tells the slave drive how it

must follow the master signal) can be displayed in a diagram with a compar-

ison of the master and slave position.

This is shown in the image for a linear relationship between the master and

slave position.

Figure 3: Linear coupling of the slave position to

the master position

Coupling using electronic gears (linear coupling)

The position of the coupled master is shown in the horizontal direction.

The position of the slave can be seen in the vertical direction.

According to this specification, when the master signal changes uniformly

(e.g. the master axis moves at a constant velocity), the velocity of the slave

axis is also constant.

Figure 4: Gear ratio between

In this case, we are referring to "electronic gears". We are talking about a

master position and slave position

type of coupling that, in practice, is often required. The gear ratio repre-

sents the slope of the linear curve.

1:1, 1:2, 1:5, etc. denotes the relationship between master and slave and

can be any integer ratio.

## Page 7

GENERAL INFORMATION ABOUT DRIVE COUPLING7

It is irrelevant here whether a positive or negative slope is defined. This

results in the slave axis rotating counter to the rotation of the master axis.

Coupling using cam (nonlinear coupling)

The position relationship of master and slave axis does not have to be neces-

sarily be linear. In principle, custom-defined positioning paths, so-called elec-

tronic cams, can be created and used.

Figure 5: Nonlinear positioning path - Coupling

the slave position to the master position using a

cam

A simple coupling for an electronic gear as well as a coupling via cams can be quickly implemented for the drive. Au-

tomation Studio provides a cam editor for creating user-specific cams.

The corresponding function blocks for operation and modification of the couplings are included with the function

blocks in the mapp Motion libraries.

The cam automat offers extensive settings for connecting multiple cams to each other.

As specified, the mapp Motion multi-axis functions should be operated like function block MpAxisBasic.

Implementation of these functions in the automatic sequence of an application program is therefore

also the same.

mapp Technology \ Concept

Motion control \ mapp Motion \ Programming \ Application program \ Libraries \ Technology \ MpAxis \

Motion control \ mapp Motion \ Programming \ Application program \ Libraries \ Core \ McAxis

Motion control \ mapp Motion \ Programming \ Application program \ Libraries \ Core \ McAxGroup \

Behavior in the event of error

If an  during active coupling that causes the axis to stop or switch off, all slave axeserror occurs on the master axis

also do the same. If the axis is switched off, the system switches from a position setpoint to the actual position and

the slaves also follow the actual position to a standstill. The coasting process of the master axis (coasting to a stop)

is followed by the slave axes using the defined coupling ratio.

If an  during active coupling that causes the axis to stop or switch off, the master axis iserror occurs on the slave axis

not affected. Likewise, an error on one slave axis has no effect on the other slave axes.

If this type of behavior is not desired, it is necessary to set up a possibility to release the axes from the coupling ratio.

A start of a movement on the slave axes cancels coupling and calls a "Stop".1

2.1Axis group

Individual axes can also be combined into an administrative axis group independent of a movement.

This allows specific commands to be directed to a user-defined selection of individual axes. Grouping drastically sim-

plifies the preparation work for many axes and considerably reduces programming effort.

With a coupling, the slave axes can also be combined for error handling.

The following tasks are particularly suitable for control using axis group commands:

Switching on

•

Homing

•

Acknowledging errors

•

Stopping movements

•

For more information, see library McAxGroup.

Motion control \ mapp Motion \ Programming \ Application program \ Libraries \ Core \ McAxGroup \

1e.g. to move from a critical movement area or to be ready for the next start more quickly

## Page 8

8AXIS COUPLING: MAPP AXIS  TM417

Axis group states

The PLCopen states are used for operating an axis group. These states simplify the overview of complex movement

procedures and make it easier to handle error situations.

GroupMoving

STOP

GroupStoppingGroupErrorstop

GroupHomingGroupStandbyGroupDisabled

Figure 6: PLCopen axis group state diagram

Transitions between these states can be initiated by calling certain function blocks. The GroupErrorStop state can be

entered from any other state when an error occurs in the group on one of the group's axes.

StateDescription

GroupDisabledThe group is disabled. No axis group movements can be performed.

GroupStandbyThe group is switched on, but no movement is being performed.

GroupHomingHoming is active for the entire group.

GroupMovingAt least one axis in the group is moving. This movement can be caused by a

group motion command or a single-axis motion command.

GroupErrorStopThe group is in an error state. Movements are stopped.

GroupStoppingThe group is stopped.

Table 1: PLCopen axis group states

Motion control \ mapp Motion \ Libraries \ Core \ McAxGroup \ Technical data \ State diagram

The current state of the axis group can be read using the "PLCopenState" element of the "info" structure in the Mp

function blocks.

## Page 9

GENERAL INFORMATION ABOUT DRIVE COUPLING9

Exercise: Commissioning 2 axes

The objective of this exercise is commissioning 2 axes for further use when covering the topic of multi-axis coupling.

In preparation for subsequent exercises, one axis (later used as master) should be able to be switched on and off

independently of the other axis. By manually turning this axis (if possible), interesting effects can be shown.

Two axes must be configured and assigned to a piece of hardware

•

It must be possible to carry out the necessary preparatory work (switching on, homing)

•

2The following possibilities exist:

Commands sent to individual axis groups via the mapp Cockpit environment3

°

Commands with one MpAxisBasic function block per axis from the MpAxis library in a PLC program

°

Commands with function blocks from the McAxis library (MC_Power, MC_Home)

°

Optional exercise: Creating axis groups

The objective of this exercise is to simplify commissioning of the axes for multi-axis coupling.

In addition to the previous exercise, there is also the possibility to prepare the necessary axes for the other topics.

Configuration in the Configuration View - mapp Motion for axis groups

•

Select the name for the new administrative axis group

•

Assign already created axes to this axis group

•

Commands with function blocks from the McAxGroup library are available (MC_GroupPower,

•

MC_BR_GroupHome_N)

Motion control \ mapp Motion \ Libraries \ Core \ McAxGroup \

2.2Purely Virtual Axis

In addition to the virtual axes on ACOPOS servo family devices and the simulation of an ACOPOS with virtual and

real axes, there is also the possibility to use a purely calculated axis on the PLC. All these virtual axes can be used for

coupling.

Figure 7: Purely virtual axis (gAxis02) is calculated on the PLC, gAxis01 is executed on the ACOPOS

Each axis created in the Configuration View can also be executed as a virtual axis on the PLC.

To do this, a configuration for a "Purely Virtual Axis" must be created in the Configuration View as a replacement for

the hardware connection.

2Only select one possibility with libraries for commissioning

3See next exercise

## Page 10

10AXIS COUPLING: MAPP AXIS  TM417

Figure 8: Configuration file from the Tool Box catalog for the Configuration View

Figure 9: Assignment of an axis reference as a Purely Virtual Axis in the Configuration View

The axis reference is then used with mapp function blocks that are described in the Automation Help.

Motion control \ mapp Motion \ Programming \ Application program \ Libraries \ Technology \ MpAxis \

Motion control \ mapp Motion \ Programming \ Application program \ Libraries \ Core \ McAxis

Further possibilities in comparison

The Purely Virtual Axis is the simplest way to create a calculated axis.

This offers the standard movements that are possible on a PLC (without end positions).

The processing cycle is task class 1 here, and a higher performance requirement for the PLC results.

Another variant would be to use a virtual axis available on the ACOPOS drive.

This is also not subject to limits and is calculated in a 400 µs cycle. When coupling via POWERLINK, the cycle time of

POWERLINK must be taken into account.

The PLC is not loaded down in terms of computing time.

However, an ACOPOS can also be added to the simulation. Here the simulated real axis would also have the possibility

to run a load simulation. This ranges from simple consideration of lag errors (no high accelerations) to a model with

a 2-mass oscillator.

The processing time is again dependent on task class 1 here, and a higher performance requirement for the PLC results.

## Page 11

LINEAR AXIS COUPLING 11
3 Linear axis coupling
The simplest form of axis coupling is linear axis coupling.
It defines the movement of the slave to the master using an integer ratio.
3.1 mapp Cockpit: Use without additional programming effort
The linear axis coupling can already be used during commissioning.
If a mechanical object has to be moved with two or more motors, a gear coupling (usually with a 1:1 ratio) can be used.
The reasons for such an application include physical limits, dynamics, the smallest possible dimensioning or costs.
With commissioning environment mapp Cockpit, the "Gear In" functionality can be used here.
Later in this training module, this functionality can also be used in the program with function block MC_GearIn from
the McAxis library.
This application is intended to provide an easy initial introduction to position coupling.
Diagnostics and service \ mapp Cockpit \ Web-based HMI \ Components \ Common view
Starting the coupling
The "Execute" field is used to initiate the coupling according to the parameters. In order to do this, the master and the
slave axes must be switched on and homed. This can be done either from mapp Cockpit or via the program.
The state of the master axis is not affected by the coupling.
End the coupling
In the mapp Cockpit, the coupling can be canceled in various ways.
The "Abort" button can be used to stop the slave axis
•
You can also use the "Stop" or "Power off" command to stop the slave axis.
•
To change to another slave movement, the commands beginning with "Move..." or "Torque Control" can be used.
•

## Page 12

12AXIS COUPLING: MAPP AXIS  TM417

Exercise: Simple linear axis coupling

The objective of this exercise is to implement the aforementioned options for the first coupling in the mapp Cockpit.

A linear axis coupling between two axes should now be established without additional programming effort.

Prepare the axes (switch on, home)

•

Open the mapp Cockpit

•

Select "Gear In" from the command area of the slave axis

•

Specify the master axis

°

Define RatioNumerator and RatioDenominator as a full rotation (e.g. 360)

°

Assign acceleration, deceleration and maximum master speed (e.g. 10 measurement units/s2 or -/s)

°

Perform by pressing "Execute"

•

If a change occurs, restart the update by pressing "Execute"

•

3.2Axis coupling program

Once all tests have been carried out during commissioning, experience gained

can be incorporated into the program. A favorable constellation of parame-

ters for the process can be stored using the corresponding blocks.

Function block MC_GearIn from the McAxis library can be used for linear axis

coupling as already known from mapp Cockpit.

As in mapp Cockpit, the ratio can be set using the RatioNumerator and Ratio-

Denominator, as well as the coupling to the setpoint or actual position ("Mas-

terValueSource").

Figure 10: Function block interface for linear axis

coupling

Motion control \ mapp Motion \ Programming \ Application program \ Libraries \ Core \ McAxis

Starting the coupling

In function block MC_GearIn using the "Execute" input.

For more details, see the Automation Help for the function block.

End the coupling

In the program, the coupling can be canceled in various ways.

Input "Stop" of function block MpAxisBasic can be used to stop the axis coupling. (MpAxis library)

•

Function block MC_Stop, MC_Halt (McAxis) or MC_GroupStop (McAxGroup, if an axis group has been defined)

•

can be used to stop the axis coupling.

Input "Power" (MpAxisBasic - MpAxis), function block MC_Power (McAxis) or, if an axis group has been defined,

•

function block MC_BR_GroupPower (McAxGroup) can also be used to stop the axis coupling.

As an alternative, each new movement on the slave cancels the synchronous movement. (McAxis: MC_MoveXxx,

•

MC_TorqueControl, etc.)

Exercise: Linear axis coupling in the program

The objective of this exercise is to implement the aforementioned options for the first coupling in the PLC program.

A linear axis coupling between two axes should now be created in the program.

## Page 13

LINEAR AXIS COUPLING 13
Prepare the axes (switch on, home)
•
Add function block MC_GearIn from the McAxis library
•
Define RatioNumerator and RatioDenominator as a full rotation (e.g. 360)
•
Assign acceleration, deceleration and maximum master speed (e.g. 10 measurement units/s2 or -/s)
•
Execute by setting "Execute" in the Watch window
•
If a change occurs, set "Execute" to FALSE and send a positive edge to the input for the update process again.
•
Optional: Switch off the master axis when setpoint coupling is active. Observe the behavior of the slave axis by
•
manually rotating the master axis if possible (differences to a coupling with the master axis switched on).
Repeat tests with actual value coupling and determine differences or similarities.

## Page 14

14 AXIS COUPLING: MAPP AXIS TM417
3.3 Introduction to cam automats
The cam automat allows event-controlled coupling of electronic cams.
The example for a capsule filling machine (see "Introduction" on page 15) moves step-by-step through a more in-
depth explanation of cam automat functions.
A cam automat is already used in the background for simple gear couplings. The automat configuration
used is described in detail in section "Additional information" of the function block description in Au-
tomation Help.
Types of cam automat
There are 2 types of cam automats4, which are calculated for devices from the ACOPOS servo family and a general cam
automat on the controller.
In the following sections, ACOPOS-specific cam automats are used in the exercises. This is characterized by the fact
that the functionality is processed decentrally on a device from the ACOPOS servo family. This reduces the load on the
controller in terms of computing time.
Required information (e.g. master axis) must be available on the device. If mapp Motion devices or the configuration
are used in Automation Studio, this is automatically done in the background to allow coupling.
Another option is a general cam automat. This makes it possible to couple to variables on the controller and does not
require any information in a device from the ACOPOS servo family. The design and structure are the same, but there
are differences in the "Advanced parameters" (reduced range of functions).
The demand for computing power on the PLC is higher with the general cam automat.
This means that a feature like the cam automat can also be used without a position controller5 on a device.
4 At the beginning of the configuration file (Configuration View) of a cam automat
5 Integrated in ACOPOS servo family

## Page 15

LINEAR AXIS COUPLING15

3.3.1Introduction

First, let's look at a procedure using a capsule filling machine.

The product transporter acts as the master axis. The slave axis closes each plastic container with a cap.

A high-speed digital input (trigger) detects if a product is present. If no product is present, then the slave

remains in standstill. Otherwise, the container is closed with a cap.

Figure 11: Starting point for the slaveFigure 12: Cap applied

Necessary steps for implementation (with cams)

In the following, we will consider how this example could be implemented. First, we need two cams.

Cam 1, which keeps the slave at a standstill when a con-Cam 2, which is required for the application of the cap.

tainer is not present.

Figure 14: Second cam for the capsule filling process

Figure 13: First cam for keeping the slave at a standstill

First, the two cams must be transferred to the drive. Then a control program must be used to check if a trigger signal

has been received:

If so, you would need to change to cam 2 with the MC_BR_CamAutomatCommand (McAxis) function block.

•

If there is no signal, cam 1 should be executed automatically.

•

Task of the cam automat

A more efficient method is to let the drive decide on its own which cam should be processed based on the current

process situation. Thus the complexity in the control program is reduced and faster response times are achieved.

The cam automat was created to meet these demands. It is initialized and parameterized on the corresponding slave

drive, where it is then processed independently. This keeps the CPU load comparably low, even when a large number

of axes are in use. The running process benefits from minimal response times. There are also many ways to intervene

in the processing of active cam automats.

In the following section, the structure and the functionality of a cam automat are shown using the example.

## Page 16

16AXIS COUPLING: MAPP AXIS  TM417

3.3.2Structure and functionality

The example can be structured in the cam automat as follows. In the process, automat states are defined, which are

switched between using change events. In the automat states, cams are followed or compensating movements are

performed.

Figure 15: Possible cam automat structure for the capsule filling machine

Automat states

The two cams in the capsule filling machine example are now each packed in a specific state. These are called cam

automat states. The following states result in the example:

State 1 ()State1

•

The slave does not execute any movement in this state because there is no product. The master does however

(product transport).

State 2 ()State2

•

The capping process is completed in this state. Both axes are moving.

State 0 ()State0

•

This state is optional and can be used as a starting state or a waiting state. This state does not have a cam assigned

to it.

In terms of the master, the length of a state corresponds to the time it takes to transport a product, i.e. waiting for

the next product.

In terms of the slave, the length of a state corresponds to the distance required to complete the capping process.

Up to 15 states can be defined. The base state (state 0) is a special case. Here it is not possible to assign a cam or a

compensation gear. Only the desired change events have to be defined for the base state. This serves more or less as

an initialization or waiting step.

For each automat state, except State0, the following can be defined:

A cam, which is first transferred to the drive before it can be used (via

•

channel feature on hardware). Then this can be used in any state.

Optionally, a compensation gear (compensation mode) that corre-

•

sponds to an automatically calculated curve can be used. This offsets

state transition position and velocity differences. It ensures a continu-

ous connection of the cams.

Figure 16: State with compensation and cam

It is possible to disable the compensation gear. If this is done, then the state only contains the cam itself.

If a compensation gear is used in an automat state, then it will always be processed before the corre-

sponding cam in the state.

ParameterDescription

CamThe cam for the state is selected here. Either a predefined, user-created or manually trans-

ferred cam.

MasterFactorMasterFactor and SlaveFactor define the master and slave-side scaling of the selected cam.

Table 2: Overview of basic parameters for state without compensation

## Page 17

LINEAR AXIS COUPLING 17
Parameter Description
SlaveFactor
Table 2: Overview of basic parameters for state without compensation
Multiple change events can be specified for each state. Each state provides the "Event" array element for defining the
change events.
Depending on which change events were used, the additional specification of optional "advanced" parameters is re-
quired. (e.g. Repeat counter init: To repeat states for a definable quantity)

## Page 18

18 AXIS COUPLING: MAPP AXIS TM417
Predefined cams
Some predefined cams are already available on the drive and do not have to be transferred there.
For this, "Predefined cam" must be selected in the state (not CamTableID or Name).
The following describes 2 possibilities:
Selection Description
This predefined linear cam can be used with a master and slave length of one unit as the
One-to-one cam cam when configuring the automat. This can be used with gauge factors to produce any
m:n straight line.
This predefined point cam can be used as the cam when configuring the automat. This
point cam can only be used when compensation mode is enabled. It cannot be used in
states with Compensation mode = "No compensation".
Zero cam
The master and slave interval length of this predefined cam is zero. However, the slope of
the curve is not zero, but can be set using the multiplication factors. This allows you to cre-
ate applications that only require one compensation procedure without a cam.
Table 3: Overview of predefined cams
Change events
A change event is a defined event that should cause a change of state (e.g trigger event "Input trigger1", or reaching
the end of the state "State end", etc.).
The event trigger is triggered on devices from the ACOPOS servo family that are connected to the "Trig-
ger" hardware inputs. It is edge-sensitive and must occur in the respective state. Signals in other states
are ignored.
The user must also define when the change should take effect. For example, it can take place at the end of the state
(End of state) or immediately (Immediately) when the event occurs. The new state that should be changed to must
also be defined. The end result is an entire series of cam automat states. Two change events have been defined for
each of the two states in our example.
At least one change event must be defined for a state to induce a state change. Up to five change events are available
for each state.
A change event has the following properties:
Type
•
Target state (Next state)
•
Transition
•
The type determines which event triggers a state change. This can be an "external" signal trigger or the end of the
current cam.
The target state determines what state should be activated next. The current state can also be selected here for rep-
etition.
The transition defines the time at which the state change occurs, which is triggered by the corresponding event. This
means that the actual state change can be placed at the end of the cam when using a trigger (digital input on the
device) as change event, which occurs according to circumstances in the cam.
Transition Description
Immediately The transition to the next state is executed immediately or at the beginning of the next
sampling cycle.
End of state The change into the next state is not executed before the end of the current state, i.e. after
the compensation gear and the cam have moved.
Table 4: Overview of the defined event attributes

## Page 19

LINEAR AXIS COUPLING19

Cam automat sequence for the capsule filling machine

After the event-controlled start (start takes place from a certain master position) of the capsule filling machine in state

0, it switches to state 1.

The slave does not perform any movements in State1. The first bottle must

therefore be left out when starting the machine. If a trigger signal (Input trig-

ger1 ...) is detected during processing of State1, then the machine changes to

State2 at the end of State1 (End of state), at which point the capping process

is then executed.

During execution of State2, one bottle is capped. If another trigger signal is

detected during this state it is repeated.

Figure 17: Capsule filling machine

If a product is not present, State2 runs completely to the end (state end) without a trigger signal having occurred,

then the machine switches to State1. Then the slave drive does not perform any movement. The cam automat remains

in State1 until a trigger signal is received again. The automat is switched to State2 in which the capping process is

continued.

It is necessary to ensure that the trigger signal arrives on time within a product interval. If multiple signals are sent,

only the first trigger signal on the end of the state is evaluated.

According to this circuit diagram, different cams can be sequenced as in a state machine.

First, the cam automat parameters are set. Then it can be started in any state. The cam automat runs through the

individual states in accordance with the set change events and subsequent states.

Figure 19: Example sequence of cam automat states

Figure 18: Sequence of cam automat states

## Page 20

20AXIS COUPLING: MAPP AXIS  TM417

3.3.3Cam automat configuration

The cam automat is configured in the Configuration View. There, all options and possible settings are clearly laid out

in a structure. Up to 14 states with 4 change events each can be configured.

Figure 20: Cam automat

as an axis feature in the

Configuration View

Figure 21: Editor for a cam automat

If the cam automat has been defined, it can now be assigned to a piece of hardware. This means that the cam automat

is transferred automatically and can then be controlled from the program.

Figure 22: The cam automat is assigned to a piece of hardware as an axis feature

## Page 21

LINEAR AXIS COUPLING21

Function block MC_BR_CamAutomatCommand from the

McAxis library can be used for control.

With this function block, the cam automat can then be ac-

tivated and thus takes over control of the slave axis.

The defined relationship between master and slave is

then initiated and can still be manipulated, e.g. by signals

on the function block input. (If the transitions have been

defined in the cam automat)

The coupling ratio can be canceled in various ways.

Using function block input "Stop"

•

Using "Stop" on function block MpAxisBasic (MpAx-

•

is), MC_Stop (McAxis) or MC_GroupStop (McAx-

Group)

Using input "Power" (MpAxisBasic - MpAxis), func-

•

tion block MC_Power (McAxis) or, if an axis group has

been defined, function block MC_BR_GroupPower

(McAxGroup).

As an alternative, each new movement on the slave

•

cancels the synchronous movement. (McAxis:

MC_MoveXxx, MC_TorqueControl, etc.)

Figure 23: Function blocks for controlling the cam automat

Motion control \ mapp Motion \ Libraries \ Core \ McAxis

## Page 22

22 AXIS COUPLING: MAPP AXIS TM417
Exercise: Simple cam automat - gears
The objective of this exercise is to simulate the gear function "Gear In" (already known from mapp Cockpit).
For this purpose, a simple cam automat with one state should be created. Coupling from a standstill is sufficient and
no special requirements have to be met for this simple example.
Create the configuration (axis feature in Configuration View) and define a status
•
In the "Advanced parameters", set the start state to 1 (0 is not used)
•
Specify the master axis
•
Set the cam to "Predefined" and "One to one cam". This corresponds to a straight line.
•
Define a coupling ratio for one revolution without compensation and factors (e.g. 36000)
•
Assign the cam automat to the hardware where the slave is being run (axis feature)
•
Building and transferring
•
Start with function block MC_BR_CamAutomatCommand and test the coupling
•
Start master movement at constant speed
•
Exercise: Actual position coupling via cam automat
The objective of this exercise is to get to know the functions provided by function block MC_GearIn.
Adjust the corresponding parameter in "Common parameters - Master"
•
Save and transfer
•
Operate cam automat with function block "MC_BR_CamAutomatCommand"
•
Manual intervention on the master axis (if possible) with active coupling ratio
•
Above all, the difference to the "Set position" setting must be determined when the master is moved "from the
outside".

## Page 23

DELAY TIMES23

4Delay times

If the position of the master axis is transferred over the network, this results in a time delay. Without accounting for

this delay, a master-slave coupling would never really work synchronously.

This time offset manifests itself as a speed-dependent error. (The time offset between 2 axes always remains the

same, but different speeds result in different position offsets between the axes.)

This problem can be solved with position setpoint coupling that has a total delay time in the master axis. In this way,

the position setpoint is calculated and transferred, but only converted into a movement after the transfer time has

elapsed.

This is a device-specific parameter that only needs to be set for the master axis.

It is calculated from the maximum total delay of the slave, double the POWERLINK cycle time (if linear interpolation on

the POWERLINK is used by default) and an ACOPOS processing cycle (400 µs) for receiving the data.

If, however, actual position coupling is configured, compensation is not possible. The delay on the network would re-

main. It would have to be compensated for using a precalculated value on the slave, which, however, would not guar-

antee synchronous operation of the axes, e.g. in the case of a dynamic change of direction by the master.

Here are some examples to illustrate the setting for the total delay. These examples do not necessarily represent a real

and meaningful arrangement of the hardware, but provide insight into possible topologies.

Cross-device coupling

In this figure, the delay time for the master is specified.

The data is first transferred on the network, and after a

delay time corresponding to the communication path, the

master and slave run synchronously (e.g. from a stand-

still).

During operation, changes in speed or direction of move-

ment on the motors take place simultaneously.

Figure 24: Master is an ACOPOS axis and the network results in a delay

time

## Page 24

24AXIS COUPLING: MAPP AXIS  TM417

Virtual master and cross-device coupling

If a virtual axis is used as master on a device from the

ACOPOS servo family, there is no delay to the slave axis on

the same drive. The ACOPOS device always calculates the

virtual axis first and then the real axis.

On the other hand, the delay that occurs over the network

must be maintained again on the device with the virtu-

al master to ensure actual synchronous operation of all

slave axes.

Figure 25: The master is a virtual axis and the delay time settings are the

same

Slaves as master (cross-device coupling)

To conclude with a more complex example, a slave axis can

also be the master axis of one or more slave axes.

The starting point here is the innermost master-slave cou-

pling group (on the right in the picture).

The outer master-slave group (on the left in the picture)

gets delay time 1 (Delay 1 in the picture) as start value and

delay time 2 (Delay 2 in the picture) is added.

This results in synchronous movement of all axes.

Figure 26: A slave of a coupling is the master axis of another coupling

## Page 25

DELAY TIMES25

Purely Virtual Master

Setting the total delay time is not necessary

To avoid having to set the total delay, a Pure Virtual Axis

can be created on the PLC. Delays caused by the network

have the same effect on all axes, so the behavior is the

same on all axes and does not have to be compensated.

Figure 27: Uniform delay on all slaves, delay time does not have to be

configured separately

Coupling within a device

In this example, the first channel of a device from the

ACOPOS servo family is used as the master. The following

axes are calculated in the same cycle in the order 1 to 3.

A delay should only be set if channel 2 or 3 is used as the

master axis. Then the channels before would be delayed

by one ACOPOS cycle (normally = 400 µs).

Figure 28: The first channel is handled first, all subsequent channels can

use the position immediately

## Page 26

26AXIS COUPLING: MAPP AXIS  TM417

Setting the total delay time

After calculation, the total delay time is usually set on the master axis or on the device where it is necessary to delay

the position setpoint in relation to the actual position.

This device-specific parameter can be found in the Physical View and is set by assigning the axis reference of the axis

to be delayed (usually the master axis).

Figure 29: Setting and compensating for the delay time as device parameter (here still unchanged)

Optional exercise: Calculating delay times

The objective of this exercise is to get a feeling for the dependencies of delays in signals.

The total delay time of a master axis should be calculated.

Determine the total delay time of the slave (hardware-specific parameter under Controller - Position via Physical

•

View)

Determine POWERLINK cycle time (POWERLINK configuration via Physical View)

•

Determine ACOPOS cycle time (user's manual, ACOPOS default setting = 400 µs)

•

Calculate result: Slave delay time + 2x POWERLINK cycle + ACOPOS cycle

•

## Page 27

NON-LINEAR AXIS COUPLING27

5Non-linear axis coupling

Coupling using cams

To implement dynamic, nonlinear movements, the B&R drive solution offers

the option of using electronic cams for axis coupling. These cams can be cre-

ated by the user in Automation Studio.

Electronic cams can be used in many different ways.

The reference to the slave position (y-axis) is defined for each master position

(x-axis).

Figure 30: Mechanic cams (Silberwolf /

de.wikipedia.org)

Cams can be used very effectively for coil winding machines. Separate axes are used to control the feed

rate, curvature and slope, respectively. This makes it possible to create any shape needed (slopes, cones,

etc.).

The condition for starting and stopping the cam coupling is the same as the options already mentioned in section see

"Cam automat configuration" on page 20 for a cam automat.

5.1Introduction

In the cam diagram, we see the master position value in the horizontal direc-

tion and the slave position in the vertical direction. The cam assigns a respec-

tive slave position value to each master position value within a defined range

(master period). The slave drive must follow this characteristic curve while

the drives are actively coupled.

Figure 31: Cam as a position relationship

between the master and the slave positions

The master position is converted to a corresponding slave position via the cam. This allows the master to move in both

directions. The slave drive is "tied" to the master via the cam.

This means that the velocity and acceleration values of the slave drive result from the velocity and acceleration of the

master in connection with the characteristic curve of the cam.

For the entire characteristic curve of the cam, a check must be performed to determine whether the slave

drive can handle the velocity and acceleration values that might occur.

Assumed: The master signal constantly changes, which corresponds to a

steady movement of the master axis.

Critical ranges (with maximum values for the slave velocity or accelera-

tion) are represented in the cam by the maximum slope (→ velocity as first

derivative of the position) and the maximum slope change (→ accelera-

tion/deceleration as second derivative of the position) according to the

position comparison.

Figure 32: Maximum velocity

that occurs on the slave drive

5.1.1Creating cams

Automation Studio has a powerful cam editor for creating cams. Cams can be edited in the cam editor as soon as they

have been inserted into the project.

Cams are added as software objects from the Toolbox to the Logical View in Automation Studio. They are then trans-

ferred to the controller and can be selected at runtime in the drive application.

## Page 28

28AXIS COUPLING: MAPP AXIS  TM417

Figure 33: Cam object "Cam" in the Logical View

It is generally advisable to create standardized cams. These have endpoints with a ratio of 1:1 or 1:0. This

allows the cam to be stretched in unit scaling as needed using corresponding function blocks or the cam

automat. As a result, they can be used on a wide range of axes with different scaling.

5.1.2Editing a cam

The cam editor in Automation Studio is a full-featured tool that helps to create and adjust exact cams for the various

different coupling tasks.

The following properties can be configured in the cam editor:

General properties

•

Color settings

•

Extension

•

Display options

•

Labels and formulas

•

Characteristic values for curves

•

Notations in the diagram

•

The following functions are provided in the cam editor:

Fixed points

•

Synchronous sections

•

Interpolation curves

•

Importing mechanical cams

•

It is now possible to define fixed points on the curve as well as synchronous sections (linear sections along the path of

the curve) to create a cam. The connection to a continuous cam occurs via interpolation curves, which are automatically

calculated by the cam editor.

A total of four fixed points and one synchronous section have been defined in the image. The cam editor automatically

integrates these definitions into a complete cam. It does this by calculating and displaying interpolation curves. The

user can even specify the form of the interpolation curves.

## Page 29

NON-LINEAR AXIS COUPLING29

Figure 34: Structure of a cam

Fixed points

A fixed point is a point in the cam for which the user defines the desired position of the slave axis in relation to a

specific position of the master axis.

The notation indicates whether position or time units should be used in the diagrams on the horizontal

axis (i.e. the master axis). The use of position is referred to as "mathematical notation". The use of time

is referred to as "physical notation" (comparable to a constant master velocity).

Therefore, in physical notation, the first derivative in the fixed point is equal to the velocity and the second

derivative in the fixed point is equal to the acceleration of the slave axis. The cam represents the path-

time diagram of the slave axis.

Synchronous sections

A synchronous section is a section in the cam where the user specifies a linear path for the master and slave positions.

A constant master axis velocity within a synchronous section also results in a constant slave movement. In other words,

the cam is linear (comparable to an electronic gear).

When using physical notation (master axis = time), the master position is entered as a time value. The

slope of the synchronous section corresponds to the velocity of the slave axis in this section. ( time→

passes evenly)

Interpolation curves

The section of a cam calculated by the cam editor to connect two defined elements (fixed points, synchronous sec-

tions) is called an interpolation curve.

After each new fixed point or synchronous section is entered, interpolation curves are automatically calculated and

displayed. The cam editor makes sure that there is exactly one interpolation curve between any two defined compo-

nents.

Likewise, when a fixed point or a synchronous section is deleted, any extra interpolation curves are also deleted.

The calculation ensures that the cam function and its first derivative are constant at the transition points

(e.g. the curves do not contain any jumps at the endpoints).

Various curve types can be selected for each interpolation curve to fine-tune the curve progression between the de-

fined areas (fixed points and synchronous sections). These provide different predefined shapes according to the type.

Specific curve progressions can be implemented using type-specific settings (turning points, joining points, etc.).

## Page 30

30AXIS COUPLING: MAPP AXIS  TM417

Importing mechanical cams

It is often necessary to replace mechanical cams with electronic ones or to reproduce content from a CAD system

electronically. The cam editor makes it possible to import and then export interpolation points, which allows calculated

curves to be reused in CAD, for example.

5.1.3Transferring cams

After the cams have been created and edited, they can now be used.

Cams are required on the device when  and must be transferred to it.using the ACOPOS-specific cam automat

A function block does not necessarily have to be used for this (but it is possible).

Using the Configuration View is the easier option here. This only requires a few steps to be carried out.

Grouping cams

Multiple cams are usually required, which can be grouped in a list and transferred together using mapp Motion. The

list can also only contain one cam and can be extended later.

The following steps are necessary with mapp Motion.

Figure 35: As an example, 5 user-defined cams were created in the Logical View

Figure 36: A cam list from the Toolbox catalog can be transferred to the Configuration View

Cam IDs are assigned in a cam list, which can also be specified for the cam automat directly as a number in any state.

Each of these cam IDs must be assigned uniquely for an ACOPOS device, and a maximum of 14 can be stored. (The

number of cams on the PLC is not limited.)

However, a cam can be used by a cam automat multiple times in different states.

## Page 31

NON-LINEAR AXIS COUPLING31

Figure 37: In this cam list, the cams are now defined with a unique cam ID

The cam list can only be assigned to a device using a feature.

To do this, a feature from the Toolbox catalog is added and assigned to the list.

Figure 38: Configuring a feature to enable transfer of the cam list

## Page 32

32AXIS COUPLING: MAPP AXIS  TM417

Device assignment for transfer

After the feature has been configured, the transfer can be configured via the assignment.

A cam list is a channel feature that is available to both a real axis and a virtual axis. A cam automat on the virtual axis

can also use the same cam.

Figure 39: Assigning the feature as a channel feature so that the list is transferred automatically after startup

Only one channel feature assignment is required for each channel where the cam list should be used.

This again confirms the recommendation to define the cams as "standardized" (integer endpoints as 1 or 0). Depending

on the state, the gauge factors (master and slave) can be used to adapt to the axis units and thus a cam can be used

for different states.

If a factor was defined indirectly for the measurement units (e.g. 0.1 = factor 10), this must be taken into

account here. Factors for strain on the cam are handled without units.

Exercise: Creating a cam

The objective of this exercise is to create a standardized bell-shaped curve (0 - 1 - 0) for a wide range of applications.

A master and slave axis should be coupled using a cam. To do so, a cam must first be created in the Logical View. The

period should include a full turn of the master and a full turn of the slave.

## Page 33

NON-LINEAR AXIS COUPLING33

Create a cam named "cam". The cam should look like the one in the graph.

•

Figure 40: Example for cam that has been named "cam"

When looking at the second curve (velocity) and third curve (acceleration), you will notice an  atacceleration jump

•

the second point. This is caused by the editor, which seeks to apply Velocity = Acceleration = 0 for each point.

If a constant acceleration is specified here ( can be  in the third diagram), then the movement willpointadjusted

take place with .much less jerk

Create a cam list in the Configuration View, assign the cam and specify a Cam ID.

•

Create a cam list feature in the Configuration View and add the previously created cam list.

•

Add and activate the previously created cam list feature in the device-specific configuration (Physical View) as a

•

channel feature.

The assigned cams are now automatically initially transferred to the drive each time the PLC is restarted.

•

Exercise: Axis coupling via cam

The objective of this exercise is to use the assigned cam on the slave drive.

The cam automat that has already been created will be adapted to use the cam.

Open the configuration of the cam automat in the Configuration View

•

Change from "Cam - Predefined" to a name or Cam ID

•

Adjust the master and slave factors to strain the cam to one revolution

•

All other parameters can be left as they are

•

Building and transferring

•

Start with function block MC_BR_CamAutomatCommand and test the coupling

•

Start master movement at constant speed

•

## Page 34

34AXIS COUPLING: MAPP AXIS  TM417

Optional exercise: Update gauge factors

The objective of this exercise is to change parameters at runtime.

The gauge factors (master and slave factor) of the standardized cam are avail-

able here.

Note: If a factor was defined indirectly for the measurement units (e.g. 0.1 =

factor 10), this must be taken into account here. Factors for strain on the cam

are handled without units.

Initialize the cam automat via the Configuration View

•

MC_BR_CamAutomatGetPar_AcpAx (library McAcpAx) for writing to the

•

cam automat structure from the configuration

Change the master and slave factor in the cam automat structure in the

•

Watch window

MC_BR_CamAutomatSetPar_AcpAx (library McAcpAx) with the same

•

structure for updating the changed values on the drive

Figure 41: Sequence of the function blocks

Motion control \ mapp Motion \ Libraries \ Core \ McAcpAx

5.2Compensation gear

For each state in the cam automat, a compensation gear can be used.

The compensation gear is an automatically calculated curve that compensates for position, speed or acceleration

differences during a state transition and maintains a continuous connection of the cams. The parameters required for

this are available in each state. (see 3.3.2 "Structure and functionality" on page 16)

Figure 42: Displays the functionality of a compensation gear

The image shows compensation between two consecutive states (cams). If compensation is used in a state, then the

compensation movement is always performed before the cam of the state.

These are the base parameters that define the compensation:

Compensation variant (compensation mode)

•

Master compensation distance (master distance)

•

Slave compensation distance (slave distance)

•

The different compensation gear modes provide possibilities for compensating path as well as velocity differences.

## Page 35

NON-LINEAR AXIS COUPLING35

Configuration in Automation Studio

The compensation gears are configured in the respective cam automat state. The state always starts with a selectable

compensation gear and ends in a cam.

Figure 43: The compensation gears that can be selected in the cam automat configuration editor (Configuration View)

Exercise: Compensation gear

The objective of this exercise is to put the compensation gear into operation.

A simple configuration of the cam automat for the gear can be used for this purpose or the coupling can be adapted

via the cam.

Up to now, coupling has always taken place from a standstill. However, if the master axis is moving, a state can be used

for speed compensation. This is done to avoid an acceleration jump on the slave axis.

Activate state where speed compensation can be defined

•

Here, it is useful to define a master distance in which compensation should be completed: e.g. Jolt minimal veloc-

ity mode with set master distance

Under "Advanced parameters", set the start state to speed compensation

•

When speed compensation has reached the end of the state, the previously tested state should be restored (gear

•

or cam coupling).

Building and transferring

•

Start master movement at constant speed

•

Start with function block MC_BR_CamAutomatCommand and test the coupling

•

## Page 36

36AXIS COUPLING: MAPP AXIS  TM417

6Dynamic phase shifting

The phase shift or offset shift changes the position on the slave axis when coupling is active.

With a phase shift, the master position is shifted with respect to the actual physical position.

With an offset shift, the slave position is adjusted directly.

The shift is only "performed" on the slave, the master is not affected by this. The shift remains until another shift

command changes it.

The phasing and offset functions can be used for each coupling, regardless of whether a gear ratio or a cam has been

started.

Functionality of phase shift

The slave position is determined by the position of the coupling master and the coupling ratio (linear or via cam).

The phase shift function generates a value for an additive element or additive

master axis. This element is added to the actual master position. The result-

ing value is then applied to the master side of the coupling ratio.

Figure 44: Master - Coupling ratio - Slave

When the phase shift is activated, the specified target

value for the phase shift is approached using a constant

motion profile. The motion profile position is continual-

ly added to the actual master position, which prevents

jumps in position on the slave axis. The master axis is not

affected by this action at all.

Figure 45: Targeted phase shift generated by the phase shift input

This changes the position specification for the coupling slave. A targeted phase shift can therefore be implemented.

This smooths out the additive master axis value that is generated.

The phase shift function can be implemented in order to separate products. After sheets of cardboard

have been cut, they lie end-to-end on a conveyor belt. They are then transferred to a second conveyor belt.

As each sheet reaches the second belt, a phase shift can be implemented. This creates a gap between

the products, which is required for further processing.

The phase shift is best suited to situation where the slave cannot be directly influenced.

When transporting a material that could be damaged during correction, for example. When correcting the master path,

the coupling ratio is taken into account and a change is not made directly (compared to offset shift).

In the event of a phase shift, the slave sees the correction as an adjustment to the actual master position (which

remains unaffected). The adjustment is made to the speed of the slave.

## Page 37

DYNAMIC PHASE SHIFTING 37
The resulting slave position is directly dependent on the coupling ratio. The gear ratio for an electronic
gear has the following effect on the result:
Gear ratio = 1:5 (Master:Slave)
Master-side shift = 2000 units (additive master axis)
Slave-side shift = 10,000 units
Offset shift in comparison to phase shift
The offset shift has a direct effect on the current slave position.
The shift is suitable for slave axes that can be moved directly (e.g. adapting a measured value to the actual height of
a product, adjusting a movement to product intervals or direct adjustment of a conveyor belt).
The conversion is also done using an "additive element" (also called "additive slave axis"). This element is added to the
actual slave position and the change is accepted without taking the coupling ratio into account.
Exercise: Phasing for linear axis coupling and cam
The objective of this exercise is to influence an axis coupling. Both linear (with fixed ratio) and a cam (e.g. bell-shaped
curve).
The speed setpoint (ParID 114) is to be recorded while the shift is performed using MC_PhasingRelative or
MC_PhasingAbsolute (McAxis library). A sufficiently large change must occur.
The phase shift is additive to the current movement. The phase shift can also be tested while the master
axis is idle (if it is time-dependent).

## Page 38

38AXIS COUPLING: MAPP AXIS  TM417

7Cam automat design

The best way to design a cam automat is to take a sheet of paper and draw the movements of the corresponding

master and slave axis. This allows you to develop a feeling for how they correlate and for important dependencies.

The example of a labeler can be used to demonstrate how this could be done.

A labeling machine usually has adhesive labels on a roll of film that have to be applied at a certain location (e.g. on a

box). The speeds of the two motors are usually not the same, which means that a synchronization process must take

place in order for the label to be applied correctly.

If products have been sorted out in between (i.e. a box is missing), due to poor quality or other criteria, the label strip

should be stopped.

Basic considerations

There are boxes that have to be synchronized to (section marked "S"), and they are usually transported e.g. on a con-

veyor belt at the most constant speed possible. The figure shows synchronous sections on the master axis (x-axis)

where a constant slave speed should be used. This is where the labels should be applied.

The positions in between do not need to be defined in more detail.

On the slave axis (y-axis), the labels should now be correctly aligned to the same synchronous sections (again marked

"S") for attaching the labels. This already results in part of the movement range.

The second position, if necessary, is the waiting position (marked "W"). Slow down around this point if the next box

is still too far away.

Figure 46: Initial considerations during the design process (master and slave)

## Page 39

CAM AUTOMAT DESIGN39

Motion

In the next step, you can now think about how the movement will look.

It is important to determine the state of a cam automat. This consists of a cam and possible compensation.

In this example, the cam can form the synchronous sections of the two axes, and compensation is to take place before

it.

The distances between the boxes result in the master distance (divided into compensation and synchronous section

= cam).

The distances between the labels result in the slave distance (divided into compensation and label midpoint = syn-

chronous section = cam).

If there are no boxes (products), the master axis will move, but the slave with the labels will stop.

If all boxes are available, a movement for synchronization of the slave to the master (conveyor belt) must first be

defined. This allows continuous movement to then take place.

Figure 47: No box, only definition of the master distance

Figure 48: Synchronization and movement (one state per color)

Transitions

Now that the rough procedure has been defined, the finer points can be worked out.

In this case, thinking through certain scenarios and defining a sequence of movements is very helpful. When to slow

down, when to move synchronously again.

This defines the transitions between starting synchronization, synchronized movements and moving to a standstill.

Exercise: Cam automat - Flying saw

The objective of this exercise is commissioning a finished cam automat configuration together with the control pro-

gram on the PLC. As a final example, a possible application should be envisioned.

Import the cam automat configuration and the task with MC_BR_CamAutomatCommand

•

Commission functionality in the Watch window for the program

•

Analyze the movement pattern with the "flying saw" application example

•

## Page 40

40AXIS COUPLING: MAPP AXIS  TM417

8Summary

The mapp Motion function blocks from the McAxis, McAcpAx and McAxGroup libraries contain user-friendly blocks for

controlling a drive coupling or axis group.

The individual components are designed in accordance with the PLCopen Motion Control standard and feature a uni-

form design with regard to function use.

The "electronic gears" allows linear position couplings of the corresponding axes to be implemented.

Corresponding functions are also provided for nonlinear position couplings using electronic cams. The application

program controls interactions between multiple cams.

Cams can be created using the cam editor in Automation Studio. Settings in the Automation Studio cam editor make it

easy to tailor a cam to the demands of a particular process. Predefined cams on the drive expand the range of functions.

Figure 49: Schematic illustration of a cartoning application

The cam automat is an extremely powerful tool for effectively linking cams. The necessary sequences are completely

predefined. Initialization of the automat structure and control of the automat mode can be handled using clear and

organized function blocks. Once the cam automat is started, the defined sequences are independently processed on

the drive. This reduces the load on the application program and results in a very fast, event-controlled positioning

sequence.

The multi-axis functions are subject to the effects of the states in the PLCopen Motion Control state diagram. The user

is provided with necessary information for planning the sequence here.

## Page 41

AUTOMATION ACADEMY41

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

## Page 42

42 AXIS COUPLING: MAPP AXIS TM417

## Page 43

AUTOMATION ACADEMY 43

## Page 44

B&R

Industrial Automation GmbH

A member of the ABB Group

B&R Straße 1

5142 Eggelsberg, Austria

office@br-automation.com

t +43 7748 6586-0

f +43 7748 6586-26

br-automation.com

V3.0.0.0 ©2023/10/03 by B&R, All rights reserved.

All registered trademarks are the property of their respective owners.

Subject to technical changes without notice.