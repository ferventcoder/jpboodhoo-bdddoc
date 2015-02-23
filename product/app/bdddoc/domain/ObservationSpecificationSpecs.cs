using System.Reflection;
using bdddoc.core;
using bdddoc.spechelpers;
using Observation = MbUnit.Framework.TestAttribute;

namespace bdddoc.domain
{
    public abstract class context_for_observation_specification : ContextSpecification<ObservationSpecification>
    {
        protected override ObservationSpecification create_sut()
        {
            return new ObservationSpecification("TestAttribute");
        }
    }

    [Concern(typeof (ObservationSpecification))]
    public class when_determining_if_a_method_is_an_observation : context_for_observation_specification
    {
        private MethodInfo a_method_that_is_an_observation;
        private MethodInfo a_method_that_is_not_an_observation;

        protected override void establish_context()
        {
            var a_concern_with_observations = new when_a_number_is_told_to_subtract_itself_to_another_number();
            a_method_that_is_an_observation = Method.pointed_at_by(a_concern_with_observations.should_add_one_number_to_another);
            a_method_that_is_not_an_observation = Method.pointed_at_by(a_concern_with_observations.a_method_that_is_not_an_observation);

            sut = create_sut();
        }


        protected override void because()
        {
        }


        [Observation]
        public void should_be_an_observation_if_it_has_the_specified_named_observation_attribute()
        {
            sut.IsSatisfiedBy(a_method_that_is_an_observation).should_be_true();
            sut.IsSatisfiedBy(a_method_that_is_not_an_observation).should_be_false();
        }
    }
}