using Observation = MbUnit.Framework.TestAttribute;

namespace bdddoc.domain
{
    using core;
    using spechelpers;

    [Concern(typeof (ConcernFactory))]
    public class when_a_concern_factory_is_told_to_create_a_concern_from_a_type : ContextSpecification<IConcernFactory>
    {
        private IConcern concern;
        private IObservationSpecification observation_specification;

        protected override void establish_context()
        {
            observation_specification = dependency<IObservationSpecification>();
            observation_specification.setup_result(x => x.IsSatisfiedBy(null)).IgnoreArguments().Return(true);

            sut = create_sut();
        }


        protected override void because()
        {
            concern = sut.create_concern_from(typeof (when_a_decimal_is_told_to_subtract_itself_to_another_number), observation_specification);
        }


        [Observation]
        public void should_create_a_concern_with_the_correct_bdd_style_name()
        {
            concern.name.should_be_equal_to(typeof (when_a_decimal_is_told_to_subtract_itself_to_another_number).Name.as_bdd_style_name());
        }

        [Observation]
        public void should_have_the_concern_populated_with_all_of_the_observations_satisfied_by_the_specification()
        {
            concern.total_number_of_observations.should_be_equal_to(3);
        }

        protected override IConcernFactory create_sut()
        {
            return new ConcernFactory();
        }
    }
}