## Page 1

TM482

Hydraulic servo pump

drives

## Page 2

2 HYDRAULIC SERVO PUMP DRIVES TM482
Prerequisites and requirements
Training modules TM210 – Working with Automation Studio
TM400 – Introduction to motion control
TM480 – The basics of hydraulics
Software AS 4.1 or higher
Hardware ---

## Page 3

TABLE OF CONTENTS 3
Table of contents
1 Introduction.........................................................................................................................................................4
1.1 Learning objectives..............................................................................................................................4
2 Structure...............................................................................................................................................................5
3 Components........................................................................................................................................................6
3.1 Servo motor and pump.......................................................................................................................6
3.2 ACOPOS..................................................................................................................................................7
3.3 Software.................................................................................................................................................7
4 Sizing....................................................................................................................................................................8
4.1 Servo Pump Sizing Tool......................................................................................................................8
5 Software concept.............................................................................................................................................13
5.1 Closed-loop control concept............................................................................................................13
6 MTHydPump library function blocks............................................................................................................15
6.1 General information...........................................................................................................................15
6.2 MTHydPumpController......................................................................................................................15
6.3 MTHydPumpPowerMeter..................................................................................................................16
6.4 MTHydPumpDriveProtection...........................................................................................................17
6.5 MTHydPumpSimulationModel.........................................................................................................19
6.6 MTHydPumpPressureTuning...........................................................................................................20
6.7 Using function blocks.......................................................................................................................23
7 Technology solution – Hydraulic servo pump control..............................................................................24
7.1 Technology Solution exercise..........................................................................................................25
7.2 ACOPOS drive cutoff protection exercise.....................................................................................25
7.3 Exercise - Speed Torque Chart........................................................................................................26
7.4 Exercise – Power monitoring...........................................................................................................27
7.5 Exercise – Pressure controller tuning............................................................................................28
8 Master/Slave mode.........................................................................................................................................29
8.1 Master-Slave exercise........................................................................................................................29
9 Position control with servo pump drive......................................................................................................31
10 Project development.....................................................................................................................................32
10.1 Project development guidelines....................................................................................................32
10.2 Commissioning instructions..........................................................................................................32
11 Summary...........................................................................................................................................................39

## Page 4

4 HYDRAULIC SERVO PUMP DRIVES TM482
1 Introduction
The TM482 training module deals with controlling hydraulic cylinders using targeted movements of hydraulic pumps.
The pump movements are controlled using an ACOPOS servo drive and a servo motor.
This training will introduce a solution based on the MTHydPump library. This approach will be implemented directly in
Automation Studio and tested using a simulated machine.
1.1 Learning objectives
This training module uses selected examples to help you learn about how hydraulic servo pumps work and the steps
involved in their commissioning.
Participants will learn how a servo pump works and about the components of a hydraulic system controlled by a
•
servo pump.
Participants will learn the parameters needed to size a servo pump drive and understand the underlying control
•
concept.
Participants will learn how to control a servo pump, calculate energy consumption and protect the components
•
from overload.
Participants will learn how to configure a servo pump control solution and put it into operation.
•

## Page 5

STRUCTURE5

2Structure

One of the most important tasks of hybrid drive systems is to move hydraulic axes. The energy source is a pump that

is connected to a variable speed motor. Hydraulic actuators (directional valves) are used to direct the hydraulic fluid

to the necessary axis. Hydraulic axes can be either linear cylinders or hydraulic motors. Another important element

in these systems is the pressure relief valve, which prevents excessive pressure build-up and thus protects against

mechanical damage. System pressure (ActPumpPressure) and motor speed (ActPumpSpeed) are needed as measured

variables.

CylinderArea2CylinderArea1

ActCylinderPressure1

12

ActCylinderPressure2

Velocity

hydraulic

Cylinder

PLC

SetPumpSpeed

SetPumpPressure

switching

Valve

ABK

N

ValveSignal

I

L

R

E

W

PT

O

P

ActPumpPressure

ActPumpSpeed

Figure 1: Hardware concept for a hydraulic servo pump drive with an X20 controller, ACOPOS drive, motor and servo pump

## Page 6

6HYDRAULIC SERVO PUMP DRIVES TM482

3Components

Hydraulic servo pumps consist of the following three elements:

Servo motor and hydraulic pump

•

Servo drive (ACOPOS)

•

Software (MTHydPump)

•

3.1Servo motor and pump

This unit is responsible for converting electrical energy into hydraulic energy. The internal gear pump is attached to a

synchronous motor via a shaft coupling and housing. There is also a hydraulic pump block with a pressure relief valve

and pressure sensor mounted directly on the pump. This equipment is attached to the machine using a mounting

flange.

If a pump block is not installed, the pressure sensor should be mounted as close as possible to the pump.

In addition, it is essential that a pressure relief valve be included in the system.

synchronous

motor with

encoder

coupling

casing

internal gear

pump

mounting

flange

pressure pressure

pump sensorrelief valve

distribution

block

Figure 2: Example of a servo motor / hydraulic pump combination

## Page 7

COMPONENTS7

3.2ACOPOS

Servo drives at B&R are referred to by the names ACOPOS and ACOPOSmulti. This document will treat

ACOPOS as a synonym for B&R servo drives.

ACOPOS drives are used to control the servo motors and therefore the con-

nected pumps as well. This is done by alternately controlling the speed of the

servo motor (and the pump) or the pressure. The speed of the motor is read

using an encoder card (AC12x in the following image). Pressure is read using

an analog input on an AC13x plug-in card.

Figure 3: Plug-in cards required on the ACOPOS

drive

Since the actual control loop runs on the ACOPOS drive, a very high degree of control quality can be

achieved regardless of the performance of the CPU.

3.3Software

Automation Studio is used to develop the software. The MTHydPumpController function block is primarily used as the

main interface and for operation since the control loop itself is running on the ACOPOS drive for performance reasons.

The MTHydPump library is available as an upgrade in Automation Studio 4.1 and is an integral part of

Automation Studio in V4.2 and later. This library requires a license and utilizes B&R Technology Guarding

technology.

MTHydPump and the ACP10 ACOPOS firmware are dependent on one another. A version of the MTHyd-

