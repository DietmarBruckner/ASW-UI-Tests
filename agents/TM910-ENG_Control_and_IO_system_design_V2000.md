## Page 1

TM910

Control and I/O system

design

## Page 2

2 CONTROL AND I/O SYSTEM DESIGN TM910
Requirements
X20 user's manual V3.60 (2020-04-16)
Redundancy for control systems V1.20 (2021-12-04)
Installation / EMC guide V1.36 (2021-04-20)
Secure Remote Maintenance user's manual V1.50 (2021-04-09)
Data sheet - POWERLINK cables
User's manuals Data sheet - X2X Link cables
Software [optional] Microsoft Excel
Hardware -

## Page 3

TABLE OF CONTENTS 3
Table of contents
1 Introduction.........................................................................................................................................................4
1.1 Learning objectives..............................................................................................................................5
1.2 Symbols and safety notices...............................................................................................................5
2 B&R system overview........................................................................................................................................6
2.1 Terminology...........................................................................................................................................7
2.2 Typical topologies................................................................................................................................8
2.3 Serial numbers and model numbers................................................................................................9
2.4 Data sheets, user's manuals and information.............................................................................10
3 B&R website functions....................................................................................................................................13
3.1 Product section on the website......................................................................................................13
3.2 Compare products.............................................................................................................................15
3.3 Search function of the website.......................................................................................................16
3.4 Download section of the website..................................................................................................17
4 Mechanical and electrical configuration.....................................................................................................20
4.1 Technical data....................................................................................................................................20
4.2 Overall system data..........................................................................................................................24
4.3 Determining required I/O modules...............................................................................................25
4.4 Calculating power consumption....................................................................................................26
4.5 Additional module data to consider..............................................................................................27
5 Selecting and constructing the topology...................................................................................................28
5.1 POWERLINK topology........................................................................................................................28
5.2 Configuring POWERLINK I/O nodes..............................................................................................29
5.3 Infrastructure components for redundancy.................................................................................31
5.4 Integration of fieldbus systems.....................................................................................................33
5.5 Remote backplane.............................................................................................................................35
5.6 Secure Remote Maintenance...........................................................................................................37
6 Power supply, EMC installation and assembly..........................................................................................40
6.1 Notes regarding grounding and shielding...................................................................................40
6.2 Power supply and protection..........................................................................................................42
6.3 Mounting and labeling......................................................................................................................43
7 Summary............................................................................................................................................................45

## Page 4

4CONTROL AND I/O SYSTEM DESIGN TM910

1Introduction

The process of designing and implementing the electronics is an important part of a machine's lifecycle. In this phase,

system requirements must be harmonized with technical feasibility and available components.

Figure 1: Schematic/realistic depiction of a control cabinet

Proper selection of control components and infrastructure is critical. In addition to EMC-compliant installation, it is

also important to consider the topology, system limits, ambient conditions and power management calculation for

the entire control system.

## Page 5

INTRODUCTION 5
1.1 Learning objectives
Using selected examples that represent typical use cases, participants will learn how the system works and what doc-
umentation is available.
Participants will get an overview of the components included in an integrated automation system.
•
Participants will learn the functions available on the B&R website and what information they can find there.
•
Participants will learn what document types are available and how to locate the information they need.
•
Participants will be familiar with typical topologies, required infrastructure components and system limits.
•
Participants will be able to select components based on the required function and know what accessories are re-
•
quired.
Participants will learn about the mechanical and electrical configuration of control and I/O systems.
•
Participants will learn tips and recommendations for EMC-compliant wiring and installation.
•
Participants will learn the requirements for supply voltage, power supply and protection.
•
Participants will be able to calculate power management for implemented I/O modules and infrastructure com-
•
ponents.
1.2 Symbols and safety notices
Unless otherwise specified, refer to the descriptions of symbols and safety notices provided in the respective manuals.

## Page 6

6CONTROL AND I/O SYSTEM DESIGN TM910

2B&R system overview

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

Figure 2: B&R product overview (from left to right: synchronous motor, ACOPOSmulti system, X20 system and integrated safety technology (yellow),

Automation Panel, Power Panel, Automation PC)

Where can I find detailed product information?

For a detailed overview and description of the individual product groups, refer to the B&R website at

www.br-automation.com in section "Products".

The product catalogs for the corresponding product groups can be downloaded in the "Downloads" area

by selecting "Catalogs and Brochures" / "Products" / "Product group".

Further information:

3 "B&R website functions" on page 13

•

Exercise: Create a topology diagram for your next project

Start by thinking about the requirements of your upcoming project. Sketch a preliminary topology and the components

it contains. Also include any general conditions to be considered during implementation. Keep your sketch and refer

back to it at the end of each chapter.

Use a large sheet of paper or a flip chart for your sketch so that you can add details later on.

## Page 7

B&R SYSTEM OVERVIEW7

2.1Terminology

Below is a glossary of the most important terminology used in B&R systems. The definitions

are of particular importance because these terms are used frequently throughout this docu-

mentation as well as in the respective user's manuals.

TermShort description

ACOPOSGeneral name for B&R drives. These power electronics are controlled via a fieldbus connec-

tion and used to control the movement of all types of motors.

Motion controlUmbrella term for anything involving movement. Power electronics controls the movement

of drive systems, such as synchronous, induction and stepper motors.

Automation RuntimeAutomation Runtime is the operating system installed on the controller. Different versions

of Automation Runtime installed on the controller offer different functions and diagnostics

options.

APC / IPCAutomation PC or Industrial PC: PC designed for industrial use.

CFCompactFlash is the external memory of the controller. Almost all B&R control systems use

a CompactFlash card as an application memory. Other systems that do not use Compact-

Flash cards have the application memory integrated in the controller, and it cannot be re-

placed externally. Using CompactFlash cards provided by B&R ensures image compatibility

and suitability for industrial environments.

Remote I/OInstead of being connected directly to the CPU, remote I/O modules can be operated re-

motely via a fieldbus system.

Node numberThe node number is set on the module via rotary switches. This number is required to iden-

tify the module in the network.

PanelHMI devices are often described as operator panels or simply panels.

POWERLINKOpen, Ethernet-based fieldbus for connecting controllers, drives, safety technology and re-

