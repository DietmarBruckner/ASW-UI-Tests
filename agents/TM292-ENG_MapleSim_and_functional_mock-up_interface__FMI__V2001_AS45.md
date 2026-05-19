## Page 1

TM292

MapleSim and Functional

Mock-up Interface (FMI)

## Page 2

2 MAPLESIM AND FUNCTIONAL MOCK-UP INTERFACE (FMI) TM292
Requirements
Training modules: TM210 - Working with Automation Studio
TM213 - Automation Runtime
TM223 - Automation Studio diagnostics
Software B&R Automation Studio 4.6.x.x or higher
Scene Viewer 3.1.8.0 or higher
Maple 2019.2 or higher
MapleSim 2019.2 or higher
MapleSim - CAD toolbox
MapleSim - B&R toolboxes
ServoSoft 3.3.518 or higher

## Page 3

TABLE OF CONTENTS 3
Table of contents
1 Introduction.........................................................................................................................................................4
1.1 Learning objectives..............................................................................................................................4
2 Simulation............................................................................................................................................................6
2.1 Preparation............................................................................................................................................6
3 MapleSim basics.................................................................................................................................................7
3.1 User interface........................................................................................................................................8
3.2 Help documentation..........................................................................................................................10
4 Modeling.............................................................................................................................................................11
4.1 Mathematical pendulum....................................................................................................................11
4.2 Modeling in MapleSim.......................................................................................................................13
5 Creating the initial Functional Mock-up Unit (FMU)................................................................................20
5.1 The pendulum model........................................................................................................................20
5.2 Subsystems.........................................................................................................................................20
5.3 Tracing signals....................................................................................................................................21
5.4 Exporting the Functional Mock-up Unit from the pendulum model.......................................22
5.5 Importing the FMU into a B&R Automation Studio project......................................................23
5.6 B&R Scene Viewer..............................................................................................................................25
6 CAD import into MapleSim............................................................................................................................27
7 Creating a model using CAD data................................................................................................................30
7.1 Generating a library from the CAD data.......................................................................................30
7.2 Subsystems..........................................................................................................................................38
7.3 Recording signals in the model.......................................................................................................41
7.4 Setting user-defined input values..................................................................................................42
7.5 Verification of the FMU....................................................................................................................44
7.6 Exporting the Functional Mock-up Unit for the overhead bridge crane.................................45
7.7 Integration in a B&R Automation Studio project........................................................................46
8 SERVOsoft toolbox..........................................................................................................................................50
9 Adding an ACOPOS drive...............................................................................................................................56
10 Exercises..........................................................................................................................................................60
11 Summary...........................................................................................................................................................61

## Page 4

4MAPLESIM AND FUNCTIONAL MOCK-UP INTERFACE (FMI) TM292

1Introduction

The word simulation comes from the Latin simulatio = pretense. In today's technology, however, simulation is under-

stood as the reproduction of a real system by a computer program. The accuracy and extent with which the real sys-

tem is reproduced depends heavily on the requirements and the available computing power. The more accurate the

reproduction of the real system is, the better it reflects reality.

Simulation models are created for various reasons in a technical environment. Fig. 1 shows some intended areas of

use. One important reason is analyzing the systems. Creating a simulation will strengthen your understanding of the

system, process or machine. The gained knowledge can be used to better control the system. From an economic point

of view, a better understanding of the system leads to an increase in efficiency. Another reason for using a simula-

tion is the ability to experiment. Destructive and non-destructive testing can both be carried out in the simulation

without high costs. An important aspect here is, for example, checking the drive sizing. Drives can also be sized using

the simulation model. One area that has become increasingly important in recent years is software testing. Machine

and process software can be tested against a digital twin (simulation model). Early detection of software errors can

significantly reduce commissioning times on site. The digital twin can also be used for predictive maintenance. The

simulation runs parallel with the real system in real time; if the deviation is too large, this indicates a malfunction.

Using a digital twin, such malfunctions can be detected at an early stage. Further reasons for creating a simulation

model include: Presentations, operator training, usability tests, ergonomic questions, etc. .

Figure 1: Use of simulation models

Further information and explanations related to the basics and virtualization of control technology can be found in

"".TM291 - Basics of virtualization and simulation for industrial control technology

TM291 - Basics of simulation for industrial control technology

1.1Learning objectives

This training module uses selected examples illustrating typical application tasks to help participants learn how vari-

ous functions in B&R Automation Studio are represented and configured.

## Page 5

INTRODUCTION 5
Participants will learn which simulation tasks MapleSim is best suited for.
•
Participants will learn how to use the MapleSim program.
•
Participants will become familiar with the following MapleSim libraries: Multibody, 1D Mechanical and Signal
•
Blocks.
Participants will become familiar with the following MapleSim apps: B&R Automation Studio FMU Generation,
•
B&R SERVOsoft Data Generation and 1-D Motion Generation.
Participants will learn how to create models of mechanical systems without deriving differential equations.
•
Participants will learn how to export a MapleSim model as a Functional Mock-Up (FMU) and then import it into
•
B&R Automation Studio.
Participants will learn how to use the B&R Scene Viewer to visualize the MapleSim model in B&R Automation Stu-
•
dio.
An advantage of pre-simulation of physical systems is the possibility to size the drives. If the system is known and
is simulated with the maximum degree of dynamics, the calculated forces and torques can be used to determine the
minimum sizes of the drives. The B&R SERVOsoft Data Generation app in MapleSim in combination with the SERVOsoft
software is a great help here.

## Page 6

6MAPLESIM AND FUNCTIONAL MOCK-UP INTERFACE (FMI) TM292

2Simulation

The typical B&R simulation levels, which differ in their level of detail, are shown in Fig. 2. A distinction is made between

the automation hardware level, the component and machine level and the process and plant level. "TM291 - Basics of

" provides a good overview of the different levels.simulation for industrial control technology

TM291 - Basics of simulation for industrial control technology

MapleSim is best suited for machine and component simulation. With MapleSim, physical quantities such as positions,

velocities and forces can be very accurately calculated. For this reason, MapleSim is also well suited for dimensioning.

In general, hardware components can also be modeled with MapleSim, but it makes sense to use the already existing

simulation models in B&R Automation Studio.

Other tools are also better suited for plant and process simulation. One reason is the handling, another the required

processing power.

Figure 2: Simulation levels

2.1Preparation

Choosing the right simulation tool is by no means easy. At the beginning, you should define where you want to use the

simulation. A few areas of application are shown in Fig. 1.

MapleSim is well suited for the following applications:

Process optimization in a safe environment: Sizing drives, developing control algorithms

•

Rapid prototyping: Quickly creating different system variants e.g. different mechanical geometries

•

Determination of system load limits: Determining the maximum overload time

