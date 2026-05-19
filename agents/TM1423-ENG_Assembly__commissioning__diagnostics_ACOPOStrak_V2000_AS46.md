## Page 1

TM1423

Assembly,

commissioning and

diagnostics for

ACOPOStrak

## Page 2

2 ASSEMBLY, COMMISSIONING AND DIAGNOSTICS FOR ACOPOSTRAK TM1423
Requirements
Basic knowledge Basic technical understanding
TM210 - Working with Automation Studio
TM223 - Automation Studio diagnostics
Training modules TM1415 - ACOPOStrak dimensioning and programming
ACOPOStrak user's manual V0.5
Documentation
Assembly diagram
Automation Studio 4.9
Software
mapp Motion 5.13
Automation Runtime Simulation
Hardware
ACOPOStrak system

## Page 3

TABLE OF CONTENTS 3
Table of contents
1 Introduction.........................................................................................................................................................4
1.1 Learning objectives..............................................................................................................................4
1.2 Symbols and safety notices...............................................................................................................4
2 Mechanics of ACOPOStrak...............................................................................................................................5
2.1 Documentation.....................................................................................................................................5
2.2 Hardware components of ACOPOStrak..........................................................................................6
2.3 Required tools.......................................................................................................................................8
2.4 Layout sketch......................................................................................................................................10
2.5 Mechanical design..............................................................................................................................11
3 Getting started.................................................................................................................................................13
3.1 Checking the mechanical components..........................................................................................13
3.2 Power supply and network...............................................................................................................13
3.3 Switching on the system for the first time..................................................................................13
3.4 Test application..................................................................................................................................15
3.5 First movements with the application..........................................................................................16
4 Diagnostics of ACOPOStrak...........................................................................................................................17
4.1 Logger...................................................................................................................................................17
4.2 System Diagnostics Manager..........................................................................................................18
4.3 Network command trace.................................................................................................................19
4.4 Diagnostics display...........................................................................................................................22
4.5 mapp Cockpit.....................................................................................................................................23
4.6 Closed POWERLINK ring...................................................................................................................27
5 Maintenance and segment replacement....................................................................................................29
5.1 Service intervals for ACOPOStrak...................................................................................................29
5.2 Monitoring the service intervals.....................................................................................................29
5.3 Maintenance........................................................................................................................................30
6 Summary............................................................................................................................................................31

## Page 4

4ASSEMBLY, COMMISSIONING AND DIAGNOSTICS FOR ACOPOSTRAK TM1423

1Introduction

ACOPOStrak is an intelligent track system based on linear motor technology that revolutionizes the flexibility of ma-

chines and systems and thus enables economic symbiosis between batch size 1 and mass production. Unique prod-

uct features such as absolute design freedom, high-speed diverting or the intelligent system software enable parallel

process stations and scalable machines. Due to the modular design, existing machines can be easily upgraded and

additional processing stations added if required.

This training module deals with the mechanics and their commissioning as well as the diagnostics and service of

ACOPOStrak. All training content is prepared so that it can be used for the actual track in workshops.

When it comes to the mechanics, the main focus is on the flexibility of ACOPOStrak and the ability to adjust the layout

of the system design as needed. For diagnostics, typical scenarios will be covered including how to solve any problems

that may arise.

Figure 1: ACOPOStrak

The information and exercises provided here explain how ACOPOStrak hardware should be operated.

1.1Learning objectives

This training module includes basic information about the mechanical design, commissioning and diagnostics of

ACOPOStrak. It also covers important information available in the user's manual and in Automation Help.

Participants will become familiar with ACOPOStrak.

•

Participants will become familiar with the mechanical components of ACOPOStrak.

•

Participants will be able to use the user's manual.

•

Participants will be able to set up an ACOPOStrak system using the assembly instructions.

•

Participants will be able to commission ACOPOStrak.

•

Participants will be able to use dynamic node allocation (DNA) with the ACOPOStrak hardware.

•

Participants will be able to perform ACOPOStrak diagnosis.

•

Participants will be able to identify and solve typical errors.

•

Participants will become familiar with the ACOPOStrak software components.

•

Participants will be able to carry out maintenance work on the shuttle.

•

Participants will be able to replace a segment.

•

1.2Symbols and safety notices

Unless otherwise specified, the symbol descriptions and safety notices listed in "TM210 - Working with Automation

Studio" apply.

Additional explanations of symbols and safety notices are available in the ACOPOStrakuser's manual.

## Page 5

MECHANICS OF ACOPOSTRAK5

2Mechanics of ACOPOStrak

This chapter explains the mechanical components and shows how they are assembled.

2.1Documentation

The ACOPOStrak user's manual is a comprehensive documentation and provides detailed data for all components.

