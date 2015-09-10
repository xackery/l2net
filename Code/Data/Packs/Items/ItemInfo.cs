using System;

namespace L2_login
{
	public class ItemInfo : Object_Base
	{
        public volatile uint ItemID = 0;//this is the item id from the file
        public volatile float X = 0;
        public volatile float Y = 0;
        public volatile float Z = 0;
        public volatile uint Stackable = 0;
        public ulong Count = 0;
        public volatile uint DroppedBy = 0;
        public volatile float DropRadius = 2.0F;
        public volatile bool Ignore = false;
        public volatile bool HasMesh = false;
        public volatile bool HasValidZ = false;
        public volatile bool Loadedin = false; //if the item wasn't dropped while we were here, we need to make sure we check for the Z distance before trying to pick it up...

        private readonly object CountLock = new object();

		public void Load(ByteBuffer buff)
		{

            ID = buff.ReadUInt32();
            ItemID = buff.ReadUInt32();
            X = buff.ReadInt32();
            Y = buff.ReadInt32();
            Z = buff.ReadInt32();
            Stackable = buff.ReadUInt32();

            //this is an item that was dropped previously... need to check for the Z range in the pickup function
            HasValidZ = true;
            Loadedin = true;

            lock (CountLock)
            {
                Count = buff.ReadUInt64();
            }

            HasMesh = Util.GetItemHasMesh(ItemID);
            if (HasMesh)
            {
                string tempitemname = Util.GetItemName(ItemID);
                if (tempitemname.Length < 3)
                {
                    HasMesh = false;
                    GameData.meshless_ignored += 1;
                }
            }
            
            

            //Globals.l2net_home.Add_Text(" ID" + ID + " ItemID " + ItemID + " X " + X + " Y " + Y + " Z " + Z + " Stackable " + Stackable + " Count " + Count + " HasMesh " + HasMesh, Globals.Green, TextType.BOT);
		}

		public void LoadDrop(ByteBuffer buff)
		{

            DroppedBy = buff.ReadUInt32();
            ID = buff.ReadUInt32();
            ItemID = buff.ReadUInt32();
            X = buff.ReadInt32();
            Y = buff.ReadInt32();
            Z = buff.ReadInt32();
            if (CheckMobZ(DroppedBy))
            {
                HasValidZ = true;
            }

            Stackable = buff.ReadUInt32();
            lock (CountLock)
            {
                Count = buff.ReadUInt64();
            }
            HasMesh = Util.GetItemHasMesh(ItemID);
            if (HasMesh)
            {
                string tempitemname = Util.GetItemName(ItemID);
                if (tempitemname.Length < 3)
                {
                    HasMesh = false;
                    GameData.meshless_ignored += 1;
                }
            }
            
            

            //Globals.l2net_home.Add_Text("length" + length + " ID" + ID + " ItemID " + ItemID + " X " + X + " Y " + Y + " Z " + Z + " Stackable " + Stackable + " Count " + Count + " HasMesh " + HasMesh, Globals.Green, TextType.BOT);
        }
        private bool CheckMobZ(uint mob)
        {
            Globals.NPCLock.EnterReadLock();
            Globals.PlayerLock.EnterReadLock();
            bool isgoodmob = false;

            try
            {
                NPCInfo npc = Util.GetNPC(mob);
                //Globals.l2net_home.Add_Text("z " + Math.Abs(Z - npc.Z), Globals.Green, TextType.BOT);
                if (npc != null)
                {
                    if (Math.Abs(Z - npc.Z) < Globals.PICKUP_Z_Diff)
                    {
                        isgoodmob = true;
                    }

                }
                else
                {
                    CharInfo player = Util.GetChar(mob);
                    if (player != null)
                    {
                        if (Math.Abs(Z - npc.Z) < Globals.PICKUP_Z_Diff)
                        {
                            isgoodmob = true;
                        }
                    }
                    else
                    { 
                        //some kind of magical item? XD
                    }
                }

            }
            catch
            { 
                //meh...
            }
            finally
            {
                Globals.NPCLock.ExitReadLock();
                Globals.PlayerLock.ExitReadLock();
            }

            return isgoodmob;
            
        }
	}
}
