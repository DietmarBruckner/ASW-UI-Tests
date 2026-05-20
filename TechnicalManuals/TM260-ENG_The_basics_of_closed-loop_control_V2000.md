## Page 1

TM260

The basics of closed-

loop control

## Page 2

2 THE BASICS OF CLOSED-LOOP CONTROL TM260
Prerequisites and requirements
Training modules TM260 - The basics of closed-loop control
Software Automation Studio 4.2 LTS

## Page 3

TABLE OF CONTENTS 3
Table of contents
1 Introduction.........................................................................................................................................................5
1.1 Learning objectives..............................................................................................................................5
1.2 Symbols and safety notices...............................................................................................................5
2 Preface..................................................................................................................................................................6
2.1 Getting started.....................................................................................................................................6
2.2 What is open-loop control?...............................................................................................................6
2.3 What is closed-loop control?.............................................................................................................7
2.4 A person as a closed-loop controller...............................................................................................7
2.5 Closed-loop control in our daily lives..............................................................................................9
3 Basic information.............................................................................................................................................10
3.1 What is a signal?................................................................................................................................10
3.2 What is a system?..............................................................................................................................12
3.3 Block diagram.....................................................................................................................................12
3.4 Open- and closed-loop control.......................................................................................................14
3.5 Standard control loop.......................................................................................................................15
4 System description..........................................................................................................................................17
4.1 Dynamical systems.............................................................................................................................17
4.2 System characteristics......................................................................................................................17
4.3 Description methods........................................................................................................................20
4.4 System response...............................................................................................................................22
4.5 What is an operating point?...........................................................................................................23
4.6 Basic components.............................................................................................................................24
5 System identification......................................................................................................................................32
5.1 What is identification?......................................................................................................................32
5.2 Step response analysis.....................................................................................................................32
6 Simulation..........................................................................................................................................................41
6.1 What is simulation?...........................................................................................................................41
6.2 Model-based development..............................................................................................................43
7 The control loop...............................................................................................................................................48
7.1 Closed-loop control tasks................................................................................................................48
7.2 Criteria for assessing a controlled system..................................................................................48
7.3 Multipoint controller.........................................................................................................................49
7.4 Conventional closed-loop controllers............................................................................................51
7.5 Sampling time.....................................................................................................................................55
7.6 Anti-windup.........................................................................................................................................56
8 Control design..................................................................................................................................................57
8.1 Control design procedure................................................................................................................57
8.2 Control design methods..................................................................................................................57
9 Closed-loop control concepts.......................................................................................................................62
9.1 Single standard control loop...........................................................................................................62
9.2 Pre-filters.............................................................................................................................................62
9.3 Path control.........................................................................................................................................63

## Page 4

4 THE BASICS OF CLOSED-LOOP CONTROL TM260
9.4 Cascade control.................................................................................................................................64
10 Signal filtering................................................................................................................................................66
10.1 Properties and parameters............................................................................................................66
10.2 Filters..................................................................................................................................................68
10.3 Application possibilities.................................................................................................................68
11 Summary...........................................................................................................................................................72

## Page 5

INTRODUCTION5

1Introduction

The demands on automation are increasing with the growing intelligence of machines and production systems due to

networking, computing power and sensors. In order to be able to increase productivity and efficiency, it is necessary to

use the information available to specifically alter and optimize these types of systems. Closed-loop control allows this

information to be processed in the best possible manner and generates added value for the machine and production

system.

B&R offers a wide range of possibilities for carrying out individual closed-loop control solutions. There are several

software libraries available in Automation Studio that contain basic functionality that can be used to master these

types of complex systems.

PackagingFood and beverage

PharmaceuticalsMedicine

Automotive

Energy

Machine manufacturing

Chemicals

Figure 1: Areas of application of closed-loop control.

1.1Learning objectives

This training module provides insight into the basics of closed-loop control.

Participants will learn the basic terminology used to describe closed-loop control systems.

•

Participants will get an overview of dynamical systems and their properties.

•

Participants will learn to describe dynamical systems in the time and spatial domains.

•

Participants will become familiar with the basic components of closed-loop control and the related system re-

•

sponses.

Participants will be able to identify simple dynamical systems using the step response.

•

Participants will gain insight into the topic of simulation.

•

Participants will become familiar with the tasks and properties of closed-loop control.

•

Participants will receive a detailed representation of a PID controller.

•

Participants will be able to apply different controller design methods.

•

Participants will be capable of developing a suitable closed-loop control concept.

•

Participants will get an overview of filters and their application possibilities in a control loop.

•

1.2Symbols and safety notices

Unless otherwise specified, the descriptions of symbols and safety notices listed in "TM210 – Working with Automation

Studio" apply.

## Page 6

6THE BASICS OF CLOSED-LOOP CONTROL TM260

2Preface

This section provides a general overview of the terms open- and closed-loop control and does not go into too much

depth. The main focus is on a person as a closed-loop controller as well as on closed-loop controllers that surround

us in our everyday lives.

2.1Getting started

The first question is always "What is it that has to be controlled?". Only then does the question arise as to how it can

be controlled. The following three terms can help to get a better understanding of what has to be controlled:

Inputs

•

Process

•

Outputs

•

InputsProcessOutputs

Figure 2: Process with inputs and outputs.

Table lamp

If the switch (input) is switched on, the lamp (process) emits light (output).

SwitchLampLight

Figure 3: Table lamp

2.2What is open-loop control?

Open-loop control means only the inputs are taken into consideration when deciding what has to be done.

Features

Information flow is unidirectional.

•

Current output values are not taken into consideration.

•

Open-loop control does not guarantee that the actual result corresponds to what's expected.

•

## Page 7

PREFACE 7
Inputs Process Outputs
Figure 4: Signal flow of a open-loop control.
2.3 What is closed-loop control?
With closed-loop control, the outputs are also taken into consideration in addition to the inputs in order to influence
the process or system.
Features
Information flow is in the form of a loop.
•
Current values of the outputs are considered, which is generally referred to as feedback.
•
Based on the difference between the expected and the actual results, suitable measures can be implemented in
•
order to correct for the discrepancy.
It is possible to react to or compensate for disturbances in the form of external influences.
•
Inputs Process Outputs
Figure 5: Signal flow of the closed-loop control.
2.4 A person as a closed-loop controller
People interact with their environment on a daily basis. This interaction is generally composed of the following phases:
Determining a desired result.
•
Carrying out actions to achieve a goal.
•
Observing the result of these actions.
•
Adjusting the action with regard to the result so that the desired outcome is achieved.
•
P
e
rce
o n p tio
si n
ci
e
D
Action
Figure 6: A person as a closed-loop controller.
As a closed-loop controller, the person is directly in the control loop. Common examples from everyday
life prove that closed-loop control is really encountered on a daily basis.

## Page 8

8THE BASICS OF CLOSED-LOOP CONTROL TM260

2.4.1A person driving

The following image explains the role of a person as a closed-loop controller while driving.

BrainHand and steering wheelCar

Target route

Eye

Actual route

Figure 7: A person as a controller when driving.

Let's imagine that the driver closes his eyes. Instead of closed-loop control there would only be open-loop control. This

would result in an accident. Cars therefore represent closed-loop control and not open-loop control.

## Page 9

PREFACE9

2.4.2Body temperature regulation

Body temperature regulation is explained in the image shown below. The brain receives information about the existing

body temperature from thermoreceptors in the body's core and skin. The body tries to maintain the temperature

setpoint in the body's core for as long as possible. If the actual temperature does not correspond to the temperature

setpoint determined in the brain, then the brain introduces mechanisms via nerves and hormones to dissipate or

generate heat. The best known responses the body has to the task of heat regulation is sweating to cool off and

shivering to warm up.

SkinBrain

Nominal temperature

Heat generation

Heat dissipation

Skin temperature

Receptors

Figure 8: Schematic representation of body temperature regulation.

The human body itself is an interesting collection of several closed-loop controllers. Some examples are cited below. If

we consider the human body, the following systems are continuously measured and kept constant within narrow limits.

Pupil in the eyes

•

Upright movement

•

Level of CO in the blood

2•

Blood sugar

•

2.5Closed-loop control in our daily lives

In everyday life, we are also surrounded by a plethora of technically-based regulated systems.

Indoor heating system.

•

Vehicle design (cruise control, ABS, ESP).

•

Service robots (vacuum cleaning robot, lawn mowing robot).

•

Refrigerators.

•

Coffee machines.

•

Elevators.

•

## Page 10

10 THE BASICS OF CLOSED-LOOP CONTROL TM260
3 Basic information
This section explains the basic terms and introduces elementary descriptions for systems. The knowledge gained will
include an exact explanation of open-loop control, closed-loop control and a standard control loop.
3.1 What is a signal?
Definition: The term signal means the representation of a piece of information that is usually present in the form of
a physical quantity.
Examples of signals are:
Electrical voltage arising between two points.
•
EEG and ECG signals providing information about brain activity and heart function.
•
Signals change their magnitude as a function of time. With regard to their waveform, they can be divided up into
continuous and discrete signals.
Continuous signal
If a signal can change its information value at any time, we are dealing with a continuous signal.
A continuous sinusoidal signal can be represented by the following function:
A : Amplitude
f : Frequency
t : Time
Discrete signal
A discrete signal only changes its value at certain points in time. These times are usually selected as equidistant. In-
formation about the signal values between the specified times is not available.
A discrete sinusoidal signal can be represented in the following form:
A : Amplitude
f : Frequency
n : 1,2,3,...
T s : Sampling time

## Page 11

