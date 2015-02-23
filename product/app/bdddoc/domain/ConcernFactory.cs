namespace bdddoc.domain
{
    using System;

    public interface IConcernFactory
    {
        IConcern create_concern_from(Type type_with_concern, IObservationSpecification observation_specification);
    }

    public class ConcernFactory : IConcernFactory
    {
        public IConcern create_concern_from(Type type_with_concern, IObservationSpecification obseration_specification)
        {
            return new Concern(type_with_concern.concern(),
                               type_with_concern.Name.as_bdd_style_name(),
                               type_with_concern.all_methods_that_meet(obseration_specification).as_observations());
        }
    }
}