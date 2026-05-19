## Page 1

TM291

TM291 - Basics of

virtualization and

simulation for control

technology

## Page 2

2 TM291 - BASICS OF VIRTUALIZATION AND SIMULATION FOR CONTROL TECHNOLOGY TM291
Requirements
Training modules No requirements
Software No requirements
Hardware No requirements

## Page 3

TABLE OF CONTENTS 3
Table of contents
1 Introduction.........................................................................................................................................................4
1.1 Learning objectives..............................................................................................................................5
1.2 Typical tasks..........................................................................................................................................5
1.3 Symbols and safety notices...............................................................................................................5
2 The concept of modern closed-loop control...............................................................................................6
2.1 Challenges for modern closed-loop control...................................................................................6
2.2 Technology functions of closed-loop control................................................................................7
3 Basic information...............................................................................................................................................8
3.1 Simulation..............................................................................................................................................8
3.2 Model......................................................................................................................................................8
3.3 Model-based development...............................................................................................................12
3.4 Virtual commissioning......................................................................................................................14
3.5 Rapid prototyping..............................................................................................................................15
4 Simulation levels..............................................................................................................................................16
4.1 Automation hardware........................................................................................................................17
4.2 Components and machine simulation..........................................................................................23
4.3 Process and plant simulation..........................................................................................................26
4.4 Model-based development with B&R............................................................................................28
5 Summary............................................................................................................................................................34

## Page 4

4TM291 - BASICS OF VIRTUALIZATION AND SIMULATION FOR CONTROL TECHNOLOGY TM291

1Introduction

Over the last few years, simulation has developed into an independent field of expertise that is used as a powerful and

versatile development tool in automation. For example, in the planning and design of control and operating systems.

Simulation is not only used for analyzing processes, but also for further development of the respective field.

This is why simulation is considered an interdisciplinary basic principle for solving problems in technical and non-

technical areas.

ControllerDevelopment

HMISimulation

Motion controlCommissioning

Safety technologyDiagnostics and

Service

Figure 1: Automation Studio - One engineering tool for the machine's entire lifecycle

High flexibility, efficient resource management and unlimited connectivity of development components are the cor-

nerstones of simulation and also represent the advantages of B&R hardware and software.

Simulation can be easily integrated into daily development processes with having the focus of development on the

core competencies of the respective field.

Basic concepts and procedures are described in this module.

## Page 5

INTRODUCTION 5
1.1 Learning objectives
This training module gives participants insight into the basics of simulation and the technology of model-based de-
velopment in combination with virtual commissioning.
Participants will receive information about the following:
Terminology related to virtualization and simulation.
•
The development process ranging from the model to simulation and virtual commissioning.
•
Important criteria for simulation, virtual commissioning and creating models.
•
The different simulation levels and their areas of application.
•
Simulation options with Automation Studio and B&R hardware.
•
Third-party tool connection for model development and simulation.
•
The principle of hardware-in-the-loop (HiL) and software-in-the-loop (SiL).
•
1.2 Typical tasks
Simulation can be used to test models that have been created on a theoretical or experimental basis in as realistic of
an environment as possible. The ability to safely optimize the system in advance makes it more productive, while at the
same time reducing the cost and risk of commissioning. The models can be simulated on different complexity levels.
Designing control loops to optimize their behavior is one of the standard applications in control engineering. Control
loops can be tested for a wide variety of applications and scenarios within a safe simulation environment.
In many cases, the structure and dynamics of machine or machine components are simulated. Simulation can be used
to determine the design of the drive solution for a machine, to test the functionality of a new machine design or to
push the physical limits of a machine. This is usually supported by a 3D simulation and includes virtual commissioning.
Another area of application for 3D simulation and virtual commissioning is complete process monitoring. This includes
system planning for industry and logistics, visualization of processes and resources and the entire material flow within
a system.
Simulation – in combination with visualization – can also be used to train employees on the virtual machine, to create
training scenarios or to simulate system operation (Lookahead).
1.3 Symbols and safety notices
Unless otherwise specified, the symbol descriptions and safety notices listed in "TM210 - Working with Automation
Studio" apply.

## Page 6

6TM291 - BASICS OF VIRTUALIZATION AND SIMULATION FOR CONTROL TECHNOLOGY TM291

2The concept of modern closed-loop

control

Closed-loop control is a crucial part of everyday life. There are many technical applications that are equipped with a

multitude of control loops. The human body itself is a collection of numerous control loops. Be it sight, touch or hearing

– sensory inputs are characterized by regulation. The human body processes these inputs and then tries to use the

actuators in a way similar to closed-loop control to achieve a desired environment.

Closed-loop control has become considerably more important in recent years. Conventional control systems, which

are still very common, meet – if at all – the rapidly increasing requirements only partially. Looking at the challenges,

B&R focuses on the holistic approach "All solutions from a single source".

This approach enables integrated closed-loop control. Both the machine controller and all applications based on

closed-loop control run on the controller. B&R also offers seamless integration of motion control and sensors via the

POWERLINK interface which is real-time capable. B&R integrated the various simulation components seamlessly into

its overall design and also offers a comprehensive diagnostics solution.

The homogeneous, synchronized hardware system from B&R guarantees maximum productivity, highest product qual-

ity, a complete diagnostics solution and limitless potential for innovation.

Machine controllerSensorTechnology functions

SimulationMotion control

