namespace NServiceBusWithRabbitMQ.Commands;

public record ExampleCommand(string Text) : ICommand;
