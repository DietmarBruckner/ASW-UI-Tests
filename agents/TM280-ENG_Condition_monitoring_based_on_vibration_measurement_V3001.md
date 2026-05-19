## Page 1

TM280

Condition monitoring

based on vibration

measurement

## Page 2

2 CONDITION MONITORING BASED ON VIBRATION
MEASUREMENT TM280
Requirements
B&R product range
Training modules TM210 – Working with Automation Studio
Automation Studio 4.2.5 and later
Automation Runtime 4.25 and later
Software mapp V1.40 and later
Hardware ETA280.0102-100

## Page 3

TABLE OF CONTENTS 3
Table of contents
1 Introduction.........................................................................................................................................................4
1.1 Learning objectives..............................................................................................................................4
2 General information about condition monitoring......................................................................................5
2.1 Effects on machine condition...........................................................................................................5
2.2 Maintenance strategy.........................................................................................................................5
2.3 Condition monitoring – How?...........................................................................................................6
3 Condition monitoring based on vibration measurement..........................................................................7
3.1 Basics of instrumentation and mathematics.................................................................................7
3.2 General parameters and characteristic values.............................................................................12
3.3 Area of use and application examples..........................................................................................14
4 B&R vibration measurement systems..........................................................................................................17
4.1 Condition monitoring module - X20CM4810................................................................................17
5 Practical applications for damage detection............................................................................................26
5.1 Analysis of imbalances......................................................................................................................26
5.2 Analyzing periodic impacts.............................................................................................................28
5.3 Analysis of bearing damage............................................................................................................31
6 Condition monitoring in a real machine environment.............................................................................35
6.1 Collecting data....................................................................................................................................35
6.2 Recording.............................................................................................................................................36
6.3 Reporting.............................................................................................................................................37
7 APROL ConMon.................................................................................................................................................39
8 Summary............................................................................................................................................................40
9 Appendix............................................................................................................................................................41
9.1 Uploading raw data...........................................................................................................................41

## Page 4

4CONDITION MONITORING BASED ON VIBRATION

MEASUREMENT TM280

1Introduction

From oscilloscope modules to sensor-equipped servo drives, from the monitoring chip inside every Automation PC to

vibration analysis modules with embedded intelligence, B&R offers every possibility to keep your finger on the pulse

of the machine without requiring any external units. Condition monitoring is already an integral part of every B&R

system and consequently of every machine that is automated with B&R technology. Predictive maintenance based on

constant condition monitoring helps avoid unplanned downtime and reduces the cost of stocking spare parts. Using

a machine's status signals as an input for open and closed-loop control systems helps extend its service life.

Figure 1:  Condition monitoring is used in a wide variety of industries

The development efforts of mechanical engineers in machine manufacturing are paying off as the power density and

functional density of production machines continue to increase. As a result, fewer machines are needed to produce the

same quantity and level of complexity. More efficient, economical production makes machine operators more compet-

itive. On the other hand, this increased efficiency also makes them more dependent on each individual machine. The

more units that can be produced per hour, the more each hour of downtime costs. And anyone who has ever operated

a machine knows that downtime can never be completely avoided.

1.1Learning objectives

This training module takes a closer look at condition monitoring based on vibration measurement.

Participants will receive general information about the topic of condition monitoring

■

Participants will learn the basics of vibration measurement technology.

■

Participants will learn about the areas of application and use cases.

■

Participants will learn about the condition monitoring module - X20CM4810.

■

Participants will receive information about practical applications for damage detection.

■

Participants will receive information about condition monitoring in a real machine environment.

■

## Page 5

GENERAL INFORMATION ABOUT CONDITION MONITORING 5
2 General information about condition
monitoring
When managers are considering whether or not to invest in new machinery, they evaluate not only the purchasing
price, but the cumulative cost over its entire useful life. In order to reduce these overall costs, often referred to as TCO
(total cost of ownership), the goal is to maximize machine availability while minimizing the time and money spent on
maintenance.
One of the ways machine manufacturers are attempting to optimize maintenance costs is to replace fixed maintenance
intervals with condition-based preventative maintenance. An advantage of this is that maintenance work can be de-
layed until it is actually needed, which can be quite a bit longer than conservatively estimated maintenance intervals.
In addition, scheduling freedom is retained and maintenance work can be performed during regularly scheduled down-
time, such as on weekends. At the same time, this solution avoids risking system failure due to neglected maintenance.
Success in this balancing act relies on condition monitoring – permanently monitoring conditions throughout the
entire system. Data gathered through condition monitoring can be used to identify the maintenance requirements of
the respective equipment.
Request Solution
Machine downtime = costly Machinery health monitoring
Maintenance = costly Maintenance control
Poor processing output (quality/quantity) = costly Process optimization
2.1 Effects on machine condition
There are different influences that can have an effect on the state of a machine.
An important factor here is, for example, the environmental conditions where the work is being done. A very hot en-
vironment with relatively high humidity can have an effect. This is also the case if it is very cold or the environment
is dusty.
An additional factor here can also be the person. If, for example, issues are not solved professionally or maintenance
work is not done properly, this can lead to problems. In addition, incorrect operation by users themselves can of course
worsen the state of the machine.
Continued operation of a machine can, in these situations, cause components to wear out.
Eventually machine/system service is necessary.
2.2 Maintenance strategy
The operating performance of every mechanical component changes over the course of operation, with each compo-
nent becoming defective at some point. It is crucial to recognize such a change before the component can no longer
fulfill its function.
For a failure-oriented operational mode ("reactive maintenance"), components are only replaced if they can no longer
fulfill their function. For planned operation ("preventive maintenance"), components are replaced at a certain point in
time – regardless of their current condition.
For condition-based maintenance, the area where maintenance is planned can be significantly isolated while reducing
the risk of failure at the same time.

## Page 6

6CONDITION MONITORING BASED ON VIBRATION

MEASUREMENT TM280

AdvantagesDisadvantages

Reactive maintenance

Utilization of reserves with re-Unexpected failure

