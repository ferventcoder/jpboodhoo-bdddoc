using System;
using System.Reflection;

namespace bdddoc.spechelpers
{
    public static class Method
    {
        public static MethodInfo pointed_at_by(Action action)
        {
            return action.Method;
        }
    }
}