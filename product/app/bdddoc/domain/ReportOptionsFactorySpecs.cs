using System;
using System.Collections.Generic;
using System.Reflection;
using bdddoc.core;
using bdddoc.spechelpers;
using Observation = MbUnit.Framework.TestAttribute;

namespace bdddoc.domain
{
    public abstract class context_for_report_options_factory : ContextSpecification<IReportOptionsFactory>
    {
        protected IAssemblyRepository assembly_resolver;
        protected string[] args;
        protected string assembly_name;

        protected override void establish_context()
        {
            assembly_resolver = dependency<IAssemblyRepository>();
            assembly_name = "MbUnit.Framework.dll";
            args = new List<string> { assembly_name, "TestAttribute", "output.html" }.ToArray();

            sut = create_sut();
        }

        protected override IReportOptionsFactory create_sut()
        {
            return new ReportOptionsFactory(assembly_resolver);
        }
    }

    [Concern(typeof (ReportOptionsFactory))]
    public class when_creating_report_options_from_an_array_of_valid_arguments : context_for_report_options_factory
    {
        private IReportOptions result;

        protected override void establish_context()
        {
            base.establish_context();

            assembly_resolver.setup_result(x => x.find_using(assembly_name)).Return(Assembly.GetExecutingAssembly());
        }

        protected override void because()
        {
            result = sut.create_from(args);
        }

        [Observation]
        public void should_return_valid_report_options()
        {
            result.assembly_to_scan.should_be_equal_to(Assembly.GetExecutingAssembly());
            result.observation_specification.should_not_be_null();
        }
    }

    [Concern(typeof (ReportOptionsFactory))]
    public class when_an_exception_is_thrown_while_trying_to_to_create_the_report_options : context_for_report_options_factory
    {
        private IReportOptions result;

        protected override void establish_context()
        {
            base.establish_context();
            assembly_resolver.setup_result(x => x.find_using(null)).IgnoreArguments().Throw(new Exception());
        }

        protected override void because()
        {
            result = sut.create_from(args);
        }

        [Observation]
        public void should_return_the_missing_options_null_object()
        {
            result.should_be_an_instance_of<MissingReportOptions>();
        }
    }
}