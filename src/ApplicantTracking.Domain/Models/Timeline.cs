using System;
using ApplicantTracking.Domain.Enumerators;

namespace ApplicantTracking.Domain.Models
{
    public class Timeline : Entity
    {
        public TimelineTypes TimelineTypeId { get; private set; }
        public int IdAggregateRoot { get; private set; }
        public string OldData { get; private set; }
        public string NewData { get; private set; }

        protected Timeline() : base() { }

        public Timeline(int id, TimelineTypes timelineTypeId, int idAggregateRoot, string newData, string oldData) : base(id)
        {
            TimelineTypeId = timelineTypeId;
            IdAggregateRoot = idAggregateRoot;
            OldData = oldData;
            NewData = newData;
        }

        public void SetLastUpdatedAt(DateTime lastUpdatedAt)
        {
            LastUpdatedAt = lastUpdatedAt;
        }
    }
}
