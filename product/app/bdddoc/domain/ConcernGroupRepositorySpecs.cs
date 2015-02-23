using Observation = MbUnit.Framework.TestAttribute;

namespace bdddoc.domain
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using core;
    using spechelpers;

    public abstract class context_for_concern_group_repository : ContextSpecification<IConcernGroupRepository>
    {
        protected IEnumerable<IConcernGroup> all_groups;
        protected IReportOptions options;
        protected IConcernFactory concern_factory;
        protected IObservationSpecification observation_specification;
        protected IConcern specific_concern;

        protected override void establish_context()
        {
            options = dependency<IReportOptions>();
            observation_specification = dependency<IObservationSpecification>();
            concern_factory = dependency<IConcernFactory>();
            specific_concern = dependency<IConcern>();
        }

        protected override IConcernGroupRepository create_sut()
        {
            return new ConcernGroupRepository(concern_factory);
        }
    }

    [Concern(typeof (ConcernGroupRepository))]
    public class when_a_concern_group_repository_is_told_to_find_all_of_The_concern_groups : context_for_concern_group_repository
    {
        protected override void establish_context()
        {
            base.establish_context();

            options.setup_result(x => x.assembly_to_scan).Return(Assembly.GetExecutingAssembly());
            specific_concern.setup_result(x => x.concerned_with).Return(typeof (int));
            concern_factory.setup_result(x => x.create_concern_from(null, observation_specification)).IgnoreArguments().Return(specific_concern);

            sut = create_sut();
        }

        protected override void because()
        {
            all_groups = sut.all_concern_groups_found_using(options);
        }


        [Observation]
        public void should_find_all_of_the_groups_in_the_assembly_using_the_options_it_was_created_with()
        {
            all_groups.Where(x => x.concerned_with.Equals(typeof (int))).Count().should_be_greater_than(0);
        }
    }
}