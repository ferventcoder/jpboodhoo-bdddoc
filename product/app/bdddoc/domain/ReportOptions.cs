using System;

namespace bdddoc.domain
{
    public interface IReportOptions
    {
        Assembly assembly_to_scan { get; set; }
        IObservationSpecification observation_specification { get; set; }
        string output_filename { get; set; }
        bool is_valid { get; }
    }

    public class MissingReportOptions : IReportOptions
    {
        public bool is_valid
        {
            get { return false; }
        }

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
    }
}