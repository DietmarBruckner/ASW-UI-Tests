## Page 1

TM950

POWERLINK

configuration and

diagnostics

## Page 2

2 POWERLINK CONFIGURATION AND DIAGNOSTICS TM950
Requirements
TM210 - Working with Automation Studio
Training modules TM213 - Automation Runtime
Automation Studio 4.11 or higher
Software
Hardware ETAL950 (POWERLINK) with X20CP1586

## Page 3

TABLE OF CONTENTS 3
Table of contents
1 Introduction.........................................................................................................................................................4
1.1 Learning objectives..............................................................................................................................5
1.2 Safety notices and symbols...............................................................................................................5
2 POWERLINK - Introduction...............................................................................................................................6
2.1 Industrial Ethernet...............................................................................................................................6
2.2 POWERLINK...........................................................................................................................................6
3 POWERLINK – Technology................................................................................................................................9
3.1 POWERLINK in the OSI model...........................................................................................................9
3.2 Data link layer.....................................................................................................................................10
3.3 CANopen application interface.......................................................................................................15
3.4 Network management (NMT).........................................................................................................25
4 POWERLINK – Diagnostics.............................................................................................................................29
4.1 Diagnostics tools...............................................................................................................................29
4.2 Startup behavior of a POWERLINK node......................................................................................39
4.3 Diagnostics strategy........................................................................................................................44
5 POWERLINK - Design and optimization......................................................................................................46
5.1 Network design and timing performance....................................................................................46
5.2 Automation Studio settings for network timing........................................................................53
5.3 Mapping in Automation Studio......................................................................................................56
5.4 Optimization options in Automation Studio...............................................................................57
6 POWERLINK - Third-party device support..................................................................................................66
6.1 XDD - Device description file..........................................................................................................66
6.2 Device description file for modular devices................................................................................67
6.3 XDD application example: openPOWERLINK MN........................................................................68
6.4 Examples of non-B&R MNs:.............................................................................................................68
7 Summary............................................................................................................................................................69
8 Appendix – Solutions to the exercises........................................................................................................70

## Page 4

4POWERLINK CONFIGURATION AND DIAGNOSTICS TM950

1Introduction

When it comes to fieldbus technology, B&R relies heavily on the real-time, Ethernet-based POWERLINK network to

handle the highest demands on timing and data throughput. Automation Studio makes it easy to use this technology

in the field. In order to take advantage of all of the possibilities offered by the POWERLINK communication protocol

and ensure it is used correctly, it is important to learn as much as possible about the design, configuration and trou-

bleshooting options presented by this technology. This training module is designed to provide this information in the

form of extensive theory and practical examples that will offer a much deeper look into what comprises POWERLINK

technology.

This training module will explain the many different POWERLINK configuration and optimization options available in

Automation Studio and their effects on the overall system. It will also introduce a range of powerful tools for trou-

bleshooting errors on a POWERLINK network.

## Page 5

INTRODUCTION 5
1.1 Learning objectives
This training module uses selected examples illustrating typical application tasks to help participants learn how POW-
ERLINK technology works and how it is applied in Automation Studio. It also presents the various options for diagnos-
ing errors and explains how to integrate 3rd-party devices.
Learning objectives and contents of this training module:
■ Participants will learn the basics of POWERLINK technology.
■ Participants will become familiar with the "CANopen" application interface for POWERLINK.
■ Participants will learn how to use service and diagnostics options.
■ Participants will learn how POWERLINK functions and is displayed in Automation Studio.
■ Participants will learn how to use the POWERLINK library in Automation Studio.
■ Participants will learn the meaning of the parameters for setting up networks.
■ Participants will learn about the possibilities for optimizing the POWERLINK network in Automation Studio.
■ Participants will learn how to integrate 3rd-party devices using a device description file (XDD).
1.2 Safety notices and symbols
The meaning and usage of safety notices and symbols is described in the main module "TM210 – Working with Au-
tomation Studio".
Special notes on this manual
Help: Unless otherwise mentioned, this note type always refers to Automation Help.

## Page 6

6POWERLINK CONFIGURATION AND DIAGNOSTICS TM950

2POWERLINK - Introduction

POWERLINK is an Ethernet-based, real-time capable network protocol. It builds upon the physical and media access

control layers defined in the IEEE 802.3 standard and allows for deterministic transmission of payload data in the

microsecond range. Since POWERLINK builds upon the Ethernet standard without violating it in any way, it can be

implemented on any new or existing hardware/software platform.

POWERLINK is primarily used to transfer process data in automation systems.

2.1Industrial Ethernet

Communication between the machines and components that make up an automation system takes place over fieldbus

networks. These are generally serial (RS485) or CAN-based systems with additional media access control and applica-

tion layers.

These types of conventional fieldbuses are, for example:

CANopen

■

PROFIBUS

■

DeviceNet

■

With more and more applications exceeding the performance capabilities of these serial and CAN-based systems,

the need for an alternative became increasingly urgent. With its widespread use in IT systems and 30-year history

of ongoing development, as well as interference-resistant cables ideal for harsh industrial environments, Ethernet

technology presented the ideal solution.

In the ISO OSI model (see 3.1 "POWERLINK in the OSI model"), the Ethernet standard describes layers 1 and 2, i.e.1

the physical and media access control layers. The CSMA/CD method used here is ideal for a decentralized network2

structure, but does not provide deterministic transmission. Deterministic transmission is an essential requirement

for industrial applications (particularly in control and positioning applications).

Figure 1: CSMA/CD mechanism

One way to prevent collisions on the network is using switches, which provide a separate bus segment for each sta-

tion. The disadvantage of this approach is that it increases the transmission delay of payload data. Especially short

throughput times in industrial applications, however, are more important than the actual data throughput.

It is also possible to use a request/response method. This approach defines a single network station that will manage

media access. In this scenario, all other stations are only permitted to send data after receiving permission.

2.2POWERLINK

To solve the problem of deterministic transmission, POWERLINK uses a request/response, or "polling" approach. One

node – known as the master, or managing node (MN) – controls access to the network. The slaves, or controlled nodes3

1Open Systems Interconnection Model

2Carrier Sense Multiple Access / Collision Detection

3Network station addressed via a node number

## Page 7

POWERLINK - INTRODUCTION7

(CNs), follow the commands of the managing node and only transmit data upon request from the managing node.

This eliminates collisions and provides a basis for real-time capability.4

Figure 2: Master-slave system

Standardization

POWERLINK is an open real-time Ethernet protocol, which means both the specification and an implementation –

known as a "protocol stack" or simply "stack" – are freely available. There are no licensing fees required. The licensing

model for the stack is a BSDlicense, which provides the maximum amount of freedom. Development and maintenance5

of the POWERLINK specification is handled by the Ethernet POWERLINK Standardization Group (EPSG). For more in-6

formation, see www.ethernet-powerlink.org.

POWERLINK is both an IEC and IEEE standard as well as a Chinese national standard for communication technolo-

gy:

IEC 61784-2: Industrial communication networks - Profiles - Part 2: Additional fieldbus profiles for real-time net-4

works based on ISO/IEC 8802-3.

4IEC 61158: Industrial communication networks - Fieldbus specifications

4IEEE 61158-2017: IEEE standard for industrial hard real-time communication

GB/T 27960-2011: Ethernet POWERLINK communication profile specification4

4Except in the event of an error (i.e. incorrect cabling or network settings)

5Berkeley Software Distribution; open source license with no reciprocal obligation to disclose source code

6www.ethernet-powerlink.org / Downloads / Technical Documents / EPSG DS 301 V1.x.x - Communication Profile Specification

## Page 8

8 POWERLINK CONFIGURATION AND DIAGNOSTICS TM950
Characteristics of a POWERLINK network
POWERLINK is based on Fast Ethernet (100 Mbit/s)
•
Up to 239 CNs
•
200 µs7 minimum cycle time
•
Recommended cable types:
•
■ Copper cable: Cat. 5, S/UTP, 26 AWG
■ Fiber optics: Multimode fiber with 62.5/125 µm or 50/125 µm core diameter
Process data size per node: Up to 1,490 bytes8
•
Asynchronous data transmission: Up to 1500 bytes per cycle and station9
•
Topology: Line, star, tree or any combination thereof
•
7 Value achieved in established MN implementations (e.g. by B&R). The theoretical limit is lower, and is not defined in the specification.
8 A higher value can be reached with the extension "Multi PReq/PRes."
9 A higher value can be achieved with the"Multi ASnd" specification extension.

## Page 9

POWERLINK – TECHNOLOGY9

3POWERLINK – Technology

3.1POWERLINK in the OSI model

The OSI model is a standardized conceptual model of an open information processing system. The most significant

manufacturer-independent transfer protocols commonly used today are based on this model. It groups the internal

functions of a communication system into seven layers. Each layer builds upon the layer below it and represents an

instance that performs its task according to specific rules. These rules are defined by protocols, which may span mul-

tiple layers.

Layer 1, the physical layer, describes the electrical, mechanical and functional interfaces to the physical transmission

medium. The next layer above it, the data link layer, provides a reliable link between two directly connected nodes by

detecting errors that may occur in the physical layer. These two layers are often referred to collectively as "the physical

layers".

Layers 3 and 4 above them are known collectively as "the transmission layers". Layer 3, the network layer, controls the

logic and timing of data transmission. Layer 4, the transport layer, segments the data and allocates it to the applica-

tions.

Layers 5 (session layer), 6 (presentation layer) and 7 (application layer) are referred to collectively as "the application

layers" since all programs and applications access them directly. As explained above, Ethernet only defines the physical

and data link layers. Protocols that do not access the same layers can be used in combination – like POWERLINK and

CANopen.

POWERLINK specifies the data link layer and the layers above it, while CANopen deals exclusively with

the application layers.

Figure 3: POWERLINK application interface – CANopen

## Page 10

10POWERLINK CONFIGURATION AND DIAGNOSTICS TM950

3.2Data link layer

The POWERLINK data link layer (DLL) defines the mechanism for network access. Network nodes, so-called POWERLINK

nodes, are only permitted to send data at defined times. Network access is managed by a dedicated node, called the

managing node (MN). A POWERLINK network always has exactly one active managing node (MN). The MN can manage

up to 239 controlled nodes (CNs).

3.2.1POWERLINK cycle

The POWERLINK cycle is divided into an isochronous and an asynchronous phase. In the isochronous phase, process

data from the network nodes is transferred cyclically. The asynchronous phase allows the exchange of acyclic data

between two or more nodes. This phase is used for network management, configuration, diagnostics and non-time-

critical data queries. It also allows for the transfer of any non-POWERLINK frames (e.g. IP-based data such as websites).

Figure 4: POWERLINK cycle

Abbrevia-NameSourceMeaning

tion

SoCStart of cycleMNThe MN sends this frame at the beginning of the POWERLINK cycle.

This synchronizes all of the network stations.

PReqPoll requestMNThe MN sends this frame along with the payload to each CN.

PResPoll responseCN/MNA CN sends a poll response with real-time data as a response to a

poll request.

SoAStart of asynchro-MNMarks the end of cyclic data communication and thus the start of

nousasynchronous communication and assigns transmission permis-

sions for asynchronous communication.

ASndAsynchronous sendCN/MNThis frame transports asynchronous (non-real-time) data.

AMNI*Active ManagingMNIn networks with redundant masters, a MN uses this frame to indi-

Node Indication,cate that it is switching from passive to active mode to take over for

used by EPSG DS302-a failed MN.

A

Table 1: POWERLINK frame types

## Page 11

POWERLINK – TECHNOLOGY11

Abbrevia-NameSourceMeaning

tion

AInv*Asynchronous Invite,MNThis frame is used to assign additional transmission rights for asyn-

used by EPSG DS302-chronous communication and is used for the "Multiple Asynchro-

Bnous Send" POWERLINK extension.

Table 1: POWERLINK frame types

*)These frame types are only included in the table for the sake of completeness. They are not shown in the image

above because they represent optional POWERLINK extensions.

The structure of a POWERLINK frame is shown in the image below (Fig. 5). The individual layers (from the OSI model)

encapsulate each other. A higher layer is always embedded in the payload data of the layer below it.

A POWERLINK frame is identified as such by value "0x88AB" in field "Ethertype" in the Ethernet frame header .10

Figure 5: POWERLINK frame structure

3.2.2Isochronous phase

In the isochronous phase the network stations exchange data cyclically. This is where real-time data traffic occurs.

Cycle time

The isochronous phase is initiated by the MN. The MN sends the Start of Cyclic (SoC) frame to all network stations.

The SoC is also used to synchronize the network. In a fully synchronized network, this frame activates the last received

output data and triggers sampling of input data. The SoC is sent as a multicast frame. The cycle time is the time11

between SoC frames sent by the MN.

10"See Ethertype 0x0800 (identifies IPv4 frames)

11Type of frame that is received simultaneously by an entire group of nodes (all POWERLINK nodes)

## Page 12

12POWERLINK CONFIGURATION AND DIAGNOSTICS TM950

tt

CycleCycle

MNSoCSoCSoC

t

Figure 6: The MN determines the cycle time by sending the SoC frame.

Direct cross-traffic

The poll request (PReq) and poll response (PRes) frames are also sent in the isochronous phase. PReq is a unicast

frame containing the MN's real-time data, generated only by the MN and sent to a specific CN. The respective CN must

respond to a received PReq with a PRes. The PRes contains the CN's real-time data. Since the PRes is defined as a

multicast frame, the CN's real-time data is distributed throughout the entire network. This makes it possible for a CN

