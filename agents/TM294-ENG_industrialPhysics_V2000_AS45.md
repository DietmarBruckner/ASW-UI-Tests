## Page 1

TM294

industrialPhysics

## Page 2

2 INDUSTRIALPHYSICS TM294
Requirements
Necessary basic
Create structured applications in Automation Studio.
knowledge
Training modules TM210 - Working with Automation Studio
industrialPhysics 2.4.x
Software
Automation Studio 4.3 and later
Hardware None

## Page 3

TABLE OF CONTENTS 3
Table of contents
1 Introduction.........................................................................................................................................................4
1.1 Learning objectives..............................................................................................................................5
1.2 Symbols and safety notices...............................................................................................................5
2 General information...........................................................................................................................................6
2.1 Simulation..............................................................................................................................................6
2.2 Model-based development.................................................................................................................7
2.3 Virtual commissioning........................................................................................................................8
3 industrialPhysics...............................................................................................................................................10
3.1 User interface......................................................................................................................................10
3.2 CAD import..........................................................................................................................................15
3.3 Object properties...............................................................................................................................15
3.4 Knowledge center..............................................................................................................................18
4 Working with industrialPhysics.....................................................................................................................19
4.1 First steps in industrialPhysics.......................................................................................................19
4.2 Light barrier and extended drive...................................................................................................24
4.3 Connecting with Automation Studio............................................................................................26
4.4 Creating new components..............................................................................................................32
4.5 Activating the rotary pusher...........................................................................................................36
4.6 Pallet truck pusher with rails..........................................................................................................37
4.7 Dynamic gripper with script trigger..............................................................................................41
4.8 Pallet lift with virtual gripper.........................................................................................................46
4.9 Pallet conveyors.................................................................................................................................47
5 Summary............................................................................................................................................................49

## Page 4

4INDUSTRIALPHYSICS TM294

1Introduction

Automation Studio offers end-to-end simulation that allows applications to be configured and tested without requir-

ing hardware. By importing CAD data into , digital twins of plants can be created and then connectedindustrialPhysics

to Automation Studio.

By creating a communication task in industrialPhysics, existing Automation Studio projects can be connected to the

simulated systems.

ControllerDevelopment

HMISimulation

Motion controlCommissioning

Safety technologyDiagnostics and

Service

Figure 1:  Automation Studio: One engineering tool for the machine's entire lifecycle

## Page 5

INTRODUCTION 5
1.1 Learning objectives
This training module gives participants insight into the basics of simulation in industrialPhysics and how to handle
the interface to Automation Studio.
Participants will get an overview of terminology related to virtual commissioning and simulation.
•
Participants will receive information about the development process ranging from the model to simulation and
•
virtual commissioning.
Participants will get an overview of the different levels of simulation and the respective areas of application.
•
Participants will learn how to work with industrialPhysics.
•
Participants will learn how to make individual elements in an industrialPhysics project dynamic.
•
Participants will learn how to create projects in industrialPhysics.
•
Participants will learn how to establish an online connection between an industrialPhysics model and an Automa-
•
tion Studio project.
1.2 Symbols and safety notices
Unless otherwise specified, the symbol descriptions and safety notices listed in "TM210 – Working with Automation
Studio" apply.

## Page 6

6INDUSTRIALPHYSICS TM294

2General information

The sharp increase in production requirements, the expanding complexity of systems and the development of greater

numbers of technical products with software all make simulation an essential component of the development process.

The following image shows the typical B&R simulation levels, which differ in their level of detail. A distinction is made

between the automation hardware level, the component and machine level and the process and plant level.

Starting at the base, you have simulation of hardware and software components. Automation Studio supports all

possible simulation options.

Dynamic processes of machines and components can be simulated on the second level. For this, B&R offers connection

to the most popular simulation tools on the market.

The third level enables simulation of complex system processes. For this type of simulation, B&R Automation Studio

provides appropriate interfaces for external software such as industrialPhysics.

Figure 2: Simulation levels: Plants and processes, machines and components, hardware

2.1Simulation

Simulation is an experimental approach to determine static and dynamic properties of a system based on a model. The

model makes it possible to gain knowledge about the system and its behavior. This knowledge can then be transferred

to reality. For the digital representation of the original system, it is irrelevant whether the system to be examined

already exists or is still in planning stage. The image below shows a simplified procedure for creating a simulation

model.

## Page 7

GENERAL INFORMATION7

Figure 3: Simplified representation of a simulation.

Advantages industrialPhysics simulation with regard to software and system development

Possibility to analyze extremely extensive or complicated systems

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

Model-based development is based the principle of simulation. A complex process is first represented in an abstract,

realistic model. It is important that all processes necessary for development are contained in the simulation model

since any further development is based on it. During the test phase, continuous improvements can be made based on

the test results from the model. This process is displayed in the following diagram.

