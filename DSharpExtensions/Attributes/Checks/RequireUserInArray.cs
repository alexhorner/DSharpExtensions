using System.Linq;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace DSharpExtensions.Attributes.Checks
{
    public class RequireUserInArray : CheckBaseAttribute
    {
        public ulong[] Value { get; }

        public RequireUserInArray(ulong[] userIds)
        {
            Value = userIds;
        }

        public override async Task<bool> ExecuteCheckAsync(CommandContext ctx, bool help)
        {
            return Value.Contains(ctx.User.Id);
        }
    }
}
