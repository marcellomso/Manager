namespace Manager.Domain.Commands.VendoresCommands
{
    public class VendorListCommand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public double CustomCommission { get; set; }
    }
}
