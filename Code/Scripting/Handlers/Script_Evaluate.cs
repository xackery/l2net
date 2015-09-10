using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace L2_login
{
    public partial class ScriptEngine
    {
        private string ReadToken(ref string inp)
        {
            string backup = inp;

            string token = Get_String(ref inp);

            if (isOp(token))
            {
                //we've got a token here
            }
            else
            {
                //we have a variable...
                //need to read over until we hit a token or the end of the line

                bool done = false;

                while (!done)
                {
                    string pre = inp;
                    string tmp = Get_String(ref inp);

                    if (isOp(tmp))
                    {
                        //we found a token...
                        //need to restore our string and get out of here

                        inp = pre;
                        done = true;
                    }
                    else
                    {
                        //if we have a null token... don't bother appending it
                        if (tmp.Length > 0)
                        {
                            //need to get the same value... but as a token, not an evaluated string
                            inp = pre;
                            tmp = Get_StringToken(ref inp);
                            token = token + " " + tmp;
                        }
                    }

                    if (inp.Length == 0)
                    {
                        //hit the end of the line...
                        done = true;
                    }
                }
            }

            return token;
        }

        private bool Evaluate(string inp)
        {
            ScriptVariable outv = new ScriptVariable();
            outv.Type = Var_Types.ASSIGNABLE;

            Assignment(outv, inp);

            switch(outv.Type)
            {
                case Var_Types.ASSIGNABLE:
                    if (System.Convert.ToInt64(outv.Value, System.Globalization.CultureInfo.InvariantCulture) >= 1)
                        return true;
                    else
                        return false;
                case Var_Types.INT:
                    if (System.Convert.ToInt64(outv.Value, System.Globalization.CultureInfo.InvariantCulture) >= 1)
                        return true;
                    else
                        return false;
                case Var_Types.DOUBLE:
                    if (System.Convert.ToDouble(outv.Value, System.Globalization.CultureInfo.InvariantCulture) >= 1)
                        return true;
                    else
                        return false;
                default:
                    ScriptEngine.Script_Error("CANNOT PERFORM BOOLEAN EVALUATION ON TYPE OF " + outv.Type);
                    break;
            }

            return false;
        }

        private void Assignment(ScriptVariable dest, string equation)
        {
            System.Collections.Queue parsed = new System.Collections.Queue();

            while (equation.Length > 0)
            {
                string token = ReadToken(ref equation);
                if (token.Length > 0)
                {
                    parsed.Enqueue(token);
                }
            }

            //now we have all our tokens in an array list...
            //time to evaluate
            ScriptVariable outv = ProcessData(parsed);

            switch (dest.Type)
            {
                case Var_Types.INT:
                    try
                    {
                        dest.Value = System.Convert.ToInt64(outv.Value, System.Globalization.CultureInfo.InvariantCulture);
                    }
                    catch
                    {
                        dest.Value = 0;
                    }
                    break;
                case Var_Types.DOUBLE:
                    try
                    {
                        dest.Value = System.Convert.ToDouble(outv.Value, System.Globalization.CultureInfo.InvariantCulture);
                    }
                    catch
                    {
                        dest.Value = 0D;
                    }
                    break;
                case Var_Types.STRING:
                    try
                    {
                        dest.Value = System.Convert.ToString(outv.Value, System.Globalization.CultureInfo.InvariantCulture);
                    }
                    catch
                    {
                        dest.Value = "error in assingment casting from type of: " + outv.Type.ToString();
                    }
                    break;
                default:
                    dest.Value = outv.Value;
                    dest.Type = outv.Type;
                    break;
            }
        }

        private ScriptVariable ProcessData(System.Collections.Queue equation)
        {
            System.Collections.ArrayList tmpvars = new System.Collections.ArrayList();

            ScriptVariable outv = new ScriptVariable();
            outv.Type = Var_Types.ASSIGNABLE;

            ScriptVariable outi;

            System.Collections.Queue neweq = new System.Collections.Queue();

            //lets process all the paran bullshit first
            while (equation.Count > 0)
            {
                string token1 = equation.Dequeue().ToString();

                if (token1 == "(")
                {
                    int pcnt = 1;

                    System.Collections.Queue subeq = new System.Collections.Queue();

                    while (pcnt != 0)
                    {
                        string ntoken = equation.Dequeue().ToString();

                        if (ntoken == "(")
                        {
                            pcnt++;
                        }
                        if (ntoken == ")")
                        {
                            pcnt--;
                        }
                        if (pcnt != 0)
                        {
                            subeq.Enqueue(ntoken);
                        }
                    }

                    outi = new ScriptVariable();
                    outi.Type = Var_Types.ASSIGNABLE;
                    outi = ProcessData(subeq);

                    outi.Name = Globals.SCRIPT_OUT_VAR + Globals.Rando.Next(int.MaxValue);
                    while (VariableExists(outi.Name))
                    {
                        outi.Name = Globals.SCRIPT_OUT_VAR + Globals.Rando.Next(int.MaxValue);
                    }
                    tmpvars.Add(outi.Name);
                    Add_Variable(outi, StackHeight);

                    neweq.Enqueue(outi.Name);
                }
                else
                {
                    neweq.Enqueue(token1);
                }
            }

            //now we have a queue of pure tokens with no parans
            while (neweq.Count > 0)
            {
                string token1 = neweq.Dequeue().ToString();

                if (neweq.Count == 0)
                {
                    //we only had 1 parameter
                    outv = Get_Var(token1);
                }
                else
                {
                    outi = new ScriptVariable();
                    outi.Type = Var_Types.ASSIGNABLE;

                    string token2 = neweq.Dequeue().ToString();

                    if (isUnaryOp(token1.ToUpperInvariant()))
                    {
                        EvaluateUnary(outi, token1.ToUpperInvariant(), token2);
                    }
                    else if (isBinaryOp(token2.ToUpperInvariant()))
                    {
                        string token3 = neweq.Dequeue().ToString();

                        EvaluateBinary(outi, token2.ToUpperInvariant(), token1, token3);
                    }

                    //add our created value to the stack
                    outi.Name = Globals.SCRIPT_OUT_VAR + Globals.Rando.Next(int.MaxValue);
                    while (VariableExists(outi.Name))
                    {
                        outi.Name = Globals.SCRIPT_OUT_VAR + Globals.Rando.Next(int.MaxValue);
                    }
                    tmpvars.Add(outi.Name);
                    Add_Variable(outi, StackHeight);

                    //now we need to push this variable to the front of our queue via a temporary queue
                    System.Collections.Queue tmpeq = new System.Collections.Queue();
                    tmpeq.Enqueue(outi.Name);
                    while (neweq.Count > 0)
                    {
                        tmpeq.Enqueue(neweq.Dequeue());
                    }
                    neweq = tmpeq;
                }

            }

            //delete all our temporary variables
            foreach (string name in tmpvars)
            {
                Script_DELETE(name);
            }

            return outv;
        }

        private void EvaluateUnary(ScriptVariable dest_ob, string op, string var1)
        {
            long dest_i = 0;
            double dest_d = 0;
            uint id = 0;
            Var_Types cast = Var_Types.NULL;

            switch (op)
            {
                case "VARIABLE_DEFINED":
                case "IS_DEFINED":
                case "[]":
                case "??":
                    if (VariableExists(var1.ToUpperInvariant()))
                    {
                        dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                        cast = Var_Types.INT;
                    }
                    else
                    {
                        dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                        cast = Var_Types.INT;
                    }
                    break;
                case "IS_LOCKED":
                    if (Locks.ContainsKey(var1.ToUpperInvariant()))
                    {
                        dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                        cast = Var_Types.INT;
                    }
                    else
                    {
                        dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                        cast = Var_Types.INT;
                    }
                    break;
                default:
                    ScriptVariable source1_ob = Get_Var(var1);

                    switch (op)
                    {
                        case "":
                            dest_ob = source1_ob;
                            return;
                        case "IS_READY":                              
                            switch(source1_ob.Type)
                            {
                                case Var_Types.INT:                                        
                                    id = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);                                        
                                    break;
                                case Var_Types.STRING:
                                    id = Util.GetSkillID((string)System.Convert.ToString(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture));                                        
                                    break;
                            }
                            if (Globals.gamedata.skills.ContainsKey(id))
                            {
                                UserSkill us = Util.GetSkill(id);

                                if (us.IsReady())
                                {
                                    dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                    cast = Var_Types.INT;
                                }
                                else
                                {
                                    dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                    cast = Var_Types.INT;
                                }
                            }
                            else
                            {
                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                cast = Var_Types.INT;
                            }                    
                            break;
                        case "IS_ITEM_EQUIPPED":
                            bool item_found = false;
                            if (source1_ob.Type == Var_Types.INT)
                            {
                                uint test_id = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);

                                if (Globals.gamedata.my_char.itm_Under == test_id)
                                    item_found = true;
                                if (Globals.gamedata.my_char.itm_REar == test_id)
                                    item_found = true;
                                if (Globals.gamedata.my_char.itm_LEar == test_id)
                                    item_found = true;
                                if (Globals.gamedata.my_char.itm_Neck == test_id)
                                    item_found = true;
                                if (Globals.gamedata.my_char.itm_RFinger == test_id)
                                    item_found = true;
                                if (Globals.gamedata.my_char.itm_LFinger == test_id)
                                    item_found = true;
                                if (Globals.gamedata.my_char.itm_Head == test_id)
                                    item_found = true;
                                if (Globals.gamedata.my_char.itm_RHand == test_id)
                                    item_found = true;
                                if (Globals.gamedata.my_char.itm_LHand == test_id)
                                    item_found = true;
                                if (Globals.gamedata.my_char.itm_Gloves == test_id)
                                    item_found = true;
                                if (Globals.gamedata.my_char.itm_Chest == test_id)
                                    item_found = true;
                                if (Globals.gamedata.my_char.itm_Legs == test_id)
                                    item_found = true;
                                if (Globals.gamedata.my_char.itm_Feet == test_id)
                                    item_found = true;
                                if (Globals.gamedata.my_char.itm_Back == test_id)
                                    item_found = true;
                                if (Globals.gamedata.my_char.itm_LRHand == test_id)
                                    item_found = true;
                                if (Globals.gamedata.my_char.itm_Hair == test_id)
                                    item_found = true;
                                if (Globals.gamedata.my_char.itm_Face == test_id)
                                    item_found = true;
                                if (Globals.gamedata.my_char.itm_rbracelet == test_id)
                                    item_found = true;
                                if (Globals.gamedata.my_char.itm_lbracelet == test_id)
                                    item_found = true;
                                if (Globals.gamedata.my_char.itm_talisman1 == test_id)
                                    item_found = true;
                                if (Globals.gamedata.my_char.itm_talisman2 == test_id)
                                    item_found = true;
                                if (Globals.gamedata.my_char.itm_talisman3 == test_id)
                                    item_found = true;
                                if (Globals.gamedata.my_char.itm_talisman4 == test_id)
                                    item_found = true;
                                if (Globals.gamedata.my_char.itm_talisman5 == test_id)
                                    item_found = true;
                                if (Globals.gamedata.my_char.itm_talisman6 == test_id)
                                    item_found = true;

                                if (item_found == true)
                                {
                                    dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                    cast = Var_Types.INT;
                                }
                                else
                                {
                                    dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                    cast = Var_Types.INT;
                                }
                                break;
                            }
                            break;
                        case "IS_AUG_EQUIPPED":
                            bool item_aug_found = false;
                            if (source1_ob.Type == Var_Types.INT)
                            {
                                uint test_id = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                                uint aug_one = 0;
                                uint aug_two = 0;
                                aug_one = Globals.gamedata.my_char.itm_Under / 65536;
                                aug_two = Globals.gamedata.my_char.itm_Under % 65536;
                                if (aug_one == test_id)
                                    item_aug_found = true;
                                if (aug_two == test_id)
                                    item_aug_found = true;

                                aug_one = Globals.gamedata.my_char.itm_REar / 65536;
                                aug_two = Globals.gamedata.my_char.itm_REar % 65536;
                                if (aug_one == test_id)
                                    item_aug_found = true;
                                if (aug_two == test_id)
                                    item_aug_found = true;

                                aug_one = Globals.gamedata.my_char.itm_LEar / 65536;
                                aug_two = Globals.gamedata.my_char.itm_LEar % 65536;
                                if (aug_one == test_id)
                                    item_aug_found = true;
                                if (aug_two == test_id)
                                    item_aug_found = true;

                                aug_one = Globals.gamedata.my_char.itm_Neck / 65536;
                                aug_two = Globals.gamedata.my_char.itm_Neck % 65536;
                                if (aug_one == test_id)
                                    item_aug_found = true;
                                if (aug_two == test_id)
                                    item_aug_found = true;

                                aug_one = Globals.gamedata.my_char.itm_RFinger / 65536;
                                aug_two = Globals.gamedata.my_char.itm_RFinger % 65536;
                                if (aug_one == test_id)
                                    item_aug_found = true;
                                if (aug_two == test_id)
                                    item_aug_found = true;

                                aug_one = Globals.gamedata.my_char.itm_LFinger / 65536;
                                aug_two = Globals.gamedata.my_char.itm_LFinger % 65536;
                                if (aug_one == test_id)
                                    item_aug_found = true;
                                if (aug_two == test_id)
                                    item_aug_found = true;

                                aug_one = Globals.gamedata.my_char.itm_Head / 65536;
                                aug_two = Globals.gamedata.my_char.itm_Head % 65536;
                                if (aug_one == test_id)
                                    item_aug_found = true;
                                if (aug_two == test_id)
                                    item_aug_found = true;

                                aug_one = Globals.gamedata.my_char.itm_RHand / 65536;
                                aug_two = Globals.gamedata.my_char.itm_RHand % 65536;
                                if (aug_one == test_id)
                                    item_aug_found = true;
                                if (aug_two == test_id)
                                    item_aug_found = true;

                                aug_one = Globals.gamedata.my_char.itm_LHand / 65536;
                                aug_two = Globals.gamedata.my_char.itm_LHand % 65536;
                                if (aug_one == test_id)
                                    item_aug_found = true;
                                if (aug_two == test_id)
                                    item_aug_found = true;

                                aug_one = Globals.gamedata.my_char.itm_Gloves / 65536;
                                aug_two = Globals.gamedata.my_char.itm_Gloves % 65536;
                                if (aug_one == test_id)
                                    item_aug_found = true;
                                if (aug_two == test_id)
                                    item_aug_found = true;

                                aug_one = Globals.gamedata.my_char.itm_Chest / 65536;
                                aug_two = Globals.gamedata.my_char.itm_Chest % 65536;
                                if (aug_one == test_id)
                                    item_aug_found = true;
                                if (aug_two == test_id)
                                    item_aug_found = true;

                                aug_one = Globals.gamedata.my_char.itm_Legs / 65536;
                                aug_two = Globals.gamedata.my_char.itm_Legs % 65536;
                                if (aug_one == test_id)
                                    item_aug_found = true;
                                if (aug_two == test_id)
                                    item_aug_found = true;

                                aug_one = Globals.gamedata.my_char.itm_Feet / 65536;
                                aug_two = Globals.gamedata.my_char.itm_Feet % 65536;
                                if (aug_one == test_id)
                                    item_aug_found = true;
                                if (aug_two == test_id)
                                    item_aug_found = true;

                                aug_one = Globals.gamedata.my_char.itm_Back / 65536;
                                aug_two = Globals.gamedata.my_char.itm_Back % 65536;
                                if (aug_one == test_id)
                                    item_aug_found = true;
                                if (aug_two == test_id)
                                    item_aug_found = true;

                                aug_one = Globals.gamedata.my_char.itm_LRHand / 65536;
                                aug_two = Globals.gamedata.my_char.itm_LRHand % 65536;
                                if (aug_one == test_id)
                                    item_aug_found = true;
                                if (aug_two == test_id)
                                    item_aug_found = true;

                                aug_one = Globals.gamedata.my_char.itm_Hair / 65536;
                                aug_two = Globals.gamedata.my_char.itm_Hair % 65536;
                                if (aug_one == test_id)
                                    item_aug_found = true;
                                if (aug_two == test_id)
                                    item_aug_found = true;

                                aug_one = Globals.gamedata.my_char.itm_Face / 65536;
                                aug_two = Globals.gamedata.my_char.itm_Face % 65536;
                                if (aug_one == test_id)
                                    item_aug_found = true;
                                if (aug_two == test_id)
                                    item_aug_found = true;

                                aug_one = Globals.gamedata.my_char.itm_rbracelet / 65536;
                                aug_two = Globals.gamedata.my_char.itm_rbracelet % 65536;
                                if (aug_one == test_id)
                                    item_aug_found = true;
                                if (aug_two == test_id)
                                    item_aug_found = true;

                                aug_one = Globals.gamedata.my_char.itm_lbracelet / 65536;
                                aug_two = Globals.gamedata.my_char.itm_lbracelet % 65536;
                                if (aug_one == test_id)
                                    item_aug_found = true;
                                if (aug_two == test_id)
                                    item_aug_found = true;

                                aug_one = Globals.gamedata.my_char.itm_talisman1 / 65536;
                                aug_two = Globals.gamedata.my_char.itm_talisman1 % 65536;
                                if (aug_one == test_id)
                                    item_aug_found = true;
                                if (aug_two == test_id)
                                    item_aug_found = true;

                                aug_one = Globals.gamedata.my_char.itm_talisman2 / 65536;
                                aug_two = Globals.gamedata.my_char.itm_talisman2 % 65536;
                                if (aug_one == test_id)
                                    item_aug_found = true;
                                if (aug_two == test_id)
                                    item_aug_found = true;

                                aug_one = Globals.gamedata.my_char.itm_talisman3 / 65536;
                                aug_two = Globals.gamedata.my_char.itm_talisman3 % 65536;
                                if (aug_one == test_id)
                                    item_aug_found = true;
                                if (aug_two == test_id)
                                    item_aug_found = true;

                                aug_one = Globals.gamedata.my_char.itm_talisman4 / 65536;
                                aug_two = Globals.gamedata.my_char.itm_talisman4 % 65536;
                                if (aug_one == test_id)
                                    item_aug_found = true;
                                if (aug_two == test_id)
                                    item_aug_found = true;

                                aug_one = Globals.gamedata.my_char.itm_talisman5 / 65536;
                                aug_two = Globals.gamedata.my_char.itm_talisman5 % 65536;
                                if (aug_one == test_id)
                                    item_aug_found = true;
                                if (aug_two == test_id)
                                    item_aug_found = true;

                                aug_one = Globals.gamedata.my_char.itm_talisman6 / 65536;
                                aug_two = Globals.gamedata.my_char.itm_talisman6 % 65536;
                                if (aug_one == test_id)
                                    item_aug_found = true;
                                if (aug_two == test_id)
                                    item_aug_found = true;
    

                                if (item_aug_found == true)
                                {
                                    dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                    cast = Var_Types.INT;
                                }
                                else
                                {
                                    dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                    cast = Var_Types.INT;
                                }
                                break;
                            }
                            break;
                        case "IS_SHOP":
                            if (source1_ob.Type == Var_Types.INT)
                            {
                                uint test_id = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                                TargetType idType = Util.GetType(test_id);
                                switch (idType)
                                {
                                    case TargetType.NONE:
                                    case TargetType.ERROR:
                                    case TargetType.ITEM:
                                    case TargetType.NPC:
                                    case TargetType.MYPET:
                                    case TargetType.MYPET1:
                                    case TargetType.MYPET2:
                                    case TargetType.MYPET3:
                                        dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                        cast = Var_Types.INT;
                                        break;
                                    case TargetType.SELF:
                                        if (Globals.gamedata.my_char.PrivateStoreType > 0)
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        else
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        break;
                                    case TargetType.PLAYER:
                                        CharInfo player = null;
                                        Globals.PlayerLock.EnterReadLock();
                                        try
                                        {
                                            player = Util.GetChar(test_id);
                                        }
                                        finally
                                        {
                                            Globals.PlayerLock.ExitReadLock();
                                        }
                                        if (player.PrivateStoreType > 0)
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        else
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        break;
                                    default:
                                        Globals.l2net_home.Add_Error("IS_SHOP was not able to resolve the ID passed to it.");
                                        dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                        cast = Var_Types.INT;
                                        break;
                                }
                            }
                            break;
                        case "IS_NOBLE":
                            if (source1_ob.Type == Var_Types.INT)
                            {
                                uint test_id = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                                TargetType idType = Util.GetType(test_id);
                                switch (idType)
                                {
                                    case TargetType.NONE:
                                    case TargetType.ERROR:
                                    case TargetType.ITEM:
                                    case TargetType.NPC:
                                    case TargetType.MYPET:
                                    case TargetType.MYPET1:
                                    case TargetType.MYPET2:
                                    case TargetType.MYPET3:
                                        dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                        cast = Var_Types.INT;
                                        break;
                                    case TargetType.SELF:
                                        if (Globals.gamedata.my_char.HeroIcon > 0)
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        else
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        break;
                                    case TargetType.PLAYER:
                                        CharInfo player = null;
                                        Globals.PlayerLock.EnterReadLock();
                                        try
                                        {
                                            player = Util.GetChar(test_id);
                                        }
                                        finally
                                        {
                                            Globals.PlayerLock.ExitReadLock();
                                        }
                                        if (player.HeroIcon > 0)
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        else
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        break;
                                    default:
                                        Globals.l2net_home.Add_Error("IS_NOBLE was not able to resolve the ID passed to it.");
                                        dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                        cast = Var_Types.INT;
                                        break;
                                }
                            }
                            break;
                        case "IS_HERO":
                            if (source1_ob.Type == Var_Types.INT)
                            {
                                uint test_id = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                                TargetType idType = Util.GetType(test_id);
                                switch (idType)
                                {
                                    case TargetType.NONE:
                                    case TargetType.ERROR:
                                    case TargetType.ITEM:
                                    case TargetType.NPC:
                                    case TargetType.MYPET:
                                    case TargetType.MYPET1:
                                    case TargetType.MYPET2:
                                    case TargetType.MYPET3:
                                        dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                        cast = Var_Types.INT;
                                        break;
                                    case TargetType.SELF:
                                        if (Globals.gamedata.my_char.HeroGlow > 0)
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        else
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        break;
                                    case TargetType.PLAYER:
                                        CharInfo player = null;
                                        Globals.PlayerLock.EnterReadLock();
                                        try
                                        {
                                            player = Util.GetChar(test_id);
                                        }
                                        finally
                                        {
                                            Globals.PlayerLock.ExitReadLock();
                                        }
                                        if (player.HeroGlow > 0)
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        else
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        break;
                                    default:
                                        Globals.l2net_home.Add_Error("IS_HERO  was not able to resolve the ID passed to it.");
                                        dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                        cast = Var_Types.INT;
                                        break;
                                }
                            }
                            break;
                        case "IS_DUELING":
                            if (source1_ob.Type == Var_Types.INT)
                            {
                                uint test_id = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                                TargetType idType = Util.GetType(test_id);
                                switch (idType)
                                {
                                    case TargetType.NONE:
                                    case TargetType.ERROR:
                                    case TargetType.ITEM:                                        
                                    case TargetType.MYPET:
                                    case TargetType.MYPET1:
                                    case TargetType.MYPET2:
                                    case TargetType.MYPET3:
                                        dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                        cast = Var_Types.INT;
                                        break;
                                    case TargetType.SELF:
                                        if (Globals.gamedata.my_char.TeamCircle > 0)
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        else
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        break;
                                    case TargetType.PLAYER:
                                        CharInfo player = null;
                                        Globals.PlayerLock.EnterReadLock();
                                        try
                                        {
                                            player = Util.GetChar(test_id);
                                        }
                                        finally
                                        {
                                            Globals.PlayerLock.ExitReadLock();
                                        }
                                        if (player.TeamCircle > 0)
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        else
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        break;
                                    case TargetType.NPC:
                                        NPCInfo npc = null;
                                        Globals.NPCLock.EnterReadLock();
                                        try
                                        {
                                            npc = Util.GetNPC(test_id);
                                        }
                                        finally
                                        {
                                            Globals.NPCLock.ExitReadLock();
                                        }
                                        if (npc.TeamCircle > 0)
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        else
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        break;
                                    default:
                                        Globals.l2net_home.Add_Error("IS_DUELING  was not able to resolve the ID passed to it.");
                                        dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                        cast = Var_Types.INT;
                                        break;
                                }
                            }
                            break;
                        case "IS_FAKEDEATH":
                            if (source1_ob.Type == Var_Types.INT)
                            {
                                uint test_id = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                                TargetType idType = Util.GetType(test_id);
                                switch (idType)
                                {
                                    case TargetType.NONE:
                                    case TargetType.ERROR:
                                    case TargetType.ITEM:
                                    case TargetType.MYPET:
                                    case TargetType.MYPET1:
                                    case TargetType.MYPET2:
                                    case TargetType.MYPET3:
                                        dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                        cast = Var_Types.INT;
                                        break;
                                    case TargetType.SELF:
                                        if (Globals.gamedata.my_char.isAlikeDead > 0)
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        else
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        break;
                                    case TargetType.PLAYER:
                                        CharInfo player = null;
                                        Globals.PlayerLock.EnterReadLock();
                                        try
                                        {
                                            player = Util.GetChar(test_id);
                                        }
                                        finally
                                        {
                                            Globals.PlayerLock.ExitReadLock();
                                        }
                                        if (player.isAlikeDead > 0)
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        else
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        break;
                                    case TargetType.NPC:
                                        NPCInfo npc = null;
                                        Globals.NPCLock.EnterReadLock();
                                        try
                                        {
                                            npc = Util.GetNPC(test_id);
                                        }
                                        finally
                                        {
                                            Globals.NPCLock.ExitReadLock();
                                        }
                                        if (npc.isAlikeDead > 0)
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        else
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        break;
                                    default:
                                        Globals.l2net_home.Add_Error("IS_FAKEDEATH  was not able to resolve the ID passed to it.");
                                        dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                        cast = Var_Types.INT;
                                        break;
                                }
                            }
                            break;
                        case "IS_INVISIBLE":
                            if (source1_ob.Type == Var_Types.INT)
                            {
                                uint test_id = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                                TargetType idType = Util.GetType(test_id);
                                switch (idType)
                                {
                                    case TargetType.NONE:
                                    case TargetType.ERROR:
                                    case TargetType.ITEM:
                                    case TargetType.MYPET:
                                    case TargetType.MYPET1:
                                    case TargetType.MYPET2:
                                    case TargetType.MYPET3:
                                    case TargetType.NPC: 
                                        dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                        cast = Var_Types.INT;
                                        break;
                                    case TargetType.SELF:
                                        // Check for Hide Buff
                                        if (Globals.gamedata.mybuffs.ContainsKey(922))
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        else
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        break;
                                    case TargetType.PLAYER:
                                        CharInfo player = null;
                                        Globals.PlayerLock.EnterReadLock();
                                        try
                                        {
                                            player = Util.GetChar(test_id);
                                        }
                                        finally
                                        {
                                            Globals.PlayerLock.ExitReadLock();
                                        }
                                        if ((player.Invisible > 0) && (player.HasEffect(AbnormalEffects.STEALTH)))
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        else
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        break;
                                    default:
                                        Globals.l2net_home.Add_Error("IS_INVISIBLE was not able to resolve the ID passed to it.");
                                        dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                        cast = Var_Types.INT;
                                        break;
                                }
                            }
                            break;
                        case "IS_INCOMBAT":
                            if (source1_ob.Type == Var_Types.INT)
                            {
                                uint test_id = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                                TargetType idType = Util.GetType(test_id);
                                switch (idType)
                                {
                                    case TargetType.NONE:
                                    case TargetType.ERROR:
                                    case TargetType.ITEM:
                                        dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                        cast = Var_Types.INT;
                                        break;
                                    case TargetType.MYPET:
                                        if (Globals.gamedata.my_pet.isInCombat > 0)
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        else
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        break;
                                    case TargetType.MYPET1:
                                        if (Globals.gamedata.my_pet1.isInCombat > 0)
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        else
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        break;
                                    case TargetType.MYPET2:
                                        if (Globals.gamedata.my_pet2.isInCombat > 0)
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        else
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        break;
                                    case TargetType.MYPET3:
                                        if (Globals.gamedata.my_pet3.isInCombat > 0)
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        else
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        break;

                                    case TargetType.SELF:
                                        if (Globals.gamedata.my_char.isInCombat > 0)
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        else
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        break;
                                    case TargetType.PLAYER:
                                        CharInfo player = null;
                                        Globals.PlayerLock.EnterReadLock();
                                        try
                                        {
                                            player = Util.GetChar(test_id);
                                        }
                                        finally
                                        {
                                            Globals.PlayerLock.ExitReadLock();
                                        }
                                        if (player.isInCombat > 0)
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        else
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        break;
                                    case TargetType.NPC:
                                        NPCInfo npc = null;
                                        Globals.NPCLock.EnterReadLock();
                                        try
                                        {
                                            npc = Util.GetNPC(test_id);
                                        }
                                        finally
                                        {
                                            Globals.NPCLock.ExitReadLock();
                                        }
                                        if (npc.isInCombat > 0)
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        else
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        break;
                                    default:
                                        Globals.l2net_home.Add_Error("IS_INCOMBAT was not able to resolve the ID passed to it.");
                                        dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                        cast = Var_Types.INT;
                                        break;
                                }
                            }
                            break;
                        case "IS_SITTING":
                            if (source1_ob.Type == Var_Types.INT)
                            {
                                uint test_id = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                                TargetType idType = Util.GetType(test_id);
                                switch (idType)
                                {
                                    case TargetType.NONE:
                                    case TargetType.ERROR:
                                    case TargetType.ITEM:
                                    case TargetType.MYPET:
                                        dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                        cast = Var_Types.INT;
                                        break;
                                    case TargetType.MYPET1:
                                        dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                        cast = Var_Types.INT;
                                        break;
                                    case TargetType.MYPET2:
                                        dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                        cast = Var_Types.INT;
                                        break;
                                    case TargetType.MYPET3:
                                        dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                        cast = Var_Types.INT;
                                        break;
                                    case TargetType.SELF:
                                        if (Globals.gamedata.my_char.isSitting == 0)
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        else
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        break;
                                    case TargetType.PLAYER:
                                        CharInfo player = null;
                                        Globals.PlayerLock.EnterReadLock();
                                        try
                                        {
                                            player = Util.GetChar(test_id);
                                        }
                                        finally
                                        {
                                            Globals.PlayerLock.ExitReadLock();
                                        }
                                        if (player.isSitting == 0)
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        else
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        break;
                                    case TargetType.NPC:
                                        NPCInfo npc = null;
                                        Globals.NPCLock.EnterReadLock();
                                        try
                                        {
                                            npc = Util.GetNPC(test_id);
                                        }
                                        finally
                                        {
                                            Globals.NPCLock.ExitReadLock();
                                        }
                                        if (npc.isSitting > 0)
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        else
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        break;
                                    default:
                                        Globals.l2net_home.Add_Error("IS_SITTING was not able to resolve the ID passed to it.");
                                        dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                        cast = Var_Types.INT;
                                        break;
                                }
                            }
                            break;
                        case "IS_WALKING":
                            if (source1_ob.Type == Var_Types.INT)
                            {
                                uint test_id = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                                TargetType idType = Util.GetType(test_id);
                                switch (idType)
                                {
                                    case TargetType.NONE:
                                    case TargetType.ERROR:
                                    case TargetType.ITEM:
                                    case TargetType.MYPET:
                                        dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                        cast = Var_Types.INT;
                                        break;
                                    case TargetType.MYPET1:
                                        dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                        cast = Var_Types.INT;
                                        break;
                                    case TargetType.MYPET2:
                                        dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                        cast = Var_Types.INT;
                                        break;
                                    case TargetType.MYPET3:
                                        dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                        cast = Var_Types.INT;
                                        break;
                                    case TargetType.SELF:
                                        if (Globals.gamedata.my_char.isRunning == 0)
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        else
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        break;
                                    case TargetType.PLAYER:
                                        CharInfo player = null;
                                        Globals.PlayerLock.EnterReadLock();
                                        try
                                        {
                                            player = Util.GetChar(test_id);
                                        }
                                        finally
                                        {
                                            Globals.PlayerLock.ExitReadLock();
                                        }
                                        if (player.isRunning == 0)
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        else
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        break;
                                    case TargetType.NPC:
                                        NPCInfo npc = null;
                                        Globals.NPCLock.EnterReadLock();
                                        try
                                        {
                                            npc = Util.GetNPC(test_id);
                                        }
                                        finally
                                        {
                                            Globals.NPCLock.ExitReadLock();
                                        }
                                        if (npc.isRunning > 0)
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        else
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        break;
                                    default:
                                        Globals.l2net_home.Add_Error("IS_WALKING was not able to resolve the ID passed to it.");
                                        dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                        cast = Var_Types.INT;
                                        break;
                                }
                            }
                            break;
                        case "IS_FISHING":
                            if (source1_ob.Type == Var_Types.INT)
                            {
                                uint test_id = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                                TargetType idType = Util.GetType(test_id);
                                switch (idType)
                                {
                                    case TargetType.NONE:
                                    case TargetType.ERROR:
                                    case TargetType.ITEM:
                                    case TargetType.MYPET:
                                    case TargetType.MYPET1:
                                    case TargetType.MYPET2:
                                    case TargetType.MYPET3:
                                    case TargetType.NPC:                                            
                                        dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                        cast = Var_Types.INT;
                                        break;
                                    case TargetType.SELF:
                                        if (Globals.gamedata.my_char.isFishing > 0)
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        else
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        break;
                                    case TargetType.PLAYER:
                                        CharInfo player = null;
                                        Globals.PlayerLock.EnterReadLock();
                                        try
                                        {
                                            player = Util.GetChar(test_id);
                                        }
                                        finally
                                        {
                                            Globals.PlayerLock.ExitReadLock();
                                        }
                                        if (player.isFishing > 0)
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        else
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        break;
                                    default:
                                        Globals.l2net_home.Add_Error("IS_FISHING was not able to resolve the ID passed to it.");
                                        dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                        cast = Var_Types.INT;
                                        break;
                                }
                            }
                            break;
                        case "IS_DEMONSWORD":
                            if (source1_ob.Type == Var_Types.INT)
                            {
                                uint test_id = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                                TargetType idType = Util.GetType(test_id);
                                switch (idType)
                                {
                                    case TargetType.NONE:
                                    case TargetType.ERROR:
                                    case TargetType.ITEM:
                                    case TargetType.MYPET:
                                    case TargetType.MYPET1:
                                    case TargetType.MYPET2:
                                    case TargetType.MYPET3:
                                    case TargetType.NPC:                                            
                                        dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                        cast = Var_Types.INT;
                                        break;
                                    case TargetType.SELF:
                                        if (Globals.gamedata.my_char.DemonSword > 0)
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        else
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        break;
                                    case TargetType.PLAYER:
                                        CharInfo player = null;
                                        Globals.PlayerLock.EnterReadLock();
                                        try
                                        {
                                            player = Util.GetChar(test_id);
                                        }
                                        finally
                                        {
                                            Globals.PlayerLock.ExitReadLock();
                                        }
                                        if (player.DemonSword > 0)
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        else
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        break;
                                    default:
                                        Globals.l2net_home.Add_Error("IS_DEMONSWORD was not able to resolve the ID passed to it.");
                                        dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                        cast = Var_Types.INT;
                                        break;
                                }
                            }
                            break;
                        case "IS_TRANSFORMED":
                            if (source1_ob.Type == Var_Types.INT)
                            {
                                uint test_id = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                                TargetType idType = Util.GetType(test_id);
                                switch (idType)
                                {
                                    case TargetType.NONE:
                                    case TargetType.ERROR:
                                    case TargetType.ITEM:
                                    case TargetType.MYPET:
                                    case TargetType.MYPET1:
                                    case TargetType.MYPET2:
                                    case TargetType.MYPET3:
                                    case TargetType.NPC:                                            
                                        dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                        cast = Var_Types.INT;
                                        break;
                                    case TargetType.SELF:
                                        if (Globals.gamedata.my_char.Transform_ID > 0)
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        else
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        break;
                                    case TargetType.PLAYER:
                                        CharInfo player = null;
                                        Globals.PlayerLock.EnterReadLock();
                                        try
                                        {
                                            player = Util.GetChar(test_id);
                                        }
                                        finally
                                        {
                                            Globals.PlayerLock.ExitReadLock();
                                        }
                                        if (player.Transform_ID > 0)
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        else
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        break;
                                    default:
                                        Globals.l2net_home.Add_Error("IS_TRANSFORMED was not able to resolve the ID passed to it.");
                                        dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                        cast = Var_Types.INT;
                                        break;
                                }
                            }
                            break;
                        case "IS_AGATHON":
                            if (source1_ob.Type == Var_Types.INT)
                            {
                                uint test_id = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                                TargetType idType = Util.GetType(test_id);
                                switch (idType)
                                {
                                    case TargetType.NONE:
                                    case TargetType.ERROR:
                                    case TargetType.ITEM:
                                    case TargetType.MYPET:
                                    case TargetType.MYPET1:
                                    case TargetType.MYPET2:
                                    case TargetType.MYPET3:
                                    case TargetType.NPC:                                            
                                        dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                        cast = Var_Types.INT;
                                        break;
                                    case TargetType.SELF:
                                        if (Globals.gamedata.my_char.Agathon_ID > 0)
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        else
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        break;
                                    case TargetType.PLAYER:
                                        CharInfo player = null;
                                        Globals.PlayerLock.EnterReadLock();
                                        try
                                        {
                                            player = Util.GetChar(test_id);
                                        }
                                        finally
                                        {
                                            Globals.PlayerLock.ExitReadLock();
                                        }
                                        if (player.Agathon_ID > 0)
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        else
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        break;
                                    default:
                                        Globals.l2net_home.Add_Error("IS_AGATHION was not able to resolve the ID passed to it.");
                                        dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                        cast = Var_Types.INT;
                                        break;
                                }
                            }
                            break;
                        case "IS_FINDPARTY":
                            if (source1_ob.Type == Var_Types.INT)
                            {
                                uint test_id = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                                TargetType idType = Util.GetType(test_id);
                                switch (idType)
                                {
                                    case TargetType.NONE:
                                    case TargetType.ERROR:
                                    case TargetType.ITEM:
                                    case TargetType.MYPET:
                                    case TargetType.MYPET1:
                                    case TargetType.MYPET2:
                                    case TargetType.MYPET3:
                                    case TargetType.NPC:                                            
                                        dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                        cast = Var_Types.INT;
                                        break;
                                    case TargetType.SELF:
                                        if (Globals.gamedata.my_char.FindParty > 0)
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        else
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        break;
                                    case TargetType.PLAYER:
                                        CharInfo player = null;
                                        Globals.PlayerLock.EnterReadLock();
                                        try
                                        {
                                            player = Util.GetChar(test_id);
                                        }
                                        finally
                                        {
                                            Globals.PlayerLock.ExitReadLock();
                                        }
                                        if (player.FindParty > 0)
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        else
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        break;
                                    default:
                                        Globals.l2net_home.Add_Error("IS_FINDPARTY was not able to resolve the ID passed to it.");
                                        dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                        cast = Var_Types.INT;
                                        break;
                                }
                            }
                            break;
                        case "IS_USINGCUBIC":
                            if (source1_ob.Type == Var_Types.INT)
                            {
                                uint test_id = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                                TargetType idType = Util.GetType(test_id);
                                switch (idType)
                                {
                                    case TargetType.NONE:
                                    case TargetType.ERROR:
                                    case TargetType.ITEM:
                                    case TargetType.MYPET:
                                    case TargetType.MYPET1:
                                    case TargetType.MYPET2:
                                    case TargetType.MYPET3:
                                    case TargetType.NPC:                                            
                                        dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                        cast = Var_Types.INT;
                                        break;
                                    case TargetType.SELF:
                                        if (Globals.gamedata.my_char.CubicCount > 0)
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        else
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        break;
                                    case TargetType.PLAYER:
                                        CharInfo player = null;
                                        Globals.PlayerLock.EnterReadLock();
                                        try
                                        {
                                            player = Util.GetChar(test_id);
                                        }
                                        finally
                                        {
                                            Globals.PlayerLock.ExitReadLock();
                                        }
                                        if (player.CubicCount > 0)
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        else
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        break;
                                    default:
                                        Globals.l2net_home.Add_Error("IS_USINGCUBIC was not able to resolve the ID passed to it.");
                                        dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                        cast = Var_Types.INT;
                                        break;
                                }
                            }
                            break;
                        case "IS_FLAGGED":
                            if (source1_ob.Type == Var_Types.INT)
                            {
                                uint test_id = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                                TargetType idType = Util.GetType(test_id);
                                switch (idType)
                                {
                                    case TargetType.NONE:
                                    case TargetType.ERROR:
                                    case TargetType.ITEM:
                                    case TargetType.NPC:                                            
                                        dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                        cast = Var_Types.INT;
                                        break;
                                    case TargetType.MYPET:
                                        if (Globals.gamedata.my_pet.PvPFlag > 0)
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        else
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        break;
                                    case TargetType.MYPET1:
                                        if (Globals.gamedata.my_pet1.PvPFlag > 0)
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        else
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        break;
                                    case TargetType.MYPET2:
                                        if (Globals.gamedata.my_pet2.PvPFlag > 0)
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        else
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        break;
                                    case TargetType.MYPET3:
                                        if (Globals.gamedata.my_pet3.PvPFlag > 0)
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        else
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        break;
                                    case TargetType.SELF:
                                        if (Globals.gamedata.my_char.PvPFlag > 0)
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        else
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        break;
                                    case TargetType.PLAYER:
                                        CharInfo player = null;
                                        Globals.PlayerLock.EnterReadLock();
                                        try
                                        {
                                            player = Util.GetChar(test_id);
                                        }
                                        finally
                                        {
                                            Globals.PlayerLock.ExitReadLock();
                                        }
                                        if (player.PvPFlag > 0)
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        else
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        break;
                                    default:
                                        Globals.l2net_home.Add_Error("IS_FLAGGED was not able to resolve the ID passed to it.");
                                        dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                        cast = Var_Types.INT;
                                        break;
                                }
                            }
                            break;
                        case "IS_RED":
                            if (source1_ob.Type == Var_Types.INT)
                            {
                                uint test_id = (uint)System.Convert.ToInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                                TargetType idType = Util.GetType(test_id);
                                switch (idType)
                                {
                                    case TargetType.NONE:
                                    case TargetType.ERROR:
                                    case TargetType.ITEM:
                                    case TargetType.NPC:                                            
                                        dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                        cast = Var_Types.INT;
                                        break;
                                    case TargetType.SELF:
                                        if (Globals.gamedata.my_char.Karma < 0)
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        else
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        break;
                                    case TargetType.MYPET:
                                        if (Globals.gamedata.my_pet.Karma < 0)
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        else
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        break;
                                    case TargetType.MYPET1:
                                        if (Globals.gamedata.my_pet1.Karma < 0)
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        else
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        break;
                                    case TargetType.MYPET2:
                                        if (Globals.gamedata.my_pet2.Karma < 0)
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        else
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                        }

                                        break;
                                    case TargetType.MYPET3:
                                        if (Globals.gamedata.my_pet3.Karma < 0)
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        else
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        break;
                                    case TargetType.PLAYER:
                                        CharInfo player = null;
                                        Globals.PlayerLock.EnterReadLock();
                                        try
                                        {
                                            player = Util.GetChar(test_id);
                                        }
                                        finally
                                        {
                                            Globals.PlayerLock.ExitReadLock();
                                        }
                                        if (player.Karma < 0)
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        else
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        break;
                                    default:
                                        Globals.l2net_home.Add_Error("IS_RED was not able to resolve the ID passed to it.");
                                        dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                        cast = Var_Types.INT;
                                        break;
                                }
                            }
                            break;
                        case "IS_PARTYMEMBER":
                            if (source1_ob.Type == Var_Types.INT)
                            {
                                uint test_id = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                                TargetType idType = Util.GetType(test_id);
                                switch (idType)
                                {
                                    case TargetType.NONE:
                                    case TargetType.ERROR:
                                    case TargetType.ITEM:
                                    case TargetType.MYPET:
                                    case TargetType.MYPET1:
                                    case TargetType.MYPET2:
                                    case TargetType.MYPET3:
                                    case TargetType.NPC:
                                    case TargetType.SELF:                                            
                                        dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                        cast = Var_Types.INT;
                                        break;
                                    case TargetType.PLAYER:
                                        CharInfo player = null;
                                        Globals.PlayerLock.EnterReadLock();
                                        try
                                        {
                                            player = Util.GetChar(test_id);
                                        }
                                        finally
                                        {
                                            Globals.PlayerLock.ExitReadLock();
                                        }
                                        if ((player.HasRelation(RelationStates.PARTY1)) ||
                                            (player.HasRelation(RelationStates.PARTY2)) ||
                                            (player.HasRelation(RelationStates.PARTY3)) ||
                                            (player.HasRelation(RelationStates.PARTY4)) ||
                                            (player.HasRelation(RelationStates.PARTYLEADER)))
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        else
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        break;
                                    default:
                                        Globals.l2net_home.Add_Error("IS_PARTYMEMBER was not able to resolve the ID passed to it.");
                                        dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                        cast = Var_Types.INT;
                                        break;
                                }
                            }
                            break;
                        case "IS_PARTYLEADER":
                            if (source1_ob.Type == Var_Types.INT)
                            {
                                uint test_id = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                                TargetType idType = Util.GetType(test_id);
                                switch (idType)
                                {
                                    case TargetType.NONE:
                                    case TargetType.ERROR:
                                    case TargetType.ITEM:
                                    case TargetType.MYPET:
                                    case TargetType.MYPET1:
                                    case TargetType.MYPET2:
                                    case TargetType.MYPET3:
                                    case TargetType.NPC:
                                    case TargetType.SELF:                                            
                                        dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                        cast = Var_Types.INT;
                                        break;
                                    case TargetType.PLAYER:
                                        CharInfo player = null;
                                        Globals.PlayerLock.EnterReadLock();
                                        try
                                        {
                                            player = Util.GetChar(test_id);
                                        }
                                        finally
                                        {
                                            Globals.PlayerLock.ExitReadLock();
                                        }
                                        if (player.HasRelation(RelationStates.PARTYLEADER))
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        else
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        break;
                                    default:
                                        Globals.l2net_home.Add_Error("IS_PARTYLEADER");
                                        dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                        cast = Var_Types.INT;
                                        break;
                                }
                            }
                            break;
                        case "IS_INPARTY":
                            if (source1_ob.Type == Var_Types.INT)
                            {
                                uint test_id = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                                TargetType idType = Util.GetType(test_id);
                                switch (idType)
                                {
                                    case TargetType.NONE:
                                    case TargetType.ERROR:
                                    case TargetType.ITEM:
                                    case TargetType.MYPET:
                                    case TargetType.MYPET1:
                                    case TargetType.MYPET2:
                                    case TargetType.MYPET3:
                                    case TargetType.NPC:
                                    case TargetType.SELF:                                            
                                        dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                        cast = Var_Types.INT;
                                        break;
                                    case TargetType.PLAYER:
                                        CharInfo player = null;
                                        Globals.PlayerLock.EnterReadLock();
                                        try
                                        {
                                            player = Util.GetChar(test_id);
                                        }
                                        finally
                                        {
                                            Globals.PlayerLock.ExitReadLock();
                                        }
                                        if (player.HasRelation(RelationStates.HAS_PARTY))
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        else
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        break;
                                    default:
                                        Globals.l2net_home.Add_Error("IS_INPARTY");
                                        dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                        cast = Var_Types.INT;
                                        break;
                                }
                            }
                            break;
                        case "IS_CLANMEMBER":
                            if (source1_ob.Type == Var_Types.INT)
                            {
                                uint test_id = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                                TargetType idType = Util.GetType(test_id);
                                switch (idType)
                                {
                                    case TargetType.NONE:
                                    case TargetType.ERROR:
                                    case TargetType.ITEM:
                                    case TargetType.MYPET:
                                    case TargetType.MYPET1:
                                    case TargetType.MYPET2:
                                    case TargetType.MYPET3:
                                    case TargetType.NPC:                     
                                        dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                        cast = Var_Types.INT;
                                        break;
                                    case TargetType.SELF:
                                        if (Globals.gamedata.my_char.ClanID > 0)
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        else
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        break;
                                    case TargetType.PLAYER:
                                        CharInfo player = null;
                                        Globals.PlayerLock.EnterReadLock();
                                        try
                                        {
                                            player = Util.GetChar(test_id);
                                        }
                                        finally
                                        {
                                            Globals.PlayerLock.ExitReadLock();
                                        }
                                            
                                        if (player.HasRelation(RelationStates.CLAN_MEMBER))
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        else
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        break;
                                    default:
                                        Globals.l2net_home.Add_Error("IS_CLANMEMBER");
                                        dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                        cast = Var_Types.INT;
                                        break;
                                }
                            }
                            break;
                        case "IS_LEADER":
                            if (source1_ob.Type == Var_Types.INT)
                            {
                                uint test_id = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                                TargetType idType = Util.GetType(test_id);
                                switch (idType)
                                {
                                    case TargetType.NONE:
                                    case TargetType.ERROR:
                                    case TargetType.ITEM:
                                    case TargetType.MYPET:
                                    case TargetType.MYPET1:
                                    case TargetType.MYPET2:
                                    case TargetType.MYPET3:
                                    case TargetType.NPC:
                                        dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                        cast = Var_Types.INT;
                                        break;
                                    case TargetType.SELF:
                                        if ((Globals.gamedata.my_char.MyCharRelation(MyRelation.CROWN) || Globals.gamedata.my_char.MyCharRelation(MyRelation.FLAG)))
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        else
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        break;
                                    case TargetType.PLAYER:
                                        CharInfo player = null;
                                        Globals.PlayerLock.EnterReadLock();
                                        try
                                        {
                                            player = Util.GetChar(test_id);
                                        }
                                        finally
                                        {
                                            Globals.PlayerLock.ExitReadLock();
                                        }
                                        if (player.HasRelation(RelationStates.LEADER))
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        else
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        break;
                                    default:
                                        Globals.l2net_home.Add_Error("IS_LEADER was not able to resolve the ID passed to it.");
                                        dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                        cast = Var_Types.INT;
                                        break;
                                }
                            }
                            break;
                        case "IS_CLANMATE":
                            if (source1_ob.Type == Var_Types.INT)
                            {
                                uint test_id = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                                TargetType idType = Util.GetType(test_id);
                                switch (idType)
                                {
                                    case TargetType.NONE:
                                    case TargetType.ERROR:
                                    case TargetType.ITEM:
                                    case TargetType.MYPET:
                                    case TargetType.MYPET1:
                                    case TargetType.MYPET2:
                                    case TargetType.MYPET3:
                                    case TargetType.NPC:
                                    case TargetType.SELF:                                            
                                        dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                        cast = Var_Types.INT;
                                        break;
                                    case TargetType.PLAYER:
                                        CharInfo player = null;
                                        Globals.PlayerLock.EnterReadLock();
                                        try
                                        {
                                            player = Util.GetChar(test_id);
                                        }
                                        finally
                                        {
                                            Globals.PlayerLock.ExitReadLock();
                                        }
                                        if (player.HasRelation(RelationStates.CLAN_MATE) || (player.ClanID == Globals.gamedata.my_char.ClanID))
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        else
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        break;
                                    default:
                                        Globals.l2net_home.Add_Error("IS_CLANMATE was not able to resolve the ID passed to it.");
                                        dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                        cast = Var_Types.INT;
                                        break;
                                }
                            }
                            break;
                        case "IS_INSIEGE":
                            if (source1_ob.Type == Var_Types.INT)
                            {
                                uint test_id = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                                TargetType idType = Util.GetType(test_id);
                                switch (idType)
                                {
                                    case TargetType.NONE:
                                    case TargetType.ERROR:
                                    case TargetType.ITEM:
                                    case TargetType.MYPET:
                                    case TargetType.MYPET1:
                                    case TargetType.MYPET2:
                                    case TargetType.MYPET3:
                                    case TargetType.NPC:
                                    case TargetType.SELF:                                            
                                        dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                        cast = Var_Types.INT;
                                        break;
                                    case TargetType.PLAYER:
                                        CharInfo player = null;
                                        Globals.PlayerLock.EnterReadLock();
                                        try
                                        {
                                            player = Util.GetChar(test_id);
                                        }
                                        finally
                                        {
                                            Globals.PlayerLock.ExitReadLock();
                                        }
                                        if (player.HasRelation(RelationStates.INSIEGE))
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        else
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        break;
                                    default:
                                        Globals.l2net_home.Add_Error("IS_INSIEGE was not able to resolve the ID passed to it.");
                                        dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                        cast = Var_Types.INT;
                                        break;
                                }
                            }
                            break;
                        case "IS_SIEGEATTACKER":
                            if (source1_ob.Type == Var_Types.INT)
                            {
                                uint test_id = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                                TargetType idType = Util.GetType(test_id);
                                switch (idType)
                                {
                                    case TargetType.NONE:
                                    case TargetType.ERROR:
                                    case TargetType.ITEM:
                                    case TargetType.MYPET:
                                    case TargetType.MYPET1:
                                    case TargetType.MYPET2:
                                    case TargetType.MYPET3:
                                    case TargetType.NPC:                                                                                    
                                        dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                        cast = Var_Types.INT;
                                        break;
                                    case TargetType.SELF:
                                        if (Globals.gamedata.my_char.MyCharRelation(MyRelation.ATTACKER))
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        else
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        break;
                                    case TargetType.PLAYER:
                                        CharInfo player = null;
                                        Globals.PlayerLock.EnterReadLock();
                                        try
                                        {
                                            player = Util.GetChar(test_id);
                                        }
                                        finally
                                        {
                                            Globals.PlayerLock.ExitReadLock();
                                        }
                                        if (player.HasRelation(RelationStates.ATTACKER))
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        else
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        break;
                                    default:
                                        Globals.l2net_home.Add_Error("IS_SIEGEATTACKER was not able to resolve the ID passed to it.");
                                        dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                        cast = Var_Types.INT;
                                        break;
                                }
                            }
                            break;
                        case "IS_SIEGEALLY":
                            if (source1_ob.Type == Var_Types.INT)
                            {
                                uint test_id = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                                TargetType idType = Util.GetType(test_id);
                                switch (idType)
                                {
                                    case TargetType.NONE:
                                    case TargetType.ERROR:
                                    case TargetType.ITEM:
                                    case TargetType.MYPET:
                                    case TargetType.MYPET1:
                                    case TargetType.MYPET2:
                                    case TargetType.MYPET3:
                                    case TargetType.NPC:
                                        dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                        cast = Var_Types.INT;
                                        break;
                                    case TargetType.SELF:
                                        if (Globals.gamedata.my_char.MyCharRelation(MyRelation.DEFENDER))
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        else
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        break;
                                    case TargetType.PLAYER:
                                        CharInfo player = null;
                                        Globals.PlayerLock.EnterReadLock();
                                        try
                                        {
                                            player = Util.GetChar(test_id);
                                        }
                                        finally
                                        {
                                            Globals.PlayerLock.ExitReadLock();
                                        }
                                        if (player.HasRelation(RelationStates.ALLY))
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        else
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        break;
                                    default:
                                        Globals.l2net_home.Add_Error("IS_SIEGEALLY was not able to resolve the ID passed to it.");
                                        dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                        cast = Var_Types.INT;
                                        break;
                                }
                            }
                            break;
                        case "IS_SIEGEENEMY":
                                if (source1_ob.Type == Var_Types.INT)
                                {
                                    uint test_id = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                                    TargetType idType = Util.GetType(test_id);
                                    switch (idType)
                                    {
                                        case TargetType.NONE:
                                        case TargetType.ERROR:
                                        case TargetType.ITEM:
                                        case TargetType.MYPET:
                                        case TargetType.MYPET1:
                                        case TargetType.MYPET2:
                                        case TargetType.MYPET3:
                                        case TargetType.NPC:
                                        case TargetType.SELF:                                            
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                        case TargetType.PLAYER:
                                            CharInfo player = null;
                                            Globals.PlayerLock.EnterReadLock();
                                            try
                                            {
                                                player = Util.GetChar(test_id);
                                            }
                                            finally
                                            {
                                                Globals.PlayerLock.ExitReadLock();
                                            }
                                            if (player.HasRelation(RelationStates.ENEMY))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        default:
                                            Globals.l2net_home.Add_Error("IS_SIEGEENEMY was not able to resolve the ID passed to it.");
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                    }
                                }
                            break;
                        case "IS_MUTUALWAR":
                                if (source1_ob.Type == Var_Types.INT)
                                {
                                    uint test_id = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                                    TargetType idType = Util.GetType(test_id);
                                    switch (idType)
                                    {
                                        case TargetType.NONE:
                                        case TargetType.ERROR:
                                        case TargetType.ITEM:
                                        case TargetType.MYPET:
                                        case TargetType.MYPET1:
                                        case TargetType.MYPET2:
                                        case TargetType.MYPET3:
                                        case TargetType.NPC:
                                        case TargetType.SELF:                                            
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                        case TargetType.PLAYER:
                                            CharInfo player = null;
                                            Globals.PlayerLock.EnterReadLock();
                                            try
                                            {
                                                player = Util.GetChar(test_id);
                                            }
                                            finally
                                            {
                                                Globals.PlayerLock.ExitReadLock();
                                            }
                                            // Globals.l2net_home.Add_Text("Script_Evaluate.cs -> Name: " + player.Name + " ID:" + player.ID + " Relation: " + player.Relation, Globals.Cyan, TextType.BOT);
                                            if (player.HasRelation(RelationStates.MUTUAL_WAR))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        default:
                                            Globals.l2net_home.Add_Error("IS_MUTUALWAR was not able to resolve the ID passed to it.");
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                    }
                                }
                            break;
                        case "IS_ONESIDEDWAR":
                                if (source1_ob.Type == Var_Types.INT)
                                {
                                    uint test_id = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                                    TargetType idType = Util.GetType(test_id);
                                    switch (idType)
                                    {
                                        case TargetType.NONE:
                                        case TargetType.ERROR:
                                        case TargetType.ITEM:
                                        case TargetType.MYPET:
                                        case TargetType.MYPET1:
                                        case TargetType.MYPET2:
                                        case TargetType.MYPET3:
                                        case TargetType.NPC:
                                        case TargetType.SELF:                                            
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                        case TargetType.PLAYER:
                                            CharInfo player = null;
                                            Globals.PlayerLock.EnterReadLock();
                                            try
                                            {
                                                player = Util.GetChar(test_id);
                                            }
                                            finally
                                            {
                                                Globals.PlayerLock.ExitReadLock();
                                            }
                                            if (player.HasRelation(RelationStates.ONESIDED_WAR))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        default:
                                            Globals.l2net_home.Add_Error("IS_ONESIDEWAR was not able to resolve the ID passed to it.");
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                    }
                                }
                            break;
                        case "IS_ALLYMEMBER":
                                if (source1_ob.Type == Var_Types.INT)
                                {
                                    uint test_id = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                                    TargetType idType = Util.GetType(test_id);
                                    switch (idType)
                                    {
                                        case TargetType.NONE:
                                        case TargetType.ERROR:
                                        case TargetType.ITEM:
                                        case TargetType.MYPET:
                                        case TargetType.MYPET1:
                                        case TargetType.MYPET2:
                                        case TargetType.MYPET3:
                                        case TargetType.NPC:                 
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                        case TargetType.SELF:
                                            if (Globals.gamedata.my_char.AllyID > 0)
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.PLAYER:
                                            CharInfo player = null;
                                            Globals.PlayerLock.EnterReadLock();
                                            try
                                            {
                                                player = Util.GetChar(test_id);
                                            }
                                            finally
                                            {
                                                Globals.PlayerLock.ExitReadLock();
                                            }
                                            if (player.HasRelation(RelationStates.ALLY_MEMBER))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        default:
                                            Globals.l2net_home.Add_Error("IS_ALLYMEMBER was not able to resolve the ID passed to it.");
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                    }
                                }
                            break;
                        case "IS_ALLYMMATE":
                                if (source1_ob.Type == Var_Types.INT)
                                {
                                    uint test_id = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                                    TargetType idType = Util.GetType(test_id);
                                    switch (idType)
                                    {
                                        case TargetType.NONE:
                                        case TargetType.ERROR:
                                        case TargetType.ITEM:
                                        case TargetType.MYPET:
                                        case TargetType.MYPET1:
                                        case TargetType.MYPET2:
                                        case TargetType.MYPET3:
                                        case TargetType.NPC:                 
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                        case TargetType.SELF:
                                            if (Globals.gamedata.my_char.AllyID > 0)
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.PLAYER:
                                            CharInfo player = null;
                                            Globals.PlayerLock.EnterReadLock();
                                            try
                                            {
                                                player = Util.GetChar(test_id);
                                            }
                                            finally
                                            {
                                                Globals.PlayerLock.ExitReadLock();
                                            }
                                            if (player.HasRelation(RelationStates.ALLY_MEMBER) && (player.AllyID == Globals.gamedata.my_char.AllyID))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        default:
                                            Globals.l2net_home.Add_Error("IS_ALLYMATE was not able to resolve the ID passed to it.");
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                    }
                                }
                            break;
                        case "IS_TWAR":
                                if (source1_ob.Type == Var_Types.INT)
                                {
                                    uint test_id = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                                    TargetType idType = Util.GetType(test_id);
                                    switch (idType)
                                    {
                                        case TargetType.NONE:
                                        case TargetType.ERROR:
                                        case TargetType.ITEM:
                                        case TargetType.MYPET:
                                        case TargetType.MYPET1:
                                        case TargetType.MYPET2:
                                        case TargetType.MYPET3:
                                        case TargetType.NPC:
                                        case TargetType.SELF:                                            
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                        case TargetType.PLAYER:
                                            CharInfo player = null;
                                            Globals.PlayerLock.EnterReadLock();
                                            try
                                            {
                                                player = Util.GetChar(test_id);
                                            }
                                            finally
                                            {
                                                Globals.PlayerLock.ExitReadLock();
                                            }
                                            if (player.HasRelation(RelationStates.TERRITORY_WAR))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        default:
                                            Globals.l2net_home.Add_Error("IS_TWAR was not able to resolve the ID passed to it.");
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                    }
                                }
                            break;
                        case "IS_BLEEDING":
                                if (source1_ob.Type == Var_Types.INT)
                                {
                                    uint test_id = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                                    TargetType idType = Util.GetType(test_id);
                                    switch (idType)
                                    {
                                        case TargetType.NONE:
                                        case TargetType.ERROR:
                                        case TargetType.ITEM:
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                        case TargetType.MYPET:
                                            //dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            //cast = Var_Types.INT;
                                            //break;
                                            if (Globals.gamedata.my_pet.HasEffect(AbnormalEffects.BLEEDING))
                                                                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.MYPET1:
                                            //dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            //cast = Var_Types.INT;
                                            //break;
                                            if (Globals.gamedata.my_pet1.HasEffect(AbnormalEffects.BLEEDING))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.MYPET2:
                                            //dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            //cast = Var_Types.INT;
                                            //break;
                                            if (Globals.gamedata.my_pet2.HasEffect(AbnormalEffects.BLEEDING))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.MYPET3:
                                            //dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            //cast = Var_Types.INT;
                                            //break;
                                            if (Globals.gamedata.my_pet3.HasEffect(AbnormalEffects.BLEEDING))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.SELF:
                                            if (Globals.gamedata.my_char.HasEffect(AbnormalEffects.BLEEDING))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.PLAYER:
                                            CharInfo player = null;
                                            Globals.PlayerLock.EnterReadLock();
                                            try
                                            {
                                                player = Util.GetChar(test_id);
                                            }
                                            finally
                                            {
                                                Globals.PlayerLock.ExitReadLock();
                                            }
                                            if (player.HasEffect(AbnormalEffects.BLEEDING))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.NPC:
                                            NPCInfo npc = null;
                                            Globals.NPCLock.EnterReadLock();
                                            try
                                            {
                                                npc = Util.GetNPC(test_id);
                                            }
                                            finally
                                            {
                                                Globals.NPCLock.ExitReadLock();
                                            }
                                            if (npc.HasEffect(AbnormalEffects.BLEEDING))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        default:
                                            Globals.l2net_home.Add_Error("IS_BLEEDING was not able to resolve the ID passed to it.");
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                    }
                                }
                            break;
                        case "IS_POISONED":
                                if (source1_ob.Type == Var_Types.INT)
                                {
                                    uint test_id = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                                    TargetType idType = Util.GetType(test_id);

                                    switch (idType)
                                    {
                                        case TargetType.NONE:
                                        case TargetType.ERROR:
                                        case TargetType.ITEM:
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                        case TargetType.MYPET:
                                            //dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            //cast = Var_Types.INT;
                                            //break;
                                            if (Globals.gamedata.my_pet.HasEffect(AbnormalEffects.POISON))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.MYPET1:
                                            if (Globals.gamedata.my_pet1.HasEffect(AbnormalEffects.POISON))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.MYPET2:
                                            //dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            //cast = Var_Types.INT;
                                            //break;
                                            if (Globals.gamedata.my_pet2.HasEffect(AbnormalEffects.POISON))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.MYPET3:
                                            //dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            //cast = Var_Types.INT;
                                            //break;
                                            if (Globals.gamedata.my_pet3.HasEffect(AbnormalEffects.POISON))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.SELF:
                                            if (Globals.gamedata.my_char.HasEffect(AbnormalEffects.POISON))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.PLAYER:
                                            CharInfo player = null;
                                            Globals.PlayerLock.EnterReadLock();
                                            try
                                            {
                                                player = Util.GetChar(test_id);
                                            }
                                            finally
                                            {
                                                Globals.PlayerLock.ExitReadLock();
                                            }
                                            if (player.HasEffect(AbnormalEffects.POISON))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.NPC:
                                            NPCInfo npc = null;
                                            Globals.NPCLock.EnterReadLock();
                                            try
                                            {
                                                npc = Util.GetNPC(test_id);
                                            }
                                            finally
                                            {
                                                Globals.NPCLock.ExitReadLock();
                                            }
                                            if (npc.HasEffect(AbnormalEffects.POISON))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        default:
                                            Globals.l2net_home.Add_Error("IS_POISONED was not able to resolve the ID passed to it.");
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                    }
                                }
                            break;
                        case "IS_REDCIRCLE":
                                if (source1_ob.Type == Var_Types.INT)
                                {
                                    uint test_id = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                                    TargetType idType = Util.GetType(test_id);

                                    switch (idType)
                                    {
                                        case TargetType.NONE:
                                        case TargetType.ERROR:
                                        case TargetType.ITEM:
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                        case TargetType.MYPET:
                                            if (Globals.gamedata.my_pet.HasEffect(AbnormalEffects.REDCIRCLE))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.MYPET1:
                                            if (Globals.gamedata.my_pet1.HasEffect(AbnormalEffects.REDCIRCLE))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.MYPET2:
                                            if (Globals.gamedata.my_pet2.HasEffect(AbnormalEffects.REDCIRCLE))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.MYPET3:
                                            if (Globals.gamedata.my_pet3.HasEffect(AbnormalEffects.REDCIRCLE))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.SELF:
                                            if (Globals.gamedata.my_char.HasEffect(AbnormalEffects.REDCIRCLE))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.PLAYER:
                                            CharInfo player = null;
                                            Globals.PlayerLock.EnterReadLock();
                                            try
                                            {
                                                player = Util.GetChar(test_id);
                                            }
                                            finally
                                            {
                                                Globals.PlayerLock.ExitReadLock();
                                            }
                                            if (player.HasEffect(AbnormalEffects.REDCIRCLE))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.NPC:
                                            NPCInfo npc = null;
                                            Globals.NPCLock.EnterReadLock();
                                            try
                                            {
                                                npc = Util.GetNPC(test_id);
                                            }
                                            finally
                                            {
                                                Globals.NPCLock.ExitReadLock();
                                            }
                                            if (npc.HasEffect(AbnormalEffects.REDCIRCLE))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        default:
                                            Globals.l2net_home.Add_Error("IS_REDCIRCLE was not able to resolve the ID passed to it.");
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                    }
                                }
                            break;
                        case "IS_ICE":
                                if (source1_ob.Type == Var_Types.INT)
                                {
                                    uint test_id = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                                    TargetType idType = Util.GetType(test_id);

                                    switch (idType)
                                    {
                                        case TargetType.NONE:
                                        case TargetType.ERROR:
                                        case TargetType.ITEM:
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                        case TargetType.MYPET:
                                            if (Globals.gamedata.my_pet.HasEffect(AbnormalEffects.ICE))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.MYPET1:
                                            if (Globals.gamedata.my_pet1.HasEffect(AbnormalEffects.ICE))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.MYPET2:
                                            if (Globals.gamedata.my_pet2.HasEffect(AbnormalEffects.ICE))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.MYPET3:
                                            if (Globals.gamedata.my_pet3.HasEffect(AbnormalEffects.ICE))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.SELF:
                                            if (Globals.gamedata.my_char.HasEffect(AbnormalEffects.ICE))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.PLAYER:
                                            CharInfo player = null;
                                            Globals.PlayerLock.EnterReadLock();
                                            try
                                            {
                                                player = Util.GetChar(test_id);
                                            }
                                            finally
                                            {
                                                Globals.PlayerLock.ExitReadLock();
                                            }
                                            if (player.HasEffect(AbnormalEffects.ICE))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.NPC:
                                            NPCInfo npc = null;
                                            Globals.NPCLock.EnterReadLock();
                                            try
                                            {
                                                npc = Util.GetNPC(test_id);
                                            }
                                            finally
                                            {
                                                Globals.NPCLock.ExitReadLock();
                                            }
                                            if (npc.HasEffect(AbnormalEffects.ICE))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        default:
                                            Globals.l2net_home.Add_Error("IS_ICE was not able to resolve the ID passed to it.");
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                    }
                                }
                            break;
                        case "IS_WIND":
                                if (source1_ob.Type == Var_Types.INT)
                                {
                                    uint test_id = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                                    TargetType idType = Util.GetType(test_id);

                                    switch (idType)
                                    {
                                        case TargetType.NONE:
                                        case TargetType.ERROR:
                                        case TargetType.ITEM:
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                        case TargetType.MYPET:
                                            if (Globals.gamedata.my_pet.HasEffect(AbnormalEffects.WIND))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.MYPET1:
                                            if (Globals.gamedata.my_pet1.HasEffect(AbnormalEffects.WIND))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.MYPET2:
                                            if (Globals.gamedata.my_pet2.HasEffect(AbnormalEffects.WIND))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.MYPET3:
                                            if (Globals.gamedata.my_pet3.HasEffect(AbnormalEffects.WIND))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.SELF:
                                            if (Globals.gamedata.my_char.HasEffect(AbnormalEffects.WIND))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.PLAYER:
                                            CharInfo player = null;
                                            Globals.PlayerLock.EnterReadLock();
                                            try
                                            {
                                                player = Util.GetChar(test_id);
                                            }
                                            finally
                                            {
                                                Globals.PlayerLock.ExitReadLock();
                                            }
                                            if (player.HasEffect(AbnormalEffects.WIND))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.NPC:
                                            NPCInfo npc = null;
                                            Globals.NPCLock.EnterReadLock();
                                            try
                                            {
                                                npc = Util.GetNPC(test_id);
                                            }
                                            finally
                                            {
                                                Globals.NPCLock.ExitReadLock();
                                            }
                                            if (npc.HasEffect(AbnormalEffects.WIND))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        default:
                                            Globals.l2net_home.Add_Error("IS_WIND was not able to resolve the ID passed to it.");
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                    }
                                }
                            break;
                        case "IS_AFRAID":
                                if (source1_ob.Type == Var_Types.INT)
                                {
                                    uint test_id = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                                    TargetType idType = Util.GetType(test_id);

                                    switch (idType)
                                    {
                                        case TargetType.NONE:
                                        case TargetType.ERROR:
                                        case TargetType.ITEM:
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                        case TargetType.MYPET:
                                            if (Globals.gamedata.my_pet.HasEffect(AbnormalEffects.FEAR) ||
                                                Globals.gamedata.my_pet.HasEffect(AbnormalEffects.SKULL_FEAR))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.MYPET1:
                                            if (Globals.gamedata.my_pet1.HasEffect(AbnormalEffects.FEAR) ||
                                                Globals.gamedata.my_pet1.HasEffect(AbnormalEffects.SKULL_FEAR))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.MYPET2:
                                            if (Globals.gamedata.my_pet2.HasEffect(AbnormalEffects.FEAR) ||
                                                Globals.gamedata.my_pet2.HasEffect(AbnormalEffects.SKULL_FEAR))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.MYPET3:
                                            if (Globals.gamedata.my_pet3.HasEffect(AbnormalEffects.FEAR) ||
                                                Globals.gamedata.my_pet3.HasEffect(AbnormalEffects.SKULL_FEAR))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.SELF:
                                            if (Globals.gamedata.my_char.HasEffect(AbnormalEffects.FEAR) ||
                                                Globals.gamedata.my_char.HasEffect(AbnormalEffects.SKULL_FEAR))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.PLAYER:
                                            CharInfo player = null;
                                            Globals.PlayerLock.EnterReadLock();
                                            try
                                            {
                                                player = Util.GetChar(test_id);
                                            }
                                            finally
                                            {
                                                Globals.PlayerLock.ExitReadLock();
                                            }
                                            if (player.HasEffect(AbnormalEffects.FEAR) ||
                                                player.HasEffect(AbnormalEffects.SKULL_FEAR))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.NPC:
                                            NPCInfo npc = null;
                                            Globals.NPCLock.EnterReadLock();
                                            try
                                            {
                                                npc = Util.GetNPC(test_id);
                                            }
                                            finally
                                            {
                                                Globals.NPCLock.ExitReadLock();
                                            }
                                            if (npc.HasEffect(AbnormalEffects.FEAR) ||
                                                npc.HasEffect(AbnormalEffects.SKULL_FEAR))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        default:
                                            Globals.l2net_home.Add_Error("IS_AFRAID was not able to resolve the ID passed to it.");
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                    }
                                }
                            break;
                        case "IS_STUNNED":
                                if (source1_ob.Type == Var_Types.INT)
                                {
                                    uint test_id = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                                    TargetType idType = Util.GetType(test_id);

                                    switch (idType)
                                    {
                                        case TargetType.NONE:
                                        case TargetType.ERROR:
                                        case TargetType.ITEM:
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                        case TargetType.MYPET:
                                            if (Globals.gamedata.my_pet.HasEffect(AbnormalEffects.STUN))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.MYPET1:
                                            if (Globals.gamedata.my_pet1.HasEffect(AbnormalEffects.STUN))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.MYPET2:
                                            if (Globals.gamedata.my_pet2.HasEffect(AbnormalEffects.STUN))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.MYPET3:
                                            if (Globals.gamedata.my_pet3.HasEffect(AbnormalEffects.STUN))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.SELF:
                                            if (Globals.gamedata.my_char.HasEffect(AbnormalEffects.STUN))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.PLAYER:
                                            CharInfo player = null;
                                            Globals.PlayerLock.EnterReadLock();
                                            try
                                            {
                                                player = Util.GetChar(test_id);
                                            }
                                            finally
                                            {
                                                Globals.PlayerLock.ExitReadLock();
                                            }
                                            if (player.HasEffect(AbnormalEffects.STUN))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.NPC:
                                            NPCInfo npc = null;
                                            Globals.NPCLock.EnterReadLock();
                                            try
                                            {
                                                npc = Util.GetNPC(test_id);
                                            }
                                            finally
                                            {
                                                Globals.NPCLock.ExitReadLock();
                                            }
                                            if (npc.HasEffect(AbnormalEffects.STUN))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        default:
                                            Globals.l2net_home.Add_Error("IS_STUNNED was not able to resolve the ID passed to it.");
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                    }
                                }
                            break;
                        case "IS_ASLEEP":
                                if (source1_ob.Type == Var_Types.INT)
                                {
                                    uint test_id = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                                    TargetType idType = Util.GetType(test_id);

                                    switch (idType)
                                    {
                                        case TargetType.NONE:
                                        case TargetType.ERROR:
                                        case TargetType.ITEM:
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                        case TargetType.MYPET:
                                            if (Globals.gamedata.my_pet.HasEffect(AbnormalEffects.SLEEP))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.MYPET1:
                                            if (Globals.gamedata.my_pet1.HasEffect(AbnormalEffects.SLEEP))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.MYPET2:
                                            if (Globals.gamedata.my_pet2.HasEffect(AbnormalEffects.SLEEP))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.MYPET3:
                                            if (Globals.gamedata.my_pet3.HasEffect(AbnormalEffects.SLEEP))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.SELF:
                                            if (Globals.gamedata.my_char.HasEffect(AbnormalEffects.SLEEP))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.PLAYER:
                                            CharInfo player = null;
                                            Globals.PlayerLock.EnterReadLock();
                                            try
                                            {
                                                player = Util.GetChar(test_id);
                                            }
                                            finally
                                            {
                                                Globals.PlayerLock.ExitReadLock();
                                            }
                                            if (player.HasEffect(AbnormalEffects.SLEEP))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.NPC:
                                            NPCInfo npc = null;
                                            Globals.NPCLock.EnterReadLock();
                                            try
                                            {
                                                npc = Util.GetNPC(test_id);
                                            }
                                            finally
                                            {
                                                Globals.NPCLock.ExitReadLock();
                                            }
                                            if (npc.HasEffect(AbnormalEffects.SLEEP))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        default:
                                            Globals.l2net_home.Add_Error("IS_ASLEEP was not able to resolve the ID passed to it.");
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                    }
                                }
                            break;
                        case "IS_MUTE":
                                if (source1_ob.Type == Var_Types.INT)
                                {
                                    uint test_id = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                                    TargetType idType = Util.GetType(test_id);

                                    switch (idType)
                                    {
                                        case TargetType.NONE:
                                        case TargetType.ERROR:
                                        case TargetType.ITEM:
                                        case TargetType.MYPET:
                                        case TargetType.MYPET1:
                                        case TargetType.MYPET2:
                                        case TargetType.MYPET3:
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                        case TargetType.SELF:
                                            if (Globals.gamedata.my_char.HasEffect(AbnormalEffects.MUTED))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.PLAYER:
                                            CharInfo player = null;
                                            Globals.PlayerLock.EnterReadLock();
                                            try
                                            {
                                                player = Util.GetChar(test_id);
                                            }
                                            finally
                                            {
                                                Globals.PlayerLock.ExitReadLock();
                                            }
                                            if (player.HasEffect(AbnormalEffects.MUTED))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.NPC:
                                            NPCInfo npc = null;
                                            Globals.NPCLock.EnterReadLock();
                                            try
                                            {
                                                npc = Util.GetNPC(test_id);
                                            }
                                            finally
                                            {
                                                Globals.NPCLock.ExitReadLock();
                                            }
                                            if (npc.HasEffect(AbnormalEffects.MUTED))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        default:
                                            Globals.l2net_home.Add_Error("IS_MUTE was not able to resolve the ID passed to it.");
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                    }
                                }
                            break;
                        case "IS_ROOTED":
                                if (source1_ob.Type == Var_Types.INT)
                                {
                                    uint test_id = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                                    TargetType idType = Util.GetType(test_id);

                                    switch (idType)
                                    {
                                        case TargetType.NONE:
                                        case TargetType.ERROR:
                                        case TargetType.ITEM:
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                        case TargetType.MYPET:
                                            if (Globals.gamedata.my_pet.HasEffect(AbnormalEffects.ROOT))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.MYPET1:
                                            if (Globals.gamedata.my_pet1.HasEffect(AbnormalEffects.ROOT))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.MYPET2:
                                            if (Globals.gamedata.my_pet2.HasEffect(AbnormalEffects.ROOT))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.MYPET3:
                                            if (Globals.gamedata.my_pet3.HasEffect(AbnormalEffects.ROOT))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.SELF:
                                            if (Globals.gamedata.my_char.HasEffect(AbnormalEffects.ROOT))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.PLAYER:
                                            CharInfo player = null;
                                            Globals.PlayerLock.EnterReadLock();
                                            try
                                            {
                                                player = Util.GetChar(test_id);
                                            }
                                            finally
                                            {
                                                Globals.PlayerLock.ExitReadLock();
                                            }
                                            if (player.HasEffect(AbnormalEffects.ROOT))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.NPC:
                                            NPCInfo npc = null;
                                            Globals.NPCLock.EnterReadLock();
                                            try
                                            {
                                                npc = Util.GetNPC(test_id);
                                            }
                                            finally
                                            {
                                                Globals.NPCLock.ExitReadLock();
                                            }
                                            if (npc.HasEffect(AbnormalEffects.ROOT))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        default:
                                            Globals.l2net_home.Add_Error("IS_ROOTED was not able to resolve the ID passed to it.");
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                    }
                                }
                            break;
                        case "IS_PARALYZED":
                                if (source1_ob.Type == Var_Types.INT)
                                {
                                    uint test_id = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                                    TargetType idType = Util.GetType(test_id);

                                    switch (idType)
                                    {
                                        case TargetType.NONE:
                                        case TargetType.ERROR:
                                        case TargetType.ITEM:
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                        case TargetType.MYPET:
                                            if (Globals.gamedata.my_pet.HasEffect(AbnormalEffects.HOLD_1))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.MYPET1:
                                            if (Globals.gamedata.my_pet1.HasEffect(AbnormalEffects.HOLD_1))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.MYPET2:
                                            if (Globals.gamedata.my_pet2.HasEffect(AbnormalEffects.HOLD_1))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.MYPET3:
                                            if (Globals.gamedata.my_pet3.HasEffect(AbnormalEffects.HOLD_1))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.SELF:
                                            if (Globals.gamedata.my_char.HasEffect(AbnormalEffects.HOLD_1))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.PLAYER:
                                            CharInfo player = null;
                                            Globals.PlayerLock.EnterReadLock();
                                            try
                                            {
                                                player = Util.GetChar(test_id);
                                            }
                                            finally
                                            {
                                                Globals.PlayerLock.ExitReadLock();
                                            }
                                            if (player.HasEffect(AbnormalEffects.HOLD_1))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.NPC:
                                            NPCInfo npc = null;
                                            Globals.NPCLock.EnterReadLock();
                                            try
                                            {
                                                npc = Util.GetNPC(test_id);
                                            }
                                            finally
                                            {
                                                Globals.NPCLock.ExitReadLock();
                                            }
                                            if (npc.HasEffect(AbnormalEffects.HOLD_1))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        default:
                                            Globals.l2net_home.Add_Error("IS_PARALYZED was not able to resolve the ID passed to it.");
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                    }
                                }
                            break;
                        case "IS_PETRIFIED":
                                if (source1_ob.Type == Var_Types.INT)
                                {
                                    uint test_id = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                                    TargetType idType = Util.GetType(test_id);

                                    switch (idType)
                                    {
                                        case TargetType.NONE:
                                        case TargetType.ERROR:
                                        case TargetType.ITEM:
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                        case TargetType.MYPET:
                                            if (Globals.gamedata.my_pet.HasEffect(AbnormalEffects.PETRIFIED))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.MYPET1:
                                            if (Globals.gamedata.my_pet1.HasEffect(AbnormalEffects.PETRIFIED))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.MYPET2:
                                            if (Globals.gamedata.my_pet2.HasEffect(AbnormalEffects.PETRIFIED))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.MYPET3:
                                            if (Globals.gamedata.my_pet3.HasEffect(AbnormalEffects.PETRIFIED))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.SELF:
                                            if (Globals.gamedata.my_char.HasEffect(AbnormalEffects.PETRIFIED))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.PLAYER:
                                            CharInfo player = null;
                                            Globals.PlayerLock.EnterReadLock();
                                            try
                                            {
                                                player = Util.GetChar(test_id);
                                            }
                                            finally
                                            {
                                                Globals.PlayerLock.ExitReadLock();
                                            }
                                            if (player.HasEffect(AbnormalEffects.PETRIFIED))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.NPC:
                                            NPCInfo npc = null;
                                            Globals.NPCLock.EnterReadLock();
                                            try
                                            {
                                                npc = Util.GetNPC(test_id);
                                            }
                                            finally
                                            {
                                                Globals.NPCLock.ExitReadLock();
                                            }
                                            if (npc.HasEffect(AbnormalEffects.PETRIFIED))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        default:
                                            Globals.l2net_home.Add_Error("IS_PETRIFIED was not able to resolve the ID passed to it.");
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                    }
                                }
                            break;
                        case "IS_BURNING":
                                if (source1_ob.Type == Var_Types.INT)
                                {
                                    uint test_id = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                                    TargetType idType = Util.GetType(test_id);

                                    switch (idType)
                                    {
                                        case TargetType.NONE:
                                        case TargetType.ERROR:
                                        case TargetType.ITEM:
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                        case TargetType.MYPET:
                                            if (Globals.gamedata.my_pet.HasEffect(AbnormalEffects.FLAME))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.MYPET1:
                                            if (Globals.gamedata.my_pet1.HasEffect(AbnormalEffects.FLAME))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.MYPET2:
                                            if (Globals.gamedata.my_pet2.HasEffect(AbnormalEffects.FLAME))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.MYPET3:
                                            if (Globals.gamedata.my_pet3.HasEffect(AbnormalEffects.FLAME))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.SELF:
                                            if (Globals.gamedata.my_char.HasEffect(AbnormalEffects.FLAME))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.PLAYER:
                                            CharInfo player = null;
                                            Globals.PlayerLock.EnterReadLock();
                                            try
                                            {
                                                player = Util.GetChar(test_id);
                                            }
                                            finally
                                            {
                                                Globals.PlayerLock.ExitReadLock();
                                            }
                                            if (player.HasEffect(AbnormalEffects.FLAME))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.NPC:
                                            NPCInfo npc = null;
                                            Globals.NPCLock.EnterReadLock();
                                            try
                                            {
                                                npc = Util.GetNPC(test_id);
                                            }
                                            finally
                                            {
                                                Globals.NPCLock.ExitReadLock();
                                            }
                                            if (npc.HasEffect(AbnormalEffects.FLAME))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        default:
                                            Globals.l2net_home.Add_Error("IS_BURNING was not able to resolve the ID passed to it.");
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                    }
                                }
                            break;
                        case "IS_FLOATING_ROOT":
                                if (source1_ob.Type == Var_Types.INT)
                                {
                                    uint test_id = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                                    TargetType idType = Util.GetType(test_id);

                                    switch (idType)
                                    {
                                        case TargetType.NONE:
                                        case TargetType.ERROR:
                                        case TargetType.ITEM:
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                        case TargetType.MYPET:
                                            if (Globals.gamedata.my_pet.HasEffect(AbnormalEffects.FLOATING_ROOT))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.MYPET1:
                                            if (Globals.gamedata.my_pet1.HasEffect(AbnormalEffects.FLOATING_ROOT))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.MYPET2:
                                            if (Globals.gamedata.my_pet2.HasEffect(AbnormalEffects.FLOATING_ROOT))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.MYPET3:
                                            if (Globals.gamedata.my_pet3.HasEffect(AbnormalEffects.FLOATING_ROOT))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.SELF:
                                            if (Globals.gamedata.my_char.HasEffect(AbnormalEffects.FLOATING_ROOT))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.PLAYER:
                                            CharInfo player = null;
                                            Globals.PlayerLock.EnterReadLock();
                                            try
                                            {
                                                player = Util.GetChar(test_id);
                                            }
                                            finally
                                            {
                                                Globals.PlayerLock.ExitReadLock();
                                            }
                                            if (player.HasEffect(AbnormalEffects.FLOATING_ROOT))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.NPC:
                                            NPCInfo npc = null;
                                            Globals.NPCLock.EnterReadLock();
                                            try
                                            {
                                                npc = Util.GetNPC(test_id);
                                            }
                                            finally
                                            {
                                                Globals.NPCLock.ExitReadLock();
                                            }
                                            if (npc.HasEffect(AbnormalEffects.FLOATING_ROOT))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        default:
                                            Globals.l2net_home.Add_Error("IS_FLOATING_ROOT was not able to resolve the ID passed to it.");
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                    }
                                }
                            break;
                        case "IS_DANCE_STUNNED":
                                if (source1_ob.Type == Var_Types.INT)
                                {
                                    uint test_id = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                                    TargetType idType = Util.GetType(test_id);

                                    switch (idType)
                                    {
                                        case TargetType.NONE:
                                        case TargetType.ERROR:
                                        case TargetType.ITEM:
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                        case TargetType.MYPET:
                                            if (Globals.gamedata.my_pet.HasEffect(AbnormalEffects.DANCE_STUNNED))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.MYPET1:
                                            if (Globals.gamedata.my_pet1.HasEffect(AbnormalEffects.DANCE_STUNNED))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.MYPET2:
                                            if (Globals.gamedata.my_pet2.HasEffect(AbnormalEffects.DANCE_STUNNED))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.MYPET3:
                                            if (Globals.gamedata.my_pet3.HasEffect(AbnormalEffects.DANCE_STUNNED))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.SELF:
                                            if (Globals.gamedata.my_char.HasEffect(AbnormalEffects.DANCE_STUNNED))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.PLAYER:
                                            CharInfo player = null;
                                            Globals.PlayerLock.EnterReadLock();
                                            try
                                            {
                                                player = Util.GetChar(test_id);
                                            }
                                            finally
                                            {
                                                Globals.PlayerLock.ExitReadLock();
                                            }
                                            if (player.HasEffect(AbnormalEffects.DANCE_STUNNED))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.NPC:
                                            NPCInfo npc = null;
                                            Globals.NPCLock.EnterReadLock();
                                            try
                                            {
                                                npc = Util.GetNPC(test_id);
                                            }
                                            finally
                                            {
                                                Globals.NPCLock.ExitReadLock();
                                            }
                                            if (npc.HasEffect(AbnormalEffects.DANCE_STUNNED))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        default:
                                            Globals.l2net_home.Add_Error("IS_DANCE_STUNNED was not able to resolve the ID passed to it.");
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                    }
                                }
                            break;
                        case "IS_FIREROOT_STUN":
                                if (source1_ob.Type == Var_Types.INT)
                                {
                                    uint test_id = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                                    TargetType idType = Util.GetType(test_id);

                                    switch (idType)
                                    {
                                        case TargetType.NONE:
                                        case TargetType.ERROR:
                                        case TargetType.ITEM:
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                        case TargetType.MYPET:
                                            if (Globals.gamedata.my_pet.HasEffect(AbnormalEffects.FIREROOT_STUN))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.MYPET1:
                                            if (Globals.gamedata.my_pet1.HasEffect(AbnormalEffects.FIREROOT_STUN))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.MYPET2:
                                            if (Globals.gamedata.my_pet2.HasEffect(AbnormalEffects.FIREROOT_STUN))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.MYPET3:
                                            if (Globals.gamedata.my_pet3.HasEffect(AbnormalEffects.FIREROOT_STUN))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.SELF:
                                            if (Globals.gamedata.my_char.HasEffect(AbnormalEffects.FIREROOT_STUN))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.PLAYER:
                                            CharInfo player = null;
                                            Globals.PlayerLock.EnterReadLock();
                                            try
                                            {
                                                player = Util.GetChar(test_id);
                                            }
                                            finally
                                            {
                                                Globals.PlayerLock.ExitReadLock();
                                            }
                                            if (player.HasEffect(AbnormalEffects.FIREROOT_STUN))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.NPC:
                                            NPCInfo npc = null;
                                            Globals.NPCLock.EnterReadLock();
                                            try
                                            {
                                                npc = Util.GetNPC(test_id);
                                            }
                                            finally
                                            {
                                                Globals.NPCLock.ExitReadLock();
                                            }
                                            if (npc.HasEffect(AbnormalEffects.FIREROOT_STUN))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        default:
                                            Globals.l2net_home.Add_Error("IS_FIREROOT_STUN was not able to resolve the ID passed to it.");
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                    }
                                }
                            break;
                        case "IS_STEALTH":
                                if (source1_ob.Type == Var_Types.INT)
                                {
                                    uint test_id = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                                    TargetType idType = Util.GetType(test_id);

                                    switch (idType)
                                    {
                                        case TargetType.NONE:
                                        case TargetType.ERROR:
                                        case TargetType.ITEM:
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                        case TargetType.MYPET:
                                            if (Globals.gamedata.my_pet.HasEffect(AbnormalEffects.STEALTH))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.MYPET1:
                                            if (Globals.gamedata.my_pet1.HasEffect(AbnormalEffects.STEALTH))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.MYPET2:
                                            if (Globals.gamedata.my_pet2.HasEffect(AbnormalEffects.STEALTH))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.MYPET3:
                                            if (Globals.gamedata.my_pet3.HasEffect(AbnormalEffects.STEALTH))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.SELF:
                                            if (Globals.gamedata.my_char.HasEffect(AbnormalEffects.STEALTH))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.PLAYER:
                                            CharInfo player = null;
                                            Globals.PlayerLock.EnterReadLock();
                                            try
                                            {
                                                player = Util.GetChar(test_id);
                                            }
                                            finally
                                            {
                                                Globals.PlayerLock.ExitReadLock();
                                            }
                                            if (player.HasEffect(AbnormalEffects.STEALTH))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.NPC:
                                            NPCInfo npc = null;
                                            Globals.NPCLock.EnterReadLock();
                                            try
                                            {
                                                npc = Util.GetNPC(test_id);
                                            }
                                            finally
                                            {
                                                Globals.NPCLock.ExitReadLock();
                                            }
                                            if (npc.HasEffect(AbnormalEffects.STEALTH))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        default:
                                            Globals.l2net_home.Add_Error("IS_STEALTH was not able to resolve the ID passed to it.");
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                    }
                                }
                            break;
                        case "IS_IMPRISIONING_1":
                                if (source1_ob.Type == Var_Types.INT)
                                {
                                    uint test_id = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                                    TargetType idType = Util.GetType(test_id);

                                    switch (idType)
                                    {
                                        case TargetType.NONE:
                                        case TargetType.ERROR:
                                        case TargetType.ITEM:
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                        case TargetType.MYPET:
                                            if (Globals.gamedata.my_pet.HasEffect(AbnormalEffects.IMPRISIONING_1))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.MYPET1:
                                            if (Globals.gamedata.my_pet1.HasEffect(AbnormalEffects.IMPRISIONING_1))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.MYPET2:
                                            if (Globals.gamedata.my_pet2.HasEffect(AbnormalEffects.IMPRISIONING_1))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.MYPET3:
                                            if (Globals.gamedata.my_pet3.HasEffect(AbnormalEffects.IMPRISIONING_1))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.SELF:
                                            if (Globals.gamedata.my_char.HasEffect(AbnormalEffects.IMPRISIONING_1))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.PLAYER:
                                            CharInfo player = null;
                                            Globals.PlayerLock.EnterReadLock();
                                            try
                                            {
                                                player = Util.GetChar(test_id);
                                            }
                                            finally
                                            {
                                                Globals.PlayerLock.ExitReadLock();
                                            }
                                            if (player.HasEffect(AbnormalEffects.IMPRISIONING_1))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.NPC:
                                            NPCInfo npc = null;
                                            Globals.NPCLock.EnterReadLock();
                                            try
                                            {
                                                npc = Util.GetNPC(test_id);
                                            }
                                            finally
                                            {
                                                Globals.NPCLock.ExitReadLock();
                                            }
                                            if (npc.HasEffect(AbnormalEffects.IMPRISIONING_1))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        default:
                                            Globals.l2net_home.Add_Error("IS_IMPRISONING_1 was not able to resolve the ID passed to it.");
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                    }
                                }
                            break;
                        case "IS_IMPRISIONING_2":
                                if (source1_ob.Type == Var_Types.INT)
                                {
                                    uint test_id = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                                    TargetType idType = Util.GetType(test_id);

                                    switch (idType)
                                    {
                                        case TargetType.NONE:
                                        case TargetType.ERROR:
                                        case TargetType.ITEM:
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                        case TargetType.MYPET:
                                            if (Globals.gamedata.my_pet.HasEffect(AbnormalEffects.IMPRISIONING_2))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.MYPET1:
                                            if (Globals.gamedata.my_pet1.HasEffect(AbnormalEffects.IMPRISIONING_2))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.MYPET2:
                                            if (Globals.gamedata.my_pet2.HasEffect(AbnormalEffects.IMPRISIONING_2))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.MYPET3:
                                            if (Globals.gamedata.my_pet3.HasEffect(AbnormalEffects.IMPRISIONING_2))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.SELF:
                                            if (Globals.gamedata.my_char.HasEffect(AbnormalEffects.IMPRISIONING_2))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.PLAYER:
                                            CharInfo player = null;
                                            Globals.PlayerLock.EnterReadLock();
                                            try
                                            {
                                                player = Util.GetChar(test_id);
                                            }
                                            finally
                                            {
                                                Globals.PlayerLock.ExitReadLock();
                                            }
                                            if (player.HasEffect(AbnormalEffects.IMPRISIONING_2))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.NPC:
                                            NPCInfo npc = null;
                                            Globals.NPCLock.EnterReadLock();
                                            try
                                            {
                                                npc = Util.GetNPC(test_id);
                                            }
                                            finally
                                            {
                                                Globals.NPCLock.ExitReadLock();
                                            }
                                            if (npc.HasEffect(AbnormalEffects.IMPRISIONING_2))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        default:
                                            Globals.l2net_home.Add_Error("IS_IMPRISONING_2 was not able to resolve the ID passed to it.");
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                    }
                                }
                            break;
                        case "IS_SOE":
                                if (source1_ob.Type == Var_Types.INT)
                                {
                                    uint test_id = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                                    TargetType idType = Util.GetType(test_id);

                                    switch (idType)
                                    {
                                        case TargetType.NONE:
                                        case TargetType.ERROR:
                                        case TargetType.ITEM:
                                        case TargetType.MYPET:
                                        case TargetType.MYPET1:
                                        case TargetType.MYPET2:
                                        case TargetType.MYPET3:
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                        case TargetType.SELF:
                                            if (Globals.gamedata.my_char.HasEffect(AbnormalEffects.MAGIC_CIRCLE))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.PLAYER:
                                            CharInfo player = null;
                                            Globals.PlayerLock.EnterReadLock();
                                            try
                                            {
                                                player = Util.GetChar(test_id);
                                            }
                                            finally
                                            {
                                                Globals.PlayerLock.ExitReadLock();
                                            }
                                            if (player.HasEffect(AbnormalEffects.MAGIC_CIRCLE))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.NPC:
                                            NPCInfo npc = null;
                                            Globals.NPCLock.EnterReadLock();
                                            try
                                            {
                                                npc = Util.GetNPC(test_id);
                                            }
                                            finally
                                            {
                                                Globals.NPCLock.ExitReadLock();
                                            }
                                            if (npc.HasEffect(AbnormalEffects.MAGIC_CIRCLE))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        default:
                                            Globals.l2net_home.Add_Error("IS_SOE was not able to resolve the ID passed to it.");
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                    }
                                }
                            break;
                        case "IS_ICE2":
                                if (source1_ob.Type == Var_Types.INT)
                                {
                                    uint test_id = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                                    TargetType idType = Util.GetType(test_id);

                                    switch (idType)
                                    {
                                        case TargetType.NONE:
                                        case TargetType.ERROR:
                                        case TargetType.ITEM:
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                        case TargetType.MYPET:
                                            if (Globals.gamedata.my_pet.HasEffect(AbnormalEffects.ICE2))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.MYPET1:
                                            if (Globals.gamedata.my_pet1.HasEffect(AbnormalEffects.ICE2))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.MYPET2:
                                            if (Globals.gamedata.my_pet2.HasEffect(AbnormalEffects.ICE2))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.MYPET3:
                                            if (Globals.gamedata.my_pet3.HasEffect(AbnormalEffects.ICE2))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.SELF:
                                            if (Globals.gamedata.my_char.HasEffect(AbnormalEffects.ICE2))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.PLAYER:
                                            CharInfo player = null;
                                            Globals.PlayerLock.EnterReadLock();
                                            try
                                            {
                                                player = Util.GetChar(test_id);
                                            }
                                            finally
                                            {
                                                Globals.PlayerLock.ExitReadLock();
                                            }
                                            if (player.HasEffect(AbnormalEffects.ICE2))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.NPC:
                                            NPCInfo npc = null;
                                            Globals.NPCLock.EnterReadLock();
                                            try
                                            {
                                                npc = Util.GetNPC(test_id);
                                            }
                                            finally
                                            {
                                                Globals.NPCLock.ExitReadLock();
                                            }
                                            if (npc.HasEffect(AbnormalEffects.ICE2))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        default:
                                            Globals.l2net_home.Add_Error("IS_ICE2 was not able to resolve the ID passed to it.");
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                    }
                                }
                            break;
                        case "IS_EARTHQUAKE":
                                if (source1_ob.Type == Var_Types.INT)
                                {
                                    uint test_id = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                                    TargetType idType = Util.GetType(test_id);

                                    switch (idType)
                                    {
                                        case TargetType.NONE:
                                        case TargetType.ERROR:
                                        case TargetType.ITEM:
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                        case TargetType.MYPET:
                                            if (Globals.gamedata.my_pet.HasEffect(AbnormalEffects.EARTHQUAKE))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.MYPET1:
                                            if (Globals.gamedata.my_pet1.HasEffect(AbnormalEffects.EARTHQUAKE))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.MYPET2:
                                            if (Globals.gamedata.my_pet2.HasEffect(AbnormalEffects.EARTHQUAKE))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.MYPET3:
                                            if (Globals.gamedata.my_pet3.HasEffect(AbnormalEffects.EARTHQUAKE))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.SELF:
                                            if (Globals.gamedata.my_char.HasEffect(AbnormalEffects.EARTHQUAKE))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.PLAYER:
                                            CharInfo player = null;
                                            Globals.PlayerLock.EnterReadLock();
                                            try
                                            {
                                                player = Util.GetChar(test_id);
                                            }
                                            finally
                                            {
                                                Globals.PlayerLock.ExitReadLock();
                                            }
                                            if (player.HasEffect(AbnormalEffects.EARTHQUAKE))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.NPC:
                                            NPCInfo npc = null;
                                            Globals.NPCLock.EnterReadLock();
                                            try
                                            {
                                                npc = Util.GetNPC(test_id);
                                            }
                                            finally
                                            {
                                                Globals.NPCLock.ExitReadLock();
                                            }
                                            if (npc.HasEffect(AbnormalEffects.EARTHQUAKE))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        default:
                                            Globals.l2net_home.Add_Error("IS_EARTHQUAKE was not able to resolve the ID passed to it.");
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                    }
                                }
                            break;
                        case "IS_INVULNERABLE":
                                if (source1_ob.Type == Var_Types.INT)
                                {
                                    uint test_id = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                                    TargetType idType = Util.GetType(test_id);

                                    switch (idType)
                                    {
                                        case TargetType.NONE:
                                        case TargetType.ERROR:
                                        case TargetType.ITEM:
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                        case TargetType.MYPET:
                                            if (Globals.gamedata.my_pet.HasEffect(AbnormalEffects.INVULNERABLE))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.MYPET1:
                                            if (Globals.gamedata.my_pet1.HasEffect(AbnormalEffects.INVULNERABLE))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.MYPET2:
                                            if (Globals.gamedata.my_pet2.HasEffect(AbnormalEffects.INVULNERABLE))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.MYPET3:
                                            if (Globals.gamedata.my_pet3.HasEffect(AbnormalEffects.INVULNERABLE))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.SELF:
                                            if (Globals.gamedata.my_char.HasEffect(AbnormalEffects.INVULNERABLE))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.PLAYER:
                                            CharInfo player = null;
                                            Globals.PlayerLock.EnterReadLock();
                                            try
                                            {
                                                player = Util.GetChar(test_id);
                                            }
                                            finally
                                            {
                                                Globals.PlayerLock.ExitReadLock();
                                            }
                                            if (player.HasEffect(AbnormalEffects.INVULNERABLE))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.NPC:
                                            NPCInfo npc = null;
                                            Globals.NPCLock.EnterReadLock();
                                            try
                                            {
                                                npc = Util.GetNPC(test_id);
                                            }
                                            finally
                                            {
                                                Globals.NPCLock.ExitReadLock();
                                            }
                                            if (npc.HasEffect(AbnormalEffects.INVULNERABLE))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        default:
                                            Globals.l2net_home.Add_Error("IS_INVULNERABLE was not able to resolve the ID passed to it.");
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                    }
                                }
                            break;
                        case "IS_REGEN_VITALITY":
                                if (source1_ob.Type == Var_Types.INT)
                                {
                                    uint test_id = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                                    TargetType idType = Util.GetType(test_id);

                                    switch (idType)
                                    {
                                        case TargetType.NONE:
                                        case TargetType.ERROR:
                                        case TargetType.ITEM:
                                        case TargetType.MYPET:
                                        case TargetType.MYPET1:
                                        case TargetType.MYPET2:
                                        case TargetType.MYPET3:
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                        case TargetType.SELF:
                                            if (Globals.gamedata.my_char.HasEffect(AbnormalEffects.VITALITY))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.PLAYER:
                                            CharInfo player = null;
                                            Globals.PlayerLock.EnterReadLock();
                                            try
                                            {
                                                player = Util.GetChar(test_id);
                                            }
                                            finally
                                            {
                                                Globals.PlayerLock.ExitReadLock();
                                            }
                                            if (player.HasEffect(AbnormalEffects.VITALITY))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.NPC:
                                            NPCInfo npc = null;
                                            Globals.NPCLock.EnterReadLock();
                                            try
                                            {
                                                npc = Util.GetNPC(test_id);
                                            }
                                            finally
                                            {
                                                Globals.NPCLock.ExitReadLock();
                                            }
                                            if (npc.HasEffect(AbnormalEffects.VITALITY))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        default:
                                            Globals.l2net_home.Add_Error("IS_REGEN_VITALITY was not able to resolve the ID passed to it.");
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                    }
                                }
                            break;
                        case "IS_REAL_TARGETED":
                                if (source1_ob.Type == Var_Types.INT)
                                {
                                    uint test_id = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                                    TargetType idType = Util.GetType(test_id);

                                    switch (idType)
                                    {
                                        case TargetType.NONE:
                                        case TargetType.ERROR:
                                        case TargetType.ITEM:
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                        case TargetType.MYPET:
                                            if (Globals.gamedata.my_pet.HasEffect(AbnormalEffects.REAL_TARGET))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.MYPET1:
                                            if (Globals.gamedata.my_pet1.HasEffect(AbnormalEffects.REAL_TARGET))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.MYPET2:
                                            if (Globals.gamedata.my_pet2.HasEffect(AbnormalEffects.REAL_TARGET))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.MYPET3:
                                            if (Globals.gamedata.my_pet3.HasEffect(AbnormalEffects.REAL_TARGET))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.SELF:
                                            if (Globals.gamedata.my_char.HasEffect(AbnormalEffects.REAL_TARGET))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.PLAYER:
                                            CharInfo player = null;
                                            Globals.PlayerLock.EnterReadLock();
                                            try
                                            {
                                                player = Util.GetChar(test_id);
                                            }
                                            finally
                                            {
                                                Globals.PlayerLock.ExitReadLock();
                                            }
                                            if (player.HasEffect(AbnormalEffects.REAL_TARGET))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.NPC:
                                            NPCInfo npc = null;
                                            Globals.NPCLock.EnterReadLock();
                                            try
                                            {
                                                npc = Util.GetNPC(test_id);
                                            }
                                            finally
                                            {
                                                Globals.NPCLock.ExitReadLock();
                                            }
                                            if (npc.HasEffect(AbnormalEffects.REAL_TARGET))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        default:
                                            Globals.l2net_home.Add_Error("IS_REAL_TARGETED was not able to resolve the ID passed to it.");
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                    }
                                }
                            break;
                        case "IS_DEATH_MARKED":
                                if (source1_ob.Type == Var_Types.INT)
                                {
                                    uint test_id = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                                    TargetType idType = Util.GetType(test_id);

                                    switch (idType)
                                    {
                                        case TargetType.NONE:
                                        case TargetType.ERROR:
                                        case TargetType.ITEM:
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                        case TargetType.MYPET:
                                            if (Globals.gamedata.my_pet.HasEffect(AbnormalEffects.DEATH_MARK))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.MYPET1:
                                            if (Globals.gamedata.my_pet1.HasEffect(AbnormalEffects.DEATH_MARK))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.MYPET2:
                                            if (Globals.gamedata.my_pet2.HasEffect(AbnormalEffects.DEATH_MARK))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.MYPET3:
                                            if (Globals.gamedata.my_pet3.HasEffect(AbnormalEffects.DEATH_MARK))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.SELF:
                                            if (Globals.gamedata.my_char.HasEffect(AbnormalEffects.DEATH_MARK))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.PLAYER:
                                            CharInfo player = null;
                                            Globals.PlayerLock.EnterReadLock();
                                            try
                                            {
                                                player = Util.GetChar(test_id);
                                            }
                                            finally
                                            {
                                                Globals.PlayerLock.ExitReadLock();
                                            }
                                            if (player.HasEffect(AbnormalEffects.DEATH_MARK))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.NPC:
                                            NPCInfo npc = null;
                                            Globals.NPCLock.EnterReadLock();
                                            try
                                            {
                                                npc = Util.GetNPC(test_id);
                                            }
                                            finally
                                            {
                                                Globals.NPCLock.ExitReadLock();
                                            }
                                            if (npc.HasEffect(AbnormalEffects.DEATH_MARK))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        default:
                                            Globals.l2net_home.Add_Error("IS_DEATH_MARKED was not able to resolve the ID passed to it.");
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                    }
                                }
                            break;
                        case "IS_TERRIFIED":
                                if (source1_ob.Type == Var_Types.INT)
                                {
                                    uint test_id = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                                    TargetType idType = Util.GetType(test_id);

                                    switch (idType)
                                    {
                                        case TargetType.NONE:
                                        case TargetType.ERROR:
                                        case TargetType.ITEM:
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                        case TargetType.MYPET:
                                            if (Globals.gamedata.my_pet.HasEffect(AbnormalEffects.SKULL_FEAR))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.MYPET1:
                                            if (Globals.gamedata.my_pet1.HasEffect(AbnormalEffects.SKULL_FEAR))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.MYPET2:
                                            if (Globals.gamedata.my_pet2.HasEffect(AbnormalEffects.SKULL_FEAR))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.MYPET3:
                                            if (Globals.gamedata.my_pet3.HasEffect(AbnormalEffects.SKULL_FEAR))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.SELF:
                                            if (Globals.gamedata.my_char.HasEffect(AbnormalEffects.SKULL_FEAR))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.PLAYER:
                                            CharInfo player = null;
                                            Globals.PlayerLock.EnterReadLock();
                                            try
                                            {
                                                player = Util.GetChar(test_id);
                                            }
                                            finally
                                            {
                                                Globals.PlayerLock.ExitReadLock();
                                            }
                                            if (player.HasEffect(AbnormalEffects.SKULL_FEAR))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.NPC:
                                            NPCInfo npc = null;
                                            Globals.NPCLock.EnterReadLock();
                                            try
                                            {
                                                npc = Util.GetNPC(test_id);
                                            }
                                            finally
                                            {
                                                Globals.NPCLock.ExitReadLock();
                                            }
                                            if (npc.HasEffect(AbnormalEffects.SKULL_FEAR))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        default:
                                            Globals.l2net_home.Add_Error("IS_TERRIFIED was not able to resolve the ID passed to it.");
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                    }
                                }
                            break;
                        case "IS_CONFUSED":
                                if (source1_ob.Type == Var_Types.INT)
                                {
                                    uint test_id = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                                    TargetType idType = Util.GetType(test_id);

                                    switch (idType)
                                    {
                                        case TargetType.NONE:
                                        case TargetType.ERROR:
                                        case TargetType.ITEM:
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                        case TargetType.MYPET:
                                            if (Globals.gamedata.my_pet.HasEffect(AbnormalEffects.CONFUSED))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.MYPET1:
                                            if (Globals.gamedata.my_pet1.HasEffect(AbnormalEffects.CONFUSED))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.MYPET2:
                                            if (Globals.gamedata.my_pet2.HasEffect(AbnormalEffects.CONFUSED))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.MYPET3:
                                            if (Globals.gamedata.my_pet3.HasEffect(AbnormalEffects.CONFUSED))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.SELF:
                                            if (Globals.gamedata.my_char.HasEffect(AbnormalEffects.CONFUSED))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.PLAYER:
                                            CharInfo player = null;
                                            Globals.PlayerLock.EnterReadLock();
                                            try
                                            {
                                                player = Util.GetChar(test_id);
                                            }
                                            finally
                                            {
                                                Globals.PlayerLock.ExitReadLock();
                                            }
                                            if (player.HasEffect(AbnormalEffects.CONFUSED))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.NPC:
                                            NPCInfo npc = null;
                                            Globals.NPCLock.EnterReadLock();
                                            try
                                            {
                                                npc = Util.GetNPC(test_id);
                                            }
                                            finally
                                            {
                                                Globals.NPCLock.ExitReadLock();
                                            }
                                            if (npc.HasEffect(AbnormalEffects.CONFUSED))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        default:
                                            Globals.l2net_home.Add_Error("IS_CONFUSED was not able to resolve the ID passed to it.");
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                    }
                                }
                            break;
                        case "IS_INVINCIBLE":
                                if (source1_ob.Type == Var_Types.INT)
                                {
                                    uint test_id = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                                    TargetType idType = Util.GetType(test_id);

                                    switch (idType)
                                    {
                                        case TargetType.NONE:
                                        case TargetType.ERROR:
                                        case TargetType.ITEM:
                                        case TargetType.MYPET:
                                        case TargetType.MYPET1:
                                        case TargetType.MYPET2:
                                        case TargetType.MYPET3:
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                        case TargetType.SELF:
                                            if (Globals.gamedata.my_char.HasExtendedEffect(ExtendedEffects.INVINCIBLE))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.PLAYER:
                                            CharInfo player = null;
                                            Globals.PlayerLock.EnterReadLock();
                                            try
                                            {
                                                player = Util.GetChar(test_id);
                                            }
                                            finally
                                            {
                                                Globals.PlayerLock.ExitReadLock();

                                            }
                                            if (player.HasExtendedEffect(ExtendedEffects.INVINCIBLE))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.NPC:
                                            NPCInfo npc = null;
                                            Globals.NPCLock.EnterReadLock();
                                            try
                                            {
                                                npc = Util.GetNPC(test_id);
                                            }
                                            finally
                                            {
                                                Globals.NPCLock.ExitReadLock();
                                            }
                                            if (npc.HasExtendedEffect(ExtendedEffects.INVINCIBLE))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        default:
                                            Globals.l2net_home.Add_Error("IS_INVINCIBLE was not able to resolve the ID passed to it.");
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                    }
                                }
                            break;
                        case "IS_AIR_STUN":
                                if (source1_ob.Type == Var_Types.INT)
                                {
                                    uint test_id = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                                    TargetType idType = Util.GetType(test_id);

                                    switch (idType)
                                    {
                                        case TargetType.NONE:
                                        case TargetType.ERROR:
                                        case TargetType.ITEM:
                                        case TargetType.MYPET:
                                        case TargetType.MYPET1:
                                        case TargetType.MYPET2:
                                        case TargetType.MYPET3:
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                        case TargetType.SELF:
                                            if (Globals.gamedata.my_char.HasExtendedEffect(ExtendedEffects.AIR_STUN))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.PLAYER:
                                            CharInfo player = null;
                                            Globals.PlayerLock.EnterReadLock();
                                            try
                                            {
                                                player = Util.GetChar(test_id);
                                            }
                                            finally
                                            {
                                                Globals.PlayerLock.ExitReadLock();

                                            }
                                            if (player.HasExtendedEffect(ExtendedEffects.AIR_STUN))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.NPC:
                                            NPCInfo npc = null;
                                            Globals.NPCLock.EnterReadLock();
                                            try
                                            {
                                                npc = Util.GetNPC(test_id);
                                            }
                                            finally
                                            {
                                                Globals.NPCLock.ExitReadLock();
                                            }
                                            if (npc.HasExtendedEffect(ExtendedEffects.AIR_STUN))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        default:
                                            Globals.l2net_home.Add_Error("IS_AIR_STUN was not able to resolve the ID passed to it.");
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                    }
                                }
                            break;
                        case "IS_AIR_ROOT":
                                if (source1_ob.Type == Var_Types.INT)
                                {
                                    uint test_id = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                                    TargetType idType = Util.GetType(test_id);

                                    switch (idType)
                                    {
                                        case TargetType.NONE:
                                        case TargetType.ERROR:
                                        case TargetType.ITEM:
                                        case TargetType.MYPET:
                                        case TargetType.MYPET1:
                                        case TargetType.MYPET2:
                                        case TargetType.MYPET3:
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                        case TargetType.SELF:
                                            if (Globals.gamedata.my_char.HasExtendedEffect(ExtendedEffects.AIR_ROOT))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.PLAYER:
                                            CharInfo player = null;
                                            Globals.PlayerLock.EnterReadLock();
                                            try
                                            {
                                                player = Util.GetChar(test_id);
                                            }
                                            finally
                                            {
                                                Globals.PlayerLock.ExitReadLock();

                                            }
                                            if (player.HasExtendedEffect(ExtendedEffects.AIR_ROOT))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.NPC:
                                            NPCInfo npc = null;
                                            Globals.NPCLock.EnterReadLock();
                                            try
                                            {
                                                npc = Util.GetNPC(test_id);
                                            }
                                            finally
                                            {
                                                Globals.NPCLock.ExitReadLock();
                                            }
                                            if (npc.HasExtendedEffect(ExtendedEffects.AIR_ROOT))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        default:
                                            Globals.l2net_home.Add_Error("IS_AIR_ROOT was not able to resolve the ID passed to it.");
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                    }
                                }
                            break;
                        case "IS_STIGMAED":
                                if (source1_ob.Type == Var_Types.INT)
                                {
                                    uint test_id = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                                    TargetType idType = Util.GetType(test_id);

                                    switch (idType)
                                    {
                                        case TargetType.NONE:
                                        case TargetType.ERROR:
                                        case TargetType.ITEM:
                                        case TargetType.MYPET:
                                        case TargetType.MYPET1:
                                        case TargetType.MYPET2:
                                        case TargetType.MYPET3:
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                        case TargetType.SELF:
                                            if (Globals.gamedata.my_char.HasExtendedEffect(ExtendedEffects.STIGMA_SHILIEN))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.PLAYER:
                                            CharInfo player = null;
                                            Globals.PlayerLock.EnterReadLock();
                                            try
                                            {
                                                player = Util.GetChar(test_id);
                                            }
                                            finally
                                            {
                                                Globals.PlayerLock.ExitReadLock();

                                            }
                                            if (player.HasExtendedEffect(ExtendedEffects.STIGMA_SHILIEN))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.NPC:
                                            NPCInfo npc = null;
                                            Globals.NPCLock.EnterReadLock();
                                            try
                                            {
                                                npc = Util.GetNPC(test_id);
                                            }
                                            finally
                                            {
                                                Globals.NPCLock.ExitReadLock();
                                            }
                                            if (npc.HasExtendedEffect(ExtendedEffects.STIGMA_SHILIEN))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        default:
                                            Globals.l2net_home.Add_Error("IS_STIGMAED was not able to resolve the ID passed to it.");
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                    }
                                }
                            break;
                        case "IS_STAKATOROOT":
                                if (source1_ob.Type == Var_Types.INT)
                                {
                                    uint test_id = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                                    TargetType idType = Util.GetType(test_id);

                                    switch (idType)
                                    {
                                        case TargetType.NONE:
                                        case TargetType.ERROR:
                                        case TargetType.ITEM:
                                        case TargetType.MYPET:
                                        case TargetType.MYPET1:
                                        case TargetType.MYPET2:
                                        case TargetType.MYPET3:
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                        case TargetType.SELF:
                                            if (Globals.gamedata.my_char.HasExtendedEffect(ExtendedEffects.STAKATOROOT))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.PLAYER:
                                            CharInfo player = null;
                                            Globals.PlayerLock.EnterReadLock();
                                            try
                                            {
                                                player = Util.GetChar(test_id);
                                            }
                                            finally
                                            {
                                                Globals.PlayerLock.ExitReadLock();

                                            }
                                            if (player.HasExtendedEffect(ExtendedEffects.STAKATOROOT))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.NPC:
                                            NPCInfo npc = null;
                                            Globals.NPCLock.EnterReadLock();
                                            try
                                            {
                                                npc = Util.GetNPC(test_id);
                                            }
                                            finally
                                            {
                                                Globals.NPCLock.ExitReadLock();
                                            }
                                            if (npc.HasExtendedEffect(ExtendedEffects.STAKATOROOT))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        default:
                                            Globals.l2net_home.Add_Error("IS_STAKATOROOT was not able to resolve the ID passed to it.");
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                    }
                                }
                            break;
                        case "IS_FREEZING":
                                if (source1_ob.Type == Var_Types.INT)
                                {
                                    uint test_id = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                                    TargetType idType = Util.GetType(test_id);

                                    switch (idType)
                                    {
                                        case TargetType.NONE:
                                        case TargetType.ERROR:
                                        case TargetType.ITEM:
                                        case TargetType.MYPET:
                                        case TargetType.MYPET1:
                                        case TargetType.MYPET2:
                                        case TargetType.MYPET3:
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                        case TargetType.SELF:
                                            if (Globals.gamedata.my_char.HasExtendedEffect(ExtendedEffects.FREEZING))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.PLAYER:
                                            CharInfo player = null;
                                            Globals.PlayerLock.EnterReadLock();
                                            try
                                            {
                                                player = Util.GetChar(test_id);
                                            }
                                            finally
                                            {
                                                Globals.PlayerLock.ExitReadLock();

                                            }
                                            if (player.HasExtendedEffect(ExtendedEffects.FREEZING))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.NPC:
                                            NPCInfo npc = null;
                                            Globals.NPCLock.EnterReadLock();
                                            try
                                            {
                                                npc = Util.GetNPC(test_id);
                                            }
                                            finally
                                            {
                                                Globals.NPCLock.ExitReadLock();
                                            }
                                            if (npc.HasExtendedEffect(ExtendedEffects.FREEZING))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        default:
                                            Globals.l2net_home.Add_Error("IS_FREEZING was not able to resolve the ID passed to it.");
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                    }
                                }
                            break;
                        case "IS_DISABLED":
                                if (source1_ob.Type == Var_Types.INT)
                                {
                                    uint test_id = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                                    TargetType idType = Util.GetType(test_id);

                                    switch (idType)
                                    {
                                        case TargetType.NONE:
                                        case TargetType.ERROR:
                                        case TargetType.ITEM:
                                        //case TargetType.MYPET:
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                        case TargetType.MYPET:
                                                if ((Globals.gamedata.my_pet.HasEffect(AbnormalEffects.FEAR)) || 
                                                (Globals.gamedata.my_pet.HasEffect(AbnormalEffects.STUN)) || 
                                                (Globals.gamedata.my_pet.HasEffect(AbnormalEffects.SLEEP)) || 
                                                (Globals.gamedata.my_pet.HasEffect(AbnormalEffects.HOLD_1)) || 
                                                (Globals.gamedata.my_pet.HasEffect(AbnormalEffects.PETRIFIED)) || 
                                                (Globals.gamedata.my_pet.HasEffect(AbnormalEffects.FLOATING_ROOT)) || 
                                                (Globals.gamedata.my_pet.HasEffect(AbnormalEffects.DANCE_STUNNED)) || 
                                                (Globals.gamedata.my_pet.HasEffect(AbnormalEffects.FIREROOT_STUN)) || 
                                                (Globals.gamedata.my_pet.HasEffect(AbnormalEffects.SKULL_FEAR)))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }          
                                            break;
                                        case TargetType.MYPET1:
                                            if ((Globals.gamedata.my_pet1.HasEffect(AbnormalEffects.FEAR)) ||
                                            (Globals.gamedata.my_pet1.HasEffect(AbnormalEffects.STUN)) ||
                                            (Globals.gamedata.my_pet1.HasEffect(AbnormalEffects.SLEEP)) ||
                                            (Globals.gamedata.my_pet1.HasEffect(AbnormalEffects.HOLD_1)) ||
                                            (Globals.gamedata.my_pet1.HasEffect(AbnormalEffects.PETRIFIED)) ||
                                            (Globals.gamedata.my_pet1.HasEffect(AbnormalEffects.FLOATING_ROOT)) ||
                                            (Globals.gamedata.my_pet1.HasEffect(AbnormalEffects.DANCE_STUNNED)) ||
                                            (Globals.gamedata.my_pet1.HasEffect(AbnormalEffects.FIREROOT_STUN)) ||
                                            (Globals.gamedata.my_pet1.HasEffect(AbnormalEffects.SKULL_FEAR)))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.MYPET2:
                                            if ((Globals.gamedata.my_pet2.HasEffect(AbnormalEffects.FEAR)) ||
                                            (Globals.gamedata.my_pet2.HasEffect(AbnormalEffects.STUN)) ||
                                            (Globals.gamedata.my_pet2.HasEffect(AbnormalEffects.SLEEP)) ||
                                            (Globals.gamedata.my_pet2.HasEffect(AbnormalEffects.HOLD_1)) ||
                                            (Globals.gamedata.my_pet2.HasEffect(AbnormalEffects.PETRIFIED)) ||
                                            (Globals.gamedata.my_pet2.HasEffect(AbnormalEffects.FLOATING_ROOT)) ||
                                            (Globals.gamedata.my_pet2.HasEffect(AbnormalEffects.DANCE_STUNNED)) ||
                                            (Globals.gamedata.my_pet2.HasEffect(AbnormalEffects.FIREROOT_STUN)) ||
                                            (Globals.gamedata.my_pet2.HasEffect(AbnormalEffects.SKULL_FEAR)))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.MYPET3:
                                            if ((Globals.gamedata.my_pet3.HasEffect(AbnormalEffects.FEAR)) ||
                                            (Globals.gamedata.my_pet3.HasEffect(AbnormalEffects.STUN)) ||
                                            (Globals.gamedata.my_pet3.HasEffect(AbnormalEffects.SLEEP)) ||
                                            (Globals.gamedata.my_pet3.HasEffect(AbnormalEffects.HOLD_1)) ||
                                            (Globals.gamedata.my_pet3.HasEffect(AbnormalEffects.PETRIFIED)) ||
                                            (Globals.gamedata.my_pet3.HasEffect(AbnormalEffects.FLOATING_ROOT)) ||
                                            (Globals.gamedata.my_pet3.HasEffect(AbnormalEffects.DANCE_STUNNED)) ||
                                            (Globals.gamedata.my_pet3.HasEffect(AbnormalEffects.FIREROOT_STUN)) ||
                                            (Globals.gamedata.my_pet3.HasEffect(AbnormalEffects.SKULL_FEAR)))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.SELF:
                                            if ((Globals.gamedata.my_char.HasEffect(AbnormalEffects.FEAR)) || 
                                                (Globals.gamedata.my_char.HasEffect(AbnormalEffects.STUN)) || 
                                                (Globals.gamedata.my_char.HasEffect(AbnormalEffects.SLEEP)) || 
                                                (Globals.gamedata.my_char.HasEffect(AbnormalEffects.HOLD_1)) || 
                                                (Globals.gamedata.my_char.HasEffect(AbnormalEffects.PETRIFIED)) || 
                                                (Globals.gamedata.my_char.HasEffect(AbnormalEffects.FLOATING_ROOT)) || 
                                                (Globals.gamedata.my_char.HasEffect(AbnormalEffects.DANCE_STUNNED)) || 
                                                (Globals.gamedata.my_char.HasEffect(AbnormalEffects.FIREROOT_STUN)) || 
                                                (Globals.gamedata.my_char.HasEffect(AbnormalEffects.SKULL_FEAR)) || 
                                                (Globals.gamedata.my_char.HasExtendedEffect(ExtendedEffects.AIR_STUN)) || 
                                                (Globals.gamedata.my_char.HasExtendedEffect(ExtendedEffects.FREEZING)))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.PLAYER:
                                            CharInfo player = null;
                                            Globals.PlayerLock.EnterReadLock();
                                            try
                                            {
                                                player = Util.GetChar(test_id);
                                            }
                                            finally
                                            {
                                                Globals.PlayerLock.ExitReadLock();

                                            }
                                            if (player.HasExtendedEffect(ExtendedEffects.FREEZING))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        case TargetType.NPC:
                                            NPCInfo npc = null;
                                            Globals.NPCLock.EnterReadLock();
                                            try
                                            {
                                                npc = Util.GetNPC(test_id);
                                            }
                                            finally
                                            {
                                                Globals.NPCLock.ExitReadLock();
                                            }
                                            if (npc.HasExtendedEffect(ExtendedEffects.FREEZING))
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            else
                                            {
                                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                                cast = Var_Types.INT;
                                            }
                                            break;
                                        default:
                                            Globals.l2net_home.Add_Error("IS_DISABLED was not able to resolve the ID passed to it.");
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                            break;
                                    }
                                }
                            break;
                        case "IN_POLY":
                            if (source1_ob.Type == Var_Types.INT)
                            {
                                uint tmp_id = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                                TargetType idType = Util.GetType(tmp_id);

                                switch (idType)
                                {
                                   // case TargetType.NONE:
                                   // case TargetType.ERROR:
                                      //  break;
                                    case TargetType.ITEM:
                                        ItemInfo tmp_item = null;
                                        Globals.ItemLock.EnterReadLock();
                                        try
                                        {
                                            tmp_item = Util.GetItem(tmp_id);
                                        }
                                        finally
                                        {
                                            Globals.ItemLock.ExitReadLock();
                                        }
                                        if (Globals.gamedata.Paths.IsPointInside(Util.Float_Int32(tmp_item.X), Util.Float_Int32(tmp_item.Y)))
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        else
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                        }
                                    //GetItem
                                        break;
                                    case TargetType.MYPET:
                                        if (Globals.gamedata.Paths.IsPointInside(Util.Float_Int32(Globals.gamedata.my_pet.X), Util.Float_Int32(Globals.gamedata.my_pet.Y)))
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        else
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                        }

                                        break;
                                    case TargetType.MYPET1:
                                        if (Globals.gamedata.Paths.IsPointInside(Util.Float_Int32(Globals.gamedata.my_pet1.X), Util.Float_Int32(Globals.gamedata.my_pet1.Y)))
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        else
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                        }

                                        break;
                                    case TargetType.MYPET2:
                                        if (Globals.gamedata.Paths.IsPointInside(Util.Float_Int32(Globals.gamedata.my_pet2.X), Util.Float_Int32(Globals.gamedata.my_pet2.Y)))
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        else
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                        }

                                        break;
                                    case TargetType.MYPET3:
                                        if (Globals.gamedata.Paths.IsPointInside(Util.Float_Int32(Globals.gamedata.my_pet3.X), Util.Float_Int32(Globals.gamedata.my_pet3.Y)))
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        else
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                        }

                                        break;
                                    case TargetType.SELF:
                                        if (Globals.gamedata.Paths.IsPointInside(Util.Float_Int32(Globals.gamedata.my_char.X), Util.Float_Int32(Globals.gamedata.my_char.Y)))
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        else
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        break;
                                    case TargetType.PLAYER:
                                        CharInfo player = null;
                                        Globals.PlayerLock.EnterReadLock();
                                        try
                                        {
                                            player = Util.GetChar(tmp_id);
                                        }
                                        finally
                                        {
                                            Globals.PlayerLock.ExitReadLock();

                                        }
                                        if (Globals.gamedata.Paths.IsPointInside(Util.Float_Int32(player.X), Util.Float_Int32(player.Y)))
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        else
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        break;
                                    case TargetType.NPC:
                                        NPCInfo npc = null;
                                        Globals.NPCLock.EnterReadLock();
                                        try
                                        {
                                            npc = Util.GetNPC(tmp_id);
                                        }
                                        finally
                                        {
                                            Globals.NPCLock.ExitReadLock();
                                        }
                                        if (Globals.gamedata.Paths.IsPointInside(Util.Float_Int32(npc.X), Util.Float_Int32(npc.Y)))
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        else
                                        {
                                            dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                            cast = Var_Types.INT;
                                        }
                                        break;
                                    default:
                                        Globals.l2net_home.Add_Error("IN_POLY was not able to resolve the ID passed to it.");
                                        dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                        cast = Var_Types.INT;
                                        break;
                                }
                            }
                            break;
                        case "SQRT":
                            if (source1_ob.Type == Var_Types.INT)
                            {
                                dest_i = (long)System.Math.Sqrt(System.Convert.ToInt64(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture));
                                cast = Var_Types.INT;
                            }
                            else if (source1_ob.Type == Var_Types.DOUBLE)
                            {
                                dest_d = System.Math.Sqrt(System.Convert.ToDouble(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture));
                                cast = Var_Types.DOUBLE;
                            }
                            break;
                        case "ABS":
                            if (source1_ob.Type == Var_Types.INT)
                            {
                                dest_i = (long)System.Math.Abs(System.Convert.ToInt64(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture));
                                cast = Var_Types.INT;
                            }
                            else if (source1_ob.Type == Var_Types.DOUBLE)
                            {
                                dest_d = (double)System.Math.Abs(System.Convert.ToDouble(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture));
                                cast = Var_Types.DOUBLE;
                            }
                            break;
                        case "SIN":
                            if (source1_ob.Type == Var_Types.INT)
                            {
                                dest_i = (long)System.Math.Sin(System.Convert.ToDouble(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture));
                                cast = Var_Types.INT;
                            }
                            else if (source1_ob.Type == Var_Types.DOUBLE)
                            {
                                dest_d = (double)System.Math.Sin(System.Convert.ToDouble(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture));
                                cast = Var_Types.DOUBLE;
                            }
                            break;
                        case "SINH":
                            if (source1_ob.Type == Var_Types.INT)
                            {
                                dest_i = (long)System.Math.Sinh(System.Convert.ToDouble(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture));
                                cast = Var_Types.INT;
                            }
                            else if (source1_ob.Type == Var_Types.DOUBLE)
                            {
                                dest_d = (double)System.Math.Sinh(System.Convert.ToDouble(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture));
                                cast = Var_Types.DOUBLE;
                            }
                            break;
                        case "ASIN":
                            if (source1_ob.Type == Var_Types.INT)
                            {
                                dest_i = (long)System.Math.Asin(System.Convert.ToDouble(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture));
                                cast = Var_Types.INT;
                            }
                            else if (source1_ob.Type == Var_Types.DOUBLE)
                            {
                                dest_d = (double)System.Math.Asin(System.Convert.ToDouble(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture));
                                cast = Var_Types.DOUBLE;
                            }
                            break;
                        case "COS":
                            if (source1_ob.Type == Var_Types.INT)
                            {
                                dest_i = (long)System.Math.Cos(System.Convert.ToDouble(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture));
                                cast = Var_Types.INT;
                            }
                            else if (source1_ob.Type == Var_Types.DOUBLE)
                            {
                                dest_d = (double)System.Math.Cos(System.Convert.ToDouble(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture));
                                cast = Var_Types.DOUBLE;
                            }
                            break;
                        case "COSH":
                            if (source1_ob.Type == Var_Types.INT)
                            {
                                dest_i = (long)System.Math.Cosh(System.Convert.ToDouble(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture));
                                cast = Var_Types.INT;
                            }
                            else if (source1_ob.Type == Var_Types.DOUBLE)
                            {
                                dest_d = (double)System.Math.Cosh(System.Convert.ToDouble(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture));
                                cast = Var_Types.DOUBLE;
                            }
                            break;
                        case "ACOS":
                            if (source1_ob.Type == Var_Types.INT)
                            {
                                dest_i = (long)System.Math.Acos(System.Convert.ToDouble(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture));
                                cast = Var_Types.INT;
                            }
                            else if (source1_ob.Type == Var_Types.DOUBLE)
                            {
                                dest_d = (double)System.Math.Acos(System.Convert.ToDouble(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture));
                                cast = Var_Types.DOUBLE;
                            }
                            break;
                        case "TAN":
                            if (source1_ob.Type == Var_Types.INT)
                            {
                                dest_i = (long)System.Math.Tan(System.Convert.ToDouble(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture));
                                cast = Var_Types.INT;
                            }
                            else if (source1_ob.Type == Var_Types.DOUBLE)
                            {
                                dest_d = (double)System.Math.Tan(System.Convert.ToDouble(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture));
                                cast = Var_Types.DOUBLE;
                            }
                            break;
                        case "TANH":
                            if (source1_ob.Type == Var_Types.INT)
                            {
                                dest_i = (long)System.Math.Tanh(System.Convert.ToDouble(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture));
                                cast = Var_Types.INT;
                            }
                            else if (source1_ob.Type == Var_Types.DOUBLE)
                            {
                                dest_d = (double)System.Math.Tanh(System.Convert.ToDouble(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture));
                                cast = Var_Types.DOUBLE;
                            }
                            break;
                        case "ATAN":
                            if (source1_ob.Type == Var_Types.INT)
                            {
                                dest_i = (long)System.Math.Atan(System.Convert.ToDouble(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture));
                                cast = Var_Types.INT;
                            }
                            else if (source1_ob.Type == Var_Types.DOUBLE)
                            {
                                dest_d = (double)System.Math.Atan(System.Convert.ToDouble(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture));
                                cast = Var_Types.DOUBLE;
                            }
                            break;
                        case "~":
                            if (source1_ob.Type == Var_Types.INT)
                            {
                                dest_i = ~System.Convert.ToInt64(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                                cast = Var_Types.INT;
                            }
                            break;
                        case "IS_RUNNING":
                            bool found = false;
                            if (source1_ob.Type == Var_Types.STRING)
                            {
                                Process[] processlist = Process.GetProcesses();

                                foreach (Process theprocess in processlist)
                                {
                                    if (theprocess.ProcessName == source1_ob.Value.ToString())
                                    {
                                        found = true;
                                        break;
                                    }
                                }
                            }

                            if (found)
                            {
                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                cast = Var_Types.INT;
                            }
                            else
                            {
                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                cast = Var_Types.INT;
                            }
                            return;
                        case "IS_NOT_RUNNING":
                            found = false;
                            if (source1_ob.Type == Var_Types.STRING)
                            {
                                Process[] processlist = Process.GetProcesses();

                                foreach (Process theprocess in processlist)
                                {
                                    if (theprocess.ProcessName == source1_ob.Value.ToString())
                                    {
                                        found = true;
                                        break;
                                    }
                                }
                            }

                            if (found)
                            {
                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                cast = Var_Types.INT;
                            }
                            else
                            {
                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                cast = Var_Types.INT;
                            }
                            break;
                        case "FILE_EXISTS":
                            found = false;
                            if (source1_ob.Type == Var_Types.STRING)
                            {
                                if (System.IO.File.Exists(source1_ob.Value.ToString()))
                                {
                                    found = true;
                                }
                            }

                            if (found)
                            {
                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                cast = Var_Types.INT;
                            }
                            else
                            {
                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                cast = Var_Types.INT;
                            }
                            break;
                        case "?I":
                            //is the variable an integer
                            if (source1_ob.Type == Var_Types.INT)
                            {
                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                cast = Var_Types.INT;
                            }
                            else
                            {
                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                cast = Var_Types.INT;
                            }
                            break;
                        case "?D":
                            //is the variable a double
                            if (source1_ob.Type == Var_Types.DOUBLE)
                            {
                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                cast = Var_Types.INT;
                            }
                            else
                            {
                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                cast = Var_Types.INT;
                            }
                            break;
                        case "?$":
                            //is the variable a string
                            if (source1_ob.Type == Var_Types.STRING)
                            {
                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                cast = Var_Types.INT;
                            }
                            else
                            {
                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                cast = Var_Types.INT;
                            }
                            break;
                        case "?S":
                            //is the variable a sortedlist
                            if (source1_ob.Type == Var_Types.SORTEDLIST)
                            {
                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                cast = Var_Types.INT;
                            }
                            else
                            {
                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                cast = Var_Types.INT;
                            }
                            break;
                        case "?A":
                            //is the variable an arraylist
                            if (source1_ob.Type == Var_Types.ARRAYLIST)
                            {
                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                cast = Var_Types.INT;
                            }
                            else
                            {
                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                cast = Var_Types.INT;
                            }
                            break;
                        case "?ST":
                            //is the variable a stack
                            if (source1_ob.Type == Var_Types.QUEUE)
                            {
                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                cast = Var_Types.INT;
                            }
                            else
                            {
                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                cast = Var_Types.INT;
                            }
                            break;
                        case "?Q":
                            //is the variable a queue
                            if (source1_ob.Type == Var_Types.QUEUE)
                            {
                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                cast = Var_Types.INT;
                            }
                            else
                            {
                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                cast = Var_Types.INT;
                            }
                            break;
                        case "?FR":
                            //is the variable a FILEREADER
                            if (source1_ob.Type == Var_Types.FILEREADER)
                            {
                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                cast = Var_Types.INT;
                            }
                            else
                            {
                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                cast = Var_Types.INT;
                            }
                            break;
                        case "?FW":
                            //is the variable a FILEWRITER
                            if (source1_ob.Type == Var_Types.FILEWRITER)
                            {
                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                cast = Var_Types.INT;
                            }
                            else
                            {
                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                cast = Var_Types.INT;
                            }
                            break;
                        case "?B":
                            //is the variable a BYTEBUFFER
                            if (source1_ob.Type == Var_Types.BYTEBUFFER)
                            {
                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                cast = Var_Types.INT;
                            }
                            else
                            {
                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                cast = Var_Types.INT;
                            }
                            break;
                        case "?W":
                            //is the variable a window
                            if (source1_ob.Type == Var_Types.WINDOW)
                            {
                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                cast = Var_Types.INT;
                            }
                            else
                            {
                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                cast = Var_Types.INT;
                            }
                            break;
                        case "?C":
                            //is the variable a user defined class
                            if (source1_ob.Type == Var_Types.CLASS)
                            {
                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                cast = Var_Types.INT;
                            }
                            else
                            {
                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                cast = Var_Types.INT;
                            }
                            break;
                        default:
                            ScriptEngine.Script_Error("UNKNOWN MATH TYPE : " + op);
                            break;
                    }
                    break;
            }

            if(cast == Var_Types.NULL)
            {
                //we tried an operation that didn't succeed
                ScriptEngine.Script_Error("can't perform [" + op + "] on [" + var1 + "]");
            }

            switch (dest_ob.Type)
            {
                case Var_Types.INT:
                    switch(cast)
                    {
                        case Var_Types.INT:
                            dest_ob.Value = dest_i;
                            break;
                        case Var_Types.DOUBLE:
                            try
                            {
                                dest_ob.Value = System.Convert.ToInt64(dest_d);
                            }
                            catch
                            {
                                //can't cast NaN to int
                                dest_ob.Value = long.MinValue;
                            }
                            break;
                    }
                    break;
                case Var_Types.DOUBLE:
                    switch (cast)
                    {
                        case Var_Types.INT:
                            dest_ob.Value = System.Convert.ToDouble(dest_i);
                            break;
                        case Var_Types.DOUBLE:
                            dest_ob.Value = dest_d;
                            break;
                    }
                    break;
                case Var_Types.STRING:
                    switch (cast)
                    {
                        case Var_Types.INT:
                            dest_ob.Value = System.Convert.ToString(dest_i);
                            break;
                        case Var_Types.DOUBLE:
                            dest_ob.Value = System.Convert.ToString(dest_d);
                            break;
                    }
                    break;
                case Var_Types.ASSIGNABLE:
                    switch (cast)
                    {
                        case Var_Types.INT:
                            dest_ob.Value = dest_i;
                            dest_ob.Type = Var_Types.INT;
                            break;
                        case Var_Types.DOUBLE:
                            dest_ob.Value = dest_d;
                            dest_ob.Type = Var_Types.DOUBLE;
                            break;
                    }
                    break;
                default:
                    //script error
                    ScriptEngine.Script_Error("couldn't cast type " + cast.ToString() + " to type " + dest_ob.Type.ToString());
                    break;
            }
        }

        private void EvaluateBinary(ScriptVariable dest_ob, string op, string var1, string var2)
        {
            ScriptVariable source1_ob = Get_Var(var1);
            ScriptVariable source2_ob = Get_Var(var2);

            long dest_i = 0;
            double dest_d = 0;
            string dest_str = "";

            Var_Types cast = Var_Types.NULL;

            switch (op)
            {
                case "IS_RESISTED":
                        if ((source1_ob.Type == Var_Types.INT) && (source2_ob.Type == Var_Types.STRING))
                        {
                            uint test_id = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                            string command = (string)System.Convert.ToString(source2_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                            command = command.ToUpperInvariant();

                            TargetType idType = Util.GetType(test_id);
                            switch (idType)
                            {
                                case TargetType.ERROR:
                                case TargetType.SELF:
                                case TargetType.ITEM:
                                case TargetType.NONE:
                                case TargetType.MYPET:
                                case TargetType.MYPET1:
                                case TargetType.MYPET2:
                                case TargetType.MYPET3:
                                    Globals.l2net_home.Add_Error("IS_RESISTED requires a target player or mob!");
                                    dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                    cast = Var_Types.INT;
                                    break;
                                case TargetType.NPC:
                                case TargetType.PLAYER:
                                    switch (command)
                                    {
                                        case "USE_SKILL":
                                            ServerPackets.Try_Use_Skill_Smart(test_id, false, false);
                                            break;
                                        case "USE_SKILL_FORCE":
                                            ServerPackets.Try_Use_Skill_Smart(test_id, true, false);
                                            break;
                                        case "USE_SKILL_SHIFT":
                                            ServerPackets.Try_Use_Skill_Smart(test_id, false, true);
                                            break;
                                        case "USE_SKILL_FORCE_SHIFT":
                                            ServerPackets.Try_Use_Skill_Smart(test_id, true, true);
                                            break;
                                        default:
                                            goto USE_SKILL_SMART_COMMAND_ERROR;
                                    }
                                    while (System.DateTime.Now.Ticks < Globals.gamedata.my_char.ExpiresTime)
                                    {   
                                        System.Threading.Thread.Sleep(1);
                                    }
                                    if (Globals.gamedata.my_char.Resisted > 0)
                                    {
                                        dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                        cast = Var_Types.INT;
                                    }
                                    else
                                    {
                                        dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                        cast = Var_Types.INT;
                                    }
                                    break;
                                default:
                                    USE_SKILL_SMART_COMMAND_ERROR:
                                    Globals.l2net_home.Add_Error("IS_RESISTED was unable to resolve the ID passed to it.");
                                    dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                    cast = Var_Types.INT;
                                    break;
                            }
                        }
                        else
                        {                        
                            ScriptEngine.Script_Error("Usage: IF SKILL_ID IS_RESISTED (USE_SKILL|USE_SKILL_FORCE|USE_SKILL_SHIFT|USE_SKILL_FORCE_SHIFT)");
                        }
                    break;
                case "GROUP_HP":
                    if ((source1_ob.Type == Var_Types.INT) && (source2_ob.Type == Var_Types.INT))
                    {
                        uint party_num = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                        uint hp_thresh = (uint)System.Convert.ToUInt32(source2_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                        uint needshp = 0;
                        uint hpp = 0;

                        // Self
                        hpp = (uint)(100 * (Globals.gamedata.my_char.Cur_HP / Globals.gamedata.my_char.Max_HP));
                        if (hpp < hp_thresh)
                        {
                            needshp++;
                        }

                        Globals.PartyLock.EnterReadLock();
                        try
                        {
                            foreach (PartyMember pl in Globals.gamedata.PartyMembers.Values)
                            {

                                hpp = (uint)(100 * (pl.Cur_HP / pl.Max_HP));

                                if (hpp < hp_thresh)
                                {
                                    needshp++;
                                }
                            }
                            if (needshp >= party_num)
                            {
                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                cast = Var_Types.INT;
                            }
                            else
                            {
                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                cast = Var_Types.INT;
                            }
                        }
                        finally
                        {
                            Globals.PartyLock.ExitReadLock();
                        }
                    }
                    else
                    {
                        ScriptEngine.Script_Error("Usage: IF NUM_PARTY GROUP_HP INT_PERCENT");
                    }
                    break;
                case "GROUP_MP":
                    if ((source1_ob.Type == Var_Types.INT) && (source2_ob.Type == Var_Types.INT))
                    {
                        uint party_num = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                        uint mp_thresh = (uint)System.Convert.ToUInt32(source2_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                        uint needsmp = 0;
                        uint mpp = 0;

                        // Self
                        mpp = (uint)(100 * (Globals.gamedata.my_char.Cur_MP / Globals.gamedata.my_char.Max_MP));
                        if (mpp < mp_thresh)
                        {
                            needsmp++;
                        }

                        Globals.PartyLock.EnterReadLock();
                        try
                        {
                            foreach (PartyMember pl in Globals.gamedata.PartyMembers.Values)
                            {

                                mpp = (uint)(100 * (pl.Cur_MP / pl.Max_MP));

                                if (mpp < mp_thresh)
                                {
                                    needsmp++;
                                }

                                //if (mpp < mp_thresh)
                                //{
                                //    needsmp++;
                                //}
                            }
                            if (needsmp >= party_num)
                            {
                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                cast = Var_Types.INT;
                            }
                            else
                            {
                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                cast = Var_Types.INT;
                            }
                        }
                        finally
                        {
                            Globals.PartyLock.ExitReadLock();
                        }
                    }
                    else
                    {
                        ScriptEngine.Script_Error("Usage: IF NUM_PARTY GROUP_MP INT_PERCENT");
                    }
                    break;
                case "GROUP_CP":
                    if ((source1_ob.Type == Var_Types.INT) && (source2_ob.Type == Var_Types.INT))
                    {
                        uint party_num = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                        uint cp_thresh = (uint)System.Convert.ToUInt32(source2_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                        uint needscp = 0;
                        uint cpp = 0;
                        // Self
                        cpp = (uint)(100 * (Globals.gamedata.my_char.Cur_CP / Globals.gamedata.my_char.Max_CP));
                        if (cpp < cp_thresh)
                        {
                            needscp++;
                        }
                        Globals.PartyLock.EnterReadLock();
                        try
                        {
                            foreach (PartyMember pl in Globals.gamedata.PartyMembers.Values)
                            {                                 

                                cpp = (uint)(100 * (pl.Cur_CP / pl.Max_CP));

                                if (cpp < cp_thresh)
                                {
                                    needscp++;
                                }

                                //if (cpp < cp_thresh)
                                //{
                                //    needscp++;
                                //}
                            }
                            if (needscp >= party_num)
                            {
                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                cast = Var_Types.INT;
                            }
                            else
                            {
                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                cast = Var_Types.INT;
                            }
                        }
                        finally
                        {
                            Globals.PartyLock.ExitReadLock();
                        }
                    }
                    else
                    {
                        ScriptEngine.Script_Error("Usage: IF NUM_PARTY GROUP_CP INT_PERCENT");
                    }
                    break; 
                case "IN_RANGE":
                    if ((source1_ob.Type == Var_Types.INT) && (source2_ob.Type == Var_Types.INT))
                    {
                        uint test_id = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);

                        TargetType idType = Util.GetType(test_id);

                        // Globals.l2net_home.Add_Text("idType = " + idType, Globals.Cyan, TextType.BOT);
                        switch (idType)
                        {
                            case TargetType.ITEM:
                            case TargetType.MYPET:
                            case TargetType.MYPET1:
                            case TargetType.MYPET2:
                            case TargetType.MYPET3:
                            case TargetType.NPC:
                            case TargetType.PLAYER:                                    
                                if (Util.Distance(test_id) <= System.Convert.ToUInt32(source2_ob.Value, System.Globalization.CultureInfo.InvariantCulture))
                                {
                                    dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                    cast = Var_Types.INT;                                     
                                }
                                else
                                {
                                    dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                    cast = Var_Types.INT;                                     
                                }
                                break;
                            case TargetType.SELF:
                                dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                cast = Var_Types.INT;
                                break;
                            case TargetType.NONE:
                            case TargetType.ERROR:
                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                cast = Var_Types.INT;
                                return;
                            default:
                                Globals.l2net_home.Add_Error("IN_RANGE was not able to resolve the ID passed to it.");
                                dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                cast = Var_Types.INT;
                                break;
                        }
                    }
                    else
                    {
                        ScriptEngine.Script_Error("Usage: IF OBJECT_ID IN_RANGE DIST");
                    }
                    break;
                case "IS_CLAN":
                        if ((source1_ob.Type == Var_Types.INT) && (source2_ob.Type == Var_Types.STRING))
                        {
                            uint test_id = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                            string test_clan = (string)System.Convert.ToString(source2_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                            Clan_Info ci = null;
                            TargetType idType = Util.GetType(test_id);
                            switch (idType)
                            {
                                case TargetType.NONE:
                                case TargetType.ERROR:
                                case TargetType.ITEM:
                                case TargetType.MYPET:
                                case TargetType.MYPET1:
                                case TargetType.MYPET2:
                                case TargetType.MYPET3:
                                case TargetType.NPC:
                                    dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                    cast = Var_Types.INT;
                                    break;
                                case TargetType.SELF:
                                    
                                    Globals.PlayerLock.EnterReadLock();
                                    try
                                    {
                                        ci = (Clan_Info)Globals.clanlist[Globals.gamedata.my_char.ClanID];
                                    }
                                    finally
                                    {
                                        Globals.PlayerLock.ExitReadLock();
                                    }
                                    if (ci.ClanName.ToUpperInvariant() == test_clan.ToUpperInvariant())
                                    {
                                        dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                        cast = Var_Types.INT;
                                    }
                                    else
                                    {
                                        dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                        cast = Var_Types.INT;
                                    }
                                    break;
                                case TargetType.PLAYER:
                                    CharInfo player = null;

                                    Globals.PlayerLock.EnterReadLock();
                                    try
                                    {
                                        player = Util.GetChar(test_id);
                                        ci = (Clan_Info)Globals.clanlist[player.ClanID];
                                    }
                                    finally
                                    {
                                        Globals.PlayerLock.ExitReadLock();
                                    }
                                    if (ci.ClanName.ToUpperInvariant() == test_clan.ToUpperInvariant())
                                    {
                                        dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                        cast = Var_Types.INT;
                                    }
                                    else
                                    {
                                        dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                        cast = Var_Types.INT;
                                    }
                                    break;
                                default:
                                    Globals.l2net_home.Add_Error("IS_CLAN was unable to resolve the ID passed to it.");
                                    dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                    cast = Var_Types.INT;
                                    break;
                            }
                        }
                        else
                        {
                            ScriptEngine.Script_Error("Usage: IF OBJECT_ID IS_CLAN CLAN_NAME");
                        }
                    break;
                case "IS_ALLY":
                        if ((source1_ob.Type == Var_Types.INT) && (source2_ob.Type == Var_Types.STRING))
                        {
                            uint test_id = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                            string test_ally = (string)System.Convert.ToString(source2_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                            Clan_Info ci = null;
                            TargetType idType = Util.GetType(test_id);
                            switch (idType)
                            {
                                case TargetType.NONE:
                                case TargetType.ERROR:
                                case TargetType.ITEM:
                                case TargetType.MYPET:
                                case TargetType.MYPET1:
                                case TargetType.MYPET2:
                                case TargetType.MYPET3:
                                case TargetType.NPC:
                                    dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                    cast = Var_Types.INT;
                                    break;
                                case TargetType.SELF:

                                    Globals.PlayerLock.EnterReadLock();
                                    try
                                    {
                                        ci = (Clan_Info)Globals.clanlist[Globals.gamedata.my_char.ClanID];
                                    }
                                    finally
                                    {
                                        Globals.PlayerLock.ExitReadLock();
                                    }
                                    if (ci.AllyName.ToUpperInvariant() == test_ally.ToUpperInvariant())
                                    {
                                        dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                        cast = Var_Types.INT;
                                    }
                                    else
                                    {
                                        dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                        cast = Var_Types.INT;
                                    }
                                    break;
                                case TargetType.PLAYER:
                                    CharInfo player = null;

                                    Globals.PlayerLock.EnterReadLock();
                                    try
                                    {
                                        player = Util.GetChar(test_id);
                                        ci = (Clan_Info)Globals.clanlist[player.ClanID];
                                    }
                                    finally
                                    {
                                        Globals.PlayerLock.ExitReadLock();
                                    }
                                    if (ci.AllyName.ToUpperInvariant() == test_ally.ToUpperInvariant())
                                    {
                                        dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                        cast = Var_Types.INT;
                                    }
                                    else
                                    {
                                        dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                        cast = Var_Types.INT;
                                    }
                                    break;
                                default:
                                    Globals.l2net_home.Add_Error("IS_ALLY was not able to resolve the ID passed to it.");
                                    dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                    cast = Var_Types.INT;
                                    break;
                            }
                        }
                        else
                        {
                            ScriptEngine.Script_Error("Usage: IF OBJECT_ID IS_ALLY ALLY_NAME");
                        }
                    break;
                case "IS_CLASS":
                        if ((source1_ob.Type == Var_Types.INT) && (source2_ob.Type == Var_Types.STRING))
                        {
                            uint test_id = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                            string test_class = (string)System.Convert.ToString(source2_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                            string class_name;

                            TargetType idType = Util.GetType(test_id);
                            switch (idType)
                            {
                                case TargetType.NONE:
                                case TargetType.ERROR:
                                case TargetType.ITEM:
                                case TargetType.MYPET:
                                case TargetType.MYPET1:
                                case TargetType.MYPET2:
                                case TargetType.MYPET3:
                                case TargetType.NPC:
                                    dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                    cast = Var_Types.INT;
                                    break;
                                case TargetType.SELF:
                                    class_name = (string)System.Convert.ToString(Util.GetClass(Globals.gamedata.my_char.ActiveClass), System.Globalization.CultureInfo.InvariantCulture);

                                    if (class_name.ToUpperInvariant() == test_class.ToUpperInvariant())
                                    {
                                        dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                        cast = Var_Types.INT;
                                    }
                                    else
                                    {
                                        dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                        cast = Var_Types.INT;
                                    }
                                    break;
                                case TargetType.PLAYER:
                                    CharInfo player = null;

                                    Globals.PlayerLock.EnterReadLock();
                                    try
                                    {
                                        player = Util.GetChar(test_id);
                                    }
                                    finally
                                    {
                                        Globals.PlayerLock.ExitReadLock();
                                    }
                                    
                                    class_name = (string)System.Convert.ToString(Util.GetClass(player.Class), System.Globalization.CultureInfo.InvariantCulture);

                                    if (class_name.ToUpperInvariant() == test_class.ToUpperInvariant())
                                    {
                                        dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                        cast = Var_Types.INT;
                                    }
                                    else
                                    {
                                        dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                        cast = Var_Types.INT;
                                    }
                                    break;
                                default:
                                    Globals.l2net_home.Add_Error("IS_CLASS was not able to resolve the ID passed to it.");
                                    dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                    cast = Var_Types.INT;
                                    break;
                            }
                        }
                        else
                        {
                            ScriptEngine.Script_Error("Usage: IF OBJECT_ID IS_CLASS CLASS_NAME");
                        }
                    break;
                case "IS_ROOTCLASS":
                        if ((source1_ob.Type == Var_Types.INT) && (source2_ob.Type == Var_Types.STRING))
                        {
                            uint test_id = (uint)System.Convert.ToUInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                            string test_class = (string)System.Convert.ToString(source2_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                            string class_name;

                            TargetType idType = Util.GetType(test_id);
                            switch (idType)
                            {
                                case TargetType.NONE:
                                case TargetType.ERROR:
                                case TargetType.ITEM:
                                case TargetType.MYPET:
                                case TargetType.NPC:
                                    dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                    cast = Var_Types.INT;
                                    break;
                                case TargetType.SELF:
                                    class_name = (string)System.Convert.ToString(Util.GetClass(Globals.gamedata.my_char.Class), System.Globalization.CultureInfo.InvariantCulture);

                                    if (class_name.ToUpperInvariant() == test_class.ToUpperInvariant())
                                    {
                                        dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                        cast = Var_Types.INT;
                                    }
                                    else
                                    {
                                        dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                        cast = Var_Types.INT;
                                    }
                                    break;
                                case TargetType.PLAYER:
                                    CharInfo player = null;

                                    Globals.PlayerLock.EnterReadLock();
                                    try
                                    {
                                        player = Util.GetChar(test_id);
                                    }
                                    finally
                                    {
                                        Globals.PlayerLock.ExitReadLock();
                                    }

                                    class_name = (string)System.Convert.ToString(Util.GetClass(player.BaseClass), System.Globalization.CultureInfo.InvariantCulture);

                                    if (class_name.ToUpperInvariant() == test_class.ToUpperInvariant())
                                    {
                                        dest_i = System.Convert.ToInt64(Get_Value("TRUE").Value);
                                        cast = Var_Types.INT;
                                    }
                                    else
                                    {
                                        dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                        cast = Var_Types.INT;
                                    }
                                    break;
                                default:
                                    Globals.l2net_home.Add_Error("IS_ROOTCLASS was not able to resolve the ID passed to it.");
                                    dest_i = System.Convert.ToInt64(Get_Value("FALSE").Value);
                                    cast = Var_Types.INT;
                                    break;
                            }
                        }
                        else
                        {
                            ScriptEngine.Script_Error("Usage: IF OBJECT_ID IS_ROOTCLASS CLASS_NAME");
                        }
                    break;
                case "+":
                    if (source1_ob.Type == Var_Types.STRING)
                    {
                        dest_str = System.Convert.ToString(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture) + System.Convert.ToString(source2_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                        cast = Var_Types.STRING;
                    }
                    else if (source1_ob.Type == Var_Types.INT)
                    {
                        dest_i = System.Convert.ToInt64(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture) + System.Convert.ToInt64(source2_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                        cast = Var_Types.INT;
                    }
                    else if (source1_ob.Type == Var_Types.DOUBLE)
                    {
                        dest_d = System.Convert.ToDouble(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture) + System.Convert.ToDouble(source2_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                        cast = Var_Types.DOUBLE;
                    }
                    break;
                case "-":
                    if (source1_ob.Type == Var_Types.INT)
                    {
                        dest_i = System.Convert.ToInt64(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture) - System.Convert.ToInt64(source2_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                        cast = Var_Types.INT;
                    }
                    else if (source1_ob.Type == Var_Types.DOUBLE)
                    {
                        dest_d = System.Convert.ToDouble(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture) - System.Convert.ToDouble(source2_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                        cast = Var_Types.DOUBLE;
                    }
                    break;
                case "*":
                    if (source1_ob.Type == Var_Types.INT)
                    {
                        dest_i = System.Convert.ToInt64(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture) * System.Convert.ToInt64(source2_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                        cast = Var_Types.INT;
                    }
                    else if (source1_ob.Type == Var_Types.DOUBLE)
                    {
                        dest_d = System.Convert.ToDouble(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture) * System.Convert.ToDouble(source2_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                        cast = Var_Types.DOUBLE;
                    }
                    break;
                case "/":
                    if (source1_ob.Type == Var_Types.INT)
                    {
                        dest_i = System.Convert.ToInt64(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture) / System.Convert.ToInt64(source2_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                        cast = Var_Types.INT;
                    }
                    else if (source1_ob.Type == Var_Types.DOUBLE)
                    {
                        dest_d = System.Convert.ToDouble(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture) / System.Convert.ToDouble(source2_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                        cast = Var_Types.DOUBLE;
                    }
                    break;
                case "^":
                    if (source1_ob.Type == Var_Types.INT)
                    {
                        dest_i = (long)System.Math.Pow(System.Convert.ToDouble(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture), System.Convert.ToDouble(source2_ob.Value, System.Globalization.CultureInfo.InvariantCulture));
                        cast = Var_Types.INT;
                    }
                    else if (source1_ob.Type == Var_Types.DOUBLE)
                    {
                        dest_d = System.Math.Pow(System.Convert.ToDouble(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture), System.Convert.ToDouble(source2_ob.Value, System.Globalization.CultureInfo.InvariantCulture));
                        cast = Var_Types.DOUBLE;
                    }
                    break;
                case "%":
                    long tmp, tmp2;
                    if (source1_ob.Type == Var_Types.INT)
                    {
                        tmp = System.Math.DivRem(System.Convert.ToInt64(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture), System.Convert.ToInt64(source2_ob.Value, System.Globalization.CultureInfo.InvariantCulture), out tmp2);
                        dest_i = tmp2;
                        cast = Var_Types.INT;
                    }
                    else if (source1_ob.Type == Var_Types.DOUBLE)
                    {
                        tmp = System.Math.DivRem(System.Convert.ToInt64(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture), System.Convert.ToInt64(source2_ob.Value, System.Globalization.CultureInfo.InvariantCulture), out tmp2);
                        dest_d = (double)tmp2;
                        cast = Var_Types.DOUBLE;
                    }
                    break;
                case "LOG":
                    if (source1_ob.Type == Var_Types.INT)
                    {
                        dest_i = (long)System.Math.Log(System.Convert.ToDouble(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture), System.Convert.ToDouble(source2_ob.Value, System.Globalization.CultureInfo.InvariantCulture));
                        cast = Var_Types.INT;
                    }
                    else if (source1_ob.Type == Var_Types.DOUBLE)
                    {
                        dest_d = (double)System.Math.Log(System.Convert.ToDouble(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture), System.Convert.ToDouble(source2_ob.Value, System.Globalization.CultureInfo.InvariantCulture));
                        cast = Var_Types.DOUBLE;
                    }
                    break;
                case "||":
                case "OR":
                    if (source1_ob.Type == Var_Types.INT)
                    {
                        if (System.Convert.ToInt64(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture) != 0 || System.Convert.ToInt64(source2_ob.Value, System.Globalization.CultureInfo.InvariantCulture) != 0)
                            dest_i = 1;
                        else
                            dest_i = 0;
                        cast = Var_Types.INT;
                    }
                    else if (source1_ob.Type == Var_Types.DOUBLE)
                    {
                        if (System.Convert.ToDouble(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture) != 0 || System.Convert.ToDouble(source2_ob.Value, System.Globalization.CultureInfo.InvariantCulture) != 0)
                            dest_i = 1;
                        else
                            dest_i = 0;
                        cast = Var_Types.INT;
                    }
                    break;
                case "&&":
                case "AND":
                    if (source1_ob.Type == Var_Types.INT)
                    {
                        if (System.Convert.ToInt64(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture) != 0 && System.Convert.ToInt64(source2_ob.Value, System.Globalization.CultureInfo.InvariantCulture) != 0)
                            dest_i = 1;
                        else
                            dest_i = 0;
                        cast = Var_Types.INT;
                    }
                    else if (source1_ob.Type == Var_Types.DOUBLE)
                    {
                        if (System.Convert.ToDouble(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture) != 0 && System.Convert.ToDouble(source2_ob.Value, System.Globalization.CultureInfo.InvariantCulture) != 0)
                            dest_i = 1;
                        else
                            dest_i = 0;
                        cast = Var_Types.INT;
                    }
                    break;
                case "|":
                    if (source1_ob.Type == Var_Types.INT)
                    {
                        dest_i = System.Convert.ToInt64(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture) | System.Convert.ToInt64(source2_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                        cast = Var_Types.INT;
                    }
                    break;
                case "&":
                    if (source1_ob.Type == Var_Types.INT)
                    {
                        dest_i = System.Convert.ToInt64(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture) & System.Convert.ToInt64(source2_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                        cast = Var_Types.INT;
                    }
                    break;
                case "<<":
                    if (source1_ob.Type == Var_Types.INT)
                    {
                        dest_i = System.Convert.ToInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture) << System.Convert.ToInt32(source2_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                        cast = Var_Types.INT;
                    }
                    break;
                case ">>":
                    if (source1_ob.Type == Var_Types.INT)
                    {
                        dest_i = System.Convert.ToInt32(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture) >> System.Convert.ToInt32(source2_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                        cast = Var_Types.INT;
                    }
                    break;
                case "XOR":
                    if (source1_ob.Type == Var_Types.INT)
                    {
                        dest_i = System.Convert.ToInt64(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture) ^ System.Convert.ToInt64(source2_ob.Value, System.Globalization.CultureInfo.InvariantCulture);
                        cast = Var_Types.INT;
                    }
                    break;
                case "==":
                    if (source1_ob.Type == Var_Types.INT)
                    {
                        dest_i = System.Convert.ToInt64(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture) == System.Convert.ToInt64(source2_ob.Value, System.Globalization.CultureInfo.InvariantCulture) ? 1 :  0;
                        cast = Var_Types.INT;
                    }
                    else if (source1_ob.Type == Var_Types.DOUBLE)
                    {
                        dest_i = System.Convert.ToDouble(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture) == System.Convert.ToDouble(source2_ob.Value, System.Globalization.CultureInfo.InvariantCulture) ? 1 : 0;
                        cast = Var_Types.INT;
                    }
                    else if (source1_ob.Type == Var_Types.STRING)
                    {
                        dest_i = System.String.Compare(source1_ob.Value.ToString(), source2_ob.Value.ToString()) == 0 ? 1 : 0;
                        cast = Var_Types.INT;
                    }
                    else
                    {
                        if (source1_ob.Value == source2_ob.Value)
                        {
                            dest_i = 1;
                            cast = Var_Types.INT;
                        }
                        else
                        {
                            dest_i = 1;
                            cast = Var_Types.INT;
                        }
                    }
                    break;
                case "!=":
                    if (source1_ob.Type == Var_Types.INT)
                    {
                        dest_i = System.Convert.ToInt64(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture) != System.Convert.ToInt64(source2_ob.Value, System.Globalization.CultureInfo.InvariantCulture) ? 1 : 0;
                        cast = Var_Types.INT;
                    }
                    else if (source1_ob.Type == Var_Types.DOUBLE)
                    {
                        dest_i = System.Convert.ToDouble(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture) != System.Convert.ToDouble(source2_ob.Value, System.Globalization.CultureInfo.InvariantCulture) ? 1 : 0;
                        cast = Var_Types.INT;
                    }
                    else if (source1_ob.Type == Var_Types.STRING)
                    {
                        dest_i = System.String.Compare(source1_ob.Value.ToString(), source2_ob.Value.ToString()) != 0 ? 1 : 0;
                        cast = Var_Types.INT;
                    }
                    else
                    {
                        if (source1_ob.Value != source2_ob.Value)
                        {
                            dest_i = 1;
                            cast = Var_Types.INT;
                        }
                        else
                        {
                            dest_i = 1;
                            cast = Var_Types.INT;
                        }
                    }
                    break;
                case "<":
                    if (source1_ob.Type == Var_Types.INT)
                    {
                        dest_i = System.Convert.ToInt64(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture) < System.Convert.ToInt64(source2_ob.Value, System.Globalization.CultureInfo.InvariantCulture) ? 1 : 0;
                        cast = Var_Types.INT;
                    }
                    else if (source1_ob.Type == Var_Types.DOUBLE)
                    {
                        dest_i = System.Convert.ToDouble(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture) < System.Convert.ToDouble(source2_ob.Value, System.Globalization.CultureInfo.InvariantCulture) ? 1 : 0;
                        cast = Var_Types.INT;
                    }
                    break;
                case "<=":
                    if (source1_ob.Type == Var_Types.INT)
                    {
                        dest_i = System.Convert.ToInt64(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture) <= System.Convert.ToInt64(source2_ob.Value, System.Globalization.CultureInfo.InvariantCulture) ? 1 : 0;
                        cast = Var_Types.INT;
                    }
                    else if (source1_ob.Type == Var_Types.DOUBLE)
                    {
                        dest_i = System.Convert.ToDouble(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture) <= System.Convert.ToDouble(source2_ob.Value, System.Globalization.CultureInfo.InvariantCulture) ? 1 : 0;
                        cast = Var_Types.INT;
                    }
                    break;
                case ">":
                    if (source1_ob.Type == Var_Types.INT)
                    {
                        dest_i = System.Convert.ToInt64(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture) > System.Convert.ToInt64(source2_ob.Value, System.Globalization.CultureInfo.InvariantCulture) ? 1 : 0;
                        cast = Var_Types.INT;
                    }
                    else if (source1_ob.Type == Var_Types.DOUBLE)
                    {
                        dest_i = System.Convert.ToDouble(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture) > System.Convert.ToDouble(source2_ob.Value, System.Globalization.CultureInfo.InvariantCulture) ? 1 : 0;
                        cast = Var_Types.INT;
                    }
                    break;
                case ">=":
                    if (source1_ob.Type == Var_Types.INT)
                    {
                        dest_i = System.Convert.ToInt64(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture) >= System.Convert.ToInt64(source2_ob.Value, System.Globalization.CultureInfo.InvariantCulture) ? 1 : 0;
                        cast = Var_Types.INT;
                    }
                    else if (source1_ob.Type == Var_Types.DOUBLE)
                    {
                        dest_i = System.Convert.ToDouble(source1_ob.Value, System.Globalization.CultureInfo.InvariantCulture) >= System.Convert.ToDouble(source2_ob.Value, System.Globalization.CultureInfo.InvariantCulture) ? 1 : 0;
                        cast = Var_Types.INT;
                    }
                    break;
                default:
                    ScriptEngine.Script_Error("UNKNOWN MATH TYPE : " + op);
                    break;
            }

            if (cast == Var_Types.NULL)
            {
                //we tried an operation that didn't succeed
                ScriptEngine.Script_Error("invalid type: can't perform [" + source1_ob.Name + "] [" + op + "] [" + source2_ob.Name + "]");
            }


            switch (dest_ob.Type)
            {
                case Var_Types.INT:
                    switch (cast)
                    {
                        case Var_Types.INT:
                            dest_ob.Value = dest_i;
                            break;
                        case Var_Types.DOUBLE:
                            try
                            {
                                dest_ob.Value = System.Convert.ToInt64(dest_d);
                            }
                            catch
                            {
                                //cant cast NaN to int
                                dest_ob.Value = long.MinValue;
                            }
                            break;
                    }
                    break;
                case Var_Types.DOUBLE:
                    switch (cast)
                    {
                        case Var_Types.INT:
                            dest_ob.Value = System.Convert.ToDouble(dest_i);
                            break;
                        case Var_Types.DOUBLE:
                            dest_ob.Value = dest_d;
                            break;
                    }
                    break;
                case Var_Types.STRING:
                    switch (cast)
                    {
                        case Var_Types.INT:
                            dest_ob.Value = System.Convert.ToString(dest_i);
                            break;
                        case Var_Types.DOUBLE:
                            dest_ob.Value = System.Convert.ToString(dest_d);
                            break;
                        case Var_Types.STRING:
                            dest_ob.Value = dest_str;
                            break;
                    }
                    break;
                case Var_Types.ASSIGNABLE:
                    switch (cast)
                    {
                        case Var_Types.INT:
                            dest_ob.Value = dest_i;
                            dest_ob.Type = Var_Types.INT;
                            break;
                        case Var_Types.DOUBLE:
                            dest_ob.Value = dest_d;
                            dest_ob.Type = Var_Types.DOUBLE;
                            break;
                        case Var_Types.STRING:
                            dest_ob.Value = dest_str;
                            dest_ob.Type = Var_Types.STRING;
                            break;
                    }
                    break;
                default:
                    //script error
                    ScriptEngine.Script_Error("couldn't cast type " + cast.ToString() + " to type " + dest_ob.Type.ToString());
                    break;
            }
        }

        private bool isOp(string token)
        {
            if (isUnaryOp(token) || isBinaryOp(token) || isParan(token))
            {
                return true;
            }
            return false;
        }

        private bool isParan(string token)
        {
            switch (token)
            {
                case "(":
                case ")":
                    return true;
            }

            return false;
        }

        private bool isUnaryOp(string token)
        {
            switch (token)
            {
                case "IS_ITEM_EQUIPPED":
                case "IS_AUGMENT_EQUIPPED":
                case "IS_SA_EQUIPPED":
                case "IS_READY":
                case "IS_SHOP":
                case "IS_NOBLE":
                case "IS_HERO":
                case "IS_DUELING":
                case "IS_FAKEDEATH":
                case "IS_INVISIBLE":
                case "IS_INCOMBAT":
                case "IS_SITTING":
                case "IS_WALKING":
                case "IS_FISHING":
                case "IS_DEMONSWORD":
                case "IS_TRANSFORMED":
                case "IS_AGATHON":
                case "IS_FINDPARTY":
                case "IS_USINGCUBIC":
                case "IS_FLAGGED":
                case "IS_RED":
                case "IS_PARTYMEMBER":
                case "IS_PARTYLEADER":
                case "IS_INPARTY":
                case "IS_CLANMEMBER":
                case "IS_LEADER":
                case "IS_CLANMATE":
                case "IS_INSIEGE":
                case "IS_SIEGEATTACKER":
                case "IS_SIEGEALLY":
                case "IS_SIEGEENEMY":
                case "IS_MUTUALWAR":
                case "IS_ONESIDEDWAR":
                case "IS_ALLYMEMBER":
                case "IS_TWAR":
                case "IS_BLEEDING":
                case "IS_POISONED":
                case "IS_REDCIRCLE":
                case "IS_ICE":
                case "IS_WIND":
                case "IS_AFRAID":
                case "IS_STUNNED":
                case "IS_ASLEEP":
                case "IS_MUTE":
                case "IS_ROOTED":
                case "IS_PARALYZED":
                case "IS_PETRIFIED":
                case "IS_BURNING":
                case "IS_FLOATING_ROOT":
                case "IS_DANCE_STUNNED":
                case "IS_FIREROOT_STUN":
                case "IS_STEALTH":
                case "IS_IMPRISIONING_1":
                case "IS_IMPRISIONING_2":
                case "IS_SOE":
                case "IS_ICE2":
                case "IS_EARTHQUAKE":
                case "IS_INVULNERABLE":
                case "IS_REGEN_VITALITY":
                case "IS_REAL_TARGETED":
                case "IS_DEATH_MARKED":
                case "IS_TERRIFIED":
                case "IS_CONFUSED":
                case "IS_INVINCIBLE":
                case "IS_AIR_STUN":
                case "IS_AIR_ROOT":
                case "IS_STIGMAED":
                case "IS_STAKATOROOT":
                case "IS_FREEZING":
                case "IS_DISABLED":
                case "IN_POLY":
                case "SQRT":
                case "ABS":
                case "SIN":
                case "SINH":
                case "ASIN":
                case "COS":
                case "COSH":
                case "ACOS":
                case "TAN":
                case "TANH":
                case "ATAN":
                case "~"://-
                case "VARIABLE_DEFINED":
                case "IS_DEFINED":
                case "[]":
                case "??":
                case "IS_RUNNING":
                case "IS_NOT_RUNNING":
                case "FILE_EXISTS":
                case "?I":
                case "?D":
                case "?$":
                case "?S":
                case "?A":
                case "?ST":
                case "?Q":
                case "?FR":
                case "?FW":
                case "?B":
                case "?W":
                case "?C":
                case "!":
                    return true;
            }

            return false;
        }

        private bool isBinaryOp(string token)
        {
            switch (token)
            {
                case "GROUP_HP":
                case "GROUP_MP":
                case "GROUP_CP":
                case "IS_RESISTED":
                case "IN_RANGE":
                case "IS_CLAN":
                case "IS_ALLY":
                case "IS_CLASS":
                case "IS_ROOT_CLASS":
                case "+":
                case "-":
                case "*":
                case "/":
                case "^":
                case "%":
                case "LOG":
                case "||":
                case "OR":
                case "&&":
                case "AND":
                case "|":
                case "&":
                case ">>":
                case "<<":
                case "XOR":
                case "==":
                case "!=":
                case "<":
                case "<=":
                case ">":
                case ">=":
                    return true;
            }

            return false;
        }

        private bool isFunction(string token)
        {
            return false;
        }
    }
}
