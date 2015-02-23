namespace bdddoc.tasks
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using domain;
    using utility;

    public class SimpleMarkDownReportWriter : IReportWriter
    {
        public void save(IConcernReport report, IReportOptions options)
        {
            File.WriteAllText(options.output_filename, build_report_output_using(report, options));
        }

        private string build_report_output_using(IConcernReport report, IReportOptions options)
        {
            var builder = new StringBuilder();

            builder.Append(build_report_header_block_using(report, options));
            report.groups.OrderBy(x => x.concerned_with.Name).each(rg => builder.Append(build_behaviour_block_using(rg)));

            return builder.ToString();
        }

        private string build_report_header_block_using(IConcernReport report, IReportOptions options)
        {
            if (!string.IsNullOrWhiteSpace(options.custom_header))
            {
                return string.Format("## {0}{1}", options.custom_header, Environment.NewLine);
            }

            return string.Format("## Scenarios: {0} - Facts: {1}{2}", report.total_number_of_concerns, report.total_number_of_observations, Environment.NewLine);
        }

        private string build_behaviour_block_using(IConcernGroup concern_group)
        {
            var builder = new StringBuilder();
            builder.AppendFormat("### {0} [ {1} Scenarios(s) , {2} Observations(s) ]{3}", concern_group.concerned_with.Name, concern_group.total_number_of_concerns,
                                 concern_group.total_number_of_observations, Environment.NewLine);
            builder.Append(Environment.NewLine);
            concern_group.concerns.OrderBy(x => x.name.name).each(cg => builder.Append(build_concern_block_using(cg)));
            return builder.ToString();
        }

        private string build_concern_block_using(IConcern concern)
        {
            var builder = new StringBuilder();
            builder.AppendFormat("#### {0}{1}", concern.name, Environment.NewLine);
            builder.Append(Environment.NewLine);
            concern.observations.OrderBy(x => x.name.name).each(x => builder.AppendFormat(" * {0}{1}", x.name, Environment.NewLine));

            return builder.ToString();
        }
    }
}