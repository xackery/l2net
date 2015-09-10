using System;

namespace L2_login
{
	public class QuestName : BaseText
	{
		//quest_id|quest_prog|main_name|prog_name|description|quest_x|quest_y|quest_z|lvl_min|lvl_max|quest_type|entity_name|contact_npc_id|contact_npc_x|contact_npc_y|contact_npc_z|restricions|short_description|clan_pet_quest|req_quest_complete|area_id
        public uint quest_prog = 0;
		public string main_name = "";
		public string prog_name = "";
		public string description = "";
        public int quest_x = 0;
        public int quest_y = 0;
        public int quest_z = 0;
        public uint lvl_min = 0;
        public uint lvl_max = 0;
        public uint quest_type = 0;
		public string entity_name = "";
        public uint contact_npc_id = 0;
        public int contact_npc_x = 0;
        public int contact_npc_y = 0;
        public int contact_npc_z = 0;
		public string restricions = "";
		public string short_description = "";
        public uint clan_pet_quest = 0;
        public uint req_quest_complete = 0;
        public uint area_id = 0;

		public void Clear()
		{
			ID = 0;
			quest_prog = 0;
			main_name = "";
			prog_name = "";
			description = "";
			quest_x = 0;
			quest_y = 0;
			quest_z = 0;
			lvl_min = 0;
			lvl_max = 0;
			quest_type = 0;
			entity_name = "";
			contact_npc_id = 0;
			contact_npc_x = 0;
			contact_npc_y = 0;
			contact_npc_z = 0;
			restricions = "";
			short_description = "";
			clan_pet_quest = 0;
			req_quest_complete = 0;
			area_id = 0;
		}

		public void Parse(string inp)
		{
            int pipe = 0, oldpipe = 0;
			//ID
            pipe = inp.IndexOf('|', oldpipe);
            ID = Util.GetUInt32(inp.Substring(oldpipe, pipe - oldpipe));
            oldpipe = pipe + 1;
            //quest_prog
            pipe = inp.IndexOf('|', oldpipe);
            quest_prog = Util.GetUInt32(inp.Substring(oldpipe, pipe - oldpipe));
            oldpipe = pipe + 1;
            //main_name
            pipe = inp.IndexOf('|', oldpipe);
            main_name = inp.Substring(oldpipe, pipe - oldpipe);
            oldpipe = pipe + 1;
            //prog_name
            pipe = inp.IndexOf('|', oldpipe);
            prog_name = inp.Substring(oldpipe, pipe - oldpipe);
            oldpipe = pipe + 1;
            //description
            pipe = inp.IndexOf('|', oldpipe);
            description = inp.Substring(oldpipe, pipe - oldpipe);
            oldpipe = pipe + 1;
            //quest_x
            pipe = inp.IndexOf('|', oldpipe);
            quest_x = Util.GetInt32(inp.Substring(oldpipe, pipe - oldpipe));
            oldpipe = pipe + 1;
            //quest_y
            pipe = inp.IndexOf('|', oldpipe);
            quest_y = Util.GetInt32(inp.Substring(oldpipe, pipe - oldpipe));
            oldpipe = pipe + 1;
            //quest_z
            pipe = inp.IndexOf('|', oldpipe);
            quest_z = Util.GetInt32(inp.Substring(oldpipe, pipe - oldpipe));
            oldpipe = pipe + 1;
            //lvl_min
            pipe = inp.IndexOf('|', oldpipe);
            lvl_min = Util.GetUInt32(inp.Substring(oldpipe, pipe - oldpipe));
            oldpipe = pipe + 1;
            //lvl_max
            pipe = inp.IndexOf('|', oldpipe);
            lvl_max = Util.GetUInt32(inp.Substring(oldpipe, pipe - oldpipe));
            oldpipe = pipe + 1;
            //quest_type
            pipe = inp.IndexOf('|', oldpipe);
            quest_type = Util.GetUInt32(inp.Substring(oldpipe, pipe - oldpipe));
            oldpipe = pipe + 1;
			//entity_name
            pipe = inp.IndexOf('|', oldpipe);
            entity_name = inp.Substring(oldpipe, pipe - oldpipe);
            oldpipe = pipe + 1;
            //contact_npc_id
            pipe = inp.IndexOf('|', oldpipe);
            contact_npc_id = Util.GetUInt32(inp.Substring(oldpipe, pipe - oldpipe));
            oldpipe = pipe + 1;
            //contact_npc_x
            pipe = inp.IndexOf('|', oldpipe);
            contact_npc_x = Util.GetInt32(inp.Substring(oldpipe, pipe - oldpipe));
            oldpipe = pipe + 1;
            //contact_npc_y
            pipe = inp.IndexOf('|', oldpipe);
            contact_npc_y = Util.GetInt32(inp.Substring(oldpipe, pipe - oldpipe));
            oldpipe = pipe + 1;
            //contact_npc_z
            pipe = inp.IndexOf('|', oldpipe);
            contact_npc_z = Util.GetInt32(inp.Substring(oldpipe, pipe - oldpipe));
            oldpipe = pipe + 1;
            //restricions
            pipe = inp.IndexOf('|', oldpipe);
            restricions = inp.Substring(oldpipe, pipe - oldpipe);
            oldpipe = pipe + 1;
            //short_description
            pipe = inp.IndexOf('|', oldpipe);
            short_description = inp.Substring(oldpipe, pipe - oldpipe);
            oldpipe = pipe + 1;
            //clan_pet_quest
            pipe = inp.IndexOf('|', oldpipe);
            clan_pet_quest = Util.GetUInt32(inp.Substring(oldpipe, pipe - oldpipe));
            oldpipe = pipe + 1;
            //req_quest_complete
            pipe = inp.IndexOf('|', oldpipe);
            req_quest_complete = Util.GetUInt32(inp.Substring(oldpipe, pipe - oldpipe));
            oldpipe = pipe + 1;
            //area_id
            area_id = Util.GetUInt32(inp.Substring(oldpipe, inp.Length - oldpipe));
		}
	}//end of HennaGroup
}
