using System;

namespace L2_login
{
	/// <summary>
	/// Summary description for ShortCuts.
	/// </summary>
	public enum ShortCut_Types : int
	{
		NULL = 0,
		ITEM = 1,
		SKILL = 2,
		ACTION = 3,
		MACRO = 4,
		RECIPE = 5
	}

	public class ShortCut : Object_Base
	{
        public ShortCut_Types Type = 0;
        public uint Level = 0;
        public uint c = 0;//byte

		public ShortCut()
		{
			Type = ShortCut_Types.NULL;
			ID = 0;
			Level = 0;
			c = 0;
		}

		public void Clear()
		{
			Type = ShortCut_Types.NULL;
			ID = 0;
			Level = 0;
			c = 0;
		}
	}//end of ShortCut
}
