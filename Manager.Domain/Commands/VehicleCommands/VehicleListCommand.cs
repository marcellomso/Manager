namespace Manager.Domain.Commands.VehicleCommands
{
    public class VehicleListCommand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Year { get; set; }
        public string Model { get; set; }
        public string Fuel { get; set; }
        public double Amount { get; set; }
        public bool Sold { get; set; }
    }
}
