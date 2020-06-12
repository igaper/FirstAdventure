using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;


namespace Engine
{
    public static class World
    {
        public static readonly List<Item> ItemList = new List<Item>();
        public static readonly List<Monster> MonsterList = new List<Monster>();
        public static readonly List<Quest> QuestList = new List<Quest>();
        public static readonly List<Location> LocationList = new List<Location>();

        static World()
        {
            PopulateItems();
            PopulateMonsters();
            PopulateQuests();
            PopulateLocations();
        }

        private static void PopulateItems()
        {
            using var con = new SQLiteConnection("Data Source=AdventurerStash.sqlite");
            con.Open();

            string stm = "SELECT * FROM Items";

            using var cmd = new SQLiteCommand(stm, con);
            using SQLiteDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                ItemList.Add(new Item(rdr.GetInt32(0), rdr.GetString(1), rdr.GetString(2), rdr.GetInt32(3), rdr.GetInt32(4)));
            }
            con.Close();
        }

        private static void PopulateMonsters()
        {
            using var con = new SQLiteConnection("Data Source=AdventurerStash.sqlite");
            con.Open();

            string stm = "SELECT * FROM Monster";

            using var cmd = new SQLiteCommand(stm, con);
            using SQLiteDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                MonsterList.Add(new Monster(rdr.GetInt32(0), rdr.GetString(1), rdr.GetInt32(2), rdr.GetInt32(3), rdr.GetInt32(4), rdr.GetInt32(5), rdr.GetInt32(6)));
            }
            con.Close();
        }

        private static void PopulateQuests()
        {
            using var con = new SQLiteConnection("Data Source=AdventurerStash.sqlite");
            con.Open();

            string stm = "SELECT * FROM Quest";

            using var cmd = new SQLiteCommand(stm, con);
            using SQLiteDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                QuestList.Add(new Quest(rdr.GetInt32(0), rdr.GetString(1), rdr.GetString(2)));
            }
            con.Close();
        }

        private static void PopulateLocations()
        {
            using var con = new SQLiteConnection("Data Source=AdventurerStash.sqlite");
            con.Open();
            string stm = "SELECT * FROM Location";

            using var cmd = new SQLiteCommand(stm, con);
            using SQLiteDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                LocationList.Add(new Location(rdr.GetString(0), rdr.GetString(1), rdr.GetString(2), rdr.GetString(3), rdr.GetString(4), rdr.GetString(5), rdr.GetString(6)));
            }
            con.Close();
        }

        public static Item ItemByID(int id)
        {
            foreach (Item item in ItemList)
            {
                if (item.ItemID == id)
                {
                    return item;
                }
            }

            return null;
        }

        public static Monster MonsterByID(int id)
        {
            foreach (Monster monster in MonsterList)
            {
                if (monster.MonsterID == id)
                {
                    return monster;
                }
            }

            return null;
        }

        public static Quest QuestByID(int id)
        {
            foreach (Quest quest in QuestList)
            {
                if (quest.QuestID == id)
                {
                    return quest;
                }
            }

            return null;
        }

        public static Location LocationByID(string id)
        {
            foreach (Location location in LocationList)
            {
                if (location.LocationID == id)
                {
                    return location;
                }
            }

            return null;
        }
    }
}