mote I/O modules.

SafetyGeneral term for safety technology in the field of machine manufacturing. The safety com-

ponents process safe input and output data, such as emergency switch-off, enable signals,

safety doors and much more.

System 2000General term referring to B&R's 2003, 2005 and 2010 control systems.

Technology GuardThis is a USB dongle that is inserted in the controller CPU's USB port. The Technology Guard

contains any required software licenses, two operating hours counters and permanent data

storage.

HMITerm referring to the representation of processes and process values. An HMI device (e.g.

with touch screen) can be used to interact with the visualized machine functions.

X2X LinkConnects controller CPUs to X20 or X67 components. Serves as a remote backplane for

transferring I/O data to the bus controller or CPU.

X20Components with IP20 protection. Complete control system with CPUs, remote I/O mod-1

ules and broad fieldbus support.

Table 1: Terminology

1IP20 = Protection against ingress of solid foreign bodies >12.5 mm and no protection against ingress of water. Source: IEC/EN 60529

## Page 8

8CONTROL AND I/O SYSTEM DESIGN TM910

TermShort description

X67Components with IP67 protection. B&R's remote I/O system.2

Table 1: Terminology

2.2Typical topologies

If the problem can be solved by replacing a module, the existing topology must be examined first. The following image

shows a typical control topology as those found on many other machines.

In the center you see the machine controller. It is connected to the other control components via a fieldbus, most

commonly POWERLINK. This is what allows the machine controller to control the remote components.

An X20 CPU, an HMI device or an industrial PC can be used as the machine controller. Above this, HMI devices or other

network components can also be connected via a network connection.

Figure 3: Typical control topology with an X20 CPU

2IP67 = Protection against dust and temporary immersion in water

## Page 9

B&R SYSTEM OVERVIEW9

2.3Serial numbers and model numbers

Difference between serial number and model number

The  printed as a barcode is the unique IDserial number

of a B&R product. The serial number contains the model

number, revision, certifications and the entire history of the

component.

The is the unique identification of modulesmodel number

of the same type and is used, for example, to order mod-

ules. This is why the material number is called model num-

ber.

Figure 4: Elements of the serial number sticker

What information does the serial number provide?

Information about a specific serial number can be found using the search function on the B&R website.

Further information:

3.3 "Search function of the website" on page 16

•

Attaching the serial number

The serial number is found on a free space on each B&R product. On devices with plastic housing, the data is laser-

engraved directly onto the surface. On metal housing it is printed on a white sticker.

X20 systemHMI devicesMotors

Figure 7: Side view of motor

Figure 5: X20 digital module, X20 backplane

module

Figure 6: Rear side of Power Panel

Table 2: Examples of serial number placement

Is the serial number visible in the software?

Machine manufacturers have the option of reading serial numbers in the control program. Alternatively,

the serial numbers of detected hardware can be viewed in System Diagnostics Manager and a list of them

can be downloaded from the controller.

## Page 10

10CONTROL AND I/O SYSTEM DESIGN TM910

2.4Data sheets, user's manuals and information

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

3 "B&R website functions" on page 13

•

A B&R user's manual is available for each product group. All product variants are described in the user's manual. There-

fore, it serves as reference documentation for the respective system. It contains all relevant data for installation, com-

missioning and maintenance.

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

Figure 8: Table of Contents of the X20 system

user's manual

Search within the user's manual

In order to obtain information about a single product variant, it is advisable to search for the model

number in the PDF of the user's manual. This is the quickest way to find the information you need.

2.4.1Control systems

Many control CPUs contain a CompactFlash card. This is where the control application and all configuration files for3

the machine are stored.

3In a few types, the application memory is built directly into the CPU and cannot be replaced externally.

## Page 11

B&R SYSTEM OVERVIEW11

Figure 10:

CompactFlash

Figure 11:

Technology Guard

Figure 9: X20CP1586 - Control CPU

Control systemTitle of user's manual

X20 system"X20 system user's manual"

System 2005"System 2005 user's manual"

System 2003"System 2003 user's manual"

Compact I/O system (EC20/"Compact I/O system user's manual"

EC21)

Maintenance notes for Sys-"B&R System 2000 maintenance"

tem 2003, 2005, 2010

Power Panel"Power Panel ... user's manual"

Industrial PC"Automation PC ... user's manual"

"Provit .... user's manual"

Table 3: Overview of user's manuals from the B&R website

## Page 12

12CONTROL AND I/O SYSTEM DESIGN TM910

2.4.2I/O systems, networks and fieldbus modules

In the control system, I/O modules serve to connect sensors and actuators. Different devices are connected with the

controller via interface and fieldbus modules.

Figure 12: X20AT2222

– Temperature

measurement module

Figure 14: X20IF1063-1 –

PROFIBUS interface module

Figure 13: X20BC0083

– POWERLINK bus

controller

## Page 13

B&R WEBSITE FUNCTIONS13

3B&R website functions

The B&R website provides up-to-date information about all B&R products. The comprehensive search function with

different filters facilitates the targeted search for information.

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

3.1 "Product section on the website" on page 13

•

3.3 "Search function of the website" on page 16

•

3.4 "Download section of the website" on page 17

•

3.1Product section on the website

Information about B&R's range of products can be found in the product section of the B&R website.

www.br-automation.com under "Products" in the main menu→

Figure 15: The different product groups can be opened via the main menu

## Page 14

14CONTROL AND I/O SYSTEM DESIGN TM910

Download X20 system user's manual from the product section

Navigate to "Control systems" via the product section. Then, select "X20 system". Scroll down to category

"Documentation". When this category is open, click on the X20 user's manual "MAX20-GER". A new page

opens. The user's manual can now be downloaded in tab "Downloads".

Figure 16: The X20 system user's manual can be downloaded from the product section.

Exercise: Download and find information in the "X20CP1586" product data

sheet

Download the data sheet for the product "X20CP1586" from the B&R website.

Then take a few minutes to gain an overview of the content of the data sheet

and how it is organized. The following information can be found in the data

sheet for "X20CP1586":

Size of RAM

•

Power consumption and permitted contact load

•

Supply voltage connection and permitted supply voltage range

•

