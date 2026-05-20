## Page 1

TM480

The basics of hydraulics

## Page 2

2 THE BASICS OF HYDRAULICS TM480
Prerequisites and requirements
Training modules TM210 – The basics of Automation Studio
TM400 – Introduction to motion control
Software AS 4.1 or later
Hardware ---

## Page 3

TABLE OF CONTENTS 3
Table of contents
1 Introduction.........................................................................................................................................................4
1.1 Learning objectives..............................................................................................................................4
1.2 Typical tasks..........................................................................................................................................4
2 Controlling hydraulic systems.........................................................................................................................6
2.1 Valve-based hydraulic drives..............................................................................................................6
2.2 Hydraulic servo pump drives.............................................................................................................8
3 Simulation models...........................................................................................................................................10
3.1 Simulation model for valve-based hydraulic drives....................................................................10
3.2 Simulation model for hydraulic servo pump drives....................................................................10
4 Basic information.............................................................................................................................................12
4.1 Hydrostatics........................................................................................................................................12
4.2 Flowing fluids......................................................................................................................................13
5 Structure of a hydraulic system....................................................................................................................15
5.1 System pressure pump.....................................................................................................................15
5.2 Tank.......................................................................................................................................................16
5.3 Cylinder.................................................................................................................................................16
5.4 Valves....................................................................................................................................................17
5.5 Sensors.................................................................................................................................................21
5.6 Hydraulic fluid.....................................................................................................................................22
5.7 Pipes and hoses..................................................................................................................................22
6 Coupled axes.....................................................................................................................................................23
6.1 Application solution...........................................................................................................................23
6.2 Linking to an ACOPOS axis..............................................................................................................24
6.3 Linking to an ARNC0 axis.................................................................................................................25
6.4 Exact synchronization......................................................................................................................26
7 Safety technology............................................................................................................................................28
8 Summary............................................................................................................................................................30
9 Literature............................................................................................................................................................31

## Page 4

4 THE BASICS OF HYDRAULICS TM480
1 Introduction
The field of hydraulics deals with the transfer of energy and signals using fluids. Hydraulic drive technology uses hy-
draulics to generate mechanical movements. The hydraulic system almost always assumes the role of a gearbox that
converts the power of a motor (electric or diesel motor) into a mechanical movement of a hydraulic actuator (linear/ro-
tary hydraulic motor).
Hydraulic system
Mechanical
Mechanical
Energy
Energy
(motor)
(piston)
Hydraulic energy (f luid)
Figure 1: Basic principle of a hydraulic drive system
Training module structure
This training module will begin by exploring the two basic procedures involved in controlling hydraulic drives. It will
also introduce and explain the different simulation models available for these two procedures.
We will then turn to discussing basic hydraulic relationships, including conventional standard components used in a
hydraulic circuit such as tanks, pumps and motors as well as linear drives, valves and piping systems.
Additional information about using the Automation Studio libraries MTHydValve (valve-based hydraulic control) and
MTHydPump (servo pump control) is covered in the TM481 and TM482 training modules, respectively.
1.1 Learning objectives
This training module uses practical examples to provide a fundamental understanding of how hydraulic control sys-
tems operate.
Closed-loop hydraulic drive control
Participants will become familiar with the components of valve-controlled hydraulic systems and systems with a
•
controlled servo pump.
Participants will learn the fundamentals of hydrostatics and the behavior of flowing liquids.
•
Participants will learn about the possibilities for simulation and for coupling hydraulic and electrical axes.
•
Participants will learn how to select the appropriate hardware and Automation Studio library based on the appli-
•
cation at hand.
Participants will learn how to understand the needs of a hydraulic solution and be able to evaluate the challenges
•
of hydraulic control.
1.2 Typical tasks
A standard application in hydraulics is providing position control of one or more hydraulic cylinder(s). The closed-loop
control task is to control a hydraulically-driven axis in such a manner so that it follows a defined position setpoint.
For many applications, however, simple position control is not enough. An application will often require the hydraulic
axis to generate a precisely defined force. To do this, the system also needs to provide force control as well as smooth
transitions between this and position control.
These type of solutions are typically found on all types of presses and in the closing mechanisms of injection molding
machines.

## Page 5

INTRODUCTION5

Figure 2: Hydraulic granulate molding press

A simplified hydraulic schematic might look something like this:

Figure 3: Hydraulic schematic for a granulate press

## Page 6

