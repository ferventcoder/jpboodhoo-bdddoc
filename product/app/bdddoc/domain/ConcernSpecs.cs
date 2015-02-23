using System.Collections.Generic;
using bdddoc.core;
using bdddoc.spechelpers;
using Observation = MbUnit.Framework.TestAttribute;

namespace bdddoc.domain
{
    public abstract class context_for_concern : ContextSpecification<IConcern>
    {
        protected string name_of_concern;
        protected IConcernObservation observation;
        protected List<IConcernObservation> observations;

        protected override IConcern create_sut()
        {
            return new Concern(typeof (int), new BDDStyleName("when_looking_for_a_certain_number"), observations);
        }

        protected override void establish_context()
        {
            observation = dependency<IConcernObservation>();
            observations = new List<IConcernObservation>();

            sut = create_sut();
        }

    }

    [Concern(typeof (Concern))]
    public class when_a_concern_is_asked_for_its_name : context_for_concern
    {

        protected override void because()
        {
            name_of_concern = sut.name;
        }


        [Observation]
        public void should_return_the_value_of_its_bdd_name()
        {
            name_of_concern.should_be_equal_ignoring_case(sut.name);
        }
    }

    [Concern(typeof (Concern))]
    public class when_a_concern_is_created_with_observations : context_for_concern
    {
        protected override void establish_context()
        {
            base.establish_context();
            observations.Add(observation);
        }

        protected override void because()
        {
            sut = create_sut();
        }

        [Observation]
        public void should_be_able_to_access_them_later()
        {
            sut.observations.should_contain(observation);
        }
    }

    [Concern(typeof (Concern))]
    public class when_calculating_the_total_number_of_observations : context_for_concern
    {
        private int total_number_of_observations;

        protected override void establish_context()
        {
            base.establish_context();
            observations.Add(dependency<IConcernObservation>());
            observations.Add(dependency<IConcernObservation>());
            sut = create_sut();
        }

        protected override void because()
        {
            total_number_of_observations = sut.total_number_of_observations;
        }

        [Observation]
        public void should_sum_the_number_of_observations_it_contains()
        {
            total_number_of_observations.should_be_equal_to(2);
        }
    }
}