In addition to the technical data, the user's manual also

contains assembly instructions for the guide system and

the installation of the segments.

Information and rules for dimensioning are also included.

The safety technology available is covered in a separate

chapter.

The appendix contains design drawings for an oval, the

segments and the magnet unit.

Furthermore, all safety notices are covered in the user's

manual.

Figure 2: Table of contents for the user's manual

## Page 6

6ASSEMBLY, COMMISSIONING AND DIAGNOSTICS FOR ACOPOSTRAK TM1423

The user's manual is available for download on the B&R website:

www.br-automation.com

Figure 3: Downloads section of the website

The user's manual is available on the website in section <Downloads>. Filters <Track technology> and

<ACOPOStrak> takes you to all the documentation available for ACOPOStrak.

Exercise: Working with the user's manual

It is especially important to be familiar with the documentation for the mechanical design of ACOPOStrak. We will

therefore download the latest version of the user's manual from the website in the next exercise.

To avoid injuries and damage, special attention is given to the safety notices.

1)Open the B&R website: www.br-automation.com

2)Download and open the ACOPOStrak user's manual (see TM1423 2.1 "Documentation" on page 5).

3)Obtain an overview in the user's manual.

4)Read point 2 "Safety guidelines" in chapter 1 "General information".

2.2Hardware components of ACOPOStrak

The main components of ACOPOStrak are the segments and the guide system. These are briefly described in the fol-

lowing sections.

2.2.1The guide system

The ACOPOStrak guide system consists of various guide rails. The individual guide rails end with a straight transition

and can thus be joined on to other segments as desired.

All guide rails can be set up as permanent or adjustable units.

## Page 7

MECHANICS OF ACOPOSTRAK7

Straight

A straight segment is attached to the straight guide rail, which is mounted

with a guide stand.

Figure 4: Straight guide rail

Curve - 45°

A curve input and a curve output segment are attached to the 45° guide

rail, which is mounted with two guide stands.

Figure 5: Guide rail 45°

Curve - 90°

One curve input, one circular arc output and one curve output segment are

attached to the 90° guide rail, which is mounted with two guide stands

and an optional additional stand.

Figure 6: Guide rail 90°

Curve - 135°

One curve input segment, two circular arc segments and one curve output

segment are attached to the 135° guide rail, which is mounted with two

guide stands and one additional stand.

Figure 7: Guide rail 135°

Curve - 180°

One curve input segment, three circular arc segments and one curve out-

put segment are attached to the 180° guide rail, which is mounted with

two guide stands and two additional stands.

Figure 8: Guide rail 180°

## Page 8

8ASSEMBLY, COMMISSIONING AND DIAGNOSTICS FOR ACOPOSTRAK TM1423

2.2.2Segments

The segments are named after the transitions in counting direction. A straight transition is marked with the letter A,

a curve transition with the letter B.

The description of the segments is also included in the order numbers.

Straight segment

The straight line segment is called an AA segment because it connects two

straight transitions. It has a length of 660 mm.

Order number: 8F1I01.66.0000-1AA

Figure 9: Straight segment AA

Curve entry segment

The curve entry segment is called an AB segment because it forms the

transition between straight and curve. It has an angle of 22.5°.

Order number: 8F1I01.2B.0000-1AB

Figure 10: Curve input segment AB

Curve exit segment

The curve exit segment is called BA segment because it forms the transi-

tion between curve and straight. It has an angle of 22.5°.

Order number: 8F1I01.2B.0000-1BA

Figure 11: Curve output segment BA

Circular arc segment

The circular arc segment is called BB segment because it connects two

curve transitions. It has an angle of 45°.

Order number: 8F1I01.4B.0000-1BB

Figure 12: Circular arc segment BB

2.3Required tools

The following components are required to install an ACOPOStrak transport system:

## Page 9

