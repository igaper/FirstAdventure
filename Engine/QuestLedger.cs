using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class QuestLedger
    {
        public int PlayerID { get; set; }
        public int QuestID { get; set; }
        public int IsCompleted { get; set; }

        public QuestLedger(int playerid, int questid, int iscompleted)
        {
            PlayerID = playerid;
            QuestID = questid;
            IsCompleted = iscompleted;
        }
    }
}
