## Page 1

TM600

Introduction to HMI

## Page 2

2 INTRODUCTION TO HMI TM600
Prerequisites and requirements
General Basic computer knowledge
Software None
Hardware None

## Page 3

TABLE OF CONTENTS 3
Table of contents
1 Introduction.........................................................................................................................................................4
1.1 Learning objectives..............................................................................................................................4
2 Visualization – A Definition..............................................................................................................................5
3 Human-machine communication....................................................................................................................6
4 HMI Applications in Automation.....................................................................................................................7
4.1 From the switchboard to touch screen visualization...................................................................7
4.2 Demands on the HMI application.....................................................................................................7
4.3 Selection criteria..................................................................................................................................9
4.4 Visualization concepts......................................................................................................................11
5 HMI design aspects.........................................................................................................................................14
6 Summary.............................................................................................................................................................17

## Page 4

4INTRODUCTION TO HMI TM600

1Introduction

This training module provides a brief look into the world of visualization. The central role of visualization, HMI systems,

basic HMI concepts as well as the aspects when designing HMI applications are explained.

Visualization in the area of automation has a central significance and is becoming more important as a data manage-

ment and diagnostic interface.

Figure 1: "A picture is worth a thousand words"

Pictures in general are a representation of the account of arbitrary processes, situations and occurrences. They help

the observer to quickly understand the situation being portrayed.

This was true for caveman paintings, for paintings made by famous artists – and still holds true for machine visualiza-

tion in HMI applications today.

1.1Learning objectives

This training module is designed to help you understand the various aspects that must be considered when designing

an HMI application.

Participants will learn the characteristics of HMI applications and where they are used.

•

Participants will learn what needs to be considered when designing an HMI application.

•

Participants will learn about B&R's portfolio of HMI products, their characteristics and advantages.

•

Participants will learn the importance of establishing standards and taking user groups into consideration.

•

## Page 5

VISUALIZATION – A DEFINITION5

2Visualization – A Definition

Since the dawn of man, visualization has played a central and significant role in the understanding of information.

The reasons for this phenomenon can be found in the physiology of the human eye and the visual cortex to which it

is connected.

From all of the human sense organs, the vision apparatus contains the highest bandwidth for absorbing information.

This fact is supported by the common phrase "A picture is worth a thousand words".

Figure 2: "A picture is worth a thousand words"

Visualization (in the sense of information technology) is the precise transformation of data to a visual image to support

the exploration, cognition and explanation of structures and processes.

Catch phrases such as "visual thinking", "visual communication", creativity via "visual brainstorming" or "virtual reality"

and technical development are evidence of the increasing importance of visualization over the last few years.

The advantages of process visualization:

Quick orientation thanks to self-documenting procedures

•

Monitoring of procedures

•

Optimal system operation

•

This provides a basis for a short process analysis of time, reduced costs, quality and increased value potential.

Visualization

## Page 6

6INTRODUCTION TO HMI TM600

3Human-machine communication

The term human-machine communication includes all of the interaction procedures involved when a person is oper-

ating a machine. Human-machine communication bridges the differences between human language and machine lan-

guage.

This means the commands, which the operator enters, and the response, which he/she receives from the device.

"The focus is on the human, not the machine"

Figure 3: Human-machine communication

Operating and monitoring systems must meet users' multifaceted requirements and expectations. An HMI system

should make system architecture tangible, and should also provide the highest possible operating comfort.

The goal is to ensure that the machine or system is in the optimum operating state and achieves maximum perfor-

mance. It is important here to quickly provide users a an overview of the machine or system.

The top priority is preventing system downtime and detecting errors as early as possible.

## Page 7