6 THE BASICS OF HYDRAULICS TM480
2 Controlling hydraulic systems
There are two methods for controlling the consumer in hydraulics.
2.1 Valve-based hydraulic drives
When using valve-based control (also known as resistance control), the flow of energy is directed through variable
hydraulic resistors (valves). This type of controller is generally very precise but sometimes has the disadvantage of
substantial loss since the orifice losses from the valve can, depending on the application, be used for control. The
following is a simplified circuit diagram for resistance control. The system pressure PumpPressure is kept constant,
and the volumetric flow rates Q and Q – and therefore the movements of the hydraulic cylinders – are set using a
A B
continuous valve.
Q
A
PumpPressure
Q
B
TankPressure
Figure 4: Simplified circuit diagram of valve-based control with continuous valve
Hardware and software concept
Software blocks included in the MTHydValve library can be used to implement valve-based hydraulic control. These
control blocks can be used to set up a hydraulic controller on any control system from B&R.
The following diagram shows an overview of the hardware concept used to implement this type of hydraulic control.
The control loop is established using standard input and output modules such as analog output modules for control-
ling the valves, analog input modules for reading pressure signals and encoder modules for reading signals from the
position sensors.

## Page 7

CONTROLLING HYDRAULIC SYSTEMS7

ActCylinderPosition

CylinderArea2CylinderArea1

ActCylinderPressure1

position

12

ActCylinderPressure2

velocity

hydraulic

cylinder

ActCylinderPressure1

ActCylinderPressure2

proportional

valve

AB

PLCValveSignal

PT

ActPumpPressure

Figure 5: B&R hardware concept for valve-based hydraulic drives

## Page 8

8 THE BASICS OF HYDRAULICS TM480
2.2 Hydraulic servo pump drives
With hydraulic servo pump drives, the volumetric flow rate to a consumer is regulated directly by adjusting a hydraulic
generator (e.g. a pump). This type of control thus operates without loss (aside from the loss of efficiency of the com-
ponents being used). The pump only generates as much energy as is actually consumed by the motor. This type of
configuration is also called a hydrostatic gear.
There are also some general disadvantages compared to valve-based hydraulic drives. The considerably more compli-
cated construction results in higher costs, for example. Making adjustments is also more time-consuming due to the
inertia involved in moving rotary machines.
Pump
M
Pressure
TankPressure
Figure 6: Simplified schematic of a hydraulic servo pump drive with pump and switching valve

## Page 9

CONTROLLING HYDRAULIC SYSTEMS9

Hardware and software concept

The following diagram depicts the B&R hardware concept for implementing a hydraulic servo pump drive. This system

uses switching valves to select the hydraulic actuator (hydraulic cylinder). The cylinder movement itself is governed by

the speed controller located directly on the ACOPOS servo drive. The higher-level controller forwards profile setpoints

to the ACOPOS servo drive.

CylinderArea2CylinderArea1

ActCylinderPressure1

12

ActCylinderPressure2

velocity

hydraulic

cylinder

PLC

SetPumpSpeed

SetPumpPressure

switching

valve

ABK

N

ValveSignal

IL

R

E

W

PT

O

P

ActPumpPressure

ActPumpSpeed

Figure 7: Hardware concept for a hydraulic servo pump drive

The control technology is complemented by software technology blocks in the MTHydPump library. This library con-

tains blocks for implementing hydraulic servo pump drive control. They also perform tasks such as ensuring that the

correct control structure is being applied to the ACOPOS servo drive and contain protective mechanisms for safe op-

eration.

## Page 10

10 THE BASICS OF HYDRAULICS TM480
3 Simulation models
Predefined simulation models are available for both types of hydraulic drive control systems that allow the control
blocks to be tested before commissioning.
We will be using them in this training module to understand the characteristics of the different hydraulic systems.
3.1 Simulation model for valve-based hydraulic drives
The dynamic behavior of a valve-based hydraulic drive with hydraulic cylinders can be simulated using the function
block MTHydValveSimulationModel from the MTHydValve library.
MTHydValveSimulationModel
BOOL Enable Active BOOL
MTHydValveSimCylinderParType CylinderParameters Error BOOL
MTHydValveSimValveParType ValveParameters StatusID DINT
MTHydValveSimSystemParType SystemParameters UpdateDone BOOL
BOOL Update
REAL ValveSignal ActCylinderPosition REAL
REAL ActPumpPressure ActCylinderVelocity REAL
REAL ProcessForce ActCylinderAcceleration REAL
ActCylinderPressure1 REAL
ActCylinderPressure2 REAL
ValveOpening REAL
Figure 8: Function block MTHydValveSimulationModel
Programming \ Libraries \ Mechatronic libraries \ Hydraulics \ MTHydValve \ Function blocks \ MTHyd-
ValveSimulationModel
3.2 Simulation model for hydraulic servo pump drives
The function block MTHydPumpSimulationModel from the library MTHydPump is available to map the dynamic behav-
ior of a hydraulic servo pump drive with simulation.

