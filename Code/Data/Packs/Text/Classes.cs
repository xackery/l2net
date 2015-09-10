using System;

namespace L2_login
{
	public class Classes : BaseText
	{
		public string Name = "";

		public void Clear()
		{
			ID = 0;
			Name = "";
		}

		public void Parse(string inp)
		{
            int pipe;
            //ID
            pipe = inp.IndexOf('|');
            ID = Util.GetUInt32(inp.Substring(0, pipe));
            //Name
            Name = inp.Substring(pipe + 1, inp.Length - pipe - 1);
		}
	}//end of Classes
}
