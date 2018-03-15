using System;

using Grayson.Utils.DDD.Domain;

namespace Grayson.ExampleCQRS.Domain.Model
{
    public partial class KmStand : EventSourcedAggregate, IApplyEvent<KmStandCreated>
    {
        public KmStand(IEventPublisher eventPublisher) : base(eventPublisher)
        {
        }

        public void Create(int stand, DateTime datum, Guid adresId)
        {
            this.Causes(new KmStandCreated(Guid.NewGuid(), stand, datum, adresId));
        }

        public void Update(Guid id, int stand, DateTime datum, Guid adresId)
        {
            this.Causes(new KmStandUpdated(id, stand, datum, adresId));
        }
    }
}