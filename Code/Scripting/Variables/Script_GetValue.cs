using System;
using System.Collections.Generic;
using System.Text;

namespace L2_login
{
    public partial class ScriptEngine
    {
        private static ScriptVariable Get_Value(string oname)
        {
            string name = oname.ToUpperInvariant();

            ScriptVariable scr_var = new ScriptVariable();

            switch (name)
            {
                case "VOID":
                    scr_var.Name = "VOID";
                    scr_var.Type = Var_Types.NULL;
                    scr_var.Value = 0L;
                    break;
                case "ZERO":
                    scr_var.Name = "ZERO";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = 0L;
                    break;
                case "ONE":
                    scr_var.Name = "ONE";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = 1L;
                    break;
                case "TWO":
                    scr_var.Name = "TWO";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = 2L;
                    break;
                case "FALSE":
                    scr_var.Name = "FALSE";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = 0L;
                    break;
                case "TRUE":
                    scr_var.Name = "TRUE";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = 1L;
                    break;
                case "TOWN":
                    scr_var.Name = "TOWN";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = 0L;
                    break;
                case "CLANHALL":
                    scr_var.Name = "CLANHALL";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = 1L;
                    break;
                case "CASTLE":
                    scr_var.Name = "CASTLE";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = 2L;
                    break;
                case "SIEGEHQ":
                    scr_var.Name = "SIEGEHQ";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = 3L;
                    break;
                case "FORTRESS":
                    scr_var.Name = "FORTRESS";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = 3L;
                    break;
                case "WALKING":
                    scr_var.Name = "WALKING";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = 0L;
                    break;
                case "RUNNING":
                    scr_var.Name = "RUNNING";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = 1L;
                    break;
                case "SITTING":
                    scr_var.Name = "SITTING";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = 0L;
                    break;
                case "STANDING":
                    scr_var.Name = "STANDING";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = 1L;
                    break;
                case "START_FAKEDEATH":
                    scr_var.Name = "START_FAKEDEATH";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = 2L;
                    break;
                case "STOP_FAKEDEATH":
                    scr_var.Name = "STOP_FAKEDEATH";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = 3L;
                    break;
                case "ALIVE":
                    scr_var.Name = "ALIVE";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = 0L;
                    break;
                case "DEAD":
                    scr_var.Name = "DEAD";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = 1L;
                    break;
                case "CHAR_NAME":
                    scr_var.Name = "CHAR_NAME";
                    scr_var.Type = Var_Types.STRING;
                    scr_var.Value = Globals.gamedata.my_char.Name;
                    break;
                case "CHAR_ID":
                    scr_var.Name = "CHAR_ID";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_char.ID;
                    break;
                case "CHAR_KARMA":
                    scr_var.Name = "CHAR_KARMA";
                    scr_var.Type = Var_Types.STRING;
                    scr_var.Value = Globals.gamedata.my_char.Karma;
                    break;
                case "CHAR_TITLE":
                    scr_var.Name = "CHAR_TITLE";
                    scr_var.Type = Var_Types.STRING;
                    scr_var.Value = Globals.gamedata.my_char.Title;
                    break;
                case "CHAR_SP":
                    scr_var.Name = "CHAR_SP";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_char.SP;
                    break;
                case "CHAR_XP":
                    scr_var.Name = "CHAR_XP";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_char.XP;
                    break;
                case "CHAR_LEVEL":
                    scr_var.Name = "CHAR_LEVEL";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_char.Level;
                    break;
                case "CHAR_SEX":
                    scr_var.Name = "CHAR_SEX";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_char.Sex;
                    break;
                case "CHAR_RACE":
                    scr_var.Name = "CHAR_RACE";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_char.Race;
                    break;
                case "CHAR_CLASS":
                    scr_var.Name = "CHAR_CLASS";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_char.Class;
                    break;
                case "CHAR_X":
                    scr_var.Name = "CHAR_X";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_char.X;
                    break;
                case "CHAR_Y":
                    scr_var.Name = "CHAR_Y";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_char.Y;
                    break;
                case "CHAR_Z":
                    scr_var.Name = "CHAR_Z";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_char.Z;
                    break;
                case "CHAR_DESTX":
                    scr_var.Name = "CHAR_DESTX";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_char.Dest_X;
                    break;
                case "CHAR_DESTY":
                    scr_var.Name = "CHAR_DESTY";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_char.Dest_Y;
                    break;
                case "CHAR_DESTZ":
                    scr_var.Name = "CHAR_DESTZ";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_char.Dest_Z;
                    break;
                case "CHAR_IS_MOVING":
                    scr_var.Name = "CHAR_IS_MOVING";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = Globals.gamedata.my_char.Moving ? 1L : 0L;
                    break;
                case "CHAR_MAX_HP":
                    scr_var.Name = "CHAR_MAX_HP";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_char.Max_HP;
                    break;
                case "CHAR_MAX_MP":
                    scr_var.Name = "CHAR_MAX_MP";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_char.Max_MP;
                    break;
                case "CHAR_MAX_CP":
                    scr_var.Name = "CHAR_MAX_CP";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_char.Max_CP;
                    break;
                case "CHAR_CUR_HP":
                    scr_var.Name = "CHAR_CUR_HP";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_char.Cur_HP;
                    break;
                case "CHAR_CUR_MP":
                    scr_var.Name = "CHAR_CUR_MP";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_char.Cur_MP;
                    break;
                case "CHAR_CUR_CP":
                    scr_var.Name = "CHAR_CUR_CP";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_char.Cur_CP;
                    break;
                case "CHAR_PER_HP":
                    scr_var.Name = "CHAR_PER_HP";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)(100.0 * (Globals.gamedata.my_char.Cur_HP / Globals.gamedata.my_char.Max_HP));
                    break;
                case "CHAR_PER_MP":
                    scr_var.Name = "CHAR_PER_MP";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)(100.0 * (Globals.gamedata.my_char.Cur_MP / Globals.gamedata.my_char.Max_MP));
                    break;
                case "CHAR_PER_CP":
                    scr_var.Name = "CHAR_PER_CP";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)(100.0 * (Globals.gamedata.my_char.Cur_CP / Globals.gamedata.my_char.Max_CP));
                    break;
                case "CHAR_CUR_LOAD":
                    scr_var.Name = "CHAR_CUR_LOAD";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_char.Cur_Load;
                    break;
                case "CHAR_MAX_LOAD":
                    scr_var.Name = "CHAR_MAX_LOAD";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_char.Max_Load;
                    break;
                case "CHAR_RUN":
                    scr_var.Name = "CHAR_RUN";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_char.isRunning;
                    break;
                case "CHAR_RUN_SPEED":
                    scr_var.Name = "CHAR_RUN_SPEED";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)( Globals.gamedata.my_char.RunSpeed * Globals.gamedata.my_char.MoveSpeedMult);
                    break;
                case "CHAR_WALK_SPEED":
                    scr_var.Name = "CHAR_WALK_SPEED";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)( Globals.gamedata.my_char.WalkSpeed * Globals.gamedata.my_char.MoveSpeedMult);
                    break;
                case "CHAR_ATTACK_SPEED":
                    scr_var.Name = "CHAR_ATTACK_SPEED";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_char.PatkSpeed;
                    break;
                case "CHAR_ATTACK_SPEED_MULT":
                    scr_var.Name = "CHAR_ATTACK_SPEED_MULT";
                    scr_var.Type = Var_Types.DOUBLE;
                    scr_var.Value = (double)Globals.gamedata.my_char.AttackSpeedMult;
                    break;
                case "CHAR_CAST_SPEED":
                    scr_var.Name = "CHAR_CAST_SPEED";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_char.MatkSpeed;
                    break;
                case "CHAR_EVAL":
                    scr_var.Name = "CHAR_EVAL";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_char.RecAmount;
                    break;
                case "CHAR_PARTY_COUNT":
                    scr_var.Name = "CHAR_PARTY_COUNT";
                    scr_var.Type = Var_Types.INT;
                    Globals.PartyLock.EnterReadLock();
                    try
                    {
                        scr_var.Value = (long)Globals.gamedata.PartyCount;
                    }
                    finally
                    {
                        Globals.PartyLock.ExitReadLock();
                    }
                    break;
                case "CHAR_PATK":
                    scr_var.Name = "CHAR_PATK";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_char.Patk;
                    break;
                case "CHAR_MATK":
                    scr_var.Name = "CHAR_MATK";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_char.Matk;
                    break;
                case "CHAR_PDEF":
                    scr_var.Name = "CHAR_PDEF";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_char.PDef;
                    break;
                case "CHAR_MDEF":
                    scr_var.Name = "CHAR_MDEF";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_char.MDef;
                    break;
                case "CHAR_ACCURACY":
                    scr_var.Name = "CHAR_ACCURACY";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_char.Accuracy;
                    break;
                case "CHAR_EVASION":
                    scr_var.Name = "CHAR_EVASION";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_char.Evasion;
                    break;
                case "CHAR_CRITICAL":
                    scr_var.Name = "CHAR_CRITICAL";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_char.Focus;
                    break;
                case "CHAR_STR":
                    scr_var.Name = "CHAR_STR";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_char.STR;
                    break;
                case "CHAR_DEX":
                    scr_var.Name = "CHAR_DEX";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_char.DEX;
                    break;
                case "CHAR_CON":
                    scr_var.Name = "CHAR_CON";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_char.CON;
                    break;
                case "CHAR_INT":
                    scr_var.Name = "CHAR_INT";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_char.INT;
                    break;
                case "CHAR_WIT":
                    scr_var.Name = "CHAR_WIT";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_char.WIT;
                    break;
                case "CHAR_MEN":
                    scr_var.Name = "CHAR_MEN";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_char.MEN;
                    break;
                case "CHAR_CUBIC_COUNT":
                    scr_var.Name = "CHAR_CUBIC_COUNT";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_char.CubicCount;
                    break;
                case "CHAR_PVP_COUNT":
                    scr_var.Name = "CHAR_PVP_COUNT";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_char.PvPCount;
                    break;
                case "CHAR_PK_COUNT":
                    scr_var.Name = "CHAR_PK_COUNT";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_char.PKCount;
                    break;
                case "CHAR_CHARGES":
                    scr_var.Name = "CHAR_CHARGES";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_char.Charges;
                    break;
                case "CHAR_SOULS":
                    scr_var.Name = "CHAR_SOULS";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_char.Souls;
                    break;
                case "CHAR_DEATH_PENALTY":
                    scr_var.Name = "CHAR_DEATH_PENALTY";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_char.DeathPenalty;
                    break;
                case "CHAR_RUNNING":
                    scr_var.Name = "CHAR_RUNNING";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_char.isRunning;
                    break;
                case "CHAR_SITTING":
                    scr_var.Name = "CHAR_SITTING";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_char.isSitting;
                    break;
                case "CHAR_LOOKS_DEAD":
                    scr_var.Name = "CHAR_LOOKS_DEAD";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_char.isAlikeDead;
                    break;
                case "CHAR_CLAN":
                    scr_var.Name = "CHAR_CLAN";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_char.ClanID;
                    break;
                case "CHAR_ALLY":
                    scr_var.Name = "CHAR_ALLY";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_char.AllyID;
                    break;
			    case "CHAR_ITEM_REAR":
				    scr_var.Name = "CHAR_ITEM_REAR";
				    scr_var.Type = Var_Types.INT;
				    scr_var.Value = (long)Globals.gamedata.my_char.itm_REar;
				    break;
			    case "CHAR_ITEM_LEAR":
				    scr_var.Name = "CHAR_ITEM_LEAR";
				    scr_var.Type = Var_Types.INT;
				    scr_var.Value = (long)Globals.gamedata.my_char.itm_LEar;
				    break;
			    case "CHAR_ITEM_NECKLACE":
				    scr_var.Name = "CHAR_ITEM_NECKLACE";
				    scr_var.Type = Var_Types.INT;
				    scr_var.Value = (long)Globals.gamedata.my_char.itm_Neck;
				    break;
			    case "CHAR_ITEM_RFINGER":
				    scr_var.Name = "CHAR_ITEM_RFINGER";
				    scr_var.Type = Var_Types.INT;
				    scr_var.Value = (long)Globals.gamedata.my_char.itm_RFinger;
				    break;
			    case "CHAR_ITEM_LFINGER":
				    scr_var.Name = "CHAR_ITEM_LFINGER";
				    scr_var.Type = Var_Types.INT;
				    scr_var.Value = (long)Globals.gamedata.my_char.itm_LFinger;
				    break;
			    case "CHAR_ITEM_HELM":
				    scr_var.Name = "CHAR_ITEM_HELM";
				    scr_var.Type = Var_Types.INT;
				    scr_var.Value = (long)Globals.gamedata.my_char.itm_Head;
				    break;
			    case "CHAR_ITEM_RHAND":
				    scr_var.Name = "CHAR_ITEM_RHAND";
				    scr_var.Type = Var_Types.INT;
				    scr_var.Value = (long)Globals.gamedata.my_char.itm_RHand;
				    break;
			    case "CHAR_ITEM_LHAND":
				    scr_var.Name = "CHAR_ITEM_LHAND";
				    scr_var.Type = Var_Types.INT;
				    scr_var.Value = (long)Globals.gamedata.my_char.itm_LHand;
				    break;
			    case "CHAR_ITEM_GLOVES":
				    scr_var.Name = "CHAR_ITEM_GLOVES";
				    scr_var.Type = Var_Types.INT;
				    scr_var.Value = (long)Globals.gamedata.my_char.itm_Gloves;
				    break;
			    case "CHAR_ITEM_CHEST":
				    scr_var.Name = "CHAR_ITEM_CHEST";
				    scr_var.Type = Var_Types.INT;
				    scr_var.Value = (long)Globals.gamedata.my_char.itm_Chest;
				    break;
			    case "CHAR_ITEM_LEG":
				    scr_var.Name = "CHAR_ITEM_LEG";
				    scr_var.Type = Var_Types.INT;
				    scr_var.Value = (long)Globals.gamedata.my_char.itm_Legs;
				    break;
			    case "CHAR_ITEM_BOOTS":
				    scr_var.Name = "CHAR_ITEM_BOOTS";
				    scr_var.Type = Var_Types.INT;
				    scr_var.Value = (long)Globals.gamedata.my_char.itm_Feet;
				    break;
			    case "CHAR_ITEM_CAPE":
				    scr_var.Name = "CHAR_ITEM_CAPE";
				    scr_var.Type = Var_Types.INT;
				    scr_var.Value = (long)Globals.gamedata.my_char.itm_Back;
				    break;
			    case "CHAR_ITEM_HANDS":
				    scr_var.Name = "CHAR_ITEM_HANDS";
				    scr_var.Type = Var_Types.INT;
				    scr_var.Value = (long)Globals.gamedata.my_char.itm_LRHand;
				    break;
			    case "CHAR_ITEM_HAIR":
				    scr_var.Name = "CHAR_ITEM_HAIR";
				    scr_var.Type = Var_Types.INT;
				    scr_var.Value = (long)Globals.gamedata.my_char.itm_Hair;
				    break;
			    case "CHAR_ITEM_FACE":
				    scr_var.Name = "CHAR_ITEM_FACE";
				    scr_var.Type = Var_Types.INT;
				    scr_var.Value = (long)Globals.gamedata.my_char.itm_Face;
				    break;
			    case "CHAR_AUG_RHAND":
				    scr_var.Name = "CHAR_AUG_RHAND";
				    scr_var.Type = Var_Types.INT;
				    scr_var.Value = (long)Globals.gamedata.my_char.aug_RHand;
				    break;
			    case "CHAR_AUG_LHAND":
				    scr_var.Name = "CHAR_AUG_LHAND";
				    scr_var.Type = Var_Types.INT;
				    scr_var.Value = (long)Globals.gamedata.my_char.aug_LHand;
				    break;
			    case "CHAR_AUG_HANDS":
				    scr_var.Name = "CHAR_AUG_HANDS";
				    scr_var.Type = Var_Types.INT;
				    scr_var.Value = (long)Globals.gamedata.my_char.aug_LRHand;
				    break;
                case "PET_NAME":
                    scr_var.Name = "PET_NAME";
                    scr_var.Type = Var_Types.STRING;
                    scr_var.Value = Globals.gamedata.my_pet.Name;
                    break;
                case "PET1_NAME":
                    scr_var.Name = "PET1_NAME";
                    scr_var.Type = Var_Types.STRING;
                    scr_var.Value = Globals.gamedata.my_pet1.Name;
                    break;
                case "PET2_NAME":
                    scr_var.Name = "PET2_NAME";
                    scr_var.Type = Var_Types.STRING;
                    scr_var.Value = Globals.gamedata.my_pet2.Name;
                    break;
                case "PET3_NAME":
                    scr_var.Name = "PET3_NAME";
                    scr_var.Type = Var_Types.STRING;
                    scr_var.Value = Globals.gamedata.my_pet3.Name;
                    break;
                case "PET_TITLE":
                    scr_var.Name = "PET_TITLE";
                    scr_var.Type = Var_Types.STRING;
                    scr_var.Value = Globals.gamedata.my_pet.Title;
                    break;
                case "PET1_TITLE":
                    scr_var.Name = "PET1_TITLE";
                    scr_var.Type = Var_Types.STRING;
                    scr_var.Value = Globals.gamedata.my_pet1.Title;
                    break;
                case "PET2_TITLE":
                    scr_var.Name = "PET2_TITLE";
                    scr_var.Type = Var_Types.STRING;
                    scr_var.Value = Globals.gamedata.my_pet2.Title;
                    break;
                case "PET3_TITLE":
                    scr_var.Name = "PET_TITLE";
                    scr_var.Type = Var_Types.STRING;
                    scr_var.Value = Globals.gamedata.my_pet3.Title;
                    break;
                case "PET_X":
                    scr_var.Name = "PET_X";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet.X;
                    break;
                case "PET1_X":
                    scr_var.Name = "PET1_X";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet1.X;
                    break;
                case "PET2_X":
                    scr_var.Name = "PET2_X";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet2.X;
                    break;
                case "PET3_X":
                    scr_var.Name = "PET3_X";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet3.X;
                    break;
                case "PET_Y":
                    scr_var.Name = "PET_Y";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet.Y;
                    break;
                case "PET1_Y":
                    scr_var.Name = "PET1_Y";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet1.Y;
                    break;
                case "PET2_Y":
                    scr_var.Name = "PET2_Y";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet2.Y;
                    break;
                case "PET3_Y":
                    scr_var.Name = "PET3_Y";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet3.Y;
                    break;
                case "PET_Z":
                    scr_var.Name = "PET_Z";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet.Z;
                    break;
                case "PET1_Z":
                    scr_var.Name = "PET1_Z";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet1.Z;
                    break;
                case "PET2_Z":
                    scr_var.Name = "PET2_Z";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet2.Z;
                    break;
                case "PET3_Z":
                    scr_var.Name = "PET3_Z";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet3.Z;
                    break;
                case "PET_DESTX":
                    scr_var.Name = "PET_DESTX";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet.Dest_X;
                    break;
                case "PET1_DESTX":
                    scr_var.Name = "PET1_DESTX";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet1.Dest_X;
                    break;
                case "PET2_DESTX":
                    scr_var.Name = "PET2_DESTX";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet2.Dest_X;
                    break;
                case "PET3_DESTX":
                    scr_var.Name = "PET3_DESTX";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet3.Dest_X;
                    break;
                case "PET_DESTY":
                    scr_var.Name = "PET_DESTY";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet.Dest_Y;
                    break;
                case "PET1_DESTY":
                    scr_var.Name = "PET1_DESTY";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet1.Dest_Y;
                    break;
                case "PET2_DESTY":
                    scr_var.Name = "PET2_DESTY";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet2.Dest_Y;
                    break;
                case "PET3_DESTY":
                    scr_var.Name = "PET3_DESTY";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet3.Dest_Y;
                    break;
                case "PET_DESTZ":
                    scr_var.Name = "PET_DESTZ";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet.Dest_Z;
                    break;
                case "PET1_DESTZ":
                    scr_var.Name = "PET1_DESTZ";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet1.Dest_Z;
                    break;
                case "PET2_DESTZ":
                    scr_var.Name = "PET2_DESTZ";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet2.Dest_Z;
                    break;
                case "PET3_DESTZ":
                    scr_var.Name = "PET3_DESTZ";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet3.Dest_Z;
                    break;
                case "PET_IS_MOVING":
                    scr_var.Name = "PET_IS_MOVING";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = Globals.gamedata.my_pet.Moving ? 1L : 0L;
                    break;
                case "PET1_IS_MOVING":
                    scr_var.Name = "PET1_IS_MOVING";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = Globals.gamedata.my_pet1.Moving ? 1L : 0L;
                    break;
                case "PET2_IS_MOVING":
                    scr_var.Name = "PET2_IS_MOVING";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = Globals.gamedata.my_pet2.Moving ? 1L : 0L;
                    break;
                case "PET3_IS_MOVING":
                    scr_var.Name = "PET3_IS_MOVING";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = Globals.gamedata.my_pet3.Moving ? 1L : 0L;
                    break;
                case "PET_MAX_HP":
                    scr_var.Name = "PET_MAX_HP";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet.Max_HP;
                    break;
                case "PET1_MAX_HP":
                    scr_var.Name = "PET1_MAX_HP";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet1.Max_HP;
                    break;
                case "PET2_MAX_HP":
                    scr_var.Name = "PET2_MAX_HP";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet2.Max_HP;
                    break;
                case "PET3_MAX_HP":
                    scr_var.Name = "PET3_MAX_HP";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet3.Max_HP;
                    break;
                case "PET_MAX_MP":
                    scr_var.Name = "PET_MAX_MP";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet.Max_MP;
                    break;
                case "PET1_MAX_MP":
                    scr_var.Name = "PET1_MAX_MP";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet1.Max_MP;
                    break;
                case "PET2_MAX_MP":
                    scr_var.Name = "PET2_MAX_MP";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet2.Max_MP;
                    break;
                case "PET3_MAX_MP":
                    scr_var.Name = "PET3_MAX_MP";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet3.Max_MP;
                    break;
                case "PET_MAX_CP":
                    scr_var.Name = "PET_MAX_CP";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet.Max_CP;
                    break;
                case "PET1_MAX_CP":
                    scr_var.Name = "PET1_MAX_CP";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet1.Max_CP;
                    break;
                case "PET2_MAX_CP":
                    scr_var.Name = "PET2_MAX_CP";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet2.Max_CP;
                    break;
                case "PET3_MAX_CP":
                    scr_var.Name = "PET3_MAX_CP";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet3.Max_CP;
                    break;
                case "PET_MAX_LOAD":
                    scr_var.Name = "PET_MAX_LOAD";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet.Max_Load;
                    break;
                case "PET1_MAX_LOAD":
                    scr_var.Name = "PET1_MAX_LOAD";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet1.Max_Load;
                    break;
                case "PET2_MAX_LOAD":
                    scr_var.Name = "PET2_MAX_LOAD";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet2.Max_Load;
                    break;
                case "PET3_MAX_LOAD":
                    scr_var.Name = "PET3_MAX_LOAD";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet3.Max_Load;
                    break;
                case "PET_MAX_FED":
                    scr_var.Name = "PET_MAX_FED";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet.Max_Fed;
                    break;
                case "PET1_MAX_FED":
                    scr_var.Name = "PET1_MAX_FED";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet1.Max_Fed;
                    break;
                case "PET2_MAX_FED":
                    scr_var.Name = "PET2_MAX_FED";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet2.Max_Fed;
                    break;
                case "PET3_MAX_FED":
                    scr_var.Name = "PET3_MAX_FED";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet3.Max_Fed;
                    break;
                case "PET_CUR_HP":
                    scr_var.Name = "PET_CUR_HP";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet.Cur_HP;
                    break;
                case "PET1_CUR_HP":
                    scr_var.Name = "PET1_CUR_HP";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet1.Cur_HP;
                    break;
                case "PET2_CUR_HP":
                    scr_var.Name = "PET2_CUR_HP";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet2.Cur_HP;
                    break;
                case "PET3_CUR_HP":
                    scr_var.Name = "PET3_CUR_HP";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet3.Cur_HP;
                    break;
                case "PET_CUR_MP":
                    scr_var.Name = "PET_CUR_MP";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet.Cur_MP;
                    break;
                case "PET1_CUR_MP":
                    scr_var.Name = "PET1_CUR_MP";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet1.Cur_MP;
                    break;
                case "PET2_CUR_MP":
                    scr_var.Name = "PET2_CUR_MP";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet2.Cur_MP;
                    break;
                case "PET3_CUR_MP":
                    scr_var.Name = "PET3_CUR_MP";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet3.Cur_MP;
                    break;
                case "PET_CUR_CP":
                    scr_var.Name = "PET_CUR_CP";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet.Cur_CP;
                    break;
                case "PET1_CUR_CP":
                    scr_var.Name = "PET1_CUR_CP";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet1.Cur_CP;
                    break;
                case "PET2_CUR_CP":
                    scr_var.Name = "PET2_CUR_CP";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet2.Cur_CP;
                    break;
                case "PET3_CUR_CP":
                    scr_var.Name = "PET3_CUR_CP";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet3.Cur_CP;
                    break;
                case "PET_PER_HP":
                    scr_var.Name = "PET_PER_HP";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)(100.0 * (Globals.gamedata.my_pet.Cur_HP / Globals.gamedata.my_pet.Max_HP));
                    break;
                case "PET1_PER_HP":
                    scr_var.Name = "PET1_PER_HP";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)(100.0 * (Globals.gamedata.my_pet1.Cur_HP / Globals.gamedata.my_pet1.Max_HP));
                    break;
                case "PET2_PER_HP":
                    scr_var.Name = "PET2_PER_HP";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)(100.0 * (Globals.gamedata.my_pet2.Cur_HP / Globals.gamedata.my_pet2.Max_HP));
                    break;
                case "PET3_PER_HP":
                    scr_var.Name = "PET3_PER_HP";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)(100.0 * (Globals.gamedata.my_pet3.Cur_HP / Globals.gamedata.my_pet3.Max_HP));
                    break;
                case "PET_PER_MP":
                    scr_var.Name = "PET_PER_MP";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)(100.0 * (Globals.gamedata.my_pet.Cur_MP / Globals.gamedata.my_pet.Max_MP));
                    break;
                case "PET1_PER_MP":
                    scr_var.Name = "PET1_PER_MP";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)(100.0 * (Globals.gamedata.my_pet1.Cur_MP / Globals.gamedata.my_pet1.Max_MP));
                    break;
                case "PET2_PER_MP":
                    scr_var.Name = "PET2_PER_MP";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)(100.0 * (Globals.gamedata.my_pet2.Cur_MP / Globals.gamedata.my_pet2.Max_MP));
                    break;
                case "PET3_PER_MP":
                    scr_var.Name = "PET3_PER_MP";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)(100.0 * (Globals.gamedata.my_pet3.Cur_MP / Globals.gamedata.my_pet3.Max_MP));
                    break;
                case "PET_PER_CP":
                    scr_var.Name = "PET_PER_CP";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)(100.0 * (Globals.gamedata.my_pet.Cur_CP / Globals.gamedata.my_pet.Max_CP));
                    break;
                case "PET1_PER_CP":
                    scr_var.Name = "PET1_PER_CP";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)(100.0 * (Globals.gamedata.my_pet1.Cur_CP / Globals.gamedata.my_pet1.Max_CP));
                    break;
                case "PET2_PER_CP":
                    scr_var.Name = "PET2_PER_CP";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)(100.0 * (Globals.gamedata.my_pet2.Cur_CP / Globals.gamedata.my_pet2.Max_CP));
                    break;
                case "PET3_PER_CP":
                    scr_var.Name = "PET3_PER_CP";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)(100.0 * (Globals.gamedata.my_pet3.Cur_CP / Globals.gamedata.my_pet3.Max_CP));
                    break;
                case "PET_CUR_LOAD":
                    scr_var.Name = "PET_CUR_LOAD";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet.Cur_Load;
                    break;
                case "PET1_CUR_LOAD":
                    scr_var.Name = "PET1_CUR_LOAD";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet1.Cur_Load;
                    break;
                case "PET2_CUR_LOAD":
                    scr_var.Name = "PET2_CUR_LOAD";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet2.Cur_Load;
                    break;
                case "PET3_CUR_LOAD":
                    scr_var.Name = "PET3_CUR_LOAD";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet3.Cur_Load;
                    break;
                case "PET_CUR_FED":
                    scr_var.Name = "PET_CUR_FED";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet.Cur_Fed;
                    break;
                case "PET1_CUR_FED":
                    scr_var.Name = "PET1_CUR_FED";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet1.Cur_Fed;
                    break;
                case "PET2_CUR_FED":
                    scr_var.Name = "PET2_CUR_FED";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet2.Cur_Fed;
                    break;
                case "PET3_CUR_FED":
                    scr_var.Name = "PET3_CUR_FED";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet3.Cur_Fed;
                    break;
                case "PET_RUN_SPEED":
                    scr_var.Name = "PET_RUN_SPEED";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)(Globals.gamedata.my_pet.WalkSpeed * Globals.gamedata.my_pet.MoveSpeedMult);
                    break;
                case "PET1_RUN_SPEED":
                    scr_var.Name = "PET1_RUN_SPEED";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)(Globals.gamedata.my_pet1.WalkSpeed * Globals.gamedata.my_pet1.MoveSpeedMult);
                    break;
                case "PET2_RUN_SPEED":
                    scr_var.Name = "PET2_RUN_SPEED";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)(Globals.gamedata.my_pet2.WalkSpeed * Globals.gamedata.my_pet2.MoveSpeedMult);
                    break;
                case "PET3_RUN_SPEED":
                    scr_var.Name = "PET3_RUN_SPEED";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)(Globals.gamedata.my_pet3.WalkSpeed * Globals.gamedata.my_pet3.MoveSpeedMult);
                    break;
                case "PET_WALK_SPEED":
                    scr_var.Name = "PET_WALK_SPEED";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)(Globals.gamedata.my_pet.RunSpeed * Globals.gamedata.my_pet.MoveSpeedMult);
                    break;
                case "PET1_WALK_SPEED":
                    scr_var.Name = "PET1_WALK_SPEED";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)(Globals.gamedata.my_pet1.RunSpeed * Globals.gamedata.my_pet1.MoveSpeedMult);
                    break;
                case "PET2_WALK_SPEED":
                    scr_var.Name = "PET2_WALK_SPEED";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)(Globals.gamedata.my_pet2.RunSpeed * Globals.gamedata.my_pet2.MoveSpeedMult);
                    break;
                case "PET3_WALK_SPEED":
                    scr_var.Name = "PET3_WALK_SPEED";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)(Globals.gamedata.my_pet3.RunSpeed * Globals.gamedata.my_pet3.MoveSpeedMult);
                    break;
                case "PET_ATTACK_SPEED":
                    scr_var.Name = "PET_ATTACK_SPEED";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet.PatkSpeed;
                    break;
                case "PET1_ATTACK_SPEED":
                    scr_var.Name = "PET1_ATTACK_SPEED";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet1.PatkSpeed;
                    break;
                case "PET2_ATTACK_SPEED":
                    scr_var.Name = "PET2_ATTACK_SPEED";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet2.PatkSpeed;
                    break;
                case "PET3_ATTACK_SPEED":
                    scr_var.Name = "PET3_ATTACK_SPEED";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet3.PatkSpeed;
                    break;
                case "PET_ATTACK_SPEED_MULT":
                    scr_var.Name = "PET_ATTACK_SPEED_MULT";
                    scr_var.Type = Var_Types.DOUBLE;
                    scr_var.Value = (double)Globals.gamedata.my_pet.AttackSpeedMult;
                    break;
                case "PET1_ATTACK_SPEED_MULT":
                    scr_var.Name = "PET1_ATTACK_SPEED_MULT";
                    scr_var.Type = Var_Types.DOUBLE;
                    scr_var.Value = (double)Globals.gamedata.my_pet1.AttackSpeedMult;
                    break;
                case "PET2_ATTACK_SPEED_MULT":
                    scr_var.Name = "PET2_ATTACK_SPEED_MULT";
                    scr_var.Type = Var_Types.DOUBLE;
                    scr_var.Value = (double)Globals.gamedata.my_pet2.AttackSpeedMult;
                    break;
                case "PET3_ATTACK_SPEED_MULT":
                    scr_var.Name = "PET3_ATTACK_SPEED_MULT";
                    scr_var.Type = Var_Types.DOUBLE;
                    scr_var.Value = (double)Globals.gamedata.my_pet3.AttackSpeedMult;
                    break;
                case "PET_CAST_SPEED":
                    scr_var.Name = "PET_CAST_SPEED";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet.MatkSpeed;
                    break;
                case "PET1_CAST_SPEED":
                    scr_var.Name = "PET1_CAST_SPEED";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet1.MatkSpeed;
                    break;
                case "PET2_CAST_SPEED":
                    scr_var.Name = "PET2_CAST_SPEED";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet2.MatkSpeed;
                    break;
                case "PET3_CAST_SPEED":
                    scr_var.Name = "PET3_CAST_SPEED";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet3.MatkSpeed;
                    break;
                case "PET_ID":
                    scr_var.Name = "PET_ID";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet.ID;
                    break;
                case "PET1_ID":
                    scr_var.Name = "PET1_ID";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet1.ID;
                    break;
                case "PET2_ID":
                    scr_var.Name = "PET2_ID";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet2.ID;
                    break;
                case "PET3_ID":
                    scr_var.Name = "PET3_ID";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet3.ID;
                    break;
                case "PET_TYPE":
                    scr_var.Name = "PET_TYPE";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet.SummonType;
                    break;
                case "PET1_TYPE":
                    scr_var.Name = "PET1_TYPE";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet1.SummonType;
                    break;
                case "PET2_TYPE":
                    scr_var.Name = "PET2_TYPE";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet2.SummonType;
                    break;
                case "PET3_TYPE":
                    scr_var.Name = "PET3_TYPE";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet3.SummonType;
                    break;
                case "PET_NPCTYPE":
                    scr_var.Name = "PET_NPCTYPE";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet.NPCID;
                    break;
                case "PET_FORM":
                    scr_var.Name = "PET_FORM";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet.Form;
                    break;
                case "PET1_FORM":
                    scr_var.Name = "PET1_FORM";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet1.Form;
                    break;
                case "PET2_FORM":
                    scr_var.Name = "PET2_FORM";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet2.Form;
                    break;
                case "PET3_FORM":
                    scr_var.Name = "PET3_FORM";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet3.Form;
                    break;
                case "PET_TARGETID":
                    scr_var.Name = "PET_TARGETID";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet.TargetID;
                    break;
                case "PET1_TARGETID":
                    scr_var.Name = "PET1_TARGETID";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet1.TargetID;
                    break;
                case "PET2_TARGETID":
                    scr_var.Name = "PET2_TARGETID";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet2.TargetID;
                    break;
                case "PET3_TARGETID":
                    scr_var.Name = "PET3_TARGETID";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet3.TargetID;
                    break;
                case "PET_RUNNING":
                    scr_var.Name = "PET_RUNNING";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet.isRunning;
                    break;
                case "PET1_RUNNING":
                    scr_var.Name = "PET1_RUNNING";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet1.isRunning;
                    break;
                case "PET2_RUNNING":
                    scr_var.Name = "PET2_RUNNING";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet2.isRunning;
                    break;
                case "PET3_RUNNING":
                    scr_var.Name = "PET3_RUNNING";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet3.isRunning;
                    break;
                case "PET_LOOKS_DEAD":
                    scr_var.Name = "PET_LOOKS_DEAD";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet.isAlikeDead;
                    break;
                case "PET1_LOOKS_DEAD":
                    scr_var.Name = "PET1_LOOKS_DEAD";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet1.isAlikeDead;
                    break;
                case "PET2_LOOKS_DEAD":
                    scr_var.Name = "PET2_LOOKS_DEAD";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet2.isAlikeDead;
                    break;
                case "PET3_LOOKS_DEAD":
                    scr_var.Name = "PET3_LOOKS_DEAD";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_pet3.isAlikeDead;
                    break;
                case "TARGET_NAME":
                    scr_var.Name = "TARGET_NAME";
                    scr_var.Type = Var_Types.STRING;
                    scr_var.Value = Script_Ops.TARGET_STRING("NAME");
                    break;
                case "TARGET_KARMA":
                    scr_var.Name = "TARGET_KARMA";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = Script_Ops.TARGET_INT("KARMA");
                    break;
                case "TARGET_TITLE":
                    scr_var.Name = "TARGET_TITLE";
                    scr_var.Type = Var_Types.STRING;
                    scr_var.Value = Script_Ops.TARGET_STRING("TITLE");
                    break;
                case "TARGET_CLAN":
                    scr_var.Name = "TARGET_CLAN";
                    scr_var.Type = Var_Types.STRING;
                    scr_var.Value = Script_Ops.TARGET_STRING("CLAN");
                    break;
                case "TARGET_ALLY":
                    scr_var.Name = "TARGET_ALLY";
                    scr_var.Type = Var_Types.STRING;
                    scr_var.Value = Script_Ops.TARGET_STRING("ALLY");
                    break;
                case "TARGET_X":
                    scr_var.Name = "TARGET_X";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = Script_Ops.TARGET_INT("X");
                    break;
                case "TARGET_Y":
                    scr_var.Name = "TARGET_Y";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = Script_Ops.TARGET_INT("Y");
                    break;
                case "TARGET_Z":
                    scr_var.Name = "TARGET_Z";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = Script_Ops.TARGET_INT("Z");
                    break;
                case "TARGET_DESTX":
                    scr_var.Name = "TARGET_DESTX";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = Script_Ops.TARGET_INT("DESTX");
                    break;
                case "TARGET_DESTY":
                    scr_var.Name = "TARGET_DESTY";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = Script_Ops.TARGET_INT("DESTY");
                    break;
                case "TARGET_DESTZ":
                    scr_var.Name = "TARGET_DESTZ";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = Script_Ops.TARGET_INT("DESTZ");
                    break;
                case "TARGET_IS_MOVING":
                    scr_var.Name = "TARGET_IS_MOVING";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = Script_Ops.TARGET_INT("IS_MOVING");
                    break;
                case "TARGET_MAX_HP":
                    scr_var.Name = "TARGET_MAX_HP";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = Script_Ops.TARGET_INT("MAX_HP");
                    break;
                case "TARGET_MAX_MP":
                    scr_var.Name = "TARGET_MAX_MP";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = Script_Ops.TARGET_INT("MAX_MP");
                    break;
                case "TARGET_MAX_CP":
                    scr_var.Name = "TARGET_MAX_CP";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = Script_Ops.TARGET_INT("MAX_CP");
                    break;
                case "TARGET_CUR_HP":
                    scr_var.Name = "TARGET_CUR_HP";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = Script_Ops.TARGET_INT("CUR_HP");
                    break;
                case "TARGET_CUR_MP":
                    scr_var.Name = "TARGET_CUR_MP";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = Script_Ops.TARGET_INT("CUR_MP");
                    break;
                case "TARGET_CUR_CP":
                    scr_var.Name = "TARGET_CUR_CP";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = Script_Ops.TARGET_INT("CUR_CP");
                    break;
                case "TARGET_PER_HP":
                    scr_var.Name = "TARGET_PER_HP";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = Script_Ops.TARGET_INT("PER_HP");
                    break;
                case "TARGET_PER_MP":
                    scr_var.Name = "TARGET_PER_MP";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = Script_Ops.TARGET_INT("PER_MP");
                    break;
                case "TARGET_PER_CP":
                    scr_var.Name = "TARGET_PER_CP";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = Script_Ops.TARGET_INT("PER_CP");
                    break;
                case "TARGET_SPOILED":
                    scr_var.Name = "TARGET_SPOILED";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = Script_Ops.TARGET_INT("SPOILED");
                    break;
                case "TARGET_RUN_SPEED":
                    scr_var.Name = "TARGET_RUN_SPEED";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = Script_Ops.TARGET_INT("RUN_SPEED");
                    break;
                case "TARGET_WALK_SPEED":
                    scr_var.Name = "TARGET_WALK_SPEED";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = Script_Ops.TARGET_INT("WALK_SPEED");
                    break;
                case "TARGET_ATTACK_SPEED":
                    scr_var.Name = "TARGET_ATTACK_SPEED";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = Script_Ops.TARGET_INT("ATTACK_SPEED");
                    break;
                case "TARGET_CAST_SPEED":
                    scr_var.Name = "TARGET_CAST_SPEED";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = Script_Ops.TARGET_INT("CAST_SPEED");
                    break;
                case "TARGET_EVAL":
                    scr_var.Name = "TARGET_EVAL";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = Script_Ops.TARGET_INT("EVAL");
                    break;
                case "TARGET_ID":
                    scr_var.Name = "TARGET_ID";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = Script_Ops.TARGET_INT("ID");
                    break;
                case "TARGET_TARGETID":
                    scr_var.Name = "TARGET_TARGETID";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = Script_Ops.TARGET_INT("TARGETID");
                    break;
                case "TARGET_TYPEID":
                    scr_var.Name = "TARGET_TYPEID";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = Script_Ops.TARGET_INT("TYPEID");
                    break;
                case "TARGET_TYPE":
                    scr_var.Name = "TARGET_TYPE";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.my_char.CurrentTargetType;
                    break;
                case "TARGET_RUNNING":
                    scr_var.Name = "TARGET_RUNNING";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = Script_Ops.TARGET_INT("RUNNING");
                    break;
                case "TARGET_SITTING":
                    scr_var.Name = "TARGET_SITTING";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = Script_Ops.TARGET_INT("SITTING");
                    break;
                case "TARGET_LOOKS_DEAD":
                    scr_var.Name = "TARGET_LOOKS_DEAD";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = Script_Ops.TARGET_INT("LOOKS_DEAD");
                    break;
                case "NEAREST_ITEM_DISTANCE":
                    scr_var.Name = "NEAREST_ITEM_DISTANCE";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = Script_Ops.NEAREST_ITEM_DISTANCE(true);
                    break;
                case "NEAREST_ITEM_ID":
                    scr_var.Name = "NEAREST_ITEM_ID";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = Script_Ops.NEAREST_ITEM_DISTANCE(false);
                    break;
                case "NEAREST_NPC_DISTANCE":
                    scr_var.Name = "NEAREST_NPC_DISTANCE";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = Script_Ops.NEAREST_NPC_DISTANCE(true);
                    break;
                case "NEAREST_NPC_ID":
                    scr_var.Name = "NEAREST_NPC_ID";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = Script_Ops.NEAREST_NPC_DISTANCE(false);
                    break;
                case "NEAREST_PLAYER_DISTANCE":
                    scr_var.Name = "NEAREST_PLAYER_DISTANCE";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = Script_Ops.NEAREST_PLAYER_DISTANCE(true);
                    break;
                case "NEAREST_PLAYER_ID":
                    scr_var.Name = "NEAREST_PLAYER_ID";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = Script_Ops.NEAREST_PLAYER_DISTANCE(false);
                    break;
                case "COUNT_NPC_TARGETME":
                    scr_var.Name = "COUNT_NPC_TARGETME";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = Script_Ops.COUNT("NPC_TARGETME");
                    break;
                case "COUNT_PLAYER_TARGETME":
                    scr_var.Name = "COUNT_PLAYER_TARGETME";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = Script_Ops.COUNT("PLAYER_TARGETME");
                    break;
                case "CHANNEL_ALL":
                    scr_var.Name = "CHANNEL_ALL";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = 0L;
                    break;
                case "CHANNEL_SHOUT":
                    scr_var.Name = "CHANNEL_SHOUT";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = 1L;
                    break;
                case "CHANNEL_PRIVATE":
                    scr_var.Name = "CHANNEL_PRIVATE";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = 2L;
                    break;
                case "CHANNEL_PARTY":
                    scr_var.Name = "CHANNEL_PARTY";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = 3L;
                    break;
                case "CHANNEL_CLAN":
                    scr_var.Name = "CHANNEL_CLAN";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = 4L;
                    break;
                case "CHANNEL_GM":
                    scr_var.Name = "CHANNEL_GM";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = 5L;
                    break;
                case "CHANNEL_PETITION":
                    scr_var.Name = "CHANNEL_PETITION";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = 6L;
                    break;
                case "CHANNEL_PETITIONREPLY":
                    scr_var.Name = "CHANNEL_PETITIONREPLY";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = 7L;
                    break;
                case "CHANNEL_TRADE":
                    scr_var.Name = "CHANNEL_TRADE";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = 8L;
                    break;
                case "CHANNEL_ALLY":
                    scr_var.Name = "CHANNEL_ALLY";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = 9L;
                    break;
                case "CHANNEL_ANNOUNCEMENT":
                    scr_var.Name = "CHANNEL_ANNOUNCEMENT";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = 10L;
                    break;
                case "CHANNEL_BOAT":
                    scr_var.Name = "CHANNEL_BOAT";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = 11L;
                    break;
                case "CHANNEL_PARTYROOM":
                    scr_var.Name = "CHANNEL_PARTYROOM";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = 15L;
                    break;
                case "CHANNEL_PARTYCOMMANDER":
                    scr_var.Name = "CHANNEL_PARTYCOMMANDER";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = 16L;
                    break;
                case "CHANNEL_HERO":
                    scr_var.Name = "CHANNEL_HERO";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = 17L;
                    break;
                case "TYPE_ERROR":
                    scr_var.Name = "TYPE_ERROR";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = -1L;
                    break;
                case "TYPE_NONE":
                    scr_var.Name = "TYPE_NONE";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = 0L;
                    break;
                case "TYPE_SELF":
                    scr_var.Name = "TYPE_SELF";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = 1L;
                    break;
                case "TYPE_PLAYER":
                    scr_var.Name = "TYPE_PLAYER";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = 2L;
                    break;
                case "TYPE_NPC":
                    scr_var.Name = "TYPE_NPC";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = 3L;
                    break;
                case "SHORTCUT_ITEM":
                    scr_var.Name = "SHORTCUT_ITEM";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = 1L;
                    break;
                case "SHORTCUT_SKILL":
                    scr_var.Name = "SHORTCUT_SKILL";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = 2L;
                    break;
                case "SHORTCUT_ACTION":
                    scr_var.Name = "SHORTCUT_ACTION";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = 3L;
                    break;
                case "SHORTCUT_MACRO":
                    scr_var.Name = "SHORTCUT_MACRO";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = 4L;
                    break;
                case "SHORTCUT_RECIPE":
                    scr_var.Name = "SHORTCUT_RECIPE";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = 5L;
                    break;
                case "TICKS_PER_MS":
                    scr_var.Name = "TICKS_PER_MS";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = 10000L;
                    break;
                case "TICKS_PER_S":
                    scr_var.Name = "TICKS_PER_S";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = 10000000L;
                    break;
                case "TICKS_PER_M":
                    scr_var.Name = "TICKS_PER_M";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = 600000000L;
                    break;
                case "TICKS_PER_H":
                    scr_var.Name = "TICKS_PER_H";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = 36000000000L;
                    break;
                case "TICKS_PER_D":
                    scr_var.Name = "TICKS_PER_D";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = 864000000000L;
                    break;
                case "RANDI":
                    scr_var.Name = "RANDI";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.Rando.Next(0, 100);
                    break;
                case "RANDD":
                    scr_var.Name = "RANDD";
                    scr_var.Type = Var_Types.DOUBLE;
                    scr_var.Value = (double)Globals.Rando.NextDouble();
                    break;
                case "NULL":
                    scr_var.Name = "NULL";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = 0L;
                    break;
                case "INT":
                    scr_var.Name = "INT";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = 1L;
                    break;
                case "DOUBLE":
                    scr_var.Name = "DOUBLE";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = 2L;
                    break;
                case "STRING":
                    scr_var.Name = "STRING";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = 3L;
                    break;
                case "FILEWRITER":
                    scr_var.Name = "FILEWRITER";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = 4L;
                    break;
                case "FILEREADER":
                    scr_var.Name = "FILEREADER";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = 5L;
                    break;
                case "ARRAYLIST":
                    scr_var.Name = "ARRAYLIST";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = 6L;
                    break;
                case "SORTEDLIST":
                    scr_var.Name = "SORTEDLIST";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = 7L;
                    break;
                case "STACK":
                    scr_var.Name = "STACK";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = 8L;
                    break;
                case "QUEUE":
                    scr_var.Name = "QUEUE";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = 9L;
                    break;
                case "CLASS":
                    scr_var.Name = "CLASS";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = 10L;
                    break;
                case "BYTEBUFFER":
                    scr_var.Name = "BYTEBUFFER";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = 11L;
                    break;
                case "WINDOW":
                    scr_var.Name = "WINDOW";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = 12L;
                    break;
                case "THREAD":
                    scr_var.Name = "THREAD";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = 13L;
                    break;
                case "PUBLIC":
                    scr_var.Name = "PUBLIC";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = 0L;
                    break;
                case "PRIVATE":
                    scr_var.Name = "PRIVATE";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = 1L;
                    break;
                case "PROTECTED":
                    scr_var.Name = "PROTECTED";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = 2L;
                    break;
                case "PI":
                    scr_var.Name = "PI";
                    scr_var.Type = Var_Types.DOUBLE;
                    scr_var.Value = System.Math.PI;
                    break;
                case "E":
                    scr_var.Name = "E";
                    scr_var.Type = Var_Types.DOUBLE;
                    scr_var.Value = System.Math.E;
                    break;
                case "CMD":
                    scr_var.Name = "CMD";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = 1L;
                    break;
                case "GUI":
                    scr_var.Name = "GUI";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = 2L;
                    break;
                case "HTML":
                    scr_var.Name = "HTML";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = 3L;
                    break;
                case "GDI":
                    scr_var.Name = "GDI";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = 4L;
                    break;
                case "ASTERISK":
                    scr_var.Name = "ASTERISK";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = 0L;
                    break;
                case "ERROR":
                    scr_var.Name = "ERROR";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = 1L;
                    break;
                case "EXCLAMATION":
                    scr_var.Name = "EXCLAMATION";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = 2L;
                    break;
                case "HAND":
                    scr_var.Name = "HAND";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = 3L;
                    break;
                case "INFORMATION":
                    scr_var.Name = "INFORMATION";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = 4L;
                    break;
                case "NONE":
                    scr_var.Name = "NONE";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = 5L;
                    break;
                case "QUESTION":
                    scr_var.Name = "QUESTION";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = 6L;
                    break;
                case "STOP":
                    scr_var.Name = "STOP";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = 7L;
                    break;
                case "WARNING":
                    scr_var.Name = "WARNING";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = 8L;
                    break;
                case "SCRIPTEVENT_CHAT":
                    scr_var.Name = "SCRIPTEVENT_CHAT";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)EventType.Chat;
                    break;
                case "SCRIPTEVENT_SELFDIE":
                    scr_var.Name = "SCRIPTEVENT_SELFDIE";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)EventType.SelfDie;
                    break;
                case "SCRIPTEVENT_SELFREZ":
                    scr_var.Name = "SCRIPTEVENT_SELFREZ";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)EventType.SelfRez;
                    break;
                case "SCRIPTEVENT_SELFENTERCOMBAT":
                    scr_var.Name = "SCRIPTEVENT_SELFENTERCOMBAT";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)EventType.SelfEnterCombat;
                    break;
                case "SCRIPTEVENT_SELFLEAVECOMBAT":
                    scr_var.Name = "SCRIPTEVENT_SELFLEAVECOMBAT";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)EventType.SelfLeaveCombat;
                    break;
                case "SCRIPTEVENT_SELFSTOPMOVE":
                    scr_var.Name = "SCRIPTEVENT_SELFSTOPMOVE";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)EventType.SelfStopMove;
                    break;
                case "SCRIPTEVENT_SELFTARGETED":
                    scr_var.Name = "SCRIPTEVENT_SELFTARGETED";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)EventType.SelfTargeted;
                    break;
                case "SCRIPTEVENT_SELFUNTARGETED":
                    scr_var.Name = "SCRIPTEVENT_SELFUNTARGETED";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)EventType.SelfUnTargeted;
                    break;
                case "SCRIPTEVENT_TARGETDIE":
                    scr_var.Name = "SCRIPTEVENT_TARGETDIE";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)EventType.TargetDie;
                    break;
                case "SCRIPTEVENT_CHATTOBOT":
                    scr_var.Name = "SCRIPTEVENT_CHATTOBOT";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)EventType.ChatToBot;
                    break;
                case "SCRIPTEVENT_UDPRECEIVE":
                    scr_var.Name = "SCRIPTEVENT_UDPRECEIVE";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)EventType.UDPReceive;
                    break;
                case "SCRIPTEVENT_SKILLUSED":
                    scr_var.Name = "SCRIPTEVENT_SKILLUSED";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)EventType.SkillUsed;
                    break;
                case "SCRIPTEVENT_SKILLLAUNCHED":
                    scr_var.Name = "SCRIPTEVENT_SKILLLAUNCHED";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)EventType.SkillLaunched;
                    break;
                case "SCRIPTEVENT_SKILLCANCELED":
                    scr_var.Name = "SCRIPTEVENT_SKILLCANCELED";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)EventType.SkillCanceled;
                    break;
                case "SCRIPTEVENT_OTHERSKILLUSED":
                    scr_var.Name = "SCRIPTEVENT_OTHERSKILLUSED";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)EventType.OtherSkillUsed;
                    break;
                case "SCRIPTEVENT_OTHERSKILLLAUNCHED":
                    scr_var.Name = "SCRIPTEVENT_OTHERSKILLLAUNCHED";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)EventType.OtherSkillLaunched;
                    break;
                case "SCRIPTEVENT_OTHERSKILLCANCELED":
                    scr_var.Name = "SCRIPTEVENT_OTHERSKILLCANCELED";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)EventType.OtherSkillCanceled;
                    break;
                case "SCRIPTEVENT_JOINPARTY":
                    scr_var.Name = "SCRIPTEVENT_JOINPARTY";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)EventType.JoinParty;
                    break;
                case "SCRIPTEVENT_LEAVEPARTY":
                    scr_var.Name = "SCRIPTEVENT_LEAVEPARTY";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)EventType.LeaveParty;
                    break;
                case "SCRIPTEVENT_UDPRECEIVEBB":
                    scr_var.Name = "SCRIPTEVENT_UDPRECEIVEBB";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)EventType.UDPReceiveBB;
                    break;
                case "SCRIPTEVENT_SERVERPACKET":
                    scr_var.Name = "SCRIPTEVENT_SERVERPACKET";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)EventType.ServerPacket;
                    break;
                case "SCRIPTEVENT_SERVERPACKETEX":
                    scr_var.Name = "SCRIPTEVENT_SERVERPACKETEX";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)EventType.ServerPacketEX;
                    break;
                case "SCRIPTEVENT_SYSTEMMESSAGE":
                    scr_var.Name = "SCRIPTEVENT_SYSTEMMESSAGE";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)EventType.SystemMessage;
                    break;
                case "SCRIPTEVENT_PARTYINVITE":
                    scr_var.Name = "SCRIPTEVENT_PARTYINVITE";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)EventType.PartyInvite;
                    break;
                case "SCRIPTEVENT_TRADEINVITE":
                    scr_var.Name = "SCRIPTEVENT_TRADEINVITE";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)EventType.TradeInvite;
                    break;
                case "SCRIPTEVENT_CLIENTPACKET":
                    scr_var.Name = "SCRIPTEVENT_CLIENTPACKET";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)EventType.ClientPacket;
                    break;
                case "SCRIPTEVENT_CLIENTPACKETEX":
                    scr_var.Name = "SCRIPTEVENT_CLIENTPACKETEX";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)EventType.ClientPacketEX;
                    break;
                case "SCRIPTEVENT_SELFPACKET":
                    scr_var.Name = "SCRIPTEVENT_SELFPACKET";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)EventType.SelfPacket;
                    break;
                case "SCRIPTEVENT_SELFPACKETEX":
                    scr_var.Name = "SCRIPTEVENT_SELFPACKETEX";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)EventType.SelfPacketEX;
                    break;
                case "SCRIPTEVENT_AGGRO":
                    scr_var.Name = "SCRIPTEVENT_AGGRO";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)EventType.Aggro;
                    break;
                case "SCRIPTEVENT_CHAREFFECT":
                    scr_var.Name = "SCRIPTEVENT_CHAREFFECT";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)EventType.CharEffect;
                    break;
                case "SCRIPTEVENT_PARTYEFFECT":
                    scr_var.Name = "SCRIPTEVENT_PARTYEFFECT";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)EventType.PartyEffect;
                    break;
                case "SYSTEM_THREADCOUNT":
                    scr_var.Name = "SYSTEM_THREADCOUNT";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Threads.Count;
                    break;
                case "SYSTEM_CURRENTFILE":
                    scr_var.Name = "SYSTEM_CURRENTFILE";
                    scr_var.Type = Var_Types.STRING;
                    //Replace the escape character \ with the realbackslash \\
                    scr_var.Value = ((ScriptThread)Threads[CurrentThread]).Current_File;//.Replace("\\", "\\\\");
                    break;
                case "SYSTEM_HANDLECOUNT":
                    scr_var.Name = "SYSTEM_HANDLECOUNT";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)System.Diagnostics.Process.GetCurrentProcess().HandleCount;
                    break;
                case "SYSTEM_MEMORYUSAGE":
                    scr_var.Name = "SYSTEM_MEMORYUSAGE";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)GC.GetTotalMemory(false);
                    break;
                case "SYSTEM_MEMORYALLOCATED":
                    scr_var.Name = "SYSTEM_MEMORYALLOCATED";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Environment.WorkingSet;
                    break;
                case "SYSTEM_STACKHEIGHT":
                    scr_var.Name = "SYSTEM_STACKHEIGHT";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)StackHeight;
                    break;
                case "SYSTEM_VERSION":
                    scr_var.Name = "SYSTEM_VERSION";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = Util.GetInt64(Globals.Version);
                    break;
                case "SYSTEM_CHRONICLE":
                    scr_var.Name = "SYSTEM_CHRONICLE";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = (long)Globals.gamedata.Chron;
                    break;
                case "ENV_MACHINENAME":
                    scr_var.Name = "ENV_MACHINENAME";
                    scr_var.Type = Var_Types.STRING;
                    scr_var.Value = Environment.MachineName;
                    break;
                case "ENV_USERNAME":
                    scr_var.Name = "ENV_USERNAME";
                    scr_var.Type = Var_Types.STRING;
                    scr_var.Value = Environment.UserName;
                    break;
                case "ENV_PATH":
                    scr_var.Name = "ENV_PATH";
                    scr_var.Type = Var_Types.STRING;
                    scr_var.Value = Globals.PATH;
                    break;
                case "SERVER_ID":
                    scr_var.Name = "SERVER_ID";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = Globals.gamedata.Server_ID;
                    break;
                case "SERVER_KEY":
                    scr_var.Name = "SERVER_KEY";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = Globals.gamedata.Obfuscation_Key;
                    break;
                case "NOW":
                    scr_var.Name = "NOW";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = System.DateTime.Now.Ticks;
                    break;
                case "GAME_TIME":
                    scr_var.Name = "GAME_TIME";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = Globals.gamedata.GameTime;
                    break;
                case "LOGIN_TIME":
                    scr_var.Name = "LOGIN_TIME";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = Globals.gamedata.LoginTime;
                    break;
                case "ZONE":
                    scr_var.Name = "ZONE";
                    scr_var.Type = Var_Types.INT;
                    scr_var.Value = Globals.gamedata.cur_zone;
                    break;
                default:
                    if(name.StartsWith("#I"))
                    {
                        scr_var.Name = name;
                        scr_var.Type = Var_Types.INT;
                        scr_var.Value = Util.GetInt64(oname.Substring(2, oname.Length - 2));
                    }
                    else if (name.StartsWith("#D"))
                    {
                        scr_var.Name = name;
                        scr_var.Type = Var_Types.DOUBLE;
                        scr_var.Value = Util.GetDouble(oname.Substring(2, oname.Length - 2));
                    }
                    else if (name.StartsWith("#$"))
                    {
                        scr_var.Name = name;
                        scr_var.Type = Var_Types.STRING;
                        scr_var.Value = oname.Substring(2, oname.Length - 2);
                    }
                    else if (name.StartsWith("\"#$"))
                    {
                        scr_var.Name = name;
                        scr_var.Type = Var_Types.STRING;
                        scr_var.Value = oname.Substring(3, oname.Length - 4);
                    }
                    else
                    {
                        //try to create a dynamic variable from this...
                        try
                        {
                            if(oname.StartsWith("0x"))
                            {
                                //trying to get a hex value
                                scr_var.Name = oname;
                                scr_var.Type = Var_Types.INT;
                                scr_var.Value = long.Parse(oname.Replace("0x", ""), System.Globalization.NumberStyles.HexNumber);
                            }
                            else if (oname.Contains("."))
                            {
                                //TODO : this doesn't work because the . is interpreted as a class operator

                                //must be a double
                                scr_var.Name = oname;
                                scr_var.Type = Var_Types.DOUBLE;
                                scr_var.Value = double.Parse(oname, System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
                            }
                            else
                            {
                                scr_var.Name = oname;

                                //try
                                //{
                                    //must be an integer
                                    scr_var.Type = Var_Types.INT;
                                    scr_var.Value = long.Parse(oname, System.Globalization.CultureInfo.InvariantCulture.NumberFormat);
                                /*}
                                catch
                                {
                                    //failed to cast to an int... must be a string
                                    scr_var.Type = Var_Types.STRING;
                                    scr_var.Value = oname;
                                }*/

                                //if we catch here it will always succeed and never give an error that the variable couldn't be found...
                            }
                        }
                        catch
                        {
                            scr_var.Name = name;
                            scr_var.Type = Var_Types.STRING;
                            scr_var.Value = "ERROR - VARIABLE NOT FOUND AT LINE " + Line_Pos.ToString();
                            ScriptEngine.Script_Error("VARIABLE " + name + " IS UNDEFINED");
                        }
                    }
                    break;
            }

            return scr_var;
        }
    }
}