## Page 8

8 INDUSTRIALPHYSICS TM294
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
2.3 Virtual commissioning
Virtual commissioning refers to the testing of the simulation model in a virtual environment in order to simulate real
commissioning as accurately as possible. In most cases of virtual commissioning, the simulation model is a virtual
machine. Data is imported, tested and changed on the virtual machine before the software is transferred to the real
machine. An executable model enables the software to be tested on a workstation computer or on a real-time system
at an early stage.
An important aspect during virtual commissioning of mechanical processes is the 3D representation that simulates
machine behavior and thus provides visual feedback for the tester.
It should be noted, however, that virtual commissioning does not replace real commissioning completely. In mod-
el-based development, two test procedures have proven to be effective: Software-in-the-loop (SiL) and hardware-in-
the-loop (HiL).

## Page 9

GENERAL INFORMATION 9
2.3.1 Software-in-the-loop (SiL)
Software-in-the-loop is a test procedure used for models in a simulation environment. During a software-in-the-loop
simulation, the software that has been developed and the simulation environment are executed on the same hardware.
The software communicates with the simulation, which is running on the same processor, via global variables or vari-
able mapping.
2.3.2 Hardware-in-the-loop (HiL)
Hardware-in-the-loop is a test procedure for evaluating a model on the real hardware used during commissioning. A
real-time simulation is used for the test system in order to represent a controlled system for a closed-loop controller
as accurately as possible. An HiL system provides the controller with all input and output signals that would exist in
the real environment.

## Page 10

10INDUSTRIALPHYSICS TM294

3industrialPhysics

3.1User interface

Figure 5: User interface

1)File toolbar

3D toolbar

2)Simulation toolbar

3)Component tree

4)Model view

5)Properties window

6)Structure toolbar

Component toolbar

Joints

Robots

PLC development

Kinematics

Motion control

Recording and playback

File toolbar

Figure 6: File toolbar

## Page 11

INDUSTRIALPHYSICS11

1)Create a new document; existing data is deleted

2)Open an existing document

3)Save the opened document

4)Save the opened document under a new name

5)Import a STEP document

6)Export a VRML document

7)One work step back

8)One work step forward

3D toolbar

Figure 7: 3D toolbar

1)Zoom the whole model (Zoom Fit)

2)Zoom the selected component

3)Select a standard view

4)Edit user-defined view

5)Change between perspective and orthogonal view

6)Navigation, follow mode and SpacePilot

7)Choice of manipulator: None, coordinate, translation or rotation

8)Shadow on/off

9)Lighting management

10)Show physics information

11)Arrange windows automatically

12)Distance and angle measurement

13)Save 3D screenshot

## Page 12

12INDUSTRIALPHYSICS TM294

Simulation toolbar

Figure 8: Simulation toolbar

1)Switch between simulation and playback

2)Create new simulation (easy or difficult reconstruction)

3)Reset simulation

4)Start the recording.

5)Starting the simulation

6)One simulation step forward

7)Switch on HiL mode

Structure toolbar

Figure 9: Structure toolbar

1)Search in model structure

2)Add new element

3)Cut element

4)Copy element

5)Add element

6)Drag-and-drop hierarchy manipulation on/off

7)Merge selected shapes

8)Show/Hide model structure

9)Reset the visibility of a component

10)Delete component

11)Bucket management

12)Suppress element on/off

13)Axis manager

14)Rail connections

## Page 13

INDUSTRIALPHYSICS13

Component toolbar

Figure 10: Component toolbar

1)Change component to "Invisible"

2)Physical property (immaterial, static, kinematic, dynamic or subcomponent)

3)Selection of collision type

4)Selection of collision body

5)Collision detection on/off

6)Debugging log on/off

7)Open component properties

Joints

Figure 11: Joints

1)Create joint

2)Show joint connections

Robots

Figure 12: Robots

1)Robot control

2)Add robot kinematics

PLC development

Figure 13: PLC development

1)Add script

2)Manage all model scripts

Kinematics

Figure 14: Kinematics

1)Add translational or rotational kinematics

## Page 14

14INDUSTRIALPHYSICS TM294

Motion control

Figure 15: Motion control

1)Motion control

2)Add object for sequence control

3)Add chain

4)Add input device

5)Add user-defined motion control

6)Motion control management

Recording and playback

Figure 16: Recording and playback

1)Manage recorded simulation tracks

2)Create a video from the current simulation track

## Page 15

INDUSTRIALPHYSICS15

3.2CAD import

Before a machine can be made dynamic, the CAD data must be imported into industrialPhysics.

This is done using one of the following two options:

Bidirectional CAD interface:

Depending on the license models of the following CAD programs, there is a toolbox with an interface that can be used

to transfer the CAD data:

AutoCAD Inventor

•

CREO Parametrics

