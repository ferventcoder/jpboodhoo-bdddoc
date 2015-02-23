namespace bdddoc.spechelpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using MbUnit.Framework;

    public static class BDDAssertions
    {
        public static void should_be_equal_ignoring_case(this string item, string other)
        {
            StringAssert.AreEqualIgnoreCase(item, other);
        }

        public static void should_be_equal_to<T>(this T item, T result)
        {
            Assert.AreEqual(result, item);
        }

        public static void should_not_be_null<T>(this T item) where T : class
        {
            Assert.IsNotNull(item);
        }

        public static void should_be_true(this bool item)
        {
            Assert.IsTrue(item);
        }

        public static void should_be_false(this bool item)
        {
            Assert.IsFalse(item);
        }

        public static void should_be_an_instance_of(this object item, Type type)
        {
            Assert.IsInstanceOfType(type, item);
        }

        public static void should_be_an_instance_of<Item>(this object item)
        {
            item.should_be_an_instance_of(typeof (Item));
        }

        public static void should_be_greater_than<T>(this T item, T value) where T : IComparable
        {
            Assert.GreaterThan(item, value);
        }

        public static void should_not_contain<T>(this IEnumerable<T> items, T item)
        {
            items.Contains(item).should_be_false();
        }

        public static void should_contain<T>(this IEnumerable<T> items, T item)
        {
            items.Contains(item).should_be_true();
        }


        public static void should_contain<T>(this IEnumerable<T> items, params T[] itemsToFind)
        {
            foreach (var itemToFind in itemsToFind)
            {
                items.should_contain(itemToFind);
            }
        }

        public static void should_only_contain<T>(this IEnumerable<T> items, params T[] itemsToFind)
        {
            items.Count().should_be_equal_to(itemsToFind.Length);
            items.should_contain(itemsToFind);
        }
    }
}