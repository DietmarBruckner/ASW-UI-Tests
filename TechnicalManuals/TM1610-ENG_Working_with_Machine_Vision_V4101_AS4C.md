## Page 1

TM1610

Working with integrated

machine vision

## Page 2

2 WORKING WITH INTEGRATED MACHINE VISION TM1610
Requirements
Basic knowledge Basic technical understanding
TM210 - Working with Automation Studio
TM213 - Automation Runtime
TM223 – Automation Studio diagnostics
Optional: TM415 - Introduction to mapp Axis
Training modules Optional: TM416 - Programming mapp Axis
Automation Studio 4.12 and later
mapp Vision starting with 5.26.1
Software mapp View starting with 5.24.2
Optional: mapp Motion 5.26.1 and later
Current camera hardware upgrade VSS112R22.041P-000 V1.8.0.674 (or higher)
ETA light 210 (control technology)
ETA light 220.1610-1 (Smart Sensor with light bar)
Hardware ETA light 220.1610-2 (backlight)
B&R sticker cover
Optional: ETA light 410 (drive technology / servo technology,2)

## Page 3

TABLE OF CONTENTS 3
Table of contents
1 Introduction.........................................................................................................................................................4
1.1 Learning objectives..............................................................................................................................4
1.2 Symbols and safety notices...............................................................................................................5
1.3 B&R online courses...............................................................................................................................5
2 Hardware configuration and commissioning..............................................................................................6
2.1 Getting started.....................................................................................................................................9
2.2 Focus and exposure time.................................................................................................................10
2.3 Global variable structure for commands, parameters and the status....................................12
3 Machine vision basics......................................................................................................................................15
3.1 Light.......................................................................................................................................................15
3.2 Lens.......................................................................................................................................................19
3.3 Colors....................................................................................................................................................24
3.4 Filters....................................................................................................................................................25
3.5 Sensors.................................................................................................................................................30
4 Vision functions...............................................................................................................................................35
4.4 Visual editor.......................................................................................................................................40
4.5 Code Reader........................................................................................................................................41
4.6 Image export......................................................................................................................................49
4.7 Offline HMI application....................................................................................................................50
4.8 Pixel Counter......................................................................................................................................55
4.9 Blob.......................................................................................................................................................57
4.10 Matching............................................................................................................................................64
4.11 Measurement....................................................................................................................................68
4.12 OCR......................................................................................................................................................73
4.13 Deep OCR...........................................................................................................................................75
4.14 Smart Camera application - Alignment......................................................................................78
5 Synchronization................................................................................................................................................81
5.2 Light bar..............................................................................................................................................84
5.3 Synchronizing with mapp Axis........................................................................................................87
6 Summary............................................................................................................................................................89

## Page 4

4WORKING WITH INTEGRATED MACHINE VISION TM1610

1Introduction

Machine vision is the visual inspection of production processes used for optimization and automatic control.

B&R vision technology consists of the intelligent cameras (Smart Sensor and Smart Camera) and the intelligent lighting

(Smart Light, light bar and backlight). The heart of a machine vision solution is the intelligent camera, which also

represents the core of the seminar.

An existing machine can be quickly expanded through simple integration. Various use cases can be covered using

multiple integrated image processing functions. The advantages of the POWERLINK bus system allow a microsecond

accurate synchronization to be achieved.

This training module is divided into four chapters:

Hardware configuration and commissioning

•

Basic physical properties

•

Vision functions

•

Synchronization

•

In the first section, the camera is put into operation for the first time. In the basics, participants will find out about

the relationship between light, lenses and sensor technology for image acquisition. The configuration, practical uses

and application areas of the integrated image processing functions are covered in the "Vision functions" section. The

last section describes synchronization of the camera and the POWERLINK bus system. Multiple bus nodes can be syn-

chronized with the camera (e.g. a drive, external light/Smart Light, additional camera, etc.).

Figure 1: B&R vision technology

1.1Learning objectives

In this training module, the basics of using integrated machine vision are explained and various use cases are shown.

There are numerous exercises available to help increase understanding. Automation Help is an invaluable reference for

completing the exercises in this training module:

## Page 5

INTRODUCTION5

Participants will recognize the advantages of an integrated machine vision solution using B&R's vision portfolio.

•

Participants will be able to add the integrated machine vision hardware to a project and configure it.

•

Participants will understand the relationship between light, lenses, colors, filters and sensor technology and be

•

able to set up usable image acquisition.

Participants will be familiar with the mapp Vision HMI application tools and their functions.

•

Participants will gain an overview of the integrated image processing functions (vision functions) and be able to

•

use them.

Participants will be able to utilize the camera and the POWERLINK bus system for synchronization.

•

Participants will implement and test machine vision program integration.

•

1.2Symbols and safety notices

Unless otherwise specified, the symbol descriptions and safety notices listed in "TM210 - Working with Automation

Studio" apply.

1.3B&R online courses

The B&R online courses provide learning units for a wide range of topics. Because the cours-

es are interactive, they allow content to be learned effectively. To help you find the B&R online

courses you need more quickly, the various courses on the website are assigned to different

training categories and are based on the modular training concept.

B&R Online Courses (https://www.br-automation.com/de/academy/virtuelles-klassenzim-

mer/onlinekurse/)

## Page 6

6WORKING WITH INTEGRATED MACHINE VISION TM1610

2Hardware configuration and commis-

sioning

B&R machine vision portfolio

Intelligent camera

•

Smart Sensor

°

Smart Camera

°

Light bar

•

Backlight

•

Polarizing filter

•

Diffuser

•

Figure 2: Vision product family

Intelligent camera

The intelligent camera is an integrated vision system that can capture and process images independently of the con-

troller used. Image acquisition and processing take place on the camera without placing an additional load on the

controller. A processor and an operating system allow the camera to operate independently.

The camera is available in two : Smart Sensor and Smart Camera.scalable variants

The Smart Sensor can execute one of the available vision functions (VF). To be able to use a different vision function,

the software configuration of the Smart Sensor needs to be adjusted and the change transferred to the target system.

Smart Camera allows the simultaneous use of multiple vision functions at runtime on a single camera. Smart Cameras

therefore feature more powerful processors and memory systems. Thus, Smart Sensors and Smart Cameras also differ

on the hardware level, but not in terms of dimensions.

Another part of the camera configuration is the selection of the . 1.3 MP, 3.5 MP and 5.3 MP image sensorsimage sensors

are available. The number of pixels and the size of the sensor are decisive for image resolution. A higher number of

pixels leads to a higher image resolution.

The  (optics) can also be configured. A distinction is made here between in-lenses

tegrated lenses with a focal length of 4.6 to 25 mm and screw-on C-mount lenses,

offered by B&R with a focal length of 12 to 50 mm.

Figure 3: B&R lenses

The basic requirement for ensuring a high-quality image acquisition is proper

.lighting

In cameras with integrated optics, 4 LED segments are installed, each with 4 mul-

ticolor LEDs or one LED color and associated lenses. Depending on the configura-

tion of the camera, different lighting scenarios can be implemented. For example,

the colors red, blue, green, lime and white can be selected. The options ultraviolet

and infrared can also be configured.

The lighting elements and the optical system, consisting of sensor and lens, are

factory-calibrated. Set program parameters can thus be directly adopted in serial

production as well as in case of replacement.

Hybrid cables provide power and connect the camera to the POWERLINK bus sys-

tem.Figure 4: Intelligent camera

## Page 7

HARDWARE CONFIGURATION AND COMMISSIONING7

Light bars and ring lights

The light bar is an intelligent external lighting device featuring electronically ad-

justable beam angles ranging from -40° to +90°; it comes in groups of either four,

six or eight.

Beam angles can be adjusted to any requirement during operation. Depending on

the configuration of the light bar, different lighting scenarios can be implement-

ed. For example, the colors red, blue, green, lime and white can be selected. The

options ultraviolet and infrared can also be configured.

Light bars are factory-calibrated. Set program parameters can thus be directly

adopted in serial production as well as in case of replacement.

Hybrid cables provide power and connect light bars to the POWERLINK bus sys-

Figure 5: Light bar

tem.

Backlight

The backlight is also a part of the external lighting system family of products. It is

available in various size variants and comes in the same LED color options as the

camera and the bar lights.

Backlights are factory-calibrated. Set program parameters can thus be directly

adopted in serial production as well as in case of replacement.

POWERLINK communication and power supply are also handled using hybrid ca-

bles.

Figure 6: Backlight

## Page 8

8WORKING WITH INTEGRATED MACHINE VISION TM1610

mapp Vision HMI application

The mapp Vision HMI application is a web-based mapp View HMI application that is used to configure vision functions.

By adding the mapp Vision component to the project, the mapp Vision HMI application becomes available and can be

accessed via the URL  in the browser.[IP_ADRESSE_SPS]:81/index.html?visuid=mappVision

Figure 7: mapp Vision HMI application overview

mapp Vision HMI application overview

1Vision component selection

2Vision function selection

3Logger

4Light and focus variables

5User logout

6Image parameters

7Extended light variables

8Image

9Image acquisition

10Repetitive mode

11Image upload

12Zoom functions

## Page 9

HARDWARE CONFIGURATION AND COMMISSIONING 9
2.1 Getting started
Initial commissioning will be implemented with the aid of Automation Help. It contains a "Getting Started" section,
which provides step-by-step instructions on how to add a Smart Sensor.
Machine vision \ mapp Vision \ Getting started
Exercise: Adding a Smart Sensor
The goal of this exercise is to insert and configure the vision hardware into the project using Automation Help.
1) Start Automation Studio.
2) Open Automation Help.
Machine vision \ mapp Vision \ Getting started
3) GettingStartedSem1610 Open the Automation Studio project and follow the steps described.
The project is provided by the trainer.
Matching is used as a vision function in the "Getting started" tutorial.
We want to use the function Code Reader here instead.
4) Use the camera's status LEDs and the I/O mapping to determine whether the camera has been configured cor-
rectly
Hardware \ Machine vision hardware \ Smart Camera / Smart Sensor \ Control and connection ele-
ments \ Status LEDs \ POWERLINK V2 mode
Machine vision \ mapp Vision \ Programming \ Vision functions \ Image acquisition \ Register
overview

## Page 10

10WORKING WITH INTEGRATED MACHINE VISION TM1610

The camera has been successfully implemented in the project and is ready for image acquisitionGoal:

via the mapp View standard interface of the mapp Vision HMI application.

Figure 8: Result of the exercise: Smart Sensor added

2.2Focus and exposure time

The focus and the exposure times are parameters that can either be selected manually or determined automatically

by the system. Depending on the exposure time, the color of the lighting and the focus, the image quality results for

the subsequent processing of the image contents with vision functions.

Parameter determination can be performed via the input "SearchAcquisionSettings" using I/O mapping in Automation

Studio or, as in this case, via the mapp Vision HMI application.

Exercise: Performing the first image acquisition

The goal of this exercise is to automatically determine parameters for the focus and the exposure time and to display

influences on the captured image by changing the exposure color.

1)Place the object in front of the camera.

2)Set the variables FlashColor01 to  and FlashSegment01 to  in the section "Light and focus vari-[1] red[15] all - 1111

ables" of the mapp Vision HMI application.