Pump library is only compatible with a certain ACP10 version. There is a new MTHydPump version for

every new ACP10 version.

## Page 8

8 HYDRAULIC SERVO PUMP DRIVES TM482
4 Sizing
An important aspect of the hydraulic servo pump is the correct sizing of the equipment. It is not necessary to be
familiar with all equations and relationships between the servo motor, servo drive and pump. It is, however, important
to be able to estimate whether the requirements can be met. There are easy standard formulas for this estimate as
well as a tool for accurate sizing under consideration of all of the parameters in play.
ID Unit Description
n rpm Speed
Q l/min Flow rate
V cm³ Displacement volume
s
η Volumetric efficiency
vol
η Hydromechanical efficiency
hm
M Nm Torque
p bar Pressure
I A Current
k Nm/A Torque constant
t
Table 1: Sizing parameters
To determine the required motor speed for a desired volumetric flow rate, it is necessary to know the volumetric flow
rate, the pump size and the volumetric efficiency of the pump (where applicable). In addition, the maximum and aver-
age motor speed are critical values for motor sizing.
Another important value for sizing is the torque required to generate a certain pressure. The torque can be easily
determined using the relationship between displacement volume and pressure.
The current required at various operating points is also critical when sizing the ACOPOS drive. It can be determined
using the motor's torque constant.
These three relationships can be used to quickly determine if certain operating states are possible with the given
hardware. More precise sizing is possible using B&R's "Servo Pump Sizing Tool" for servo hydraulic systems.
4.1 Servo Pump Sizing Tool
B&R's Servo Pump Sizing Tool is used to size pumps, motors and drives. This tool is specially tailored to applications
with hydraulic servo pump drives.
The Servo Pump Sizing Tool is included on the Automation Studio DVD starting with V4.2.2.
4.1.1 Sizing with the Servo Pump Sizing Tool
Sizing is done using a defined machine cycle with the necessary pressure and volumetric flow rate.

## Page 9

SIZING9

Figure 4: Example of a machine cycle with pressure and volumetric flow rate.

4.1.1.1Components to be sized

Internal gear pump

Default: Pumps are available from a hydraulic partner.

■

Other pumps can simply be added to the .csv file.

■

Synchronous motor

Default: All relevant B&R motors are available with the usual configurations.

■

Other motors can simply be added to the .csv file.

■

ACOPOS servo drives

Default: All B&R ACOPOS and ACOPOSmulti drives are available.

■

## Page 10

10HYDRAULIC SERVO PUMP DRIVES TM482

4.1.1.2Sizing procedure

1)Define a name for the sizing configuration and enter the mains voltage, mains frequency and ambient tempera-

ture.

2)Enter the machine cycle data including the necessary pressure and volumetric flow rate directly or import it as

a .csv file.

3)Select the pump

4)Select the motor

5)Select the drive

6)Select Start Evaluation

Figure 5: Servo Pump Sizing Tool

Result

After the evaluation has been started, a concise report is shown with a general overview of the sizing configuration. It

is clear here at first glance whether or not the sizing meets the respective requirements.

Figure 6: Sizing results report

## Page 11

SIZING11

Clicking on Show Results displays more detailed information. Sizing information is provided for every portion of the

machine cycle. In addition, it is also possible to click on Show Diagrams to view a graph that traces motor temperature

and the current values.

Figure 7: Sizing results for the individual portions of theFigure 8: Sizing diagrams

machine cycle.

4.1.2Additional options

Operation with 2 pumps, 1 motor and 1 ACOPOS servo drive

■

Figure 9: Hydraulic system with 2 pumps, 1 motor and 1 ACOPOS drive

Field weakening

■

Machine cycle defined via the torque and speed.

■

Sizing configuration without a pump. Sizing configuration for applications with only a motor and ACOPOS drive.

■

Creating a PDF report that documents the entire sizing configuration.

■

4.1.3Exercise 1 - Servo Pump Sizing Tool

In this exercise, the Servo Pump Sizing Tool will be looked at in more detail.

## Page 12

12HYDRAULIC SERVO PUMP DRIVES TM482

a)Open the Servo Pump Sizing Tool

b)Import the machine cycle example (example_export_import_cycle.csv)

c)Complete sizing operation

d)Create PDF report

e)Analyze and discuss the various results

4.1.4Exercise 2 - Servo Pump Sizing Tool

In this exercise, the Servo Pump Sizing Tool will be looked at in more detail.

a)Open the Servo Pump Sizing Tool

b)Sizing for the following machine cycle is needed.

Figure 10: Machine cycle example

c)Complete sizing operation

d)Create PDF report

e)Analyze and discuss the various results

## Page 13

SOFTWARE CONCEPT13

5Software concept

For typical servo pump applications, pressure changes in a matter of milliseconds; it is therefore necessary to run the

control loop with a very fast cycle time. It is also important that the values for the current pressure and current speed

be made available with the least possible amount of delay. This is why the position controller has been replaced by a

pressure controller (PQCTRL) on the ACOPOS drive. With this approach, the control loop directly on the ACOPOS servo

drive is calculated using a 400 µs cycle.

To make things as easy as possible, configuration and setpoint specification are handled exclusively on the controller

using the MTHydPumpController function block.

Figure 11: Overall approach to closed-loop servo pump control

POWERLINK and CAN are available as interfaces between the ACOPOS drive and controller. Setpoints and actual values

for speed and pressure are written to and received from the ACOPOS drive cyclically. The function block can implement

pre-filtering before the setpoints are sent to the ACOPOS drive.

5.1Closed-loop control concept

The actual control loop for the servo pump is an alternating pressure/speed controller. There are therefore two set-

points – pressure and speed. A setpoint for the flow rate in l/min can be converted directly into a speed setpoint (see

"Sizing" section). The existing ACOPOS speed controller is used here. The PQCTRL function block is used as the pres-

## Page 14

14HYDRAULIC SERVO PUMP DRIVES TM482

sure controller to replace the position controller. A speed is provided on the output of the PQCTRL function block in

order to control the pressure setpoint or set the speed setpoint to nSet. The following block diagram shows the con-

trol concept for the pressure/speed controller on the ACOPOS drive.

Figure 12: PQCTRL control concept on the ACOPOS drive

