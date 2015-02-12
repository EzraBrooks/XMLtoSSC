XMLtoSSC
===
XMLtoSSC is a C# console application for interpreting XML into commands for a a Lynxmotion SSC-32 servo controller.

Dependencies:
* .NET Framework 4.0 or higher (or the Mono equivalent if it supports all the namespaces used)
* [SSCSharp](https://www.github.com/EzraBrooks/SSCSharp), my C# SSC-32 control class, must be added to the assembly environment to build this application.

Sample XML command file:
```
<servocontrol port="COM3">
    <!-- Prints "Hello World!" sans quotes to the console -->
    <print>Hello World!</print>
    <!-- Sets all servos on an AX12A Robotic Arm (0-5) to position 1500 -->
    <movement servo="all" position="1500"/>
    <!-- Sets servo 0 to position 0 -->
    <movement servo="0" position="0"/>
    <!-- Sets servo 1 to position 0 over 1000ms -->
    <movement servo="1" position="0" time="1000"/>

</servocontrol>
```