3)Set SearchAquisitionSettings to "TRUE" in the section "Extended light variables".

## Page 11

HARDWARE CONFIGURATION AND COMMISSIONING11

4)Perform image acquisition.

Once parameter determination is completed, values were entered at the variables "Read focus" and "ReadExpo-

sureTime".

5)Set the variable SearchAquisitionSettings to "FALSE".

6)Enter the values of the variables Read Focus and Read ExposureTime into the variables SetFocus and Exposure-

Time01.

7)Execute the image acquisition with the determined values.

## Page 12

12WORKING WITH INTEGRATED MACHINE VISION TM1610

Due to the automatically determined parameters, the final image should be properly illuminatedResult:

and focused.

Furthermore, it is clear that the contrast of the objects strongly depends on the lighting color. Due to

the red lighting, the red colored letters are hardly recognizable.

Figure 9: Result of the exercise: Performing the first image

acquisition

2.3Global variable structure for commands, parameters and the status

For the operation of the camera from a program and to be able to save image acquisition parameters permanently,

it is useful to create structures. In this case, structures for the commands to the camera, the general parameters and

the status of the camera are created and made available as a global variable.

This variable structure is also used in the sample solutions in this training module.

Exercise: Creating a gVisionCtrl variable

The goal of this exercise is to create a variable structure for operating the camera from the program and to link it in

the I/O mapping of the camera.

The B&R sticker is used for the implementation.

1)Create a global structure with substructures for commands, parameters and the status in the file "Global.typ".

## Page 13

HARDWARE CONFIGURATION AND COMMISSIONING13

2)Create a global variable of the created type in the file "Global.var".

3)Link the created parameters for SetFocus, ExposureTime01, FlashColor01, FlashSegment01, ImageAcquisition-

Ready, ImageProcessingActive and ImageAcquisition in the I/O mapping of the camera.

4)Create a new Structured Text program and assign it to task class #4.

5)Set lighting color, lighting segment, exposure time and focus in the program within the INIT routine. Use the pa-

rameters previously determined in the mapp Vision HMI application. The parameters may vary depending on the

design.

6)Transfer the project to the target system.

7)Execute an image acquisition in the mapp Vision HMI application and check whether the parameters from the

INIT routine have been transferred to the mapp Vision HMI application.

## Page 14

14WORKING WITH INTEGRATED MACHINE VISION TM1610

Due to the defined values in the INIT routine of the created program, the parameters are retainedResult:

even after a restart of the controller. Image acquisition can be executed directly after connecting to the

mapp Vision HMI application.

Global.typ in text view:

TYPE

gVisionCtrlType : STRUCT

Cmd : gVisionCtrlCmdType;

Parameters : gVisionCtrlParType;

Status : gVisionCtrlStatusType;

END_STRUCT;

gVisionCtrlCmdType : STRUCT

AcquireImage : BOOL;

SearchAcquisitionSettings : BOOL;

Enable : BOOL;

END_STRUCT;

gVisionCtrlParType : STRUCT

FlashColor : USINT;

FlashSegment : USINT;

SetFocus : UINT;

ExposureTime : UDINT;

NumSearchMax : USINT;

Nettime : DINT;

DelayNettime : DINT;

END_STRUCT;

gVisionCtrlStatusType : STRUCT

CameraReady : BOOL;

ImageProcessingActive : BOOL;

END_STRUCT;

END_TYPE

Global.var in text view:

VAR

gVisionCtrl : gVisionCtrlType;

END_VAR

Figure 10: mapp Vision HMI application

## Page 15

MACHINE VISION BASICS15

3Machine vision basics

The basics are divided into the following subsections:

3.1 "Light" on page 15

•

3.2 "Lens" on page 19

•

3.3 "Colors" on page 24

•

3.4 "Filters" on page 25

•

3.5 "Sensors" on page 30

•

Each subsection starts with an explanation of the terms. In addition, correlations to practical applications are ex-

plained and concluded with exercises.

3.1Light

The lighting influences the contrast of images and the recognizability of contours and surfaces. These image parame-

ters are particularly important for processing in a vision application.

Consistent lighting conditions are critical in order to obtain reproducible results, for example when scanning several

images in succession.

3.1.1Illuminance vs. luminance

is the amount of light that shinesIlluminance [lu/m², lx]

on a surface. The luminous intensity is independent of

the surface finish.

corresponds to the reflecting beamsLuminance[cd/m²]

of light and their beam angle. In simple terms, this de-

scribes the density of beams reflected from an object

and captured by the camera/eye. Therefore, there is a

correlation between the illuminance and the reflecting lu-

minance.

Figure 11: Illuminance vs. luminance

The  indicates the luminous output of aluminous flux

light source per unit of time. The luminous flux indicates

how much light a light source emits in all directions. It in-

dicates the total light output and is measured in Lumen

(lm).

The  indicates the luminous flux in re-luminous intensity

lation to the solid angle. The luminous intensity is there-

fore the part of the luminous flux that shines in a specific

direction. It is measured in Candela (cd).

For additional information about terminology, see:

https://www.licht.de/de/grundlagen/lichtlexikon

## Page 16

16WORKING WITH INTEGRATED MACHINE VISION TM1610

3.1.2Photometry vs. radiometry

is the detection of objects (reflecting is the detection of objects (reflecting beamsPhotometryRadiometry

beams of light) from the visible spectrum/wavelengthsof light) from the visible (e.g. red, blue, green) and invisi-

of light. Simplified, this describes everything that a per-ble (ultraviolet and infrared) spectrum of light.

son can see with the naked eye.

Figure 12: Photometry

Figure 13: Radiometry

3.1.3Soft light vs. hard light

comes from a large light source and/or a close comes from a small light source and/or a dis-Soft lightHard light

light source.tant light source.

Figure 15: Hard light

Figure 14: Soft light

3.1.4Reflective light vs. Diffused light

can be detected due to strong and/or is homogeneous light. The object is uni-Reflected lightDiffused light

inhomogeneous distribution of the beams of light. Theformly illuminated everywhere. There are fewer shadows

light on the object is unevenly distributed. Some parts offrom the object. Homogeneous distribution of the light

the object are overly lit, while other parts are hardly visi-is achieved through additional material (diffuser) be-

ble.tween the light source and the object. In nature, the sun

acts as a light source and the clouds as diffusers.

## Page 17

MACHINE VISION BASICS17

Figure 17: Diffused light

Figure 16: Reflected light

3.1.5Bright-field illumination vs. dark-field illumination

is from an angle greater than illuminates the object lateral-Bright-field illuminationDark-field illumination

45° from the perspective of the camera. Examples ofly/flatly in order to check the surface condition. Scratch-

practical applications are pressure detection with a lightes, notches, engravings, etc. can therefore be detect-

bar or illumination of transparent material with a back-ed. Transparent objects are illuminated from behind at a

light.specific angle.

Figure 18: Bright-field illumination

Figure 19: Dark-field illumination

3.1.6Light segments

are integrated with lenses andLight segments (FlashSegment)

LEDs in machine vision products. Depending on the configuration

of the products, different lighting can be implemented. For exam-

ple, the colors red, blue and white can be selected. The options ul-

traviolet and infrared can also be configured.

In the I/O mapping, the segments are addressed using "FlashSeg-

ment".

Figure 20: LED segments

## Page 18

18WORKING WITH INTEGRATED MACHINE VISION TM1610

3.1.7Exposure time

The corresponds to the time during which the lens allows light to reach the sensor. Camera illumina-exposure time

tion is active during the same time if it is used. Exposure time is addressed using "ExposureTime" and specified in

microseconds in the I/O mapping for the camera.

Figure 21: I/O mapping with "ExposueTime01" and "FlashSegment01"

Exercise: Changing the exposure time

The goal of this exercise is to see how the exposure time affects the image acquisition and the captured image.

To be able to compare the differences, a screenshot of each image is generated.

1)The image acquisition is the same as in the first exercise.

2)Now take a picture with half of the exposure time.

3)Now take a picture with the double exposure time.

4)Finally, perform an image acquisition with the lighting switched off (FlashSegment = "[0] off"), and increase the

exposure time until the object is clearly visible again.

5)Now compare the two screenshots.

6)Optional: Use external light as a disruptive factor and observe effects. A flashlight, for example, can be used for

this purpose. → Create and save a screenshot of the image acquisition.

Depending on the lighting time, the captured objects are displayed brighter or darker.Result:

The lighting time affects the contrast of the captured image.

With a long lighting time, it is possible to take a good quality picture even without integrated lighting.

BUT: It is extremely sensitive to clutter (flashlight). This is why it always makes sense to use the integrated

lighting.

Figure 24:  Double exposure timeFigure 22: Determined exposure timeFigure 23: Half exposure time

## Page 19

MACHINE VISION BASICS19

3.2Lens

The lens (optics) is for the camera like the eyes for a human being. A lens collects

and projects light beams onto the sensor. By modifying the lens, the size and

sharpness of an image can be influenced.

In this chapter, various terms relevant to the topic of lenses will be discussed.

Figure 25: The lens as

the eye of the machine

3.2.1Aperture

The or opening widthis the opening in the lens that allows the light beams to reach the sensor. In optics,aperture, ,

the aperture is specified as an f-number in relation to the focal length (for example, f/1.4).

The f-number indicates how many times the size of the aperture would fit in the focal length, i.e. the distance from

the lens to the sensor.

The larger the f-number, the smaller the aperture and the greater the depth of field.

Figure 26: Aperture table for 25 mm focal length

Figure 27: Aperture

## Page 20

20WORKING WITH INTEGRATED MACHINE VISION TM1610

3.2.2Depth of field

The defines the range of distance in which an object is in sharp focus.depth of field

Depending on the aperture and the focal length used, the range in which objects are displayed sharply differs. The

distance at which this focus area is located again depends on the focal length and the focusing.

If the focus is on close objects, the focus area is smaller than when focusing on more distant objects. The lens can be

moved back and forth using the focus setting.

The focus can be set in the I/O mapping in "SetFocus".

Figure 28: Depth of field

3.2.3Focal length

The is the distance between the main plane of the lens and the point of focus. Together with the formatfocal length

of the sensor, it affects the image size ("Field of view") and is specified in millimeters.

By adjusting the focal length, the working distance, i.e. the distance between the object and the camera, can be in-

fluenced.

At the same time, changing the focal length affects the possible depth of field within the working distance.

As the focal length increases, the working distance increases and the image size decreases.

The focal length of the camera/lens is fixed and cannot be changed.

When adjusting the focus, the lens in the camera is moved mechanically.

Figure 30: Focal lengthFigure 29: Focal length

## Page 21

MACHINE VISION BASICS21

3.2.4Working distance

The  is the distance between the cam-working distance

era and the object.

The working distance can be adapted to the application

by changing the focal length. This also affects the depth

of field and the image size ("Field of view").

Figure 31: Working distance

3.2.5Minimum object distance

The defines the minimum possible distance between the lens and the object and is specifiedminimum object distance

by the optics used.

Falling below the minimum object distance results in loss of sharpness of the image.

The working distance can be adapted to the corresponding task by changing the focal length.

The data for these values of the B&R vision camera can be found in the data sheet.

3.2.6Vignetting

