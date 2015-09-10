using System;
using System.Collections.Generic;
using System.Text;
using SlimDX;
using SlimDX.DirectInput;

namespace L2_login
{
    public class DX_Keyboard
    {
        private System.Threading.Thread dx_keyboard_thread;
        Keyboard keyboard;

        public DX_Keyboard()
        {
            DirectInput dinput = new DirectInput();

            keyboard = new Keyboard(dinput);
            keyboard.SetCooperativeLevel(Globals.l2net_home, CooperativeLevel.Nonexclusive | CooperativeLevel.Background);
            keyboard.Acquire();

            dx_keyboard_thread = new System.Threading.Thread(new System.Threading.ThreadStart(DX_KeyboardEngine));

            dx_keyboard_thread.IsBackground = true;

            dx_keyboard_thread.Start();
        }

        private void DX_KeyboardEngine()
        {
            while (true == true)
            {
                System.Threading.Thread.Sleep(Globals.SLEEP_DirectInputDelay);

                UpdateKeyboard();
            }
        }

        private void UpdateKeyboard()
        {
            KeyboardState state = keyboard.GetCurrentState();

            bool pressed = false;

            foreach (Key k in state.PressedKeys)
            {
                if (k.ToString() == Globals.DirectInputKey)
                {
                    pressed = true;

                    if (Globals.DirectInputLast == false)
                    {
                        Globals.l2net_home.Toggle_Botting();
                        //Util.KillThreads();
                        Globals.DirectInputLast = true;
                    }
                    else
                    {
                    }
                }
                else if (Globals.DirectInputSetup == true)
                {
                    Globals.DirectInputSetupValue = k.ToString();

                    try
                    {
                        Globals.DirectInputSetup = false;
                        Globals.setupwindow.label_toggle_key.Text = Globals.DirectInputSetupValue;
                        Globals.setupwindow.button_change_toggle.Enabled = true;

                        Globals.setupwindow.button_change_kill.Enabled = true;
                        Globals.setupwindow.comboBox_voice.Enabled = true;
                        Globals.setupwindow.textBox_l2path.Enabled = true;
                        Globals.setupwindow.textBox_key.Enabled = true;
                        Globals.setupwindow.comboBox_texturemode.Enabled = true;
                        Globals.setupwindow.comboBox_viewrange.Enabled = true;
                    }
                    catch
                    {
                    }
                }
                else if (k.ToString() == Globals.DirectInputKey2)
                {
                    pressed = true;

                    if (Globals.DirectInputLast2 == false)
                    {
                        Globals.DirectInputLast2 = true;
                        Util.KillThreads();
                        Util.Stop_Connections();
                    }
                    else
                    {
                    }
                }
                else if (Globals.DirectInputSetup2 == true)
                {
                    Globals.DirectInputSetupValue2 = k.ToString();

                    try
                    {
                        Globals.DirectInputSetup2 = false;
                        Globals.setupwindow.label_kill_key.Text = Globals.DirectInputSetupValue2;
                        Globals.setupwindow.button_change_kill.Enabled = true;

                        Globals.setupwindow.button_change_toggle.Enabled = true;
                        Globals.setupwindow.comboBox_voice.Enabled = true;
                        Globals.setupwindow.textBox_l2path.Enabled = true;
                        Globals.setupwindow.textBox_key.Enabled = true;
                        Globals.setupwindow.comboBox_texturemode.Enabled = true;
                        Globals.setupwindow.comboBox_viewrange.Enabled = true;
                    }
                    catch
                    {
                    }
                }
            }

            if (pressed == false)
            {
                Globals.DirectInputLast = false;
                Globals.DirectInputLast2 = false;
            }
        }
    }
}
