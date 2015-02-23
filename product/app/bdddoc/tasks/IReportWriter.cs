namespace bdddoc.tasks
{
    using domain;

    public interface IReportWriter
    {
        void save(IConcernReport report, IReportOptions options);
    }
}