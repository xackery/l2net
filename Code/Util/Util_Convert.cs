using System;
using System.Collections.Generic;
using System.Text;

namespace L2_login
{
    public static partial class Util
    {
        static public double Double_Cap(double d)
        {
            try
            {
                if (d > 1 || double.IsPositiveInfinity(d))
                {
                    return 1;
                }

                if (d < 0 || double.IsNegativeInfinity(d) || double.IsNaN(d))
                {
                    return 0;
                }

                return d;
            }
            catch
            {
                return 0;
            }
        }

        static public double Double_Cap2(double d)
        {
            try
            {
                if (double.IsPositiveInfinity(d) || double.IsNegativeInfinity(d) || double.IsNaN(d))
                {
                    return 0;
                }

                return d;
            }
            catch
            {
                return 0;
            }
        }

        static public float Float_Cap(float d)
        {
            try
            {
                if (d > 1 || float.IsPositiveInfinity(d))
                {
                    return 1;
                }

                if (d < 0 || float.IsNegativeInfinity(d) || float.IsNaN(d))
                {
                    return 0;
                }

                return d;
            }
            catch
            {
                return 0;
            }
        }

        static public float Float_Cap2(float d)
        {
            try
            {
                if (float.IsPositiveInfinity(d) || float.IsNegativeInfinity(d) || float.IsNaN(d))
                {
                    return 0;
                }

                return d;
            }
            catch
            {
                return 0;
            }
        }

        static public int Double_Int32(double d)
        {
            int i;

            try
            {
                i = System.Convert.ToInt32(d);
            }
            catch
            {
                i = 0;
            }

            return i;
        }

        static public int Float_Int32(float d)
        {
            int i;

            try
            {
                i = System.Convert.ToInt32(d);
            }
            catch
            {
                i = 0;
            }

            return i;
        }

        static public string Remove_Dec(string str)
        {
            //if (str.IndexOf(Globals.NumberGroupSeparator) != -1)
            str = str.Replace(Globals.NumberGroupSeparator, "");

            if (str.IndexOf(Globals.NumberDecimalSeparator) != -1)
                str = str.Substring(0, str.IndexOf(Globals.NumberDecimalSeparator));

            return str;
        }

        static public double GetDouble(string str)
        {
            try
            {
                if (str.Length == 0)
                    return 0;
                //return System.Convert.ToDouble(str);
                return double.Parse(str, System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
            }
            catch
            {
                return 0;
            }
        }

        static public long GetInt64(string str)
        {
            try
            {
                str = Util.Remove_Dec(str);
                if (str.Length == 0)
                    return 0;
                //return System.Convert.ToInt64(str);
                return long.Parse(str, System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
            }
            catch
            {
                return 0;
            }
        }

        static public ulong GetUInt64(string str)
        {
            try
            {
                str = Util.Remove_Dec(str);
                if (str.Length == 0)
                    return 0;
                //return System.Convert.ToUInt64(str);
                return ulong.Parse(str, System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
            }
            catch
            {
                return 0;
            }
        }

        static public int GetInt32(string str)
        {
            try
            {
                str = Util.Remove_Dec(str);
                if (str.Length == 0)
                    return 0;
                //return System.Convert.ToInt32(str);
                return int.Parse(str, System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
            }
            catch
            {
                return 0;
            }
        }

        static public uint GetUInt32(string str)
        {
            try
            {
                str = Util.Remove_Dec(str);
                if (str.Length == 0)
                    return 0;
                //return System.Convert.ToUInt32(str);
                return uint.Parse(str, System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
            }
            catch
            {
                return 0;
            }
        }
    }
}