Derating requirements based on ambient temperature

•

Figure 17: X20 CPU - X20CP1586

1)www.br-automation.com

2)Search for the data sheet using the model number search function

3)Download the data sheet

4)Find the required product data

5)Make a note of your results

www.br-automation.com  Search for "X20CP1586"→

## Page 15

B&R WEBSITE FUNCTIONS15

3.2Compare products

The available variants of each B&R product are listed in the product section. The product variants are compared by

selecting the checkbox of the respective variant. Then, click on the "Compare" button to open the comparison.

www.br-automation.com  under "Products" in the main menu - Select category - Compare→

Figure 18: The checkboxes of the X20 product variants "X20CP1301" and "X20CP1381" are marked

After clicking on "Compare", the detailed product comparison opens. Differences and similarities between the two

product variants are shown in a table and highlighted in color.

## Page 16

16CONTROL AND I/O SYSTEM DESIGN TM910

Figure 19: Differences between the selected variants are highlighted in color

Exercise: Product comparison - Digital output modules

Many of the modules in the I/O portfolio have some features in common. The product comparison function on the B&R

website is a good way to identify the differences. Compare the modules "X20DO2321" and "X20DO2322" and note the

differences. Alternatively, you can compare the data sheets for the two products.

1)Navigate to the X20 product section

2)Select category "Digital output modules"

3)Select modules "X20DO2321" and "X20DO2322"

4)Click "Compare products"

5)Make a note of the differences

www.br-automation.com  Products  X20 system  Compare products→→→

3.3Search function of the website

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

## Page 17

B&R WEBSITE FUNCTIONS17

Figure 20: Results for "X20CP1*". Under "Content type", results can be filtered by category

Download the "X20 system user's manual" using the search function

Enter "X20 user's manual" in the search box at the top right and press ENTER (if the desired result is not

included, results can be filtered by content type "Download"). Clicking on the result opens another page

from which the user's manual can be downloaded.

Figure 21: The "X20 system user's manual" can be downloaded via the search function

3.4Download section of the website

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

## Page 18

18CONTROL AND I/O SYSTEM DESIGN TM910

The drop-down menu "Product Groups" is available for searching for specific content. When you select a product group,

additional drop-down menus are loaded to limit the content displayed.

The filters to the left of the results allow even more precise filtering.

The content will be downloaded as soon as you click on the download icon to the right of the entry.

Figure 22: To get access to the download section of product "X20CP1586", product group "Control and I/O Systems" must be selected

Download the "X20 System user's manual" from the download section

Use section "Downloads" to navigate to the general download section. Then select option "Control and

I/O Systems" from drop-down menu "Product Group". Select "X20 system" and "Central units" from the

drop-down menus that follow.

Scroll down to category "Documentation". Clicking on the result opens another page from which the

user's manual can be downloaded.

Figure 23: The X20 system user's manual can be downloaded from section "Downloads"

## Page 19

B&R WEBSITE FUNCTIONS19

Exercise: Download available data for an analog input module

The B&R website offers an extensive range of information in addition to the data sheets. The product and download

area is used to find information. The following information can be found for the module "X20AI4622":

Description in the Products section

•

Technical data in the Products section

•

Certificates (CSA HazLoc, ATEX, UL, etc.)

•

Data sheet (technical data, connection examples, etc.)

•

E-CAD and M-CAD data

•

Figure 24: Search results for "X20AI4622"

Figure 25: Organization of downloads for the module X20AI4622

www.br-automation.com  Search for "X20AI4622"→

## Page 20

20CONTROL AND I/O SYSTEM DESIGN TM910

4Mechanical and electrical configura-

tion

This section mainly deals with the characteristics of the X20 control system. As we design a remote I/O node step

by step, our focus will widen from the technical data for individual I/O modules to a holistic view of the complete

system. We will calculate the power consumption of the entire configuration and apply derating based on ambient

temperature.

4.1Technical data

Technical data for modules is found via the search function on the B&R web-

site. The search results on the website include technical data and available

downloads. Here you can get a good overview of the features a module offers.

Data sheets are the primary source of technical data for modules. The com-

plete data sheet can be downloaded as a PDF file.

In addition to technical data, data sheets include information such as pinouts,

connection examples, output and input circuit diagrams, derating info, power

dissipation, the switching of inductive loads and notes on installation.

Figure 26: Table of contents from the data sheet

for the module X20DO9322

4.1.1Pinouts

The images below show connection examples found in the data sheets of X20 modules.

Figure 27: X20DO9322 digital output module -Figure 29: X20PD0012 potential distributor

Figure 28: X20AO4622 analog output module -

Connection examplemodule - Connection example

Connection example

## Page 21

MECHANICAL AND ELECTRICAL CONFIGURATION21

X20 user's manual:

Chapter 3.14 "Universal 1, 2, or 3-wire connections"

•

4.1.2Module power supply

There are two ways to supply power to modules connected via the remote backplane (X2X Link).

Bus supply

•

The bus supply powers communication between the modules and is necessary depending on the module for pow-

ering module-internal electronics.

I/O power supply

•

The I/O power supply is required for supplying the power electronics. It is necessary depending on the module for

supplying module-internal electronics as well as for the power required by actuators.

Both power supplies are transported depending on the implemented bus module (X20BM11 or X20BM01) and provided

to the I/O module.

Figure 31: X20DO9322 - Connection example The I/O

Figure 30: Power supply via X20 bus module; bus supply and I/O power supplypower supply is required for switching the outputs

Potential groups

In order to implement potential groups, the I/O power supply can be switched separately using power supply modules

(e.g. X20PS3300) together with the X20BM01 bus module.

Figure 32: Simple implementation of various potential groups

## Page 22

22CONTROL AND I/O SYSTEM DESIGN TM910

X20 user's manual:

Chapter 4.8 "Power supply concept"

•

Chapter 4.9.1 "Potential groups"

•

Data for power calculation

The technical data for a module also shows the supply concept of individual modules. The power supply that is required

for a module can be seen in the "power consumption" area.

ModuleExcerpt from data sheet

X20DO9322 digital output mod-

ule, 12 outputs, 24 VDC, 0.5 A,

source, 1-line connections

Figure 33: I/O power supply is required for switching the digital outputs

