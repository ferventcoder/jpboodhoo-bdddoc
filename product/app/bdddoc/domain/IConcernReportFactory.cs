namespace bdddoc.domain
{
    public interface IConcernReportFactory
    {
        IConcernReport create_using(IReportOptions options);
    }
}