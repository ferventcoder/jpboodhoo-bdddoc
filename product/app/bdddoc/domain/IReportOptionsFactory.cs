namespace bdddoc.domain
{
    public interface IReportOptionsFactory
    {
        IReportOptions create_from(string[] args);
    }
}