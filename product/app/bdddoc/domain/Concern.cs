namespace bdddoc.domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using utility;

    public interface IConcern : ITypeForAConcern
    {
        BDDStyleName name { get; }
        IEnumerable<IConcernObservation> observations { get; }
        int total_number_of_observations { get; }
    }

    public class Concern : IConcern
    {
        private readonly IEnumerable<IConcernObservation> all_observations;
        public virtual Type concerned_with { get; private set; }
        public BDDStyleName name { get; private set; }

        public Concern(Type target_concern, BDDStyleName concern_name, IEnumerable<IConcernObservation> observations)
        {
            all_observations = observations;
            concerned_with = target_concern;
            name = concern_name;
        }

        public IEnumerable<IConcernObservation> observations
        {
            get { return all_observations.one_at_a_time(); }
        }

        public int total_number_of_observations
        {
            get { return all_observations.Count(); }
        }
    }
}