to read data from another CN (in the PRes frame) directly from the network. Since this type of communication occurs

directly, without passing through the MN, it is called direct cross-traffic.

Figure 7: Direct cross-traffic with (POWERLINK) broadcast mechanism

Advantages of direct cross-traffic

Data transfer in one cycle, i.e. reduced response time

■

More cyclic nodes possible (without increasing cycle time)

■

Shortens the minimum required POWERLINK cycle time (with same number of nodes)

■

Reduces the load on the MN

■

Completing data transfer in one cycle makes it possible to achieve the lowest possible latency in CN-to-

CN communication.

One application of cross-traffic is for electronic axis synchronization.

Poll response chaining

Since the MN can also send a PRes, the same process data can be distributed to multiple CNs in a single step (broadcast

channel). This mechanism is required for poll response chaining mode. CNs operated in this mode no longer require a

PReq frame from the MN. Instead, they each respond to the PResMN frame with a constant delay. The delay for each

node is automatically calculated at startup.12

12Special POWERLINK frames are used to calculate the timing for the topology (SoA with SyncReq flag, ASnd with SyncResp flag).

## Page 13

POWERLINK – TECHNOLOGY13

Figure 8: Poll response chaining

Advantages of poll response chaining

More cyclic nodes possible (without increasing cycle time)

■

Shortens the minimum required POWERLINK cycle time (with same number of nodes)

■

Reduced topology-related latency (particularly with line topology)

■

The benefits of poll response chaining are primarily due to the elimination of one PReq frame per station.

This optimization is used in central closed-loop control systems where small volumes of data are trans-

ferred, e.g. in CNC systems (position control).

Multiplexing

To optimize the cycle time and better represent the real application in the network, nodes can also be operated in

multiplexing mode. This means that each CN is only addressed by the MN in every nth cycle.

Multiplexing prescale

Cycle iCycle i+1Cycle i+2Cycle i+3

4223311223311876221133455112233

Multiplexed: 4 - 8Standard: 1, 2, 3

Time

Figure 9: Multiplexing diagram

## Page 14

14POWERLINK CONFIGURATION AND DIAGNOSTICS TM950

Advantages of multiplexing

Optimizes the use of bandwidth

■

More cyclic nodes possible (without increasing cycle time)

■

Shortens the minimum required POWERLINK cycle time (with same number of nodes)

■

Multiplexing can increase the maximum number of stations that are possible within one POWERLINK

cycle. Since multiplexing nodes can read PRes frames from other nodes at any time, a combination of

multiplexing and cross-communication can result in highly optimized solutions.

This optimization is used when transmitting status and configuration data (e.g. temperature measure-

ment) used in slow control algorithms.

A CN can be operated in "Standard" (PReq/PRes in every cycle), "Multiplexed" or "Poll Response Chaining"

mode. "Multiplexed Chaining" mode is not an option, i.e. it is not possible to set both modes at once.

3.2.3Asynchronous phase

The asynchronous phase follows immediately after the isochronous phase (see Fig. 4). The MN initiates this phase by

sending the Start of Asynchronous (SoA) frame. Like the SoC, the SoA is also a multicast frame. In this frame, the MN

grants transfer permission to any station in the network (Fig. 10). The selected node may now send either asynchro-13

nous POWERLINK data (ASnd) or an Ethernet frame of some sort (e.g. IP data). It is up to the node whether it is sent

as a unicast, multicast or broadcast frame.

The MN grants prioritized transfer permission based on previous access requests from network stations. These re-

quests to the MN can be contained in both asynchronous (ASnd) and cyclic (PRes) frames.

The asynchronous phase makes "hot plugging" possible, allowing CNs to be added to the network without affecting

its real-time behavior.

Figure 10: Assignment of an asynchronous slot

The main application of asynchronous communication is to transfer network management commands

(NMT) and asynchronous access to objects (SDO protocol).

Multiple asynchronous send

Multiple asynchronous frames can be sent in one cycle in order to increase throughput in the asynchronous phase.

Each CN must still be invited by the MN, but this must be done using a different type of frame – the Asynchronous

Invite (AInv). The MN does not invite itself with an AInv frame, but rather sends its asynchronous data directly in the

"extended" asynchronous phase.

13i.e. even to itself

## Page 15

POWERLINK – TECHNOLOGY 15
MN
POWERLINK Cycle
Asynchronous phase
SoC PReq PReq SoA AInv ASnd ASnd AInv
PRes PRes ASnd ASnd ASnd
CN
Figure 11: Multiple asynchronous send
When the goal is to optimize the throughput of non-real-time data on a POWERLINK network, multiple
asynchronous send is used.
This optimization is used, for example, when transmitting image data that requires a large amount of
bandwidth.
3.3 CANopen application interface
-The POWERLINK application interface is based on CANopen14. Therefore, POWERLINK can also be referred to as
"CANopen over Ethernet". Although calling it this oversimplifies matters somewhat and fails to highlight POWERLINK's
real-time capabilities, it does capture the fundamental nature of the communication system.
This section describes the structure of the CANopen interface and explains how it is used to establish a data connec-
tion between POWERLINK nodes.
3.3.1 Object dictionary
The structure of the CANopen application interface uses an object dictionary (OD). The object dictionary consists of
a group of predefined, numbered objects with various functions.
Objects
Properties of an object:
■ An object can be viewed as a type of container. This container can simply hold data that can be read and written,
or it can possess additional functionality, so that access triggers a predefined action.
■ An object can be addressed via a unique 16-bit index.
■ An object can contain up to 256 subobjects, each addressed via an 8-bit subindex.
■ An object is defined by a number of parameters, including access rights, data type and type of data transfer.
14 Due to the differences in the physical layer and the fixed real-time behavior of POWERLINK compared to CAN bus physics, there are minor differences between CANopen and
POWERLINK.

## Page 16

16 POWERLINK CONFIGURATION AND DIAGNOSTICS TM950
An object can be defined with the following parameters:
Index 0x1F98
Name NMT_CycleTiming_REC
Object type RECORD
Data type NMT_CycleTiming_TYPE
Category 15.
Table 2: Parameters of an object
Subindex 0x03
Name PResMaxLatency_U32
Data type UNSIGNED32 Category CN: M, MN: -
Value range UNSIGNED32 Access const16
Default value no PDO mapping No
Table 3: Parameters of a sub-object
Structure of the object dictionary
The object dictionary is divided into multiple ranges, which are described in Tab. 4. Most important for POWERLINK
users is the object range ≥ 0x2000. POWERLINK technology integrators can define objects freely in this range.
Index Object range description
0x0000 Not used
0x0001 .. 0x001F Static data types
0x0020 .. 0x003F Complex data types
0x0040 .. 0x005F Manufacturer-specific complex data types
0x0060 .. 0x007F Device profile specific static data types
0x0080 .. 0x009F Device profile specific complex data types
0x00A0 .. 0x03FF Reserved for further use
0x0400 .. 0x041F POWERLINK-specific static data types
0x0420 .. 0x04FF POWERLINK-specific complex data types
0x0500 .. 0x0FFF Reserved for further use
0x1000 .. 0x1FFF Communication profile area
0x2000 .. 0x5FFF Manufacturer-specific profile area
0x6000 .. 0x9FFF Standardized device profile area
0xA000 .. 0xBFFF Standardized interface profile area
Table 4: "Object dictionary structure" from EPSG DS 301 V1.x.x - Communication Profile Specification
Application data that is transferred cyclically or acyclically is found the object range ≥ 0x2000.
15 MM: Mandatory; object must exist
16 const: Constant; value can only be read and does not change.

## Page 17

POWERLINK – TECHNOLOGY 17
Device profiles
Object groups in the range ≥ 0x6000 have been predefined in order to provide a consistent, vendor-independent in-
terface for specific device types (see Tab. 4). A group of objects dedicated to a specific device type is called a device
profile. Some examples include rotary encoders, frequency inverters and valve controllers.
It is not absolutely necessary to use one of these standard interfaces, but for components that are not permanently
integrated in an overall system it is recommended.
Standardized device profiles are provided by the user organization CAN in Automation (CiA)17. This orga-
nization also assigns each device a CANopen Vendor IDT18.
The Ethernet POWERLINK Standardization Group (EPSG)19 can also assign a (CANopen) vendor ID. How-
ever, this is then only permitted be used for POWERLINK devices.
3.3.2 Service Data Objects (SDO)
The Service Data Object protocol can provide asynchronous access to objects. The object dictionary of a POWERLINK
node can be accessed from any other node using the SDO protocol as long as the object exists and access is permitted.
The SDO protocol operates according to the server/client principle. The client reads/writes data stored on the server.
Usage Example
Non-cyclic data access Transferring object data ≥ 0x2000 in the asynchronous phase (ASnd
frame)
POWERLINK network configuration Startup of a POWERLINK node
Diagnostics Read error counter
File transfer Firmware upgrade
Table 5: Possible uses of the SDO protocol
17 www.can-cia.org
18 he Vendor ID identifies the manufacturer of a CANopen or POWERLINK device.
19 www.ethernet-powerlink.org

## Page 18

18POWERLINK CONFIGURATION AND DIAGNOSTICS TM950

The server contains e.g. data in object 0x1C0B/0x01. Another POWERLINK node (SDO client) can access20

this data by sending an SDO read command (containing the index and subindex) to the SDO server. The

server answers with an SDO response containing the data from object 0x1C0B/0x01 (in example 0x06).

Figure 12: SDO server-client principle

SDO communication occurs in the asynchronous phase, i.e. the SDO frame is embedded in the ASnd

frame.

SDO read or write commands can also be bundled to send multiple read or write commands in a single21

ASnd frame. This can increase the throughput of SDO communication.

3.3.3Process data objects (PDO)

Cyclically transferred process data objects are embedded in PDO frames. A PDO frame can contain one or more objects

and their data.

Transfer PDO properties:

PDO frames transferred cyclically, i.e. time-based not event-based

■

PDO frames are sent as user data in the PRes or PReq frame (see "POWERLINK Payload" in Fig. 5 "POWERLINK

■

frame structure")

Producer-consumer principle:

■

Every node can produce object data for the network in the form of PDO frames.

■

One or more other nodes can read this PDO frame in the same cycle.

■

There is no direct connection between any two nodes.

■

20Any particular POWERLINK node

21Using "Read Multiple Parameter by Index" or "Write Multiple Parameter by Index"

## Page 19

POWERLINK – TECHNOLOGY19

TPDO and RPDO

A PDO frame is sent by a network station (producer). For this node, the frame is a Transmit PDO () frame. ForTPDO

the node (consumer) that receives it, this frame is a Receive PDO () frame. In other words, any PDO frame canRPDO

be seen as either a TPDO or RPDO depending on the perspective (see Fig. 14 "TPDO mapping of an object" on page

21 and Fig. 15 "RPDO mapping of an object").

3.3.4Mapping

How does user data get from the network (from a PDO frame) into a process variable and vice versa?22

The answer to this question is summarized as the term "mapping". Mapping functionality is derived from POWERLINK's

CANopen application layer.

Mapping is the process of assigning cyclic network data to a  node's objects (and their respectivespecific

data points / process variables). To exchange cyclic data between two nodes, then, mapping (as well as

source and target objects) is required independently on both the transmitter and receiver ends.

With POWERLINK, mapping occurs in mapping objects.

Mapping objects

Mapping is defined by objects contained, like all other objects, in a bus station's object dictionary. Mapping objects

can be found on both the MN and CNs.

The object dictionary has a range dedicated specifically to mapping.

Object areaObject nameContents

RPDO0x1400 to 0x14FFRPDO communication parameterMapping version and source ad-23

dressing

0x1600 to 0x16FFRPDO mapping parameter"RPDO to object" connections

TPDO0x1800 to 0x18FFTPDO communication parameterMapping version and target ad-24

dressing

0x1A00 to 0x1AFFTPDO mapping parameter"Object to TPDO" connections

Table 6: Object range for PDO mapping objects.

RPDO and TPDO mapping always require a corresponding pair of communication and mapping parameters (see Tab.

6) that provides the network all the necessary information about the object data link. This pair is always identified by

the last two characters in the index number.

Example for PDO mapping object pairs (see Tab. 6):

RPDO: 0x14 and 0x160101

■

TPDO: 0x18 and 0x1A0000

■

22In this training module, "process variable" refers to application variables that are refreshed cyclically via POWERLINK.

23Receive Process Data Object

24Transmit Process Data Object

## Page 20

20 POWERLINK CONFIGURATION AND DIAGNOSTICS TM950
Relationship between object dictionary and PDO mapping
The following image (Fig. 13) illustrates the relationship between the object dictionary, mapping objects, application
objects, process data objects (PDOs), POWERLINK frames and process images.
The mapping objects establish the correlation between the cyclic POWERLINK frame payload data (PDO frame) and
the application object or process image for each node. A concrete example of the content of a (RPDO) mapping object
and its interpretation are provided below. TPDO mapping works the same way.
PDO mapping does not create a direct data link between two nodes, but rather describes the correlation
between a data block in the PDO frame (i.e. network) and a source/target object (i.e. application data
point). This source/target object is also found in the object dictionary, just in a different range (≥ 0x2000)
than the PDO mapping objects.
Information about how PDO data is to be interpreted is not transferred along with the PDO data, but is
contained in the object dictionary.
MN CN
Application engineers Application engineers
(PLC processing) (PLC processing)
Process image Process image
(Optiona)l (Optiona)l
OD OD
Manufacturer- and Manufacturer- and
device-specific device-specific
objects objects
0x2000 0x2000
… …
PReq
TPDO RPDO
OD PRes MN OD
Mapping objects Mapping objects
... Copy Copy ...
POWERLINK
0x14xx according to according to 0x14xx
0x16xx mapping mapping 0x16xx
0x18xx 0x18xx
0x1Axx RPDO PRes TPDO 0x1Axx
… …
Figure 13: The content of mapping objects defines correlation between network and application
Steps to determine the mapping
The mapping structure for one transfer direction is determined by the following steps:

## Page 21

POWERLINK – TECHNOLOGY 21
1) Which nodes are communicating with each other?
=> TPDO, RPDO communication parameters of respective node
(Tab. 6 "Object range for PDO mapping objects." on page 19)
2) Which data should be transferred?
=> Source/target objects in node's respective manufacturer-/device-specific object range (Tab. 4 ""Object dictio-
nary structure" from EPSG DS 301 V1.x.x - Communication Profile Specification" on page 16)
3) Where is the object data in the PDO?
■ Node sending data:
=> TPDO mapping parameters (Tab. 6), source objects sent together in TPDO
■ Node receiving data:
=> RPDO mapping parameters (Tab. 6), RPDO data copied to target object
These steps represent a pattern that can be applied to the examples below with the same numbering.
3.3.5 Mapping example: Rotary encoder sends position data to the MN
1. Transfer direction: Rotary encoder (CN1) to network - Definition of TPDO mapping
1) Which nodes are communicating with each other?
=> Encoder (CN1) sends data to the MN. A CN can only ever
CN
send cyclic PDOs to all POWERLINK stations.
Application 1 Application
Software VarPosition [U16] 2) Which data should be transferred?
=> Position data of the encoder, i.e. data of the source ob-
ject 0x2001/0x43 with the size 16 bits (0x10).
OD
Manufacturer- and 3) Node sending data (encoder): Where should the source ob-
device-specific 0x2001/0x43[U16] ject data be located in the TPDO?
Object Objects
=> Right at the beginning of the TPDO with offset 0
Dictionary
Copy
OD according to
Mapping 0x1800/0x01
mapping
Objects 0x1A00/0x01
Payload [U16]
Network
header TPDO Frame CRC
PRes
Figure 14: TPDO mapping of an object
Questions 1 through 3 determine the content of the mapping objects for the rotary encoder. According to the POW-
ERLINK specification for CNs, the answer to Question 1 is always a target node ID of 0x00. The content of object
0x1A00/0x01 is composed of the answers to Questions 2 and 3. Objects 0x1800 and 0x1A00 form a pair that represents
a unique description of the TPDO - see Tab. 6 "Object range for PDO mapping objects." on page 19.
Result of question Object Contents Meaning
no.
1 0x1800/0x01 0x00 Target NodeID: Always 0 for
CN
2 and 3 0x1A00/0x01 0x0010000000432001 See following breakdown
Table 7: Mapping objects of rotary encoder (CN1)
0x0010 0x0000 0x00 0x43 0x2001
Length in bits Offset in bits Reserved Subindex source ob- Index source object
ject
Table 8: Breakdown of object 0x1A00/0x01

## Page 22

22 POWERLINK CONFIGURATION AND DIAGNOSTICS TM950
2. Transfer direction: Network to MN – Defining the RPDO mapping
Based on the PDO mapping of the transmitted frame defined above, we can now define the RPDO mapping on the
receiving end.
1) Which nodes are communicating with each other?
=> Encoder (CN1) sends data to the MN. In other words, the
MN
MN receives a PDO from the network with a source node ID
Application PLC application
Software VarPosition [U16] of 1.
2) Which data should be transferred?
=> Position data of the encoder; stored on the MN in target
OD
Manufacturer- and object 0x3200/0x05 with a size of 16 bits.
device-specific 0x3200/0x05[U16] 3) Node receiving data (MN): Where in the RPDO is the target
Object Objects
object data located?
Dictionary
=> Position data of the encoder is located in the PDO at the
OD Copy
position defined by the previous TPDO mapping, i.e. right at
Mapping 0x1400/0x01 according to
Objects 0x1.600/0x01 mapping the beginning of the RPDO with offset 0.
Payload [U16]
Network
header RPDO Frame CRC
PRes
Figure 15: RPDO mapping of an object
The answers to questions 1 through 3 determine the content of the mapping objects for the MN. A 0x16xx subobject
in the MN's object dictionary defines how it is to interpret the RPDO data - see Tab. 6 "Object range for PDO mapping
objects." on page 19. The content of this subobject depends on the structure of the RPDO.
The CN that the MN receives data from is determined by the associated object (object pair) of 0x1600: PDO commu-
nication parameter 0x1400. The content of object 0x1400/0x01 reflects NodeID 1 of the rotary encoder since this is
the node from which the PDO is received.
Result of question Object Contents Meaning
no.
1 0x1400/0x01 0x01 Source NodeID: 1 (encoder)
2 and 3 0x1600/0x01 0x0010000000053200 See following breakdown
Table 9: Mapping objects of rotary encoder (CN1)
0x0010 0x0000 0x00 0x05 0x3200
Length in bits Offset in bits Reserved Subindex target ob- Index target object
ject
Table 10: Breakdown of object 0x1600/0x01

## Page 23

POWERLINK – TECHNOLOGY 23
3.3.6 Mapping example: Rotary encoder sending position data directly to an X20
bus controller
1. Transfer direction: Rotary encoder (CN1) to network - Definition of TPDO mapping
1) Which nodes are communicating with each other?
=> Encoder (CN1) also sends data to the X20 bus controller
CN
Application 1 Application (CN2). A CN can only ever send cyclic PDOs to all POWERLINK
Software VarPosition [U16] VarSpeed[U32]
stations.
2) Which data should be transferred?
OD
Manufacturer- and => Velocity data of the encoder, i.e. data of the source ob-
device-specific 0x2001/0x43[U16] 0x2002/0x01[U32]
Object Objects ject 0x2002/0x01 with the size 32 bits (0x20).
Dictionary
OD Copy 3) Node sending data (encoder): Where should the source ob-
M Ob a j p e p c i t n s g 0 0 x x 1 1A 80 0 0 0 / / 0 0 x x 0 0 1 1 ac m c a o p rd p in in g g to acco C r o d p in y g to ject data be located in the TPDO?
0x1A00/0x02 mapping => Following the position data, i.e. offset is 16 bits.
Payload[U16] Payload[U32]
Network
header TPDO Frame CRC
PRes
Figure 16: TPDO mapping of two objects
Questions 1 through 3 determine the content of the mapping objects for the rotary encoder.
Result of question Object Contents Meaning
no.
1 0x1800/0x01 0x00 Target NodeID: Always 0 for
CN
- 0x1A00/0x01 0x0010000000432001 See breakdown above (Tab. 8)
2 and 3 0x1A00/0x02 0x0020001000012002 See following breakdown
Table 11: Mapping objects of rotary encoder (CN1)
0x0020 0x0010 0x00 0x01 0x2002
Length in bits Offset in bits Reserved Subindex source ob- Index source object
ject
Table 12: Breakdown of object 0x1A00/0x02
2. Transfer direction: Network to X20 bus controller - Definition of RPDO mapping
Based on the PDO mapping of the transmitted frame defined above, we can now define the RPDO mapping on the
receiving end.

## Page 24

24 POWERLINK CONFIGURATION AND DIAGNOSTICS TM950
1) Which nodes are communicating with each other?
=> Encoder (CN1) sends data to the X20 bus controller
CN
(CN2). In other words, CN2 receives a PDO from the network
Application 2 PLC application
Software VarSpeed [U32] with a source node ID of 1.
2) Which data should be transferred?
=> Velocity data of the encoder; stored on CN2 in target ob-
OD
Manufacturer- and ject 0x2000/0x01 with a size of 32 bits (0x20).
device-specific 0x2000/0x01[U32] 3) Receive node (X20 bus controller): Where is the data of the
Object Objects
target objects located in the RPDO?
Dictionary
=> Velocity data of the encoder is located in the PDO at the
OD Copy
Mapping 0x1401/0x01 according to position defined by the previous TPDO mapping, i.e. in the
Objects 0x1601/0x01 mapping RPDO after the position data with a 16-bit offset (0x10).
Payload[U16] Payload[U32]
Network
header RPDO Frame CRC
PRes
Figure 17: RPDO mapping of one object with cross-traffic
The answers to questions 1 through 3 determine the content of the mapping objects for the X20 bus controller (CN2).
A 0x16xx subobject in the X20 bus controller's object dictionary defines how it is to interpret the RPDO data - see Tab.
6 "Object range for PDO mapping objects." on page 19. The content of this subobject depends on the structure
of the RPDO.
The CN that the MN receives data from is determined by the associated object (object pair) of 0x1601: PDO communi-
cation parameter 0x1401. The content of object 0x1401/0x01 reflects NodeID 1 of the rotary encoder since this is the
node from which the PDO is received.
Result of question Object Contents Meaning
no.
1 0x1401/0x01 0x01 Source NodeID: 1 (encoder)
2 and 3 0x1601/0x01 0x0020001000012000 See following breakdown
Table 13: Mapping objects of rotary encoder (CN1)
0x0020 0x0010 0x00 0x01 0x2000
Length in bits Offset in bits Reserved Subindex target ob- Index target object
ject
Table 14: Breakdown of object 0x1601/0x01
Exercise: Mapping questions
1) Which range of the object dictionary is used for mapping?
2) How many objects are required to describe a mapping?
3) How many object dictionaries are required for a cyclic data link with two nodes?
4) How can a mapping describe an acyclic data link?
5) What link is defined by the mapping?
6) How can one determine the direction of a transmission (transmitting or receiving)?
7) What could be the reason that object 0x1401 is used in the second mapping example rather than object 0x1400?
8) Object 0x1800/0xF0 contains the following value: 0x0030002000FF4000. What does this value mean?

## Page 25

POWERLINK – TECHNOLOGY 25
Additional information about the mapping can be found in the EPSG DS 301 V1.x.x - Communication Pro-
file Specification / Search for objects from Tab. 6 "Object range for PDO mapping objects." on page 19
3.4 Network management (NMT)
The term "network management" (NMT) refers to all mechanisms used to query and modify the operating modes of
POWERLINK nodes. It also includes the identification of new nodes.
3.4.1 MN and CN state machines
The MN is responsible for network management (NMT). The MN controls the states of the CNs by querying status
information and issuing NMT commands. Each node follows one of the nodes described in the "EPSG DS 301 V1.x.x -
Communication Profile Specification25 (see Fig. 18 "MN state machine" and CN state machine), which describes possi-
ble state transitions. For each node there is a defined startup sequence that ends in the OPERATIONAL state.
The OPERATIONAL state indicates successful configuration and integration in the POWERLINK network.
Only when this state has been achieved does the node begin exchanging valid cyclic data.
25 " www.ethernet-powerlink.org

## Page 26

26 POWERLINK CONFIGURATION AND DIAGNOSTICS TM950
NMT_GS_
INITIALIZATION
NMT_MS (NMT_MT1)
NMT MN state machine auto
[NodeID==C_ADR_MN_DEF_NODE_ID]
NMT_MS_ (NMT_MT7) NMT_MS_
NOT_ACTIVE Timeout(SoC,SoA) BASIC_Ethernet
[NMT_StartUp_U32.bit13==1]
NMT_MS_EPL_MODE
(NMT_MT2)
Timeout(SoC,SoA)
[NMT_StartUp_U32.bit13==0]
NMT_MS_ (NMT_MT6)
PRE_OPERATIONAL_1 Error condition
(NMT_MT3)
auto
NMT_MS_
PRE_OPERATIONAL_2
(NMT_MT4)
auto
NMT_MS_
READY_TO_OPERATE
(NMT_MT5)
auto
NMT_MS_
OPERATIONAL
Type of communication
POWERLINK POWERLINK cycle
No communication Listen only Legacy Ethernet Reduced cycle (SoCPReqPRes
(IP and others)
(SoAASnd) SoAAsnd)
Figure 18: MN state machine

## Page 27

