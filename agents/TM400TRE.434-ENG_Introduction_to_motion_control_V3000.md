## Page 1

TM400

Introduction to motion

control

## Page 2

2 INTRODUCTION TO MOTION CONTROL TM400
Requirements
Training modules No prerequisites
Automation Studio 4.3.3
Automation Help 4.3.4
Software mapp Motion Technology Package Version 5.0
or
ACP10/ARNC0 Technology Package Version 5.0
Hardware No prerequisites

## Page 3

TABLE OF CONTENTS 3
Table of contents
1 Introduction.........................................................................................................................................................4
1.1 Learning objectives..............................................................................................................................4
2 The drive solution..............................................................................................................................................5
2.1 The basic requirements of a drive system.....................................................................................5
3 Components of a drive system......................................................................................................................6
3.1 Electric motors.....................................................................................................................................6
3.2 Position encoders..............................................................................................................................16
3.3 Inverters...............................................................................................................................................23
3.4 Drive mechanics and power transmission...................................................................................30
4 The B&R drive solution...................................................................................................................................35
4.1 Typical topologies..............................................................................................................................35
4.2 Product overview...............................................................................................................................36
4.3 Implementing the motion application..........................................................................................37
4.4 Selecting the right technology.......................................................................................................38
5 Drive sizing and tuning..................................................................................................................................40
5.1 Drive sizing with SERVOsoft............................................................................................................41
6 Documentation and installation...................................................................................................................43
6.1 User's manuals....................................................................................................................................43
6.2 Notes regarding grounding and shielding...................................................................................45
6.3 CAD configurator...............................................................................................................................46
6.4 Speed-torque characteristic curves..............................................................................................47
7 Summary............................................................................................................................................................48

## Page 4

4INTRODUCTION TO MOTION CONTROL TM400

1Introduction

Nearly every machine or system component today involves movements of varying complexity, with the trend clearly

moving in the direction of mechatronic drive solutions.

Movement sequences that used to be carried out using mechanical constructions that were sometimes quite elaborate

can now be carried out with the highest degree of flexibility and efficiency using the latest motion control technologies.

Figure 1: B&R motion control product palette

A drive solution that is uniform and can be used across different systems plays a major role these days. The more the

individual components can be coordinated with one another, the stronger the technology will be. The mechatronic

drive system can be integrated into the process as a functional unit.

This makes it possible for developers to focus primarily on optimizing the higher-level process.

This documentation will clearly and simply describe the fundamental concepts and procedures involved in motion

control.

1.1Learning objectives

This training module provides an overview of the elements and functionality of a mechatronic motion control solution.

Participants will learn about the components that make up a mechatronic motion control solution.

•

Participants will learn about the functionality of various electric drives and applications where they are typically

•

used.

Participants will learn how position encoders and inverters function.

•

Participants will learn about the properties of various drive mechanisms.

•

Participants will learn the basics of B&R drive components and how they are integrated into different topologies.

•

Participants will learn about important criteria for setting up a drive configuration.

•

Participants will be given an overview of the documentation provided for the B&R drive solution.

•

## Page 5

THE DRIVE SOLUTION5

2The drive solution

Additional terms usually connected with a drive solutions include electric drive system, power transmission, process,

drive configuration and servo drive.

These and similar expressions are frequently used to describe the range of components in a drive system. A clear

definition with a single term is virtually impossible to find.

Assessment

There is a wide range of different electrical drive systems available. In addition, there are generally multiple designs of

a single component available, each with its own specific strengths and weaknesses.

For example, a servo-driven linear motor with high-precision positioning is required for one type of application, where-

as an induction motor combined with a frequency inverter is sufficient for handling another application.

The following questions must be answered in relation to these circumstances:

What components make up a drive or positioning system?

•

What are the differences between existing technologies?

•

What are the separate technologies used for specifically?

•

A simple illustration of the components themselves attempts to answer these questions.

The following diagram generally applies to electric drive

systems across the board:

The following components are used:

Power converter or energy conversion

•

Electric motor

•

Mechanical gear (gearboxes and couplings)

•

Mechanical process (machine or mechanical system)

•

The inverter takes electrical energy from the mains and

turns it into a suitable form that can be used by the elec-

tric motor. The motor then converts the electrical energy

into kinetic energy, thereby putting the mechanical sys-

tem into motion (via a gearbox if necessary).

Figure 2: Basic motion control components

2.1The basic requirements of a drive system

What properties characterize a drive system?

This type of system needs to be highly dynamic and provide an exceptional degree of repeatability. The word "dynam-

ics" is a general term that encompasses force, propulsion or force that enacts or responds to change. This brings up

the topic of how force changes over time.

In practice, the following characteristics are often necessary:

Quickly reaching a certain speed

•

Quickly reaching an exact position

•

Being able to maintain a certain speed over time

•

Being able to maintain a predefined torque

•

As a result, a drive system must be able to position the connected mechanical components exactly according to spec-

ifications – while applying the greatest amount of force – without losing precision.

This characteristic ties directly into the productivity of the machine. In many applications, it is the positioning precision

that determines whether or not a certain drive system is a suitable solution. In addition to its dynamic properties, a

drive must also be able to take on precise positions and regulate them with an appropriate amount of force.

Selecting the electric motor is not the only decisive factor, however. Sophisticated measuring equipment and control

algorithms also play a major role in handling these tasks.

High demands can only be met when all of the components in a system interact seamlessly.

Selecting the proper drive and motor technology therefore makes it possible to match technical requirements to a

cost-efficient solution.

## Page 6

6INTRODUCTION TO MOTION CONTROL TM400

3Components of a drive system

A drive system refers to a physical structure that is able to move a machine through the act of energy conversion.

When looking at the process, it is best to start at the machine, which is usually driven by an electric motor. A gearbox can

sometimes be used to connect these two separate units in order to help with the adjustment of rotation and torque.

Figure 3: Schematic illustration of a drive system

An electric motor converts electrical energy into mechanical energy. This results in various levels of torque and force. In

order to control these values as needed, a power converter is required. Its main task is to prepare the electrical energy

that will be supplied to the motor.

For positioning tasks, it is important to know the current position of the drive. In these cases, a position encoder is

used; it is usually mounted directly on the motor.

The inverter receives its positioning commands from a controller, which executes the application program containing

the necessary motion sequences.

3.1Electric motors

The history of the electromechanical machine – in particular the development of motors – starts at the beginning of

the 19th century. Over time, many different types have emerged that vary not only in their construction, but in their

basic characteristics as well.

All of these variations were designed to be used for particular tasks, which further highlighted their strengths in those

areas.

The most conventional motor types are as follows:

3.1.2 "DC motors"

•

3.1.4 "Asynchronous machine"

•

3.1.5 "Synchronous machine"

•

3.1.7 "Stepper motor"

°

3.1.6 "Direct drives"

•

3.1.6.1 "Linear motors"

°

3.1.6.2 "Torque motors"

°

The next section will deal with the properties and design of motors in general. It will shed some light on how they work

and where each particular motor type is used.

## Page 7

COMPONENTS OF A DRIVE SYSTEM7

3.1.1The basic principle of electric motors

The basic principle of electric drives can be explained by :Lorentz force

If current is applied to an electrical conductor in a magnetic field, the conduc-

tor will be acted upon by a force.

This force's direction of action depends on the direction of the two initiating

values: the flow of current and the magnetic field.

The "left hand rule" illustrates these relationships.

Figure 4: "Left hand rule"

Mathematically, the approach for determining force looks

SizeDescription

like this:

FForce vector

BMagnetic field vector

Where  is the angle between the direction of the magnet-α

LLength vector of the conductor in the

ic field and the direction the current is flowing.

field

ICurrent

For electric motors, this angle is almost always 90°, as can be seen in the following diagrams.

The force on the conductor depends on the intensity of the magnetic field,

the strength of the current and the length of the conductor inside the

magnetic field.

The following diagrams illustrate how this force is transformed into rota-

tional motion.

Figure 5: Current applied to a coil in the magnetic

field

A current-conducting, rotatable coil is located in a magnetic field. A flow of current in the conductor creates mechanical

force in the coil sections perpendicular to the direction of the magnetic field; these sections are perpendicular to the

image plane in the diagram.

## Page 8

8INTRODUCTION TO MOTION CONTROL TM400

These forces act on the rotational circumference of the coil. The torque for

the resulting rotation is represented as follows:

Starting from this position, the system would come to rest after a certain

amount of time:

Figure 6: Rotatable coil at rest

