using MassTransit;
using System;
using System.Threading.Tasks;

namespace Grayson.ExampleCQRS.Infrastructure.MessageBus
{
    public class MassTransitConsumer<TRequest> : IConsumer<TRequest>
        where TRequest : class
    {

        public async Task Consume(ConsumeContext<TRequest> context)
        {
            //await context.RespondAsync<TResponse>(_implementation.Invoke(context.Message));
            await Console.Out.WriteLineAsync($"received message customer: {context.Message.GetType()}");
        }
    }
}