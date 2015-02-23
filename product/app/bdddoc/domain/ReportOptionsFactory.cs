namespace bdddoc.domain
{
    using System;

    public class ReportOptionsFactory : IReportOptionsFactory
    {
        private readonly IAssemblyRepository assembly_resolver;

        public ReportOptionsFactory() : this(new AssemblyRepository())
        {
        }

        public ReportOptionsFactory(IAssemblyRepository assembly_resolver)
        {
            this.assembly_resolver = assembly_resolver;
        }

        public IReportOptions create_from(string[] args)
        {
            if (args.Length == 0) return new MissingReportOptions();

            try
            {
                Console.WriteLine("{0}", string.Join(" ", args));

                var options = new ReportOptions
                    {
                        assembly_to_scan = assembly_resolver.find_using(args[0]),
                        observation_specification = new ObservationSpecification(args[1]),
                        output_filename = args[2],
                    };

                foreach (var arg in args)
                {
                    if (arg.Contains("markdown"))
                    {
                        options.report_type = BddReportType.Markdown;
                    }
                }   
                
                foreach (var arg in args)
                {
                    if (arg.Contains("custom_header"))
                    {
                        options.custom_header = arg.Replace("custom_header", string.Empty).Trim();
                        if (options.custom_header.StartsWith("=")) options.custom_header = options.custom_header.Remove(0, 1);
                    }
                }

                return options;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred:{0} {1}", Environment.NewLine, ex);
                return new MissingReportOptions();
            }
        }
    }
}