﻿namespace Manager.Domain.Commands.OpportunityCommands
{
    public class OpportunityListCommand
    {
        public int Id { get; set; }
        public string Veiche { get; set; }
        public string Vendor { get; set; }
        public double Amount { get; set; }
        public string Status { get; set; }
    }
}
