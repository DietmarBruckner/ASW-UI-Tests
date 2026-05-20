## Page 1

TM920

Diagnostics and service

## Page 2

2 DIAGNOSTICS AND SERVICE TM920
Prerequisites and requirements
Basic technical training
General
Access to www.br-automation.com.
Automation Runtime 4.33
Software
Windows 10
X20 system user's manual - Version 3.50 (October 2018)
Installation / EMC guide - Version 1.20 (June 2018)
ACOPOSmulti user's manual - Version 1.10 (April 2018)
Documentation Redundancy for control systems - Version 1.14 (September 2017)
ACOPOSinverter P74 user's manual - Version 2.60 (December 2018)
Integrated Safety Technology user's manual - Version 1.120 (July 2017)
SafeMOTION user's manual - Version 4.5 (November 2018)

## Page 3

TABLE OF CONTENTS 3
Table of contents
1 Introduction.........................................................................................................................................................4
1.1 Learning objectives..............................................................................................................................4
1.2 Safety notices and symbols...............................................................................................................4
1.3 Terminology...........................................................................................................................................5
2 Situation analysis and problem identification.............................................................................................7
2.1 Checking possible sources of error..................................................................................................7
2.2 Source of information: Machine manufacturer...........................................................................10
2.3 Source of information: B&R.............................................................................................................10
3 System overview and topologies..................................................................................................................13
3.1 B&R system overview.........................................................................................................................13
3.2 Typical topologies..............................................................................................................................13
4 Obtaining information about the B&R system..........................................................................................17
4.1 Serial numbers and model numbers..............................................................................................17
4.2 Data sheets and user's manuals.....................................................................................................19
4.3 Structure of B&R user's manuals....................................................................................................19
4.4 B&R website functions.....................................................................................................................20
5 Diagnostic tools...............................................................................................................................................27
5.1 Diagnostics without a PC.................................................................................................................27
5.2 Diagnostics via PC.............................................................................................................................31
6 Diagnostics using the System Diagnostics Manager..............................................................................33
6.1 SDM functions....................................................................................................................................34
6.2 Establish a connection to SDM.......................................................................................................37
6.3 Saving Logger files............................................................................................................................38
6.4 Generating and saving a system dump........................................................................................39
6.5 Information in the hardware tree..................................................................................................40
6.6 Diagnostics via I/O and status data points................................................................................43
6.7 Information about the application status in WebXs..................................................................46
7 Troubleshooting...............................................................................................................................................47
7.1 Service parts........................................................................................................................................47
7.2 Replacing a module...........................................................................................................................48
7.3 Starting up again and functional testing.....................................................................................56
8 Backing up and restoring..............................................................................................................................59
8.1 Backing up and restoring using the Runtime Utility Center.....................................................59
8.2 Access to the integrated FTP server.............................................................................................68
9 Summary............................................................................................................................................................69
10 Appendix..........................................................................................................................................................70
10.1 Online connection cable..................................................................................................................70
10.2 Where SDM and Runtime Utility Center can be used...............................................................70
10.3 Changing network settings on the PC.........................................................................................71
10.4 Notes regarding grounding and shielding.................................................................................72
10.5 Power supply and protection........................................................................................................74

## Page 4

4DIAGNOSTICS AND SERVICE TM920

1Introduction

For decades, the company B&R Industrial Automation GmbH has been a leader in innovation and full-line supplier of1

control technology in the field of mechanical engineering.

Our customers are machine and system manufacturers who use B&R products for automation. The machine and sys-

tem manufacturers are generally responsible for software programming, maintenance, spare parts supply and war-

ranty claims.

Figure 1: Examine surroundingsFigure 3: Correct errors

Figure 2: Contact manufacturer

The B&R website offers all of the documentation and user's manuals for our products as well as a complete product

overview.

Further information

4.4 "B&R website functions" on page 20

•

1.1Learning objectives

This training module provides an overview of the B&R system and corresponding documentation resources. It also

provides an overview of available diagnostic options. Many exercises and application examples included serve to ex-

pand upon the lessons learned.

Participants will learn about B&R's approach to integrated automation and where to find necessary sources of in-

•

formation.

Participants will learn their way around the B&R website and find out how to download user's manuals and data

•

sheets.

Participants will be familiar with the diagnostic options that are available without using a PC.

•

Participants will learn how to perform basic diagnostics without a PC and how to interpret the resulting data us-

•

ing the available data sheets and user's manuals.

Participants will learn about the diagnostic options that require a PC and how to evaluate the information they

•

provide.

Participants will learn how to replace modules and resume operation of the B&R system.

•

Participants will learn who to contact to obtain specific information.

•

Participants will know the possibilities for backing up and restoring data

•

Participants will learn how to backup and restore the values of process variables on a B&R controller.

•

1.2Safety notices and symbols

Safety notices in this manual are organized as follows:

Disregarding these safety guidelines and notices can result in severe injury, death or substantialDanger:

damage to property.

1www.br-automation.com

## Page 5

INTRODUCTION5

Disregarding these safety guidelines and notices can result in severe injury or substantial dam-Warning:

age to property.

Disregarding these safety guidelines and notices can result in injury or damage to property.Caution:

These instructions are important for avoiding malfunctions.

Additional notices and information in this manual are organized as follows:

Provides important tips and additional information.Note:

References additional documentation. (Automation Help, data sheets, user's manuals)Help:

Example:

Hardware \ Motion control \ <Device> \ Technical data \ (<Type>) \ Status indicators23

An example illustrates the topic in greater depth.Example:

The result of a completed task is summarized briefly.Result:

Organization of safety notices in external manuals:

This manual contains references to other manuals. How safety notices are organized in external manuals is listed in

the respective manual.

Exercise: Task definitions & exercises

Sections marked with an orange stripe on the left side contain information about exercises as well as the associated

actions to be taken. The exercises are designed to provide a deeper understanding of the information provided.

1.3Terminology

Below is a glossary of the most important terminology used in B&R systems. The definitions

are of particular importance because these terms are used frequently throughout this docu-

mentation as well as in the respective user's manuals.

TermShort description

ACOPOSGeneral name for B&R drives. These power electronics are controlled via a fieldbus connec-

tion and used to control the movement of all types of motors.

Motion controlUmbrella term for anything involving movement. Power electronics controls the movement

of drive systems, such as synchronous, induction and stepper motors.

Table 1: Terminology

2Angle brackets indicate variable placeholders "<...>"

3Parentheses indicate optional entries "(...)"

## Page 6

6 DIAGNOSTICS AND SERVICE TM920
Term Short description
Automation Runtime Automation Runtime is the operating system installed on the controller. Different versions
of Automation Runtime installed on the controller offer different functions and diagnostics
options.
APC / IPC Automation PC or Industrial PC: PC designed for industrial use.
CF CompactFlash is the external memory of the controller. Almost all B&R control systems use
a CompactFlash card as an application memory. Other systems that do not use Compact-
Flash cards have the application memory integrated in the controller, and it cannot be re-
placed externally. Using CompactFlash cards provided by B&R ensures image compatibility
and suitability for industrial environments.
Remote I/O Instead of being connected directly to the CPU, remote I/O modules can be operated re-
motely via a fieldbus system.
Node number The node number is set on the module via rotary switches. This number is required to iden-
tify the module in the network.
Panel HMI devices are often described as operator panels or simply panels.
POWERLINK Open, Ethernet-based fieldbus for connecting controllers, drives, safety technology and re-
mote I/O modules.
Safety General term for safety technology in the field of machine manufacturing. The safety com-
ponents process safe input and output data, such as emergency switch-off, enable signals,
safety doors and much more.
System 2000 General term referring to B&R's 2003, 2005 and 2010 control systems.
Technology Guard This is a USB dongle that is inserted in the controller CPU's USB port. The Technology Guard
contains any required software licenses, two operating hours counters and permanent data
storage.
HMI Term referring to the representation of processes and process values. An HMI device (e.g.
with touch screen) can be used to interact with the visualized machine functions.
X2X Link Connects controller CPUs to X20 or X67 components. Serves as a remote backplane for
transferring I/O data to the bus controller or CPU.
X20 Components with IP20 protection4. Complete control system with CPUs, remote I/O mod-
ules and broad fieldbus support.
X67 Components with IP67 protection5. B&R's remote I/O system.
Table 1: Terminology
4 IP20 = Protection against ingress of solid foreign bodies >12.5 mm and no protection against ingress of water. Source: IEC/EN 60529
5 IP67 = Protection against dust and temporary immersion in water

## Page 7

SITUATION ANALYSIS AND PROBLEM IDENTIFICATION7

2Situation analysis and problem identi-

fication

Before contacting the manufacturer of the control electronics or the machine

manufacturer, it is necessary to get an overview of the system to isolate and

localize the problem. Working one step at a time and documenting each step

along the way is the most productive approach.

It is often external influences that occur with daily use of the machine that

cause the smaller problems. The following contents help to identify problems

and analyze the current situation.

If the problem is recognized correctly, it is easier to initiate the next steps. In addition, the time required to solve the

problem can be significantly reduced.

2.1Checking possible sources of error

Acquiring as much information as possible for a clear overview is the first line of action.

The general conditions under which the machine is operated are crucial factors as is the timing of actions performed

by the machine operator.

"Has anything been changed on the machine? Were the actions taken also documented?"

This information helps to isolate the cause of error step by step and helps develop a solution.

2.1.1Operating materials

Operating materials are crucial to the lifespan of a machine. Neglecting these materials can lead to machine downtime

or have negative effects on functionality.

Operating materials include:

Lubricant

•

Compressed air

•

Hydraulic fluid

•

Coolant

•

Electricity

•

2.1.2Mechanics

The mechanics in a machine are often subject to demanding conditions. Years of operation will eventually lead to wear

and tear.

Typical problems

Worn bearings6

•

Poor lubrication

•

Warping

•

Increased play (worn toothed gears, gear backlash)

•

Belt tension on the drives not as specified by the manufacturer

•

Damage caused by jolts and vibrations

•

6The ability to predict machine and system failures before they happen reduces costs and increases availability.Condition monitoring modules from B&R precisely detect poten-

tial cases when service work may be needed.

## Page 8

8DIAGNOSTICS AND SERVICE TM920

2.1.3Power supply, supply network

First the electricity supply makes machine operation possible. A stable supply network is a requirement for permanent

and reliable machine operation.

Potential problems with the supply:

Voltage fluctuations

•

powerfail message

•

Phase failure

•

Defective or weak fuse

•

Differences in potential

•

Uneven network load

•

2.1.4Wiring, cabling, shielding

Sensors and actuators are connected to the I/O system through wiring. Likewise, all remote control components, such

as the controller itself, are connected with higher-level fieldbuses or networks.

Potential wiring problems:

Open lines

•

Poor contact

•

Unplugged connector

•

Unsuitable cable

•

Bus, sensor and motor cables with poor or no shielding7

•

Poor or worn insulation

•

Short circuits and cross faults

•

Ground fault

•

Insufficient flex radius

•

Coupling of disturbances caused by inappropriate shielding

•

Excessive cable lengths (voltage drop and reflections)

•

Missing terminating resistors during network coupling (e.g. CAN Bus)

•

Where can I find information aboutwiring, shielding and grounding concepts?

Descriptions regarding wiring, shielding and grounding concepts for the B&R system can be found in

the respective B&R user's manual. Connection examples for individual hardware components are also

included.

Further information

4.3 "Structure of B&R user's manuals" on page 19

•

2.1.5Environmental and time factors

In addition to other defects that can occur due to damaged mechanics or wiring, environmental factors and the effect

of time are also causes of errors.

This includes the following influences:

High and low temperatures, temperature fluctuations (temperature limit values are documented in the user's

•

manual)

Heat accumulation from incorrect installation position or from minimum spacing not being observed

•

Humidity and condensation, incorrectly configured climate control systems

•

Dust and dirt

•

Oily or aggressive vapors in the air

•

Vibrations

•

Aging, material fatigue

•

UV radiation

•

Derating due to elevation (specification listed in the user's manual)

•

710.4 "Notes regarding grounding and shielding" on page 72

## Page 9

SITUATION ANALYSIS AND PROBLEM IDENTIFICATION 9
Where are the limit values listed?
All limit values, such as ambient temperature or humidity, for operating the B&R product can be found
in the respective user's manual.
Further information:
4.3 "Structure of B&R user's manuals" on page 19
•
2.1.6 Process parameters
Modern machines are capable of performing flexible manufacturing processes for a wide range of products. Settings
and configurations make it possible to adapt the machine to the production process. Process-related variables affect
the machine's response.
Influences for the machine:
Changed parameters
•
Changed controller settings
•
Change in behavior by the machine operator
•
New recipe loaded
•
Changed limit values
•
Different raw material or product
•
Different material or temperature
•
Higher or lower material hardness
•
Other tool properties
•
2.1.7 Controller electronics
The smooth operation of electronics is guaranteed for certain general conditions. These limits are defined in stan-
dards. The potential sources of error described above may lead to the failure of an electronic component. It is impor-
tant to detect any defects so that the necessary measures for maintenance or replacement can be taken.
Further information
2.2 "Source of information: Machine manufacturer" on page 10
•
5 "Diagnostic tools" on page 27
•
7 "Troubleshooting" on page 47
•
2.1.8 Software, process
The software on the machine controller represents and controls complex machine processes. Software errors do not
simply appear without reason, but occur mostly when process values have changed and the corresponding software
function has not been adequately tested.
If limit values are exceeded without consideration, this represents a potential source of error.
Potential sources of errors:
All of the factors described above
•
Faulty programming, insufficient software tests
•
Limit values are exceeded due to incorrect entries but not regarded in the software
•
Combinations of different machine functions were not completely tested
•
Program for sequential control doesn't take account of all machine conditions
•

## Page 10

10DIAGNOSTICS AND SERVICE TM920

2.2Source of information: Machine manufacturer

Required information for machines should be obtained ideally from the ma-

chine manufacturer. The following list provides an overview of the informa-

tion that can be requested from the machine manufacturer or that can be

found directly on the machine.

The following information is available:

Images on the HMI application

•

Overview pages

°

Alarm pages

°

Diagnostic pages8

°

Error logs, log files

°

Trend curves

°

Manual of the machine

•

Machine data

°

Error descriptions

°

Suggested corrective measures

°

Maintenance guidelines

°

Lubrication chart of mechanical parts

°

Tightening torque for belt tension

°

Contact data

°

Circuit diagrams of the machine

•

Website of the machine manufacturer

•

Contact the machine manufacturer

If replacement parts or service is needed, the first point of contact is always the machine manufacturer. They know

the machine's structure, the mechanics, electronics and application software.

Contact data can usually be found in the control cabinet or in the user's manual. Most manufacturers have their own

website that also contains contact data.

When contacting the machine manufacturer, make sure you have the machine number and a clear de-

scription of the problem prepared.

2.3Source of information: B&R

B&R's systems are completely documented and operated the same way. A broad range of diag-

nostics options provide information about the system status and help to perform initial diag-

nostics and error localization.

Further information:

4 "Obtaining information about the B&R system" on page 17

•

5 "Diagnostic tools" on page 27

•

Checklist for contacting B&R

In some rare cases, the machine manufacturer may no longer be in business. In this case, be sure to contact the nearest

B&R subsidiary. You can always find contact information for the nearest subsidiary on the B&R website:

www.br-automation.com  "Company"/"Locations"→

There are a few things to clear up before contacting the manufacturer or B&R. The following checklist can be used as

a guide when preparing to contact B&R.

8System Diagnostics Manager is a web-based interface that is used for diagnosing B&R systems. The contents of System Diagnostics Manager can be directly integrated in the

HMI application by the machine manufacturer, see:6 "Diagnostics using the System Diagnostics Manager" on page 33.

## Page 11

SITUATION ANALYSIS AND PROBLEM IDENTIFICATION11

QuestionNotes

Company name, location, contact data

Machine manufacturer?

Packaging machine, pump controller, etc.

Machine type?

Brief description of the production process

What is being produced?

Description of the type of problem: Standstill, noises, product quality, com-

What type of problem is oc-

munication failure, etc.

curring?

Approximate frequency of problem:

How often does the problem

Once a day, every hour, just once, etc.

occur?

Started: "... last Wednesday at ..."

When did the problem first

occur?

Is the behavior repetitive? Under what conditions does the behavior occur?

Reproducibility

Under what environmental conditions (temperature, humidity, etc.) is the ma-

Environmental conditionschine being operated. Installation of electromagnetic compatibility (EMC)

Different product being produced, mechanical modifications, changes to ca-

Has anything been changed?bling, software update from the manufacturer, changed parameters, etc.

Contact person, documentation of the changes, reasons for changes, etc.

Who made the changes?

Hardware being used, control concept, etc.

What controller is being

used?

How the components and modules are connected together, etc.

What fieldbus is being used?

Have a topology diagram ready, state the important components, etc.

What does the topology look

Further information:

like?

3.2 "Typical topologies" on page 13

•

What states are being signalized by the LED status indicators? (Controller,

LED status indicatorsI/O modules, interfaces, bus controller, drives, etc.)

Who is responsible for theControl program for the machine controller, source control system, archiving

software?

Are there any backups?

Table 2: Checklist for B&R contact

## Page 12

12DIAGNOSTICS AND SERVICE TM920

QuestionNotes

Generate system dump according to the instructions and have it ready. All

hardware modules and software versions used as well as Logger and profiler

files are saved in the system dump.

System dump

Further information:

6.4 "Generating and saving a system dump" on page 39

•

Every B&R module has a serial number. The serial number provides important

data and information.

Which parts of the machine and which hardware is affected?

serial numbers

Further information:

4.1 "Serial numbers and model numbers" on page 17

•

Table 2: Checklist for B&R contact

Exercise: Preparing the B&R checklist

In this exercise you will draw up a brief list to determine whether all of the relevant data is available for an existing

machine. If you're already certain that some important information is missing or cannot be determined, efforts can

be made to obtain this information.

1)Go through the checklist for a machine one step at a time

2)Write down information that is already known

