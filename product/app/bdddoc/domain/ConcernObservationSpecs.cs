using bdddoc.core;
using bdddoc.spechelpers;
using Observation = MbUnit.Framework.TestAttribute;

namespace bdddoc.domain
{
    [Concern(typeof (ConcernObservation))]
    public class when_a_concern_observation_is_asked_for_its_name : ContextSpecification<ConcernObservation>
    {
        private string name;

        protected override void establish_context()
        {
            sut = create_sut();
        }

        protected override void because()
        {
            name = sut.name;
        }


        [Observation]
        public void should_return_the_value_of_its_bdd_name()
        {
            name.should_be_equal_ignoring_case(sut.name);
        }

        protected override ConcernObservation create_sut()
        {
            return new ConcernObservation(new BDDStyleName("what_is_in_a_name))"));
        }
    }
}