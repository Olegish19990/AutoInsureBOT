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
    public interface IBotStateHandler
    {
        public Task HandleAsync(Update update, UserSession userSession, ITelegramBotClient botClient);
    }
}