••

gard to wearConsequential damage

•

No costs during the period ofHigh downtime costs

••

useLow operational safety

•

Preventive maintenance

Can be planned wellNo utilization of reserves with re-

••

gard to wear

Increased risk of failure after

•

maintenance

Fixed costs

•

Condition-based maintenance

Early recognition of problemsDealing with the issue

••

Downtime can be plannedInvestment costs

••

Utilization of reserves with re-

•

gard to wear

High operational safety

•

Avoidance of consequential

•

damage

Table 1: Maintenance strategies

2.3Condition monitoring – How?

Information must be collected in order to be able to monitor the state of the machine, and from that information

corresponding conclusions have to be drawn regarding the required maintenance of monitored parts – this is referred

to as condition monitoring.

This information can be collected by evaluating various signals and condition parameters.

Some significant physical condition parameters are:

Vibrations

■

Velocity

■

Motor current

■

Temperature

■

Pressure

■

Flow rate

■

Fill level

■

Conductivity

■

Figure 2: Physical measured values

In this training module, we're focusing on condition monitoring based on vibration measurement tech-

nology.

Different modules (e.g. analog input or temperature modules) for collecting condition parameters are

available in the B&R product portfolio. There are also specialized modules such as valve control modules

with integrated switching time detection or oscilloscope function.

## Page 7

CONDITION MONITORING BASED ON VIBRATION MEASUREMENT7

3Condition monitoring based on vibra-

tion measurement

In addition to productive output, machines also generate undesired vibrations. These undesired vibrations can be

monitored with condition monitoring.

Vibration measurement technology can be used to collect data related to mechanical vibrations that occur on a ma-

chine. For this, corresponding vibration sensors (acceleration sensors) are used. The acceleration measured within the

scope of the condition monitoring is typically measured with piezoelectric sensors.

3.1Basics of instrumentation and mathematics

For the most part, structure-borne sound, i.e. the sound that spreads through a solid object, is measured. The three

vibration magnitudes correlate mathematically to the integration or differentiation of the basic variables. Vibration

velocity is calculated through integration from the vibration acceleration; vibration displacement is calculated through

integration from the vibration velocity.

Figure 3: Structure-borne sound

If a fixed medium is stimulated by an impact, structure-borne sound spreads throughout it. This consists of additional

frequencies that are determined by the shape of the structure and the material it is made from (e.g. gong or concrete

block).

3.1.1Vibrations

Vibrations are forms of movement that occur very frequently in nature. A vibration is a cyclic, i.e. repetitive simultane-

ous movement of a structure in its rest or equilibrium position.

Figure 4: Vibrations

## Page 8

8 CONDITION MONITORING BASED ON VIBRATION
MEASUREMENT TM280
Vibrations have the following parameters, among others:
Amplitude
•
Period duration
•
Frequency
•
Causes and effects
There are numerous overlapping causes for vibrations. The amplitude of the vibration depends on several factors such
as attenuation through joints or grease, the rigidity of the component, the housing and foundations, and much more.
A few typical causes are explained below.
Imbalance
Imbalances on a rotating structure not only cause forces on the bearing and foundations, but also vibrations in the
machine.
The character of these vibrations is harmonic. The excitation frequency corresponding to the rotary frequency of the
imbalanced rotor.
Impacts
Foreign objects as well as loose or colliding parts can cause shocks between rotating and stationary parts. These
shocks repeat periodically once or several times each time the shaft revolves.
The frequency of these shock repetitions corresponds to the rotary frequency of the shaft or its harmonic frequency.
Roller bearing damage
Most bearing damage results from changes on the surface (pitting). By rolling over the damaged area on the inner ring,
outer ring, cage or rolling element, pulse-shaped shocks occur that make the bearing structure and its components
vibrate.
Each of these shocks appears in the vibration signal through the typical course of a shock sequence. Characteristic
values can be obtained from these measurements that give an indication of the condition of the bearing.
The excitation frequency on the inner ring, outer ring, cage and roller bearing damage is specified by the
bearing manufacturer.
Effects
Increased vibrations can result in malfunctions in the machine, particularly in measurement and control devices. If this
causes the measuring equipment to resonate as well, incorrect measurements will result and manufacturing quality
will suffer.
In addition, stress will develop on the components of the machine. Unwanted vibrations result in increased wear with
partly plastic distortion of components and increased crack formation all the way up to failure.
3.1.2 Fast Fourier transforms (FFT)
Vibration signals generally consist of a number of vibrations that occur simultaneously and overlap. Individual frequen-
cies are not directly evident from a timing diagram.

## Page 9

CONDITION MONITORING BASED ON VIBRATION MEASUREMENT9

Figure 5: Time signal of a vibration

The Fourier transform is the basic principle of frequency analysis. It assumes that each harmonic vibration can be

broken down into any number of sinusoidal and cosinusoidal waves, the sum of which reproduces the original vibration.

In order to be able to evaluate individual partial vibrations as amplitude and frequency, the digitized time signal is

converted into a frequency spectrum. In addition, a small extract is taken from the signal; this is known as the time

window. Using the FFT (Fast Fourier transformation) algorithm, the frequency spectrum is calculated from this so that

each involved vibration and its associated frequencies and amplitudes is shown as a single line in the line spectrum.

Figure 6: Fast Fourier transformation

A closer look at the spectral lines lets you determine the frequency resolution.

Frequency resolution [Hz] = 1 / measurement time [s]

## Page 10

10CONDITION MONITORING BASED ON VIBRATION

MEASUREMENT TM280

For a single sine signal with a constant frequency, a single line is shown in the frequency spectrum.

3.1.3Envelope analysis

The envelope analysis is a method for differentiating between harmonic causes (imbalance, orientation) and im-

pact-related causes (roller bearing damage, gearing damage, etc.).

Here, the frequency spectrum of the envelope signal is evaluated.

Figure 7: Time signal with envelope (blue)

Figure 8: Frequency spectrum of the envelope

