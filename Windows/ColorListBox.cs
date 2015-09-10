using System;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace L2_login
{
    class ColorListBox : ListBox
    {
        private ArrayList LineColors;

        private System.Threading.ReaderWriterLockSlim EditLock = new System.Threading.ReaderWriterLockSlim();
        private SmartTimer timer_resize;

        public ColorListBox()
        {
            timer_resize = new SmartTimer();
            timer_resize.Interval = Globals.MESSAGE_RESIZE_TIMER;
            timer_resize.OnTimerTick += timer_resize_Tick;

            this.DrawMode = DrawMode.OwnerDrawVariable;
            this.HorizontalScrollbar = true;
            //this.HorizontalExtent = this.Width - 32;
            this.Font = new Font("Arial", 9);
            
            LineColors = new ArrayList();

            this.MeasureItem += new MeasureItemEventHandler(ColorListBox_MeasureItem);
            this.DrawItem += new DrawItemEventHandler(ColorListBox_DrawItem);

            this.SelectedIndexChanged += new EventHandler(ColorListBox_SelectedIndexChanged);

            this.Resize += new EventHandler(ColorListBox_Resize);
        }

        void ColorListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (SelectedItems.Count == 0)
                {
                    return;
                }

                string sel = "";

                foreach (string txt in this.SelectedItems)
                {
                    sel = sel + txt;// +Environment.NewLine;
                }

                sel = sel.Trim();

                if (sel.Length > 0)
                {
                    Clipboard.SetText(sel);
                }
            }
            catch
            {
                //oh well
            }
        }

        void ColorListBox_Resize(object sender, EventArgs e)
        {
            //need to set up a timer to handle this... so we don't do spastic resizing
            timer_resize.Start();

            //this.HorizontalExtent = this.Width - 32;
            //this.RefreshItems();
        }

        private void timer_resize_Tick()
        {
            timer_resize.Stop();

            //this.BeginUpdate();
            //this.RefreshItems();
            //this.EndUpdate();

            NewRefresh();
        }

        delegate void NewRefresh_Callback();
        private void NewRefresh()
        {
            if (this.InvokeRequired)
            {
                NewRefresh_Callback d = new NewRefresh_Callback(NewRefresh);
                this.Invoke(d);
                return;
            }

            this.BeginUpdate();
            this.RefreshItems();
            this.EndUpdate();
        }
        /*private string GetText(int i)
        {
            try
            {
                return this.Items[i].ToString();
            }
            catch
            {
                return "";
            }
        }*/

        void ColorListBox_MeasureItem(object sender, MeasureItemEventArgs e)
        {
            if (e.Index < Items.Count && e.Index >= 0)
            {
                //need to use this to add support for items that take up multiple lines
                string text = ((ListBox)sender).Items[e.Index].ToString();

                SizeF size = e.Graphics.MeasureString(text, this.Font);

                //calculate how many lines tall this chat should be...
                e.ItemHeight = 16 * ((int)(size.Width / this.Width) + 1);

                if (e.ItemHeight >= 150)
                {
                    //we are too tall... need to go wider
                    this.HorizontalExtent = this.Width * 2;
                    e.ItemHeight = 16 * ((int)(size.Width / this.HorizontalExtent) + 1);
                }
            }
        }

        void ColorListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (EditLock.TryEnterWriteLock(Globals.THREAD_WAIT_GUI))
            {
                try
                {
                    if (LineColors.Count > 0 && e.Index < Items.Count && e.Index >= 0)
                    {
                        e.DrawBackground();

                        string line = ((ListBox)sender).Items[e.Index].ToString();
                        Brush color = (System.Drawing.Brush)LineColors[e.Index];

                        /*SizeF size = e.Graphics.MeasureString(line, e.Font);

                        if (16 * ((int)(size.Width / this.Width) + 1) > 61)
                        {
                            //we are too tall... need to go wider
                            this.HorizontalExtent = this.Width * 2;
                        }*/

                        //these commented line will auto resize the scroll bar if the text is too long. not word wrap... auto scroll bar
                        /*if (this.HorizontalExtent < (int)(size.Width + 5))
                        {
                            this.HorizontalExtent = (int)(size.Width + 5);
                        }*/

                        e.Graphics.DrawString(line, e.Font, color, e.Bounds, StringFormat.GenericDefault);

                        e.DrawFocusRectangle();
                    }
                }
                catch
                {
                    //well shit...
                    Globals.l2net_home.Add_Error("failed to draw item from colorlistbox");
                }
                finally
                {
                    EditLock.ExitWriteLock();
                }
            }
        }

        public void AddItem(string text, Brush color)
        {
            EditLock.EnterWriteLock();
            try
            {
                this.Items.Add(text);
                LineColors.Add(color);

                if (Items.Count > Globals.MAX_LINES)
                {
                    Items.RemoveAt(0);
                    LineColors.RemoveAt(0);
                }

                //need to resolve a way to scroll to the bottom without selecting the text and we can go back to this order
                /*
                this.SetSelected(this.Items.Count - 1, true);
                this.SetSelected(this.Items.Count - 1, false);*/
                this.TopIndex = this.Items.Count - 1;
            }
            /*catch
            {
                //well shit...
                Globals.l2net_home.Add_Error("failed to add \"" + text + "\" to end of colorlistbox");
            }*/
            finally
            {
                EditLock.ExitWriteLock();
            }
        }

        public void AddItemStart(string text, Brush color)
        {
            EditLock.EnterWriteLock();
            try
            {
                if (Items.Count >= Globals.MAX_LINES)
                {
                    Items.RemoveAt(Items.Count - 1);
                    LineColors.RemoveAt(LineColors.Count - 1);
                }

                this.Items.Insert(0, text);
                LineColors.Insert(0, color);
            }
            catch
            {
                //well shit...
                Globals.l2net_home.Add_Error("failed to add \"" + text + "\" to start of colorlistbox");
            }
            finally
            {
                EditLock.ExitWriteLock();
            }
        }

        public void Clear()
        {
            EditLock.EnterWriteLock();
            try
            {
                //this.HorizontalExtent = this.Width;
                this.Items.Clear();
                LineColors.Clear();
            }
            catch
            {
                //well shit...
                Globals.l2net_home.Add_Error("failed to clear colorlistbox");
            }
            finally
            {
                EditLock.ExitWriteLock();
            }
        }
    }
}
