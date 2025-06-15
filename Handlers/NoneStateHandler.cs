using AutoInsureBot.Models;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

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



                string helloMessage = "" +
                    "I’m your virtual assistant for purchasing car insurance.\r\n" +
                    "My job is to help you get your insurance policy quickly and easily.\r\n\r\n" +
                    "Here’s how it works:\r\n" +
                    "You send photos of your passport and vehicle registration document.\r\n" +
                    "I will process the documents and show you the extracted information.\r\n" +
                    "Once you confirm the data, I’ll offer you a fixed price — 100 USD.\r\n" +
                    "After your agreement, I’ll issue your insurance policy and send it to you." +
                    "\n\n";
             
                var inlineKeyboard = new InlineKeyboardMarkup(new[]
                    {
                      new []
                     {
                            InlineKeyboardButton.WithCallbackData("Start session", "start_session"),
                      }
                    });


                await botClient.SendMessage(
                       chatId: userId,
                       text:helloMessage,
                       replyMarkup: inlineKeyboard
                   );

                userSession.botState = BotState.SessionStart;


            }



        }
    }


}