•

IronCAD

•

SolidEdge

•

SolidWorks

•

Importing STEP data:

Almost any CAD program can export CAD data in STEP format. This format is standardized according to application

log ISO 10303-2xx STEP.

Formats AP203 and AP214 were developed for mechanical CAD data. AP214 is the most recent format with more fea-

tures and should therefore be used. AP214 is sometimes called "STEP with colors".

?/Knowledge center/doc/20_CAD/

3.3Object properties

Physical properties

Immaterial: These components are excluded from any physical calculation (de-

fault setting). They have no weight and no collision body.

Static: Static objects have a collision body that does not move along with the

object.

Kinematic: Kinematic objects behave like static objects, with the difference

that the collision body moves along with the object.

Dynamic: Dynamic objects have a weight in addition to a collision body. They

react to all physical events such as accelerations caused by collisions and grav-

Figure 17: Physical properties

ity.

Subcomponent: A subcomponent inherits all the physical properties of the

component above it.

## Page 16

16INDUSTRIALPHYSICS TM294

Collision types

No collision: Structure behaves as if it has no solid object geometry.

Normal collision: Default setting.

A body behaves like an ideal solid object when colliding with dynamic objects.

Sticky surface: Dynamic objects adhere to the surface.

Unlike grippers, they are permanent and cannot be deactivated.

Light barrier: During penetration with a dynamic body, one status output is

set to TRUE.

ID scanner: Obsolete; replaced by RFID reader

6D tracking: Detects the position of a dynamic object as 6D transformer.

2D/1D tracking: Detects the position of a dynamic object as 3D vector.

Kinematic gripper: Permanently defines a maximum of one object at penetra-

tion

Status returned as signal "Vacuum".

Can be switched off.

Compliant gripper (dynamic): Permanently defines objects when penetrating

at collision position or by aligning the transformers.

Status returned as signal "Vacuum".

Can be switched off.

Moving surface (linear conveyor): Dynamic objects are moved by friction in the

direction of a specified vector.

Rotating surface (circular conveyor): Dynamic objects are moved by friction

circularly around a specified vector.

Transient source: Generates transient objects based on a time or trigger.

Transient sink: Destroys transient objects during collision. Can be switched

off.

Live statistics: Like IR light curtain.

Additional counter for the total number of objects detected currently and dur-

ing simulation runtime.

Black box process: Representation of a process as a black box with release

time (cycle time), capacity and release direction.

Captures dynamic objects at penetration, stacks them according to the direc-

tion defined if capacity is available and keeps them for the release time de-

fined.

If capacity is exceeded: Function "Normal collision" until capacity is available.

Figure 18: Collision types

Force field: Dynamic objects are moved in the direction of a specified vector.

Rotating force field: Dynamic objects are moved circularly around a specified

vector.

## Page 17

INDUSTRIALPHYSICS17

Collision types

RFID reader: Can read data from a dynamic object in the event of a collision.

RFID writer: Can read and write data from a dynamic object in the event of a

collision.

Gripper suppression: Components gripped by a gripper are released at pene-

tration with "Suppress gripper".

Forming process: Creates a continuous line of volumetric flow rate from an in-

coming component.

Cut a transient component using command "Cut".

Continuous source: Creates a continuous, transient line.

Cut a transient component using command "Cut".

Continuous forwarder: Conveys incoming transient components from a con-

tinuous source along the z-axis.

Continuous sink: Continuously destroys an incoming line of components from

a continuous source.

Cutting blade: Splits objects at penetration on the plane that contains the ori-

gin of the blade and lies parallel to the x-y plane of the object to be split.

Cut only takes place at the edge of signal "Cut" during collision.

Continuous looper: Memory of a continuous product flow. Integrates via the

difference between input and output speed and stores up to a defined capaci-

ty limit value.

Geometric sensor: Provides the minimum and maximum position of the object

boundaries in relation to the z-axis of the sensor during penetration.

Touch probe sensor: Similar to a light barrier with an additional option of de-

tecting the boundaries of the penetrating object along the z-axis of the sen-

sor.

Figure 19: Collision types

## Page 18

18INDUSTRIALPHYSICS TM294

3.4Knowledge center

Figure 20: Knowledge center

The industrialPhysics knowledge center can be opened via the menu bar and contains several examples and documents

for the various functions.

## Page 19

WORKING WITH INDUSTRIALPHYSICS19

4Working with industrialPhysics

Figure 21: End-of-line example

The following examples are based on a palletizing machine for beverage cartons. 14 cartons are collected on a platform

and then stacked into two layers on a pallet. When the pallet is full, it is passed on and a new empty pallet is lowered

from a stack onto the conveyor belt.

In the next chapters, the following steps are carried out to make the machine completely dynamic:

Activate the infeed conveyor belt

•

Create a dynamic source from the product