## Page 11

SIMULATION MODELS 11
MTHydPumpSimulationModel
BOOL Enable Busy BOOL
LREAL StartCylinderPosition Active BOOL
MTHydPumpCylinderParType CylinderParameters Error BOOL
MTHydPumpValveParType ValveParameters StatusID DINT
MTHydPumpPumpParType PumpParameters
MTHydPumpSystemParType SystemParameters
SINT ValveSignal CylinderPosition REAL
REAL PumpSpeed CylinderVelocity REAL
REAL ProcessForce PumpPressure REAL
CylinderPressure1 REAL
CylinderPressure2 REAL
Figure 9: Function block MTHydPumpSimulationModel
Programming \ Libraries \ Mechatronic libraries \ Hydraulics \ MTHydPump \ Function blocks\ MTHyd-
PumpSimulationModel

## Page 12

12THE BASICS OF HYDRAULICS TM480

4Basic information

Our goal for the hydraulic systems covered in this training module is to utilize the laws of hydraulics – more specifically,

the laws of hydrostatics – to generate a desired movement. For this reason, we will be discussing some fundamental

hydraulic relationships in the following section.

4.1Hydrostatics

The basic law of hydrostatics assumes a massless, frictionless and incompressible fluid.  applies to thesePascal's Law

types of fluids:

Pressure exerted anywhere in a confined incompressible fluid is transmitted equally in all directions throughout the

fluid. The pressure ratio in the fluid is equal to the load force with respect to its effective surface. The pressure is

always applied at 90 degrees to the wall of the container.

One example of this is the hydrostatic press.

Figure 10: Hydrostatic press

Here, pressure p is the effect of a force F applied to a surface A.

The following units of measurement are commonly used for pressure:

Printing unitValue (SI unit)

1 Pa (pascal)

1

1 bar5

10

1 psi (pound per square inch)

7030

Table 1: Units of measurement for pressure

The  in hydrostatics (Archimedes) follows from this relationship, which states that a small amount ofLaw of the Lever

force F can generate a large amount of force F if the surface area A2 is larger than the surface A.

121

## Page 13

BASIC INFORMATION13

4.2Flowing fluids

In a fluid at rest, pressure increases with depth, and points at the same depth have the same pressure. If a fluid flows

through a pipe, however, a drop in pressure occurs. Similar to electrical engineering, where an electrical current causes

a drop in voltage on an electrical resistor, in hydraulics a flowing fluid leads to a drop in pressure along the direction

of flow.

Figure 11: Electrical and hydraulic resistance

ElectronicsHydraulics

Voltage UPressure p

Current IVolumetric flow rate Q

Electrical resistance RHydraulic resistance R

EH

Table 2: Analogy between electrical and hydraulic values

Pipe flow

If the flow speed in a pipe is low enough, then there is a linear relationship between the loss of pressure and the

volumetric flow rate. This is what is known as  pipe flow; the resistance coefficient depends on the diameter andlaminar

length of the pipe as well as the viscosity of the fluid. Laminar pipe flow becomes unstable when the volumetric flow

rate increases, however, leading to what is known as  pipe flow. In this case, there is a quadratic relationshipturbulent

between the loss in pressure and the volumetric flow rate.

Figure 12: Laminar pipe flow

## Page 14

14THE BASICS OF HYDRAULICS TM480

Pipes in hydraulic systems are usually designed to promote laminar flowing, which considerably reduces pressure drop

and flow noise.

Flow through (sharp-edged) orifice:

Orifices produce a square-root relationship between the flow through the orifice and the pressure difference. Since

hydraulic valves in particular work according to this principle, this is especially important for controlling a hydraulic

system. The loss of pressure at the orifice depends on the geometry and the construction of the orifice. The smaller

and sharper the opening of the orifice, the higher the loss of pressure will be.