There are two ways to sustain rotational motion:

Reversing the direction of the current flow

•

Reversing the magnetic field polarity

•

Reversing the direction of the current flow

The coil rotates past the rest position as a result of its mechanical inertia. At

this point, the flow of current is reversed, thereby inverting the coil forces' di-

rection of action. The rotational movement thus continues. The exciter field

is inverted by reversing the direction of the current flow in the exciter wind-

ing. The flow of current is controlled by electronic switching elements (power

transistors), thereby eliminating mechanical parts that are subject to wear.

Figure 7: Reversing the direction of the current

flow

On a DC motor, this is achieved using a collector and brushes to establish

contact, as shown in the image above. In this context, this is referred to as

mechanical commutation.

Wear on the mechanical elements in the commutator (collector, carbon brush-

es) and the resulting required maintenance are disadvantages of the collec-

tor motor.

The exciter field (stator) can be changed electronically using power transis-

tors. In this case, the rotor corresponds to a magnet.

Figure 8: Rotation caused by reversing the

direction of current

Reversing the magnetic field polarity

Electric motors are made up of a moving part (the rotor) and a fixed part

(the stator). In our example, the rotating coil corresponds to the rotor. The

magnetic field is generated by the stator.

The information above will make it easier to understand how electric mo-

tors work.  has the job of making sure that a conductorCommutation

winding with current flowing through it is always in the exciter field in the

correct position (at 0° to the field).

Figure 9: Rotation caused by reversing the magnetic

field

## Page 9

COMPONENTS OF A DRIVE SYSTEM9

3.1.2DC motors

A direct current motor is also known as a DC motor. How a DC motor oper-

ates was touched on in the last section. A DC motor is designed with multiple

windings on the rotor that, when positioned ideally, are supplied with current

via static carbon brushes on the collector.

The stator field can be divided into several poles as well for larger motors.

How it works remains principally the same. Several carbon brushes ensure

targeted current supply for the rotor windings.

Figure 10: Design of a DC motor (haade /

de.wikipedia.org)

Before the development of industrial power electronics, the DC motor was considered more beneficial than the three-

phase motor due to its ease of use (easy speed adjustment by changing the supply voltage).

The possibilities that have emerged in modern drive technology for three-phase motors began pushing the DC motor

more and more out of the picture when it came to positioning applications.

Areas of application:

Automotive technology (e.g. windshield wiper motors, motors in power windows, etc.)

•

Consumer electronics (e.g. vibrate function on cell phones)

•

Drives

•

Household appliances (e.g. vacuum cleaners)

•

3.1.3Polyphase motor

Developments in the area of electronics and materials have led to a shift from DC motors to AC motors in drive systems.

Servo drives, which in the past were almost exclusively used for DC technology, are now giving way to three-phase

synchronous motors (see Synchronous machine).

AC motors operate according to variations in the stator field. The field generated by the stator coils where the rotor

is located is changed based on a certain timing that results in a rotating magnetic field alignment ( rotating field).→

The required voltage feed to the stator windings is best described using the voltage characteristics of the three-phase

mains power supply:

## Page 10

10INTRODUCTION TO MOTION CONTROL TM400

Figure 11: How a polyphase motor works

The sinusoidal supply voltage of the individual phases reach their respective peak values one after the other in periodic

intervals with an electrical offset of 120°. The windings are also equally distributed on the stator.

The rotor can be set up as a permanent magnet or an electromagnet (  current-conducting coil). We can therefore→

look at the rotor as a magnet that aligns itself according to the field in which it resides.

The maximum supply voltage – and therefore the maximum of the stator field influence – moves along the circumfer-

ence of the stator. The magnetic field vector made up of the individual coil fields rotates.

The rotor is "passed" between the individual stator windings.

Special polyphase motor construction types are gaining ground. For example, direct drives are steadily becoming more

popular because of their special characteristics for automated positioning.

The following two types of three-phase current electric motors differ in how the magnetic field is generated:

3.1.4 "Asynchronous machine" on page 10

•

3.1.5 "Synchronous machine" on page 11

•

For a description of how to control the different types of polyphase motors, see 3.3 "Inverters" on page

23.

3.1.4Asynchronous machine

As is normally the case with a polyphase motor, the stator of an induction motor can have a three-phase winding.

Unlike a synchronous motor, the rotor is not permanently magnetized. Conduction bars are connected in the rotor via

a short circuit ring (squirrel-cage rotor). This results in a system of conductor loops.

## Page 11

COMPONENTS OF A DRIVE SYSTEM11

Figure 12: Squirrel-cage rotor of an induction motor