is a shading of the image towards the image edges. This shading is caused, for example, by the reducedVignetting

amount of light from light beams hitting the aperture at an angle, especially with longer lenses.

Since lenses are round, but image sensors are usually square, the effect is usually most noticeable in the corners of

the image.

Depending on the lens, aperture and lighting used, the effect of vignetting can be adjusted to the requirements.

In machine vision products, lenses and sensors are optimally matched and factory calibrated, thus vignetting is a minor

issue and vignetting correction can be enabled.

## Page 22

22WORKING WITH INTEGRATED MACHINE VISION TM1610

Figure 32: Vignetting

3.2.7Optical distortion

is a geometric aberration in which an object can be optically stretched or curved on its horizontalOptical distortion

and vertical axis.

This effect is strongly dependent on the positioning of the aperture, the distance and the shape of the lens.

## Page 23

MACHINE VISION BASICS23

3.2.8Chromatic aberration

occurs because light is refracted differently at the lens edges depending on the wavelength.Chromatic aberration

The splitting of the light spectrum by a prism illustrates this effect.

Exercise: Changing the focus

The goal of this exercise is to illustrate the effects of the focus on the captured image. For this purpose, objects are

photographed from different distances and the focus is adjusted manually.

1)First, select an object. A water bottle, for example, or a handmade name tag can be used for this.

2)Place the object approx. 10 cm in front of the camera and adjust the focus manually.

3)Then place the object approx. 20 cm in front of the camera and readjust the focus.

The point at which the lens focuses and brings the captured object into focus at image acquisitionResult:

depends on the distance to the object. Adjusting the focus manually might take a few attempts, but is

possible in principle.

In addition to the sharpness of the image, it becomes apparent that changing the distance also changes

the dimensions of the object, thus the object as such is perceived differently.

Figure 34: Result with long object distanceFigure 33: Result with short object distance

## Page 24

24WORKING WITH INTEGRATED MACHINE VISION TM1610

3.3Colors

Colors are divided into the visible and invisible spectrum. For example, ultraviolet and infrared are in the invisible spec-

trum.

Figure 35: Color spectrum

For each color of light, the photons move at a certain wavelength. The amount of energy of the photons defines the

color of the light. Photons of shorter wavelengths have higher amounts of energy. "Ordinary" white light is a mixture

of multiple composite colors. White light can be split into its components using a prism.

3.3.1Complementary colors

Complementary colors, or opposite colors, are located opposite each other

in the color wheel of the applied color model and form a very high contrast

to each other.

This property can be used, for example, when illuminating objects.

If the object is illuminated with a complementary color, it is displayed with

higher contrast.

Conversely, an object with the same lighting color can be deliberately hidden.

Figure 36: Color wheel

Red and green are complementary colors. If a green object is photographed, red lighting colorExample:

produces an image with very high contrast.

The lighting color can be set in the I/O mapping for the camera in "FlashColor01".

## Page 25

MACHINE VISION BASICS25

Exercise: Changing the color

The goal of this exercise is to influence the contrast of the differently displayed elements on the object "B&R sticker"

by changing the lighting color. To better compare the results, the images should be loaded onto the computer and

compared.

1)Perform the first image acquisition with red lighting color. If the exposure time or the focus are off, determine

these parameters using SearchAcquisitionSettings and save them in structure gVisionCtrl.

2)Change LED color to blue and adjust the exposure time to achieve a similar image brightness.

3)Change LED color to white and adjust the exposure time to achieve a similar image brightness.

4)Change LED color to infrared and adjust the exposure time to achieve a similar image brightness.

The camera's integrated infrared filter is enabled to minimize interference from ambient light. In this

exercise, the filter is enabled in the mapp Vision HMI using parameter .IRFilter = TRUE

5)Compare the images.

All images should have a similar brightness by adjusting the exposure time to the lighting color.Result:

Differently colored parts of text are displayed weaker or stronger in contrast depending on the lighting

color.

Lighting color plays a major role in the detection of objects.

Figure 37: LED color: RedFigure 38: LED color: BlueFigure 39: LED color: WhiteFigure 40: LED color: Infrared

3.4Filters

Filters make it possible to influence image acquisition as a kind of image pre-processing. Depending on the type of

filter used, different sources of interference can be eliminated or elements can be brought to the foreground.

There are physical filters and software filters:

Physical filters

Polarizing filter

•

Infrared filter (IR filter)

•

Diffuser

•

Programmable filters

Sobel filter

•

User filter

•

## Page 26

26WORKING WITH INTEGRATED MACHINE VISION TM1610

3.4.1Polarizing filter

A  has the property of absorbing complementary polarized light.polarizing filter

The further light beams move away from the polarization plane, the more they get absorbed.

Thus, if the polarization plane is oriented vertically, light striking the filter horizontally (90°) is absorbed to the maxi-

mum.

Polarizing filters are used in image acquisition to avoid reflections and enhance colors and contrasts, among other

things.

Since light, deviating from the polarization plane, is partially or completely absorbed, illuminance is lost. The illumi-

nance must then be compensated for by a longer exposure time.

The front glass of the camera can be equipped with a polarizing filter at the factory.

Figure 41: Principle of polarizing filter1

Figure 42: Without a polarizing filterFigure 43: With a polarizing filter

Machine vision \ Basics of image processing \ Light \ Types of illumination \ Polarizing filters

3.4.2Diffuser

Diffuse lighting is used when reflective objects (shiny metal parts, circuit boards, etc.) must be uniformly illuminated,

since focused light can produce very different grayscale values for the same object parts due to reflection, depending

on the position of the objects.

Machine vision \ Basics of image processing \ Light \ Types of illumination \ Diffuse lighting

Exercise: Using the physical filters

The goal of this exercise is to compare different images with different filters (polarizing filter and diffuser).

Screenshots must be taken during the exercise.

1)Perform image acquisition of "Shiny object".

2)View image in the mapp Vision HMI application.

3)Mount the manual polarizing filter for the tests on the camera.

4)Perform image acquisition of "Shiny object".

5)View image in the mapp Vision HMI application.

6)Mount the diffuser on the camera.

1www.vision-doctor.com

## Page 27

MACHINE VISION BASICS27

7)Perform image acquisition of "Shiny object".

8)View image in the mapp Vision HMI application.

The comparison shows different image quality (DRAFT: Add images)Result:

Figure 45: With diffuserFigure 44: Without filterFigure 46: with polarizing filter

3.4.3Infrared filter (IRFilter)

The camera is equipped with two bandpass filters.

The bandpass filter for the visible range is active by default and blocks out infrared light interference from the sur-

roundings.

The bandpass filter for the infrared range must be enabled when using infrared LED light and blocks out any visible

ambient light.

The bandpass filter can be set to the visible range by setting variable "IRFilter" with the value "FALSE" and the value

"TRUE" to the infrared range.

3.4.4Sobel filter

The  is a software filter for improved visibility of contours on objects. The surface finish is thereby neglect-Sobel filter

ed, and the contrast is directed to the contours of the object.

Figure 47: Without a Sobel filterFigure 48: With a Sobel filter

Hardware \ Machine vision hardware \ Smart Camera \ Smart Sensor \ Functional description \ Prepro-

cessing (linear filters) \ Common filter types

The Sobel filter can be activated in the vision application file (here CodeReader.visionapplication).

## Page 28

28WORKING WITH INTEGRATED MACHINE VISION TM1610

Figure 49: Activate the Sobel filter

Exercise: Using the Sobel filter

The goal of this exercise is to apply a filter integrated in the system to the image acquisition. The Sobel filter makes

it possible to display edges on objects with high contrast.

1)Activate the Sobel filter in the project.

2)Transfer the project change to the controller.

3)Perform an image acquisition of the "B&R sticker".

4)Optional: Photograph another object (e.g. a water bottle).

5)View image in the mapp Vision HMI application.

Compared to previous images, the object is displayed in black, so the surface itself is not consid-Result:

ered any further. Only the contours of the object are highlighted.

Figure 51: Result 2: Sobel filter

Figure 50: Result 1: Sobel filter

## Page 29

MACHINE VISION BASICS29

3.4.5User filter

The  is also a software filter that is created by the user. It can be activated in the vision application file (hereuser filter

CodeReader.visionapplication).

Figure 52: Configure and activate the user filter

Machine vision \ mapp Vision \ Programming \ Vision functions \ Image acquisition

## Page 30

30WORKING WITH INTEGRATED MACHINE VISION TM1610

3.5Sensors

3.5.1Sensor type

The image sensor converts light into digital signals that are inter-

preted by the processor as 8-bit grayscale values. Each grayscale

value represents one pixel. All pixels together correspond to the

sensor surface.

Figure 53: Sensor

There are essentially two different types of : CMOS and CCD.sensors

CMOS (Complementary Metal Oxide Semiconductor) sensors convert the voltage of the electrons in each individual

pixel. This leads to a higher light sensitivity of each pixel. This type of sensor is used in B&R products.

CCD (Charged-Coupled Device) sensors have the pixels connected in series. The charge is shifted from pixel to pixel

and the voltage at the end of the row is converted via transistors.

In addition to the two sensor types, a distinction is also made between monochrome and color sensors.

Monochrome sensors only produce black and white im-

ages and have a higher sensitivity and resolution than

color sensors. Monochrome sensors are used in B&R

products.

Color sensors can detect multiple colors through a color

filter in front of the sensor surface.

Figure 54: Monochrome sensor vs. color sensor

Hardware \ Machine vision hardware \ Smart Camera \ Smart Sensor \ Function description \ Integrated

monochrome illumination

## Page 31

MACHINE VISION BASICS31

3.5.2Sensor size

The  describes the physical size of the sensor and is usually specified as a diagonal in inches. The sensorsensor size

size and the number of pixels result in the maximum achievable resolution or the achievable quality of the images in

terms of noise and detectable grayscale values.

The figures in inches are not real inches but are based on the Vidicon tube. In addition to the direct metric specification

of the active area (e.g. 16 mm × 24 mm), the tradition of using the outer diameter of the glass bulb in inches (e.g. 2/3″)

to specify the size has been preserved from the days of video camera tubes. However, the light-sensitive surface of

the tubes was significantly smaller than the outer diameter of the tubes. For example, a 1″ tube had an active area with

an image diagonal of approximately 16 mm. By definition, a 1″ CCD chip has the same image diagonal as a 1″ tube. 2

Sensor size = Pixel size x resolution

Pixel size and resolution best determine the sensor size.

Larger pixel = More light intensity at the pixel.

Figure 55: Sensor size

3.5.3Pixel size

The  describes thephysical size of the individual pixels on the sensor. Larger pixels enable a higher lightpixel size

sensitivity and have a positive effect on the noise of the sensor.

Hardware \ Machine Vision Hardware \ Smart Camera / Smart Sensor \ Technical data - image sensor

3.5.4Resolution

The  corresponds to the pixel area on the sensor. The shades of gray of our sensors are 8 bits.resolution

Hardware \ Machine Vision Hardware \ Smart Camera / Smart Sensor \ Technical data - image sensor

3.5.5Shutter

The  opens or closes the optical path to the sensor. The shutter controls the exposure time of the sensor usingshutter

the preset exposure time ("ExposureTime").

A main distinction is made between global and rolling shutters.

