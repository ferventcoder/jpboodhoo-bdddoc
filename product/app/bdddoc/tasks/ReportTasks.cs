namespace bdddoc.tasks
{
    using System;
    using System.IO;
    using System.Reflection;
    using domain;

    public class ReportTasks : IReportTasks
    {
        private readonly IReportOptionsFactory report_options_factory;
        private readonly TextWriter writer;
        private readonly IConcernReportFactory concern_report_factory;
        private IReportWriter report_writer;
        public static readonly string help_message = string.Format("usage: {0} [assembly_filename] [test_attribute_name] [report_output_filename] [markdown] [custom_header=\"value\"]", Assembly.GetExecutingAssembly().GetName().Name);

        public ReportTasks() : this(Console.Out, new ReportOptionsFactory(), new ConcernReportFactory(), new SimpleHtmlReportWriter())
        {
        }

        public ReportTasks(TextWriter writer, IReportOptionsFactory report_options_factory, IConcernReportFactory concern_report_factory, IReportWriter report_writer)
        {
            this.writer = writer;
            this.report_options_factory = report_options_factory;
            this.concern_report_factory = concern_report_factory;
            this.report_writer = report_writer;
        }

        public void run_report_using(string[] args)
        {
            var options = report_options_factory.create_from(args);
            if (options.is_valid)
            {
                if (options.report_type == BddReportType.Markdown)
                {
                    report_writer = new SimpleMarkDownReportWriter();
                }

                write_report_using(options);
                return;
            }
            display_help();
        }

        private void display_help()
        {
            writer.Write(help_message);
        }

        private void write_report_using(IReportOptions report_options)
        {
            report_writer.save(concern_report_factory.create_using(report_options), report_options);
        }
    }
}