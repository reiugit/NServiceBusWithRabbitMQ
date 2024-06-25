using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NServiceBusWithRabbitMQ.Commands;
using NServiceBusWithRabbitMQ.Events;
using RabbitMQ.Client.Exceptions;

var builder = Host.CreateApplicationBuilder();

var endpointConfiguration = new EndpointConfiguration("NServiceBusWithRabbitMQ");

endpointConfiguration.UseSerialization<SystemJsonSerializer>();
endpointConfiguration.EnableInstallers();
endpointConfiguration.UseTransport<RabbitMQTransport>()
    .UseConventionalRoutingTopology(QueueType.Quorum)
    .ConnectionString("amqp:guest:guest@localhost:5672") // or "host=localhost"
    .Routing()
    .RouteToEndpoint(typeof(ExampleCommand) ,"NServiceBusWithRabbitMQ");

builder.UseNServiceBus(endpointConfiguration);

var app = builder.Build();

var messageSession = app.Services.GetRequiredService<IMessageSession>();

try
{
    await messageSession.Publish(new ExampleEvent("Example Event"));
    await messageSession.Send(new ExampleCommand("Example Command"));
}
catch (Exception ex)
{
    if (ex is BrokerUnreachableException)
    {
        Console.WriteLine("\nLocal RabbitMQ Instance not found.\nPlease start RabbitMQ Container and then restart the application.");
    }
    else
    {
        Console.WriteLine(ex.Message);
    }

    return;
}

app.Run();


