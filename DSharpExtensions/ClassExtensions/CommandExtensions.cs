using System.Linq;
using DSharpExtensions.Attributes;
using DSharpPlus.CommandsNext;

namespace DSharpExtensions.ClassExtensions
{
    public static class CommandExtensions
    {
        public static string GetUsage(this Command command)
        {
            Usage usage = (Usage) command.CustomAttributes.FirstOrDefault(a => a.GetType() == typeof(Usage));
            return usage != null ? usage.Value : "";
        }
    }
}