Figure 2: "All solutions from a single source" - Modern closed-loop control from B&R

2.1Challenges for modern closed-loop control

The field of mechatronics deals with the interaction of mechanical, electronic and IT-related systems. In mechatronics,

the boundaries between the areas of mechanics, electronics and information technology are put aside. Instead, the

system is viewed as a single functional unit. The key objective is the preparation and processing of all information so

that it can be used across all of these areas.

## Page 7

THE CONCEPT OF MODERN CLOSED-LOOP CONTROL7

MECHATRONICS

Control Mechanical

systemengineering

Electrical Computer

engineeringscience

Figure 3: Concept of mechatronics

Globalization and increasing competition have led to a shorter production cycle of machines with complex functional-

ities. This requires smart machines and production systems. Connectivity, sensor technology and computing power

play an important role in that context and have been continuously developed further. These changes have also created

challenges for the automation industry. With the aim to increase productivity and efficiency, it is necessary to use

the information available for specific optimization purposes. This results in increasingly complex systems with highly

mathematical algorithms running in the background. Closed-loop control allows this information to be processed in

the best possible manner and generates added value for the machine and production system.

2.2Technology functions of closed-loop control

B&R offers a wide range of possibilities for implementing individual closed-loop control solutions. Automation Studio

offers an extensive array of software tools for this purpose. In addition to the standard libraries, which contain ele-

mentary basic functionalities, mapp components, technology-specific functions and solution samples are also avail-

able in order to master complex systems in the best possible way. Automation Studio features numerous interfaces

to external development tools, including simulation tools.

Figure 4: With Automation Studio, a wide range of hardware and software can be controlled and linked.

## Page 8

8TM291 - BASICS OF VIRTUALIZATION AND SIMULATION FOR CONTROL TECHNOLOGY TM291

3Basic information

The term "simulation" covers many different aspects, which together represent a strong and versatile tool for closed-

loop control. The basic terms are thus introduced and elementary correlations in simulation explained.

3.1Simulation

Simulation is an experimental procedure to examine static and dynamic properties of a system using a model. For this

purpose, a digital representation of the original system – also called "simulator" – is used. For this representation, it

is irrelevant whether the system to be examined already exists or is still in planning stage.

Simulation has been proved to be an efficient and cost-effective method in the process of testing and planning systems

in detail. The sharp increase in production requirements, the expanding complexity of systems and the development of

greater numbers of technical products with software all make simulation an essential component of the development

process.

The image below shows a simplified procedure for creating a simulation model.

Conditions

Requirements

Real machineModelSimulation

Thermo forming machine – Forming station

Figure 5: Simulation steps: From the real machine to the simulation model via the CAD model

3.2Model

The model, also called "simulator" or "digital twin", is the basis for a simulation. The appropriate examinations and

analyses can be performed if the model is specifically targeted at the problem.

A simulation model is a limited representation of reality that allows to consider the essential properties of the system

for the respective task. At this point, it is important to emphasize that no simulation model can recreate reality exactly.

A simulation model is always the product of a compromise between accuracy and complexity.

## Page 9

BASIC INFORMATION 9
Vehicle powertrain rod
Speed "v " of the vehicle can be influenced by accelerator pedal position "x " and by gear ratio (gear se-
F P
lection) "ü" . The gas pedal has an effect on the injection and thus on motor torque " ". This is converted
MM
by the gearbox corresponding to translation "ü" and led to the wheels via the axis ratio. Adding the wheel
diameter results in the driving force that is needed to accelerate vehicle mass "m". There are also external
forces, e.g. caused by air resistance "
FL
" or a road αgradient, that act in the direction of or against the
vehicle movement. In this use case, the following applies:
"x " and "ü" are the manipulated variables.
• P
• "F L " and α are the disturbance variables.
"v " is the output signal.
• F
Inputs Outputs
x
y
x2
Simulaon model y2
x3
ü
Automobile
/ ü … Actuang Input
 … DisturbanceInput
… Output
Figure 6: Example of a model based on the driving dynamics of a car

## Page 10

10 TM291 - BASICS OF VIRTUALIZATION AND SIMULATION FOR CONTROL TECHNOLOGY TM291
3.2.1 Model creation
At the beginning of each model creation, benefit and effort must be determined. The benefit usually depends on the
expected results, whereas the effort depends on the level of detail of the model.
A five-phase model is used for model creation to achieve meaningful results:
Analysis and problem formulation
•
Definition of quantifiable goals and collection of problem-relevant data.
Abstraction
•
The system to be simulated is abstracted and transformed into a model. The purpose of the model determines
the scope, type and system-based limits of the model. In order to create an adequate model, the level of detail
(degree of abstraction) must be determined.
Creation of simulation model
•
The model is implemented as a simulation model.
Simulation experiments
•
Examination of the simulation model for behavior and completeness as well as a comparison with the real sys-
tem. In the event of error behavior, the first step must be repeated.
Documentation
•
Evaluation and documentation of the results.
As the model itself is an abstract image of a real system, it should be abstract but nevertheless realistic. This means
that only the properties necessary to solve a specific problem are included when creating the model.
3.2.2 Level of detail
In addition to high-quality data collection and system knowledge, a suitable level of detail for the model has a consid-
erable influence on both the quality of the simulation results and the necessary modeling effort.
The specification of the simulation model is based on the relevant properties that must be mapped in detail. The
desired degree of accuracy and number of simulation results as well as the later use of the model have a considerable
influence on the level of detail. This means that the model is detailed to the extent required for simulating the important
properties as best as possible.
To determine the level of detail, it is important to keep the modeling as accurate as necessary instead
of as accurate as possible.