X20DO6639 digital output mod-

ule, 6 relays, N.O. contacts, 240

VAC / 2 A, 30 VDC / 2 A

Figure 34: I/O supply is not used

Modules with bus supply of 0.01 W

For all modules that only require 0.01 W for the bus supply, the module-internal electronics are powered exclusively via

the I/O power supply. When switching off (potential group) the I/O power supply, these modules are no longer visible

to the controller and no diagnostic data is available.

ModuleExcerpt from data sheet

X20AT4222 temperature input

module, 4 resistance measure-

ment inputs, PT100, PT1000,

0.1°C resolution, 3-line connec-

tions

Figure 35: The bus supply power requirements are 0.01 W - module-internal electronics are powered

exclusively via I/O power supply

X20 user's manual:

Chapter 4.8.10 "Internal I/O power supply failure (ModuleOk)"

•

## Page 23

MECHANICAL AND ELECTRICAL CONFIGURATION23

Modules with direct I/O power supply

For modules with a direct I/O power supply, the supply comes via the terminal block of the module. The power specified

here for powering the module electronics is referred to as "External I/O" in the data sheet.

Figure 36: "External I/O" power consumption data for X20DO8331

Figure 37: X20DO8331: Output supply is fed

directly to the module

Exercise: Connect sensors and actuators

B&R components are particularly flexible. The various temperature sensors, for example, offer 2-wire, 3-wire or 4-wire

connections. Using the data sheet of the module "X20AT4222", check the pinout and the connection examples.

There are different versions of the digital output modules just like there were for the digital input modules. In the data

sheet for the module "X20DO8332", locate the pinout and power supply data.

1)Download data sheet "X20AT4222"

2)Check the pinout for 2- and 3-wire connections

3)Download data sheet "X20DO8332"

4)Check the pinout for the outputs

5)Check the power supply concept

X20 user's manual:

Chapter 3.14 "Universal 1, 2, or 3-wire connections"

•

For digital input and output modules, it is possible to use the X20PDxxxx potential distributor modules.

## Page 24

24CONTROL AND I/O SYSTEM DESIGN TM910

4.2Overall system data

Data for the control system as a whole is documented in

the corresponding user's manual. A complete description

of the system characteristics and functioning of the sys-

tem can be found here. This includes data on dimensions

and minimum spacing as well as information about cal-

culating the power supply and protection requirements.

In addition, the user's manual contains recommendations

for electrical installation and system installation. Numer-

ous topology diagrams and calculation examples provide

help for selecting the correct components.

The specified data applies to the control system as a

whole. Any deviations described in the data sheets of the

individual modules.

Throughout this training module, the relevant sections of

the X20 user's manual are noted in a text box beside each

exercise.

Figure 38: Table of contents for the X20 user's manual

Exercise: Download and explore the X20 user's manual

The X20 user's manual contains useful information about the system characteristics and the mechanical and electrical

configuration of the X20 control system. Open Chapters 2, 3, 6 and 7 in the X20 user's manual to find information about

how to use the system correctly. The user's manuals for other B&R products are structured similarly.

1)Download the X20 user's manual from the Downloads section

2)Find the general information about the X20 system

Find the information about mounting orientation and minimum spacing

°

Find the temperature range specified for the complete system.

°

3)Find the data about the mechanical structure, dimensions and minimum spacing

X20 user's manual:

Chapter 3 "System characteristics"

•

Chapter 4.3 "Installation"

•

Chapter 4.8 "Power supply concept"

•

Chapter 4.9 "Fuse protection of the X20 system"

•

## Page 25

MECHANICAL AND ELECTRICAL CONFIGURATION25

4.3Determining required I/O modules

In many cases, the number of required

analog and digital input and output

channels has been specified. A quick

overview of the available module vari-

ants can be found on the product page

on the B&R website. Clicking on the in-

formation icon displays an overview of

the most important technical data.

Figure 39: X20DO9322 product information on the X20 product page

When selecting a module be sure to include any accessories (bus modules, terminal blocks) that may also be required.

The required accessories can be found in the product description on the website or in the corresponding data sheet.

Figure 40: Required accessories for X20DO9322 on the product page

Figure 41: X20DO9322 data sheet; order data and required accessories

Exercise: Select components for the specified quantity structure

The quantity structure of the I/O channels is already specified for a plant component. The listing can be found in

the table below. Determine the type and quantity of modules from the X20 system that are required. Don't forget to

account for the required number of bus modules and terminal blocks. In order to simplify spare parts inventory for

the end customer, the same module is always used for a given function, even if this results in extra I/O channels being

left over.

Your selection of modules will be used in later exercises to calculating the power requirements.

1)Locate suitable modules in the Products section, use the "Compare products" function

2)Determine the required quantity of each module type

3)Determine the required quantity of bus modules and terminal blocks

The model numbers of the required bus modules and terminal blocks can be found under the "Re-

quired accessories" section of the respective data sheet.

4)Enter your results in the table below

RequirementsX20 moduleQuantity

23 digital inputs, 24 VDC, 1-wire connections; sink

18 digital outputs, relay, 240 VAC / 30 VDC, switching current 2 A

Table 4: Quantity structure required for a piece of equipment

## Page 26

26CONTROL AND I/O SYSTEM DESIGN TM910

RequirementsX20 moduleQuantity

32 digital outputs, 24 VDC, switching current 0.5 A, 1-wire connections, source

10 analog inputs, 4 to 20 mA, 12-bit

13 analog outputs, 10 V, 16-bit

5 temperature inputs, PT1000, 3-wire measurement

Bus modules

Terminal blocks

Table 4: Quantity structure required for a piece of equipment

4.4Calculating power consumption

The power necessary for operation is provided by the power supply modules, the X20 CPU, the bus receivers and the

bus transmitters. These devices contribute +7 W for the bus supply and 240 W for the I/O power supply to the system.

In order to calculate power requirements, we need to add up the power requirements of the modules on the bus supply

and I/O power supply. Then, we need to compare the power requirements of the I/O modules with the power supplied

by the power supply modules.

The required data can be found in the data sheet of the respective module:

1.15 W is required on the I/O power supply for the module X20DO9322.