Harmonic causes cannot be properly identified in an envelope spectrum.

3.1.4Sensor technology

Vibration sensors convert the mechanical vibrations of the machines being measured into an electrical signal.

The acceleration measured within the scope of the condition monitoring is typically measured with piezoelectric sen-

sors.

The Integrated Electronics Piezo Electric (IEPE) technology used in B&R sensors strengthens the signal directly in the

sensor and emits it as a low-resistance voltage signal. Sensor sensitivity is specified in mV/g.

## Page 11

CONDITION MONITORING BASED ON VIBRATION MEASUREMENT11

Piezoelectric sensors cannot measure static magnitudes.

Accelerometers

IEPE interface

•

Sensitivity: 100 mV / g

•

Measurement range:  50 g±

•

2 mounting options

•

Figure 9: B&R acceleration sensor

3.1.5Sensor positioning

To ensure optimal detection and measurement of frequencies propagating from a point of damage, sensor positioning

is very important. The ideal position for mounting a sensor on a structure is often difficult to reach and is not always

necessary.

Since sound waves propagate throughout the entire structure, the damage frequencies are measured with varying

intensity or amplitude (green arrow).

If a flexible connection is used, a valid measurement is no longer possible (red arrow).

Figure 10: Sensor position

Detailed information regarding the installation of sensors can be found in the data sheet of the associ-

ated module.

3.1.6Determining limits and alarm limits

Some manufacturers give limits for permissible vibrations and other relevant factors for assessing the status of the

machine.

For certain machines and systems, limits are fixed by norms. With the exception of ISO 10816, these give little infor-

mation for assessing the actual status of the machine.

Characteristic values are calculated from the measured signals, which are representative of the status of the system

at the given measurement point. A fundamental statement can be made by comparing the characteristic values with

predefined limit values.

## Page 12

12CONDITION MONITORING BASED ON VIBRATION

MEASUREMENT TM280

Operators can also draw on their own experience when

assessing the status of the machine. Long-term observa-

tion of the characteristic values and the machine's history

can provide relevant values based on experience.

The characteristic curve of the characteristic values can

also be used for state assessment. Changes in the trend

are observed with the normal condition as the starting

point.

Figure 11: Trend progression

3.2General parameters and characteristic values

It is possible to gain good insight into the condition of a machine or system by collecting the parameters associated

with errors. This parameter data is used by different algorithms to calculate the characteristic values.

Selecting suitable characteristic values and assessing them over a longer period of time is the basis for effective and

successful monitoring of a machine.

3.2.1RMS value

The RMS value is also known as the quadratic mean, or the root-mean-square. Along with the amplitude, it also takes

the energy content of the vibration into account and is the mathematical background for many characteristic values

of assessment.

The RMS value can be calculated for the raw signal and also for the envelope signal.

ISO 10816

Formed from the raw signal of the vibration velocity in a frequency range from 10 Hz to 1 kHz according to ISO 10816.

Figure 12: Assessment in accordance with ISO 10816

3.2.2Peak value

The peak value of a mechanical vibration signal indicate the maximum sum of individual impacts that come from the

ambient noise. Different types of damage give rise to strong impacts, which show up in the peak value.

## Page 13

CONDITION MONITORING BASED ON VIBRATION MEASUREMENT13

3.2.3Crest factor

The crest factor is defined as a the quotient derived from the peak value and the RMS value.

It can be used for detecting bearing damage or poor lubrication.

Figure 13: Crest factor

The crest factor can remain unchanged or even sink again, even despite damage.

3.2.4K(t) value

The K(t) is described in the VDI 3832 guideline and is calculated from the RMS value and the peak value of a time signal

for vibration acceleration.

This ratio correlates to reference values that are held shortly after the running-in time.

The K(t) value decreases with progressive wear and can be divided into three classes:

Undamaged

•

Early damage

•

Pronounced damage

•

Figure 14: K(t) value progression

## Page 14

14CONDITION MONITORING BASED ON VIBRATION

MEASUREMENT TM280

3.2.5Kurtosis

The kurtosis factor is a type of weighted crest factor. This is an effective characteristic value for assessing the number

of peaks (steepness, concavity) in a signal. The value is around zero for a normal distribution. A positive value shows

a more leptokurtic distribution and a negative value a more platykurtic distribution.

It can be used for detecting bearing damage or poor lubrication.

Figure 15: Kurtosis

3.3Area of use and application examples

The areas of application are manifold, but usually in areas where very high failure or downtime costs arise.

An additional criterion is machines that require expensive components, therefore resulting in high service costs.

This area of application isn't restricted to monitoring a machine or system, but can also be used for process optimiza-

tion or improvement.

The goal with condition monitoring is to increase machine availability, lengthen maintenance cycles and

also raise the process output (quality and quantity).

Additional information can be found in the data sheet of the associated module.

3.3.1Wood industry

Application

Wood drying

Goals

Detect bearing damage

Detect accumulation of dust

Benefits

Prevent production downtime

Reducing the maintenance costs

Prevention of fires caused by ignition of sawdust on hot bearings

Figure 16: Wood industry

## Page 15

CONDITION MONITORING BASED ON VIBRATION MEASUREMENT15

3.3.2Packaging industry

Application

Label printing machine

Goals

Detect bearing damage

Extension of maintenance intervals

Benefits

Reduce unplanned downtime

Ensure correct contact pressure setting

Ensure optimal print quality (resonance detection)

Figure 17: Packaging industry

3.3.3Textile industry

Application

Industrial washing machines

Goals

Early detection of imbalance

Monitor damage to bearings and suspension system

Benefits

Competitive advantage through faster wash cycle

Save energy and reduce wear on machine

Figure 18: Textile industry

3.3.4Metalworking industry

Application

Milling machine

Goals

Detect bearing damage

Detecting the amount of mechanical wear

Resonance point monitoring

Benefits

Prevent production downtime

Reducing the maintenance costs

Consistent production quality

Figure 19: Metalworking industry

## Page 16

16CONDITION MONITORING BASED ON VIBRATION