MECHANICS OF ACOPOSTRAK 9
Werkzeuge Messmittel
Torque wrench (5 to 25 Nm) 2x precision spirit level according to DIN 877
• •
Long nut SW13 Feeler gauge 13 blades 0.05-1 mm, blade length 100
• •
Hex key SW5 mm
•
Hex key SW8 Caliper with depth gauge (< 250 mm)
• •
Hex key SW3 3x absolute dial gauges 0.01 mm, measuring range:
• •
1/4" Hexagon socket key attachment size 5 12.5 mm
•
8F1TCA.GAS00000I-1 assembly support
•
8F1TCA.GAT01000I-1 alignment tool for guide ele-
•
ments 180° / 135°
8F1TCA.GAT02000I-1 lignment tool for guide element
•
90°, 45°, gerade
8F1TCA.GMS00000I-1 measuring shuttle for guide
•
rail transition
8F1TCA.GHAT0000I-1 height adjustment tool for ac-
•
cessory stands
Beim Aufbau eines Systems mit Weiche, werden zusät-
zlich die folgenden Messmittel benötigt:
Diverter Setup Tool
•
8F1TCA.DCDG0000I-1 calipers for diverter gap
•
8F1TCA.DSAT0000I-1 adjustment tool for precisely
•
positioning segments horizontally in guide elements
8F1TCA.DHOMD000I-1 measuring device for deter-
•
mining the height offset of opposing double-v guide
rails in the diverter area
8F1TCA.DCB00000I-1 adjustment and control blocks
•
for the diverter gap
8F1TCA.DCBT0000I-1
•
calibration block for height offset / calibration for di-
verter gap calipers
The list of tools and measuring devices as well as further information and safety notices can be found
in the user's manual.

## Page 10

10ASSEMBLY, COMMISSIONING AND DIAGNOSTICS FOR ACOPOSTRAK TM1423

Diverter setup tool

The diverter setup tool (DST for short) is a special shuttle that is required for setting up the diverters and adjusting

them. In addition to the shuttle, there is also a display unit that can be used to read the attractive force.

Description

1 ... Double-V roll

2 ... Flat roll

3 ... Magnetic unit with load cell

4 ... Movable end stop

5 ... Adjusting screw for end stop

6 ... Connector with cable to display unit

There are three rolls (orange/white) with a small offset

on one side of the shuttle. This offset is used to set the

distance between the opposite guide rails in the diverter

area.

Figure 13: Diverter setup tool design

Function

To enable the fully electronic diverter function, the attractive force be-

tween the shuttles and the segments on both sides must be identical. This

way, a force difference can be generated via field strengthening and field

weakening that makes the shuttle change sides.

The attractive force is indirectly proportional to the air gap (see distance

"d" in Fig. 14 "Positioning of the segments"), i.e. the larger the air gap, the

lower the attractive force (see Fig. 14 "Positioning of the segments"). The

following steps are necessary to change the position of the segment in

the guide system.

1)Retract the DST stops using the adjusting screw (see point 5 Fig. 13

"Diverter setup tool design") until they touch the segment.

2)Loosen all mounting screws on the segment.

3)Retract or extend the DST stops until the desired attractive force is

shown on the display.

4)Tighten all mounting screws again (according to the tightening

torque in the user's manual).

Figure 14: Positioning of the segments

Instructions for how to use the diverter setup tool are described in the user's manual in chapter "Assem-

bly" under the diverter settings.

2.4Layout sketch

As with ACOPOStrak dimensioning and programming, it is important to have an overview of the system. It is therefore

required to create a layout sketch with all the connections of the power supply and network. This sketch can be used to

check if the wiring matches the configuration to avoid errors during commissioning. For commissioning and diagnos-

## Page 11

MECHANICS OF ACOPOSTRAK11

tics, the node numbers and names of the segment variables should be noted in the sketch. This way, error messages

can be quickly traced back to the correct segment.

After the commissioning has been completed, it is recommended to store the latest layout sketch in the control cab-

inet.

The SVG file or the 2D version of the B&R Scene Viewer file can be used as the basis for the layout sketch. These are

created automatically during startup of the controller or the simulation.

Figure 15: Sample sketch of the training layout

NN: yyNode number of the segment (DNA or physical node number)

Linked segment variable in the configuration

Seg_x_yyx ... Track name

yy... Node number of the segment

Updating the sketch

If changes are made to the hardware or the configuration, the sketch must be adapted accordingly. An outdated sketch

makes it difficult to keep an overview and diagnose the system.

2.5Mechanical design

This chapter explains how to set up the mechanical parts. The documentation for this is available in the user's manual

in chapter "Assembly".

Layout for training

We will be working with the following layout:

## Page 12

12ASSEMBLY, COMMISSIONING AND DIAGNOSTICS FOR ACOPOSTRAK TM1423

Figure 17: ACOPOStrak layout for training with B&R Scene Viewer in

Figure 16: ACOPOStrak layout for training3D

Design

The layout for training is set up and adjusted using the assembly instructions from the user's manual.

Exercise: Overview of the assembly instructions

In the user's manual, the chapter "Assembly" describes how to set up ACOPOStrak. This contains detailed instructions

on how the system should be set up.

Before setting up the system, it is important to familiarize yourself with the instructions.

1)Open the chapter "Assembly" in the user's manual.

2)Obtain an overview of the instructions.

3)Read the individual steps of the instructions.

The mechanical assembly of the ACOPOStrak system can also be viewed as a video tutorial on the B&R

homepage at the following address:

