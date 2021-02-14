﻿using System.Threading.Tasks;
using Asuka.Commands;
using Asuka.Configuration;
using Asuka.Database;
using Asuka.Database.Models;
using Discord.Commands;
using Microsoft.Extensions.Options;

namespace Asuka.Modules.Tags
{
    [Group("tag")]
    [Remarks("Tags")]
    [Summary("Add, edit, or delete tags.")]
    [RequireContext(ContextType.Guild)]
    public class TagModule : CommandModuleBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public TagModule(
            IOptions<DiscordOptions> config,
            IUnitOfWork unitOfWork) :
            base(config)
        {
            _unitOfWork = unitOfWork;
        }

        [Command("add")]
        [Alias("a", "create", "c")]
        [Remarks("Create a new tag for the server.")]
        public async Task AddAsync(string tagName, string tagContent)
        {
            var tag = new Tag
            {
                Name = tagName,
                Content = tagContent,
                UserId = Context.User.Id,
                GuildId = Context.Guild.Id
            };

            // try
            // {
                await _unitOfWork.Tags.AddAsync(tag);
                _unitOfWork.Complete();
                await ReplyAsync($"Added new tag `{tag.Name}`.");
            // }
            // catch
            // {
            //     await ReplyAsync($"Error adding `{tagName}`, either a tag with the same name already exists or the input parameters are invalid.");
            // }
        }

        [Command("edit")]
        [Alias("e", "modify", "m")]
        [Remarks("Edit an existing tag from the server.")]
        public async Task EditAsync(string tagName, string tagContent)
        {
            await Task.CompletedTask;
        }

        [Command("get")]
        [Alias("g", "fetch", "f")]
        [Remarks("Get an existing tag from the server.")]
        public async Task GetAsync(string tagName)
        {
            var tag = await _unitOfWork.Tags.GetAsync(tagName);
            string content = tag.Content;

            // No such tag exists in guild.
            if (string.IsNullOrEmpty(content))
            {
                await ReplyAsync($@"Tag `{tagName}` does not exist. .·´¯\`(>▂<)´¯\`·. ");
                return;
            }

            await ReplyAsync(content);
        }

        [Command("remove")]
        [Alias("r", "delete", "d")]
        [Remarks("Remove a tag from the server.")]
        public async Task RemoveAsync(string tagName)
        {
            await Task.CompletedTask;
        }

        [Command("list")]
        [Alias("l", "all")]
        [Remarks("List all tags from the server.")]
        public async Task ListAsync()
        {
            await Task.CompletedTask;
        }

        [Command("info")]
        [Alias("i", "stats")]
        [Remarks("Show info for a tag from the server.")]
        public async Task InfoAsync(string tagName)
        {
            await Task.CompletedTask;
        }
    }
}
