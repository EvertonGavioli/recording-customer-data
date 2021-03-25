using System;

namespace RCD.Core.DomainObjects
{
    public abstract class Entity
    {
        protected Entity()
        {
            Id = new Guid();
        }

        public Guid Id { get; set; }
    }
}
