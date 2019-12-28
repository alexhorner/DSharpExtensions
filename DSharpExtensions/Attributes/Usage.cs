using System;

namespace DSharpExtensions.Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class Usage : Attribute
    {
        public string Value { get; }

        public Usage(string value)
        {
            Value = value;
        }
    }
}