## Page 15

MTHYDPUMP LIBRARY FUNCTION BLOCKS 15
6 MTHydPump library function blocks
6.1 General information
The MTHydPump library includes function blocks for controlling and monitoring a servo pump. An ACOPOS drive that
is connected to a controller via POWERLINK is required here.
Each version of the MTHydPump library is only compatible with a certain version of the ACP10_MC library.
6.2 MTHydPumpController
The MTHydPumpController function block is used to control servo pumps. Similar to a PLCopen function block, it func-
tions as an interface to the ACOPOS drive.
MTHydPumpController
UDINT Axis Busy BOOL
UDINT MasterAxis Active BOOL
BOOL Enable Error BOOL
MTHydPumpControllerConfigType Configuration StatusID DINT
MTHydPumpControllerParType Parameters UpdateDone BOOL
BOOL Update
REAL SetPumpPressure ActPumpPressure REAL
REAL SetPumpSpeed ActPumpSpeed REAL
MTHydPumpControllerModeEnum ControllerMode
USINT SelectControllerParSet SelectedControllerParSet USINT
InPressureControl BOOL
InSpeedControl BOOL
InMinimumPressureControl BOOL
BiQuadFilterActive BOOL
Figure 13: The MHTHydPumpController function block
Programming \ Libraries \ Mechatronic libraries \ Hydraulics \ MTHydPump \ Function blocks\ MTHyd-
PumpController
It is necessary to bring the ACOPOS drive to a standstill (PLCopen state) and switch on the control structure on the
ACOPOS drive. This is done using the PLCopen function block MC_Power. Function block MTHydPumpController is
only fully functional in the Standstill axis state! After the MTHydPumpController function block has switched off the
position controller, the user is not permitted to switch it back on. Axis handling is therefore a prerequisite for the
MTHydPumpController function block.

## Page 16

16HYDRAULIC SERVO PUMP DRIVES TM482

Figure 14: Process from the perspective of PLCopen

The pressure/speed controller on the ACOPOS drive is created and configured once the MTHydPumpController func-

tion block is enabled. It then goes into a cyclic state where the setpoints and actual values are transferred cyclically.

When changes occur within the parameter structure, a rising edge on the Update input is needed to transfer the values

acyclically to the ACOPOS drive.

The system is configured using the two input structures Configuration and Parameters. This structures contain gen-

eral parameters and limits that can be taken from data sheets as well as parameters that need to be identified during

commissioning.

Parameter structure:

General parameters

•

Sensor scaling

•

Control loop parameter sets (0-9)

•

Parameters that need to be determined

Filter time for the pressure signal Parameters.ActPressureFilterTime

•

Controller parameters Parameters.ControllerParameterSet[ ].PressureController.ProportionalGain, -.Integra-

•

tionTime, -.DerivativeTime, -.FilterTime

Maximum negative speed Parameters.ControllerParameterSet[ ].MaxReversePumpSpeed

•

Acceleration limit Configuration.MaxPumpAcceleration

•

The parameter Configuration.MaxPumpAcceleration must be determined using the calculation tools or

calculated by experts before commissioning the machine. Commissioning without an acceleration ramp

can result in cavitation that can lead to the destruction of the pump!

The function block also provides master-slave functionality for pairing multiple axes. These functions are described in

more detail in the Additional functionality section.

6.3MTHydPumpPowerMeter

The MTHydPumpPowerMeter function block does not handle any functions actively. It is only used to display and cal-

culate electrical, hydraulic and mechanical power. The electrical and mechanical power values are read directly from

ACOPOS ParIDs. The hydraulic power is calculated using the current pressure, current speed and pump parameters.

## Page 17

MTHYDPUMP LIBRARY FUNCTION BLOCKS 17
MTHydPumpPowerMeter
UDINT Axis Busy BOOL
BOOL Enable Active BOOL
MTHydPumpPowerMeterParType Parameter Error BOOL
BOOL Update StatusID DINT
UpdateDone BOOL
REAL ActPumpSpeed ElectricalPower REAL
REAL ActPumpPressure MechanicalPower REAL
HydraulicPower REAL
Figure 15: The MTHydPumpPowerMeter function block
Programming \ Libraries \ Mechatronic libraries \ Hydraulics \ MTHydPump \ Function blocks\ MTHyd-
PumpPowerMeter
6.4 MTHydPumpDriveProtection
The MTHydPumpDriveProtection function block provides overload protection for the ACOPOS drive, motor and pump.
Since a cutoff of the ACOPOS drive (and thus the motor) during operation must be avoided, this function block limits
the maximum values for torque and speed to the nominal values that are possible for continuous operation when it
appears that the ACOPOS drive and motor may be overloaded. To protect the pump from thermal damage when it is
in the overload range, the maximum torque is reduced down to 0 Nm.
MTHydPumpDriveProtection
UDINT Axis Busy BOOL
BOOL Enable Active BOOL
REFERENCE TO REAL LoadMotorModel Error BOOL
REFERENCE TO REAL MotorTemperature StatusID DINT
MTHydPumpPumpProtectionEnum PumpProtection UpdateDone BOOL
MTHydPumpDriveAdvParType AdvancedParameters
BOOL Update
REAL ActPumpPressure ActMaxLoad MTHydPumpMaxLoadEnum
REAL ActPumpSpeed ActMaxLoadPercent REAL
ActTorqueLimit REAL
ActSpeedLimit REAL
InProtection BOOL
ActPumpLoad REAL
BOOL PumpFlushingActivated PumpFlushingRequired BOOL
MinPumpFlushingSpeed REAL
TempModelInitialized BOOL
Figure 16: The MTHydPumpDriveProtection function block
Programming \ Libraries \ Mechatronic libraries \ Hydraulics \ MTHydPump \ Function blocks\ MTHyd-
PumpDriveProtection
What the MTHydPumpDriveProtection function block does can be divided into two parts. The first part provides over-
load protection for the ACOPOS drive and motor. The second part provides overload protection for the pump.

## Page 18

18HYDRAULIC SERVO PUMP DRIVES TM482

6.4.1ACOPOS drive and motor protection function

Function block MHydPumpDriveProtection checks multiple load parameters on the ACOPOS drive. As soon as a load