POWERLINK – TECHNOLOGY 27
NNMMTT__GGSS__
IINNIITTIIAALLIIZZAATTIIOONN
NNMMTT__CCSS
NNMMTT CCNN ssttaattee mmaacchhiinnee
((NNMMTT__CCTT11))
aauuttoo,,
[[NNooddeeIIDD !!==
((NNMMTT__CCTT33)) CC__AADDRR__MMNN__DDEEFF__NNOODDEE__IIDD]]
TTiimmeeoouutt
((SSooAA,,SSooCC,,PPRReeqq,,PPRReess))
NNMMTT__CCSS__
BBAASSIICC__EEtthheerrnneett NNMMTT__CCSS__
NNOOTT__AACCTTIIVVEE
((NNMMTT__CCTT1122))
SSooAA,,SSooCC,,PPRReeqq,,PPRReess
((NNMMTT__CCTT22))
SSooAA,,SSooCC
NNMMTT__CCSS__EEPPLL__MMOODDEE
NNMMTT__CCSS__ ((NNMMTT__CCTT1111))
PPRREE__OOPPEERRAATTIIOONNAALL__11 EErrrroorr ccoonnddiittiioonn
((NNMMTT__CCTT44))
((NNMMTT__CCTT55)) SSooCC
NNMMTT
EEnnaabblleeRReeaaddyyTTooOOppeerraattee
NNMMTT__CCSS__
PPRREE__OOPPEERRAATTIIOONNAALL__22
((NNMMTT__CCTT1100))
((NNMMTT__CCTT66)) NNMMTT
aauuttoo EEnntteerrPPrreeooppeerraattiioonnaall22
NNMMTT__CCSS__
RREEAADDYY__TTOO__OOPPEERRAATTEE
((NNMMTT__CCTT77)) ((NNMMTT__CCTT99))
NNMMTT NNMMTT
EEnntteerrPPrreeooppeerraattiioonnaall22
SSttaarrttNNooddee
NNMMTT__CCSS__
OOPPEERRAATTIIOONNAALL
((NNMMTT__CCTT88))
NNMMTT__CCSS__
NNMMTT
SSTTOOPPPPEEDD
SSttooppNNooddee
TTyyppee ooff ccoommmmuunniiccaattiioonn
LLeeggaaccyy EEtthheerrnneett PPOOWWEERRLLIINNKK PPOOWWEERRLLIINNKK ccyyccllee
NNoo ccoommmmuunniiccaattiioonn LLiisstteenn oonnllyy ((IIPP aanndd ootthheerrss)) RReedduucceedd ccyyccllee ((SSooCCPPRReeqqPPRReessSSooAA
((SSooAAAAssnndd)) AAssnndd))
Figure 19: CN state machine
3.4.2 Network startup
How startup of a POWERLINK network can be carried out step-by-step is shown as an example in Fig. 20 "Startup
procedure for a CN"26. This illustrates the state transitions of a CN (see Fig. 19 "CN state machine") as triggered by:
■ A received POWERLINK frame (SoA, SoC), i.e. status change to:
- NMT_CS_PREOPERATIONAL_1
- NMT_CS_PREOPERATIONAL_2
■ An NMT command directly addressed to the CN (contained in an ASnd frame)
- NMTEnableReadyToOperate: Change to NMT_CS_READY_TO_OPERATE
- NMTStartNode: Change to NMT_CS_OPERATIONAL
26 from EPSG DS 301 V1.x.x - Communication Profile Specification

## Page 28

28 POWERLINK CONFIGURATION AND DIAGNOSTICS TM950
MN CN
NMT_GS_POWERED
NMT_GS_INITIALIZING
NMT_GS_RESET_APPLICATION
NMT_GS_RESET_CONFIGURATION
NMT_MS_NOT_ACTIVE NMT_CS_NOT_ACTIVE
NMT_MS_PRE_OPERATIONAL_1 NMT_CS_BASIC_Ethernet _MODE
Start reduced cycle SoA
NMT_CS_PRE_OPERATIONAL_1
SoA(R. ServiceID:IdentRequest) SoA BOOT_STEP1
ASnd (ServiceID:IdentResponse)
ASnd
SDO communication [Client]
*
SDO communication [Server]
*
SoA(R. ServiceID:IdentRequest)
SoA
ASnd (ServiceID:IdentResponse)
ASnd
BOOT_STEP1E_OK
NMT_MS_PRE_OPERATIONAL_2
Start isochronous cycle SoC
NMT_CS_PRE_OPERATIONAL_2
Command:NMTEnableReadyToOperate ASnd BOOT_STEP2
NMT_CS_READY_TO_OPERATE
SoA(R. ServiceID:StatusRequest) SoA
ASnd (ServiceID:StatusResponse)
ASnd
BOOT_STEP2E_OK
NMT_MS_READY_TO_OPERATE
Send PReq PReq CHECK_COMMUNICATION
Send PRes
PRes
CHECK_COMMUNICATION E_OK
NMT_MS_OPERATIONAL
Command:NMTStartNode ASnd START
NMT_CS_OPERATIONAL
SoA(R. ServiceID:StatusRequest) SoA
ASnd ASnd (ServiceID:StatusResponse)
START_CN E_OK
Operational
*Operation is done several times
Figure 20: Startup procedure for a CN

## Page 29

POWERLINK – DIAGNOSTICS29

4POWERLINK – Diagnostics

4.1Diagnostics tools

Often it is no simple task to figure out why a machine won't start – or even worse, why it stopped working in the middle

of a process. When it comes to pinpointing the source of a malfunction as quickly as possible, there's no substitute for

powerful diagnostics tools and the knowledge of how to use them. These tools are presented in the following sections.

4.1.1Automation Studio

Automation Studio is the first place to go to identify malfunctions in a POWERLINK network. The status data points

and the logger provide a quick initial diagnosis.

ModuleOK

The I/O mapping contains a data point called "ModuleOK". For POWERLINK nodes (e.g. X20 bus controller) it is set27

to TRUE, as long as:

The CN is in the state OPERATIONAL

■

The  in the CN's PRes frame has the value "".RD flag1

■

This means that if the frame contains PDO data, then it is valid.

Figure 21: ModuleOK flag in I/O mapping

Module supervised

By default, the MN monitors the Automation Studio hardware configuration for any nodes added to the POWERLINK

network. This can be configured on the CN using the "Module Supervised" option. When "Module Supervised = On",28

the PLC will enter service mode if the respective CN fails.

Figure 22: CN's "Module Supervised" parameter in the Configuration View

Logger

Any network errors detected by the MN, such as problems with SDO communication, are also entered in the logger.

Figure 23: Logger reports a failed SDO write access to an object of CN1 (IF3.ST1).

27If "ModuleOK" is set to FALSE, then the node is either not in the OPERATIONAL state or its data is invalid – it does not necessarily mean that there is anything wrong with the

module itself.

28The same setting is available for individual X20 modules and has the same effect.

## Page 30

30POWERLINK CONFIGURATION AND DIAGNOSTICS TM950

Activating the fieldbus module view in the logger

POWERLINK-relevant errors are also displayed in the logger's fieldbus module view.■

This view must be activated separately.

Figure 24: Logger module window

To look up the meaning of the error codes, you can either select the error in the logger and press F1, or navigate to

the corresponding section of Automation Help:

Communication / POWERLINK / Diagnostics / Error numbers

Diagnostic data points

In addition to "ModuleOK" there are other diagnostic data points that can be helpful in locating errors.

The image on the left shows the diagnostic data

points available for an X20BC0083 bus controller:

For example, if the POWERLINK connection on■

the bus controller is lost (and reestablished),

the "EthPhyXLinkLoss" counter is incremented.

A short description of the data points can be■

found in Automation Studio in the correspond-

ing column of the I/O mapping window .

The diagnostic data points that monitor an■

Ethernet interface (hence the "Eth" at the be-

ginning of their name) are read asynchronously

via the SDO protocol.

Figure 25: Diagnostic data points for the X20BC0083 in the I/O mapping window

(monitor mode)

The Ethernet connection and corresponding cabling of POWERLINK stations can be monitored using the

predefined diagnostics data points for B&R devices – either from the MN or from Automation Studio.

This speeds up the process of pinpointing the source of errors at a hardware level.

Communication / POWERLINK / Diagnostics / Diagnostic data points

SDO library - AsEPL

Automation Studio provides a library that can be used, for example, to access a CN's objects using the SDO protocol.

This feature can also be used for diagnostic purposes by reading the contents of an object to check a device's status

or an error code. In order to do so, you need to know the index and subindex of the object.

Programming / Libraries / Communication / AsEPL

## Page 31

POWERLINK – DIAGNOSTICS31

Relevant diagnostics objects are:

0x1003: "ERR_History_ADOM":29

■

0x1C00 to 0x1C17: "DLL error handling objects"

■

Further information

For more information about the features described here, as well as additional diagnostic options, consult Automation

Help:

Diagnostics and service / I/O and network diagnostics / POWERLINK

Communication / POWERLINK / Diagnostics

The following training modules provide helpful information about diagnostics in general (independently of POWER-

LINK):

TM920 – Diagnostics and Service for End Users (independently of Automation Studio)

■

TM223 - Automation Studio diagnostics

■

Exercise: Create an Automation Studio project based on the hardware in the ETAL950 cube.

In the following exercise, a project will be created that will later be used to illustrate various concepts.

1)Create an Automation Studio project and name it "POWERLINK_ETA" (to be used later) with the following hard-

ware configuration:

Figure 26: Overview of hardware configuration for the "POWERLINK_ETA" project

X20 CPU (PLC)

■

X20BC0083 with NodeID 1 + X2X module (power supply, analog input)

■

X20BC0083 with Node ID 2 + X2X module (power supply, digital input, analog output)

■

"Module supervised = off" for all modules

■

2)Create a program named "CopyAItoAO" as shown in Tab. 18 "ANSI C program "CopyAItoAO"" on page 59 with

the corresponding variable declaration (local, same data type as individual analog channel, i.e. "INT").

3)Assign the "CopyAItoAO" program to the task class "Cyclic #1" .

29Not all nodes support all of the objects (i.e. optional objects) specified in "EPSG DS 301 V1.x.x - Communication Profile Specification".

## Page 32

32POWERLINK CONFIGURATION AND DIAGNOSTICS TM950

4)Map the variables of the "CopyAItoAO" program to the AI and AO modules as shown in Fig. 48 "CN to CN connec-

tion via the MN" on page 59.

5)Set up a direct CN-to-CN connection of CN1 data point "AnalogInput01" and CN2 data point "AnalogOutput01",

as shown in Fig. 49 "Direct CN to CN connection" on page 60. This connection is referred to as cross-traffic or

"crosslink" and doesn't require any user variables or programs on the PLC.

See references to Automation Help provided in section 5.4.1.3 "Direct cross-traffic" on page 58.

The ETA cube's control panel behaves as described in Tab. 19 "Description of POWERLINK ETA test system

functions" on page 61. Turning a potentiometer (P1, P2) changes the value of the 7-segment display

from 0 to 9.9. P1 modifies the left display, and P2 the right display.

4.1.2System Diagnostics Manager

Most of the diagnostics options pre-

sented in section 4.1.1 "Automation

Studio" are also available in System Di-

.agnostics Manager

This allows the majority of diagnostic

tasks to be performed independently

of Automation Studio.

Figure 27: System Diagnostics Manager view in browser window

Diagnostics and service / Diagnostics tools / System Diagnostics Manager

Exercise: Read objects using the SDO protocol.

Use the AsEPL library to perform asynchronous read access to an object on the CN. Control this access in Automation

Studio using the Watch window. Read access to the network occurs as shown in Fig. 12 "SDO server-client principle"

on page 18. In this case, the bus controller (CN) is the SDO server.

1)Open the Automation Studio project "POWERLINK_ETA".

2)Add a sample implementation of the AsEPL library.

## Page 33

POWERLINK – DIAGNOSTICS33

Figure 28: Add an AsEPL sample implementation in the Logical View.

3)Modify the eplCyclic.st file for the sample implementation. In the step EPL_READ_OE, change the following lines

(changes in red):

EPL_READ_OE:  (*state to read an object entry*)

epl.fub.EplSDORead_0.enable := 1;

(*enable the FUB*)

epl.fub.EplSDORead_0.pDevice := ADR('IF3');

(*Device Name of Powerlink interface*)

epl.fub.EplSDORead_0.node := 2;

(*Node Number of the Station to be read*)

epl.fub.EplSDORead_0.index := 16#1C0B;

(*Read Index 16#1C0B : CNLoss SoC*)

epl.fub.EplSDORead_0.subindex := 1;

(*Subindex 1 is the CumulativeCnt_U32*)

epl.fub.EplSDORead_0.pData := ADR(epl.para.readData);

(*Pointer to the variable for the read Data*)

epl.fub.EplSDORead_0.datalen := SIZEOF(epl.para.readData);

(*Length of the Data to be read*)

4)Transfer project to the PLC30

30Program Compact Flash card if necessary

## Page 34

34POWERLINK CONFIGURATION AND DIAGNOSTICS TM950

5)Open the Watch window for the object "eplCyclic.st".

6)Add the variable "epl".

7)Set "epl.cmd.readThresholdCnt" to "TRUE".

8)The value read from bus controller object 0x1C0B/0x01, which counts the number of lost SoC frames, appears31

in "epl.para.readData".

9)Cause a brief interruption of POWERLINK communication on Node 2 (i.e. disconnect/reconnect Ethernet cable).32

10)Read object 0x1C0B/0x01 on Node 2 again.

11)Open System Diagnostics Manager and check the status variables of the X20BC0083 module with NodeID 2:33

One of the error counters (EthPhyXLinkLoss) should match the exact number of interruptions.

Nevertheless, this counter is only conditionally comparable with the 0x1C0B/0x01 object, which doesn't count

the number of connection interruptions, but the number of failed SoC frames. Both counters are incremented in

the event of interruption, however.

The CN has an internal time monitor of received SoC frames. When a frame is lost, the value of the

