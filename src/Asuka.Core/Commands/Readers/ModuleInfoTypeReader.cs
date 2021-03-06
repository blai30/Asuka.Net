﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;

namespace Asuka.Commands.Readers
{
    public class ModuleInfoTypeReader : TypeReader
    {
        public override Task<TypeReaderResult> ReadAsync(
            ICommandContext context,
            string input,
            IServiceProvider services)
        {
            var commands = services.GetService(typeof(CommandService)) as CommandService;
            string moduleName = input.ToUpperInvariant();

            // Get module by name or alias.
            var module = commands?.Modules.FirstOrDefault(info =>
                info.Name.ToUpperInvariant() == moduleName ||
                info.Aliases.Any(alias => alias.ToUpperInvariant() == moduleName));

            // Module is invalid.
            return Task.FromResult(module == null
                ? TypeReaderResult.FromError(CommandError.ParseFailed, $"Module `{input}` does not exist. UwU")
                : TypeReaderResult.FromSuccess(module));
        }
    }
}
