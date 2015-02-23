using System;
using System.Collections.Generic;
using System.Reflection;

namespace bdddoc.domain
{
    public interface IObservationSpecification
    {
        bool IsSatisfiedBy(MethodInfo method);
    }

    public class ObservationSpecification : IObservationSpecification
    {
        private readonly string observation_attribute_name;

        public ObservationSpecification(string observation_attribute_name)
        {
            this.observation_attribute_name = observation_attribute_name;
        }

        public bool IsSatisfiedBy(MethodInfo method)
        {
            return new List<object>(method.GetCustomAttributes(false))
                .Exists(x => is_an_observation_attribute(x.GetType()));
        }

        private bool is_an_observation_attribute(Type attribute_type)
        {
            return string.Compare(observation_attribute_name, attribute_type.Name, true) == 0;
        }
    }
}