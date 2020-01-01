using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace DSharpExtensions.Attributes.Checks
{
    public class RequireSpecificGuild : CheckBaseAttribute
    {
        public ulong Value { get; }

        public RequireSpecificGuild(ulong guildId)
        {
            Value = guildId;
        }

        public override async Task<bool> ExecuteCheckAsync(CommandContext ctx, bool help)
        {
            return ctx.Guild != null && ctx.Guild.Id == Value;
        }
    }
}
