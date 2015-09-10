using System;
using System.Text;

namespace L2_login
{
    public partial class GameServer
    {
        public static void CleanUp()
        {
            if (Globals.Script_Debugging)
            {
                Globals.l2net_home.Add_Debug("cleanup tick");
            }

            if (Globals.gamedata.running)
            {
                AddInfo.CleanUp_Char();
                AddInfo.CleanUp_NPC();
                AddInfo.CleanUp_Item();
            }
        }
    }
}
