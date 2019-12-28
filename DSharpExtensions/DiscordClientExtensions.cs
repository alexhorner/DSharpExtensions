using System.Reflection;
using System.Threading.Tasks;
using DSharpExtensions.Internal;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.Net;

namespace DSharpExtensions
{
    public static class DiscordClientExtensions
    {
        public static async Task<DiscordDmChannel> CreateDmChannelAsync(this DiscordClient client, ulong id)
        {
            DiscordApiClient apiClient = ReflectionUtility.GetObjectFieldByType<BaseDiscordClient, DiscordApiClient>(client) as DiscordApiClient; //I know this isn't pretty.

            MethodInfo createDmAsyncMethod = typeof(DiscordApiClient).GetMethod("CreateDmAsync", BindingFlags.NonPublic | BindingFlags.Instance);

            return await (createDmAsyncMethod.Invoke(apiClient, new object[] { id }) as Task<DiscordDmChannel>);
        }
    }
}
