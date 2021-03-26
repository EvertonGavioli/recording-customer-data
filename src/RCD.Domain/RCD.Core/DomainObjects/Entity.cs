using FluentValidation.Results;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RCD.Core.DomainObjects
{
    public abstract class Entity
    {
        protected Entity()
        {
            Id = new Guid();
        }

        public Guid Id { get; protected set; }

        [NotMapped]
        public ValidationResult ValidationResult { get; protected set; }

        public virtual bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