•

Predictive maintenance: Operating a digital twin that runs parallel to a real system -> early error/wear detection.

•

Simplification of interdisciplinary communication: Using visualization, it quickly becomes clear whether the sys-

•

tem was modeled as originally intended.

## Page 7

MAPLESIM BASICS7

3MapleSim basics

Compared to other simulation tools, using MapleSim together with Modelica is a very efficient modeling method (see

Fig. 3) . Unlike the conventional method, a MapleSim model does not require differential equations. This saves time

and reduces the probability of errors. There is more time to build different system variants and test them against each

other, for example.

Figure 3: Maple workflow

Systems theory and modeling

Initial value problems

The solution of an initial value problem is the solution of the differential equation with additional consideration of a

specified initial value.

https://en.wikipedia.org/wiki/Initial_value_problem

In mathematics, physical models can very often be mapped with the help of standard differential equation systems. An

example in mechanics is a spring-mass oscillator or in electrical engineering an RLC circuit. Usually, these differential

equation systems contain derivatives of second order state variables (which can be converted into a first order state

equation system). This system can become very complex, however, which means that its solution is no longer easy

to calculate analytically. Numerical solution algorithms are then used (one-step or multi-step methods), of which the

best-known examples are the Euler method or the Runge-Kutta method.

FMI (Functional Mock-up Interface) / FMU (Functional Mock-up Unit)

The  (FMI) defines a standardized interface that can be used to couple different simula-Functional Mock-up Interface

tion software. A physical system (e.g. an industrial plant) can consist of several components that can come from dif-

ferent manufacturers. This is where the FMI comes into play. The idea is to enable a variety of components to interact

with each other in a complex way by merging and displaying them in a simulation.

Experience has shown that the implementation of FMI via a software modeling tool makes it possible to create simu-

lation models that can be coupled with each other.

An example model based on a vehicle could look like this: Software A handles the engine, Software B handles the gear-

box, Software C handles the controls, Software D handles ... With FMI, these various software models can easily be

coupled and combined to a complete model.

## Page 8

8 MAPLESIM AND FUNCTIONAL MOCK-UP INTERFACE (FMI) TM292
Another possibility is to create a software library called FMU (Functional Mock-up Unit). The FMI specifications are
provided as open source licenses. Each FMU model is made available as a ZIP file with extension ".fmu" and includes
the following components:
- an XML file describing, among other things, the definition of the variables used by the FMU;
- all formulas used in a model (defined as C functions);
- optional additional data such as parameter tables, the user interface, documentation required by the model, etc.
https://en.wikipedia.org/wiki/Functional_Mock-up_Interface#Architecture
Modelica
Modelica is an object-oriented modeling language for physical models. There are different graphical development en-
vironments for this language (e.g. MapleSim or Dymola). They allow the user to develop complex simulation models
using graphical symbols, each representing a physical object. Objects are connected using connectors, which are also
usually non-directional, i.e. bidirectional. An example of this is a motor shaft. With the motor shaft, the motor can set
a gearbox in motion. The load of the gearbox also affects the motor via the motor shaft, however, and the motor shaft
thus has 2 directions of action, one from the motor to the gearbox and vice versa.
Modelica is suitable for describing interdisciplinary problems in a wide range of fields such as mechanics, electrical
engineering and electronics, thermodynamics, hydraulics and pneumatics, closed-loop control and process control.
The language definition and the Modelica standard library are freely available and are further developed and promoted
by the Modelica Association.
A major advantage of Modelica is that it works with equations instead of assignments. Variables being searched for
do not need to be resolved. Another advantage is that variables can have properties assigned (physical size, unit). This
allows verification of equations by the simulation software.
https://en.wikipedia.org/wiki/Modelica
Computing time
The cycle time of the numerical solver in MapleSim must match the task class cycle time of B&R Automation Studio to
achieve correct simulation. Setting a lower time results in a more accurate calculation and simulation of the physical
model. But this also means that computing time can increase considerably. It is necessary here to choose a time that
reproduces the simulation with sufficient accuracy while keeping the computing time as low as possible.
3.1 User interface
The following figure provides an overview of the basic structure of MapleSim.

## Page 9

MAPLESIM BASICS9

Figure 4: MapleSim user interface

ComponentDescription

Includes the tools to run a simulation, visualize its results, search MapleSim forMain toolbar

help and perform other simple tasks.

Includes tools to hierarchically browse the model and its subsystems, changeModel workspace toolbar

the model view, view the associated MODELICA code, group components and

add measurement sensors.

Includes tools to add annotations and arrange objects.Annotations toolbar

Allows the model to be built and edited in block diagram view.Model workspace

Includes expandable menus that can be used to build the model and managePalettes pane

the MapleSim project. 5 tabs can be used:

- : Ready-made models and domain-specific componentsLibrary components

can be added to the model.

- : Entries from the subsystem and self-made componentsLocal components

can be inserted.

- : Here you can navigate through the model with the help of a struc-Model tree

ture tree.

- : Attachments for the model can be seen here: Documents, para-Attached files

meter sets and CAD drawings.

- : Contains entries with ready-made tools to build or an-Add apps or templates

alyze the model.

Buttons on the  can be used for visualization to display the fol-ConsoleConsole toolbar

lowing:

- : Displays all associated messages that reflect the status ofConsole output

the MapleSim engine during a simulation. There is also the possibility to delete

the messages.

- : Shows all diagnostic messages for debugging whileDiagnostics information

building the model. The subsystem where an error occurs can be identified.

The message type can be selected.Console toolbar

## Page 10

10MAPLESIM AND FUNCTIONAL MOCK-UP INTERFACE (FMI) TM292

ComponentDescription

Contains the following entries:Parameters pane

- : Properties of model components such as names, property values,Properties

initial conditions and measurement sensor properties can be viewed and modi-

fied.

- : Simulation options such as the duration of the simulationSimulation settings

or optional parameters for the solver and the simulation engine.

- : Here you can specify options for visualizing the multibodyMultibody settings

components.

The content of this tab changes depending on the choice in the Model work-

space.

The simulation is started when clicking on  . If the simulation was successful, the  iconPlayShow simulation results

flashes. By clicking on this symbol, the resulting visualization of the simulation can be viewed. The configured plots

are also available in this window.

Figure 5: Simulation results and plots for a double pendulum model as an illustrated example.

3.2Help documentation

All functions offered by MapleSim are described in the . To get help for a specific ele-MapleSim help documentation

ment, press the F2 key.

Help documentation is also available online at .www.maplesoft.com

At the beginning, it is also worthwhile to view youtube video  .Getting Started with MapleSim

Quick help related to various objects in MapleSim can be called up by pressing the F2 key.

## Page 11