3)Note down the information that must be obtained later

4)Clarify whether the offered service and diagnostics methods are supported

## Page 13

SYSTEM OVERVIEW AND TOPOLOGIES13

3System overview and topologies

3.1B&R system overview

B&R offers products for every aspect of a machine's electronics. Controllers, HMI units, I/O modules, motion control

technology and a variety of infrastructure components such as cables, power supplies and network components make

up the B&R product portfolio.

B&R products are divided into the following main categories:

Control technology

•

Motion control

•

Safety technology

•

HMI

•

B&R product overview

The following graphic shows an excerpt of B&R's most important product groups. On the left you see some motion

control components. Control technology, remote I/O modules and integrated safety technology are shown front and

center. In the back and on the right you see an operator panels and industrial PC.

Figure 4: B&R product overview (from left to right: synchronous motor, ACOPOSmulti system, X20 system and integrated safety technology (yellow),

Automation Panel, Power Panel, Automation PC)

Where can I find detailed product information?

For a detailed overview and description of the individual product groups, refer to the B&R website at

www.br-automation.com in section "Products".

The product catalogs for the corresponding product groups can be downloaded in the "Downloads" area

by selecting "Catalogs and Brochures" / "Products" / "Product group".

Further information:

4.4 "B&R website functions" on page 20

•

3.2Typical topologies

If the problem can be solved by replacing a module, the existing topology must be examined first. The following image

shows a typical control topology as those found on many other machines.

In the center you see the machine controller. It is connected to the other control components via a fieldbus, most

commonly POWERLINK. This is what allows the machine controller to control the remote components.

## Page 14

14DIAGNOSTICS AND SERVICE TM920

An X20 CPU, an HMI device or an industrial PC can be used as the machine controller. Above this, HMI devices or other

network components can also be connected via a network connection.

Figure 5: Typical control topology with an X20 CPU

3.2.1Expansion via fieldbus

Communication with 3rd-party devices via different fieldbus systems is a basic feature of B&R systems. The fieldbus

that is used to connect a device can be connected to various positions in the system.

Fieldbus connection to the control CPU

All B&R control systems offer additional slots for fieldbus

interfaces, which allows them to be expanded easily at

any time without having to add new interfaces. A wide

range of fieldbus interfaces are available for connecting9

different devices.

Figure 6: Connection of X20 I/Os via POWERLINK in

addition to two other fieldbuses (blue and green)

9A few examples of fieldbus systems include: POWERLINK, Profibus, DeviceNet, etc.

## Page 15

SYSTEM OVERVIEW AND TOPOLOGIES15

Fieldbus connection to POWERLINK

An expandable bus controller allows B&R to also offer

fieldbuses right at the level of the I/O system. The addi-

tional fieldbus interface is connected right next to a POW-

ERLINK bus controller. This makes it possible to add a

fieldbus interface anywhere on the machine.

Figure 7: Additional fieldbus interface

next to the POWERLINK bus controller

Remote backplane - X2X Link

Multiple I/O stations consisting of X20 and X67 modules

can be connected via X2X Link.

The maximum distance between two modules is 100 me-

ters. Up to 254 modules are supported per line.

Figure 8: X20 and X67 modules can be connected to the CPU via X2X Link

or POWERLINK. X20 and X67 modules are linked together using X2X Link.

3.2.2Redundancy

The B&R system offers a variety of redundancy options in order to maximize the availability of machines. When using

POWERLINK, it is possible to choose between ring redundancy and cable redundancy. In addition, controller redun-

dancy is also available.

Ring redundancy

For ring redundancy, devices are connected in a line, with

the last unit connected back to the master. When the con-

nection is interrupted, the ring redundancy manager re-

acts by feeding in data from both sides. The master rec-

ognizes when the ring is closed again and once again sup-

plies data only from one side into the ring. This guaran-

tees continuous communication.

Interruption of the ring can be evaluated on the controller

using data points or on the modules using the LEDs. The

diagnostic data points are also shown in System Diagnos-

tics Manager (SDM).

Figure: Example of ring redundancy

## Page 16

16DIAGNOSTICS AND SERVICE TM920

Cable redundancy

In this form of network redundancy, two networks are

available and every device is connected to both networks.

If an error occurs on one network, the link selector recog-

nizes this and switches to the other. This solution also en-

sures maximum availability of machinery and equipment.

Figure: Example of cable redundancy

Controller redundancy

B&R's controller redundancy solution ensures maximum availability for entire systems as well as individual machines.

Controller redundancy allows data to be synchronized within microseconds with no more than 2 cycles lost when

switching over to the respective controller. This functionality is seamlessly integrated in the real-time operating sys-

tem and is easy to use.

A second, identical X20 CPU is added to the existing control topology and configured as redundant in the software.

Figure 9: Schematic representation of controller redundancy with connection to the process bus

The user's manual "" can be downloaded from the B&R website. For op-Redundancy for control systems

tions and examples on the subject of redundancy, refer to the user's manual available via the following

path:

Downloads \ Control and I/O Systems \ X20 system \ Hub & redundancy system \ X20 redundancy sys-

tem

## Page 17

OBTAINING INFORMATION ABOUT THE B&R SYSTEM17

4Obtaining information about the B&R

system

B&R offers its customers a variety of information. All information is available

online around the clock and is constantly updated.

Product-specific information can be found by entering the serial number on

the B&R website. The corresponding data sheets and user's manuals can be

easily downloaded. More details about the exact procedure is explained in the

following chapters.

4.1Serial numbers and model numbers

Difference between serial number and model number

The  printed as a barcode is the unique IDserial number

of a B&R product. The serial number contains the model

number, revision, certifications and the entire history of

the component.

The is the unique identification of modulesmodel number

of the same type and is used, for example, to order mod-

ules. This is why the material number is called model num-

ber.

Figure 10: Elements of the serial number sticker

What information does the serial number provide?

Information about a specific serial number can be found using the search function on the B&R website.

Further information:

4.4.3 "Search function of the website" on page 24

•

## Page 18

18DIAGNOSTICS AND SERVICE TM920

Attaching the serial number

The serial number is found on a free space on each B&R product. On devices with plastic housing, the data is laser-

engraved directly onto the surface. On metal housing it is printed on a white sticker.

X20 systemHMI devicesMotors

Figure 13: Side view of motor

Figure 11: X20 digital module, X20 backplane

module

Figure 12: Rear side of Power Panel

Table 3: Examples of serial number placement

Is the serial number visible in the software?

Machine manufacturers have the option of reading serial numbers in the control program. Alternatively,

the serial numbers of detected hardware can be viewed in System Diagnostics Manager and a list of them

can be downloaded from the controller.

Further information:

6.5 "Information in the hardware tree" on page 40

•

Exercise: Serial number search

Use the CPU's serial number, integrated in the practice setup, to find out what information is available.

1)Identify the serial number of the CPU

2)Visit the B&R website, www.br-automation.com

3)Enter the serial number in the search field in the upper right corner

## Page 19

OBTAINING INFORMATION ABOUT THE B&R SYSTEM19

Figure 14: The search function is located in the upper right corner behind the magnifying glass

4)Note down results and evaluate them

4.2Data sheets and user's manuals

Data sheets and user's manuals are available on the B&R website.

