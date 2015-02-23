namespace bdddoc.console
{
    using System;
    using tasks;

    public class BootStrap
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Received {0}", Environment.CommandLine);

            new ReportTasks().run_report_using(args);

#if DEBUG
            Console.WriteLine("Press Enter to Continue...");
            Console.ReadLine();
#endif
        }
    }
}