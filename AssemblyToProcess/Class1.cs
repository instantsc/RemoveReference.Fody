using RemoveReference2;

[assembly: RemoveReference("System.Runtime, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
[assembly: AddReference("System.Runtime", "4.1.2.0")]

namespace AssemblyToProcess;

public class Class1
{
}