﻿using System.Linq;
using System.Threading.Tasks;
using Asuka.Commands;
using Asuka.Configuration;
using Discord;
using Discord.Commands;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Asuka.Modules.General
{
    [Group("serverinfo")]
    [Alias("server")]
    [Remarks("General")]
    [Summary("Display information about the server.")]
    [RequireContext(ContextType.Guild)]
    public class ServerInfoModule : CommandModuleBase
    {
        public ServerInfoModule(
            IOptions<DiscordOptions> config,
            ILogger<ServerInfoModule> logger) :
            base(config, logger)
        {
        }

        [Command]
        [Remarks("serverinfo")]
        public async Task ServerInfoAsync()
        {
            // Collect guild information.
            var guild = Context.Guild;
            var textChannels = guild.TextChannels;
            var voiceChannels = guild.VoiceChannels;
            var emotes = guild.Emotes;
            var roles = guild.Roles;
            string guildIconUrl = guild.IconUrl;

            // Sort collection of roles to print alphabetically.
            var roleList = roles.Select(role => role.Name).ToList();
            roleList.Sort();

            var embed = new EmbedBuilder()
                .WithTitle("Icon direct link")
                .WithUrl(guildIconUrl)
                .WithAuthor(guild.Name, guildIconUrl)
                .WithDescription($"Owner: {guild.Owner.Mention}")
                .WithColor(Config.Value.EmbedColor)
                .WithThumbnailUrl(guildIconUrl)
                .WithFooter($"Created: {guild.CreatedAt:R}")
                .AddField(
                    "ID",
                    guild.Id)
                .AddField(
                    "Region",
                    guild.VoiceRegionId,
                    true)
                .AddField(
                    "Max Bitrate",
                    guild.MaxBitrate + " kbps",
                    true)
                .AddField(
                    "Members",
                    guild.MemberCount,
                    true)
                .AddField(
                    "Channels",
                    $"{textChannels.Count} text\n" +
                    $"{voiceChannels.Count} voice",
                    true)
                .AddField(
                    "Emotes",
                    $"{emotes.Select(emote => !emote.Animated).Count()} static\n" +
                    $"{emotes.Select(emote => emote.Animated).Count()} animated",
                    true)
                .AddField(
                    "Premium",
                    $"Level {(int) guild.PremiumTier}\n" +
                    $"{guild.PremiumSubscriptionCount} boosts",
                    true)
                .AddField(
                    "Roles",
                    string.Join(", ", roleList))
                .Build();

            await ReplyAsync(embed: embed);
        }
    }
}
