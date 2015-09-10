using System;

namespace L2_login
{
	/// <summary>
	/// Summary description for Code.
	/// </summary>
	public class Code
	{
		public Code()
		{
		}

		private static char ClampMinus(int a, int b)
		{
			int x = (int)a - (int)b;
			while(x>25)
				x-=26;
			while(x<0)
				x+=26;
			char c = (char)x;
			return c;
		}
	}
}
