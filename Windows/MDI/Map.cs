using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Runtime.InteropServices;
using SlimDX;
using SlimDX.Direct3D9;
using SlimDX.Windows;

namespace L2_login
{
	public class Map : Base
    {
		private System.Collections.ArrayList cache_draw = new ArrayList();
		private System.Collections.ArrayList tmp_players = new ArrayList();
		private System.Collections.ArrayList tmp_npcs = new ArrayList();
		private System.Collections.ArrayList tmp_items = new ArrayList();
		private System.Collections.ArrayList tmp_path = new ArrayList();
		private System.Collections.ArrayList tmp_walls = new ArrayList();

        private Device dxDevice;
        private Sprite dxTextSprite;
        private SlimDX.Direct3D9.Font dxFont;
        private Line dxLine;

        private const int wid = 200;
        private const int wid_2 = wid / 2;
        private const int hgt = 16;

        private int last_MAPX = -1000;
        private int last_MAPY = -1000;
        private int last_MAPZ = -10000000;

        private ArrayList maps = new ArrayList();

		//drawing variables
		private int dx, dy, dr, dr2;

		private int xc;
		private int yc;
		private int zc;

		private int xm;
		private int ym;

        private float scale;

		private DrawData ddt;
		private string ddtext;

        private uint my_target;
        private float my_z;
        private float zrange_draw;
		//end of drawing variables

		//my pretty pens
		public static System.Drawing.Pen BlackPen = new System.Drawing.Pen(System.Drawing.Color.Black);
        public static System.Drawing.Pen WhitePen = new System.Drawing.Pen(System.Drawing.Color.White);
        public static System.Drawing.Pen BluePen = new System.Drawing.Pen(System.Drawing.Color.Blue);
        public static System.Drawing.Pen DBluePen = new System.Drawing.Pen(System.Drawing.Color.DarkBlue);
        public static System.Drawing.Pen LBluePen = new System.Drawing.Pen(System.Drawing.Color.LightBlue);
        public static System.Drawing.Pen RedPen = new System.Drawing.Pen(System.Drawing.Color.Red);
        public static System.Drawing.Pen YellowPen = new System.Drawing.Pen(System.Drawing.Color.Yellow);
        public static System.Drawing.Pen GreenPen = new System.Drawing.Pen(System.Drawing.Color.DarkGreen);
        public static System.Drawing.Pen PurplePen = new System.Drawing.Pen(System.Drawing.Color.FromArgb(184, 0, 184));
        public static System.Drawing.Pen LPurplePen = new System.Drawing.Pen(System.Drawing.Color.FromArgb(247, 0, 247));

