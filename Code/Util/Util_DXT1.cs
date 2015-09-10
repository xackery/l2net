using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace L2_login
{
    public static partial class Util
    {

        public static Bitmap GetBMP(byte[] data)
        {
            int offset = 128;

            Bitmap bmp = new Bitmap(16, 12);
            int x_off = 0;
            int y_off = 0;

            offset += 32;

            for (int i = 0; i < 12; i++)
            {
                //read the color data in
                ushort c1 = System.BitConverter.ToUInt16(data, offset); offset += 2;
                ushort c2 = System.BitConverter.ToUInt16(data, offset); offset += 2;

                int r1 = (c1 >> 11);
                int g1 = (c1 ^ (c1 >> 11 << 11)) >> 5;
                int b1 = (c1 ^ (c1 >> 5 << 5));

                int r2 = (c2 >> 11);
                int g2 = (c2 ^ (c2 >> 11 << 11)) >> 5;
                int b2 = (c2 ^ (c2 >> 5 << 5));

                //scale colors to 32 bit
                float sr1 = (float)r1 / 31;
                float sg1 = (float)g1 / 63;
                float sb1 = (float)b1 / 31;

                float sr2 = (float)r2 / 31;
                float sg2 = (float)g2 / 63;
                float sb2 = (float)b2 / 31;

                byte nv;
                int index1, index2, index3, index4;

                for (int iy = 0; iy < 4; iy += 1)
                {
                    nv = data[offset]; offset++;

                    index4 = (nv >> 6);
                    index3 = ((nv ^ (nv >> 6 << 6)) >> 4);
                    index2 = ((nv ^ (nv >> 4 << 4)) >> 2);
                    index1 = (nv ^ (nv >> 2 << 2));

                    bmp.SetPixel(x_off + 0, y_off + iy, GetColor(index1, sr1, sg1, sb1, sr2, sg2, sb2));
                    bmp.SetPixel(x_off + 1, y_off + iy, GetColor(index2, sr1, sg1, sb1, sr2, sg2, sb2));
                    bmp.SetPixel(x_off + 2, y_off + iy, GetColor(index3, sr1, sg1, sb1, sr2, sg2, sb2));
                    bmp.SetPixel(x_off + 3, y_off + iy, GetColor(index4, sr1, sg1, sb1, sr2, sg2, sb2));
                }

                x_off += 4;
                if (x_off == 16)
                {
                    x_off = 0;
                    y_off += 4;
                }
            }

            return bmp;
        }

        private static Color GetColor(int index, float r1, float g1, float b1, float r2, float g2, float b2)
        {
            //check if c1 > 2
            float s1 = r1 + g1 + b1;
            float s2 = r2 + g2 + b2;

            float red = 255, green = 255, blue = 255;

            if (s1 >= s2)
            {
                switch (index)
                {
                    case 0:
                        red = r1 * 255;
                        green = g1 * 255;
                        blue = b1 * 255;
                        break;
                    case 1:
                        red = r2 * 255;
                        green = g2 * 255;
                        blue = b2 * 255;
                        break;
                    case 2:
                        red = r1 * 170 + r2 * 85;
                        green = g1 * 170 + g2 * 85;
                        blue = b1 * 170 + b2 * 85;
                        break;
                    case 3:
                        red = r1 * 85 + r2 * 170;
                        green = g1 * 85 + g2 * 170;
                        blue = b1 * 85 + b2 * 170;
                        break;
                }
            }
            else
            {
                switch (index)
                {
                    case 0:
                        red = r1 * 255;
                        green = g1 * 255;
                        blue = b1 * 255;
                        break;
                    case 1:
                        red = r2 * 255;
                        green = g2 * 255;
                        blue = b2 * 255;
                        break;
                    case 2:
                    case 3:
                        red = ((r1 + r2) / 2) * 255;
                        green = ((g1 + g2) / 2) * 255;
                        blue = ((b1 + b2) / 2) * 255;
                        break;
                    /*case 3:
                        red = 255;
                        green = 255;
                        blue = 255;
                        break;*/
                }
            }

            return Color.FromArgb((int)red, (int)green, (int)blue);
        }
    }
}