Figure 13: Flow through a sharp-edged orifice

Since the loss in pressure is almost entirely dependent on the geometry of these type of orifices rather than the tem-

perature of the oil, hydraulic valves are designed according to this principle. This provides robust, temperature-inde-

pendent control action for a hydraulic system.

## Page 15

STRUCTURE OF A HYDRAULIC SYSTEM15

5Structure of a hydraulic system

A hydraulic drive system is generally composed of the following components:

System pressure pump

•

Tank

•

Cylinder

•

Valves

•

Sensors

•

Hydraulic fluid

•

Pipes and hoses

•

In the following sections, we will take a closer look at the most important components of a hydraulic circuit.

5.1System pressure pump

Most displacement devices can be used as either a motor or a pump since the hydrostatic feeding process is reversible.

In actual usage, however, pumps and motors are often designed differently and optimized for their respective appli-

cation.

Maximum speed, maximum differential pressure, displacement and overall efficiency are important characteristics

of a pump or rotary motor. These characteristics are also decisive in actual usage; of course, the construction also

plays a role here. The displacement volume V is defined as:  , where Q represents the volumetric flow rate and

D

n represents the speed. Servo motors and pumps allow the displacement volume to be varied, while in fixed displace-

ment pumps it remains constant. Pumps have the ability to change the flow rate by modifying the drive speed and

the amount of displacement.

In industrial environments, axial piston pumps are used quite frequently. These are further subdivided into wobble-

plate, swashplate and bent-axis pumps. Of these variations, swashplate pumps (see image below) are the most sig-

nificant since they are very robust and can be manufactured inexpensively. With this type of pump, the cylinder drum

(1) is driven. This also sets the pistons (2) inside the drum into motion. These pistons are fastened via the slide shoes

(4) to the swiveling swash plate (3) mounted in the housing. This causes them to also move in the axial direction when

rotating. While the pistons move to the left, they suck fluid into the resulting vacuum area from the connected line,

and when they move to the right they push the fluid back out. This is referred to as the pump's suction and pressure

phases. The stationary valve plate (5) switches the connected lines between the suction and pressure phase.

## Page 16

16THE BASICS OF HYDRAULICS TM480

Figure 14: Cross section of a swash plate pump

5.2Tank

There are two types of hydraulic systems: those with a closed circuit, where the flow returning from the consumer

is fed right back into the pump, and systems with an open circuit. In an open circuit, the hydraulic fluid flows from

a reservoir (the tank) through the pump to the consumers and back to the tank via supply lines. The tank's volume

is calculated such that the pump's defined volumetric flow rate causes a specific hydraulic fluid retention time in the

tank. This retention time is used to separate out any air bubbles and particulate matter that may have gotten into the

hydraulic fluid during installation, when the components were machined or as a result of normal wear. It is also used

to separate out water and dissipate heat via the reservoir's surface.

5.3Cylinder

Like rotary motors, hydraulic cylinders also convert hydraulic energy back into mechanical energy. Due to their simple

construction, hydraulic cylinders are highly robust and cost-effective actuators for linear movements. Single-action

and double-action cylinders are the two main types. Double-action cylinders can be either equal-area cylinders (both

piston surface areas are the same size) or differential cylinders (the piston surface areas are different sizes). The dif-

ferent piston surface areas of A and A cause double-action differential cylinders to have a preferred direction of

posneg

movement that can generate more force. This is usually defined as the positive direction of movement. The speed of

the pistons is higher when moving in the negative direction of movement, however.

## Page 17

STRUCTURE OF A HYDRAULIC SYSTEM17

Figure 15: Hydraulic cylinder (differential cylinder)

This asymmetry (different gain for movement in the positive or negative direction) must be taken into consideration

when controlling a hydraulic cylinder in order to achieve a consistent dynamic behavior in both directions of movement.

Cylinders are often designed with end position damping. Keep in mind that these damping elements are designed for

cylinder delay without an external load. These types of damping elements cannot be used to decelerate moving loads.

A suitable position controller must be used to do this.

5.4Valves

According to the DIN/ISO 1219 standard, valves are defined as follows:

"Devices for the open or closed loop control of start, stop and direction as well as pressure or flow (volumetric flow

rate) of the pressure medium extracted by the hydraulic pump or stored in a container".

Valves vary based on:

Design (slide, rotary slide or seat type)

•

Control (manually operated, electrical, hydraulic, pneumatic, etc.)

•

