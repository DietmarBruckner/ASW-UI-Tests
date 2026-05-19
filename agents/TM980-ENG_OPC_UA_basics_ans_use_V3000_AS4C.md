## Page 1

TM980

OPC UA basics and use

## Page 2

2 OPC UA BASICS AND USE TM980
Requirements
Basic knowledge Basic technical understanding
Training module TM210 - Automation Studio basics
UaExpert 1.6.3
Software Automation Studio 4.12
OPC UA FX package 1.2.0
X20CP1586 (ETAL210 - ETA light system control technology)
Hardware
X20BC008U (ETAL980 - ETA Light OPC UA)

## Page 3

TABLE OF CONTENTS 3
Table of contents
1 Introduction.........................................................................................................................................................4
1.1 Learning objectives..............................................................................................................................4
2 OPC UA basics....................................................................................................................................................5
2.1 OPC Unified Architecture....................................................................................................................5
2.2 Use cases...............................................................................................................................................6
2.3 Information model...............................................................................................................................9
3 OPC UA bus controller integration...............................................................................................................12
3.1 Preparation and configuration of the bus controller..................................................................12
3.2 Establishing a connection with Unified Automation UaExpert client.....................................14
3.3 Reading data.......................................................................................................................................16
4 OPC UA devices in Automation Studio.......................................................................................................19
5 Automation Runtime OPC UA server...........................................................................................................23
5.1 Configuration of the default view..................................................................................................25
5.2 Applying units, users, limits and historizing................................................................................27
6 Certificates and security................................................................................................................................33
6.1 Trusted list...........................................................................................................................................37
7 Publisher/Subscriber configuration.............................................................................................................41
8 PLCopen function blocks...............................................................................................................................44
9 Methods.............................................................................................................................................................45
10 Appendix..........................................................................................................................................................49
10.1 OPC UA FX package.........................................................................................................................49
10.2 Firmware update X20BC008U.......................................................................................................50
10.3 Optional exercises............................................................................................................................51
11 Summary...........................................................................................................................................................53

## Page 4

4OPC UA BASICS AND USE TM980

1Introduction

OPC Unified Architecture (OPC UA) is a manufacturer-independent communication protocol for automation applica-

tions in industry. It enables end-to-end communication from individual sensors and actuators to the ERP system or

the cloud.

B&R has been relying on OPC UA for years and is an active member of various OPC Foundation working groups. Find

out more about the solutions and products B&R offers with OPC UA.

Configuring OPC UA devices and assigning access rights in B&R's Automation Studio engineering environment is com-

pleted with just a few clicks. PLCopen function blocks are used to implement OPC UA functions in an automation

project.

Figure 1: OPC UA logo

1.1Learning objectives

With selected application examples and exercises, participants will learn the basics of OPC UA communication tech-

nology.

Participants will learn the basics of OPC UA technology.

•

Participants will become familiar with the possibilities, advantages and typical use cases of OPC UA.

•

Participants will be able to connect to an OPC UA server via an OPC UA client.

•

Participants will be able to configure an OPC UA client in AS.

•

Participants will be able to commission the Automation Runtime OPC UA server in Automation Studio.

•

Participants will be able to configure the user role system in AS.

•

Participants are familiar with the advantages of publish/subscribe architecture and can configure it in Automa-

•

tion Studio.

Participants will learn how to use the OPC UA library in Automation Studio.

•

Participants will be able to implement and call OPC UA methods.

•

Participants will learn how to handle certificates.

•

## Page 5

OPC UA BASICS5

2OPC UA basics

2.1OPC Unified Architecture

pen latform ommunications nified rchitectureOPCUA

(OPC UA) and is an international standard for secure, re-

liable, manufacturer- and platform-independent informa-

tion exchange in industrial communication.

OPC UA enables integrated communication ranging from

ERP systems to individual sensors and actuators and

makes semantic interoperability available in automation

industry. OPC UA is flexible and fully independent and

contributes substantially to the success of the 4th indus-

trial (r)evolution.

Figure 2: OPC UA pyramid

Advantages

International standard (IEC 62541)

•

Safe data exchange

•

Reliable

•

Vendor-independent

•

Platform-independent

•

Vertical and horizontal communication

•

2.1.1International standard (IEC 62541)

StandardOPC UA specifications

OPC UA has had specifications defined by the OPC Foun-

dation and has been standardized in IEC 62541.IEC/TR 62541-1Part 1: Overview and concepts

The OPC UA specification is composed of 14 parts and is

IEC/TR 62541-2Part 2: Security model

continuously supplemented by specifications from other

organizations such as PLCopen and other consortia.

IEC 62541-3Part 3: Address space model

IEC 62541-4Part 4: Services

IEC 62541-5Part 5: Information model

IEC 62541-6Part 6: Mapping

IEC 62541-7Part 7: Profiles

IEC 62541-8Part 8: Data access

IEC 62541-9Part 9: Alarms and conditions

IEC 62541-10Part 10: Programs

IEC 62541-11Part 11: Historical access

Part 12: Discovery

IEC 62541-13Part 13: Aggregates

Part 14: PubSub

IEC 62541-100PLCopen IEC 61131-3

Table 1: OPC UA specifications

## Page 6

6OPC UA BASICS AND USE TM980

OPC Foundation

The OPC Foundation is an independent committee that specifies and further

develops the OPC UA standard.

B&R has been relying on the OPC UA standard for years and is an active mem-

ber of various OPC Foundation working groups.

See https://opcfoundation.org/

Figure 3: OPC Foundation

2.2Use cases

OPC UA is used in various constellations on B&R systems:

For connecting HMI applications with controllers, e.g. mapp View

•

For data exchange between B&R controllers

•

For data exchange with other controllers

•

For connecting to fieldbus devices

•

(B&R bus controller, OPC UA capable sensors or actuators)

Figure 4: OPC UA - Industrial communication

Communication \ OPC UA \ Use cases

## Page 7

OPC UA BASICS7

2.2.1HMI applications

A more common and typical application of OPC UA is

exchanging process variables between HMI applications

and PLC systems.

Explained in simple terms, HMI applications as OPC UA

clients connect with PLC systems as OPC UA servers and

read or write OPC UA node attributes in the OPC UA ad-

dress space. OPC UA nodes receive process variable data

to/from the PLC via the OPC UA server.

Figure 5: Data exchange between HMI application and control system

Monitored items vs. continuous reading

In this case, HMI applications should prevent unnecessary network traffic caused by continuous reading of the node.

Any nodes needed by the HMI application are subscribed to the server and are tasked with monitoring for value mod-