BASIC INFORMATION 11
Continuous Discrete
y(t) y[n]
t t
Figure 9: Example of a continuous sinusoidal signal. Figure 10: Example of a discrete sinusoidal signal.

## Page 12

12 THE BASICS OF CLOSED-LOOP CONTROL TM260
3.2 What is a system?
Definition: A system is the connection of different components that are integrated together into a whole for the pur-
pose of carrying out certain tasks. The interaction of a system with the system environment occurs via the input and
output signals.
d
u System y
Figure 11: Simple representation of a system term.
The interacting signals have different meanings:
Input signals that should influence the system in a manageable way are called manipulated variables u.
•
Input signals that are not under our control are called disturbance variables d.
•
Output signals y are generated by the system and in turn influence the system environment.
•
Automobile power transmission system
Speed v of the vehicle can be influenced by the gas pedal x and by the gear ratio (choice of gear) ü.
F P
The gas pedal has an effect on the injection and therefore on the motor torque M . This is converted by
M
the gearbox corresponding to the translation ü and led via the axis gear ratio to the wheels. If the wheel
diameter is also considered, then we get the driving force that accelerates the weight of the vehicle. In
addition, there are outer forces generated through e.g. air resistance F L or a road incline α which have an
effect in the direction of vehicle movement or against it. In this use case:
x and ü are the manipulated variables,
• P
• F L and α are the disturbance variables and
v is the output signal.
• F
F α
L
x
p
Vehicle v
F
ü
Figure 12: Representation of a vehicle power transmission system.
3.3 Block diagram
Definition: A block diagram is a graphic representation of the flow of signals and information and, with it, of the
effective relationships of interconnected systems. Every block represents a system. The inputs and outputs are linked
by arrows where the flow of information is only allowed to take place in the direction of the arrow.
The elements of a block diagram are provided below:
Arrows represent values that change over time, i.e. signals.
•
Blocks represent processing units, i.e. dynamical systems.
•
Signals have a unique effective direction that is described by the arrow. The beginning of the arrow describes
•
where the signal begins. The tip of the arrow points to the block in which the signal arises as the cause of other
processes.

## Page 13

BASIC INFORMATION 13
Inputs System Outputs
Figure 13: Block diagram of a system.
Advantages:
By connecting the blocks to the corresponding signals, it is easy to represent whole systems.
•
Simple and clear representation of the system.
•
For drawing up the block diagram, no knowledge of the quantitative relationships that is represented by the indi-
•
vidual elements is necessary.
Important symbols
The following simple symbols are used for functional dependencies that also could be represented by a block.
Summing point: The sign at each arrowhead (summing point) indicates whether that signal is to be added or
•
subtracted. "+" is used if the algebraic sign is left out.
u u u = u - u
1 3 3 1 2
u
2
Figure 14: Summing point.
Branch points: A branch point is a point from which the signal from a block goes concurrently to other blocks or
•
summing points.
u u u = u = u
1 3 1 2 3
u
2
Figure 15: Branch points.

## Page 14

14 THE BASICS OF CLOSED-LOOP CONTROL TM260
The relationships of blocks
Series connection
•
System1 System2
Figure 16: Block diagram of blocks connected in series.
Parallel connection
•
System1
System2
Figure 17: Block diagram of blocks connected in parallel.
3.4 Open- and closed-loop control
3.4.1 Open-loop control
Definition: The simplest way to influence a system is by implementing a controller for the system. Systems in which
the output has no effect on control are called open-loop control systems.
Disturbance variable
Input variable Controller System Output variable
Figure 18: Block diagram of an open-loop control.
Features
Represents an open action flow.
•
No additional sensors required.
•
System stability cannot be influenced.
•
Disturbances in general cannot be compensated
•
Model inaccuracies fully take effect
•

## Page 15

BASIC INFORMATION 15
3.4.2 Closed-loop control
Definition: A system that maintains a prescribed relationship between the output and the reference input by compar-
ing them and using the difference as a means of control is called a feedback control system, and feedback control sys-
tems are often referred to as closed-loop control systems. In this configuration, the controller is then called a closed-
loop controller.
Disturbance variable
Input variable Controller System Output variable
Figure 19: Block diagram of a closed-loop control.
Features
Represents a closed loop.
•
Sensors for measuring the output signals are necessary.
•
System stability can be influenced. A stable system can be destabilized and inverted.
•
Disturbances are also eliminated if they are not measured.
•
Inaccuracies of the controlled system are compensated by closed-loop control. Closed-loop control is robust.
•
3.5 Standard control loop
Probably the most frequent type of control loop is the single standard control loop which is shown in the following
diagram.
d
e u
r R G y
n
Figure 20: Single standard control loop
The most commonly used symbols for describing the signals are:
G: Controlled system: The system to be controlled includes actuator/system/sensor.
•
R: Closed-loop controller, analog or digital implementation of the control rules.
•
r: Reference variable, defines the setpoint of the controlled variable.
•
y: Controlled variable, actual value of the variable to be controlled.
•
e: Control error, deviation between value setpoint and actual value.
•
u: Manipulated variable, controller output, controlled system input.
•
d: Disturbance variable, non-controllable influence on the controlled system.
•
n: Measurement error, inaccuracy or disturbance of the measured values.
•

## Page 16

16 THE BASICS OF CLOSED-LOOP CONTROL TM260
Standard control loop with temperature control
The temperature of the room should comply with the desired temperature as best possible.
Outside temperature
(open windows, doors, etc.)
Temperature
Current
deviation
Set temperature
Heating
Controller y
element
r e u
Figure 21: Standard control loop for temperature control.

## Page 17

SYSTEM DESCRIPTION 17
4 System description
This section considers the properties that systems feature and how they can be described.
4.1 Dynamical systems
Definition: A system is considered to be a dynamic system if the output variables of the system are affected not only
by the input variables at the present time but also by their past values. Physically speaking, the system possesses
states that can either store or dissipate energy. If the output variables depend only on the current value of the input
variables, then the system is considered to be static.
Electrical systems
Two simple electrical systems are considered, specifically an Ohm resistance and an ideal capacitor. The
input is the current and the output the voltage on the component.
i(t) i(t)
R u (t) C u (t)
R C
Figure 22: Static system. Figure 23: Dynamical system.
With resistance R, one can derive that the output variable is uniquely determined at any point in time by
the input variable. This represents a static system.
The voltage on the capacitor on the other hand depends also on the initial voltage on the capacitor. This
represents a dynamical system.
4.2 System characteristics
Linearity
A system is linear if the principle of superposition applies. Hence, for the linear system, the response for several inputs
can be calculated by handling one input at a time and adding the results. For example, when doubling the input variable
the output variable must also be doubled.

## Page 18

18 THE BASICS OF CLOSED-LOOP CONTROL TM260
y y y
x x x
Linear Nonlinear Nonlinear
Figure 24: Output behavior of different systems.
Time invariance
A system is considered to have time invariance if it displays the same behavior at all times for the same input. This
means an input step always leads to the same step response regardless of when the step is applied to the system.
u(t) y(t)
System
t t
t t
Figure 25: Time-invariant system with inputs with different times.
The following equation shows a time-variant system where the output depends directly on the time.

## Page 19

SYSTEM DESCRIPTION 19
Stability
A linear, time-invariant single-input single-output system is called BIBO stable (Bounded Input Bounded Output) if for
ever bounded input, the output is finite.
u(t) y(t)
System
t t
Figure 26: BIBO stability.
In practice, stability can be checked by stimulating the system with an impulse. If the system reaches the initial point
again then the system is stable.
Oscillating
A system is oscillating if the output can oscillate on a constant input. Normally, the oscillation fades away exponentially
and disappears. The following graph shows the step response for three different oscillating systems.
System1
y(t) System2
System3
t
Figure 27: Step responses of oscillating systems.
This training module deals exclusively with linear time-invariant systems (LTI systems).

## Page 20

20 THE BASICS OF CLOSED-LOOP CONTROL TM260
4.3 Description methods
4.3.1 Time domain
In the time domain, systems can be described using differential equations. Basic physical and mathematical knowledge
is required for this. The systematic and analytic approach will be shown using two simple examples.
Electrical system
The following diagram shows an electrical network consisting of a resistance and a capacitor. The input is the voltage
u and the output the voltage on the capacitor u .
C
i
R
u C u
C
Figure 28: RC network.
The component equation of the capacitor is used to calculate the current.
The voltage drop on the resistor is proportional to the current.
By applying Kirchhoff's voltage law, you come to the following equation:
The differential equation results from the implementation of the above component equations.
By solving the differential equation, you receive the description of the time behavior of the output with the initial
condition u (0)=0.
C

## Page 21

SYSTEM DESCRIPTION 21
Mechanical system
The following diagram shows a mass oscillator, consisting of a weight, spring and damper, which is stimulated by an
external force. The input is the external force F, the output is the position x of the mass m.
k d
m
F x, v
Figure 29: Mass oscillator.
The following is a list of all influences that have an effect on the mass:
The spring with the spring constant k generates a force on the body that is proportional to the deflection x
•
(F =Δx·k).
F
Movement of the body is decelerated with a speed-proportional damper with the damper constant d (F =d·v).
• d
The system is deflected by the external force.
•
The inertia force counteracts acceleration (F =m·a).
• T
Balance of forces (in x-direction).
From that you get the equation that the system describes.
The speed is expressed through the derivative of the distance according to the time.
Acceleration is obtained through the derivative of the speed with respect to time.
Implementing this in the force equation results in the following differential equation.
4.3.2 Frequency domain
The Laplace transform method is an operational method that can be used advantageously for solving linear differential
equations. By use of Laplace transforms, many common functions, such as sinusoidal functions, damped sinusoidal
functions, and exponential functions, can be converted into algebraic functions of a complex variable s. Operations
such as differentiation and integration can be replaced by algebraic operations in the complex plane. This is how you
get an algebraic equation which is easier to handle than the original differential equation in the time domain.
A system is often represented in the frequency domain as a transfer function. The transfer function describes the
relationship between the input and output signals of a dynamical system.
Now the transfer functions of the previously addressed examples are derived below.
Electrical example
The differential equation of the RC network is the starting point.