Global shutters expose all pixels simultaneously for a defined time.

Rolling shutters, on the other hand, expose pixels line by line or column by column.

B&R cameras use electronically controlled global shutters.

2https://de.wikipedia.org/wiki/CCD-Sensor#Gr%C3%B6%C3%9Fenangaben

## Page 32

32WORKING WITH INTEGRATED MACHINE VISION TM1610

3.5.6Line sensor

The  enables continuous image acquisition over one orline sensor

more lines (pixel rows) of the sensor. The camera cyclically records

information of the object passing the sensor. This results in the

possibility of theoretically scanning endlessly long objects line by

line. In practice, however, this is not infinite. An image has a maxi-

mum number of lines.

One area of application can be the inspection of continuous mate-

rials such as fabric webs or rotating materials such as bottles.

This function can be activated in the vision application file.

Figure 56: Line sensor

3.5.7Frame rate

The  indicates the maximum number of images that can be created per time unit. Different frame rates mayframe rate

be required depending on the application.

The maximum possible frame rate can be influenced by using filters or using the camera as a line sensor (not supported

by all devices).

3.5.8Binning

Using , areas of 2x2 pixels can be combined into one pixel. binning

This also increases the depth of field and smooths the edges.

This allows reducing image size and resolution while simultane-

ously accelerating image acquisition.

This function can be activated in the vision application file.

Figure 57: Binning

3.5.9Subsampling

means that only every second pixel in every secondSubsampling

line is evaluated. If active, one pixel is read and the next one is

skipped. As with binning, this reduces image size and resolution

to accelerate image acquisition.

The depth of field remains unaffected and the edge sharpness re-

mains the same.

This function can be activated in the vision application file.

Figure 58: Subsampling

## Page 33

MACHINE VISION BASICS33

3.5.10Field of view

refers to the image acquisition area of the camera.Field of view (FOV)

The maximum possible acquisition area is limited by the FOV to the area

suitable for the application.

The FOV can be adjusted via parameters in the vision application file.

Figure 60: "Field of view" in orange color

Figure 59: Vision application file with line sensor, binning, subsampling, field of view and filter

Exercise: Configuring the field of view

The goal of this exercise is to adapt the image area to the object to be captured via the field of view parameters. The

area is determined using the coordinate display in the mapp Vision HMI application.

1)Perform an image acquisition of the "B&R sticker".

2)First, perform an image acquisition of example "B&R sticker" to read the positions of the desired field of view.

3)Click in the upper left corner of the B&R sticker and read the coordinates (X and Y offsets).

4)Click in the upper right corner of the B&R sticker and read the coordinates.

5)Determine the image width. From the top left corner to the top right corner.

6)Click in the lower left corner of the rectangle and read the coordinates.

7)Determine the image height, from the lower left to the upper left corners.

8)Configure the "Field of view" with the determined coordinates in Automation Studio (ImageWidth, ImageHeight,

ImageOffsetX and ImageOffsetY).

9)Transfer the project and perform a warm restart.

10)View the result in the mapp Vision HMI application and adjust it if necessary.

## Page 34

34WORKING WITH INTEGRATED MACHINE VISION TM1610

By changing the field of view parameters, the image acquisition is limited to the defined area.Result:

The image contains the areas relevant for further processing determined by the FOV.

In addition to the reduced image content, image processing time can be reduced by adjusting the FOV

to the application.

Figure 61: Example with configured field of

view.

## Page 35

VISION FUNCTIONS 35
4 Vision functions
The mapp Vision technology package associated with machine vision products provides a range of vision functions.
These are image processing functions that, depending on the use case, can recognize barcodes or texts or provide
other information about the captured object.
The vision function thus interprets the image captured by the camera and provides application-specific information
that can be further used in the Automation Studio project.
The following vision functions are described in this training module:
4.5 "Code Reader" on page 41
•
4.8 "Pixel Counter" on page 55
•
4.9 "Blob" on page 57
•
4.10 "Matching" on page 64
•
4.12 "OCR" on page 73
•
4.11 "Measurement" on page 68
•
4.12 "OCR" on page 73
•
4.13 "Deep OCR" on page 75
•
4.14 "Smart Camera application - Alignment" on page 78
•
An overview of all available vision functions can be found in the help documentation.
Machine vision \ mapp Vision \ Programming \ Vision functions
What software is required to use the vision functions?
Automation Studio
•
mapp Vision
•
Firmware for the camera
•
Browsers
•
4.1 mapp Vision HMI application
Vision functions can be configured in the mapp Vision HMI application.
The section "Parameters" includes all parameters for configuring the vision functions.
Under "Process variables", all process variables and results that are also linked to the vision function are displayed. The
results can also be found in the I/O mapping of the camera. The "Constants" show the maximum values of the results.
Machine vision \ mapp Vision \ Programming \ mapp Vision HMI

## Page 36

36WORKING WITH INTEGRATED MACHINE VISION TM1610

Figure 62: Vision function overview

Models

Models must be created for model-based vision func-

tions. A model serves as a template for the function used

for finding results during image processing. The mod-

el must first be trained for the desired use case. This

process is also called teach-in.

Machine vision \ mapp Vision \ Program-

ming \ mapp Vision HMI \ Configuration \

Main area vision function overview \ Vision

function configuration \ Models

Figure 63: Model parameters

4.2Region of interest (ROI)

To define one or more regions of interest in the captured image, the  or for short , is used. Theregion of interestROI

ROI specifies the area where a barcode, text or object should be detected. Objects outside this area are not taken into

account by the vision functions.

In contrast to the field of view, ROI does not influence the image acquisition, but limits the area in which a vision

function searches for elements.

The region of interest is adjusted in the mapp Vision HMI application.

## Page 37

VISION FUNCTIONS37

Machine vision \ mapp Vision \ Programming \ mapp Vision HMI \ Configuration \ Main area vision func-

tion overview \ Configuration tools \ ROI tools

Area ROI drawing tools

Rectangle ROI drawing tool

•

Circle or ellipses ROI drawing tool

•

Ring ROI drawing tool

•

Freehand drawing tool (additive)

•

Subtractive rectangle ROI drawing tool

•

Subtractive circle or ellipse ROI drawing tool

•

Subtractive ring ROI drawing tool

•

Eraser tool (subtractive)

•

Drawing tool interactions

Rotating an ROI tool

•

Scaling an ROI tool

•

Selecting an ROI drawing tool

•

## Page 38

38WORKING WITH INTEGRATED MACHINE VISION TM1610

ROI operations

Deleting ROI drawing tools

•

Copying ROI drawing tools

•

Pasting ROI drawing tools from the clipboard

•

Commands for multiple selected ROI drawing tools

Resizing

•

Adjusting the angle

•

Moving to a geometric center line

•

## Page 39

VISION FUNCTIONS39

4.3Loading/Saving the vision application

With this function it is possible to save all changes to the vision application that were implemented in the mapp Vision

HMI application in the controller and load them back into the camera. The entire vision application is always saved

or loaded.

The following steps describe how to .save the vision application

Step 1Step 2

Switch to the "Load/Save vision application" tab in theClick the button "Save to PLC".

mapp Vision HMI application.

Step 3

To ensure that the current vision application file is not only available on the controller but also in the project, it

must be updated via the automation component comparison.

## Page 40

40WORKING WITH INTEGRATED MACHINE VISION TM1610

Step 4

In the automation component comparison, the vision application file "Blob" is marked red since saving it generated

a new version of this file on the controller.

Step 5

To ensure that the new version of the vision application file is also saved in the project, it can be mirrored to the

project page using the button "Mirror to left".

4.4Visual editor

The configuration of the image acquisition and the corresponding vision functions is done in the graphical Vision

.Application Editor

Functions from the Toolbox are inserted and connected in the user interface. Several functions can be combined when

using Smart Cameras.

Parameters, input and output values are configured and linked directly at the functions.

For detailed information about the Vision Application Editor, see Automation Help.

Machine vision \ mapp Vision \ Programming \ Editors \ Vision Application Editor (graphical)

## Page 41

VISION FUNCTIONS41

4.5Code Reader

Vision function  is a generic data code reader. The Code Reader reads and interprets one-dimensional (e.g.CodeReader

barcodes) and two-dimensional (e.g. QR codes) codes. 40 different 1D and 2D data codes are currently supported.

Machine vision \ mapp Vision \ Programming \ Vision functions \ CodeReader

Exercise: Preparing the vision function Code Reader

With the Smart Sensor used in training, only one vision function can be active at a time. For this reason, the Code

Reader must be configured and linked to the Smart Sensor.

Figure 64: Output configuration

Select the vision function at the vision application reference in the mapp Vision component file, and transfer the project

to the controller.

For detailed information about parameters, see Automation Help.

Machine vision \ mapp Vision \ Programming \ Vision functions \ CodeReader \ CodeReader data (cyclic

read)

## Page 42

42WORKING WITH INTEGRATED MACHINE VISION TM1610

Exercise: Code Reader

The goal of this exercise is to read a barcode using the vision function Code Reader. The type of the barcode is identified

automatically and then configured manually.

1)Place the object in front of the camera.

Figure 65: Barcode

2)Open the mapp Vision HMI application and perform an image acquisition.

3)Enable the vision function VfCodeReader_1 in the menu bar.

4)Set ROI to search for the desired code.

5)To enable the function, set "Enable" to "1" in section "Process variables".

6)Set "NumSearchMax" to "1" to limit the number of searched objects to 1.

7)Set "TestExecute" to "1" to mark detected objects with a blue rectangle in the mapp Vision HMI application dur-

ing image acquisition.

8)Execute the vision function using the "Execute" button.

9)The vision function provides information about the symbol type, the content of the barcode itself, its position

and other data that is displayed in the mapp Vision HMI application and at the I/O data points of the camera.

10)Check the image processing time ("CameraProcessingTime").

11)To shorten the image processing time, it makes sense to set the symbol type manually. To do this, set "Symbol-

Type" to the detected type "[10] EAN-8".

12)Re-execute the vision function with "Execute".

13)Compare the new image processing time with the automatic detection time.

## Page 43

VISION FUNCTIONS43

: The barcode is recognized in both cases and the content is read. By manually specifying the sym-Result

bol type of the barcode, the image processing time is significantly reduced.

The processing times shown here are examples only.

## Page 44

44 WORKING WITH INTEGRATED MACHINE VISION TM1610
4.5.1 Parameter sets
Each code type has a set of parameters that are used for identification. Predefined parameters or user parameters
can be selected here. This is defined by the parameter mode:
Values Information
ParameterMode = 0 Use max. recognition (polarity: black on white):
Parameter values are configured for the maximum object recognition rate (at the ex-
pense of recognition speed). Exception for 1D codes: "Fast recognition" is also used
here if ParameterMode = 0.
ParameterMode = 1 Use max. recognition (polarity: white on black):
Parameter values are configured for the maximum object recognition rate (at the ex-
pense of recognition speed).
ParameterMode = 10 Use fast recognition (polarity: black on white):
Parameter values are configured for fastest possible object recognition (at the ex-
pense of object recognition rate).
ParameterMode = 11 Use fast recognition (polarity: white on black):
Parameter values are configured for fastest possible object recognition (at the ex-
pense of object recognition rate).
ParameterMode = 20 Use trained parameter:
Parameter values are configured automatically (see register ParameterOptimization).
ParameterMode = 30 Use user-defined parameter:
Parameter values are set by the user on their own ("expert mode"). The initial values
of user-defined parameters are the values of the last selected parameter set.
Parameter optimization
Automatic optimization of the predefined parameters can increase detection speed and reliability. When parameter
optimization is set to "1", the parameters for the code are stored with each new image acquisition. This makes detec-
tion faster and more reliable (higher probability of detection).
Values Information
ParameterOptimization = 0 NoInfluenceToTrainedParameter (default value):
Standard operation. Optimization is not performed.
ParameterOptimization = 1 OptimizeTrainedParameter:
Parameter optimization is active. With each image acquisition, the algorithm
optimizes the parameter in the background to the acquired image and code.
ParameterOptimization = 2 ResetTrainedParameter:
Resets to the trained parameter set.

