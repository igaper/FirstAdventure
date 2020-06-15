using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Engine
{
    public class Database
    {
        public SQLiteConnection myConnection;
        public readonly static List<QuestRewards> LocationList = new List<QuestRewards>();
        public Database()
        {
            myConnection = new SQLiteConnection("Data Source=AdventurerStash.sqlite");
        }
    }
}
