using System;

namespace L2_login
{
	public class SkillInfo
	{
		public uint ID = 0;
        public uint Level = 0;
        public string Name = "";
        public string Desc1 = "";
        public string Desc2 = "";
        public string Desc3 = "";

		public void Parse(string inp)
		{
			int pipe = 0, oldpipe = 0;
			//ID
            pipe = inp.IndexOf('|', oldpipe);
            ID = Util.GetUInt32(inp.Substring(oldpipe, pipe - oldpipe));
            oldpipe = pipe + 1;
			//Level
            pipe = inp.IndexOf('|', oldpipe);
            Level = Util.GetUInt32(inp.Substring(oldpipe, pipe - oldpipe));
            oldpipe = pipe + 1;
            //Name
            pipe = inp.IndexOf('|', oldpipe);
            Name = inp.Substring(oldpipe, pipe - oldpipe);
            oldpipe = pipe + 1;
            //Desc1
            pipe = inp.IndexOf('|', oldpipe);
            Desc1 = inp.Substring(oldpipe, pipe - oldpipe);
            oldpipe = pipe + 1;
            //Desc2
            pipe = inp.IndexOf('|', oldpipe);
            Desc2 = inp.Substring(oldpipe, pipe - oldpipe);
            oldpipe = pipe + 1;
            //Desc3
            Desc3 = inp.Substring(oldpipe, inp.Length - oldpipe);
		}
	}
}