## Page 22

22 THE BASICS OF CLOSED-LOOP CONTROL TM260
U (0)=0 arises in the frequency domain as a result of Laplace transformation:
C
The transfer function can be formed from that by using simple mathematical conversions.
Mechanical example
The differential equation of the mass oscillator serves as the starting point.
x(0)=0 and v(0)=0 arise in the frequency domain as a result of Laplace transformation.
The transfer function can be formed from that by using simple mathematical conversions.
4.4 System response
In order to analyze a system, it is often appropriate to apply a defined input variable (test function) to the system. The
plotted response of the input can be used for analyzing the system.
Step response
The system response is referred to as a step response if you select the unit step as the test function for the system.
A unit step is described as:
u(t) h(t)
System
1
t t
Figure 30: Step response of a system.
Impulse response
The response of the system on the unit impulse (also called Dirac impulse) is called the impulse response.
u(t) g(t)
System
t t
Figure 31: Impulse response of a system.
Mathematically speaking, the impulse is the derivative of the step response.
The unit impulse cannot be implemented because it is theoretically infinitely high and infinitely short.
In practice, it is sufficient to select an impulse that is much quicker than the system to be tested and
attains a higher value.

## Page 23

SYSTEM DESCRIPTION 23
4.5 What is an operating point?
By operating point we generally mean the state of a system with a constant input variable. When stationary, a constant
output appears. This means that the operating point in general does not change or does so only negligibly.
An example of an operating point could be the voltage on the capacitor after it was loaded.
Transition from one operating point to another
If the operating point is varied for dynamical systems, the new operating point does not immediately appear. The
system must first react to the change. This transition describes the dynamic behavior of a system. For example, if the
voltage on the capacitor has to be increased, the capacitor first has to load before the new operating point (voltage
on the capacitor) appears.
u(t) y(t)
u2(t) y2(t)
System
u1(t)
y1(t)
t t
Figure 32: Operating point switchover of a system.

## Page 24

24 THE BASICS OF CLOSED-LOOP CONTROL TM260
4.6 Basic components
This section provides information about the most important basic components in the time and frequency domains
and shows their system responses clearly.
4.6.1 Proportional element (P element)
The output is proportional to the input and is amplified by the factor K .
P
Equation in the time domain
Transfer function
Step and impulse responses
Step response Impulse response
3 3
h(t) h(t)
u(t) u(t)
2.5 2.5
2 2
1.5 1.5
1 1
0.5 0.5
0 0
0 1 2 3 4 5 0 1 2 3 4 5
Time in s Time in s
Figure 33: System responses of the P component with KP = 2.5.
Resistance
A typical example of a proportional element is electrical resistance. The voltage across the resistor
changes in proportion to the current running through it.

## Page 25

SYSTEM DESCRIPTION 25
4.6.2 Integral component (I element)
The output is proportional to the time integral of the input. The integration time constant T corresponds to the time
that is required for the output to reach the value of the input.
Equation in the time domain
Instead of gain factor K, the inverse of the integration time (K = 1/T) is often used.
I I I
Transfer function
Step and impulse responses
Step response Impulse response
5 1.5
h(t) h(t)
4.5
u(t) u(t)
4
3.5
1
3
2.5
2
0.5
1.5
1
0.5
0 0
0 2 4 6 8 10 0 2 4 6 8 10
Time in s Time in s
Figure 34: System responses of an I component with TI = 2.
Fluid container
A typical example of an integral component is a tank which is filled with fluid. The output would be the
fill level of the container.
4.6.3 Differentiator (D element)
The output is proportional to the derivative of the input. The time T corresponds to an amplification of the derivative.
D
Equation in the time domain
Transfer function

## Page 26

26 THE BASICS OF CLOSED-LOOP CONTROL TM260
Pure D elements are not possible in practice. However, they represent a general idealization for the cal-
culation. Real differentiators are possible as a DT1 element or DT2 element.
4.6.4 First-order lag element (PT1 element)
A system that features a proportional transfer behavior with first-order delay is called a PT1 element. The PT1 element
is characterized by a gain factor K by which the input is amplified, and a time constant T that defines the timing.
P 1
Differential equation
If you compare this equation with that of the RC network, you will recognize that the time constant cor-
responds to the product of R and C. The gain of the RC network would be equal to 1.
Transfer function
Step and impulse responses
The step response of a PT1 element is characterized by
no oscillations occurring and
•
the slope at the beginning not being equal to zero.
•
Step response Impulse response
4 3
h(t) h(t)
3.5 u(t) u(t)
2.5
3
2
2.5
2 1.5
1.5
1
1
0.5
0.5
0 0
0 2 4 6 8 10 0 2 4 6 8 10
Time in s Time in s
Figure 35: System responses of a PT1 element with KP = 3 and T1 = 1.3.
RC network
The RC element from the preceding section "Electrical system" on page 20 is a typical example of a PT1
element (often also called first-order low pass in electrical engineering).
Mechatronics \ mapp Control \ mapp Control Tools \ Libraries \ Core \ MTBasics \ Function blocks \
MTBasicsPT1

## Page 27

SYSTEM DESCRIPTION 27
4.6.5 Second-order lag element (PT2 element)
A system that features a proportional transfer behavior with second-order lag is called a PT2 element. The PT2 element
is characterized by a gain factor K by which the input is amplified, a time constant T and a damping coefficient D that
P
describes the overshoot behavior.
Differential equation
Transfer function
For damping D ≥ 1, the PT2 element is not oscillating and can be implemented as a series connection of two PT1 ele-
ments with the time constants T and T .
1 2
Step and impulse responses
Step response Impulse response
2.5 1.5
h(t) h(t)
u(t) u(t)
2
1
1.5
1
0.5
0.5
0 0
0 2 4 6 8 10 0 2 4 6 8 10
Time in s Time in s
Figure 36: System responses of the PT2 element with KP = 2, T1 = 1 and T2 = 1.5.
Mass oscillator
The mass oscillator from the preceding section "Mechanical system" on page 21 is a typical example of
an oscillating PT2 element.
Mechatronics \ mapp Control \ mapp Control Tools \ Libraries \ Core \ MTBasics \ Function blocks \
MTBasicsPT2
Only second-order lag elements that are not oscillating are dealt with in the MTBasics library.

## Page 28

28 THE BASICS OF CLOSED-LOOP CONTROL TM260
4.6.6 First-order differentiator (DT1 element)
A DT1 element represents a differentiator with an additional time delay (see PT1) that "weakens" the differentiating
behavior. The time constant T determines the time delay and K corresponds to the gain of the differentiator.
1 D
Transfer function
Step and impulse responses
Step response Impulse response
2.5 1.5
h(t) h(t)
u(t) u(t)
1
2
0.5
1.5
0
1
-0.5
0.5
-1
0 -1.5
0 2 4 6 8 10 0 2 4 6 8 10
Time in s Time in s
Figure 37: System responses of the DT1 element with KD = 3 and T1 = 1.5.

## Page 29

SYSTEM DESCRIPTION 29
Analog high-pass
For example, high-pass filters for suppressing the offset drift of a drive are used in filter technology. The
simplest implementation of a high-pass filter is an RC high-pass.
u
C
i
C
u
u R R
Figure 38: RC high-pass filter.
By applying Kirchhoff's voltage law, you come to the following equation:
The following arises in the frequency domain as a result of Laplace transformation:
The transfer function can be formed from that by using simple mathematical conversions.
Mechatronics \ mapp Control \ mapp Control Tools \ Libraries \ Core \ MTBasics \ Function blocks \
MTBasicsDT1
4.6.7 Second-order differentiator (DT2 element)
A DT2 element represents a differentiator with an additional time delay (see PT2) that "weakens" the differentiating
behavior. The time constants T and T determine the time delay and K corresponds to the amplification of the dif-
1 2 D
ferentiator.
Transfer function

## Page 30

30 THE BASICS OF CLOSED-LOOP CONTROL TM260
Step and impulse responses
Step response Impulse response
1.2 4.5
h(t) h(t)
4
u(t) u(t)
1 3.5
3
0.8
2.5
2
0.6
1.5
1
0.4
0.5
0.2 0
-0.5
0 -1
0 2 4 6 8 10 0 2 4 6 8 10
Time in s Time in s
Figure 39: System responses of the DT2 element with KD = 2, T1 = 1 and T2 = 0.5.
Mechatronics \ mapp Control \ mapp Control Tools \ Libraries \ Core \ MTBasics \ Function blocks \
MTBasicsDT2
4.6.8 Dead time
The dead-time element is a system whose output variable is equal to the time-shifted input variable. Dead time T
t
describes by how much the output is shifted.
Equation in the time domain
Transfer function
Step response

## Page 31

