using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace AutoInsureBot.Services
{
    public class TelegramBotService
    {
        public TelegramBotClient Client { get; }

        public TelegramBotService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            var token = configuration["TelegramBot:Token"];
            Client = new TelegramBotClient(token, httpClientFactory.CreateClient());
        }
    }
}