limit is exceeded (90% by default), the function block automatically limits the torque and speed on the ACOPOS drive.

The following parameters are checked cyclically:

Continuous current

•

Peak current

•

Power

•

Heat sink temperature

•

Motor temperature

•

Temperature of the motor model

•

Junction temperature (IGBTs)

•

These default limits for the load parameters can be changed in parameter structure AdvancedParameters.

Figure 17: Limiting torque and speed on motors and ACOPOS drives

6.4.2Pump protection function

To protect the pump, a thermal pump model is executed in the function block that calculates the pump's thermal load.

Figure 18: Procedure for pump protection

The thermal load of the pump is calculated in the MTHydPumpDriveProtection function block on the PLC and scaled

to a range from 0–100%. Torque is reduced if the load exceeds a certain temperature, which brings the motor to a

standstill in certain cases. To prevent this worst case scenario, output PumpFlushingRequired signals that the thermal

load has exceeded the load limit resulting in a reduction. In addition, output MinPumpFlushingSpeed indicates the

optimum speed for flush mode. To induce thermal cooling, it is necessary to flush the pump at a minimum flow rate.

The load is reduced by a certain percentage depending on the flush duration or flush volume. If torque is reduced to

## Page 19

MTHYDPUMP LIBRARY FUNCTION BLOCKS19

0%, the PumpFlushingActivated input must be signaled that cooling measures (flushing) are being taken since this

input is used to disable torque limiting.

6.4.3Initializing the motor temperature model

During operation, the motor temperature model is calculated cyclically on the ACOPOS drive. If the controller is restart-

ed, the values calculated for the motor temperature model are discarded and the ACOPOS drive begins calculations

again at 0.0% load.

Figure 19: Motor temperature model not applied.

The MTHydValveDriveProtection function block initializes the motor temperature model on the ACOPOS drive with

preset values when the controller is restarted. In order to initialize the temperature model, the axis must be in the

"Disabled" PLCopen axis state, the LoadMotorModel and MotorTemperature inputs must be connected correctly and

their values must be in the valid range. If not, a warning is output and the motor temperature model will not be initial-

ized (TempModelInitialized = FALSE).

Figure 20: Motor temperature model applied.

The motor temperature model can only be initialized when the axis is in the "Disabled" PLCopen state.

6.5MTHydPumpSimulationModel

The MTHydPumpSimulationModel function block is used to simulate a hydraulic servo pump. This system comprises

a servo pump, a switching valve and a hydraulic cylinder. The function block is already initialized with certain default

values so a specific hydraulic system is simulated right away when you call the function block. The individual parame-

ters can be modified as desired allowing any other hydraulic system to be simulated.

## Page 20

20 HYDRAULIC SERVO PUMP DRIVES TM482
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
Figure 21: The MTHydPumpSimulationModel function block
Programming \ Libraries \ Mechatronic libraries \ Hydraulics \ MTHydPump \ Function blocks\ MTHyd-
PumpSimulationModel
6.6 MTHydPumpPressureTuning
Function block MTHydPumpPressureTuning automatically configures the pressure controller on the ACOPOS drive.
MTHydPumpPressureTuning
BOOL Enable Active BOOL
MTHydPumpTuningConfigType TuningConfiguration Error BOOL
BOOL Update StatusID DINT
UpdateDone BOOL
REAL ActPumpPressure SetPumpPressure REAL
REAL ActPumpSpeed SetPumpSpeed REAL
BOOL StartTuning TuningDone BOOL
MTHydPumpTuningCtrlConfigType ControllerConfiguration ControllerCalculationDone BOOL
BOOL StartControllerCalculation TestDone BOOL
MTHydPumpTuningTestType TestSignal Controller MTPIDParametersType
BOOL StartTest CompensationFilter MTHydPumpBiQuadFilterType
BOOL Abort FilterRecommended BOOL
TuningState MTHydPumpTuningStateEnum
Figure 22: Visual representation of function block MHTHydPumpPressureTuning
Programming \ Libraries \ Mechatronic libraries \ Hydraulics \ MTHydPump \ Function blocks\ MTHyd-
PumpPressureTuning
Prerequisites for autotuning
To complete tuning, function blocks MTHydPumpPressureTuning and MTHydPumpController must be linked to each
other.

## Page 21

MTHYDPUMP LIBRARY FUNCTION BLOCKS21

MTHydPumpController

AxisBusy

MTHydPumpPressureTuning

EnableActive

EnableActive

ConfigurationError

TuningConfigurationError

ParametersStatusID

UpdateStatusID

UpdateUpdateDone

UpdateDone

SetPumpPressureActPumpPressureActPumpPressureSetPumpPressure

ActPumpSpeedSetPumpSpeedSetPumpSpeedActPumpSpeed

StartTuningTuningDoneControllerMode

ControllerConfigurationControllerCalculationDone

StartControllerCalculationTestDone

TestSignalController

StartTestCompensationFilter

AbortFilterRecommended

TuningState

Figure 23: Connecting the function blocks

It is also necessary to ensure that the cylinder is in the position where pressure control should be carried out (cylinder

is pressing against the material and the valve is open).

Hydraulic Contact

Cylinder

AB

Switching

Valve

PT

Servo pump

Figure 24: Cylinder in contact position

## Page 22

22HYDRAULIC SERVO PUMP DRIVES TM482

Tuning process

The hydraulic system is excited with a certain rotary speed signals (jump signals and harmonic signals ). These

•

excitation signals can be configured on the function block.

The pressure is also measured (system response)

•

Figure 25: Excitation signal for pressure controller tuning

With the excitation signal and the measured pressure, the function block identifies the dynamic system and cal-

•

culates the frequency response (Bode plot). The resonant frequency is also identified.

Figure 26: Bode plot

After the system has been successfully identified, a PI or PID controller and a compensation filter is automatically

•

created.

Command StartTest can be used to verify the calculated PID parameters based on pressure jumps. The test sig-

•

nal can be configured on function block MTHydPumpPressureTuning.

Figure 27: Test signal to check the controller parameters

If necessary, fine tuning can also be carried out. This is done by changing the controller configuration (RiseTime,

•

ControllerType, DerivativeTimeScale, FilterTimeScale) and starting the new controller parameter calculation with

