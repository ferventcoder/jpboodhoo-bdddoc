namespace bdddoc.domain
{
    using System.Reflection;

    public class AssemblyRepository : IAssemblyRepository
    {
        public Assembly find_using(string assembly_filename)
        {
            return Assembly.LoadFrom(assembly_filename);
        }
    }
}