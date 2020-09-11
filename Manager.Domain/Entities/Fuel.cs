namespace Manager.Domain.Entities
{
    public class Fuel: BaseEntity
    {
        public string Name { get; private set; }

        public Fuel(string name)
        {
            Name = name;
        }
    }
}
