# Example Mod

This mod is to try out various things for creating a mod.  The plan is to use
github and create a pull request for each thing I try out so that the code
can be separated.

Things to try:

* Console commands
* Writing to console
* Saving configurations
* Notifications
* Hotkeys (like Automaton and CNEI)
* Displaying and closing a simple window
* Window interactivity
* Add-on for map overlay

## Getting started

First, create a new directory somewhere on your computer.  Copy the contents of your
Cryofall game directory (in steam: right-click on game, select manage, select 'browse
local files') to a new directory somewhere else on your computer.  

Then create a 'Data' folder and inside that create a 'Mods' folder.  Then remove the
'steam' file from the main directory (with 'CryoFall Client.exe').  Then go into the
'Core' directory and double-click on 'Extract Core and Mods.cmd'.  NOTE: you should
have created the `Data\Mods` folder first.  This extracts Core.cpk into a folder of
the same name (and Editor.mpk if you used the cryofall editor).

Now in the `Data` folder, create a file called 'ModsConfig.xml' with this content:

```xml
<?xml version="1.0" encoding="utf-8" standalone="yes"?>
<mods>
  <unpacked_mod>
    <mod_id>core_1.0.0</mod_id>
    <is_core_mod>1</is_core_mod>
    <path>Core/Core.cpk</path>
  </unpacked_mod>
  <unpacked_mod>
    <mod_id>ExampleMod_0.0.1</mod_id>
    <is_core_mod>0</is_core_mod>
    <path>Data/Mods/ExampleMod</path>
  </unpacked_mod>
</mods>
```

If you used the editor, add this:

```xml
  <unpacked_mod>
    <mod_id>editor_1.0.0</mod_id>
    <is_core_mod>0</is_core_mod>
    <path>Core/Editor.mpk</path>
  </unpacked_mod>
```

Finally, clone this repo into `Data/Mods`, creating the folder 
`Data/Mods/ExampleMod`.  Now the mod should load when you restart
the game.  When you make changes, the game should recompile
automatically.  For this sample with no actual changes, the only
way to tell it is running is to check the logs.  In the `Data/Logs`
folder, open the most recent `Client_CryoFAll_xxx.log` file and look
for something like this:

```
23.11.21 14:29:50.642 [IMP] Real file system initialized successfully:
Core mod: Core (D:/git/CryofallMod/Core/Core.cpk)
Mods list:
   * TestModPleaseIgnore (D:/git/CryofallMod/Data/Mods/TestModPleaseIgnore)
   * ExampleMod (D:/git/CryofallMod/Data/Mods/ExampleMod)
Total files count: 9278
23.11.21 14:29:50.643 [IMP] Active core/mods list: 
  * core - ClientServer v1.0.0 (from folder "D:/git/CryofallMod/Core/Core.cpk")
  * ExampleMod - Client v0.0.1 (from folder "D:/git/CryofallMod/Data/Mods/ExampleMod")
```

The main thing I added for this to a bare new project using the SDK is this
readme and the `.gitignore` file:


## Updates

1. Console command that acts like help: `em`


## Ideas for useful mods:


### 1. Data export

For like CNEI, extract all data on mobs, items, recipes, buildings, etc.
for use in a website.


### 2. Pragma sensor helper

It would not be *super* useful because the range is quite varied and there
are 5 levels of sensor readings.  You would have to listen to the ping and
pong sounds and detect the time interval between.  It could get really
confusing if receiving multiple pongs (is that possible?).  

My idea would be to start when you activate it.  If you go more than 20
seconds without receiving a pong, then it clears it.  Timer starts on
receiving a 'ping'.   When you receive the pong, it clears anything
outside that range of the custom 'bitmap' from your current location
using the maximum distance...



### 3. Navigation

It would be cool to be able to track locations.  Maybe with:

* Display on map
* Interface for organizing waypoints
* Auto-recognize coordinates posted in chats
* Share with party/faction/global
