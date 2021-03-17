using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPGCharacterCreatorServer.Models
{
    public class Player
    {
        public int PlayerId { get; set; }
        public string PlayerName { get; set; }
        public string Pronouns { get; set; }
        public string PlayerRace { get; set; }
        public int ClassId { get; set; }
        public int PlayerStatId { get; set; }
        public int Height { get; set; }
        public int Age { get; set; }
        public string Personality { get; set; }
        public string HairColor { get; set; }
        public string EyeColor { get; set; }
        public string UniqueFeatures { get; set; }
    }
}