MEASUREMENT TM280

3.3.5Plastics industry

Application

Blow molding machines

Goals

Monitor tool closing process

Monitor machine lubrication

Benefits

Competitive advantage through improved processing quality

Machine availability increased

Reduced maintenance cycles

Figure 20: Plastics industry

## Page 17

B&R VIBRATION MEASUREMENT SYSTEMS17

4B&R vibration measurement systems

New options result from the integration of vibration measurement systems into the automation system. By setting

basic limit values, warnings or an alarm can be issued or even logical correlations with other parameters can be used,

such as the load, speed or shape of the trend curve.

4.1Condition monitoring module - X20CM4810

The X20CM4810 was developed specifically for condition monitoring based on vibration measurement. It is based

on integrated data analysis (FFT and envelope analysis) of the input signal and provides different parameters and

characteristic values.

Figure 21: Condition monitoring by B&R

Among other things, it can be used to measure imbalance, bearing damage, misalignment or periodically occurring

impact on a machine. Acceleration sensors are used to measure vibrations. In addition, the module converts the mea-

sured acceleration values into speed values.

With the resolution provided by module, evaluating the acceleration values provides sufficient informa-

tion, even in the lower frequency domains. B&R therefore recommends using only the acceleration values.

For this reason, the calculation of the speed values is disabled by default.

Additional details and information can be found in the data sheet of the module.

4.1.1Signal processing

The module supports wanted signals up to 10 kHz and uses a maximum sampling rate of 51.5625 kHz. As previously

mentioned, it has an integrated data analysis (based on FFT and envelope analysis).

## Page 18

18CONDITION MONITORING BASED ON VIBRATION

MEASUREMENT TM280

Figure 22: Signal processing block diagram

4.1.2Filter configuration

The module has a number of configurable filters.

High-pass filter

There is an adjustable high-pass filter for the entire module.

Possible settings are:

500 Hz

•

1 kHz

•

2 kHz

•

Low-pass filter for raw signal

For each channel, there is an adjustable low-pass filter for the raw signal that can be used to control the sampling

frequency. Reducing the maximum frequency allows the frequency resolution in the spectrum to be increased. In ad-

dition, the lowest frequency of the raw signal that still has to be evaluated is usually defined automatically.

Maximum frequencyFrequency resolution in spectrumMinimum frequency

10000 Hz3.1471 Hz9.441 Hz

5000 Hz1.5736 Hz4.720 Hz

2000 Hz0.6294 Hz1.888 Hz

1000 Hz0.3147 Hz0.944 Hz

500 Hz0.1574 Hz0.472 Hz

100 Hz0.0629 Hz0.188 Hz

Low-pass filter for envelope signal

For each channel there is an adjustable low-pass filter for the envelope signal. Reducing the maximum frequency allows

the frequency resolution in the spectrum to be increased. In addition, the lowest frequency of the envelope signal that

still has to be evaluated is usually defined automatically.

Maximum frequencyFrequency resolution in spectrumMinimum frequency

2000 Hz0.6294 Hz1.888 Hz

1000 Hz0.3147 Hz0.944 Hz

500 Hz0.1574 Hz0.472 Hz

200 Hz0.0629 Hz0.188 Hz

## Page 19

B&R VIBRATION MEASUREMENT SYSTEMS19

4.1.3Buffering the time signal

The values that have been sampled are stored in the mod-

ule's internal buffer. The size of the buffer is constant and

can store 8192 measured values. This results in the ra-

tio between the sampling frequency and the duration of

measurement.

Duration of measurement = Buffer size / Sampling fre-

quency

Since the values stored are dependent on the config-

ured sampling frequency and not the hardware-based

sampling frequency, not all values that are measured are

stored. At a measurement duration of 318 ms, every sec-

ond value is stored; at a duration of 15.9 ms, every hun-

dredth value is stored.

Figure 23: Ring buffer

Maximum frequencySampling frequencyMeasurement duration

10000 Hz25781 Hz0.3178 s

5000 Hz12891 Hz0.6355 s

2000 Hz5156 Hz1.5888 s

1000 Hz2578 Hz3.1775 s

500 Hz1289 Hz6.3550 s

200 Hz516 Hz15.8875 s

Table 2: Overview of possible measurement times

Every 300 ms, a copy from the buffer is forwarded for processing, i.e. every 300 ms the parameters and

characteristic values are updated.

There is also the possibility to upload the module-internal buffer of the module.

4.1.4Parameters and characteristic values

The module automatically calculates parameters and characteristic values from the ring buffer via integrated data

analysis and they are updated every 300 ms. How current the data is for the analysis can be influenced via the sampling

frequency (or low-pass filter for raw signal).

For the calculation, the following sources are differentiated between:

Raw signal

•

High-pass filter raw signal (high frequency)

•

Envelope signal

•

Hardware \ X20 system \ X20 modules \ Other modules \ X20CM4810 \ Register description \ Values

Raw signal

This is the raw signal for vibration acceleration or speed between the configured minimum and the maximum frequen-

cy (low-pass filter raw signal).

## Page 20

20CONDITION MONITORING BASED ON VIBRATION

MEASUREMENT TM280

The following parameters and characteristic values are determined for this area:

RMS value

•

Peak value

•

Crest factor

•

K(t) value

•

Kurtosis

•

High-pass filter raw signal (high frequency)

This is the high-pass filter raw signal for vibration acceleration in the frequency domain between the set high-pass

filter value and 10 kHz.

The following parameters and characteristic values are determined for this area:

RMS value

•

Peak value

•

Crest factor

•

Envelope signal

This is the envelope signal of the vibration acceleration or speed between the configured minimum up to the maximum

frequency (low-pass filter envelope signal).

The following parameters and characteristic values are determined for this area:

RMS value

•

4.1.4.1Exercise – Characteristic values and parameters

While doing the exercise, the training structure is moved and the impact on the parameters observed.

Exercise: Characteristic values and parameters

1)Create a new Automation Studio project