ifications (MonitoredItems). If node values change, the clients are informed (Publish) and only the changed values are

transfered.

mapp View

The mapp View HMI system has been developed by B&R and uses OPC UA for communication to PLC systems from

B&R or other manufacturers.

Figure 6: How mapp View works

2.2.2OPC UA bus controller

This bus controller provides OPC UA server functions. This allows any OPC UA clients access to read or write data from

I/O modules connected to the bus controller.

For additional information about using a bus controller, see chapter 3 "OPC UA bus controller integration" on page

12.

## Page 8

8OPC UA BASICS AND USE TM980

2.2.3Controller to controller

With OPC UA, communication between PLC systems can

also be facilitated and harmonized. For this, the function

blocks specified by PLCopen are used.

Figure 7: Connection between OPC UA client and OPC UA server

There are more than 20 function blocks available in Automation Studio library

AsOpcUac that can be used to handle establishing connections, transferring

data or calling methods.

Figure 8: Function block "UA_Read"

2.2.4Audit clients

Audit mechanisms are included on the B&R OPC UA server. These inform an audit client which user changed which OPC

UA node as well as what they used to make the change and when it occurred.

Figure 9: Audit clients operating principle

For example, the OPC UA audit client can be informed by the OPC UA server via audit event that OPC UA client 1 changed

variable VarX from 65 to 70 degrees at 1:55 PM. The nodes that should trigger audit events are set in the Automation

Studio configuration.

## Page 9

OPC UA BASICS9

2.2.5Third-party systems

OPC UA third-party systems can also be connected to B&R systems without any programming effort. Just like for the

OPC UA bus controller, the third-party device is configured in Automation Studio. The required nodes can be declared

as channels.

With this configuration, the OPC UA can set up drivers in Automation Runtime on the third-party device as OPC UA

client appropriate monitored items and transfer them as inputs to the PLC process variables or even write the process

variables of the PLC as outputs onto the third-party device using the OPC UA Write service.

Figure 10: Integration of 3rd-party systems

2.3Information model

On the B&R OPC UA server, the PLC system is mapped in

the OPC UA address space based on the OPC UA specifi-

cations.

Part 3 (Address Space Model)

•

Part 5 (Information model)

•

Companion Specification PLCopen OPC UA Informa-

•

tion Model 1.00 Specification

Figure 11: Information model

The address space is defined using nodes and their attributes, which are connected using references.

## Page 10

10 OPC UA BASICS AND USE TM980
2.3.1 OPC UA nodes
The B&R OPC UA server contains OPC UA nodes that are required according to the specifications (e.g. the server object).
In addition, it contains all nodes that are specifically created on the B&R system during startup or during operation
of the B&R OPC UA server (e.g. tasks and variables of the tasks), including their type descriptions, which are mapped
as nodes.
NodeID
A node is uniquely identified in the address space via its NodeID. The NodeID consists of the NamespaceIndex, Identi-
fier and IdentifierType. There is also the IdentifierType GUID, which is not used by B&R.
NamespaceIndex
•
This regulates the responsibility for identifying a node, see "Namespaces" on page 10.
Identifier
•
This consists either of a numeric value (resource-saving in small systems) [e.g. 2253 (server object)] or a string [e.g.
"Submarine::ballast"].
IdentifierType (numeric or string)
•
Specifies whether the identifier is defined by a numerical value or by a character string.
Browse name
If it is not known which nodes a server contains, then the address space can be searched with browser ser-
•
vices (browse) starting from the root node or other known nodes. When browsing, a node is identified using the
BrowseName.
2.3.2 Namespaces
Since the nodes are defined by the OPC Foundation, the PLCopen organization or B&R itself, two identical identifiers
could be assigned at random, for example.
In order to prevent this, the responsibility of the nodes in the OPC UA address space is regulated using the namespace.
The namespace therefore specifies which institution has defined the node (naming authority) and is listed in the form
of a namespace URI.
Automation Runtime:
Namespace URI Description
http://opcfoundation.org/UA/ Types and objects specified by the OPC Foundation
urn:<hostname>/BR/UA/EmbeddedServer Types and objects for server provider
http://opcfoundation.org/UA/DI/ Types and objects for device integration
http://PLCopen.org/OpcUa/IEC61131-3/ Types and objects for PLCopen
urn:B&R/plc/ Static B&R types and objects
urn:B&R/pv/ B&R information model for PLC process variables
urn:PLCopen/pv/ PLCopen information model for PLC process variables
http://br-automation.com/Diagnostics Types and objects for Commissioning Cockpit diagnostics
Table 2: Automation Runtime namespaces
Communication \ OPC UA \ Information model \ OPC UA nodes

## Page 11

OPC UA BASICS11

OPC UA bus controller

Namespace URIDescription

http://opcfoundation.org/UA/Types and objects specified by the OPC Foundation

http://www.br-automation.com/io-system/B&R Information model for configuration and process variables

Table 3: OPC UA bus controller namespaces

Hardware \ X20 System \ X20 module \ Bus controller \ X20BC008U \ Bus controller information model

PowerPanel T-Series

Namespace URIDescription

http://opcfoundation.org/UA/Types and objects specified by the OPC Foundation

urn:<hostname>/BR/UA/EmbeddedServerTypes and objects for server provider

http://opcfoundation.org/UA/DI/Types and objects for device integration

http://br-automation.com/OpcUa/BrTypes/General types and objects defined by B&R

http://br-automation.com/OpcUa/HMI/Termi-Device types and objects defined by B&R

nal/

Table 4: PowerPanel T-Series namespaces

Hardware \ Power Panel \ Power Panel T50 \ Software \ OPC UA server \ Information model

Since the URI tends to be quite long and specifying it in the NodeID is not

desired, these URIs are managed in a "NamespaceArray" in the server object.

Only the index in this table is used in the NodeID.

NamespaceIndex 0: http://opcfoundation.org/UA/

•

NamespaceIndex 3: http://opcfoundation.org/UA/DI/

•

NamespaceIndex 6: urn:B&R/pv/

•

Figure 12: Address space - NamespaceArray

## Page 12

12OPC UA BASICS AND USE TM980

3OPC UA bus controller integration

The bus controller X20BC008U connects OPC UA systems

with the X20 I/O system. In a corresponding information

model, which is derived from the OPC UA DI model, data

from both the bus controller and the X20 I/O modules can

be requested or transmitted using OPC UA services.

The OPC UA standard means that the bus controller can

be used not only by B&R, but also by any OPC UA client.

Figure 13: Bus controller communication

Once the hardware configuration has been transferred to the B&R controller, the corresponding OPC UA driver can

