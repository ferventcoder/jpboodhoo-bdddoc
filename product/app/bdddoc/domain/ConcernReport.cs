namespace bdddoc.domain
{
    using System.Collections.Generic;
    using System.Linq;
    using utility;

    public interface IConcernReport : IGroupingOfConcerns
    {
        IEnumerable<IConcernGroup> groups { get; }
    }

    public class ConcernReport : IConcernReport
    {
        private readonly IEnumerable<IConcernGroup> concern_groups;

        public ConcernReport(IEnumerable<IConcernGroup> concern_groups)
        {
            this.concern_groups = concern_groups;
        }

        public int total_number_of_concerns
        {
            get { return concern_groups.Sum(x => x.total_number_of_concerns); }
        }

        public int total_number_of_observations
        {
            get { return concern_groups.Sum(x => x.total_number_of_observations); }
        }


        public IEnumerable<IConcernGroup> groups
        {
            get { return concern_groups.one_at_a_time(); }
        }
    }
}