0x1C0B/0x01 object is incremented. The MN can read this value using the SDO protocol (3.3.2 "Service

Data Objects (SDO)" on page 17).

Reading error counter objects like this (see "SDO library - AsEPL" on page 30) can also help identify

sporadic interruptions of the Ethernet connection and localize them using the node number.

B&R devices offer additional error counters, that are very helpful for diagnosing physical network con-34

nections (Ethernet cable and connections). These counters can be read automatically via SDO and viewed

in Automation Studio or System Diagnostics Manager.

4.1.4Overview of external diagnostic tools

Additional diagnostics options are available in the form of external diagnostic tools. Since POWERLINK is based on

standard Ethernet, all Ethernet-based network diagnostics tools can be used. For diagnosis of difficult network prob-

lems, the following solutions have proven effective:

(free - GPL): http://www.wireshark.org/35WireShark

■

B&R Analyzer for Wireshark TM: At www.br-automation.com search for "Wireshark

■

31see "EPSG DS 301 V1.x.x - Communication Profile Specification": Object "DLL_CNLossSoC_REC"

32Make sure that the node and its I/O modules are not supervised in Automation Studio (Module supervised = off).

33see Fig. 25 "Diagnostic data points for the X20BC0083 in the I/O mapping window (monitor mode)" on page 30

34Not all B&R devices provide these counters (see Automation Help under "Diagnostic data points").

35GPL: General Public License; changes to the software must also be released under the GPL.

## Page 35

POWERLINK – DIAGNOSTICS35

These programs allow you to log and analyze network traffic using a PC and a standard network card. Wireshark comes

with POWERLINK support. For additional analysis options, you can download a plug-in from the B&R website. Simply

search the B&R website for "Wireshark" and download the "B&R Analyzer for Wireshark TM" plug-in. This plug-in in-

cludes, among other things, logical analysis of network traffic with advanced statistics, as well as the possibility to

connect to hardware for accurate timestamps (X20ET8819).

In this manual, only the use of Wireshark will be discussed in detail.

4.1.5Wireshark

This section provides an introduction to working with the "Wireshark" tool for capturing and analyzing network com-

munication.

Requirements for network recordings

It is a good idea to install a second Ethernet card in the PC that will be performing the capture, since the

primary one is generally needed for other purposes, such as connecting to the PLC.

To record data traffic, an Ethernet port on the PC performing the capture is connected directly to any station in the

POWERLINK network. Ideally this is done using the same type of cable that is used for the POWERLINK network itself.36

The location on the network that the packets are captured has only a minor effect on the timing between the frames

(hub and tolerance times, etc.) and no effect on their order.

Figure 29: POWERLINK network analysis using a PC

All protocols (TCP/IP, Windows networks, etc.) should be deactivated in the control panel on the PC per-

forming the capture to prevent unwanted packets from disturbing the POWERLINK network.

The Npcap Packet driver (NPCAP) must remain enabled; otherwise, the interface will not ap-Exception:

pear in Wireshark.

Most network cards have one or more LEDs to indicate the connection status and network activity. As soon as the

connection to an active POWERLINK network is established, these LEDs should light up accordingly.

If the PC fails to connect to the POWERLINK network, check the advanced settings of the PC's network

card. A hub can also be connected between the PC and the network, since the physical connection is

already properly preconfigured and does not require any settings to be changed.

Recommended network card settings:

Half-duplex

■

100 Mbit/s transfer rate

■

Auto crossover (optional)

■

36Cable recommendations can be found in section 2.2 "POWERLINK" on page 6.

## Page 36

36POWERLINK CONFIGURATION AND DIAGNOSTICS TM950

Wireshark installation

The download link to the latest version and information about the Wireshark installation can be found at:

http://www.wireshark.org/

•

Starting a POWERLINK network recording

Once the Wireshark window has opened there are a number of ways to begin a capture. One of them is about:

1)Open "Interface list"

2)Select the network interface with a POWERLINK connection

3)Press "Start" button

Figure 30: Starting a network capture with Wireshark

This opens the "Capture Interfaces" window, showing you the activity on the POWERLINK network. If the packets are

sorted correctly ("Time" column in ascending order) then the recurring POWERLINK cycle can be observed.

Figure 31: POWERLINK cycle in Wireshark

The precision of the timestamp is determined by the interaction between the operating system and net-

work card on the PC performing the capture. The level of precision is generally enough to show the frames

in the correct order, but not enough to represent the exact time that they were received.

To get a reliable timestamp in high resolution you need to use special network analysis equipment. These

tools are introduced in section 4.1.6 "Hardware-supported network analysis".

Filter application

Filters can be used to gain a more focused view of network activity, showing only specific frames or connections. A

distinction is made between two types:

## Page 37

POWERLINK – DIAGNOSTICS37

Only affects which frames of a currently running or saved capture are displayed.Display filter:

■

Figure 32: Using a display filter in Wireshark

As shown above, the display filter can be entered in the field at the top with the aid of syntax. It is possible to use

logical operations (similar to C syntax). One the filter is applied, only packets that meet the requirements expressed

in the filter will be displayed. In the example above, these include all packets that are either sent from the node with

POWERLINK NodeID "1" (epl.src == 1) or sent to the node with NodeID "1" (epl.dest == 1).

Only packets that meet the criteria of the filter are captured. This can considerably reduce the sizeCapture filter:

■

of a saved capture. The capture filter uses a different syntax and should only be used by experts.

Exercise: Perform a network capture with Wireshark.

In this exercise, we will be performing a network capture during startup of a CN.

1)Open the Automation Studio project "POWERLINK_ETA" and transfer it to the PLC .3738

2)Connect the PC network card to any hub interface on the active POWERLINK network. The X20 bus controller also

has an internal hub (for two interfaces).

3)If you have problems connecting, consult section "Requirements for network recordings" on page 35 .

4)Verify the connection (LEDs on PC network card showing activity).

5)Switch off the CNs or the power supply to the POWERLINK nodes.

6)Starts the Wireshark capture – see "Starting a POWERLINK network recording" on page 36

7)Switching on the POWERLINK nodes

8)Stop capture after approx. 1 minute.

9)Save the capture in *.pcap format (e.g. under POWERLINK_bootup.pcap).

If the PC's network card is configured correctly, then Wireshark can be used to capture all POWERLINK

network traffic to be analyzed in more detail.

Exercise: Add a column to view cyclic payload data.

In this exercise, we will be adding another column to let us view cyclic POWERLINK payload data.

37Created in "Exercise: Create an Automation Studio project based on the hardware in the ETAL950 cube." on page 31

38Program CompactFlash card if necessary

## Page 38

38POWERLINK CONFIGURATION AND DIAGNOSTICS TM950

1)Apply the display filter shown in the image to the capture performed in the previous exercise. The filter ensures

the validity of the PDO data.

2)Select a PRes frame

3)In the detailed view, right-click on the "Data" field below and select "Apply as column".

4)The new column should appear in the network capture list above.

5)Rename the new column to "PRes payload" by right-clicking on the column and selecting "Edit Column Details".

6)The column can only display values if the current capture does not contain a (dynamic) SDO mapping configu-

ration. To do this, restart the recording as soon as the controlled node is in the OPERATIONAL state in order to

then observe the data during operation - i.e. no stored recordings as previously.

7)Manipulate a DI from a DI module with an attached switch and observe the manipulation in Automation Studio

(or SDM).

8)The same modification must also be visible in the CN2's "PRes payload" in Wireshark. Follow the continuous trace

in Wireshark by activating the automatic scroll function with "Go / Auto Scroll in Live Capture" or by clicking on

the button shown below. Section "Filter application" on page 36 may be helpful here. Recommended filter:

"epl.src == 2 && epl.pres"

#

POWERLINK transmits payload data in Little Endian format. Keep this in mind when interpreting SDO

and PDO payload data in the Wireshark capture.

## Page 39

POWERLINK – DIAGNOSTICS39

From the detailed view of a received frame in Wireshark, it is easy to create new columns to view addi-

tional information.

4.1.6Hardware-supported network analysis

Limitations of network capture using standard PC

Network captures obtained using a standard PC may be insufficient to effectively diagnose errors for the following

reasons:

The timestamps of captured frames are not precise enough.

■

The event that caused the error occurred outside of the capture time frame.

■

The capture file is too large to work with.

■

The error occurs sporadically

■

Network capture using B&R X20ET8819

The  network analysis tool overcomes these problems and provides a network capture with highly preciseX20ET8819

and reliable timestamps. Digital inputs and filters can also be used to trigger a capture – this is especially helpful for

sporadically occurring errors. This tool can be downloaded from the B&R website along with its features and docu-

mentation.

www.br-automation.com /

■

Products / Control systems / X20 system / X20 hub systems / X20ET8819

Figure 34: Network analysis

Figure 33: Hardware-supported network recordingtool X20ET8819

Network capture using netAnalyzer from Hilscher

A comparable tool called  is available from Hilscher:netAnalyzer

http://www.hilscher.com

■

These tools are introduced here to round off the topic of hardware-based network analysis. More detailed

documentation and additional software requirements must be obtained from the respective manufac-

turer.

4.2Startup behavior of a POWERLINK node

POWERLINK nodes follow a fixed scheme during startup, which - as described in 3.4 "Network management (NMT)"

on page 25 - is defined by their state machines. The green LED on a POWERLINK node indicates its current status. The

NodeID, or node number, gives the POWERLINK node a unique address. A misconfiguration has an effect right away.

For this reason, the NodeID is also a startup issue and is included in this section.

## Page 40

40POWERLINK CONFIGURATION AND DIAGNOSTICS TM950

4.2.1LED status indicators

ImageLEDColorDescription

S/EGreen/For a description, see the data sheet on the B&R website:1)

Red

www.br-automation.com / Products / Control systems / X20 sys-

L/A IFxGreen

tem / Bus controllers / X20BC0083 / Downloads / Documentation /

X20BC0083 data sheet

Table 15: X20BC0083 - Status LEDs

1)LED "Status/Error" is a green/red dual LED.

Every POWERLINK device uses the same LED status/error indicators and corresponding blink codes. A general descrip-

tion of the LEDs and their behavior is provided in EPSG DS 301 V1.x.x - Communication Profile Specification.

Exercise: Observe the CN's LED status indicators.

Use the LEDs to check the network status of a CN.

1)Open the data sheet for the X20BC0083 module (Tab. 15 "X20BC0083 - Status LEDs" on page 40)

2)Switch off the MN (PLC) and CN1.

3)For the following steps:

Observe the "S/E" and "L/A" LEDs on CN1.

■

Make a note of the state changes that occur based on the blink codes.

■

4)Switch on the MN and CN (at the same time).

5)Turn the CN off and then on again.

6)Disconnect the MN from the network and then reconnect it.

7)Switch off the MN or disconnect the CN from the network (same effect).

## Page 41

POWERLINK – DIAGNOSTICS 41
Observations for each step in the exercise:
3) With the CN is switched on, it enters BASIC_ETHERNET mode, because no POWERLINK communica-
tion is detected on the network. After that, status changes to PRE_OPERATIONAL_1 (very short sin-
gle flash), PRE_OPERATIONAL_2 (double flash) and finally OPERATIONAL39 (On) can be seen.
4) Similar behavior, except that the CN seems40 to switch straight to PRE_OPERATIONAL_2.
5) The red blinking LED indicates a faulty network connection. "L/A" indicates that there is a link, but
no activity.
6) "L/A" LED now shows neither link nor activity.
All in all, just a quick glance at the LED status indicators is needed to recognize which status the node is in.
These 3 cases should be distinguishable:
Light on = OPERATIONAL (ready for use)
•
Light blinking = PRE_OPERATIONAL_1, PRE_OPERATIONAL_2 or READY_TO_OPERATE (node is being
•
configured)
Flickering = BASIC_ETHERNET (no connection to MN)
•
If the LED status indicators continue to blink, resort to more in-depth diagnostics (e.g. with Wireshark).
Exercise: Observe the MN's LED status indicators.
Use the LEDs to check the network status of an MN.
1) Open the X20CP1586 data sheet (go to www.br-automation.com and search for "X20CP1586")
Note: The blink codes behave largely the same as the ones for the X20BC0083.
2) Switch off the PLC and disconnect from the POWERLINK network.
3) For the following steps:
■ Observe the "S/E" LED on the MN (PLC).
■ Make a note of the state changes that occur based on the blink codes.
4) Switch on MN again.
5) Compare your observations with the CN's "S/E" behavior in the previous exercise.
The "S/E" LED on the MN (PLC) behaves the same as the one on the CN. However, the MN starts up
independently, even without a network connection.
Different blink speeds indicate different POWERLINK states. With the exception of the BASIC_ETHERNET
state (rapid flickering), the following rule of thumb applies: The faster the blinking frequency, the higher
the state (LED on = OPERATIONAL). The state of the CN will always trail behind the state of the MN. In
other words, the CN can never be in a higher state than the MN.
Exercise: Interpreting a network recording
Examine the startup sequence of a POWERLINK CN using a network capture.
1) Open the *.pcap file from "Exercise: Perform a network capture with Wireshark." on page 37
2) Apply and combine (logical operations using ==, !=, &&, ||) various display filters.
39 The READY_TO_OPERATE status is too short to be observed
40 PRE_OPERATIONAL_1 is too short to be observed

## Page 42

42POWERLINK CONFIGURATION AND DIAGNOSTICS TM950

Meaning/ApplicationFilters

epl.asnd.svid

Startup of all nodes

•

NMT commands and SDO traffic

•

Start up a CN with the node number <CN NodeID>(epl.src == <CN NodeID> || epl.dest == <CN

NodeID>) && (epl.asnd.svid || epl.pres.stat

>= 0x6d)

SoA frame contains IdentRequestepl.soa.svid == 1

SoA frame contains StatusRequestepl.soa.svid == 2

ASnd frame contains IdentResponseepl.asnd.svid == 1

ASnd frame contains StatusResponseepl.asnd.svid == 2

ASnd frame contains StatusResponse with PRE_OPERATIONAL_1epl.asnd.sres.stat == 0x1d

ASnd frame contains SDOepl.asnd.svid == 5

PRes with status OPERATIONALepl.pres.stat == 0xfd

Table 16: Wireshark display filters for CN startup

3)Use the search function (keyboard shortcut "CTRL+F") to find the recording of any character string in the high-

lighted text (e.g. an NMTReset command).

4)Find another filter for the NMT command "NMTEnableReadyToOperate" in the POWERLINK specification.

The specification can be downloaded from the B&R website under:

www.ethernet-powerlink.org / Downloads / Technical Documents / EPSG DS 301 V1.x.x - Communication Profile

Specification