contain complete documentation of the system characteristics, installation and maintenance guide-User's manuals

lines, electrical installation and data sheets for each module of a product category.

contain information about a specific module. They document each module including general technicalData sheets

data, pinouts, equivalent circuit diagrams and a register description.

Where can user's manuals and data sheets be downloaded?

All B&R documents can be found on the B&R website.

There are 3 ways to find the desired document:

Via the product section

•

Via the search function

•

Via the download section

•

The procedure for downloading the X20 system user's manual from the B&R website will be explained

later.

Further information:

4.4 "B&R website functions" on page 20

•

4.3Structure of B&R user's manuals

A B&R user's manual is available for each product group. All product variants are described in the user's manual. There-

fore, it serves as reference documentation for the respective system. It contains all relevant data for installation, com-

missioning and maintenance.

## Page 20

20DIAGNOSTICS AND SERVICE TM920

Contents of a B&R user's manual:

System characteristics

•

Mechanical properties

•

Electrical characteristics

•

Installation instructions

•

Module descriptions

•

Technical data

•

LED status indicators

•

Maintenance guidelines

•

Mechanical handling

•

Standards and certifications

•

Danger, warning and safety notices

•

Figure 15: Table of Contents of the X20 system

user's manual

Search within the user's manual

In order to obtain information about a single product variant, it is advisable to search for the model

number in the PDF of the user's manual. This is the quickest way to find the information you need.

Exercise: Search for and download the X20 user's manual

This exercise involves searching for and downloading a user's manual from

the B&R website. After that, the task is to gain an overview of the contents

and structure of the manual.

Search for the following information in the X20 user's manual:

Size of the X20CP1586 product's working memory

•

Connection of the supply to the terminal and the supply voltage range of

•

the X20CP1586 product

Different shielding concepts on X20 Modules10

•

Steps for mounting or dismounting the X20 system

•

Figure 16: X20 CPU - X20CP1586

Replacing the backup battery

•

1)Visit the B&R website, www.br-automation.com

2)Search for the X20 system user's manual. For help, see 4.4 "B&R website

functions" on page 20

3)Download user's manual

4)Search for product data and general information

5)Note down results and evaluate them

4.4B&R website functions

The B&R website provides up-to-date information about all B&R products. The comprehensive search function with

different filters facilitates the targeted search for information.

10This exercise can also be performed for a different B&R control system if an X20 system is not being used.

## Page 21

OBTAINING INFORMATION ABOUT THE B&R SYSTEM21

The following information can be found on the B&R website:

Product catalogs, user's manuals

•

Data sheets, certificates

•

Drivers, updates, CAD data

•

B&R worldwide contact info

•

Support Portal, Material Return Portal

•

Global search function, e.g. for serial

•

and model numbers

And much more

•

An example of how to download B&R documents from the website are shown in each of the following chapters.

There are 3 ways to find the desired document:

4.4.1 "Product section on the website" on page 21

•

4.4.3 "Search function of the website" on page 24

•

4.4.4 "Download section of the website" on page 25

•

4.4.1Product section on the website

Information about B&R's range of products can be found in the product section of the B&R website.

www.br-automation.com under "Products" in the main menu→

Figure 17: The different product groups can be opened via the main menu

## Page 22

22DIAGNOSTICS AND SERVICE TM920

Download X20 system user's manual from the product section

Navigate to "Control systems" via the product section. Then, select "X20 system". Scroll down to category

"Documentation". When this category is open, click on the X20 user's manual "MAX20-GER". A new page

opens. The user's manual can now be downloaded in tab "Downloads".

Figure 18: The X20 system user's manual can be downloaded from the product section.

## Page 23

OBTAINING INFORMATION ABOUT THE B&R SYSTEM23

4.4.2Compare products

The available variants of each B&R product are listed in the product section. The product variants are compared by

selecting the checkbox of the respective variant. Then, click on the "Compare" button to open the comparison.

www.br-automation.com  under "Products" in the main menu - Select category - Compare→

Figure 19: The checkboxes of the X20 product variants "X20CP1301" and "X20CP1381" are marked

After clicking on "Compare", the detailed product comparison opens. Differences and similarities between the two

product variants are shown in a table and highlighted in color.

## Page 24

24DIAGNOSTICS AND SERVICE TM920

Figure 20: Differences between the selected variants are highlighted in color

4.4.3Search function of the website

The search field is located at the top right of the B&R website.

Serial numbers or product names are suitable as search terms, for example. When searching for a product whose name

starts with "X20CP1", it is recommended to enter "X20CP1" to get optimal results.*

The results matching the search term are categorized. The results can be filtered according to these four different

categories under "Content type".

The following categories are available for the search result:

Page content

•

Product

•

Download

•

Press releases

•

Figure 21: Results for "X20CP1*". Under "Content type", results can be filtered by category

## Page 25

OBTAINING INFORMATION ABOUT THE B&R SYSTEM25

Download the "X20 system user's manual" using the search function

Enter "X20 user's manual" in the search box at the top right and press ENTER (if the desired result is not

included, results can be filtered by content type "Download"). Clicking on the result opens another page

from which the user's manual can be downloaded.

Figure 22: The "X20 system user's manual" can be downloaded via the search function

4.4.4Download section of the website

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

## Page 26

26DIAGNOSTICS AND SERVICE TM920

The filters to the left of the results allow even more precise filtering.

The content will be downloaded as soon as you click on the download icon to the right of the entry.

Figure 23: To get access to the download section of product "X20CP1586", product group "Control and I/O Systems" must be selected

Download the "X20 System user's manual" from the download section

Use section "Downloads" to navigate to the general download section. Then select option "Control and

I/O Systems" from drop-down menu "Product Group". Select "X20 system" and "Central units" from the

drop-down menus that follow.

Scroll down to category "Documentation". Clicking on the result opens another page from which the

user's manual can be downloaded.

Figure 24: The X20 system user's manual can be downloaded from section "Downloads"

## Page 27

DIAGNOSTIC TOOLS27

5Diagnostic tools

The following section describes the diagnostic possibilities for preliminary

diagnostics, which can be performed with or without an additional computer.

Before getting started, it is advisable to make sure you have all important

information from the manufacturer such as data sheets and manuals.

It is recommended to have a computer with a network card and network cable

ready for diagnosis. It is also helpful to have an Internet connection for the

diagnostic computer. This makes it possible to quickly obtain further infor-

mation as needed.

The following diagnostic methods can be used:

5.1 "Diagnostics without a PC" on page 27

•

5.2 "Diagnostics via PC" on page 31

•

5.1Diagnostics without a PC

When an error occurs, the first line of action is to get an overview.

Does the HMI application already show the first status reports?

What is the behavior of the LED status indicators for the electromechanics and control electronics?

This information provides important hints for the initial diagnosis.

5.1.1HMI application

Almost every machine has a visualization. This is used to operate and diagnose the machine. Overview pages display

the most important information. Any message texts available or specialized alarm and diagnostics pages can also

provide information about the cause of the problem.

Modern visualization applications also provide a help system and instructions for working with the machine.

Overview screenAlarm pageTrend page

Figure 25: Overview page - Everything at aFigure 26: Alarms, messages and warnings

Figure 27: Trend curves for graphic representation

glanceprovide information about the machine status

of process values

Table 4: Sample visualization pages

## Page 28

28DIAGNOSTICS AND SERVICE TM920

Can System Diagnostics Manager be embedded in the HMI application?

The machine's manual explains all the possibilities of the HMI application. The manufacturer of the ma-

chine can also provide more detailed information.

It is possible that some machine manufacturers integrate System Diagnostics Manager in the HMI ap-

plication.

Further information:

6.1 "SDM functions" on page 34

•

5.1.2Electromechanical parts, sensor, actuator

Most machines contain some optical signaling devices for diagnostics of the

devices connected to the machine controller. Thus many sensors and actua-

tors have LED status indicators.

More complex devices often feature status displays for displaying error codes

or textual information. The manufacturer's data sheet for the sensor or actu-

ator should explain how to interpret and understand this information.

The LED status indicators on devices are generally assigned specific func-

tions. For example, some LED signals indicate the quality of the measured

value, while others indicate the quality of communication with the higher lev-

el system. Different blinking patterns are used to display additional informa-

tion.

In addition to the meaning of status indicators, further important informa-

tion is contained in the manufacturer data sheet or the user's manual. For ex-

ample, improper installation of a device can lead to errors. The image shows

the metal EMC plate. The regulations for cable arrangement on the EMC plate

can be found in the corresponding user's manual. In the ACOPOSinverter P74

user's manual, it can be found in chapter "Installation", subchapter "Initial In-

stallation".

Figure 28: ACOPOSinverter P74 frequency

converter with status display and operating

elements; EMC plate below

5.1.3LED status indicators on the control system

All B&R devices contain LED status indicators that indicate the current oper-

ating status and quality. The following section briefly describes what infor-

mation can be obtained from the LED status indicators and where to find ad-

ditional information.

The LED status indicators on B&R devices are designed so that they are visible

from the front in the control cabinet. HMI devices are the only exception since

the LED status indicators are located on the back of the displays.

Figure 29: LED status indicators on an

Automation PC 810

## Page 29

DIAGNOSTIC TOOLS29

Types of LED status indicators

Each LED is assigned different functions. For instance I/O modules feature a general module status as well as one LED

status indicator per channel in order to display the channel status.

Communication interfaces also feature one or two LEDs each for displaying communication activity in addition to the

general module status. Each LED provides additional information using specific blink codes.

The following table provides an overview of the LED types on control systems.

DeviceLED informationOverview on the device

Module status

•

I/O modulesModule error

•

Channel status

•

LED status indicators on a 2-channel

digital input module

Module status

•

Boot phase

•

Control CPUsBattery status

•

CompactFlash

•

Communication

•

LED status indicators on an X20 CPU

Table: Example of some LED status indicators on the X20 system

The image shows the description of the LED status indicators of the analog X20AI4322 input module. The

LED called "r" shows the module status. If LED "r" blinks, then the module is in mode "PREOPERATIONAL".

This means that the module is started internally and ready for operation. However, modules in module

"PREOPERATIONAL" have not been initialized or configured yet. Reasons for this could be that the module

has not yet been accessed, that there is no configuration for the module in the automation project or

that the bus connection to the module is suspended.

Figure 30: Extract from data sheet X20AI4322, chapter 4 "LED status indicators"

After switching out a module, a firmware update is carried out by the controller CPU if required. This is

indicated by double flashing. You have to wait until this procedure is complete before proceeding.

## Page 30

30DIAGNOSTICS AND SERVICE TM920

Where can I find detailed description of the LED status indicators?

To get access to the description of the LED status indicators, the serial number of the device is required.

With this information, the corresponding documents, such as data sheets and user's manuals, can then

be downloaded from the B&R website.

Further information:

4 "Obtaining information about the B&R system" on page 17

•

Exercise: Interpret X20AT2222 LED status indicators

Use the data sheet of the X20AT2222 module to interpret the LED status indi-

cators. LED status indicator "e" is blinking red (single flash). What does this

mean and what could cause this?

1)Visit the B&R website, www.br-automation.com

Figure 31: LED status indicators of the

X20AT2222 module

2)Search for "Data sheet X20AT2222". For help, see 4.4 "B&R website func-

tions" on page 20

3)Download the data sheet

4)Read chapter "LED status indicators"

5)Interpret description

A single flash on LED "e" means there is a pending warning or error on a channel. This can be caused

by an open circuit or a value being too high or low. It is also possible that the wrong channel type was

configured or the sensor was connected to the 2-wire connections instead of the 3-wire connections. The

green flashing channel LED indicates which channel is affected.

Exercise: System overview using LED status indicators

This exercise focuses on obtaining a complete overview of the current practice system. The focus will be on the LED

status indicators of the individual components.

1)Note down all installed components

2)Visit the B&R website, www.br-automation.com

3)Search and download required data sheets and user's manuals. For help, see 4.4 "B&R website functions" on page

20

4)Evaluate the LED status for:

Control CPU (general operating state, interfaces, battery, CompactFlash card)

°

I/O modules (general operating state, channel status)

°

HMI

°

Motion control (axis status, communication, encoder interface)

°

Safety technology (SafeLOGIC, SafeIOs)

°

When is the controller ready for operation?

Only when status "RUN" is signaled for all components, it can be assumed that the control system is

working perfectly. If any component is signaling a different status, then the LED status indicators can be

used to localize the affected component, which can then be replaced if necessary.

## Page 31

