using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NServiceBusWithRabbitMQ.Commands;
using NServiceBusWithRabbitMQ.Events;

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

await messageSession.Publish(new ExampleEvent("Example Event"));
await messageSession.Send(new ExampleCommand("Example Command"));

app.Run();





