using System.Collections.Generic;
using System.Linq;
using Fody;

namespace RemoveReference.Fody
{
    public class ModuleWeaver : BaseModuleWeaver
    {
        public override void Execute()
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
                WriteInfo(assemblyNameReference.ToString());
                ModuleDefinition.AssemblyReferences.Remove(assemblyNameReference);
            }

            RemoveReference();
        }

        public override IEnumerable<string> GetAssembliesForScanning()
        {
            yield break;
        }

        private void RemoveReference()
        {
            var referenceToRemove = ModuleDefinition.AssemblyReferences.FirstOrDefault(x => x.Name == "RemoveReference");
            if (referenceToRemove == null)
            {
                WriteInfo("\tNo reference to 'RemoveReference' found. References not modified.");
                return;
            }

            ModuleDefinition.AssemblyReferences.Remove(referenceToRemove);
            WriteInfo("\tRemoved reference to 'RemoveReference'.");
        }
    }
}