## Page 45

VISION FUNCTIONS45

Exercise: Code Reader: Optimizing image processing time and robustness

The goal of this exercise is to improve the image processing time and robustness via different modes of parameter

optimization.

1)Place the object with 2D barcode in front of the camera.

Figure 66: 2D barcode

2)Open the mapp Vision HMI application and perform an image acquisition.

3)Set ROI to search for the desired code.

4)Enable the vision function by setting parameter "Enable" to "1".

5)Set "NumSearchMax" to "1".

6)Set "SymbolType" to "[52] QR Code Model 2".

7)Set "ParameterMode" to "10" to use the fast parameters for the search.

8)Set "TestExecute" to "1".

9)Start the code search.

10)Check the results and image processing time (as with "Exercise: Code Reader").

11)Parameter optimization Set "ParameterMode" to "20" to use the optimized parameters.:

12)Set "ParameterOptimization" to "1".

Parameter optimization only needs to be enabled for a few image acquisitions: While it is enabled, the search be-

comes faster and more robust with each image acquisition. With "ParameterOptimization" = "2", optimization

can be reset for the next image acquisition in order to start new optimization.

13)Perform 3 image acquisitions.

14)Reset "ParameterOptimization" to "0".

15)Start the code search.

16)Check the image processing time.

## Page 46

46WORKING WITH INTEGRATED MACHINE VISION TM1610

: The predefined parameter ParameterMode can be used to adapt the detection behavior andResult

speed to the application. Maximum detection rate, fastest possible detection or optimized parameters

are possible. With mode 10, parameters are used in the exercise that enable fast recognition of the bar-

code. Mode 20 is suitable to further optimize parameters.

Figure 68: Reading QR code with optimized parameters in

Figure 67: Reading the QR code with fast parameters in

"ParameterMode" = "20"

"ParameterMode" = "10"

Parameter optimization does not necessarily lead to an improved detection speed. The detection of the

barcode can also be improved by addressing poor image quality due to blurring, poor lighting or low

contrast.

## Page 47

VISION FUNCTIONS47

4.5.2Code grading

Evaluation of code quality ("CodeGrading") can also be enabled. This is done according to up to 23 different criteria

according to the ISO 15415 and ISO 15416 standards.

The grading is displayed using process variable "GradingValue". This lists the criterion with the lowest rating. Process

variable "EnhancedGradingInformation" contains the grading information for all criteria. If grading is enabled, a value

between 0 and 40 is displayed with 40 being best. If grading is disabled, -1.0 is displayed.

Machine vision \ mapp Vision \ Programming \ Vision functions \ CodeReader \ CodeReader data (cyclic

read)

Exercise: Code Reader: User parameters and code assessment

The goal of this exercise is to search barcodes with user-defined parameters and determine the quality of the barcode

using the quality score.

1)Place the object with "2D barcode with frame" in front of the camera.

Figure 69: Object 4 - 2D barcode with frame

2)Open the mapp Vision HMI application and perform an image acquisition.

3)Set ROI to search for the desired code.

4)Enable the vision function by setting parameter "Enable" to "1".

5)Set "NumSearchMax" to "1".

6)Set "SymbolType" to "[50] Data Matrix ECC 200".

7)Set "ParameterMode" to "[30] user defined" to use the user-defined parameters.

8)Set parameter "C2dPolarity" in the area to [1] light_on_dark.

9)Set "TestExecute" to "1" and start the code search.

10)Activate code grading with "Code Grading" = "1".

11)Start the code search.

12)Check the results and the image processing time.

## Page 48

48WORKING WITH INTEGRATED MACHINE VISION TM1610

: Code grading provides quality parameters for the recognized barcode and can be used as a ba-Result

sis for further parameter optimization. However, code grading is at the expense of increased image pro-

cessing time.

Figure 70: Reading the QR code with active code evaluationFigure 71: Reading the QR code without code evaluation

## Page 49

VISION FUNCTIONS49

4.6Image export

The image that is saved on the camera can be loaded onto the controller using function block ViBaseGetImage in the

logic.

Machine Vision \ mapp Vision \ Programming \ Libraries \ Core \ ViBase \ Function blocks \ Function

block ViBaseGetImage

The image can also be uploaded directly to the server via the browser. To export an image, a button is provided in the

online HMI application that opens a file manager to select the storage location.

Use cases

Uploading can be used, for example, for the following applications

Providing an image for offline HMI application

•

Displaying images in mapp View applications

•

Task: Image download

The goal of this exercise is to save an image to the computer using the online HMI application.

1)Open the online HMI application.

2)Perform the image acquisition.

3)Download the image to the PC.

The image is loaded into the download section of the computer. It can now be used for the offlineResult:

HMI application.

Figure 72: Image upload

## Page 50

50WORKING WITH INTEGRATED MACHINE VISION TM1610

4.7Offline HMI application

The mapp Vision HMI application can also be referred to as an online HMI application because a connected and active

camera is mandatory for execution.

However, the mapp Vision HMI application can also be run independently as an offline HMI application. This refers to

offline mode for the machine vision HMI application and the editors. A machine vision application can thus be config-

ured without a camera connection and with image material only. Switching between the two modes can be seen in the

following figure (in Automation Studio):

Figure 73: Offline HMI application

Machine vision \ mapp Vision \ Programming \ mapp Vision HMI application \ Offline HMI application

The offline HMI application is used for offline configuration of vision functions and is accessible via a vision application.

The vision application must be opened in the visual editor for this. The button to start the offline HMI application links

is located in the upper left corner of the window.

The vision application must be saved first as otherwise the button for the offline HMI application is disabled.

Differences to the online HMI application

The biggest difference is certainly that normal image acquisition and all its functions (e.g. filtering) are not available in

an offline HMI application. So real image acquisition with all its parameters is not available. FunctionProcessingTime,

for example, therefore has no meaning in an offline HMI application. Since the values are not comparable with the

values on the camera when executed on the PC.

Images are acquired via a directory dialog box and selecting an image file.

The vision application is built directly before opening the offline HMI application. It is not necessary to compile the

entire AS project.

If parameters have been changed, they must be saved in the online HMI application in edit mode. Parameters are

automatically updated in the offline HMI application.

The values of variables are automatically written back to the vision application.

## Page 51

VISION FUNCTIONS51

The following aspects of a vision function can be configured:

The "Execution ROI".

•

Parameters of a vision function.

•

Models (for model-based vision functions).

•

Using image material

It is important to note the following points related to image formats and image sizes for imported images.

When the offline HMI application is started for the first time, the standard dialog box for opening a file is also opened.

An image file can now be loaded from the local file storage.

The following formats are supported:

BMP (format of the camera image acquisition)

•

GIF

•

JPG

•

PNG

•

TIFF

•

The intended use case is to make a configuration without connecting to an active camera. Images acquired with the

camera can be used for configuring vision functions offline at a later time. BMP is therefore not only preselected when

opening an image file, it is also strongly recommended to use this file format. It is even required to use the camera's

raw data (BMP) for the final project in order to ensure reproducible results.

Only monochrome images can be imported. Importing a color image results in an error (error code: 77554).

Other images can be loaded using button <Open file>. This button is located in the offline HMI application instead of

button <Acquires an image>.

## Page 52

52WORKING WITH INTEGRATED MACHINE VISION TM1610

Offline HMI application description

The offline HMI application is a web-based mapp View HMI application that is used to configure vision functions. There

are only minor differences to the online HMI application, which are described here.

mapp Vision HMI application overview

1Grayscale histogram

2Vision function selection

3ROI tools

4Vision function variables

5Vision constants

6Vision process variables

7Image selection

8Image

9Execute vision function.

10"Fast forward" is a function of the offline HMI application. It makes it possible to execute a vision appli-

cation with more than one image in a batch.

11Zoom functions

12ROI mode

## Page 53

VISION FUNCTIONS53

Exercise: Code Reader - Offline HMI application

The goal of this exercise is to read the barcode on a saved image using the vision function Code Reader.

1)Open the previously created Code Reader.

Figure 74: Vision function - Code Reader

2)Open the offline HMI application

3)Load the previously saved image.

4)Enable the vision function VfCodeReader_1 in the menu bar.

5)Set ROI to search for the desired code.

6)To enable the function, set "Enable" to "1" in section "Process variables".

7)Set "NumSearchMax" to "1" to limit the number of searched objects to 1.

8)Set "SymbolType" to "[10] EAN-8".

9)Set "TestExecute" to "1" to mark detected objects with a blue rectangle in the mapp Vision HMI application.

10)Execute the vision function using the "Execute" button.

## Page 54

54WORKING WITH INTEGRATED MACHINE VISION TM1610

11)The vision function provides information about the symbol type, the content of the barcode itself, its position

and other data that is displayed in the mapp Vision HMI application and at the I/O data points of the camera.

12)Now change "SymbolType" to "[51] QR Code Model 1".

13)Execute the vision function using the "Execute" button.

14)The vision function provides information about the symbol type, the content of the QR code itself, its position

and other data that is displayed in the mapp Vision HMI application and at the I/O data points of the camera.

: The code is recognized in both cases and the content is read. The correct code is found by man-Result

ually specifying the symbol type of the barcode.

## Page 55

VISION FUNCTIONS55

4.8Pixel Counter

Vision function Pixel Counter is a function for counting pixels and extracting features from them. Pixel Counter enables

the definition of regions through simple operation. The pixels that correspond to a predefined grayscale value interval

(ThresholdMin/Max) are counted within these regions.

Machine vision \ mapp Vision \ Programming \ Vision functions \ Pixel Counter

Use cases

Pixel Counter can be used for the following applications, for example

Ink saturation

•

Fill level detection for bulk material

•

Exercise: Pixel Counter

The goal of this exercise is to test an object using the vision function Pixel Counter in the offline HMI application.

1)Create a new vision function application in Automation Studio for the pixel counter.

Figure 75: PixelCount.visionapplication

2)Load the following object into the offline HMI application and select ROI.

The images are already stored in the logical view for this training in GettingStartedSem1650(..\sem1610\Get-

tingStartedSem1610\Logical\ExerciseImages\*)

Figure 76: 20_PixelCounter / BnR_Sticker_Red.bmp

3)Switch to the tab "Edit model".

4)Add a new blob model using "Add model".

## Page 56

56WORKING WITH INTEGRATED MACHINE VISION TM1610

