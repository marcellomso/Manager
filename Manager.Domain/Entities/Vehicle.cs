namespace Manager.Domain.Entities
{
    public class Vehicle: BaseEntity
    {
        public string Name { get; private set; }
        public string Year { get; private set; }
        public string Model { get; private set; }
        public int FuelId { get; private set; }
        public Fuel Fuel { get; private set; }
        public double Amount { get; private set; }
        public string Status { get; private set; }

        protected Vehicle() {}

        public Vehicle(string name, string year, string model, Fuel fuel, double amount, string status)
        {
            SetProperties(name, year, model, fuel, amount, status);
        }

        public void Update(string name, string year, string model, Fuel fuel, double amount, string status)
        {
            SetProperties(name, year, model, fuel, amount, status);
        }

        private void SetProperties(string name, string year, string model, Fuel fuel, double amount, string status)
        {
            Name = name;
            Year = year;
            Model = model;
            Fuel = fuel;
            Amount = amount;
            Status = status;
        }
    }
}
