﻿namespace Asuka.Database.Models
{
    public class ReactionRole
    {
        public int Id { get; set; }
        public ulong GuildId { get; set; }
        public ulong MessageId { get; set; }
        public ulong RoleId { get; set; }
        public string EmoteName { get; set; }
    }
}