Action (continuous, switching)

•

unterscheiden.

Valves can implement different functionality depending on how these characteristics are combined, for example:

Determining direction

•

Closed-loop pressure control

•

Closed loop control of volumetric flow rate

•

Lock

•

Etc.

•

Continuous valves: Proportional and servo valves

The switching position of continuous valves can be modified constantly. This makes it possible to set the direction as

well as the precise volumetric flow rate.

## Page 18

18THE BASICS OF HYDRAULICS TM480

With a multi-stage valve, the control signal (usually a voltage value in the range -10 V .. +10 V) for the valve does not

directly cause the valve spool to move and in turn change the volumetric flow rate. Instead, one or more power stages

reinforce this signal in order to move the spool rapidly and change the volumetric flow rate through the valve more

quickly. Servo valves, which are used as particularly high-speed actuators, always have a multi-stage design.

The valve connections can be seen in the following image. They are labeled P for high pressure, T for low pressure (tank)

and A and B for the lines to and from the consumer, e.g. to the pressure chambers of a hydraulic cylinder. X indicates

the connection for a control pressure line. It is used here to provide the pressure needed for the hydraulic piloting

stage in the valve. Y indicates an overflow oil line that is also connected to the tank.

Figure 16: Cross-section of a 4/3-way servo valve

Continuous valves are characterized using two characteristic curves. Due to their structure, they have a volumetric

flow rate to pressure ratio similar to orifices when opened a certain amount. They are usually described by specifying a

nominal volumetric flow rate for connections A and B as Q or Q at nominal pressure difference and maximum

nom,Anom,B

valve opening. This allows the volumetric flow rate / pressure ratios to be specified in the following form:

and

## Page 19

STRUCTURE OF A HYDRAULIC SYSTEM19

The negative sign in front of Q means that the volumetric flow runs out of the chamber instead of into the chamber.

B

To adapt the volumetric flow rates of connections A and B to asymmetric consumers such as differential cylinders,

valves are often designed with different nominal volumetric flow rates.

The second characteristic curve of a continuous valve is determined by its fabrication and is specified in its data sheet

as the valve characteristic curve. It specifies the ratio between the valve voltage and the valve opening. The following

image illustrates a typical valve characteristic curve for a 4/3-way proportional valve. The characteristic curve is usually

flatter where the input voltage is lower, an area known as the microcontrol range, and then becomes steeper as the

voltage increases. This makes it possible to increase the resolution at smaller valve openings.

## Page 20

20THE BASICS OF HYDRAULICS TM480

Switching valves

Switching valves are direction control valves with fixed switching positions and are therefore non-continuous ele-

ments.

Pressure relief valves

Pressure relief valves limit the maximum pressure on their input to a mechanically-adjustable value and are generally

used to implement safety-related functions. They are an important component of every hydraulic system. A pressure

relief valve should always be positioned directly behind the system pressure pump to limit the maximum system pres-

sure.

## Page 21

STRUCTURE OF A HYDRAULIC SYSTEM21

Pressure regulating valve

A pressure regulating valve keeps the output pressure at a constant value that can be adjusted mechanically. It is

sometimes used to maintain a constant pressure level in a hydraulic system. The disadvantage of this type of valve

is a noticeable drop in pressure when the input pressure on the valve is very high, thereby causing significant power

loss through the valve.

Check valve

Check valves ensure that fluid can only flow in one direction.

5.5Sensors

There is a saying in closed loop control that holds equally true for hydraulics: Your control will never be more precise

than your measurements!

Additional sensors can often be used to improve the control quality in a hydraulic system if the sensors supply suffi-

ciently accurate and current measured values.

The following are the most important sensors for a hydraulic positioning or force control system.

Position sensors

These sensors are the most important for a position controller because they measure the controlled variable. The

following sections will deal mostly with an SSI encoder since it is the most commonly used encoder type.

Pressure sensors

Hydraulic systems usually have a manometer near the system's high-pressure point. A manometer is an easy-to-read

pressure gauge on which the measured pressure is most commonly indicated using a needle (or sometimes using a

digital display). However, these measuring instruments only provide information about average pressure at a given

time and are not suitable when the pressure changes rapidly. Electrical pressure sensors are used to quickly and accu-

rately record pressure values or pressure curves that can be used for regulating pressure. There are three main types

here:

Piezoelectric pressure sensors: Piezo elements that are compressed by fluid pressure create voltage proportional

•

