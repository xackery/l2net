using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace L2_login
{
    class FollowOption
    {
        public volatile int WalkerStyle = 0;
        public volatile FollowAssistOption LogicWhenIdle = new FollowAssistOption();
        public volatile FollowAssistOption LogicWhenCombat = new FollowAssistOption();
    }
}
