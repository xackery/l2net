//#define TESTING

using System;
using System.Collections;

namespace L2_login
{
    public enum ScriptState : byte
    {
        //0 = stopped ok
        //1 - running ok
        //2 - stopped script complete
        //3 - stopped error
        //4 - stopped EOF
        //5 - paused for buffing/healing
        Stopped = 0,
        Running = 1,
        Finished = 2,
        Error = 3,
        EOF = 4,
        Paused = 5,
        DelayStart = 6
    }

    public partial class ScriptEngine
	{
		public System.Threading.Thread scriptthread;

        public ScriptEngine()
		{
            Reset_Script();
		}

		public void Reset_Script()
		{
            if (scriptthread != null)
            {
                try
                {
                    scriptthread.Abort();
                    System.Threading.Thread.Sleep(200);
                    scriptthread = null;
                }
                catch
                {
                }
            }
			//gotta reset:
			//stack
			//stack pointer (height)
			//variable lists
			//label lists
            Files.Clear();
            Threads.Clear();
            ClearEvents();
            Locks.Clear();
            Classes.Clear();
            Blocked_ServerPackets.Clear();
            Blocked_ServerPacketsEX.Clear();
            Blocked_ClientPackets.Clear();
            Blocked_ClientPacketsEX.Clear();

            GlobalVariables.Clear();

            ResetThreads();

            scriptthread = new System.Threading.Thread(new System.Threading.ThreadStart(RunScript));
            scriptthread.Priority = System.Threading.ThreadPriority.AboveNormal;

            //scriptthread.SetApartmentState(System.Threading.ApartmentState.STA);
            scriptthread.IsBackground = true;
		}

        public bool Proccess_Line(string line, bool advance_line)
        {
            ScriptLine new_line = new ScriptLine();
            new_line.FullLine = line;

            string cmd = Get_String(ref line).ToUpperInvariant();

            new_line.Command = GetCommandType(cmd);
            new_line.UnParsedParams = line;

            return Proccess_Line(new_line, advance_line);
        }