command StartControllerCalculation. The newly calculated parameters can then be verified again using the test

signal. The following fine tuning procedure has proven to be successful in real-world situations:

Too slow: Reduce  RiseTime→

°

Overshoot is too large: Increase  PID controller instead of PI controller, DerivativeTimeScale→

°

If FilterRecommended =TRUE  ActivateCompensationFilter =TRUE→

°

## Page 23

MTHYDPUMP LIBRARY FUNCTION BLOCKS23

It is necessary to ensure that during tuning the current pump pressure (ActPumpPressure) does not ex-

ceed the maximum pump pressure (MaxPumpPressure). Tuning is canceled in this case.

6.7Using function blocks

Keep the following in mind when using the function blocks provided in the MTHydPump library:

Before you can use the MTHydPumpController function block for speed or pressure control, the axis must be in

•

the PLCopen state "Standstill". Otherwise the function block is enables, but closed-loop control for setpoint val-

ues in not possible because the controller has not yet been enabled on the ACOPOS drive.

If function block MTHydPumpController is used together with MTHydPumpDriveProtection, the following call se-

•

quence must be adhered to in order for both to work properly:

Enable = TRUEEnable = TRUE

PLCopen Status

Acve=TRUE

=DISABLED

MTHydPumpMTHydPumpController

DriveProtecon

TempModelInialized =TRUEPLCopen Status

ReadySwitch on Axis

Acve=TRUE=STANDSTILL

Figure 28: Function block connection with StandaloneMode = mtPUMP_WITH_PRESSURE_CONTROL

If function block MTHydPumpDriveProtection is used in stand-alone mode, the following call sequence must be

•

adhered to in order for it to work properly:

Enable = TRUE

PLCopen Status TempModelInialized =TRUE

=DISABLEDAcve=TRUE

MTHydPump

DriveProtecon

PLCopen Status

ReadySwitch on Axis

=STANDSTILL

Figure 29:  Function block connection with StandaloneMode = mtPUMP_WITHOUT_PRESSURE_CONTROL

## Page 24

24HYDRAULIC SERVO PUMP DRIVES TM482

7Technology solution – Hydraulic servo

pump control

The B&R mechatronic solutions, and also Technology Solution Hydraulic Servo Pump Control, are fully integrated in

Automation Studio. The result is a simple and uniform tool for developing projects as well as commissioning closed-

loop control processes and machines.

This Technology Solution provides the user a preprogrammed solution in the form of a package containing all the

necessary programs, variables and data type declarations as well as an HMI application and a simulation model. The

programs are designed so that they can be added to and executed in any existing project. Users can also create their

own template projects or startup projects and manage them in a separate directory.

Figure 30: Technology Solution – Description

Technology Solution Hydraulic servo pump control includes speed and pressure control for a hydraulic system with a

cylinder, switching valve and servo pump. It is possible to operate the servo pump in two different modes:

Speed control

■

Alternating speed/pressure control

■

## Page 25

TECHNOLOGY SOLUTION – HYDRAULIC SERVO PUMP CONTROL25

Figure 31: Start page for the Hydraulic Servo Pump Control Technology Solution HMI application.

7.1Technology Solution exercise

In this exercise, Technology Solution Hydraulic Servo Pump Control is out into operation.

a)Inserting the Technology Solution in a new project.

Figure 32: Insert the Technology Solution

b)Activating the HydraulicServoPumpControl configuration

Figure 33: Activate the configuration

c)Transfer the project to the controller (ARsim)

d)Connect to the HMI application (VNC viewer, IP: 127.0.0.1).

e)Execute speed control or alternating pressure/speed control.

f)Become familiar with implementation of the Technology Solution. How are the function blocks configured, and

how is the application linked with the simulation model, etc.?

7.2ACOPOS drive cutoff protection exercise

In this exercise, the ACOPOS drive cutoff protection provided in function block MTHydPumpDriveProtection will be

tested using Technology Solution Hydraulic Servo Pump Control.

## Page 26

26HYDRAULIC SERVO PUMP DRIVES TM482

a)Use the previous project with Technology Solution inserted.

b)Using the HMI application (VNC Viewer), move the cylinder into the material and use closed loop control to main-

tain a pressure of 200 bar.

c)Hold pressure until function block MTHydPumpDriveProtection begins to limit maximum torque and speed.

d)View Trend to monitor how speed limiting reduces the current pressure and the pressure setpoint can no longer

maintained.

Figure 34: Trend shows speed reduction

Through the simulation, it is possible that the pump speed starts to oscillate during torque limiting.

7.3Exercise - Speed Torque Chart

In this exercise, a load cycle will be recorded with Technology Solution Hydraulic Servo Pump Control and drive sizing

will be checked in the Speed Torque Chart.

## Page 27

TECHNOLOGY SOLUTION – HYDRAULIC SERVO PUMP CONTROL27

a)Configure and start motion trace with ParIDs 251 (current speed) and 277 (motor torque).

b)Execute multiple movements with the cylinder using the HMI application (VNC Viewer).

c)Stop motion trace and save the trace as a csv file.

d)Open the Speed Torque Chart in Automation Studio.

e)Add the load cycle that was just recorded.

Figure 35: Add the load cycle to the Speed Torque Chart as a .csv file.

f)Check the drive sizing using the Speed Torque Chart (operating point, torque reserves, thermal reserves, speed

reserves).

Figure 36: Speed Torque Chart

7.4Exercise – Power monitoring

In this exercise, the simulated power values in Technology Solution Hydraulic Servo Pump Control is monitored using

function block MTHydPumpPowerMeter:

a)Use the previous project with Technology Solution inserted.

b)Using the HMI application (VNC Viewer), move the cylinder into the material and use closed loop control to main-

tain a pressure of 120 bar.

c)Go to Automation Studio.

d)Open "Watch Window" for task HydCtrl.

e)Add function block MTHydPumpPowerMeter_0.

f)Monitor the electrical, mechanical and hydraulic power.

g)Move the cylinder and monitor the change to the power values.

## Page 28

