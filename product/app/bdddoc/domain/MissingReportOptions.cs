namespace bdddoc.domain
{
    using System;
    using System.Reflection;

    public class MissingReportOptions : IReportOptions
    {
        public bool is_valid
        {
            get { return false; }
        }

        public BddReportType report_type { get; private set; }
        public string custom_header { get; set; }

        public Assembly assembly_to_scan
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public IObservationSpecification observation_specification
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public string output_filename
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
    }
}