        public bool Proccess_Line(ScriptLine command, bool advance_line)
		{
            ScriptCommands my_command = command.Command;
            string line = command.UnParsedParams;

            bool do_advance = false;

#if !DEBUG
			try
			{
#endif
			switch(my_command)
			{
                case ScriptCommands.SET:
                    Dead_Command("SET","=");
                    do_advance = true;
                    break;
                case ScriptCommands.MATH:
                    Dead_Command("MATH","=");
                    do_advance = true;
                    break;
                case ScriptCommands.SORT:
                    Script_SORT(line);
                    do_advance = true;
                    break;
                case ScriptCommands.DELETE_GLOBAL:
                    Script_DELETE_GLOBAL(line);
                    do_advance = true;
                    break;
                case ScriptCommands.DELETE:
                    Script_DELETE(line);
                    do_advance = true;
                    break;
                case ScriptCommands.TEST_WEBSITE:
                    Script_TEST_WEBSITE(line);
                    do_advance = true;
                    break;
                case ScriptCommands.TEST_ODBC:
                    Script_TEST_ODBC(line);
                    do_advance = true;
                    break;
                case ScriptCommands.TEST_DNS:
                    Script_TEST_DNS(line);
                    do_advance = true;
                    break;
                case ScriptCommands.TEST_PING:
                    Script_TEST_PING(line);
                    do_advance = true;
                    break;
                case ScriptCommands.SEND_EMAIL:
                    Script_SEND_EMAIL(line);
                    do_advance = true;
                    break;
                case ScriptCommands.MESSAGE_BOX:
                    Script_MESSAGE_BOX(line);
                    do_advance = true;
                    break;
                case ScriptCommands.NMESSAGE_BOX:
                    Script_NMESSAGE_BOX(line);
                    do_advance = true;
                    break;
                case ScriptCommands.NMESSAGE_BOX2:
                    Script_NMESSAGE_BOX2(line);
                    do_advance = true;
                    break;
                case ScriptCommands.INCLUDE:
                    Script_INCLUDE(line);
                    do_advance = true;
                    break;
                case ScriptCommands.DEFAULT:
                    do_advance = true;//we can just skip over this line, nothing really needed
                    break;
                case ScriptCommands.CASE:
                    do_advance = true;//we can just skip over this line, nothing really needed
                    break;
                case ScriptCommands.SWITCH:
                    Script_SWITCH(line);
                    break;
                case ScriptCommands.ENDSWITCH:
                    do_advance = true;//we can just skip over this line, nothing really needed
                    break;
                case ScriptCommands.LOCK:
                    return Script_LOCK(line);
                case ScriptCommands.UNLOCK:
                    Script_UNLOCK(line);
                    do_advance = true;
                    break;
                case ScriptCommands.SET_EVENT:
                    Script_SET_EVENT(line);
                    do_advance = true;
                    break;
                case ScriptCommands.THREAD:
                    Script_THREAD(line);
                    do_advance = true;
                    break;
                case ScriptCommands.BREAK:
					Script_BREAK(line);
					break;
                case ScriptCommands.GET_TIME:
					Script_GET_TIME(line);
                    do_advance = true;
                    break;
                case ScriptCommands.CALLSUB:
					Script_CALLSUB(line);
					break;
                case ScriptCommands.RETURNSUB:
					Script_RETURNSUB();
					break;
                case ScriptCommands.SUB:
					Script_SUB(line);
                    do_advance = true;
                    break;
                case ScriptCommands.CALL_EXTERN:
                    Script_CALL_EXTERN(line);
                    break;
                case ScriptCommands.CALL:
					Script_CALL(line);
					break;
                case ScriptCommands.RETURN:
					Script_RETURN(line);
					break;
                case ScriptCommands.FUNCTION:
					Script_FUNCTION(line);
                    do_advance = true;
                    break;
                case ScriptCommands.DISTANCE:
					Script_DISTANCE(line);
                    do_advance = true;
                    break;
                case ScriptCommands.WHILE:
					Script_WHILE(line);
					break;
                case ScriptCommands.WEND:
					Script_WEND();
					break;
                case ScriptCommands.DO:
                    do_advance = true;
                    break;
                case ScriptCommands.LOOP:
					Script_LOOP(line);
					break;
                case ScriptCommands.FOREACH:
                    Script_FOREACH(line);
                    break;
                case ScriptCommands.NEXTEACH:
                    Script_NEXTEACH();
                    break;
                case ScriptCommands.FOR:
					Script_FOR(line);
					break;
                case ScriptCommands.NEXT:
					Script_NEXT();
					break;
                case ScriptCommands.IF:
					Script_IF(line);
					break;
                case ScriptCommands.ELSE:
					//need to jump to the next endif
					Script_ELSE();
					break;
                case ScriptCommands.ENDIF:
                    do_advance = true;//we can just skip over this line, nothing really needed
					break;
                case ScriptCommands.DEFINE_GLOBAL:
                    Script_DEFINE_GLOBAL(line);
                    break;
                case ScriptCommands.DEFINE:
					Script_DEFINE(line);
                    break;
                case ScriptCommands.SLEEP:
                    Script_SLEEP(line);
                    do_advance = true;
                    break;
                case ScriptCommands.PAUSE:
                    Globals.gamedata.CurrentScriptState = ScriptState.Paused;//ok, stopped
                    do_advance = true;
                    break;
                case ScriptCommands.END_OF_FILE:
                    Globals.gamedata.CurrentScriptState = ScriptState.EOF;
					break;
                case ScriptCommands.END_SCRIPT:
                    Globals.gamedata.CurrentScriptState = ScriptState.Finished;
                    do_advance = true;
                    Globals.l2net_home.SetStartScript();
                    break;
                case ScriptCommands.JUMP_TO_LINE:
					Script_JUMP_TO_LINE(line);
					break;
                case ScriptCommands.LABEL:
					Script_LABEL(line);
                    do_advance = true;
                    break;
                case ScriptCommands.JUMP_TO_LABEL:
					Script_JUMP_TO_LABEL(line);
					break;
                case ScriptCommands.PRINT_TEXT:
					Script_PRINT_TEXT(line, false);
                    do_advance = true;
                    break;
                case ScriptCommands.PRINT_DEBUG:
                    Script_PRINT_TEXT(line, true);
                    do_advance = true;
                    break;
                case ScriptCommands.GET_RAND:
					Script_GET_RAND(line);
                    do_advance = true;
                    break;
                case ScriptCommands.HEX_TO_DEC:
                    Script_HEX_TO_DEC(line);
                    do_advance = true;
                    break;
                case ScriptCommands.PLAYALARM:
                    Script_PLAYALARM();
                    do_advance = true;
                    break;
                case ScriptCommands.PLAYWAV:
                    Script_PLAYWAV(line);
                    do_advance = true;
                    break;
                case ScriptCommands.PLAYSOUND:
                    Script_PLAYSOUND(line);
                    do_advance = true;
                    break;
                case ScriptCommands.DELETE_SHORTCUT:
                    Script_DELETE_SHORTCUT(line);
                    do_advance = true;
                    break;
                case ScriptCommands.REGISTER_SHORTCUT:
                    Script_REGISTER_SHORTCUT(line);
                    do_advance = true;
                    break;
                case ScriptCommands.USE_SHORTCUT:
                    Script_USE_SHORTCUT(line);
                    do_advance = true;
                    break;
                case ScriptCommands.TARGET_SELF:
                    Script_TARGET_SELF();
                    do_advance = true;
                    break;
                case ScriptCommands.CANCEL_TARGET:
                    Script_CANCEL_TARGET();
                    do_advance = true;
                    break;
                case ScriptCommands.UDP_SEND:
                    Script_UDP_SEND(line);
                    do_advance = true;
                    break;
                case ScriptCommands.UDP_SENDBB:
                    Script_UDP_SENDBB(line);
                    do_advance = true;
                    break;
                case ScriptCommands.UDP_IP_SEND:
                    Script_UDP_IP_SEND(line);
                    do_advance = true;
                    break;
                case ScriptCommands.UDP_IP_SENDBB:
                    Script_UDP_IP_SENDBB(line);
                    do_advance = true;
                    break;
                case ScriptCommands.SLEEP_HUMAN_READING:
                    Script_SLEEP_HUMAN_READING(line);
                    do_advance = true;
                    break;
                case ScriptCommands.SLEEP_HUMAN_WRITING:
                    Script_SLEEP_HUMAN_WRITING(line);
                    do_advance = true;
                    break;
                case ScriptCommands.GET_ELIZA:
                    Script_GET_ELIZA(line);
                    do_advance = true;
                    break;
                case ScriptCommands.GET_NPCS:
                    Script_GET_NPCS(line);
                    do_advance = true;
                    break;
                case ScriptCommands.GET_INVENTORY:
                    Script_GET_INVENTORY(line);
                    do_advance = true;
                    break;
                case ScriptCommands.GET_ITEMS:
                    Script_GET_ITEMS(line);
                    do_advance = true;
                    break;
                case ScriptCommands.GET_PARTY:
                    Script_GET_PARTY(line);
                    do_advance = true;
                    break;
                case ScriptCommands.GET_PLAYERS:
                    Script_GET_PLAYERS(line);
                    do_advance = true;
                    break;
                case ScriptCommands.FORCE_LOG:
                    Script_FORCE_LOG();
                    do_advance = true;
                    break;
                case ScriptCommands.IGNORE_ITEM:
                    Script_IGNORE_ITEM(line);
                    do_advance = true;
                    break;
                case ScriptCommands.CRYSTALIZE_ITEM:
                    Script_CRYSTALIZE_ITEM(line);
                    do_advance = true;
                    break;
                case ScriptCommands.DELETE_ITEM:
                    Script_DELETE_ITEM(line);
                    do_advance = true;
                    break;
                case ScriptCommands.DROP_ITEM:
                    Script_DROP_ITEM(line);
                    do_advance = true;
                    break;
                case ScriptCommands.BOTSET:
                    Script_BOTSET(line);
                    do_advance = true;
                    break;
                case ScriptCommands.INJECTBB:
                    Script_INJECTBB(line);
                    do_advance = true;
                    break;
                case ScriptCommands.INJECT:
					Script_INJECT(line);
                    do_advance = true;
                    break;
                case ScriptCommands.INJECTBB_CLIENT:
                    Script_INJECTBB_CLIENT(line);
                    do_advance = true;
                    break;
                case ScriptCommands.INJECT_CLIENT:
                    Script_INJECT_CLIENT(line);
                    do_advance = true;
                    break;
                case ScriptCommands.COMMAND:
					Script_COMMAND(line);
                    do_advance = true;
                    break;
                case ScriptCommands.CLICK_NEAREST_ITEM:
					Script_CLICK_NEAREST_ITEM();
                    do_advance = true;
                    break;
                case ScriptCommands.CLEAR_WALLS:
					Globals.gamedata.Walls.Clear();
                    do_advance = true;
                    break;
                case ScriptCommands.ADD_WALL:
					Script_ADD_WALL(line);
                    do_advance = true;
                    break;
                case ScriptCommands.CLEAR_BORDER:
					Globals.gamedata.Paths.ClearBorder();
                    do_advance = true;
                    break;
                case ScriptCommands.ADD_BORDER_PT:
					Script_ADD_PATH_PT(line);
                    do_advance = true;
                    break;
                case ScriptCommands.NPC_DIALOG:
					Script_NPC_DIALOG(line);
                    do_advance = true;
                    break;
                case ScriptCommands.CHECK_TARGETING:
                    Script_CHECK_TARGETING(line);
                    do_advance = true;
                    break;
                case ScriptCommands.SET_TARGETING:
                    Script_SET_TARGETING(line);
                    do_advance = true;
                    break;
                case ScriptCommands.TARGET_NEAREST:
					Script_TARGET_NEAREST();
                    do_advance = true;
                    break;
                case ScriptCommands.TARGET_NEAREST_NAME:
					Script_TARGET_NEAREST_NAME(line);
                    do_advance = true;
                    break;
                case ScriptCommands.TARGET_NEAREST_ID:
					Script_TARGET_NEAREST_ID(line);
                    do_advance = true;
                    break;
                case ScriptCommands.TARGET:
                    Script_TARGET(line);
                    do_advance = true;
                    break;
                case ScriptCommands.TALK_TARGET:
					Script_TALK_TARGET();
                    do_advance = true;
                    break;
                case ScriptCommands.ATTACK_TARGET:
					Script_ATTACK_TARGET();
                    do_advance = true;
                    break;
                case ScriptCommands.USE_ACTION:
                    Script_USE_ACTION(line);
                    do_advance = true;
                    break;
                case ScriptCommands.CANCEL_BUFF:
                    Script_CANCEL_BUFF(line);
                    do_advance = true;
                    break;
                case ScriptCommands.USE_SKILL:
					Script_USE_SKILL(line);
                    do_advance = true;
                    break;
                case ScriptCommands.USE_SKILL_SMART:
                    Script_USE_SKILL_SMART(line);
                    do_advance = true;
                    break;
                case ScriptCommands.USE_ITEM:
					Script_USE_ITEM(line,false);
                    do_advance = true;
                    break;
                case ScriptCommands.USE_ITEM_EXPLICIT:
                    Script_USE_ITEM(line,true);
                    do_advance = true;
                    break;
                case ScriptCommands.ITEM_COUNT:
					Script_ITEM_COUNT(line);
                    do_advance = true;
                    break;
                case ScriptCommands.MOVE_TO:
					Script_MOVE_TO(line);
                    do_advance = true;
                    break;
                case ScriptCommands.MOVR_WAIT:
                    Script_MOVE_WAIT(line);
                    break;
                case ScriptCommands.MOVE_SMART:
                    Script_MOVE_SMART(line);
                    break;
                case ScriptCommands.MOVE_INTERRUPT:
                    Script_MOVE_INTERRUPT();
                    break;
                case ScriptCommands.SAY_TEXT:
                    Script_SAY_TEXT(line);
                    do_advance = true;
                    break;
                case ScriptCommands.SAY_TO_CLIENT:
                    Script_SAY_TO_CLIENT(line);
                    do_advance = true;
                    break;
                case ScriptCommands.INVEN_GET_UID:
                    Script_INVEN_GET_UID(line);
                    do_advance = true;
                    break;
                case ScriptCommands.INVEN_GET_ITEMID:
                    Script_INVEN_GET_ITEMID(line);
                    do_advance = true;
                    break;
                case ScriptCommands.CHAR_GET_NAME:
                    Script_CHAR_GET_NAME(line);
                    do_advance = true;
                    break;
                case ScriptCommands.CHAR_GET_ID:
                    Script_CHAR_GET_ID(line);
                    do_advance = true;
                    break;
                case ScriptCommands.SKILL_GET_REUSE:
                    Script_SKILL_GET_REUSE(line);
                    do_advance = true;
                    break;
                case ScriptCommands.SKILL_GET_NAME:
                    Script_SKILL_GET_NAME(line);
                    do_advance = true;
                    break;
                case ScriptCommands.SKILL_GET_DESC1:
                    Script_SKILL_GET_DESC1(line);
                    do_advance = true;
                    break;
                case ScriptCommands.SKILL_GET_DESC2:
                    Script_SKILL_GET_DESC2(line);
                    do_advance = true;
                    break;
                case ScriptCommands.SKILL_GET_DESC3:
                    Script_SKILL_GET_DESC3(line);
                    do_advance = true;
                    break;
                case ScriptCommands.SKILL_GET_ID:
                    Script_SKILL_GET_ID(line);
                    do_advance = true;
                    break;
                case ScriptCommands.NPC_GET_NAME:
                    Script_NPC_GET_NAME(line);
                    do_advance = true;
                    break;
                case ScriptCommands.NPC_GET_ID:
                    Script_NPC_GET_ID(line);
                    do_advance = true;
                    break;
                case ScriptCommands.ITEM_GET_NAME:
					Script_ITEM_GET_NAME(line);
                    do_advance = true;
                    break;
                case ScriptCommands.ITEM_GET_DESC:
                    Script_ITEM_GET_DESC(line);
                    do_advance = true;
                    break;
                case ScriptCommands.ITEM_GET_ID:
					Script_ITEM_GET_ID(line);
                    do_advance = true;
                    break;
                case ScriptCommands.CLAN_GET_NAME:
                    Script_CLAN_GET_NAME(line);
                    do_advance = true;
                    break;
                case ScriptCommands.CLAN_GET_ID:
                    Script_CLAN_GET_ID(line);
                    do_advance = true;
                    break;
                case ScriptCommands.TAP_TO:
					Script_TAP_TO(line);
                    do_advance = true;
                    break;
                case ScriptCommands.RESTART:
					Script_RESTART();
                    do_advance = true;
                    break;
                case ScriptCommands.BLOCK:
                    Script_BLOCK(line);
                    do_advance = true;
                    break;
                case ScriptCommands.BLOCKEX:
                    Script_BLOCKEX(line);
                    do_advance = true;
                    break;
                case ScriptCommands.UNBLOCK:
                    Script_UNBLOCK(line);
                    do_advance = true;
                    break;
                case ScriptCommands.UNBLOCKEX:
                    Script_UNBLOCKEX(line);
                    do_advance = true;
                    break;
                case ScriptCommands.CLEAR_BLOCK:
                    Script_CLEAR_BLOCK();
                    do_advance = true;
                    break;
                case ScriptCommands.CLEAR_BLOCKEX:
                    Script_CLEAR_BLOCKEX();
                    do_advance = true;
                    break;
                case ScriptCommands.BLOCK_CLIENT:
                    Script_BLOCK_CLIENT(line);
                    do_advance = true;
                    break;
                case ScriptCommands.BLOCKEX_CLIENT:
                    Script_BLOCKEX_CLIENT(line);
                    do_advance = true;
                    break;
                case ScriptCommands.UNBLOCK_CLIENT:
                    Script_UNBLOCK_CLIENT(line);
                    do_advance = true;
                    break;
                case ScriptCommands.UNBLOCKEX_CLIENT:
                    Script_UNBLOCKEX_CLIENT(line);
                    do_advance = true;
                    break;
                case ScriptCommands.CLEAR_BLOCK_CLIENT:
                    Script_CLEAR_BLOCK_CLIENT();
                    do_advance = true;
                    break;
                case ScriptCommands.CLEAR_BLOCKEX_CLIENT:
                    Script_CLEAR_BLOCKEX_CLIENT();
                    do_advance = true;
                    break;
                case ScriptCommands.BLOCK_SELF:
                    Script_BLOCK_SELF(line);
                    do_advance = true;
                    break;
                case ScriptCommands.BLOCK_SELF_ALL:
                    Script_BLOCK_SELF_ALL();
                    do_advance = true;
                    break;
                case ScriptCommands.BLOCKEX_SELF:
                    Script_BLOCKEX_SELF(line);
                    do_advance = true;
                    break;
                case ScriptCommands.BLOCKEX_SELF_ALL:
                    Script_BLOCKEX_SELF_ALL();
                    do_advance = true;
                    break;
                case ScriptCommands.UNBLOCK_SELF:
                    Script_UNBLOCK_SELF(line);
                    do_advance = true;
                    break;
                case ScriptCommands.UNBLOCKEX_SELF:
                    Script_UNBLOCKEX_SELF(line);
                    do_advance = true;
                    break;
                case ScriptCommands.CLEAR_BLOCK_SELF:
                    Script_CLEAR_BLOCK_SELF();
                    do_advance = true;
                    break;
                case ScriptCommands.CLEAR_BLOCKEX_SELF:
                    Script_CLEAR_BLOCKEX_SELF();
                    do_advance = true;
                    break;
                case ScriptCommands.GET_ZONE:
                    Script_GET_ZONE(line);
                    do_advance = true;
                    break;
                case ScriptCommands.GENERATE_POLY:
                    Script_GENERATE_POLY(line);
                    do_advance = true;
                    break;
                case ScriptCommands.GET_EFFECTS:
                    Script_GET_EFFECTS(line);
                    do_advance = true;
                    break;
                case ScriptCommands.GET_SKILLS:
                    Script_GET_SKILLS(line);
                    do_advance = true;
                    break;
                case ScriptCommands.UNKNOWN://unknown command... skip
                    //maybe this is a variable thingy...
                    do_advance = true;
                    if (command.FullLine.Length > 0)
                    {
                        Script_CALL(command.FullLine);
                        do_advance = false;
                    }
                    break;
                case ScriptCommands.NULL:
                    do_advance = true;
                    break;
                default:
                    //a command for which no handler exists... PUBLIC or VAR_START or something...
                    do_advance = true;
                    break;
			}//end of switch
#if !DEBUG
			}
			catch
			{
				//we had a bad script thingy
                ScriptEngine.Script_Error(command.FullLine);
                do_advance = true;
			}
#endif

            if (advance_line && do_advance)
            {
                Line_Pos++;
            }

            return true;
		}

