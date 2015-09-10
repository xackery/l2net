using System;

namespace L2_login
{
    public enum NetPacketType : uint
    {
        Update = 0,
        Script = 1,
        ScriptBB = 2,
        NPCUpdate = 3
    }

	public class NetPacket
	{
        public volatile uint Type;
        private string _Sender;
        public volatile uint SenderID;
        private string _Name;
        public volatile uint ID;
        public volatile float MaxCP;
        public volatile float CurCP;
        public volatile float MaxHP;
        public volatile float CurHP;
        public volatile float MaxMP;
        public volatile float CurMP;
        public volatile int Param1;
        public volatile int Param2;
        public volatile int Param3;
        public volatile int Param4;
        private ByteBuffer _BBuff;

        private readonly object SenderLock = new object();
        private readonly object NameLock = new object();
        private readonly object BBuffLock = new object();

        public ByteBuffer BBuff
        {
            get
            {
                ByteBuffer tmp;
                lock (BBuffLock)
                {
                    tmp = this._BBuff;
                }
                return tmp;
            }
            set
            {
                lock (BBuffLock)
                {
                    _BBuff = value;
                }
            }
        }
		public string Name
		{
			get
			{
				string tmp;
				lock(NameLock)
				{
					tmp = this._Name;
				}
				return tmp;
			}
			set
			{
				lock(NameLock)
				{
					_Name = value;
				}
			}
		}
        public string Sender
        {
            get
            {
                string tmp;
                lock (SenderLock)
                {
                    tmp = this._Sender;
                }
                return tmp;
            }
            set
            {
                lock (SenderLock)
                {
                    _Sender = value;
                }
            }
        }

		public NetPacket()
		{
			Clear();
		}

		public void Clear()
		{
			Type = 0;
            Sender = "";
            SenderID = 0;
			Name = "";
            ID = 0;
			MaxCP = 0;
			CurCP = 0;
			MaxHP = 0;
			CurHP = 0;
			MaxMP = 0;
			CurMP = 0;
			Param1 = 0;
			Param2 = 0;
			Param3 = 0;
			Param4 = 0;
		}

		public void Parse(ByteBuffer t_buff)
		{
			//int off = 0;
			Type = t_buff.ReadUInt32();
            Sender = t_buff.ReadString();
            SenderID = t_buff.ReadUInt32();

            switch(Type)
            {
                case (uint)NetPacketType.Update:
                case (uint)NetPacketType.Script:
                    Name = t_buff.ReadString();
                    ID = t_buff.ReadUInt32();
                    MaxCP = t_buff.ReadUInt32();
                    CurCP = t_buff.ReadUInt32();
                    MaxHP = t_buff.ReadUInt32();
                    CurHP = t_buff.ReadUInt32();
                    MaxMP = t_buff.ReadUInt32();
                    CurMP = t_buff.ReadUInt32();
                    Param1 = t_buff.ReadInt32();
                    Param2 = t_buff.ReadInt32();
                    Param3 = t_buff.ReadInt32();
                    Param4 = t_buff.ReadInt32();
                    break;
                case (uint)NetPacketType.ScriptBB:
                    BBuff = new ByteBuffer(t_buff.Get_ByteArray2());
                    break;
            }
		}

		public void Parse(byte[] buff)
		{
            ByteBuffer bbuff = new ByteBuffer(buff);
            Parse(bbuff);
		}

		public byte[] GetBytes()
		{
            ByteBuffer t_bbuff = new ByteBuffer();
            t_bbuff.WriteUInt32(Type);
            t_bbuff.WriteString(Sender);
            t_bbuff.WriteUInt32(SenderID);

            switch(Type)
            {
                case (uint)NetPacketType.Update:
                case (uint)NetPacketType.Script:
                    t_bbuff.WriteString(Name);
                    t_bbuff.WriteUInt32(ID);
                    t_bbuff.WriteUInt32((uint)MaxCP);
                    t_bbuff.WriteUInt32((uint)CurCP);
                    t_bbuff.WriteUInt32((uint)MaxHP);
                    t_bbuff.WriteUInt32((uint)CurHP);
                    t_bbuff.WriteUInt32((uint)MaxMP);
                    t_bbuff.WriteUInt32((uint)CurMP);
                    t_bbuff.WriteInt32(Param1);
                    t_bbuff.WriteInt32(Param2);
                    t_bbuff.WriteInt32(Param3);
                    t_bbuff.WriteInt32(Param4);
                    break;
                case (uint)NetPacketType.ScriptBB:
                    t_bbuff.WriteBytes(BBuff.Get_ByteArray());
                    break;
            }

            t_bbuff.TrimToIndex();

			return t_bbuff.Get_ByteArray();
		}
	}
}
