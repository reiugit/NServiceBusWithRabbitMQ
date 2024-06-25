namespace NServiceBusWithRabbitMQ.Events;

public record ExampleEvent(string Text) : IEvent;
