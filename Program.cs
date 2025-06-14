using AutoInsureBot;
using AutoInsureBot.Handlers;
using AutoInsureBot.Handlers.BotStateHandlerFactory;
using AutoInsureBot.Services;
using AutoInsureBot.Store;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;



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
        services.AddTransient<NoneStateHandler>();
        services.AddTransient<AwaitingPassportStateHandler>();
        services.AddTransient<AwaitingTechPassportStateHandler>();
        services.AddTransient<AwaitingDataConfirmationStateHandler>();
        services.AddTransient<AwaitingPriceAgreementStateHandler>();
      
        services.AddSingleton<IBotStateHandlerFactory, BotStateHandlerFactory>();
    

        services.AddMemoryCache();
    


    })
    .Build()
    .Run();