MODELING 11
4 Modeling
To see how MapleSim helps to simplify physical models and to see how the necessary mathematics disappears into
the background, a simple model of a mathematical pendulum will be created. First, the mathematical model is derived
by hand and then MapleSim is used.
It becomes clear how much MapleSim takes over the steps necessary to perform a complete simulation. For more
complex systems, the effort required to manually derive the equations quickly becomes very large. This can still be
kept to a minimum using the Modelica approach.
4.1 Mathematical pendulum
g
l
α
m
Figure 6: Mathematical pendulum, sketch
Deriving the motion equation with Newton
The following derivation is made using Newton's 2nd law:
Mass m can only move along the arc (t = tangential). F describes the force and a the acceleration. The motion equation
for the mathematical pendulum is written as follows:
It is important that tangential acceleration a acts in the same direction as force F (see Fig. 7 and Fig. 8). If the direc-
t t
tions are different, this must be taken into account by the sign.
Fig. 7 shows that a t can be expressed using angle α. α is measured in the opposite direction as a t , thus the negative sign.

## Page 12

12MAPLESIM AND FUNCTIONAL MOCK-UP INTERFACE (FMI) TM292

g

l

α

ma

t

Figure 7: Mathematical pendulum: Position, velocity and acceleration

The tangential force F can also be described using weight F=m·g and angle .α

tG

g

l

α

mF

t

F

rα

F

F = Fsin(α)⋅Frα

tGG

F

t

F

G

Figure 8: Mathematical pendulum: Forces

When used in equation F = m·a, this results in the following (with l as pendulum length and g as acceleration due to

tt

gravity):

Converting the equation results in the differential equation for the mathematical pendulum:

Deriving the motion equation with Lagrange

Lagrange mechanics can also be used for derivation as an alternative to Newton. In Lagrange mechanics, the gener-

alized coordinates must first be determined. This is a minimal set of independent coordinates to unambiguously de-

scribe the spatial state of the system.

https://en.wikipedia.org/wiki/Generalized_coordinates

Lagrange mechanics is explained in the form of an example in the following. First, the Lagrange function L - with the

help of the kinetic energy E and the potential energy E - is determined as follows:

kinpot

## Page 13

MODELING 13
Then this function is derived according to the generalized coordinates q and time (Lagrange equation):
For the mathematical pendulum, α is defined as a generalized coordinate. This choice is generally not easy and there
are systems where it is not possible to make such a choice. In any case, the system must be holonomic.
https://en.wikipedia.org/wiki/Lagrangian_mechanics
The Lagrange function is then set up:
The function is derived to obtain the Lagrange equation:
Simplification of Lagrange equation results in the motion equation (identical with Newton):
4.2 Modeling in MapleSim
4.2.1 Multibody library
The Multibody library is introduced in the following section. This library provides blocks for modeling 3-D mechanics.
Bodies, masses and joints, among other things, are made available.

## Page 14

14MAPLESIM AND FUNCTIONAL MOCK-UP INTERFACE (FMI) TM292

Figure 9: Multibody library

4.2.2Mathematical pendulum

Creating the model with MapleSim

In MapleSim, the following elements from the  will be used:Multibody library

A  to fix the pendulum in space: fixed frame

A  (hinge joint) to allow rotational freedom: revolute,

A  instead of the rope: . A simplification is already being made here. The rope of the pendulumrigid body frame

is assumed to be always tensioned. The rope can therefore be replaced by a rigid body. In manual derivation, this

simplification is implicitly made by the choice of the coordinate system.

A :  This represents a mass within a system. In the properties for this object, you can also configurerigid body

a rotational inertia.

The individual elements can be dragged and dropped in the  . These can then be connected to each otherworkspace

using flanges (i.e. connection points). The finished model looks like this in the :workspace

## Page 15

MODELING15

Figure 10: Mathematical pendulum in MapleSim

With the help of the shortcut menu (right mouse button),elements can be rotated and flipped in the

.workspace

Figure 11: Rotating and flipping elements

Now the simulation can be started  and the result reviewed .

The model is displayed in the  window as follows:3D playback

## Page 16

16MAPLESIM AND FUNCTIONAL MOCK-UP INTERFACE (FMI) TM292

Figure 12: MapleSim 3D playback window

It can be seen that the pendulum starts with a deflection of 90°. This is based on default values of the inserted library

elements. In addition, the pendulum swings in the x-y plane when the coordinate system is viewed from the bottom left.

The F2 key can be used to open the help documentation for each element.

What is necessary to make the pendulum swing in the y-z plane should be indicated in the form of an example. First,

the properties of the hinge joint are observed:

Figure 13: Hinge joint parameters

ê indicates the rotation axis of the joint. In this case [0,0,1] on the z axis. If the pendulum should swing in the y-z plane,

1

the joint must be able to rotate around the x-axis. Therefore ê=[1,0,0] is set. The other parameters will not be observed

1

for the time being because they are not required here.

In addition, the parameters for the rigid body must be observed:

## Page 17

MODELING17

Figure 14: Rigid body parameters

indicates the direction in which the rigid body is extended. [1,0,0] m means the rod is extended 1 m in the x-direction.

This is changed to  = [0,0,1]. When the simulation is started again, the pendulum now swings in the y-z plane.

In general, all positions and expansions of an element are described in the coordinate system for flange

a.. A detailed explanation can be found in the MapleSim help documentation (F2).

*[Image OCR]
```
3 - D Playback Window

    x - y plane
    y - z plane
```
[End OCR]*

Figure 15: Mathematical pendulum simulation

You can also use  (under ) to output the system's motion equations .Equation extractionAdd apps or templates

Figure 16: Automatically generated motion equation

Further simplification results an identical motion equation, which was already derived manually above.

## Page 18

18MAPLESIM AND FUNCTIONAL MOCK-UP INTERFACE (FMI) TM292

Figure 17: Equation extraction from MapleSim.

Modeling a mathematical pendulum

Create a simulation model of a mathematical pendulum

Use the following values:

m = 1 kg

l = 1 m

g

g = 9.81 m/s²

l

α

m

1)Familiarize yourself with the .Multibody library

2)Build the model and start the simulation.

3)Convert your model so that the pendulum swings in a different direction. As depicted in Fig. 15.

4)Adapt your model so that the pendulum can swing in all three spatial directions.

Tip: Use a spherical joint.

5)Set the initial conditions for the point mass so that the pendulum performs true 3D movement.

Tip: Change the initial conditions in the parameters for the mass. Make sure that  matches the dimensions of

the rod and introduce an initial speed to the system using . Set the initial conditions IC to .Strictly enforce

r,v

## Page 19

MODELING19

6)Record the path of the mass. Tip: Use the  element in the  area of the .Path TracevisualizationMultibody library

