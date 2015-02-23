using System.Collections.Generic;
using bdddoc.core;
using bdddoc.spechelpers;
using Observation = MbUnit.Framework.TestAttribute;

namespace bdddoc.utility
{
    [Concern(typeof (EnumerableExtensions))]
    public class when_an_enumerable_is_walked_one_item_at_a_time : StaticContextSpecification
    {
        private IEnumerable<int> all_items_one_at_a_time;

        protected override void establish_context()
        {
        }

        protected override void because()
        {
            all_items_one_at_a_time = new List<int> {1, 2, 3, 4}.one_at_a_time();
        }


        [Observation]
        public void should_get_an_set_of_items_that_contains_each_of_the_original_items()
        {
            all_items_one_at_a_time.should_only_contain(1, 2, 3, 4);
        }
    }

    [Concern(typeof (EnumerableExtensions))]
    public class when_each_is_invoked : StaticContextSpecification
    {
        private IList<int> all_items;

        protected override void establish_context()
        {
            all_items = new List<int>();
        }

        protected override void because()
        {
            new List<int> {1, 2, 3, 4}.each(x => all_items.Add(x));
        }


        [Observation]
        public void should_perform_the_action_for_each_item_in_the_enumerable()
        {
            all_items.should_only_contain(1, 2, 3, 4);
        }
    }
}