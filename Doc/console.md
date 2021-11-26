# console

This feature will add VERY basic console commands.

Step 1 is to find an appropriate command in the core mod that we can
use as a basis.  I'm simply going to copy this file from the core
mod (where this mod is in `Data/Mods/ExampleMod`, the core mod should
be unpacked into `Core/Core.cpk`):

```
Scripts/ConsoleCommands/Console/ConsoleConsoleHelp.cs
```

And create a new folder `Scripts/ConsoleCommands` in my mod and put
the file there, renaming it and changing the name of the class:

```
Scripts/ConsoleCommands/ExampleMod/ConsoleConsoleExampleMod.cs
```
