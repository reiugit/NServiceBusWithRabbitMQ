using NServiceBusWithRabbitMQ.Commands;

namespace NServiceBusWithRabbitMQ.Handlers;

public class ExampleCommandHandler : IHandleMessages<ExampleCommand>
{
    public async Task Handle(ExampleCommand message, IMessageHandlerContext context)
    {
        await Task.Delay(100, context.CancellationToken);

        Console.WriteLine($"Received command with text: {message.Text}");
    }
}
