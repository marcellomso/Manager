namespace Manager.Domain.Entities
{
    public class OpportunityStatus: BaseEntity
    {
        public string Description { get; private set; }

        protected OpportunityStatus() {}

        public OpportunityStatus(string description)
        {
            Description = description;
        }
    }
}