to the pressure, which is amplified by a measurement amplifier.

Strain gauge pressure sensors: Strain gauges are attached to a membrane. Deformation of the membrane under

•

pressure causes a change in resistance which is evaluated by a measuring bridge.

Piezo-resistive pressure sensors: These sensors are highly compact. They work like strain gauge sensors, except

•

that the membrane is made of silicon and contains a semiconductor that allows the strain gauge bridge to be in-

tegrated in the membrane.

Load cells

Load cells also function according to the same principles as the pressure sensors. However, they are built to measure

force instead of pressure.

## Page 22

22 THE BASICS OF HYDRAULICS TM480
Additional sensors
Flow rate sensors, temperature sensors, and rotational angular encoders are also used for motors that can be inte-
grated in a closed loop control system.
With the exception of the position sensors, most sensors used in hydraulic systems provide voltage signals in the
ranges 0 V to 10 V and -10 V to 10 V or current signals in the ranges 0 to 20 mA and 4 to 20 mA.
5.6 Hydraulic fluid
In addition to the primary tasks of transferring force and energy as well as providing the volumetric connection be-
tween pumps and consumers, the pressure fluid also fulfills secondary functions such as reducing the wear caused by
friction between moving parts by providing lubrication, protection against corrosion and dissipating frictional heat.
Pressure fluids have two properties that are significant for the regulation of hydraulic systems: Compressibility and
viscosity.
Pressure fluid volume is not constant; instead it decreases under the influence of compressive forces. The compress-
ibility is usually specified by the compression module. The compression module for conventional hydraulic oils is in
the range of E = 16000 bar and is greatly decreased, particularly in the lower pressure range, when air bubbles are
dissolved in the oil. The effective compression module for a hydraulic system depends on the layout of the piping and
on the use of hoses.
The viscosity of a hydraulic fluid determines its load-bearing capacity and is greatly decreased as the temperature
rises, which has a significant influence on the damping of hydraulic controlled systems. A viscosity value that is too
low causes high leakage losses and a toughness that is too high causes losses resulting from internal fluid friction.
5.7 Pipes and hoses
Every hydraulic system has a piping system for high pressure (P for "pump") and for low pressure (T for "tank").
Additionally, overflow oil lines (L or Y) are often used to dispense of "leak oil" that escapes from gaps in the seal in pis-
ton machines and which must be removed pressure-free from the housing. Servo-hydraulic systems also use separate
control oil lines (X) to provide a pressurizing medium to the hydraulic piloting stages. This measure can be compared
with the separation of the signal and power circuits in electronics.
Lines can be designed as pipes or hoses, whereby hoses are of course much softer in regard to their volume constancy
under high pressures and therefore significantly reduce the effective compression module of pressure medium and
line.

## Page 23

COUPLED AXES23

6Coupled axes

Real machines often require synchronizing multiple axes with one another in a controlled manner. Depending on the

complexity of the synchronization conditions, there are many different ways to establish axis links.

6.1Application solution

A simple link between a hydraulic axis and another hydraulic or electrical axis can be implemented directly in the ap-

plication code using setpoint generation for the hydraulic control block.

The following images show an example of how an axis coupling could look within the application for valve-based hy-

draulic drives.

SetCylinderPosion

EndPosion

MTProﬁlePosionGenerator

ValveOpeningValveSignal

SCALE

123

MTHydValvePosionControllerMTHydValveLinearizaon

Axis 1

GAIN

ValveOpeningValveSignal

SCALE

123

MTHydValvePosionControllerMTHydValveLinearizaon

Axis 2

Figure 17: Axis coupling via application – valve-based hydraulic drives

## Page 24

24THE BASICS OF HYDRAULICS TM480

The following images show an example of how an axis coupling could look within the application for servo pump drives.

nSetSetVelocitySetVelocity

+nSet

SetPositionpSetpSet

MTLookUpTable

MTProﬁlePosionGeneratorMTHydPumpController

ActPositionAddVelocity

MTBasicsPID

Axis 1

GAIN

SetVelocitynSetSetVelocity

+nSet

pSet

pSet

MTLookUpTable

MTHydPumpController

SetPosition

ActPositionAddVelocity

MTBasicsPID

Axis 2

Figure 18: Axis coupling via application – hydraulic servo pump drives

6.2Linking to an ACOPOS axis

One real and one virtual axis can be calculated on any ACOPOS servo drive. A virtual axis makes it possible to calculate

