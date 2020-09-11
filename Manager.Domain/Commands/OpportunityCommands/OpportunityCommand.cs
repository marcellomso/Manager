using System;

namespace Manager.Domain.Commands.OpportunityCommands
{
    public class OpportunityCommand
    {
        public int Veiche { get; set; }
        public int Vendor { get; set; }
        public double Amount { get; set; }
    }
}
