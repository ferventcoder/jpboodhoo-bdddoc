namespace bdddoc.spechelpers
{
    using System;
    using System.Reflection;

    public static class Method
    {
        public static MethodInfo pointed_at_by(Action action)
        {
            return action.Method;
        }
    }
}