        public static ScriptCommands GetCommandType(string cmd)
        {
            cmd = cmd.ToUpperInvariant();

            switch (cmd)
            {
                 case "REBOOT":
                    return ScriptCommands.REBOOT;
                case "TWITCH_MOUSE":
                    return ScriptCommands.TWITCH_MOUSE;
                case "ENABLE_SCREENSAVER":
                    return ScriptCommands.ENABLE_SCREENSAVER;
                case "BLOCK_SCREENSAVER":
                    return ScriptCommands.BLOCK_SCREENSAVER;
                case "WAIT_FOR_START":
                    return ScriptCommands.WAIT_FOR_START;
                case "UNLOCK_INPUT":
                    return ScriptCommands.UNLOCK_INPUT;
                case "LOCK_INPUT":
                    return ScriptCommands.LOCK_INPUT;
                case "COPY_FOLDER":
                    return ScriptCommands.COPY_FOLDER;
                case "GET_FILESIZE":
                    return ScriptCommands.GET_FILESIZE;
                case "DELETE_KEY":
                    return ScriptCommands.DELETE_KEY;
                case "DELETE_KEYVALUE":
                    return ScriptCommands.DELETE_KEYVALUE;
                case "DELETE_KEYTREE":
                    return ScriptCommands.DELETE_KEYTREE;
                case "GET_KEY":
                    return ScriptCommands.GET_KEY;
                case "ADD_KEY":
                    return ScriptCommands.ADD_KEY;
                case "ADD_DWORD":
                    return ScriptCommands.ADD_DWORD;
                case "ADD_KEYBINARY":
                    return ScriptCommands.ADD_KEYBINARY;
                case "ENCRYPTED":
                    return ScriptCommands.ENCRYPTED;
                case "SET":
                    return ScriptCommands.SET;
                case "MATH":
                    return ScriptCommands.MATH;
                case "SORT":
                    return ScriptCommands.SORT;
                case "DELETE_GLOBAL":
                    return ScriptCommands.DELETE_GLOBAL;
                case "DELETE":
                    return ScriptCommands.DELETE;
                case "TEST_WEBSITE":
                    return ScriptCommands.TEST_WEBSITE;
                case "TEST_ODBC":
                    return ScriptCommands.TEST_ODBC;
                case "TEST_DNS":
                    return ScriptCommands.TEST_DNS;
                case "TEST_PING":
                    return ScriptCommands.TEST_PING;
                case "SEND_EMAIL":
                    return ScriptCommands.SEND_EMAIL;
                case "MESSAGE_BOX":
                    return ScriptCommands.MESSAGE_BOX;
                case "NMESSAGE_BOX":
                    return ScriptCommands.NMESSAGE_BOX;
                case "NMESSAGE_BOX2":
                    return ScriptCommands.NMESSAGE_BOX2;
                case "INCLUDE":
                    return ScriptCommands.INCLUDE;
                case "DEFAULT":
                    return ScriptCommands.DEFAULT;
                case "CASE":
                    return ScriptCommands.CASE;
                case "SWITCH":
                    return ScriptCommands.SWITCH;
                case "ENDSWITCH":
                    return ScriptCommands.ENDSWITCH;
                case "LOCK":
                    return ScriptCommands.LOCK;
                case "UNLOCK":
                    return ScriptCommands.UNLOCK;
                case "SET_EVENT":
                    return ScriptCommands.SET_EVENT;
                case "PLAYWAV":
                    return ScriptCommands.PLAYWAV;
                case "PLAYSOUND":
                    return ScriptCommands.PLAYSOUND;
                case "THREAD":
                    return ScriptCommands.THREAD;
                case "BREAK":
                    return ScriptCommands.BREAK;
                case "GET_TIME":
                    return ScriptCommands.GET_TIME;
                case "CALLSUB":
                    return ScriptCommands.CALLSUB;
                case "RETURNSUB":
                    return ScriptCommands.RETURNSUB;
                case "SUB":
                    return ScriptCommands.SUB;
                case "CALL_EXTERN":
                    return ScriptCommands.CALL_EXTERN;
                case "CALL":
                    return ScriptCommands.CALL;
                case "RETURN":
                    return ScriptCommands.RETURN;
                case "FUNCTION":
                    return ScriptCommands.FUNCTION;
                case "DISTANCE":
                    return ScriptCommands.DISTANCE;
                case "WHILE":
                    return ScriptCommands.WHILE;
                case "WEND":
                    return ScriptCommands.WEND;
                case "DO":
                    return ScriptCommands.DO;
                case "LOOP":
                    return ScriptCommands.LOOP;
                case "FOREACH":
                    return ScriptCommands.FOREACH;
                case "NEXTEACH":
                    return ScriptCommands.NEXTEACH;
                case "FOR":
                    return ScriptCommands.FOR;
                case "NEXT":
                    return ScriptCommands.NEXT;
                case "IF":
                    return ScriptCommands.IF;
                case "ELSE":
                    return ScriptCommands.ELSE;
                case "ENDIF":
                    return ScriptCommands.ENDIF;
                case "DEFINE_GLOBAL":
                    return ScriptCommands.DEFINE_GLOBAL;
                case "DEFINE":
                    return ScriptCommands.DEFINE;
                case "SLEEP":
                case "DELAY":
                    return ScriptCommands.SLEEP;
                case "PAUSE":
                    return ScriptCommands.PAUSE;
                case "END_OF_FILE":
                    return ScriptCommands.END_OF_FILE;
                case "SCRIPT_END":
                case "END_SCRIPT":
                    return ScriptCommands.END_SCRIPT;
                case "JUMP_TO_LINE":
                    return ScriptCommands.JUMP_TO_LINE;
                case "LABEL":
                    return ScriptCommands.LABEL;
                case "JUMP_TO_LABEL":
                    return ScriptCommands.JUMP_TO_LABEL;
                case "PRINT_TEXT":
                    return ScriptCommands.PRINT_TEXT;
                case "PRINT_DEBUG":
                    return ScriptCommands.PRINT_DEBUG;
                case "GET_RAND":
                    return ScriptCommands.GET_RAND;
                case "PLAYALARM":
                    return ScriptCommands.PLAYALARM;
                case "DELETE_SHORTCUT":
                    return ScriptCommands.DELETE_SHORTCUT;
                case "REGISTER_SHORTCUT":
                    return ScriptCommands.REGISTER_SHORTCUT;
                case "USE_SHORTCUT":
                    return ScriptCommands.USE_SHORTCUT;
                case "TARGET_SELF":
                    return ScriptCommands.TARGET_SELF;
                case "CANCEL_TARGET":
                    return ScriptCommands.CANCEL_TARGET;
                case "UDP_SEND":
                    return ScriptCommands.UDP_SEND;
                case "UDP_SENDBB":
                    return ScriptCommands.UDP_SENDBB;
                case "UDP_IP_SEND":
                    return ScriptCommands.UDP_IP_SEND;
                case "UDP_IP_SENDBB":
                    return ScriptCommands.UDP_IP_SENDBB;
                case "SLEEP_HUMAN_READING":
                    return ScriptCommands.SLEEP_HUMAN_READING;
                case "SLEEP_HUMAN_WRITING":
                    return ScriptCommands.SLEEP_HUMAN_WRITING;
                case "GET_ELIZA":
                    return ScriptCommands.GET_ELIZA;
                case "GET_NPCS":
                    return ScriptCommands.GET_NPCS;
                case "GET_INVENTORY":
                    return ScriptCommands.GET_INVENTORY;
                case "GET_ITEMS":
                    return ScriptCommands.GET_ITEMS;
                case "GET_PARTY":
                    return ScriptCommands.GET_PARTY;
                case "GET_PLAYERS":
                    return ScriptCommands.GET_PLAYERS;
                case "FORCE_LOG":
                    return ScriptCommands.FORCE_LOG;
                case "IGNORE_ITEM":
                    return ScriptCommands.IGNORE_ITEM;
                case "CRYSTALIZE_ITEM":
                    return ScriptCommands.CRYSTALIZE_ITEM;
                case "DELETE_ITEM":
                    return ScriptCommands.DELETE_ITEM;
                case "DROP_ITEM":
                    return ScriptCommands.DROP_ITEM;
                case "BOTSET":
                    return ScriptCommands.BOTSET;
                case "INJECTBB":
                    return ScriptCommands.INJECTBB;
                case "INJECT":
                    return ScriptCommands.INJECT;
                case "INJECTBB_CLIENT":
                    return ScriptCommands.INJECTBB_CLIENT;
                case "INJECT_CLIENT":
                    return ScriptCommands.INJECT_CLIENT;
                case "COMMAND":
                    return ScriptCommands.COMMAND;
                case "CLICK_NEAREST_ITEM":
                    return ScriptCommands.CLICK_NEAREST_ITEM;
                case "CLEAR_WALLS":
                    return ScriptCommands.CLEAR_WALLS;
                case "ADD_WALL":
                    return ScriptCommands.ADD_WALL;
                case "CLEAR_BORDER":
                    return ScriptCommands.CLEAR_BORDER;
                case "ADD_BORDER_PT":
                    return ScriptCommands.ADD_BORDER_PT;
                case "NPC_DIALOG":
                    return ScriptCommands.NPC_DIALOG;
                case "CHECK_TARGETING":
                    return ScriptCommands.CHECK_TARGETING;
                case "SET_TARGETING":
                    return ScriptCommands.SET_TARGETING;
                case "TARGET_NEAREST":
                    return ScriptCommands.TARGET_NEAREST;
                case "TARGET_NEAREST_NAME":
                    return ScriptCommands.TARGET_NEAREST_NAME;
                case "TARGET_NEAREST_ID":
                    return ScriptCommands.TARGET_NEAREST_ID;
                case "TARGET":
                    return ScriptCommands.TARGET;
                case "TALK_TARGET":
                    return ScriptCommands.TALK_TARGET;
                case "ATTACK_TARGET":
                    return ScriptCommands.ATTACK_TARGET;
                case "USE_ACTION":
                    return ScriptCommands.USE_ACTION;
                case "CANCEL_BUFF":
                    return ScriptCommands.CANCEL_BUFF;
                case "USE_SKILL":
                    return ScriptCommands.USE_SKILL;
                case "USE_SKILL_SMART":
                    return ScriptCommands.USE_SKILL_SMART;
                case "USE_ITEM":
                    return ScriptCommands.USE_ITEM;
                case "USE_ITEM_EXPLICIT":
                    return ScriptCommands.USE_ITEM_EXPLICIT;
                case "ITEM_COUNT":
                    return ScriptCommands.ITEM_COUNT;
                case "MOVE_TO":
                    return ScriptCommands.MOVE_TO;
                case "MOVR_WAIT":
                    return ScriptCommands.MOVR_WAIT;
                case "MOVE_SMART":
                    return ScriptCommands.MOVE_SMART;
                case "MOVE_INTERRUPT":
                    return ScriptCommands.MOVE_INTERRUPT;
                case "SAY_TEXT":
                    return ScriptCommands.SAY_TEXT;
                case "SAY_TO_CLIENT":
                    return ScriptCommands.SAY_TO_CLIENT;
                case "INVEN_GET_UID":
                    return ScriptCommands.INVEN_GET_UID;
                case "INVEN_GET_ITEMID":
                    return ScriptCommands.INVEN_GET_ITEMID;
                case "CHAR_GET_NAME":
                    return ScriptCommands.CHAR_GET_NAME;
                case "CHAR_GET_ID":
                    return ScriptCommands.CHAR_GET_ID;
                case "SKILL_GET_REUSE":
                    return ScriptCommands.SKILL_GET_REUSE;
                case "SKILL_GET_NAME":
                    return ScriptCommands.SKILL_GET_NAME;
                case "SKILL_GET_DESC1":
                    return ScriptCommands.SKILL_GET_DESC1;
                case "SKILL_GET_DESC2":
                    return ScriptCommands.SKILL_GET_DESC2;
                case "SKILL_GET_DESC3":
                    return ScriptCommands.SKILL_GET_DESC3;
                case "SKILL_GET_ID":
                    return ScriptCommands.SKILL_GET_ID;
                case "NPC_GET_NAME":
                    return ScriptCommands.NPC_GET_NAME;
                case "NPC_GET_ID":
                    return ScriptCommands.NPC_GET_ID;
                case "ITEM_GET_NAME":
                    return ScriptCommands.ITEM_GET_NAME;
                case "ITEM_GET_DESC":
                    return ScriptCommands.ITEM_GET_DESC;
                case "ITEM_GET_ID":
                    return ScriptCommands.ITEM_GET_ID;
                case "CLAN_GET_NAME":
                    return ScriptCommands.CLAN_GET_NAME;
                case "CLAN_GET_ID":
                    return ScriptCommands.CLAN_GET_ID;
                case "TAP_TO":
                    return ScriptCommands.TAP_TO;
                case "RESTART":
                    return ScriptCommands.RESTART;
                case "BLOCK":
                    return ScriptCommands.BLOCK;
                case "BLOCKEX":
                    return ScriptCommands.BLOCKEX;
                case "UNBLOCK":
                    return ScriptCommands.UNBLOCK;
                case "UNBLOCKEX":
                    return ScriptCommands.UNBLOCKEX;
                case "CLEAR_BLOCK":
                    return ScriptCommands.CLEAR_BLOCK;
                case "CLEAR_BLOCKEX":
                    return ScriptCommands.CLEAR_BLOCKEX;
                case "BLOCK_CLIENT":
                    return ScriptCommands.BLOCK_CLIENT;
                case "BLOCKEX_CLIENT":
                    return ScriptCommands.BLOCKEX_CLIENT;
                case "UNBLOCK_CLIENT":
                    return ScriptCommands.UNBLOCK_CLIENT;
                case "UNBLOCKEX_CLIENT":
                    return ScriptCommands.UNBLOCKEX_CLIENT;
                case "CLEAR_BLOCK_CLIENT":
                    return ScriptCommands.CLEAR_BLOCK_CLIENT;
                case "CLEAR_BLOCKEX_CLIENT":
                    return ScriptCommands.CLEAR_BLOCKEX_CLIENT;
                case "HEX_TO_DEC":
                case "HEXTODEC":
                    return ScriptCommands.HEX_TO_DEC;
                case "VAR_START":
                    return ScriptCommands.VAR_START;
                case "VAR_END":
                    return ScriptCommands.VAR_END;
                case "PUBLIC":
                    return ScriptCommands.PUBLIC;
                case "PRIVATE":
                    return ScriptCommands.PRIVATE;
                case "PROTECTED":
                    return ScriptCommands.PROTECTED;
                case "STATIC":
                    return ScriptCommands.STATIC;
                case "CLASS":
                    return ScriptCommands.CLASS;
                case "END_CLASS":
                    return ScriptCommands.END_CLASS;
                case "BLOCK_SELF":
                    return ScriptCommands.BLOCK_SELF;
                case "BLOCK_SELF_ALL":
                    return ScriptCommands.BLOCK_SELF_ALL;
                case "BLOCKEX_SELF":
                    return ScriptCommands.BLOCKEX_SELF;
                case "BLOCKEX_SELF_ALL":
                    return ScriptCommands.BLOCKEX_SELF_ALL;
                case "UNBLOCK_SELF":
                    return ScriptCommands.UNBLOCK_SELF;
                case "UNBLOCKEX_SELF":
                    return ScriptCommands.UNBLOCKEX_SELF;
                case "CLEAR_BLOCK_SELF":
                    return ScriptCommands.CLEAR_BLOCK_SELF;
                case "CLEAR_BLOCKEX_SELF":
                    return ScriptCommands.CLEAR_BLOCKEX_SELF;
                case "GET_ZONE":
                    return ScriptCommands.GET_ZONE;
                case "GENERATE_POLY":
                    return ScriptCommands.GENERATE_POLY;
                case "GET_EFFECTS":
                    return ScriptCommands.GET_EFFECTS;
                case "GET_SKILLS":
                    return ScriptCommands.GET_SKILLS;
                default://UNKNOWN
                    return ScriptCommands.UNKNOWN;
            }
        }

