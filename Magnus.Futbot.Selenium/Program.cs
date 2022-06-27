using Magnus.Futbot.Selenium;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<ProfilesConsumer>();
    })
    .Build();

await host.RunAsync();
