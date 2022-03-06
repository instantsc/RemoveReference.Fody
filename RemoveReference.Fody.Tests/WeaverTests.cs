using System;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using Fody;
using RemoveReference2;
using RemoveReference2.Fody;

namespace RemoveReference.Fody.Tests;

[TestFixture]
public class WeaverTests
{
    [Test]
    public void ValidateReferenceIsRemovedAndAdded()
    {
        var weavingTask = new ModuleWeaver();
        var testResult = weavingTask.ExecuteTestRun("AssemblyToProcess.dll");
        var assemblyNames = testResult.Assembly.GetReferencedAssemblies();
        Assert.AreEqual(1, assemblyNames.Count(x => x.Name == "System.Runtime"));
        Assert.AreEqual(new Version("4.1.2.0"), assemblyNames.Single(x => x.Name == "System.Runtime").Version);
    }

    [Test]
    public void ValidateAttributesAndReferencesAreRemoved()
    {
        var weavingTask = new ModuleWeaver();
        var testResult = weavingTask.ExecuteTestRun("AssemblyToProcess.dll");
        var assemblyNames = testResult.Assembly.GetReferencedAssemblies();
        Assert.IsFalse(assemblyNames.Any(x => x.Name == "RemoveReference2"));
        var attributes = testResult.Assembly.GetCustomAttributes();
        Assert.AreEqual(0, attributes.OfType<AddReferenceAttribute>().Count());
    }
}
