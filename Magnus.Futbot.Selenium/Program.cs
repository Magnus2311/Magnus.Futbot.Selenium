using Magnus.Futbot.Selenium.Consumers;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<InitProfileConsumer>();
        services.AddHostedService<SecurityCodeConsumer>();
    })
    .Build();

await host.RunAsync();
