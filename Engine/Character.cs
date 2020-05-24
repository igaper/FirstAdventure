using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Character
    {
        public int MaximumHitPoints { get; set; }
        public int CurrentHitPoints { get; set; }
        public Character(int maximumhitpoints, int currenthitpoints)
        {
            MaximumHitPoints = maximumhitpoints;
            CurrentHitPoints = currenthitpoints;
        }
    }
}
