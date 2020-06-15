using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class LocationStuff
    {
        public int UniqueID { get; set; }
        public string LocationID { get; set; }
        public int MonsterID { get; set; }
        public int ItemID { get; set; }
        public int QuestID { get; set; }
        public int ItemRequiredToEnter { get; set; }

        public LocationStuff(int uid, string locationid, int monsterid, int itemid, int questid, int itemreqtoenter)
        {
            UniqueID = uid;
            LocationID = locationid;
            MonsterID = monsterid;
            ItemID = itemid;
            QuestID = questid;
            ItemRequiredToEnter = itemreqtoenter;
        }
    }
}