HMI APPLICATIONS IN AUTOMATION 7
4 HMI Applications in Automation
HMI is becoming increasingly important in automation.
In the early days, the user could only interact with a machine's process using conventional methods such as lights,
level indicators, buttons and switches and monitoring the machine's status was only possible using analog gauges. In
recent years, increasing demand has been placed on visualization with regard to functionality and ergonomics.
"Integrated HMI lives up to the demands of automation."
This is another result of the increased performance of HMI hardware and their sinking prices. Evidence of this trend
can be seen in the recent shift from remote HMI to integrated HMI.
4.1 From the switchboard to touch screen visualization
Switchboards
Switchboards with rudimentary process monitoring instruments were inflexible and resulted in high production costs
due to the extensive amount of manual handwork required.
Intelligent switchboards
Intelligent switchboards were somewhat more flexible, but still expensive.
Line displays
By using line displays with keys provided a great step forward in terms of flexibility.
CRT monitors
CRT monitors with keyboards enabled line-oriented data output with a higher concentration of information, and later
a fully graphical display was also enabled. However, collectively this technology in an industrial environment was very
prone to error.
Industrial PCs
In comparison, industrial PCs with flat-screen displays and matrix keyboards are more resistant and allow different
operating systems to be used.
Industrial PCs / Panels
Operator panels with touch screens and/or buttons are the latest in visualization technology. They enable intuitive
operation and process monitoring.
Smartphones and tablets
Smart devices such as tablets, smartphones, etc. are considered perfect examples of powerful technology with ulti-
mate usability. Unsurprisingly, operators of industrial machines and systems – and therefore also manufacturers of
such equipment – desire nothing less when interacting with the machinery they use every day.
4.2 Demands on the HMI application
This section describes the most important demands that are placed on an HMI application.
When designing a visualization concept, one should keep in mind that a list of requirements is created that identifies
and prioritizes individual criteria. Once this specification has been established, it is then possible to select the visual-
ization hardware and the tools necessary for implementation.
"Exact specifications are the foundation of every solution"
The main aspects of an HMI application involve the monitoring and operation of a machine or plant.
Monitoring processes can be performed in various ways. Processes are usually displayed in a process chart while status
messages are often displayed in an alarm system or indicated via external status displays such as status lamps or
key LEDs.

## Page 8

8INTRODUCTION TO HMI TM600

Machine and system status:

Process charts

•

Key LED indicators

•

Figure 4: Process chart

Messages and alarms:

Critical, targeted process monitoring

•

There are various guidelines regarding input for the operation of a visualization, which depend greatly on the functional

specification for the area of application.

A combination of different operating concepts, such as touch and key operation, is also possible.

Operating the system:

Changing process data

•

Sending commands to the process flow

•

Operating concepts:

Membrane keys, short stroke keys

•

Keyboards, buttons and switches

•

Touch screen1

•

Figure 5: For every challenge there's the right solution

1Different touch technologies are used in automation depending on the requirements.Resistive touch displays enable the evaluation of single-touch entries and can also, for ex-

ample, be operated with gloves.Capacitive touch displays enable multi-touch entries, depending on the implemented visualization software, and can be operated using your fin-

gers, with special gloves on or with specially-made stylus pens.

## Page 9

HMI APPLICATIONS IN AUTOMATION9

Diagnostics and service:

Analysis of problematic situations

•

Support during problem correction

•

Local or remote system diagnostics integrated

•

Historical record of events

•

Role-dependent HMI content for service technicians

•

Figure 6: Machine status diagnostics

The production data obtained during a process must be recorded for analysis and further processing afterwards.

Data management:

Saving and loading process data

•

Storing data on the server

•

Printing and archiving

•

Recipes

•

Machine parameters

•

Figure 7: Data management

4.3Selection criteria

The HMI application which should be applied at the end of the process depends on many factors.

Selection criteria:

Mechanics and ergonomics of the machine

•

System and machine type

•

Visualization concept

•

Requirements for the HMI application

•

Screen size

°

Additional software components

°

Networking

°

Tool for the developers

•

Education

°

Qualifications

°

Hardware and software knowledge

°

Development costs, runtime costs and licenses

•

Training and support

•

Commissioning, service and maintenance of software

•

Software scalability and expandability

•

## Page 10

