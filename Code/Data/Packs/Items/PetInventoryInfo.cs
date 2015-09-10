using System;


namespace L2_login
{
    public class PetInventoryInfo : Object_Base
    {

        public volatile uint ItemID = 0;
        public volatile uint Slot = 0;
        //public volatile ulong Count = 0;
        private ulong _Count = 0;
        public volatile uint Type1 = 0;
        public volatile uint Type2 = 0;
        public volatile uint Type3 = 0;
        public volatile uint isEquipped = 0;
        public volatile uint Enchant = 0;
        public volatile uint Type4 = 0;
        public volatile int AttackElementType = 0;
        public volatile int AttackElementPower = 0;
        public volatile int fire_defense = 0;
        public volatile int water_defense = 0;
        public volatile int wind_defense = 0;
        public volatile int earth_defense = 0;
        public volatile int unholy_defense = 0;
        public volatile int holy_defense = 0;
        public volatile int enchant_effect_0 = 0;
        public volatile int enchant_effect_1 = 0;
        public volatile int enchant_effect_2 = 0;

        public volatile bool InNewList = true;

        private readonly object CountLock = new object();

        public ulong Count
        {
            get
            {
                ulong tmp;
                lock (CountLock)
                {
                    tmp = this._Count;
                }
                return tmp;
            }
            set
            {
                lock (CountLock)
                {
                    _Count = value;
                }
            }
        }

        public void Load(ByteBuffer buff)
        {
            Type1 = buff.ReadUInt16();
            ID = buff.ReadUInt32();
            ItemID = buff.ReadUInt32();
            Count = buff.ReadUInt64();
            Type2 = buff.ReadUInt16();
            Type3 = buff.ReadUInt16();
            isEquipped = buff.ReadUInt16();
            Slot = buff.ReadUInt32();
            Enchant = buff.ReadUInt16();
            Type4 = buff.ReadUInt16();
            AttackElementType = buff.ReadInt16();
            AttackElementPower = buff.ReadInt16();
            fire_defense = buff.ReadInt16();
            water_defense = buff.ReadInt16();
            wind_defense = buff.ReadInt16();
            earth_defense = buff.ReadInt16();
            unholy_defense = buff.ReadInt16();
            holy_defense = buff.ReadInt16();
        }
    }
}
