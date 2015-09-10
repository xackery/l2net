using System;

namespace L2_login
{
    public class NPCString : BaseText
    {
        public string text = "";

        public void Clear()
        {
            ID = 0;
            text = "";
        }

        public void Parse(string inp)
        {
            int pipe = 0, oldpipe = 0;
            //ID
            pipe = inp.IndexOf('|', oldpipe);
            ID = Util.GetUInt32(inp.Substring(oldpipe, pipe - oldpipe));
            oldpipe = pipe + 1;
            //Text
            pipe = inp.IndexOf('|', oldpipe);
            text = inp.Substring(oldpipe, inp.Length - oldpipe);
        }

    }

}
