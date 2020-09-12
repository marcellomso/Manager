namespace Manager.Domain.Commands.VendoresCommands
{
    public class VendorUpdateCommand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Role { get; set; }
        public decimal CustomCommission { get; set; }
    }
}
