using System;

namespace DSharpExtensions.Attributes
{
    public class FriendlyName : Attribute
    {
        public string Value { get; }

        public FriendlyName(string value)
        {
            Value = value;
        }
    }
}