https://www.br-automation.com/learn-track

## Page 13

GETTING STARTED13

3Getting started

After the entire ACOPOStrak system has been set up and adjusted, it can be commissioned.

3.1Checking the mechanical components

Before commissioning the system, the mechanical design is checked once again. A shuttle is guided manually along

the entire system to check the orientation of the segments and the functionality of the diverters. If a diverter is aligned

incorrectly, the shuttle may spontaneously change sides or a change of sides may require more attractive force.

Depending on the route, this causes the shuttle to change to the wrong side and to return an encoder signal amplitude

error when passing a diverter. This results in an assembly error stop, i.e. in a standstill of the entire system.

If an incorrect alignment is detected, alignment must be carried out again using the assembly instructions.

3.2Power supply and network

After the mechanical system has been assembled and checked, the power supply and the network are wired when the

power is switched off. For this, all cables are installed according to the layout sketch (see chapter 2.4 "Layout sketch"

on page 10).

Depending on the counting direction,

the pinout of the segments looks like

this:

The power supply can only be wired in

counting direction. The network can be

wired in both directions, with the POW-

ERLINK supply from the bus controller

connected via port 3 (middle port).

Figure 18: Pinout of segments

Segments are only permitted to be connected when the power is switched off!

Wiring under voltage will irreparably damage the segments.

After all cables have been connected, the wiring must be checked again for correctness. The earlier an error is detected,

the better it is in terms of corrections.

3.3Switching on the system for the first time

After the wiring has been completed and checked, the system can be commissioned step by step.

Application engineers

For commissioning ACOPOStrak, we recommend preparing a test application (see 3.4 "Test application" on page

15). This additional step helps with diagnostics since hardware and software are commissioned separately. Possi-

ble errors can thus be assigned to the hardware at the beginning, saving time during troubleshooting.

Compared to the machine application, the test application has a simpler design, which makes it possible to rule out

software errors during commissioning the system for the first time.

## Page 14

14ASSEMBLY, COMMISSIONING AND DIAGNOSTICS FOR ACOPOSTRAK TM1423

Figure 19: Complexity of the application

The application is transferred to the CFast card using an offline installation. This is then plugged into the slot on the

Automation PC (APC).

The final application of the machine can also be used for initial commissioning. It may be difficult, how-

ever, to assign errors to the hardware or software depending on the complexity. (Difference between the

test application and the final application of the machine Fig. 19 "Complexity of the application")

Switch-on behavior

After the application is prepared and installed on the CFast card, the system can be switched on for the first time.

When switching on the system for the first time, firmware updates are automatically installed on the individual com-

ponents. This may take a while. This procedure is not permitted to be interrupted. The entire POWERLINK network and

the ACOPOStrak segments are then initialized.

The status of the individual segments and the assembly status are in-

dicated via the test application.

After startup has been completed, the controller should be in RUN

mode and the assembly should be set to status "Disabled". If this is

not the case, the system must be checked with the diagnostic tools

to correct the error.

Figure 21: Status of the controller in the Automation Studio status bar

Figure 20: Assembly status in the application (ladder

diagram)

## Page 15

GETTING STARTED15

3.4Test application

As already mentioned in chapter "Switching on the system for the first time" on page 13, it makes sense to prepare

a separate test application for initial commissioning.

The hardware configuration used for this is the same as for the later machine application. The purpose of the test

application is to enable simple movements across the entire system.

The "Getting started" tutorial in Automation Help can be used as the basis for the test application since it covers the

basic functions, such as switching on or off or starting a movement. In addition, the movement parameters in the

"Getting started" tutorial can be changed at runtime.

Motion control/ mapp Motion / Guides / Getting Started / ACOPOStrak

Explanations are available in TM1415.

Figure 22: Function block for movements on the ACOPOStrak

If the system consists of multiple track segments, process points must be used in order to move the shuttles back and

forth between the individual tracks segments.

Information about the integration of the process points is available in TM1415.

For the test application, as well as for the later machine application, it is important to know the status and the current

information of the system. The following function blocks are available for readout:

Figure 24: MC_BR_AsmReadInfo_AcpTrak

Figure 23: MC_BR_AsmReadStatus_AcpTrak

## Page 16

16ASSEMBLY, COMMISSIONING AND DIAGNOSTICS FOR ACOPOSTRAK TM1423

There are also function blocks available to read the status and current information of the individual segments.

Figure 26: MC_BR_SegReadInfo_AcpTrak

Figure 25: MC_BR_SegReadStatus_AcpTrak

Motion control / mapp Motion / Libraries / Core / McAcpTrak / Function blocks

3.5First movements with the application

After the system has been successfully started, the first movement can be performed via the application.

