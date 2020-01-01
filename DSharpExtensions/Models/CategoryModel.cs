using System;
using System.Collections.Generic;
using System.Dynamic;

namespace DSharpExtensions.Models
{
    public class CategoryModel
    {
        public string Name { get; set; }
        public List<Attribute> Attributes { get; set; }
        public List<CommandModel> Commands { get; set; }
    }
}
