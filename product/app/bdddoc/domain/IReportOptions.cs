namespace bdddoc.domain
{
    using System.Reflection;

    public interface IReportOptions
    {
        Assembly assembly_to_scan { get; set; }
        IObservationSpecification observation_specification { get; set; }
        string output_filename { get; set; }
        bool is_valid { get; }
        BddReportType report_type { get; }
        string custom_header { get; set; }
    }
}