namespace bdddoc.tasks
{
    using System.IO;
    using System.Linq;
    using System.Text;
    using domain;
    using utility;

    public class SimpleHtmlReportWriter : IReportWriter
    {
        public void save(IConcernReport report, IReportOptions options)
        {
            File.WriteAllText(options.output_filename, build_report_output_using(report, options));
        }

        private string build_report_output_using(IConcernReport report, IReportOptions options)
        {
            var builder = new StringBuilder();

            builder.Append(build_report_header_block_using(report, options));
            builder.AppendFormat("<ul>");
            report.groups.OrderBy(x => x.concerned_with.Name).each(rg => builder.Append(build_behaviour_block_using(rg)));
            builder.AppendFormat("</ul>");

            return builder.ToString();
        }

        private string build_report_header_block_using(IConcernReport report, IReportOptions options)
        {
            if (!string.IsNullOrWhiteSpace(options.custom_header))
            {
                return string.Format("<h1>{0}</h1>", options.custom_header);
            }

            return string.Format("<h1>Concerns: {0} - Observations: {1}</h1>", report.total_number_of_concerns, report.total_number_of_observations);
        }

        private string build_behaviour_block_using(IConcernGroup concern_group)
        {
            var builder = new StringBuilder();
            builder.AppendFormat("<h1><li>Behaviour of: {0} [ {1} Concern(s) , {2} Observation(s) ]</li></h1>", concern_group.concerned_with.Name, concern_group.total_number_of_concerns,
                                 concern_group.total_number_of_observations);
            builder.AppendFormat("<ul>");
            concern_group.concerns.OrderBy(x => x.name.name).each(cg => builder.Append(build_concern_block_using(cg)));
            builder.Append("</ul>");
            return builder.ToString();
        }

        private string build_concern_block_using(IConcern concern)
        {
            var builder = new StringBuilder();
            builder.AppendFormat("<h2><li>{0}</li></h2>", concern.name);
            builder.Append("<ul>");
            concern.observations.OrderBy(x => x.name.name).each(x => builder.AppendFormat("<li>{0}</li>", x.name));
            builder.Append("</ul>");

            return builder.ToString();
        }
    }
}