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


(https://www.draw.io/?lightbox=1&highlight=0000ff&edit=_blank&layers=1&nav=1&title=CowParking.drawio#R7VxZc9s2EP41mkke3OGhw3q0fNUZt%2FFYbVr3DSJhCilJKABky%2Fn1xcUbFmWZFKkmM9aYXIIg8e23B3ZlD9zzaHNNwGr5G%2FZhOHAsfzNwLwaOYw8dZyB%2BLP9FSSbToRIEBPl6UCaYo%2B9QCy0tXSMf0sJAhnHI0Koo9HAcQ48VZIAQ%2FFwc9ojD4lNXIIAVwdwDYVX6F%2FLZUklPnUkm%2FxWiYJk82R5P1ZUIJIP1SugS%2BPg5J3IvB%2B45wZipo2hzDkMBXoKLuu%2FqlavpixEYs11uePj0%2BfOfyxt47Vx%2F%2BfR4FQ1v8eWJrdXzBMK1XrF%2BW%2FaSQBAQvF7pYZAwuDEBDxbJcKv6Yna6XM4TiCPIyAsfoic61Xdohrj69DmD27G0bJmDejjVQqBVHKQzZyjwAw2EGZRZROd3s6%2Bb68t%2F4i8v1snF%2Be%2Bzk3EFkznD3r9cdAGfkAf5wUyorQwUxyn2oZjZGriz5yVicL4Cnrj6zK2Dy5Ys4m9yYfNDASXiPDsLURBzGcNiQBW77Wor66MKcncw2hUY7yHw8yjecOEYRGLh8YKq9VsfZhgQMWwOCQLhR2G1BEcDQayrFcEe%2F%2BWt1ih%2BxFs0YNdroHmsNwnOBULboyqj7bFBFadtaaJq5PcwQJRBktcG43ha59xFceg13y9jgsMQRgIhx7rOHEHfMU%2BxrMN80hbmrsGxjkP%2B1BldgbgA4fjbWgQBhd0JVeCd8SExJhEIswH8KBC%2F726kjgBhQh9qVv6SamI1JBEvyPse1dxL34J17C1FxFhTJu2ZQqKodyQruCP4CVHEAx6Kg3nNyx%2BDlRhC7WGNZFiffcDYPxN5HD%2FzQkAp8opYwQ1if4uQ%2B8tInz3krlxsdDSWJy%2FJScxfPneTOH3IX8tuk2fJfW%2FUC8Vr4sF6H8ENOYCsPpJCv5Crbo31I4MaExmBIWDoqZjhmnSrn3CHkfT%2F5nwtDW%2FJDGrZ%2BqZ8Qlqax3ZLE1mliRQulYkk09JV70%2B%2BUTfka5pEya6ojkROr0jkWqWAXfYxu7LIHZUmcg7LotMf24X1i1W2XSLDcF%2FfVKLnsD3fZMa%2Funm6FQl7LHfcUQRiv4kUw7Cf35Eau6YYKXCHyDHM7zvZ00KpSLC3G%2B4rRmjXGKHZ4K0dwkbJzgxllqLNmhEx5B1bWNiVMTvTIpVG5f3xziFiWJrIPbAx7xAj2i6yjUclLEcVs0y3A3ldusmNzdtldYd89gRQKBcpChHSy9Etbu6dVba9nN9prUUcGkbTHkrtSR%2BxtILqjldcOKGy1i72u%2FZwtXl937xIBHMoyw1pwSjbnhdv4MtYGGTqbZT4JkYMgRBRoWm%2FUIAiWWVKnnshUtUnARPXEMFfRZ2fL2sFPfTIXTL3ioUyVVBTptqBNs0TJM0ritHRYIW2gT6nrbHHtAlqhT33kELWDHuSqRLinBdpswCydinZs18hs0cMsUedU6TaDmmJIn9A2hBD9EypZwEisKAF%2F7AXxRcKY4oFW2QmLQIs4ww5Dk6kwbw7Tky7z2iGdjGlMRQz3YkBlEYaXkZUkjXkUCk1DK0PsobsZ%2B2WYnS7U9HtY8%2BSnmlt0nNopI%2Bjt9gk2D3oLZpV8bPR9bPR1XczOWSjy2wlR9np2lpt2poY1NagnF1rUD97XU2UoJxOm11t8sjQ%2FjIPnHTJo8baXeXGRCWwt02kajW9N42JN4WE7hsTbtUm51DvRvsMpD0d9Q1JU4nih46uu3pFxcHjb9eW66r2gb3ivt8C%2BN%2F2GJN8p%2BdxeeyU%2B2J7MnA8KU104B6j04eKXCk36UGTMXnaUTUZnfp626FhrNbbWuoBzDDO%2F6HFu7pEHhSOgcPBPws%2BL4oDfvRtDYluAqxXPmBiRChBFKtQ57I2iwmIuSd8FzWaJ0FPG4mucyiG3MMQAtk4boAkabkY0YFuEynXUE%2BPhVhoUrgPCBRboGOgSvcdRff1knHDXLmWfeIZYEzavHULn%2BTfJzbAHDU1Wxa%2BwbBInxSqJx0DIbpvJ7o7fEWv9S9IjfuXvFR3Ff1PXtz6fN4IozNurTpgSo1bcTdZ81FEk88LysjaY1wT73E0uiYE8n4Gy6lzIakbP%2FOarnuaowyrm4G2sthyHEj01ESqQhUh%2BMTyCa6Aj%2B9SqUpaehKC3kaN7nOSoSl%2F%2FbErODr69LyGOCpXcKYliuxawalM1FgNkZ9m%2F%2BZBDc%2F%2BWYZ7%2BR8%3D)


## Issue 
### Azure IoT Provionning Profile 
Le client n'existe pas en python, faudras call en REST

https://github.com/Azure/azure-iot-sdk-python

CRUD Operation with X.509 Group Enrollment	✖️	Manages device enrollment using X.509 group enrollment with the service SDK.