## Page 11

BASIC INFORMATION11

The level of detail is explained further via the examples of combustion in an engine, dynamic driving behaviour of a

vehicle and road traffic.

The following processes are important in : The mix ratio of the fuel, thethe simulation of a combustion engine

•

injection pressure, the combustion processes, the emissions as well as the dynamics of the cylinders and the

crankshaft.

It is not the detailed information about the engine that is relevant to the vehicle as anSimulation of a vehicle:

•

overall model but the output variables such as torque or power. The dynamics of the power transmission system,

the transverse dynamic forces and the dynamics of the tires are also essential for a vehicle. The driver can also

actively influence the dynamic behavior of the vehicle via the gas pedal and the steering wheel.

When simulating road traffic, whole vehicle streams are of more interest than the de-Simulation of road traffic:

•

tails of the individual vehicles. A vehicle is the underlying unit. It is sufficient to specify a certain speed for the in-

dividual vehicles that the movement is simulated with. In addition, further parameters such as direction, acceler-

ation and turning radius can be decisive.

Figure 7: Level of detail for models

## Page 12

12 TM291 - BASICS OF VIRTUALIZATION AND SIMULATION FOR CONTROL TECHNOLOGY TM291
3.3 Model-based development
Model-based development is based on the principle of simulation and is a multi-stage solution that is continuously
validated.
In this chapter, the steps of model-based development are explained and the two simulation methods software-in-
the-loop (SiL) and hardware-in-the-loop (HiL) are introduced.
External simulation tools can also be used to create the simulation model. For this, B&R offers seamless
integration of the most common software such as MATLAB/Simulink and MapleSim.
3.3.1 Modeling and simulation
Model and simulation are two elements that form a symbiosis within model-based development. Simulation is used
for further analysis of more complex issues of which the model is a part of, while the model itself uses the simulation
as a validation and verification tool. As mentioned above, model-based development is a multi-stage process.
The first step is a detailed analysis of the respective system and the corresponding issues via the simulation. The
requirements for the model are defined. This step is followed by designing the model that is based on the problem
and contains all the relevant processes required for development. A model should be abstract but still realistic. This
means that only the necessary properties are included when developing the model.
Model analysis
Model design
Simulaon
Test and validaon
Soware development
Virtual commissioning
Real commissioning
Figure 8: Procedure of model-based development
The model must be validated both against the original system and its technical correctness. This step is performed
in the simulation. Furthermore, the physical limits or any other problems can also be identified in the simulation. The
simulation provides a safe platform on which the behavior of the real system can be verified in the context of any
potential environment. As long as none of the verification processes return poor results, the model-based development
can be continued. If the verification process results in poor results, however, it is necessary to go back to problem
analysis and correct any modeling errors. Once all steps – especially validation – have been completed successfully, the
model can be developed further.
The last two steps of model-based development deal with virtual and real commissioning. Virtual commissioning is
described in chapter Virtual commissioning.
Image Fig. 8 shows the steps of model-based development.

## Page 13

BASIC INFORMATION13

3.3.2Software-in-the-loop (SiL)

During a software-in-the-loop simulation, the software that has been developed as well as a simulation environment

are executed on the same hardware. In the process the software communicates with the simulation that is also running

on the same processor. When using Automation Studio, only a PC without additional hardware is required.

It is also possible to execute the entire simulation on the subsequent target hardware. The controller is already working

in its intended environment. Unlike in reality, it is not connected to any real inputs or outputs.

Simulation toolSPS

Model

ModelController

Data processing

HMI

Figure 9: Structure of a software-in-the-loop simulation

## Page 14

14TM291 - BASICS OF VIRTUALIZATION AND SIMULATION FOR CONTROL TECHNOLOGY TM291

3.3.3Hardware-in-the-loop (HiL)

Hardware-in-the-loop systems use real-time simulation to depict a controlled system as accurately as possible for a

controller. An HiL system provides the controller with all input and output signals that would exist in the real environ-

ment. Analog and digital signals as well as bus signals between the HiL system and the controller are exchanged via

the I/O interfaces.

Simulation tool

ControllerModel

Machine emulatorSPS

ModelController

Data processing

HMI

Figure 10: Structure of a hardware-in-the-loop simulation

This method delivers the results closest to reality and already includes the influences of the real hardware.

An emulator is an electronic device that can replicate a system functionally, electrically and mechanically.

3.4Virtual commissioning

With the number of control loops in modern machinery continually on the rise, control software takes on an increasingly

prominent role in helping to ensure the functionality of these systems. In order to make the increasing complexity

of this software manageable without increasing the risk of errors, it must be tested at an early stage. Conventional

methods require the physical machine or plant for this.

Virtual commissioning enables early testing of the machine software regardless of whether the physical machine al-

ready exists. During virtual commissioning, data is imported, tested and changed on the virtual machine before the

software is transferred to the real machine. This means that early testing takes place on either an executable model

of the machine on the workstation computer or on a real-time system.

An important aspect during virtual commissioning of mechanical processes is the 3D representation that simulates

machine behavior and thus provides visual feedback for the tester. The simulation can range from individual robots and

machine islands to entire factories or manufacturing systems. This enables early verification of the control software

