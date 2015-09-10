using System;

namespace L2_login
{
	/// <summary>
	/// Summary description for VariableList.
	/// </summary>
    public class VariableList : System.Collections.SortedList
	{
		public VariableList()
		{
			this.Clear();
		}

		public void Add_Variable(ScriptVariable var)
		{
            this.Add(var.Name, var);
		}
	}//end of class
}