28 HYDRAULIC SERVO PUMP DRIVES TM482
7.5 Exercise – Pressure controller tuning
In this exercise, the pressure controller on the ACOPOS drive is automatically set up using Technology Solution Hy-
draulic Servo Pump Control.
a) Use the previous project with Technology Solution inserted.
b) Using the HMI application (VNC Viewer), move the cylinder into the material and use closed loop control to main-
tain a pressure of 120 bar.
c) Leave the valve open.
d) Execute command StartTuning on the Tuning HMI page.
e) Observe tuning in the Trend report.
f) Check the calculated controller parameters with the test signal.
In Automation Studio, install a Trace with inputs ActPumpPressure and ActPumpSpeed and outputs Set-
°
PumpPressure and SetPumpSpeed on function block MTHydPumpPressureTuning.
Start the test signal using StartTest on the Tuning" HMI page.
°
After the test signal, verify the controller quality in Automation Studio using the Trace function.
°
g) If necessary, change the controller configuration (adjust RiseTime, ControllerType, DerivativeTimeScale, Activate-
CompensationFitler) and recalculate the new parameters.

## Page 29

MASTER/SLAVE MODE 29
8 Master/Slave mode
Another important function is master/slave mode. This functionality can be used to create multi-pump systems.
To use master/slave mode, the reference to the master must be specified for the slave system function blocks. Cross-
communication is used to transfer the master's speed specifications to the slaves as well. The ControllerMode input
on the function block can be used to switch master/slave mode on and off. This allows the slave axis to be linked and
unlinked as required. With an appropriate hydraulic system, both can also work as standalone systems and carry out
independent movements at the same time.
Required configuration of the slave pump
1) Axis
2) MasterAxis
3) ControllerMode = mtPUMP_SLAVE_MODE
4) Configuration.SlaveGearRatio
5) Configuration.MaxPumpAcceleration
6) Configuration.MaxPumpSpeed
7) Configuration.ReversePumpMotorSpeed
8) Parameters.ControllerParameterSet[x].MaxReversePumpSpeed
9) Parameters.DisableMinPressureController = TRUE
Positive limitation of the speed setpoint Configuration.MaxPumpSpeed is not enabled in Slave mode!
Speed controller settings – master/slave
Tune the speed controller – Each axis separately!
See section 10.2.2 "Tuning the ACOPOS speed controller".
1) Tune the speed controller from the master
1) Turn off the slave axis
2) Autotuning, check parameters with step response
3) Fine-tune parameters manually if necessary
2) Tune the speed controller from the slave
1) Turn off the master axis
2) Autotuning, check parameters with step response
3) Fine-tune parameters manually if necessary
3) Check parameters with step response from both axes
1) Master: MTHydPumpController.ControllerMode = mtPUMP_SPEED_CONTROL
2) Slave: MTHydPumpController.ControllerMode= mtPUMP_SLAVE_MODE
3) Check parameters with step response
4) Fine-tune parameters manually if necessary
8.1 Master-Slave exercise
In this exercise, master-slave control is implemented in Automation Studio using Technology Solution HydraulicSer-
voPumpControl.

## Page 30

30HYDRAULIC SERVO PUMP DRIVES TM482

Use the previous project with Technology Solution HydraulicServoPumpControl inserted.

•

Add another ACOPOS "8V128M.00-2" (slave) in System Designer.

•

Add the POWERLINK interface "8AC114.60-2" to slot SS1.

•

Connect the "ACOPOS slave drive" with the "ACOPOS master drive" via POWERLINK. The configuration window

•

should then open.

Configure a resolver "8AC122.60-3" for slot SS2. Slot SS3 can remain unused (skip page).

•

Configure motor "8LSA65.EA030D200-1" for motor connection MT1.

•

Activate motor simulation.

•

Limit switch and quick stop are "normally open"

•

Figure 37: System Designer – Master/Slave hardware concept

Implement MTHydPumpController in task HydCtrl and configure as slave

•

–Axis = Axis reference for the slave axis (gAxis02)

–MasterAxis = Axis reference for the master axis (gAxis01)

–ControllerMode = mtPUMP_SLAVE_MODE

–Configuration.SlaveGearRatio = 1

–Configuration.MaxPumpAcceleration = 60000.0 rpm/s

–Configuration.MaxPumpSpeed = 3000.0 rpm

–Parameters.ControllerParameterSet[0].MaxReversePumpSpeed = -4000.0 rpm

–Parameters.DisableMinPressureController = 1

Switch on the master axis using the HMI application and start the controller.

•

Switch on the slave axis from the test window

•

Switch on MTHydPumpController for the slave axis (Enable = TRUE).

•

Slave axis should follow the master axis with ratio SlaveGearRatio.

•

## Page 31

POSITION CONTROL WITH SERVO PUMP DRIVE31

9Position control with servo pump drive

Hydraulic servo pump drives can of course also be used for position control. For this to be implemented, a cylinder

velocity setpoint must be converted to rotary speed and transferred to MTHydPumpController. Position control is

handled by another control block. The following image shows 2 sample concepts for implementing position control

using B&R software blocks.

n

vnSetv

SetSetSet

+

p

sSet

Set

p

Set

MTLookUpTable

MTProﬁleMTHydPumpController

vPosionGenerator

Add

s

Act

MTBasicsPID

Figure 38: Position control using MTBasicsPID as position controller

n

nSetv

SetSet

s

Act

p

Set

p

Set

MTHydGenVelocityGeneratorMTLookUpTable

MTHydPumpController

Figure 39: Position control using MTHydGenVelocityGenerator as velocity generator with the function of position-based braking

## Page 32

