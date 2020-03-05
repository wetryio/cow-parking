# cow-parking
Citizen Of Wallonia Project

## Devices informations (Obsolete ?)

#### Children Device ID (Obsolete ?)
demoDeviceChildSensorHcSr

PK : VXl+Bgohb7GR6EbY57fKbvIqMRr0Ri1KceHRofiLNT4=
SK : EbClslecBoU6R6AVX/t91f8PuH6TeIqoco9izs7xmzw=
PCS : HostName=CowHub.azure-devices.net;DeviceId=demoDeviceChildSensorHcSr;SharedAccessKey=VXl+Bgohb7GR6EbY57fKbvIqMRr0Ri1KceHRofiLNT4=
SCS : HostName=CowHub.azure-devices.net;DeviceId=demoDeviceChildSensorHcSr;SharedAccessKey=EbClslecBoU6R6AVX/t91f8PuH6TeIqoco9izs7xmzw=


#### Children Device ID (Obsolete ?)
demoDeviceChildLightController

PK : hU6JqUfZhdramuZH5aIVvoD/AT5zObgBRLJfvblkNv8=
SK : JZ7ycOlWTBotweFS/wyeiEOWSfRYz38/7fUaXb5pQBk=
PCS : HostName=CowHub.azure-sevices.net;DeviceId=demoDeviceChildLightController;SharedAccessKey=hU6JqUfZhdramuZH5aIVvoD/AT5zObgBRLJfvblkNv8=
SCS : HostName=CowHub.azure-devices.net;DeviceId=demoDeviceChildLightController;SharedAccessKey=JZ7ycOlWTBotweFS/wyeiEOWSfRYz38/7fUaXb5pQBk=



### HC-SR04

https://thepihut.com/blogs/raspberry-pi-tutorials/hc-sr04-ultrasonic-range-sensor-on-the-raspberry-pi
https://www.emotion-tech.com/imprimante-3d-i3-metalmotion
https://tutorials-raspberrypi.com/raspberry-pi-ultrasonic-sensor-hc-sr04/

### CSharp Lover
https://github.com/unosquare/raspberryio (HC-SR04 Support)


### Iot HUB
https://docs.microsoft.com/en-us/azure/iot-hub/

### Deploy Edge
https://sandervandevelde.wordpress.com/2019/08/16/iot-edge-group-enrollments-using-symmetric-keys/


### Generate config.gz
https://www.raspberrypi.org/forums/viewtopic.php?t=195965

sudo modprobe configs


### Battery level in Raspi
https://github.com/rricharz/pi-top-battery-status



### Orientation IOT EDGE

https://docs.microsoft.com/en-us/azure/iot-edge/tutorial-csharp-module

Un module par sensor / action, cela crée un "genre" api pour les device edge, ex :

[Get]
deviceId/sensor/hc04

[Post]
deviceId/output/light

Possible de lancer un handle qui écoute la distance et envois au hub.

Avantage : 

 - Utilisation du EDGE
 - Utilisation de docker
 - Un module = 1 sensor / Single Responsability
 - 

Inconvénient : 
 - Un peu plus chaud a faire



### Flux (Ma vision)

1) On formate les devices, avec une image type.

2) Au lance du device, celui-ci génére un ID unique et est register dans un groupe "Admin Stock"

3) Quand un projet est crée, la personne peux "commander X" devices

4) A ce moment, le module de provisioning, vas réecrire le yaml de configuration du edge, pour se binder sur les informations du projet 
    a) Un enrollement group est crée pour le projet et celui-ci est utiliser pour les devices

5) Une dois réecris le module reboot

6) Les devices doivent à présent apparaitre dans le Enrollment Group du projet



## Issue 
### Azure IoT Provionning Profile 
Le client n'existe pas en python, faudras call en REST

https://github.com/Azure/azure-iot-sdk-python

CRUD Operation with X.509 Group Enrollment	✖️	Manages device enrollment using X.509 group enrollment with the service SDK.
