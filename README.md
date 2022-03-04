## This is an add-in for [Fody](https://github.com/Fody/Fody/)

Facilitates removing references in a compiled assembly during a build.

### Before (decompiled view of assembly references)

![Image](Images/Before.png)

### After (decompiled view of assembly references)

![Image](Images/After.png)

### Quick Start

1.  Install the RemoveReference2.Fody NuGet package for the project that builds your assembly

2. Add a RemoveReference attribute in your code for each assembly reference you want to remove from the compiled assembly:

        [assembly: RemoveReference("mscorlib, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e")]    
        [assembly: RemoveReference("System.Core, Version=2.0.5.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e")]    

3. Rebuild your project and enjoy!

## Nuget 

Nuget package http://nuget.org/packages/RemoveReference2.Fody

To Install from the Nuget Package Manager Console 
    
    PM> Install-Package RemoveReference2.Fody