communicate with the X20BC008U bus controller in Automation Runtime via the standard Ethernet interface using

OPC UA.

3.1Preparation and configuration of the bus controller

In order to be able to use the bus controller, the first step is to prepare the network in which it is to be used.

It is possible to either use a local network with fixed PC and bus controller IP addresses or assign the IP addresses

within a DHCP network and use the hostnames.

If the topology and network have been set up, the bus controller can now be configured. Network settings can be made

using the address switches on the front of the bus controller. It is possible to set a fixed IP address or to get the IP

address from a DHCP server.

Hardware \ X20 system \ X20 modules \ Bus controller \ X20BC008U \ Address switch

If the bus controller is addressable in the network, it is possible to update the firmware. The latest firmware is available

for download on the B&R website. This can then be loaded onto the bus controller by accessing the IP address via a web

browser. For more information, see see "Optional exercise: Perform a firmware update for X20BC008U" on page 50.

Exercise: Build the topology and adjust the network settings

In this exercise, the network topology is built and the PC's network settings are adjusted. It is possible to set up a local

network either using the integrated hub of the bus controller or to set up a network via a DHCP server.

Local network:

1)Build the network topology according to the sketch.

2)PC network settings:

IP address: 192.168.1.128

Subnet mask: 255.255.255.0

Figure 14: Structure of local network

## Page 13

OPC UA BUS CONTROLLER INTEGRATION13

Network via DHCP server:

1)Build the network topology according to the sketch.

2)Make sure that the IP address of the PC is assigned automatically.

Figure 15: Network setup via DHCP server

Exercise: Configure the OPC UA bus controller

The objective of this exercise is to configure the OPC UA bus controller X20BC008U according to the network topology.

To do this, turn the address switches to a defined position. The positions for the different modes are described in

Automation Help.

1)Static IP address for the bus controller:

Set the address switch to a value between "0x01 - 0x7F".

2)DHCP mode for the bus controller:

Set the address switch to a value between "0x80 - 0xEF".

## Page 14

14OPC UA BASICS AND USE TM980

3.2Establishing a connection with Unified Automation UaExpert client

The free program "UaExpert", which was developed by Unified Automation, is used to establish the connection to the

bus controller. It is a simple and versatile OPC UA client.

In order to use this program, you must register on the website.

https://www.unified-automation.com/downloads/opc-ua-clients.html

For detailed instructions about how to use the UaExpert client, see the UaExpert help documentation in chapter "First

Steps".

1) Add a server.3) Select signature and encryption.

Figure 16: UaExpert: Add a new server via the toolbar

2) Enter the server's URL.

Figure 18: UaExpert: Select signature and encryption

4) Connect to the server

Figure 17: UaExpert: Enter server URL

Figure 19: UaExpert: Connect the server

## Page 15

OPC UA BUS CONTROLLER INTEGRATION15

If the UaExpert certificate is not in the server's trusted list (6.1 "Trusted list" on page 37), a connection

cannot be established between the UaExpert client and the OPC UA server.

Figure 20: Result: Trusted list

Hardware \ X20 system \ X20 modules \ Bus controller \ X20BC008U \ Connect to UaExpert

Exercise: Establish a connection with the UaExpert bus controller

Program "UaExpert" is used to establish a connection to bus controller X20BC008U. Automation Help provides infor-

mation about how to establish a connection.

1)Install UaExpert.

2)Open UaExpert.

3)Add a new server.

4)Connect to the server.

As soon as the connection has been established, the structure of

the address space provided by the bus controller will be displayed

in its own window.

The bus controller is shown in subsection "DeviceSet" and con-

tains all objects provided by the bus controller.

## Page 16

16OPC UA BASICS AND USE TM980

3.3Reading data

When the connection is established, it is possible to access the desired data

in the "Address space" window. The following path can be used, for example,

to navigate to the inputs of the DI module:

Root / Objects / DeviceSet / X20BC008U / X2X / SubDevices / ST002|

X20DI6371 / ProcessData

Figure 21: UaExpert: Address space

The inputs or nodes can then be dragged and dropped into the "Data access view".

Figure 22: UaExpert: Adding nodes via drag-and-drop

The inputs and outputs or nodes can now be read out or set. For the latter, double-click on the corresponding output

and select the check box.

## Page 17

OPC UA BUS CONTROLLER INTEGRATION17

Figure 23: UaExpert: Reading and setting nodes

Exercise: Read inputs / Set outputs

The objective of this exercise is to read and set variables from the bus controller using the UaExpert program.

1)Navigate to the following path in window "Address space":

Root / Objects / DeviceSet / X20BC008U / X2X / SubDevices / ST002|X20DI6371 / ProcessData

2)Drag nodes "DigitalInput1" and "DigitalInput2" into "Data access view".

3)Navigate to the following path in window "Address space":

Root / Objects / DeviceSet / X20BC008U / X2X / SubDevices / ST003|X20DO6322 / ProcessData

4)Drag nodes "DigitalOutput1" and "DigitalOutput2" into "Data access view".

5)Navigate to the following path in window "Address space":

Root / Objects / DeviceSet / X20BC008U / X2X / SubDevices / ST004|X20AI2222 / ProcessData

6)Drag node "AnalogInput1" into the "Data Access View".

7)Observe the value change in "Data Access View" if the switches or buttons of the  OPC UA ETA light are enabled

or the potentiometer is changed.

8)Observe the status change of the green LED on the  OPC UA ETA light if the digital outputs are set via "Data Ac-

cess View".

Value changes can be performed and observed in "Data Access View".

Figure 24: UaExpert: Result: Reading inputs and outputs

## Page 18

18OPC UA BASICS AND USE TM980

Optional exercise: Change the "packed" format using a method call

The objective of this exercise is to configure the "packed" format on the I/O module (X20DI6371) via the "Packed inputs"

node. The default setting for the format of the digital I/Os is "unpacked" (0 for off).

1)In the "Address space" window, navigate to the following path and check whether DigitalInput01 to DigitalIn-

put06 are listed:

Root / Objects / DeviceSet / X20BC008U / X2X / SubDevices / ST002 |X20DI6371 / ProcessData

2)Navigate to the following path in window "Address space":

Root / Objects / DeviceSet / X20BC008U / X2X / SubDevices / ST002 | X20DI6371 / Configuration / General /

Packed inputs

3)Use drag & drop to place the "Packed inputs" note in the "Data Access View".

4)Set the value of "Packed inputs" to 1 to allow the configuration.

5)Navigate to the following path in window "Address space":

