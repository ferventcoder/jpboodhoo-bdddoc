using bdddoc.core;
using bdddoc.spechelpers;
using Observation = MbUnit.Framework.TestAttribute;

namespace bdddoc.domain
{
    public abstract class context_for_bdd_style_name : ContextSpecification<BDDStyleName>
    {
        protected string name_for_bdd_name = "name_formatted_with_jp_s_bdd_conventions";

        protected override void establish_context()
        {
            sut = create_sut();
        }

        protected override BDDStyleName create_sut()
        {
            return new BDDStyleName(name_for_bdd_name);
        }
    }

    [Concern(typeof (BDDStyleName))]
    public class when_a_bdd_style_name_is_asked_for_its_name : context_for_bdd_style_name
    {
        private string name;

        protected override void because()
        {
            name = sut.name;
        }


        [Observation]
        public void should_return_its_name_using_bdd_style_naming_conventions()
        {
            name.should_be_equal_ignoring_case("name formatted with jp's bdd conventions");
        }
    }

    [Concern(typeof (BDDStyleName))]
    public class when_a_bdd_style_name_is_implicitly_converted_to_a_string : context_for_bdd_style_name
    {
        private string name;


        protected override void because()
        {
            name = sut;
        }


        [Observation]
        public void should_return_its_name_using_bdd_style_naming_conventions()
        {
            name.should_be_equal_ignoring_case("name formatted with jp's bdd conventions");
        }
    }

    [Concern(typeof (BDDStyleName))]
    public class when_a_bdd_style_name_is_converted_to_a_string : context_for_bdd_style_name
    {
        private string name;

        protected override void establish_context()
        {
            sut = new BDDStyleName("name_formatted_with_jp_s_bdd_conventions");
        }

        protected override void because()
        {
            name = sut.ToString();
        }


        [Observation]
        public void should_return_its_name_using_bdd_style_naming_conventions()
        {
            name.should_be_equal_ignoring_case("name formatted with jp's bdd conventions");
        }
    }

    [Concern(typeof (BDDStyleName))]
    public class when_a_bdd_style_name_determines_equality : context_for_bdd_style_name
    {
        private BDDStyleName other_instance_of_same_name;

        protected override void establish_context()
        {
            base.establish_context();
            other_instance_of_same_name = create_sut();
        }

        protected override void because()
        {
        }


        [Observation]
        public void should_be_equal_if_the_undelrying_name_is_exactly_the_same()
        {
            sut.Equals(other_instance_of_same_name).should_be_true();
            sut.Equals("Name".as_bdd_style_name()).should_be_false();
            sut.Equals("name 2".as_bdd_style_name()).should_be_false();
        }
    }
}