using System;
using System.Collections.Generic;
using System.Text;
using SlimDX.Direct3D9;

namespace L2_login
{
    public class MapData
    {
        public string FileName = "";
        public int X = -100;
        public int Y = -100;
        public int Z_Min = -1000000;
        public int Z_Max = 1000000;
        public bool Encrypted = false;
        private System.IO.MemoryStream _image = null;
        private Texture _dxTexture;

        public float UpperX = 0;
        public float UpperY = 0;
        public float LowerX = 0;
        public float LowerY = 0;

        private System.DateTime _lastaccessed_imagestream = new DateTime(0L);
        private System.DateTime _lastaccessed_dxtexture = new DateTime(0L);

        public void ReleaseResources()
        {
            if (_image != null)
            {
                if ((System.DateTime.Now - _lastaccessed_imagestream).Ticks > Globals.MAP_HOLD_STREAM)
                {
                    //release the stream
                    _image.Close();
                    _image = null;
                    _lastaccessed_imagestream = new DateTime(0L);
                }
            }
            if (_dxTexture != null)
            {
                if ((System.DateTime.Now - _lastaccessed_dxtexture).Ticks > Globals.MAP_HOLD_TEXTURE)
                {
                    //release the stream
                    _dxTexture.Dispose();
                    _dxTexture = null;
                    _lastaccessed_dxtexture = new DateTime(0L);
                }
            }
        }

        public System.IO.MemoryStream Image
        {
            get
            {
                _lastaccessed_imagestream = System.DateTime.Now;
                return _image;
            }
            set
            {
                _lastaccessed_imagestream = System.DateTime.Now;
                _image = value;
            }
        }
        public Texture dxTexture
        {
            get
            {
                _lastaccessed_dxtexture = System.DateTime.Now;
                return _dxTexture;
            }
            set
            {
                _lastaccessed_dxtexture = System.DateTime.Now;
                _dxTexture = value;
            }
        }

        public void Clear()
        {
            FileName = "";
            X = -100;
            Y = -100;
            Z_Min = -1000000;
            Z_Max = 1000000;
            UpperX = 0;
            UpperY = 0;
            LowerX = 0;
            LowerY = 0;
        }

        public void Setup()
        {
            UpperX = (X * Globals.UNITS - Globals.ModX);
            UpperY = (Y * Globals.UNITS - Globals.ModY);
            LowerX = ((X + 1) * Globals.UNITS - Globals.ModX);
            LowerY = ((Y + 1) * Globals.UNITS - Globals.ModY);
        }

        public void Parse(string inp)
        {
            int pipe;
            //FileName
            pipe = inp.IndexOf('|');
            FileName = inp.Substring(0, pipe);
            inp = inp.Remove(0, pipe + 1);
            //X
            pipe = inp.IndexOf('|');
            X = Util.GetInt32(inp.Substring(0, pipe));
            inp = inp.Remove(0, pipe + 1);
            //Y
            pipe = inp.IndexOf('|');
            Y = Util.GetInt32(inp.Substring(0, pipe));
            inp = inp.Remove(0, pipe + 1);
            //Z_Min
            pipe = inp.IndexOf('|');
            Z_Min = Util.GetInt32(inp.Substring(0, pipe));
            inp = inp.Remove(0, pipe + 1);

            pipe = inp.IndexOf('|');
            if (pipe == -1)
            {
                //Z_Max
                Z_Max = Util.GetInt32(inp);
                //Encrypted
                Encrypted = false;
            }
            else
            {
                //Z_Max
                Z_Max = Util.GetInt32(inp.Substring(0, pipe));
                inp = inp.Remove(0, pipe + 1);
                //Encrypted
                if (Util.GetInt32(inp) == 0)
                {
                    Encrypted = false;
                }
                else
                {
                    Encrypted = true;
                }
            }

            Setup();
        }
    }
}