in order to avoid errors during actual commissioning or operation. It further allows to optimize and verify complex

correlations with regard to material flow and robot control.

The virtual commissioning of models is not only used for verifying control software and preventing errors, it also helps

to save time and money by significantly shortening the commissioning time on the physical machine. Capacity tests

can be performed easily in the office and alternative control concepts can be tested safely on the model. In addition,

virtual commissioning can help to use real test setups or prototypes more effectively.

## Page 15

BASIC INFORMATION15

Virtual commissioning is becoming more and more important in model-based development since it saves commis-

sioning time, reduces development and implementation costs, maintains process and product quality and keeps pro-

duction costs low.

However, it should be noted that virtual commissioning does not replace real commissioning. It is not possible to elim-

inate all error sources, especially because the aspect of human interaction cannot be simulated sufficiently (incorrect

cabling, changed sensor positions).

Figure 11: 3D visualization in comparison to the real machine

3.5Rapid prototyping

Rapid prototyping, or even just prototyping, is a method for developing software that delivers results within short

time. This allows to check whether or not the proposed solution is suitable.

A prototype is a piece of executable software or model that is frequently used as the basis for further developments.

Rapid prototyping works hand in hand with model-based development and simulation. The aim is to identify problems

in good time and eliminate them with little effort. The main aspect of rapid prototyping is, however, to quickly apply

changes, specifications or ideas to the software or model and directly evaluate its benefit and functionality. In this

way, impractical solutions can be identified and eliminated at an early stage.

This allows the requirements to be specified and verified during development, minimizing the risk of development

errors. In addition, unintended interactions between individual components can be detected in good time. This results

in better verification of the degree of completion and an early quality assurance.

It should be noted that despite rapid prototyping, the requirements are collected and documented in detail from the

start. This prevents the development process from being protracted.

## Page 16

16TM291 - BASICS OF VIRTUALIZATION AND SIMULATION FOR CONTROL TECHNOLOGY TM291

4Simulation levels

The following image shows the typical B&R simulation levels, which differ in their level of detail. A distinction is made

between the  level, the  level and the  level.automation hardwarecomponent and machineplant and process

The automation hardware level forms the basis offering simulation of all B&R hardware components. Automation Stu-

dio offers extensive simulation options such as Automation Runtime simulation (ARsim) or ACOPOS simulation.

At the level of components and machines, dynamic processes of machines and their subsystems can be simulated. For

this, B&R offers connections to the most popular simulation tools such as MATLAB/Simulink and MapleSim.

The plant and process level allows to simulate complex system processes such as material flow or entire production

plants. Automation Studio features interfaces for external software such as industrialPhysics and ISG-virtuos for this.

PROCESSES AND FACTORIES

MACHINES AND COMPONENTS

Automation Studio

FMU import

Automation Studio

Target for Simulink

HARDWARE

ARsim

ACOPOS simulation

Figure 12: Simulation levels: Plants and processes, machines and components, hardware

## Page 17

SIMULATION LEVELS17

4.1Automation hardware

In the following chapter, the automation hardware level is introduced. With simulation at the automation hardware

level, B&R makes it possible to develop hardware-independent applications.

Simulation toolReal hardware

VNC

ARsim

ACOPOS simulation

Motor simulation

Figure 13: Complete simulation at every level

4.1.1Automation Runtime simulation (ARsim)

The fundamental idea of integrated automation and the free scalability of automation solutions result in challenges for

the configuration tool as well as the runtime system they are based on. The Automation Runtime simulation (ARsim) is

Windows-based and corresponds to the functionality of all B&R hardware platforms and target systems, making the

development of applications hardware-independent.

Machine software

Customer technologyB&R technology

Ultrafast HydraulicsReal-time

automationEthernet

Robotics / EnergyTemperatureOpenLibrariesModel-based

CNCstandards

Automation Runtime

Figure 14: Automation Runtime - The platform for a scalable system

Basic functions of Automation Studio and Automation Runtime

Automation Studio is the configuration environment used for B&R automation components. This includes controllers,

motion control components, safety modules and HMI applications. Clearly organized project-structuring options and

the ability to manage multiple configurations ensure that teams can work together efficiently and all machine variants

can be displayed within a single project. Users can choose from a wide range of programming languages, diagnostic

tools and editors to assist them at every stage of engineering. Standard libraries provided by B&R and the integrated

IEC programming languages allow a highly efficient workflow.

Automation Runtime provides the user with a hardware-independent, multitasking and deterministic tool for creating

applications. It manages hardware and software resources and offers complete diagnostics. Extensive simulation op-

tions enable applications to be configured and tested without any hardware.

## Page 18