a position setpoint as a function of the actual position and the position setpoint of another real or virtual axis, which

can be located on another ACOPOS servo drive (cam profile functionality).

A virtual axis on an ACOPOS drive can also be used to generate position setpoints for a hydraulic axis. This position

setpoint is transferred cyclically to the CPU via POWERLINK, where the position control loop is implemented for the

corresponding hydraulic axis.

After adjusting the physical dimensions accordingly (usually a conversion from increments to millimeters), the set

value is connected with the position controller for the hydraulic axis.In this case, make sure to account for the latencies

resulting from the communication (see Section 6.4).

The following image shows an example of how an axis coupling between an electric axis and a valve-based hydraulic

axis could look.

## Page 25

COUPLED AXES25

pressure2

velocityposition

pressure1

hydraulic

cylinder

virtual axis

ActPosition

ValveSignalValveOpening

proportional

valveSetPosition

AB

123

PowerlinkHydraulic axis

PT

PLC

Figure 19: Axis coupling ACOPOS and valve-based hydraulics drive

The following image shows an example of how an axis coupling between an electric axis and a hydraulic servo pump

drive could look.

Virtual axis

nSetSetVelocitySetVelocity

+nSet

pSet

pSet

MTLookUpTable

Powerlink

MTHydPumpController

SetPosition

AddVelocityActPosition

PLC

Hydraulic axis

Figure 20: Axis coupling ACOPOS drive and hydraulic servo pump drive

6.3Linking to an ARNC0 axis

The ARNCO Soft CNC system can be used when more hydraulic axes than electrical axes are present on ACOPOS servo

drives to implement complex axis couplings (cam profiles).

In addition to the actual CNC functionality, ARNC0 also offers the possibility to implement cam profile links between

any axes on a CPU. The Soft CNC system calculates position setpoints for the electrical as well as for the hydraulic

axes. For the electrical axes, these reference values are forwarded to the position control loop in the ACOPOS unit. The

position setpoints for the hydraulic axes are forwarded to the corresponding position control loop that is implemented

in a hydraulic task on the target system.

The following image shows an example of how an axis coupling between a CNC axis and a valve-based hydraulic axis

could look.

## Page 26

26THE BASICS OF HYDRAULICS TM480

electrical

axis

pressure2

velocityposition

pressure1

hydraulic

cylinder

SetPosition

Axis 1

ActPosition

ValveSignalValveOpening

proportional SetPosition

valveSetPosition

Axis 2AB

123

ARNC0 So‐CNCHydraulic axis

PT

PLC

Figure 21: Axis coupling ARNC0 and valve-based hydraulic drive

The following image shows an example of how an axis coupling between a CNC axis and a hydraulic servo pump drive

could look.

electrical

axis

nSetSetVelocitySetVelocity

+nSet

SetPosition

pSet

pSetAxis1

MTLookUpTableSetVelocity

Axis2MTHydPumpController

SetPositionSetPosition

Axis2

AddVelocityActPosition

ARNC0 So‐CNC

PLC

Hydraulic axis

Figure 22: Axis coupling ARNC0 and hydraulic servo pump drive

6.4Exact synchronization

If position set values are transferred to an axis control loop via communication channels (e.g. POWERLINK or other

fieldbuses), then these position set values arrive delayed by the communication latency. To achieve highly-precise

synchronization of a system with multiple axes, the position set values of axes with low or no communication latencies

must be delayed. This is implemented in the ACOPOS INIT parameter module for an ACOPOS axis using the parameter

t_total, which forwards a position set value to the position control loop with a delay.

## Page 27

COUPLED AXES27

Figure 23: Setpoint delay on the ACOPOS drive

The function block MTBasicsTimeDelay from the library MTBasics offers an identical functionality.With it, signals in

the application code can be delayed, e.g. for a hydraulic axis.

## Page 28

