using System.Reflection;
using System.Threading.Tasks;
using DSharpExtensions.Internal;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.Net;

namespace DSharpExtensions.ClassExtensions
{
    public static class DiscordClientExtensions
    {
        //Objects
        private static DiscordApiClient apiClient;

        //Methods
        private static MethodInfo createDmAsyncMethod;

        public static async Task<DiscordDmChannel> CreateDmChannelAsync(this DiscordClient client, ulong id)
        {
            if (apiClient == null)
            {
                apiClient = ReflectionUtility.GetObjectFieldByType<BaseDiscordClient, DiscordApiClient>(client) as DiscordApiClient; //I know this isn't pretty.
            }

            if (createDmAsyncMethod == null)
            {
                createDmAsyncMethod = typeof(DiscordApiClient).GetMethod("CreateDmAsync", BindingFlags.NonPublic | BindingFlags.Instance);
            }

            return await (createDmAsyncMethod.Invoke(apiClient, new object[] { id }) as Task<DiscordDmChannel>);
        }
    }
}
