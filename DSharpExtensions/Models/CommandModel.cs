using System;
using System.Collections.Generic;
using DSharpPlus.CommandsNext.Attributes;

namespace DSharpExtensions.Models
{
    public class CommandModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<CheckBaseAttribute> ExecutionChecks { get; set; }
    }
}