10 INTRODUCTION TO HMI TM600
"For every application and requirement there's the right solution"
Long-term availability of the hardware and software components and the ability to provide a complete solution are
significant factors for product selection.
Possibilities for visualization in the B&R system
Type Platform
Integrated visualization Server: Automation Runtime
mapp View Client: HTML5 browser
Integrated visualization Automation Runtime or
Visual Components Microsoft Windows
PVI-based visualization Microsoft Windows
PviServices Visual Studio.Net
OPC-based visualization for SCADA packages
Microsoft Windows
(Supervisory Control And Data Acquisition)
APROL process control system Linux
Table 1: Overview of available visualization software
Visualization \ mapp View
Visualization \ Visual Components
Communication \ PVI
Communication \ OPC
Communication \ OPC UA
A hardware–software combination results from the profile of demands.
This means that compromises regarding functionality and expansion possibilities might have to be reached.
A comparison for the selection looks something like this:
Integrated visualization Proprietary development 3rd-party systems (SCADA)
Time investment Application-dependent Application-dependent Application-dependent
B&R support 100% Support for communication Support for communication
with the B&R system with the B&R system
Training course Yes No No
Runtime costs 2 Low Unknown High
Standard components 100% Unknown Unknown
Hardware/Software availabil- Several years Unknown Unknown
ity
Complete system from a sin- 100% Unknown Unknown
gle source
Table 2: Selection profile and comparison of advantages
Keep in mind there is no standard visualization that can meet all demands 100%. However, the difference between the
standard and the requirements can usually be bridged with a little work on the application.
"Learn to understand the limitations"
Physical or product-related peculiarities often lead to the rash assumption that a product is defective. Attention must
be given to this aspect when putting together the user profile.
2 Runtime costs relate to, for example, the licensing costs for the number of devices installed in the field.

## Page 11

HMI APPLICATIONS IN AUTOMATION11

A single-touch system does not allow multiple buttons to be pressed at the same time due to the nature

of the touch technology being used. What gets registered is the recorded X/Y position.

4.4Visualization concepts

There are different methods and solutions for an HMI application depending on the structure and the specifications

of the machine.

This ranges from a visualization consisting of a controller and visualization terminal via the networking of several

machine elements to fully integrated remote operation.

Local – machine-related operation

The operator panel in the illustration assumes the functionality of a controller alongside displaying the visualization.

Remote input and output modules are connected over a bus controller via a fieldbus. The configuration is extended

over remote drive technology over the same fieldbus.

Features:

Located right on a mechanical operating unit

•

Figure 8: Power Panel: Operator panel and controller in a single device

Centralized operator station

A centralized controller or panel with integrated controller assumes the control of remote input and output modules,

integrated safety technology and remote drives. The centralized operator station is connected to the controller via

Ethernet. Furthermore, the network connection enables the networking of several machine parts and represents ac-

cess for remote operation.

Features:

Grouping of multiple operating units

•

Cross-machine networking possible

•

## Page 12

12INTRODUCTION TO HMI TM600

Figure 9: Centralized operator station with remote Ethernet connection

Remote operation

A network-capable controller enables remote access via known standard mechanisms. In addition to remote visualiza-

tion via web technology or VNC, data can also be exchanged via FTP, web server and OPA UA.

Features:

Targeted access via modem, intranet or Internet

•

Sending SMS

•

FTP server

•

WEB server

•

VNC server

•

OPC UA server

•

Figure 10: Seamless system diagnostics from any location using any Web browser

Machines in a group – networked visualization

The connection to a cross-machine network is enabled through the network capability of controllers and operator

panels. The data of independently working machines can be transferred to a higher-order operator panel over a net-

work. At this point, the gathered representation of status information of all machines as well as the recording of prod-

uct-relevant information occur, for example.

Features:

Similar machines

•

Self-contained functionality

•

Networked together

•

## Page 13

HMI APPLICATIONS IN AUTOMATION13

Figure 11: Networked operator stations with higher-level visualization

## Page 14