5)Set the ROI for the area (circle with holes on the left) to count in.

Click on "Teach".

6)When the area has been taught-in and confirmed, switch back to the "Vision function" tab.

7)Enable the vision function by setting parameter "Enable" to "1".

8)Set "NumSearchMax" to "1".

9)Set "TestExecute" to "1".

10)Start the vision function with the "Execute" button.

## Page 57

VISION FUNCTIONS57

The number of pixels corresponding to the configuration in the model is output in NumPixels(n).Result:

All pixels that are located within the Model ROI and also meet the criteria of the configured range of

grayscale values are counted per model.

Figure 77: Result of the Pixel Counter

The parameters "ThresholdMin/Max" can be used to adjust the detection range of the objects.

4.9Blob

A blob (Binary Large Object) is an area of contiguous pixels with the same defined gray value range. It is used to detect

and segment blobs in an image. These can be recognized on the basis of geometric and color parameters.

Blob enables, among other things, the teach-in of areas based on the following parameters.

NameDescription

MeanGrayValueThreshold value for grayscale values

AreaMin/MaxThreshold value for size

Circularity/Rectangularity/Ani-Geometric shape

sometry

MorphologyShape of blob

Machine vision \ mapp Vision \ Programming \ Vision functions \ Blob

Use cases

Blob can be used for the following applications, for example

Position detection

•

Fast measurement

•

Color detection

•

Count products

•

## Page 58

58WORKING WITH INTEGRATED MACHINE VISION TM1610

Models

A model serves as a template that is used to search for results during image processing. The model must first be

trained for the desired use case. This process is also called teach-in.

For other vision functions, such as Matching, an area, also called ROI (region of interest), must be specified. Models

are created and managed in the model view. The model view is enabled via the button "Models" in the mapp Vision HMI

application. The blob parameters can be determined automatically via teach-in.

Exercise: Blob

The goal of this exercise is to detect an object over a created model in the offline HMI application using vision function

Blob.

1)Create a new vision function application in Automation Studio for function Blob.

2)Start the offline HMI application.

3)Load the image.

Figure 78: 30_Blob / BnR_Sticker_Red.bmp

4)Select the vision function "VfBlob_1".

5)Switch to the tab "Edit model".

## Page 59

VISION FUNCTIONS59

6)Add a new blob model using "Add model".

7)Click on the "Marker" button and place the cross-hair pointer in the image on a circle.

Click on "Teach". Repeat this procedure for all circle in the same model.

8)When all circles have been taught-in and confirmed, switch back to the "Vision function" tab.

9)Enable the vision function by setting parameter "Enable" to "1".

10)Set "NumSearchMax" to "4".

11)Set "TestExecute" to "1".

12)Start the vision function with the "Execute" button.

## Page 60

60WORKING WITH INTEGRATED MACHINE VISION TM1610

Although all circles differ in color and contrast, a model can be configured in such a way that theseResult:

color differences have no influence on the detection of the objects.

Figure 79: Result of the model-based blob

The parameters "ThresholdMin/Max" and "AreaMin/Max" can be used to adjust the detection range of

the objects. This can improve the contours during detection especially at the edges of objects.

4.9.1Region features

Region features can be used to distinguish objects with similar grayscale values and area contents based on shapes.

When this function is enabled, a geometric exclusion procedure is applied and objects that deviate from the configu-

ration are not further considered.

The processing and detection of the shapes takes place in the camera and increases the runtime of the vision function.

The following distinguishing features are possible.

Circularity

•

Rectangularity

•

Anisometry

•

Exercise: Blob: Region features

The goal of this exercise is to use region features to implement a distinction between objects with similar contrasts

and area contents based on their shape. Separate models are created for the different shapes, which reliably distin-

guish the objects from each other.

1)Load the following object into the offline HMI application.

Figure 80: 30_Blob / BnR_Sticker_Red.bmp

2)Switch to the vision function.

## Page 61

VISION FUNCTIONS61

3)Switch to the tab "Edit model".

4)Delete all models from previous exercises with "Delete model".

5)Add a new model using "Add model".

6)Add a cross above the marker and place it on a circle in the image.

7)Click on "Teach" and teach-in the circular model.

8)Add another model by clicking "Add model".

9)Insert a cross over the marker and place it on a square in the image.

10)Click on "Teach" and teach-in the square.

11)Switch to the tab "Vision function".

12)Enable the vision function by setting parameter "Enable" to "1".

13)Set "NumSearchMax" to "10".

14)Set "TestExecute" to "1".

15)Start the vision function with "Execute".

16)Enable shape differentiation during the search with "RegionFeatures" = "1".

17)Start the vision function with "Execute".

: The vision function detects all objects for each of the two created models at the first attempt,Result

no matter if circle or square. A more precise distinction is not possible due to the similar area contents

and contrasts.

When activating the region features, the shape of the object is distinguished in addition to the area con-

tent and contrast. NumResults reports back five objects. In addition, each object provides information

about which model it concerns.

Figure 81: Model-based blob: Region features

## Page 62

62 WORKING WITH INTEGRATED MACHINE VISION TM1610
4.9.2 Morphology
Morphology can be used, for example, to separate objects from each other (overlapping, objects close to each other)
or to close gaps in objects (holes in objects). The parameter "CircleMaskRadius" can be used to define the scope of
the function.
The maximum "CircleMaskRadius" is 10.5 pixels.
The following parameters can be configured:
Name Description
MorphologyType = None Morphology is inactive.
MorphologyType = Closing The "Close" function is used.
MorphologyType = Opening The "Open" function is used.

## Page 63

VISION FUNCTIONS63

Exercise: Model-based blob: Morphology

The goal of this exercise is to close holes in the image of an object (blob) using morphology. Such holes can be caused,

for example, by reflections or other influences on the surface of the object and resulting grayscale value deviations

on the blob.

1)Load the following object into the offline HMI application.

Figure 82: 30_Blob / BnR_Sticker_Red.bmp

2)Switch to the tab "Edit model".

3)Delete all models from previous exercises with "Delete model".

4)Add a new model using "Add model".

5)Add a cross above the "marker" and place it on circle with holes in the left of the image.

6)Set "MorphologyType" to "Closing" and execute teach-in.

7)Increase the radius of the function gradually (e.g. in 2 pixel steps) via "CircleMaskRadius" and execute teach-in

again.

Repeat the process until all holes have been integrated into the object during the teach-in process.

8)Switch to the tab "Vision function".

9)Enable the vision function by setting parameter "Enable" to "1".

10)Set "NumSearchMax" to "1".

11)Set "TestExecute" to "1".

12)Start the vision function with "Execute".

The scope of the morphology function is gradually adjusted by increasing the parameter "Cir-Result:

cleMaskRadius". Initially, smaller holes are incorporated into the object until the scope is large enough

to integrate the larger holes as well.

## Page 64

64 WORKING WITH INTEGRATED MACHINE VISION TM1610
4.10 Matching
By means of Matching, objects are localized on a sub-pixel level even if they are twisted or partially covered. The model
is taught-in to an object via a reference image, the so-called template.
Matching distinguishes between the following methods.
Shape-based Matching
•
Correlation-based Matching
•
Machine vision \ mapp Vision \ Programming \ Vision functions \ Matching
Use cases
Matching can be used for the following applications, for example
Object detection
•
Reliable localization of objects
•
Pick-and-place tasks
•
Counting products
•
4.10.1 Shape-based Matching
Using shape-based Matching, objects can be reliably detected regardless of their scaling, perspective distortion, slight
contour deformations, partial overlap and other impairments.
Depending on the application, shape-based Matching can be applied as "Shape model" or "Deformable shape model".
The latter is suitable for achieving a higher degree of accuracy with deformed objects.
Representative of the diverse possibilities of the function, the scaling and overlapping of objects will be used in the
following exercise.
Scaling
Objects can be detected within the configured boundaries using shape-based Matching and a configuration of the
scaling factor with respect to the taught-in model.
By means of "ModelScaleMin/Max", the scale area is defined in which objects of the same shape but of different size
are to be detected. Via "SearchScaleMin/Max" the search of the objects can be further narrowed down to save time
and resources in the application.
Overlap
Using shape-based Matching and a configuration of the degree of overlap of objects, even interlocking objects can be
detected. The degree of permissible overlap can be specified via the parameter "MaxOverlap" as a percentage value
in relation to the total area of the object.
For detailed information about parameters, see Automation Help.
Machine vision \ mapp Vision \ Programming \ Vision functions \ Matching \ Matching configuration
(acyclic write)

## Page 65

VISION FUNCTIONS65

Exercise: Matching: Shape-based

The goal of this exercise is to use shape-based Matching to detect objects of the same shape but of different size,

orientation and overlap. The vision function is configured via parameters.

1)Create the vision function application in Automation Studio for function Matching.

2)Increase the constant "NumSearchMax" to e.g. 10 in the settings of the vision function.

3)Load the following object into the offline HMI application.

Figure 83: 40_Matching / BnR_Sticker_Red.bmp

4)Select the vision function "Matching".

5)Switch to the tab "Edit model".

6)Add a new shape model using "Add model".

7)Adjust the scaling of the object and the search to be able to identify objects of same shape but of different size

in the search.

ModelScaleMin = 0.4

°

ModelScaleMax = 1.6

°

SearchScaleMin = 0.5

°

SearchScaleMax = 1.1

°

8)Place the ROI in the image on the middle or lower B&R logo and click on "Teach".

9)Switch to the "Vision function" page.

10)Set "MinScore" to 0.4.

## Page 66

66WORKING WITH INTEGRATED MACHINE VISION TM1610

Each detected object is assigned a score from 0.0 to 1.0 (in 0.01 increments). The parameter "MinS-

core" can be used to filter objects based on the achieved value and to optimize the processing time.

11)Set "MaxOverlap" to 0.6.

The specified value permits objects to overlap up to 60%.

12)Enable the vision function by setting parameter "Enable" to "1".

13)Set "NumSearchMax" to "4".

14)Set "TestExecute" to "1".

15)Start the search.

With the specified scaling parameters, these objects can be detected as independent elementsResult:

despite the difference in size.

By adjusting the parameters "MinScore" and "MinOverlap", the detection of the objects can be addition-

ally influenced. For example, if the parameter "MinOverlap" is set too small, the two overlapping objects

will not be recognized. Only starting from a value of approx. 0.6 are the two logos in this case displayed

as individual objects.

## Page 67

VISION FUNCTIONS67

4.10.2Correlation-based Matching

Correlation-based Matching is based on changes in grayscale values within the predefined range during a teach-in

process. Normalized cross-correlation (NCC) is used to grade how well the model matches the image being searched.

In contrast to shape-based Matching, is it possible to find objects with slightly different shapes or highly textured

surfaces as well as objects in blurred images.

With correlation-based Matching, scaling and overlapping of objects cannot be implemented.

For detailed information about parameters, see Automation Help.

Machine vision \ mapp Vision \ Programming \ Vision functions \ Matching \ Matching configuration

(acyclic write)

Exercise: Matching: Correlation-based

The goal of this exercise is to identify all rectangles in the left hatching using correlation-based matching. The rec-

tangles have an uneven surface texture with different grayscale values. The hatched background makes it even more

difficult to identify the objects.

