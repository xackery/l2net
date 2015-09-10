using System;

namespace L2_login
{
	public class ItemName : BaseText
	{
		public string Name = "";
		public string Add_Name = "";
		public string Description = "";
        public int Special_Enchant_Amount = 0;
        public string Special_Enchant_Desc = "";
        public bool Has_Mesh = false;

		//etcitemgrp.txt
		//int ID
		public string Icon = "";
        public long Durability = 0;
        public uint Weight = 0;
        public uint Material = 0;
        public uint Crystallizable = 0;
        public uint Stackable = 0;
        public uint Family = 0;
        public uint Grade = 0;

		//weapongrp.txt
		//int ID
        //public uint Drop_Type = 0;//not used
		//string Icon
		//int Durability;
		//int Weight
		//int Material
		//int Crystallizable
        public uint Projectile = 0;//waaay big ass values.. what the hell for?
        public uint Body_Part = 0;//really big on pet inventory things
        public uint Hardness = 0;
        public uint Random_Damage = 0;
        public uint PAtt = 0;
        public uint MAtt = 0;
        public uint Weapon_Type = 0;
        public uint Crystal_Type = 0;
        public uint Critical = 0;
        public int Hit_Mod = 0;
        public int Avoid_Mod = 0;
        public uint Shield_Pdef = 0;
        public uint Shield_Rate = 0;
        public uint Speed = 0;
        public uint MP_Consume = 0;
        public uint SS = 0;
        public uint SPS = 0;
        public uint Curvature = 0;
        public int isHero = 0;

		//armorgrp.txt
		//public int ID;
		//public int Drop_Type;
        //public uint Drop_Radius = 0;//not used
        //public uint Drop_Height = 0;//not used
		//public string Icon;
		//public int Durability;
		//public int Weight;
		//public int Material;
		//public int Crystallizable;
		//public int Body_Part;
        public uint Armor_Type = 0;
		//public int Crystal_Type;
		//public int Avoid_Mod;
        //public uint Pdef = 0;
        //public uint Mdef = 0;
        //public uint MPbonus = 0;

		public void Clear()
		{
			ID = 0;
			Name = "";
			Add_Name = "";
			Description = "";
            Special_Enchant_Amount = 0;
            Special_Enchant_Desc = "";
		}

		public void ParseETC(string inp)
		{
            int pipe = 0, oldpipe = 0;
            //ID
            pipe = inp.IndexOf('|', oldpipe);
            ID = Util.GetUInt32(inp.Substring(oldpipe, pipe - oldpipe));
            oldpipe = pipe + 1;
            //Drop Mesh 1
            pipe = inp.IndexOf('|', oldpipe);
            string mesh = inp.Substring(oldpipe, pipe - oldpipe);
            oldpipe = pipe + 1;
            if (mesh.Length > 0)
            {
                Has_Mesh = true;
            }
            else
            {
                Has_Mesh = false;
            }
            //Icon 0
            pipe = inp.IndexOf('|', oldpipe);
            Icon = inp.Substring(oldpipe, pipe - oldpipe);
            oldpipe = pipe + 1;
            //Durability
            pipe = inp.IndexOf('|', oldpipe);
            Durability = Util.GetInt64(inp.Substring(oldpipe, pipe - oldpipe));
            oldpipe = pipe + 1;
            //Weight
            pipe = inp.IndexOf('|', oldpipe);
            Weight = Util.GetUInt32(inp.Substring(oldpipe, pipe - oldpipe));
            oldpipe = pipe + 1;
            //Material
            pipe = inp.IndexOf('|', oldpipe);
            Material = Util.GetUInt32(inp.Substring(oldpipe, pipe - oldpipe));
            oldpipe = pipe + 1;
            //Crystallizable
            pipe = inp.IndexOf('|', oldpipe);
            Crystallizable = Util.GetUInt32(inp.Substring(oldpipe, pipe - oldpipe));
            oldpipe = pipe + 1;
            //Stackable
            pipe = inp.IndexOf('|', oldpipe);
            Stackable = Util.GetUInt32(inp.Substring(oldpipe, pipe - oldpipe));
            oldpipe = pipe + 1;
            //Family
            pipe = inp.IndexOf('|', oldpipe);
            Family = Util.GetUInt32(inp.Substring(oldpipe, pipe - oldpipe));
            oldpipe = pipe + 1;
            //Grade
            Grade = Util.GetUInt32(inp.Substring(oldpipe, inp.Length - oldpipe));
		}