7)Dampen the oscillation of the pendulum. Tip: Use a bushing.

Modeling results

-->

## Page 20

20MAPLESIM AND FUNCTIONAL MOCK-UP INTERFACE (FMI) TM292

5Creating the initial Functional Mock-

up Unit (FMU)

To be able to use a model from MapleSim in B&R Automation Studio, a Functional Mock-up Unit (FMU) must be created

from it. An FMU should be created from the pendulum model. The following steps are necessary for this:

1) Create a subsystem from an existing model.

2) Install and define the mechanical drives.

3) Specify a solver (e.g., sampling rate 400 µs).fixed step solver

4) Export the model as an FMU with .RK4 solver

Tools used

• B&R Automation Studio FMU generation

5.1The pendulum model

The existing pendulum model (see Fig. 10) should be converted into an FMU.

5.2Subsystems

As the first step, a subsystem must be formed from the model (see Fig. 18), which is later converted into an FMU. Right-

click with the mouse to select and convert all necessary elements into a subsystem.

Figure 18: Pendulum model as a subsystem

In the subsystem, inputs and outputs must then be defined that will later serve as an interface in B&R Automation

Studio . It should be noted here that these must be unidirectional, which is indicated by the arrows (see Fig. 19). To

achieve this, additional elements from the 1D mechanical library are required. On the input side, element  fromTorque

the  library and on the output side,  from the  library.Torque DriversAngle SensorSensor

## Page 21

CREATING THE INITIAL FUNCTIONAL MOCK-UP UNIT (FMU)21

Figure 19: Pendulum subsystem with inputs and outputs

This subsystem can then be integrated and tested in a MapleSim simulation (see Fig. 20).

Figure 20: Subsystem pendulum integrated in a MapleSim simulation

Building a subsystem

Convert the existing pendulum model into a subsystem.

5.3Tracing signals

With the aid of measurement sensors (), signals from the model can be visualized allowing the model itself toprobes

be verified. The measurement sensors are shown in the . The pendulum results are shownModel Workspace toolbar

in Fig. 21 in the form of an example.

## Page 22

22MAPLESIM AND FUNCTIONAL MOCK-UP INTERFACE (FMI) TM292

Figure 21: The results of the simple pendulum with measurement sensor.

Tracing signals

Experiment with the model and its physical quantities. Output various measured values, such as position, velocity or

forces. Try to interpret these values and explain how you can tell that the system is modeled correctly.

5.4Exporting the Functional Mock-up Unit from the pendulum model

Open , see Fig. 22. Then the model must be exported, see Fig. 23.FMU export

Figure 22: Open B&R Automation Studio FMU generation.

It is necessary to ensure that the step size is equal to the cycle time of the PLC. (see Fig. 23). If the step

size deviates from the cycle time, correct behavior of the model in the simulation is no longer guaranteed.

Names for archives and models with more than 7 characters should be avoided.

If the program name has less than 10 characters, a connection to B&R Scene Viewer is established auto-

matically.

If the function block has less than 20 characters, the connection to B&R Scene Viewer is established au-

tomatically.

When using more characters, the names in B&R Scene Viewer must be adjusted manually.

## Page 23

CREATING THE INITIAL FUNCTIONAL MOCK-UP UNIT (FMU)23

Figure 23: Parameters for FMU export of the pendulum.

After loading the subsystem, the following parameters in the  are important for creating an FMU (seeExport options

Fig. 23):

- : RK4 is recommended. The task class cycle time in which the task is called on the PLC must be entered in theSolver

field "Task class cycle time".

- : The geometries of the model should also be exported.Visualization options

The solvers: CK45 and Rosenbrock are variable step solvers. If this type of solver is used, the function

block can determine its internal cycle time itself independent of the task class cycle time on the controller.

Advantage: The model is more stable. Very short cycle times (less than 100 µs) are also possible.

Disadvantage: Unforeseen cycle time violations can occur because the computing time is variable. A cycle

time violation leads to incorrect results.

5.5Importing the FMU into a B&R Automation Studio project

First, the FMU library has to be loaded from the toolbox catalog (see Fig. 24).

## Page 24

24MAPLESIM AND FUNCTIONAL MOCK-UP INTERFACE (FMI) TM292

Figure 24: Adding an FMU in B&R Automation Studio.

The following wizard (see Fig. 25) can now be used to search for the exported FMU file. The remaining fields will then

be filled in automatically.

Figure 25: FMU library import wizard.

When the FMU has been imported into B&R Automation Studio, the task must be assigned to the corresponding Cyclic#

(see right side Fig. 26). In addition, the time of the  must be set according to the "Task class cycle time" that isCyclic#

set when exporting the FMU (in this case, 400 µs). It may be necessary to adjust the System Tick.

## Page 25

CREATING THE INITIAL FUNCTIONAL MOCK-UP UNIT (FMU)25

Figure 26: B&R Automation Studio GUI.

It is now possible to open B&R Scene Viewer (see Fig. 27) by double-clicking on  (left side Fig. 26).Pendel.scn

5.6B&R Scene Viewer

Figure 27: B&R Scene Viewer for the simple pendulum.

Via the PVI menu (see Fig. 28), it is possible to check if the correct settings have been made for a connection to B&R

Automation Studio (see Fig. 29) and if the PVI connector is then started (see Fig. 28).

## Page 26

26MAPLESIM AND FUNCTIONAL MOCK-UP INTERFACE (FMI) TM292

Figure 28: PVI tab in B&R Scene Viewer.

Figure 29: The settings for PVI.

As a final step, the simulation can be started via the Watch window in B&R Automation Studio by setting the Enable

values of the visualization function block and the FMU function block to TRUE (see middle Fig. 26).

A pendulum movement is visible in B&R Scene Viewer.

In B&R Scene Viewer, you can use:

- The left mouse button to select an object,

- The middle mouse button + Ctrl to drag the camera and

- The middle mouse button + Shift + Ctrl to rotate the camera (around the selected object or virtual point).

## Page 27

CAD IMPORT INTO MAPLESIM27

6CAD import into MapleSim

Importing STEP files

To import CAD files, the CAD toolbox must be opened. To do this, select menu option  ->  (see Fig. 30).FileImport CAD...

Figure 30:  in the  menu.Import CADFile

This opens the CAD toolbox (see Fig. 31 ).

## Page 28

28MAPLESIM AND FUNCTIONAL MOCK-UP INTERFACE (FMI) TM292

Figure 31: The .CAD toolbox

Important elements of the toolbox are shown in Fig. 32.

## Page 29

CAD IMPORT INTO MAPLESIM29

Figure 32: The toolbar of the .Import CAD toolbox

## Page 30

30MAPLESIM AND FUNCTIONAL MOCK-UP INTERFACE (FMI) TM292

