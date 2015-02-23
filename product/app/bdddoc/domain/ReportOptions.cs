namespace bdddoc.domain
{
    using System.Reflection;

    public class ReportOptions : IReportOptions
    {
        public Assembly assembly_to_scan { get; set; }

        public IObservationSpecification observation_specification { get; set; }
        public string output_filename { get; set; }

        public bool is_valid
        {
            get { return true; }
        }

        public string custom_header { get; set; }

        public BddReportType report_type { get; set; }
    }
}