DIAGNOSTIC TOOLS 31
5.2 Diagnostics via PC
System Diagnostics Manager
System Diagnostics Manager is a web-based interface used for diagnosing B&R control systems. The hostname or IP
address of the controller as well as a web browser are needed to gain access. A separate chapter in this documentation
is dedicated to System Diagnostics Manager.
Further information:
6 "Diagnostics using the System Diagnostics Manager" on page 33
•
Which version of Automation Runtime is required?
System Diagnostics Manager was introduced with Automation Runtime version11 3.00 and later; the in-
tegrated drive diagnostics was introduced with Automation Runtime 3.08 and later. To make System Di-
agnostics Manager available, it needs to be enabled in the application program.
mapp technology WebXs
If mapp Technology components were used for creating applications, they can be shown via the mapp diagnostic page
"WebXs". The components deliver comprehensive status information that is helpful when communicating directly with
the machine manufacturer.
Further information:
6.7 "Information about the application status in WebXs" on page 46
•
Which version of Automation Runtime is required?
mapp Technology WebXs was introduced as of Automation Runtime 4.08 and later. mapp WebXs must
be enabled in the application program so that this diagnostic option becomes available.
Runtime Utility Center
The Runtime Utility Center can be used for all B&R systems with Automation Runtime. It permits a connection to be
created over a serial interface and the Ethernet interface.
The Runtime Utility Center is a service tool and can be downloaded from the B&R website. In some additional chapters
in this documentation, the functions of the Runtime Utility Center are explained in more detail.
Further information:
8.1 "Backing up and restoring using the Runtime Utility Center" on page 59
•
10.2 "Where SDM and Runtime Utility Center can be used" on page 70
•
HMI Diagnostics tool
The HMI diagnostics tool is downloaded from the B&R website and is used for reading device data from B&R industrial
PCs. The data provides information about operating hours, components used and the Windows installation used.
PVI Snapshot Viewer
The Process Visualization Interface (PVI) is a communication service between Windows applications and B&R con-
trollers. The PVI Snapshot Viewer is used to troubleshoot the connection between the PC and controller. It displays
the connected process variables and their statuses.
OPC UA is used to transfer data in modern applications. A separate logger file for troubleshooting can be found in
System Diagnostics Manager.
HMI Service Center
HMI Service Center is delivered preinstalled on a bootable USB flash drive from B&R. It can be used to read device data
from B&R Industrial PCs, Panel PCs, Automation Panels, interface options and I/O boards.
Embedded OS Installer
Functions for backing up and restoring Windows installations are available via the Embedded OS Installer, which is
also downloaded via the B&R website. Images can be backed up and restored. Functions include:
11 The Automation Runtime version can be read in the Runtime Utility Center using command "AR Version". For step-by-step instructions, see 8.1.5 "Outputting data via online
connection" on page 65.

## Page 32

32DIAGNOSTICS AND SERVICE TM920

Easy installation of B&R Windows CE and B&R Windows XP Embedded.

•

Installation of customized Windows CE and Windows XP Embedded images.

•

Simple installation of MS-DOS on a USB flash drive.

•

Creating a USB flash drive for a B&R upgrade (Bios, MTCX).12

•

Simple installation of Windows Embedded Standard 7 setup files on a USB flash drive.

•

Disk image generation and backup.

•

File manager for editing existing disk images.

•

The downloads for the HMI tools are stored in section "Downloads" in category "Software / HMI software". The instal-

lation of the HMI tools is only possible on Windows operating systems.

Figure 32: HMI software in the download section of the B&R website

12The MTCX (Maintenance Controller Extended) is located on the CPU board of the device. It is responsible for the following monitoring and control functions.

## Page 33

DIAGNOSTICS USING THE SYSTEM DIAGNOSTICS MANAGER33

6Diagnostics using the System Diag-

nostics Manager

B&R controller components come with an extensive diagnostics interface. This interface is called "System Diagnostics

Manager" or "SDM" for short. The controller is accessed using Google Chrome via the IP address or hostname of the

controller.13

SDM is accessed using a web browser via the link "".http://ip-address/SDM

Figure 33: System Diagnostics Manager homepage

Can System Diagnostics Manager be embedded in the HMI application?

Depending on the machine visualization application, some of the SDM pages may already be embedded

in the HMI application. This is described in greater detail in the manual provided by the machine manu-

facturer.

13SDM can generally be enabled on all B&R control systems with integrated web server by the machine manufacturer. Alternatively, the 8.1 "Backing up and restoring using the

Runtime Utility Center" on page 59 can be used. A comparison of the possibilities in SDM and in the Runtime Utility Center is provided here: 10.2 "Where SDM and Runtime

Utility Center can be used" on page 70

## Page 34

34DIAGNOSTICS AND SERVICE TM920

6.1SDM functions

The most important functions in SDM can be accessed via the homepage. A

system dump can be generated via the button in the center, see 6.4 "Gen-

erating and saving a system dump" on page 39. All relevant information

sources and Logger files from the controller are then loaded. These files can

be used for further diagnostics.

The other buttons can be used to request information about the individual

parts of the controller. The information displayed on these in-depth pages

can also be saved as a file.

Figure 34: Areas of System Diagnostics Manager

General system overview

The general system overview contains

data such as the battery status, CPU us-

age and the basic system settings. The

configured CPU timing and memory us-

age is also displayed.

Figure 35: General system overview

Displaying and saving Logger files

The B&R control system monitors itself. Relevant and important information about the system status is displayed in

a list. The Logger files are displayed for further analysis in SDM or uploaded and saved by the controller. Data refer to

the control system, integrated safety technology, fieldbuses or user events triggered by the control program. Machine

manufacturers are provided with functions to create their own Logger files that, for example, log all events of the

machine sequence.

Figure 36: Display of Logger files in SDM

## Page 35

DIAGNOSTICS USING THE SYSTEM DIAGNOSTICS MANAGER35

Hardware modules and I/O status

The hardware diagnostics provid-

ed by SDM are particularly help-

ful. This makes it possible to di-

agnose the entire control system

status at once. The serial numbers

of the hardware modules are list-

ed. An additional function is read-

ing channel data from various I/

O modules. At the bottom part of

the window, information about

the current values of inputs and

outputs is displayed.

Figure 37: Hardware information and I/O status

With analog modules, voltages and

currents are converted into numer-

ical values which can be processed

on the controller. Independent of

the resolution, the numerical val-

ues exist predominantly in the 16-

Figure 38: Display of measure values of voltage and currents (extract from X20 user's manual)

bit 2s complement.

Additional information

6.5 "Information in the hardware tree" on page 40

•

6.6 "Diagnostics via I/O and status data points" on page 43

•

Motion control

Diagnostic options are available

for machines with drive technolo-

gy via the SDM's Motion diagnos-

tics. The axis status, homing sta-

tus, current position and move-

ment status are shown. If an er-

ror occurs, additional information

such as the axis error list can be

obtained by clicking on the traffic

light icon.

Figure 39: Displaying the axis diagnostics, all axis objects are listed

## Page 36

36DIAGNOSTICS AND SERVICE TM920

In the axis error list, the axis error

numbers are displayed with the

corresponding clear text descrip-

tion and timestamp.

Figure 40: Displaying the axis error list for a selected axis object

The network command trace logs

commands that are sent to and

from the drive. Button "Snapshot"

is pressed in order to take a snap-

shot of the data. Any data object

on the displayed list can be up-

loaded or saved. It is only possible

to analyze the data in Automation

Studio.

Figure 41: List of the recorded network command trace data objects

Embedded SDM in the HMI application

Machine manufacturers are able to integrate the SDM right in the HMI application. The same functions are available

as those provided when running the SDM in a web browser. Just the display of diagnostic pages differs a bit from the

embedded SDM version.

System overviewSystem loggerDrive technology diagnostics

Figure 42: SDM system overviewFigure 43: SDM system loggerFigure 44: SDM drive technology diagnostics

Embedding of application specific data

The System Diagnostics Manager (SDM) enables application specific data be accessible by software developers. Status

information relating to a controller application can be shown by pushing the "application status" button.

If the "Application Status" function in the application is not used, you end up

directly on the start page of mapp Technology WebXs (if this is enabled).

Figure 45: "Application status" button in SDM

## Page 37

DIAGNOSTICS USING THE SYSTEM DIAGNOSTICS MANAGER37

Can SDM be used to upload a software object?

System Diagnostics Manager can be used to view the system status and general system information.

There are no functions provided for manipulating process variables or uploading/downloading software

objects. The Runtime Utility Center can be used for such application situations.

Further information:

8.1 "Backing up and restoring using the Runtime Utility Center" on page 59

•

Exercise: System overview with the help of SDM

Use the SDM to get a general overview of the practice system. Check the general system status, the system usage, the

overview of hardware modules and the status of the motion components.

1)Establishing a connection to the SDM

2)Check system status

3)Check system usage and make a note of the maximum values

4)Make a note of the battery status

5)Have a look at the overview of hardware modules and check the status of the individual modules

6)Call up the status of the motion components

While the LED status indicators can be used to obtain a good overview of the control system's overall

status, SDM makes it easier to view all of this information at once. SDM is a better option for larger

machines with distributed control cabinets, control components hard to access or when running remote

diagnostics.

6.2Establish a connection to SDM

Before connecting to the controller, the network configuration of the PC in use may have to be modified. It is recom-

mended to make a note of the original settings before doing this.

A standard network cable is used to connect with the controller. The LED status indicators of the PC and the controller

indicate whether there is an online connection or not.

The connection to SDM is made via the controller's IP address or hostname. SDM is accessed via the browser using14

link"."http://ip-address/SDM

Step 1 - Connect PC to controllerStep 2 - View SDM in web browser

Figure 46: Connecting PC to controller via network cable

Figure 47: Accessing the SDM via a web browser

Which browser is required?

An SVG-capable web browser is required to view SDM pages. Most current web browsers support this.

An SVG plug-in is offered when using older browsers. If viewing SVGs is still not possible, then an HTML

view of System Diagnostics Manager can also be accessed via "".http://IP-address/sdm-vga

14B&R recommends using Google Chrome as your browser.

## Page 38

38DIAGNOSTICS AND SERVICE TM920

Which IP address must be entered in the browser?

The controller's IP address or hostname can be found in the documentation provided or requested di-

rectly from the manufacturer. In some cases, the set IP address is written on the control cabinet.

6.3Saving Logger files

The Logger records important system events in several Logger files. The events are displayed as a list using SDM. The

machine manufacturer has the option to record events via the application program in Logger files.

Why are there multiple Logger files?

Several Logger files with different affiliations can exist on the B&R control system. The controller may

also contain Logger files for fieldbuses, integrated safety technology or machine events, for example.

If necessary, back up each Logger file individually or perform a system dump.

Saving Logger files

Start by establishing a connection to the SDM.

Step 1Step 2

Figure 49: Selection of the desired Logger file

Figure 48: Select the category "Logger"

Step 3Finished

Depending on the web browser settings, the file will ei-

ther be directly downloaded directly into the Downloads

folder or a window will appear for selecting the location

to save the file.

Figure 50: The Logger file is uploaded by pressing button "Upload from

target"

## Page 39

DIAGNOSTICS USING THE SYSTEM DIAGNOSTICS MANAGER39

At what times were Logger entries created?

When interpreting Logger events, the current time on the control CPU is of particular importance. Before

evaluating Logger entries, a potential deviation from the system time should be considered and noted.

The system time is shown in SDM under category "System".

Figure 51: The current system time is displayed in category "System / General"

Exercise: Back up Logger file

This exercise will focus on displaying and saving the system log "$arlogsys". Take a moment to interpret the entries

before saving. Also be sure to check and to make a note of the time of the controller.

1)Establish a connection to SDM

2)Open Logger

3)Take a moment to check the Logger files (time and entries) and evaluate their contents

4)Save $arlogsys

6.4Generating and saving a system dump

The system dump contains important information for hardware and software diagnostics on B&R systems. This in-

cludes Logger files, profiler files, the I/O status and hardware configuration. Start by establishing a connection to SDM.

The following instructions show how to generate a system dump.

Step 1Step 2Step 3

Figure 53: Start a new system dump

Figure 55: Select option "Parameters and data

files"15

Figure 54: Confirm with OK

Figure 52: Press the "System Dump" button

15This option saves all of the control system Logger files in ZIP format and adds them to the system dump.

## Page 40

40DIAGNOSTICS AND SERVICE TM920

Step 4

Figure 56: Upload system dump

The system dump data contains important information about the machine controller. The machine manufacturer or

B&R can use this data for further analysis.

Exercise: Triggering and saving a system dump

A full system dump including all Logger files and parameter files must be generated and saved.

1)Establish a connection to SDM

2)Start a new system dump

3)Save system dump

6.5Information in the hardware tree

System Diagnostics Manager displays and saves Logger files. In order to further evaluate the Logger data, some sys-