•

Activate the first pusher

•

Commission the photoelectric sensors for packet detection

•

Activate the two other conveyor belts

•

Add a second photoelectric sensor

•

Activating the rotary pusher

•

Activate the carriage

•

Activate the entire pallet truck including pusher and suction cup for cardboard

•

Activate the pallet lift

•

Commission the pallet conveyor belts for infeed and outfeed

•

4.1First steps in industrialPhysics

Coordinate system

Each element in industrialPhysics has its own coordinate system. All movements, such as conveyor belt movements or

pure translations, are performed based on this coordinate system. The directions are defined as follows:

XRed

YGreen

ZBlue

Figure 22: Coordinate system

## Page 20

20INDUSTRIALPHYSICS TM294

To adjust the coordinate system of a body, first open the component window (ALT + SHIFT + M or right click in the

model structure -> Component). By selecting  under the  tab, the coordinateModify transformationsTransformation

system can be moved and rotated.

Figure 23: Component properties -> Transformation

Physical properties

As described in chapter 3.2, each component must be assigned one of the following properties.

ImmaterialImmaterial components cannot move, have no collision body and therefore cannot

have a collision type. All imported components are immaterial by default.

StaticStatic components have a collision body and can be assigned a collision type. The

function of the collision body is only available at the original position of the compo-

nent since the collision body of a static component does not move with it. (e.g. light

barriers, conveyors, etc.)

KinematicKinematic components are similar to static components, with the difference that

their collision bodies move along. (e.g. gripper, pusher)

DynamicDynamic components have the same properties as kinematic objects. In addition, dy-

namic components are affected by gravity and only these components can be detect-

ed by light barriers or moved by grippers. (e.g. cardboard boxes)

Table 1: Physical properties

## Page 21

WORKING WITH INDUSTRIALPHYSICS21

SubcomponentSubcomponents inherit all properties from the main component. (e.g. A cardboard

box consists of cardboard and adhesive tape. A cardboard box is dynamic, cardboard

and adhesive tape are subcomponents)

Table 1: Physical properties

Drive

A drive must be configured for many components (conveyor, force field, translation, rotation, etc.). There are a variety

of drives, which are explained in the following table.

Direct driveThe component moves abruptly to a specified position. (v and a = ∞)

Speed driveThe component moves at a specified speed. (a = ∞)

Ramped driveThe component accelerates as configured at the specified speed.

Positioning driveThe component moves towards the specified position at the configured speed

and acceleration.

MotionControl drivePositioning drive with extended setting options via I/Os. (e.g. jerk time, etc.)

PID drivePositioning drive that is controlled via a PID controller.

Vibration driveThe component vibrates at the specified amplitude and frequency.

Generic driveSpecial drive used for TechnologyPlugins

Table 2: Drive

Conveyor

If a conveyor is used, the direction of movement must be defined in addition to selecting the drive. This is done by

specifying a vector in the coordinate system of the object under  in the option menu.Direction

If the forward direction of a  with  does not match the desired logical direction, itconveyorramped drive

can be corrected without changing the coordinate system by entering a negative number.

Figure 24: Conveyor - Direction

## Page 22

22INDUSTRIALPHYSICS TM294

Source

During simulation, new objects can be created via sources. The new objects are created in certain time intervals or

via a trigger variable based on a reference object. The newly generated objects are always , regardless of thedynamic

physical properties of the source.

When a component is used as a source, the following properties can be configured.

Figure 25: Source

Respect IRTime-based or trigger-based releases are only performed if no other dynamic

object is in the release area.

Use complexIf the reference object consists of subcomponents, it is possible to decide

geometrywhether the subcomponents or a simplified overall geometry should be created.

Generate fromIn order to generate dynamic components, a source must be created from ob-

prototypeject "NoShape" via the dynamic component and this option must be selected.

The display of the prototype is suppressed and new instances are generated

from the prototype instead. The source object is therefore no longer visible.

Use time frame /New objects are generated (time-controlled or trigger-controlled).

Use trigger signal

Transient parametersInitial speed and properties with regard to graphical representation can be set

for the newly generated object.

Table 3: Source settings

## Page 23

WORKING WITH INDUSTRIALPHYSICS23

Task: Conveyor belt

This section shows how to open the previously described model and make its first components dynamic.

The cardboard boxes are generated at the infeed conveyor belt and

transported forward on the conveyor belt.

Adjust the properties of the required components and test the proce-

dure on the first machine part.

Figure 26: Infeed

1)Open iPhysics file .End_of_Line_Packaging_EN.iphx

2)Change the property of the  to .floorStatic

3)Change the property of the  to  and the property of the subelement to .boxDynamicSubcomponent

4)Start the simulation. What do you expect to happen?

5)Change the property of  to  and select  as the collision type.Conveyor Belt_InfeedStaticMoving surface