1)Load the following object into the offline HMI application.

Figure 84: 40_Matching / BnR_Sticker_Red.bmp

2)Switch to the model view (edit models).

3)Delete all models from previous exercises with "Delete model".

4)Add a new correlation-based model (ncc model) using "Add model".

5)Place the ROI in the image over one of the three rectangles and click on "Teach".

Figure 85: Teach NCC model

6)Switch to the "Vision function" page.

7)Set "MinScore" to 0.6.

Each detected object is assigned a score from 0.0 to 1.0 (in 0.01 increments). The parameter "MinS-

core" can be used to filter objects based on the achieved value and to optimize the processing time.

8)Enable the vision function by setting parameter "Enable" to "1".

9)Set "NumSearchMax" to "3".

10)Set "TestExecute" to "1".

11)Start the search.

## Page 68

68WORKING WITH INTEGRATED MACHINE VISION TM1610

The correlation-based Matching identifies all objects, even though they have a textured surfaceResult:

and thus fluctuating grayscale values in the captured image.

Figure 86: Result - Correlation-based Matching

Figure 87: Result of the search - Matching

4.11Measurement

The vision function measurement is used to perform efficient and highly accurate measurements of lengths, distances

or objects. Edges along objects can be measured with sub-pixel accuracy.

The precise measurement of lengths, distances or radii can be used for quality control or positioning and tracking of

objects.

An appropriate model must be configured for the respective measurement task. For edges, one model describes the

starting point and another model describes the end point for the measurement. Circle measurements also require one

model each for the circle center and the circle circumference.

Depending on the model used, the corresponding values for length, angle, orientation, distance or a combination of

these are returned at the function output "Result".

Machine vision \ mapp Vision \ Programming \ Vision functions \ Measurement

Use cases

Quality inspection

•

Distance measurements

•

Radius measurements

•

## Page 69

VISION FUNCTIONS69

Exercise: Measurement center point of the circle

The goal of this exercise is to calculate the distance between the center points of two circles.

1)Create the vision function application in AS.

2)Open the following object in the offline HMI application.

Figure 88: 50_Measurement / BnR_Sticker_Red.bmp

3)Select the "Measurement" vision function.

4)Switch to the tab "Edit model".

5)Add a new circle model using "Add model".

6)Set "Transition" to "Positive".

The parameter "Transition" can be used to configure which contrast direction should be used.

Figure 89: Transition positiveFigure 90: Transition negative

7)The "ROI window" above the 1. Place the circle and confirm with "Teach".

Two models are always created for circles. The circle model and the center point model.

8)Repeat steps 7 to 9 for the second circle.

## Page 70

70WORKING WITH INTEGRATED MACHINE VISION TM1610

9)Select the operation "Orthogonal distance" in the section "Measurement definition".

10)Select the center point of the first circle as reference for the measurement; Reference = 2 (center point model).

11)Select the center point of the second circle as target for the measurement; Target = 4 (center point model).

12)Save the configured operation.

13)Switch to the "Vision function" page.

14)Enable the vision function by setting parameter "Enable" to "1".

15)Set "NumSearchMax" to "1" because only one measurement is performed.

16)Set "TestExecute" to "1".

17)Start the search.

: The vision function returns the orthogonal distance of the two circle center points.Result

Figure 91: Result of the measurement of circle center points

Exercise: Measurement edges

The goal of this exercise is to calculate the distance between two edges that are parallel to each other.

1)Switch to the tab "Edit model".

2)Add a new circle model (edge model 1) using "Add model".

Machine Vision \ mapp Vision \ Programming \ Vision Functions \ Measurement \ Measurement

configuration (acyclic write)

3)The "ROI window" above the 1. Place the edge, indicate the direction with the arrow and confirm with "Teach".

## Page 71

VISION FUNCTIONS71

The arrow indicates from which direction the edge should be detected.

The parameter "Transition" can be used to change the effective direction of the arrow.

4)Repeat steps 3 and 4 for the second edge.

If the edges are not completely detected during teaching, the detection can be improved via the pa-

rameter "Completeness".

5)Select the operation "Orthogonal distance" in the section "Measurement definition".

6)Select the first edge as reference for the measurement (Reference = 1).

7)Select the second edge as target for the measurement (target = 2).

8)Save the configured operation.

9)Switch to the "Vision function" page.

10)Enable the vision function by setting parameter "Enable" to "1".

11)Set "NumSearchMax" to "1" because only one measurement is performed.

12)Set "TestExecute" to "1".

13)Start the search.

## Page 72

72WORKING WITH INTEGRATED MACHINE VISION TM1610

The vision function returns the orthogonal distance of the two edges.Result:

Figure 92: Result of the measurement of circle center points

## Page 73

VISION FUNCTIONS73

4.12OCR

In general, OCR (Optical Character Recognition) refers to the automated recognition of alphanumeric characters within

acquired images. This works by comparing the pixel patterns and text areas with known and taught-in patterns, similar

to general object detection (such as Matching).

In addition, the vision function is equipped with a search angle ("SearchAngle") that determines the direction of the

text during text search.

Similar to the Code Reader, OCR has text evaluation (grading). Here, the recognition quality of all segmented characters

in a line is determined.

Machine vision \ mapp Vision \ Programming \ Vision functions \ OCR

Grading

After activation, grading provides information about the recognition quality of the segmented characters in a line.

This quality criterion is output at the parameter "GradingValue" and provides information about whether the taught-

in characters can be reliably detected.

Activating the grading function affects the runtime and the internal memory consumption of the camera.

OCR classifier

The vision function OCR identifies texts based on predefined OCR classifiers that have been taught-in for a wide variety

of fonts.

Different methods are available to detect the font used.

Use of "ParameterMode" = 0, which uses a universal classifier.

•

Use of "ParameterMode" = 1, which allows manual changes to parameters, whereby individual classifiers can be

•

selected.

The ParameterMode = 1 assumes expert knowledge. For detailed information, see Automation Help.

Machine vision \ mapp Vision \ Programming \ Vision functions \ OCR \ OCR parameter (cyclic read)

Exercise: OCR text recognition

The goal of this exercise is to detect a text using the OCR vision function and to optimize the text detection time.

1)Create the vision function application in AS.

2)Load the following object into the offline HMI application.

## Page 74

74WORKING WITH INTEGRATED MACHINE VISION TM1610

Figure 93: 60_OCR / BnR_Sticker_Red.bmp

3)Select the vision function "OCR".

4)Set ROI for the text.

5)Set the text direction manually using the mouse or with the parameter "SearchAngle" (parameterMode must be

set to 1).

6)Enable the vision function by setting parameter "Enable" to "1".

7)Since a result found always consists of only one line, "NumSearchMax" must be set to "1".

8)Set "TestExecute" to "1".

Optionally, text grading can also be enabled with "Grading" = "1".

9)Start the search.

10)Set measures to optimize the detection time.

The detection parameters can be adjusted via the parameterMode = 1. In the example the minimum contrast

°

is set to 80, which allows for reducing the acquisition time.

Additionally, a text format can be selected to speed up text recognition.

°

11)Start the search.

## Page 75

VISION FUNCTIONS75

: The OCR vision function can also be used to recognize and evaluate multi-line texts; an ROI mustResult

be defined for each line. By setting the parameter mode = 1, further parameters can be influenced. In-

creasing the minimum contrast significantly accelerated the detection.

The use of "grading" also affects the processing time.

4.13Deep OCR

Vision function Deep OCR reads and interprets texts based on a number of pre-trained fonts suitable for a wide variety

of applications using "deep learning" (dot-matrix fonts, semi-fonts, industrial fonts, handwriting, etc.). It is therefore

possible to achieve very high recognition rates without additional training. The pre-trained deep learning network is

stored on the camera.

For good quality of the characters to be recognized, a character width of 8 pixels in image acquisition

has proven useful. A minimum quiet zone around the ROI is also necessary and must be kept free.

Machine vision \ mapp Vision \ Programming \ Vision functions \ Deep OCR

Exercise: Deep OCR - Text recognition

The goal of this exercise is to detect a text using the vision function Deep OCR and optimize the text detection time.

1)Create the vision function application in AS.

## Page 76

76WORKING WITH INTEGRATED MACHINE VISION TM1610

2)Load the following object into the offline HMI application.

Figure 94: 70_DeepOCR / BnR_Sticker_Red.bmp

3)Select the vision function "Deep OCR".

4)Define the ROI for B&R and/or "A member of the ABB Group".

5)Set the text direction manually with the mouse.

6)Enable the vision function by setting parameter "Enable" to "1".

7)Set the parameter "NumSearchMax" to the number of lines. (e.g. 2)

8)Set "TestExecute" to "1".

9)Start the search.

## Page 77

VISION FUNCTIONS77

: Using the vision function Deep OCR, even handwritten texts can be detected and evaluated.Result

## Page 78

78WORKING WITH INTEGRATED MACHINE VISION TM1610

4.14Smart Camera application - Alignment

Alignment functionality requires the use of two vision functions. The parameters of a vision function, based on a pre-

cisely determinable and defined object, support and improve the alignment for the subsequent application of the sec-

ond vision function.

Possible producers of reference data for Alignment are: Code Reader, Blob, Matching or Subpixel Blob.

All vision functions are consumers of Alignment data. Parameters OffsetROIX, OffsetROIY, OffsetROIOrientation, Off-

setROIRotCenterX and OffsetROIRotCenterY are written to with the determined values.

Machine vision \ mapp Vision \ Programming \ Vision functions \ Vision functions - Alignment

Optional exercise: Tracked Measurement with alignment via matching

The goal of this exercise is to measure an object using two connected vision functions.

1)Create the vision function application in Automation Studio with matching and measurement.

2)Link function Matching with function Measurement as shown in the image below.

Figure 95: Vision function link for Alignment

3)Load the following object into the offline HMI application.

## Page 79

VISION FUNCTIONS79

Figure 96: 60_SmartCamera\SlidingCalliper\Sliding_01.bmp

4)Teach-in a "Shape model" in "Edit models".

Figure 97: 60_SmartCamera\SlidingCalliper\Sliding_01.bmp

5)Test the model on the various images (Enable=True, NumSearchMax=1, TestExecute=True).

6)For the image used for matching, set Alignment to [1] Set Reference and run "Execute"

7)Set Alignment to "[2] generate Alignment data" and select "Execute".

8)Calibrate the vision function "Measurement".

9)Calibrate edge model "Mode 1" for edge 1.

10)Calibrate edge model "Mode 1" for edge 2.

11)Create operation "orthogonal_distance" for both edges

12)Test the model on the various images (Enable=True, NumSearchMax=1, TestExecute=True).

## Page 80

80WORKING WITH INTEGRATED MACHINE VISION TM1610

The opening width can be measured for all images of the caliper, regardless of how the caliperResult:

is positioned.

Figure 98: Result: MatchingFigure 99: Result: Measurement

## Page 81

SYNCHRONIZATION81

5Synchronization

Images of moving objects must often be captured using machine vision technology. The image must be captured

at a specific time point, so that it is acquired at exactly the right time. The POWERLINK bus system used allows the

synchronization of machine vision products and other devices connected to the bus with µs precision. The image

acquisition, for example, can be synchronized and triggered to an axis.

5.1Conditions