SYSTEM DESCRIPTION 31
Step response Impulse response
1.2 1.5
h(t) h(t)
u(t) u(t)
1
0.8 1
0.6
0.4 0.5
0.2
0 0
0 2 4 6 8 10 0 2 4 6 8 10
Time in s Time in s
Figure 40: System responses of the dead-time element with Tt = 3.5.
Conveyor belt
A typical example of a dead time is a conveyor belt. The transported good at the start of the belt only
arrives at the end after a certain time (dead time).
Flow quantity intake
Collector
Figure 41: Conveyor belt as dead-time element. T = L / V, L = length, V = velocity
Mechatronics \ mapp Control \ mapp Control Tools \ Libraries \ Core \ MTBasics \ Function blocks \
MTBasicsTimeDelay

## Page 32

32 THE BASICS OF CLOSED-LOOP CONTROL TM260
5 System identification
This chapter deals with the identification of linear systems. After a short explanation of the term "identification", some
systems that frequently appear in practice will be identified using step response analysis.
5.1 What is identification?
"Identification" means determining a model for an unknown system. This occurs by measuring the relationships be-
tween the input and output variables. Doing so determines a model of the system which can be used for subsequent
control design. It should be pointed out that this only identifies the input/output behavior and does not, however,
receive information about the internal relationships. In general, a distinction is made between parametric and non-
parametric identification.
The objective of non-parametric identification is, for example, directly determining the pulse response using input and
output variable measurements. In the process no model structure is set as a basis.
For parametric identification, a model with a defined number of parameters is taken as a basis. These parameters are
then determined in the framework of the identification task so that the model coincides in the best manner possible
with the system that has to be identified.
This training manual only deals with step response analysis, which represents a simple parametric identification
method.
Disturbance
Measured signal
Test Signal System
Model output
Model
Deviation
Algorithm for
Criteria
parameter estimation
Figure 42: Flowchart of a (parametric) system identification.
5.2 Step response analysis
Procedures based on the step response can be used to identify simple models. In the process, the step response is
plotted and the model parameters are selected so that the step response of the identified model best matches that
of the measurement.

## Page 33

SYSTEM IDENTIFICATION 33
Procedure:
1) Apply a step with the step height r to the system and plot the step response.
2) Determine a model using the step response that matches its characteristics as best possible. Possible character-
istics include:
Initial gradient
°
Oscillating
°
Stationary behavior
°
3) Calculate the unknown model parameters using the step response.
4) Evaluate the model.
Model approach
°
Model parameters
°
A decision is made using the model evaluation to see if the identified model is just enough or if points 2-4 need to
be repeated.
The following sections show how you can identify a few systems that commonly appear in practice. The four points
that are described above form the basis for system identification.

## Page 34

34 THE BASICS OF CLOSED-LOOP CONTROL TM260
5.2.1 First-order systems
1) The step response of an unknown system.
t
Figure 43: Step response.
2) The following properties can be read from the step response:
There is an initial gradient to the time t=0 available.
°
No overshooting occurs. ˆ
°
A stationary end value appears.
°
As a consequence of this, a PT1 element with the following transfer function is selected:
3) Determining the unknown model parameters K and T:
P 1
The gain K can be calculated by reading the stationary value h after the step. The calculation rule for the
° P ∞
gain is:
The time constant T of the PT1 elements can be determined in two ways:
° 1
a) The symbolic solution of the step response in the time domain is used to calculate the time constant T.
1
Let's consider the step response to the time t=T. The following applies:
1
This means T can be read directly at 63% of the stationary limit.
1
b) By setting up the tangent at the beginning of the step response and determining the intersection with the
stationary limit.

## Page 35

SYSTEM IDENTIFICATION 35
h∞
63% h∞
r
t
T
1
Figure 44: Graphic calculation of parameters. KP = 4 and T1 = 0.5.
5.2.2 Integral component with delay time
1) The step response of an unknown system.
3 u(t)
h(t)
2.5
2
1.5
1
0.5
0 t
0 5 10
Figure 45: Step response.
2) The following properties can be read from the step response:
No initial gradient is available.
°
No overshooting occurs.
°
The output rises on an input step in a linear fashion (integrating behavior).
°
A suitable approach for the unknown system is an IT1 element with the following transfer function:
An IT1 element arises due to the series connection of an integral component and a PT1 element.
K 1
I
s 1+T s
1
Figure 46: Interconnection of an IT1 element.
3) Determining the unknown parameters K and T:
I 1
If you lengthen the linear slope and it then intersects the time axis, you get the time constant T of the PT1 el-
° 1
ement.
The pitch of the linear slope goes into the calculation of the integration constants K.
° I

## Page 36

36 THE BASICS OF CLOSED-LOOP CONTROL TM260
3 u(t)
h(t)
2.5
2
Δh
1.5
Δt
1
0.5 r
0 t
0 5 10
T
1
Figure 47: Labeling with KI = 0.22 and T1 = 0.8.

## Page 37

SYSTEM IDENTIFICATION 37
5.2.3 Higher-order systems
1) The step response of an unknown system.
1.2 u(t)
h(t)
1
0.8
0.6
0.4
0.2
0 t
0 10 20 30
Figure 48: Step response.
2) The following properties can be read from the step response:
There is no initial gradient to the time t=0 available.
°
No overshooting occurs.
°
A stationary end value appears.
°
A simple approach for the unknown system is a PT1 element in combination with a dead time T.
t
V
-sT
e t
1+Ts
Figure 49: Interconnection of a PT1 and dead-time element.
This method is also used for higher-order systems (PTn). It is often sufficient to approximate a system
using a PT1 element with dead time. Of particular interest is the dynamic behavior which in general
is well depicted by the characteristic of the turning point or inflection tangent.
3) Determining the unknown parameters K , T and T.
P 1 t
The gain K can be determined by reading the stationary value after the step. In addition, the stationary value
° P
h must only be divided by the height of the input step r.
∞
Determine the turning point and map the inflection tangent. The time constant T is the time where the in-
° 1
flection tangent intersects with the stationary end value.
The intersection of the inflection tangent with the time axis gives the dead time T.
° t

## Page 38

38 THE BASICS OF CLOSED-LOOP CONTROL TM260
1.2 r u(t)
h(t)
1
0.8
0.6 h∞
0.4
Turning point
0.2
0 t
0 10 20 30
T
T 1
t
Figure 50: Graphic calculation of parameters. KP = 0.8, T1 = 4.8 and Tt = 2.5
1
PTn
PT1 dead me
0.8
0.6
0.4
0.2
0 t
0 10 20 30
Figure 51: The two systems PTn and PT1 with dead time only differ slightly in the step response.

## Page 39

SYSTEM IDENTIFICATION 39
5.2.4 Oscillating second-order systems
1) The step response of an unknown system.
u(t)
4 h(t)
3
2
1
0 t
0 5 10 15
Figure 52: Step response.
2) The following properties can be read from the step response:
There is no initial gradient to the time t=0 available.
°
Oscillations occur that nevertheless subside with time.
°
A stationary end value appears.
°
A simple approach for the unknown system is an oscillating PT2 element with the gain K , time constant T and
P
damping factor D.
3) Determining the unknown parameters K , T, D.
P
The gain K can be determined by reading the stationary value after the step. In addition, the stationary value
° P
h must only be divided by the height of the input step r.
∞
Determine the maximum value h (highest overshoot) and its respective time t.
° 1 1
The time constant T and the damping D can be calculated using the following formulas.
°

## Page 40

40 THE BASICS OF CLOSED-LOOP CONTROL TM260
h u(t)
1
4 h(t)
3
h∞
2
1
0 t
0 5 10 15
t
1
Figure 53: For the graphic calculation of the necessary points.

## Page 41

SIMULATION 41
6 Simulation
This chapter gives an insight into the topic of simulation. A few basic definitions will be explained followed by the
simulation options offered by B&R.
6.1 What is simulation?
Simulation is the realistic recreation of a system with its static and dynamic processes. Knowledge about the system
and its behavior can be acquired using the model. This can then be transferred to reality.
The sharp increase in production requirements, the systems that are getting increasingly complex and the increasing
development of technical products with software all make simulation one of the essential components of the devel-
opment process.
The following diagram shows a simplified procedure for creating a simulation model.
Requirements Result
Compare with
Model Simulation
measurements
Figure 54: Simplified representation of a simulation.
The models from the preceding sections can be used as a simulation model. In doing so, it is not relevant if the system
was modeled with the help of differential equations or simply identified using the step response.
6.1.1 Simulation levels
The image below shows the simulation levels typical of B&R. The lowest level forms the simulation of hardware and
software components. All simulation options are available in Automation Studio. In the higher level, dynamic processes
of machines and systems can be simulated. For this, B&R offers accessibility to the most popular simulation tools on

## Page 42

42THE BASICS OF CLOSED-LOOP CONTROL TM260

the market. The highest level offers the option of simulating complex system processes. Even for process simulation

B&R offers corresponding interfaces for external software in Automation Studio.

Simulation tool

ControllerModel

PLCMachine emulator

Controller

Data processingModel

HMI

Figure 55: Simulation levels.

6.1.2Difference between simulation and real system

A simulation model is essentially a limited image of reality in which the essential properties of the system are consid-

ered for the respective task. At this point it is important to emphasize that no simulation model can recreate reality

exactly. Rather, a simulation model always represents a compromise between model accuracy and model complexity.

In the graph below a measurement is compared to a simulation result. It is clearly visible that the simulation model

does not take any disturbance into consideration which results in minor differences. Usually disturbances are also

considered to investigate its influence on the control loop.

y(t)Real

Simulaon

t

Figure 56: Comparison of the real controlled system with the simulation.

## Page 43

SIMULATION43

6.1.3Level of detail

For the specification of the simulation model, one must select which system characteristics are important and have