Figure 42: Power consumption for X20DO9322 electronics

Up to an additional 144 W are required for the power supply of the digital outputs.

Figure 43: Power consumption for X20DO9322 I/O channels [144 W]

The X20PS3300 power supply module provides 7 W on the bus supply and 240 W on the I/O power supply.

Figure 44: Power supplied by X20PS3300 for bus supply (X2X Link) [7 W] and I/O power supply [240 W]

X20 user's manual:

Chapter 4.12 "Power balance"

•

Chapter 4.12.3 "Example: Bus controller and modules"

•

Chapter 4.12.4 "Example: Potential groups "

•

## Page 27

MECHANICAL AND ELECTRICAL CONFIGURATION 27
Exercise: Calculate the power consumption of the I/O modules
First calculate the power for bus and I/O supply for the configuration in the previous exercise.
For this example, we will assume that the digital outputs (24 VDC) each have a maximum load of 200 mA at 100%
simultaneity.
1) Calculate the power consumption for the bus supply per module
2) Calculate the power consumption for the I/O power supply per module
3) Calculate the total power consumption for the bus supply and for the I/O power supply
X20 user's manual:
Chapter 4.12 "Calculating the power requirements"
•
4.5 Additional module data to consider
Derating
In order to calculate the power consumption on the I/O power supply, the maximum channel current is usually as-
sumed. The maximum values can be taken from the data sheet.
Due to the compact construction of the control and I/O system, it is only able to dissipate a certain amount of heat.
The data sheets therefore provide additional information about derating to compensate for this.
Derating based on ambient temperature
•
Derating based on sea level
•
Derating based on mounting orientation
•
The maximum current is reduced on a case-by-case basis for each output channel. Only certain outputs may be
switched on together and neighboring modules must not exceed a certain power dissipation. This can also lead to a
reduction of the maximum permissible ambient temperature.
Exercise: Consider requirements for derating and switching inductive loads
Data for switching inductive loads is provided in the data sheet. The derating requirements with regard to ambient
temperature and operation at maximum continuous current are also provided. Check the data for switching inductive
loads and derating for the modules X20DO9322 and X20DO8331.
1) Download data sheet "X20DO9322"
2) Download data sheet "X20DO8331"
3) Check and compare the values for switching inductive loads
4) Check and compare the values for derating

## Page 28

28CONTROL AND I/O SYSTEM DESIGN TM910

5Selecting and constructing the topolo-

gy

Modern control systems have a modular structure that allows their topology to be adapted to the structure of a given

machine or plant. I/O nodes can be arranged centrally or remotely. In addition, different expansions on the fieldbus

level and redundancy solutions are available. This section uses an example to illustrate the various options and which

components are required to implement them.

5.1POWERLINK topology

As a successor to classic fieldbus technology, POWERLINK offers maximum

performance and uncompromising real-time capabilities based on the inter-

nationally established Ethernet standard.

A transmission speed of 100 Mbit/s and a synchronization accuracy of +/- 100 ns allow even the most demanding tasks

in the areas of control engineering, robotics, CNC and motion control to be combined in a single network.

Number of hub levels, jitter, use of switches, fiber optic converters

are used for cabling an Ethernet network. Integrated 2-port hubs allow devices make it very easy to connectHubs

devices in a line structure. When a data packet passes through multiple hubs on the way to its destination, the delay

times of the individual hubs add up accordingly.

The delay time of a hub is not constant and typically varies by 40 ns. This  adds up with every hub. The jitterjitter

influences the timing precision with which the end device can complete its task. The effects of jitter can be especially

critical in the area of motion control.

, like hubs, are used for cabling Ethernet networks. Switches can also be used when cabling POWERLINK net-Switches

works, but it is important to keep in mind that switches cause larger delays and more jitter than hubs do.

To bridge longer distances,  can be used in POWERLINK networks. It is recommended to ensurefiber optic converters

that the fiber optic converters used are devices that operate as hubs (or repeaters) with a processing time of <1 µs.

Cable latency

The time it takes a signal to pass through a cable (copper or fiber optic) results in a certain delay, or latency. For

example, a 100 meter cable would cause a delay of 0.5 µs. In this respect, a cable of this length has causes a delay

similar to that of a hub. When calculating the minimum POWERLINK cycle time, the lengths of the cables used must

be taken into consideration.

Topology recommendations

To optimize the performance of a POWERLINK network, there are some guidelines that should be followed. Long line

structures have a negative effect on the POWERLINK cycle time.

When using servo drives (ACOPOS), it is recommended to

keep the hub depth below 10. As far as the POWERLINK cy-

cle time is concerned, the tree layout is better than the line

layout. The simplicity of cabling a line structure should be

weighed against the better cycle time of a tree structure.

For applications where timing is critical, a mixed tree/line

structure like the one in the image can be used.

Figure 45: Mixed tree/line structure: From the point of view of the

managing node (MN), the tree structure should come first and then the

line structure.

POWERLINK cables

Cables available from B&R are recommended for wiring. All available variants and lengths are available in the user's

manual or on the B&R website.

## Page 29

SELECTING AND CONSTRUCTING THE TOPOLOGY29

Figure 46: POWERLINK cable X20CA0E61.xxxxx (length specifications: 0.2 m (xxxxx = 00020) to 20 m (xxxxx = 02000)).

The following cabling guidelines must be observed:

Use Cat 5 SF/UTP industrial data cables.

•

Observe the minimum cable bend radius (see data sheet for the cable).

•

Fasten the cable underneath the bus controller. The cable must be fastened vertically under the female RJ45 con-

•

nector on the bus controller.

The customer must implement additional measures in the event of further requirements.

•

Data sheet "POWERLINK cable (X20CA0E61)"

Installation / EMC guide:

Chapter 3.4.1.1 "B&R cable recommendations"

•

POWERLINK - Design and optimization

Early in the process of designing a POWERLINK application, it is important to clearly define and verify the requirements

to be achieved by the network that connects the electronic components to one another. The more complex the network

and the higher the performance requirements of the application, the more essential this preparation becomes.

Information about the design and configuration of POWERLINK networks is provided in the training mod-

ule "TM950 - POWERLINK configuration and diagnostics". Chapter 5 in particular explains the design and

