namespace Manager.Domain.Commands.VendoresCommands
{
    public class VendorCommand
    {
        public string Name { get; set; }
        public int Role { get; set; }
        public decimal CustomCommission { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
    }
}
