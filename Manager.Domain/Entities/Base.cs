using System;

namespace Manager.Domain.Entities
{
    public class Base
    {
        public Guid Id { get; private set; }

        protected Base() {
            Id = new Guid();
        }
    }
}
