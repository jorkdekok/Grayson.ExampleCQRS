using Grayson.Utils.DDD;
using System;
using System.Diagnostics;

namespace Grayson.ExampleCQRS.Domain.Model
{
    public class Rit : EventSourcedAggregate, IApplyEvent<RitCreated>, IApplyEvent<RitUpdated>
    {
        public string Name { get; private set; }

        public Rit(IServiceBus bus) : base(bus)
        {
        }

        public void Apply(RitCreated @event)
        {
            this.Id = @event.Id;
            this.Name = @event.Name;
            Debug.WriteLine("Event apply");
        }

        public void Apply(RitUpdated @event)
        {
            this.Name = @event.Name;
        }

        public void Create(Guid id, string name)
        {
            this.Causes(new RitCreated(id, name));
        }

        public void Update(Guid id, string name)
        {
            this.Causes(new RitUpdated(id, name));
        }
    }
}