tem knowledge and additional information delivered by SDM are required. The information is shown in SDM as a hard-

ware tree.

An X20 controller controls a machine. Some X20 I/

O modules are directly connected to the X2X inter-

face of the CPU. The last module on the X2X Link

network is an illuminated ring key field.

An X20 bus controller is connected via the POWER-

LINK connection in blue. This has node number 1.

Some X20 I/O modules are connected to the inte-

grated X2X interface of the X20 bus controller.

The POWERLINK connection continues from an

X20 bus controller to an ACOPOS servo drive. This

has node number 2.

Figure 57: Sample configuration with an X2X and POWERLINK

connection

Hardware tree in the SDM

The enabled and configured hardware modules are shown in SDM "Hardware" category. They are displayed in a tree

structure, so it is possible, starting from the controller CPU, to navigate to the I/O module or drive via the individual

fieldbuses.

## Page 41

DIAGNOSTICS USING THE SYSTEM DIAGNOSTICS MANAGER41

The image shows that another X20 module was detected on the X2X interface, which is not part of the hardware

configuration.

Figure 58: Depiction of the current hardware configuration in the SDM

What do the yellow "!" and the red "X" mean?

In the event of a faulty configuration or missing hardware, the interface where a problem is occurring will

be highlighted by an exclamation point with a yellow background. The entry that caused the problem is

marked with an "X" shaded in red.

Module status and module details in the SDM

When a module is selected in the hardware tree, the module status and details about the module are shown on the

right half of the screen. The X20DO6322 module is marked in the image. The module status shows "ModulOk" = TRUE.

This means that the hardware module has been recognized and configured correctly. This module works correctly and

delivers valid input data.

The module path and the B&R serial number are shown in the module details. If the serial number is known, the corre-

sponding user's manual can easily be downloaded from the B&R website.

Further information:

4 "Obtaining information about the B&R system" on page 17

•

Figure 59: The module status and module details of the X20DO6322 module are shown

Module path

The module path in the module details clearly describes the place where a module is located. This provides information

about the used fieldbus, the node number, the used interface and the slot of the individual module. The module path

describes the position of the module in the hierarchy of the control system. If module path "IF6.ST2" is shown, for

example, it must be read as follows:

## Page 42

42DIAGNOSTICS AND SERVICE TM920

Element of theDescription of the element

module path

IF6X2X interface of the X20CP1586 X20 controller. The interface

name is documented in the data sheet of the X20 controller.

ST2The X20DO6322 module is inserted in slot 2 on the X2X inter-16

face of the X20 bus controller.

Table 5: Module path decoding

Figure 60: X20AT4222 in the SDM

hardware tree

The descriptions of the interfaces (IF1, IF2, IF3, IF6, etc.) can be found in the data sheet of the respective

device. Here, there is also information about the transfer rate, cable length and data transfer of the in-

terface.

Figure 61: Excerpt of description "IF3" POWERLINK interface from "Data sheet X20(c)CPx58x"

Exercise: Remove bus connection or I/O module and analyze Logger entry

Cancel the connection to the X20 bus controller during operation or remove an I/O during runtime. The controller is

restarted in service mode and the execution of the controller application is canceled. Read the Logger file and analyze

the entries.

1)Disconnect the connection to the bus station

2)Wait for boot up in service mode

3)Establish a connection to SDM

4)Open the logger file

5)Interpreting the Logger file using the hardware tree in SDM

16The abbreviation "ST" stands for slot and describes the position of a module slot in the respective hierarchy.

## Page 43

DIAGNOSTICS USING THE SYSTEM DIAGNOSTICS MANAGER43

The controller was automatically started in service mode by disconnecting from a bus station. Text "Mod-

ule removed while running" additional information "IF3.ST1" are shown in the Logger entry.

Figure 62: Logger entry in SDM - "Module removed while running"

A change can also be recognized in the hardware tree. The remote X20 module is marked red. The infor-

mation "Not plugged" is listed in the module status and in the details for the corresponding position in

hardware tree "IF6.ST2".

If, for example, an entire X20 bus controller was removed, all I/O modules in the background would also

be marked red.

Figure 63: Hardware tree in the SDM - IF3.ST1 is not inserted

How do I recognize a POWERLINK problem in SDM?

A problem can be identified on the POWERLINK fieldbus by displaying the Logger and the hardware tree

in combination. However, it cannot yet be identified if the failure of the network participant was caused

by EMC problems, a contact problem or a hardware problem. In this case, the status points of the fieldbus

interfaces and bus controllers give additional information.

Further information:

6.6 "Diagnostics via I/O and status data points" on page 43

•

6.6Diagnostics via I/O and status data points

Via button "IO Info", which is located below the hardware tree in SDM, the data

of the I/O modules are displayed. B&R modules have status data points that

provide information about the module status or fieldbus communication. By

Figure 64: Enable to the I/O information

clicking on the button, all information is displayed.

## Page 44

44DIAGNOSTICS AND SERVICE TM920

Status inputs of I/O modules

Besides input values of the I/O channels, further information about the validity of the measured values, wire breaks,

short circuits or overcurrent is added to the I/O information. This is shown for each module marked in the hardware

tree. This image shows an X20DO6322 digital output module. Input "StatusDigitalOutput01" shows value FALSE.

Figure 65: I/O information and status data of an X20DO6322 output module

All data channels are completely described in "Data sheet X20(c)DO6322" in chapter "Status of the digital

outputs". Input "StatusInput01" is described in this image. There is a meaningful channel status for each

I/O channel.

Figure 66: Description of "StatusDitigalOutput01" in "Data sheet X20(c)DO6322"

## Page 45

DIAGNOSTICS USING THE SYSTEM DIAGNOSTICS MANAGER45

Status inputs of fieldbus devices and

interfaces

Each interface and each fieldbus sta-

tion has its own diagnostic data points.

Using this data, it is possible to record

the quality of the communication and

communication disturbances when the

machine is still running.

The image shows the diagnostic data

points of the X20BC0083 POWERLINK

bus controller. The X2X interface and

the POWERLINK interface of the con-

troller itself also offer such diagnostic

data points.

Figure 67: Diagnostic data points of X20BC0083 e.g. EthPhys1LinkLoss shows the amount of

connection disruptions to the 1st Ethernet port of the bus controller.

During operation, these diagnostic data points may only increase every two hours maximum; otherwise,

the network cabling should be checked.

When the system is booted, it's possible that some of these error counters are incremented until startup

is complete.

Further information:

10.4 "Notes regarding grounding and shielding" on page 72

•

Exercise: Diagnosis of the fieldbus connection using diagnostic data points

The X20BC0083 POWERLINK bus controller as well as all other fieldbus interfaces and devices have diagnostic data

points. The goal is to become familiar with the reaction of the complete system in the event of connection interruption

in the POWERLINK network and to read the diagnostic information related to this.

1)Disconnect the connection between the X20 controller and Powerlink bus controller X20BC0083.

2)Wait for the system to restart

3)Open the System Diagnostics Manager.

4)Read Logger entry

5)Open hardware tree in the SDM and enable I/O status

6)Read the error counter for the POWERLINK interfaces of X20BC0083.

7)Establish POWERLINK connection

8)Wait for reaction in SDM and at the I/O modules.

Connecting and disconnecting the POWERLINK cable over and over causes the error counters to incre-

ment. The system restart that was triggered by the interruption of the connection can be diagnosed us-

ing the Logger. Any potential disturbances in communication or wiring problems can be determined on

the running machine via the statuts data points.

## Page 46

46DIAGNOSTICS AND SERVICE TM920

6.7Information about the application status in WebXs

If mapp Technology components and the WebX library were used while creating the application, these components

can be displayed via the mapp diagnostic page "WebXs". The components deliver comprehensive status information

that is helpful when communicating directly with the machine manufacturer.

The connection to WebXs is established by entering" in the address bar of the browser On the17"IP address/mapp

left of the image, a mapp component is being selected. The input parameters and status information of the selected

mapp component are shown in the center of the image. The example below shows component MpAxisBasic that is

used to control a drive axis.

Figure 68: mapp Technology WebXs: Input parameters and status information of MpAxisBasic

What happens if function block output "Error" has value TRUE?

If output "Error" has value TRUE and if the statusID is not equal to 0, then an entry is generated in Logger

file "$mapp". The Logger file can be shown and saved using System Diagnostics Manager.

17B&R recommends to use Google Chrome as browser.

## Page 47

TROUBLESHOOTING47

7Troubleshooting

Once the problem has been identified and all the necessary information has

been obtained, the problem can be solved.

If the problem can be solved by replacing a module, it is important to deal

with backing up the process data and parameter data of the machine control.

The network topology must be examined beforehand.

If the problem was caused by material wear, the affected maintenance part

can be easily replaced.

Is there anything to consider before replacing or servicing the product?

It is recommended to download the component data sheet and user's manual from the B&R website.

These documents contain important information on handling. If the documentation is not followed, ir-

reparable damage may occur.

Further information:

4 "Obtaining information about the B&R system" on page 17

•

7.1Service parts

The B&R system contains only a small amount of service parts. All service parts are externally accessible and easy to

replace. These parts must be replaced from time to time in order to ensure long-lasting operation.

All required accessories and maintenance parts are linked to the serial number. All maintenance parts and their main-

tenance intervals are listed in the . The regional regulations regarding the replacement of service partsuser's manual

during operation of the machine must be observed.

Service parts include:

Fans for PCs, drives and CPUs

•

Filter pads for fan kits

•

Backup batteries for all CPUs

•

Backup batteries for Power Panels

•

Backup batteries for PC BIOS

•

Backup batteries for EnDat 2.2 encoder interfaces

•

Figure 69: Lithium battery, suitable

for most B&R components18

When replacing batteries, be sure to follow the procedure specified in the user's manual and the recom-

mended service intervals. Failure to do so can result in a . According to the user's manual, theloss of data

controller bridges data buffering for a certain time so that the battery can be changed.

The process variables can be listed and then saved in the Runtime Utility Center using command "Variable

List". Step-by-step instructions, see 8.1.5 "Outputting data via online connection" on page 65. Find

out from the machine manufacturer which data is stored in the battery-backed memory area.

The battery status can be read via LED status indicators, System Diagnostics Manager as well as the

Runtime Utility Center. Usually the battery status is shown in the alarm system of the HMI application.

Further information

5.1.3 "LED status indicators on the control system" on page 28

•

6 "Diagnostics using the System Diagnostics Manager" on page 33

•

181 piece 4A0006.00-000 or 4 pieces 0AC201.91

## Page 48

48DIAGNOSTICS AND SERVICE TM920

Exercise: Replacing the battery

Replace the backup battery in the control CPU.

Follow the instructions in the user's manual.

1)Check the battery status (SDM, LED status indicators)

2)Visit the B&R website, www.br-automation.com

3)Search for the X20 system user's manual. For help, see 4.4 "B&R website

functions" on page 20

4)Download user's manual

5)Back up all process variables, see 8.1.5 "Outputting data via online con-

nection" on page 65

6)Read chapter "Changing the Lithium Battery"

7)Change the battery as described in the user's manual.

7.2Replacing a module

The following section lists the most important information about module switching. Before replacing the module,

make sure that all relevant measures have been taken. Detailed instructions as well as danger notifications and warn-

ings are documented in the  of the respective system.user's manual

Node numbers

The topology and functioning of the B&R system was designed so that all programs and configuration data is saved

on the CompactFlash or on the internal Flash memory of the machine controller.

This means that all of the necessary settings and parameters are loaded to the devices connected to the machine when

it is started. Each unique node number is used to identify the devices on the fieldbus.

The node number is set right on the device using the selector switch and must be unique on the respective fieldbus.19

The backplane modules on remote X20 I/O modules can also contain node number switches.

Figure 71:

Node number

switch

Figure 70: All stations are networked together via POWERLINK. Each station has a unique node numberon bus

controller

X20BC0087

What should be kept in mind regarding the node number?

When replacing a device, make sure that the node number set for the new device is the same as that of

the old device. New devices delivered by B&R are set to node number $00. The node number on the B&R

system is set to form, e.g. $1B corresponds to decimal 27.hexadecimal

19Devices without a node number switch are automatically addressed and configured with an ascending node number.

## Page 49

TROUBLESHOOTING49

Module type

Before replacing the module, make sure that the right module type is being used. The original device model number

must match that of the replacement. This can be checked using the model number located next to the serial number.

The control CPU will not be able to configure or start an incorrect module that is connected.

The serial number of modules that are difficult to access can be read using the hardware tree in the System Diagnostics

Manager.

Further information:

4.1 "Serial numbers and model numbers" on page 17

