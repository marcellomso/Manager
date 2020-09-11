namespace Manager.Domain.Commands.VehicleCommands
{
    public class VehicleCommand
    {
        public string Name { get; set; }
        public string Year { get; set; }
        public string Model { get; set; }
        public int Fuel { get; set; }
        public decimal Amount { get; set; }
    }
}