Because the rotor is located in a changing magnetic field, voltage is induced in the conductor loops (Lenz's law). This

voltage generates a current flow in the conductor bars. A force (Lorentz force) caused by the stator field acts upon the

active conductors, which gets the rotor moving.

After starting, the rotor turns at a speed slightly less than that of the rotating field. This speed difference, known as

"slip", is necessary to induce enough current in the rotor to overcome friction, air resistance and load torque.

The rotor can never reach the speed of the rotating field; the movement is therefore asynchronous, which is why in-

duction motors are also referred to as asynchronous motors.

The mechanical and electrical characteristics of induction motors determine where they are typically used. Induction

motors are usually operated at their rated speed. They are rarely at a standstill since the cooling of the motor is mostly

dependent on speed. On some motors, for example, cooling is handled by a paddle wheel mounted on the rotor that

directs the flow of air through cooling fins. For fans and pumps, it often suffices to start the motor slowly and get it

up to between 30% and 100% of the rated speed.

Possible applications for induction motors:

Pumps

•

Compressors

•

Fans

•

Material handling

•

Presses, mixers, stirrers

•

Centrifuges

•

This motor type is well-suited for operation with a frequency inverter. In this case, the rotating field is

determined without reference to the rotor position.

3.1.5Synchronous machine

With these motors, the laminated stator is connected to the star-formed three-phase winding (U, V and W designs).

Connecting to three-phase mains causes the stator winding to generate a rotating field.

The rotor in a synchronous motor has either an electromagnet (current-conducting winding arrangement) or a perma-

nent magnet. In this way, the rotor field is generated "actively".

## Page 12

12INTRODUCTION TO MOTION CONTROL TM400

Figure 13: Schematic diagram of a synchronous motor

The rotor is aligned with no "slip" in the rotating field, hence the term "synchronous motor". These characteristics

make synchronous motors a good choice for positioning tasks. Speed is linked to the number of pole pairs and the

frequency of the alternating current.

The high energy density of new, extremely powerful permanent magnets in-

creases the motor's performance while simultaneously reducing its mass.

This results in increased drive dynamics and smaller motor sizes. Optimized

concentricity enables high-precision positioning.

The mechanical and electrical characteristics of synchronous motors allow

them to be operated well at standstill as well as at their rated and maximum

speed along the motor's characteristic curve. Surface cooling enables the mo-

tor to maintain a specific torque value during standstill, then approach a new

position and then take on holding torque again. Movement cycles and dynam-

ics in the ms range are possible.

Figure 14: B&R synchronous AC motor

Possible areas of application:

High-precision actuators and positioning drives

•

Electronic gear couplings

•

Handling systems

•

Machine tools, CNC applications

•

Robots

•

The following sections contain descriptions of widely used special designs of permanent-magnet syn-

chronous motors. These are often used as direct drives.

Hardware \ Motion control \ Three-phase synchronous motors

3.1.6Direct drives

A direct drive motor is unique in that the motor is connected directly to the machine. This type of system places high

demands on proper dimensioning since the speed of the motor is the same as that of the machine. This configuration

## Page 13

COMPONENTS OF A DRIVE SYSTEM 13
totally eliminates the need for a gearbox. Additional information about drive mechanics can be found here: 3.4 "Drive
mechanics and power transmission" on page 30
In this context, the drive's physical size when operating at high speeds is very important. The reason for this is not just
the omission of a gearbox, but also the reduced mass of the motor itself.
This can be attributed to the specified power output since this increases when speed is increased at the same torque.
Due to their varying speeds, direct drive motors are often divided into the following classes:
Low-speed motors
•
High-speed motors
•
Low-speed motors
With low-speed motors, speed is reduced due to the high number of poles. For example, a 30-pole machine has a
nominal speed of 200 rpm. A classic area of application involves the use of generators in hydroelectric power plants.
These generators typically have a nominal speed of 65.2 rpm based on 92 poles.
Motors that have a large number pole pairs are called "high-pole motors". These types of motors have a
lower speed and deliver higher torque.
At the same power output, these motors can provide additional torque.
High-speed motors
High-speed motors are significantly faster than conventional motors. They can reach speeds in excess of 100,000 rpm.
This is possible through the use of frequency inverters and a supply frequency from several hundred Hz to over 1000
Hz. Physically, these motors are smaller than conventional motors but have the same power output. High demands are
placed on the rotating parts as they must sometimes counteract considerable radial acceleration (centrifugal force).
Typical areas of application for high-speed motors include turbomolecular pumps (vacuum pumps) and electrical tur-
bochargers, which can achieve speeds of up to 130,000 rpm.
The achievable output power of a motor is determined by its mass and size.
Specific types of direct drive motors include:
3.1.6.1 "Linear motors"
•
3.1.6.2 "Torque motors"
•
Advantages of direct drive motors in detail
The primary consideration is to use suitable drives to provide the force, torque and movements required for carrying
out processes such as conveying, mixing or separating.
Drive dimensioning therefore requires that the machine's operating point be adjusted to the operating point of the
load process (torque, speed). This adjustment to the process is generally made using a gearbox that adjusts the torque
and speed accordingly:

## Page 14

14INTRODUCTION TO MOTION CONTROL TM400

Figure 15: Adjustment using mechanical gearboxes

A gearbox is not necessary when the operating point of the process coincides with that of the machine. The motor –

in this case, the electric motor – becomes a direct drive motor.

A direct drive motor is free from backlash since gearbox or ball screw mechanisms are not used.

System values such as current, force/torque and speed/rotations can be determined directly and integrated into a

control concept. In addition to improving positioning accuracy, this also makes it easier to control the drive.

General characteristics of direct drive motors:

Low inertia

•

Precision (no backlash) coupled with dynamics

•

No use of mechanical parts that are subject to wear

•

Small installation dimensions

•

Large hollow shaft diameters possible

•

The high power density of direct drive motors means that they can become considerably warm. Because of this, they

are often equipped with water or air cooling systems, which is not always necessary in comparable drives that use

mechanical power conversion.

3.1.6.1Linear motors

Translational direct drive motors use the functional principles of rotating motors (translation = movement in a straight

line). The principle of the permanent magnet synchronous motor is the most common.

Figure 16: Linear motor design

## Page 15

COMPONENTS OF A DRIVE SYSTEM15

Linear motors have the same components as polyphase motors, i.e. a stator and rotor, in linear form. The rotor slide

is positioned linearly due to the three-phase current feed for the stator windings.

Areas of application:

Machine tools

•

Positioning systems

•

Handling systems

•

Shearing equipment

•

SuperTrak / ACOPOStrak

•

3.1.6.2Torque motors

Torque motors are generally designed and manufactured as high-pole, per-

manent magnet synchronous motors.

Torque motors are often built with a rotor molded into a hollow shaft. This

makes the mechanical connection possible for transferring high torque val-

ues. Torque motors can be perfectly adapted to the machine.

Possible areas of application:

Gearless direct drive motors

•

Pressure cylinder axes (precision, zero backlash, stiffness)

•

Eccentric presses

•

Film stretching machines

•

Paper machines

•

Winder drives

•

Figure 17: B&R motors, torque motor in the

background

3.1.7Stepper motor

The stepper motor belongs to the family of synchronous motors. How it

works is very simple. The rotor moves by a minimal degree in a series of steps.

This is done by controlling the rotating electromagnetic field in the stator coil.

A stepper motor usually has a significantly higher number of pole pairs than

a synchronous motor.

Figure 18: Design of a stepper motor (Teravolt /

en.wikipedia.org)

## Page 16

16INTRODUCTION TO MOTION CONTROL TM400

If the motor is operated with an unsuitable load, too much torque or at a

speed that is too high, then the rotor will fall out of sync.

This is known as "step loss". However, it can be prevented by proper drive

sizing.

Intelligent stepper motor controllers are able to detect when steps are

lost. It is also possible to combine them with a position encoder.

Figure 19: B&R stepper motors

In addition, the rotor can be positioned in smaller steps within a full step (microstep mode). Extremely precise posi-

tioning is possible if the drive is dimensioned properly.

Stepper motors are characterized by their long service life, high torque and low cost. Speeds up to 1000 rpm are com-

mon. Stepper motors are usually controlled and positioned without encoder feedback.

Possible areas of application:

Infeed axes

•

Positioning units

•

Hose pumps

•

Slew drives

•

Looms

•

CNC units

•

Dot matrix and ink jet printers

•

Hardware \ Motion control \ Stepper motors

3.2Position encoders

A position encoder is an important part of many drive systems. It makes it possible to accurately determine the posi-

tion and orientation of a mechanical element. The movement speed is then derived from this information. The mea-

sured positioning value frequently has a direct influence on the drive solution to be used.

Encoder systems with varying resolution and modes of operation can be used. The position encoder is often directly

integrated in the motor, but it is also possible to measure position at the load.

Position feedback to the positioning controller may be necessary depending on the task at hand. Position

feedback is often not required, however, if frequency inverters or stepper motors are being used. For one,

this is often a cheaper solution; at the same time, precise positioning and speed can often be sufficiently

achieved without an encoder.

The system of units

The exact position of a motor is the most important bit of information when controlling a positioning process. By

introducing an unambiguous unit system that defines the position zero point and the division of motor revolutions into

a certain number of positioning units, the drive system (and the associated mechanical components of the machine)

can be moved to a defined position.

Doing so allows the positioning application to use physical units when specifying the distance to be

traveled. The path is then not specified in terms of rotor rotations, but rather in tenths of a mm or degree.

## Page 17

COMPONENTS OF A DRIVE SYSTEM17

Overview of different encoder systems and supported devices

This training module describes the most important encoder systems. For a complete overview and list of devices that

support encoders, see the website.

www.br-automation.com Products  Motion control  Overview of encoder systems→→→

3.2.1The position encoder as a measuring device

The position encoder is an important measuring device within a drive configuration. In a positioning system, the po-

sition encoder is used as a measuring tool and takes on multiple roles.

The position encoder provides the drive controller with information about the

current position and speed of the motor. The stator field on the electric motor

is systematically controlled by the servo drive (electronic commutation). This

control makes it possible to put the motor's rotor in a defined position or to

dynamically put it in motion.

This allows the drive controller's internal control to react to deviations in the

drive from the predefined positioning sequence (position setpoint, speed

setpoint).

A drive controller must also be able to accurately determine the current posi-

tion of the motor's rotor within a rotation so that it can be activated at the

correct position.Figure 20: Measurable variables through encoder

systems

This is why the position encoder for servo motors ( synchronous motors→

controlled by servo drives) is usually connected directly to the drive shaft in

the motor's housing.

Resolution and degree of accuracy are important criteria when selecting an

encoder type. Both factors affect the overall quality of the control task. The

resolution indicates the physically distinct measured values of the encoder

within one encoder revolution (number of lines).

Figure 21: Integrated position encoder

Better encoders will feature more distinct measured values or lines within a rotation. The smallest measurable position

difference within a rotation is also tied to this. Resolution affects the measurability of very small position changes

and deviations. The degree of accuracy of an encoder describes the smallest positioning distance that can be distin-

guished. It is indicated in seconds or minutes of arc.

In the next few sections, we will take a closer look at the following encoder systems:

Optical incremental encoder

•

Inductive absolute encoder - resolver

•

Optical absolute encoder

•

Absolute encoder - EnDat

•

Absolute encoder for functional safety

•

Synchronous Serial Interface - SSI

•

BiSS interface

•

HIPERFACE motor feedback system

•

3.2.2Optical incremental encoder

An optical incremental encoder has an opaque glass disc with a vapor-deposited digital bar code. Its properties include

change of rotation, velocity profile and high-precision position detection. In order to determine the position, a homing

procedure must always be carried out.

## Page 18

18INTRODUCTION TO MOTION CONTROL TM400

Figure 22: Optical incremental encoder

This superimposition results in the processing of a sine signal or a cosine sig-

nal shifted 90° to the rectangular encoder signals.

These are used by the evaluation electronics to generate the position counter

state.

The direction of rotation can be determined by analyzing the sequence of

falling and rising edges of the encoder tracks.

Figure 23: Signal evaluation

When using an incremental encoder, the position of the mechanics ( encoder position) cannot be concluded right→

away as this is not covered by the encoder information. The only position information that is detected is whether an

"increment" is taking place in the positive or negative direction. Because of this, the position of the encoder within a

rotation cannot be determined.

An additional reference track that is executed provides a basis to help determine the position within a rotation. A

homing procedure also has to be carried out in order to create a relationship between the counter and the current

position.

The resolution of the incremental encoder depends on the number of lines,

the type of evaluation and the maximum input frequency of the processing

logic.

The optical incremental encoder has a very high resolution (several million

increments possible per rotation) and is distinguished by its ability to carry

out high-speed evaluation.

This is certainly an advantage for controlling the servo drive (speed, position,

etc.). Information about a deviation of the current values from the setpoints

is available very quickly on the drive controller. Reactions are possible with

minimum delay time.

Figure 24: An incremental encoder in the lab

(Tycho / de.wikipedia.org)

## Page 19

COMPONENTS OF A DRIVE SYSTEM19

Before a positioning procedure can begin, a homing procedure must be performed to initialize the posi-

tioning system. In most cases, the mechanical system is brought to a defined position, for example by

approaching a fixed reference or limit switch.

Figure 25: Homing procedure

The current position is then assigned a defined value (for software-based positioning). From this point

on, the drive system is effectively equipped with the information about where the mechanics are located.

Positioning can now be started.

The homing procedure can be omitted if only the speed needs to be determined. This is because only the

counted lines per unit of time are relevant.

3.2.3Inductive absolute encoder - resolver

Military technology paved the way for a very robust encoder with a simple construction. These characteristics corre-

spond to those of a resolver. A resolver works according to the principles of a rotary transformer. In a rotary trans-

former, the rotor consists of a coil (winding), which together with the stator winding makes up a transformer. The

resolver is essentially built the same way, with the difference that the stator is made up of two windings offset from

each other by 90° instead of just one.

Figure 26: Resolver design and measurement signal

## Page 20

20INTRODUCTION TO MOTION CONTROL TM400

The signal is generated by feeding a sine signal with a constant frequency to the rotor coil (S3). This uses the basic

principles of transformers to transfer the voltage signals S1 and S2 to the 90° offset stator coils.

The waveforms for S1 and S2 (shown above) result when the rotor is moving. The envelope curves for these signals

depict two sine waves offset by 90°. The processing logic uses this information to determine the position.

If the range of movement for the axis is within one encoder revolution, then a unique position can be assigned to each

encoder value and homing is not necessary. This is what defines an absolute encoder.

On a resolver, the position information is repeated with each new rota-

tion. For example, if you were to disable the drive system and manually

turn the motor shaft 360°, the analyzing system would not be able to de-

tect this manipulation.

If the motor's range of movement goes beyond this one particular rota-

tion, then a homing procedure must be performed.

The resolution of the resolver depends on the processing logic and the

frequency of the supply to the rotor coil (4096 to 16384 increments).

Figure 27: Dismantled resolver

A certain amount of time passes before the processing logic sends the value corresponding to the current position,

resulting in additional dead time for the control loop.

3.2.4Optical absolute encoder

With absolute encoders, a unique value is assigned to each encoder po-

sition. The resolution of an encoder revolution is dictated by a bit-coded

optical encoder disc.

Figure 28: Bit-coded encoder disc

Encoder discs are designed to work with either binary or Gray code.

The position is specified as a bit combination, with each bit corresponding

to a track on the disc. The signal for the processing logic can be transferred

via SSI (Synchronous Serial Interface - SSI) protocol, for example.

Figure 29: Binary code

Figure 30: Gray code

An optical absolute encoder is similar to a resolver in that a full encoder revolution can be clearly resolved. This is known

as a "single-turn" encoder.

A homing procedure is not necessary for this type of encoder as long as one motor rotation is not exceeded during

positioning. After the system is started, the encoder displays a unique value. This value can then be used to determine

the position of the mechanical gear.

## Page 21

COMPONENTS OF A DRIVE SYSTEM21

An upgrade to the "single-turn" encoder is the "multi-turn" encoder. This type

of encoder includes a counter that records the number of rotations complet-

ed. This information is used to extend the explicitly defined position mea-

surement range to a specific number of rotations (typically 4096).

A homing procedure is only necessary once when using a multi-turn encoder.

As soon as the position offset has been determined once, it is possible to

ascertain the current machine position.

Figure 31: Counter mechanism in a "multi-turn"

encoder

The position offset is the difference between the actual internal encoder position and the machine po-

sition.

If the machine is in the zero position, for example, and the software position value is 56343, then the

position should be referenced to the value zero. In this case, the position offset is 56343.

Figure 32: Encoder offset: Translation of the encoder position to the actual mechanical position

This offset can be used from any position to determine the position of the machine.

The counting mechanism is implemented either with an additional mechanical transformation gearbox or electronic

logic.

3.2.5Absolute encoder - EnDat

The EnDat position encoder ("codera") combines the two optical encoder types – incremental and absolute –1EnDat

making it possible to take advantage of both technologies.

1The EnDat interface is a digital bidirectional interface developed by HEIDENHAIN for position encoders.

## Page 22

22INTRODUCTION TO MOTION CONTROL TM400

Figure 33: EnDat design

Advantages:

The advantages of this type of encoder are high-speed signal transfer and high resolution.Incremental encoder:

•

These characteristics represent the ideal conditions for drive control.

There is a constant relationship (offset) between the encoder position and the machine posi-Absolute encoder:

•

tion. The encoder position can be used to figure out the current position of the mechanics ( software position→

for the control program). A homing procedure is not always necessary. In addition, the valid range of movement

for the encoder must be taken into consideration ("single-turn" / "multi-turn").

Embedded parameter chip

The EnDat encoder system includes nonvolatile, maintenance-free data memory to store all of the data required to

operate the drive motor. Values such as motor parameters and the characteristics of the encoder are pre-programmed

in this memory. This data is automatically transferred to the servo drive when the system is started.

3.2.6Absolute encoder for functional safety

In today's motion applications, it is common to put the drive into a mode where speed is limited or safe torque is

applied whenever the safety chain is breached, for example when a protective door is opened. These types of appli-

cations rely on a safe position encoder. For example, the EnDat 2.2 - FS (the FS stands for "functional safety") can be

used to monitor safe positioning and speed.

Additional information regarding safe motion control can be found in the "TM540 – Integrated Safe Mo-

tion Control" training module.

In addition, B&R offers a standard seminar, "Automation Studio Training: Integrated Safe Motion Con-

trol", on this topic.

Hardware \ Motion control \ ACOPOSmulti with SafeMC

Hardware \ Motion control \ ACOPOSmulti with SafeMC SinCos

3.2.7Synchronous Serial Interface - SSI

The Synchronous Serial Interface (SSI) is a way for absolute encoders to transmit data. Because transmission takes

place serially, it is possible to receive absolute information concerning a position. Many different vendors use this

interface.

## Page 23

COMPONENTS OF A DRIVE SYSTEM 23
Features:
Synchronous: Position data is sent synchronously based on a clock signal.
•
Serial: Position data is sent consecutively using a certain baud rate.
•
This type of data transmission is very robust and easy to establish. The data itself is transferred over two wire pairs.
Other advantages include reduced cabling complexity and expense as well as additional shielding against interference
thanks to the twisted pair wiring.
The number of data bits can be configured, with data values being transferred as either binary or Gray code.
Data transfer
The measured value is permanently read by the sensor. When a data value is read, a cycle sequence on the clock line is
output. Each time the sequence edge rises, a data bit is set on the data line. If the last bit has been sent, the sequence
is stopped. Transmission takes place in connection with a defined delay time.
1 2 3 4
Clock
Data Bit n Bit n-1 Bit n-2 Bit 1 Bit 0
Figure 34: Transfer via Synchronous Serial Interface (SSI)
3.2.8 BiSS interface
The BiSS interface (bidirectional / serial / synchronous) is an open source solution. It is based on a protocol for imple-
menting real-time interfaces that can be used to exchange digital data between controllers, sensors and actuators.
The BiSS protocol can be used in industrial applications that require higher transfer speeds and safety.
3.2.9 HIPERFACE motor feedback system
HIPERFACE stands for "HIgh PERformance InterFACE" and is a standard interface for motor feedback systems from
the company SICK STEGMANN. This interface was designed specifically to meet the needs of digital drive control and
provides the user with unified (and simplified) mechanical and electrical interfaces.
Basic features include a combination of incremental and absolute encoder, an embedded parameter chip and the
option of mechanically-assisted multi-turn position acquisition.
3.3 Inverters
A power converter's job is to convert electrical energy from a mains power supply so that it can be used to operate
electric motors.
For polyphase motors, the stator can be adjusted by changing the amount of power supplied to the stator windings.
The alignment and intensity of the magnetic field in the stator result from the respective winding voltages. The speed
and power of the motor can thus be controlled as needed.

## Page 24

24INTRODUCTION TO MOTION CONTROL TM400

The power mains provide single- or

multi-phase AC voltage, for example a

3-phase supply running at 50 Hz.

As you can see in the following im-

age, sinusoidal voltages with a con-

stant frequency and amplitude are sup-

plied; this is referred to as three-phase

alternating current.

Figure 35: Phase shifts in a three-phase power system

An AC motor (and in some cases a synchronous motor) can be operated directly on this power grid. Here, the stator

field of the motor rotates according to the frequency of the supply voltage.

The actual speed of the rotor on an AC motor is set slightly below the synchronous frequency (slip speed

of the motor). The synchronous motor would move exactly with the rotating field (at ideal zero load).

A power converter is now needed to selectively control the characteristics of the stator voltages for positioning. The

power converter can take electrical energy from the mains supply and pass on to the motor the voltage characteristics

required for positioning.

There are two main types:

Variable-frequency transformers

•

Servo drives

•

The following section will break down these power converters into their subparts and examine them more closely.

3.3.1Function principle

The power electronics are basically the same for frequency converters and servo drives.

## Page 25

COMPONENTS OF A DRIVE SYSTEM25

Figure 36: Power converter principle, power electronics

The following components are shown in the diagram above:

Bridge rectifier

•

DC voltage DC bus

•

Power inverter

•

The bridge rectifier takes the sinusoidal AC voltage it receives from the power mains and turns it into DC voltage.

This DC voltage is stored in the DC bus. Here, the DC bus capacitors handle both the storage and stabilization of the

electrical energy. This turns the DC bus into a sort of "energy pool" from which the downstream power inverter draws

energy.

The voltage required to control the motor is clocked from the DC bus voltage. Pulse width modulation (PWM) can be

used to generate highly flexible and dynamic voltage characteristics.

## Page 26

26INTRODUCTION TO MOTION CONTROL TM400

With pulse width modulation, an alternating closing and opening of the voltage valve within a constant

period generates a specific RMS value on the output. The longer the valve is open within a cycle, the larger

the effective output value of this period.

Figure 37: Pulse width modulation principle

The clock frequency is a decisive factor for the quality of RMS value generation.

Other components at a glance

Additional functional units are available on closer examination:

Figure 38: Power converter design

Line filter

A power converter can cause disturbance signals in the mains power supply (for example through the rectifier and

inverter). Line filters are a good way to prevent these disturbances in the mains supply while not influencing other

equipment that is using it.

Feeds energy back to the DC bus

When a motor is being braked, the inverter treats it as a generator. It is able to reconvert the kinetic energy from the

mechanical system to electrical energy. This electrical energy is then absorbed by the DC bus.

## Page 27

COMPONENTS OF A DRIVE SYSTEM27

From there, this energy surplus can be used in the following ways:

Links the DC bus

•

The DC bus circuits of several power converter modules can be connected to each other in parallel. The result is a

common DC bus for the connected drive modules. A drive that has excess energy from a braking procedure makes

this energy available to the other components in the DC bus network. The energy in the system is thus consumed

optimally.

Braking resistor / brake chopper

•

Here, the excessive energy that cannot be absorbed by the DC bus is converted to heat via a braking resistor.

The braking chopper connects the DC bus voltage to the braking resistor. When the maximum braking energy is

reached, the circuit breaker is fully conductive.

Power regeneration

•

The excess energy in the DC bus can be regenerated into the mains power supply. An inverter operating in the

opposite direction handles the corresponding regeneration of voltage to the mains power supply. This optimizes

energy use since excess energy is returned to the mains supply network instead of being converted into heat.

Temperature monitoring

Current thermal relationships within a system are important when operating an inverter. Certain elements become

warm during operation but are not allowed to exceed critical temperatures.

Figure 39: Power converter: Determines the junction temperature; Motor: Handles temperature monitoring

and the temperature model.

The  of the power transistors must be monitored. Since it is not possible to carry out measure-junction temperature

ments directly in the component, a sensor is used to gauge the temperature on the heat sink. The exact construction

of the power transistors is known (thermal transitions). With the measured value, temperature modeling can be used

to determine the actual junction temperature.

When a load is placed on the motor, the . It is possible to get the current temperature valuestator windings heat up

using sensors. In addition, temperature modeling is also used to calculate the winding temperature from the stator

currents. This is a way the system can compensate for the delayed heating of the sensor (thermal inertia). The result

is optimal protection for the motor.

## Page 28

28INTRODUCTION TO MOTION CONTROL TM400

3.3.2Variable-frequency transformers

The "constant-voltage inverter" is the simplest of today's variable-frequency

transformers. The inverter regulates the motor voltage and frequency in a lin-

ear relationship. This results in very weak torque at low speeds.

The speed of the connected motor varies depending on its present load. Com-

pensation can also be carried out without positioning feedback by using cur-

rent measurement to determine the actual load (slip compensation). This

method is sufficient for simple applications with little speed variation and

without heavy starting. It is used predominantly by AC motors.

Figure 40: ACOPOSinverter frequency converter

by B&R

In the classic sense, a frequency inverter is basically a device for setting rotary speed:

Rotating field value generation occurs without reference to the rotor position (no position encoder).

•

The control behavior is rather slow and therefore not tuned to highly dynamic processes.

•

Dimensioning is usually based on nominal power.

•

The properties described are valid for this device group in the classic sense. There are plenty of variable-frequency

transformers on the market that support advanced options such as vector control and encoder feedback. The use of

synchronous motors is also supported depending on the device.

Possible areas of application include:

See areas of application for Asynchronous machine.

•

Winder

•

Cranes

•

Isolated operation without control

•

Pumps, fans

•

Packaging machines

•

Centrifuges

•

Mixers/Stirrers

•

Washing machines

•

Conveyors/Palletizers

•

Hardware \ Motion control \ ACOPOSinverter P74

## Page 29

COMPONENTS OF A DRIVE SYSTEM29

3.3.3Digital servo drives

Digital servo drives can be used to control synchronous motors with integrat-

ed positioning measurement. Here, the motor is not controlled via a prede-

fined speed value; instead, a position setpoint is predefined that the control

loop in the servo system attempts to reach.

If an encoder system is directly integrated in the control loop, it is possible to

maintain an achieved position and check hanging loads, for example.

This is the basis used by compact, powerful algorithms to solve control-re-

lated tasks. The monitoring equipment and services for operating the drive

(application interface) are also managed by this system.

Figure 41: ACOPOSmulti system from B&R

The basic control concept consists of three cascading control loops:

Position controller

•

Velocity controller

•

Current controller

•

A corresponding manipulated variable results from the comparative values

of the control loop. This is converted into the control signals for pulse width

modulation. The integrated position encoder plays an important role here.

it provides the value that defines the current position of the drive (and the

speed derived from it).

This information serves as a comparative value for the respective control

loop. This also illustrates the importance of a high degree of accuracy and

high-speed transfer of this information. The current is also measured at a

high resolution. Intelligent algorithms ensure that measurements are evalu-

ated properly.

Figure 42: Position, velocity, acceleration, time

A servo drive is a positioning device:

Cascaded control loops are employed for closed-loop control.

•

High-resolution encoder systems are incorporated.

•

Dynamic positioning occurs with a high degree of accuracy for target positioning and speed.

•

The position reached is held at a standstill by holding torque.

•

Possible areas of application include:

See "Areas of application" Synchronous machine

•

Packaging machines

•

Materials handling

•

Plastic machines

•

Paper and print processing

•

Textile industry

•

Wood industry

•

Machining centers

•

Variable hydraulic pump control

•

Semiconductor industry

•

CNC applications

•

Robots

•

## Page 30

30 INTRODUCTION TO MOTION CONTROL TM400
Hardware \ Motion control
ACOPOS
•
ACOPOSmulti
•
Decentralized motion control \ ACOPOSremote
•
Decentralized motion control \ ACOPOSmotor
•
ACOPOSmicro
•
3.3.4 Comparison of variable-frequency transformers and servo drives
The following comparison gives an overview of the specific characteristics of variable-frequency transformers and
servo drives. Each system is designed for different purposes and ideally suited for driving the corresponding process,
depending on what it is.
Frequency inverters Servo drives
PWM ground frequency 1 .. 16 kHz 5 .. 20 kHz
Current controller 0.5 .. 2 kHz 16 .. 20 kHz
Velocity controller 4 .. 20 ms 0.2 ms
Position controller Missing Standard
Brake chopper Option, usually short circuit braking Standard
Asynchronous machine Yes Yes
Synchronous machine No / Limited Yes
Overload capability Low High
Highly dynamic movements No Yes
Temperature model No Yes
Power regeneration Unusual Possible
Torque at speed 0 No Yes
Autotuning function Yes Yes
Standalone operation Yes No
Table 1: Comparison of typical characteristics
The boundaries between the main types of devices are fluid. There are also frequency inverters with in-
tegrated position controllers and elements that are typical for servo drives, but this is not the rule.
3.4 Drive mechanics and power transmission
An important aspect of drive technology has to do with power transmission. The forces generated by the motor can
either be transferred to the mechanical process directly or through the use of a gearbox.
The inertia of the load, the torsion of shafts, the slip of belts or the play of gears or spindles all have to be taken into
consideration when dimensioning the gears as well as when designing the drive control application itself.

## Page 31

COMPONENTS OF A DRIVE SYSTEM31

The following types of power transmission will be briefly explained further below:

3.4.2 "Direct drive"

•

3.4.3 "Rotating load"

•

3.4.4 "Gearbox"

•

3.4.5 "Pulleys, belt drives"

•

3.4.6 "Spindle drive"

•

3.4.7 "Rack and pinion"

•

3.4.1General information

When driving machine components (also known as "load"), many forces

occur that result in many different effects. For example, when acceleration

is taking place or being changed dynamically or the speed or direction of

rotation changes, it is necessary to overcome inertia.

In power transmission, high levels of mechanical stress can occur – espe-

cially during accelerating and braking – in the form of torsion or centrifu-

gal force. Sudden load changes ("jerk") place extreme stress on the mate-

rials and reduce the service life of mechanical components – signs of wear

are the result.

Figure 43: Motion profile with velocity, position,

acceleration and time

The energy that is put into a mass to get it to accelerate is exerted to overcome static friction and the inertia of the

mass, but a large majority is also stored as kinetic energy. When braking the load, the same rules apply as for its

acceleration.

Complete mechanical systems also tend to vibrate at certain resonances, which can also affect drive control and overall

process quality.

Before drive components are selected, it is necessary to take a closer look at the mechanics. The components them-

selves must be dimensioned and selected with the mechanical properties in mind. The complete system must be adapt-

ed to the conditions needed to drive the load. (see "Drive sizing and tuning" on page 40)

So what do we have to keep in mind? Here are a few keywords:

Inertia

•

Torque

•

Acceleration

•

Jerk

•

Braking

•

Kinetic energy

•

Centrifugal force

•

Revolution/Translation

•

Slip

•

Dynamics / Motion profile

•

Resonance / Vibration tendency

•

Efficiency/Performance

•

Torsion/Give/Rocking

•

Friction

•

Wear

•

Limit values of the drive components

•

Emergency stop

•

3.4.2Direct drive

A direct drive is referred to when the electric motor is connected directly to the machine.

In this case, special attention needs to be paid to the motor since it must be capable of running at the same speed

as the machine.

The major advantage of this type of drive concept is the absence of a gearbox. See 3.1.6 "Direct drives" on page 12.

## Page 32

32INTRODUCTION TO MOTION CONTROL TM400

3.4.3Rotating load

The rotating load is one of the simplest of mechanical systems. In addi-

tion to the rotating disc itself, there is often an additional load present as

well. As a result, the inertia of the disc is increased, and additional torque

(an imbalance) is generated depending on the angle of inclination and the

angle of rotation of the load. The angle of rotation will determine whether

this torque will be an accelerator or a decelerator. The eccentric load re-

sults in sinusoidal, pulsating additional torque if the speed remains con-

stant.

Figure 44: Rotating load (SERVOsoft / controleng.ca)

Inertia depends on how the mass is distributed as well as the diameter of the load. This is the same as when a figure

skater shifts his or her weight in order to increase or decrease the speed at which the body turns.

Typical applications:

Turntables

•

Pumps, fans

•

Vibrators

•

3.4.4Gearbox

A gearbox is an element that allows the path, speed or acceleration to be

changed. It is usually mounted directly on the drive motor. A gear reduc-

tion results in a reduction of the rotational speed, but it also increases the

torque. People probably talk most frequently about mechanical gears. An

axis coupling that is made by exchanging data over a fieldbus is referred

to as an electronic gear.

Some features or characteristics of a gearbox include things like backlash,

inertia, torsional strength and the specified permissible radial and axial

forces.

Figure 45: B&R planetary gearboxes for direct

mounting on the motor

Gears can be broken down according to their gear ratios:

Gears with the same ratio

•

Gears with different ratios

•

## Page 33

COMPONENTS OF A DRIVE SYSTEM33

Gears with the same linear ratio

With these, power is transmitted linearly.

They include the following types:

Planetary gearbox

•

Spur gear

•

Bevel gears

•

Helical gears

•

Sliding gears

•

Belt and chain gears

•

Figure 46: Spur gear combined with a worm gear

(Glenn McKechnie / de.wikipedia.org)

Gears with different ratios

With these, power is not transmitted linearly. In electric motion control, these types of gears are replaced by electronic

cams (see training module "TM441 - Motion Control: Multi-axis Functions").

They include the following types:

Cam mechanisms

•

Coupled gears

•

Stepping gears

•

3.4.5Pulleys, belt drives

Belt drives are also known as belt transmissions or envelope drives. It is clas-

sified as a traction drive and is used in many different technical areas. In the

early days of industrialization, the belt drive was used as a transmission.

In modern belt drives, V-belts or toothed belts are used. These belts can trans-

fer large amounts of force with only a little amount of tension. In very simple

applications, for example, belts can be used as a clutch.

Figure 47: Belt drive with two taut V-belts

(Borowski / de.wikipedia.org)

Some advantages include:

Runs quietly

•

Shock absorbance

•

No lubrication required

•

Affordable option for bridging large distances

•

Good weight/performance ratio

•

High speeds possible

•

Some disadvantages include:

Limited temperature range

•

Belt stretch, tensioner needed

•

Sensitive to other production equipment

•

Belt slippage

•

High load on the shaft

•

## Page 34

34INTRODUCTION TO MOTION CONTROL TM400

3.4.6Spindle drive

A spindle drive is also referred to as an elevating screw. Here, a rotation is

"translated" into a linear movement.

In the field of machine manufacturing, balls screws (also known as recirculat-

ing ball screws) are used. The ball screw nut grips the linear movement of the

spindle. There are different open and closed nut systems whose basic func-

tion is the same, but whose precision is different.

Figure 48: Ball screw

Typical applications include machine tools that are moved longitudinally or in tool carriages. In automation, complete

units are offered by combining the drive motor, linear guide and elevating screw. A robotic palletizer is a case where

they are used in multiple spatial directions.

Unlike a spindle with trapezoidal thread, it is technically possible to reduce play to just a few µm. Loads of several kN are

possible in addition to movements up to 200 m/min. Heat generated by high stress can cause the spindle to expand.

3.4.7Rack and pinion

A rack can be used to translate a rotation, or vice versa. Racks have been used

in technical equipment throughout history, e.g. cog railroads and retaining

dams have used this form of power transmission.

The way it works is simple. A cog engages the toothed rack and, depending

on the direction of rotation, translates it into a linear movement going the

other way.

Figure 49: Rack and pinion (Dirk Gräfe /

de.wikipedia.org)

They are used in applications such as CD player trays, the rack and pinion steering system in vehicles and in the winches

used to lift and clamp hanging loads.

## Page 35

THE B&R DRIVE SOLUTION35

4The B&R drive solution

The spectrum of B&R products includes all well-known types of drive technol-2

ogy. Depending on the technical requirements at hand, many different types

of drive concepts can be used for automation. The following sections will pro-

vide an overview of the thoroughness of the products in this particular prod-

uct range.

In addition to variable-frequency transformers and servo drives, control de-

vices and motors for stepper motor and DC applications form the basic foun-

dation. Synchronous motors and gearbox options round out the complete

range of products.

Universal connectivity of all components over POWERLINK as well as easy con-

figuration and programming of the drive application makes it possible to flex-

ibly and efficiently set up complex machinery.

Figure 50: Overview of B&R drive technology

4.1Typical topologies

All components contributing to machine control are connected on a single fieldbus network. The controller that exe-

cutes the machine application, the process HMI application and the various drive technologies can be mixed homoge-

neously. Regardless of whether a stepper or servo motor is needed, the drive can always be connected to the other

control components via POWERLINK.

Figure 51: Drive solution connected via POWERLINK

2Visit our website: www.br-automation.com  Products  Motion control→→

## Page 36

36INTRODUCTION TO MOTION CONTROL TM400

Connecting mechatronic units

Large machines are often composed of mechatronic units. A mechatronic unit is a machine component that carries

out a specific function; it consists of drive technology, sensors and actuators.

In the complete machine, it is important that these units are connected to each other properly.

Figure 52: Connecting mechatronic units to the complete machine with POWERLINK

The simplicity of networking within a machine speaks for itself and has significant advantages:

One fieldbus is used for the entire machine: POWERLINK.

•

A central controller networks all remote components.

•

Applications, configurations and data are made accessible from a defined point.

•

Drive parameters and module configurations are loaded directly to the devices when the system is started. This

•

eliminates the need for additional steps when a module has to be replaced.

The controller, drives and I/O points are synchronized via POWERLINK, which makes it very easy to handle cou-

•

pling tasks.

The machine can be divided into mechatronic units that can be flexibly connected to each other when needed

•

without much effort.

All diagnostic data is accessible via the central controller. Extensive information pertaining to the complete sys-

•

tem is collected and furnished to System Diagnostics Manager (SDM).

Communication \ POWERLINK

4.2Product overview

B&R's motion control technology melds seamlessly into its overall automation system – along with machine control,

HMI and safety technology – to create a  for machines and equipment. Our motion controlcomplete system solution

components are fully interoperable, so individual devices can be swapped out at any time to adapt to changes in the

machine's configuration or requirements.

are our guiding principles: Our servo drives and inverter modules are equipped with hardwiredSafety and flexibility

or integrated safety technology, which allows intelligent responses to hazardous situations. In addition to minimizing

downtime, this also boosts machine output substantially. B&R servo technology is perfectly suited to both centralized

and distributed architectures. The portfolio includes a drive solution for any situation, whether inside the control cab-

inet or mounted directly on the machine.

Choose from a broad spectrum of motors, gearboxes and motor-gearbox assemblies. B&R develops its components to

meet the specific needs of both standard and specialty machinery in a  – from metalworkingwide range of industries

to food and beverage. Select from our selected list of standard motors for exceptionally .fast delivery

## Page 37

THE B&R DRIVE SOLUTION37

Figure 53: ACOPOSmulti drive systemFigure 54: Servo motorsFigure 55: Planetary and angular gearboxes

www.br-automation.com  Product  Motion control→→

Automation Studio

Automation Studio combines the planning and design of the logic, HMI, safe-

ty and motion applications. Built-in diagnostics and commissioning interface

facilitates project planning design and helps when commissioning the mo-

tion components. All settings and parameters are stored in the Automation

Studio project. Automation Help contains the full documentation for drive

hardware and software.

Figure 56: Automation Studio: Simple

integration of the drive solution

Hardware \ Motion control

Hardware \ X20 system

Motion control \ mapp Motion

4.3Implementing the motion application

A look at the possible topologies makes it clear how flexible the options are for combining and implementing motion

components. For this, universal software access is provided allowing all different types of drive applications to be

realized.

Basic requirements involving the electrical characteristics of a drive system include simplified speed/position speci-

fications and axis couplings.

This is all possible through the use of an intelligent platform: mapp Motion. All axis movements are coordinated using

function blocks. The underlying layers control the specific characteristics of the different drive systems.PLCopen

## Page 38

38INTRODUCTION TO MOTION CONTROL TM400

Figure 57: One platform: mapp Motion

In this way, the motion tasks can be handled using the same application program, regardless of whether an electric

drive axis with a stepper motor, variable-frequency transformer or servo drive is being used. This also ensures the

seamless integration of safety technology and vision systems.

Fast and easy motion control programming

With mapp Motion, B&R provides a completely new approach to motion control programming. This solution includes

components for controlling single-axis movements as well as CNC and robotics applications. As part of the mapp

Technology software framework, the new mapp Motion components can be combined as needed and are easy to

configure. This makes configuration faster and easier.

A complete package for all applications

B&R provides mapp components for programming single axes (mapp Axis) as well as for programming CNC machines

(mapp CNC) and robotics applications (mapp Robotics). In addition, applications can be programmed using function

blocks PLCopen Part 1 or Part 4. Naturally, mapp Motion can also be used to implement all ACOPOS drive system com-

ponents.

Faster development with mapp Motion

With all mapp Motion components, configuration is faster and easier since basic functions are already implemented in

the components and must only be further configured for the specific application. mapp Motion implements the pro-

gramming interfaces independent of hardware and technology. This makes it possible to reuse the same application

program for recurring positioning sequences or different machine variants – regardless of the drive hardware used.

Easy configuration

B&R also provides Mechatronic designs for common configurations such as standard kinematic chains. For specific

robotics models, the Automation Studio development environment has preconfigured parameter settings, such as

dimensions or dynamic parameters. This minimizes the time needed for configuration.

4.4Selecting the right technology

It is not always easy to keep track of the wide variety of motors and drive types.

The following section therefore provides an overview of motor types, their areas of application and the corresponding

solution from B&R.

## Page 39

THE B&R DRIVE SOLUTION 39
Technology Features and areas of application B&R solution
Low torque, torque at low speeds, good standstill be- 4 ACOPOSmicro
havior, inexpensive, compact (power density), usually
4 X20SM
without an encoder, not very dynamic
Stepper motor 4 X67SM
Variable speed drives, transport of small objects, posi- 4 B&R
tioning, smaller winders Stepper motor
Not typically used in positioning applications, energy ef- 4 X20MM
ficient, maintenance (brushes)
DC motor 4 X67MM
Small auxiliary drives
Inexpensive, robust, maintenance-free, not very effi- 4 ACOPOSinverter
cient, control without encoder possible
Induction motor 4 ACOPOS
4 ACOPOSmulti
Transport, conveyor belts, pumps, fans
High dynamics (low inertia), can be positioned (preci- 4 ACOPOS P3
sion), long service life, more expensive, passively cooled,
4 ACOPOSmicro
high power density
4 ACOPOSmulti
Synchronous motor Positioning applications, dynamic speed changes, start- 4 ACOPOSremote
stop applications 4 ACOPOSmotor
4 8LV, 8LS, 8JS
Motors
High torque, custom design, lower speeds, no gearbox 4 ACOPOS P3
Direct drives (precision, noise, maintenance)
4 ACOPOSmicro
Torque
• 4 ACOPOSmulti
Linear Winders, printing presses, weaving machines, machine
•
tools 4 8LT motors
High performance, high torque, all environments, power 4 ACOPOS P3
density, low velocity, poor energy balance
4 ACOPOSmulti
Hydraulic drive
4 X20
Casting, presses, stamping, printing, metal-cutting ma-
chines 4 X67
Table 2: Motors, properties, areas of application, B&R solution

## Page 40

40INTRODUCTION TO MOTION CONTROL TM400

5Drive sizing and tuning

In order to select the right motion control system, it is necessary to understand the entire drive sequence and the

machine that it will be driving. All components of the system need to be taken into consideration when sizing the drive.

Drive components that are incorrectly sized can lead to big problems; especially if these problems are not discovered

until the machine is being commissioned. For example, it may not be possible to achieve the expected dynamics or

level of efficiency. The quality of the units produced will be affected, and the drive mechanics could become damaged.

The following diagram illustrates all of the components that are important when dimensioning the drive:

The mechanical process

•

Power transmission

•

Motor and system for determining position

•

Inverters

•

Energy supply

•

Figure 58: Simplified illustration of a drive system

Designing a drive system is a repetitive process – the individual steps involved may have to be carried out more than

once depending on the circumstances.

Procedure:

Select the type of drive (linear, rotary, direct, etc.).

•

Select the motor according to the required speed and torque characteristics.

•

Check the thermal capacity of the motor.

•

Select the physical motor options.

•

Select the encoder system.

•

Select the inverter.

•

Check the efficiency of the drive solution and repeat the steps if necessary.

•

In this context, there are additional aspects that must be considered, including both country-specific characteristics

as well as local conditions.

The following questions need answers:

What kind of power grid is available on site?

•

How constant is the power grid?

•

At what elevation will the machine be operated?

•

Are country-specific guidelines and standards being observed?

•

## Page 41

DRIVE SIZING AND TUNING41

5.1Drive sizing with SERVOsoft

To size a drive properly, a number of calculations need to be made that involve how the mechanics are designed. Values

calculated in this way can be used to determine the necessary motor, inverter and subsequently the required power

supply.

SERVOsoft drive sizing software can be used to simplify this process. A free

version of this software is included on the Automation Studio installation

DVD and can be registered free of charge from the manufacturer.3

Figure 59: SERVOsoft logo

Figure 60: SERVOsoft displaying the motion profile for a rack and pinion sequence

The following components can be entered in the full version for calculation:

Up to 20 axes with a common DC bus

•

Rotating and linear drive axes

•

12 different preconfigured drive mechanisms

•

Motors, gearboxes, load couplings, positioning precision

•

B&R components (motors, inverters, gearboxes) selected from the SERVOsoft database

•

Power supply, bleeder and capacitor modules

•

Mass moment of inertia for all components

•

Motion profiles with up to 5,000 segments per axis

•

User-defined reserves for all components

•

3SERVOsoft is a product from the company ControlEng: http://www.controleng.ca/servosoft/.

## Page 42

42 INTRODUCTION TO MOTION CONTROL TM400
The following data can be calculated:
System check whether the drive configuration is realistic
•
Torque, moment of inertia, current
•
Overall efficiency
•
Power for power modules, DC bus
•
Energy costs
•
List of necessary materials
•
Motor and gearbox combinations
•
A selection of sample projects for common drive configurations makes it easy to learn how to develop projects with
SERVOsoft. It also provides a first impression of the importance of drive dimensioning.

## Page 43

DOCUMENTATION AND INSTALLATION43

6Documentation and installation

A B&R user's manual is available for each product group. All product variants are described in the user's manual. There-

fore, it serves as reference documentation for the respective system. It contains all relevant data for installation, com-

missioning and maintenance.

Search within the user's manual

In order to obtain information about a single product variant, it is advisable to search for the model

number in the PDF of the user's manual. This is the quickest way to find the information you need.

Downloadable contents can be found on the B&R website in the download section.

www.br-automation.com Under "Downloads" in the main menu→

The following content can be found in the Downloads section:

Data sheets

■

User's manuals

■

Catalogs

■

Drivers and Updates

■

Tools

■

Certificates

■

And much more

■

The drop-down menu "Product Groups" is available for searching for specific content. When you select a product group,

additional drop-down menus are loaded to limit the content displayed.

The filters to the left of the results allow even more precise filtering.

The content will be downloaded as soon as you click on the download icon to the right of the entry.

Figure 61: To get access to the download section of product "X20CP1586", product group "Control and I/O Systems" must be selected

6.1User's manuals

Drives generally provide a mains connection and connections to the motor. Drives must only be replaced when the

power is turned off.

After replacing the drive, check the grounding of the drive and the cable, the wiring of the digital inputs (emergency

stop, quick stop, trigger, enable, limit switch inputs) and the torque of the fastening screws for ACOPOSmulti modules.

Additional information about this is provided in the respective user's manual.

## Page 44

44INTRODUCTION TO MOTION CONTROL TM400

Figure 64: ACOPOSmotor

Figure 62:

ACOPOS

Figure 63: ACOPOSmulti (in back), ACOPOSmicro (on the left) and

ACOPOSinverter

SystemTitle of user's manual

ACOPOSinverter P74"ACOPOSinverter P74 user's manual"

ACOPOSinverter P84"ACOPOSinverter P84 user's manual"

ACOPOSinverter X64"ACOPOSinverter X64 user's manual"

ACOPOS"ACOPOS user's manual"

ACOPOSmulti"ACOPOSmulti user's manual"

ACOPOSremote"User's manual for decentralized motion control"

ACOPOSmotor"User's manual for decentralized motion control"

ACOPOSmicro"ACOPOSmicro user's manual"

ACOPOSmulti with SafeMOTION"ACOPOSmulti with SafeMOTION user's manual"

ACOPOS P3"ACOPOS P3 user's manual"

Table 3: Overview of motion control user's manuals on the B&R website

User's manuals include danger notices with important information about how to use the respective sys-

tems. The instructions in the user's manuals must be followed.

B&R motors and 3rd-party motors (motors from other manufacturers) are handled entirely differently.

The following applies to B&R motors.

## Page 45

DOCUMENTATION AND INSTALLATION45

Figure 65: 8LSA56.E0060D000-0 -

Synchronous motor, size 5, with EnDat encoder

Figure 66: Premium planetary gearboxes

B&R motor typeTitle of user's manual

Stepper motors"User's manual for stepper motors"

8LVA compact motors"8LVA... data sheet"

8LVB gear motors"8LVB... data sheet"

8LS, 8LSN synchronous mo-"8LS servo motors user's manual"

tors

8JS synchronous motors"8JS data sheet..."

8KS synchronous motors"8KS data sheet..."

8LT torque motors"8LT... data sheet"

8MS synchronous motors"8MS... data sheet"

Planetary gearbox"Overview of motor-gearbox combinations"

ACOPOSmotor"User's manual for decentralized motion control"

Table 4: Overview of user's manuals for motors on the B&R website

6.2Notes regarding grounding and shielding

The following section compiles a few universally applicable recommendations for grounding and shielding. The regula-

tions in the respective user's manual always apply. The procedure for correcting problems on machines where ground-

ing and shielding result in failures should be adapted on a case-by-case basis.

## Page 46

46INTRODUCTION TO MOTION CONTROL TM400

Shielding and grounding for drive technology

To prevent the effects of disturbances, the following cables must be properly shielded:

•

Motor cables

°

Encoder cables

°

Control cables

°

Data lines

°

Inductive switching elements such as contactors or relays must be equipped with corresponding suppressor ele-

•

ments such as varistors, RC elements or damping diodes.

All electrical connections must be kept as short as possible.

•

Cable shields must always be attached to designated shield connection clamps and the connector housing.

•

Twisting the braided shield or extending it with individual conductors is not permitted!

Shielded cables with copper braiding or tinned copper braiding must be used.

•

Unused cable conductors must be grounded on both sides whenever possible.

•

SourceContents

ACOPOSmulti user's manual

Version 1.02 (August 2014)

EMC-compatible installation

Chapter 5 • Wiring

Section 1.1

Section 1.1.4Ground and shield connection diagrams

Table 5: Notes regarding shielding and grounding in the ACOPOS drive system

6.3CAD configurator

In addition to the data sheets, there is also a CAD configurator to assist system designers when integrating B&R motors

and gearboxes. It can be used to configure motors, motor options and gearbox fittings. Design data is available for

download in many formats.

Figure 67: User interface of the CAD configurator

## Page 47

DOCUMENTATION AND INSTALLATION47

www.br-automation.com Products  Motion control  CAD configurator→→→

6.4Speed-torque characteristic curves

Speed-torque characteristic curves are also available in addition to the technical data for the synchronous motors be-

ing offered. Multiple speed-torque characteristic curves can be combined in a diagram by selecting drives with differ-

ent supply voltages. The characteristic curves created in this way can be directly saved as image files.

Figure 68: Speed-torque characteristic curve for motor type 8LSC43.ee030ffgg-3

www.br-automation.com Products  Motion control  Synchronous motors→→→

## Page 48

48INTRODUCTION TO MOTION CONTROL TM400

7Summary

The level of performance exhibited by modern drive systems has improved significantly thanks to technological ad-

vancements in the area of power and signal electronics.

Mechanical, electrical and IT-related components are combined to automate a process. Making sure that this mecha-

tronic system is optimized as much as possible is decisive for overcoming complex requirements.

Figure 69: The B&R motion control product portfolio

Closely coordinating everything to match the requirements of the process starts with selecting the right drive system

components. When doing so, the specific characteristics of system components and their impact on the complete

system are most important.

Basic knowledge of the components, technologies and procedures being used in a system is very helpful for the soft-

ware developer.

This is the foundation necessary to adapt, configure and optimize the mechatronic drive system into a functional unit

that can be used over and over again.

## Page 49

AUTOMATION ACADEMY49

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

## Page 50

50 INTRODUCTION TO MOTION CONTROL TM400

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

V3.0.0.0 ©2023/09/28 by B&R, All rights reserved.

All registered trademarks are the property of their respective owners.

Subject to technical changes without notice.