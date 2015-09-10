using System;

namespace L2_login
{
	public class Actions : BaseText
	{
        public int Type = 0;
        public int Category = 0;
        public int cat2_cnt = 0;
		public string Cmd = "";
		public string Icon = "";
		public string Name = "";
		public string Desc = "";//use this for the switch statement on action usage

		public void Clear()
		{
			ID = 0;
			Name = "";
		}

		public void Parse(string inp)
		{
            int pipe = 0, oldpipe = 0;
			//ID
            pipe = inp.IndexOf('|', oldpipe);
            ID = Util.GetUInt32(inp.Substring(oldpipe, pipe - oldpipe));
            oldpipe = pipe + 1;
            //Type
            pipe = inp.IndexOf('|', oldpipe);
            Type = Util.GetInt32(inp.Substring(oldpipe, pipe - oldpipe));
            oldpipe = pipe + 1;
            //Category
            pipe = inp.IndexOf('|', oldpipe);
            Category = Util.GetInt32(inp.Substring(oldpipe, pipe - oldpipe));
            oldpipe = pipe + 1;
            //cat2_cnt
            pipe = inp.IndexOf('|', oldpipe);
            cat2_cnt = Util.GetInt32(inp.Substring(oldpipe, pipe - oldpipe));
            oldpipe = pipe + 1;
			//Name
            pipe = inp.IndexOf('|', oldpipe);
            Name = inp.Substring(oldpipe, pipe - oldpipe);
            oldpipe = pipe + 1;
            //Icon
            pipe = inp.IndexOf('|', oldpipe);
            Icon = inp.Substring(oldpipe, pipe - oldpipe);
            oldpipe = pipe + 1;
            //Desc
            pipe = inp.IndexOf('|', oldpipe);
            if (pipe == -1)
            {
                Desc = inp.Substring(oldpipe, inp.Length - oldpipe);
                return;
            }
            else
            {
                Desc = inp.Substring(oldpipe, pipe - oldpipe);
                oldpipe = pipe + 1;
            }
            //Cmd
            pipe = inp.IndexOf('|', oldpipe);
            if (pipe == -1)
            {
                Cmd = inp.Substring(oldpipe, inp.Length - oldpipe);
                return;
            }
            else
            {
                Cmd = inp.Substring(oldpipe, pipe - oldpipe);
                oldpipe = pipe + 1;
            }
		}
	}//end of Classes
}