First test run

The first movement is to guide an empty shuttle across the entire layout.

I is important to pay attention to unusual noises and shuttle behavior during the test. This way, incorrectly set diverters

or incorrectly set transitions can be detected in the guide system. Before the test is continued, any problems detected

must be solved.

If the first test run has been completed successfully, the speed can be slowly increased. Once that is done, the shuttle

behavior must be observed.

If the speed tests have been completed successfully, the number of shuttles can finally be increased.

The goal is to have the desired number of shuttles moving across the system at the maximum speed.

Next steps

The purpose of the first test was to check the functionality of the hardware and the alignment of the individual ele-

ments. Once these test have been completed successfully, the machine application can be tested.

## Page 17

DIAGNOSTICS OF ACOPOSTRAK17

4Diagnostics of ACOPOStrak

This chapter briefly introduces the diagnostic tools and diagnostic options available for ACOPOStrak.

4.1Logger

The Logger is the most important diagnostic tool in the context of ACOPOStrak.

In addition to errors and warnings, it also provides information about the machine processes.

Figure 27: User interface of the Logger

1)Buttons to update data

2)A selection of different data categories

3)Buttons to display data by type

4)Logger entry of type "Information"

5)Details of the selected Logger entry

Logger entry

A Logger entry can be divided into different types

Error

•

Warning

•

Information

•

Success

•

The most important information of a Logger entry includes the timestamp (time of entry creation), the affected com-

ponent (e.g. shuttle, sector, etc.) and the description of the event.

The help documentation provides more detailed information.

## Page 18

18ASSEMBLY, COMMISSIONING AND DIAGNOSTICS FOR ACOPOSTRAK TM1423

Click on <F1> to open the documentation for the selected entry in Automation Help. If no further docu-

mentation exists for the entry, only the help documentation will open.

Figure 28: Sample entry in Automation Help

4.2System Diagnostics Manager

System Diagnostics Manager (abbreviated "SDM") can be used in combination with ACOPOStrak as usual.

The hardware overview is particularly helpful for this.

The hardware overview shows all configured segments with the assigned node number. If the wiring of the segments

does not match the configuration, this would be displayed in the overview since the segments cannot be reached via

POWERLINK.

## Page 19

DIAGNOSTICS OF ACOPOSTRAK19

It is also possible to see here why it is an advantage to name the segments in Automation Studio after the segment

variable because this name is displayed as the equipment ID.

4.3Network command trace

The network command trace logs commands that are sent to and from the segments at the network level.

Automation Help provides documentation about which commands and parameters are recorded and how they are

interpreted.

The following use case shows how a diverter can be set correctly using the network command trace.

Motion control \ ACP10/ARNC0 \ NC diagnostics\ Network command trace

Setting a diverter using the network command trace

Due to manufacturing tolerances, the actual spacing between the segments in the diverter area may differ from the

configured distance. The configuration must therefore be adapted to the real system.

The network command trace is also used to log the initial positions at which shuttles are detected. Shuttles are de-

tected automatically when the assembly is switched on. Using the initial positions, the actual spacing of the diverter

can be easily calculated.

## Page 20

20ASSEMBLY, COMMISSIONING AND DIAGNOSTICS FOR ACOPOSTRAK TM1423

Figure 29: Placing the shuttles

For this purpose, a shuttle is placed in the diverter area (parallel area). The assembly is then switched on and the data

of the network command trace is loaded.

The position of the shuttles is one of the last entries for a segment, so it makes sense to sort the entries by node

numbers and jump to the last entries of the respective segments.

Figure 30: Output of shuttle positions in the network command trace

The first entry related to the shuttle position specifies the number of shuttles detected on the segment. Then, the

individual positions are read one after the other.

## Page 21

DIAGNOSTICS OF ACOPOSTRAK21

Figure 31: Positions and spacing in the diverter area

VariableDescriptionSpecifying a position

Pos1Position of the shuttle on Seg_A_2Relative to the start of the segment

Pos2Position of the shuttle on segment Seg_B_5Relative to the start of the segment

XSpacing for diverter configuration

YPosition of the shuttle on segment Seg_B_5Relative to the end of the segment

Table 1: Legend for Fig. 31 "Positions and spacing in the diverter area"

VariableDescriptionValue

Length of a straight line segment660 mm

Length of a curve input segment450mm

Length of a curve output segment450mm

Length of a circular arc segment240mm

Table 2: Segment lengths

The actual spacing can be identified very accurately using a simple calculation. The configuration can then be adapted

to the system.

The information in brackets describes length Y.

The network command trace can also be exported as a file via mapp Cockpit. Therefore, a connection

between Automation Studio and the controller is not necessary.

