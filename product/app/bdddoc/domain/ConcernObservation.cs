namespace bdddoc.domain
{
    public class ConcernObservation : IConcernObservation
    {
        public BDDStyleName name { get; private set; }

        public ConcernObservation(BDDStyleName name)
        {
            this.name = name;
        }
    }
}