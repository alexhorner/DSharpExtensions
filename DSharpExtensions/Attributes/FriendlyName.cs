using System;

namespace DSharpExtensions.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = false)]
    public class FriendlyName : Attribute
    {
        public string Value { get; }

        public FriendlyName(string value)
        {
            Value = value;
        }
    }
}