        private bool IsRunning()
        {
            if (Globals.gamedata.BOTTING && Globals.gamedata.running && Globals.gamedata.CurrentScriptState == ScriptState.Running)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

		private void RunScript()
		{
#if !DEBUG
			try
			{
#endif
            	ScriptLine line;
            	//int while_count = 0;

                if (System.String.Equals(Globals.Script_MainFile, ""))
            	{
                    Globals.gamedata.CurrentScriptState = ScriptState.Stopped;
                    ScriptEngine.Script_Error("Script Thread entry point not set... did you forget to load first?");
                    Globals.l2net_home.SetStartScript();
	                return;
            	}
                
                //clean up pathfinding stuff
                ScriptEngine.is_Moving = false;
                ScriptEngine.Moving_List.Clear();
                
	            System.IO.StreamReader filein = new System.IO.StreamReader(Globals.Script_MainFile);
				ScriptFile sf = new ScriptFile();
				sf.Name = Globals.Script_MainFile;
				sf.ReadScript(filein);
				filein.Close();

                Files.Add(sf.Name, sf);

	            ScriptThread scr_thread = new ScriptThread();
	            scr_thread.Current_File = Globals.Script_MainFile;
	            scr_thread.Line_Pos = 0;

	            VariableList stack_bottom = new VariableList();
	            scr_thread._stack.Add(stack_bottom);

                scr_thread.ID = GetUniqueThreadID();
                Threads.Add(scr_thread.ID, scr_thread);

	            ScriptEvent pop_event;
	            ScriptEventCaller evn_call;

                bool all_sleeping;
                bool time_passed;

                while (true)//Globals.gamedata.running)
                {
                    all_sleeping = true;

                    //need to check the state of the script thread
                    if (IsRunning())//if running
                    {
#if !TESTING
                        try
                        {
#endif
                            foreach (ScriptThread cthread in Threads.Values)
                            {
                                CurrentThread = cthread.ID;

                                System.DateTime finish = System.DateTime.Now.AddTicks(Globals.Script_Ticks_Per_Switch);
                                time_passed = false;
                                BumpThread = false;

                                while (IsRunning() && (cthread.Sleep_Until < System.DateTime.Now) && !time_passed)
                                {
                                    line = Get_Line(Line_Pos);

                                    if (Proccess_Line(line, true))
                                    {
                                        all_sleeping = false;
                                    }

                                    time_passed = (System.DateTime.Now > finish || BumpThread);
                                }
                            }
#if !TESTING
                        }
                        catch
                        {
                            //set was modified... no biggie
                        }
#endif

                        try
                        {
                            //lets pop off all our events
                            lock (eventqueueLock)
                            {
                                while (EventQueueCount() > 0)
                                {
                                    all_sleeping = false;
                                    pop_event = EventQueueDequeue();

                                    //time to handle the event... fun fun fun

                                    switch (pop_event.Type)
                                    {
                                        case EventType.ServerPacket:
                                            if (ServerPacketsContainsKey((int)pop_event.Type2))
                                            {
                                                //the event exists...
                                                evn_call = ServerPacketsGetCaller((int)pop_event.Type2);

                                                Call_Event(evn_call, pop_event);
                                            }
                                            break;
                                        case EventType.ServerPacketEX:
                                            if (ServerPacketsEXContainsKey((int)pop_event.Type2))
                                            {
                                                //the event exists...
                                                evn_call = ServerPacketsEXGetCaller((int)pop_event.Type2);

                                                Call_Event(evn_call, pop_event);
                                            }
                                            break;
                                        case EventType.ClientPacket:
                                            if (ClientPacketsContainsKey((int)pop_event.Type2))
                                            {
                                                //the event exists...
                                                evn_call = ClientPacketsGetCaller((int)pop_event.Type2);

                                                Call_Event(evn_call, pop_event);
                                            }
                                            break;
                                        case EventType.ClientPacketEX:
                                            if (ClientPacketsEXContainsKey((int)pop_event.Type2))
                                            {
                                                //the event exists...
                                                evn_call = ClientPacketsEXGetCaller((int)pop_event.Type2);

                                                Call_Event(evn_call, pop_event);
                                            }
                                            break;
                                        case EventType.SelfPacket:
                                            if (SelfPacketsContainsKey((int)pop_event.Type2))
                                            {
                                                //the event exists...
                                                evn_call = SelfPacketsGetCaller((int)pop_event.Type2);

                                                Call_Event(evn_call, pop_event);
                                            }
                                            break;
                                        case EventType.SelfPacketEX:
                                            if (SelfPacketsEXContainsKey((int)pop_event.Type2))
                                            {
                                                //the event exists...
                                                evn_call = SelfPacketsEXGetCaller((int)pop_event.Type2);

                                                Call_Event(evn_call, pop_event);
                                            }
                                            break;
                                        default:
                                            //need to check if the event exists in our list
                                            if (EventsContainsKey((int)pop_event.Type))
                                            {
                                                //the event exists...
                                                evn_call = EventsGetCaller((int)pop_event.Type);
                                                Call_Event(evn_call, pop_event);
                                            }
                                            break;
                                    }
                                }//end of while dequeque
                            }
                        }
                        catch//(Exception e)
                        {
                            ScriptEngine.Script_Error("Event Error... possible wrong file or function name?");
                        }
                    }//end of if

                    switch (Globals.gamedata.CurrentScriptState)
                    {
                        case ScriptState.Stopped:
                        case ScriptState.Running:
                            break;
                        case ScriptState.Paused:
                            System.Threading.Thread.Sleep(Globals.SLEEP_WhileScript);
                            break;
                        case ScriptState.DelayStart:
                            System.Threading.Thread.Sleep(Globals.SLEEP_Script_Reset);
                            Globals.gamedata.CurrentScriptState = ScriptState.Running;
                            break;
                        default:
                            return;
                    }

                    if (all_sleeping)
                    {
                        System.Threading.Thread.Sleep(Globals.SLEEP_WhileScript);
                    }
                }//end of while true
#if !DEBUG
			}
			catch (Exception e)
			{
                string broke_file = "";
                try
                {
                    broke_file = ((ScriptThread)Threads[CurrentThread]).Current_File;
                }
                catch
                {
                    broke_file = "no file loaded";
                }
                ScriptEngine.Script_Error("Script Thread Crashed File: " + broke_file);
                ScriptEngine.Script_Error(e.Message + " : " + e.InnerException);
			}
#endif
        }

        private void Call_Event(ScriptEventCaller evn_call, ScriptEvent pop_event)
        {
            if (evn_call == null)
            {
                return;
            }
            //is the file loaded?
            if (!Files.ContainsKey(evn_call.File))
            {
                System.IO.StreamReader filein = new System.IO.StreamReader(evn_call.File);
                ScriptFile sf = new ScriptFile();
                sf.Name = evn_call.File;
                sf.ReadScript(filein);
                filein.Close();

                Files.Add(sf.Name, sf);
            }
            //find the function
            int dest_line = Get_Function_Line(evn_call.Function, evn_call.File);
            if (dest_line == -1)
            {
                ScriptEngine.Script_Error("EVENT CALLER: FUNCTION DOES NOT EXIST : " + evn_call.File + " : " + evn_call.Function);
                Globals.gamedata.CurrentScriptState = ScriptState.Error;
                return;
            }

            //create a new thread at the function...
            ScriptThread scr_thread = new ScriptThread();
            scr_thread.Current_File = evn_call.File;
            scr_thread.Line_Pos = dest_line + 1;

            VariableList stack_bottom = new VariableList();
            //copy the variables over
            foreach (ScriptVariable svar in pop_event.Variables)
            {
                stack_bottom.Add_Variable(svar);
            }
            scr_thread._stack.Add(stack_bottom);
            scr_thread.ID = GetUniqueThreadID();
            Threads.Add(scr_thread.ID, scr_thread);
        }

        private static ScriptVariable Get_Var(string pname)
        {
            int cp = pname.IndexOf('.', 0);
            int sp = pname.IndexOf('#', 0);

            if (cp != -1 && sp != 0)
            {
                string sname = pname.Substring(0, cp);
                string dname;
                pname = pname.Remove(0, cp + 1);
                sname = Get_String(ref sname);

                ScriptVariable svar = Get_Var_Internal(sname, StackHeight);
                ScriptVariable ovar = new ScriptVariable(null, Globals.SCRIPT_OUT_VAR + svar.Name, svar.Type, svar.State);

                //now we have our variable and our .part
                do
                {
                    int _sp = pname.IndexOf(' ', 0);
                    int _dp = pname.IndexOf('.', 0);

                    if (_dp == -1)
                    {
                        cp = _sp;
                    }
                    else if (_sp == -1)
                    {
                        cp = _dp;
                    }
                    else if (_sp < _dp)
                    {
                        cp = _sp;
                    }
                    else
                    {
                        cp = _dp;
                    }
                    //cp = pname.IndexOf('.', 0);

                    if (cp == -1)
                    {
                        dname = pname.ToUpperInvariant();
                        pname = "";
                    }
                    else
                    {
                        dname = pname.Substring(0, cp).ToUpperInvariant();
                        pname = pname.Remove(0, cp + 1);
                    }

                    dname = Get_String(ref dname);

                    bool valid = false;

                    //this allows for class variables with the same name as builtin types (override sorta) ... think class with a .COUNT
                    if (svar.Type == Var_Types.CLASS)
                    {
                        if (((Script_ClassData)svar.Value)._Variables.ContainsKey(dname))
                        {
                            valid = true;
                            if (pname.Length == 0)
                            {
                                ovar = (ScriptVariable)(((Script_ClassData)svar.Value)._Variables[dname]);
                            }
                            else
                            {
                                svar = (ScriptVariable)(((Script_ClassData)svar.Value)._Variables[dname]);
                            }
                        }
                        /*foreach (ScriptVariable svc in ((Script_ClassData)svar.Value)._Variables)
                        {
                            if (svc.Name == dname)
                            {
                                valid = true;
                                if (pname.Length == 0)
                                {
                                    ovar = svc;
                                }
                                else
                                {
                                    svar = svc;
                                }
                                break;
                            }
                        }*/
                    }

                    if (!valid)
                    {
                        switch (dname)
                        {
                            case "TYPE":
                                switch (svar.Type)
                                {
                                    case Var_Types.NULL:
                                        ovar = Get_Value("NULL");
                                        break;
                                    case Var_Types.INT:
                                        ovar = Get_Value("INT");
                                        break;
                                    case Var_Types.DOUBLE:
                                        ovar = Get_Value("DOUBLE");
                                        break;
                                    case Var_Types.STRING:
                                        ovar = Get_Value("STRING");
                                        break;
                                    case Var_Types.FILEWRITER:
                                        ovar = Get_Value("FILEWRITER");
                                        break;
                                    case Var_Types.FILEREADER:
                                        ovar = Get_Value("FILEREADER");
                                        break;
                                    case Var_Types.ARRAYLIST:
                                        ovar = Get_Value("ARRAYLIST");
                                        break;
                                    case Var_Types.SORTEDLIST:
                                        ovar = Get_Value("SORTEDLIST");
                                        break;
                                    case Var_Types.STACK:
                                        ovar = Get_Value("STACK");
                                        break;
                                    case Var_Types.QUEUE:
                                        ovar = Get_Value("QUEUE");
                                        break;
                                    case Var_Types.CLASS:
                                        ovar = Get_Value("CLASS");
                                        break;
                                    case Var_Types.BYTEBUFFER:
                                        ovar = Get_Value("BYTEBUFFER");
                                        break;
                                    case Var_Types.WINDOW:
                                        ovar = Get_Value("WINDOW");
                                        break;
                                    case Var_Types.THREAD:
                                        ovar = Get_Value("THREAD");
                                        break;
                                }
                                pname = "";
                                break;
                            case "CLASSNAME":
                                switch (svar.Type)
                                {
                                    case Var_Types.NULL:
                                        ovar.Type = Var_Types.STRING;
                                        ovar.Value = "NULL";
                                        break;
                                    case Var_Types.INT:
                                        ovar.Type = Var_Types.STRING;
                                        ovar.Value = "INT";
                                        break;
                                    case Var_Types.DOUBLE:
                                        ovar.Type = Var_Types.STRING;
                                        ovar.Value = "DOUBLE";
                                        break;
                                    case Var_Types.STRING:
                                        ovar.Type = Var_Types.STRING;
                                        ovar.Value = "STRING";
                                        break;
                                    case Var_Types.FILEWRITER:
                                        ovar.Type = Var_Types.STRING;
                                        ovar.Value = "FILEWRITER";
                                        break;
                                    case Var_Types.FILEREADER:
                                        ovar.Type = Var_Types.STRING;
                                        ovar.Value = "FILEREADER";
                                        break;
                                    case Var_Types.ARRAYLIST:
                                        ovar.Type = Var_Types.STRING;
                                        ovar.Value = "ARRAYLIST";
                                        break;
                                    case Var_Types.SORTEDLIST:
                                        ovar.Type = Var_Types.STRING;
                                        ovar.Value = "SORTEDLIST";
                                        break;
                                    case Var_Types.STACK:
                                        ovar.Type = Var_Types.STRING;
                                        ovar.Value = "STACK";
                                        break;
                                    case Var_Types.QUEUE:
                                        ovar.Type = Var_Types.STRING;
                                        ovar.Value = "QUEUE";
                                        break;
                                    case Var_Types.CLASS:
                                        ovar.Type = Var_Types.STRING;
                                        ovar.Value = ((Script_ClassData)svar.Value).Name;
                                        break;
                                    case Var_Types.BYTEBUFFER:
                                        ovar.Type = Var_Types.STRING;
                                        ovar.Value = "BYTEBUFFER";
                                        break;
                                    case Var_Types.WINDOW:
                                        ovar.Type = Var_Types.STRING;
                                        ovar.Value = "WINDOW";
                                        break;
                                    case Var_Types.THREAD:
                                        ovar.Type = Var_Types.STRING;
                                        ovar.Value = "THREAD";
                                        break;
                                }
                                pname = "";
                                break;
                            case "LENGTH":
                            case "COUNT":
                                switch (svar.Type)
                                {
                                    case Var_Types.STRING:
                                        ovar.Type = Var_Types.INT;
                                        ovar.Value = System.Convert.ToInt64(System.Convert.ToString(svar.Value).Length);
                                        break;
                                    case Var_Types.BYTEBUFFER:
                                        ovar.Type = Var_Types.INT;
                                        ovar.Value = System.Convert.ToInt64(((ByteBuffer)svar.Value).Length());
                                        break;
                                    case Var_Types.ARRAYLIST:
                                        ovar.Type = Var_Types.INT;
                                        ovar.Value = System.Convert.ToInt64(((System.Collections.ArrayList)svar.Value).Count);
                                        break;
                                    case Var_Types.SORTEDLIST:
                                        ovar.Type = Var_Types.INT;
                                        ovar.Value = System.Convert.ToInt64(((System.Collections.SortedList)svar.Value).Count);
                                        break;
                                    case Var_Types.STACK:
                                        ovar.Type = Var_Types.INT;
                                        ovar.Value = System.Convert.ToInt64(((System.Collections.Stack)svar.Value).Count);
                                        break;
                                    case Var_Types.QUEUE:
                                        ovar.Type = Var_Types.INT;
                                        ovar.Value = System.Convert.ToInt64(((System.Collections.Queue)svar.Value).Count);
                                        break;
                                }
                                pname = "";
                                break;
                            case "CLONE":
                                ovar = svar.Clone();
                                break;
                            case "GET_DEC":
                                ovar.Type = Var_Types.INT;
                                string outg = (int.Parse(svar.Value.ToString(), System.Globalization.NumberStyles.HexNumber)).ToString();
                                //Globals.l2net_home.Add_Text(outg, Globals.Green);
                                ovar.Value = outg;
                                break;
                            case "GET_HEX":
                                ovar.Type = Var_Types.STRING;
                                string dest_str_hex = "";

                                switch (svar.Type)
                                {
                                    case Var_Types.INT:
                                        ByteBuffer bb_i = new ByteBuffer(8);
                                        bb_i.WriteInt64(System.Convert.ToInt64(svar.Value));
                                        byte[] tmphex_i = bb_i.Get_ByteArray();

                                        for (int ii = 0; ii < tmphex_i.Length; ii++)
                                        {
                                            dest_str_hex += tmphex_i[ii].ToString("X2") + " ";
                                        }
                                        break;
                                    case Var_Types.DOUBLE:
                                        ByteBuffer bb_d = new ByteBuffer(8);
                                        bb_d.WriteDouble(System.Convert.ToDouble(svar.Value));
                                        byte[] tmphex_d = bb_d.Get_ByteArray();

                                        for (int ii = 0; ii < tmphex_d.Length; ii++)
                                        {
                                            dest_str_hex += tmphex_d[ii].ToString("X2") + " ";
                                        }
                                        break;
                                    case Var_Types.STRING:
                                        ByteBuffer bb_s = new ByteBuffer(System.Convert.ToString(svar.Value).Length * 2 + 2);
                                        bb_s.WriteString(System.Convert.ToString(svar.Value));
                                        byte[] tmphex_s = bb_s.Get_ByteArray();

                                        for (int ii = 0; ii < tmphex_s.Length; ii++)
                                        {
                                            dest_str_hex += tmphex_s[ii].ToString("X2") + " ";
                                        }
                                        break;
                                    case Var_Types.BYTEBUFFER:
                                        byte[] tmphex_bb = ((ByteBuffer)svar.Value).Get_ByteArray();

                                        for (int ii = 0; ii < tmphex_bb.Length; ii++)
                                        {
                                            dest_str_hex += tmphex_bb[ii].ToString("X2") + " ";
                                        }
                                        break;
                                }
                                ovar.Value = dest_str_hex;
                                pname = "";
                                break;
                            case "GET_HEX32":
                                ovar.Type = Var_Types.STRING;
                                string dest_str_hex32 = "";

                                switch (svar.Type)
                                {
                                    case Var_Types.INT:
                                        ByteBuffer bb_i = new ByteBuffer(4);
                                        bb_i.WriteInt32(System.Convert.ToInt32(svar.Value));
                                        byte[] tmphex_i = bb_i.Get_ByteArray();

                                        for (int ii = 0; ii < tmphex_i.Length; ii++)
                                        {
                                            dest_str_hex32 += tmphex_i[ii].ToString("X2") + " ";
                                        }
                                        break;
                                }
                                ovar.Value = dest_str_hex32;
                                pname = "";
                                break;
                            case "REVERSE":
                                if (svar.Type == Var_Types.ARRAYLIST)
                                {
                                    ((System.Collections.ArrayList)svar.Value).Reverse();
                                }
                                else if (svar.Type == Var_Types.STRING)
                                {
                                    if (string.IsNullOrEmpty(((string)svar.Value)))
                                    {
                                        //reverse of nothing is itself
                                    }
                                    else
                                    {
                                        System.Text.StringBuilder builder = new System.Text.StringBuilder(((string)svar.Value).Length);
                                        for (int i = ((string)svar.Value).Length - 1; i >= 0; i--)
                                        {
                                            builder.Append(((string)svar.Value)[i]);
                                        }
                                        svar.Value = builder.ToString();
                                    }
                                }
                                pname = "";
                                break;
                            case "CLEAR":
                                switch (svar.Type)
                                {
                                    case Var_Types.ARRAYLIST:
                                        ((System.Collections.ArrayList)svar.Value).Clear();
                                        break;
                                    case Var_Types.SORTEDLIST:
                                        ((System.Collections.SortedList)svar.Value).Clear();
                                        break;
                                    case Var_Types.STACK:
                                        ((System.Collections.Stack)svar.Value).Clear();
                                        break;
                                    case Var_Types.QUEUE:
                                        ((System.Collections.Queue)svar.Value).Clear();
                                        break;
                                }
                                pname = "";
                                break;
                            case "TO_UPPER":
                                if (svar.Type == Var_Types.STRING)
                                {
                                    ovar.Type = Var_Types.STRING;
                                    ovar.Value = System.Convert.ToString(svar.Value).ToUpper();
                                    //svar.State = Var_State.PUBLIC;
                                }
                                pname = "";
                                break;
                            case "TO_UPPER_INVARIANT":
                                if (svar.Type == Var_Types.STRING)
                                {
                                    ovar.Type = Var_Types.STRING;
                                    ovar.Value = System.Convert.ToString(svar.Value).ToUpperInvariant();
                                    //svar.State = Var_State.PUBLIC;
                                }
                                pname = "";
                                break;
                            case "TO_LOWER":
                                if (svar.Type == Var_Types.STRING)
                                {
                                    ovar.Type = Var_Types.STRING;
                                    ovar.Value = System.Convert.ToString(svar.Value).ToLower();
                                    //svar.State = Var_State.PUBLIC;
                                }
                                pname = "";
                                break;
                            case "TO_LOWER_INVARIANT":
                                if (svar.Type == Var_Types.STRING)
                                {
                                    ovar.Type = Var_Types.STRING;
                                    ovar.Value = System.Convert.ToString(svar.Value).ToLowerInvariant();
                                    //svar.State = Var_State.PUBLIC;
                                }
                                pname = "";
                                break;
                            case "TRIM":
                                if (svar.Type == Var_Types.STRING)
                                {
                                    ovar.Type = Var_Types.STRING;
                                    ovar.Value = System.Convert.ToString(svar.Value).Trim();
                                    //svar.State = Var_State.PUBLIC;
                                }
                                pname = "";
                                break;
                            case "LASTINDEXOF":
                                if (svar.Type == Var_Types.STRING)
                                {
                                    string sv1 = Get_String(ref pname);
                                    ScriptVariable v1 = Get_Var(sv1);

                                    ovar.Type = Var_Types.INT;
                                    ovar.Value = System.Convert.ToString(svar.Value).LastIndexOf(v1.Value.ToString());
                                }
                                pname = "";
                                break;
                            case "INDEXOF":
                                if (svar.Type == Var_Types.STRING)
                                {
                                    string sv1 = Get_String(ref pname);
                                    ScriptVariable v1 = Get_Var(sv1);

                                    ovar.Type = Var_Types.INT;
                                    ovar.Value = System.Convert.ToString(svar.Value).IndexOf(v1.Value.ToString());
                                }
                                pname = "";
                                break;
                            case "TRIMEND":
                                if (svar.Type == Var_Types.STRING)
                                {
                                    string sv1 = Get_String(ref pname);
                                    ScriptVariable v1 = Get_Var(sv1);

                                    ovar.Type = Var_Types.STRING;
                                    ovar.Value = System.Convert.ToString(svar.Value).TrimEnd(v1.Value.ToString().ToCharArray());
                                }
                                pname = "";
                                break;
                            case "TRIMSTART":
                                if (svar.Type == Var_Types.STRING)
                                {
                                    string sv1 = Get_String(ref pname);
                                    ScriptVariable v1 = Get_Var(sv1);

                                    ovar.Type = Var_Types.STRING;
                                    ovar.Value = System.Convert.ToString(svar.Value).TrimStart(v1.Value.ToString().ToCharArray());
                                }
                                pname = "";
                                break;
                            case "CONTAINS":
                                if (svar.Type == Var_Types.STRING)
                                {
                                    string sv1 = Get_String(ref pname);
                                    ScriptVariable v1 = Get_Var(sv1);

                                    if (svar.Value.ToString().Contains(v1.Value.ToString()))
                                    {
                                        ovar = Get_Value("TRUE");
                                    }
                                    else
                                    {
                                        ovar = Get_Value("FALSE");
                                    }
                                }
                                break;
                            case "SUBSTRING":
                                if (svar.Type == Var_Types.STRING)
                                {
                                    string sv1 = Get_String(ref pname);
                                    ScriptVariable v1 = Get_Var(sv1);

                                    string sv2 = Get_String(ref pname);
                                    ScriptVariable v2 = Get_Var(sv2);

                                    ovar.Type = Var_Types.STRING;
                                    ovar.Value = System.Convert.ToString(svar.Value).Substring(System.Convert.ToInt32(v1.Value), System.Convert.ToInt32(v2.Value));
                                }
                                break;
                            case "REPLACE":
                                if (svar.Type == Var_Types.STRING)
                                {
                                    string sv1 = Get_String(ref pname);
                                    ScriptVariable v1 = Get_Var(sv1);

                                    string sv2 = Get_String(ref pname);
                                    ScriptVariable v2 = Get_Var(sv2);

                                    ovar.Type = Var_Types.STRING;
                                    ovar.Value = System.Convert.ToString(svar.Value).Replace(v1.Value.ToString(), v2.Value.ToString());
                                }
                                break;
                            case "STARTSWITH":
                                if (svar.Type == Var_Types.STRING)
                                {
                                    string sv1 = Get_String(ref pname);
                                    ScriptVariable v1 = Get_Var(sv1);

                                    if (svar.Value.ToString().StartsWith(v1.Value.ToString()))
                                    {
                                        ovar = Get_Value("TRUE");
                                    }
                                    else
                                    {
                                        ovar = Get_Value("FALSE");
                                    }
                                }
                                break;
                            case "ENDSWITH":
                                if (svar.Type == Var_Types.STRING)
                                {
                                    string sv1 = Get_String(ref pname);
                                    ScriptVariable v1 = Get_Var(sv1);

                                    if (svar.Value.ToString().EndsWith(v1.Value.ToString()))
                                    {
                                        ovar = Get_Value("TRUE");
                                    }
                                    else
                                    {
                                        ovar = Get_Value("FALSE");
                                    }
                                }
                                break;
                            case "FLUSH":
                                if (svar.Type == Var_Types.FILEWRITER)
                                {
                                    ((System.IO.StreamWriter)svar.Value).Flush();
                                }
                                break;
                            case "CLOSE":
                                if (svar.Type == Var_Types.FILEWRITER)
                                {
                                    ((System.IO.StreamWriter)svar.Value).Close();
                                }
                                else if (svar.Type == Var_Types.FILEREADER)
                                {
                                    ((System.IO.StreamReader)svar.Value).Close();
                                }
                                break;
                            case "WRITE":
                                if (svar.Type == Var_Types.FILEWRITER)
                                {
                                    string sv1 = Get_String(ref pname);
                                    ScriptVariable v1 = Get_Var(sv1);

                                    ((System.IO.StreamWriter)svar.Value).WriteLine(
                                        System.Convert.ToString(v1.Value, System.Globalization.CultureInfo.InvariantCulture));
                                }
                                break;
                            case "READ":
                                if (svar.Type == Var_Types.FILEREADER)
                                {
                                    string sv1 = Get_String(ref pname);
                                    ScriptVariable v1 = Get_Var(sv1);

                                    switch (v1.Type)
                                    {
                                        case Var_Types.INT:
                                            v1.Value = System.Convert.ToInt64(((System.IO.StreamReader)svar.Value).ReadLine(), System.Globalization.CultureInfo.InvariantCulture);
                                            break;
                                        case Var_Types.DOUBLE:
                                            v1.Value = System.Convert.ToDouble(((System.IO.StreamReader)svar.Value).ReadLine(), System.Globalization.CultureInfo.InvariantCulture);
                                            break;
                                        case Var_Types.STRING:
                                            v1.Value = ((System.IO.StreamReader)svar.Value).ReadLine();
                                            break;
                                    }
                                }
                                break;
                            case "GET_KEY":
                                if (svar.Type == Var_Types.SORTEDLIST)
                                {
                                    string sv1 = Get_String(ref pname);
                                    ScriptVariable v1 = Get_Var(sv1);

                                    ovar.Type = Var_Types.STRING;
                                    ovar.Value = System.Convert.ToString(((System.Collections.SortedList)svar.Value).GetKey(System.Convert.ToInt32(v1.Value)));
                                }
                                break;
                            case "ADD":
                            case "PUSH":
                                switch (svar.Type)
                                {
                                    case Var_Types.ARRAYLIST:
                                        string sv1 = Get_String(ref pname);
                                        ScriptVariable v1 = Get_Var(sv1);

                                        ((System.Collections.ArrayList)svar.Value).Add(v1);
                                        break;
                                    case Var_Types.SORTEDLIST:
                                        string sortedlist_sv1 = Get_String(ref pname);
                                        ScriptVariable sortedlist_v1 = Get_Var(sortedlist_sv1);
                                        string sortedlist_sv2 = Get_String(ref pname).ToUpperInvariant();
                                        //ScriptVariable v2 = Get_Var(sv2);//key is a string, don't need this

                                        if (((System.Collections.SortedList)svar.Value).ContainsKey(sortedlist_sv2))
                                        {
                                            ScriptEngine.Script_Error("key: " + sortedlist_sv2 + " already exists in: " + sortedlist_v1.Name);
                                        }
                                        else
                                        {
                                            ((System.Collections.SortedList)svar.Value).Add(sortedlist_sv2, sortedlist_v1);
                                        }
                                        break;
                                    case Var_Types.STACK:
                                        string stack_sv1 = Get_String(ref pname);
                                        ScriptVariable stack_v1 = Get_Var(stack_sv1);

                                        ((System.Collections.Stack)svar.Value).Push(stack_v1);
                                        break;
                                    case Var_Types.QUEUE:
                                        string queue_sv1 = Get_String(ref pname);
                                        ScriptVariable queue_v1 = Get_Var(queue_sv1);

                                        ((System.Collections.Queue)svar.Value).Enqueue(queue_v1);
                                        break;
                                }
                                pname = "";
                                break;
                            case "REMOVE":
                                switch (svar.Type)
                                {
                                    case Var_Types.ARRAYLIST:
                                        string arraylist_sv1 = Get_String(ref pname);

                                        ((System.Collections.ArrayList)svar.Value).RemoveAt(Util.GetInt32(arraylist_sv1));
                                        break;
                                    case Var_Types.SORTEDLIST:
                                        string sortedlist_sv1 = Get_String(ref pname).ToUpperInvariant();

                                        ((System.Collections.SortedList)svar.Value).Remove(sortedlist_sv1);
                                        break;
                                    case Var_Types.STRING:
                                        string string_sv1 = Get_String(ref pname);
                                        ScriptVariable v1 = Get_Var(string_sv1);

                                        string string_sv2 = Get_String(ref pname);
                                        ScriptVariable v2 = Get_Var(string_sv2);

                                        ovar.Type = Var_Types.STRING;
                                        ovar.Value = System.Convert.ToString(svar.Value).Remove(System.Convert.ToInt32(v1.Value), System.Convert.ToInt32(v2.Value));
                                        break;
                                }
                                pname = "";
                                break;
                            case "REMOVEAT":
                                switch (svar.Type)
                                {
                                    case Var_Types.ARRAYLIST:
                                        string sv1 = Get_String(ref pname);

                                        ((System.Collections.ArrayList)svar.Value).RemoveAt(Util.GetInt32(sv1));
                                        break;
                                    case Var_Types.SORTEDLIST:
                                        string sortedlist_sv1 = Get_String(ref pname);

                                        ((System.Collections.SortedList)svar.Value).RemoveAt(Util.GetInt32(sortedlist_sv1));
                                        break;
                                }
                                pname = "";
                                break;
                            case "CONTAINS_KEY":
                                if (svar.Type == Var_Types.SORTEDLIST)
                                {
                                    string sv1 = Get_String(ref pname).ToUpperInvariant();

                                    if (((System.Collections.SortedList)svar.Value).ContainsKey(sv1))
                                    {
                                        ovar = Get_Value("TRUE");
                                    }
                                    else
                                    {
                                        ovar = Get_Value("FALSE");
                                    }
                                }
                                break;
                                
                            case "EFFECT_TIME":
                                if (svar.Type == Var_Types.SORTEDLIST)
                                {
                                    string sv1 = Get_String(ref pname).ToUpperInvariant();

                                    if (((System.Collections.SortedList)svar.Value).ContainsKey(sv1))
                                    {

                                        //Looks crappy but it works
                                        long duration = System.Convert.ToInt64(((ScriptVariable)(((Script_ClassData)(((ScriptVariable)(((SortedList)svar.Value)[sv1])).Value))._Variables["DURATION"])).Value);
                                        duration = (duration - System.DateTime.Now.Ticks) / TimeSpan.TicksPerSecond;
                                        ovar.Type = Var_Types.INT;
                                        ovar.Value = duration;
                                    }
                                }
                                break;                                 
                            case "POP":
                                if (svar.Type == Var_Types.STACK)
                                {
                                    ScriptVariable v1 = (ScriptVariable)(((System.Collections.Stack)svar.Value).Pop());
                                    ovar.Type = v1.Type;
                                    ovar.Value = v1.Value;
                                }
                                if (svar.Type == Var_Types.QUEUE)
                                {
                                    ScriptVariable v1 = (ScriptVariable)(((System.Collections.Queue)svar.Value).Dequeue());
                                    ovar.Type = v1.Type;
                                    ovar.Value = v1.Value;
                                }
                                pname = "";
                                break;
                            case "PEEK":
                                if (svar.Type == Var_Types.STACK)
                                {
                                    ScriptVariable v1 = (ScriptVariable)(((System.Collections.Stack)svar.Value).Peek());
                                    ovar.Type = v1.Type;
                                    ovar.Value = v1.Value;
                                }
                                if (svar.Type == Var_Types.QUEUE)
                                {
                                    ScriptVariable v1 = (ScriptVariable)(((System.Collections.Queue)svar.Value).Peek());
                                    ovar.Type = v1.Type;
                                    ovar.Value = v1.Value;
                                }
                                pname = "";
                                break;
                            case "WRITE_UINT16":
                                if (svar.Type == Var_Types.BYTEBUFFER)
                                {
                                    string sv1 = Get_String(ref pname);
                                    ScriptVariable v1 = Get_Var(sv1);

                                    try
                                    {
                                        ((ByteBuffer)svar.Value).WriteUInt16(System.Convert.ToUInt16(v1.Value));
                                    }
                                    catch
                                    {
                                        ScriptEngine.Script_Error("possible overflow writing " + v1.Name + " as UINT16");
                                    }
                                }
                                pname = "";
                                break;
                            case "WRITE_UINT32":
                                if (svar.Type == Var_Types.BYTEBUFFER)
                                {
                                    string sv1 = Get_String(ref pname);
                                    ScriptVariable v1 = Get_Var(sv1);

                                    try
                                    {
                                        ((ByteBuffer)svar.Value).WriteUInt32(System.Convert.ToUInt32(v1.Value));
                                    }
                                    catch
                                    {
                                        ScriptEngine.Script_Error("possible overflow writing " + v1.Name + " as UINT32");
                                    }
                                }
                                pname = "";
                                break;
                            case "WRITE_UINT64":
                                if (svar.Type == Var_Types.BYTEBUFFER)
                                {
                                    string sv1 = Get_String(ref pname);
                                    ScriptVariable v1 = Get_Var(sv1);

                                    ((ByteBuffer)svar.Value).WriteUInt64(System.Convert.ToUInt64(v1.Value));
                                }
                                pname = "";
                                break;
                            case "WRITE_INT16":
                                if (svar.Type == Var_Types.BYTEBUFFER)
                                {
                                    string sv1 = Get_String(ref pname);
                                    ScriptVariable v1 = Get_Var(sv1);

                                    try
                                    {
                                        ((ByteBuffer)svar.Value).WriteInt16(System.Convert.ToInt16(v1.Value));
                                    }
                                    catch
                                    {
                                        ScriptEngine.Script_Error("possible overflow writing " + v1.Name + " as INT16");
                                    }
                                }
                                pname = "";
                                break;
                            case "WRITE_INT32":
                                if (svar.Type == Var_Types.BYTEBUFFER)
                                {
                                    string sv1 = Get_String(ref pname);
                                    ScriptVariable v1 = Get_Var(sv1);

                                    try
                                    {
                                        ((ByteBuffer)svar.Value).WriteInt32(System.Convert.ToInt32(v1.Value));
                                    }
                                    catch
                                    {
                                        ScriptEngine.Script_Error("possible overflow writing " + v1.Name + " as INT32");
                                    }
                                }
                                pname = "";
                                break;
                            case "WRITE_INT64":
                                if (svar.Type == Var_Types.BYTEBUFFER)
                                {
                                    string sv1 = Get_String(ref pname);
                                    ScriptVariable v1 = Get_Var(sv1);

                                    ((ByteBuffer)svar.Value).WriteInt64(System.Convert.ToInt64(v1.Value));
                                }
                                pname = "";
                                break;
                            case "WRITE_DOUBLE":
                                if (svar.Type == Var_Types.BYTEBUFFER)
                                {
                                    string sv1 = Get_String(ref pname);
                                    ScriptVariable v1 = Get_Var(sv1);

                                    ((ByteBuffer)svar.Value).WriteDouble(System.Convert.ToDouble(v1.Value));
                                }
                                pname = "";
                                break;
                            case "WRITE_BYTE":
                                if (svar.Type == Var_Types.BYTEBUFFER)
                                {
                                    string sv1 = Get_String(ref pname);
                                    ScriptVariable v1 = Get_Var(sv1);

                                    try
                                    {
                                        ((ByteBuffer)svar.Value).WriteByte(System.Convert.ToByte(v1.Value));
                                    }
                                    catch
                                    {
                                        ScriptEngine.Script_Error("possible overflow writing " + v1.Name + " as BYTE");
                                    }
                                }
                                pname = "";
                                break;
                            case "WRITE_STRING":
                                if (svar.Type == Var_Types.BYTEBUFFER)
                                {
                                    string sv1 = Get_String(ref pname);
                                    ScriptVariable v1 = Get_Var(sv1);

                                    ((ByteBuffer)svar.Value).WriteString(System.Convert.ToString(v1.Value));
                                }
                                pname = "";
                                break;
                            case "READ_UINT16":
                                if (svar.Type == Var_Types.BYTEBUFFER)
                                {
                                    string sv1 = Get_String(ref pname);
                                    ScriptVariable v1 = Get_Var(sv1);
                                    v1.Type = Var_Types.INT;
                                    v1.Value = System.Convert.ToInt64(((ByteBuffer)svar.Value).ReadUInt16());
                                }
                                pname = "";
                                break;
                            case "READ_UINT32":
                                if (svar.Type == Var_Types.BYTEBUFFER)
                                {
                                    string sv1 = Get_String(ref pname);
                                    ScriptVariable v1 = Get_Var(sv1);
                                    v1.Type = Var_Types.INT;
                                    v1.Value = System.Convert.ToInt64(((ByteBuffer)svar.Value).ReadUInt32());
                                }
                                pname = "";
                                break;
                            case "READ_UINT64":
                                if (svar.Type == Var_Types.BYTEBUFFER)
                                {
                                    string sv1 = Get_String(ref pname);
                                    ScriptVariable v1 = Get_Var(sv1);
                                    v1.Type = Var_Types.INT;
                                    v1.Value = System.Convert.ToInt64(((ByteBuffer)svar.Value).ReadUInt64());
                                }
                                pname = "";
                                break;
                            case "READ_INT16":
                                if (svar.Type == Var_Types.BYTEBUFFER)
                                {
                                    string sv1 = Get_String(ref pname);
                                    ScriptVariable v1 = Get_Var(sv1);
                                    v1.Type = Var_Types.INT;
                                    v1.Value = System.Convert.ToInt64(((ByteBuffer)svar.Value).ReadInt16());
                                }
                                pname = "";
                                break;
                            case "READ_INT32":
                                if (svar.Type == Var_Types.BYTEBUFFER)
                                {
                                    string sv1 = Get_String(ref pname);
                                    ScriptVariable v1 = Get_Var(sv1);
                                    v1.Type = Var_Types.INT;
                                    v1.Value = System.Convert.ToInt64(((ByteBuffer)svar.Value).ReadInt32());
                                }
                                pname = "";
                                break;
                            case "READ_INT64":
                                if (svar.Type == Var_Types.BYTEBUFFER)
                                {
                                    string sv1 = Get_String(ref pname);
                                    ScriptVariable v1 = Get_Var(sv1);
                                    v1.Type = Var_Types.INT;
                                    v1.Value = System.Convert.ToInt64(((ByteBuffer)svar.Value).ReadInt64());
                                }
                                pname = "";
                                break;
                            case "READ_DOUBLE":
                                if (svar.Type == Var_Types.BYTEBUFFER)
                                {
                                    string sv1 = Get_String(ref pname);
                                    ScriptVariable v1 = Get_Var(sv1);
                                    v1.Type = Var_Types.DOUBLE;
                                    v1.Value = System.Convert.ToDouble(((ByteBuffer)svar.Value).ReadDouble());
                                }
                                pname = "";
                                break;
                            case "READ_BYTE":
                                if (svar.Type == Var_Types.BYTEBUFFER)
                                {
                                    string sv1 = Get_String(ref pname);
                                    ScriptVariable v1 = Get_Var(sv1);
                                    v1.Type = Var_Types.INT;
                                    v1.Value = System.Convert.ToInt64(((ByteBuffer)svar.Value).ReadByte());
                                }
                                pname = "";
                                break;
                            case "READ_STRING":
                                if (svar.Type == Var_Types.BYTEBUFFER)
                                {
                                    string sv1 = Get_String(ref pname);
                                    ScriptVariable v1 = Get_Var(sv1);
                                    v1.Type = Var_Types.STRING;
                                    v1.Value = System.Convert.ToString(((ByteBuffer)svar.Value).ReadString());
                                }
                                pname = "";
                                break;
                            case "INDEX":
                                if (svar.Type == Var_Types.BYTEBUFFER)
                                {
                                    ovar.Type = Var_Types.INT;
                                    ovar.Value = System.Convert.ToInt64(((ByteBuffer)svar.Value).GetIndex());
                                }
                                pname = "";
                                break;
                            case "RESET_INDEX":
                                if (svar.Type == Var_Types.BYTEBUFFER)
                                {
                                    ((ByteBuffer)svar.Value).ResetIndex();
                                }
                                pname = "";
                                break;
                            case "TRIM_TO_INDEX":
                                if (svar.Type == Var_Types.BYTEBUFFER)
                                {
                                    ((ByteBuffer)svar.Value).TrimToIndex();
                                }
                                pname = "";
                                break;
                            case "RUN_WINDOW":
                                if (svar.Type == Var_Types.WINDOW)
                                {
                                    try
                                    {
                                        ((ScriptWindow)svar.Value).Open_Window();
                                    }
                                    catch
                                    {
                                        ScriptEngine.Script_Error("failed to open window " + svar.Name);
                                    }
                                }
                                pname = "";
                                break;
                            case "CLOSE_WINDOW":
                                if (svar.Type == Var_Types.WINDOW)
                                {
                                    try
                                    {
                                        ((ScriptWindow)svar.Value).Kill();
                                    }
                                    catch
                                    {
                                        ScriptEngine.Script_Error("failed to close window " + svar.Name);
                                    }
                                }
                                pname = "";
                                break;
                            case "SET_FILENAME":
                                if (svar.Type == Var_Types.WINDOW)
                                {
                                    string sv1 = Get_String(ref pname);
                                    string sv2 = Get_String(ref pname);

                                    try
                                    {
                                        ((ScriptWindow)svar.Value).Set_FileName(sv1, sv2);
                                    }
                                    catch
                                    {
                                        ScriptEngine.Script_Error("failed to set filename for " + svar.Name + " with : " + sv1 + " :: " + sv2);
                                    }
                                }
                                pname = "";
                                break;
                            case "SET_NAME":
                                if (svar.Type == Var_Types.WINDOW)
                                {
                                    string sv1 = Get_String(ref pname);

                                    try
                                    {
                                        ((ScriptWindow)svar.Value).Set_WindowName(sv1);
                                    }
                                    catch
                                    {
                                        ScriptEngine.Script_Error("failed to set windowname for " + svar.Name + " with : " + sv1);
                                    }
                                }
                                pname = "";
                                break;
                            case "SET_HTML":
                                if (svar.Type == Var_Types.WINDOW)
                                {
                                    string sv1 = Get_String(ref pname);

                                    try
                                    {
                                        ((ScriptWindow)svar.Value).Set_HTML(sv1);
                                    }
                                    catch
                                    {
                                        ScriptEngine.Script_Error("failed to set html for " + svar.Name + " with : " + sv1);
                                    }
                                }
                                pname = "";
                                break;
                            case "REFRESH":
                                if (svar.Type == Var_Types.WINDOW)
                                {
                                    try
                                    {
                                        ((ScriptWindow)svar.Value).Refresh();
                                    }
                                    catch
                                    {
                                        ScriptEngine.Script_Error("failed to refresh for " + svar.Name);
                                    }
                                }
                                pname = "";
                                break;
                            case "STANDARD_IN":
                                if (svar.Type == Var_Types.WINDOW)
                                {
                                    string sv1 = Get_String(ref pname);

                                    try
                                    {
                                        ((ScriptWindow)svar.Value).Send_StandardInput(sv1);
                                    }
                                    catch
                                    {
                                        ScriptEngine.Script_Error("failed to send to standard in for " + svar.Name);
                                    }
                                }
                                pname = "";
                                break;
                            case "WAIT_CLOSE":
                                if (svar.Type == Var_Types.WINDOW)
                                {
                                    try
                                    {
                                        ((ScriptWindow)svar.Value).Wait_Close();
                                    }
                                    catch
                                    {
                                        ScriptEngine.Script_Error("failed to wait close " + svar.Name);
                                    }
                                }
                                pname = "";
                                break;
                            case "WAIT_IDLE":
                                if (svar.Type == Var_Types.WINDOW)
                                {
                                    try
                                    {
                                        ((ScriptWindow)svar.Value).Wait_Idle();
                                    }
                                    catch
                                    {
                                        ScriptEngine.Script_Error("failed to wait idle " + svar.Name);
                                    }
                                }
                                pname = "";
                                break;
                            case "STOP":
                                if (svar.Type == Var_Types.THREAD)
                                {
                                    try
                                    {
                                        ((ScriptThread)svar.Value).Stop();
                                    }
                                    catch
                                    {
                                        ScriptEngine.Script_Error("failed to stop thread " + svar.Name);
                                    }
                                }
                                pname = "";
                                break;
                            case "START":
                                if (svar.Type == Var_Types.THREAD)
                                {
                                    try
                                    {
                                        ((ScriptThread)svar.Value).Start();
                                    }
                                    catch
                                    {
                                        ScriptEngine.Script_Error("failed to start thread " + svar.Name);
                                    }
                                }
                                pname = "";
                                break;
                            case "SEND_TO_STACK":
                                if (svar.Type == Var_Types.THREAD)
                                {
                                    try
                                    {
                                        ((ScriptThread)svar.Value).Start();
                                    }
                                    catch
                                    {
                                        ScriptEngine.Script_Error("failed to start thread " + svar.Name);
                                    }
                                }
                                pname = "";
                                break;
                            default:
                                //is it a class?
                                valid = false;
                                switch (svar.Type)
                                {
                                    case Var_Types.CLASS:
                                        //this isn't really needed since we already check if it is a class variable before we even get this far
                                        if (((Script_ClassData)svar.Value)._Variables.ContainsKey(dname))
                                        {
                                            valid = true;
                                            if (pname.Length == 0)
                                            {
                                                ovar = (ScriptVariable)(((Script_ClassData)svar.Value)._Variables[dname]);
                                            }
                                            else
                                            {
                                                svar = (ScriptVariable)(((Script_ClassData)svar.Value)._Variables[dname]);
                                            }
                                        }
                                        /*foreach (ScriptVariable svc in ((Script_ClassData)svar.Value)._Variables)
                                        {
                                            if (svc.Name == dname)
                                            {
                                                valid = true;
                                                if (pname.Length == 0)
                                                {
                                                    ovar = svc;
                                                }
                                                else
                                                {
                                                    svar = svc;
                                                }
                                                break;
                                            }
                                        }*/
                                        break;
                                    case Var_Types.ARRAYLIST:
                                        ScriptVariable sva = Get_Var(dname);
                                        switch (sva.Type)
                                        {
                                            case Var_Types.INT:
                                                valid = true;
                                                if (pname.Length == 0)
                                                {
                                                    ovar = (ScriptVariable)(((System.Collections.ArrayList)svar.Value)[System.Convert.ToInt32(sva.Value)]);
                                                }
                                                else
                                                {
                                                    svar = (ScriptVariable)(((System.Collections.ArrayList)svar.Value)[System.Convert.ToInt32(sva.Value)]);
                                                }
                                                break;
                                        }
                                        break;
                                    case Var_Types.SORTEDLIST:
                                        ScriptVariable svl = Get_Var(dname);
                                        switch (svl.Type)
                                        {
                                            case Var_Types.INT:
                                                valid = true;
                                                if (pname.Length == 0)
                                                {
                                                    ovar = (ScriptVariable)(((System.Collections.SortedList)svar.Value).GetByIndex(System.Convert.ToInt32(svl.Value)));
                                                }
                                                else
                                                {
                                                    svar = (ScriptVariable)(((System.Collections.SortedList)svar.Value).GetByIndex(System.Convert.ToInt32(svl.Value)));
                                                }
                                                break;
                                            case Var_Types.STRING:
                                                valid = true;
                                                if (pname.Length == 0)
                                                {
                                                    ovar = (ScriptVariable)(((System.Collections.SortedList)svar.Value)[System.Convert.ToString(svl.Value).ToUpperInvariant()]);
                                                }
                                                else
                                                {
                                                    svar = (ScriptVariable)(((System.Collections.SortedList)svar.Value)[System.Convert.ToString(svl.Value).ToUpperInvariant()]);
                                                }
                                                break;
                                        }
                                        break;
                                }

                                if (!valid)
                                {
                                    ScriptEngine.Script_Error(dname + " does not exist!");
                                    throw new Exception();
                                }
                                break;
                        }
                    } //end of if this wasn't a class variable to being with
                } while (pname.Length != 0);//.IndexOf(".", 0) != -1);

                return ovar;
            }
            else
            {
                return Get_Var_Internal(pname, StackHeight);
            }
        }

        public static void Script_Error(string text)
        {
            int current_line;

            try
            {
                current_line = Line_Pos;
            }
            catch
            {
                current_line = -1;
            }

            Globals.l2net_home.Add_Error("SCRIPT ERROR : THREAD[" + CurrentThread.ToString() + "] LINE[" + current_line.ToString() + "] : " + text);
        }

        private static ScriptVariable Get_Var_Internal(string pname, int h)
        {
            string name = pname.ToUpperInvariant().Trim();

            if (((VariableList)Stack[h]).ContainsKey(name))
            {
                return (ScriptVariable)((VariableList)Stack[h])[name];
            }

            //it wasnt one of our private vars...
            //maybe it is a global var?

            if (GlobalVariables.ContainsKey(name))
            {
                return (ScriptVariable)GlobalVariables[name];
            }

            return Get_Value(pname);
        }

		public static string Get_String(ref string inp, bool evaluate = true)
		{
			//lets remove all the blank spaces at the start of the line
            inp = inp.Trim();

			if(inp.Length == 0)
			{
				return "";
			}

			string cmd;// = "";
			int spc;

			if(inp[0] == '"')
			{
				//this is a quote
				spc = inp.IndexOf('"',1);
				while(inp[spc-1] == '\\')
				{
					spc = inp.IndexOf('"',spc+1);
				}

				if(spc > 0)
				{
					cmd = inp.Substring(1,spc-1);
					inp = (inp.Remove(0,spc+1)).TrimStart(' ');
				}
				else
				{
					cmd = inp.Substring(1,inp.Length-1);
					inp = "";
				}

                if (evaluate)
                {
                    cmd = Get_String_Internal(cmd);
                }
			}
			else
			{
				//not a quote
				spc = inp.IndexOf(' ');
                int tb = inp.IndexOf('\t');

                if (tb != -1 && tb < spc)
                {
                    spc = tb;
                }

				if(spc > 0)
				{
					cmd = inp.Substring(0,spc);
					inp = inp.Remove(0,spc+1);
				}
				else
				{
					cmd = inp;
					inp = "";
				}
			}

			//lets replace all \" with just "
			cmd = cmd.Replace( "\\\"", "\"");


            if (cmd.Contains("\\n"))
            {
                //need to check... is this a real new line... or a \\n
                cmd = cmd.Replace("\\\\n", "!@#$%^&!@#$%^&");
                cmd = cmd.Replace("\\n", Environment.NewLine);
                cmd = cmd.Replace("!@#$%^&!@#$%^&", "\\n");
            }

			return cmd;
		}

        public static string Get_StringToken(ref string inp)
        {
            //lets remove all the blank spaces at the start of the line
            inp = inp.Trim();

            if (inp.Length == 0)
            {
                return "";
            }

            string cmd;// = "";
            int spc;

            if (inp[0] == '"')
            {
                //this is a quote
                spc = inp.IndexOf('"', 1);
                while (inp[spc - 1] == '\\')
                {
                    spc = inp.IndexOf('"', spc + 1);
                }

                if (spc > 0)
                {
                    cmd = inp.Substring(0, spc + 1);
                    inp = (inp.Remove(0, spc + 1)).TrimStart(' ');
                }
                else
                {
                    cmd = inp;
                    inp = "";
                }
            }
            else
            {
                //not a quote
                spc = inp.IndexOf(' ');
                int tb = inp.IndexOf('\t');

                if (tb != -1 && tb < spc)
                {
                    spc = tb;
                }

                if (spc > 0)
                {
                    cmd = inp.Substring(0, spc);
                    inp = inp.Remove(0, spc + 1);
                }
                else
                {
                    cmd = inp;
                    inp = "";
                }
            }

            //lets replace all \" with just "
            //cmd = cmd.Replace("\\\"", "\"").Replace("\\n", Environment.NewLine);

            return cmd;
        }

        private static string Get_String_Internal(string cmd)
        {
            //since sometimes we want the variable value and not the name...
            //we want to find and replace all variables in our string
            int var_start;
            int var_end = cmd.Length + 2;
            while ((var_start = cmd.IndexOf("<&", 0)) != -1)
            {
                //got the start, lets grab the end
                int vcount = 0;
                for (int ic = var_start + 2; ic < cmd.Length - 1; ic++)
                {
                    if (cmd[ic] == '<' && cmd[ic + 1] == '&')
                    {
                        vcount++;
                    }
                    if (cmd[ic] == '&' && cmd[ic + 1] == '>')
                    {
                        if (vcount == 0)
                        {
                            var_end = ic;
                            break;
                        }
                        else
                        {
                            vcount--;
                        }
                    }
                }

                string name = cmd.Substring(var_start + 2, var_end - var_start - 2);

                string nname = Get_String_Internal(name);
                ScriptVariable scr_var = Get_Var(nname);

                string value = "";

                switch(scr_var.Type)
                {
                    case Var_Types.INT:
                        value = System.Convert.ToInt64(scr_var.Value).ToString(System.Globalization.CultureInfo.InvariantCulture);
                        break;
                    case Var_Types.DOUBLE:
                        value = System.Convert.ToDouble(scr_var.Value).ToString(System.Globalization.CultureInfo.InvariantCulture);
                        break;
                    case Var_Types.STRING:
                        value = System.Convert.ToString(scr_var.Value);
                        break;
                }

                cmd = cmd.Replace("<&" + name + "&>", value);
            }

            return cmd;
        }

        private bool VariableExists(string name)
        {
            if (((VariableList)Stack[StackHeight]).ContainsKey(name))
            {
                //variable of this name already exists
                return true;
            }

            if (GlobalVariables.ContainsKey(name))
            {
                //variable of this name already exists
                return true;
            }

            return false;
        }

        private ScriptLine Get_Line(int cnt)
        {
            try
            {
                return ((ScriptLine)((ScriptFile)Files[((ScriptThread)Threads[CurrentThread]).Current_File])._ScriptLines[cnt]);
            }
            catch
            {
                ScriptEngine.Script_Error("failed to get line " + cnt.ToString());

                ScriptLine sl = new ScriptLine();
                sl.Command = ScriptCommands.END_OF_FILE;
                return sl;
            }
        }

        private ScriptLine Get_Line(int cnt, string file)
		{
			try
			{
                return ((ScriptLine)((ScriptFile)Files[file])._ScriptLines[cnt]);
			}
            catch
			{
                ScriptEngine.Script_Error("failed to get line " + cnt.ToString());

                ScriptLine sl = new ScriptLine();
                sl.Command = ScriptCommands.END_OF_FILE;
                return sl;
			}
		}
	}//end of class
}//end of namespace