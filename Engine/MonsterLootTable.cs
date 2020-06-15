using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class MonsterLootTable
    {
        public int UniqueID { get; set; }
        public int MonsterID { get; set; }
        public int ItemID { get; set; }
        public int ItemDropRate { get; set; }

        public MonsterLootTable(int uid, int monsterid, int itemid, int itemdroprate)
        {
            UniqueID = uid;
            MonsterID = monsterid;
            ItemID = itemid;
            ItemDropRate = itemdroprate;
        }
    }
}
