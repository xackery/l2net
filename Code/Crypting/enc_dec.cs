using System;
using System.Text;

namespace L2_login
{
	/// <summary>
	/// Summary description for enc_dec.
	/// </summary>
	public class Encoder
	{
		static public uint decode_version = 1;
		private int offset;
		private StringBuilder bits;
		private StringBuilder output = new StringBuilder();
		private int i;

		public Encoder()
		{
			Reset();
		}

		public void Reset()
		{
			offset = 0;
			Set_Bits();
		}

		private void Set_Bits()
		{
			bits = new StringBuilder();
			bits.Append("THURSDAY, June29HealthDay NewsManeuvering through t"+
				"raffic while talking on the phone increases the likelihood "+
				"of an accident five-fold and is actually more dangerous tha"+
				"n driving drunk, U.S. researchers report.That finding held "+
				"true whether the driver was holding a cell phone or using a"+
				"hands-free device, the researchers noted.'As a society, we "+
				"have agreed on not tolerating the risk associated with drun"+
				"k driving,' said researcher Frank Drews, an assistant profe"+
				"ssor of psychology at the University of Utah. 'This study s"+
				"hows us that somebody who is conversing on a cell phone is "+
				"exposing him or herself and others to a similar risk -- cel"+
				"l phones actually are a higher risk,' he said.His team's re"+
				"port appears in the summer issue of the journal Human Facto"+
				"rs.In the study, 40 people followed a pace car along a pres"+
				"cribed course, using a driving simulator. Some people drove"+
				"while talking on a cell phone, others navigated while drunk"+
				"(meaning their blood-alcohol limit matched the legal limit "+
				"of 0.08 percent), and others drove with no such distraction"+
				"s or impairments.'We found an increased accident rate when "+
				"people were conversing on the cell phone,' Drews said. Driv"+
				"ers on cell phones were 5.36 times more likely to get in an"+
				"accident than non-distracted drivers, the researchers found"+
				"LONDON -- Former 'Baywatch' star David Hasselhoff h"+
				"ad surgery after severing a tendon in his right arm in an a"+
				"ccident in a London gym bathroom, his spokeswoman said Frid"+
				"ay.The 53-year-old actor, who played lifeguard Mitch Buchan"+
				"non on the TV beach drama for 11 years, was shaving at a gy"+
				"m in the Sanderson Hotel on Thursday when he hit his head o"+
				"n a chandelier, showering his arm with broken glass, his pu"+
				"blicist, Judy Katz, said.Doctors operated to repair the inj"+
				"ury and Hasselhoff spent one night at St. Thomas' Hospital "+
				"in central London, Katz said.'He's fine,' Katz said by phon"+
				"e from New York. 'He's out of the hospital and will resume "+
				"filming tomorrow.'Hasselhoff is working on an ad campaign f"+
				"or Pipex, a British internet company, she said.");

		}

		public string Encode(string _enc)
		{
			//output = new StringBuilder();
			output.Length = 0;

			for(i=0; i<_enc.Length;i++)
			{
				output.Append(Encode_Char(_enc));
			}

			offset += _enc.Length;

			bits.Append(_enc);
			bits.Remove(0,_enc.Length);
			//bits = bits.Substring(_enc.Length,bits.Length - _enc.Length);
			offset = 0;

			return output.ToString();
		}

		public string Dencode(string _enc)
		{
			//output = new StringBuilder();
			output.Length = 0;

			for(i=0; i<_enc.Length;i++)
			{
				output.Append(Decode_Char(_enc));
			}

			offset += _enc.Length;

			if(decode_version == 1)
			{
				bits.Append(output);
				bits.Remove(0,_enc.Length);
				//bits += output;
				//bits.Substring(output.Length,bits.Length - output.Length);
				offset = 0;
			}

			return output.ToString();
		}

		private char Encode_Char(string work)
		{
			char ec = ClampPlus(work[i], bits[ClampLength(i+offset)]);
			return ec;
		}

		private char Decode_Char(string work)
		{
			char dec = ClampMinus(work[i], bits[ClampLength(i+offset)]);
			return dec;
		}

		private char ClampPlus(int a, int b)
		{
			int x = (int)a + (int)b;
			while(x>255)
				x-=256;
			while(x<0)
				x+=256;
			char c = (char)x;
			return c;
		}

		private char ClampMinus(int a, int b)
		{
			int x = (int)a - (int)b;
			while(x>255)
				x-=256;
			while(x<0)
				x+=256;
			char c = (char)x;
			return c;
		}

		private int ClampLength(int l)
		{
			return l % bits.Length;
		}
	}//end of class
}
