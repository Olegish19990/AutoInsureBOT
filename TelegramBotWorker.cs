using Telegram.Bot;
using Telegram.Bot.Types.Enums;
using Mindee;
using Mindee.Input;
using Mindee.Http;
using Mindee.Product.Generated;
using Telegram.Bot.Types;
using System.Text.Json;
using AutoInsureBot.Services;
using AutoInsureBot.Models;
using AutoInsureBot.Store;
using Microsoft.Extensions.Caching.Memory;
namespace AutoInsureBot
{
    public class TelegramBotWorker : BackgroundService
    {
        private readonly TelegramBotService _botService;
        private readonly ILogger<TelegramBotWorker> _logger;
        private readonly MindeeService _mindeeService;
        private readonly IUserSessionStore _userSessionStore;
        public TelegramBotWorker(
            TelegramBotService botService, 
            ILogger<TelegramBotWorker> logger,
            MindeeService mindeeService,
            IUserSessionStore userSessionStore)
        {
            _botService = botService;
            _logger = logger;
            _mindeeService = mindeeService;
            _userSessionStore = userSessionStore;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var bot = _botService.Client;

            var me = await bot.GetMe(stoppingToken);
            _logger.LogInformation($"Запущен бот @{me.Username}");

            bot.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                cancellationToken: stoppingToken
            );

            await Task.Delay(Timeout.Infinite, stoppingToken);
        }


    
        private async Task HandleUpdateAsync(ITelegramBotClient botClient, Telegram.Bot.Types.Update update, CancellationToken cancellationToken)
        {
            var userSession = _userSessionStore.GetUserSession(update.Message.From.Id);
            if(userSession==null)
            {
                UserSession newUserSession = new UserSession();
                _userSessionStore.setUserSession(update.Message.From.Id,newUserSession);
                _logger.LogInformation($"Create new User Session: {update.Message.From.Id}");
                userSession = newUserSession;   
            }
            switch (userSession.botState)
            {
                case BotState.None:
                    {
                        Console.WriteLine($"Botstate NONE for {update.Message.From.Id}: {userSession.botState}");
                        userSession.botState = BotState.AwaitingPassport;
                        break;
                    }
                case BotState.AwaitingPassport:
                    {
                        Console.WriteLine($"Botstate Awaiting passport for {update.Message.From.Id}: {userSession.botState}");
                        break;
                    }
                case BotState.AwaitingTechPassport:
                    {
                        break;
                    }
                case BotState.AwaitingDataConfirmation:
                    {
                        break;
                    }
                case BotState.AwaitingPriceAgreement:
                    {
                        break;
                    }
                case BotState.Completed:
                    {
                        break;
                    }
                    

            }
        }

        private Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "Ошибка в Telegram боте");
            return Task.CompletedTask;
        }
    }
}