6)Adjust the conveyor.  and )(directiondrive

7)Create a  defined as the .sourcebox

8)Start the simulation. What do you expect to happen?

## Page 24

24INDUSTRIALPHYSICS TM294

4.2Light barrier and extended drive

Light barrier

Light barriers detect the penetration of a dynamic element into the collision body of the light barrier. The status is

indicated by a BOOL variable. Another option is to decide whether this event should be displayed by the industrial-

Physics light barrier. The signal can also be inverted.

Figure 27: Light barrier

Extended drive

When drives are used for (translational) movement of objects, they must be configured with realistic limits.

It is also possible to scale the movement of a drive and define cam plates via this menu. These options are only needed

if models are made dynamic entirely in industrialPhysics by scripts as opposed to controlling them via a PLC connection

(as in this case).

Figure 28: Drive - Limits

Buttons "- " and "+" can be used to change the position during active simulation. The limits determined this way must

then be entered under .Mechanic limits

If the forward direction of a translational movement does not match the desired logical direction, the

local coordinate system of the object must be rotated. (see chapter 4.1 - Coordinate systems)

Task: Pusher

In this section, further components of the palletizing machine are put into operation.

## Page 25

WORKING WITH INDUSTRIALPHYSICS25

To prevent the boxes from falling off the conveyor belt, the belts

and limits must be set to "Static". It is necessary to detect when a

box arrives at the end of the conveyor belt in order to trigger push-

ing it onto the next belt. Adjust the properties of the required com-

ponents and test the entire procedure.

Figure 29: Infeed

1)Change the property of the  and the  to .limitbeltsStatic

2)Change the property of the  to  and the  to .sensorStaticcollision typeLight barrier

3)Change the property of the  to  and activate pusherKinematicTranslation in X.

4)Adjust the .  and )pusher(drive with rampmechanical end stops

5)Start the simulation and test the functionality.

## Page 26

26INDUSTRIALPHYSICS TM294

4.3Connecting with Automation Studio

Configuration options

To establish a TCP connection with Automation Studio, it must first be configured in industrialPhysics. Different meth-

ods can be used for this, depending on the licenses. The HIL wizard is available with every full version. The industri-

alPhysics B&R runtime license does not include this feature. In this version, the TCP connection must be configured

manually.

Manual configuration of the TCP connection

If an HIL connection does not yet exist in the project, it must first be created. This is done in the ComTCP view. (HIL

-> ComTCP view or Alt+Shift+T)

Figure 30: Adding the HIL connection

In the following window, it is necessary to specify a unique name, the functionality of industrialPhysics (TCP server or

client), the address of the computer where the TCP server is running and the port.

Figure 31: Settings for the HIL connection

In order to transfer inputs and outputs via TCP, they must be added to the HIL connection configuration just created

by right-clicking in the I/O tab containing the components.

Figure 32: Adding variables to HIL connection configuration

When all inputs and outputs are defined, the packet must be generated and then imported into the AS project as

described in Communication with Automation Studio.

## Page 27

WORKING WITH INDUSTRIALPHYSICS27

HiL wizard

Automation Studio and industrialPhysics are connected via a communication task generated in industrialPhysics. This

is done via menu option .HIL -> Start wizard

The connection must be given a unique name and the IP address of the computer where industrialPhysics is running

must be entered. For ARsim: 127.0.0.1. Any port can be used. The package is generated in the specified directory.

Figure 33: HiL wizard

## Page 28

28INDUSTRIALPHYSICS TM294

Labeler

When creating a HiL configuration, only I/Os that are used can be preselected via the labeling editor.

Figure 34: Labeler

It is generally advisable to suppress all variables using a  filter in order to allow individual variables for individualReject

components.

## Page 29

WORKING WITH INDUSTRIALPHYSICS29

Communication with Automation Studio

To establish a connection with Automation Studio, the package generated in industrialPhysics must be imported. This

means that folder MNG is created in folder "Libraries", which contains a task and a library. Task MNG_Prg should be

executed in a 10 ms task class and the library must be added to the project.

Figure 35: Automation Studio

The communication variables are global structures called "SimInputs" and "SimOutputs".

If the communication package runs on the controller, the node status can be checked in industrialPhysics under

(Alt + Shift + T or HiL -> ComTcpView).ComTcpView

Figure 36: ComTcpView - Node status

?/Knowledge center/doc/40_HIL/BuR/HowTo/Howto_DE_HIL_BuR.pdf

Update HIL connection

If connection settings have been changed or connection variables removed, the  buttonGenerate HIL configuration

can be used to recreate the communication package without having to run the wizard again.

To add new variables, you must run the HIL wizard, however. This is also because the  should always be usedIO filter

in the .Label editor

## Page 30

30INDUSTRIALPHYSICS TM294