14 INTRODUCTION TO HMI TM600
5 HMI design aspects
The graphic representation of complex procedures is enabled through the use of the newest technology. This makes
the exchange of information between human and machine easier.
The developer is given more room for creativity through the provided options when creating graphical interfaces.
However, because the primary focus is on user ergonomics, a few rules must be placed on the design aspects.
The following rules must be taken into account:
Corporate identity requirements
•
Industry-specific standards and stipulations
•
System control
•
Service and commissioning
•
Update and maintenance
•
Error analysis / Remote maintenance
•
Future expansions from hardware and software scaling
•
Training for operating personnel
•
"Combine functional specification with user friendliness"
5.1 User groups
The user – the person operating the machine or system – must be the main consideration when creating an HMI ap-
plication.
What user groups need to be taken into consideration?
Machine operator
•
Lead machine operator
•
Foreman
•
Service technicians
•
Process engineers
•
Cleaning staff
•
When taking a closer look at the tasks of the individual operators, we can see corresponding requirements for the HMI
application:
Machine operator
The machine operator generally has limited access rights for operating and monitoring the system. This means that
he/she cannot see all of the functions and cannot fully intervene with the process. Furthermore, this can also mean
that each action made by the operator is logged in order to analyze any problems that may arise at a later point in time.
Lead machine operator
Unlike the machine operator, the lead machine operator has advanced access to the system and can fully intervene in
the process as well as change all relevant parameters.
Foreman
The foreman or production manager does not have to intervene in the process. They are only interested in the data
at the end of the process. How much has been produced? Was the system running at full production capacity? What
problems occurred? Why did these problems occur?
Service technician / Process engineer
The service technician / process engineer has full access in regard to operation and service and can decisively intervene
in the process. This person must be able to oversee and control the machine from the implementation to the error
analysis + problem detection + problem correction.
Cleaning staff
A special operating mode must be included for the cleaning personnel to clean the touch screen without inadvertently
actuating a command.

## Page 15

HMI DESIGN ASPECTS 15
5.2 Design-related information
In addition to ergonomics, these guidelines must be taken into consideration when creating an HMI application:
Display and operation of the process sequence
•
Service and analysis
•
Data management for logging / archiving
•
Human limitations
Limits:
The human eye does not register value changes faster than 200 ms (5 times per second).
•
When designing you shouldn't necessarily rely on the ability of the color vision.
•
The human ear considers a delay of more than 30-60 ms between an action and a resulting reaction as slow.
•
Process representation
Display types:
Textual display, when text is sufficient
•
Graphic display, when graphics are required
•
Moderate use of dynamic objects
•
Grouping of procedures
•
Effective menu guidance
•
"Less is more"
Usability
Methods:
Suitable, variable text size and graphical resolution
•
Color design (determined by CI)
•
Data entry support
•
Operating safety
°
Navigation between input fields
°
Plausibility test
°
Password protection
°
User interface
•
Keyboard input
°
Touch screen
°
Both in combination
°
Multiple operator stations in the system
Properties:
Uniform access methods
•
Common network
•
Different device types and designs combined
•
Main goal – A uniform and thorough operating philosophy
Requirements:
Page branches are designed in a structured and logical manner
•
Meaningful use of colors (an input field is displayed with a different color than a value indicator)
•
Meanings
•
Graphics
°
Icons
°
Key labeling
°
When designing data input features, remember that the operator cannot always empty his hands before inputting
data or operating and may even require gloves for his current task.

## Page 16

16 INTRODUCTION TO HMI TM600
When developing an HMI application, it is important to consider it from the perspective of an operator in the field and
not a developer in an office.

## Page 17

SUMMARY17

6Summary

In today's world, the design of elements in an HMI application, such as the standard behavior for data input or a but-

ton, is pre-determined by the software packages and usually can be influenced by the programmer of an application

through configuration.

Figure 12: A complete HMI application

Not only the developer's creativity while designing the HMI application, but also the user's needs should take center

stage during development.

The quality of a system is characterized by its HMI system: Who doesn't like working with a tool that is tailored to

their needs?

## Page 18

18INTRODUCTION TO HMI TM600

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

## Page 19

AUTOMATION ACADEMY 19

## Page 20

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