2)Add training assembly hardware

3)Switch on the IEPE sensor supply for the channels via the I/O configuration

Figure 24: Activate IEPE sensor supply

## Page 21

B&R VIBRATION MEASUREMENT SYSTEMS21

The sensor needs to be supplied with power in order to take measurements. The option of turning the

power supply on and off exists in order to allow a sensor to be connected to two different channels.

One channel supplies the sensor with power (IEPE current supply on), so the supply should be turned

off for the second channel (IEPE current supply off).

4)Offline installation or project installation and startup of the training structure

5)Open the I/O mapping of the condition monitoring module

6)Observe characteristic values

7)Move the training structure by hand, deal an impact and observe characteristic values

4.1.5FFT and frequency bands

An FFT algorithm converts the measurements in the ring buffer into a frequency spectrum with spectral lines. The

frequency resolution should be set as high as possible to obtain the most precise results possible.

Frequency resolution [Hz] = 1 / measurement time [s]

8192 measurements in the time domain produce 4096 (8192 / 2) spectral lines.

Restrictions can be made in the frequency domain using frequency bands in order to be able to better identify certain

fault events.

Different damage frequencies can be monitored in parallel by simultaneously using several frequency bands.

Figure 25: Frequency bands

Different configurations for up to 32 frequency bands are available.

Possibilities:

Broadband RMS value

•

Speed-dependent RMS value

•

Noise

•

Hardware \ X20 system \ X20 modules \ Other modules \ X20CM4810 \ Register description \ Frequency

bands

Broadband RMS value

This is the root mean square between the configured minimum frequency up to the configured maximum frequency.

## Page 22

22 CONDITION MONITORING BASED ON VIBRATION
MEASUREMENT TM280
The following signal sources are available for selection:
Raw vibration acceleration signal
•
Raw velocity signal
•
Enveloped vibration acceleration signal
•
Enveloped velocity signal
•
The harmonic frequencies of the configured frequency domain can also be calculated as well.
Speed-dependent RMS value
This is the root mean square of a movable window. The standardized switching frequency and a tolerance have to be
configured to define the window. This window is coupled to a dynamic velocity.
The window results as follows:
Minimum frequency = (speed * switching frequency) - tolerance
•
Maximum frequency = (speed * switching frequency) + tolerance
•
The following signal sources are available for selection:
Raw vibration acceleration signal
•
Raw velocity signal
•
Enveloped vibration acceleration signal
•
Enveloped velocity signal
•
The harmonic frequencies of the window can also be included in the calculation.
Noise
This is the noise of a quadrant where the configured maximum frequency (low-pass filter for raw signal, low-pass filter
for envelope signal) is divided by 4 and configured for whichever of these 4 quadrants the noise has to be determined.
The following signal sources are available for selection:
Raw vibration acceleration signal
•
Raw velocity signal
•
Enveloped vibration acceleration signal
•
Enveloped velocity signal
•
This analysis allows slippage to be effectively measured, for example. The higher the friction, the more
noise that is created.
4.1.6 Analog input
The acceleration sensor input can also be used as an analog input with different special functions.
The following functions are available:
Normal input function
•
Characteristic value calculation
•
in continuous mode
°
in trigger mode (single shot)
°
Hardware \ X20 system \ X20 modules \ Other modules \ X20CM4810 \ Register description \ Analog
input functions

## Page 23

B&R VIBRATION MEASUREMENT SYSTEMS23

Normal input function

The last 8 measured values are always averaged and the result is made available. Here the direct input signal (raw

signal) is used.

Characteristic value calculation

One of the following parameters is determined:

Mean value

•

Peak value

•

RMS value

•

Crest factor

•

Available as a signal source for the parameters, there is either the raw signal filtered to the configured sampling fre-

quency (low-pass filter for raw signal) or the raw signal filtered to 10 kHz. In addition, you can specify for how many

sampled values to calculate the respective safety characteristic. The time between 2 samples depends on the config-

ured maximum frequency (low-pass filter for raw signal).

Continuous mode

The function can be enabled via the start signal – measurement is carried out continually.

Trigger mode (single shot)

The function can be enabled via a trigger – only one measurement is carried out.

4.1.7Functionality explained with the example of a fan

Initial situation

The condition monitoring module uses an acceleration sensor to monitor a rotating fan. Damage to the fan is causing

an imbalance in its rotation.

Measurement with sensors

The mechanical imbalance is measured by an acceleration sensor and converted into an analog signal.

Figure 26: Raw acceleration sensor signal

Signal evaluation and calculation in the module

Using an FFT algorithm, the frequency spectrum is calculated from the raw signal. This frequency spectrum illustrates

the disturbance frequencies caused by the imbalance. The module can be configured to read specific values that help

determine the nature of the imbalance.

## Page 24

24CONDITION MONITORING BASED ON VIBRATION

MEASUREMENT TM280

Figure 27: Frequency spectrum

Module settings in Automation Studio

In the first step, the parameters and characteristic values provided by the module can be drawn on.

For a more detailed analysis, the frequency bands can be used as help. The configured damage frequency is used to

observe the frequency domain of the imbalance.

The switching frequency (excitation frequency) corresponding to the rotary frequency of the imbalanced

rotor.

The measured values are output as vibration acceleration [g] or vibration velocity. They provide information about the

magnitude of the imbalance. The larger the measured value, the greater the imbalance.

The measured values are compared against the machine's limit values. This is where long-term recording of data over

the entire lifecycle of the machine becomes very important.

Figure 28: Frequency spectrum using frequency bands

Interpreting the results

The measured values provide insights into the condition of the machine. They can be used to determine whether there

is any damage and what type of damage it is. The severity of the damage can only be judged in relation to the machine's

limit values.

This is why it is so essential to monitor and record data for a machine continuously over its entire life span. With

historical trend recordings, the machine can be permanently monitored to detect the signs of developing damage as

quickly as possible. Broadband values provide a good overview of the general condition of the machine, and should

therefore always be monitored.

