using Observation = MbUnit.Framework.TestAttribute;

namespace bdddoc.domain
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using core;
    using spechelpers;

    public abstract class context_for_concern_report_factory : ContextSpecification<IConcernReportFactory>
    {
        protected IConcernReport report;
        protected Assembly assembly;
        protected IConcernGroupRepository concern_group_repository;
        protected List<IConcernGroup> concern_groups;
        protected IReportOptions options;

        protected override void establish_context()
        {
            concern_group_repository = dependency<IConcernGroupRepository>();
            concern_groups = new List<IConcernGroup>();
            options = dependency<IReportOptions>();
            assembly = Assembly.GetExecutingAssembly();

            sut = new ConcernReportFactory(concern_group_repository);
        }

        protected override IConcernReportFactory create_sut()
        {
            return new ConcernReportFactory(concern_group_repository);
        }
    }

    [Concern(typeof (ConcernReportFactory))]
    public class when_the_concern_report_factory_is_told_to_create_a_concern_report : context_for_concern_report_factory
    {
        protected override void establish_context()
        {
            base.establish_context();
            concern_groups.Add(dependency<IConcernGroup>());
            concern_groups.Add(dependency<IConcernGroup>());

            concern_group_repository.setup_result(x => x.all_concern_groups_found_using(options)).Return(concern_groups);
        }

        protected override void because()
        {
            report = sut.create_using(options);
        }


        [Observation]
        public void should_create_a_report_that_consists_of_all_the_concern_groups_found_by_the_repository()
        {
            report.groups.Count().should_be_equal_to(2);
        }
    }
}