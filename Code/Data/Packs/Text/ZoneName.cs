using System;

namespace L2_login
{
	public class ZoneName : BaseText
	{
		public string Name = "";
        public uint WorldX = 0;
        public uint WorldY = 0;
        public double MinZ = 0;
        public double MaxZ = 0;

		public void Clear()
		{
			ID = 0;
			Name = "";
            WorldX = 0;
            WorldY = 0;
            MinZ = 0;
            MaxZ = 0;
		}

		public void Parse(string inp)
		{
            int pipe = 0, oldpipe = 0;
            //ID
            pipe = inp.IndexOf('|', oldpipe);
            ID = Util.GetUInt32(inp.Substring(oldpipe, pipe - oldpipe));
            oldpipe = pipe + 1;
            //WorldX
            pipe = inp.IndexOf('|', oldpipe);
            WorldX = Util.GetUInt32(inp.Substring(oldpipe, pipe - oldpipe));
            oldpipe = pipe + 1;
            //WorldY
            pipe = inp.IndexOf('|', oldpipe);
            WorldY = Util.GetUInt32(inp.Substring(oldpipe, pipe - oldpipe));
            oldpipe = pipe + 1;
            //MaxZ
            pipe = inp.IndexOf('|', oldpipe);
            MaxZ = Util.GetDouble(inp.Substring(oldpipe, pipe - oldpipe));
            oldpipe = pipe + 1;
            //MinZ
            pipe = inp.IndexOf('|', oldpipe);
            MinZ = Util.GetDouble(inp.Substring(oldpipe, pipe - oldpipe));
            oldpipe = pipe + 1;
            //Name
            Name = inp.Substring(oldpipe, inp.Length - oldpipe);
        }
	}//end of Races
}
