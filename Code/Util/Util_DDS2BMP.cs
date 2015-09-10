using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using SlimDX.Direct3D9;
using System.Windows.Forms;
using System.IO;

namespace L2_login
{
    public static partial class Util
    {
        private static Device GraphicCard = null;
        private static PresentParameters GraphicSettings = new PresentParameters();

        private static Panel GraphicPanel = new Panel();

        public static Bitmap Dds2BMP(byte[] Bytes)
        {

            var _with1 = GraphicSettings;
            _with1.SwapEffect = SwapEffect.Discard;
            _with1.Windowed = true;
            _with1.BackBufferCount = 2;
            _with1.PresentationInterval = PresentInterval.Immediate;
            _with1.AutoDepthStencilFormat = Format.D16;
            _with1.EnableAutoDepthStencil = true;
            GraphicCard = new Device(new Direct3D(), 0, DeviceType.Hardware, GraphicPanel.Handle, CreateFlags.HardwareVertexProcessing, GraphicSettings);
            Stream s = new MemoryStream(Bytes);

            try
            {
                Texture MyImage = Texture.FromStream(GraphicCard, s);
                Bitmap a;
                a = new Bitmap(Image.FromStream(Texture.ToStream(MyImage, ImageFileFormat.Bmp)));
                return a;
            }
            catch
            {
                return null;
            }
        }
    }
}