Figure 37: Create HIL configuration

The ZIP file created must then be imported into the Automation Studio project again.

## Page 31

WORKING WITH INDUSTRIALPHYSICS31

Exercise A: Automation Studio

This section uses the HIL wizard to establish a connection to Automation Studio.

In order to control the components with Automation Studio, the TCP

connection must be configured via the wizard.

Figure 38: HiL wizard

1)Start the wizard under  -> .HILStart wizard

2)Select the target platform .BuR Automation Studio 4

3)Configure the ComTCP basic settings.

4)Select the desired inputs and outputs.

5)Generate the communication package.

6)Import the ZIP file created into an AS project.

7)Add library  to your project. (10 ms task)MNG

Exercise B: Automation Studio

In this section, a connection to Automation Studio is established by manually configuring the HIL connection.

In order to control the components with Automation Studio, the

TCP connection must be established using the manual configuration

possibilities.

1)Create a TCP communication configuration.

2)Configure the basic settings. (Name, Server/Client, IP, Port)

3)Add industrialPhysics variables to this TCP communication configuration.

4)Generate the communication package.

5)Import the ZIP file created into an AS project.

6)Add library  to your project. (10 ms task)MNG

## Page 32

32INDUSTRIALPHYSICS TM294

4.4Creating new components

Elements

If components are required in addition to those imported from CAD data, they can be added directly in industri-

al Physics. Apart from the 4 standard shapes cuboid, ball, cylinder and cone, there is a special component called

"NoShape". This is an intangible parent node that can be used for grouping real physical components.

Figure 39: Creating a new component

Position and an alignment can already be specified during creation. If the default settings are kept, however, the object

will be created exactly in the center of the coordinate data of the node that is set before the object is added.

## Page 33

WORKING WITH INDUSTRIALPHYSICS33

Positioning

All components in industrialPhysics can be repositioned at any time. This is done by either holding and dragging the

translation arrows or by making changes in window "Component properties".

Figure 41: Translation

Figure 40: Coordinate

## Page 34

34INDUSTRIALPHYSICS TM294

Figure 42: Component properties -> Transformation

Size

The size of elements added to industrialPhysics can also be changed in window "Component properties" under menu

option  if this was not done during creation.Shape

Figure 43: Component properties -> Shape

## Page 35

WORKING WITH INDUSTRIALPHYSICS35

Task: New components

This section shows how to add new components to a project.

In order to optimize the time sequence for sorting, an additional light

barrier is used to detect the correct position of the previous box.

Figure 44: New component: Light barrier

1)Change the property of  and  to  and to .Conveyor_Belt_RotateConveyor_Belt_EndStaticMoving surface

2)Adjust the conveyor belt settings. (direction and drive)

3)Change the property of  to .Belts_InfeedStatic

4)Add a new box object below node .mb3

5)Adjust the new object. (position, size, color)

6)Change the property of the new object to  and .StaticLight barrier

7)Test whether the new light barrier detects the correct position of the eighth box.

## Page 36

36 INDUSTRIALPHYSICS TM294
4.5 Activating the rotary pusher
Friction
industrialPhysics does not differentiate between static and dynamic friction. Each body has a fictitious friction coeffi-
cient that can be set under Physics in window Component properties. In the event of collision between two objects, the
resulting friction coefficient is calculated multiplicatively from the two friction coefficients and from the global friction
coefficient. The global friction coefficient is defined in the project settings. (Edit -> Properties -> Physics Simulation)
Recoil
Impact modeling is carried out analogously to the friction by multiplying the impact coefficients of the two compo-
nents and the global impact coefficient.
The number of impacts can also be determined by a drop test.
Based on v =v' = 0, the following applies:
2 2
k = 0: Completely plastic impact
k = 1: Completely elastic impact
?/Knowledge center/doc/10_General/17_Friction/Howto_DE_MOD_FRICTION.pdf

## Page 37

WORKING WITH INDUSTRIALPHYSICS37

Task: Rotary pusher

This section shows how to make the rotary pusher dynamic in order to fill the pallet truck with the correct number

and orientation of boxes.

The pallet should be filled with one row of eight boxes in the vertical

direction and two rows of three boxes each in the horizontal direc-

tion. In order to rotate the last six packages, the rotary pusher is ex-

tended.

Figure 45: Rotary pusher

1)Change the property of  to .Rotary pusher<2>Kinematic

2)Add  to the  higher-level element.Translation along Xrotary pusher

3)Change the collision body of  to .Rotary pusher<2>Cylinder along Z

4)Adjust the settings of the . and )rotary pusher(drive mechanical end stops

5)Change the property of the  to  and .carriageKinematicTranslation in Y

6)Adjust the settings for the . and )carriage(drive mechanical end stops

7)Change the property of  to .Pallet_LocationStatic

