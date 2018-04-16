namespace Grayson.ExampleCQRS.Infrastructure.MessageBus
{
    public static class RabbitMqConstants
    {
        public const string CommandsQueue = "commands.queue";
        public const string EventsQueue = "events.service";
        public const string Password = "guest";
        public const string RabbitMqUri = "rabbitmq://10.0.75.1:5672/grayson/";
        public const string UserName = "guest";

        public static string GetCommandsQueue(string boundedContextName)
        {
            return $"{CommandsQueue}.{boundedContextName}";
        }

        public static string GetEventsQueue(string boundedContextName)
        {
            return $"{EventsQueue}.{boundedContextName}";
        }
    }
}