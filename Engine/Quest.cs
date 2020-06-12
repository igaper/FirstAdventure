using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Quest
    {
        public int QuestID { get; set; }
        public string QuestName { get; set; }
        public string QuestDescription { get; set; }
        public Quest(int id, string name, string description)
        {
            QuestID = id;
            QuestName = name;
            QuestDescription = description;
        }
    }
}