        public void ParseETC(ItemName inp)
        {
            //ID
            ID = inp.ID;
            //Drop Mesh 1
            Has_Mesh = inp.Has_Mesh;
            //Icon 0
            Icon = inp.Icon;
            //Durability
            Durability = inp.Durability;
            //Weight
            Weight = inp.Weight;
            //Material
            Material = inp.Material;
            //Crystallizable
            Crystallizable = inp.Crystallizable;
            //Stackable
            Stackable = inp.Stackable;
            //Family
            Family = inp.Family;
            //Grade
            Grade = inp.Grade;
        }

		public void ParseWeapon(string inp)
		{
            int pipe = 0, oldpipe = 0;
            //ID
            pipe = inp.IndexOf('|', oldpipe);
            ID = Util.GetUInt32(inp.Substring(oldpipe, pipe - oldpipe));
            oldpipe = pipe + 1;
            //Drop Mesh 1
            pipe = inp.IndexOf('|', oldpipe);
            string mesh = inp.Substring(oldpipe, pipe - oldpipe);
            oldpipe = pipe + 1;
            if (mesh.Length > 0)
            {
                Has_Mesh = true;
            }
            else
            {
                Has_Mesh = false;
            }
            //Icon 0
            pipe = inp.IndexOf('|', oldpipe);
            Icon = inp.Substring(oldpipe, pipe - oldpipe);
            oldpipe = pipe + 1;
            //Durability
            pipe = inp.IndexOf('|', oldpipe);
            Durability = Util.GetInt64(inp.Substring(oldpipe, pipe - oldpipe));
            oldpipe = pipe + 1;
            //Weight
            pipe = inp.IndexOf('|', oldpipe);
            Weight = Util.GetUInt32(inp.Substring(oldpipe, pipe - oldpipe));
            oldpipe = pipe + 1;
            //Material
            pipe = inp.IndexOf('|', oldpipe);
            Material = Util.GetUInt32(inp.Substring(oldpipe, pipe - oldpipe));
            oldpipe = pipe + 1;
            //Crystallizable
            pipe = inp.IndexOf('|', oldpipe);
            Crystallizable = Util.GetUInt32(inp.Substring(oldpipe, pipe - oldpipe));
            oldpipe = pipe + 1;
            //Body_Part
            pipe = inp.IndexOf('|', oldpipe);
            Body_Part = Util.GetUInt32(inp.Substring(oldpipe, pipe - oldpipe));
            oldpipe = pipe + 1;
            //Hardness
            pipe = inp.IndexOf('|', oldpipe);
            Hardness = Util.GetUInt32(inp.Substring(oldpipe, pipe - oldpipe));
            oldpipe = pipe + 1;
			//Random_Damage
            pipe = inp.IndexOf('|', oldpipe);
            Random_Damage = Util.GetUInt32(inp.Substring(oldpipe, pipe - oldpipe));
            oldpipe = pipe + 1;
            //PAtt
            /*
            pipe = inp.IndexOf('|', oldpipe);
            PAtt = Util.GetUInt32(inp.Substring(oldpipe, pipe - oldpipe));
            oldpipe = pipe + 1;
            //MAtt
            pipe = inp.IndexOf('|', oldpipe);
            MAtt = Util.GetUInt32(inp.Substring(oldpipe, pipe - oldpipe));
            oldpipe = pipe + 1;
            */
            //Weapon_Type
            pipe = inp.IndexOf('|', oldpipe);
            Weapon_Type = Util.GetUInt32(inp.Substring(oldpipe, pipe - oldpipe));
            oldpipe = pipe + 1;
            //Crystal_Type
            pipe = inp.IndexOf('|', oldpipe);
            Crystal_Type = Util.GetUInt32(inp.Substring(oldpipe, pipe - oldpipe));
            oldpipe = pipe + 1;
            /*
            //Critical
            pipe = inp.IndexOf('|', oldpipe);
            Critical = Util.GetUInt32(inp.Substring(oldpipe, pipe - oldpipe));
            oldpipe = pipe + 1;
            //Hit_Mod
            pipe = inp.IndexOf('|', oldpipe);
            Hit_Mod = Util.GetInt32(inp.Substring(oldpipe, pipe - oldpipe));
            oldpipe = pipe + 1;
            //Avoid_Mod
            pipe = inp.IndexOf('|', oldpipe);
            Avoid_Mod = Util.GetInt32(inp.Substring(oldpipe, pipe - oldpipe));
            oldpipe = pipe + 1;
            //Shield_Pdef
            pipe = inp.IndexOf('|', oldpipe);
            Shield_Pdef = Util.GetUInt32(inp.Substring(oldpipe, pipe - oldpipe));
            oldpipe = pipe + 1;
            //Shield_Rate
            pipe = inp.IndexOf('|', oldpipe);
            Shield_Rate = Util.GetUInt32(inp.Substring(oldpipe, pipe - oldpipe));
            oldpipe = pipe + 1;
            //Speed
            pipe = inp.IndexOf('|', oldpipe);
            Speed = Util.GetUInt32(inp.Substring(oldpipe, pipe - oldpipe));
            oldpipe = pipe + 1;
            */
            //MP_Consume
            pipe = inp.IndexOf('|', oldpipe);
            MP_Consume = Util.GetUInt32(inp.Substring(oldpipe, pipe - oldpipe));
            oldpipe = pipe + 1;
            //SS
            pipe = inp.IndexOf('|', oldpipe);
            SS = Util.GetUInt32(inp.Substring(oldpipe, pipe - oldpipe));
            oldpipe = pipe + 1;
            //SPS
            pipe = inp.IndexOf('|', oldpipe);
            SPS = Util.GetUInt32(inp.Substring(oldpipe, pipe - oldpipe));
            oldpipe = pipe + 1;
            //Curvature
            pipe = inp.IndexOf('|', oldpipe);
            Curvature = Util.GetUInt32(inp.Substring(oldpipe, pipe - oldpipe));
            oldpipe = pipe + 1;
            //isHero
            isHero = Util.GetInt32(inp.Substring(oldpipe, inp.Length - oldpipe));
		}

