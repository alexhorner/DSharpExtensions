using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace DSharpExtensions.Attributes.Checks
{
    public class RequireSpecificUser : CheckBaseAttribute
    {
        public ulong Value { get; }

        public RequireSpecificUser(ulong userId)
        {
            Value = userId;
        }

        public override async Task<bool> ExecuteCheckAsync(CommandContext ctx, bool help)
        {
            return ctx.User.Id == Value;
        }
    }
}