5)Compare the startup sequence to Fig. 20 "Startup procedure for a CN" on page 28. Use a suitable filter from Tab.

16 to do this.

The startup sequence of a POWERLINK CN can be observed using Wireshark. If you are familiar with the

steps of the startup sequence, this method is ideal for troubleshooting a node that fails to reach the

OPERATIONAL state.

## Page 43

POWERLINK – DIAGNOSTICS43

4.2.4POWERLINK node number

Every POWERLINK node has a node number, or , that uniquely identifies it in the POWERLINK segment. For easyNodeID

maintenance this number is generally set using two hexadecimal rotary switches.41

The node number is applied when the station is switched on. Any modifications made after startup will

be applied after the next startup procedure. The position of the node number switches must match the

configuration of the respective POWERLINK node in Automation Studio.

Communication / POWERLINK / AR configuration / Insert module

Communication / POWERLINK / Addressing

POWERLINK (CN) station number in B&R X20 system

Figure 35: Station number switches

Switch positionDescription

0x00Reserved, switch position not permitted42

0x01 - 0xEFNode number of the POWERLINK station. Operation as a controlled node (CN).

0xF0 - 0xFFReserved, switch position not permitted.

Table 17: X20BC0083 - Station number

NodeID 240 (0xF0) is a special case. This node number is reserved for the managing node (MN).

The range 1 to 239 (0x01 - 0xEF) is available for CNs.

Setting in Automation Studio

In Automation Studio, the NodeID is set as follows:

1)Right-click on bus controller shortcut menu and select

"Change node number".

2)Enter the new NodeID

3)The new NodeID appears in the "PLC Address" column

(red box)

Figure 36: Setting the NodeID in Automation Studio

41Even personnel without special programming skills should be able to quickly and easily exchange a POWERLINK station.

42Unless node supports dynamic node allocation (DNA)

## Page 44

44POWERLINK CONFIGURATION AND DIAGNOSTICS TM950

Automatic node number assignment

For stations that support it, the node number can also be assigned automatically during startup. This extended POW-

ERLINK feature is known as dynamic node allocation (DNA).

For modules with node number switches, DNA mode must be activated before it can be used. On B&R devices this is

done by setting NodeID = 0. A fixed NodeID must always be entered for the first station (from MN's perspective) in

a line ("Head of a line" node).

Communication / POWERLINK / General information / Dynamic node allocation (DNA)

IP addressing

If a POWERLINK node supports IP (Internet Protocol), then the last position in the IP address corresponds to the43

node number:

192.168.100.NodeID

■

A gateway is required in order to access this IP address from outside the POWERLINK network. The gateway remaps

the IP address using NAT (Network Address Translation).

Exercise: Incorrect node number setting

In this exercise we will be examining the effects of an incorrect node number setting using various diagnostic tools.

1)On CN1, the node number is set to "0x11"

2)Restart the CN (turn off/on) to apply the node number.

3)Observe the CN's LED status indicators.

4)Open System Diagnostics Manager (SDM). What clues can be gathered here (hardware view, diagnostic data

points, logger)?

5)Maybe open the logger for better readability, as well as the hardware comparison window in Automation Studio

(Online / Compare / Hardware).

6)Now set the node number of (formerly) CN1 to "2" and switch both CNs (formerly CN1 and CN2) off and back on.

7)Repeat the diagnostic steps and compare the results.

8)Perform a network capture with Wireshark and interpret the results of a startup phase.

The SDM overview provides an excellent starting point. Missing modules can quickly be identified in the

hardware view. Troubleshooting is particularly tricky if a node number is assigned twice in the POWER-

LINK network because of the irritating side-effects this causes. If you loose track, it may help to connect

devices to the network successively one at a time.

4.3Diagnostics strategy

A systematic approach is absolutely essential for fast error diagnosis. To do this, you can gradually move from a su-

perficial approach for diagnosis to more detailed examination. Depending on the complexity of the error, appropriate

tools are used. The following procedure is recommended:

Step 1: Check the state of the CNs / verify successful startup

Look at S/E and L/A LEDs

•

Remote diagnostics with System Diagnostics Manager (SDM): ModuleOk" data point

•

Remote diagnostics with Automation Studio: "ModuleOk" data point

•

43In a POWERLINK segment the IP address always starts with 192.168.100.xxx.

## Page 45

POWERLINK – DIAGNOSTICS 45
Step 2: Investigation of failure to start up or sporadic failure
Perform and evaluate network capture using
•
Wireshark
°
OmniPeek
°
X20ET8819 for timing measurements and if error is not obvious
°
Step 3: Further investigation of faulty nodes
Check network connections
•
Check diagnostic data points in SDM or Automation Studio (e.g. "EthPhy1LinkOk")
°
Read error counters (e.g. object 0x1C0B/0x01) using AsEPL library in Automation Studio
°
RJ45 plug contacts
°
Cable type
°
Bend radius
°
Cable drag chain suitability
°
Check POWERLINK settings
•
Timeouts
°
Cycle time
°
Minimum layout
•
Reproduce error with minimum number of nodes
°
Add CNs one at a time until error occurs
°
Most errors encountered in a POWERLINK network can be efficiently diagnosed using this method.

## Page 46

46POWERLINK CONFIGURATION AND DIAGNOSTICS TM950

5POWERLINK - Design and optimization

5.1Network design and timing performance

Early in the process of designing a POWERLINK application, it is important to clearly define and verify the requirements

to be achieved by the network that connects the electronic components to one another. The more complex the network

and the higher the performance requirements of the application, the more essential this preparation becomes.

Figure 37: Typical topology of a POWERLINK network

5.1.1Connection to other networks

The door to the "outside world": Gateways

In order to transfer data from a POWERLINK segment into other networks, such as an IP-based intranet or another

POWERLINK segment, you need to use a gateway or router (see Fig. 37 "Typical topology of a POWERLINK network"

on page 46).

Reasons a gateway is absolutely necessary:

Preserve the real-time capability of POWERLINK

■

Prevent overloading the 3rd party network with cyclic POWERLINK frames

■

A POWERLINK network is self-contained and is not permitted to be connected directly to another net-

work.

Routing and addressing mechanisms are used to interface between multiple IP-based networks. These are described

in Automation Help under:

Communication / Ethernet / Network settings

Communication / POWERLINK / FAQ

Basic Ethernet mode

A POWERLINK node can also function as a normal Ethernet node. When a POWERLINK CN does not detect any POWER-

LINK frames on the network, it switches automatically to BASIC_ETHERNET mode (see Fig. 19 "CN state machine" on

page 27). This is the only case where a POWERLINK node may be connected directly to a non-POWERLINK network. The

POWERLINK interface can be used as a normal Ethernet interface.

## Page 47

POWERLINK - DESIGN AND OPTIMIZATION47

5.1.2Cycle time (MN parameters)

The cycle time is defined as the time between two SoC (Start of Cyclic) frames. The SoC is sent by the MN to mark the

beginning of a new POWERLINK cycle and synchronize all the stations in the network (see Fig. 4 "POWERLINK cycle"

on page 10).

The parameter "Cycle time" is included in the MN settings in the configuration tool (e.g. Automation Studio). The cor-

responding object number in the OD is 0x1006.

The shortest possible cycle time depends on the following parameters:

Number of nodes

■

Volume of cyclic data per node

■

Operating modes of individual nodes (3.2.2 "Isochronous phase" on page 11)

■

Maximum size of asynchronous frames (5.1.5 "Asynchronous MTU size (MN parameter)" on page 52)44

■

Network latency (topology, hubs, cable length)

■

MNCN

PRes

Node response times (5.1.3 "PResMaxLatency", 5.1.4 "ASndMaxLatency")

■

These parameters can be used to calculate the cycle time. For additional information, see 5.1.2.1 "Esti-

mated cycle time requirements"

5.1.2.1Estimated cycle time requirements

If you at least know the number and type of nodes in the POWERLINK network, you can quickly estimate the fastest

possible cycle time by using the following simplified assumptions:

Figure 38: Simplified formula for calculating the POWERLINK cycle time

IPGInterpacket gap (960 ns); mandatory pause time between two Ethernet frames (according to IEEE 802.3)

Hub levelNumber of hubs between MN and observed CN (not counting interface on CN itself)

The diagram of the frames in a POWERLINK cycle is a helpful reference when performing this calculation (see Fig. 4

"POWERLINK cycle" on page 10).

44And number, in the case of Multi-ASnd

## Page 48

48POWERLINK CONFIGURATION AND DIAGNOSTICS TM950

Example calculation:

Network layout:

•

1 CN receives 30 bytes, sends 136 bytes and is connected via an extra hub.

Estimation of the shortest possible POWERLINK cycle time:

•

T = 40 µs + 6 µs (PReq) + (6 µs + 8 µs) (PRes) + 3 µs (hub) = 63 µs

Cycle Min

5.1.2.2POWERLINK cycle time in Automation Studio

In Automation Studio the cycle time is set on the  as shown in the following image:MN POWERLINK interface

The minimum cycle time value can be calculated for the topology in System Designer using the Hardware Configura-

function:tion Analyzer

Communication / POWERLINK / Calculations / Cycle time

More detailed information and explanations regarding the relationship between topology and timing can also be found

in Automation Help.

Communication / POWERLINK / Wiring

Settings in Automation Studio

Section 5.2 "Automation Studio settings for network timing" on page 53 describes where to find this parameter

in Automation Studio.

5.1.3PollResponse timeout (CN parameter)

PollResponse timeout parameter

When a station is polled with a PReq frame, it takes a certain amount of time for it to respond with a PRes frame.

The  defines the maximum length of time (in µs) that the MN will wait for a PRes frame fromPollResponse timeout

a polled station (see Fig. 40 "Topology example: The MN checks the response delay"). This parameter should be at

least as high as the PResMaxLatency (see below), and must take the network topology (cable delay and hub level) into

consideration. The managing node waits to send the next (PReq) frame until the PollResponse timeout has triggered

on the managing node (see Fig. 41 "PollResponse timeout") or the PRes frame has been received.

In the configuration tool (e.g. Automation Studio), the "PollResponse timeout" parameter is included in the controlled

node settings. The corresponding object number in the managing node's OD is 0x1F92.45

Settings in Automation Studio

Section 5.2 "Automation Studio settings for network timing" on page 53 describes where to find this parameter

in Automation Studio.

45Index: 0x1F92, Subindex: <NodeID of the CN>

## Page 49

POWERLINK - DESIGN AND OPTIMIZATION49

Figure 39: PollResponse latency

Figure 40: Topology example: The MN checks the response delay

Figure 41: PollResponse timeout

## Page 50

50 POWERLINK CONFIGURATION AND DIAGNOSTICS TM950
This parameter depends on the topology of the network (cable lengths and hubs/switches increase la-
tency).
A line topology ("daisy chain") also includes an integrated hub for each controlled node (see Fig. 37 "Typ-
ical topology of a POWERLINK network" on page 46).
PollResponse timeout minimum: PResMaxLatency
The maximum PReq-PRes response time of a node (measured directly at the node) is called PResMaxLatency. The
PResMaxLatency value does not take topology delays or frame sizes into account and cannot be set in Automation
Studio, but is predefined by the properties of the node.
The PResMaxLatency value can be found in the controlled node's data sheet or device description file (see 6.1 "XDD -
Device description file"). The corresponding object number in the CN's OD is 0x1F98/0x03.
5.1.4 Asynchronous timeout (MN parameter)
Asynchronous timeout parameter
When a station receives permission from the MN to send asynchronous data, there is a certain delay between when
the SoA is sent and when the ASnd from the respective station is received. The Asynchronous Timeout parameter
defines the maximum length of time (in µs) that the MN will wait to receive a frame after sending the SoA – similar to
the PollResponse timeout (CN parameter) - see also Fig. 40 "Topology example: The MN checks the response delay".
The managing node waits to send the next asynchronous frame (for multi-ASnd) until the Asynchronous timeout has
triggered on the managing node (see Fig. 43 "Asynchronous timeout") or the asynchronous frame has been received.
The SoC frame is not delayed in case of a timeout.
Since this parameter is defined once for the entire network, it should be set to at least the maximum possible ASnd-
MaxLatency value (see below) of all stations.
Generally, the ASndMaxLatency value is the same as the PResMaxLatency value for a given station. In
this case, the asynchronous timeout can be set to the highest PRes timeout value in the network (CN
parameter in Automation Studio), since this value also accounts for topology-related delays46.
The parameter "Asynchronous timeout" is included in the MN settings in the configuration tool (e.g. Automation Stu-
dio). The corresponding object number in the MN's OD is 0x1F8A/0x02 and is called "AsyncSlotTimeout" in the POW-
ERLINK specification.
Settings in Automation Studio
Section 5.2 "Automation Studio settings for network timing" on page 53 describes where to find this parameter
in Automation Studio.
46 The difference in frame size between PRes and ASnd is not accounted for.

## Page 51

POWERLINK - DESIGN AND OPTIMIZATION51

Figure 42: Asynchronous latency

Figure 43: Asynchronous timeout

This parameter depends on the topology of the network (cable lengths and hubs/switches increase la-

tency).

Minimum asynchronous timeout: ASndMaxLatency

The maximum SoA-ASnd response time for a node (measured directly on node) is referred to as .ASndMaxLatency

The ASndMaxLatency value does not account for topology-related delays or frame sizes and cannot be configured in

Automation Studio. It is predefined by the properties of the node.

The ASndMaxLatency value can be found in the controlled node's data sheet or device description file (see 6.1 "XDD -

Device description file"). The corresponding object number in the CN's OD is 0x1F98/0x06.

## Page 52

52POWERLINK CONFIGURATION AND DIAGNOSTICS TM950

5.1.5Asynchronous MTU size (MN parameter)

The asynchronous MTU size defines the maximum payload in bytes that an Ethernet frame in the POWERLINK asyn-47

chronous phase is permitted to have.

The parameter "Asynchronous MTU size" is included in the MN settings in the configuration tool (e.g. Automation

Studio). The corresponding object number in the OD (of the MN or CN) is 0x1F98/0x08 and is called "AsyncMTU" in

the POWERLINK specification.

This value influences the shortest possible cycle time.

If a network is to be operated with short cycle times, this parameter must not be set too high. The lowest possible

setting is 300 bytes. If, on the other hand, a larger asynchronous throughput is more important for a particular appli-

cation, then the parameter should be set higher (max. 1500 bytes).48

Figure 44: MTU size - Frame size affects cycle time

Settings in Automation Studio

Section 5.2 "Automation Studio settings for network timing" on page 53 describes where to find this parameter

in Automation Studio.

5.1.6Loss of SoC tolerance (MN parameter)

CNs monitor the precision of the SoC frames sent by the MN. If an SoC is not received within a defined tolerance, it

is considered lost. The parameter "Loss of SoC tolerance" must therefore be set depending on the performance of

the MN.

The parameter "Loss of SOC tolerance" is included in the MN settings in the configuration tool (e.g. Automation Studio).

The corresponding object number in the CN's OD is 0x1C14.

47Maximum Transmission Unit (Ethernet Payload)

48Some IP stacks (on the CN) only support a standard Ethernet MTU of 1500 bytes. In this case, the value must be adjusted accordingly.

## Page 53

POWERLINK - DESIGN AND OPTIMIZATION53

This parameter should be set to a higher value (more than 2x the cycle time) if the POWERLINK MN is

running on a non-real-time system (e.g. openPOWERLINK under Windows XP). On a real-time capable

system like Linux/RTPREEMPT, it can be lowered to improve error monitoring performance.

With the B&R system, this value is not relevant due to the high-performance MN architecture and there-

fore cannot be set.

There is no way to modify this parameter in Automation Studio.

5.2Automation Studio settings for network timing

The following images show where you can find the POWERLINK timing parameters in Automation Studio.

Further information can be found in Automation Help:

Select the parameter and press "F1".

Communication / POWERLINK / Calculations

MN parameters

Figure 45: MN parameters for network design in Automation Studio

## Page 54

54POWERLINK CONFIGURATION AND DIAGNOSTICS TM950

CN parameters

Figure 46: CN parameters for network design in Automation Studio

Exercise: Verify the POWERLINK configuration

Use the Hardware Configuration Analyzer in Automation Studio to determine the POWERLINK network timing charac-

teristics. The configuration will automatically be verified in the process.

1)Change the "Response timeout" parameter of CN2 to 2 µs

