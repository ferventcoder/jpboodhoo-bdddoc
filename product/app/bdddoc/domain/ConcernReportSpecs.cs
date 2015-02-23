using System.Collections.Generic;
using bdddoc.core;
using bdddoc.spechelpers;
using Observation = MbUnit.Framework.TestAttribute;

namespace bdddoc.domain
{
    public abstract class context_for_concern_report : ContextSpecification<IConcernReport>  
    {
        protected List<IConcernGroup> concern_groups_in_report;

        protected override void establish_context()
        {
            concern_groups_in_report = new List<IConcernGroup>();
        }

        protected override IConcernReport create_sut()
        {
            return new ConcernReport(concern_groups_in_report);
        }
    }

    [Concern(typeof (ConcernReport))]
    public class when_a_concern_report_with_concern_groups_is_asked_for_the_number_of_concerns_it_has : context_for_concern_report
    {
        private int total_number_of_concerns;
        private IConcernGroup first_concern_group;
        private IConcernGroup second_concern_group;

        protected override void establish_context()
        {
            base.establish_context();

            first_concern_group = dependency<IConcernGroup>();
            second_concern_group = dependency<IConcernGroup>();

            first_concern_group.setup_result(x => x.total_number_of_concerns).Return(15);
            second_concern_group.setup_result(x => x.total_number_of_concerns).Return(5);
            concern_groups_in_report.Add(first_concern_group);
            concern_groups_in_report.Add(second_concern_group);

            sut = create_sut();
        }

        protected override void because()
        {
            total_number_of_concerns = sut.total_number_of_concerns;
        }


        [Observation]
        public void should_sum_up_the_number_of_concerns_in_all_of_its_concern_groups()
        {
            total_number_of_concerns.should_be_equal_to(20);
        }
    }

    [Concern(typeof (ConcernReport))]
    public class when_a_concern_report_with_concern_groups_is_asked_for_the_number_of_observations_it_has : context_for_concern_report
    {
        private int total_number_of_observations;
        private IConcernGroup first_concern_group;
        private IConcernGroup second_concern_group;

        protected override void establish_context()
        {
            base.establish_context();
            first_concern_group = dependency<IConcernGroup>();
            second_concern_group = dependency<IConcernGroup>();

            first_concern_group.setup_result(x => x.total_number_of_observations).Return(15);
            second_concern_group.setup_result(x => x.total_number_of_observations).Return(20);
            concern_groups_in_report.Add(first_concern_group);
            concern_groups_in_report.Add(second_concern_group);

            sut = create_sut();
        }

        protected override void because()
        {
            total_number_of_observations = sut.total_number_of_observations;
        }


        [Observation]
        public void should_sum_up_the_number_of_observations_in_all_of_its_concern_groups()
        {
            total_number_of_observations.should_be_equal_to(35);
        }
    }
}