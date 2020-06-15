using Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FirstAdventure
{
    public partial class rbtMessages : Form
    {
        private Player _player;
        private List<QuestLedger> Journal = new List<QuestLedger>();
        private Monster _monster;
        public rbtMessages()
        {
            InitializeComponent();
        }

        private void FirstAdventure_Load(object sender, EventArgs e)
        {
            _player = new Player(1, 10, 10, 0, 0, 1);
            MoveTo(World.LocationByID("I10"));
            World.Inventory.Add(new PlayerInventory(1, 1, 1));
            lblHitPoints.Text = _player.CurrentHitPoints.ToString();
            lblGold.Text = _player.Gold.ToString();
            lblExperience.Text = _player.ExperiencePoints.ToString();
            lblLevel.Text = _player.Level.ToString();
        }

        //Change current location in player class to one indicated by button click. Need to use LocationByID fuction since LocationList is holding nearby locations as string
        private void btnGoNorth_Click(object sender, EventArgs e)
        {
            MoveTo(World.LocationByID(_player.CurrentLocation.LocationToNorth));
        }
        private void btnGoSouth_Click(object sender, EventArgs e)
        {
            MoveTo(World.LocationByID(_player.CurrentLocation.LocationToSouth));
        }

        private void btnGoWest_Click(object sender, EventArgs e)
        {
            MoveTo(World.LocationByID(_player.CurrentLocation.LocationToWest));
        }

        private void btnGoEast_Click(object sender, EventArgs e)
        {
            MoveTo(World.LocationByID(_player.CurrentLocation.LocationToEast));
        }

        private void MoveTo(Location newLocation)
        {
            //Does the location have any required items
            if (!World.HasRequiredItemToEnterThisLocation(newLocation))
            {

                rtbMessages.Text += "You don't have all items required to enter" + Environment.NewLine;
                return;
            }

            // Update the player's current location
            _player.CurrentLocation = newLocation;

            // Show/hide available movement buttons
            btnGoNorth.Visible = (newLocation.LocationToNorth != "NULL");
            btnGoEast.Visible = (newLocation.LocationToEast != "NULL");
            btnGoSouth.Visible = (newLocation.LocationToSouth != "NULL");
            btnGoWest.Visible = (newLocation.LocationToWest != "NULL");

            // Display current location name and description
            rtbLocation.Text = newLocation.LocationName + Environment.NewLine;
            rtbLocation.Text += newLocation.LocationDescription + Environment.NewLine;
            ScrollToBottomOfMessages();

            // Completely heal the player
            _player.CurrentHitPoints = _player.MaximumHitPoints;

            // Update Hit Points in UI
            lblHitPoints.Text = _player.CurrentHitPoints.ToString();

            //Check for each location and quest in LocationStuffList if it has a quests in this location. Needed this way since there might be mutliple quests
            foreach (LocationStuff stuff in World.LocationStuffList)
            {
                // If location has a quest
                if (newLocation.LocationID == stuff.LocationID && stuff.QuestID != 0)
                {
                    if (Journal.Count == 0)
                    {
                        AddQuestToJournal(World.QuestByID(stuff.QuestID));
                    }

                    bool CheckForQuest()
                    {
                        foreach (QuestLedger ql in Journal)
                        {
                            if(ql.QuestID == stuff.QuestID)
                            {
                                return true;
                            }
                        }
                        return false;
                    }
                    if (!CheckForQuest())
                    {
                        AddQuestToJournal(World.QuestByID(stuff.QuestID));
                    }
                    // Check all the quest player has currently
                    foreach (QuestLedger ql in Journal) 
                    {
                        //If player has the quest check if it can be completed
                        if(ql.QuestID == stuff.QuestID && World.HasAllQuestCompletionItems(ql.QuestID) && ql.IsCompleted == 0) 
                        { 
                            //If it can do the following
                            // Display message
                            rtbMessages.Text += Environment.NewLine;
                            rtbMessages.Text += "You complete the '" + World.QuestByID(ql.QuestID).QuestName + "' quest." + Environment.NewLine;

                            // Remove quest items from inventory
                            World.RemoveQuestCompletionItems(ql.QuestID);

                            // Show and give quest rewards
                            rtbMessages.Text += "You receive: " + Environment.NewLine;
                            rtbMessages.Text += World.QuestByID(ql.QuestID).ExpReward.ToString() + " experience points" + Environment.NewLine;
                            _player.ExperiencePoints += World.QuestByID(ql.QuestID).ExpReward;
                            rtbMessages.Text += World.QuestByID(ql.QuestID).GoldReward.ToString() + " gold" + Environment.NewLine;
                            _player.Gold += World.QuestByID(ql.QuestID).GoldReward;
                            ScrollToBottomOfMessages();

                            foreach (QuestRewards qr in World.QuestRewardList)
                            {
                                if(qr.QuestID == ql.QuestID && qr.ItemID!=0)
                                {
                                    rtbMessages.Text += World.ItemByID(qr.ItemID).Name + Environment.NewLine;
                                    World.AddItemToInventory(World.ItemByID(qr.ItemID), qr.Amount);
                                }
                            }    
                            rtbMessages.Text += Environment.NewLine;
                            // Mark the quest as completed
                            ql.IsCompleted = 1;
                        }
                        
                    }
                    
                }
            }
            
            // Does the location have a monster?
            foreach(LocationStuff stuff in World.LocationStuffList)
            {
                if (newLocation.LocationID == stuff.LocationID && stuff.MonsterID != 0)
                {
                    rtbMessages.Text += "You see a " + World.MonsterByID(stuff.MonsterID).MonsterName + Environment.NewLine;
                    ScrollToBottomOfMessages();

                    // Make a new monster, using the values from the standard monster in the World.Monster list
                    Monster standardMonster = World.MonsterByID(stuff.MonsterID);

                    _monster = new Monster(standardMonster.MonsterID, standardMonster.MonsterName, standardMonster.MaximumHitPoints, standardMonster.MaximumDamage,
                       standardMonster.MinimumDamage, standardMonster.RewardGold, standardMonster.RewardExperiencePoints);

                    cboWeapons.Visible = true;
                    cboPotions.Visible = true;
                    btnUseWeapon.Visible = true;
                    btnUsePotion.Visible = true;
                    break;
                }
                else
                {
                    _monster = null;

                    cboWeapons.Visible = false;
                    cboPotions.Visible = false;
                    btnUseWeapon.Visible = false;
                    btnUsePotion.Visible = false;
                }
            }
        // Refresh player's inventory list
        UpdateInventoryListInUI();

        // Refresh player's quest list
        UpdateQuestListInUI();

        // Refresh player's weapons combobox
        UpdateWeaponListInUI();

        // Refresh player's potions combobox
        UpdatePotionListInUI();
        }
        private void UpdateInventoryListInUI()
        {
            dgvInventory.RowHeadersVisible = false;

            dgvInventory.ColumnCount = 2;
            dgvInventory.Columns[0].Name = "Name";
            dgvInventory.Columns[0].Width = 197;
            dgvInventory.Columns[1].Name = "Quantity";

            dgvInventory.Rows.Clear();

            foreach (PlayerInventory pi in World.Inventory)
            {
                if (pi.Amount > 0)
                {
                    dgvInventory.Rows.Add(new[] { World.ItemByID(pi.ItemID).Name, pi.Amount.ToString() });
                }
            }
        }

        private void UpdateQuestListInUI()
        {
            dgvQuests.RowHeadersVisible = false;

            dgvQuests.ColumnCount = 2;
            dgvQuests.Columns[0].Name = "Name";
            dgvQuests.Columns[0].Width = 197;
            dgvQuests.Columns[1].Name = "Done?";

            dgvQuests.Rows.Clear();

            foreach (QuestLedger ql in Journal)
            {
                dgvQuests.Rows.Add(new[] { World.QuestByID(ql.QuestID).QuestName, ql.IsCompleted.ToString() });
            }
        }

        private void UpdateWeaponListInUI()
        {
            List<Item> weapons = new List<Item>();

            foreach (PlayerInventory pi in World.Inventory)
            {
                if (World.ItemByID(pi.ItemID).Type == "W")
                {
                    if (pi.Amount > 0)
                    {
                        weapons.Add(World.ItemByID(pi.ItemID));
                    }
                }
            }

            if (weapons.Count == 0)
            {
                // The player doesn't have any weapons, so hide the weapon combobox and "Use" button
                cboWeapons.Visible = false;
                btnUseWeapon.Visible = false;
            }
            else
            {
                cboWeapons.DataSource = weapons;
                cboWeapons.DisplayMember = "Name";
                cboWeapons.ValueMember = "ItemID";

                cboWeapons.SelectedIndex = 0;
            }
        }

        private void UpdatePotionListInUI()
        {
            List<Item> healingPotions = new List<Item>();

            foreach (PlayerInventory pi in World.Inventory)
            {
                if (World.ItemByID(pi.ItemID).Type == "P")
                {
                    if (pi.Amount > 0)
                    {
                        healingPotions.Add(World.ItemByID(pi.ItemID));
                    }
                }
            }

            if (healingPotions.Count == 0)
            {
                // The player doesn't have any potions, so hide the potion combobox and "Use" button
                cboPotions.Visible = false;
                btnUsePotion.Visible = false;
            }
            else
            {
                cboPotions.DataSource = healingPotions;
                cboPotions.DisplayMember = "Name";
                cboPotions.ValueMember = "ItemID";

                cboPotions.SelectedIndex = 0;
            }
        }
        private void AddQuestToJournal(Quest quest)
        {
                // Display the messages
                rtbMessages.Text += "You receive the " + quest.QuestName + " quest." + Environment.NewLine;
                rtbMessages.Text += quest.QuestDescription + Environment.NewLine;
                rtbMessages.Text += "To complete it, return with:" + Environment.NewLine;
                foreach (QuestCompletionItem qci in World.QuestCompletionItemsList)
                {
                    if (qci.QuestID == quest.QuestID)
                    {
                        if (qci.Amount == 1)
                        {
                            rtbMessages.Text += qci.Amount.ToString() + " " + World.ItemByID(qci.ItemID).Name + Environment.NewLine;
                        }
                        else
                        {
                            rtbMessages.Text += qci.Amount.ToString() + " " + World.ItemByID(qci.ItemID).NamePlural + Environment.NewLine;
                        }
                    }
                }
                rtbMessages.Text += Environment.NewLine;

                // Add the quest to the player's quest list
                Journal.Add(new QuestLedger(1, quest.QuestID, 0));
        }
        private void btnUseWeapon_Click(object sender, EventArgs e)
        {
            // Get the currently selected weapon from the cboWeapons ComboBox
            Item currentWeapon = (Item)cboWeapons.SelectedItem;

            // Determine the amount of damage to do to the monster
            int damageToMonster = RandomNumberGenerator.NumberBetween(currentWeapon.MinValue, currentWeapon.MaxValue);

            // Apply the damage to the monster's CurrentHitPoints
            _monster.CurrentHitPoints -= damageToMonster;

            // Display message
            rtbMessages.Text += "You hit the " + _monster.MonsterName + " for " + damageToMonster.ToString() + " points." + Environment.NewLine;
            ScrollToBottomOfMessages();

            // Check if the monster is dead
            if (_monster.CurrentHitPoints <= 0)
            {
                // Monster is dead
                rtbMessages.Text += Environment.NewLine;
                rtbMessages.Text += "You defeated the " + _monster.MonsterName + Environment.NewLine;
                ScrollToBottomOfMessages();

                // Give player experience points for killing the monster
                _player.ExperiencePoints += _monster.RewardExperiencePoints;
                rtbMessages.Text += "You receive " + _monster.RewardExperiencePoints.ToString() + " experience points" + Environment.NewLine;
                ScrollToBottomOfMessages();

                // Give player gold for killing the monster 
                _player.Gold += _monster.RewardGold;
                rtbMessages.Text += "You receive " + _monster.RewardGold.ToString() + " gold" + Environment.NewLine;
                ScrollToBottomOfMessages();

                foreach (MonsterLootTable lootItem in World.LootTable)
                {
                    if (RandomNumberGenerator.NumberBetween(1, 100) <= lootItem.ItemDropRate && _monster.MonsterID == lootItem.MonsterID)
                    {
                        World.AddItemToInventory(World.ItemByID(lootItem.ItemID), 1);
                        rtbMessages.Text += "You loot " + " " + World.ItemByID(lootItem.ItemID).Name + Environment.NewLine;
                        ScrollToBottomOfMessages();
                    }
                }

                // Refresh player information and inventory controls
                lblHitPoints.Text = _player.CurrentHitPoints.ToString();
                lblGold.Text = _player.Gold.ToString();
                lblExperience.Text = _player.ExperiencePoints.ToString();
                lblLevel.Text = _player.Level.ToString();

                UpdateInventoryListInUI();
                UpdateWeaponListInUI();
                UpdatePotionListInUI();

                // Add a blank line to the messages box, just for appearance.
                rtbMessages.Text += Environment.NewLine;

                // Move player to current location (to heal player and create a new monster to fight)
                MoveTo(_player.CurrentLocation);
            }
            else
            {
                // Monster is still alive

                // Determine the amount of damage the monster does to the player
                int damageToPlayer = RandomNumberGenerator.NumberBetween(0, _monster.MaximumDamage);

                // Display message
                rtbMessages.Text += "The " + _monster.MonsterName + " did " + damageToPlayer.ToString() + " points of damage." + Environment.NewLine;
                ScrollToBottomOfMessages();
                // Subtract damage from player
                _player.CurrentHitPoints -= damageToPlayer;

                // Refresh player data in UI
                lblHitPoints.Text = _player.CurrentHitPoints.ToString();

                if (_player.CurrentHitPoints <= 0)
                {
                    // Display message
                    rtbMessages.Text += "The " + _monster.MonsterName + " killed you." + Environment.NewLine;
                    ScrollToBottomOfMessages();
                    // Move player to "Home"
                    MoveTo(World.LocationByID("I10"));
                }
            }
        }
        private void btnUsePotion_Click(object sender, EventArgs e)
        {
            // Get the currently selected potion from the combobox
            Item potion = (Item)cboPotions.SelectedItem;

            // Add healing amount to the player's current hit points
            _player.CurrentHitPoints = (_player.CurrentHitPoints + RandomNumberGenerator.NumberBetween(potion.MinValue, potion.MaxValue));

            // CurrentHitPoints cannot exceed player's MaximumHitPoints
            if (_player.CurrentHitPoints > _player.MaximumHitPoints)
            {
                _player.CurrentHitPoints = _player.MaximumHitPoints;
            }

            // Remove the potion from the player's inventory
            foreach (PlayerInventory pi in World.Inventory)
            {
                if (pi.ItemID == potion.ItemID)
                {
                    pi.Amount--;
                    break;
                }
            }

            // Display message
            rtbMessages.Text += "You drink a " + potion.Name + Environment.NewLine;

            // Monster gets their turn to attack

            // Determine the amount of damage the monster does to the player
            int damageToPlayer = RandomNumberGenerator.NumberBetween(0, _monster.MaximumDamage);

            // Display message
            rtbMessages.Text += "The " + _monster.MonsterName + " did " + damageToPlayer.ToString() + " points of damage." + Environment.NewLine;
            ScrollToBottomOfMessages();

            // Subtract damage from player
            _player.CurrentHitPoints -= damageToPlayer;

            if (_player.CurrentHitPoints <= 0)
            {
                // Display message
                rtbMessages.Text += "The " + _monster.MonsterName + " killed you." + Environment.NewLine;
                ScrollToBottomOfMessages();

                // Move player to "Home"
                MoveTo(World.LocationByID("I10"));
            }

            // Refresh player data in UI
            lblHitPoints.Text = _player.CurrentHitPoints.ToString();
            UpdateInventoryListInUI();
            UpdatePotionListInUI();
        }
        private void ScrollToBottomOfMessages()
        {
            rtbMessages.SelectionStart = rtbMessages.Text.Length;
            rtbMessages.ScrollToCaret();
        }
    }
}
