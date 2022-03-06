using System;

namespace RemoveReference2
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public class RemoveReferenceAttribute : Attribute
    {
        public RemoveReferenceAttribute(string fullName)
        {
        }
    }
}