7Creating a model using CAD data

In this example, an overhead bridge crane is set up using MapleSim. The procedure is divided into the following parts:

1)Create model for the overhead bridge crane.

2)Animate the overhead bridge crane.

3)Export FMU from the model.

4)Integrate the overhead bridge crane model in B&R Automation Studio.

7.1Generating a library from the CAD data

7.1.1Importing CAD data

The overhead bridge crane model is made available as a .step file (see Fig. 33 ).

Figure 33: The .step model for the overhead bridge crane.

Defining subgroups

Subgroups of the base and the bridge are then formed (see Fig. 34).

Each of these subgroups later becomes an object or function block in the visual editor. Individual objects at the highest

level are also converted to a function block. In Fig. 34, this is the case for the trolley.

Figure 34: The subgroups of the overhead bridge crane model.

## Page 31

CREATING A MODEL USING CAD DATA31

7.1.2Determining the basic orientation

Creating and assigning coordinate systems

After the CAD data has been imported, each of the subgroups needs its own coordinate system (flange) in order to

connect the objects later in the workspace. At the position of the coordinate system there is a flange in the workspace

that can be used to connect the objects. To add a coordinate system (flange), click on the corresponding icon according

to Fig. 32 and select a point as shown in Fig. 35. Correctly creating the coordinate systems is important for the proper

functionality of the entire simulation.

The coordinate systems, as described in Fig. 36, Fig. 38 and Fig. 39 also have to be created.

Figure 35: The coordinate system for the base.

Fig. 36 Shows the coordinate system for modeling the connection between bridge and base. In MapleSim Workspace,

for example, a prismatic joint can be inserted along the z-axis after import, which can cause the bridge to move along

the z-axis relative to the base. In Fig. 36, coordinate system A was added for the bridge. A reference to coordinate

system A was added for the base (copy/paste). As a result, the base gets a coordinate system (reference to A) that

has the same global coordinates as A, which is assigned to the bridge.

Figure 36: The coordinate systems between base and bridge.

The coordinate system and its reference can be connected in the workspace using a prismatic joint, for example (only

allows translation). In Fig. 37, a prismatic joint that allows movement in the z-direction is used ([0,0,1]).

## Page 32

32MAPLESIM AND FUNCTIONAL MOCK-UP INTERFACE (FMI) TM292

Figure 37: Connection of base and bridge in workspace.

In Fig. 38 (bridge - trolley) and Fig. 39 (trolley - rope), other required coordinate systems are shown.

Figure 38: The coordinate systems between bridge and trolley.

Figure 39: The coordinate systems between trolley and rope.

The  icon      must be pressed to add the parts. After that the parts are in the  menu (seeImportLocal Components

Fig. 40). They can now be used in the workspace.

## Page 33

CREATING A MODEL USING CAD DATA33

Figure 40: The Local components toolbox.

When importing CAD data, the material properties (right click) should be checked to ensure that they are

available and correct. This has a significant effect on the physical behavior of the system.

The coordinate systems and the material properties can also be changed afterwards by double-clicking

on the object.

Exercise: Import CAD data

Import the .step file for the overhead crane. Proceed as described in the previous chapter.

The following steps are now performed one after the other:

Import the .step file.

•

Define the . These aregroups

•

- Base,

- Bridge

Define and assign the coordinate systems.

•

Apply the settings.

•

7.1.3Identify and define the elements / joints

Add all necessary elements into the model

To be able to add all necessary elements, the imported model must first be checked and then the base must be con-

nected to the . A , as described in Fig. 41 is used (other elements, which are important for the ex-groundfixed frame

ample, are also highlighted in the image).

## Page 34

34MAPLESIM AND FUNCTIONAL MOCK-UP INTERFACE (FMI) TM292

Figure 41: Elements in the Multibody toolbox.

Now that the CAD data has been imported, the individual parts (base, bridge and trolley) must be joined together with

joints. Important types of joints for this example are  and , see Fig. 41.  corresponds to ansphericalprismaticSpherical

element rotating around an axis, whereas  corresponds to a sliding element.prismatic

Fig. 42 shows an excerpt from the MapleSim help documentation related to a .prismatic joint

The cube (2) can only move along one axis (here the x-axis). Two coordinate systems are needed to generally describe

the relative motion between the bearing (1) and the cube (2). One coordinate system for the bearing (1) and one coor-

dinate system for the cube (2).

With the help of the coordinate systems, it is possible to carry out a rotational or translational movement of an object

in all directions. If the trolley is connected to the bridge in this way (with the help of a ), the bridge canprismatic joint

be moved on the base along the x-axis.

Two coordinate systems are always required for a relative movement between two objects. This is

achieved simply by forming a coordinate system for the first body, which is then copied (or referenced)

for the second body and adapted accordingly.

Figure 42: The prismatic joint.

## Page 35

CREATING A MODEL USING CAD DATA35

Exercise: Creating an initial model with CAD elements

Use a , a  and the base to create a model (see Fig. 43). If you now simulate/run the model,fixed framespherical joint

what results can you see in the window?3D playback

Figure 43: An initial model with a self-made subgroup.

Check the tooltip (hover over the connector with the mouse) on the base to determine if you are con-

necting the correct input.

To test the model, the play icon    can be pressed in the Main toolbar. To switch to the 3D playback

, the corresponding icon can be clicked  . The movement of the model can be examined andwindow

verified here ( window - see e.g. Fig. 46).3D playback

Coupling the elements

Now the complete model for the overhead bridge crane will be created. For reasons of simplicity, the model is built in

an initial step with a fixed rope length. The rope is modeled using a rod, which is an approximation of reality.

To link the elements, the connections are set up as described in Fig. 44. The properties must be set in accordance

with Fig. 45.

Figure 44: Model of the complete overhead bridge crane in the MapleSim workspace.

## Page 36

36MAPLESIM AND FUNCTIONAL MOCK-UP INTERFACE (FMI) TM292

Figure 45: Values of the elements in the model.

Exercise: Creating the complete model

Create the complete model of the overhead bridge crane as described in Fig. 44 and test it.

The function blocks created from the CAD data are added to a new model in combination with elements from the

•

library.Multibody

These elements are then coupled together.

•

The parameters of the  elements for  and  must be adaptedvisualizationcylindrical geometryspherical geometry

•

to obtain a realistic visualization of rope and mass.

## Page 37

CREATING A MODEL USING CAD DATA37

Figure 46: Model of the overhead bridge crane visualized in .3D playback

7.1.4Installation of 1D elements

In order to animate the model, elements from the  library are required.1D mechanical

When animating the model with the aid of trajectories, the following steps must be carried out:

The drive/trajectory of the trolley must be defined.

•

A gearbox for the bridge must be installed.

