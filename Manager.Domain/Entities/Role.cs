namespace Manager.Domain.Entities
{
    public class Role: Base
    {
        public string Name { get; private set; }
        public double Commission { get; private set; }

        protected Role() {}

        public Role(string name, double commission)
        {
            Name = name;
            Commission = commission;
        }
    }
}