The following points must be taken into account for image acquisition with "Nettime":

The POWERLINK interface must be configured as the "System timer" in the controller configuration.

•

Figure 100: System timer configured as POWERLINK interface

The POWERLINK cycle must be synchronous to the task class used.

•

The POWERLINK response time (DelayNettime) must be taken into account in order to calculate sufficiently far

•

into the future.

For additional information about the POWERLINK response time, see Automation Help.

Machine vision \ mapp Vision \ Use cases \ Triggering image acquisition with NetTime

The POWERLINK topology must be taken into account: 1 μs per 100 m copper / fiber optic cable per direction and

•

2 μs per hub level (times depend on the hub systems used).

The Nettime of the master by using the data point "NettimeSoC" of the POWERLINK master interface must be

•

determined.

The interrupt to copy the data from the POWERLINK cycle to the camera must be known 100 µs before the last

•

SoC. This means that with normal behavior, the time from retrieving the NetTime from the POWERLINK manag-

ing node (MN) to the camera's response is a maximum of 4 + 1 POWERLINK cycles since the time from the Net-

timeSoC is already one cycle in the past.

The camera and light must be synchronized with ACOPOS drive components by using a function block.

•

Machine Vision \ Programming \ Libraries \ Core \ ViBase \ Function blocks \ Function block

ViBaseAxisBasedAcquisition

With a Smart Light, the behavior is basically the same as with a Smart Camera, except that the Smart Light mod-

•

ule takes half an X2X cycle longer (the X2X cycle in the camera is always the same value as the POWERLINK cycle

time). The CalculatedTimestamp is forwarded from the function block to the vision component.

Machine vision \ mapp Vision \ Use cases \ Triggering image acquisition with NetTime

Communication \ POWERLINK \ Response time

Exercise: Preparing the synchronization

To use this feature in the vision function, the "Trigger delay" must be configured as Nettime.

## Page 82

82WORKING WITH INTEGRATED MACHINE VISION TM1610

Figure 101: Camera synchronization mode

Link variable gVisionCtrl.Parameters.DelayNettime in the I/O mapping of the interface.

The current POWERLINK system time ("NettimeSoC") is provided by the controller and is available in the

I/O mapping of the POWERLINK bus. All devices that are to be controlled synchronized must use this

time base.

Link the variable gVisionCTRL.Parameters.DelayNettime in the I/O mapping of the camera.

## Page 83

SYNCHRONIZATION83

Figure 102: Timestamp ("DelayNettime")

The data point "NettimeSOC" corresponds to the time at which the POWERLINK bus system starts the

cyclic communication to the bus stations (Start of cycle). The parameter "DelayNettime" can be used to

take delays in the structure into account. This makes it possible to compensate for delays that may be

caused by bus stations due to response times or topology components such as hubs, for example.

## Page 84

84WORKING WITH INTEGRATED MACHINE VISION TM1610

5.2Light bar

The lighting integrated in the camera can be used for a variety of applications. Other tasks require lighting that illu-

minates the object from the side. This can be useful when dealing with grazing light (light entering from the side) or

reflections, for example. The beam angle of the light bar can be adjusted to the task via data points.

Light bars are connected via POWERLINK and are configured in Automation Studio in the same way as the camera.

The light bar configuration is also entered as a reference in the ViComponent.

Exercise: Integrating a light bar

The goal of this exercise is to add a light bar to the system and to replace the lighting of the camera with it. The "2D

barcode" is recorded cyclically and the light is synchronized with the camera. A calculation is programmed as a task

for synchronization.

1)Place the following object in front of the camera.

Figure 103: B&R sticker cover

2)Create a constant for the POWERLINK cycles in the Global.Var file for calculation.

VAR CONSTANT

PLK_CYCLE_TIME: UINT := 400;

END_VAR

VAR

gLightbarEnable : BOOL;

END_VAR

3)Link the variable "gVisionCtrl.Parameters.Nettime" with NettimeSoC from the POWERLINK master.

## Page 85

SYNCHRONIZATION85

Figure 104: NettimeSoC

4)Integrate the light bar in POWERLINK.

Set the node number switch on the device accordingly.

5)Create the global data type "gLightbarCtrlType" with the substructures "Cmd", "Parameter" and "Status".

TYPE

gLightbarCtrlType :     STRUCT

Cmd : gLightbarCtrlCmdType;

Parameters : gLightbarCtrlParType;

Status : gLightbarCtrlStatusType;

END_STRUCT;

END_TYPE

TYPE

gLightbarCtrlCmdType :     STRUCT

FlashTrigger : BOOL;

ResetFlashTrigger : BOOL;

END_STRUCT;

gLightbarCtrlParType :     STRUCT

SetAngle : UINT;

ExposureTime : UDINT;

FlashColor : USINT;

Nettime : DINT;

END_STRUCT;

gLightbarCtrlStatusType :     STRUCT

LightbarReady : BOOL;

END_STRUCT;

END_TYPE

6)Create the global variable "gLightbarCtrl".

VAR

gLightbarCtrl : gLightbarCtrlType;

END_VAR

7)Link variable "gLightbarCtrl" with the I/O mapping for the light bar.

## Page 86

86WORKING WITH INTEGRATED MACHINE VISION TM1610

8)Add the parameters for the light bar in the INIT section of the program, and adjust the parameter for the camera

so that the lighting of the camera is disabled (FlashColor = 0). The ExposureTime is the same as for the camera.

gLightbarCtrl.Parameters.FlashColor     := 99;

gLightbarCtrl.Parameters.ExposureTime     := 31;

gLightbarCtrl.Parameters.SetAngle     := 45;

gVisionCtrl.Parameters.FlashColor      := 0;

9)In the cyclic part, insert the calculation of the Nettime and extend the IF statement to trigger the light bar.

// Nettime calculation with 400 µs POWERLINK cycle time

gLightbarCtrl.Parameters.Nettime := gVisionCtrl.Parameters.Nettime + 5*PLK_CYCLE_TIME;

gVisionCtrl.Parameters.DelayNettime := gLightbarCtrl.Parameters.Nettime;

IF gLightbarEnable AND

gVisionCtrl.Status.CameraReady AND

NOT gVisionCtrl.Status.ImageProcessingActive THEN

gLightbarCtrl.Cmd.FlashTrigger := TRUE;

gVisionCtrl.Cmd.AcquireImage:= TRUE;

ELSE

gLightbarCtrl.Cmd.FlashTrigger := FALSE;

gVisionCtrl.Cmd.AcquireImage:= FALSE;

END_IF

10)Transfer project in task class#1.

11)Optionally, perform image acquisitions of reflecting objects (e.g. water bottle). This can be done by first using

the camera lighting and then the light bar to notice differences in reflection.

: The camera creates a new image of the QR code and identifies it when the camera is ready. TheResult

lighting is provided by the light bar in this case. The lighting of the camera is deactivated.

Since the QR code does not change in front of the lens, it is possible to check whether images are actually

being captured cyclically by covering the lens.

## Page 87

SYNCHRONIZATION87

5.3Synchronizing with mapp Axis

The function block "ViBaseAxisBasedAcquisition" can be used to create synchronized images of moving axes.

Function block ViBaseAxisBasedAcquisition calculates timestamps for image acquisitions of the machine vision cam-

era based on axis positions and acquires images at these times. The positions are read directly from the axis config-

ured in the currently used vision application of the associated vision component.

Task: Synchronizing camera with axis

The goal of this exercise is to add an axis to the system and synchronize the camera with the axis. The camera is

pointed at the axis and images are acquired at certain angles.

1)Place the axis in front of the camera and determine the new focus.

2)ViAxis program is already configured in the CPU in TK#4 when using project Getting Started.

Note: The axis is configured as a rotary axis with a period of 360 degrees.

3)Create a VAMatching application and assign it to the vision component.

Open configuration Vision Acquisition as a table (VisionApplication)

Acquisition configuration / Trigger source = mappAxis

°

Acquisition configuration / Trigger source / AxisReference = gAxis_1

°

Figure 105: Vision application - table view

4)Create a function block ViBaseAxisBasedAcquisition in the Variables.var file of the ST program.

VAR

ViBaseAxisBasedAcquisition_0 : ViBaseAxisBasedAcquisition;

END_VAR

5)Call the function block ViBaseAxisBasedAcquisition in the cyclic section of the program to create an image every

90 degrees.

// Es wird periodsch ein Bild alle 90 Grad aufgenommen

ViBaseAxisBasedAcquisition_0.MpLink := ADR(gCamera);

ViBaseAxisBasedAcquisition_0.AcquisitionParameters.AcquisitionPositions[0] := 0;

ViBaseAxisBasedAcquisition_0.AcquisitionParameters.AcquisitionPositions[1] := 90;

ViBaseAxisBasedAcquisition_0.AcquisitionParameters.AcquisitionPositions[2] := 180;

ViBaseAxisBasedAcquisition_0.AcquisitionParameters.AcquisitionPositions[3] := 270;

ViBaseAxisBasedAcquisition_0();

6)Transfer project in task class#1.

7)Start the vision HMI application and test the image acquisition.

8)Teach-in the arrow on the motor disk as model.

9)Test the vision function in the HMI application.

10)Save the settings in the vision HMI application.

## Page 88

88WORKING WITH INTEGRATED MACHINE VISION TM1610

11)Starting the motor in the watch window of task "ViAxis" with variable "StartMotor=TRUE"

12)Start the image acquisition in the watch window of your own task at function block "ViBaseAxisBasedAcquisi-

tion_0.Enable=TRUE"

: The camera generates axis-synchronized images every 90 degrees from the starting position.Result

Figure 106: Model parameters

Figure 107: Vision function parameters

Figure 109: Result in the I/O mapping of the vision camera

Figure 108: Image

## Page 89

SUMMARY89

6Summary

Hardware configuration and commissioning

The B&R machine vision portfolio consists of:

Camera (Smart Sensor / Smart Camera)

•

Light bar

•

Backlight

•

The advantages of machine vision are:

Full integration into the B&R automation system

•

Next generation usability - for developers and users

•

µs accuracy and synchronous data exchange

•

Full integration into mapp Technology

•

Complete portfolio: Camera, lenses, lights and software

•

Machine vision basics

The basics are divided into the following subsections:

Light

•

Lens

•

Colors

•

Filters

•

Sensors

•

Vision functions

Integrated machine vision functions allow various tasks to be completed:

Figure 110: Vision functions and tasks

Synchronization

Using the POWERLINK bus system, several bus devices can be synchronized with each other in order to control them

at the same time.

The so-called "Nettime" of the POWERLINK bus system is used. In addition to the Nettime, the POWERLINK response

time, topology, speed and acceleration of the axis must be taken into account.

## Page 90

90WORKING WITH INTEGRATED MACHINE VISION TM1610

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

## Page 91

AUTOMATION ACADEMY 91

## Page 92

B&R

Industrial Automation GmbH

A member of the ABB Group

B&R Straße 1

5142 Eggelsberg, Austria

office@br-automation.com

t +43 7748 6586-0

f +43 7748 6586-26

br-automation.com

V4.1.0.1 ©2024/07/17 by B&R, All rights reserved.

All registered trademarks are the property of their respective owners.

Subject to technical changes without notice.