32 HYDRAULIC SERVO PUMP DRIVES TM482
10 Project development
The following guidelines should be observed. Nevertheless, there are often individual situations and cus-
tomer requirements that demand a customized solution.
10.1 Project development guidelines
These project development guidelines are meant to serve as a guide for implementation, minimizing potential sources
of errors and efficient project management.
a) Analyze the starting situation.
Has the system been sized?
°
Integrated or standalone variants?
°
b) Take part in a training and work through this training manual.
c) Become familiar with the library.
d) If necessary, contact the B&R Mechatronic Technologies department.
e) Determine customer requirements, collect machine data.
Be sure you understand the hydraulic schematics. If you have difficulties with the hydraulic schematics,
°
please contact the B&R Mechatronic Technologies department or the customer's hydraulic department.
Identify critical/decisive factors related to product quality (e.g. specific cylinder movements, braking
°
processes, etc.).
Identify special operating conditions (e.g. oil preheating, setup conditions, long phases where pressure must
°
be maintained).
Survey possible control concepts used by the customer.
°
f) Document customer requirements in a short but precise specification.
g) Develop a control concept in the form of a block diagram.
Be sure you understand the setpoint profiles for pressure and speed.
°
Identify the active pressure reduction phases.
°
Brakes for closing mechanisms (especially for injection molding).
°
h) If necessary, contact the B&R Mechatronic Technologies department.
i) Configure the function blocks
What parameters result from the machine data?
°
Collect necessary machine data.
°
Which parameters must be determined during commissioning?
°
j) If necessary, contact the B&R Mechatronic Technologies department.
k) Consider the procedures necessary for commissioning the machine.
l) Consult with the customer.
m) Implementation
n) Test on the simulation model.
o) Installation
p) Review the project.
10.2 Commissioning instructions
When commissioning a servo pump, there are a few points that should be taken into consideration beforehand in order
to eliminate possible sources of error. The following list is intended as a guide during commissioning. A possibility for
tuning the control loop is also given.

## Page 33

PROJECT DEVELOPMENT 33
10.2.1 Commissioning procedure
1. Check the cabling.
AC131 power supply
•
AC131 pressure sensor connection
•
POWERLINK / CAN
•
Motor cable (encoder, supply)
•
Motor fan (24 V or 230 V, direction of rotation at 24 V)
•
ACOPOS supply (ground, power mains -> use caution with IT mains)
•
2. Check the hydraulics.
Dimensions on the suction side for calculating the maximum acceleration rate (suction hose diameter, suction
•
hose length and pump/oil level difference)
Calculate or check the acceleration rate MaxPumpAcceleration.
•
Set the acceleration rate on the function block.
•
Pressure relief valve (must be available)
•
Mounting position of the pressure sensor (should be mounted as close to the pump as possible, preferably on
•
the pump itself)
Oil level
•
Pump connections (suction side to the tank, pressure side to the system)
•
Discuss the hydraulic schematic with the customer.
•
3. Direction of rotation of the motor
Autotune the ACOPOS speed controller.
•
When using a resolver, check the filter time (approx. 2 ms).
•
Set the speed setpoint SetPumpSpeed to 0 rpm.
•
Set the controller MTHydPumpController() to speed control (ControllerMode = mtPUMP_SPEED_CONTROL).
•
Start closed-loop servo pump control (previously MCPower, etc.).
•
Set the speed setpoint SetPumpSpeed to 10 rpm.
•
Check the direction of rotation of the motor using the holes in the coupling housing and compare with the direc-
•
tion arrow on the pump.
If there are no holes, there are two alternative variants:
a) Close all valves, connect a measurement hose to the pump block and check if oil flows (may take a few min-
utes).
b) Close all valves and check for an increase in the system pressure. The speed can also be increased if necessary
since the pressure increases very slowly at low speeds.
If the direction of rotation is incorrect, it can be inverted using the ReversePumpMotorSpeed parameter. (When
•
using B&R motors with a Dorninger Hytronics DHPH pump, the direction of rotation is inverted in most cases.)
4. Check the pressure relief valve (after the setting has been made).
Operate the pump at a constant speed (approximately 100–200 rpm).
•
Check if the desired system pressure results.
•
If necessary, a hydraulic engineer should readjust it.
•

## Page 34

34HYDRAULIC SERVO PUMP DRIVES TM482

5. Pump protection

Cavitation protection

•

Calculate the MaxPumpAcceleration parameter and set accordingly to limit the acceleration of the pump and

the motor. By using the MTHydPumpController function block, the configured axis limits such as v_pos, v_neg,

a1_pos and a1_neg are disabled. In Automation Help, there a link to an Excel sheet that can be used to make

the calculation.

Thermal pump protection

•

The MTHydPumpDriveProtection function block can be used to enable a pump protection feature that esti-

mates the thermal load on the pump in a mathematical model and protects it against overheating. All pumps

from Dorninger Hytronics are supported. Pumps from other manufacturers can also be protected as long as

their size and displacement volume are known since they will presumably have similar thermal characteristics.

6. Tuning the ACOPOS speed controller

See section "Tuning the ACOPOS speed controller".

•

7. Setting the pressure controller

See section "Setting the pressure controller".

•

10.2.2Tuning the ACOPOS speed controller

A prerequisite for functional closed-loop control of pressure and flow rate is a properly tuned speed controller. Impor-

tant for the speed controller is that it follows the desired speed as precisely as possible and that the output of the

speed controller provides a clean setpoint for the flow controller. In order to prevent a permanent control deviation,

it is necessary to set an I component for the speed controller. The P component can be determined with the help of

ACOPOS autotuning. The I component must be set manually.

Figure 40: Block diagram for tuning the speed controller

Procedure:

Set the tuning mode to ncSPEED + ncT_FILTER

•

Start auto-tuning

•

Set integral component of speed controller (default value: Tn = 50 ms).

•

With AS 4.0 and higher, the "Servo Loop Optimizer" can optionally be used for autotuning. This gives youOPTIONAL:

a range of options for compensating resonant frequencies.

Closed-loop speed controller step responses should be recorded in order to check the control loop parameters (if the

hydaulic system allows it). All valves must be closed for these tests. The MTHydPumpController function block must

be enabled and the minimum pressure controller must be disabled (DisableMinPressCtrl = TRUE). Speed jumps from

a standstill (e.g. 0-MaxPumpSpeed) as well as speed jumps under load (e.g. 500-MaxPumpSpeed) should be recorded.

These speed jumps are applied to the SetPumpSpeed input of the MTHydPumpController function block.

When tuning the speed controller, all valves are closed and the entire volumetric flow flows directly back into the tank.

Before carrying out the step response tests, the Configuration.MaxPumpAcceleration parameter must

be set correctly in order to prevent cavitation of the pump caused by rapid acceleration!

## Page 35

PROJECT DEVELOPMENT35

The gain Kv determined by autotuning should be checked for plausibility and adjusted manually if nec-

essary!

