using Observation = MbUnit.Framework.TestAttribute;

namespace bdddoc.tasks
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Text;
    using core;
    using domain;
    using spechelpers;

    public abstract class context_for_report_tasks : ContextSpecification<IReportTasks>
    {
        protected IReportOptions report_options;
        protected IReportOptionsFactory report_options_factory;
        protected StringBuilder builder;
        protected IConcernReportFactory concern_report_factory;
        protected IReportWriter report_writer;
        protected IConcernReport report;
        protected TextWriter writer;

        protected override void establish_context()
        {
            report_options = new ReportOptions();
            report_options_factory = dependency<IReportOptionsFactory>();
            concern_report_factory = dependency<IConcernReportFactory>();
            report_writer = dependency<IReportWriter>();
            builder = new StringBuilder();
            writer = new StringWriter(builder);
            report = dependency<IConcernReport>();
        }

        protected override IReportTasks create_sut()
        {
            return new ReportTasks(writer, report_options_factory, concern_report_factory, report_writer);
        }
    }

    [Concern(typeof (ReportTasks))]
    public class when_told_to_run_the_report_and_the_options_created_from_the_arguments_are_not_valid : context_for_report_tasks
    {
        protected override void establish_context()
        {
            base.establish_context();
            report_options = dependency<IReportOptions>();
            report_options.setup_result(x => x.is_valid).Return(false);
            report_options_factory.setup_result(x => x.create_from(null)).IgnoreArguments().Return(report_options);

            sut = create_sut();
        }

        protected override void because()
        {
            sut.run_report_using(new string[0]);
        }


        [Observation]
        public void should_write_out_the_help_message_to_the_text_writer()
        {
            builder.ToString().should_be_equal_ignoring_case(ReportTasks.help_message);
        }
    }

    [Concern(typeof (ReportTasks))]
    public class when_told_to_run_the_report_and_the_options_created_from_the_arguments_are_valid : context_for_report_tasks
    {
        protected override void establish_context()
        {
            base.establish_context();

            report_options.output_filename = "blah";
            report_options_factory.setup_result(x => x.create_from(null)).IgnoreArguments().Return(report_options);
            concern_report_factory.setup_result(x => x.create_using(report_options)).Return(report);

            sut = create_sut();
        }

        protected override void because()
        {
            sut.run_report_using(new string[0]);
        }


        [Observation]
        public void should_tell_the_report_writer_to_save_the_report()
        {
            report_writer.was_told_to(x => x.save(report, report_options));
        }
    }

    [Concern(typeof (ReportTasks))]
    public class when_generating_a_real_report : context_for_report_tasks
    {
        protected override void establish_context()
        {
            base.establish_context();
            report_options_factory = dependency<IReportOptionsFactory>();
            report_options_factory.setup_result(x => x.create_from(null)).IgnoreArguments().Return(create_report_options());
            concern_report_factory = new ConcernReportFactory();
            report_writer = new SimpleHtmlReportWriter();
            writer = Console.Out;
            File.Delete(report_output_path);

            sut = create_sut();
        }

        protected override void after_each_specification()
        {
            base.after_each_specification();
            File.Delete(report_output_path);
        }

        private string report_output_path
        {
            get { return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "output.html"); }
        }

        private IReportOptions create_report_options()
        {
            return new ReportOptions
                {
                    assembly_to_scan = Assembly.GetExecutingAssembly(),
                    observation_specification = new ObservationSpecification("TestAttribute"),
                    output_filename = report_output_path
                };
        }

        protected override void because()
        {
            sut.run_report_using(null);
        }


        [Observation]
        public void should_generate_the_report_file()
        {
            File.Exists(report_output_path).should_be_true();
        }
    }
}