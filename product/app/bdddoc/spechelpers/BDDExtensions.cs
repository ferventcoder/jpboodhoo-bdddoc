namespace bdddoc.spechelpers
{
    using System;
    using Rhino.Mocks;
    using Rhino.Mocks.Interfaces;

    public static class RhinoMocksExtensions
    {
        public static void was_told_to<T>(this T mock, Action<T> item)
        {
            mock.AssertWasCalled(item);
        }

        public static IMethodOptions<R> setup_result<T, R>(this T mock, Func<T, R> func)
        {
            return mock.Expect(func).Repeat.AtLeastOnce();
        }
    }
}