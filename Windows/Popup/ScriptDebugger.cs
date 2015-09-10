using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace L2_login
{
    public partial class ScriptDebugger : Form
    {
        public ScriptDebugger()
        {
            InitializeComponent();

            UpdateUI();
        }

        public void UpdateUI()
        {
            button_close.Text = Globals.m_ResourceManager.GetString("button_npc_close");
            button_snapshot.Text = Globals.m_ResourceManager.GetString("snapshot");
            button_pause_resume.Text = Globals.m_ResourceManager.GetString("pause_resume_script");

            this.Refresh();
        }

        private void button_close_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void SetButtons(bool state)
        {
            button_snapshot.Enabled = state;
            button_pause_resume.Enabled = state;
        }

        private void button_snapshot_Click(object sender, EventArgs e)
        {
            SetButtons(false);

            //fill in the tables of stuff
            treeView_variables.Nodes.Clear();

            ScriptState last_state = Globals.gamedata.CurrentScriptState;

            if (last_state != ScriptState.Paused)
            {
                Globals.l2net_home.Add_Debug("jingjing not in sleeping state... moving to sleeping state");
                Globals.gamedata.CurrentScriptState = ScriptState.Paused;
                System.Threading.Thread.Sleep(500);
            }

            TakeSnapshot();

            if (last_state != ScriptState.Paused)
            {
                Globals.l2net_home.Add_Debug("restoring previous state");
                Globals.gamedata.CurrentScriptState = last_state;
            }

            SetButtons(true);
        }

        private void TakeSnapshot()
        {
            //locks...
            TreeNode lock_node = new TreeNode("Locks");

            foreach (string sv in ScriptEngine.Locks.Keys)
            {
                TreeNode sv_node = new TreeNode(sv);

                lock_node.Nodes.Add(sv_node);
            }

            treeView_variables.Nodes.Add(lock_node);

            //files...
            TreeNode file_node = new TreeNode("Files");

            foreach (string sv in ScriptEngine.Files.Keys)
            {
                TreeNode sv_node = new TreeNode(sv);

                file_node.Nodes.Add(sv_node);
            }

            treeView_variables.Nodes.Add(file_node);

            //classes...
            TreeNode class_node = new TreeNode("Classes");

            foreach (string sv in ScriptEngine.Classes.Keys)
            {
                TreeNode sv_node = new TreeNode(sv);

                class_node.Nodes.Add(sv_node);
            }

            treeView_variables.Nodes.Add(class_node);

            //globals...
            TreeNode global_node = new TreeNode("Globals");

            foreach (ScriptVariable sv in ((VariableList)ScriptEngine.GlobalVariables).Values)
            {
                TreeNode sv_node = new TreeNode(sv.Name);
                BuildVarTree(sv_node, sv);

                global_node.Nodes.Add(sv_node);
            }

            treeView_variables.Nodes.Add(global_node);
            //the stack
            foreach (ScriptThread st in ScriptEngine.Threads.Values)
            {
                TreeNode thread_node = new TreeNode("Thread " + st.ID.ToString());

                TreeNode thread_info_node = new TreeNode("Thread Info");
                TreeNode thread_info_node_file = new TreeNode("File : " + st.Current_File);
                TreeNode thread_info_node_line = new TreeNode("Running Line : " + st.Line_Pos.ToString());
                TreeNode thread_info_node_stack = new TreeNode("Current Stack Depth : " + (st.StackHeight + 1).ToString());
                TreeNode thread_info_node_func = new TreeNode("Function Call Depth : " + st._functioncalls.Count.ToString());
                TreeNode thread_info_node_sub = new TreeNode("Sub Call Depth : " + st._subcalls.Count.ToString());
                TreeNode thread_info_node_sleep = new TreeNode("Sleep Until : " + st.Sleep_Until.ToLongTimeString());
                thread_info_node.Nodes.Add(thread_info_node_file);
                thread_info_node.Nodes.Add(thread_info_node_line);
                thread_info_node.Nodes.Add(thread_info_node_stack);
                thread_info_node.Nodes.Add(thread_info_node_func);
                thread_info_node.Nodes.Add(thread_info_node_sub);
                thread_info_node.Nodes.Add(thread_info_node_sleep);
                thread_node.Nodes.Add(thread_info_node);

                for (int i = 0; i < st._stack.Count; i++)
                {
                    TreeNode vlist_node = new TreeNode("Stack Level " + i.ToString());

                    foreach (ScriptVariable sv in ((VariableList)st._stack[i]).Values)
                    {
                        TreeNode sv_node = new TreeNode(sv.Name);
                        BuildVarTree(sv_node, sv);

                        vlist_node.Nodes.Add(sv_node);
                    }

                    thread_node.Nodes.Add(vlist_node);
                }

                treeView_variables.Nodes.Add(thread_node);
            }
        }

        private void BuildVarTree(TreeNode parent, ScriptVariable sv)
        {
#if! DEBUG
            try
            {
#endif
            TreeNode type;
            TreeNode value;
            TreeNode count;

            switch (sv.Type)
            {
                case Var_Types.NULL:
                    type = new TreeNode("Type : NULL");
                    value = new TreeNode("Value : NULL");

                    parent.Nodes.Add(type);
                    parent.Nodes.Add(value);
                    break;
                case Var_Types.INT:
                    type = new TreeNode("Type : INT");
                    value = new TreeNode("Value : " + System.Convert.ToInt64(sv.Value).ToString());

                    parent.Nodes.Add(type);
                    parent.Nodes.Add(value);
                    break;
                case Var_Types.DOUBLE:
                    type = new TreeNode("Type : DOUBLE");
                    value = new TreeNode("Value : " + System.Convert.ToDouble(sv.Value).ToString());

                    parent.Nodes.Add(type);
                    parent.Nodes.Add(value);
                    break;
                case Var_Types.STRING:
                    type = new TreeNode("Type : STRING");
                    value = new TreeNode("Value : " + System.Convert.ToString(sv.Value));

                    parent.Nodes.Add(type);
                    parent.Nodes.Add(value);
                    break;
                case Var_Types.FILEWRITER:
                    type = new TreeNode("Type : FILEWRITER");
                    value = new TreeNode("Value : " + ((System.IO.StreamWriter)sv.Value).ToString());

                    parent.Nodes.Add(type);
                    parent.Nodes.Add(value);
                    break;
                case Var_Types.FILEREADER:
                    type = new TreeNode("Type : FILEREADER");
                    value = new TreeNode("Value : " + ((System.IO.StreamReader)sv.Value).ToString());

                    parent.Nodes.Add(type);
                    parent.Nodes.Add(value);
                    break;
                case Var_Types.ARRAYLIST:
                    type = new TreeNode("Type : ARRAYLIST");
                    count = new TreeNode("Count : " + ((System.Collections.ArrayList)sv.Value).Count.ToString());
                    value = new TreeNode("Values");

                    foreach (ScriptVariable child_sv in ((System.Collections.ArrayList)sv.Value))
                    {
                        //TreeNode child_node = new TreeNode(child_sv.Name);
                        TreeNode child_node = new TreeNode("[" + ((System.Collections.ArrayList)sv.Value).IndexOf(child_sv).ToString() + "]");
                        child_node.Nodes.Add("Index : " + ((System.Collections.ArrayList)sv.Value).IndexOf(child_sv).ToString());
                        child_node.Nodes.Add("Name : " + child_sv.Name);

                        BuildVarTree(child_node, child_sv);

                        value.Nodes.Add(child_node);
                    }

                    parent.Nodes.Add(type);
                    parent.Nodes.Add(count);
                    parent.Nodes.Add(value);
                    break;
                case Var_Types.SORTEDLIST:
                    type = new TreeNode("Type : SORTEDLIST");
                    count = new TreeNode("Count : " + ((System.Collections.SortedList)sv.Value).Count.ToString());
                    value = new TreeNode("Values");

                    try
                    {
                        foreach (string key in ((System.Collections.SortedList)sv.Value).Keys)
                        {
                            try
                            {
                                ScriptVariable child_sv = (ScriptVariable)(((System.Collections.SortedList)sv.Value)[key]);

                                //TreeNode child_node = new TreeNode(child_sv.Name);
                                TreeNode child_node = new TreeNode("[" + key + "]");
                                child_node.Nodes.Add("Key : " + key);
                                child_node.Nodes.Add("Name : " + child_sv.Name);

                                BuildVarTree(child_node, child_sv);

                                value.Nodes.Add(child_node);
                            }
                            catch
                            {
                                Globals.l2net_home.Add_Debug("error creating debug leaf for sortedlist - foreach inside " + key);
                            }
                        }
                    }
                    catch
                    {
                        Globals.l2net_home.Add_Debug("error creating debug leaf for sortedlist - foreach " + sv.Name);
                    }

                    parent.Nodes.Add(type);
                    parent.Nodes.Add(count);
                    parent.Nodes.Add(value);
                    break;
                case Var_Types.STACK:
                    type = new TreeNode("Type : STACK");
                    count = new TreeNode("Count : " + ((System.Collections.Stack)sv.Value).Count.ToString());

                    parent.Nodes.Add(type);
                    parent.Nodes.Add(count);
                    break;
                case Var_Types.QUEUE:
                    type = new TreeNode("Type : QUEUE");
                    count = new TreeNode("Count : " + ((System.Collections.Queue)sv.Value).Count.ToString());

                    parent.Nodes.Add(type);
                    parent.Nodes.Add(count);
                    break;
                case Var_Types.CLASS:
                    type = new TreeNode("Type : CLASS - " + ((Script_ClassData)sv.Value).Name);
                    value = new TreeNode("Values");

                    foreach (ScriptVariable child_sv in ((Script_ClassData)sv.Value)._Variables.Values)
                    {
                        TreeNode child_node = new TreeNode(child_sv.Name);

                        BuildVarTree(child_node, child_sv);

                        value.Nodes.Add(child_node);
                    }

                    parent.Nodes.Add(type);
                    parent.Nodes.Add(value);
                    break;
                case Var_Types.BYTEBUFFER:
                    type = new TreeNode("Type : BYTEBUFFER");
                    count = new TreeNode("Length - " + ((ByteBuffer)sv.Value).Length().ToString());
                    value = new TreeNode("Values");

                    for (int i = 0; i < ((ByteBuffer)sv.Value).Length(); i++)
                    {
                        TreeNode child_node = new TreeNode("0x" + i.ToString("X2") + " : " + "0x" + ((ByteBuffer)sv.Value).GetByte(i).ToString("X2") + " = '" + (char)((ByteBuffer)sv.Value).GetByte(i) + "'");
                        value.Nodes.Add(child_node);
                    }

                    parent.Nodes.Add(type);
                    parent.Nodes.Add(count);
                    parent.Nodes.Add(value);
                    break;
                case Var_Types.WINDOW:
                    type = new TreeNode("Type : WINDOW");

                    parent.Nodes.Add(type);
                    break;
                case Var_Types.THREAD:
                    type = new TreeNode("Type : THREAD");

                    TreeNode thread_info_node_id = new TreeNode("ID : " + ((ScriptThread)sv.Value).ID.ToString());
                    TreeNode thread_info_node_file = new TreeNode("File : " + ((ScriptThread)sv.Value).Current_File);
                    TreeNode thread_info_node_line = new TreeNode("Running Line : " + ((ScriptThread)sv.Value).Line_Pos.ToString());
                    TreeNode thread_info_node_stack = new TreeNode("Current Stack Depth : " + (((ScriptThread)sv.Value).StackHeight + 1).ToString());
                    TreeNode thread_info_node_func = new TreeNode("Function Call Depth : " + ((ScriptThread)sv.Value)._functioncalls.Count.ToString());
                    TreeNode thread_info_node_sub = new TreeNode("Sub Call Depth : " + ((ScriptThread)sv.Value)._subcalls.Count.ToString());
                    TreeNode thread_info_node_sleep = new TreeNode("Sleep Until : " + ((ScriptThread)sv.Value).Sleep_Until.ToLongTimeString());
                    parent.Nodes.Add(thread_info_node_id);
                    parent.Nodes.Add(thread_info_node_file);
                    parent.Nodes.Add(thread_info_node_line);
                    parent.Nodes.Add(thread_info_node_stack);
                    parent.Nodes.Add(thread_info_node_func);
                    parent.Nodes.Add(thread_info_node_sub);
                    parent.Nodes.Add(thread_info_node_sleep);
                    break;
            }
#if! DEBUG
            }
            catch
            {
                //broke our little tree... too bad
                Globals.l2net_home.Add_Debug("error creating debug leaf for var: " + sv.Name + " of type: " + sv.Type.ToString());
            }
#endif
        }

        private void button_pause_resume_Click(object sender, EventArgs e)
        {
            if (Globals.gamedata.CurrentScriptState == ScriptState.Paused)
            {
                Globals.gamedata.CurrentScriptState = ScriptState.Running;
                Globals.l2net_home.Add_Debug("Script Resumed from Paused State");
            }
            else
            {
                Globals.gamedata.CurrentScriptState = ScriptState.Paused;
                Globals.l2net_home.Add_Debug("Script Paused");
            }
        }

    }
}