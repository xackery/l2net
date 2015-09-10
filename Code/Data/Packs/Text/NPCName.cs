using System;

namespace L2_login
{
	public class NPCName : BaseText
	{
		public string Name = "";
		public string Description = "";
        public uint Red = 0;
        public uint Green = 0;
        public uint Blue = 0;

		public void Clear()
		{
			ID = 0;
			Name = "";
			Description = "";
			Red = 0;
			Green = 0;
			Blue = 0;
		}

		public void Parse(string inp)
		{
            int pipe = 0, oldpipe = 0;
            //ID
            pipe = inp.IndexOf('|', oldpipe);
            ID = Util.GetUInt32(inp.Substring(oldpipe, pipe - oldpipe));
            oldpipe = pipe + 1;
            //Name
            pipe = inp.IndexOf('|', oldpipe);
            Name = inp.Substring(oldpipe, pipe - oldpipe);
            oldpipe = pipe + 1;
			//Description
            pipe = inp.IndexOf('|', oldpipe);
            Description = inp.Substring(oldpipe, pipe - oldpipe);
            oldpipe = pipe + 1;
            //Blue
            pipe = inp.IndexOf('|', oldpipe);
            Blue = byte.Parse(inp.Substring(oldpipe, pipe - oldpipe), System.Globalization.NumberStyles.HexNumber);
            oldpipe = pipe + 1;
            //Green
            pipe = inp.IndexOf('|', oldpipe);
            Green = byte.Parse(inp.Substring(oldpipe, pipe - oldpipe), System.Globalization.NumberStyles.HexNumber);
            oldpipe = pipe + 1;
            //Red
            Red = byte.Parse(inp.Substring(oldpipe, inp.Length - oldpipe), System.Globalization.NumberStyles.HexNumber);
		}
	}//end of NPCName
}
