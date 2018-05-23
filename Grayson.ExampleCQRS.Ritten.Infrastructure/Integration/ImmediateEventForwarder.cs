using IntegrationEvents = Grayson.ExampleCQRS.Ritten.Application.IntegrationEvents;
using Grayson.SeedWork.DDD.Application.Integration;
using Grayson.SeedWork.DDD.Domain;

using Microsoft.Extensions.Logging;

using System.Threading.Tasks;
using Grayson.ExampleCQRS.Ritten.Domain.AggregatesModel.RitAggregate;

namespace Grayson.ExampleCQRS.Ritten.Infrastructure.Integration
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

            // translate to integration event
            IntegrationEvent integrationEvent = null;
            switch (@event)
            {
                case RitCreated ritCreated:
                    integrationEvent = new IntegrationEvents.RitCreated(ritCreated.Name, ritCreated.BeginStand, ritCreated.BeginStandId, ritCreated.EindStand, ritCreated.EindStandId, ritCreated.Id);
                    break;
                case RitUpdated ritUpdated:
                    integrationEvent = new IntegrationEvents.RitUpdated(ritUpdated.BeginStand, ritUpdated.BeginStandId, ritUpdated.EindStand, ritUpdated.EindStandId, ritUpdated.Id, ritUpdated.Name);
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