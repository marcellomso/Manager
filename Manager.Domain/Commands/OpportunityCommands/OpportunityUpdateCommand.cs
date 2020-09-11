namespace Manager.Domain.Commands.OpportunityCommands
{
    public class OpportunityUpdateCommand
    {
        public int Id { get; set; }
        public int Vehicle { get; set; }
        public decimal Amount { get; set; }
    }
}
