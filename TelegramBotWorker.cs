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
using AutoInsureBot.Handlers;
using AutoInsureBot.Handlers.BotStateHandlerFactory;
namespace AutoInsureBot
{
    public class TelegramBotWorker : BackgroundService
    {
        private readonly TelegramBotService _botService;
        private readonly ILogger<TelegramBotWorker> _logger;
        private readonly MindeeService _mindeeService;
        private readonly IUserSessionStore _userSessionStore;
        private readonly IBotStateHandlerFactory _botStateHandlerFactory;



        public TelegramBotWorker(
            TelegramBotService botService, 
            ILogger<TelegramBotWorker> logger,
            MindeeService mindeeService,
            IUserSessionStore userSessionStore,
            IBotStateHandlerFactory botStateHandlerFactory
            )
        {
            _botService = botService;
            _logger = logger;
            _mindeeService = mindeeService;
            _userSessionStore = userSessionStore;
            _botStateHandlerFactory = botStateHandlerFactory;
        
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var bot = _botService.Client;

            var me = await bot.GetMe(stoppingToken);
            _logger.LogInformation($"Bot started @{me.Username}");

            bot.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                cancellationToken: stoppingToken
            );

            await Task.Delay(Timeout.Infinite, stoppingToken);
        }



        private async Task HandleUpdateAsync(ITelegramBotClient botClient, Telegram.Bot.Types.Update update, CancellationToken cancellationToken)
        {
            var userId = update.CallbackQuery?.From?.Id
                        ?? update.Message?.From?.Id
                        ?? throw new InvalidOperationException("Cannot determine user ID");

            UserSession userSession = _userSessionStore.GetUserSession(userId);
            if (userSession==null)
            {
                UserSession newUserSession = new UserSession();
                _userSessionStore.setUserSession(userId,newUserSession);
                _logger.LogInformation($"Create new User Session: {userId}");
                userSession = newUserSession;   
            }

            var handler = _botStateHandlerFactory.GetHandler(userSession.botState);
            await handler.HandleAsync(update, userSession, botClient);
        }

        private Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "Error");
            return Task.CompletedTask;
        }
    }
}