28 THE BASICS OF HYDRAULICS TM480
7 Safety technology
In many applications, the software safety concept is not designed until after the main functionality of the system
has been completed. This can cause difficulties and even delays in commissioning because the application code must
often be revised.
A sophisticated and seamless safety concept for an application is more important than any sophisticated
and optimized control concept because a safety flaw can cause personal injury or damage to property.
However, a control concept can always be "tweaked" as long as the general safety conditions are taken
into account.
A safety concept for a hydraulic application must always include a safe control system, such as those used in B&R's
Integrated Safety Technology concept, which shuts down the drive or puts it into a safe state if operational limits are
violated. It is important to determine whether the safety controller being used is specifically designed to protect per-
sonnel or whether there is any guarantee that it will prevent potential damage to the system when shutting down in any
operating state. Integrated Safety Technology responds to a wide range of safety-related conditions in a highly flexi-
ble manner (Smart Safe Reaction), which makes it easier to create a custom safety system for different applications.
The application now has to be adapted to the functionality of the safety controller since personal injury and material
damage must be prevented at all times, particularly during commissioning. At a minimum, the following safety-related
aspects must be taken into consideration for a hydraulic application:
Limitations and prohibited operating areas
A cyclic check must occur in the application task to monitor whether all measurement signals (and any values calculated
from the measurement values) are within the permissible range so that an error can be output or the system stopped
immediately in the event of emergency.
Mandatory measurement signals and valid ranges include:
Position, which is limited by a maximum and minimum position
•
Speed, limited by a maximum value
•
Pressure, in particular the system pressure, is limited by the amount of pressure which has been specified for the
•
system (piping, components).
Particularly in a hydraulic application, the correct sequence is important when stopping and closing rotating machines
and other moving parts as well as connected valves.
The hydraulic circuit diagram in the image shown below provides an example. Here, the two safety valves V2 and V3 are
arranged so that cylinder chambers A and B will close if a malfunction occurs, which automatically stops the cylinder.
In the application, it is important to ensure that operation with the proportional valve V1 is only possible when the
safety valves V2 and V3 are open. Inversely, the situation could arise whereby excessive pressures occur in the cylinder
chambers by closing the switching valves V2 and V3 while the cylinder is in motion at high speed and the load has a
high moment of inertia. Generally, these types of situations are handled by suitable hydraulic connections with pres-
sure-relief valves. Nevertheless, these operating conditions should be avoided whenever possible.

## Page 29

SAFETY TECHNOLOGY29

Figure 24: Hydraulic circuit diagram with safety valves

The use of a trajectory generator to specify the movement profile is an important feature of the B&R hydraulic control

concept. The parameters of the trajectory generator can be configured to ensure that software position limits are

adhered to. It is important to remember, however, that these limits only apply to the drive's reference position and

not its actual position.

Integrating the safety controller

As mentioned above, any system or machine that poses potential risk of injury or damage is obligated to include safety

technology that can shut down the system via a safety controller in order to minimize risks. These sort of inputs can

also be integrated in the application task. In most cases, the application must prevent damage to the machine – and

the accompanying risk of injury – when the machine is forced to shut down by the safety equipment. In order to do

this, the safety equipment and the machine application must always be coordinated with one another.

Testing the implemented safety functions

An implemented safety function must also undergo explicit testing without putting the system or personnel at risk.

The application should not undergo function testing until the application's safety net is working properly.

## Page 30

30 THE BASICS OF HYDRAULICS TM480
8 Summary
Automation Studio and the range of B&R hardware products offer comprehensive functionality for implementing
closed loop controls for hydraulically-driven axes and linked axes as well as for maintenance and diagnostics. An im-
portant factor for successful application of hydraulic drive control is keeping a clear and organized overview of the
implemented MTHydValve and MTHydPump library functionality so that the right decisions can be made for a given
hydraulic task. These libraries are covered extensively in the TM481 (MTHydValve) and TM482 (MTHydPump) training
modules.
All that is left to do, particularly when working with standard applications, is to configure the function blocks to be
used. This helps the application engineer maintain an overview of the application at all times and create each specific
aspect of a system's overall functionality step by step.

## Page 31

LITERATURE 31
9 Literature
H. Murrenhoff: "The basics of fluid mechanics. Part 1: Hydraulics" Institute for Fluid Power Drives and Controls,
•
RWTH Aachen, ISBN 3.89653-205-3
B. Manhartsgruber: "Servo hydraulics" lecture notes, Institute of Machine Design and Hydraulic Drives, Joh. Kepler
•
University, Linz, URL: http://imh.jku.at/ftp/index.en.php

## Page 32

32THE BASICS OF HYDRAULICS TM480

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

## Page 33

AUTOMATION ACADEMY 33

## Page 34

34 THE BASICS OF HYDRAULICS TM480

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

V3.0.0.0 ©2023/10/23 by B&R, All rights reserved.

All registered trademarks are the property of their respective owners.

Subject to technical changes without notice.