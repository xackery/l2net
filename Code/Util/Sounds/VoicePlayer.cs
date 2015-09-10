#if DEBUG
    #define SILENT
#endif

using System;
using System.Collections.Generic;
using System.Text;

namespace L2_login
{
    class VoicePlayer
    {
        public static WSounds Sounds;

        public static void Init()
        {
            Sounds = new WSounds();
        }

        public static void PlayAlarm()
        {
#if !SILENT
            Sounds.PlayWavFile(Globals.PATH + "\\Sounds\\alarm.wav");
#endif
        }

        public static void PlayWavFile(string wav)
        {
            Sounds.PlayWavFile(wav);
        }

        public static void PlaySound(int id)
        {
#if !SILENT
            if (id >= 1 && id <= 9)
            {
                switch (Globals.Voice)
                {
                    case 0:
                        //nothing
                        break;
                    case 1://A
                        Sounds.PlayWavResource("A" + id.ToString() + ".wav");
                        break;
                    case 2://C
                        Sounds.PlayWavResource("C" + id.ToString() + ".wav");
                        break;
                    case 3://H
                        Sounds.PlayWavResource("H" + id.ToString() + ".wav");
                        break;
                    case 4://R
                        Sounds.PlayWavResource("R" + id.ToString() + ".wav");
                        break;
                    case 5://T
                        Sounds.PlayWavResource("T" + id.ToString() + ".wav");
                        break;
                    case 6://Ch
                        Sounds.PlayWavResource("Ch" + id.ToString() + ".wav");
                        break;
                    case 7://M
                        Sounds.PlayWavResource("M" + id.ToString() + ".wav");
                        break;
                }
            }
            else
            {
                Globals.l2net_home.Add_Error("invalid sound id", false);
                return;
            }
#endif
        }
    }
}