2)Verify the POWERLINK parameters with <Open> / <Hardware Configuration Analyzer>

3)Fix the incorrect "Response timeout" value. The new value must be higher than the calculated minimum value.

Use the Hardware Configuration Analyzer to detect and resolve configuration errors before the project

is transfered to the target system. In order to do this, the actual network topology must be represented

in accurately in System Designer.

Diagnosis and service / Diagnostic tool / Hardware Configuration Analyzer

Exercise: Check the utilization of the POWERLINK cycle at runtime

Use Automation Studio to calculate the utilization of the POWERLINK cycle while the system is running.

1)Observe the "CycleIdleTime" data point on the POWERLINK MN interface (monitor mode).

## Page 55

POWERLINK - DESIGN AND OPTIMIZATION55

2)Compare it with the configured cycle time on the POWERLINK MN.

3)Disconnect a random POWERLINK station from the network.

4)Observe the change in the "CycleIdleTime" data point.

The "CycleIdleTime" data point can be used to check the utilization of the POWERLINK network in SDM

or Automation Studio. This practical technique can be used in combination with the calculation of the

theoretical cycle time.

Communication / POWERLINK / Diagnostics / Diagnostic data points

Diagnostics and Service / I/O and network diagnostics / Recommendations for the application / Logging

diagnostic data points / POWERLINK data points

Exercise: Checking the POWERLINK cycle time

Use Wireshark to determine the actual cycle time.

1)Create a network trace of an active network (CN in OPERATIONAL state).

2)Calculate cycle time by right-clicking on an SoC frame / "Set Time Reference"

3)The "Time" column shows the time until the next SoC frame.

4)The format is set under "View / Time Display Format".

5)Change the POWERLINK cycle time in AS.

6)Compile the project and transfer it to the PLC

7)After the MN is restarted (automatically), check the cycle time with Wireshark.

## Page 56

56POWERLINK CONFIGURATION AND DIAGNOSTICS TM950

Wireshark can be used to measure timing. However, the displayed timestamps can only be relied on when

additional hardware support is used (X20ET8819). Be sure to account for network topology and hub level

in your measurements.

5.3Mapping in Automation Studio

In Automation Studio, the mapping is further abstracted and is configured in the background. All one sees is a bus

station's cyclically transferred object, which can be linked to a process variable. This is not a direct link – it requires

a number of intermediate steps.

MN to CN connection

When a POWERLINK node is added to an Automation Stu-

dio project, its cyclic data points are available in the I/O

mapping. For non-B&R devices, the channel name in the49

I/O mapping also includes the object number.

Figure 47: I/O mapping of a POWERLINK module

With POWERLINK, an I/O mapping channel corresponds to a specific (cyclically) transferred object that

exists on the selected node. All activated I/O mapping channels for CNs are summarized in the "process

image" on the MN.

The two process images (input and output process image) serve as the interface to the PLC application.

The cyclic objects of a POWERLINK node never actually exist in the object dictionary of another node (e.g. the MN), they

are simply represented in its local memory (see Fig. 13 "The content of mapping objects defines correlation between

network and application" on page 20).

Automation Studio / Automation Runtime handle all of the configurations required for an OPERATIONAL CN to transfer

cyclic objects to the MN application.

Configuration set in the background by the MN:

Connection of frame data to object: CN mapping

■

Connection of frame data to process image: MN mapping

■

Enable programming access (MN): Connection of  to process imageprocess variable

■

For additional information about the POWERLINK node configuration in Automation Studio, see the help documenta-

tion. The explanations in sections below apply to both 3rd-party devices and B&R devices.

Communication / POWERLINK / 3rd-party devices / Configuration of OEM devices

CN to CN - Direct cross-communication

In Automation Studio, a cross-communication link (CN to CN) can be established with little effort. The CNs are con-

figured via Automation Runtime. The procedure presented in the help documentation applies to other nodes besides

the described iCNs:

Communication / POWERLINK / Intelligent Controlled Node / Cross-communication between iCNs

In a data link with cross-communication, an (RPDO) frame error – caused by electromagnetic interference,

for example – can only be detected by the application. Such an application might take the form of a

cyclically incremented counter that is also contained in the PDO and is monitored by the receiving node.

With a normal PReq-PRes data link, a (repeated) frame error would cause a state change in the MN or CN.

49For CNs that support dynamic mapping, a cyclic channel must first be activated in the I/O configuration.

## Page 57

POWERLINK - DESIGN AND OPTIMIZATION57

5.4Optimization options in Automation Studio

A POWERLINK offers numerous parameters that can influence the performance of the network. This section will intro-

duce and describe the most important parameters.

5.4.1Cycle time and possible number of nodes

The following Automation Studio settings can be used to shorten the POWERLINK cycle time or increase the maximum

number of nodes (CNs participating in the full cycle) in a POWERLINK network.

5.4.1.1Multiplexing

Multiplexing prescaler (MN parameters)

This parameter defines the prescaler for multiplexing stations. It determines the number of POWERLINK cycles across

which the multiplexed nodes should be distributed. A value of 3 means that a multiplexing CN is addressed in every

third cycle (see "Multiplexing" on page 13). The number of multiplexed stations is divided by the multiplexing prescaler,

and the result is rounded up to the next whole number. This results in the number of multiplexed stations per cycle.

If you multiply the multiplexing prescaler by the POWERLINK cycle time, the result is the "cycle time of a multiplexing

node", i.e. the time it takes to update the data of a node for which multiplexing is activated.

This parameter is only available if the MN supports multiplexing.

Multiplexed station (CN parameters)

Multiplexed mode is activated for each CN individually as shown below.

Automation Studio automatically calculates the multiplexing slot in which a CN is addressed. The slot can also be

defined manually under the "Advanced multiplexing" settings.

This parameter is only available if the CN and MN both support this mode.

## Page 58

58POWERLINK CONFIGURATION AND DIAGNOSTICS TM950

Communication / POWERLINK / Multiplexed

5.4.1.2Poll response chaining

Chained station (CN parameters)

A CN can be operated in poll response chaining mode (see "Poll response chaining" on page 12) by activating the fol-

lowing parameter:

Poll response chaining is activated for each CN individually as shown below. This parameter is only available if the CN

and MN both support this mode.

For additional information about PRes chaining, see the POWERLINK specification and Automation Help.

Communication / POWERLINK / General information / Poll response chaining

5.4.1.3Direct cross-traffic

Another way to reduce the required POWERLINK cycle time is using direct cross-traffic (see "Direct cross-traffic" on

page 12) for CN-to-CN communication. An example of an application using direct cross-traffic is for motor control in

cases where a drive needs to read the position of a master axis to synchronize movement of coupled axes.

Programming / I/O configuration / I/O mapping / Cross-traffic

Programming / Editors / Configuration editors / I/O mapping / Select variable dialog box / Channels

Communication / POWERLINK / General information / Cyclic data exchange

CN to CN communication via the MN

Without direct cross-traffic, network stations can only communicate by way of the MN, which copies the data and

forwards it on to the other CN. The image below illustrates the steps involved in this process for an Automation Studio

project.

## Page 59

POWERLINK - DESIGN AND OPTIMIZATION59

Figure 48: CN to CN connection via the MN

void _CYCLIC CopyAItoAOCyclic(void)

{

Program code

AnalogOutput = AnalogInput;

}

Table 18: ANSI C program "CopyAItoAO"

Data path:

1)The first CN (bus controller with AI module) sends analog input data to the MN in the PRes frame.

2)The MN receives the data and makes it available to its application in a process image.

3)The process image is mapped to a process variable.

4)The contents of the process variable for the analog input module (AI) are copied to the process variable for the

analog output module (AO) in a task.

5)The AO process variable is mapped to the process image for the AO module.

6)The MN sends the AO data to the second CN (bus controller with AO module) in the PReq frame.

Direct CN to CN communication

This data path via the MN can be eliminated by having the CNs communicate with each other directly.

## Page 60

60POWERLINK CONFIGURATION AND DIAGNOSTICS TM950

Figure 49: Direct CN to CN connection

With the right configuration, a PRes frame can be received by any other CN in the network. This configuration of a

"monitoring" node generally takes place during startup, but can also occur later while the node is in the OPERATIONAL

state. The path taken by the data is reduced to a single step, as shown in the image above (Fig. 49 "Direct CN to CN

connection").

Data path:

1)The first CN (bus controller with AI module) sends analog input data to the MN in the PRes frame, and the second

CN (bus controller with AO module) receives the data.

The mapping of a "monitoring" CN needs to be configured (on the MN) so that it successfully links the

received PRes data to the AO module. A cyclic data connection to the MN does not exist here, however.

Exercise: Examine the data path for CN-to-CN communication.

In this exercise, we will send a CN's input data first to the MN, where it will be copied and forwarded on to the second

CN, as illustrated in Fig. 48 "CN to CN connection via the MN". Then we will compare this to the path for direct CN-to-

CN communication ( see Fig. 49 "Direct CN to CN connection").

## Page 61

POWERLINK - DESIGN AND OPTIMIZATION61

Figure 50: Panel elements and their data points in the AS sample project "POWERLINK_ETA"

Overview of panel functions with Automation Studio sample project "POWERLINK_ETA":

Potentiometers  and  on the control panel are mapped to the first two inputs of the  module.P1P2X20AI4622

■

The two  are mapped to the first two analog outputs of the module.7-segment displaysX20AO4622

■

The analog values of Potentiometer P1 and P2 appear on the left and right 7-segment displays, respectively.

■

Data is exchanged between potentiometer and display via POWERLINK, following a different data path for each

■

display.

Table 19: Description of POWERLINK ETA test system functions

1)Start Automation Studio project "POWERLINK_ETA" and transfer the project to the PLC50

2)Establish online connection with the PLC.

3)Start monitor mode

4)Force data point  in the I/O mapping of the X20AO4622 module using any valuesAnalogOutput02

5)Observe the seven-segment display

6)Compare with your own project and examine the path taken by the analog data using Fig. 48 "CN to CN connec-

tion via the MN" on page 59 as a reference.

7)Force data point  in the I/O mapping of the X20AO4622 module. Why is this not possible?AnalogOutput01

Note: The process variable entered in the I/O mapping is  of module X20AI4622, but is not a PLCAnalogInput01

variable like in the previous case. Compare with Fig. 49 "Direct CN to CN connection" on page 60 .

50. Program CompactFlash card

## Page 62

62POWERLINK CONFIGURATION AND DIAGNOSTICS TM950

8)Change the I/O mapping so that the lower seven-segment display also receives the data from potentiometer P1,

which is connected to module X20AI4622. Here, direct cross-traffic is to be used as the connection type:

9)Perform a network capture and save it as Direkt_Crosslink.pcap (for later use).

