## Page 1

TM470

Axis coupling: Cam

Automat

## Page 2

2 AXIS COUPLING: CAM AUTOMAT TM470
Requirements
Basic knowledge B&R Motion knowledge
SEM415
Trainings
SEM417
Automation Studio 4.10 (or higher)
Software
mapp Motion Technology Package
ETAL210
Hardware
ETAL410

## Page 3

TABLE OF CONTENTS 3
Table of contents
1 Introduction.........................................................................................................................................................4
1.1 Learning objectives..............................................................................................................................4
1.2 Safety notices and symbols...............................................................................................................4
2 General information...........................................................................................................................................6
2.1 Axis coupling.........................................................................................................................................6
2.2 Cam automat........................................................................................................................................6
2.3 Use cases...............................................................................................................................................7
3 Getting started...................................................................................................................................................9
3.1 Configuration file: Axis feature.......................................................................................................10
4 Linear coupling.................................................................................................................................................12
4.1 Linear coupling via mappCockpit...................................................................................................12
4.2 Gear in via cam automat..................................................................................................................12
5 Diagnostics........................................................................................................................................................14
6 Coupling via cams............................................................................................................................................16
6.1 Creating cams.....................................................................................................................................16
6.2 Using cams..........................................................................................................................................17
6.3 Stretching factors..............................................................................................................................17
7 Master value source.........................................................................................................................................19
7.1 Axis position as source......................................................................................................................19
7.2 Other master source types...............................................................................................................19
8 Events and transitions....................................................................................................................................21
8.1 Event types..........................................................................................................................................21
8.2 Transition.............................................................................................................................................22
8.3 Synchronous update.........................................................................................................................22
8.4 Next state............................................................................................................................................22
9 Adjustment during runtime...........................................................................................................................24
10 Compensation.................................................................................................................................................26
10.1 Compensation Types.......................................................................................................................26
10.2 Position compensation...................................................................................................................27
10.3 Speed compensation......................................................................................................................30
11 Advanced topics..............................................................................................................................................32
11.1 Advanced parameters......................................................................................................................32
11.2 Calculating cams during runtime..................................................................................................35
11.3 Error handling and restart..............................................................................................................36
11.4 ACOPOS function blocks................................................................................................................36
12 Summary..........................................................................................................................................................39

## Page 4

4AXIS COUPLING: CAM AUTOMAT TM470

1Introduction

The cam automat is a sequential machine with parameter settings which allows an effective link to be made for elec-

tronic cams, compensation functions and event handling. There are many practical applications for these solutions,

such as synchronous cutting procedures, dynamic transfer processes and flexible length allocation.

Easy to use configuration objects and function blocks are provided for comprehensive handling of these functionali-

ties.

Figure 1: Labeling machine with synchronized Axis

1.1Learning objectives

This training module explains the B&R Cam Automat and its functionality. Numerous exercises help solidify under-

standing. In addition, it will frequently refer to the extensive Automation Help and former Motion Trainings, invaluable

references for completing the exercises in this training module.

Participants will be given detailed information about cam automat technology.

•

Participants will be given an overview of informative resources.

•

Participants will be able to describe cams and work with them.

•

Participants will know how to read motion profiles and do diagnostics.

•

Participants will know about different states of a cam automat.

•

Participants will be able to handle cam automat events.

•

Participants will know how master and slave stretching factors work.

•

Participants will know how to set movement limitations.

•

Participants will know how to work with different compensation types.

•

Participants wil know about the functionality of ACOPOS function blocks.

•

Participants will be able to complete cam automat solutions.