        public static System.Drawing.SolidBrush BlackBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
        public static System.Drawing.SolidBrush WhiteBrush = new System.Drawing.SolidBrush(System.Drawing.Color.White);
        public static System.Drawing.SolidBrush BlueBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Blue);
        public static System.Drawing.SolidBrush DBlueBrush = new System.Drawing.SolidBrush(System.Drawing.Color.DarkBlue);
        public static System.Drawing.SolidBrush LBlueBrush = new System.Drawing.SolidBrush(System.Drawing.Color.LightBlue);
        public static System.Drawing.SolidBrush RedBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
        public static System.Drawing.SolidBrush YellowBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Yellow);
        public static System.Drawing.SolidBrush GreenBrush = new System.Drawing.SolidBrush(System.Drawing.Color.DarkGreen);
        public static System.Drawing.SolidBrush PurpleBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(184, 0, 184));
        public static System.Drawing.SolidBrush LPurpleBrush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(247, 0, 247));

        public static Color text_color;
        public static Color text_color_shadow;

        public static System.Drawing.Font drawFont = new System.Drawing.Font("Arial", 10);
        private PresentParameters presentParams = new PresentParameters();

        private volatile bool LoadTextures = false;
        private System.DateTime LastTextureLoad = new DateTime(0L);
        private volatile bool resized = false;
        private volatile bool resources_created = false;

        public PictureBox pictureBox_test;

		public Map(System.Windows.Forms.Form pf)
		{
			InitializeComponent();

            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

            this.pictureBox_test.MouseDown += new MouseEventHandler(Form1_MouseDown);

			this.MdiParent = pf;
			this.MdiParent.Resize += new EventHandler(MdiParent_Resize);
			MdiParent_Resize(null,null);

            LoadMiniMap();
            Init_DX();
		}
       
        private void Init_DX()
        {
            presentParams.Windowed = true;
            presentParams.SwapEffect = SwapEffect.Discard;
            presentParams.BackBufferWidth = this.Width;//ClientSize
            presentParams.BackBufferHeight = this.Height;//ClientSize

            if (Globals.Use_Hardware_Acceleration)
            {
                dxDevice = new Device(new Direct3D(), 0, DeviceType.Hardware, this.pictureBox_test.Handle, CreateFlags.HardwareVertexProcessing, presentParams);
            }
            else
            {
                dxDevice = new Device(new Direct3D(), 0, DeviceType.Hardware, this.pictureBox_test.Handle, CreateFlags.SoftwareVertexProcessing, presentParams);
            }

            this.Resize += new EventHandler(Map_Resize);
        }

        private void Init_Resources()
        {
            resources_created = true;

            System.Drawing.Font theFont = new System.Drawing.Font("Arial", 10f, FontStyle.Regular, GraphicsUnit.Pixel);
            dxFont = new SlimDX.Direct3D9.Font(dxDevice, theFont);

            dxTextSprite = new Sprite(dxDevice);

            dxLine = new Line(dxDevice);

            LastTextureLoad = System.DateTime.Now.AddMilliseconds(500);
            LoadTextures = true;
        }

        private void UnloadResources()
        {
            resources_created = false;

            if (dxFont != null)
            {
                dxFont.Dispose();
                dxFont = null;
            }

            if (dxTextSprite != null)
            {
                dxTextSprite.Dispose();
                dxTextSprite = null;
            }

            if (dxLine != null)
            {
                dxLine.Dispose();
                dxLine = null;
            }

            foreach (MapData map in maps)
            {
                try
                {
                    if (map.dxTexture != null)
                    {
                        map.dxTexture.Dispose();
                        map.dxTexture = null;
                    }
                }
                catch
                {
                    //meh
                }
            }
        }

        void Map_Resize(object sender, EventArgs e)
        {
            resized = true;
        }

		protected override void Dispose( bool disposing )
		{
            this.MdiParent.Resize -= new EventHandler(MdiParent_Resize);

			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.pictureBox_test = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_test)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox_test
            // 
            this.pictureBox_test.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox_test.Location = new System.Drawing.Point(0, 0);
            this.pictureBox_test.Name = "pictureBox_test";
            this.pictureBox_test.Size = new System.Drawing.Size(622, 518);
            this.pictureBox_test.TabIndex = 0;
            this.pictureBox_test.TabStop = false;
            // 
            // Map
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.ClientSize = new System.Drawing.Size(622, 518);
            this.Controls.Add(this.pictureBox_test);
            this.Name = "Map";
            this.Text = "Map";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_test)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

        //delegate void Draw_Callback();
        public void Draw()
        {
            this.Invalidate(new System.Drawing.Region(new Rectangle(0,0, this.Width, this.Height)));
        }

        protected override void OnPaintBackground(PaintEventArgs prevent)
		{
			//this should fix the flicker kekeke
			//prevent the drawing of the background
		}

        protected override void OnPrint(PaintEventArgs e)
        {
            //base.OnPrint(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint (e);
            try
            {
                if (this.Width > 100 && this.Height > 100)
                {
                    if (resized)
                    {
                        resized = false;

                        UnloadResources();

                        presentParams.BackBufferWidth = this.Width;//ClientSize
                        presentParams.BackBufferHeight = this.Height;//ClientSize

                        dxDevice.Reset(presentParams);

                        LastTextureLoad = System.DateTime.Now.AddMilliseconds(500);
                        LoadTextures = true;
                    }

                    ClearUnusedMaps();

                    SlimDX.Result res = dxDevice.TestCooperativeLevel();

                    if (res == ResultCode.Success)
                    {
                        if (!resources_created)
                        {
                            Init_Resources();
                        }
                        DrawGame();
                    }
                    if (res == ResultCode.DeviceNotReset)
                    {
                        dxDevice.Reset(presentParams);//Reset the device.
                        Init_Resources();
                    }
                    if (res == ResultCode.DeviceLost)
                    {
                        UnloadResources();
                    }
                }
                else
                {
                    //they have shrunk our window hella small...
                }
            }
            catch
            {

            }
        }

        protected void DrawGame()
        {
            dxDevice.Clear(ClearFlags.Target, Color.White, 1.0f, 0);
            dxDevice.BeginScene();

            DrawGameInner();

            dxDevice.EndScene();
            try
            {
                dxDevice.Present();
                //dxDevice.Present(this.Handle);
            }
            catch //(DeviceLostException)
            {
                UnloadResources();
            }
        }

		protected void DrawGameInner()
		{
            if (LoadTextures)
            {
                try
                {
                    LoadTexturesInternal();
                }
                catch
                {
#if DEBUG
                    Globals.l2net_home.Add_OnlyDebug("failed to load textures internal");
#endif
                }
            }

            my_target = Globals.gamedata.my_char.TargetID;

            my_z = Globals.gamedata.my_char.Z;
            zrange_draw = Math.Abs((float)Util.GetInt32(Globals.l2net_home.textBox_zrange_map.Text));

            //xc = 16384;
            //yc = 16384;
            xc = Util.Float_Int32(Globals.gamedata.my_char.X);// +Util.GetInt32(Globals.l2net_home.textBox_offset_x.Text);
            yc = Util.Float_Int32(Globals.gamedata.my_char.Y);// +Util.GetInt32(Globals.l2net_home.textBox_offset_y.Text);
            zc = Util.Float_Int32(Globals.gamedata.my_char.Z);

            xm = this.Width / 2;//ClientSize
            ym = this.Height / 2;//ClientSize

			scale = MapThread.GetZoom();

			//need to fix these copy things, its the same values being referenced...
			//we need to make copies, not references
			tmp_path.Clear();
			tmp_walls.Clear();
			cache_draw.Clear();

            try
            {
                if (Globals.gamedata.my_pet.ID != 0)
                {
                    ddt = new DrawData();
                    ddt.ID = Globals.gamedata.my_pet.ID;
                    ddt.X = Util.Float_Int32(Globals.gamedata.my_pet.X);
                    ddt.Y = Util.Float_Int32(Globals.gamedata.my_pet.Y);
                    ddt.Radius = Globals.gamedata.my_pet.CollisionRadius;
                    if (Globals.ShowNamesPcs)
                    {
                        ddt.Text = Globals.gamedata.my_pet.Name;
                    }
                    else
                    {
                        ddt.Text = "";
                    }

                    ddt.Color1 = 5;

                    if (Globals.gamedata.my_pet.Karma > 0)
                        ddt.Color2 = 0;
                    else if (Globals.gamedata.my_pet.PvPFlag == 1)
                        ddt.Color2 = 1;
                    else if (Globals.gamedata.my_pet.PvPFlag == 2)
                        ddt.Color2 = 2;
                    else
                        ddt.Color2 = 3;

                    cache_draw.Add(ddt);
                }
                if (Globals.gamedata.my_pet1.ID != 0)
                {
                    ddt = new DrawData();
                    ddt.ID = Globals.gamedata.my_pet1.ID;
                    ddt.X = Util.Float_Int32(Globals.gamedata.my_pet1.X);
                    ddt.Y = Util.Float_Int32(Globals.gamedata.my_pet1.Y);
                    ddt.Radius = Globals.gamedata.my_pet1.CollisionRadius;
                    if (Globals.ShowNamesPcs)
                    {
                        ddt.Text = Globals.gamedata.my_pet1.Name;
                    }
                    else
                    {
                        ddt.Text = "";
                    }

                    ddt.Color1 = 5;

                    if (Globals.gamedata.my_pet1.Karma > 0)
                        ddt.Color2 = 0;
                    else if (Globals.gamedata.my_pet1.PvPFlag == 1)
                        ddt.Color2 = 1;
                    else if (Globals.gamedata.my_pet1.PvPFlag == 2)
                        ddt.Color2 = 2;
                    else
                        ddt.Color2 = 3;

                    cache_draw.Add(ddt);
                }
                if (Globals.gamedata.my_pet2.ID != 0)
                {
                    ddt = new DrawData();
                    ddt.ID = Globals.gamedata.my_pet2.ID;
                    ddt.X = Util.Float_Int32(Globals.gamedata.my_pet2.X);
                    ddt.Y = Util.Float_Int32(Globals.gamedata.my_pet2.Y);
                    ddt.Radius = Globals.gamedata.my_pet2.CollisionRadius;
                    if (Globals.ShowNamesPcs)
                    {
                        ddt.Text = Globals.gamedata.my_pet2.Name;
                    }
                    else
                    {
                        ddt.Text = "";
                    }

                    ddt.Color1 = 5;

                    if (Globals.gamedata.my_pet2.Karma > 0)
                        ddt.Color2 = 0;
                    else if (Globals.gamedata.my_pet2.PvPFlag == 1)
                        ddt.Color2 = 1;
                    else if (Globals.gamedata.my_pet2.PvPFlag == 2)
                        ddt.Color2 = 2;
                    else
                        ddt.Color2 = 3;

                    cache_draw.Add(ddt);
                }
                if (Globals.gamedata.my_pet3.ID != 0)
                {
                    ddt = new DrawData();
                    ddt.ID = Globals.gamedata.my_pet3.ID;
                    ddt.X = Util.Float_Int32(Globals.gamedata.my_pet3.X);
                    ddt.Y = Util.Float_Int32(Globals.gamedata.my_pet3.Y);
                    ddt.Radius = Globals.gamedata.my_pet3.CollisionRadius;
                    if (Globals.ShowNamesPcs)
                    {
                        ddt.Text = Globals.gamedata.my_pet3.Name;
                    }
                    else
                    {
                        ddt.Text = "";
                    }

                    ddt.Color1 = 5;

                    if (Globals.gamedata.my_pet3.Karma > 0)
                        ddt.Color2 = 0;
                    else if (Globals.gamedata.my_pet3.PvPFlag == 1)
                        ddt.Color2 = 1;
                    else if (Globals.gamedata.my_pet3.PvPFlag == 2)
                        ddt.Color2 = 2;
                    else
                        ddt.Color2 = 3;

                    cache_draw.Add(ddt);
                }
            }
            catch
            {
#if DEBUG
                Globals.l2net_home.Add_OnlyDebug("failed to cache pet internal");
#endif
            }

            try
            {
                SortedList tmp_party = new SortedList();
                if (Globals.PartyLock.TryEnterReadLock(Globals.THREAD_WAIT_DX))
                {
                    try
                    {
                        foreach (uint key in Globals.gamedata.PartyMembers.Keys)
                        {
                            tmp_party.Add(key, key);
                        }
                    }
                    finally
                    {
                        Globals.PartyLock.ExitReadLock();
                    }
                }

                if (Globals.PlayerLock.TryEnterReadLock(Globals.THREAD_WAIT_DX))
                {
                    try
                    {
                        foreach (CharInfo player in Globals.gamedata.nearby_chars.Values)
                        {
                            if (Math.Abs(player.Z - my_z) <= zrange_draw)
                            {
                                ddt = new DrawData();
                                ddt.ID = player.ID;
                                ddt.X = Util.Float_Int32(player.X);
                                ddt.Y = Util.Float_Int32(player.Y);
                                ddt.Radius = player.CollisionRadius;
                                if (Globals.ShowNamesPcs)
                                {
                                    ddt.Text = player.Name;
                                }
                                else
                                {
                                    ddt.Text = "";
                                }

                                if (tmp_party.ContainsKey(player.ID))
                                {
                                    ddt.Color1 = 5;
                                }
                                else
                                {
                                    ddt.Color1 = 2;
                                }

                                if ((player.Karma > 0) && (Globals.gamedata.Chron <= Chronicle.CT2_6))
                                    ddt.Color2 = 0;
                                else if ((player.Karma < 0) && (Globals.gamedata.Chron >= Chronicle.CT3_0))
                                    ddt.Color2 = 0;
                                else if (player.PvPFlag == 1)
                                    ddt.Color2 = 1;
                                else if (player.PvPFlag == 2)
                                    ddt.Color2 = 2;
                                else
                                    ddt.Color2 = 3;

                                cache_draw.Add(ddt);
                            }
                        }
                    }
                    finally
                    {
                        Globals.PlayerLock.ExitReadLock();
                    }
                }
            }
            catch
            {
                //busted getting the lock... oh well
#if DEBUG
                Globals.l2net_home.Add_OnlyDebug("failed to lock on players for minimap caching");
#endif
            }

            try
            {
                if(Globals.NPCLock.TryEnterReadLock(Globals.THREAD_WAIT_DX))
                {
                    try
                    {
                        foreach (NPCInfo npc in Globals.gamedata.nearby_npcs.Values)
                        {
                            if (Math.Abs(npc.Z - my_z) <= zrange_draw && npc.isInvisible != 1 ) //if == 1, the mob shouldn't show up.
                            {
                                ddt = new DrawData();
                                ddt.ID = npc.ID;
                                ddt.X = Util.Float_Int32(npc.X);
                                ddt.Y = Util.Float_Int32(npc.Y);
                                ddt.Radius = npc.CollisionRadius;
                                if (Globals.ShowNamesNpcs)
                                {
                                    ddt.Text = Util.GetNPCName(npc.NPCID);
                                }
                                else
                                {
                                    ddt.Text = "";
                                }
                                if (npc.isAttackable == 0)
                                {
                                    ddt.Color1 = 3;
                                }
                                else
                                {
                                    ddt.Color1 = 0;
                                }
                                //#if DEBUG
                                //		if( Globals.gamedata.Paths.IsPointInside( ddt.X, ddt.Y) )
                                //			ddt.Color2 = 0;
                                //		else
                                //#endif
                                ddt.Color2 = 3;
                                cache_draw.Add(ddt);
                            }
                        }
                    }
                    finally
                    {
                        Globals.NPCLock.ExitReadLock();
                    }
                }
            }
            catch
            {
                //busted getting the lock... oh well
#if DEBUG
                Globals.l2net_home.Add_OnlyDebug("failed to lock on npcs for minimap caching");
#endif
            }

            try
            {
                if (Globals.ItemLock.TryEnterReadLock(Globals.THREAD_WAIT_DX))
                {
                    try
                    {
                        foreach (ItemInfo item in Globals.gamedata.nearby_items.Values)
                        {
                            if (Math.Abs(item.Z - my_z) <= zrange_draw)
                            {
                                ddt = new DrawData();
                                ddt.ID = item.ID;
                                ddt.X = Util.Float_Int32(item.X);
                                ddt.Y = Util.Float_Int32(item.Y);
                                ddt.Radius = item.DropRadius;
                                if (Globals.ShowNamesItems)
                                {
                                    ddt.Text = Util.GetItemName(item.ItemID);
                                }
                                else
                                {
                                    ddt.Text = "";
                                }
                                ddt.Color1 = 1;
                                ddt.Color2 = 3;
                                cache_draw.Add(ddt);
                            }
                        }
                    }
                    finally
                    {
                        Globals.ItemLock.ExitReadLock();
                    }
                }
            }
            catch
            {
                //busted getting the lock... oh well
#if DEBUG
                Globals.l2net_home.Add_OnlyDebug("failed to lock on items for minimap caching");
#endif
            }

			try
			{
				if(Globals.l2net_home.checkBox_minimap.Checked)// && miniMap != null)
				{
                    //0,0,0 = 20_18
                    int x_block = (int)((Globals.gamedata.my_char.X + Globals.ModX) / Globals.UNITS);
                    int y_block = (int)((Globals.gamedata.my_char.Y + Globals.ModY) / Globals.UNITS);
                    int z_diff = (int)Math.Abs(Globals.gamedata.my_char.Z - last_MAPZ);

                    if (x_block != last_MAPX || y_block != last_MAPY || z_diff >= Globals.ZRANGE_DIFF)
                    {
                        last_MAPX = x_block;
                        last_MAPY = y_block;
                        last_MAPZ = (int)Globals.gamedata.my_char.Z;

                        switch(Globals.ViewRange)
                        {
                            case 1:
                                LoadMapFile(last_MAPX, last_MAPY, last_MAPZ);
                                LoadMapFile(last_MAPX - 1, last_MAPY, last_MAPZ);
                                LoadMapFile(last_MAPX + 1, last_MAPY, last_MAPZ);
                                LoadMapFile(last_MAPX, last_MAPY - 1, last_MAPZ);
                                LoadMapFile(last_MAPX, last_MAPY + 1, last_MAPZ);
                                break;
                            case 2:
                                LoadMapFile(last_MAPX, last_MAPY, last_MAPZ);
                                LoadMapFile(last_MAPX - 1, last_MAPY, last_MAPZ);
                                LoadMapFile(last_MAPX + 1, last_MAPY, last_MAPZ);

                                LoadMapFile(last_MAPX, last_MAPY - 1, last_MAPZ);
                                LoadMapFile(last_MAPX - 1, last_MAPY - 1, last_MAPZ);
                                LoadMapFile(last_MAPX + 1, last_MAPY - 1, last_MAPZ);

                                LoadMapFile(last_MAPX, last_MAPY + 1, last_MAPZ);
                                LoadMapFile(last_MAPX - 1, last_MAPY + 1, last_MAPZ);
                                LoadMapFile(last_MAPX + 1, last_MAPY + 1, last_MAPZ);
                                break;
                            default:
                                LoadMapFile(last_MAPX, last_MAPY, last_MAPZ);
                                break;
                        }
                    }

                    //DrawMap
                    switch(Globals.ViewRange)
                    {
                        case 1:
                            DrawMap(last_MAPX, last_MAPY, last_MAPZ);
                            DrawMap(last_MAPX - 1, last_MAPY, last_MAPZ);
                            DrawMap(last_MAPX + 1, last_MAPY, last_MAPZ);
                            DrawMap(last_MAPX, last_MAPY - 1, last_MAPZ);
                            DrawMap(last_MAPX, last_MAPY + 1, last_MAPZ);
                            break;
                        case 2:
                            DrawMap(last_MAPX, last_MAPY, last_MAPZ);
                            DrawMap(last_MAPX - 1, last_MAPY, last_MAPZ);
                            DrawMap(last_MAPX + 1, last_MAPY, last_MAPZ);

                            DrawMap(last_MAPX, last_MAPY - 1, last_MAPZ);
                            DrawMap(last_MAPX - 1, last_MAPY - 1, last_MAPZ);
                            DrawMap(last_MAPX + 1, last_MAPY - 1, last_MAPZ);

                            DrawMap(last_MAPX, last_MAPY + 1, last_MAPZ);
                            DrawMap(last_MAPX - 1, last_MAPY + 1, last_MAPZ);
                            DrawMap(last_MAPX + 1, last_MAPY + 1, last_MAPZ);
                            break;
                        default:
                            DrawMap(last_MAPX, last_MAPY, last_MAPZ);
                            break;
                    }
                }
				else
				{
					//draw no map
				}

				//draw self
                if (my_target == Globals.gamedata.my_char.ID)
                {
                    DrawFilledBox(xm - 5, ym - 5, xm + 5, ym + 5, Color.Red);
                }
                else
                {
                    DrawBox(xm - 5, ym - 5, xm + 5, ym + 5, Color.Red);
                }
    		}
			catch
			{
				//failed to draw minimap or self
#if DEBUG
				Globals.l2net_home.Add_OnlyDebug("failed to draw minimap or self");
#endif
			}

            try
			{
			    //only care to draw it if we have 3 points, since that is the only time it becomes meaningful
			    if(Globals.gamedata.Paths.PointList.Count > 0)
			    {
				    //lets scale the points accordingly
				    for(int i = 0; i < Globals.gamedata.Paths.PointList.Count; i++)
				    {
					    Point npt = new Point();
					    npt.X = ((Point)Globals.gamedata.Paths.PointList[i]).X;
                        npt.X = GetScaledX(npt.X);//((npt.X - xc) / scale) + xm;

					    npt.Y = ((Point)Globals.gamedata.Paths.PointList[i]).Y;
                        npt.Y = GetScaledY(npt.Y);//((npt.Y - yc) / scale) + ym;

                        DrawBox((int)npt.X - 2, (int)npt.Y - 2, (int)npt.X + 2, (int)npt.Y + 2, Color.Black);

					    tmp_path.Add(npt);
				    }
                    if (Globals.gamedata.Paths.PointList.Count > 1)
                    {
                        //lets do that last points on the polygon
                        Point p1 = (Point)tmp_path[0];
                        Point p2 = (Point)tmp_path[tmp_path.Count - 1];
                        DrawLine((int)p1.X, (int)p1.Y, (int)p2.X, (int)p2.Y, Color.Black);

                        //now lets draw the lines
                        for (int pi = 1; pi < tmp_path.Count; pi++)
                        {
                            p1 = (Point)tmp_path[pi - 1];
                            p2 = (Point)tmp_path[pi];
                            DrawLine((int)p1.X, (int)p1.Y, (int)p2.X, (int)p2.Y, Color.Black);
                        }
                    }
			    }
			    //***************
			    //
			    //now lets draw our walls
			    for(int i = 0; i < Globals.gamedata.Walls.Count; i++)
			    {
				    Wall tmp_w = new Wall();
				    Point npt1 = new Point();
				    Point npt2 = new Point();
                    npt1.X = ((Wall)Globals.gamedata.Walls[i]).P1.X;
                    npt1.X = GetScaledX(npt1.X);//((npt1.X - xc) / scale) + xm;

                    npt1.Y = ((Wall)Globals.gamedata.Walls[i]).P1.Y;
                    npt1.Y = GetScaledY(npt1.Y);//((npt1.Y - yc) / scale) + ym;

                    npt2.X = ((Wall)Globals.gamedata.Walls[i]).P2.X;
                    npt2.X = GetScaledX(npt2.X);//((npt2.X - xc) / scale) + xm;

                    npt2.Y = ((Wall)Globals.gamedata.Walls[i]).P2.Y;
                    npt2.Y = GetScaledY(npt2.Y);//((npt2.Y - yc) / scale) + ym;

                    tmp_w.P1 = npt1; tmp_w.P2 = npt2;
				    tmp_walls.Add(tmp_w);
			    }
			    for(int pi = 0; pi < tmp_walls.Count; pi++)
			    {
				    Point p1 = ((Wall)tmp_walls[pi]).P1;
				    Point p2 = ((Wall)tmp_walls[pi]).P2;
                    DrawLine((int)p1.X, (int)p1.Y, (int)p2.X, (int)p2.Y, Color.MediumVioletRed);
			    }

                //drawing A* grid 
#if DEBUG
                if (Globals.debugPath != null)
                {
                    for (int i = 0; i < Globals.debugPath.Count; i++)
                    {
                        int drawX, drawY, drawX2, drawY2;
                        System.Drawing.Color tempPen;

                        tempPen = Color.White;

                        drawX = GetScaledX(((AstarNode)Globals.debugPath[i]).x);//(((((AstarNode)Globals.debugPath[i]).x - xc)) / scale) + xm;
                        drawY = GetScaledY(((AstarNode)Globals.debugPath[i]).y);//(((((AstarNode)Globals.debugPath[i]).y - yc)) / scale) + ym;
                        drawX2 = GetScaledX(((AstarNode)Globals.debugPath[i]).x2);//(((((AstarNode)Globals.debugPath[i]).x2 - xc)) / scale) + xm;
                        drawY2 = GetScaledY(((AstarNode)Globals.debugPath[i]).y2);//(((((AstarNode)Globals.debugPath[i]).y2 - yc)) / scale) + ym;

                        DrawLine(drawX, drawY, drawX, drawY2, tempPen);
                        DrawLine(drawX, drawY2, drawX2, drawY2, tempPen);
                        DrawLine(drawX2, drawY2, drawX2, drawY, tempPen);
                        DrawLine(drawX2, drawY, drawX, drawY, tempPen);
                    }
                }
                if (Globals.debugNode != null)
                {
                    int drawX, drawY, drawX2, drawY2;
                    System.Drawing.Color tempPen;

                    tempPen = Color.Gold;

                    drawX = GetScaledX(Globals.debugNode.x);//((Globals.debugNode.x - xc) / scale) + xm;
                    drawY = GetScaledY(Globals.debugNode.y);//((Globals.debugNode.y - yc) / scale) + ym;
                    drawX2 = GetScaledX(Globals.debugNode.x2);//((Globals.debugNode.x2 - xc) / scale) + xm;
                    drawY2 = GetScaledY(Globals.debugNode.y2);//((Globals.debugNode.y2 - yc) / scale) + ym;

                    DrawLine(drawX, drawY, drawX, drawY2, tempPen);
                    DrawLine(drawX, drawY2, drawX2, drawY2, tempPen);
                    DrawLine(drawX2, drawY2, drawX2, drawY, tempPen);                 
                    DrawLine(drawX2, drawY, drawX, drawY, tempPen);
                }
                if (Globals.debugNode2 != null)
                {
                    int drawX, drawY, drawX2, drawY2;
                    System.Drawing.Color tempPen;

                    tempPen = Color.Aquamarine;

                    drawX = GetScaledX(Globals.debugNode2.x);//((Globals.debugNode2.x - xc) / scale) + xm;
                    drawY = GetScaledY(Globals.debugNode2.y);//((Globals.debugNode2.y - yc) / scale) + ym;
                    drawX2 = GetScaledX(Globals.debugNode2.x);//((Globals.debugNode2.x2 - xc) / scale) + xm;
                    drawY2 = GetScaledY(Globals.debugNode2.y);//((Globals.debugNode2.y2 - yc) / scale) + ym;

                    DrawLine(drawX, drawY, drawX, drawY2, tempPen);
                    DrawLine(drawX, drawY2, drawX2, drawY2, tempPen);
                    DrawLine(drawX2, drawY2, drawX2, drawY, tempPen);
                    DrawLine(drawX2, drawY, drawX, drawY, tempPen);
                }
                if (Globals.debugNode3 != null)
                {
                    int drawX, drawY, drawX2, drawY2;
                    System.Drawing.Color tempPen;

                    tempPen = Color.SpringGreen;

                    drawX = GetScaledX(Globals.debugNode3.x);//((Globals.debugNode3.x - xc) / scale) + xm;
                    drawY = GetScaledY(Globals.debugNode3.y);//((Globals.debugNode3.y - yc) / scale) + ym;
                    drawX2 = GetScaledX(Globals.debugNode3.x);//((Globals.debugNode3.x2 - xc) / scale) + xm;
                    drawY2 = GetScaledY(Globals.debugNode3.y);//((Globals.debugNode3.y2 - yc) / scale) + ym;

                    DrawLine(drawX, drawY, drawX, drawY2, tempPen);
                    DrawLine(drawX, drawY2, drawX2, drawY2, tempPen);
                    DrawLine(drawX2, drawY2, drawX2, drawY, tempPen);
                    DrawLine(drawX2, drawY, drawX, drawY, tempPen);
                }

#endif
			}
#if DEBUG
            catch (Exception e)
			{
				//failed to draw path or walls
				Globals.l2net_home.Add_OnlyDebug("failed to draw paths of walls : " + e.Message);
            }
#else
            catch
			{
				//failed to draw path or walls
				Globals.l2net_home.Add_OnlyDebug("failed to draw paths of walls");
            }
#endif


            try
			{
                if (Globals.White_Names)
                {
                    text_color = Color.White;
                    //text_brush = WhiteBrush;
                    text_color_shadow = Color.Black;
                    //text_brush_shadow = BlackBrush;
                }
                else
                {
                    text_color = Color.Black;
                    //text_brush = BlackBrush;
                    text_color_shadow = Color.Gray;
                    //text_brush_shadow = WhiteBrush;
                }

				//draw all the text objects
				for(int i = 0; i < cache_draw.Count; i++)
				{
                    dx = GetScaledX(((DrawData)cache_draw[i]).X);//(int)((((DrawData)cache_draw[i]).X - xc) / scale) + xm;
                    dy = GetScaledY(((DrawData)cache_draw[i]).Y);//(int)((((DrawData)cache_draw[i]).Y - yc) / scale) + ym;
                    dr2 = (int)(((DrawData)cache_draw[i]).Radius / scale);
                    dr = (int)(((DrawData)cache_draw[i]).Radius / scale / 2);
                    if (dr2 < Globals.MIN_RADIUS)
                    {
                        dr2 = Globals.MIN_RADIUS;
                        dr = dr2 / 2;
                    }

					if( ((DrawData)cache_draw[i]).ID == my_target)
					{
						switch(((DrawData)cache_draw[i]).Color1)
						{
							case 0://black
                                DrawFilledBox(dx - dr, dy - dr, dx + dr, dy + dr, Color.Black);
								break;
							case 1://yellow
                                DrawFilledBox(dx - dr, dy - dr, dx + dr, dy + dr, Color.Yellow);
								break;
							case 2://blue
                                DrawFilledBox(dx - dr, dy - dr, dx + dr, dy + dr, Color.Blue);
								break;
                            case 3://darkgreen
                                DrawFilledBox(dx - dr, dy - dr, dx + dr, dy + dr, Color.SkyBlue);
								break;
                            case 4://red
                                DrawFilledBox(dx - dr, dy - dr, dx + dr, dy + dr, Color.Red);
                                break;
                            case 5://orangered
                                DrawFilledBox(dx - dr, dy - dr, dx + dr, dy + dr, Color.OrangeRed);
                                break;
                        }
					}
					else
					{
						switch(((DrawData)cache_draw[i]).Color1)
						{
							case 0://black
                                DrawBox(dx - dr, dy - dr, dx + dr, dy + dr, Color.Black);
								break;
							case 1://yellow
                                DrawBox(dx - dr, dy - dr, dx + dr, dy + dr, Color.Yellow);
								break;
							case 2://blue
                                DrawBox(dx - dr, dy - dr, dx + dr, dy + dr, Color.Blue);
								break;
                            case 3://darkgreen
                                DrawBox(dx - dr, dy - dr, dx + dr, dy + dr, Color.SkyBlue);
                                break;
                            case 4://red
                                DrawBox(dx - dr, dy - dr, dx + dr, dy + dr, Color.Red);
                                break;
                            case 5://orangered
                                DrawBox(dx - dr, dy - dr, dx + dr, dy + dr, Color.OrangeRed);
                                break;
                        }
                    } 

                    if (((DrawData)cache_draw[i]).Text.Length > 0)
                    {
                        ddtext = ((DrawData)cache_draw[i]).Text;

                        switch (((DrawData)cache_draw[i]).Color2)
                        {
                            case 0://red
                                DrawText(ddtext, dx - wid_2, dy - hgt - 10, wid, hgt, Color.Red);
                                break;
                            case 1://purple
                                DrawText(ddtext, dx - wid_2, dy - hgt - 10, wid, hgt, Color.FromArgb(184, 0, 184));
                                break;
                            case 2://light purple
                                DrawText(ddtext, dx - wid_2, dy - hgt - 10, wid, hgt, Color.FromArgb(247, 0, 247));
                                break;
                            case 3://black
                                DrawText(ddtext, dx - wid_2 - 1, dy - hgt - 10 + 1, wid, hgt, text_color_shadow);
                                DrawText(ddtext, dx - wid_2, dy - hgt - 10, wid, hgt, text_color);
                                break;
                        }
                    }
				}
			}
			catch
			{
				//failed to draw items
				//dont really care to output text like this
#if DEBUG
				Globals.l2net_home.Add_OnlyDebug("failed to draw from cached players/npcs/items");
#endif
			}
        }

        private void DrawMap(int x_block, int y_block, int z)
        {
            MapData map = GetMapFile(x_block, y_block, z);

            if (map == null)
            {
                return;
            }
            if (map.dxTexture == null)
            {
                return;
            }

            int lx = GetScaledX(map.UpperX);
            int ly = GetScaledY(map.UpperY);
            int mx = GetScaledX(map.LowerX);
            int my = GetScaledY(map.LowerY);

            int _lx = lx;
            int _ly = ly;
            int _mx = mx;
            int _my = my;

            if ((mx < 0) || (my < 0) || (lx > Width) || (ly > Height))
            {
                //culling
                return;
            }

            float Tu1 = 0, Tv1 = 0, Tu2 = 1, Tv2 = 1;

            //scale the texture coords
            float texture_width = Math.Abs(lx) + Math.Abs(mx);
            float texture_height = Math.Abs(ly) + Math.Abs(my);

            if (lx < 0)
            {
                Tu1 = ((float)(0 - _lx)) / ((float)(_mx - _lx));
                lx = 0;
            }

            if (ly < 0)
            {
                Tv1 = ((float)(0 - _ly)) / ((float)(_my - _ly));
                ly = 0;
            }

            if (mx > Width)
            {
                Tu2 = ((float)(Width - _lx)) / ((float)(_mx - _lx));
                mx = Width;
            }

            if (my > Height)
            {
                Tv2 = ((float)(Height - _ly)) / ((float)(_my - _ly));
                my = Height;
            }

            VertexBuffer Vertices = new VertexBuffer(dxDevice, 6 * Marshal.SizeOf(typeof(Vertex)), Usage.WriteOnly, VertexFormat.None, Pool.Managed);

            DataStream stream = Vertices.Lock(0, 0, LockFlags.None);
            Vertex[] vertexData = new Vertex[6];
            vertexData[0].Position = new Vector4(lx, ly, 0f, 1f);
            vertexData[0].Color = Color.White.ToArgb();
            vertexData[0].Tu = Tu1;
            vertexData[0].Tv = Tv1;

            vertexData[1].Position = new Vector4(mx, my, 0f, 1f);
            vertexData[1].Color = Color.White.ToArgb();
            vertexData[1].Tu = Tu2;
            vertexData[1].Tv = Tv2;

            vertexData[2].Position = new Vector4(lx, my, 0f, 1f);
            vertexData[2].Color = Color.White.ToArgb();
            vertexData[2].Tu = Tu1;
            vertexData[2].Tv = Tv2;

            vertexData[3].Position = new Vector4(mx, my, 0f, 1f);
            vertexData[3].Color = Color.White.ToArgb();
            vertexData[3].Tu = Tu2;
            vertexData[3].Tv = Tv2;

            vertexData[4].Position = new Vector4(lx, ly, 0f, 1f);
            vertexData[4].Color = Color.White.ToArgb();
            vertexData[4].Tu = Tu1;
            vertexData[4].Tv = Tv1;

            vertexData[5].Position = new Vector4(mx, ly, 0f, 1f);
            vertexData[5].Color = Color.White.ToArgb();
            vertexData[5].Tu = Tu2;
            vertexData[5].Tv = Tv1;
            stream.WriteRange(vertexData);
            Vertices.Unlock();

            switch (Globals.Texture_Mode)
            {
                case 1:
                    dxDevice.SetSamplerState(0, SamplerState.MagFilter, TextureFilter.Linear);
                    break;
                case 2:
                    dxDevice.SetSamplerState(0, SamplerState.MagFilter, TextureFilter.GaussianQuad);
                    break;
                default:
                    dxDevice.SetSamplerState(0, SamplerState.MagFilter, TextureFilter.Point);//TextureFilter.None
                    break;
            }

            dxDevice.SetTexture(0, map.dxTexture);
            dxDevice.SetStreamSource(0, Vertices, 0, Marshal.SizeOf(typeof(Vertex)));
            dxDevice.VertexFormat = VertexFormat.PositionRhw | VertexFormat.Diffuse | VertexFormat.Texture1;

            dxDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, 2);
            dxDevice.SetTexture(0, null);

            stream.Close();
            Vertices.Dispose();
        }

        private void DrawFilledBox(int x1, int y1, int x2, int y2, Color col)
        {
            VertexBuffer Vertices = new VertexBuffer(dxDevice, 6 * Marshal.SizeOf(typeof(Vertex)), Usage.WriteOnly, VertexFormat.None, Pool.Managed);

            DataStream stream = Vertices.Lock(0, 0, LockFlags.None);
            Vertex[] vertexData = new Vertex[6];
            vertexData[0].Position = new Vector4(x1, y1, 0f, 1f);
            vertexData[0].Color = col.ToArgb();
            vertexData[1].Position = new Vector4(x2, y1, 0f, 1f);
            vertexData[1].Color = col.ToArgb();
            vertexData[2].Position = new Vector4(x2, y2, 0f, 1f);
            vertexData[2].Color = col.ToArgb();

            vertexData[3].Position = new Vector4(x2, y2, 0f, 1f);
            vertexData[3].Color = col.ToArgb();
            vertexData[4].Position = new Vector4(x1, y2, 0f, 1f);
            vertexData[4].Color = col.ToArgb();
            vertexData[5].Position = new Vector4(x1, y1, 0f, 1f);
            vertexData[5].Color = col.ToArgb();
            stream.WriteRange(vertexData);
            Vertices.Unlock();

            dxDevice.SetStreamSource(0, Vertices, 0, Marshal.SizeOf(typeof(Vertex)));
            dxDevice.VertexFormat = VertexFormat.PositionRhw | VertexFormat.Diffuse | VertexFormat.Texture1;

            dxDevice.DrawPrimitives(PrimitiveType.TriangleList, 0, 2);

            stream.Close();
            Vertices.Dispose();
        }

        private void DrawBox(int x1, int y1, int x2, int y2, Color col)
        {
            dxLine.Begin();

            Vector2[] vLine = new Vector2[2];
            vLine[0] = new Vector2(x1, y1);
            vLine[1] = new Vector2(x2, y1);
            dxLine.Draw(vLine, col);

            vLine = new Vector2[2];
            vLine[0] = new Vector2(x2, y1);
            vLine[1] = new Vector2(x2, y2);
            dxLine.Draw(vLine, col);

            vLine = new Vector2[2];
            vLine[0] = new Vector2(x2, y2);
            vLine[1] = new Vector2(x1, y2);
            dxLine.Draw(vLine, col);

            vLine = new Vector2[2];
            vLine[0] = new Vector2(x1, y2);
            vLine[1] = new Vector2(x1, y1);
            dxLine.Draw(vLine, col);

            dxLine.End();
        }

        private void DrawLine(int x1, int y1, int x2, int y2, Color col)
        {
            dxLine.Begin();

            Vector2[] vLine = new Vector2[2];
            vLine[0] = new Vector2(x1, y1);
            vLine[1] = new Vector2(x2, y2);
            dxLine.Draw(vLine, col);

            dxLine.End();
        }

        private void DrawText(string text, int x, int y, int w, int h, Color col)
        {
            dxTextSprite.Begin(SpriteFlags.AlphaBlend);
            Rectangle tr = new Rectangle(x, y, w, h);
            dxFont.DrawString(dxTextSprite, text, tr, DrawTextFormat.Center, col);
            dxTextSprite.End();
        }

        private int GetScaledX(float x)
        {
            return (int)((x - xc) / scale) + xm;
        }
        private int GetScaledX(double x)
        {
            return (int)((x - xc) / scale) + xm;
        }
        private int GetScaledY(float y)
        {
            return (int)((y - yc) / scale) + ym;
        }
        private int GetScaledY(double y)
        {
            return (int)((y - yc) / scale) + ym;
        }

		private bool InDrawSpace(MouseEventArgs e)
		{
            if (e.X > this.Left &&
                e.X < this.Width + this.Left &&
                e.Y > this.Top &&
                e.Y < this.Top + this.Height)
			{
				return true;
			}
			return false;
		}

		private void Form1_MouseDown(object sender, MouseEventArgs e)
		{
			//clicky on the mapy
			if(!Globals.gamedata.logged_in)
				return;
			if(!Globals.gamedata.running)
				return;
			//if(!L2NET.drawing_game)
			//	return;
			if(!InDrawSpace(e))
				return;

			if(e.Button == MouseButtons.XButton1)
			{
				if(Globals.l2net_home.trackBar_map_zoom.Value+1 <= Globals.l2net_home.trackBar_map_zoom.Maximum)
					Globals.l2net_home.trackBar_map_zoom.Value++;
			}
			if(e.Button == MouseButtons.XButton2)
			{
				if(Globals.l2net_home.trackBar_map_zoom.Value-1 >= Globals.l2net_home.trackBar_map_zoom.Minimum)
					Globals.l2net_home.trackBar_map_zoom.Value--;
			}
			if(e.Button == MouseButtons.Left)
			{
                int xc = Util.Float_Int32(Globals.gamedata.my_char.X);
                int yc = Util.Float_Int32(Globals.gamedata.my_char.Y);
                int zc = Util.Float_Int32(Globals.gamedata.my_char.Z);

                int Width = this.pictureBox_test.Width;
                int Height = this.pictureBox_test.Height;

				int xm = Width / 2;
				int ym = Height / 2;

                int mx = e.X - this.Left;
                int my = e.Y - this.Top;

                float mouse_scale = MapThread.GetZoom();

				int dx = (int)((mx - xm) * mouse_scale) + xc;
				int dy = (int)((my - ym) * mouse_scale) + yc;

                float radius;
                int minR = (int)(Globals.MIN_RADIUS * mouse_scale);

                //lets check if they clicked on something

                //Oddi: Check if they clicked on bounding polygon point
                if (Globals.gamedata.Paths.PointList.Count > 0)
                {
                    try
                    {
                        for (int i = 0; i < Globals.gamedata.Paths.PointList.Count; i++)
                        {
                            Point npt = new Point();
                            npt.X = ((Point)Globals.gamedata.Paths.PointList[i]).X;
                            npt.Y = ((Point)Globals.gamedata.Paths.PointList[i]).Y;

                            if (Math.Abs(npt.X - dx) < minR && Math.Abs(npt.Y - dy) < minR)
                            {
                                if (!Globals.gamedata.AddPolygon)
                                {
                                    //Globals.l2net_home.Add_Text("Clicked a bounding polygon point!", Globals.Yellow); //This works
                                    //DrawFilledBox((int)npt.X - 2, (int)npt.Y - 2, (int)npt.X + 2, (int)npt.Y + 2, Color.Black); //Select point
                                    Globals.l2net_home.Add_Text("Bounding polygon point selected, please select a new position", Globals.Green, TextType.BOT);

                                    Globals.gamedata.PointClicked = true;
                                    Globals.gamedata.New_Point_i = i;
                                    return;
                                }
                                else
                                {
                                    Globals.gamedata.Paths.PointList.RemoveAt(i);
                                    return;
                                }
                            }
                        }
                    }
                    catch
                    {

                    }

                }

                if (Globals.gamedata.PointClicked)
                {
                    try
                    {
                        Point pt = new Point();
                        pt.X = dx;
                        pt.Y = dy;
                        Globals.gamedata.Paths.PointList[Globals.gamedata.New_Point_i] = pt;

                        Globals.gamedata.PointClicked = false;
                        //Globals.l2net_home.Add_Text("Point moved to: " + pt.X.ToString() + " " + pt.Y.ToString(), Globals.Yellow);
                        return;
                    }
                    catch
                    {

                    }

                }

                if (Globals.gamedata.AddPolygon)
                {
                    try
                    {
                        Point pt = new Point();
                        pt.X = dx;
                        pt.Y = dy;
                        Globals.gamedata.Paths.PointList.Add(pt);
                        return;
                    }
                    catch
                    {

                    }
                }


                if (Globals.ItemLock.TryEnterReadLock(Globals.THREAD_WAIT_DX))
                {
                    try
                    {
                        foreach (ItemInfo item in Globals.gamedata.nearby_items.Values)
                        {
                            radius = item.DropRadius;
                            if (radius < minR)
                            {
                                radius = minR;
                            }

                            if (Math.Abs(item.X - dx) < radius &&
                                Math.Abs(item.Y - dy) < radius)
                            {
                                ServerPackets.ClickItem(item.ID, Util.Float_Int32(item.X), Util.Float_Int32(item.Y), Util.Float_Int32(item.Z), Globals.gamedata.Shift);
                                return;
                            }
                        }
                    }
                    finally
                    {
                        Globals.ItemLock.ExitReadLock();
                    }
                }

                if (Globals.NPCLock.TryEnterReadLock(Globals.THREAD_WAIT_DX))
                {
                    try
                    {
                        foreach (NPCInfo npc in Globals.gamedata.nearby_npcs.Values)
                        {
                            if (npc.isInvisible != 1)//gotta make sure the npc is visible before we try to target it
                            {
                                radius = npc.CollisionRadius;
                                if (radius < minR)
                                {
                                    radius = minR;
                                }

                                if (Math.Abs(npc.X - dx) < radius &&
                                    Math.Abs(npc.Y - dy) < radius)
                                {
                                    ServerPackets.ClickChar(npc.ID, Util.Float_Int32(npc.X), Util.Float_Int32(npc.Y), Util.Float_Int32(npc.Z), Globals.gamedata.Control, Globals.gamedata.Shift);
                                    return;
                                }
                            }
                        }
                    }
                    finally
                    {
                        Globals.NPCLock.ExitReadLock();
                    }
                }

                if (Globals.PlayerLock.TryEnterReadLock(Globals.THREAD_WAIT_DX))
                {
                    try
                    {
                        foreach (CharInfo player in Globals.gamedata.nearby_chars.Values)
                        {
                            radius = player.CollisionRadius;
                            if (radius < minR)
                            {
                                radius = minR;
                            }

                            if (Math.Abs(player.X - dx) < radius &&
                                Math.Abs(player.Y - dy) < radius)
                            {
                                ServerPackets.ClickChar(player.ID, Util.Float_Int32(player.X), Util.Float_Int32(player.Y), Util.Float_Int32(player.Z), Globals.gamedata.Control, Globals.gamedata.Shift);
                                return;
                            }
                        }
                    }
                    finally
                    {
                        Globals.PlayerLock.ExitReadLock();
                    }
                }

                if (Globals.gamedata.my_pet.ID != 0)
                {
                    radius = Globals.gamedata.my_pet.CollisionRadius;
                    if (radius < minR)
                    {
                        radius = minR;
                    }

                    if (Math.Abs(Globals.gamedata.my_pet.X - dx) < radius &&
                        Math.Abs(Globals.gamedata.my_pet.Y - dy) < radius)
                    {
                        ServerPackets.ClickChar(Globals.gamedata.my_pet.ID, Util.Float_Int32(Globals.gamedata.my_pet.X), Util.Float_Int32(Globals.gamedata.my_pet.Y), Util.Float_Int32(Globals.gamedata.my_pet.Z), Globals.gamedata.Control, Globals.gamedata.Shift);
                        return;
                    }
                }
                if (Globals.gamedata.my_pet1.ID != 0)
                {
                    radius = Globals.gamedata.my_pet1.CollisionRadius;
                    if (radius < minR)
                    {
                        radius = minR;
                    }

                    if (Math.Abs(Globals.gamedata.my_pet1.X - dx) < radius &&
                        Math.Abs(Globals.gamedata.my_pet1.Y - dy) < radius)
                    {
                        ServerPackets.ClickChar(Globals.gamedata.my_pet1.ID, Util.Float_Int32(Globals.gamedata.my_pet1.X), Util.Float_Int32(Globals.gamedata.my_pet1.Y), Util.Float_Int32(Globals.gamedata.my_pet1.Z), Globals.gamedata.Control, Globals.gamedata.Shift);
                        return;
                    }
                }
                if (Globals.gamedata.my_pet2.ID != 0)
                {
                    radius = Globals.gamedata.my_pet2.CollisionRadius;
                    if (radius < minR)
                    {
                        radius = minR;
                    }

                    if (Math.Abs(Globals.gamedata.my_pet2.X - dx) < radius &&
                        Math.Abs(Globals.gamedata.my_pet2.Y - dy) < radius)
                    {
                        ServerPackets.ClickChar(Globals.gamedata.my_pet2.ID, Util.Float_Int32(Globals.gamedata.my_pet2.X), Util.Float_Int32(Globals.gamedata.my_pet2.Y), Util.Float_Int32(Globals.gamedata.my_pet2.Z), Globals.gamedata.Control, Globals.gamedata.Shift);
                        return;
                    }
                }
                if (Globals.gamedata.my_pet3.ID != 0)
                {
                    radius = Globals.gamedata.my_pet3.CollisionRadius;
                    if (radius < minR)
                    {
                        radius = minR;
                    }

                    if (Math.Abs(Globals.gamedata.my_pet3.X - dx) < radius &&
                        Math.Abs(Globals.gamedata.my_pet3.Y - dy) < radius)
                    {
                        ServerPackets.ClickChar(Globals.gamedata.my_pet3.ID, Util.Float_Int32(Globals.gamedata.my_pet3.X), Util.Float_Int32(Globals.gamedata.my_pet3.Y), Util.Float_Int32(Globals.gamedata.my_pet3.Z), Globals.gamedata.Control, Globals.gamedata.Shift);
                        return;
                    }
                }

                //else
                if (!Globals.gamedata.Shift)
                {
                    ServerPackets.MoveToPacket(dx, dy, zc);
                }
			}
		}


        private void LoadMiniMap()
        {
            try
            {
                string loaded;

                //load the map datafiles...
                System.IO.StreamReader filein = new System.IO.StreamReader(Globals.PATH + "\\data\\maps.txt");

                while ((loaded = filein.ReadLine()) != null)
                {
                    MapData mapdata = new MapData();

                    mapdata.Parse(loaded);

                    maps.Add(mapdata);
                }
            }
            catch
            {
                Globals.l2net_home.Add_PopUpError("failed to load data\\maps.txt");
            }
        }

        private void LoadMapFile(int x_block, int y_block, int z)
        {
            MapData map = GetMapFile(x_block, y_block, z);

            if (map != null)
            {
                if (map.Image == null)
                {
                    if (System.IO.File.Exists(Globals.PATH + "\\Maps\\" + map.FileName))
                    {
                        map.Image = new System.IO.MemoryStream();

                        if (map.Encrypted)
                        {
                            byte[] data = AES.Decrypt(Globals.PATH + "\\Maps\\" + map.FileName, Globals.Map_Key, Globals.Map_Salt);

                            (new System.Drawing.Bitmap(new System.IO.MemoryStream(data))).Save(map.Image, System.Drawing.Imaging.ImageFormat.Bmp);
                        }
                        else
                        {
                            (new System.Drawing.Bitmap(Globals.PATH + "\\Maps\\" + map.FileName)).Save(map.Image, System.Drawing.Imaging.ImageFormat.Bmp);
                        }

                        LoadTextures = true;
                    }
                }
            }
        }

        private void ClearUnusedMaps()
        {
            foreach (MapData map in maps)
            {
                map.ReleaseResources();
            }
        }

        private MapData GetMapFile(int x_block, int y_block, int z)
        {
            MapData return_map = null;

            foreach (MapData map in maps)
            {
                if (map.X == x_block && map.Y == y_block && map.Z_Min <= z && map.Z_Max >= z)
                {
                    return_map = map;
                }
            }

            return return_map;
        }

        private void LoadTexturesInternal()
        {
            if ((System.DateTime.Now - LastTextureLoad).Ticks < Globals.SLEEP_TEXTURE)
            {
                return;
            }

            foreach (MapData map in maps)
            {
                if (map.Image != null && map.dxTexture == null)
                {
                    map.Image.Position = 0;

                    map.dxTexture = Texture.FromStream(dxDevice, map.Image, Usage.None, Pool.Managed);
                    //map.dxTexture = Texture.FromStream(dxDevice, stream, map.Image.Width, map.Image.Height, 0, Usage.Dynamic, Format.R8G8B8, Pool.Managed, Filter.None, Filter.None, 0);*/
                    LastTextureLoad = System.DateTime.Now;
                    return;
                }
            }

            LoadTextures = false;
        }
	}//end of class
}