optimization of POWERLINK networks.

5.2Configuring POWERLINK I/O nodes

A variety of bus controllers are available to connect X20 modules to existing control systems via standard fieldbus

technologies like POWERLINK, DeviceNet, PROFIBUS, CANopen, ModbusTCP or EtherNet/IP.

POWERLINK is the preferred fieldbus for use in B&R solutions. It offers exceptional freedom in topology selection as

well as superior system performance and additional redundancy options.

The following image shows a schematic diagram of a system configuration with a fieldbus connection.

Figure 47: Schematic diagram of a system configuration for fieldbus connection

## Page 30

30 CONTROL AND I/O SYSTEM DESIGN TM910
X20 user's manual:
Chapter 3.5 "For all fieldbuses, integration through standardization"
•
Chapter 3.18 "X20 system configuration"
•
Chapter 3.18.1 "Fieldbus connection"
•
Exercise: Select components for use with a POWERLINK bus controller
In the previous exercises, you selected the required I/O components. The next step is to select the corresponding
infrastructure components. The X20 modules are operated behind a POWERLINK bus controller. The bus controller,
including the bus module, now has to be factored into the power calculation. You need to determine whether an addi-
tional power supply module is required and where it will be implemented in the configuration.
1) Select the bus controller, bus module and power supply module for the bus controller
X20 power supply modules for bus controllers are available with and without electrical isolation.
2) Incorporate new components into existing power calculation
3) Check whether results are positive
4) Select required power supply modules
When using a power supply module for bus supply and I/O power supply (e.g. X20PS3300), you should
use bus module X20BM01.
5) Check the power requirements calculation again
X20 user's manual:
Chapter 3.18.1 "Fieldbus connection"
•
Chapter 4.9 "Fuse protection of the X20 system"
•
Chapter 4.9.1 "Potential groups"
•

## Page 31

SELECTING AND CONSTRUCTING THE TOPOLOGY31

After calculating the total power for the bus supply, it appears that the result of the power requirements

calculation is negative and approximately 2 W is missing.

Using an X20PS3300 as the power supply module adds 7 W for the bus supply and 240 W for the I/O

power supply. If all the digital output modules are put behind the X20PS3300, the result of the power

requirements calculation becomes positive. In addition, all the outputs can then be operated as a poten-

tial group.

Figure 48: Schematic diagram of the sample configuration - X20PS3300 highlighted blue

The power consumption calculation can be used to design the cooling solution for the control cabinet.

It should be noted that additional power dissipation can be generated by the actuators as the result of

contact resistance.

If a continuous current of 1.2 A per channel is assumed for a module with 6 channels and the contact

resistance, or R, is 120 m, then the additional power dissipation can be calculated as follows:Ω

RDS(on)

26 * 1.2 A * 120 m = 1.04 WΩ

Data sheet: "Calculation of the additional power dissipation resulting from actuators"

5.3Infrastructure components for redundancy

The B&R system offers a variety of redundancy options in order to maximize the availability of machines. When using

POWERLINK, it is possible to choose between ring redundancy and cable redundancy. In addition, controller redun-

dancy is also available.

Ring redundancy

For ring redundancy, devices are connected in a line, with the last unit con-

nected back to the master. When the connection is interrupted, the ring

redundancy manager reacts by feeding in data from both sides. The mas-

ter recognizes when the ring is closed again and once again supplies data

only from one side into the ring. This guarantees continuous communica-

tion.

Interruption of the ring can be evaluated on the controller using data

points or on the modules using the LEDs. The diagnostic data points are

also shown in System Diagnostics Manager (SDM).

Figure: Example of ring redundancy

## Page 32

32CONTROL AND I/O SYSTEM DESIGN TM910

Cable redundancy

In this form of network redundancy, two networks are available and every de-

vice is connected to both networks. If an error occurs on one network, the link

selector recognizes this and switches to the other. This solution also ensures

maximum availability of machinery and equipment.

Figure: Example of cable redundancy

Controller redundancy

B&R's controller redundancy solution ensures maximum availability for entire systems as well as individual machines.

Controller redundancy allows data to be synchronized within microseconds, with no more than 2 cycles lost when

switching over to the backup controller. This functionality is seamlessly integrated in the real-time operating system

and is easy to use.

A second, identical X20 CPU is added to the existing control topology and configured as redundant in the software.

Figure 49: Schematic representation of controller redundancy with connection to the process bus

FunctionTitle of user's manual

Redundancy"Redundancy for control systems"

Table 5: Overview of user's manuals from the B&R website

Exercise: Select topology and infrastructure components

The selected I/O configuration is connected to an X20 controller. The POWERLINK connection should provide cable

redundancy. You need to select the necessary infrastructure components and cables to cover a transfer distance of

20 m.

Information regarding how to implement various types of redundancy can be found in the corresponding user's man-

ual.

## Page 33

SELECTING AND CONSTRUCTING THE TOPOLOGY33

1)Download manual "Redundancy for control systems"

2)Select the desired redundancy solution

3)Select the required components

Redundancy for control systems:

Chapter 2.3 "POWERLINK cable redundancy system"

•

X20 user's manual:

Chapter 7.11.1 "POWERLINK/Ethernet cable"

•

The simplest form of cable redundancy is set up between an X20 interface module and a bus controller with an inte-

grated link selector. In practice, you will find multiple POWERLINK stations in the same network. As a result, you will

require the modular X20 hub system or expandable bus controllers.

DescriptionOrder numberQuantity

X20 interface moduleX20IF2181-21

Bus controller with integrated link selectorX20BC80841

POWERLINK cable, 20 mX20CA0E61.020002

Table 6: Components required for a direct connection between CPU and I/O nodes

Figure 50: Schematic diagram of an X20 CPU with a cable-redundant POWERLINK connection to an I/O node.

5.4Integration of fieldbus systems

Communication with 3rd-party devices via different fieldbus systems is a basic feature of B&R systems. The fieldbus

that is used to connect a device can be connected to various positions in the system.

## Page 34

34CONTROL AND I/O SYSTEM DESIGN TM910

Fieldbus connection to the control CPU

All B&R control systems offer additional slots for fieldbus

interfaces, which allows them to be expanded easily at

