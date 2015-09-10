//#define TESTING

using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace L2_login
{
    class MapThread
    {
        public static void DrawGameThread()
        {
#if TESTING && DEBUG
                while (true)
                {
					try
					{
                        if (Globals.map_window != null && Globals.map_window.IsDisposed == false)
                        {
                            Globals.map_window.Draw();
                        }
					}
					catch
					{
						Globals.l2net_home.Add_Error("failed to draw the map");
					}

                    System.Threading.Thread.Sleep(Globals.SLEEP_DrawGameThread);//sleep for 200...5 fps sorta
                }
#else
            try
			{
                while (Globals.gamedata.running)
                {
                    //draw everything here... but not when minimized
                    if (Globals.gamedata.drawing_game && Globals.l2net_home.WindowState != System.Windows.Forms.FormWindowState.Minimized)
                    {
						    try
						    {
                                if (Globals.map_window != null && Globals.map_window.IsDisposed == false)
                                {
                                    Globals.map_window.Draw();
                                }
						    }
						    catch
						    {
							    Globals.l2net_home.Add_Error("failed to draw the map");
						    }
                    }

                    //we need to handle the caching of maps here...
                    //also... remove far away maps that are no longer needed...

                    //

                    System.Threading.Thread.Sleep(Globals.SLEEP_DrawGameThread);//sleep for 200...5 fps sorta
                }
			}
			catch
			{
				Globals.l2net_home.Add_Error("crash: drawgame thread ... hardcore gayporn badness");
			}

            Globals.l2net_home.Add_Text("drawgame thread ending now", Globals.Red, TextType.BOT);
#endif
        }

        public static float GetZoom()
        {
            return ((float)Globals.l2net_home.trackBar_map_zoom.Value) / 2;
        }
    }
}
