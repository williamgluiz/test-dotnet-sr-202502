using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicantTracking.Domain.Models
{
    public abstract class Entity
    {
        public int Id { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime? LastUpdatedAt { get; protected set; }

        protected Entity() { }

        protected Entity(int id)
        {
            Id = id;
            CreatedAt = DateTime.Now;
        }

        public void SetUpdated()
        {
            LastUpdatedAt = DateTime.Now;
        }

        public override bool Equals(object obj)
            => obj is Entity other && Id == other.Id;

        public override int GetHashCode()
            => Id.GetHashCode();
    }
}
