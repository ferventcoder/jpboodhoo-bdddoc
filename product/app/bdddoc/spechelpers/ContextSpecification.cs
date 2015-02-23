namespace bdddoc.spechelpers
{
    using MbUnit.Framework;
    using Rhino.Mocks;

    public abstract class StaticContextSpecification
    {
        [SetUp]
        public void setup()
        {
            establish_context();
            because();
        }

        [TearDown]
        public void tear_down()
        {
            after_each_specification();
        }

        protected abstract void because();
        protected abstract void establish_context();

        protected virtual void after_each_specification()
        {
        }

        protected InterfaceType dependency<InterfaceType>()
        {
            return MockRepository.GenerateMock<InterfaceType>();
        }
    }

    [TestFixture]
    public abstract class ContextSpecification<SystemUnderTest> : StaticContextSpecification
    {
        protected SystemUnderTest sut;
        protected abstract SystemUnderTest create_sut();
    }
}