Root / Objects / DeviceSet / X20BC008U / X2X / SubDevices / ST002 | X20DI6371 / Configuration / Control / Apply

changes

6)Right-click on the "ApplyChanges" method and click "Call" to apply the settings.

7)Click on "Rebrowse" in the "Address space" window to update the address space.

In the "Data Access View", all 6 digital inputs are displayed bit-coded under the DigitalInput (byte) node.

Figure 25: Before: DigitalInputsFigure 26: After: DigitalInputs "packed"

"unpacked"

## Page 19

OPC UA DEVICES IN AUTOMATION STUDIO19

4OPC UA devices in Automation Studio

It is possible to configure OPC UA devices just like POWERLINK or fieldbus devices in Automation Studio. The advantage

of using OPC UA devices is that you don't need device description files for every single device.

With the so-called OPC UA Any Device, it is possible to connect third-party systems to the B&R system. The OPC UA Any

Device represents a simple access that can be configured in the Physical View and directly linked to an ETH interface.

This device acts as a client and can connect to any server. This means that any OPC UA server – whether it be a controller,

a bus controller or an OPC UA-enabled third-party device – can be configured in Automation Studio.

The server information and the variables to be written or read can now be defined directly on this device. These data

points are then available via I/O mapping and can be linked to any process variable.

Figure 27: Adding OpcUa_any device from the Toolbox

The OpcUa_any Device, also called generic OPC UA station, is assigned to the Ethernet interface of the CPU in Automa-

tion Studio from the Toolbox.

Communication \ OPC UA \ Configuration in AS \ OPC UA Any Device

All necessary settings are then made in the configuration.

The most important connection parameter is ServerEndPointUrl, which is specified with the following syntax:

opc.tcp://ip-address:port or opc.tcp://hostname:port

Ultimately, this is the same as establishing a connection with UaExpert.

Now it is possible to create channels that are linked to nodes of the OPC UA server. Create a channel by assigning

it a name. Then the browse path of the node must be specified with the following syntax: /namespaceindex:browse-

name/namespaceindex:browsename/ ...

The direction (input or output) and the data type must be adapted according to the node that will be used so that the

data can be read in or written out correctly.

## Page 20

20OPC UA BASICS AND USE TM980

Figure 28: OPC UA station channel settings

After saving the configuration, the previously configured channels appear in the I/O mapping of device OpcUa_any

device. Here it is possible to assign process variables to the channels.

Figure 29: I/O mapping for OPC UA station

Before the project is transferred, it is important to enable the OPC UA system of the CPU. Otherwise,

using the generic OPC UA station (OpcUa_any device) will not work!

OPC UA server diagnostics

All events that occur on the OPC UA server are logged in the Logger file "Connectivity". If, for example, the browse path

of the channels is entered incorrectly, this will be displayed as a warning in the Logger.

## Page 21

OPC UA DEVICES IN AUTOMATION STUDIO21

Figure 30: OPC UA server diagnostics: Specified node does not exist

The Logger also logs if the connection to the server was successful.

Figure 31: OPC UA server diagnostics: Connection OK

Diagnostics and service \ Diagnostics tool \ Logger

Exercise: Configure and use OpcUa_any device in a project

This exercise shows how to read variables and values of an OPC UA-capable device at runtime.

The same network topology is still used for this exercise.

It is not necessary to configure the bus controller because of the OpcUa_any device.

1)Create a new Automation Studio project with an X20CP1586 controller.

2)Configure the Ethernet interface; either static IP address or DHCP.

3)Enable the OPC UA system in the CPU configuration, see "Enabling the OPC UA system" on page 23.

4)Assign the OpcUa_any device from the Toolbox to the CPU Ethernet interface.

5)Enter the ServerEndpointUrl in the OpcUa_any device configuration:

Syntax opc.tcp://ip-address:port e.g. opc.tcp://192.168.1.2:4840

## Page 22

22OPC UA BASICS AND USE TM980

The default port is 4840. However, this can be changed or read in the CPU configuration (under OPC-

UA System \ Activate OPC-UA System \ Network settings).

6)Create three channels in the OpcUa_any device configuration, see screenshot above.

/0:Root/0:Objects/2:DeviceSet/2:X20BC008U/2:X2X/2:SubDevices/2:Channel 1:ST2

/2:ProcessData/2:DigitalInput01

/0:Root/0:Objects/2:DeviceSet/2:X20BC008U/2:X2X/2:SubDevices/2:Channel 2:ST3

/2:ProcessData/2:DigitalOutput01

/0:Root/0:Objects/2:DeviceSet/2:X20BC008U/2:X2X/2:SubDevices/2:Channel 3:ST4

/2:ProcessData/2:AnalogInput01

A break in the path is not possible.

7)Save OpcUa_any device configuration and check in its I/O mapping whether the channels have been created.

8)Transfer the project to the controller.

9)Check Logger, see "OPC UA devices in Automation Studio" on page 19.

10)Activate monitor mode in the I/O mapping and then observe the value changes of the channels after pressing

the switches or turning the potentiometer on the  OPC UA ETA Light.

11)Force the digital output in the I/O mapping and observe the status change of the LED on the  OPC UA ETA Light.

Value changes can be performed and observed in the I/O mapping.

Figure 32: Result: I/O mapping

## Page 23

AUTOMATION RUNTIME OPC UA SERVER23

5Automation Runtime OPC UA server

Enabling the OPC UA system

Automation Runtime offers a completely integrated and easily configurable OPC UA server. This can be activated via

an Automation Runtime configuration and the variables can then be released via a separate configuration view.

For basic configuration, it's sufficient to enable the OPC UA system. The remaining parameters stay unchanged.

The server configuration also offers the option of setting server properties such as the port, encrypted access, trans-

mission cycle times or the number of variables that can be linked.

The OPC UA system must be activated for operation as a server as well as a client.

Figure 33: Configuration of the OPC UA system

Communication \ OPC UA \ Configuration in AS \ Enable OPC UA system

Access & Security - User management system

The Configuration View contains folder "Access & Security/UserRoleSystem" in which two configuration elements are

stored.

Basic roles for the system can be created in the "Role" file. These can be an operator, a service technician or an admin-

istrator, for example.

No new roles can be created at runtime.

In the "User" file, individual users can then be created to which one or more roles can be assigned.

Users as well as the associated roles can be adapted at runtime.

Do not delete or rename existing roles "Administrators" and "Everyone".

Also do not delete or rename existing users "Admin" and "Anonymous".

Programming \ Access & Security \ User role system \ Configuration \ Automation Studio configuration

## Page 24

24OPC UA BASICS AND USE TM980

