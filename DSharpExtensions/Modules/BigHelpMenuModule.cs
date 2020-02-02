using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DSharpExtensions.Attributes;
using DSharpExtensions.Attributes.Checks;
using DSharpExtensions.Models;
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
        public static string Description = null;
        private static List<CategoryModel> _categories;

        [Command("help")]
        public async Task Help(CommandContext ctx)
        {
            DiscordEmbedBuilder helpEmbed = new DiscordEmbedBuilder
            {
                Color = EmbedColor,
                Title = HelpTitle,
                Description = Description
            };

            if (_categories == null)
            {
                _categories = new List<CategoryModel>();

                foreach (KeyValuePair<string, Command> command in ctx.CommandsNext.RegisteredCommands)
                {
                    if (command.Value.IsHidden || command.Value.Module.ModuleType.GetCustomAttribute(typeof(HiddenAttribute)) != null)
                    {
                        continue;
                    }

                    string moduleName = command.Value.Module.ModuleType.GetCustomAttribute(typeof(FriendlyName)) != null ? (command.Value.Module.ModuleType.GetCustomAttribute(typeof(FriendlyName)) as FriendlyName).Value : command.Value.Module.ModuleType.Name;

                    if (_categories.All(c => c.Name != moduleName))
                    {
                        _categories.Add(new CategoryModel
                        {
                            Name = moduleName,
                            Attributes = command.Value.Module.ModuleType.GetCustomAttributes().ToList(),
                            Commands = new List<CommandModel>()
                        });
                    }

                    _categories.FirstOrDefault(c => c.Name == moduleName).Commands.Add(new CommandModel
                    {
                        Name = command.Key,
                        Description = command.Value.Description,
                        ExecutionChecks = command.Value.ExecutionChecks
                    });
                }
            }

            Dictionary<string, string> helpCategories = new Dictionary<string, string>();

            foreach (CategoryModel category in _categories)
            {
                string categoryContent = "";

                foreach (CommandModel command in category.Commands)
                {
                    if (
                        (command.ExecutionChecks.FirstOrDefault(a => a.GetType().IsAssignableFrom(typeof(RequireSpecificGuild))) is RequireSpecificGuild requireSpecificGuild && !await requireSpecificGuild.ExecuteCheckAsync(ctx, false)) ||
                        (command.ExecutionChecks.FirstOrDefault(a => a.GetType().IsAssignableFrom(typeof(RequireGuildInArray))) is RequireGuildInArray requireGuildInArray && !await requireGuildInArray.ExecuteCheckAsync(ctx, false)) ||
                        (command.ExecutionChecks.FirstOrDefault(a => a.GetType().IsAssignableFrom(typeof(RequireSpecificUser))) is RequireSpecificUser requireSpecificUser && !await requireSpecificUser.ExecuteCheckAsync(ctx, false)) ||
                        (command.ExecutionChecks.FirstOrDefault(a => a.GetType().IsAssignableFrom(typeof(RequireUserInArray))) is RequireUserInArray requireUserInArray && !await requireUserInArray.ExecuteCheckAsync(ctx, false))
                    )
                    {
                        continue;
                    }

                    if (categoryContent != "")
                    {
                        categoryContent += "\n";
                    }

                    categoryContent += command.Name + (!string.IsNullOrEmpty(command.Description) ? " - " + command.Description : "");
                }

                if (categoryContent != "")
                {
                    helpCategories.Add(category.Name, categoryContent);
                }
            }

            foreach (KeyValuePair<string, string> category in helpCategories)
            {
                helpEmbed.AddField(category.Key, category.Value);
            }

            await ctx.RespondAsync(embed: helpEmbed);
        }
    }
}
