using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using DSharpExtensions.Attributes;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;

namespace DSharpExtensions.Modules
{
    [Hidden]
    public class BigHelpMenuModule : BaseCommandModule
    {
        public static DiscordColor EmbedColor = new DiscordColor(150, 150, 255);
        public static string HelpTitle = "Help Menu";

        private static Dictionary<string, string> _helpCategories;

        [Command("help")]
        public async Task Help(CommandContext ctx)
        {
            DiscordEmbedBuilder helpEmbed = new DiscordEmbedBuilder
            {
                Color = EmbedColor,
                Title = HelpTitle
            };

            if (_helpCategories == null)
            {
                _helpCategories = new Dictionary<string, string>();

                Dictionary<string, List<Command>> moduleCommands = new Dictionary<string, List<Command>>();

                foreach (KeyValuePair<string, Command> command in ctx.CommandsNext.RegisteredCommands)
                {
                    if (command.Value.IsHidden || command.Value.Module.ModuleType.GetCustomAttribute(typeof(HiddenAttribute)) != null)
                    {
                        continue;
                    }

                    string moduleName = command.Value.Module.ModuleType.GetCustomAttribute(typeof(FriendlyName)) != null ? (command.Value.Module.ModuleType.GetCustomAttribute(typeof(FriendlyName)) as FriendlyName).Value : command.Value.Module.ModuleType.Name;

                    if (!moduleCommands.ContainsKey(moduleName))
                    {
                        moduleCommands.Add(moduleName, new List<Command>());
                    }

                    if (moduleCommands.TryGetValue(moduleName, out List<Command> commands))
                    {
                        commands.Add(command.Value);
                    }
                }

                foreach (KeyValuePair<string, List<Command>> module in moduleCommands)
                {
                    string finalCommands = "";

                    foreach (Command command in module.Value)
                    {
                        if (finalCommands != "")
                        {
                            finalCommands += "\n";
                        }

                        finalCommands += command.Name + (!string.IsNullOrEmpty(command.Description) ? " - " + command.Description : "");
                    }

                    _helpCategories.Add(module.Key, finalCommands);
                }
            }

            foreach (KeyValuePair<string, string> category in _helpCategories)
            {
                helpEmbed.AddField(category.Key, category.Value);
            }

            await ctx.RespondAsync(embed: helpEmbed);
        }
    }
}
