namespace bdddoc.domain
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using core;

    public static class BDDReflectionExtensions
    {
        public static IEnumerable<Type> all_types_with_a_concern_attribute(this Assembly assembly)
        {
            return assembly.GetTypes().Where(is_a_concern);
        }

        public static bool is_a_concern(this Type type)
        {
            var concern_attributes = type.GetCustomAttributes(typeof(ConcernAttribute), false);
            return concern_attributes != null && concern_attributes.Length == 1;
        }

        public static Type concern(this Type type)
        {
            var concern_attributes = type.GetCustomAttributes(typeof(ConcernAttribute), false);
            return ((ConcernAttribute)concern_attributes[0]).concerned_with;
        }

        public static IConcernObservation as_observation(this MethodInfo method)
        {
            return new ConcernObservation(method.Name.as_bdd_style_name(), is_pending_or_ignored(method));
        }

        public static IEnumerable<MethodInfo> all_methods_that_meet(this Type type, IObservationSpecification observation_specification)
        {
            return type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public)
                       .Where(observation_specification.IsSatisfiedBy);
        }

        private static bool is_pending_or_ignored(MethodInfo method)
        {
            return new List<object>(method.GetCustomAttributes(false))
                .Exists(x =>
                    has_attribute_string("PendingAttribute", x.GetType()) ||
                    has_attribute_string("IgnoredAttribute", x.GetType())
                    );
        }

        private static bool has_attribute_string(string compare, Type type)
        {
            return string.Compare(compare, type.Name, ignoreCase: true, culture: CultureInfo.InvariantCulture) == 0;
        }

        public static IEnumerable<IConcernObservation> as_observations(this IEnumerable<MethodInfo> methods)
        {
            return methods.Select(x => x.as_observation());
        }
    }
}