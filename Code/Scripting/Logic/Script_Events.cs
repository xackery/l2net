using System;
using System.Collections.Generic;
using System.Text;

namespace L2_login
{
    public enum EventType : byte
    {
        Chat = 0,
        SelfDie = 1,
        SelfRez = 2,
        SelfEnterCombat = 3,
        SelfLeaveCombat = 4,
        SelfStopMove = 5,
        SelfTargeted = 6,
        SelfUnTargeted = 7,
        TargetDie = 8,
        ChatToBot = 9,
        UDPReceive = 10,
        SkillUsed = 11,
        SkillLaunched = 12,
        SkillCanceled = 13,
        OtherSkillUsed = 14,
        OtherSkillLaunched = 15,
        OtherSkillCanceled = 16,
        JoinParty = 17,
        LeaveParty = 18,
        UDPReceiveBB = 19,
        ServerPacket = 20,
        ServerPacketEX = 21,
        SystemMessage = 22,
        PartyInvite = 23,
        TradeInvite = 24,
        ClientPacket = 25,
        ClientPacketEX = 26,
        Aggro = 27,
        SelfPacket = 28,
        SelfPacketEX = 29,
        CharEffect = 30,
        PartyEffect = 31

    }

    public class ScriptEvent
    {
        public System.Collections.ArrayList Variables = new System.Collections.ArrayList();
        public EventType Type;
        public int Type2;
    }

    public class ScriptEventCaller
    {
        private string _file;

        public EventType Type;
        public string Function;

        public string File
        {
            get
            {
                return _file;
            }
            set
            {
                if (System.IO.File.Exists(value))
                {
                    _file = value;
                }
                else
                {
                    _file = Globals.PATH + "\\Scripts\\" + value;
                }
            }
        }

    }
}
