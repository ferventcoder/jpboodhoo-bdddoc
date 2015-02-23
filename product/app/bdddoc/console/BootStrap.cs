using bdddoc.tasks;

namespace bdddoc.console
{
    public class BootStrap
    {
        public static void Main(string[] args)
        {
            new ReportTasks().run_report_using(args);
        }
    }
}