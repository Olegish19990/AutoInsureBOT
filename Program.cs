using AutoInsureBot;
using AutoInsureBot.Services;
using AutoInsureBot.Store;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;



Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, config) =>
    {
        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        config.AddJsonFile("secret.json", optional: true, reloadOnChange: true);

    })
    .ConfigureServices((hostContext, services) =>
    {
        
        services.AddHostedService<TelegramBotWorker>();
        services.AddHttpClient(); 
        services.AddSingleton<TelegramBotService>();
        services.AddSingleton<MindeeService>();
        services.AddSingleton<IUserSessionStore, UserSessionStore>();
        services.AddMemoryCache();
    


    })
    .Build()
    .Run();
