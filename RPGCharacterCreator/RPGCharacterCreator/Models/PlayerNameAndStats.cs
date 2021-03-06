using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPGCharacterCreatorServer.Models
{
    public class PlayerNameAndStats
    {
        public string PlayerName { get; set; }
        public string PlayerRace { get; set; }
        public string ClassName { get; set; }
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Constitution { get; set; }
        public int Intelligence { get; set; }
        public int Wisdom { get; set; }
        public int Charisma { get; set; }
    }
}