The uploaded file is then opened in Automation Studio for analysis.

## Page 22

22ASSEMBLY, COMMISSIONING AND DIAGNOSTICS FOR ACOPOSTRAK TM1423

4.4Diagnostics display

For diagnostics, the display is connected to the middle POWERLINK port

(port 3) of the segment with the corresponding cable. It provides data

about the node number of the segment, the firmware version used and

the current status.

It has 4 buttons that are used for navigation and operation.

Node number:

If the node number is displayed with prefix "DNA", the segment has the

physical node number 0 and is assigned the configured node number via

POWERLINK .

If the node number is displayed with prefix "NN", this means that this node

number was directly assigned to the segment. The node number can be

changed via the menu on the display and must be set to 0 in order to use

dynamic node allocation (DNA).

Figure 32: Display module

Connection sequence:

1)Connect the cable to the segment.

2)Connect the display to the cable.

Non-observance of the connection sequence can result in irreparable damage of the segment!

Display revision

Only displays with revision C0 or later are permitted to be used with ACOPOStrak segments!

Menu

The main page appears as soon as the display is connected to the

segment. It shows the firmware version used and the node number

with the corresponding prefix "DNA" or "NN".

Figure 33: Main page

## Page 23

DIAGNOSTICS OF ACOPOSTRAK23

Clicking on one of the 4 buttons takes you to the main menu where

you can navigate to the submenus via the 4 buttons.

Figure 34: Main menu

Submenu "Node number" is used to set the physical node number of

the segment. This must be set to 0 if DNA is used.

Figure 35: Submenu "Node number"

In submenu "ACP10 version", the currently used firmware version can

be read.

Figure 36: Submenu "ACP10 version"

Data about the display, such as the serial number and the revision,

can be read via submenu "Display information".

Figure 37: Submenu "Display information"

4.5mapp Cockpit

mapp Cockpit is a web-based HMI application used for the commissioning and diagnostics of mapp Motion compo-

nents. The most important area of application in combination with ACOPOStrak is tracing.

The design of the HMI application is explained in detail in Automation Help:

## Page 24

24ASSEMBLY, COMMISSIONING AND DIAGNOSTICS FOR ACOPOSTRAK TM1423

Diagnostics and Service / mapp Cockpit / Web-based HMI application

4.5.1Parameter IDs

Parameter IDs (abbreviated "ParIDs") are values that are used directly on the drive. They can be read and in some cases

also written to by the drive, i.e. by the segments.

Depending on the ParID, the validity of the ParIDs refers to the entire segment or to the individual axis channels.

Examples of ParIDs that apply to the segment are "DC bus", "Motor" and "Limit values".

ParIDs related to axis channels are e.g. "Position CTRL", "Velocity CTRL" and "Feed-forward CTRL".

A complete list of available ParIDs can be found in the help documentation.

Motion control / mapp Motion / Modules / McAcpSys / ACOPOStrak firmware / ACOPOStrak parameter

IDs

4.5.2Axis channel

When a shuttle moves to a segment, an axis channel is assigned to it. This is always the next free channel. If a shuttle

leaves a segment, the assigned axis channel is released again and is assigned to a subsequent shuttle.

The position of the axis channel is the position setpoint of the shuttle. The actual position of the shuttle is read again

via the encoder.

Figure 38: Assigning the axis channels

Sh1 ... Shuttle ID

•

AC1 ... Index of the assigned axis channel on segment Seg_A_2

•

This behaviour must be taken into account when tracing directly on the drive.

## Page 25

DIAGNOSTICS OF ACOPOSTRAK25

4.5.3Segment trace

By clicking on button "Add data point", a pop-up window opens. This can be used to select the desired segment that

is represented by the segment variable.

Figure 39: mapp Cockpit user interface

Since ParIDs are read directly from the drive, subgroup "Hardware" must be used. Different ParIDs can be selected here

and added to the trace configuration via "Add".

The data point is displayed as a string in the trace configuration:

*ACP:Type of data point

SL1.IF1.ST1_POWERLINK address of the segment

Axis1Index of the axis channel (0 for entire segment)

Figure 40: Data point as string:112Value of the ParID (e.g. 112 = Lag error)

Diagnostics and Service / mapp Cockpit / Web-based HMI application/ Trace / Trace Configuration View

Tracing different axis channels

To address and read the different axis channels, a new da-

ta point with the desired ParID is added and the axis chan-

nel is manually changed to the required index.

Figure 41: Trace configuration with multiple axis channels

## Page 26

26ASSEMBLY, COMMISSIONING AND DIAGNOSTICS FOR ACOPOSTRAK TM1423

4.5.4Shuttle-based trace