        public void ParseWeapon(ItemName inp)
        {
            //ID
            ID = inp.ID;
            //Drop Mesh 1
            Has_Mesh = inp.Has_Mesh;
            //Icon 0
            Icon = inp.Icon;
            //Durability
            Durability = inp.Durability;
            //Weight
            Weight = inp.Weight;
            //Material
            Material = inp.Material;
            //Crystallizable
            Crystallizable = inp.Crystallizable;
            //Body_Part
            Body_Part = inp.Body_Part;
            //Hardness
            Hardness = inp.Hardness;
            //Random_Damage
            Random_Damage = inp.Random_Damage;
            //PAtt
            PAtt = inp.PAtt;
            //MAtt
            MAtt = inp.MAtt;
            //Weapon_Type
            Weapon_Type = inp.Weapon_Type;
            //Crystal_Type
            Crystal_Type = inp.Crystal_Type;
            //Critical
            Critical = inp.Critical;
            //Hit_Mod
            Hit_Mod = inp.Hit_Mod;
            //Avoid_Mod
            Avoid_Mod = inp.Avoid_Mod;
            //Shield_Pdef
            Shield_Pdef = inp.Shield_Pdef;
            //Shield_Rate
            Shield_Rate = inp.Shield_Rate;
            //Speed
            Speed = inp.Speed;
            //MP_Consume
            MP_Consume = inp.MP_Consume;
            //SS
            SS = inp.SS;
            //SPS
            SPS = inp.SPS;
            //Curvature
            Curvature = inp.Curvature;
            //isHero
            isHero = inp.isHero;
        }

