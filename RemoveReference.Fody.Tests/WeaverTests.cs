using System.Linq;
using NUnit.Framework;
using Fody;
using RemoveReference2.Fody;

namespace RemoveReference.Fody.Tests;

[TestFixture]
public class WeaverTests
{
    [Test]
    public void ValidateReferenceIsRemoved()
    {
        var weavingTask = new ModuleWeaver();
        var testResult = weavingTask.ExecuteTestRun("AssemblyToProcess.dll");
        var assemblyNames = testResult.Assembly.GetReferencedAssemblies();
        Assert.IsFalse(assemblyNames.Any(x => x.Name == "System.Runtime"));
    }
}