8)Adjust the  and  of the .frictionrecoilrotary pusher

9)Create a new ComTCP package and import it into Automation Studio.

10)Adjust your AS project in order to move a complete pallet of boxes onto the crane platform.

4.6Pallet truck pusher with rails

Curves

If elements should follow more complex paths instead of being moved in simple translational or rotational directions,

curves must be defined.

The curve is defined in window "Component properties". To move a component along a curve, the curve must be de-

fined in parent node "NoShape".

Figure 46: Curves - Model structure

## Page 38

38INDUSTRIALPHYSICS TM294

Figure 47: Component properties - Curves

To create a new curve, click on button "Add a new curve". The following settings can be made:

VisibilityShows or hides the curve.

RailTurns the curve into a rail to which dynamic "track cars" adhere automatically.

DriveA drive can be selected for rails.

ForcesForces can be activated for rails.

FrictionA friction is defined on the curve.

ColorDefines the color of the curve.

X, Y, Z, Red X, Red Y, Red ZTransforms the starting point and the alignment of the curve

Table 4: Curve properties

To add individual elements to the curve, right-click in the white field and select the desired object.

When an arc is added, the coordinate system rotates with the arc.

?/Knowledge center/doc/10_General/12_Chain/101_Modeling_chain_finished.iphx

Task: Curve-based pusher

To push the boxes onto the waiting pallet, the pusher must be made dynamic.

## Page 39

WORKING WITH INDUSTRIALPHYSICS39

The pusher is guided by two belts. In order to perform this move-

ment, a curve must first be defined. The pusher will then follow the

movement of the curve.

Figure 48: Pallet truck pusher

1)Change the property of the  of the pallet truck to .platformKinematic

2)Add a new  below node .NoShape objectPortal

3)Rearrange elements  and .PusherBelt

4)Add a new  to node .curveBelt

5)Add individual  in order to adjust the path of the belt.curve elements

6)Add a  to node  and configure it.chainBelt

7)Change the property of the  to  and test the movement using the chain drive.pusherKinematic

8)Change the property of the  to .portalTranslation in X

Solution: Rail configuration

## Page 40

40INDUSTRIALPHYSICS TM294

Figure 49: Solution: New NoShape element

Figure 50: Solution: Rail

## Page 41

WORKING WITH INDUSTRIALPHYSICS41

4.7Dynamic gripper with script trigger

Dynamic gripper

Dynamic objects can be detected and moved by . A BOOL input and a BOOL output are used todynamic grippers

operate this gripper.

The gripper is switched on via input  so that dynamic objects adhere to the gripper object and can thus beGripper

moved along or held in place.

Output  provides feedback as to whether the gripper has a dynamic object adhering to it.Vacuum

There are the following additional setting options:

Figure 51: Gripper options

The most important ones are:

Priority: If several grippers want to grip the same object at the same time, the higher priority determines which

•

gripper the object adheres to.

Recenter workpiece: Recenter the object on the gripper, regardless of which part of the gripper was used to de-

•

tect the object.

Constraint DOFs: The 3 directions and 3 rotations can be released individually so that the object can still move in

•

this direction.

## Page 42

42INDUSTRIALPHYSICS TM294

Scripting

It is possible to create scripts to easily automate tasks or to perform processes in industrialPhysics as realistically

as possible.

This is done by clicking on button "Insert script" or by double-clicking on the script icon in the model structure.

Figure 52: Button "Insert script"

Figure 53: Script icon in model structure

The new script window is empty and thus a script structure must be inserted.

Figure 54: Inserting a strip structure

## Page 43

WORKING WITH INDUSTRIALPHYSICS43

The same menu option is used to insert templates for the definitions.

Local constantsConstants that can only be used and read locally.

Local variablesVariables that can only be used locally.

Input constantsConstants that can be written to via the TCP connection.

Output variablesVariables that can be transferred to the PLC via the TCP connection.

Connect constantsConstants that can access other model variables.

Connect variablesVariables that can access other model variables.

The connection to other variables can be either a relative or an absolute path specification.

Link a script variable to an existing variable of the same object (with a status variable of a ramped drive in this case):

VAR VarName := CONNECT(".?KinBack");

Link a script variable with an existing variable of an object that is on the same level in the hierarchy but below the

same parent node:

VAR VarName := CONNECT("../Sister object?KinBack");

../../ can be linked as desired in order to move on to a higher level in the hierarchy. In some cases, however, it is advisable

to use absolute path specifications:

VAR VarName := CONNECT("Node1/Node2/Components?IR");Physics://physics/

This path information is also available in window "Component properties" under "Properties":

Figure 55: Path information

## Page 44

44INDUSTRIALPHYSICS TM294

To save a script, it must be embedded:

Figure 56: Script - Embed

All keywords and data types can be found in the integrated help documentation:

