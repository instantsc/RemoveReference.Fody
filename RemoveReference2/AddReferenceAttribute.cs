using System;

namespace RemoveReference2
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public class AddReferenceAttribute : Attribute
    {
        public AddReferenceAttribute(string name, string version)
        {
        }
    }
}
