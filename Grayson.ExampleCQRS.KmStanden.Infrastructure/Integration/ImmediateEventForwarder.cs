using Grayson.ExampleCQRS.KmStanden.Domain.AggregatesModel.KmStandAggregate;
using IntegrationEvents = Grayson.ExampleCQRS.KmStanden.Application.IntegrationEvents;
using Grayson.SeedWork.DDD.Application.Integration;
using Grayson.SeedWork.DDD.Domain;

using Microsoft.Extensions.Logging;

using System.Threading.Tasks;

namespace Grayson.ExampleCQRS.KmStanden.Infrastructure.Integration
{
    public class ImmediateEventForwarder : ICommittedEventHandler<IDomainEvent>
    {
        private readonly IIntegrationEventBus _integrationEventBus;
        private readonly ILogger _logger;

        public ImmediateEventForwarder(
            ILogger logger,
            IIntegrationEventBus integrationEventBus)
        {
            _logger = logger;
            _integrationEventBus = integrationEventBus;
        }

        public async Task When(IDomainEvent @event)
        {
            _logger.LogInformation($"Sending event: {@event.GetType()}");

            // translate to integration event?
            IntegrationEvent integrationEvent = null;
            switch (@event)
            {
                case KmStandCreated kmStandCreated:
                    integrationEvent = new IntegrationEvents.KmStandCreated(kmStandCreated.Id, kmStandCreated.Stand, kmStandCreated.Datum, kmStandCreated.AdresId);
                    break;

                default:
                    break;
            }

            if (integrationEvent != null)
            {
                _integrationEventBus.Publish(integrationEvent);
            }
        }
    }
}