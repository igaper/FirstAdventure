using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Monster
    {
        public int MonsterID { get; set; }
        public string MonsterName { get; set; }
        public int MaximumHitPoints { get; set; }
        public int CurrentHitPoints { get; set; }
        public int MaximumDamage { get; set; }
        public int MinimumDamage { get; set; }
        public int RewardGold { get; set; }
        public int RewardExperiencePoints { get; set; }

        public Monster(int monsterid, string monstername, int maximumhitpoints, int maximumdamage, int minimumdamage, int rewardgold, int rewardxp)
        {
            MonsterID = monsterid;
            MonsterName = monstername;
            MaximumHitPoints = maximumhitpoints;
            CurrentHitPoints = MaximumHitPoints;
            MaximumDamage = maximumdamage;
            MinimumDamage = minimumdamage;
            RewardGold = rewardgold;
            RewardExperiencePoints = rewardxp;

        }
    }
    
}