Figure 57: Script - Help documentation

## Page 45

WORKING WITH INDUSTRIALPHYSICS45

Task: Gripper

An intermediate layer of cardboard is required between two layers of boxes.

The intermediate cardboard layer must be collected from a stack by a

gripper on the underside of the pallet truck. In order to simulate the

automatic feeding of the boxes, a source is created from the top layer

and triggered by a script.

Figure 58: Cardboard boxes

1)Change the property of the  to .pallet truck Translation in Z

2)Adjust the translational properties for the . (end stops, drive, acceleration and speed)pallet truck

3)Change the property of  to  and to .Suction_Pallet truckKinematicGripper (Dynamic)

4)Change the property of the top  to  and create a  with .intermediate layerStaticsourceRelease by trigger

5)Create a script that triggers the creation of an  by linking the Boolean variable of the triggerintermediate layer

with that of the gripper when the  is .gripperactivated

6)Extend your AS project by automatically adding an intermediate layer.

## Page 46

46 INDUSTRIALPHYSICS TM294
4.8 Pallet lift with virtual gripper
Extended use of scripts
In the previous example, two variables were linked to save work steps that only exist in simulation and not in reality.
Another example for using a script is when collisions of complex objects would use too much computing power and
could therefore not be executed.
It is not possible to let the teeth of the pallet lift reach into the native body of the pallet and thus hold it. This is why
a virtual gripper is placed above the pallets. The gripper is switched on and off depending on the horizontal position
of the pallet lift. Thus the collision bodies can be modeled as full cuboids.
In addition, only one fork of the pallet lift is moved. Its position is copied by the second fork in order to enable realistic
control.

## Page 47

WORKING WITH INDUSTRIALPHYSICS47

Task: Pallet lift

When a pallet has been filled with 2 layers of boxes, it is transported away and the pallet lift places a new pallet onto

the conveyor. The pallet lift required for this is made dynamic in this step.

This example shows how to use a script-based virtual gripper. The

physics engine of industrialPhysics reacts unexpectedly at times or

otherwise, collisions of native objects would require too much com-

puting power. In this case, several script-controlled workarounds

must be implemented in the simulation to avoid any changes to the

Automation Studio project. This task aims at adding a virtual grip-

per for the slightly unstable stack of pallets and avoiding the big CPU

load of native object calculations.

Figure 59: Pallet lift

1)Change the property of  to .Lifting station PZM2 Translation in X

2)Adjust the  of the . (end stops, drive, acceleration and speed)positioning drivelifting station

3)Change the property of the  to .pallet carrierTranslation in Z

4)Adjust the  for the . (end stops, drive, acceleration and speed)drive with ramppallet carrier

5)Change the property of  to .pallet carrier<2>Translatory in Z

6)Adjust the  for <2>. (end stops, drive, acceleration and speed)direct drivepallet carrier

7)Link the two pallet carriers with a .script

8)Create a new  below node  and position it.box elementLifting station PZM2

9)Change the property of the to  and .virtual gripper Kinematic Gripper (Dynamic)

10)Create a  to control the .scriptvirtual gripper

11)Change the property of  to .conveyor rollers 1Static

12)Change the property of the  to .EUR-palletsDynamic

13)Test the functionality.

14)Adjust the Automation Studio project so that a pallet is placed on the conveyor when requested.

4.9Pallet conveyors

In order to transport the full pallet away and place the empty pallet on the loading position, the conveyor belts must

be started.

## Page 48

48INDUSTRIALPHYSICS TM294

Exercise: Pallet conveyors

To make the machine complete, the full pallets must be transported

away and the new, empty pallets must be moved from the lift to the

loading point. For this, the three segments of the conveyor belt must

be made dynamic.

Figure 60: Pallet conveyors

1)Change the collision type of  to .conveyor rollers 1Moving surface

2)Adjust the conveyor. (direction and drive)

3)Change the property of  to .conveyor rollers 2 and 3Static

4)Change the collision type of  to .conveyor rollers 2 and 3Moving surface

5)Adjust the conveyors. (direction and drive)

6)Think about how you can position the empty pallets exactly at the loading position.

7)Adjust your Automation Studio project in order to run the palletizing machine continuously.

## Page 49

SUMMARY 49
5 Summary
Plant simulation and virtual commissioning are the essential aspects of industrialPhysics. Easy to import CAD data and
the connection with Automation Studio make it possible to test the application on the digital twin in an efficient way.
The connection to industrialPhysics has made it possible to extend simulation in Automation Studio by adding the
possibility of simulating plants.

## Page 50

50INDUSTRIALPHYSICS TM294

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

## Page 51

AUTOMATION ACADEMY 51

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

V2.0.0.0 ©2023/09/27 by B&R, All rights reserved.

All registered trademarks are the property of their respective owners.

Subject to technical changes without notice.