to be depicted exactly. This means that the model is only detailed to the extent required to simulate the important

properties as best possible. In the following example of a combustion motor, individual vehicle and road traffic with

several vehicles, the level of detail is illustrated more precisely.

The following processes are important in a combustion engine: the mix ra-Simulation of a combustion engine:

•

tio of the fuel, the injection pressure, the combustion processes, the emissions and the dynamics of the cylinders

and the crankshaft.

The vehicle as an overall model is not interested in the detailed information of the mo-Simulation of a vehicle:

•

tor but merely in its output variables such as torque and performance. Additionally, the dynamics of the power

transmission system, the transverse dynamics and the dynamics of the tires are all essential for a vehicle. The

driver who can actively influence the dynamic behavior of the vehicle via the gas pedal and the steering wheel has

considerable influence.

When simulating road traffic, the details of individual vehicles are unimportant; en-Road traffic simulation:

•

tire vehicle streams are of much more interest. A vehicle is the underlying unit. It's sufficient to specify a certain

speed for the individual vehicles that the motion is simulated with.

Figure 57: Level of detail of models.

6.2Model-based development

The model-based development is based on the principle of simulation. A complex process is first depicted in an ab-

stract, realistic model. Here it is important that the processes that are necessary for and relevant to the development

are contained in the model because further development is based on the simulation model. During the test phase,

continuous improvements due to the test results can be made on the model. This process can be seen in the following

diagram.

## Page 44

44 THE BASICS OF CLOSED-LOOP CONTROL TM260
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
Figure 58: Procedure for model-based development.
The mechatronic libraries of B&R are developed according to this scheme.
External simulation tools can also be used to create the simulation model. For this, B&R offers seamless
integration of the most common software products such as MATLAB/Simulink and MapleSim.

## Page 45

SIMULATION 45
Advantages of model-based development
Model-based development allows an analysis of extremely broad or complicated systems.
•
More precise adjustment for the pre-installation of system parameters.
•
Early detection of errors.
•
Functionality can be tested before commissioning without any danger which considerably reduces the startup
•
time.
Classical development
Mechanics Electronics Software
Project start Project conclusion
Model-based development
Mechanics
Shortened Time-to-Market
Electronics
Software
Project start Project conclusion
Figure 59: Parallel development using simulation.
6.2.1 Software-in-the-loop (SiL)
With the simulation type software-in-the-loop, the developed software as well as a simulation environment are exe-
cuted on the same hardware. In the process the software communicates with the simulation that is also running on
the same processor. When using Automation Studio, only a PC without additional hardware is required to provide SiL
testing.

## Page 46

46THE BASICS OF CLOSED-LOOP CONTROL TM260

Simulation tool

Model

PLC

ModelController

Data processing

HMI

Figure 60: Structure of a software-in-the-loop simulation.

It is also possible to execute the entire simulation on the subsequent target hardware. The controller is already working

in its intended environment. Unlike in reality, it is not connected to any real inputs or outputs.

6.2.2Hardware-in-the-loop (HiL)

Hardware-In-the-loop systems use real-time simulation to depict a controlled system as accurately as possible for a

controller. A HiL system makes all input and output signals available to the controller that it "sees" in the subsequent

real environment. Analog, digital and bus signals between the HiL system and the controller are connected via the I/

O interfaces.

## Page 47

SIMULATION47

Simulation tool

ControllerModel

PLCMachine emulator

Controller

Data processingModel

HMI

Figure 61: Structure of a hardware-in-the-loop simulation.

This method delivers the results nearest to reality and considers the influences of the real hardware.

An emulator is an electronic device that can replicate a system functionally, electrically and mechanically.

## Page 48

48 THE BASICS OF CLOSED-LOOP CONTROL TM260
7 The control loop
This chapter deals with the different controller types with their properties and requirements.
7.1 Closed-loop control tasks
The general structure and properties of closed-loop control were already introduced in chapter 3 "Basic information"
on page 10. This section investigates standard control loops in accordance with the following diagram. Controlled
system G, which is connected to a control loop with controller R that needs to be designed is referred to here. The
quality requirements that need to be met for that are related to the behavior of the control loop which consists of two
input variables reference variable r, disturbance variable d and output variable y.
d
e u
r R G y
n
Figure 62: Single standard control loop
The main tasks of a closed-loop control are listed below:
A closed-loop control should stabilize an unstable process.
•
The controlled variable y must follow a change in reference variable r (setpoint tracking control).
•
The influence of a disturbance d on the controlled variable y must be suppressed (disturbance rejection control).
•
The impact of the measuring noise n on the control response must be kept low.
•
The control loop should be robust. This means the stability of the control loop must be guaranteed despite
•
changing parameters of the controlled system.
7.2 Criteria for assessing a controlled system
When assessing control quality, the characteristic curve of the controlled variable y(t) or the control deviation e(t)
should be considered under the influence of well-defined test signals. As a test signal, a step-like excitation of the
reference variable of the investigated control loop is often used. The criteria on the closed loop can be specified sep-
arately for the tracking and disturbance response.
Response to setpoint changes Disturbance response
y(t) y(t) Ts
Inflection tangent
ymax
ymax
emax e∞
ye
emax
e∞
t
Td t
Tr
Ts
Figure 63: Response of a control loop to the sudden change of Figure 64: Response of a control loop during a sudden disturbance
reference variable r. d.

## Page 49

THE CONTROL LOOP 49
Compensation time T is the time until the controlled variable enters a certain tolerance band (usually +/- 2%) at
• s
the stationary value y without leaving it again.
e
Maximum overshoot e is the magnitude of the maximum control deviation that appears after the first time
• max
the setpoint (100%) is reached.
Delay time T is calculated from the intersection of the inflection tangent of the first rise with the time axis
• d
Rise time T is the time difference between the intersection of the inflection tangent of the first rise with the
• r
time axis and the stationary value y .
e
Steady state error e is the control error after adjustment to the controlled variable.
• ∞
The introduced variables e and T are values for damping. The time T provides information about the system speed.
max s r
Damping and speed taken together is called the dynamics of the control response. The steady state error e charac-
∞
terizes the steady state.
7.3 Multipoint controller
With multipoint controllers, the manipulated variable can only attain a few discrete values (usually two or three). Cor-
respondingly, a distinction is made between a 2-step controller and a 3-step controller. They can additionally be im-
plemented with or without hysteresis.
Properties of multipoint controllers
The manipulated variables are fully utilized.
•
Simple implementation.
•
High energy consumption.
•
Sustained oscillation.
•
Mechatronics \ mapp Control \ mapp Control Tools \ Libraries \ Core \ MTBasics \ Function blocks \
MTBasicsLevelController
7.3.1 2-step controller
The manipulated variable can attain two discrete values where one of them is usually zero. If the control error e is
positive, the 2-step controller switches on (maximum manipulated variable), if it is zero or negative, it switches off
(minimum manipulated variable).
u
u
u
max u
max
y
L
e
e
-y
L
u
min u
min
Figure 65: Characteristic curve of a 2-step controller without
Figure 66: Characteristic curve of a 2-step controller with hysteresis
hysteresis
An improved closed-loop control can be achieved by equipping the 2-step controller with a hysteresis. This lowers the
switching rate and allows the control error to fluctuate in the set band. Due to the constantly oscillating manipulated
variable, a closed-loop control with 2-step controllers gives rise to a sustained oscillation around the setpoint. The
typical area of application of 2-step controllers are, for example, temperature controls on ovens, fridges and irons.

## Page 50

50 THE BASICS OF CLOSED-LOOP CONTROL TM260
An example using a heating system
In the case of boiler water temperature, the burner is switched on if the water temperature drops below the established
setpoint by more than a certain value. The burner keeps running until a certain value that is above the setpoint is
exceeded. The burner is only then switched off.
7.3.2 3-point controller
The 3-point controller has a total of three switching states: U , 0 and U .
min max
If the control error is located in a certain tolerance range (|e| < ε), no manipulated variable is output.
This is how a steady state without sustained oscillation can be reached with the 3-step controller. The main application
area is actuators with motor operation where the direction of rotation can be changed with the 3-step controller.
As with the 2-step controller, the 3-step controller can be executed with hysteresis.
u
u
u
max
u
max
-ε
e -ε
ε e
ε
u
min
u
min
Figure 67: Characteristic curve of a 3-step controller without
hysteresis Figure 68: Characteristic curve of a 3-step controller with hysteresis
Water temperature control example
Two thermometers are set to two different switching values T1, T2 with T1 < T2. If the temperature falls below T1, the
warm water valve is opened. If the temperature rises above T2, the cold water valve is opened For T1 < T < T2 both
valves remain closed (dead zone).

## Page 51

THE CONTROL LOOP 51
7.4 Conventional closed-loop controllers
Corresponding to the actuators dealt with in section 4 "System description" on page 17, the different controllers are
also differentiated according to their timing. The PID controller is used today as one of the most important controllers
in the industrial environment. This is comprised of a P component (proportional), I component (integral) and D com-
ponent (differential).
7.4.1 P component
With the P component, the manipulated variable is proportional to the control deviation.
Equation in the time domain
Transfer function
The P component thus only generates a manipulated variable if a control deviation is present on the input. If a con-
trolled system is controlled with a pure P controller, a steady state error remains. This steady state error can be lowered
by increasing the proportional gain Kp. However, this increase in Kp leads sooner or later to instability of the closed
control loop. This is the main disadvantage of the P controller.
Step response
e(t) u(t)
K *e
p 2
e
2
K *e
p 1
e
1
t t
T T
off off
Properties of the P component
Quick reaction to control deviations or quick rise
•
Never fully compensates (because a manipulated variable is not output when control deviation is missing). That
•
is why a control deviation remains.
Very simple and inexpensive (often only mechanical).
•
Proportional gain Kp as configuration parameter.
•
The Kp unit depends on the control error e(t) = r(t) - y(t) and on the output u(t). If the input is e.g. a position
(mm) and the output of the controller a speed (mm/s), then the unit of the proportional gain is 1/s.
7.4.2 I component
The manipulated variable of the I component is proportional to the temporal integral of the control error.
Equation in the time domain
Transfer function

