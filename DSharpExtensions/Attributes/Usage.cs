using System;

namespace DSharpExtensions.Attributes
{
    public class Usage : Attribute
    {
        public string Value { get; }

        public Usage(string value)
        {
            Value = value;
        }
    }
}
