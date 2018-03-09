using System;
using System.Collections.Generic;
using System.Text;
using Grayson.ExampleCQRS.Domain.Model;
using Grayson.Utils.DDD;

namespace Grayson.ExampleCQRS.ReadModel.Application.Services
{
    public class EventsProcessorService : ApplicationService, IDomainEventSubscriber<KmStandCreated>
    {
        public void On(KmStandCreated @event)
        {
        }
    }
}
