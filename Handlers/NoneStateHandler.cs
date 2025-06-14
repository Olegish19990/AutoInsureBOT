using AutoInsureBot.Models;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace AutoInsureBot.Handlers
{
    public class NoneStateHandler : IBotStateHandler
    {
        public async Task HandleAsync(Update update, UserSession userSession, ITelegramBotClient botClient)
        {
            var userId = update.CallbackQuery?.From?.Id
                     ?? update.Message?.From?.Id
                     ?? throw new InvalidOperationException("Cannot determine user ID");


            if ((update.Message?.Text == "/start") || (update.Message?.Text == "/help"))
            {



                string helloMessage = " I’m your virtual assistant for purchasing car insurance.\r\n" +
                    "My job is to help you get your insurance policy quickly and easily.\r\n\r\n" +
                    "Here’s how it works:\r\n" +
                    "1 You send photos of your passport and vehicle registration document.\r\n" +
                    "2️ I will process the documents and show you the extracted information.\r\n" +
                    "3️ Once you confirm the data, I’ll offer you a fixed price — 100 USD.\r\n. " +
                    "4 After your agreement, I’ll issue your insurance policy and send it to you." +
                    "\n\nPlease send photo of you're passport";
                await botClient.SendMessage(userId, helloMessage);
                userSession.botState = BotState.AwaitingPassport;
            }

         
 
        }
    }


}
