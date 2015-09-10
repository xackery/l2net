using System;
using System.Windows.Forms;

namespace L2_login
{
    public enum InventorySlots : uint
    {
        Shirt = 0x01,
        Ear = 0x06,
        Neck = 0x08,
        Finger = 0x30,
        Head = 0x40,
        RHand = 0x80,
        LHand = 0x100,
        Gloves = 0x200,
        Chest = 0x400,
        Pants = 0x800,
        Feet = 0x1000,
        Dunno = 0x2000,
        LRHand = 0x4000,
        FullBody = 0x8000,
        Accessory = 0x40000,
    }

    public enum InventoryElement : int
    {
        None = -1,
        Fire = 0,
        Water = 1,
        Wind = 2,
        Earth = 3,
        Holy = 4,
        Unholy = 5,
    }

	public class InventoryInfo : Object_Base
	{
        public volatile uint Type = 0;//ushort
        public volatile uint ItemID = 0;//this is the id from the file
        private ulong _Count = 0;
        public volatile uint Type2 = 0;
        public volatile uint Type3 = 0;//ushort
        public volatile uint isEquipped = 0;//0x01 equipped | 0x00 not//ushort
        public volatile InventorySlots Slot = 0;
        public volatile uint Enchant = 0;//ushort
        public volatile uint Type4 = 0;//ushort
        public volatile uint AugID = 0;//C6
        public volatile uint Mana = 0;//C6
        public volatile uint Location = 0;//location slot???
        public volatile bool InNewList = true;

        public volatile InventoryElement attack_element;
        public volatile int attack_magnitude = 0;
        public volatile int fire_defense = 0;
        public volatile int water_defense = 0;
        public volatile int wind_defense = 0;
        public volatile int earth_defense = 0;
        public volatile int unholy_defense = 0;
        public volatile int holy_defense = 0;
        public volatile uint location_slot = 0;
        public volatile int lease_time = 0;
        public volatile int enchant_effect_0 = 0;
        public volatile int enchant_effect_1 = 0;
        public volatile int enchant_effect_2 = 0;

		private readonly object CountLock = new object();

		public ulong Count
		{
			get
			{
                ulong tmp;
				lock(CountLock)
				{
					tmp = this._Count;
				}
				return tmp;
			}
			set
			{
				lock(CountLock)
				{
					_Count = value;
				}
			}
		}

        public void Load(ByteBuffer buff, int statsyesno)
        {
            uint previousEquipted = 0;
            ulong previousCount = 0;
            //ch(hdddqhhhdhhdddhhhhhhhhhhhhd)
            ID = buff.ReadUInt32();
            ItemID = buff.ReadUInt32();
            Location = buff.ReadUInt32(); //Location slot

            if (statsyesno == 1)
            {
                foreach (InventoryInfo inv_inf in Globals.gamedata.inventory.Values)
                {
                    if (Util.GetInventoryItemID(inv_inf.ID) == ItemID || inv_inf.ID == ID)
                    {
                        previousEquipted = inv_inf.isEquipped;
                        previousCount = inv_inf.Count;
                        break;
                    }
                }
            }

            Count = buff.ReadUInt64();
            Type2 = buff.ReadUInt16();
            Type3 = buff.ReadUInt16();
            isEquipped = buff.ReadUInt16();
            Slot = (InventorySlots)buff.ReadUInt32();
            Enchant = buff.ReadUInt16();
            Type4 = buff.ReadUInt16();
            AugID = buff.ReadUInt32();
            Mana = buff.ReadUInt32();
            lease_time = buff.ReadInt32();

            buff.ReadUInt16();

            attack_element = (InventoryElement)buff.ReadUInt16();
            attack_magnitude = buff.ReadInt16();
            fire_defense = buff.ReadInt16();
            water_defense = buff.ReadInt16();
            wind_defense = buff.ReadInt16();
            earth_defense = buff.ReadInt16();
            unholy_defense = buff.ReadInt16();
            holy_defense = buff.ReadInt16();
            enchant_effect_0 = buff.ReadUInt16();
            enchant_effect_1 = buff.ReadUInt16();
            enchant_effect_2 = buff.ReadUInt16();

            buff.ReadUInt32();

            //stats stuff
            if (statsyesno == 1)
            {
                updateStats(ID, ItemID, previousEquipted, isEquipped, previousCount, Count);
            }
        }

        public void updateStats(uint uID, uint uItemID, uint previousisEquipped, uint isEquipped, ulong previousCount, ulong newCount)
        {
            if (previousisEquipped == 1 || isEquipped == 1)
            {
                return;
            }

            long differenceint = 0;
            //this can be negative... if so we dont want to do anything... btw dont change these to ulong, or it will break, I'm warning you Oddi!!! don't do it!!!
            differenceint = (long)newCount - (long)previousCount;
            if (differenceint < 0)
            {
                //no way, I didn't lose any items like soulshots. 
                return;
            }

            if (uItemID == 57)
            {
                //this is updated with the xp/sp stuff.... just because it updates more often.
                if (!Globals.gamedata.initial_Adena_Gained_received)
                {
                    GameData.initial_Adena = Count;
                    Globals.gamedata.initial_Adena_Gained_received = true;
                }
                else
                {
                    GameData.current_Adena = Count;
                }

            }
            else
            {
                int countlistview = Globals.l2net_home.listView_stats.Items.Count;
                int found = 0;
                if (countlistview > 0)
                {
                    for (int i = 0; i <= countlistview - 1; i++)
                    {
                        if (Globals.l2net_home.listView_stats.Items[i].SubItems[1].Text.Equals(uItemID.ToString()))
                        {
                            ulong current = 0;
                            ulong replaceint = 0;

                            //get the current listview's amount
                            current = System.Convert.ToUInt64(Globals.l2net_home.listView_stats.Items[i].SubItems[2].Text);

                            //necessary to add items together that aren't stackable. 
                            if (differenceint == 0)
                            {
                                differenceint += 1;
                            }

                            //add the listview's current amount and the amount we gained from this packet.
                            replaceint = current + (ulong)differenceint;

                            //replace the count for the specific item
                            Globals.l2net_home.listView_stats.Items[i].SubItems[2].Text = replaceint.ToString();

                            //lets break out of this loop...
                            i = countlistview;
                            found = 1;
                        }
                    }
                }
                if (found == 0)
                {
                    //didnt find the item, let's create a new item in our listview.
                    ListViewItem statsItem = new ListViewItem(Util.GetItemName(uItemID));
                    statsItem.SubItems.Add(uItemID.ToString());
                    statsItem.SubItems.Add(differenceint.ToString());
                    statsItem.SubItems.Add(uID.ToString());
                    Globals.l2net_home.listView_stats.Items.Add(statsItem);
                }
            }
        }
	}
}
