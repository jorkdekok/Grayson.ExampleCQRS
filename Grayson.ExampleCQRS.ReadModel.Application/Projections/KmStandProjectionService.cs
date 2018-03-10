using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Grayson.ExampleCQRS.Domain.Model;
using Grayson.Utils.DDD;

namespace Grayson.ExampleCQRS.ReadModel.Application.Projections
{
    public class KmStandProjectionService : ApplicationService, IDomainEventSubscriber<KmStandCreated>
    {
        public void When(KmStandCreated @event)
        {
            throw new NotImplementedException();
        }
    }
}
