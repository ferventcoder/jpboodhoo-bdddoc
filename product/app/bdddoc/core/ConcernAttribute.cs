namespace bdddoc.core
{
    using System;

    [AttributeUsage(AttributeTargets.Class)]
    public class ConcernAttribute : Attribute
    {
        public Type concerned_with { get; private set; }

        public ConcernAttribute(Type concern)
        {
            concerned_with = concern;
        }
    }
}