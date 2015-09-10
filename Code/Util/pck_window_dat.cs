using System;
/*
using System.Collections.Generic;
using System.Linq;
using System.Text;
*/
namespace L2_login
{
    public class pck_window_dat
    {

        public int action; // 1 - add to arraylist , 2 - clear arraylist , 3 - refresh , 4 - save arraylist , 5 - load arraylist
        public int type; // 1 - Client , 2 - Server
        public string time;
        public byte[] bytebuffer;
        
          public pck_window_dat()
            {
                        action =new int();
                        type =new int();
                        time ="";
         }
        public pck_window_dat(pck_window_dat dat)
                {
                    action = dat.action;
                    type = dat.type;
                    time = String.Copy(dat.time);
                    bytebuffer = new byte[dat.bytebuffer.Length];
                    for (int i = 0; i < bytebuffer.Length; i++)
                    {
                        bytebuffer[i] = dat.bytebuffer[i];
                    }
                  }
        public pck_window_dat(byte[] bbufer)
        {
            bytebuffer = new byte[bbufer.Length];
            for (int i = 0; i < bytebuffer.Length; i++)
            {
                bytebuffer[i] = bbufer[i];
            }

        }
    }
}
