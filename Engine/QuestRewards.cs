﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class QuestRewards
    {
        public int UniqueID { get; set; }
        public int QuestID { get; set; }
        public int ItemID { get; set; }
        public int Amount { get; set; }
        public QuestRewards(int uid, int questid, int itemid, int amount)
        {
            UniqueID = uid;
            QuestID = questid;
            ItemID = itemid;
            Amount = amount;
        }
    }
}