Unlike tracing ParIDs, the shuttle-based trace refers to a sector, not a segment. To be able to use the trace, assembly

feature "Shuttle trace" must be configured and referenced in the assembly configuration.

Figure 43: Referencing the assembly feature "Feature_Shuttle_Trace"

Figure 42: Configuration of feature "Feature_Shuttle_Trace"

For this, any sector is referenced in the configuration and the maximum number of shuttles is specified.

As soon as the new configuration has been transferred to the controller, the name of the sector appears in the list of

available data points in mapp Cockpit.

Figure 44: Data points of the shuttle trace with index

Since multiple shuttles can be traced simultaneously, the individual data points are provided with indexes between

1 and the configured maximum number. The segment-based data points of the shuttles have a second index since

a shuttle can be controlled by up to four segments at the same time (see Orange Box in Fig. 44 "Data points of the

shuttle trace with index").

4.5.5Results

Button "Analysis" opens a new window in which the data can be displayed.

To illustrate the trace data, the lag error and the speed setpoint of a shuttle in the diverter area were traced on the

segments with node numbers 17, 18 and 24 (see Fig. 45 "Section of the layout"). The shuttle moves from left to right

across the layout and leaves the diverter via segment "Seg_B_24".

## Page 27

DIAGNOSTICS OF ACOPOSTRAK27

Figure 45: Section of the layout

Figure 46: Trace analysis in mapp Cockpit

Figure Fig. 46 "Trace analysis in mapp Cockpit" shows the lag error and the speed setpoint of all three segments

combined in one diagram.

A positive lag error means that the actual position of the shuttle is smaller than the position setpoint. If the movement

is performed in the positive direction, this means that the shuttle lags behind the target position. In the middle of

segment "Seg_A_17", the lag error changes abruptly to negative. This jump is caused by the first diverter area because

the opposite segment slowly approaches the shuttle which is then pulled into the diverter area by attractive force. The

shuttle moves ahead of the position setpoint and the lag error becomes negative.

The stronger deflections of the lag error at the beginning and at the end of each segment are caused by the air gap

between the segments.

Since the shuttle on segment "Seg_B_24" moves against the positive counting direction, the lag error is negative al-

though the shuttle lags behind the position setpoint.

The data can be exported directly from mapp Cockpit for documentation purposes. To visualize and compare different

data sets, they can be imported into mapp Cockpit. The corresponding buttons are available above the display of the

data sets on the left side of mapp Cockpit.

4.6Closed POWERLINK ring

When wiring ACOPOStrak, it is possible that an ISC/ISC cable is swapped with a PLK/ISC cable by mistake. This means

that two POWERLINK lines are connected with each other.

If the system is switched on in this state, the assembly status changes to ErrorStop because of communication errors.

POWERLINK is initialized from the lowest node number in ascending order. As soon as a segment, which is located di-

rectly next to the error location, is initialized, the initialization is aborted. The segments are still recognized as stations

in the POWERLINK network. For this reason, it is not possible to localize the error location with System Diagnostics

Manager.

However, the following entries are stored in the Logger for all segments that cannot be initialized:

## Page 28

28ASSEMBLY, COMMISSIONING AND DIAGNOSTICS FOR ACOPOSTRAK TM1423

Figure 47: Warning: Cyclic network communication

Figure 48: Warning: Drive not synchronous with master

Figure 49: Error: Communication lost

The Logger entries can be used to find out which segment is affected first. The wiring is checked on this segment

using the layout sketch and the wrong PLK/ISC cable is replaced with an ISC/ISC cable. After a restart, POWERLINK is

initialized again.

## Page 29

MAINTENANCE AND SEGMENT REPLACEMENT29

5Maintenance and segment replace-

ment

5.1Service intervals for ACOPOStrak

The mechanics of ACOPOStrak are largely maintenance-free. Only the shuttles must be inspected at certain intervals,

since they are moving parts of the system.

The following tables contain the service intervals of the shuttles for different operating modes. The intervals are guide

values since the actual values may vary depending on the design and accuracy of alignment.

Shuttle maintenance

ACOPOStrak transport system design with diverter

ComponentService lifeActivity

All wheels30000 km at up to 2 m/s and typical payloadReplacing the wheels

ACOPOStrak transport system design without diverter

ComponentService lifeActivity

All wheels40000 km at up to 2 m/s and typical payloadReplacing the wheels

All wheels30000 km at up to 4 m/s and typical payloadReplacing the wheels

The typical payload depends on the shuttle type and is specified in the technical data of the user's manual.

It is recommended to visually inspect the shuttles in order to detect damage early.

For more information about shuttle maintenance, see the user's manual.

Maintenance intervals

ComponentService lifeActivity

All wheels4000 kmChecking wheels for wear

Lubrication felt2000 kmLubricating the lubrication felt

Anti-static brushes8000 kmVisual inspection

5.2Monitoring the service intervals

The kilometers traveled by the individual shuttles must be evaluated via the application.

Function block MC_BR_ShReadInfo_AcpTrak is used for this. Current values, route and maneuver information as well

as life cycle values are available at output "ShuttleInfo".

The lifecycle values include the distance covered since the

last shuttle identification. The data is linked to the respec-

tive shuttle via the shuttle reference. The data is lost in

the event of a power failure or if the shuttle is deleted (e.g.

during interlinking).

Figure 50: Function block MC_BR_ShReadInfo_AcpTrak

## Page 30

30ASSEMBLY, COMMISSIONING AND DIAGNOSTICS FOR ACOPOSTRAK TM1423

The data must be uniquely identified so it can be permanently linked to a shuttle. For this purpose, each shuttle has a

unique QR code on both sides by default. A camera can be used to identify the shuttle and link the data.

If a shuttle exceeds the service interval, a warning (e.g.

pop-up window as in Fig. 51 "Example of a warning in

mapp View HMI application") can be displayed via the HMI

application or the shuttle is automatically sent to a main-

tenance station via the application.

The design can be individually adapted to the machine.

Figure 51: Example of a warning in mapp View HMI application

5.3Maintenance

5.3.1Maintenance of the shuttles

Maintenance work must be performed on the shuttles at the specified service intervals.

These include:

Lubricating the lubrication felt

•

Replacing the wheels

•

Replacing the anti-static brush

•

Replacing the lubrication felt

•

Detailed instructions for maintenance work are available in the user's manual.

The instructions can be found in the technical data of the guide system in under "Maintenance".

5.3.2Segment replacement

If a segment breaks, the modular design of ACOPOStrak makes it possible to quickly replace the segment with minimal

effort.

Impermissible operation of the segments leads to irreparable damage.

Procedure

The segment replacement can be divided into the following steps:

Remove the screws in the flat guide rail.

•

Lower the flat guide rail.

•

Remove the screws in the upper guide rail.

•

Replace the segment.

•

Screw the segment to the upper guide rail.

•

Lift the flat guide rail.

•

Screw the flat guide rail.

•

The segments are removed according to the same instructions as used for installation, only in reverse order.

The instructions for installing the segments are available in chapter "Assembly" in the user's manual.

## Page 31

SUMMARY31

6Summary

ACOPOStrak is the flexible track system for ultimate production efficiency.

The core of the track system is a linear motor assembled from four types of modular segments: A straight segment, a

45° segment and two 22.5° segments – one curved to the right, the other to the left. With its modular, flexible system

concept, ACOPOStrak enables completely new machine designs.

The modular design, which is continued in the guide system, allows ACOPOStrak to be adapted and integrated to the

respective process.

Figure 52: ACOPOStrak

ACOPOStrak can be quickly set up and commissioned by using the comprehensive documentation.

As one of the product highlights of ACOPOStrak, the fully electronic high-speed diverters not only allow product flows

to be diverted and merged. Without interfering with the mechanics of an existing assembly, the layout can be extended

with additional track segments as desired.

Multiple tools that are directly integrated in Automation Studio or the controller allow comprehensive diagnostics of

ACOPOStrak.

The mechanical parts of ACOPOStrak are nearly maintenance-free. Only the shuttles must be inspected regularly. The

possibility of replacing shuttles during operation reduces the time required for maintenance work.

In addition, the modular design and process-oriented software concept of ACOPOStrak allow it to be combined with

conventional conveyor belts. Implementation with ACOPOStrak can thus be limited to certain plant parts and process

areas, which results in more flexibility and cost efficiency.

## Page 32

32ASSEMBLY, COMMISSIONING AND DIAGNOSTICS FOR ACOPOSTRAK TM1423

Further information

Further sources of information are available to deepen participants' understanding of the previous top-

ics.

TopicSource

Technical data ACOPOStrak hardwareACOPOStrak user's manual

## Page 33

AUTOMATION ACADEMY33

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

## Page 34

34 ASSEMBLY, COMMISSIONING AND DIAGNOSTICS FOR ACOPOSTRAK TM1423

## Page 35

AUTOMATION ACADEMY 35

## Page 36

B&R

Industrial Automation GmbH

A member of the ABB Group

B&R Straße 1

5142 Eggelsberg, Austria

office@br-automation.com

t +43 7748 6586-0

f +43 7748 6586-26

br-automation.com

V2.0.0.0 ©2023/10/30 by B&R, All rights reserved.

All registered trademarks are the property of their respective owners.

Subject to technical changes without notice.