The POWERLINK connection between analog input 2 and analog output 2 goes through the MN and can

therefore be influenced by the MN. The (already digitalized) data take the following path:

X20AI4622 / X2X / Bus controller A / PRes / MN / PLC task / MN: PReq / Bus controller B / X2X /

■

X20AO4622

In contrast, the direct connection (with ) between AI1 and AO1 cannot be influenceddirect cross-traffic

by the MN since the data never passes through it. The data takes the following path:

X20AI4622 / X2X / Bus controller A / PRes / Bus controller B / X2X / X20AO4622

■

Exercise: Interrupt the data path on the MN.

With a data link between two CNs via the MN, interrupt the connection on the MN. This demonstrates the effects on

the receiving station and the functionality of cyclic data exchange.

1)Change the connection between  and  in the Automation Studio project "POWER-AnalogInput02AnalogOutput02

LINK_ETA" back to a CN-MN-CN path (no direct cross-traffic, see Fig. 48 "CN to CN connection via the MN").

2)Compile the project and transfer it to the PLC

3)Interrupt the CN-MN-CN data path by stopping the copy process on the MN (PLC). To do this, open the Watch

window for the object "" and stop cyclic execution.CopyAItoAO

4)Turn the potentiometer ""P2

The MN periodically sends the latest output data value to CN2. From POWERLINK's point of view, data

transfer works fine, but the output data is no longer updated by the PLC. In the Watch window you can

see that only the input data (AnalogInput) is being updated.

Exercise: Compare data paths and the effects of direct cross-traffic.

In order to complete this exercise, you must first work through section 4.1.5 "Wireshark".

Trace the path of cyclic data (PDOs) between two CNs in Wireshark. Compare two different approaches, each involving

different POWERLINK frames in the data link. Also observe the effects on the shortest possible cycle time.

## Page 63

POWERLINK - DESIGN AND OPTIMIZATION63

1)Perform a network capture (using Wireshark) and save it as Link_over_MN.pcap.

2)Compare the two captures Direkt_Crosslink.pcap (from previous exercise) and Link_over_MN.pcap.

Make a note of the differences

■

Calculate the theoretically required cycle time (using auxiliary formula in section 5.1.2.1 "Estimated cycle time

■

requirements" on page 47) for both network recordings

When there is direct cross-communication between two CNs without a "detour" through the MN, no data

is sent in the PReq frame to the "output data bus controller", since it gets all the data it needs straight

from the PRes frame of the "input data bus controller". Only the minimum size of the PReq frame needs

to be sent to the bus controller, since it contains no data.

Direct cross-traffic can therefore have a very positive effect on the overall system:

Node-to-node response time decreases

■

Theoretically required cycle time decreases

■

Increases max. number of CNs in the POWERLINK network

■

Reduces the load on the MN because it does not have to copy the data

■

The best use of direct cross-traffic is obtained in combination with 5.4.1.2 "Poll response chaining" or

with several cross-traffic connections with poll response chaining for the transmitting and Multiplexing

for the receiving nodes.

5.4.2Asynchronous bandwidth - Multiple asynchronous send

For a more detailed explanation of this POWERLINK extension, see section 3.2.3 "Asynchronous phase" on page 14.

Asynchronous slots per cycle (MN parameter)

This parameter can be used to increase the number of asynchronous frames that can be sent per cycle.

An additional asynchronous frame in the POWERLINK cycle can only be transmitted by a node that sup-

ports Multiple ASnd. The MN must also support the feature of course.

For more information, see the Automation Help.

Communication / POWERLINK / Multiple asynchronous send

## Page 64

64POWERLINK CONFIGURATION AND DIAGNOSTICS TM950

5.4.3Response time of the overall system

A PLC system normally includes three basic components:

Operating system (Automation Runtime)

■

Backplane bus / internal data bus (X2X)

■

Fieldbus (POWERLINK)

■

The way these components interact can be optimized for the needs of a particular application using various settings

in Automation Studio.

The following image shows the numbered individual steps that add up to the overall system response time (total

latency). For additional information, see Automation Help.

Communication / POWERLINK / Response time

For iCN: Communication / POWERLINK / Intelligent Controlled Node / Response time

Figure 51: Response time of the overall system

The overall system response time is comprised of:

Input latency (1-5)

■

Data processing / task duration (6)

■

Output latency (7-12)

■

Latency optimization

Automation Studio offers a number of ways to reduce the overall response time of a PLC system. These configuration

parameters and the effect they have on the system are described in Automation Help (see previous help documentation

link). The direction of data transfer (e.g. for input/output latency) is specified from the perspective of the PLC.

For these settings to have an effect, it is important that all cyclic processes are synchronized, i.e. that all

cycle times are whole number factors or multiples of each other.

Automation Runtime system timer must also be synchronized with the POWERLINK cycle.

Most of these configuration parameters are only available for B&R devices and are not included in the POWERLINK

specification, since their influence extends beyond the POWERLINK network.

Synchronization of the overall system

For a detailed description of synchronization between the individual system components and their hardware depen-

dencies, see Automation Help under:

Communication / POWERLINK / Synchronization

With SG4 target systems, for example, task classes are synchronized with the POWERLINK network using the settings

under <> →  →  → .CPUSettingsTimingSystem timer

## Page 65

POWERLINK - DESIGN AND OPTIMIZATION65

Figure 52: Synchronizing tasks with the POWERLINK network in SG4 systems

Optimization limits

Optimizing latency always comes at the cost of available CPU processing time and the size of frames that can be

transferred in a single bus cycle. In other words, the latency can only be optimized if a task is not very time-intensive

and the volume of data to be transferred in each cycle is not to large.

Where the limit lies here depends strongly on the application and can therefore only be determined in practical situ-

ations.

Generally, you have to choose between an optimal response time and stable synchronization of stations

with the POWERLINK cycle.

## Page 66

66POWERLINK CONFIGURATION AND DIAGNOSTICS TM950

6POWERLINK - Third-party device sup-

port

6.1XDD - Device description file

Every device with a POWERLINK interface must include a description of its char-

acteristics in the form of an XML device description () file.XDD

This file can be interpreted by a configuration tool in order to configure the indi-

vidual nodes and in turn the entire POWERLINK network (from the MN).

The XDD file describes the object dictionary and other network and device parameters of the respective

POWERLINK node.

An  file is an expanded form of the XDD file.XDC

This file contains deviating configuration values in addition to the default values of an object; otherwise,

there are no differences.

The XDD file is comparable to the configuration files for other bus systems:

GSD: PROFIBUS

■

EDS: (CANopen, DeviceNet)

■

The format is standardized. Sample files and additional information are available on the EPSG website (www.ether-

net-powerlink.org) and in Automation Help:

Communication / POWERLINK / 3rd-party devices

XDD checker

It is possible to have an XDD file checked for errors. You can upload a file to the EPSG website and receive an email

listing any errors that are found.

www.ethernet-powerlink.org / Service

■

Exercise: Add a rotary encoder to an Automation Studio project.

Add a 3rd-party device to an Automation Studio project and integrate it into the POWERLINK network.

1)Download the XDD file from the manufacturer's website.51

2)Connect the rotary encoder to the PLC and power supply.

3)Import the XDD file. See section of Automation Help referenced above. Here you will find a description of how to

import an XDD file into Automation Studio.

51Alternatively, your trainer may provide you with a file.

## Page 67

POWERLINK - THIRD-PARTY DEVICE SUPPORT67

4)Update the PLC configuration

5)Check the POWERLINK status of the rotary encoder using the S/E LED.

6)Rotate the encoder shaft and observe changes to the input data (monitor mode)

Automation Studio's XDD import feature makes it easy to integrate 3rd-party devices in the POWERLINK

network. The XDD file must be obtained from the manufacturer of the device.

Exercise: Questions regarding integration of a rotary encoder in a POWERLINK network

1)Why does not a cyclic data channel need to be activated in Automation Studio for the rotary encoder?

2)Why are the mapping objects (see Tab. 6 "Object range for PDO mapping objects." on page 19) not written during

startup (perform Wireshark trace)?

3)What does the write access to object 0x1010/0x01 with the content "Save" mean (according to the POWERLINK52

specification)?

4)What does the write access to object 0x1011/0x01 with the content "Load" mean (according to the POWERLINK

specification)?

6.2Device description file for modular devices

If a device is made of several different and replaceable modules that transfer their data via POWERLINK, it is possible to

describe each module with a separate file. The configuration tool imports more than just an XDD alone; it also imports

modular XDD files of all "child modules" as well as the "head station" as can be seen in Fig. 53 .

Modular XDDs for the X20 system can be found here:

www.br-automation.com / Downloads / Control and I/O systems / X20 system / Bus controller / X20BC0083 /

■

Fieldbus device description files

Figure 53: Example for the description of modular devices with XDD files

52In order to observe access to 0x1010 in the Wireshark trace, you may first have to use the AsEPL library in Automation Studio to write the object 0x1011/0x01 with the content

"load" and then turn the node off/on. This can happen as described in "Exercise: Read objects using the SDO protocol." on page 32, with SDO write access instead of SDO read

access.

## Page 68

68POWERLINK CONFIGURATION AND DIAGNOSTICS TM950

Modular XDD files are used to import modular 3rd-party devices to any configuration tool. The X20 system

can be used as a CN with an openPOWERLINK-based MN, for example.

6.3XDD application example: openPOWERLINK MN

As can be seen in the following image, individual XDD files of various CNs can be imported to a configuration tool

whereby an overall network configuration is created. In the example with an openPOWERLINK MN, the configuration

tool is called "openConfigurator". If a B&R MN were used, then the configuration tool would be Automation Studio.

The configuration is provided to the openPOWERLINK MN as a CDC file in order to write the CNs' parameters (objects)

using the SDO protocol and configure the network.

Figure 54: XDD application example: Configuring a POWERLINK network with an openPOWERLINK MN

6.4Examples of non-B&R MNs:

6.4.1openPOWERLINK

openPOWERLINK is an open source implementation of the POWERLINK protocol that can be used either as a managing

node (MN) or a controlled node (CN). This implementation of the POWERLINK protocol is also called a "protocol stack"

or simply "stack". The openPOWERLINK stack was developed as a generic implementation that can easily be ported to

various hardware and software platforms. The Intel PC architecture (x86) under GNU/Linux is used as the reference

platform for openPOWERLINK. Ported versions are available for Windows, VxWorks and FPGA platforms, among others.

openPOWERLINK can be downloaded here:

http://sourceforge.net/projects/openpowerlink/

■

6.4.2CODESYS

A POWERLINK MN can also be implemented using the CODESYS tool chain and specific PLC manufacturers.

http://www.be-services.net / Products / POWERLINK integration in CODESYS

■

## Page 69

SUMMARY69

7Summary

Although Automation Studio makes working with the POWERLINK protocol exceptionally easy, the full benefit of this

powerful technology can only be enjoyed with a more comprehensive understanding of its inner workings. That was

the motivation for creating this training module. The better you understand the processes involved, the faster you

will be able to set up and optimize a POWERLINK network, and the more effectively you will be able use the available

diagnostic tools.

When you begin troubleshooting, it is best to try the most basic diagnostic features first. These include the LEDs on

each POWERLINK node as well as the messages and data points that are provided in Automation Studio or System

Diagnostics Manager.

If these approaches are unsuccessful, the next course of action is a more detailed network capture. Further analysis

is then generally conducted by the device manufacturer on the basis of this capture. It is important for the actual

network behavior to be reflected with precise timing. This ensures that further analysis based on the capture will lead

to successful results.

## Page 70

70 POWERLINK CONFIGURATION AND DIAGNOSTICS TM950
8 Appendix – Solutions to the exercises
"Exercise: Mapping questions" on page 24
1) 0x1400 to 0x1AFF: Object area for PDO mapping objects
2) A pair of objects that always exist together, i.e. 2 objects: "Communication and mapping parameters"
3) Each node has its own object dictionary. Only 2 nodes are involved in direct cross-communication, so the answer
is: 2. With a CN-MN-CN connection, 3 nodes are involved – so 3 object dictionaries.
4) Not at all. The mapping only describes cyclic data transmission with PDOs.
5) The link between the network (PDO frame) and the source/target object in the application. (But not: The connec-
tion between two nodes!)
6) With the first two numbers of the mapping parameter: 0x18xx corresponds to send direction, 0x16xx receive di-
rection.
7) Objects 0x1400 and 0x1600 are already reserved for the PDO mapping of the PReq frame, since each CN must be
addressed by a PReq frame (or PRes MN frame in the case of PRes chaining).
8) Length: 48 bits, Offset: 32 bits Subindex: 0xFF, Index: 0x4000
"Exercise: Questions regarding integration of a rotary encoder in a POWERLINK network " on page 67
1) The rotary encoder uses a preconfigured mapping that cannot be modified, also called a "static mapping". A data
channel must only be activated in the I/O configuration if the mapping of the node can be configured dynamical-
ly, like the bus controller for example.
2) Reason: Also static mapping. The mapping is not modified; it is already provided.
3) See POWERLINK specification "EPSG DS 301 V1.x.x - Communication Profile Specification": Save the current val-
ues of all parameters that can be saved in nonvolatile memory for later use. This means these parameters will not
have to be reconfigured via SDO and instead can be loaded automatically at the next startup.
4) See POWERLINK specification "EPSG DS 301 V1.x.x - Communication Profile Specification": Load the default con-
figuration for parameters that can be saved in nonvolatile memory. This resets the node configuration. The de-
fault configuration is not activated until the node is restarted.

## Page 71

AUTOMATION ACADEMY71

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

## Page 72

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