It can be very helpful to set warning and alarm limits to ensure that detection is prompt and reliable.

## Page 25

B&R VIBRATION MEASUREMENT SYSTEMS25

Figure 29: Interpreting the results

## Page 26

26CONDITION MONITORING BASED ON VIBRATION

MEASUREMENT TM280

5Practical applications for damage de-

tection

Using the training hardware, some practical error scenarios can be run through and analyzed. The structure comprises

one motor, two self-aligning ball bearings on the motor shaft, and a fan as the mechanical system.

Figure 30: Structure of the training hardware

Two condition monitoring modules are used and three acceleration sensors per module are connected.

The sensors nearer to the motor are connected to the 1st module (Bearing A), the sensors nearer to the fan are con-

nected to the 2nd module (Bearing B).

Three damage scenarios can be simulated via the structure:

Imbalance

•

Periodic impact

•

Bearing damage

•

Additional practical applications can be found in the data sheet of the module.

5.1Analysis of imbalances

An imbalance on the mechanics can arise due to several reasons – e.g. in the case of a fan, it may be dirty or damaged.

Imbalance leads to vibrations and increased wear, particularly at high speeds, which is why counterweights are applied

to compensate for this as counterbalance. In practice, it is never possible to fully compensate for this, meaning each

rotating body always has residual imbalance.

The X20CM4810 condition monitoring module can only measure the intensity of the imbalance, not its

position. For this reason, it cannot be used for balancing.

## Page 27

PRACTICAL APPLICATIONS FOR DAMAGE DETECTION27

A possible imbalance can be recognized via the parameters and characteristic values calculated by the module.

For an exact analysis, a frequency band should be implemented where the relationship of the imbalance is visible in

the frequency spectrum in the table below.

Frequency in raw signal spectrumFrequency in envelope spectrum

1 x fn-

Table 3: Frequency of imbalance

fn ... nominal speed

Figure 32: Frequency spectrum imbalanceFigure 31: Raw signal imbalance

Error simulation

Magnetic weights can be attached to the blades to simulate a mechanical imbalance.

5.1.1Exercise – Measuring an imbalance

This exercise is performed directly on the training structure. The first step is to take measurements on a balanced

fan in order to obtain controlled values that can be used for comparison. The imbalance is simulated by attaching

magnetic weights to the fan.

When an X20CM4810 is added, each channel (4 channels per module) is assigned 8 frequency bands. The first frequency

band assigned to each channel (FrequencyBand01, 09, 17, 25) is already configured to measure imbalance.

Exercise: Measuring an imbalance

1)Move the motor via the motion test window or via mapp component MpAxis

"Controller - Switch on" in the motion test window or "Power" via the MpAxis component

°

"Homing" in the motion test window or "Home" via MpAxis component

°

"Basis Movement - Positive" in the motion test window or "Move Velocity" via the MpAxis component

°

2)The current speed of the motor in Hertz must be transferred to the module via the I/O mapping of the module.

The channel ActSpeed01 to 04 must either be forced to the current speed or a corresponding variable

must be connected to the channel.

## Page 28

28CONDITION MONITORING BASED ON VIBRATION

MEASUREMENT TM280

Figure 33: Current speed of the motor - ActSpeed01 to ActSpeed04 correspond to the 4 different channels

If the axis moves e.g. at 5000 units/s and 1000 units represent a revolution, then the motor runs at

300 rpm.

This results in a current speed of 5 Hz  ActSpeed [Hz] = 5→

Hardware \ X20 system \ X20 modules \ Other modules \ X20CM4810 \ Register description \ General

registers \ ActSpeed

3)Check the frequency bands configured for imbalance (FrequencyBand01, 09, 17) and make a note of the values

Since an imbalance has not yet been simulated, the values for a balanced machine are measured here.

In the real world, this is done during commissioning with the machine configured optimally. These

results are then used for comparison when measuring imbalances later on.

4)Stop motor to remeasure with imbalance

5)Attach magnetic weight to fan in order to simulate an imbalance

6)Start motor and set ActSpeed  Repeat measurement→

7)Compare results against values from balanced machine and interpret

8)Increase speed of motor  Adjust parameter for ActSpeed [Hz]→

9)Repeat imbalance measurement - Compare the values and interpret the results

The higher the speed, the greater the possibility of imbalance.

5.2Analyzing periodic impacts

There are several reasons why an impact on the mechanics can arise, e.g. in the case of a fan, defective mechanical

parts or foreign bodies can cause a periodic impact by touching the fan.

In addition, components strike their counterparts on each revolution. This in turn causes the attachment parts to

vibrate at their natural frequency. Envelope analysis can be used to separate the causes of impact.

## Page 29

PRACTICAL APPLICATIONS FOR DAMAGE DETECTION29

It also looks very similar to individual parts striking each other when parts in the machine are loose. These

two causes of damage cannot be analyzed separately.

Frequency in raw signal spectrumFrequency in envelope spectrum

-1 x fn

Table 4: Frequency of a periodic impact

fn ... nominal speed

Figure 34: Raw periodic impact signal

Figure 35: Frequency spectrum periodic impact

Error simulation

A small plate is mounted so that it brushes the fan blades in order to simulate a periodic impact on the mechanical

structure.

5.2.1Exercise – Measuring a periodic impact

This exercise is performed directly on the training structure. The first step is to take measurements on a fan without any

impact events in order to obtain controlled values that can be used for comparison. The periodic impact is simulated

using a small metal or plastic plate mounted to the right of the fan so that it comes in contact with the rotor blades.

Frequency bands need to be configured for measuring a periodic impact.

Exercise: Measuring a periodic impact

1)Configure frequency bands 03, 11, 19 via the module configuration for measuring a periodic impact

## Page 30

30CONDITION MONITORING BASED ON VIBRATION

MEASUREMENT TM280

Figure 36: Configuring frequency bands

The fan possesses 9 rotary blades, which results in 9 impacts per revolution.

The value of "Normalized damage frequency at 60 rpm" must be adjusted here  Value to be set =→