Exercise: Configure the Automation Runtime OPC UA server and create users.

The objective of this exercise is to configure a user role system for the OPC UA server. The roles "ServiceEngineer" and

"Admin" and a user "Mike" are created.

1)Enable the OPC UA system in the CPU configuration.

2)Create role "Service engineer" in the Configuration View under AccessAndSecurity\UserRoleSystem.

3)Create user "Admin" with role "Admin" and password "Admin".

4)Create user "Mike" with role "ServiceEngineer" and password "Mike".

The configuration of the user role system should correspond to the following figure:

Figure 34: Result: User role system

## Page 25

AUTOMATION RUNTIME OPC UA SERVER25

5.1Configuration of the default view

Adding OPC UA mapping

The OPC UA default view will be inserted from the Toolbox into the Connectivity folder of the Configuration View. In

this view, all variables of the corresponding configuration are displayed for approval. This means that the Automation

Runtime OPC UA server can then make these available.

In the OPC UA default view, many settings can also be made for ranges of values, write and read permissions, units

and much more.

The OPC UA default view is added in the Configuration View under Connectivity/OpcUa from the Toolbox.

Figure 35: Adding the OPC UA default view from the Toolbox

Releasing variables

After opening the OPC UA default view, the global and lo-

cal process variables, which should be visible as OPC UA

tags on the client, are enabled. As soon as the variables

are enabled, the access rights, the limits, the activation of

the history and the unit can be defined via the Properties

window.

Figure 36: OPC UA default view - Enabling a tag

Link to user administration

The access restrictions to the OPC UA system are based on the user management integrated in Automation Runtime

and can be configured directly in the OPC UA default view.

To do this, the roles authorized for the overall system must first be specified on level "Default View". The permissions

that are assigned for the individual roles are inherited to all released data points, but can still be overwritten on the

properties of all data points.

After clicking on "Default View", the desired roles are added in the Properties window via "Add role".

## Page 26

26OPC UA BASICS AND USE TM980

Figure 37: OPC UA default view - Adding roles

Communication / OPC UA / Configuration in AS / OPC UA default view configuration / OPC UA default

view configuration / Properties of OPC UA tags / Authorization

Programming \ Access & Security

Exercise: Create a program and initialize variables

A new Structured Text program is created and the following variables are added: Temperature (INT), Distance (USINT),

Switch (BOOL) and Text (STRING).

In the initialization subroutine, the variables are set to any values.

In the cyclic program, the distance variable is increased by one if the switch is set to TRUE.

1)Add a new ST program.

2)Declare variables.

3)Initialize variables in the initialization subroutine.

4)Program the logic; cyclically increase variable "Distance" by one if the switch is active.

5)Transfer program.

The logic should be programmed and the variables created as follows.

Figure 38: Result: Creating a program

Exercise: Enable the OPC UA default view

The OPC UA default view is added to the configuration. This allows the variables created in the program to be released

on the Automation Runtime OPC UA server.

1)Add the OPC UA default view in the Configuration View under Connectivity\OpcUA using the Toolbox.

2)Select the default view and add all roles in the properties window.

## Page 27

AUTOMATION RUNTIME OPC UA SERVER27

3)Configure write permissions for the Administrator and ServiceEngineer roles.

4)Enable variables "Temperature", "Distance", "Switch" and "Text", see "Releasing variables" on page 25.

5.2Applying units, users, limits and historizing

Assigning units

After selecting a variable in the OPC UA default view, a unit can be assigned to the variable via the Toolbox. For this

there is a large number of predefined units that can be searched via the search bar or filter settings.

Figure 39: Toolbox - Assigning units

Limits

Limit values can also be assigned to the nodes. Generally valid default limit values can be assigned, or also role-related

ones. The desired roles can be added via "Add role". A role-related limit value can then be defined.

Communication / OPC UA / Configuration in AS / OPC UA default view configuration / OPC UA default

view configuration / Properties of OPC UA tags / Range of values / Role-based range of values

## Page 28

28OPC UA BASICS AND USE TM980

Figure 40: Properties - Adding limits

There are different ways of handling

value range violations. They can be ac-

cepted (Accept), rejected (Reject) or

clamped (Clamp). In the case of the lat-

ter, the maximum or minimum value is

applied if the value range is violated.

Figure 41: Properties - Accepting/Rejecting value range violations

## Page 29

AUTOMATION RUNTIME OPC UA SERVER29

Rights and roles

Each node can be assigned its own role and rights. For example, nodes can only be written by administrators. Either

the rights/roles of the previous instance can be inherited, or you can set them yourself.

Figure 42: Properties - Rights/Roles

Historizing

It is possible to enable the historizing

function for each node. If this is en-

abled, the values are written to a buffer

at a certain time interval. This buffer

can then be read and the values of the

node can be analyzed.

Figure 43: Properties - Historizing

Exercise: Configure units, limits and rights

The OPC UA default view has already been added to the configuration. Now, further properties of the OPC UA tags

are configured.

1)Assign a unit to variables "Distance" and "Temperature".

2)Assign limits to variables "Distance" and "Temperature".

3)Add role-dependent settings to variables "Distance" and "Temperature" for the value range.

4)Transfer the project.

The following attributes are assigned to variable "Distance":

Unit: "meter"

•

Default value range: 0 / 10

•

Value range violation: Reject

•

The following attributes are assigned to variable "Temperature":

Unit: "degree Celsius"

•

Default value range: -100 / 100

•

"Administrator" value range: -50 / 50

•

"Service engineer" value range: -20 / 20

•

Default value range "Everyone": -10 / 10

•

Value range violation: Truncate

•

## Page 30

30OPC UA BASICS AND USE TM980

The properties of the variables should correspond to the following figures.

Figure 44: Properties of "Distance"

Figure 45: Properties of "Temperature"

Exercise: Use UaExpert to test the limits of the variables with different users

A connection is established with UaExpert to the Automation Runtime OPC UA server. The set limits are checked by

logging in via different users.

1)Establish a connection to the Automation Runtime OPC UA server.

2)Navigate to the following path in the address space: Root/Objects/PLC/Modules/<Default>/Program.

3)Add the variables to "Data Access View".

4)Change the values of the variables with different users and observe their behavior.

The user can be changed either in the shortcut menu of the server or at the top of the toolbar. User-

name and password depend on the configured roles in the OPC UA default view in the Automation Studio

project.

Figure 46: UaExpert: Changing the user

## Page 31

AUTOMATION RUNTIME OPC UA SERVER31

If user "Anonymous" with role "Everyone" wants to enable variable "Switch", this is prevented since this