any time without having to add new interfaces. A wide

range of fieldbus interfaces are available for connecting4

different devices.

Figure 51: Connection of X20 I/Os via POWERLINK

in addition to two other fieldbuses (blue and green)

Fieldbus connection to POWERLINK

An expandable bus controller allows B&R to also offer

fieldbuses right at the level of the I/O system. The addi-

tional fieldbus interface is connected right next to a POW-

ERLINK bus controller. This makes it possible to add a

fieldbus interface anywhere on the machine.

Figure 52: Additional fieldbus interface

next to the POWERLINK bus controller

Exercise: Expand by adding a remote fieldbus interface

A PROFIBUS device needs to be installed in the control cabinet. The device is located near the POWERLINK node we have

already configured. To do this, we will need a PROFIBUS master interface. In order to keep the communication paths

short, it's possible to use an expandable bus controller with the required interface. POWERLINK cable redundancy is

moved to the modular X20 hub system.

1)Find section "Expandable bus controllers" in the X20 user's manual

2)Select the components for the expandable bus controller

3)Select the components for the X20 hub system with active hub expansion modules

4)Update the list of materials

X20 user's manual:

Chapter 9.18 "Expandable bus controllers"

•

Redundancy for control systems:

Chapter 2.3.2 "X20HB8884 - Compact link selector"

•

4A few examples of fieldbus systems include: POWERLINK, Profibus, DeviceNet, etc.

## Page 35

SELECTING AND CONSTRUCTING THE TOPOLOGY35

The previous configuration with a cable-redundant bus controller is now replaced with an expandable bus controller.

DescriptionOrder numberQuantity

X20 interface module, for DTM configuration, 1 PROFIBUS DP V0/V1X20IF1061-11

master interface

Cable-redundant bus controller X20BC8084 is replaced by an ex-X20BC10831

pandable bus controller supported by X20 interface modules

Module X20BB80 is replaced by an X20 bus base with an expansionX20BB811

slot for the X20 add-on module.

Power supply X20PS9400 including terminal block X20TB12 is used again.

Table 7: Required components for expandable bus controllers

To achieve cable redundancy in the configuration with the expandable bus controller, an X20 hub system with active

hub expansion modules is used.

DescriptionOrder numberQuantity

X20 bus base, for X20 base module, power supply module and two1

expansion slotsX20BB82

X20 compact link selector1

X20HB8884

X20 hub expansion module, coated, integrated 2-port hub, 2x RJ45X20HB28852

X20 power supply module for standalone hub and compact link se-X20PS80021

lector

X20 terminal blockX20TB121

Table 8: Required components for implementing POWERLINK cable redundancy

Figure 53: Schematic diagram of an X20 CPU with redundancy via X20 hub system and an expandable POWERLINK bus controller with a

PROFIBUS master interface

5.5Remote backplane

The remote backplane allows a rack system to be decentralized. All modules are connected to the uniform backplane

(X2X Link). Directly connected X20, X67 or XV modules can be placed at distances of up to 100 m outside the confines of

the control cabinet. X2X Link guarantees maximum resistance to disturbances with the use of twisted copper cables.

## Page 36

36CONTROL AND I/O SYSTEM DESIGN TM910

A unique feature of the X20 system is the possibility to later integrate machine options on bus modules that are not

yet being used without having to change the software addressing.

Figure 54: X20 and X67 modules can be connected to the CPU via X2X Link or POWERLINK. X20 and X67 modules are linked together using X2X Link.

Allocating the X2X Link address

The remote X2X Link backplane, which connects the individual I/O modules with each other, is set up to be self-ad-

dressing. Because of this, it is not necessary to set the node numbers.

In certain cases, e.g. when configura-

X20 system

tions of modular machines change, it

is necessary to define specific module

groups at a fixed address, regardless of

the preceding modules in the line.

For this purpose, there are modules in

both the X20 system and the X67 sys-

tem with node number switches that

allow you to set the X2X Link address.

All subsequent modules refer to this

Bus module with offset and use it automatically for ad-

node number switches

dressing purposes.

X67 system

#10#11#12#30#31#20#21#22#50#51#52

X2X Link

X20 user's manual:

Chapter 3.3 "Remote backplane"

•

Chapter 3.13 "Configurable X2X Link address"

•

Chapter 3.18.2 "Connection to X2X Link backplane"

•

X2X Link cables

For the X67 system, when using standard cables available from B&R, the cable shield is brought into the X67 module via

the connector (complete 360° shielding). Field-assembled cables are available for the X20 system. When using field-

assembled cables, the shielding at both ends of the cable must be properly grounded.

Data sheet "X2X Link cable (X67CA0Xxx)"

## Page 37

SELECTING AND CONSTRUCTING THE TOPOLOGY37

Installation / EMC guide:

Chapter 4.3.2 "Shielding and grounding"

•

Exercise: Install digital output modules remotely via remote backplane

In the exercises so far, the digital output modules have been powered via the power supply module X20PS3300. In this

exercise, we want to install the digital outputs 10 meters away from the POWERLINK node using the remote backplane

(X2X Link).

1)Select bus transmitter and bus receiver

2)Select bus modules and terminal blocks

3)Select X2X cable

The X20PS3300 power supply module used so far is now replaced by an X20BT9100 bus transmitter. The following

components are required for remote installation of digital outputs:

Figure 55: Schematic diagram of an X20 CPU with redundancy via X20 hub system and an expandable POWERLINK bus controller with a

PROFIBUS master interface and digital outputs that have been installed via the remote backplane (X2X Link)

5.6Secure Remote Maintenance

Our Secure Remote Maintenance solution makes it easy to service plants and machinery anywhere in the world from

your office or on the go. Offer service and maintenance of your machines to customers all over the world, or simply

keep an eye on your own assets. With B&R's solution, service technicians and engineers can access machines from

anywhere in the world to retrieve logbook entries, application data and much more.

Components of the Secure Remote Maintenance solution

Secure Remote Maintenance consists of at least the following components.

LinkManagerGateManagerSiteManager

Access software used to establishIntermediate serverConnecting device

the connection

Configuration managementConnection of the controller to

••

User managementthe GateManagerAccess from a PC

