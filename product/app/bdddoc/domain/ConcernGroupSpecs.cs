using Observation = MbUnit.Framework.TestAttribute;

namespace bdddoc.domain
{
    using System;
    using System.Collections.Generic;
    using core;
    using spechelpers;

    public abstract class context_for_concern_group : ContextSpecification<IConcernGroup>
    {
        protected IConcernGroup sut;
        protected IConcern concern_in_group;
        protected IList<IConcern> concerns_in_group;

        protected override void establish_context()
        {
            concern_in_group = dependency<IConcern>();
        }

        protected override IConcernGroup create_sut()
        {
            return new ConcernGroup(concerns_in_group);
        }
    }

    [Concern(typeof (ConcernGroup))]
    public class when_a_concern_group_is_created_with_a_list_of_concerns : context_for_concern_group
    {
        protected override void establish_context()
        {
            concerns_in_group = new List<IConcern> {concern_in_group};
        }

        protected override void because()
        {
            sut = create_sut();
        }


        [Observation]
        public void should_be_able_to_access_the_concerns_later()
        {
            sut.concerns.should_contain(concern_in_group);
        }
    }

    [Concern(typeof (ConcernGroup))]
    public class when_a_concern_group_is_asked_for_its_concern_type : context_for_concern_group
    {
        private Type concerned_with;

        protected override void establish_context()
        {
            base.establish_context();
            concern_in_group.setup_result(x => x.concerned_with).Return(typeof (int));
            concerns_in_group = new List<IConcern> {concern_in_group};
            sut = create_sut();
        }

        protected override void because()
        {
            concerned_with = sut.concerned_with;
        }


        [Observation]
        public void should_return_the_concern_type_of_its_first_concern()
        {
            concerned_with.should_be_equal_to(typeof (int));
        }
    }

    [Concern(typeof (ConcernGroup))]
    public class when_a_concern_group_is_asked_for_the_total_number_of_its_concerns : context_for_concern_group
    {
        private int total_number_of_concerns;

        protected override void establish_context()
        {
            concerns_in_group = new List<IConcern>
                {
                    dependency<IConcern>(),
                    dependency<IConcern>(),
                    dependency<IConcern>()
                };
            sut = create_sut();
        }

        protected override void because()
        {
            total_number_of_concerns = sut.total_number_of_concerns;
        }


        [Observation]
        public void should_sum_up_the_number_of_concerns_it_contains()
        {
            total_number_of_concerns.should_be_equal_to(3);
        }
    }

    [Concern(typeof (ConcernGroup))]
    public class when_a_concern_group_is_asked_for_its_total_number_of_observations : context_for_concern_group
    {
        private int total_number_of_observations;

        protected override void establish_context()
        {
            base.establish_context();
            concern_in_group.setup_result(x => x.total_number_of_observations).Return(1);

            var another_concern_in_group = dependency<IConcern>();
            another_concern_in_group.setup_result(x => x.total_number_of_observations).Return(3);

            concerns_in_group = new List<IConcern> {concern_in_group, another_concern_in_group, dependency<IConcern>()};

            sut = create_sut();
        }

        protected override void because()
        {
            total_number_of_observations = sut.total_number_of_observations;
        }


        [Observation]
        public void should_sum_up_number_of_observations_in_each_of_its_concerns()
        {
            total_number_of_observations.should_be_equal_to(4);
        }
    }
}