•

Backing up and restoring

All fieldbuses, I/O modules and drives are configured and started by the controller's CPU. If you replace CPU of the

controller yourself, you should be aware that all data that's saved on the controller will go missing. Therefore it's nec-

essary, if possible, to back up important data in advance.

Further information

8 "Backing up and restoring" on page 59

•

Exercise: Creating a topology diagram

Having a topology diagram of the machine controller to hand can be very helpful when communicating with other

colleagues or with B&R Support. In the vast majority of cases, the topology is derived directly from the machine's

electrical diagrams.

In this exercise, we will be creating a topology diagram of the practice setup used. This is done by outlining and noting

all bus connections and the node numbers of the bus stations. The material numbers of the individual modules can

also be entered in the topology.

7.2.1I/O, fieldbus devices, interfaces

In the control system, I/O modules serve to connect sensors and actuators. Different devices are connected with the

controller via interface and fieldbus modules.

Figure 72: X20AT2222

– Temperature

measurement module

Figure 74: X20IF1063-1 –

PROFIBUS interface module

Figure 73: X20BC0083

– POWERLINK bus

controller

When replacing I/O modules, fieldbus devices and interfaces, make sure that the new module is the same type.

## Page 50

50 DIAGNOSTICS AND SERVICE TM920
Also be sure to check the node number switches20 on the module. If the LED status indicators on the module indicate
"RUN" in accordance with the user's manual or data sheet, then the module replacement has been completed success-
fully.
Recommended procedure for module replacement:
Check model number of replacement part.
•
Switch off supply voltage21
•
Set the node number switch correctly
•
Replace module
•
Switch supply voltage back on.
•
• Wait for startup → Check LED status indicators
How long does the module replacement take?
After replacing I/O modules, fieldbus devices or interfaces, it can take a few minutes for the control
system to start running again. The reason is that the operating software and the configuration must be
transferred from the control CPU to the new module. This occurs only once and is signaled by the LED
status indicators on the module.
When is a module permitted to be exchanged?
Refer to the respective user's manual to determine whether a module can be replaced during operation
with supply voltage turned on.
7.2.2 Control CPU
Many control CPUs contain a CompactFlash card22. This is where the control application and all configuration files for
the machine are stored.
The following information also applies to HMI devices (Power Panels) and industrial PCs (Automation PC)
that are used to perform control tasks.
Recommended procedure for replacing the control CPU:
Check model number of replacement part.
•
If possible, back up all process variables.
•
Turn off supply voltage.
•
Set node number switch properly on the replacement device.
•
If there is a CompactFlash card, remove it and insert it in the new CPU.
•
Remove Technology Guard from the USB port and insert in the new CPU.
•
Switch supply voltage back on.
•
•
Wait for startup →Check LED status indicators.
20 Bus controllers, interface modules, X20 backplane modules and X67 modules mostly contain node number switches. All other modules are addressed automatically.
21 X20 modules can also be replaced during operation. First make sure that this is also allowed on the respective machine.
22 In a few types, the application memory is built directly into the CPU and cannot be replaced externally.

## Page 51

TROUBLESHOOTING51

Figure 76:

CompactFlash

Figure 77:

Technology Guard

Figure 75: X20CP1586 - Control CPU

Can the old CPU be exchanged with a new, different CPU?

The model number of the replacement CPU must match that of the original CPU. Different CPUs use

different processors. The operating system installed on the CompactFlash card must match the original

CPU type. The controller will therefore not start properly if a different CPU is used. This is signaled via23

the LED status indicators on the CPU.

CompactFlash cards are only permitted to be removed or inserted when the power is off.

When using a control CPU with an integrated application memory, the machine manufacturer must pro-

vide either a replacement CPU with the application program preinstalled or the data needed to generat-

ing a remote install structure.

Further information:

8.1.3 "Generate project installation package" on page 64

•

Does the data in the battery-backed memory get lost during CPU replacement?

Depending on the control program, some process data and parameters may be stored in the control

CPU's battery-backed memory. These data and parameters will be lost when the control CPU is replaced

and must therefore be backed up prior to replacement.

Only the machine controller's software project can indicate whether or not relevant process data and

parameters are stored in the battery-backed memory on the control CPU. Only the machine manufacturer

can provide this information.

23In this case, the machine manufacturer would have to change the hardware configuration of the control application and create a new CompactFlash card.

## Page 52

52DIAGNOSTICS AND SERVICE TM920

7.2.3HMI devices, industrial PCs

Power Panels with and without touch screens

A  is essentially a controller with integrated display. The samePower Panel

rules as for a control CPU apply for dealing with these devices. Interface cards

can be integrated into the device.

Figure 78: Power Panel 5PP520.0573-01

Does the new touch screen have to be calibrated after replacing the module?

Many B&R touch screen devices are equipped with a touch controller that supports hardware calibration.

This means that these devices are delivered pre-calibrated. Recalibration is generally not required after

replacement. For detailed information about touch screen calibration see section "Touch screen" in the

respective user's manual.

The procedure for recalibrating a touch screen is usually a function of the machine's HMI application and

can be found in the user's manual of the machine.

Automation PC (APC), Panel PC (PPC)

An  works according to the same principles as a conventional PC.industrial PC

The operating system and application programs are stored on a hard drive

or CompactFlash card. The mass storage devices used in B&R's industrial PCs

can be accessed and replaced easily from the front without having to open up

the device. An Automation Panel is usually used to display the user interface.

Figure 79: Panel PC 910 system unit

On a Panel PC, the monitor and PC are integrated into one device. On the newer generation of Panel PCs, the PC unit

is mounted on an Automation Panel. This allows users to replace the PC or display unit independently of one another.

Hard drives, solid state disks (SSD), CompactFlash and CFast cards are only permitted to be removed

when the power is switched off.

Recommended procedure for replacing a PC:

Check product ID of replacement device.

•

Remove supply voltage.

•

Remove hard drive and CompactFlash card.

•

Install hard drive and CompactFlash card on new device as needed.

•

Reinstall Technology Guard on the new device.

•

Remove plug-in cards.

•

Insert plug-in cards in new device as needed.

•

Set node number switches.

•

Install backup battery (included in delivery) on replacement device.

•

Restore power supply.

•

Wait for startup to complete.

•

Figure 80: Automation PC 910

## Page 53

TROUBLESHOOTING53

What should be considered before replacing a B&R PC?

Be sure to note the BIOS settings before replacing a PC. These must then be set identically in the replace-

ment device. A description of how to do this can be found in the user's manual of the respective PC.

If interface cards are built into the PC then they should be built into the new device in the same position.

If dongles are used to protect software then they should be moved to the new PC.

Automation Panel

An  is a display device and can only be used in connection with an Automation PC.Automation Panel

Does the new touch screen have to be calibrated after replacing the mod-

ule?

Touch screen recalibration may be necessary after replacing an Automa-

tion Panel. On Windows systems, this can be done in the Control Panel.

On devices with hardware keys, the key configuration is stored on the PC.

When the PC is replaced, the key configuration must be transferred to the

new PC. This can be done using the "Automation Device Interface" (ADI)

in the Control Panel. The ADI software is described in the user's manual

of the PC.

Figure 81: Automation Panel

5AP981.1043-01 with touch

screen and hardware keys

7.2.4Motion control - Drive systems

Drives generally provide a mains connection and connections to the motor. Drives must only be replaced when the

power is turned off.

After replacing the drive, check the grounding of the drive and the cable, the wiring of the digital inputs (E-stop, quick

stop, trigger, enable, limit switch inputs) and the torque of the fastening screws for ACOPOSmulti modules.

Additional information about this is provided in the respective user's manual.

Figure 84: ACOPOSmotor

Figure 82:

ACOPOS

Figure 83: ACOPOSmulti (in back), ACOPOSmicro (on the left) and

ACOPOSinverter

User's manuals include danger notices with important information about how to use the respective sys-

tems. The instructions in the user's manuals must be followed.

## Page 54

54DIAGNOSTICS AND SERVICE TM920

7.2.5Motion control - Motors

B&R motors and 3rd-party motors (motors from other manufacturers) are handled entirely differently.

The following applies to B&R motors.

Figure 85: 8LSA56.E0060D000-0 -

Synchronous motor, size 5, with EnDat encoder

Figure 86: Premium planetary gearboxes

B&R motor typeDescription of the document

8LVA, 8JS, 8LT, 8MS, etc."Data sheet 8LVA", "Data sheet 8JS", etc.

Stepper motors"User's manual for stepper motors"

Planetary gearbox"System overview of planetary gearbox"

ACOPOSmotor"User's manual for decentralized motion control"

Table 6: Overview of user's manuals for motors on the B&R website

## Page 55

TROUBLESHOOTING55

7.2.6Integrated safety technology

Safe I/O modules, SafeLOGIC, SafeLOGIC-X

SafeLOGIC controllers handle safe evaluation of data from components such

as light curtains or E-stop buttons.

The  describesuser's manual for integrated safety technology

exactly how to handle safety-related components and mainte-

nance scenarios. All safety notices and danger specifications

must be followed.

Figure 87: X20SL8100 - SafeLOGIC

In the user's manual for integrated safety technology, the technical data provided for the respective

SafeLOGIC controllers includes descriptions of maintenance scenarios.

The description of SafeLOGIC controller SL8100, for example, can be found in chapter 2 "X20 System",

subchapter 2.6.7.2.6 "Maintenance scenarios". These scenarios include aspects like replacing a module.

How are safety-related sensors, actuators and drives connected?

Instructions on connecting safety-related sensors, actuators and drives with the STO function can also

be found in the user's manual for integrated safety technology. Information about switching on safe-

ty-related actuators can be found in chapter 2 "X20 modules" under connection descriptions of secure

digital output modules.

Remote Control dialog box

SafeLOGIC-X controllers have neither selector switches

nor confirmation buttons. Instead, a Remote Control dia-

log box can be embedded in the HMI application by the

machine manufacturer. This can be used, for example, to

confirm a replaced module. The same dialog box can also

be used for the current model of the X20SLxxxx series.

Figure 88: Remote Control dialog box embedded in the HMI application

"$safety" logger file

The Logger (6.1 "SDM functions" on page 34) also saves important information related to the state of the safety con-

troller. These files can be stored locally when Logger file "$safety" is selected.

Further information:

6.3 "Saving Logger files" on page 38

•

## Page 56

56DIAGNOSTICS AND SERVICE TM920

ACOPOSmulti with SafeMOTION

In contrast to standard ACOPOSmulti inverter modules, ACOPOSmulti mod-

ules with SafeMOTION are able to evaluate encoder signals internally with re-

gard to safety.

The  describes ex-user's manual for ACOPOSmulti SafeMOTION

actly how to handle safety-related components and mainte-

nance scenarios. All safety notices and danger specifications

must be followed.

The maintenance scenarios are described in the user's manual

for SafeMOTION.

Maintenance scenarios are described in chapter 6 "Safety tech-

nology", subchapter 6.7.5. Included are descriptions of ACOPOS-

multi SafeMOTION inverter module replacement, motor re-

placement and updates.

Figure 89: 8BVI0014HCDS.000-1 - ACOPOSmulti

SafeMOTION inverter modules

SystemTitle of user's manual

Safety technology"Integrated safety technology user's manual"

Safe motion controlUser's manual for SafeMOTION

Table 7: Overview of user's manuals for safety on the B&R website

7.3Starting up again and functional testing

The following step sequence can be used when replacing a defective module. System Diagnostics Manager is available

as support for diagnostics.

## Page 57

TROUBLESHOOTING57

Figure 90: Required steps for replacing a module

The status of System Diagnostics Man-

ager in the image shows that there are

no more errors identified by the control

system.

Functional testing must be used to as-

certain whether the machine runs prop-

erly.

Figure 91: System Diagnostics Manager start page

Embedded SDM in the HMI application

Machine manufacturers are able to integrate the SDM right in the HMI application. The same functions are available

as those provided when running the SDM in a web browser. Just the display of diagnostic pages differs a bit from the

embedded SDM version.

## Page 58

58DIAGNOSTICS AND SERVICE TM920

System overviewSystem loggerDrive technology diagnostics

Figure 92: SDM system overviewFigure 93: SDM system loggerFigure 94: SDM drive technology diagnostics

DescriptionInformation source

Machine manual

•Safety and general notices

B&R user's manuals

•

HMI application

•

LED status indicators

•Status of B&R controller, I/O modules and drives

System Diagnostics Manager

•

Runtime Utility Center

•

Turning on and starting up the machineMachine manual

•

HMI application

•

Machine process functionMachine manual

•

mapp technology WebXs24

•

Table 8: Information sources for starting up again and function testing

