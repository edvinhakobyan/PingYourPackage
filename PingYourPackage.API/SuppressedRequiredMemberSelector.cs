﻿using System.Net.Http.Formatting;
using System.Reflection;

namespace PingYourPackage.API
{
    internal class SuppressedRequiredMemberSelector : IRequiredMemberSelector
    {
        public bool IsRequiredMember(MemberInfo member)
        {
            return false;
        }
    }
}