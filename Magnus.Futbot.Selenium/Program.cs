using Magnus.Futbot.Selenium.Consumers;
using Magnus.Futbot.Selenium.Procucers;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services
            .AddHostedService<InitProfileConsumer>()
            .AddHostedService<SecurityCodeConsumer>();

        services
            .AddSingleton<ProfilesProducer>()
            .AddSingleton<SubmitCodeProducer>();
    })
    .Build();

await host.RunAsync();
