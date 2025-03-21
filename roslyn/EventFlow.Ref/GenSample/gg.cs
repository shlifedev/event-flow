using System;
using System.Collections.Generic;
using System.Reflection;
using EventFlow.Ref.Listeners;
using EventFlow.Ref.Messages;

namespace LD.Framework.EventFlow
{
    public partial class EventFlow
    {
        public static void Register(ChatApplication target)
        {
            EventFlowGeneric<TestUserChatMessage>.Register(target);
        }
        
        public static void Unregister(ChatApplication target)
        {
            EventFlowGeneric<TestUserChatMessage>.Register(target);
        }
    }
}