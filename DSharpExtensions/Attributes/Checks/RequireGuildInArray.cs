using System.Linq;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace DSharpExtensions.Attributes.Checks
{
    public class RequireGuildInArray : CheckBaseAttribute
    {
        public ulong[] Value { get; }

        public RequireGuildInArray(ulong[] guildIds)
        {
            Value = guildIds;
        }

        public override async Task<bool> ExecuteCheckAsync(CommandContext ctx, bool help)
        {
            return ctx.Guild != null && Value.Contains(ctx.Guild.Id);
        }
    }
}
