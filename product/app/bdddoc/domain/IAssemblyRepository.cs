namespace bdddoc.domain
{
    using System.Reflection;

    public interface IAssemblyRepository
    {
        Assembly find_using(string assembly_filename);
    }
}