•

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
Help: References additional documentation. (Automation Help, data sheets, user's manuals)
Example:
Hardware \ Motion control \ <Device>1 \ Technical data \ (<Type>)2 \ Status indicators
Example: An example illustrates the topic in greater depth.
Result: The result of a completed task is summarized briefly.
Organization of safety notices in external manuals:
This manual contains references to other manuals. How safety notices are organized in external manuals is listed in
the respective manual.
Exercise: Tasks and exercises
Sections marked with an orange stripe on the left side contain information about exercises as well as the associated
actions to be taken. The exercises are designed to provide a deeper understanding of the information provided.
1 Angle brackets indicate variable placeholders "<...>"
2 Parentheses indicate optional entries "(...)"

## Page 6

6AXIS COUPLING: CAM AUTOMAT TM470

2General information

2.1Axis coupling

Coupling axis electronically results in a predefined synchronized movement of drive axes. The coupling is established

using either a specified gear ratio or cams.

Position setpoint coupling is generally assumed. It is also possible to perform coupling using the actual position,

speed, torque or any other signals.

The coupling is defined using axes that are then assigned to hardware.

Coupling across devices in the ACOPOS servo family is therefore not a problem, and the coupling principle can be

applied anytime with any devices in the B&R product range. This is the case regardless of the hardware type being

used or the performance reflected by the size of the device.

Axis A is coupled with axis B using the position setpoint. This means that while the axis are actively cou-

pled, axis A must adjust its position according to the position of axis B. In this case, axis B is the mas-

ter, which specifies a reference position, and axis A the slave, which has a position based on the master

position.

Figure 2: Schematic illustration of a coupling

A coupling always requires a master signal, which provides the reference (position, time) and at least one slave axis,

which must follow this reference value using a "rule". However, the master signal does not have to come from a real

axis. In principle, axis can also be coupled to a variety of suitable reference values. Virtual axes, among others, are

available in the B&R drives or inside the PLC for this purpose.

The master remains unaffected by the coupling procedure. It is simply used as the basis for the desired coupling signal.

For example, if a drive's position value is used as the master signal, then this master axis can still be given a command

even while coupling is active. In this situation, however, the slave axis is still completely dependent on the master signal.

When working with multiple axis, delay times always have to be taken in mind. More detailed information

can be found in TM417.

mapp Technology \ Concept

Motion control \ mapp Motion \ Libraries \ Technology \ MpAxis

2.2Cam automat

The cam automat is a sequential machine with parameter settings which allows an effective link to be made for elec-

tronic cams, compensation functions and event handling. The automat can be assigned on real and virtual slave axis

and is only influencing the slave axis. The characteristics for the individual states and the events for transition between

states are predefined in an axis feature configuration file.

After starting the automat, the cam automat including states and transitions are processed decentralized on the

ACOPOS. This reduces load on the application program and allows very fast, event and trigger driven cam transition.

During operation, the automat structure can no longer be changed. However, online changes to individual parameters

can be made.

The cam automat has a start state "0" for realizing conditions before moving on to the next states which consist of a

compensation gear followed by a cam. If not defined differently in the settings, the base state is active as initialization

state immediately after starting the automat. The transition from one state to another takes place when the defined

event occurs. An event can be e.g. a toggled limit switch or a position being reached by the master signal.

## Page 7

GENERAL INFORMATION7

Figure 3: Basic state diagram scheme

Motion control \ ACP10/ARNC0 \ Reference manual \ ACOPOS Drive Functions \ Cam automat

On some pages, the help references lead to the ACP10/ARNC0 chapter. This is only for general informa-

tion. For practical use, the mapp Motion liabraries like MpAxis or McAxis are needed.

Behavior in the event of an error

If an  during active coupling that causes the axis to stop or switch off, all slave axeserror occurs on the master axis

stop their movement but do not switch off. If the axis is switched off, the system switches from a position setpoint

to the actual position and the slaves follow the actual position to a standstill. The coasting process of the master axis

(coasting to a stop) is followed by the slave axes using the defined coupling ratio.

If an  during active coupling that causes the axis to stop or switch off, the master axis iserror occurs on the slave axis

not affected. Likewise, an error on one slave axis has no effect on the other slave axes.

If this type of behavior is not desired, it is necessary to set up a possibility to release the axes from the coupling ratio.

A start of a movement on the slave axes cancels coupling.3

2.3Use cases

Labelling Machine

In this use case, labels are periodically printed onto

products moving on a conveyor.

The conveyor with the product is defined as the master

axis and the labelling belt with the labels works as the

slave. In the cam automat is defined, in what position

the labelling belt is moving relative to the master and

what happens when a product is missing on the convey-

or.

When the conveyor increases or decreases in speed or

even stops, the labelling belt does the same.

Flying Saw

3e.g. to move from a critical movement area or to be ready for the next start more quickly

## Page 8

8AXIS COUPLING: CAM AUTOMAT TM470

In this use case, a product moving with a constant speed

is periodically cut by saw in a predefined length.

The movement of the product to be cut is defined as the

master axis and the slide of the saw is defined as the

slave. The main purpose of the cam automat in this ma-

chine is the synchronization of the product and the slide

of the saw before cutting.

Only when the slide of the saw and the product are mov-

ing in the same speed, the saw can be moved forward to

cut without being damaged.

## Page 9

GETTING STARTED9

3Getting started

MpAxisCamSequencer

To realise the connection between the master and slave axis, the function block "MpAxisCamsequencer" from the

MpAxis library is needed. This function block is used for extended operation of the cam automat. It allows sequential

couplings to be carried out, cam automat configurations to be transferred or read, and a shift to be modulated via the

master or slave position. Signals can also be set to the cam automat. The master and slave axis are defined by using

inputs "MpLinkMaster" and "MpLink".

A positive edge at the input "StartSequence" starts the cam automat in the configured starting state. A negative edge

stops the active cam automat on the specified axis. The master axis stays untouched.

Figure 4: Function block MpAxisCamsequencer

Motion control \ mapp Motion \ Libraries \ Technology \ MpAxis \ MpAxisCamSeuqencer

To start the cam automat, the function block "MC_BR_CamAutomatCommand" from the McAxis library

can also be used.

For this training we need to comission a system with two axes. One will be defined as the master axis and one will

work as the slave axis. For the biggest part of the training the master axis will move with a constant velocity and the

slave axis, linked to the master axis, will do movements based on the configuration in the various exercises.

## Page 10

10AXIS COUPLING: CAM AUTOMAT TM470

Exercise: Commissioning 2 axes

The objective of this exercise is commissioning 2 axes for further use to cover the topic of cam-automat.

In preparation for subsequent exercises, one axis (later used as master) should be able to be switched on and off

independently of the other axis.

Create a new Automation Studio project and connect to your hardware

•

Comission the two axis to get them running

•

Commands to power and home the axis should be sent with one "MpAxisBasic" function block per axis from the

•

MpAxis library

To handle the cam automat the function block "MpAxisCamSequencer" from MpAxis library is needed

•

Adapt your structured text program to power on and home the axis automatically at startup

•

Motion control \ mapp Motion \ Libraries \ Technology \ MpAxis

Detailed information on how to commission axis can be found in TM415.

3.1Configuration file: Axis feature

The configuration file "axis feature" is used to activate specific features on an axis. The file can be found in the toolbox

when the folder "mappMotion" in the configuration view is selected.

Figure 5: Axis feature in the tool box of the Automation Studio

There are several types you can choose such as "Profile generator" or "Digital cam switch". For this training, we will go

for the option "Cam automat". By choosing this option, more settings will appear.

Figure 6: Axis feature cam automat

Cam automat type

For the setting "Cam automat type" there are the options "ACOPOS" and "Common".

An ACOPOS cam automat can only be used as a feature for a real or virtual ACOPOS axis. As in this training an ACOPOS

drive is used, the ACOPOS cam automat has to be chosen. In addition to the libraries "McAxis" and "MpAxis", the aco-

pos-specific library "McAcpAx" can also be used to operate this type of cam automat.

A Common cam automat is used for all other types of drives that are not part of the ACOPOS family. The common cam

automat can only be used as a feature for pure virtual axes and for axes with an active profile generator. Just like a

pure virtual axis, a common cam automat is running inside the PLC.

## Page 11

GETTING STARTED11

Motion control \ mapp Motion \ Configuration \ Basic Components \ Axis feature \ Cam automat

Assigning the configuration file

After creating an axis feature configuration file it needs to be assigned to an axis. The file gets assigned to the axis

where the defined movment should take place. When using a cam automat, this is the slave.

To do this, the configuration window of the drive in the physical view has to be opened. Assign the cam automat

configuration file to the axis where the slave is running.

Apart from the axis feature configuration file, there are also predefined cam automat configuration files

in the toolbox.

Figure 7: Drive configuration

## Page 12

12 AXIS COUPLING: CAM AUTOMAT TM470
4 Linear coupling
4.1 Linear coupling via mappCockpit
The linear axis coupling can already be used during commissioning.
If a mechanical object has to be moved with two or more motors, a gear coupling (usually with a 1:1 ratio) can be used.
The reasons for such an application include physical limits, dynamics, the smallest possible dimensioning or costs.
With commissioning environment mapp Cockpit, the "Gear In" functionality can be used here.
This functionality can also be used in the program with function block MC_GearIn from the McAxis library, by using the
cam automat or with MpAxisCoupling from the MpAxis library.
Motion control \ mapp Motion \ Guides \ Diagnostic \ mapp Cockpit for mapp Motion components \
Axes \ ACOPOS (real)
4.2 Gear in via cam automat
The simplest form of a cam automat is most likely the "gear in" functionality. But it also brings lots of advantages
with it compared to other ways of achieving this funtionality. By using the cam automat, you also have the benefits of
having states and transitions. This can be an advantage when several predefined parameter settings are needed. Also
signals can be sent easily by the PLC to trigger events.
Exercise: Gear in with cam automat
The objective of this exercise is to recreate the gear function "Gear In" (known from mapp Cockpit).
For this purpose, a simple cam automat with one state should be created. Coupling from a standstill is sufficient and
no special requirements have to be met for this simple example.
Create the configuration (axis feature in Configuration View) and define a state
•
In the "Advanced parameters", set the start state to 1 (0 is not used)
•
Specify the master axis
•
Set the cam to "Predefined" and "One to one cam". This corresponds to a straight line.
•
Define a coupling ratio for one revolution without compensation and factors (e.g. 360)
•
Assign the cam automat to the hardware where the slave is being run (axis feature)
•
Build and transfer
•
Start the cam automat with function block "MpAxisCamSequencer"
•
Start master movement at constant speed
•

## Page 13

LINEAR COUPLING13

Figure 8: Configuration example

## Page 14

14AXIS COUPLING: CAM AUTOMAT TM470

5Diagnostics

As with all B&R technologies, there also needs to be a propper tool to diagnose and debug cam automat solutions.

In this case, it's the trace-functionality of Automation Studio and mapp Cockpit. Besides that, the logger, the watch-

window and other well known debugging tools can also help at finding solutions.

Diagnostics and service \ Diagnostic tools \ Trace

Diagnostics and service \ mapp Cockpit

By tracing the position of the slave axis and the current cam automat state, you have a detailed insight into the behavior

of the current settings. It can also be very helpful to add the velocity of the slave to the trace to read the maximum and

minimum velocity or jumps in velocity for example. You can do the trace either in automation studio or in mapp Cockpit.

Figure 9: Example of a trace in Automation Studio

The red graph shows the actual state index of the cam automat. The order and type of transitions have to be defined

in the axis feature configuration file.

The green graph shows the position of the slave axis. By moving the cursor on the curve and left-clicking the exact

positions can be seen.

In this trace-example, the maximum position of the slave axis is 360. This is why the position-graph in

this screenshot jumps to zero once it reaches 360.

The blue graph shows the velocity of the slave axis in positive and negative direction.

Exercise: Trace

The objective of this exercise is to create a trace of our previous comissioned cam automat. With the trace we diagnose

and understand the behaviour of the slave axis. This exercise shows how to do a trace in automation studio.

## Page 15

DIAGNOSTICS15

Right click the structered text file with the MpAxis function blocks

•

Open a Trace with right click "Open" -> "Trace"

•

Insert a new trace configuration and add variables

•

Add the position and velocity of the slave axis and the actual state of the cam automat

•

Install the configuration and let it save data while the cam automat is running

•

Stop the trace and upload the data

•

Figure 10: Trace config

This exercise can also be done in mapp Cockpit. More information about mapp Cockpit can be found in

TM415.

Diagnostics and service \ mapp Cockpit \ Getting started

## Page 16

16AXIS COUPLING: CAM AUTOMAT TM470

6Coupling via cams

In the cam diagram, we see the master position value in the horizontal direc-

tion and the slave position in the vertical direction. The cam assigns a respec-

tive slave position value for each master position value within a defined range

(master period). The slave drive must follow this profile while the drives are

actively coupled.

Figure 11: Cam as a position relationship

between the master and the slave positions.

The master position is converted to a corresponding slave position via the cam. This allows the master to move in both

directions. The slave drive is "tied" to the master via the cam.

This means that the velocity and acceleration values of the slave drive result from the velocity and acceleration of the

master in connection with the profile of the cam.

For the entire course of the cam, it must be checked if the slave drive can handle the velocity and accel-

eration values that might occur.

Assumed: The master signal constantly changes, which corresponds to a

steady movement of the master axis.

Critical ranges (with maximum values for the slave velocity or accelera-

tion) are represented in the cam by the maximum slope (-> velocity as first

derivative of the position) and the maximum slope change (-> accelera-

tion/deceleration as second derivative of the position) according to the

position comparison.

Figure 12: Maximum velocity

that occurs on the slave drive

6.1Creating cams

Automation Studio has a powerful cam editor for creating cams. Cams can be edited in the cam editor as soon as they

have been inserted into the project.

Cams are added as software objects from the Toolbox to the Logical View in Automation Studio. They need to be

downloaded to the drive from the controller. The easiest way is to specify the cam in the cam list and add them to a

feature of the axis. This way, they are automatically downloaded and can then be used by the ACOPOS because they

are available on the drive.

Figure 13: Cam object "Cam" in the Logical View

It is generally advisable to create standardized cams. These have endpoints with a ratio of 1:1 or 1:0. This

allows the cam to be stretched in unit scaling as needed using corresponding function blocks or the cam

automat. As a result, they can be used on a wide range of axes with different scaling.

## Page 17

COUPLING VIA CAMS17

Figure 14: Cam with 1:1 ratio

6.2Using cams

After the cams have been created and edited, they can now be used.

Cams are required on the device when  and must be transferred to it.using the ACOPOS-specific cam automat

A function block does not necessarily have to be used for this (but it is possible).

Instead, the easy way via the Configuration View can be chosen.

A detailed step by step explanation on how to transfer cams to the acopos can be found in TM417.

6.3Stretching factors

Stretching factors are needed to stretch a cam to the needed length and has to be defined for every cam automat

state and can also help at adapting the cam to the unit scaling of the axis.

The simplest way to show the effects of the stretching factors is using a one-to-one cam which corresponds to a

straight line with a slope of one. By applying stretching factors, the length and slope can be adapted.

In the left graph, a master and slave stretching factor of 100 is applied which leads to a slope of one, while in the right

graph the master stretching factor was changed to 200 which leads to a slope of 0,5.

These values are only units, the measurement resolution is defined in the axis configuration.

Figure 15: Graph to show stretching factors

Exercise: Axis coupling via cam

The objective of this exercise is to create a standardized bell-shaped curve (0 - 1 - 0) for a wide range of applications.

A master and slave axis should be coupled using a cam. To do so, a cam must first be created in the Logical View. The

period should include a full turn of the master and a full turn of the slave.

## Page 18

18AXIS COUPLING: CAM AUTOMAT TM470

Create a cam named "bell". The cam should look like the one in the graph.

•

Figure 16: Example for cam that has been named "bell"

Figure 17: Example values

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

channel feature on the channel where the slave is running.

The assigned cams are now automatically initially transferred to the drive each time the PLC is restarted.

•

Exercise: Using a selfmade cam in a cam automat

The objective of this exercise is to use the assigned cam on the slave drive.

The cam automat that has already been created will be adapted to use the cam.

Open the configuration of the cam automat in the Configuration View

•

Change from "Cam - Predefined" to a name or Cam ID

•

Adjust the master and slave factors to stretch the cam to one revolution

•

All other parameters can be left as they are

•

Build and transfer

•

Start with function block MC_BR_CamAutomatCommand and test the coupling

•

Start master movement at constant speed

•

## Page 19

MASTER VALUE SOURCE19

7Master value source

In the axis feature configuration file you can find the parameter "Master - Value source". The master value source can

either be set as a common parameter for all states or as exception for individual states. In most cases, an axis position

is used for the master value source. Here we can decide between "Set position" and "Actual position". There is also

the possibilty to choose "System time" as the source value. By choosing "ParID" you can use one of many predefined

Prameter IDs as master value source.

Figure 18: Master value source options

7.1Axis position as source

Set position

With the set position as the master value source the cam automat references to the the position setpoint of the master

axis and not the actual value of the position. The slave follows the the master set position. This setting is selected by

default and most commonly used as common master value source.

Actual position

With the acutal position as the master value source the cam automat references to the actual value of the master axis

position. This is why turning it manually also makes the slave axis move. Coupling to the actual position is needed when

no set position is given, which can be the case when working with 3rd party drives. A drawback of this option is that

communication delays can't be compensated.

7.2Other master source types

Parameter IDs (ParID)

ACOPOS parameter IDs can also be used as master value source. With these ParIDs specific data inside the ACOPOS

can be accessed. PLCopen function blocks in the background serve as data preparators. When choosing the option

ParID as master value source you have to enter an ID you can find in the automation help.

Motion control \ ACP10/ARNC0 \ Reference manual \ ACP10 \ ACOPOS Parameter IDs

## Page 20

20AXIS COUPLING: CAM AUTOMAT TM470

Example:

The cam automat runs on an ACOPOS with a plug-in module with a second encoder

•

As master value source, the position of this encoder is needed

•

Choose MasterParID as the master value source for this state and enter 423 as your ParID

•

423 is defined as mcACPPAR_ENCOD2_S_ACT in the McAcpPar library and addresses the actual po-

•

sition of the second encoder in the ACOPOS

Figure 19: Setting in config file

System time (µs)

One of the options for the master value source is the system time. This type is most commonly only used in selective

form for a specific state. A use case example would be a cutting or welding process. These processes are often time

based and not related to the position of another axis.

Make sure to choose the stretching factors of the state high enough as the system time has a very high

resolution (microseconds). A high resolution results in high values for streching factors for a propper

coupling.

## Page 21

EVENTS AND TRANSITIONS21

8Events and transitions

Transitions between states in a cam automat are handled by events. There is a possibilty to have up to five different

events in one state. Per event you have to choose the type of the event, when the transition happens, wheter a syn-

chronous update is needed and the next state the cam automat goes into when the event occurs.

Events within a state are prioritised with rising numbers starting from 0. This means, even when two or more events

get triggered, the event with the lowest number becomes effective.

Figure 20: Events

It is recommended to define a event with end of state transition into its own state to have a clear transi-

tion defined when no other event gets triggered. However, if a valid event does not occur before the end

of the cam interval, the compensation curve and cam for this state are repeated by default. That means

an interval end corresponds to a default event with transition into its own state.

8.1Event types

There are many options when it comes to the type of the event.

With "State end" the event gets triggered when the movement in

•

this state is finished.

In the advanced parameters of the state a "repeat counter init" can

•

be set. There is the option to trigger an event when this counter is

reached.

With "Master start position", an event is triggered everytime the

•

master passes the start position.

A "Signal from PLC" can be set by using the function block "MpAxis-

•

CamSequencer". This makes it possible to trigger events from the

application, but also results in higher delay times.

A better option would be to use the input triggers directly on the

•

the ACOPOS which is usually preferred on real applications

The automation help provides detailed information about the dif-

•

ferent event types.

Figure 21: Event types

Motion control \ ACP10/ARNC0 \ Reference manual \ ACOPOS Drive Functions \ Cam automat \ States

\ Events

In the automation help, these events are listed as constants. By hovering over the event in the Automation

Studio, the constant of the event is shown.

## Page 22

22AXIS COUPLING: CAM AUTOMAT TM470

8.2Transition

There is also the option to change transition types for the transitions between the states. One option is the "End of

state" transition, the other one is the "Immediate" transition. Either one has their own specific behaviour and use-

cases.

With "End of state" the cam automat stays in this state after an event is triggered as long as the movement isn't

finished. This makes sure that the movement of this state is always finished.

With "Immediate" the cam automat changes the state immediately when an event is triggered. This can be helpful in

case of an error to bring the machine to a safe position quickly for example.

Figure 22: Transition

8.3Synchronous update

With the function block MpAxisCamSequencer you have the option to lock the synchronous parameter update of the

cam automat. By adapting the "ParLockCommand" in the "MpAxisCamSequencerParType", a parameter lock-setting

can be chosen. By executing the command "ParLock", the lock-setting is updated.

By default, the "ParLockCommand" is set to "mcCAMAUT_NO_LOCK", which means that the parameters

are not locked and can be updated any time.

When the option "mcCAMAUT_UNLOCK_SYNCHRON" is chosen, the parameters only update when a defined event

occurs. Therefore we have to allow the synchronous update in the event settings. Now, when this event is triggered,

the parameters are updated.

Figure 23: Synchronous update

Motion control \ mapp Motion \ Liabraries \ Core \ McAxis \ Data types and constants\ Enumerators

\ McCamAutParLockCmdEnum

More advanced information (described with internal ACOPOS constants):

Motion control \ ACP10/ARNC0 \ Reference manual \ ACOPOS Drive Functions \ Cam automat \ Online

change

8.4Next state

In the event-settings the next state also has to be selected. It is possible to choose between 15 states (including the

base state). There is also the option to select "Cam automat end" which ends the cam automat. There is no rule, that

the states have to be used sequential. So also a mixed order can be used, or states can be used freely.

## Page 23

EVENTS AND TRANSITIONS23

Figure 24: Next state

Exercise: Simplified gears with changing gear ratio

The objective of this exercise is to create a cam automat configuration to get a gear functionality with to different

gear ratios changing on signals. With Signal 1 = TRUE, the axis should move with a ratio of one two one. With Signal 2

= TRUE, the slave ratio should double to get a ratio of one to two.

In this exercise there is no compensation needed.

Define a cam automat with start state 1

•

In state 1 a simple one to one coupling is defined

•

With signal 2 set to true, the cam automat goes into state 2

•

In state 2, the slave ratio doubles

•

With signal 1 set to true, the cam automat goes into state 1 again

•

Start tracing and do a couple of changes and check the trace afterwards

•

Settings like this lead to a velocity jump with every transition which is practically not allowed without

compensation. Only for training purposes it can be done like this.

Exercise: Immediate Transition

The objective of this exercise is to know the difference between "End of state" transition and "Immediate" transition.

Keep the same configuration as in your previous exercise

•

Change the transition type in one of your states to "Immediate" and leave the other one at "End of state"

•

Transfer the project and start the cam automat

•

Start a trace and do a couple of transitions

•

Check the differences

•

## Page 24

24AXIS COUPLING: CAM AUTOMAT TM470

9Adjustment during runtime

With cam automat it is possible to do changes during runtime. By applying changes to the master and slave stretching

factors, fine adjustments can be made during runtime. Therefore the function block "MpAxisCamSequencer" can be

used, which has the function blocks "MC_BR_CamAutomatGetPar" and "MC_BR_CamAutomatSetPar" embedded into

its functionality. Alternatively fucntion blocks from McAxis library can be used.

Figure 25: Usage of the function blocks

(Optional) Exercise: Update gauge factors

The objective of this exercise is to change parameters at runtime.

The gauge factors (master and slave factor) of the standardized cam are available here.

Initialize the cam automat via the Configuration View

•

Enable the synchronous update in the event configuration as seen in the chapter "Events and transitions"

•

The "MpAxisCamSequenceType.Command" has to be set to "mcGET_PAR_ACTUAL" to copy the parametersGet

•

from the configured cam automat

MpAxisCamSequencer.GetSequence (library MpAxis) for loading the cam automat configuration from the drive

•

to the user structure

As we are working with an Acopos device, the correct parameter type to make changes or use is "McAcpAxCa-

•

mAutParType", also give the data size

The automation help mentions the "McCamAutParType" which only works for common drives and not

for acopos devices.

Change the master and slave stretching factor in the cam automat structure in the Watch window

•

The "MpAxisCamSequenceType.Mode" has to be set to "mcAXIS_CAM_SEQ_SET_ON_UPDATE" to get changesSet

•

done when an update is done

The "MpAxisCamSequenceType.Command" has to be set to "mcSET_SYNC_UPDATE_FROM_ADR" to transferSet

•

the update parameters from the user structure

Set MpAxisCamSequencer.Update (library MpAxis) for updating the changed values on the drive

•

## Page 25

ADJUSTMENT DURING RUNTIME25

Motion control \ mapp Motion \ Libraries \ Technology \ MpAxis \ Function blocks \ MpAxisCamSe-

quencer

The MpAxisCamSequenceType and MpAxisCamSequenceType can be found here on this automa-GetSet

tion help page.

## Page 26

26AXIS COUPLING: CAM AUTOMAT TM470

10Compensation

For each state in the cam automat, a compensation gear can be used.

The compensation gear is an automatically calculated curve which compensates for position, speed or acceleration

differences during a state transition and maintains a continuous connection of the cams. The parameters required for

this are available in each state.

Figure 26: Displays the functionality of a compensation gear

The image shows compensation between two consecutive states (cams). If compensation is used in a state, then the

compensation movement is always performed  the cam of the state.before

These are the base parameters that define the compensation:

Compensation variant (compensation mode)

•

Master compensation distance (master distance)

•

Slave compensation distance (slave distance)

•

The different compensation gear modes provide possibilities for compensating path as well as velocity differences.

10.1Compensation Types

There are several compensation types to compensate position and velocity differences between cams. In this training,

the most frequently used types are shown in more detail. The automation help provides information about all com-

pensation types.

To find information about a certain type you see in the automation studio, hover your cursor over the type you are

looking for. The correct term to look for in the automation help will pop up on the right side.

Figure 27: Finding compensation types in the automation help

Automation StudioAutomation Help

No compensationncOFF mode

Table 1: Compensation Types

## Page 27

COMPENSATION 27
Automation Studio Automation Help
Distances between cams ncONLYCOMP mode
Distance between exit and entry point of cam ncONLYCOMP_DIRECT mode
Distances between cam center points ncWITH_CAM mode
Master distance between latch position and cam center point ncMA_LATCHPOS mode
Slave distance between latch position and cam center point ncSL_LATCHPOS mode
Slave compensation to absolute slave position ncSL_ABS mode
Jolt minimal velocity mode with set master distance Mode ncV_COMP_S_MA
Jolt minimal velocity mode with set slave distance Mode ncV_COMP_S_SL
Time optimal jolt limited velocity mode Mode ncV_COMP_A_SL
Time optimal jolt limited velocity mode, cyclic master velocity evaluation Mode ncV_COMP_A_CYC
Slave compensation to absolute master and slave position ncMA_SL_ABS mode
Table 1: Compensation Types
Motion control \ ACP10/ARNC0 \ Reference manual \ ACOPOS Drive Functions \ Cam automat \ Com-
pensation gears and change of cams
Legend for following diagrams
Abbreviation Decription
c1 x/y Master/Slave curve one
c2 x/y Master/Slave curve two
e2 x/y Effective master/slave compensation
p2 x/y Configured master/slave compensation length
Motion control \ ACP10/ARNC0 \ Reference manual \ ACOPOS Drive Functions \ Cam automat \ Com-
pensation gears and change of cams \ Modes to compensate position differences \ Abbreviations for
the following compensation modes
10.2 Position compensation
10.2.1 Distance between cams
This compensation type is the one of the most common types to compensate position differences.
The configured compensation distances are effective from the curve end of the previous state to the curve start of
the current state.

## Page 28

28AXIS COUPLING: CAM AUTOMAT TM470

Figure 28: Compensation between two cams with end-of-state transition and without "lead in"

The master and slave intervals remain the same even for "immediate" transitions and when entering the next cam

directly "lead in". Only the effective compensation distance will get longer.

More information about the behaviour with parameters like "lead in" or with "immediate" transition can be found in

the automation help.

Motion control \ ACP10/ARNC0 \ Reference manual \ ACOPOS Drive Functions \ Cam automat \ Com-

pensation gears and change of cams \ Modes to compensate position differences \ ncONLYCOMP mode

10.2.2Distance between exit an entry point of cam

This compensation type is also a very frequently used type to compensate position differences.

With transition type "end-of-state" and without a "lead in", it has the same behaviour as the previous compensation

type "Distance between cams".

In this mode a compensation gear is calculated when entering the state. The configured compensation distances are

used  as effective length. They run from the exit point of the previous state to the curve start of the currentunchanged

state. The difference only emerges when "immediate" transition or a "lead in" has been chosen. Then, the master and

slave intervals are shifted. The effective compensation length does not change (e2=p2).

## Page 29

COMPENSATION29

Figure 29: Compensation between two cams with immediate transition out of cam one

Motion control \ ACP10/ARNC0 \ Reference manual \ ACOPOS Drive Functions \ Cam automat \ Compen-

sation gears and change of cams \ Modes to compensate position differences \ ncONLYCOMP_DRIECT

mode

10.2.3Distance between cam center points

The configured compensation distances apply from the "middle" of curve one to the "middle" of curve two.

The advantage of the "middle of the curve" reference point is that fixed master and slave intervals can be maintained

- even for immediate transitions and when changes are made to curve multiplication factors, when compressing and

stretching the curve periods.

Figure 30: Compensation between two cams with end-of-state transition

As seen in the picture, the defined compensation length (p2) starts from the middle of curve one and ends in the

middle of the second curve. When using an "immediate" transition out of cam one or a "lead in" into curve two, only

the effective compensation length (e2) changes. Related graphs can be found in the automation help.

A behaviour like this can be useful when e.g. a cut has to be done after a certain distance. This distance can be defined

as "p2x/y" as this distance doesn't change, independent from transition type or cam stretching factors.

## Page 30

30AXIS COUPLING: CAM AUTOMAT TM470

Motion control \ ACP10/ARNC0 \ Reference manual \ ACOPOS Drive Functions \ Cam automat \ Com-

pensation gears and change of cams \ Modes to compensate position differences \ ncWITH_CAM mode

To get a smooth compensation between two one-tow-one curves, a simple formula can be applied to get

the master and slave compensation distance.

Exercise: Distance between cams

The objective of this exercise is to know the difference

between the two widely used compensation types "Dis-

tance between cams" and "Distance between exit and en-

try point of cam".

The difference only gets visible when changing the

•

transition type to "Immediately"

Try both compensation types without changing any-

•

thing else

The difference can be hard to see (bigger stretching

•

factors can help)

Make a trace and check the differences

•

Figure 31: Configuration example

10.3Speed compensation

The compensation movement is calculated in order to adapt the slave speed when the slope of a curve is changed or the

master axis is switched. Coupling will also work when the master is already running while the slave is still in standstill.

Motion control \ ACP10/ARNC0 \ Reference manual \ ACOPOS Drive Functions \ Cam automat \ Com-

pensation gears and change of cams \ Modes to compensate speed differences

10.3.1Time optimal and jolt limited compensation

Precalculated

With the compensation type "Time optimal jolt limited velocity mode" speed differences can be compensated. For a

time optimal, jolt limited compensation curve, the maximum slave acceleration and the slave jolt time are specified.

When the state is entered, the movement of the slave axis is calculated according to these parameters.

## Page 31

COMPENSATION31

Cyclic calculation

The difference when using the compensation type "Time optimal jolt limited velocity mode, cyclic master velocity

evaluation" is, that the compensation is recalculated every cycle. The entry and exit slope are taken as a constant, only

the master position is reevaluated every cycle.

10.3.2Speed compensation with set distances

There is also the possibility to achieve a speed compensation with fixed master or fixed slave distances. With the

compensation types "Jolt minimal velocity mode with set master distance" and "Jolt minimal velocity mode with set

slave distance" a fixed distance can be set for master or slave. With the set distance, the slave acceleration is calculated.

Exercise: Online Compensation

The objective of this exercise is to learn about the compensation type "Time optimal jolt minimal mode, cyclic master

velocity evaluation".

The same concept has been used with "McGear_In" from the library "McAxis"

For this compensation type you have to choose the "max slave acceleration" and "slave jolt time". With these values,

the compensation curve is recalculated every cycle.

Define a one-to-one cam

•

Choose the correct compensation type

•

Enter the right parameters, you can start with a max slave acceleration of 50 units/s and a slave jolt time of 4

•

seconds

Start the cam automat and make a trace

•

Try different parameters and analyse the difference

•

Figure 32: configuration example

## Page 32

32AXIS COUPLING: CAM AUTOMAT TM470

11Advanced topics

11.1Advanced parameters

By activating the advanced parameters of a compensation, more options appear. These advanced settings are only

accessible when a compensation is selected. There are different options for position and velocity compensation types.

Figure 33: Advanced parameters in axis feature configuration file

By default, most of the settings are zero. To get the cam automat working, a valid value has to be assigned

to each value. However, these values are more of a guideline for the cam automat and not a strict rule.

E.g.: When the slave acceleration is zero, the slave will not get into movement.

For position compensation types, the max slave acceleration is divided into two parts. The following graph will point

out the two parts.

Figure 34: Compensation part one and two

11.1.1Lead in

With the parameter "master cam lead in" you can make the compensation reach into your current cam. This will shorten

the cam and can lead to a smoother transition.

In the following graph, two cams and a compensation between them can be seen. For "Cam b" a position compensation

has been selected with the parameter "Lead in" being used.

## Page 33

ADVANCED TOPICS33

Figure 35: Compensation between cams with "Lead in"

According to the graph, this cam automat configu-

ration could be fitting.

In state 1 is a one-to-one cam without compensa-

tion (Cam a).

In state 2, there is also an one-to-one cam with po-

sition compensation and advanced parameters be-

ing used.

Especially the parameter "Master cam lead in" is

noteworthy as this makes the compensation reach

into "Cam b".

Figure 36: Example configuration for "Lead in"

Exercise: Lead in

The objective of this exercise is show the effects of the advanced parameter "lead in". With "lead in" you can make

the first curve reach into to the second curve wich leads to a smoother movement of the slave axis (e.g. velocity never

drops to zero).

## Page 34

34AXIS COUPLING: CAM AUTOMAT TM470

Define two states

•

In state one, create a one-to-one movement with stretching factor 10000 for master and slave

•

For state two, choose the previous defined cam "bell" (stretching factor 36000) as your curve with direct com-

•

pensation

Set the master/slave distance to 300

•

Activate the advanced parameters and make sure that all parameters are filled

•

Figure 37: Example settings of advanced parameters

Make a trace and try different "lead in" in state two starting from 0 up to 150

•

To see what part of the movement belongs to curve and what part belongs to the compensation you can

add the following variable to your trace: "MpAxisCamSequencer_0.InCam"

11.1.2Setting speed and acceleration limits

There's also the possibilty to limit the velocity and acceleration of the slave. There are several use-cases where these

settings come to use.

E.g. for some motors high velocity or acceleration are not allowed, while on other applications these factors would

have a bad impact on the product as you see in the example further below.

All defined values correlate with each other.

E.g.: When a higher velocity and higher compensation distance is chosen, the acceleration has to be high

enough to get the slave to the needed velocity to reach the defined compensation distance.

Exercise: Limitation of max slave acceleration

The objective of this exercise is to limit the slave accelereration in the advanced parameters to get the functionality

of a vertical elevator. When moving a box vertical upwards, it's important that it doesn't stop abruptly. Otherwise the

box could fly of the elevator. Therefore we have to limit the decceleration at the top of the elevator.

Define a cam automat with 3 states

•

In state 0 (base state) the cam automat waits for the master to pass the start position

•

In state 1 the elevator moves upwards (positive direction with direct compensation)

•

Make sure to limit the decceleration in advanced parameters (max slave acceleration2)

•

In state 2 the elevator moves back to its origin without compensation (negative direction)

•

## Page 35

ADVANCED TOPICS35

Figure 38: Elevator scheme

11.2Calculating cams during runtime

Creating cams in the cam editor and downloading at start is not the only way to use cams on the ACOPOS. It's also

possible to create cams during runtime by using certain function blocks. The function blocks "MR_BR_CalcCamFrom-

Points" and "MC_BR_CalcCamFromSections" from the McAxis library can calculate cams with given sections or points.

These generated cams are then saved onto the PLC or downloaded directly on the ACOPOS.

Motion control \ mapp Motion \ Liabries \ Core \ McAxis \ Function blocks \ MR_BR_CalcCamFromPoints

Motion control \ mapp Motion \ Liabries \ Core \ McAxis \ Function blocks \

MC_BR_CalcCamFromSections

Figure 39: Cam saving procedure

## Page 36

36AXIS COUPLING: CAM AUTOMAT TM470

The function block "MC_BR_SaveCamProfileObj" is now called "MC_BR_CamSaveDataObject".

The function blocks "MC_CamTableSelect" and "MC_BR_DownloadedCamProfileObj" are now combined

in one function block called "MC_BR_CamPrepare".

11.3Error handling and restart

Error handling with cam automat can also be done in a very efficient way. In case of error, the cam automat can get

into a specific state to return to a safe position.

When, for example, a light gate is crossed and the application has to be stopped, the machine returns to a safe position

first. This light gate is connected directly to an ACOPOS input trigger to avoid network delay time and guarantee fast

reaction time.

Figure 40: Event with input trigger on ACOPOS

Restart

In some applications, the sequence still needs to be finished once it was stopped e.g. to keep material loss short. To

continue at the same point in the cam sequence where it was stopped, the input "MpAxisCamSequencer.Continue" or

"MC_BR_CamAutomatCommand.Restart" has to be set.

Additional information can be found at:

Motion control \ mapp Motion \ Libraries \ Core \ Function blocks \ MC_BR_CamAutomatCommand \

"Restart" input

11.4ACOPOS function blocks

The ACOPOS function blocks are running directly on the ACOPOS to avoid PLC and network delay time and have very

fast evaluation of parameters. ACOPOS function blocks are freely configurable function blocks (PID control, arithmetic

operations, set value generation, handling of digital and analog IOs on the drive ...) that are executed real- time directly

in the drive.

Detailed information about ACOPOS function blocks can be found in the TM471.

Motion control \ ACP10/ARNC0 \ Reference manual \ ACOPOS Drive Functions \ ACOPOS Function Blocks

These function blocks can also be used in a cam automat to trigger very fast transitions between states directly on

the ACOPOS.

Download

To use them we need to transfer them to the ACOPOS by assigning a parameter table to the drive as a channel feature.

Add  from Toolbox - object catalog with description “ACOPOS parameter table for mappACOPOS Parameter table

Motion” if mappMotion is used and the user wants to configure ACOPOS function blocks.

## Page 37

ADVANCED TOPICS37

Axis feature

Add  in mappMotion package in configuration view of Automation Studio to download the ACOPOS Pa-Axis Feature

rameter Table.

After adding , the user should find options below for axis feature and select related feature as shownAxis Feature

below.

Channel feature

Configure desired  in drive configuration as channel configuration, e.g. cam list feature and ACOPOS pa-Axis Feature

rameter table for ACOPOS function blocks.

Usage in a cam automat

To use these ACOPOS function blocks in the cam automat, they have to be added in the common parameters of the

axis feature configuration file. The ParID has to be entered as an integer value. The correct ID to each ACOPOS function

block can be found in the automation help.

Motion Control \ ACP10/ARNC0 \ ACP10 \ ACOPOS Parameter IDs

## Page 38

38AXIS COUPLING: CAM AUTOMAT TM470

It is possible to assign up to 4 parameter IDs per cam automat. These "EventParIDs" can then be used in the states as

events. This way, very fast ACOPOS triggered transitions are possible.

## Page 39

SUMMARY39

12Summary

The cam automat is an extremely powerful tool for effectively linking cams. The necessary sequences are complete-

ly predefined in a well structured configuration file but can be influenced during runtime via events and parameter

changes.

Controlling of the automat is handled using the function block MpAxisCamSequencer from the MpAxis library. It in-

cludes functions to start the cam automat, to set signals to trigger events, to update parameters and many more.

Figure 41: Schematic design of a cutting machine

Once the cam automat is started, the defined sequences are independently processed on the drive. Internal ACOPOS

function blocks and direct input triggers on the ACOPOS can be used to avoid network delay time. This reduces the

load on the application program and results in a very fast, event-controlled positioning sequence.

## Page 40

40AXIS COUPLING: CAM AUTOMAT TM470

Automation Academy

Your knowledge advantage

The Automation Academy provides  courses for our customers as well as for our own employees.targeted training

Expand your skills in the field of automation technology and learn to independently implement efficient automation

using B&R systems.solutions

Decide for yourself which  you prefer!learning concept

Classroom learning

B&R offers  at all B&R locations. Services include seminar documents, effective communication ofstandard seminars

learning content by experienced trainers and an Automation Diploma. A combination of group work and self-study

provides the high level of flexibility needed to maximize the learning experience.

Virtual classroom

supplement B&R's continuing education portfolio with a virtual classroom, offering an alternative toRemote Lectures

our on-site seminars. Selected content from our standard seminars is offered online. In addition to remote learning

methods, powerful simulation tools and secure remote maintenance are used.

Online courses

Take control of the content and learn at your own pace. With B&R , you can take your first steps in theonline courses

world of B&R automation at any time. Based on a comprehensive narrative, you will independently work out how to use

our products. The mix of different media allows a logical sequence to be followed when learning as well as a selective

choice of information to be used as a reference tool.

Contact

Would you like additional training? Are you interested in finding out what the B&R Automation Academy has to offer?

If so, this is the right place.

Access additional information here:

https://www.br-automation.com/de/academy/

Enjoy your next training course!

## Page 41

AUTOMATION ACADEMY 41

## Page 42

42 AXIS COUPLING: CAM AUTOMAT TM470

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

V2.0.0.0 ©2023/10/03 by B&R, All rights reserved.

All registered trademarks are the property of their respective owners.

Subject to technical changes without notice.