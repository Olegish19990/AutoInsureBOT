using AutoInsureBot.Models;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace AutoInsureBot.Handlers
{
    public class CompletedStateHandler : IBotStateHandler
    {
        public Task HandleAsync(Update update, UserSession userSession, ITelegramBotClient botClient)
        {
            if(update.Message?.Text=="/restart")
            {
                userSession.passportInformation = null;
                userSession.techPassportInformation = null;
                userSession.botState = BotState.None;
                botClient.SendMessage(update.Message.From.Id, "Bot restarted. Type /start, for new session.");
            }
            return Task.CompletedTask;
        }
    }


}
