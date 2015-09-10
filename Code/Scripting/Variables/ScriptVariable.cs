using System;

namespace L2_login
{
	/// <summary>
	/// Summary description for ScriptVariable.
	/// </summary>
	public class ScriptLabel
	{
		public string Name;
		public int Line;
        public string File;
        public Var_State State = Var_State.PUBLIC;
	}

	public enum Var_Types : int
	{
		NULL = 0,
		INT = 1,
		DOUBLE = 2,
		STRING = 3,
        FILEREADER = 4,
        FILEWRITER = 5,
        ARRAYLIST = 6,
        SORTEDLIST = 7,
        STACK = 8,
        QUEUE = 9,
        CLASS = 10,
        BYTEBUFFER = 11,
        WINDOW = 12,
        THREAD = 13,
        ASSIGNABLE = 20,//this is the type used in equation handling for temp variables
	}

    public enum Var_State : int
    {
        PUBLIC = 0,
        PRIVATE = 1,
        PROTECTED = 2,
        STATIC = 3
    }

	public class ScriptVariable
	{
        public object Value;
        public string Name;
        public Var_Types Type;
        public Var_State State;

		public ScriptVariable()
		{
			Value = (int)0;
			Name = "NULL";
			Type = Var_Types.INT;
            State = Var_State.PUBLIC;
		}

        public ScriptVariable(object value, string name, Var_Types type, Var_State state)
        {
            Name = name;
            Type = type;
            State = state;

            Value = value;

            switch(Type)
            {
                case Var_Types.BYTEBUFFER:
                    if (value == null)
                        Value = null;
                    else
                        Value = new ByteBuffer((ByteBuffer)value);
                    break;
                default:
                    Value = value;
                    break;
            }
        }

        public ScriptVariable Clone()
        {
            if (State == Var_State.STATIC)
            {
                return this;
            }

            ScriptVariable sout = new ScriptVariable();

            sout.Name = Name;
            sout.Type = Type;
            sout.State = State;

            switch (Type)
            {
                case Var_Types.NULL:
                    sout.Value = null;
                    break;
                case Var_Types.INT:
                    sout.Value = (long)Value;
                    break;
                case Var_Types.DOUBLE:
                    sout.Value = (double)Value;
                    break;
                case Var_Types.STRING:
                    sout.Value = (string)Value;
                    break;
                case Var_Types.FILEWRITER:
                    sout.Value = Value;
                    break;
                case Var_Types.FILEREADER:
                    sout.Value = Value;
                    break;
                case Var_Types.ARRAYLIST:
                    sout.Value = new System.Collections.ArrayList(((System.Collections.ArrayList)Value).Capacity);

                    foreach (ScriptVariable nv in ((System.Collections.ArrayList)Value))
                    {
                        ((System.Collections.ArrayList)sout.Value).Add(nv.Clone());
                    }
                    break;
                case Var_Types.SORTEDLIST:
                    sout.Value = new System.Collections.SortedList(((System.Collections.SortedList)Value).Capacity);

                    foreach (System.Collections.DictionaryEntry dic in ((System.Collections.SortedList)Value))
                    {
                        ((System.Collections.SortedList)sout.Value).Add(dic.Key, ((ScriptVariable)dic.Value).Clone());
                    }
                    break;
                case Var_Types.STACK:
                    sout.Value = new System.Collections.Stack(((System.Collections.Stack)Value).Count);

                    foreach (ScriptVariable nv in ((System.Collections.Stack)Value))
                    {
                        ((System.Collections.Stack)sout.Value).Push(nv.Clone());
                    }
                    break;
                case Var_Types.QUEUE:
                    sout.Value = new System.Collections.Queue(((System.Collections.Queue)Value).Count);

                    foreach (ScriptVariable nv in ((System.Collections.Queue)Value))
                    {
                        ((System.Collections.Queue)sout.Value).Enqueue(nv.Clone());
                    } 
                    break;
                case Var_Types.CLASS:
                    sout.Value = new Script_ClassData();
                    ((Script_ClassData)sout.Value).Name = ((Script_ClassData)Value).Name;

                    foreach (ScriptVariable nv in ((Script_ClassData)Value)._Variables.Values)
                    {
                        ((Script_ClassData)sout.Value)._Variables.Add(nv.Name, nv.Clone());
                    }
                    break;
                case Var_Types.BYTEBUFFER:
                    sout.Value = new ByteBuffer((ByteBuffer)Value);
                    break;
                case Var_Types.WINDOW:
                    sout.Value = Value;
                    break;
                case Var_Types.THREAD:
                    sout.Value = Value;
                    break;
                default:
                    sout.Value = Value;
                    break;
            }

            return sout;
        }
    }//end of class
}