user does not have the respective rights.

Figure 47: Result: Testing variables

Exercise: Historizing data points

In this exercise, historizing variable "Distance" is enabled. This makes it possible to view the history of a variable over

time under "History Trend View" in UaExpert.

1)Select variable "Distance" and open the properties window.

2)Enable historizing.

3)Set buffer size to 100.

4)Set sampling interval to timer4.

5)Transfer the project.

6)Update the configuration in UaExpert (right click on "Root - Rebrowse").

7)Enable variable "Switch" so that variable "Distance" is increased cyclically.

8)Add "History Trend View".

Figure 48: UaExpert: Adding "History Trend View"

9)Add variable "Distance" to "History Trend View" via drag-and-drop.

10)Start the cyclic update.

## Page 32

32OPC UA BASICS AND USE TM980

You can now view the characteristic curve over time of a variable in "History Trend View". Because variable

"Distance" is of data type USINT, the value is set to 0 if more than 255 is counted.

Figure 49: Result: Historizing

## Page 33

CERTIFICATES AND SECURITY33

6Certificates and security

Certificates

Instead of renewing the certificates, the OPC Foundation utilizes the functionality of TLS Layers For additional infor-1

mation, see Automation Help.

Figure 50: Handling certificates

Programming / Access & Security / Transport Layer Security (TLS)

1Transport Layer Security (TLS, more widely known under the predecessor name Secure Sockets Layer (SSL), is a hybrid encryption protocol for secure data transmission on the

Internet (en.wikipedia.org/wiki/Transport_Layer_Security).

## Page 34

34OPC UA BASICS AND USE TM980

To create a certificate in Automation Studio, a new certificate is added to the Configuration View in the

AccessAndSecurity \ CertificateStore \ OwnCertificates \ Certificates package from the Toolbox.

Figure 51: Pasting the certificate

When generating the certificate, the data can now be en-

tered with the corresponding requirements.

Figure 52: Configuring the certificate

## Page 35

CERTIFICATES AND SECURITY35

In order to use the certificate, it must be assigned to an SSL configuration. This is added in the Configuration View in

the package AccessAndSecurity/TransportLayerSecurity from the Toolbox.

Figure 53: Adding the SSL configuration

In this SSL configuration, the configu-

ration type must now be changed from

"General SSL configuration" to "OPC

UA SSL configuration". Then the certifi-

cate, the key and the password of the

certificate are assigned.

Figure 54: Configuring the SSL configuration

## Page 36

36OPC UA BASICS AND USE TM980

The SSL configuration must be as-

signed to the OPC UA server so that

the Automation Runtime OPC UA server

of the controller can then take over the

certificate.

Figure 55: Assigning the SSL configuration

Exercise: Create and use your own certificates

In this exercise, you will create your own certificate and test it with UaExpert.

1)Under AccessAndSecurity \ CertificateStore \ OwnCertificates \ Certificates, add a new OPC UA certificate from

the Toolbox.

2)Enter data and complete the certificate creation.

3)Add an SSL configuration under AccessAndSecurity\TransportLayerSecurity from the Toolbox.

4)In field "SSL configuration type", select option "OPC UA SSL configuration" and assign the created certificate, pri-

vate key and password.

5)In the CPU configuration under OPC UA System \ Activate OPC UA System \ Security, select option "SSLConfigu-

ration" in field "Software Certificates".

6)Transfer the project and use UaExpert to confirm the new certificate when establishing the connection.

When establishing a connection with the UaExpert client, the user's own certificate can be confirmed.

Figure 56: Confirming the certificate

## Page 37

CERTIFICATES AND SECURITY37

6.1Trusted list

A trust list can be stored on the Automation Runtime OPC UA server. First, the certificates you would like to

specify as trusted are added to the package AccessAndSecurity \ CertificateStore \ Third Party Certificates \

SoftwareCertificates from the Toolbox.

Figure 57: Third-party certificates

Next, item "Validate SSL communication partners" is en-

abled and the desired certificates are assigned. This cor-

responds to the trusted list. The OPC UA server with this

SSL configuration only trusts clients that have a certifi-

cate from this trusted list.

Figure 58: Trusted list

Exercise: Attempt to establish an encrypted connection without a client certificate on the server

In this exercise, attempt to establish an encrypted connection without a client certificate on the server.

1)Enable data point "Validate SSL communication" in the SSL configuration under AccessAndSecurity \ Transport-

LayerSecurity.

2)Adjust the safety guidelines in the configuration of the OPC UA system as shown.

## Page 38

38OPC UA BASICS AND USE TM980

Figure 59: Safety guidelines

3)Transfer the project.

4)Connect to the server.

Figure 60: Establishing an encrypted connection

5)Check if the connection is rejected.

## Page 39

CERTIFICATES AND SECURITY39

If the UaExpert certificate is not in the server's trusted list, no connection can be established between

the UaExpert client and the OPC UA server.

Figure 61: Result: Trusted list

Exercise: Use the trusted list

The objective of this exercise is to use the trusted list. For this, the UaExpert certificate is added to the trusted list.

The final step is a test to see whether it is possible to establish an encrypted connection.

1)Select "Own certificate" in UaExpert under Settings\Manage Certificates and save it in the project folder via com-

mand "Copy application certificate to".

Figure 62: UaExpert: Managing certificates

2)Add the saved UaExpert certificate in the Configuration View under AccessAndSecurity \ CertificateStore \

ThirdPartyCertificates \ SoftwareCertificates.

3)Add the UaExpert certificate to the trust list in the SSL configuration.

4)Transfer the project.

5)Establish an encrypted connection using UaExpert.

6)Check if the connection is accepted.

## Page 40

40OPC UA BASICS AND USE TM980

If the client certificate (from UaExpert) was added to the trusted list and attempts to establish a con-

nection, this can now be done successfully.

Figure 63: Connection setup successful

## Page 41

PUBLISHER/SUBSCRIBER CONFIGURATION41

7Publisher/Subscriber configuration

Publish-subscribe model

OPC UA used to work with a client/server mechanism, where a client sends a request to the server. The server processes

the request and then sends a response back to the client. The client/server system will reach its limits if there are many

too nodes on the network.

That was the reason for adding a publish-subscribe architecture to the OPC UA communication solution. The pub-

lish-subscribe model enables one-to-many and many-to-many communication. A server sends its data to the network

(publish) and every client can receive this data (subscribe).

With a client-server mechanism, a client requests in-With a publish-subscribe model, a server sends its da-

formation and receives a response from a server.ta to the network (publish) and every client can receive

this data (subscribe).