•

The drive/trajectory for the bridge must be defined.

•

Measurement sensors must be set.

•

The following software modules are used in this chapter:

1D Mechanical library

•

1D Motion Generation

•

B&R SERVOsoft data generation

•

Connection to the 1D mechanical domain

Some joints offer a connection to the  library. For example, the previously used   and1D mechanicalprismatic joint

. These joints have connections (framed in red in the small pictures) that can be connected to arevolute joint

drive, a sensor or other mechanical systems. These connections are bidirectional.

Adding a gearbox to the bridge

At this point, a gearbox is added to the model. This allows the translatory bridge to be connected to a rotary drive. In

the following images, the required components are shown.

The source used is a sinusoidal signal whose output must be converted into a translational position unit in order to

be connected to the flange of the . In order to use a rotary unit for a drive (as shown in the example forprismatic joint

the trolley), the corresponding rotary  must be used.motion driver

The desired value for the gear ratio is set for the gearbox (see Fig. 47).

## Page 38

38MAPLESIM AND FUNCTIONAL MOCK-UP INTERFACE (FMI) TM292

Figure 47: MapleSim elements for modeling, as well as the gear ratio parameters.

Exercise: Installing 1D elements in the model

Add the corresponding 1D elements to the model.

Use a rotary drive for the bridge element.  Tip: Use a gearbox.

•

Use a translational drive for the trolley.

•

7.2Subsystems

Subsystem of the overhead bridge crane

The subsystem of the overhead bridge crane is created as follows (see Fig. 48).

## Page 39

CREATING A MODEL USING CAD DATA39

Figure 48: Create a subsystem using the menu accessed with the right mouse button.

After creating the subsystem, the bidirectional inputs must be replaced with unidirectional ones because the interface

of the FMU only supports  (double) types.LREAL

Use of a motion driver based on speed

The  are replaced by  (Fig. 49 and Fig. 50). This makes it easy to specify a movement inposition drivesspeed drives

the Watch Window later in Automation Studio, which simplifies testing. If you use a position drive and then later set

a large position jump in the Watch Window, for example, the simulation would crash because position jumps - even

in reality - are not possible.

Figure 49: Drives for the mechanics - Domains.

In MapleSim, unidirectional inputs of subsystems are marked with arrows. Functions and function blocks in B&R Au-

tomation Studio can only process unidirectional inputs. As mentioned, only  variables are permitted when im-LREAL

porting FMUs into B&R Automation Studio.

## Page 40

40MAPLESIM AND FUNCTIONAL MOCK-UP INTERFACE (FMI) TM292

Figure 50: Subsystem for the overhead bridge crane.

Using the fixed step solver

The solver of the model must be set to a  (see Fig. 51). The step size must correspond to the cyclefixed step solver

time in B&R Automation Studio.

Figure 51: The FMU model should run with a  (400 µs).fixed step solver

The MapleSim help documentation offers support for simulation settings. The following table shows a description of

different solvers in MapleSim.

## Page 41

CREATING A MODEL USING CAD DATA 41
Solver type Variable The type of solver used for the simulation.
- Variable: A variable step size is selected to keep the error tolerance low.
- Fixed: Selecting a fixed step size does not take the error tolerance into ac-
count.
Important: Solvers with a fixed step size are identical to those used by
MapleSim in the exported code.
Solver Variable: CK45 These are DAE solvers that are used during simulation. The following options
(non-rigid) are available for a variable step size:
- CK45 (semi-rigid):
Fixed: Euler - RK45 (non-rigid):
- Rosenbrock (rigid):
If the model is complex, a rigid DAE solver can be used to minimize the time nec-
essary to simulate.
The following options are available for solvers with a fixed step size:
- Euler: Forward Euler solver
- Implicit Euler: An implicit Euler solver, also suitable for rigid systems
- RK2: Second-order Runge-Kutta solver
- RK3: Third-order Runge-Kutta solver
- RK4: Fourth-order Runge-Kutta solver
Different solver options in MapleSim.
Exercise: Step size and instability
Check if the model is running stable with the selected step size. In addition, the system should be tested to determine
the step size at which the model becomes unstable. Why does the system become unstable?
Embed the overhead bridge crane model in a subsystem.
•
Swap out the motion driver elements related to position for those related to speed.
•
Select a fixed step size with an RK4 solver and test the system.
•
Try out how high the step size can be set without making the system unstable.
•
Explain why the system can become unstable.
•
7.3 Recording signals in the model
Torque output of the bridge controller

## Page 42

42MAPLESIM AND FUNCTIONAL MOCK-UP INTERFACE (FMI) TM292

Figure 52: Add a torque and speed sensor to the model.

The torque must be output via a connection between the sensor and the outer boundary of the subsystem (see Fig.

52 ). The measurement sensor must be coupled to the model with an analog connection Fig. 53.

Figure 53: The crane model, ready for FMU export.

7.4Setting user-defined input values

It is possible to generate user-defined signals in MapleSim that can be used as motion profiles. This can be done

under by selecting the  area and then . The motionAdd Apps or Templates Component Creation1D Motion Generation

profiles created in this way can then be selected as blocks under under . To illustrateLocal Components Components

this, the first step is to generate a motion profile for the trolley.

Set up the trolley drive

In the 1D Motion Generator, the signal path can be created. First, the various segments of the signal are created. As

signal type we will use Signal. In Fig. 54, the required areas are marked in red.

## Page 43

CREATING A MODEL USING CAD DATA43

Figure 54: The motion profile for the trolley.

After the motion profile for the trolley has been defined, it must be added as a block and connected to the translational

joint between the bridge and the trolley. The block now replaces the Sine block. Fig. 55 shows this connection in the

workspace.

Connecting multi-dimensional signals

If Signal is selected as the signal type when creating a motion profile with the 1D Motion Generation app, a 3D signal

is output at the block output. The signal consists of position, speed and acceleration. In our example we only need the

speed, so we select the 2nd signal (Fig. 55)

Figure 55: Connecting the speed from the 3 dimensional output of the motion profile

Set up the drive for the bridge

The motion profile for the bridge drive is now defined, as shown in Fig. 56.

## Page 44

44MAPLESIM AND FUNCTIONAL MOCK-UP INTERFACE (FMI) TM292

Figure 56: The motion profile for the bridge motor.

Limits to motor speed can be calculated:

A motor rated at 3000 revolutions per minute can therefore be selected. Converting between the units is done as

follows:

Exercise: Motion profiles

Create corresponding motion profiles using the  and connect it to the model for the overhead1D Motion Generator

bridge crane.

1)Create the motion profile for the trolley and the bridge using the .1D Motion Generator

2)Connect these trajectories to the subsystem and test it.

7.5Verification of the FMU

