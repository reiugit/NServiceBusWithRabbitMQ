using NServiceBusWithRabbitMQ.Events;

namespace NServiceBusWithRabbitMQ.Handlers;

public class ExampleEventHandler : IHandleMessages<ExampleEvent>
{
    public async Task Handle(ExampleEvent message, IMessageHandlerContext context)
    {
        await Task.Delay(100, context.CancellationToken);

        Console.WriteLine($"\nReceived event with text: {message.Text}");
    }
}
