# CityAR
 
Mobile AR app that detects floors in your environment and renders a city on the floor. 
City is rendered over the camera pass through wherever floors are detected.
City consists of just buildings at random positions in the floor, with randomized colors and dimensions within a certain range.
if the user taps on the screen at any position on a detected, a car will spawn.
if the user taps on a car or building, they will be destroyed.
buildings and cars have real world occlusion on supported devices.

## Use Instructions

once the app is installed on your device, give the app permission to access the phones camera, all you will need to do is slowly look around the environment with your phone to let it scan the environment, the city buildings will emerge as floor planes are detected by the app

## Install Instructions

application was made using Unity 6, version 6000.0.27f1, the latest long term support release from unity at time of writing, you will need to install that or later version to open and run the application locally.

application can be tested within the local editor using unity's XR simulator, read [here](https://docs.unity3d.com/Packages/com.unity.xr.arfoundation@6.0/manual/xr-simulation/simulation-getting-started.html) for instructions and details for how to use that, but it should allow you to move and look around, and click the mouse to simulate using the app on a phone in a real environment. 

to use the application outside the editor, you will need an ios or android device that is either arkit or arcore compatible, this application was only tested in editor and on an android mobile phone.

## Android

an android apk for this project can be found attached to the latest release from this repo, and can be sideloaded onto any compatiable android mobile phone
if you want to make your own buld see [these](https://docs.unity3d.com/Manual/android-BuildProcess.html) instructions

## IOS

if you want to run the app on a compatible ios device, you will need to make your own build, you will need a mac, and to follow [these](https://docs.unity3d.com/Manual/iphone-BuildProcess.html) instructions

## Crediting and Assets used.

models used for buildings and cars are from Kenny Assets, found [here](https://kenney.nl/assets/city-kit-commercial) and [here](https://kenney.nl/assets/car-kit)

used AR plane occlusion shader and AR plane material from the unity AR foundations sample project repo found [here](https://github.com/Unity-Technologies/arfoundation-samples) 