The following image shows a block diagram of the ACOPOS control loop. The step response can be recorded with the

listed ParIDs using the ACOPOS trace function.

Figure 41: Block diagram for checking the speed controller

10.2.3Setting the pressure controller

Pressure controller autotuning

The cylinder must always be in the position in which pressure control should be performed, i.e. the cylinder must be

pressing against resistance. In addition, the valves must be switched properly so that pressurization can take place

accordingly. The servo pump must also be connected to the cylinder chamber responsible for pressurization.

Speed Controller Pressure Filter Cylinder in Check trace

DONE!OK?Start TuningStart TestYES

Tuningadjustingpressure stateof pressure

Start Controller Modify

CalculationControllerNO

(optional)Configuration

Figure 42: Auto-tuning pressure controller process

Current situation

Performing speed controller autotuning

•

Filter the pressure signal accordingly (e.g. ActPressureFilterTime = 0.005 ms).

•

Put the cylinder in the position for pressure control.

•

Tuning

StartTuning

•

TestTuning

•

Check pressure signals ActPumpPressure and SetPumpPressure to determine whether dynamic and overshoot

•

requirements are being met. Check manipulated variable ActPumpSpeed to determine whether speed reserves

are available.

If the requirements are not met, the following possibilities exist:

•

Fine tuning

Too slow: Reduce RiseTime. This is possible as long as speed reserves exist (ActPumpSpeed < Max-

°

PumpSpeed).

Too much overshoot: Change to ControllerType = mtPUMP_PID_CONTROLLER and change the strength of the

°

D component with DerivativeTimeScale. If oscillation occurs in the rotary speed, increase FilterTimeScale or

reduce DerivativeTimeScale.

If FilterRecommended = TRUE, set ActivateCompensationFilter = TRUE. This should make higher dynamics

°

possible.

## Page 36

36HYDRAULIC SERVO PUMP DRIVES TM482

Manually configuring the pressure controller

In order to optimally tune the pressure controller, we recommend that the desired axis (cylinder) be moved outside of

the movement towards the end position and then maintaining pressure. Pressure jumps can also be carried out. These

step responses can be used to tune the control loop so that the behavior meets specific requirements.

The function block must be tuned in pressure control mode ((ControllerMode = mtPUMP_PRESSURE_SPEED_CON-

TROL). The speed setpoint SetPumpSpeed should be set to an appropriate value (e.g. 1000 rpm). The motor should still

be able to accelerate with maximum torque at this speed. If an induction motor is used with field weakening enabled,

this is no longer the case. To ensure that no damage occurs during tuning, the pressure limiting valve should be set

to a pressure that is non-critical for the system.

Figure 43: Block diagram for setting the I component

Control loop parameters greatly depend on the hydraulic and mechanical system. The speed controller

and pressure controller are configured just once for a machine type. A configuration can include multiple

parameter sets.

## Page 37

PROJECT DEVELOPMENT37

Procedure

1)Set pressure filter (Parameters.ActPressureFilterTime) accordingly: If the filter time is set too low, then signal

disturbance will not be sufficiently suppressed (effect: motor doesn't run smoothly). However, setting the filter

time too high makes the control sluggish. A good value to start with is Parameters.ActPressureFilterTime = 0.02

s.

2)Start with the initial value of the pressure controller's gain factor (Kp), set the desired pressure setpoint Set-

PumpPressure and let the motor drive the cylinder until it hits the stopper. Then keep increasing Kp until oscil-

lation occurs on the controller output (e.g. louder motor than before) and reduce Kp by 20%. If the initial value

causes oscillations, reduce Kp until behavior is stabilized. A good value to start with for Kp is Parameters.Con-

trollerParameterSet[x].PressureController.ProportionalGain = 30 rpm/bar.

3)Start with the initial value of the pressure controller's integration time constant (Ti), set the desired pressure

setpoint SetPumpPressure and let the motor drive the cylinder until it hits the stopper. Then keep reducing Ti un-

til an undershoot occurs on the measurement value and increase Ti by 10%. If the initial value causes an under-

shoot, increase Ti until the overshoot disappears from the measurement value. A good value to start with for Ti is

Parameters.ControllerParameterSet[x].PressureController.IntegrationTime = 0.1 s.

Figure 44: Step responses

4) Start with the initial value of the derivative action time constant (Td) and increase until overshoot isOptional:

hardly detectable. In addition, each time Td is increased, the user should also check whether the overshoot is re-

duced by decreasing Ti. If the overshoot is suppressed too much with the initial value, decrease Td until the over-

shoot is hardly detectable. Oscillations that may occur due to increasing Td can be damped with T1. A good value

to start with for Td is Parameters.ControllerParameterSet[x].PressureController.DerivativeTime = 0.01 s.

5) Overshoot and undershoot can be avoided by configuring a setpoint filter (PT1) for positive (Para-Optional:

meters.ControllerParameterSet.SetPressureFilterTimePosDir) and negative (Parameters.ControllerParame-

terSet.SetPressureFilterTimeNegDir) setpoint jumps.

6) Each cylinder can be tuned separately for each direction of movement as needed. The various parame-Optional:

ter sets (SelectControllerParSet) can be used to switch quickly back and forth between the different parameters.

Figure 45: Tuning procedure form the pressure controller

## Page 38

38 HYDRAULIC SERVO PUMP DRIVES TM482
Minimum pressure controller
Good values to start with for the minimum pressure controller parameters are those used for the maximum pressure
controller (SetPumpPressure). The same rules 2-4 and 6 used for the maximum pressure controller can be used for
tuning.

## Page 39

SUMMARY 39
11 Summary
Combining B&R motion components with efficient internal gear pumps results in an advanced solution to hydraulic
tasks that will be effective well into the future. The library and function blocks used for closed-loop servo pump control
are easy to use and are an ideal replacement for costly analog controller cards.

## Page 40

40HYDRAULIC SERVO PUMP DRIVES TM482

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

## Page 41

AUTOMATION ACADEMY 41

## Page 42

42 HYDRAULIC SERVO PUMP DRIVES TM482

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

V3.0.0.0 ©2023/10/23 by B&R, All rights reserved.

All registered trademarks are the property of their respective owners.

Subject to technical changes without notice.