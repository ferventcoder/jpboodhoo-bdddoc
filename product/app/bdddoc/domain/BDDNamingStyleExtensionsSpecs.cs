using bdddoc.core;
using bdddoc.spechelpers;
using Observation = MbUnit.Framework.TestAttribute;

namespace bdddoc.domain
{
    [Concern(typeof (BDDNamingStyleExtensions))]
    public class when_creating_a_bdd_style_name_from_a_string : StaticContextSpecification
    {
        private BDDStyleName result;

        protected override void establish_context()
        {
        }

        protected override void because()
        {
            result = "when_creating_a_bdd_name_from_a_string".as_bdd_style_name();
        }


        [Observation]
        public void should_create_a_bdd_style_name_from_the_original_string()
        {
            result.name.should_be_equal_ignoring_case("when creating a bdd name from a string");
        }
    }
}