		public void ParseArmor(string inp)
		{
            int pipe = 0, oldpipe = 0;
            //ID
            pipe = inp.IndexOf('|');
			ID = Util.GetUInt32(inp.Substring(0,pipe));
            oldpipe = pipe + 1;
            //Drop Mesh 1
            pipe = inp.IndexOf('|', oldpipe);
            string mesh = inp.Substring(oldpipe, pipe - oldpipe);
            oldpipe = pipe + 1;
            if (mesh.Length > 0)
            {
                Has_Mesh = true;
            }
            else
            {
                Has_Mesh = false;
            }
            //Icon 0
            pipe = inp.IndexOf('|', oldpipe);
            Icon = inp.Substring(oldpipe, pipe - oldpipe);
            oldpipe = pipe + 1;
            //Durability
            pipe = inp.IndexOf('|', oldpipe);
            Durability = Util.GetInt64(inp.Substring(oldpipe, pipe - oldpipe));
            oldpipe = pipe + 1;
            //Weight
            pipe = inp.IndexOf('|', oldpipe);
            Weight = Util.GetUInt32(inp.Substring(oldpipe, pipe - oldpipe));
            oldpipe = pipe + 1;
            //Material
            pipe = inp.IndexOf('|', oldpipe);
            Material = Util.GetUInt32(inp.Substring(oldpipe, pipe - oldpipe));
            oldpipe = pipe + 1;
            //Crystallizable
            pipe = inp.IndexOf('|', oldpipe);
            Crystallizable = Util.GetUInt32(inp.Substring(oldpipe, pipe - oldpipe));
            oldpipe = pipe + 1;
            //Body_Part
            pipe = inp.IndexOf('|', oldpipe);
            Body_Part = Util.GetUInt32(inp.Substring(oldpipe, pipe - oldpipe));
            oldpipe = pipe + 1;
            //Armor_Type
            pipe = inp.IndexOf('|', oldpipe);
            Armor_Type = Util.GetUInt32(inp.Substring(oldpipe, pipe - oldpipe));
            oldpipe = pipe + 1;
            //Crystal_Type
            pipe = inp.IndexOf('|', oldpipe);
            Crystal_Type = Util.GetUInt32(inp.Substring(oldpipe, pipe - oldpipe));
            oldpipe = pipe + 1;
            //Avoid_Mod
            pipe = inp.IndexOf('|', oldpipe);
            Avoid_Mod = Util.GetInt32(inp.Substring(oldpipe, inp.Length - oldpipe));
            /*
            oldpipe = pipe + 1;
            //Pdef
            pipe = inp.IndexOf('|', oldpipe);
            Pdef = Util.GetUInt32(inp.Substring(oldpipe, pipe - oldpipe));
            oldpipe = pipe + 1;
            //Mdef
            pipe = inp.IndexOf('|', oldpipe);
            Mdef = Util.GetUInt32(inp.Substring(oldpipe, pipe - oldpipe));
            oldpipe = pipe + 1;
            //MPbonus
            MPbonus = Util.GetUInt32(inp.Substring(oldpipe, inp.Length - oldpipe));*/
		}

        public void ParseArmor(ItemName inp)
        {
            //ID
            ID = inp.ID;
            //Drop Mesh 1
            Has_Mesh = inp.Has_Mesh;
            //Icon 0
            Icon = inp.Icon;
            //Durability
            Durability = inp.Durability;
            //Weight
            Weight = inp.Weight;
            //Material
            Material = inp.Material;
            //Crystallizable
            Crystallizable = inp.Crystallizable;
            //Body_Part
            Body_Part = inp.Body_Part;
            //Armor_Type
            Armor_Type = inp.Armor_Type;
            //Crystal_Type
            Crystal_Type = inp.Crystal_Type;
            //Avoid_Mod
            Avoid_Mod = inp.Avoid_Mod;
            //Pdef
            /*
            Pdef = inp.Pdef;
            //Mdef
            Mdef = inp.Mdef;
            //MPbonus
            MPbonus = inp.MPbonus;*/
        }

		public void Parse(string inp)
		{
            int pipe = 0, oldpipe = 0;
            //ID
            pipe = inp.IndexOf('|', oldpipe);
            ID = Util.GetUInt32(inp.Substring(oldpipe, pipe - oldpipe));
            oldpipe = pipe + 1;
            //Name
            pipe = inp.IndexOf('|', oldpipe);
            Name = inp.Substring(oldpipe, pipe - oldpipe);
            oldpipe = pipe + 1;
            //Add_Name
            pipe = inp.IndexOf('|', oldpipe);
            Add_Name = inp.Substring(oldpipe, pipe - oldpipe);
            oldpipe = pipe + 1;
            //Description
            pipe = inp.IndexOf('|', oldpipe);
            Description = inp.Substring(oldpipe, pipe - oldpipe);
            oldpipe = pipe + 1;
            //Special_Enchant_Amount
            pipe = inp.IndexOf('|', oldpipe);
            if (pipe == -1)
            {
                Special_Enchant_Amount = Util.GetInt32(inp.Substring(oldpipe, inp.Length - oldpipe));
                return;
            }
            Special_Enchant_Amount = Util.GetInt32(inp.Substring(oldpipe, pipe - oldpipe));
            oldpipe = pipe + 1;
            //Special_Enchant_Desc
            Special_Enchant_Desc = inp.Substring(oldpipe, inp.Length - oldpipe);
        }
	}//end of ItemName
}
