using System;
using System.Collections.Generic;
using System.Linq;
using Fody;
using Mono.Cecil;

namespace RemoveReference2.Fody
{
    public class ModuleWeaver : BaseModuleWeaver
    {
        public override void Execute()
        {
            RunRemoveReference();
            RunAddReference();
            RemoveReference();
        }

        private void RunRemoveReference()
        {
            var attributesToRemove = ModuleDefinition.Assembly.CustomAttributes
               .Where(customAttribute => customAttribute.AttributeType.Name == "RemoveReferenceAttribute")
               .ToList();
            var namesToRemove = new HashSet<string>(attributesToRemove.Select(x => (string)x.ConstructorArguments[0].Value));

            foreach (var customAttribute in attributesToRemove)
            {
                ModuleDefinition.Assembly.CustomAttributes.Remove(customAttribute);
            }

            var referencesToRemove = ModuleDefinition.AssemblyReferences
               .Where(x => namesToRemove.Contains(x.FullName))
               .Distinct()
               .ToList();

            foreach (var assemblyNameReference in referencesToRemove)
            {
                WriteInfo($"Removing reference to {assemblyNameReference}");
                ModuleDefinition.AssemblyReferences.Remove(assemblyNameReference);
            }
        }

        private void RunAddReference()
        {
            var attributesToRemove = ModuleDefinition.Assembly.CustomAttributes
               .Where(customAttribute => customAttribute.AttributeType.Name == "AddReferenceAttribute")
               .ToList();
            var namesToAdd = new HashSet<(string, Version)>(attributesToRemove.Select(x =>
                ((string)x.ConstructorArguments[0].Value, Version.Parse((string)x.ConstructorArguments[1].Value))));

            foreach (var customAttribute in attributesToRemove)
            {
                ModuleDefinition.Assembly.CustomAttributes.Remove(customAttribute);
            }

            foreach (var (name, version) in namesToAdd)
            {
                var newReference = new AssemblyNameReference(name, version);
                WriteInfo($"Adding reference to {newReference}");
                ModuleDefinition.AssemblyReferences.Add(newReference);
            }
        }

        public override IEnumerable<string> GetAssembliesForScanning()
        {
            yield break;
        }

        private void RemoveReference()
        {
            var referenceToRemove = ModuleDefinition.AssemblyReferences.FirstOrDefault(x => x.Name == "RemoveReference2");
            if (referenceToRemove == null)
            {
                WriteInfo("\tNo reference to 'RemoveReference2' found. References not modified.");
                return;
            }

            ModuleDefinition.AssemblyReferences.Remove(referenceToRemove);
            WriteInfo("\tRemoved reference to 'RemoveReference2'.");
        }
    }
}
