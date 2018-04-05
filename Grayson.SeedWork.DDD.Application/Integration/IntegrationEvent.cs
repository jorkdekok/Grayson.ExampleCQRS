using System;

namespace Grayson.SeedWork.DDD.Application.Integration
{
    public class IntegrationEvent
    {
        public IntegrationEvent()
        {
            EventId = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }

        public Guid EventId { get; }
        public DateTime CreationDate { get; }
    }
}
