using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class PlayerInventory
    {
        public int PlayerID { get; set; }
        public int ItemID { get; set; }
        public int Amount { get; set; }

        public PlayerInventory(int playerid, int itemid, int amount)
        {
            PlayerID = playerid;
            ItemID = itemid;
            Amount = amount;
        }
    }
}