18 TM291 - BASICS OF VIRTUALIZATION AND SIMULATION FOR CONTROL TECHNOLOGY TM291
Real-time operating system
Functionality of ARsim
ARsim is a Windows-based simulation of Automation Runtime that has no real-time capability and corresponds to the
functionality of all other B&R hardware target systems. ARsim can be used to fully simulate all controllers, HMI appli-
cations and drives. Via a TCP/IP connection, ARsim can communicate with Automation Studio locally or via a network.
Automation Studio is usually located on the same PC that the ARsim is running on. However, it is also possible for
ARsim and Automation Studio to communicate via an external TCP/IP connection on separate PCs.
Structure of ARsim
ARsim consists of three components:
The Loader, which provides operating elements for the user and controls the start and stop of ARsim
•
The debugger, which provides the environment for diagnosing and locating errors for Automation Studio
•
The runtime system in which the user tasks are executed
•
ARsim Loader ARsim debugger
ARsim runtime system / user tasks
Figure 15: Functionality of an Automation Runtime simulation
Project management \ Simulation \ ARsim
Multiple start of ARsim
ARsim can be started several times as a Windows application. This makes it possible to simulate systems with several
controllers. To enable multiple starts, the PLC properties must be changed. The automatic shutdown of ARsim must
be disabled when closing the configuration! Each ARsim that has been started should use its own IP address for an
Ethernet online communication.
Project management \ Simulation \ ARsim \ Operation of ARsim startup \ Multiple startups of ARsim
4.1.2 ACOPOS
The ACP10SIM library is used to simulate ACOPOS axes with their functionalities on the controller. It is therefore pos-
sible to develop and test application projects with ACP10 axes without developing and testing drive hardware. The
resulting simulation project can then be transferred to the real machine with only minor modifications.

## Page 19

SIMULATION LEVELS19

Encoder

POWERLINK

Motor

Figure 16: Overview of drive configuration components

Functionality of drive communication

The drive is operated by drive parameters that are divided into parameters for hardware configuration and positioning

sequences. Data is exchanged between the controller and ACOPOS servo drive over POWERLINK. The NC Manager

serves as the link between the user application and the NC operating system located on the ACOPOS servo drive. The

ACOPOS servo drive provides the motor with setpoint values that are returned via an encoder interface.

Motion \ Project development \ Motion control \ Configuration modules

NC Init module

•

NC mapping tables

•

NC Manager configuration

•

ACOPOS parameter table

•

NC Error text table

•

Application

NC Manager

NC operating system

StatusActualvalue

NC mapping table *..ncmNC Initmodule *..ax

Encoder

Motor

ParameterMotorSet value

ACOPOS parameter Tables*..aptNC _Errortextmodule*..ett

ACOPOS

NC configuration*..ncc

PLC

Figure 17: Functionality of NC communication between PLC, ACOPOS and motor

Modes of ACOPOS simulation

## Page 20

20 TM291 - BASICS OF VIRTUALIZATION AND SIMULATION FOR CONTROL TECHNOLOGY TM291
The simulation is activated and the mode set in Automation Studio either in the NC mapping table or in System De-
signer in the configuration of a module. In the NC mapping table, the setting can be selected for each axis with one
restriction: If an axis (channel) of an ACOPOSmulti or an ACOPOS P3 is simulated, the other axes must also be simu-
lated. With System Designer, the entire module can be switched to simulation. This setting has higher priority than
the setting in the NC mapping table.
The ACOPOS simulation has three modes:
Off
•
The simulation is inactive. The real ACOPOS axis is operated via the POWERLINK interface.
Standard
•
A minimum simulation mode that includes setpoint generation but does not simulate the ACOPOS controller cas-
cade. The "simulation short circuit" is already executed prior to the position controller, which means that the set-
point is copied directly to the actual value. This mode is sufficient for many applications and requires the least
computing time. In addition, the simple model avoids error conditions such as lag errors or overtemperature. The
term "target value generation" includes basic movements, the cam automat, virtual axis and ACOPOS function
blocks (SPT).
Complete
•
Complete simulation. This mode largely corresponds to the simulation mode directly on the ACOPOS. Not on-
ly the setpoint generation but also the entire controller cascade is calculated with the model of a drive train. Ex-
tended functional units such as temperature models and DC bus modifications are also active. However, hard-
ware-dependent functions for different encoders and plug-in cards are not simulated. This mode is required if
extended functions such as torque control are used in the application. Testing certain error conditions such as
lag errors or overtemperature may also require this mode.
If the simulation has been activated, an ACP10SIM axis takes over the function instead of the ACOPOS. The correspond-
ing setting is transferred to the ACP10SIM axis via parameter CMD_SIMULATION during startup of ACP10-SW. Switch-
ing the simulation mode in the application on and off afterwards only results in setting or resetting the corresponding
status bits.
Motion \ Reference manual \ ACOPOS function blocks \ Simulation

## Page 21

SIMULATION LEVELS21

4.1.3Function blocks of library MCSimIf

Library MCSimIf contains function blocks that read the required data of an axis for a load simulation on the controller

or transfer the result of the load simulation to the axis. Function blocks of library MTLoadSim can be used for a load

simulation on the controller.

ACP10_MCMC_Sim_IF

Control softwareACOPOS simulationMachine model

ProgramProgramProgram

ProgramProgram

HMIAlarmsUsers

Figure 18: Components of communication with the MCSimIf library

This library is an extension of library ACP10_MC. For this reason, the function blocks of this library can

only be used if library ACP10_MC is used to operate the axes.

Library MCSimIf consists of four function blocks.

MC_BR_ReadLoadSimTorque

•

This function block is used to make the air gap torque calculated on the ACP10SIM axis cyclically available. This

air gap torque theoretically acts between the stator and rotor of an electric motor.

MC_BR_WriteLoadSimTorque

•

With this function block, the simulated torque can be transferred to the axis.

MC_BR_WriteLoadSimPosition

•

This function block transfers the simulated position of the rotor to the ACP10SIM axis used.

MC_BR_ReadLoadSimInputData

•

With this function block the value position, speed and acceleration can be read cyclically from the axis.