Figure 64: Client/Server systemFigure 65: Publish-subscribe model

Publisher configuration

For information about how to configure the Publisher, see Automation Help.

Communication \ UPC UA FX \ PubSub \ PubSub configuration in Automation Studio \ PubSub publisher

configuration

## Page 42

42OPC UA BASICS AND USE TM980

Figure 66: Publisher configuration

The following program is used in the publisher configuration.

Source code: MasterConveyorVariables: MasterConveyor

PROGRAM _CYCLIC VAR

(*Standard application     Var_Cos : REAL;

- counting number /      Var_Sin : REAL;

calculate sin/cos*)     Num_Cos : REAL;

Var_Start := Var_Start+0.1;     Var_Start : REAL;

Var_Sin := SIN(Var_Start); END_VAR

Var_Cos := COS(Var_Start);

IF Var_Cos > 0.999 THEN

Num_Cos := Num_Cos+1;

END_IF;

END_PROGRAM

Exercise: Configure a subscriber

Statistical data accumulates on a control system. This data is needed on several machines. In this exercise, participants

will learn how to send data back and forth between different devices using a Pub/Sub model.

1)Connect controllers to a DHCP network.

## Page 43

PUBLISHER/SUBSCRIBER CONFIGURATION43

2)Create a new ST program and variables for SlaveConveyor.

VAR

VarStart : REAL;

VarSin : REAL;

VarCos : REAL;

VarNum : REAL;

END_VAR

3)Use variables in the init program so they are created by the compiler.

4)Configure subscriber, see Automation Help.

Communication \ UPC UA FX \ PubSub \ PubSub configuration in Automation Studio \ PubSub sub-

scriber configuration

5)Transfer configurations and test communication.

Figure 67: Subscriber configuration

## Page 44

44OPC UA BASICS AND USE TM980

8PLCopen function blocks

Using the PLCopen function blocks of the AsOpcUac li-

brary, it is possible to program the establishment of a

connection to an OPC UA server or the reading/writing of

nodes.

The AsOpcUac library can be used to exchange the

process data on a B&R controller with OPC UA servers

on systems of different platforms. The library's function

blocks for OPC UA client functionality were developed

in cooperation between OPC Foundation and PLCopen

working groups. A sample project for using the function

examples is installed with Automation Studio.

Figure 68: PLCopen client function blocks

For information about how the function blocks work and how to use them, see Automation Help:

Programming / Libraries / Communication / AsOpcUac

Automation Software / Example programs / Communication with OPC UA client function blocks

Exercise: Read/Write variables with PLCopen function blocks in the sample project

In this exercise, the functionality of libraries "AsOpcUac" and "AsOpcUas" is explained using the sample project. The

client and server functionality are programmed in this project. By enabling the function blocks in the correct order,

server variables can be read and written to.

In this example project, port 4841 is used for OPC UA.

1)Unzip file "OpcUa_Sample.zip" under path C:\BrAutomation\AS412\Samples.

2)Open the project in the unzipped folder and transfer it to the simulation.

3)Open the Watch window of the "Client01" program in the "Client" package and add all variables.

4)Open the Watch window of program "ServerTask" in the "Server" package and add all variables to the server vari-

ables overview.

5)Set variable "ExecuteConnect_0" to TRUE in order to connect to the server.

6)Set variable "ExecuteGetnamespaceindex_0" to TRUE to read the index of the namespace.

7)Set variable "ExecuteNodeGetHandle_0" to TRUE to receive the handle for the configured node.

8)Set variable "ExecuteRead_0" to TRUE to read server variable "VarX". This is saved in variable "VarA" on the client.

9)Set variable "ExecuteWrite_0" to TRUE to write client variable "VarB" to server variable "VarX".

Programming \ Libraries \ Communication \ AsOpcUac \ Program examples

Communication \ Fieldbuses \ X20 fieldbuses / X67 system \ Bus controller \ OPC UA \ Program exam-

ples

## Page 45

METHODS45

9Methods

Methods allow a client to start a "program" on a server. The client can transfer input arguments to the server. When

the server has finished processing the method, it can then transfer output arguments to the client.

Figure 69: Example of how methods work

The advantage of OPC UA is that this handshake is already implemented. The user only has to program the method

and decide when the method should be called.

Communication \ Fieldbuses / Fieldbuses in X20 / X67 system \ Bus controller \ OPC UA \ Methods

Communication \ OPC UA \ Use cases \ Methods

Exercise: Call methods on bus controller X20BC008U

Method "Reboot" is used to restart the bus controller from UaExpert.

1)Establish the connection to X20BC008U using UaExpert.

2)Navigate to Root/Objects/DeviceSet/X20BC008U/MethodSet in "Address space".

3)Execute the "Reboot" method via a right click on "Call".

4)Observe the startup of the bus controller.

## Page 46

46OPC UA BASICS AND USE TM980

All methods are executed with a right-click on "Call".

Figure 70: Method "Reboot"

Exercise: Call methods with PLCopen function blocks in the sample project

In this exercise, a method is called that multiplies two numbers. To do this, the server must first be prepared for method

calls. The prepared method can be called via program "Client03" or alternatively via UaExpert.

1)Open "OpcUa_Sample" and transfer it to the simulation.

2)Open the Watch window of program "SrvMethod" in package "Server" to add all variables.

3)Set variable "Step" to 1 to prepare the server for the following method calls.

4)Use UaExpert to establish a connection to the simulation's Automation Runtime server.

5)Navigate to path Root/Objects/PLC/Modules/<Default>/SrvMethod in "Address space" and call method "Multi-

ply".

6)Open the Watch window of program "Client03" in package "Client" and add all variables.

7)Set variable "ExecuteConnect_0" to TRUE in order to connect to the server.

8)Set variable "ExecuteGetnamespaceindex_0" to TRUE to read the index of the namespace.

9)Set variable "ExecuteMethodGetHandle_0" to TRUE to receive the handle for the configured method.

10)Set "variable" ExecuteMethodCall_0" to TRUE to call the method.

## Page 47

METHODS47

After calling the method with UaExpert, the result is displayed if the method was processed successfully.

Figure 71: Result: Method "Multiply"

Exercise: Create methods

The objective of this exercise is to create and call a method that subtracts two values.

1)In the "Server" package, add a new "Structured Text All in One Program" with the name "MyMethod".

2)Copy the complete code of the "SrvMethod" task to "MyMethod".

3)Copy all variables of the "SrvMethod" task to "MyMethod".

4)Add a new OPC UA method declaration file from the Toolbox to program "MyMethod".

