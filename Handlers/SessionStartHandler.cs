using AutoInsureBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace AutoInsureBot.Handlers
{
    public class SessionStartHandler : IBotStateHandler
    {
        public async Task HandleAsync(Update update, UserSession userSession, ITelegramBotClient botClient)
        {
            var userId = update.CallbackQuery?.From?.Id
                    ?? update.Message?.From?.Id
                    ?? throw new InvalidOperationException("Cannot determine user ID");

            var callbackData = update.CallbackQuery.Data;

           if(callbackData=="start_session")
            {
                await botClient.SendMessage(userId, "Please, send photo of you're passport");
                userSession.botState = BotState.AwaitingPassport;
            }

        }
    }
}