900 (where the unit is specified as [1/100])

2)Move the motor via the motion test window or mapp component MpAxis (see exercise "Measuring an imbal-

ance")

3)Current speed of the motor in Hertz must be transferred to the module again (see exercise "Measuring an imbal-

ance")

4)Check the frequency bands configured for periodic impact FrequencyBand03, 11, 19 and make a note of the val-

ues  A value between 1 - 3 mg should be provided→

5)Attach plate to ETA and repeat measurement with impact

Don't set the speed (motor speed) too high, because this could produce too much noise.

6)Renewed consideration of the values of the frequency bands

Interpreting the results

°

Measurement with condition parameters  Peak, Crest Factor Raw, Kurtosis Raw→

°

Additional exercise: Measuring using analog input functionality

1)Switch on analog-input functionality of the channels via the module configuration  Trigger mode→

## Page 31

PRACTICAL APPLICATIONS FOR DAMAGE DETECTION31

2)Move the motor via the motion test window or mapp component MpAxis (see exercise "Measuring an imbal-

ance")

3)Start recording with Trigger, perform measurement without plate  AnalogInput0x should return a low value→

4)Attach plate to ETA and repeat measurement with impact

Don't set the speed (motor speed) too high, because this could produce too much noise.

5)Renewed consideration of the values

6)Consideration with different configurations

Which parameter calculation produced the most helpful results?

°

What effect does configuring the samples have?

°

5.3Analysis of bearing damage

Bearing damage also causes a periodic impact signal. Damage to the inner or outer bearing ring or the balls themselves

also causes a periodic impact during rotation.

Many types of bearing damage are caused by imprecisions in the bearing surface such as material damage or mi-

cro-cracks. These pits are rolled over by the roller elements and cause impacts on the roller bearing and its attachment

parts.

The mechanism is very similar to the striking of a bell: The clapper strikes the body of the bell, and the

bell starts vibrating at its natural frequency.

In the case of the bearing, each time the roller moves over the damaged area, it is like striking the clapper

and the roller parts and attached parts start to vibrate.

Outer ring damage

In most cases the outer ring remains stationary while the inner ring turns. This gives a clearly defined fixed load zone.

Most damage occurs in this load zone. If pitting or other surface damage occurs, it is rolled over by the rolling elements.

Vibrations occur when rolling over, which can be measured on parts of the housing.

Frequency in raw signal spectrumFrequency in envelope spectrum

-1 x fa, 2 x fa, 3 x fa ...

Table 5: Frequency of outer ring damage

fa ... frequency of the outer ring damage

## Page 32

32CONDITION MONITORING BASED ON VIBRATION

MEASUREMENT TM280

Inner ring damage

Any inner ring damage that occurs travels with the rotating shaft. Due to the different rotary speeds of the revolving

roller elements and the inner ring, pronounced modulations occur. As a result, inner ring damage frequencies are usu-

ally shown with sidebands in the spectrum.

Frequency in raw signal spectrumFrequency in envelope spectrum

-i x fi +/- i x fn

Table 6: Frequency of inner ring damage

fi ... frequency of the inner ring damage

fn ... nominal speed

Details of bearing damage frequencies are normally provided by the manufacturer and can be taken from

the data sheets for the bearings.

Figure 38: Raw signal – Frequency spectrum

Figure 37: Raw signal – Bearing damage

Figure 39: Envelope signal – Bearing damage

Figure 40: Envelope signal – Frequency spectrum

Error simulation

One of the two self-aligning ball bearings is defective. The data sheet of the manufacturer can be used for a detailed

analysis.

5.3.1Exercise – Measuring bearing damage

Frequency bands need to be configured to measuring bearing damage.

## Page 33

PRACTICAL APPLICATIONS FOR DAMAGE DETECTION33

Exercise: Measuring bearing damage

1)There are three relevant values

Damage frequency on inner ring

°

Damage frequency on outer ring

°

Damage frequency directly on the ball

°

All three 3 potential types of damage need to be monitored  It's best to configure a separate fre-→

quency band for each value.

Figure 41: Bearing data sheet from the manufacturer

Special case: Self-aligning ball bearings

Since the bearings are self-aligning ball bearings, when there is damage to the bearing that generates

an impact on both passing balls you need to select .2x the specified damage frequency

2)Configure three frequency bands per channel (e.g. 04 - 06, etc.) via the module configuration for measuring a

bearing damage signal.

## Page 34

34CONDITION MONITORING BASED ON VIBRATION

MEASUREMENT TM280

Figure 42: Configuring frequency bands

The value of "Normalized damage frequency at 60 rpm" must be adjusted here. [1/100] is again spec-

ified as the unit.

3)Move the motor via the motion test window or mapp component MpAxis (see instructions under "Workshop 2:

Measuring a mechanical imbalance")

4)Current speed of the motor in Hertz must be transferred to the module again (see "Workshop 2: Measuring a me-

chanical imbalance")

5)Find out:

How does increased speed affect the results?

°

Which bearing is defective  Bearing A or Bearing B?→

°

Where is the damage on the defective bearing  On the inner ring, outer ring or directly on the ball?→

°

## Page 35

CONDITION MONITORING IN A REAL MACHINE ENVIRONMENT35

6Condition monitoring in a real machine

environment

Basically three central tasks can be identified for the implementation of con-

dition monitoring in the real machine environment.

Collecting data

•

Recording

•

Reporting

•

mapp Technology is used to implement individual tasks since many useful

basic functions are present there.

Figure 43: mapp Technology

A condition monitoring solution can be a fixed component of a series-produced machine. However, a solution can also

be developed as a standalone product, which is subsequently retrofitted to existing systems or just sold separately.

The measured values collected can deviate from machine to machine. Detailed knowledge about the ma-

chine is necessary to be able to make a classification in relation to the limit value or alarm, e.g. via the

mechanical design. During vibration monitoring, for example, this is the only way to define which vibra-

tion values are critical for the machine.