Programming \ Libraries \ Motion libraries \ MC_SimIf

4.1.4Diagnostics in simulation

Automation Runtime simulation supports a variety of diagnostic tools that provide information about the current

system state. A list of different diagnostic options for Automation Runtime simulation is provided below. The use of

the integrated diagnostic tools is documented in Automation Help. For further explanations and exercises, see training

module "TM223 - Automation Studio Diagnostics".

## Page 22

22 TM291 - BASICS OF VIRTUALIZATION AND SIMULATION FOR CONTROL TECHNOLOGY TM291
Diagnostics and service \ Diagnostic tool
Status bar
•
Information about the target system
•
System Diagnostics Manager
•
Monitor mode \ Online comparison
•
Logger
•
Profiler
•
Motion trace
•
Variables oscilloscope (Trace)
•
Variable monitor (Watch)
•
Network command trace
•
Diagnostics and service \ I/O and network diagnostics
Programming \ Libraries \ Configuration, system information, runtime control \ ArProject

## Page 23

SIMULATION LEVELS23

4.2Components and machine simulation

In the following chapter, the component and machine level is introduced. B&R offers interfaces for various external

simulation and modeling tools. These include Automation Studio Target for Simulink, which provides a direct interface

between Automation Studio and MATLAB®/Simulink®, and the Functional Mock-up Interface (FMI), which is an open

standard supported by numerous tools on the market.

Real HardwareSimulation tool

VNC

ARSim

External development tools

ACOPOS Simulation

Motor Simulation

Figure 19: Simulation on all levels

4.2.1Functional Mock-up Interface (FMI)

The Functional Mock-up Interface FMI describes a standardized interface that is implemented in an executable unit

called  and was designed for the communication between simulation models and sim-Functional Mock-up Unit (FMU)

ulation tools. It is possible to use FMUs on industrial hardware from B&R using FMU imports in Automation Studio.

Single or multiple FMUs can thus run on the industrial target system in combination with other programs and exchange

data. An FMU can either contain its own solver  or require a simulation environment (co-simulation)(model exchange)

in order to perform numeric calculations.

FMI model exchange

This variant makes it possible for a modeling or simulation environment to generate the C code of a dynamic model that

can be executed in other modeling and simulation tools. Models are defined using differential, algebraic and discrete

equations with time, status and step events. If the C code describes a continuous system, this system will be solved

using integrators of the environment currently being used.

FMI for co-simulation

The goal of FMI for co-simulation is to provide an interface standard for coupling simulation tools in co-simulation

environments. The exchange of data between subsystems is done using limited communication points. In the time

between communication points, all subsystems are solved independently of one another by the separate integrated

solver. A higher-level algorithm regulates the communication between individual FMUs and other programs. This high-

er-level algorithm is not part of the FMI standard.

## Page 24

24 TM291 - BASICS OF VIRTUALIZATION AND SIMULATION FOR CONTROL TECHNOLOGY TM291
Limitations
AS only supports FMUs with version 2.0 of the FMI standard.
•
Only co-simulation is supported at this time (model exchange not supported).
•
AS only supports source code FMUs (no precompiled .dll files, etc.)
•
The import mechanism was tested using the online examples from MapleSoft MapleSim* and DASSAULT
•
SYSTEMS DYMOLA**. FMUs exported from MapleSim can be imported and used without limitations. FMUs from
Dymola can be used with limitations (for more information, please contact B&R Support). FMUs from other tools
have not been tested.
IN/OUT data types must be of type REAL (8 bytes, C/C++ double).
•
For further explanations and exercises, see training module "TM291 - Functional Mock-up Interface".
Project management \ Simulation \ Functional Mock-up Interface (FMI)
4.2.2 Automation Studio Target for Simulink
Automation Studio Target for Simulink is an add-on toolbox for Simulink that serves as the interface between MAT-
LAB®/Simulink® and Automation Studio. The simulation and development tool MATLAB®/Simulink® reduces the time
for developing a model and receiving a high-quality source code to just a few minutes. Languages C and C++ are sup-
ported. With Automation Studio Target for Simulink, the automatically generated code can be integrated into an Au-
tomation Studio project as a program organization element (POU) or function block.
Generating a program organization unit generation vs. generating a function block
With Automation Studio Target for Simulink, it is possible to generate a program organizational element – also called
"task" – from a Simulink model or a function block in Automation Studio. During function block generation, a user library
is created that contains the Simulink model as a function block. The function block can then be used as an instance in
various tasks. By generating a program organization element, however, a program is created from the Simulink model
that provides direct access to all global variables, functions and function blocks. Programs have attributes "_CYCLIC",
"_INIT", or "_EXIT". These determine whether it is the cyclic program part, the program part to initialize or the program
part to uninstall.
Programming \ Programs
Programming \ Functions and function blocks
B&R Automation Studio Toolbox
Automation Studio Target for Simulink Toolbox offers a selection of B&R Simulink blocks. These form the interface
between the C code and Automation Studio. There is a configuration block for defining the most important variables
for code generation in Automation Studio, and there are ten blocks for defining inputs and outputs in the VAR file and
structures in the TYPE file of Automation Studio.
For further explanations and exercises, see training module "TM293 - Automation Studio Target for Simulink".
Automation software \ Software installation \ Automation Studio Target for Simulink
4.2.2.1Requirements
The following software is required to use B&R Automation Studio Target for Simulink®:

## Page 25

SIMULATION LEVELS 25
B&R Automation Studio Target for Simulink® (ASTfS)
•
MATLAB® and Simulink® latest version up to version 2012b starting with ASTfS version 5.0
•
MATLAB® and Simulink® version 2007a up to version 2012b with ASTfS version 4.5.0
•
MATLAB Coder
•
Simulink Coder® or Embedded CoderTM
•
C++/C compiler: Depending on Matlab®/Simulink® version, e.g. SDK7.0 or Visual Studio
•
Automation Studio version 3.0.90 or later is required in order to transfer the code directly to Automation
Studio during code generation.

## Page 26

26 TM291 - BASICS OF VIRTUALIZATION AND SIMULATION FOR CONTROL TECHNOLOGY TM291
4.3 Process and plant simulation
In the following chapter, the plant and process level will be introduced. Plant and process simulation is used for devel-
oping process engineering plants and optimizing technical and logistical processes. A distinction is made between
static and dynamic process and plant simulations.
In the past, conventional simulation tools in this field were used with the aim to display stationary processes in the
simulation. The system would be displayed at an exact time when being in equilibrium. The resulting model was time-
independent.
Now, dynamic process simulation is used in addition to static process simulation. In a dynamic process simulation, the
system is displayed at different points in time. Changes in the system can therefore be identified and analyzed based
on system activities. However, real-time analysis results in a considerably higher computing power.
Process and plant simulation is an important tool for analyzing process units individually or when combined into com-
plex overall processes. The resulting mass and energy efficiencies allow a deeper understanding of the dependencies
and interactions of different process steps. The simulation results play a key role in the development, planning and
optimization of processes. If necessary, new process units are developed and integrated into the existing simulation.
Physics engine
Plant and process simulation is the basis for virtual commissioning. With a suitable software program, all components
of the plant and process simulation can be calculated and displayed in the best possible way. A physics engine is re-
quired for realistic simulation of movement sequences as well as collision detection and behaviour, especially with
regard to real-time simulation of production and filling processes. A 3D user animation is usually added, which enables
virtual commissioning for the developer.
Examples of physics engines:
Kinematics (e.g. robot kinematics, gripping)
•
Actuators (e.g. conveyor drives, axis drive)
•
Sensor technology (e.g. light barriers, tactile sensors, image recognition)
•
Rigid body physics (e.g. friction, mass / inertia)
•
A carefully considered process and plant simulation helps to understand the planning of complex processes. In this
way, model variants can be tested in advance and different approaches to time and cost can be analyzed and com-
pared. Emergencies or bottleneck situations can also be simulated and solutions developed. This avoids damage and
unnecessary costs.

## Page 27

SIMULATION LEVELS27

4.3.1Integration of third-party tools

In order to display the simulation of processes and plants graphically and three-dimensionally, special software is

required. This means that entire buildings including all dimensions as well as processes and structures involved can

be calculated in detail.

By integrating third-party tools such as ISG-virtuos or industrialPhysics into Automation Studio, workflows become

more efficient.

Figure 20: 3D visualization and simulation of a plant in industrialPhysics in combination with Automation Studio

## Page 28

28TM291 - BASICS OF VIRTUALIZATION AND SIMULATION FOR CONTROL TECHNOLOGY TM291

4.4Model-based development with B&R

Automation Studio is the hub for model-based development with B&R. Hardware configuration, software develop-

ment, model development and simulation come together in Automation Studio, providing various development op-

tions for implementing a machine solution.

On the one hand, a wide range of software development options are offered that provide standardized software

blocks, industry-specific Technology Packages and mapp Technology.

On the other hand, there are interface connections for third-party tools, such as modeling and simulation software,

that create an even larger variety of development options.

Automation Studio features a complete simulation for the controller, HMI application, drive and motors. In essence,

all components of an integrated automation solution from B&R can be simulated.

Real hardwareSimulation tool

VNC

Technology software

ARsim

Control software

ACOPOS simulation

External development tools

Motor simulation

B&R offers a comprehensive product portfolio that allows model-based development at different levels depending on

the requirements.

## Page 29

SIMULATION LEVELS29

4.4.1Modeling and simulation with B&R

The modeling of components, machines, plants and processes takes place using modeling software. The third-party

tools either provide an interface to Automation Studio themselves or an interface is offered. The models are integrated

in languages C, C++ or Structured Text as function blocks in a user library or as programs in an existing Automation

Studio project.

The Project Explorer in Automation Studio plays a central role in model-based development. The Project Explorer con-

tains the Logical View for organizing software, the Physical View for organizing hardware and the Configuration View

for managing configurations of multiple machine variants in a single project.

Program organization units (POU) are arranged in the Logical View in a tree structure. The complete logic of the soft-

ware is managed in the Logical View accordingly, be it the controller, the model, the user libraries or the general pro-

grams.

The hardware required for the machine is managed either in a hierarchical view, the Physical View or in a graphical view

(System Designer). Hardware management features in Automation Studio are used to perform the following tasks:

Inserting and configuring hardware modules

•

Mapping variables to I/O data points

•

Configuring fieldbus modules and interfaces

•

Different hardware configurations can be managed in the Configuration View. The configurations typically differ in

terms of software scope and the exact hardware used. Different variants of a machine type can be created in the

Configuration View or exist in addition to the real hardware configuration of a simulation configuration. With a dou-