## Page 52

52 THE BASICS OF CLOSED-LOOP CONTROL TM260
A constant control error e(t) thus leads to a linear slope on the output u(t) of the I component. The integral action time
Ti determines the gradient of the slope. The I component changes the manipulated variable until the control deviation
is eliminated. Due to this characteristic, the I controller for constant disturbances does not leave any steady state error
behind. When the control deviation is eliminated, the accumulated value of the errors stays on the output.
Step response
e(t) uu((tt))
e
2
e
2
e 1 e 1
t tt
T T T
off i off
Figure 69: Input step and step response of an I controller
For example, an integral action time Ti = 2s means that the output value of the I controller u(t) has reached the variable
of the constant input value e(t) after 2 seconds. It is clear from the graph above that the output of the I controller rises
faster the smaller the value of Ti is.
Properties of the I component
Completely compensates for the control error. This means no steady state errors are left.
•
Slow reaction to control deviations.
•
Tends towards overshooting.
•
Decreases the stability behavior of the control loop.
•
Integral action time T as a configuration parameter.
• i
The danger of a "Windup effect (undesired integration) (see Fig. 78 "PI controller with anti-windup." on page
•
56).

## Page 53

THE CONTROL LOOP 53
7.4.3 D component
The D component creates a manipulated variable that is proportional to the derivative of the control error with respect
to time. The manipulated variable therefore disappears with constant control deviation.
Equation in the time domain
Transfer function
Since the D component is not physically possible, it is implemented through a DT1 element. The transfer function of
the real D controller therefore looks like the following:
Step response
u(t)
e(t)
e
1
t
t
T
f
Figure 70: Input step and step response of a real D element (DT1)
Properties of the D component
Quick reaction to changes in the control deviation
•
Does not compensate which is why it can only be used together with other control elements.
•
Improves the stability behavior of the control loop.
•
Derivative time T and filter time constant T as configuration parameters.
• d f
7.4.4 PID controllers
The manipulated variable formed by the PID controller consists of three components (P, I and D) that overlap additively.
Equation in the time domain
Transfer function
Alternative PID controller structure
The following diagram shows two graphical representations of a PID controller. Switching between the two structures
is possible with the specified formulas.

## Page 54

54 THE BASICS OF CLOSED-LOOP CONTROL TM260
Kp
Ki 1
E(s) U(s) ☰ E(s) Kp U(s)
s s · Ti
Kd · s s · Td
Figure 71: Structure design of a PID controller
Step response
u(t)
e(t)
K *e
p
e t
t
T T
i f
Figure 72: Input step and step response of a real PID controller (PID T1 controller).
Properties of the PID controller
The PID controller combines the properties of the PI and PD controllers. The maximum overshoot is lower than
•
with the PD controller and contains no steady state error due to the I component.
Due to the added I component, the compensation time becomes longer than on the PD controller.
•
Unlike the PI controller, it has an improved stability behavior.
•
On account of three changeable parameters, choosing the controller parameters by testing becomes difficult.
•
However, corresponding theory or tuning rules (see "Control design methods" on page 57) facilitate this.
Mechatronics \ mapp Control \ mapp Control Tools \ Libraries \ Core \ MTBasics \ Function blocks \
MTBasicsPID
Selection criteria
In order to solve a closed-loop control task, it should firstly be determined whether it is necessary to use a PID con-
troller or whether simpler controller types are sufficient for solving the task. This selection depends on the quality
requirements and the properties of the controlled system. The following graph is a comparison of P, I, PI, PD and PID
controllers in a control loop with a PT2 element as the controlled system.

## Page 55

THE CONTROL LOOP 55
y(t) P y(t) P
PD PD
PI PI
I I
r(t) PID PID
t
t
Figure 73: Comparison of the controller types Figure 74: Comparison of the controller
with step-like change of the reference variable. types with step-like disturbance.
Below, rough guidelines for controller selection are specified:
1) If an I component is already present in the controlled system, then a P controller is sufficient for preventing a
steady state error. However, if the controlled system shows a proportional behavior, then the controller must
feature an I component.
2) The I controller cannot react quickly to large control deviations. That is why the control loop has a slow transition
behavior.
3) The P and D components accelerate the transition behavior of the control loop because the controller reacts very
fast to changes in the control deviation. However, for large values of T , the control loop tends towards becom-
d
ing unstable. A problem with the derivative term is that it amplifies higher frequency measurements or process-
es noise that can cause large amounts of change in the output. Therefore, the D Component may only be used
with well-filtered measurement values.
7.5 Sampling time
The previously described analog standard controllers are implemented today in the industrial grade version. In doing
so, the calculation functions of the P, I and D behaviors are applied directly from a programmable process computer.
The following diagram shows the principal structure of a digital control loop.
d(t)
u(t)
r(t) Digital controller D/A Actuator System
y(t)
Measuring
A/D
device
Figure 75: Structural design of a digital controller.
Due to the sampling of the measurement signals and the calculation of the manipulated variable for discrete times,
the following rules for selecting the sampling time must be taken into consideration:
If the sampling time T is selected very small in comparison to the time constant of the control system, then the
• s
controller works approximately as a continuous controller.
If the sampling time T selected is greater than the system time constant, then the controller implementation can
• s
produce a clearly degraded controller.
For a good setpoint tracking performance and fast interference suppression, select the sampling time T that is
• s
at least 1/10 of the smallest time constants of the system.

## Page 56

56 THE BASICS OF CLOSED-LOOP CONTROL TM260
7.6 Anti-windup
In practice, the manipulated variable u(t) cannot attain any given value, rather this is limited due to two factors:
1) The output signal of the controller cannot exceed the output range of the digital-analog converter.
2) Every actuator has a limited range, such as a valve that is either completely open or closed.
If the manipulated variable has reached the physical limit, the integration of the controller continues working without
the real manipulated variable increasing. The controlled system is not supplied with the manipulated variable u calcu-
lated by the controller but with a lower u (the maximum manipulated variable that the actuator can perform). If the
max
control error "e" becomes smaller, then for the ramp-down of u(t), an undesired delay of the manipulated variable and
with it the manipulated variable y(t) arises because the integral component of the controller has already integrated
the manipulated variable u(t) very far. The integral component must first lower this high value (the integral component
discharges) until a manipulated variable arises that is smaller than the possible maximum. This undesired high inte-
gration is also called the "windup effect".
This effect can be counteracted with anti-windup measures. In the simplest scenario, the integral component is deac-
tivated when the manipulated variable limits have been reached.
u(t) Umax
r(t) - Kp Ti System
Umin
Limits
-
Δu(t)
Δu ≠ 0
Figure 76: PI controller with anti-windup.

## Page 57

CONTROL DESIGN 57
8 Control design
This section explains the principal procedure for control design and addresses the tuning rules used frequently in
practice.
8.1 Control design procedure
The following decisions have to be made in order to be able to solve a closed-loop control task:
Selecting the control loop structure: The signal connections which have to be produced by the controller have to
•
be determined. This involves selecting the controlled and manipulated variables to be used.
Selecting the controller structure: A decision must be made as to which controller must be used. Guidelines for
•
this were addressed in "PID controllers" on page 53.
Selection of controller parameters: The controller parameters must be selected in such a way that they fulfill the
•
quality requirements set for the control loop. This task is also referred to as the calculation of the control loop.
However, if the desired quality requirements cannot be achieved by just varying the controller parameters, then the
same procedure must be repeated by selecting another control structure. If this path does not provide the desired
result either – perhaps despite multiple changes to the control structure – then the quality requirements have to be
redefined or alternative advanced control methods used.
8.2 Control design methods
With the following experimental control design procedure, controllers can be determined without too much effort. In
doing so, either the closed control loop or the step response can be used.
8.2.1 Heuristic tuning rules
The parameters for the proportional component K , integral component T and derivative component T are preselect-
p i d
ed according to previous experience and then varied.

## Page 58

58 THE BASICS OF CLOSED-LOOP CONTROL TM260
Procedure
1) The procedure starts with a non-critical setting (K small, T = 0, T = 0) and the gain K is slowly increased until
p i d p
the system lightly oscillates.
1.5
r(t)
y(t)
1
0.5
0 t
0 10 20 30 40 50
Figure 77: Step response of a system with Kp small, Ti = 0, Td = 0.
2) After that the T is increased until the system no longer oscillates.
d
1.2 r(t)
y(t)
1
0.8
0.6
0.4
0.2
0 t
0 10 20 30 40 50
Figure 78: Step response of the systems with adjusted Td. (Kp small, Ti = 0, Td > 0)
3) Then the integral component T is increased step-by-step in order to eliminate a steady state error.
i
1.5
r(t)
y(t)
1
0.5
0 t
0 10 20 30 40 50
Figure 79: Step response of the system with additional Ti. (Kp small, Ti > 0, Td > 0)
4) If control is stable, then K and T can be increased again until satisfactory control action is achieved.
p d
Designing a controller according to this method does not always result inn optimal parameters, but it is
an established and practical method to calculate controller parameters.