Figure 72: Adding OPC UA method declaration file

5)Add a new method named "Sub" to the declaration file.

6)Arguments "x" (direction VAR_INPUT, linked variable "VarA"), "y" (direction VAR_INPUT, linked variable "VarB") and

"z" (direction VAR_OUTPUT, linked variable "VarResult") are created in this method.

## Page 48

48OPC UA BASICS AND USE TM980

Figure 73: OPC UA method declaration file: Variables

7)Enable method "Sub" in the OPC UA default View in task "MyMethod" and delete example "WaterTank".

8)Change the "MethodName" in the program code of "MyMethod" from "Multiply" to "Sub" at steps 1 (prepare

UaSrv_MethodOperate) and 4 (start operate).

9)In step 3, change the calculation to "VarResult:= VarA - VarB" execute method code).

10)Transfer the project.

11)Set variable "Step" to 1 to prepare the server for the following method calls.

12)Call methods with UaExpert.

## Page 49

APPENDIX49

10Appendix

10.1OPC UA FX package

Enabling or disabling the OPC UA FX Technology Package as well as changing the version of the Technol-

ogy Package may cause the controller to restart during project installation.

Communication \ OPC UA FX \ General \ Installing the Technology Package

Optional exercise: Install OPC UA FX package

To be able to use the OPC UA FX Technology Package, it must be installed in Automation Studio via the Upgrades

dialog box.

1)Under Extras \ Upgrades, download and install the "OpcUaFx" Technology Package.

2)Select the OPC UA FX Technology Package under Project \ Change runtime versions.

Figure 74: Extras\Upgrades

Figure 75: Project \ Change Runtime versions

## Page 50

50OPC UA BASICS AND USE TM980

10.2Firmware update X20BC008U

If an error occur during the firmware download, we recommend using a different browser.

Hardware \ X20 System \ X20 module \ Bus controller \ X20BC008U \ Firmware update

Optional exercise: Perform a firmware update for X20BC008U

The following steps are required to update the firmware of the bus controller:

1)Download the firmware from the B&R website (Search term: "Firmware X20BC008U").

2)Call up the IP address or the host name of the bus controller in the browser

(e.g. http://192.168.1.2 or http://bropc2 ).

3)Select the downloaded firmware file under "Advanced/Firmware download".

4)Select "Start download".

5)When the download is complete, restart the bus controller via "Restart bus controller".

After the downloaded firmware has been selected and command "Start download" has been executed,

the firmware is updated. After download, the bus controller must be restarted.

Figure 76: Firmware download

## Page 51

APPENDIX51

10.3Optional exercises

Optional exercise: Publisher / Subscriber continuation

The objective of the exercise is to make a variable available to the subscriber. The program from a previous exercise is

used for this ("Exercise: Create a program and initialize variables" on page 26). The variable "Distance" is transferred.

Figure 77: Example of a possible topology

In order to communicate with another network station via ARsim, the IP address of the ETH adapter of

the PC running ARsim must be entered in the ARsim Ethernet configuration, not 127.0.0.1.

Communication \ OPC UA FX \ PubSub \ Diagnostics \ FAQ

1)In the Configuration View under Connectivity \ OpcUaFx, add the PublishSubscribe file (UaPubSub) via the Tool-

box.

2)In the PublishSubscribe file under WriterGroups, set the publishing interval to 100 ms.

3)In the PublishSubscribe file under WriterGroups \ PublishedDataSet, select the variable "Distance" in the source

variable field.

4)Transfer the project.

5)Check the Logger; an entry such as "OPC UA PubSub was started successfully" should be present.

6)In the PublishSubscribe file, right-click on PubsubConnection and select the option "Export all published

DataSets for all connections to *.uabinary".

7)Save the .uabinary file.

8)Add a new configuration in the Configuration View and name it "Subscriber" (hardware for example PC = simula-

tion).

9)Enable the OPC UA system in the CPU configuration.

10)Add a new ST program with the name "Subscriber" in the Logical View.

VAR

DistanceSubscriber : USINT;

END_VAR

PROGRAM _INIT

DistanceSubscriber := DistanceSubscriber;

END_PROGRAM

11)Add the OPC UA default view in the Configuration View under Connectivity\OpcUA using the Toolbox.

12)Enable the variable "DistanceSubscriber".

## Page 52

52 OPC UA BASICS AND USE TM980
13)In the Configuration View under Connectivity \ OpcUaFx, add the PublishSubscribe file (UaPubSub) via the Tool-
box.
14)In the PublishSubscribe file, right-click on ReaderGroups and click on the option "Import DataSets from *.uabina-
ry".
15)Select the previously saved .uabinary file.
16)In the PublishSubscribe file under ReaderGroups \ SubscribedDataSet, select variable "DistanceSubscriber" in the
target variable field.
17) Delete WriterGroup1 in the PublishSubscribe file.
18)In the Logical View, adjust the Ethernet settings so the local laptop IP address is used for the simulation, for ex-
ample.
19)In the software configuration, assign the "Subscriber" task to task class #1 (100 ms).
20)Transfer the project, e.g. offline installation to the simulation.
21)In UaExpert, set the "Switch" variable to TRUE and check whether the "Distance" variable changes its value cycli-
cally.
22)Use Monitor mode in Automation Studio to check the "DistanceSubscriber" variable to see whether it changes its
value cyclically.

## Page 53

SUMMARY53

11Summary

OPC Unified Architecture (OPC UA) is a manufacturer-independent communication protocol for automation applica-

tions in industry. It enables end-to-end communication from individual sensors and actuators to the ERP system or

the cloud.

Figure 78: OPC UA pyramid

Configuring OPC UA devices and assigning access rights in B&R's Automation Studio engineering environment is com-

pleted with just a few clicks. PLCopen function blocks are used to implement OPC UA functions in an automation

project.

## Page 54

54OPC UA BASICS AND USE TM980

Automation Academy

Gain additional knowledge

The Automation Academy provides  courses for our customers as well as for our own employees.targeted training

Expand your skills in the field of automation technology and learn to independently implement efficient automation

using B&R systems.solutions

Decide for yourself which  you want to follow!learning concept

Classroom training

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

## Page 55

AUTOMATION ACADEMY 55

## Page 56

B&R

Industrial Automation GmbH

A member of the ABB Group

B&R Straße 1

5142 Eggelsberg, Austria

office@br-automation.com

t +43 7748 6586-0

f +43 7748 6586-26

br-automation.com

V3.0.0.0 ©2024/09/09 by B&R, All rights reserved.

All registered trademarks are the property of their respective owners.

Subject to technical changes without notice.