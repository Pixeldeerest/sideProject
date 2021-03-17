using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPGCharacterCreatorServer.Models
{
    public class CharacterClass
    {
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public string ClassWeapon { get; set; }
        public string BasicAttack { get; set; }
        public int ClassBonus { get; set; }
    }
}