Adding measurement sensors

Traces can be recorded in MapleSim using measurement probes that have been added to the model. This allows, among

other things, position, speed, acceleration and force to be measured. The results can be checked after running the

simulation based on Fig. 57 and Fig. 58.

## Page 45

CREATING A MODEL USING CAD DATA45

Figure 57: The complete model of the overhead bridge crane.

Figure 58: Results of the complete overhead bridge crane.

Exercise: Testing the model - Installing the measurement probes.

Add the measurement sensor () for the overhead bridge crane model. Decide for yourself which parametersprobe

•

need to be included to verify the model you have created.

Start the simulation and record the measured values.

•

Try using the measured values to determine if the model is correct and why.

•

7.6Exporting the Functional Mock-up Unit for the overhead bridge crane

The next step is opening the FMU export to be able to export the model (see Fig. 59). This can be found in the tab Add

.apps or templates / B&R connector

The corresponding parameters, which are important for proper export, can be found here: Fig. 59.

## Page 46

46MAPLESIM AND FUNCTIONAL MOCK-UP INTERFACE (FMI) TM292

Figure 59: Parameters for FMU export of the overhead bridge crane.

Exercise: Exporting the model with the B&R Automation Studio FMU Generator

Open the FMU generator.

•

Set the parameters for proper export.

•

Export the FMU.

•

7.7Integration in a B&R Automation Studio project

Visualization with B&R Scene Viewer

One of the first steps in integrating the FMU is to open B&R Automation Studio and create a new project. The FMU can

then be installed in  ->  ->  (see Fig. 60).Toolbox - Object catalogLibraryFMU library

## Page 47

CREATING A MODEL USING CAD DATA47

Figure 60: Importing the overhead bridge crane FMU into B&R Automation Studio.

The program must be assigned to the corresponding  with the correct sampling time. It is important that theCyclic#

sampling time matches the cycle time of the FMU from MapleSim (in this case, this is a  with 400 µsfixed step solver

sampling time (see Fig. 61)).

It is important to note that the higher the complexity of the system and the lower the sampling time,

the more computing power the computer/PLC must provide. It can therefore often be useful to select

a long sampling time to keep the computing power low. The sampling time cannot be excessively long,

however; otherwise, solving equations with the solver will become unstable. A reasonable compromise

must be found here.

Figure 61: Assigning the tasks in the Cyclic#.

B&R Scene Viewer can now be opened by double-clicking on the corresponding icon (see Fig. 62).

## Page 48

48MAPLESIM AND FUNCTIONAL MOCK-UP INTERFACE (FMI) TM292

Figure 62: Opening the B&R Scene Viewer from B&R Automation Studio.

It is necessary to check if the PVI settings in the Scene Viewer are correct (see Fig. 63), then the connection to ARSim/

AR can be established.

Figure 63: Values for the PVI settings in B&R Scene Viewer.

Now the model can be controlled via the Watch window (see Fig. 64).

It is important to set both ENABLEs in the Watch window to TRUE so that the connection to the B&R Scene

Viewer is enabled and the visualization is possible (see Fig. 64, crane1.Enable = TRUE, craneView_0.Enable

= TRUE).

In the <model_name>View_0 function block, the calculations for visual representation in B&R Scene Viewer are per-

formed. If visualization is not needed, it can be switched off, thus saving computing power.

## Page 49

CREATING A MODEL USING CAD DATA49

Figure 64: The Watch window in B&R Automation Studio for controlling the B&R Scene Viewer.

Exercise: Integration in B&R Automation Studio

Add the FMU to a new B&R Automation Studio project and test it.

Import the FMU into B&R Automation Studio as an FMU library.

•

Open B&R Scene Viewer.

•

Establish a connection to the B&R Scene Viewer.

•

Set the corresponding Enable values to TRUE.

•

Test the program. You should now be able to maneuver the crane.

•

## Page 50

50MAPLESIM AND FUNCTIONAL MOCK-UP INTERFACE (FMI) TM292

8SERVOsoft toolbox

SERVOsoft  can be used to size drives. The  toolbox in MapleSim is used to generateB&R SERVOsoft Data Generation

the necessary data for this purpose (see Fig. 65). From here, the  toolbox obtains theB&R SERVOsoft Data Generation

necessary dynamic information to be able to size a motor.

Figure 65: The data generator from SERVOsoft.

After the toolbox has been launched, the system can be loaded (see Fig. 66). In order to configure the drive for the

bridge motor, the bridge must be specified under  . In addition,  must be selected under actuator selectionrpmOutput

for the speed. The profile can be displayed by pressing the "Preview export" button.units

Figure 66: The chart of a velocity/torque curve generated by the motion generator.

An .xls file can then be generated by selecting a suitable directory and file name and pressing . The file createdExport

contains the sampling time, the rotation speed, the inertia (always 0 in the MapleSim export) and the torque. This data