## Page 59

CONTROL DESIGN 59
8.2.2 Ziegler-Nichols
Another option of setting controllers is provided by the oscillation attempt in accordance with the Ziegler-Nichols
method. This method manages without a controlled system model, however, it can only can be implemented for
processes that can be operated for at least a short time on the stability limit.
Requirements
The controlled system is stable and can be operated intermittently in the marginally stable domain.
Procedure
1) The control loop is closed using a P controller.
2) The controller gain is increased until the controlled variable has stable and consistent oscillations. The controller
gain set with it is called k , the period duration of the overshoot T .
krit krit
150
r(t)
y(t)
100
T
krit
50
0 t
0 10 20 30 40 50 60 70
Figure 80: Step response of a controlled system for determining kkrit and Tkrit.
3) Controller parameters are determined according to the following table.
Controller Gain Integration time Derivative time
P 0.5k - -
krit
PI 0.45k 0.85T -
krit krit
PID 0.6k 0.5T 0.12T
krit krit krit
Mechatronics \ mapp Control \ mapp Control Tools \ Libraries \ Core \ MTBasics \ Function blocks \
MTBasicsOscillationTuning
8.2.3 Chien, Hrones, Reswick
Many controlled systems can be approximately explained by a PTT model see "Higher-order systems" on page 37. The
1 t
three model parameters K , T and T can be read from the measured step response.
p d
For controlled systems of the type described above, numerous tuning rules have been specified in literature. These
rules are based partially on empirical methods and partially on simulation of corresponding models. The most popular
empirical tuning rules by far are those of Chien, Hrones and Reswick.
With the procedure according to Chien, Hrones and Reswick, it is possible to determine the control parameters rela-
tively efficiently for slow controlled systems.
Requirements
The method can be applied for higher-order controlled systems that can be approximately described by a PTT
1 t
model.

## Page 60

60 THE BASICS OF CLOSED-LOOP CONTROL TM260
Procedure
1) Apply a step on the controlled system.
2) Record step response of the controlled system.
3) Determine the unknown parameters K , T and T.
p 1 t
1.2 r r(t)
y(t)
1
0.8
0.6 h∞
Kp =
r
0.4
Turning point
0.2
0 t
0 10 15 20 25 30
Tt
T1
Figure 81: Step response of a controlled system with inflection tangent for determining Kp, T and Td.
4) Determine control parameters according to the following table.
Controller Gain Integration time Derivative time
P (0.3/K ) · (T/T) - -
p 1 t
PI (0.35/K ) · (T/T) 1.2 · T -
p 1 t 1
PID (0.6/K ) · (T/T) T 0.5 · T
p 1 t 1 t
8.2.4 T-sum method
The T-sum procedure is a tuning rule for PID controllers that delivers good results with high reliability. This rule is
particularly valid for controlled systems with S-shaped step responses. The sum of all time constants T is used as a
Σ
parameter for these controlled systems. It is a variable that characterizes the speed of the controlled system.
Requirements
This rule is valid for controlled systems with PTn-Tt characteristics that feature an S-shaped step response. The
system must already be in a stable operating point before a step is output.
1.2 r(t)
y(t)
1
0.8
0.6
0.4
0.2
0 t
0 5 10 15 20 25 30
Figure 82: S-shaped step response of a system.

## Page 61

CONTROL DESIGN 61
Procedure
Apply a step to the controlled system.
•
Record step response of the controlled system.
•
•
Calculate the sum of all time constants in the system and read static controlled system gain for t → ∞.
In order to determine the total time constant T , move a line orthogonally to the time axis until the two areas are
• Σ
equal as shown under Fig. 85 "Determining TΣ". The intersection with the time axis results in the total time con-
stant.
1.2 r(t)
y(t)
1
k
s
A
2
0.6
A = A
0.4 2 1
0.2
A
1
0 t
0 5 10 15 20 25 30
T
∑
Figure 83: Determining TΣ
Determine control parameters according to the following table.
•
Controller Gain Integration time Derivative time
PI (0.5/k ) 0.5 · T -
s Σ
PID 1/k ) 0.667· T 0.167 · T
s Σ Σ
Mechatronics \ mapp Control \ mapp Control Tools \ Libraries \ Core \ MTBasics \ Function blocks \
MTBasicsStepTuning

## Page 62

62 THE BASICS OF CLOSED-LOOP CONTROL TM260
9 Closed-loop control concepts
Below, the single-loop control loop is expanded in order to improve the control quality for different requirements.
9.1 Single standard control loop
Until now only single control loops have been considered where exactly one output variable was present as the mea-
surement value and where the controlled system could only be influenced by exactly one manipulated variable. De-
pending on the quality requirements (see "Closed-loop control tasks" on page 48), the single control loop can be set
up either as setpoint tracking or disturbance rejection control.
d
e u
r R G y
n
Figure 84: Single standard control loop
Setpoint tracking control
For this controller, the manner in which the controlled variable follows changes in the reference value play the decisive
role. The main task of the controller here is to match the controlled variable as fast as possible to the reference variable.
An example of a setpoint tracking control is the pressure control of a press for which the setpoint is a step index profile
with several different pressures.
Disturbance rejection control
The disturbance rejection control must keep the controlled variable under the control of disturbances in a constant
setpoint. The control quality can be assessed based on how well or fast the controller can suppress the disturbance.
An example of a disturbance rejection control is the temperature control of a room where the setpoint is constant
over a long time.
1.06
3.5
Setpoint Setpoint
1.05
Setpoint tracking Setpoint tracking
3 Disturbance rejection 1.04 Disturbance rejection
1.03
2.5
1.02
2 1.01
1
1.5
0.99
1 t t
0 100 200 300 400 500 0 50 100 150 200
Figure 85: Setpoint tracking control and disturbance rejection Figure 86: Setpoint tracking control and disturbance rejection
control compared for a setpoint step change. control in comparison to a step-like disturbance.
9.2 Pre-filters
Using a pre-filter achieves a smoother reference variable which means less overshooting and a lowered manipulated
variable. The behavior of disturbances is not influenced by the pre-filter. In most cases a PT1 element or a ramp gen-
erator is used as a pre-filter.

## Page 63

CLOSED-LOOP CONTROL CONCEPTS 63
r e u
r Filter R G y
Figure 87: Single standard control loop with pre-filter.
Mechatronics \ mapp Control \ mapp Control Tools \ Libraries \ Core \ MTBasics \ Function blocks \
MTBasicsPT1
Mechatronics \ mapp Control \ mapp Control Tools \ Libraries \ Core \ MTBasics \ Function blocks \
MTBasicsLimiter
Standard control loop with and without pre-filter
In this example, a PID controller without a pre-filter that must follow a setpoint step change on the input is implement-
ed. As you can see, a relatively high overshoot arises. By adding a PT1 as a pre-filter, you get the step response of the
PT1 as a reference variable. The controller is now capable of following this reference variable much better and with
low overshooting.
1.2 1.2
1 1
r(t) r(t)
0.8 y(t) 0.8 r (t)
y(t)
0.6 0.6
0.4 0.4
0.2 0.2
0 t 0 t
0 10 20 30 40 50 0 10 20 30 40 50
Figure 88: Response of a control loop without a pre- Figure 89: Response of a control loop with pre-filter
filter to the step-like change of the reference variable. to the step-like change of the reference variable.
9.3 Path control
With path control or trajectory sequential control, the objective is to lead the controlled variable along a specified
trajectory and simultaneously suppress disturbances. The course of the reference variable can be planned in advance
and the respective manipulated variable calculated. This manipulated variable is then applied to the system and leads
it along a predefined output variable. This type of control is also called feed-forward control.
With path control, the idea is to combine the open and closed-loop controls. Feed-forward control leads the controlled
variable along a known trajectory and the controller simultaneously corrects the deviation of the actual trajectory from
the trajectory setpoint.

## Page 64

64 THE BASICS OF CLOSED-LOOP CONTROL TM260
Feed-forward
control
Trajectory planning
r uV
e uR u
R G y
Figure 90: Block diagram of a control loop for path control or trajectory sequential control.
Mechatronics \ mapp Control \ mapp Control Tools \ Libraries \ Core \ MTProfile
9.4 Cascade control
If a system consists of several different subsystems, then the concept of the cascade control can be used. The con-
troller output variable of one controller (setpoint tracking controller) serves as a reference variable for the next control
loop (inner loop controller). This of course requires additional system variables to be measured in addition to actual
controlled variables.
Outer closed-loop Inner closed-loop d1
Controller system
d2
controller controller
r1 u1 y1
r2 R2 R1 G1 G2 y2
Figure 91: Cascaded control loop
Cascade control design
1) In the first step the innermost control loop is designed in such a way that it is considerably faster than the next-
outer control loop. The objective of the design of the inner loop is to compensate the disturbance d as best pos-
1
sible so that it has no considerable influence on the outer loop.
2) Summary of the inner loop is a block with the input variable r, the disturbance input d and the output variable
1 1
y. This block along with G forms the controlled system for the outer controller R .
1 2 2
3) Design of the outer controller. The outer loop is designed in such a way that the controlled variable y has a good
2
setpoint tracking performance with regard to the reference variable r .
2
Advantages
For complicated controlled systems, designing the controller for a single control loop can be difficult or even im-
•
possible. The cascade control offers the option of partitioning the controlled system and designing simple con-
trol loops.
This simplifies the setting of controllers.
•
Disturbances on the inner control loop are compensated for before they have an impact on the outermost one.
•
Good physical interpretation.
•
Feed-forward controls or restrictions of the controlled variable can be very easily accounted for.
•