Mathematical procedures, e.g. the standard deviation, for analyzing the measured values collected can

be used. Deviations can be determined this way, but it is not necessarily possible to relate this to the

state of the machine.

If possible, a comparison of good and bad test cases should be carried out.

If vibration information should serve as the basis for determining the state of a machine, then, to begin,

the raw data should always be analyzed too - please see  of the raw data.

By doing so, basic information and interesting frequency domains can be determined and the configu-

ration of the module adjusted correspondingly – e.g. recording duration, frequency resolution of the FFT,

frequency bands, etc.

6.1Collecting data

To begin, relevant information/physical quantities must be collected. Corresponding sensors are built into the ma-

chine for this.

## Page 36

36CONDITION MONITORING BASED ON VIBRATION

MEASUREMENT TM280

Figure 44: Collecting information

By connecting sensors to the associated I/O modules, information is treated for post-processing. Depending on the

range of functions of the I/O module, certain analysis options are available directly via the module.

If, for example, the imbalance of a machine should be monitored, then a vibration sensor is used. In com-

bination with the condition monitoring module, the imbalance at the current speed can already be de-

termined purely via the configuration of the frequency bands.

Exercise: Detecting an imbalance

1)Configuration of the three channels of the two condition monitoring modules for imbalance

2)Transmit the current speed of the motor to the modules

6.2Recording

In addition to collecting information, the information should also be saved for future traceability. In some circum-

stances, a relationship between different events can be created on the machine.

Figure 45: Recording information

## Page 37

CONDITION MONITORING IN A REAL MACHINE ENVIRONMENT37

Information can be logged quickly and easily in a file via the data logging func-

tionality (MpData).

Several variables for storage can be registered and the save interval defined.

In addition, there are examples for implementation in the HMI application.

Figure 46: Data logging

Additional information and a detailed description can be found in the help documentation for MpData.

Exercise: Recording an imbalance

1)Connect frequency bands to variables via the I/O mapping

2)Record the frequency bands for monitoring the imbalance using MpData (CSV file)

3)Recording duration 1 sec

4)Vary the speed of the motor

Exercise: Analysis of the recorded data

1)Recorded data can be uploaded from the controller via FTP.

2)Open file with Excel and generate diagram

6.3Reporting

The collected information should also be monitored for compliance with limits. If there is a violation of a limit, then

this should be reported and also logged.

Figure 47: Reporting the status

## Page 38

38CONDITION MONITORING BASED ON VIBRATION

MEASUREMENT TM280

Messages for limit violations can be triggered via the alarm system (MpAlar-

mX).

Different properties and language-dependent texts can be defined for the

messages.

In addition, there are examples for implementation in the HMI application.

Figure 48: Alarm system

Additional information and a detailed description can be found in the help documentation for MpAlarmX.

Monitor limits and trigger messagesAdditional exercise:

The imbalance values should be monitored for compliance with limit values (e.g. standard deviation,

mean value, etc.) and a message should be triggered for any violations.

Messages are not only triggered or displayed locally but can also be sent via

the messaging system (MpTweet) in the form of a text message.

A notification hierarchy and language-dependent texts can be defined.

A message can be acknowledged via response message.

Figure 49: Messaging system

Additional information and a detailed description can be found in the help documentation for MpTweet.

## Page 39

APROL CONMON39

7APROL ConMon

APROL ConMon solution is based on the APROL process control system.

With it, B&R offers a solution for measuring, recording and evaluating all rel-

evant condition parameters to provide optimal support for the continual im-

provement process.

APROL ConMon can be used as a standalone solution or integrated into ex-

isting APROL process control systems.

It can be customized to monitor a single machine, an entire factory, a building,

a process or a plant.

Figure 50: APROL ConMon

Figure 51: APROL ConMon system structure

Condition parameters are collected using different I/O modules. A comprehensive library makes powerful control mod-

ules available.

Numerous ready-made reports are available in APROL. Separate reports can be created and interactive dashboards

shown.

## Page 40

40CONDITION MONITORING BASED ON VIBRATION

MEASUREMENT TM280

8Summary

Condition monitoring involves constantly monitoring the health of a machine over years of operation. The availability,

maintenance and service life of a machine can be considerably improved and optimized through the application of a

condition monitoring solution.

There is a wide variety of physical parameters that, when monitored constantly, can provide key insight into the con-

dition of a machine and the efficiency with which it is operating. B&R's condition monitoring modules make it possible

to systematically monitor and analyze the health and efficiency of a machine using vibration measurement.

The module can be used to identify damage and potential sources of error such as imbalance, misalignment, bearing

damage and periodic impact, all of which generate measurable vibrations throughout the mechanical structure of the

machine.

Figure 52: Condition monitoring

This training module provides information about the use of condition monitoring in all types of industries. Signal

evaluation based on vibration analysis is described in detail and applied in the course of various workshops in order

to identify imbalance, periodic impacts and bearing damage.

## Page 41

APPENDIX41

9Appendix

9.1Uploading raw data

It's possible to upload the raw data saved in a buffer on the module using the AsIOVib library. Either the current mea-

surement (8k) or the last eight measurements (64k) can be uploaded.

Programming \ Libraries \ Direct I/O access \ AsIOVib

Information can be quickly and easily saved to a document via the recipe func-

tionality (MpRecipe).

Several variables for storage can be registered and the file type (CSV or XML)

defined.

In addition, there are examples for implementation in the HMI application.

Figure 53: Saving the data

Additional information and a detailed description can be found in the help documentation for MpRecipe.

Upload and save raw dataAdditional exercise:

The buffer of the raw signal (X value, Y value) of the acceleration must be uploaded and saved via

MpRecipe in a CSV file.

The data can be shown in Excel (data still has to be rearranged) or processed further.

## Page 42

42CONDITION MONITORING BASED ON VIBRATION

MEASUREMENT TM280

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

V3.0.0.1 ©2023/12/06 by B&R, All rights reserved.

All registered trademarks are the property of their respective owners.

Subject to technical changes without notice.