••

Different variants support 4G,Access from mobile devices

••

WiFi, etc.

Table 9: Components of the Secure Remote Maintenance solution

## Page 38

38CONTROL AND I/O SYSTEM DESIGN TM910

Figure 56: System design for the Secure Remote Maintenance solution

Figure 57: The schematic diagram of the topology from the previous chapters has now been extended to include the Secure Remote

Maintenance solution. This enables remote access to the controller. Use cases are remote maintenance, programming or recording of

production data. Additional use cases are documented in the Secure Remote Maintenance solution user's manual in chapter 6.

## Page 39

SELECTING AND CONSTRUCTING THE TOPOLOGY 39
Secure Remote Maintenance user's manual:
Chapter 3.1 "Overview"
•
Chapter 3.2 "GateManager"
•
Chapter 3.3 "SiteManager"
•
Chapter 3.4 "LinkManager"
•
Chapter 6 "Solution models and end customer scenarios"
•

## Page 40

40CONTROL AND I/O SYSTEM DESIGN TM910

6Power supply, EMC installation and as-

sembly

B&R control systems offer all the components needed to implement complex automation solutions. In addition to

selecting the components necessary to implement the selected topology and the modules needed for the I/O system,

it is important to consider the requirements for power supply, protection, EMC-compliant installation and control

cabinet cooling. When designing a solution, it is also important to account for system limitations such as the maximum

permissible ambient temperature and network distances.

Module and terminal locking clips, cables shields and a labeling system are offered as accessories for B&R solutions.

6.1Notes regarding grounding and shielding

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

Figure 58: X20DC1176 connection schematic -

Encoder cable connection with shielding

The following general guidelines apply for shielding:

The top-hat rail must always be mounted to a conductive backplane.

•

The control cabinet back wall must be connected with GND.

•

Shielded cables must be grounded on both sides.

•

## Page 41

POWER SUPPLY, EMC INSTALLATION AND ASSEMBLY41

Figure 60: Shielding via top-hat rail or busbar

Figure 59: Shielding via X20 cable shield clamp

Figure 61: Wiring diagram for X20 modules

with an Ethernet cable

X20 system user's manual:

Chapter 4 "Mechanical and electrical configuration"

•

Chapter 4.6 "Shielding"

•

Chapter 4.7 "Cabling guidelines for X20 modules with Ethernet cables"

•

Chapter 7.11.2 "X2X Link cable"

•

Chapter 8.2.2 "Immunity requirements"

•

Chapter 8.2.3 "Interference emission requirements"

•

Installation / EMC guide:

Chapter 3.3.3 "Installation with increased vibration requirements (4 g)"

•

Chapter 3.4.1.2 "Shield connection"

•

Chapter 7.3 "Buffer module"

•

Exercise: Check the installation with regard to electromagnetic compatibility (EMC)

Downtime caused by EMC problems can be prevented by installing appropriate components. Corresponding recom-

mendations are provided in the data sheets and user's manuals. B&R components are designed for complete shielding

and grounding. This includes shield component sets available as accessories for control and I/O systems, for example.

B&R recommends connecting controllers and remote I/O systems to the cables offered in the B&R product catalog. A

detailed cable specification for use in industrial environments is provided in the respective data sheet.

1)Look up the shielding and grounding concepts for X20 modules in the X20 system user's manual

2)Cable shield for X20 modules - Shield component set

3)Look up the specifications for X2X and POWERLINK cables

## Page 42

42CONTROL AND I/O SYSTEM DESIGN TM910

User's manual "X20 System user's manual":

Chapter 4.6 "Shielding"

•

Chapter 4.7 "Cabling guidelines for X20 modules with Ethernet cables"

•

Chapter 7.4 "Cable shield plates"

•

User's manual ""Installation / EMC guide

Data sheet "", search within data sheet for "X20CA0E61.00020"POWERLINK cable X20CA0E61.xxxx

Data sheet "", search within data sheet for "X67CA0X99.1000"Data sheet X2X Link Cable (X67CA0Xxx)

6.2Power supply and protection

The X20 system is protected according to the power supply concept. Using the X20BM01 bus module and organizing

the power supply bus modules accordingly allows various potential groups to be implemented (e.g. for input groups

or various power circuits for the outputs).1)

Figure 62: Protecting various potential groups

1)A 1 A slow-blow fuse is recommended for the bus supply.

## Page 43

POWER SUPPLY, EMC INSTALLATION AND ASSEMBLY43

It is possible to set up potential groups through the use of different supplies for the power supply modules.

Figure 63: Example of extended X2X Link power supply

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

6.3Mounting and labeling

The X20 system and B&R systems in general are documented extensively.

The recommendations provided in the user's manual help with integrating the

components into the control cabinet.

In the manual, B&R also provides notes regarding proper installation and lists

additional accessories.

Figure 64: Installing an X20 node on the

mounting rail

## Page 44

44 CONTROL AND I/O SYSTEM DESIGN TM910
X20 user's manual:
Chapter 4.1 "Dimensions"
•
Chapter 4.2 "Design support"
•
Chapter 4.3 "Installation"
•
Chapter 4.5 "Stress relief using cable ties"
•
Chapter 5.3 "Assembling an X20 system"
•
Chapter 5.4 "Installing the X20 system on the top-hat rail"
•
Chapter 5.5 "Uninstalling the X20 system from the top-hat rail"
•
Chapter 5.6 "Expanding an X20 system"
•
Chapter 5.7 "Installing accessories"
•
Chapter 5.8 "Label tags"
•
Chapter 7.8 "Terminal labeling"
•
Chapter 7.9 "Labeling tool"
•

## Page 45

SUMMARY45

7Summary

The process of designing and implementing the electronics is an important part of a machine's lifecycle. In this phase,

system requirements must be harmonized with technical feasibility and available components.

Figure 65: Schematic/realistic depiction of a control cabinet

Proper selection of control components and infrastructure is critical. In addition to EMC-compliant installation, it is

also important to consider the topology, system limits, ambient conditions and power management calculation for

the entire control system.

## Page 46

46CONTROL AND I/O SYSTEM DESIGN TM910

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

## Page 47

AUTOMATION ACADEMY 47

## Page 48

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