## Page 65

CLOSED-LOOP CONTROL CONCEPTS65

ACOPOS

The concept of cascade control is commonly used in motion control technology because a motor brings

with it diverse different system dynamics. The electrical subsystem is the fastest. Therefore the inner-

most control loop is the current controller. It sets a certain current that is responsible for the electrical

torque on the mechanical wave. Then comes the speed controller and followed by the outer most posi-

tion control loop.

ACOPOS

Set positionSet speedSet current

Position Speed Current

ControllerControllerController

Act current

Act position

Act position

Figure 92: Cascade control on the ACOPOS drive

## Page 66

66 THE BASICS OF CLOSED-LOOP CONTROL TM260
10 Signal filtering
This chapter deals with the basics of signal filtering. First, important characteristics in the frequency domain will be
explained and subsequently filters with some important application possibilities for closed-loop control will be intro-
duced.
10.1 Properties and parameters
Filters are dynamical systems, as already dealt with in 4.1 "Dynamical systems" on page 17. Some terms are required
for characterizing filters and are therefore explained below.
Frequency response
Frequency response is the relationship between the input signal and output signal in a linear, time-invariant system
with regard to amplitude and phase. The relationship in the frequency plane between inputs and outputs can be spec-
ified using sinusoidal signals as shown below.
u(t) G(jω ) y(t)
u(t) u(t) = u0sin(ωit)
u0(t)
t
φ
y(t) y(t) = yisin(ωit+φi)
yi(t)
t
Figure 93: A linear time-invariant system with a sinusoidal input signal and respective output signal.
It is easy to calculate the frequency response manually by plotting the amplitude change and the phase shift for differ-
ent frequencies ω. If the amplitude (amplitude response) and the phase (phase response) are applied logarithmically,
then this representation is called a Bode plot.

## Page 67

SIGNAL FILTERING 67
10
0
-10
-20
-30
-40
-2 -1 0 1 2
10 10 10 10 10
)Bd(
edutilpmA
0
-20
-40
-60
-80
-100
-2 -1 0 1 2
10 10 10 10 10
Circuit frequency (rad/s)
)eerged(
esahP
Figure 94: Representation of the amplitude response (above) and the phase
response (below) in the logarithmic representation for a first-order delay element.
Cutoff frequency
The frequency where the output signal is weakened or strengthened by half compared to the input signal is known as
a cutoff or limit frequency. In the Bode plot this corresponds to a weakening or gain of 3 dB with regard to first-order
systems. Additionally, the phase reached exactly half of its limit at the cutoff frequency.
Order
The order describes the weakening of frequencies above or below the limit frequency of the filter. Filter with higher
system orders can be implemented easily by implementing a cascaded connection for lower-order filters. Typically,
the transfer function should drop shortly before the cutoff frequency and afterwards pass over from order*20dB per
frequency decade.
0
-50
-100
-150
-200
)Bd(
niaG
0
-180
-360
-540
-2 -1 0 1 2
10 10 10 10 10
)seerged(
esahP
Order
1
2
3
4
5
Angular frequency (rad/s)
Figure 95: Magnitude and phase response of a low-pass filter with different orders.

## Page 68

68 THE BASICS OF CLOSED-LOOP CONTROL TM260
10.2 Filters
Filters can be differentiated in the frequency domain with regard to their properties. The most popular filters will be
explained in the following section.
Filters Description
Low-pass filter The low-pass filter allows low frequencies to pass while suppressing higher
frequencies. This filter is usually used for signal smoothing or as a pre-filter.
High-pass filter A high-pass filter passes signals above the cutoff frequency undampened
but suppresses those below it. A high-pass filter is used to clean up low-fre-
quency interfering signals.
Band-pass filter For this filter, a frequency band can be defined that defines the passband of
the filter. All frequencies outside of this window are suppressed.
Band stop A frequency band that should be suppressed is defined here.
Notch or BiQuadFilter With a notch or BiQuadFilter, certain frequency bands can be suppressed or
strengthened. For example, these filters can be used for suppressing reso-
nant frequencies.
Moving average filter The moving average filter forms the mean value of a signal via a defined win-
dow. This filter can be used for signal smoothing.
The filters mentioned above are available in the MTFilter library. More detailed information about transfer
function, Bode plot, order and interfaces, refer to the Automation Help.
Mechatronics \ mapp Control \ mapp Control Tools \ Libraries \ Core \ MTFilter
10.3 Application possibilities
The following lists a few important applications of filters for closed-loop control. These filters contribute to the control
quality being improved.
Pre-filters
Pre-filters are often used in a control loop in order to improve the setpoint tracking performance of a closed-loop
control.
r e u
r Filter R G y
Figure 96: Control loop with pre-filter
By filtering the setpoints, the positioning behavior is improved while the disturbance response of the control loop
stays the same.

## Page 69

SIGNAL FILTERING 69
1.2
1
0.8
Without a pre-filter
With a pre-filter
0.6
0.4
0.2
0
0 10 20 30 40 50
Figure 97: Step response of a control loop with and without pre-filters.
Mechatronics \ mapp Control \ mapp Control Tools \ Libraries \ Core \ MTBasics \ Function blocks \
MTBasicsPT1
Mechatronics \ mapp Control \ mapp Control Tools \ Libraries \ Core \ MTBasics \ Function blocks \
MTBasicsLimiter
Suppression of resonant frequencies
It's often possible to determine the resonant frequency of a system with more in-depth knowledge of the process
These frequencies can then be suppressed using certain filters.
e u ũ
r R F G y
Figure 98: Control loop with a filter for suppressing resonant frequency.
1.5
Without filter
With filter
1
0.5
0
0 5 10 15
Figure 99: Step response of a control loop with and without filters.
Mechatronics \ mapp Control \ mapp Control Tools \ Libraries \ Core \ MTFilter \ Function blocks \
MTFilterNotch

## Page 70

70 THE BASICS OF CLOSED-LOOP CONTROL TM260
Mechatronics \ mapp Control \ mapp Control Tools \ Libraries \ Core \ MTFilter \ Function blocks \
MTFilterBiquad
Reduction of signal disturbance
Filters are often used to clean up measurement signals and remove disturbances.
e u
r R G y
n
Filter
Figure 100: Control loop with a filter for suppressing interfering signals.
1.5
Measurement signal
Filtered measurement signal
1
0.5
0
-0.5
-1
0 2 4 6 8 10 12 14 16
Figure 101: Signal filtering of a noisy measurement signal.
Programming \ Libraries \ Mechatronic libraries \ Basic controller design \ MTFilter \ Function blocks
\ MTFilterLowPass
Derivative estimation of signals
In some cases, the derivative of a measured signal is required for closed-loop control. To calculate the derivative, a
differentiator (First-order differentiator (DT1 element) and Second-order differentiator (DT2 element) can be used.
The disadvantage is in the phase shift that results from the approximate derivative estimation.
Because the derivative strengthens high frequencies, the derived signal could be unusable since it is overcome by the
interfering signal (disturbance).
e u
Speed setpoint R G Actual position
Actual speed
Filter
Figure 102: Control loop with a filter for derivative estimation of signals.

## Page 71

SIGNAL FILTERING 71
1.5
Actual position
Actual speed
1
0.5
0
0 1 2 3 4 5
Figure 103: Determining the actual speed using a derivative estimator.
Mechatronics \ mapp Control \ mapp Control Tools \ Libraries \ Core \ MTBasics \ Function blocks \
MTBasicsDT1
Mechatronics \ mapp Control \ mapp Control Tools \ Libraries \ Core \ MTBasics \ Function blocks \
MTBasicsDT2
Filtering that is too strong leads to a time delay of the measurement signal. If this time delay is too big
then it can make the whole system unstable. It is important to make sure not to filter signals too strongly.

## Page 72

72 THE BASICS OF CLOSED-LOOP CONTROL TM260
11 Summary
In modern machines and production systems, closed-loop control plays an increasingly important role because com-
plex processes only become manageable and deliver the desired result on account of it.
The task of closed-loop control is the physical understanding of a system, the interpretation of the information pro-
vided by sensors and the targeted optimization of the system. These points make it clear that quite a lot of knowledge
and experience is necessary to achieve the desired goals.
Building on a system-theoretical basis, the way dynamical systems can be described and identified with simple meth-
ods is shown.
The significant topic of simulation is dealt with in detail, just like all simulation options with Automation Studio that
are necessary for an efficient development process.
Emphasis is put on the single-loop control loop in combination with the PID controller because a majority of control
problems can be solved with it. However, knowledge about the control loop, the control design procedure and closed-
loop control concepts are essential for effective implementation. They are calculated in an understandable way and
consolidated with numerous practical examples.
The training module "The basics of closed-loop control" provides a solid basis for getting a first look at closed-loop
control.
MTBasics MTData MTFilter MTLookUp MTProfile
Figure 104: B&R offers a comprehensive portfolio of closed-loop control functions.

## Page 73

AUTOMATION ACADEMY73

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

## Page 74

74 THE BASICS OF CLOSED-LOOP CONTROL TM260

## Page 75

AUTOMATION ACADEMY 75

## Page 76

B&R

Industrial Automation GmbH

A member of the ABB Group

B&R Straße 1

5142 Eggelsberg, Austria

office@br-automation.com

t +43 7748 6586-0

f +43 7748 6586-26

br-automation.com

V2.0.0.0 ©2023/09/26 by B&R, All rights reserved.

All registered trademarks are the property of their respective owners.

Subject to technical changes without notice.