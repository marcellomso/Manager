using Manager.Domain.Enuns;

namespace Manager.Domain.Entities
{
    public class Vehicle: Base
    {
        public string Name { get; private set; }
        public string Year { get; private set; }
        public string Model { get; private set; }
        public EFuelType Fuel { get; private set; }
        public double Amount { get; private set; }
        public string Status { get; private set; }

        protected Vehicle() {}

        public Vehicle(string name, string year, string model, EFuelType fuel, double amount, string status)
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
