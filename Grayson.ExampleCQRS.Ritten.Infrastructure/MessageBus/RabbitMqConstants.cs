﻿namespace Grayson.ExampleCQRS.Infrastructure.MessageBus
{
    public static class RabbitMqConstants
    {
        public const string CommandsQueue = "commands.queue";
        public const string EventsQueue = "events.service";
        public const string Password = "guest";
        public const string RabbitMqUri = "rabbitmq://localhost/grayson/";
        public const string UserName = "guest";
    }
}