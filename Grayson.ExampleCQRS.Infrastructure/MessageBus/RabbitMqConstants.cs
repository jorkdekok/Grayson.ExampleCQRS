namespace Grayson.ExampleCQRS.Infrastructure.MessageBus
{
    public static class RabbitMqConstants
    {
        public const string RabbitMqUri = "rabbitmq://localhost/grayson/";
        public const string UserName = "guest";
        public const string Password = "guest";
        public const string CommandsQueue = "commands.queue";
        public const string EventsQueue = "events.service";
    }
}
