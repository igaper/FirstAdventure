using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data.Common;

namespace Engine
{
    public static class World
    {
        public static Database database = new Database();
        public static readonly List<Item> ItemList = new List<Item>();
        public static readonly List<Monster> MonsterList = new List<Monster>();
        public static readonly List<Quest> QuestList = new List<Quest>();
        public static readonly List<Location> LocationList = new List<Location>();
        public static readonly List<LocationStuff> LocationStuffList = new List<LocationStuff>();
        public static readonly List<MonsterLootTable> LootTable = new List<MonsterLootTable>();
        public static readonly List<QuestRewards> QuestRewardList = new List<QuestRewards>();
        public static readonly List<QuestCompletionItem> QuestCompletionItemsList = new List<QuestCompletionItem>();
        public static List<PlayerInventory> Inventory = new List<PlayerInventory>();

        static World()
        {
            PopulateItems();
            PopulateMonsters();
            PopulateQuests();
            PopulateLocations();
            PopulateLocationStuff();
            PopulateMonsterLootTable();
            PopulateQuestRewards();
            PopulateQuestCompletionItems();
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
                ItemList.Add(new Item(rdr.GetInt32(0), rdr.GetString(1), rdr.GetString(2), rdr.GetInt32(3), rdr.GetInt32(4), rdr.GetString(5)));
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
                QuestList.Add(new Quest(rdr.GetInt32(0), rdr.GetString(1), rdr.GetString(2), rdr.GetInt32(3), rdr.GetInt32(4)));
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

        private static void PopulateLocationStuff()
        {
            using var con = new SQLiteConnection("Data Source=AdventurerStash.sqlite");
            con.Open();
            string stm = "SELECT * FROM LocationStuff";

            using var cmd = new SQLiteCommand(stm, con);
            using SQLiteDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                LocationStuffList.Add(new LocationStuff(rdr.GetInt32(0), rdr.GetString(1), rdr.GetInt32(2), rdr.GetInt32(3), rdr.GetInt32(4), rdr.GetInt32(5)));
            }
            con.Close();
        }
        private static void PopulateMonsterLootTable()
        {
            using var con = new SQLiteConnection("Data Source=AdventurerStash.sqlite");
            con.Open();
            string stm = "SELECT * FROM MonsterLootTable";

            using var cmd = new SQLiteCommand(stm, con);
            using SQLiteDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                LootTable.Add(new MonsterLootTable(rdr.GetInt32(0), rdr.GetInt32(1), rdr.GetInt32(2), rdr.GetInt32(3)));
            }
            con.Close();
        }
        private static void PopulateQuestCompletionItems()
        {
            using var con = new SQLiteConnection("Data Source=AdventurerStash.sqlite");
            con.Open();
            string stm = "SELECT * FROM QuestCompletionItems";

            using var cmd = new SQLiteCommand(stm, con);
            using SQLiteDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                QuestCompletionItemsList.Add(new QuestCompletionItem(rdr.GetInt32(0), rdr.GetInt32(1), rdr.GetInt32(2), rdr.GetInt32(3)));
            }
            con.Close();
        }
        private static void PopulateQuestRewards()
        {
            using var con = new SQLiteConnection("Data Source=AdventurerStash.sqlite");
            con.Open();
            string stm = "SELECT * FROM QuestRewards";

            using var cmd = new SQLiteCommand(stm, con);
            using SQLiteDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                QuestRewardList.Add(new QuestRewards(rdr.GetInt32(0), rdr.GetInt32(1), rdr.GetInt32(2), rdr.GetInt32(3)));
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
        public static bool HasRequiredItemToEnterThisLocation(Location location)
        {
            //Variables for checking if player has the number of items required to enter location
            int HowManyItemesRequired = 0;
            int HowManyItemsPlayerHas = 0;
            //Checking list of location stuff which contains items required to enter for each location. Locations can have multiple items required therefore variables above are needed.
            foreach (LocationStuff stuff in LocationStuffList)
            {
                    //If this location has item listed. If it's 0 it means no items are required to enter
                    if (stuff.LocationID == location.LocationID && stuff.ItemRequiredToEnter != 0)
                    {
                        //If it's different than zero increase the amount of items that we need
                        HowManyItemesRequired++;
                        //Then check player inventory if he has this item and if he didn't use it before
                        foreach(PlayerInventory pi in Inventory)
                        {
                            if(stuff.ItemRequiredToEnter == pi.ItemID && pi.Amount > 0)
                            {
                                //If player has this item and has more than 0 of it we match item required. If player doesn't have the item it will later return false
                                HowManyItemsPlayerHas++;
                            }
                        }
                    }
            }
            //Checking if the amount of items needed by location is equal to items player has in inventory. If the amounts don't match he's missing something
            if(HowManyItemsPlayerHas != HowManyItemesRequired)
            {
                return false;
            }
            //In all other possibilities player either has the items required or there are no items required therefore we return true
            return true;
        }

        public static bool DoesThisLocationHaveAQuest(Location Location)
        {
            foreach (LocationStuff stuff in LocationStuffList)
            {
                if(stuff.LocationID == Location.LocationID && stuff.QuestID != 0)
                {
                    return true;
                }
            }
            return false;
        }
        public static bool HasAllQuestCompletionItems(int questid)
        {
            int QuestCompletionItemsNeeded = 0;
            int QuestCompletionItemsOwned = 0;
            // See if the player has all the items needed to complete the quest
            foreach (QuestCompletionItem qci in QuestCompletionItemsList)
            {
                //Check for every quest completion item to check on every one in case there are mutiple ones needed.
                if (qci.QuestID == questid && qci.ItemID!=0) 
                {
                    QuestCompletionItemsNeeded++;
                    // Check each item in the player's inventory, to see if they have it, and enough of it
                    foreach (PlayerInventory pi in Inventory)
                    {
                        if (qci.ItemID == pi.ItemID && qci.Amount<=pi.Amount) // If the player doesn't have any of the items or in required quantity return false immidietly
                        {
                            QuestCompletionItemsOwned++;
                        }
                    }
                }
            }
            if (QuestCompletionItemsNeeded == QuestCompletionItemsOwned)
            {
                return true;
            }
            return false;
        }
        public static void RemoveQuestCompletionItems(int questid)
        {
            foreach (QuestCompletionItem qci in QuestCompletionItemsList)
            {
                foreach (PlayerInventory pi in Inventory)
                {
                    if (qci.QuestID == questid && pi.ItemID == qci.ItemID)
                    {
                        // Subtract the quantity from the player's inventory that was needed to complete the quest
                        pi.Amount -= qci.Amount;
                    }
                }
            }
        }
        public static void AddItemToInventory(Item itemToAdd, int amount)
        {
            foreach (PlayerInventory ii in Inventory)
            {
                if (ii.ItemID == itemToAdd.ItemID)
                {
                    // They have the item in their inventory, so increase the quantity by one
                    ii.Amount+= amount;

                    return; // We added the item, and are done, so get out of this function
                }
            }

            // They didn't have the item, so add it to their inventory, with a quantity of 1
            Inventory.Add(new PlayerInventory(1, itemToAdd.ItemID, amount));
        }
    }
}