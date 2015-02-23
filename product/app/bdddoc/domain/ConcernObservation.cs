namespace bdddoc.domain
{
    public class ConcernObservation : IConcernObservation
    {
        public BDDStyleName name { get; private set; }

        public ConcernObservation(BDDStyleName name, bool pending)
        {
            this.name = name;
            if (pending)
            {
                this.name = new BDDStyleName("[PENDING] " + name.name);
            }
        }
    }
}