using System;
using System.Collections.Generic;
using System.Reflection;
using bdddoc.core;
using bdddoc.spechelpers;
using Observation = MbUnit.Framework.TestAttribute;

namespace bdddoc.domain
{
    [Concern(typeof (BDDReflectionExtensions))]
    public class when_determining_whether_a_type_is_a_concern : StaticContextSpecification
    {
        private Type type_that_is_a_concern;
        private Type type_that_is_not_a_concern;

        protected override void establish_context()
        {
            type_that_is_a_concern = typeof (when_a_decimal_is_told_to_subtract_itself_to_another_number);
            type_that_is_not_a_concern = typeof (something_that_is_not_a_concern);
        }

        protected override void because()
        {
        }

        [Observation]
        public void should_only_be_a_concern_if_it_has_the_concern_attribute()
        {
            type_that_is_a_concern.is_a_concern().should_be_true();
            type_that_is_not_a_concern.is_a_concern().should_be_false();
        }
    }

    [Concern(typeof (BDDReflectionExtensions))]
    public class when_building_a_concern_observation_from_a_method_that_is_an_observation : StaticContextSpecification
    {
        private IConcernObservation result;
        private MethodInfo observation;

        protected override void establish_context()
        {
            var type_with_observations = new when_a_decimal_is_told_to_subtract_itself_to_another_number();
            observation = Method.pointed_at_by(type_with_observations.should_jump_one_to_another_and_see_what_is_said);
        }

        protected override void because()
        {
            result = observation.as_observation();
        }

        [Observation]
        public void should_create_a_concern_observation_that_has_the_bdd_style_name()
        {
            result.should_not_be_null();
            result.name.should_be_equal_to(new BDDStyleName(observation.Name));
        }
    }

    [Concern(typeof (BDDReflectionExtensions))]
    public class when_finding_all_concerns_in_a_specific_assembly : StaticContextSpecification
    {
        private IEnumerable<Type> concern_types;

        protected override void establish_context()
        {
        }

        protected override void because()
        {
            concern_types = Assembly.GetExecutingAssembly().all_types_with_a_concern_attribute();
        }


        [Observation]
        public void should_return_all_types_that_have_the_concern_attribute_applied_to_them()
        {
            concern_types.should_contain(typeof (when_a_decimal_is_told_to_subtract_itself_to_another_number),
                                         typeof (when_a_number_is_told_to_add_itself_to_another_number),
                                         typeof (when_a_number_is_told_to_subtract_itself_to_another_number));
        }

        [Observation]
        public void should_not_return_types_that_do_not_have_the_concern_attribute_on_them()
        {
            concern_types.should_not_contain(typeof (something_that_is_not_a_concern));
        }
    }
}