can now be loaded from SERVOsoft once the ) load is selected and the  button is clicked (see Fig. 67).(RotarySequence

## Page 51

SERVOSOFT TOOLBOX51

Figure 67: SERVOsoft - Loading the load profile.

In the newly opened wizard, the  function must be selected on the Sequence tab (see Fig. 68).import

Figure 68: SERVOsoft - Importing data from the .xls file.

In the following window,  and  must now be selected (see Fig. 69).Time and velocityPayload and thrust

Figure 69: SERVOsoft - Selecting the data to be imported.

The data is now displayed in a newly opened wizard (see Fig. 70).

## Page 52

52MAPLESIM AND FUNCTIONAL MOCK-UP INTERFACE (FMI) TM292

Figure 70: SERVOsoft - Data from MapleSim.

Pressing  applies the data (see Fig. 71). It can then be seen that the first line is displayed incorrectly. Since there isOk

a header line in the .xls file, it must be eliminated by selecting the  option.Header

Figure 71: SERVOsoft - Removing the header.

Confirm to display the data graphically (see Fig. 73). If the source for the curves should be changed (e.g.  - seeThrust

Fig. 72), this can be done by clicking  (see Fig. 73).Select Profiles

Figure 72: SERVOsoft - Selecting the profile.

## Page 53

SERVOSOFT TOOLBOX53

Figure 73: SERVOsoft - Profiles for speed and thrust.

After confirmation, the load profile is now applied and can be used to size the drive (see Fig. 74). To do this, select the

drive unit ) and then press the  button.(Drive and motorOpen servo motor database

Figure 74: SERVOsoft - Selecting drive units.

The motor and drive can now be selected. First, a motor suitable for the load profile is selected (see Fig. 75).

## Page 54

54MAPLESIM AND FUNCTIONAL MOCK-UP INTERFACE (FMI) TM292

Figure 75: SERVOsoft - Selecting the motor.

Afterwards, an inverter suitable for the motor is selected (see Fig. 76).

Figure 76: SERVOsoft - Selecting an inverter.

All percentage bars must be in the green area. When confirming the selection of inverter and motor, a summary is

provided in the general overview (see Fig. 77). Also here, green percentage bars indicate that the drive unit can generate

enough power for the required load profile or voltage and current for the motor.

## Page 55

SERVOSOFT TOOLBOX55

Figure 77: The SERVOsoft software with correctly selected drive modules.

The selected modules can also be found on the B&R website, e.g. the ACOPOS module (see Fig. 78). This can now be

integrated into B&R Automation Studio.

Figure 78: The selected ACOPOS module.

Additional information can be found in .TM465 - SERVOsoft

TM465 - SERVOsoft

Exercise: Using the SERVOsoft Export toolbox

Open the  app.SERVOsoft

•

Load the system.

•

Select the bridge drive.

•

Set the output speed in rpm and select  in the output window. View the curve.thrust vs. velocity

•

Select a suitable directory and file name and export the data to the .xls file.

•

Exercise: Using the SERVOsoft software

Open the SERVOsoft software.

•

Select  under the  group to load the load profile.RotaryAxis

•

Load the .xls file that was generated in MapleSim using the  toolbox.B&R SERVOsoft Data Generation

•

Display and verify the corresponding speed and load torque curves.

•

Now select the  option under  to be able to size the drive unit.Open servo motor databaseDrive and motor

•

Select a suitable motor and inverter.

•

Verify that the selected drive unit is the correct choice.

•

## Page 56

56MAPLESIM AND FUNCTIONAL MOCK-UP INTERFACE (FMI) TM292

9Adding an ACOPOS drive

Integrating an ACOPOS drive

The next step is to integrate an ACOPOS drive in B&R Automation Studio and simulate it parallel to the FMU for the

overhead bridge crane. The speed of the ACOPOS drive is selected as the input parameter, which is then directly as-

signed to the speed of the overhead crane in the program code. To do this, an  object must first be created (seeAxis

Fig. 79).

Figure 79: Adding an Axis object to the mappMotion library.

The  object must be configured accordingly (see Fig. 80).Axis

Figure 80: Configuring the Axis object.

The corresponding drive (found with SERVOsoft) can now be added (see Fig. 81). The corresponding motor can also

be integrated at the same time.

## Page 57

ADDING AN ACOPOS DRIVE57

Figure 81: Adding an ACOPOS drive and a drive unit to the project.

The configuration of the ACOPOS drive is now opened and the defined  object assigned there (see Fig. 82, ).AxisgAxis_1

Figure 82: Parameter values for the ACOPOS drive.

The configuration of the ACOPOS is thus completed. To be able to use this in conjunction with the FMU, a new program

will be created (see Fig. 83).

## Page 58

58MAPLESIM AND FUNCTIONAL MOCK-UP INTERFACE (FMI) TM292

Figure 83: Create a new task to connect the ACOPOS drive to the FMU.

In the new program, the speed output of the ACOPOS drive will be assigned to the speed input of the bridge (see Fig.

84).

Figure 84: Program code to connect the ACOPOS drive output to the FMU input.

## Page 59

ADDING AN ACOPOS DRIVE 59
The drive can now be set in motion in Automation Studio to check the functionality. It is also possible to view the
movement in the Scene Viewer file at the same time.
Additional information can be found in TM412 - ACOPOSinverter configuration and commissioning.
TM412 - ACOPOSinverter configuration and commissioning
Exercise: Integrating an ACOPOS drive into B&R Automation Studio
Creating an ACOPOS drive in B&R Automation Studio and testing it. A trace can be used to view the initial values and
thus check the correct behavior of the overhead bridge crane.
Create an axis in mappMotion and configure it.
•
Insert an ACOPOS drive and a motor according to the configuration in SERVOsoft.
•
Configure the ACOPOS drive and link the axis that has been created to it.
•
Create a new program and link the speed from the output of the ACOPOS drive to the input of the FMU.
•
Give various commands to the drive and record the speed and motor torque. Observe the behavior of the model
•
in the Scene Viewer.

## Page 60

60MAPLESIM AND FUNCTIONAL MOCK-UP INTERFACE (FMI) TM292

10Exercises

Exercise: Variable pendulum length

Change the overhead bridge crane model so that the rope length is variable. One possibility is using a translational

from the  under the category .spring damper actuatorMultibody libraryForces and moments

Figure 85: Overhead bridge crane model with variable rope length.

Create a crane model with variable rope length. Tip: Use a speed drive for easier operation in AS and to avoid

•

large position jumps in AS.

Create a new FMU in MapleSim and integrate it in B&R Automation Studio. Measure interesting signals and out-

•

put them.

Test the new model.

•

## Page 61

SUMMARY61

11Summary

Nowadays, it is possible to simulate extensive and complex machines and their physical properties.

It's therefore possible to simulate machines in advance without having to build the real machine. This saves time and

money because errors can be detected and corrected at an early stage. The control functionality when using a B&R PLC

for such machines can also be tested and verified in advance.

MapleSim is a program that allows simulation of physical entities at machine level. The Modelica approach allows even

large and complex systems to be set up quickly and easily. This can be used to derive an FMU that can be integrated

and simulated in B&R Automation Studio. The result can be visualized and verified using B&R Scene Viewer.

Drives can also be optimally sized to handle dynamic system conditions using SERVOsoft. The B&R SERVOsoft toolbox

provides an easy way to quickly get the associated data into the program. An inverter and drive layout can be created.

In B&R Automation Studio, the inverter and the drive unit can be simulated and coupled with the simulation model.

This allows the layout to be verified.

The  training module provides initial insight into how physical sys-MapleSim and Functional Mockup Interface (FMI)

tems or machines can be modeled as mathematical models using a physics modeler (MapleSim) and connected to

models (drive model) in B&R Automation Studio.

MapelSim Gallery

MapleSim Gallery offers a large number of finished models. It is in any case useful to get ideas before modeling is

started. This helps avoid aberrations. The models can be downloaded, and a video can also be used to quickly identify

system behavior in advance.

Figure 86: MapleSim Gallery

## Page 62

62MAPLESIM AND FUNCTIONAL MOCK-UP INTERFACE (FMI) TM292

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

## Page 63

AUTOMATION ACADEMY 63

## Page 64

B&R

Industrial Automation GmbH

A member of the ABB Group

B&R Straße 1

5142 Eggelsberg, Austria

office@br-automation.com

t +43 7748 6586-0

f +43 7748 6586-26

br-automation.com

V2.0.0.1 ©2023/09/27 by B&R, All rights reserved.

All registered trademarks are the property of their respective owners.

Subject to technical changes without notice.