ble-click, you can switch between the different configurations and view the hardware assigned to the configuration in

System Designer as well as in the Physical View. Only one configuration can be active at a time.

For more detailed information about the Project Explorer, see "TM210 Working with Automation Studio"

or in Automation Studio Help:

Project management \ Logical View

Project management \ Hardware management

Physical view

•

System Designer

•

Project management \ Configuration View

The figure below shows the Configuration View with different configurations of a machine and a simulation configu-

ration.

Simulation

Panel-based

X20 and VNC

Figure 21: Different configurations can be managed at the same time in one project.

## Page 30

30TM291 - BASICS OF VIRTUALIZATION AND SIMULATION FOR CONTROL TECHNOLOGY TM291

As already mentioned, Automation Studio offers a complete simulation for any hardware. This allows different simu-

lation levels to be created in the Configuration View.

If it is not possible or desirable to operate the actual motor on the machine, it can be simulated instead. Movement

profiles can be carried out on the controller or PC, even if the entire drive system is not available.

The platform-independent Automation Runtime system allows control programs to be created and tested directly on

the PC. This function is also available for the safety application. Control applications can be executed in slow motion

or time lapse in order to hone in on different phases of the machine's lifecycle.

Integrated VNC and web server functionality makes it possible to operate HMI applications not just remotely, but also

directly on the PC.

The integrated WinIO interface makes it possible to fully simulate I/O points.

Simulation of a controller can be started by selecting the simulation icon in Automation Studio. All control programs

run directly on the PC. This means that all of the software functions in the control application can be configured and

tested independently of the hardware. When you switch to simulation mode, the project is rebuilt, the simulation

environment is automatically started and an online connection to Automation Runtime Simulation is established.

In model-based development, 3D simulation plays a particularly important role for virtual commissioning. A 3D simu-

lation allows to simulate the behavior of the machine and thus provides visual feedback in addition to early testing. A

3D simulation is usually developed from the model – if supported by the model development software – or is created

in addition to model development via simulation software and can then be connected to Automation Studio via a TCP/

IP connection. The model itself is used as a function block or program in the ARsim simulation.

Figure 22: Integrating a 3D visualization into Automation Studio including simulation

## Page 31

SIMULATION LEVELS31

4.4.2Software-in-the-loop with B&R

Software-In-the-loop means that both the developed software and a simulation environment run on the same hard-

ware.

In combination with Automation Studio, a fast and simple software-in-the-loop solution on the PC without hardware

in the office is possible.

A software-in-the-loop variant can also be implemented directly on the target hardware, an X20 controller or on an

industrial PC.

The communication between the developed software and the simulation model takes place via global variables or a

variable mapping.

ModelController

Data processing

HMI

Figure 23: Structure of a software-in-the-loop simulation

## Page 32

32TM291 - BASICS OF VIRTUALIZATION AND SIMULATION FOR CONTROL TECHNOLOGY TM291

4.4.3Hardware-in-the-loop with B&R

Hardware-in-the-loop systems use real-time simulation to display a controlled system as accurately as possible for a

controller. In a HiL system, the developed software and the model run on real hardware. Analog, digital and bus signals

are exchanged between the model and the developed software via the I/O interfaces.

In combination with Automation Studio, a HiL system can also be solved in an Automation Studio project. A configu-

ration is created for each target system in the Configuration View, to which the corresponding software programs are

then assigned. Either an X20 controller or an industrial PC can be used as hardware.

The communication between the developed software and the simulation model takes place ideally via the I/O mapping

of the POWERLINK interface.

ControllerMachine emulator

Figure 24: Structure of a hardware-in-the-loop simulation

4.4.4Advantages of model-based development

Model-based development, as well as working with simulation and virtual commissioning, offers a number of advan-

tages in software and machine development.

Accelerated time to market

•

High product quality

•

High efficiency and productivity

•

Space and time for innovation

•

Rapid prototyping

•

Shortened development test cycle

•

Early error diagnostics

•

Possibility to analyze extremely extensive or complicated systems

•

More precise adjustment when presetting system parameters

•

Ability to test functionality before commissioning, reducing startup time

•

Developing applications with no machine or only parts of drive hardware

•

## Page 33

SIMULATION LEVELS 33
Classic development
Mechanic Electric Software
Project start
Project completion
Model-based development
Mechanic
Shortened time to market
Electric
Software
Project start Project completion
Figure 25: Parallel development using simulation

## Page 34

34TM291 - BASICS OF VIRTUALIZATION AND SIMULATION FOR CONTROL TECHNOLOGY TM291

5Summary

In the age of industry 4.0, modeling, simulation and virtual commissioning make up an integral part of automation

technology. With unlimited connectivity between development components, simulation facilitates a high degree of

flexibility and efficient resource management. These qualities ensure optimal utilization of development resources

and reduce startup times by up to 80%.

ControllerDevelopment

HMISimulation

Motion controlCommissioning

Safety technologyDiagnostics and

Service

Figure 26: Automation Studio - One engineering tool for the machine's entire lifecycle

Based on the seamless integration of modeling, simulation and virtual commissioning at all levels in Automation Stu-

dio, B&R offers a comprehensive platform for model-based development.

## Page 35

AUTOMATION ACADEMY35

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

V2.0.0.1 ©2023/09/27 by B&R, All rights reserved.

All registered trademarks are the property of their respective owners.

Subject to technical changes without notice.