24If the machine application uses mapp technology, then there is a web-based diagnostics for the mapp functions.The machine manufacturer, however, must document in the

machine manual which mapp components are responsible for controlling a certain machine function.

## Page 59

BACKING UP AND RESTORING59

8Backing up and restoring

Software updates and software installation packages are only available from the machine manufacturer. A software

update is only necessary if there is a software error or if changes have been made to the functions in the software.

The following chapters describe how to back up and restore the CompactFlash card and how to create your own In-

struction Lists using the Runtime Utility Center.

There are many conditions dictating whether or not the software on a machine controller can be backed

up and restored. In many cases, the source files for the control software are not stored on the Compact-

Flash card or on the internal Flash memory.

Battery-backed data from the machine controller is not applied when backing up and restoring data.

Backing up data requires knowledge of the control software. All necessary information is provided by the

machine manufacturer and the maintenance manual for the machine.

8.1Backing up and restoring using the Runtime Utility Center

The Runtime Utility Center is a system tool that provides a range of utilities for diagnostics and service on B&R con-

trollers. The installation program for the Runtime Utility Center is included in the Automation Studio installation or

can be downloaded separately from the B&R website.

Figure 95: Runtime Utility Center start page

The most important functions are:

Performing service functions via an online connection to the controller

•

Variable functions for backing up and restoring process variables

•

Creating individual Instruction Lists for testing and installation procedures

•

Backing up and restoring a CompactFlash/CFast card

•

Offline installation of a control project on a CompactFlash/CFast card

•

Creating project installation packages for USB installation

•

Custom mode allows the creation of a user-defined user interface

•

How can I open the Runtime Utility Center help documentation?

The Runtime Utility Center contains complete help documentation. This help documentation is opened

by pressing the . The Runtime Utility Center must be opened before doing this. The following<F1 key>

entries provide additional important information about using the Runtime Utility Center.

Runtime Utility Center \ Start page

Runtime Utility Center \ Operation \ Workspace

Runtime Utility Center \ Operation \ Commands \ Establish connection, wait for new connection

Runtime Utility Center \ Operation \ Commands \ PLC Info \ Logger

## Page 60

60DIAGNOSTICS AND SERVICE TM920

Downloading the Runtime Utility Center

The Runtime Utility Center is part of the  and can be downloaded from the B&R website:PVI development setup

www.br-automation.com  Downloads  "PVI Development Setup".→→

Figure 96: Downloads section, product group "Software"  "Automation NET/PVI"→

Installing the Runtime Utility Center

The downloaded installation package must be extracted before installation. The installation program can then be

started. No changes have to be made during the installation for use of the Runtime Utility Center.

Figure 97: Select a languageFigure 98: Clicking on "Start installation"

8.1.1Backing up and restoring a CompactFlash card

The following section will demonstrate the steps necessary to back up and restore the machine controller's Compact-

Flash card. When backing up, an image of the CompactFlash card is created and saved to the PC. When restoring, an

existing image is copied to the CompactFlash card.

When restoring an image, all of the data stored on the CompactFlash card will be lost. A conventional

card reader is required to back up and restore a CompactFlash card.

Backing up and restoring a CompactFlash card only involves the contents of the CompactFlash card.25

Process data in the battery-backed memory area is not taken into account. These data can be backed up

using the Runtime Utility Center (8.1.4 "Online connection and Instruction Lists" on page 65).

25Configuration and program modules on systems without CompactFlash cards are stored directly in the CPU's memory. These contents can only be backed up using an Instruction

List in the Runtime Utility Center. Program modules can be uploaded using the "Module functions" category.

## Page 61

BACKING UP AND RESTORING61

Procedure

The Runtime Utility Center is started via the Windows start menu.

In order to back up and restore the data on a CompactFlash card, it must first be connected to the PC using a card

reader. By clicking on menu option ", the dialog box for backing up and restoring the"Create / restore a drive image

CompactFlash card is opened.

Backing up a CompactFlash card

Step 1Step 2

Figure 99: Select "create image from disk"Figure 100: Select CompactFlash card from the list

The connected drives are reloaded via the "Refresh" but-

ton.

Step 3Step 4

Figure 102: An image file of the CompactFlash is created

Figure 101: Specify storage location and click on "Create image"

The memory location of the image file is selected by

clicking on the "Browse" button.

Restoring a CompactFlash card

Which CompactFlash card is suitable for recovery?

The CompactFlash card can only be restored on a CompactFlash card that is the same size or larger. This

function is limited to CompactFlash cards from B&R.

## Page 62

62DIAGNOSTICS AND SERVICE TM920

Step 1Step 2

Figure 103: Select "restore image to disk"Figure 104: Select CompactFlash card from the list

The connected drives are reloaded via the "Refresh" but-

ton.

Step 3Step 4

Figure 106: When restoring an image, all data on

the CompactFlash card will be deleted Proceed?

Figure 105: Select image file and click on"Restore image"

The memory location of the image file is selected by

clicking on the "Browse" button.

Runtime Utility Center \ Creating a list \ Data storage medium \ CompactFlash functions \ Generating

CompactFlash

Exercise: Backing up the CompactFlash card

For precautionary reasons, create an image file of the CompactFlash card's contents. This image can then be restored

at a later point in time. Before turning off the CPU, refer to the LED status indicators or the SDM to check the status

of the backup battery.

1)Check the status of the backup battery

2)Turn off control CPU

3)Remove the CompactFlash card

4)Insert CompactFlash card to card reader and connect to PC

5)Start Runtime Utility Center, select "Create / Restore a disk image"

6)Select CompactFlash

## Page 63

BACKING UP AND RESTORING63

7)Select the memory location for the image

8)Create image file

8.1.2Backing up files from CompactFlash

A CompactFlash card can contain multiple partitions. In Windows, only the "C" drive of the CompactFlash card can be

seen. Runtime Utility Center can be used to back up and restore files from the other partitions.

Click on menu option" to open the dialog box for backing up the CompactFlash"Create, edit and execute projects (.pil)

files.

In the Runtime Utility Center, the "Tools" tab is used to

get to option "Back up files from CompactFlash card /

" where the dialog box for saving the files canimage file

be opened.

The CompactFlash card is connected to the PC using a

card reader.

Figure 107: Runtime Utility Center "Tools" \ "Backup files from

CompactFlash card" / "Back up image file"

Step 1Step 2

Figure 108: Select CompactFlash card from the list

Click on "Select disc", select CompactFlash card and

click on "OK"Figure 109: Back up Logger file "$arlogsys" from CompactFlash card

Select Logger file, set destination directory and click on

"Start"

## Page 64

64DIAGNOSTICS AND SERVICE TM920

The files selected will be stored in the destination directory with the same structure shown in the selec-

tion dialog box.

The files can be copied to another CompactFlash card via tab "Extras", option"Restore files to Compact-

.Flash card"

Runtime Utility Center \ Operation \ Backing up \ Restoring files on CF

Exercise: Back up the Logger file from the CompactFlash card

The Logger file is stored on the CompactFlash card. While the Logger file can be uploaded using SDM, the Runtime

Utility Center offers the possibility to back up the Logger file directly from the CompactFlash card. The Logger file

name is "". The goal of this exercise is to find the Logger file on the CompactFlash card and then back it up.$arlogsys

1)Start Runtime Utility Center

2)Select "Tools - Back up files from CompactFlash" from the main menu

3)Select CompactFlash

4)Select the Logger file "$arlogsys".

5)Specify destination directory

6)Create file backups

The Logger file can be used by the machine manufacturer or B&R for analysis.

If the control CPU is no longer operational, the Logger file can still be backed up by the CompactFlash

card for further analysis by following the procedure described above.

8.1.3Generate project installation package

Updating the control software is performed using a project installation package. Using a USB flash drive, the software

can then be transfered directly to the CPU via a DHCP/FTP server or a CompactFlash card. The project installation

package is created from a Runtime Utility Center export package provided by the machine manufacturer.

Loading Runtime Utility Center export package

Select "" to load the Runtime Utility Center export package. Then, the following functions areOpen project (ZIP, .pil)

available:

Figure 110: Runtime Utility Center start page with export file already loaded, see header of window

## Page 65

BACKING UP AND RESTORING65

Performing offline installation

•

This function can be used to perform an initial transfer to a CompactFlash/CFast card.

Creating project installation package

•

This function can be used to create a project installation package, e.g. for USB installation.

Runtime Utility Center \ Creating a list / data medium \ Project installation

If the project installation package has been transfered to a USB flash drive, for example, this can be inserted in the

controller's USB port. The next time the controller is restarted, it checks the version of the application and, if necessary,

transfers it to the controller. However, this function has to have been enabled in the control project.

8.1.4Online connection and Instruction Lists

The Runtime Utility Center provides the option to create Instruction Lists for service purposes. This makes it possible

to group multiple commands into a single list. The entire list can then be executed as a sequence.

As with SDM,the Ethernet interface is available for connecting to the controller. The network settings might have to26

be changed on the PC, see (10.3 "Changing network settings on the PC" on page 71).

Additionally, a connection via a serial interface is supported.

What has to be considered when connecting via the serial interface?

In this case, make sure the serial interface is not already being used by the control program. A serial cable

would also be required for this. For a description of the cable construction see:10.1 "Online connection

cable" on page 70.

8.1.5Outputting data via online connection

The following section describes how to establish a connection to the control CPU using the Runtime Utility Center.

The upload of the Logger file was used as an example. Further procedures must be configured in the same way.

First, open the Runtime Utility Center and select menu option "". The fol-Create, modify and execute projects (.pil)

lowing view is displayed:

Description of window:

1)Command set

2)Instruction List: The list of com-

mands to be executed is created

here

3)Short description of the selected

command

Commands are added to the Instruc-

tion List using drag-and-drop

Figure 111: Runtime Utility Center main window

26The Runtime Utility Center and System Diagnostics Manager offer several diagnostic options in equal measure. For a comparison of the two tools, see 10.2 "Where SDM and

Runtime Utility Center can be used" on page 70.

## Page 66

66DIAGNOSTICS AND SERVICE TM920

Establish the connection

Description of steps:

1)Drag command "Connection" from

the command set and drop it in the

Instruction List. The configuration

dialog box will open automatically

2)Select device type "Ethernet

(TCPIP)"

3)Select "Properties"

4)"IP address / Set hostname" and

enter IP address

5)"Select "Obtain destination ad-

dress automatically"

Figure 112: Adding an Ethernet connection to the Instruction List

After completing the settings, close the dialog box by pressing "OK".

Which value must the destination and source addresses have?

The destination address is the node number of the communication interface set on the control CPU. The

source address must not be the same as the destination address. For example, if the destination address

has the value 1, then any other value can be used for the source address except for 1. A connection cannot

be established in some circumstances to the controller if both values are the same.

Add command "Logger"

Description of steps:

1)Drag command "Logger" from27

the command set and drop in In-

struction List. The configuration

dialog box will open automatically

2)Select "Load system logger mod-

ule"

3)Select output format

4)Select target path

After all settings have been made, the

configuration dialog box can be closed

by pressing "OK".

Figure 113: Adding command "Logger" to Instruction List

27Command "Logger" only works for systems whose user memory is organized as a file system. For all other controller CPUs, command "Logbook" must be used.

## Page 67

BACKING UP AND RESTORING67

Execute Instruction List

The Instruction List can now be execut-

ed. This is done by clicking on the "Exe-

cute" icon in the menu bar or by press-

ing the "F5" key. A confirmation window

appears before the Instruction List is

executed.

Figure 114: Execute Instruction List

A progress indicator appears after the

sequence has been started. As they are

executed, the commands appear with

their status in the output window. The

sequence can be aborted at any time by

clicking on "Stop".

Figure 115: Display of progress indicator for executing Instruction List

Runtime Utility Center \ Operation \ Workspace

Runtime Utility Center \ Operation \ Commands \ Establish connection, wait for new connection

Runtime Utility Center \ Operation \ Commands \ PLC info

Runtime Utility Center \ Operation \ Menus \ Start

Exercise: Read Automation Runtime version and save Logger file

The Automation Runtime version provides information about the integrated functions of the controller. The diagnostic

data that can be provided depends on the Automation Runtime version, among other things. Information on events on

the controller is recorded in several Logger files. This information helps to localize sources of errors. Read the installed

Automation Runtime version and upload Logger file "$arlogsys" from the controller.

1)Start Runtime Utility Center

2)Click on menu option "Create, edit and execute projects (.pil)"

3)Add command "Connection" to Instruction List

4)Add command "AR Version" to Instruction List

## Page 68

