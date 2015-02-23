namespace bdddoc.domain
{
    public interface IConcernObservation
    {
        BDDStyleName name { get; }
    }

    public class ConcernObservation : IConcernObservation
    {
        public BDDStyleName name { get; private set; }

        public ConcernObservation(BDDStyleName name)
        {
            this.name = name;
        }
    }
}