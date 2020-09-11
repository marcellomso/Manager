namespace Manager.Domain.Entities
{
    public abstract class BaseEntity
    {
        public int Id { get; private set; }
        public bool Deleted { get; private set; }

        public virtual void Delete()
        {
            Deleted = true;
        }
    }
}