68 DIAGNOSTICS AND SERVICE TM920
5) Add command "Logger" together with Logger file "$arlogsys" to Instruction List
6) Start Instruction List
Exercise: Save profiler file using the Runtime Utility Center
If an error occurs, such as a cycle time violation, then the controller restarts in service mode. Information about the
causative task class is saved in the Logger file. The profiler file contains additional details on the error. Every newly
created profiler file receives a new name. The names of the profiler files end with the string "$f" and can be identified
by uploading a module list.
1) Start Runtime Utility Center
2) Click on menu option "Create, edit and execute projects (.pil)"
3) Add command "Connection" to Instruction List
4) Add command "module list" to Instruction List
5) Start Instruction List
6) Evaluate the names of the profiler files from the module list
7) Add command "Upload" to Instruction List and add the name of the desired profiler file
8) Start Instruction List
Exercise: Backing up all process variables
Perform a data backup by saving all of the variables on a control CPU to a file. The backup can contain parameters, cur-
rent process values or settings. The values can only be interpreted in combination with the control project or through
knowledge of the machine manufacturer.
1) Start Runtime Utility Center
2) Click on menu option "Create, edit and execute projects (.pil)"
3) Add command "Connection" to Instruction List
4) Add command "Variable List" to Instruction List
5) Start Instruction List
Runtime Utility Center \ Operation \ Menus \ File
Runtime Utility Center \ Operation \ Commands \ Establish connection, wait for new connection
Runtime Utility Center \ Operation \ Commands \ List functions
Runtime Utility Center \ Operation \ Menus
After saving all variables from the controller, a file with all variable names and corresponding values is
created. This file can be used at a later point in time for restoring the process data. This is done by using
the process variable function, "Write variable list to PLC".
8.2 Access to the integrated FTP server
B&R control systems have an integrated FTP28 server. If the FTP server is enabled, then the Flash memory of the con-
troller can be accessed. In order to do this, the configured access rights must allow it and the username and password
must be known. Username and password cannot be reset or changed. The access data for the FTP server can be found
in the corresponding documentation of the machine manufacturer
For example, configuration files and recipe files can be backed up and restored via this mechanism.
FTP can be accessed, for example, via the Windows command line with the command "ftp ip-adresse", Internet Explorer
or third-party tools (e.g. FileZilla).
28 File Transfer Protocol (FTP) is a network protocol (specified in the RFC 959 from 1985) for transferring data via IP networks. Source: de.wikipedia.org

## Page 69

SUMMARY69

9Summary

First an attempt is made to create an overview for a comprehensive diagnosis. The resulting error description helps

with narrowing down to the error source.

A wide range of influences from the surroundings, the raw materials used and process parameters can affect how a

machine works.

B&R products enable complete diagnostics via LED status indicators, System Diagnostics Manager and Runtime Utility

Center. Complete data sheets and wide-ranging user manuals document the technical data, the installation and the

maintenance of all B&R modules.

Figure 116: Examine surroundingsFigure 118: Correct errors

Figure 117: Contact manufacturer

The B&R system overview showed the basic structure of the B&R control system. The diagnostics possibilities that are

available both with and without a PC were also discussed.

A complete overview of the user's manuals for the individual B&R product groups was provided. A checklist was also

introduced to help prepare yourself before contacting B&R.

## Page 70

70DIAGNOSTICS AND SERVICE TM920

10Appendix

10.1Online connection cable

Ethernet connection cable

A standard crossover network cable is recommended when connecting the B&R controller to a PC.

Serial connection cable

A cable with the following pinout is required to connect a B&R controller to a PC via the serial interface.

9-pin female DSUB connec-

PCControl9-pin female DSUB connector

tor

2 (RXD)3 (TXD)

-----

3 (TXD)2 (RXD)

-----

5 (GND)5 (GND)

-----

Table 9: Cable pinout for online connection cable

9-pin female DSUB connec-

PCControlX20 controller terminal block

tor

2 (RXD)11 (TXD)

-----

3 (TXD)21 (RXD)

-----

5 (GND)22 (GND)

-----

Table 10: Cable pinout for direct connection to an X20 terminal block

10.2Where SDM and Runtime Utility Center can be used

The following tables compare the possibilities of the System Diagnostics Manager (SDM) and the Runtime Utility Cen-

ter (RUC).

In general, the SDM can only be used if a system has an integrated web server and it is enabled. Read access is granted

on the controller. No additional software installation is necessary on the PC.

The Runtime Utility Center can be used to diagnose systems without integrated web server and to back up and restore

data. A serial or Ethernet port can be used for connection. The Runtime Utility Center must be installed on a PC with

a Windows operating system before use.

General diagnostics

FunctionSDMRUC

Output and set timeYesYes29

Output battery statusYesNo

Output system versionYesYes30

Table 11: Comparison of general diagnostics functions

29Time can only be output in SDM.

30The runtime environment has software versions for the operating system, motion control and visualization.In the Runtime Utility Center, only the operating system version can

be output.

## Page 71

APPENDIX 71
Function SDM RUC
Read Logger31 Yes Yes
Output hardware list Yes Yes
Detect hardware problems Yes No
I/O and read status data points Yes No
Determine system usage Yes No
Output memory usage Yes Yes
Output configured timing Yes No
Output module lists Yes Yes
Output axis errors Yes No
Reading network command trace Yes No
Install trace configuration32 Yes No
Table 11: Comparison of general diagnostics functions
Backing up and restoring
Function SDM RUC
Saving Logger files Yes Yes
Saving profiler files Yes Yes33
Generate system dump Yes No
Upload software objects No Yes
Download software objects No Yes
Backing up and restoring a CompactFlash card No Yes
Back up and restore variables No Yes
Output and display variables No Yes
Table 12: Comparison of back up and restore options
10.3 Changing network settings on the PC
The IP address of the Ethernet interface can be changed on the PC. This is done via the settings of the network adapter,
see Windows 10 "Control Panel" \ "Network and Internet" \ "Network and Sharing Center" \ "Change adapter settings".
All available network connections are displayed.
Right-click on the desired adapter, then click on "Properties". Select "Internet Protocol, version 4 (TCP/IPv4)" and click
on "Properties". Now the IP address can be changed.
As shown in the example, a static IP address with value "10.0.0.1" and subnet mask "255.255.255.0" are assumed for the
controller. The configuration of the PC must be in the same subnet.
31 There are usually several Logger files on current B&R control systems for diagnosing different functional areas of the controller. For control systems that do not have a USB in-
terface or an onboard Ethernet interface, function "Logbook" of the Runtime Utility Center must be used.
32 Installing a trace configuration makes it possible to record pre-configured process values on the runtime system.The finished recording can then be uploaded from the con-
troller for further analysis.
33 The profiler file can be loaded via the "Upload" module function on the controller. The name of the profiler file ends with the string "$f" and can be identified by uploading a
module list.

## Page 72

72DIAGNOSTICS AND SERVICE TM920

Figure 119: Windows 10: Change adapter settings for the LAN Connection adapter under Network and Sharing Center.

We recommend writing down the original adapter settings before making any changes.

By entering "Show network connections" in the search field of the Windows 10 Start menu, the selection

is already restricted making it possible to get the list of network adapters more quickly.

10.4Notes regarding grounding and shielding

The following section compiles a few universally applicable recommendations for grounding and shielding. The regula-

tions in the respective user's manual always apply. The procedure for correcting problems on machines where ground-

ing and shielding result in failures should be adapted on a case-by-case basis.

Shielding and grounding for control systems

On all shielded cables, the shield must be grounded. These include:

Analog signals (input and output modules)

•

Interface modules

•

Counter modules

•

X2X Link cables

•

Fieldbus connections (POWERLINK, PROFIBUS DP, CAN, etc.)

•

Figure 120: X20DC1176 connection schematic -

Encoder cable connection with shielding

The following general guidelines apply for shielding:

The top-hat rail must always be mounted to a conductive backplane.

•

The control cabinet back wall must be connected with GND.

•

Shielded cables must be grounded on both sides.

•

## Page 73

APPENDIX73

Figure 122: Shielding via top-hat rail or busbar

Figure 121: Shielding via X20 cable shield clamp

Figure 123: Wiring diagram for X20 modules

with an Ethernet cable

"X20 system user's manual"Content

Chapter 4 "Mechanical and electrical configura-Direct connection of the shield to the ground con-

tion"nection of bus modules

Cable shielding plate

Chapter 4.6

Shielding bracket

Shielding via top-hat rail or busbar

Chapter 4.7Wiring guidelines for X20 modules with an Ether-

net cable

Chapter 7.10.2General specifications for X2X Link cables

Chapter 8.2.2Requirements for immunity to disturbances

Chapter 8.2.3Emission requirements

Table 13: Notes regarding shielding and grounding in the control system

Installation / EMC guideContent

Chapter 3.3.3 "Installation with increased vibra-Padding, retaining clips, adjustment, covering un-

tion requirements (4 g)"used slots, strain relief

Chapter 3.4.1.2 "Shield connection"Ground connection, shield terminal blocks, bend

radius

Chapter 6.3 "Buffer module"24 V power supply buffering

Table 14: Installation notes for control systems

## Page 74

74 DIAGNOSTICS AND SERVICE TM920
Shielding and grounding for drive technology
To prevent the effects of disturbances, the following cables must be properly shielded:
•
Motor cables
°
Encoder cables
°
Control cables
°
Data cables
°
Inductive switching elements such as contactors or relays must be equipped with corresponding suppressor ele-
•
ments such as varistors, RC elements or damping diodes.
All electrical connections must be kept as short as possible.
•
Cable shields must always be attached to the designated conductive shielding clamps and the connector hous-
•
ing. Twisting the braided shield or extending it with individual conductors is not permitted!
Shielded cables with copper braiding or tinned copper braiding must be used.
•
Unused cable conductors must be grounded on both sides whenever possible.
•
"ACOPOSmulti user's manual", chapter 5 "Wiring":
Subsection 1.1 "EMC-compliant installation"
•
Subsection 1.1.4 "Connection diagrams for ground connections and shield connections"
•
Exercise: Check the installation with regard to electromagnetic compatibility (EMC)
Downtime caused by EMC problems can be prevented by installing appropriate components. Corresponding recom-
mendations are provided in the data sheets and user's manuals. B&R components are designed for complete shielding
and grounding. This includes shield component sets available as accessories for control and I/O systems, for example.
B&R recommends connecting controllers and remote I/O systems to the cables offered in the B&R product catalog. A
detailed cable specification for use in industrial environments is provided in the respective data sheet.
1) Look up the shielding and grounding concepts for X20 modules in the X20 system user's manual
2) Cable shield for X20 modules - Shield component set
3) Look up the specifications for X2X and POWERLINK cables
User's manual "X20 System user's manual":
Chapter 4.6 "Shielding"
•
Chapter 4.7 "Cabling guidelines for X20 modules with Ethernet cables"
•
Chapter 7.4 "Cable shield plates"
•
User's manual "Installation / EMC guide"
Data sheet "POWERLINK cable X20CA0E61.xxxx", search within data sheet for "X20CA0E61.00020"
Data sheet "Data sheet X2X Link Cable (X67CA0Xxx)", search within data sheet for "X67CA0X99.1000"
10.5 Power supply and protection
The X20 system is protected according to the power supply concept. Using the X20BM01 bus module and organizing
the power supply bus modules accordingly allows various potential groups to be implemented (e.g. for input groups
or various power circuits for the outputs).1)
1) A 1 A slow-blow fuse is recommended for the bus supply.

## Page 75

APPENDIX75

Figure 124: Protecting various potential groups

It is possible to set up potential groups through the use of different supplies for the power supply modules.

Figure 125: Example of extended X2X Link power supply

X20 system user's manual:

Chapter 4.8 "Power supply concept"

•

Chapter 4.9 "Fuse protection of the X20 system"

•

Chapter 4.9.1 "Potential groups"

•

Chapter 4.9.2 "Supply via bus transmitter"

•

Chapter 4.8.12 "X2X Link supply"

•

Chapter 4.8.12.2 "Extended and redundant X2X Link supply"

•

## Page 76

76DIAGNOSTICS AND SERVICE TM920

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

## Page 77

AUTOMATION ACADEMY 77

## Page 78

78 DIAGNOSTICS AND SERVICE TM920

## Page 79

AUTOMATION ACADEMY 79

## Page 80

B&R

Industrial Automation GmbH

A member of the ABB Group

B&R Straße 1

5142 Eggelsberg, Austria

office@br-automation.com

t +43 7748 6586-0

f +43 7748 6586-26

br-automation.com

V2.0.0.1 ©2023/10/23 by B&R, All rights reserved.

All registered trademarks are the property of their respective owners.

Subject to technical changes without notice.