using AutoInsureBot.Models;
using AutoInsureBot.Services;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace AutoInsureBot.Handlers
{
    public class AwaitingPassportStateHandler : IBotStateHandler
    {
        private MindeeService _mindeeService;
        public AwaitingPassportStateHandler(MindeeService mindeeService)
        {
            _mindeeService = mindeeService;
        }
        public async Task HandleAsync(Update update, UserSession userSession, ITelegramBotClient botClient)
        {
            var userId = update.CallbackQuery?.From?.Id
                     ?? update.Message?.From?.Id
                     ?? throw new InvalidOperationException("Cannot determine user ID");

            try
            {

                if (update.Message.Photo != null)
                {


                    var bestPhoto = update.Message.Photo.OrderByDescending(p => p.FileSize).First();
                    var file = await botClient.GetFile(bestPhoto.FileId);

                    using (var memoryStream = new MemoryStream())
                    {
                        await botClient.DownloadFile(file.FilePath, memoryStream);
                        memoryStream.Position = 0;


                        var response = await _mindeeService.ExtractPassportDataAsync(memoryStream, file.FilePath);
                        var jObject = JObject.Parse(response);

                        var predictionToken = jObject["document"]?["inference"]?["prediction"];

                        var prediction = predictionToken.ToObject<PredictionPassport>();

                        string message = $"Passport Information:\n" +
                                         $"Surname: {prediction.surname?.value}\n" +
                                         $"Name: {prediction.name?.value}\n" +
                                         $"Patronymic: {prediction.patronymic?.value}\n" +
                                         $"Date of Birth: {prediction.date_of_birth?.value}\n" +
                                         $"Sex: {prediction.sex?.value}\n" +
                                         $"Nationality: {prediction.nationality?.value}\n" +
                                         $"Document Number: {prediction.document_no?.value}\n" +
                                         $"Record Number: {prediction.record_no?.value}\n" +
                                         $"Date of Expiry: {prediction.date_of_expiry?.value}";


                        userSession.passportInformation = prediction;
                        var inlineKeyboard = new InlineKeyboardMarkup(new[]
                        {
                      new []
                     {
                            InlineKeyboardButton.WithCallbackData("Yes", "confirm_yes_passport"),
                            InlineKeyboardButton.WithCallbackData("No", "confirm_no_passport")
                      }
                    });

                        await botClient.SendMessage(
                            chatId: userId,
                            text: message + "\n\nIs the information correct?",
                            replyMarkup: inlineKeyboard
                        );

                        userSession.botState = BotState.AwaitingDataConfirmation;



                    }
                }
            }
            catch(Exception ex)
            {
                await botClient.SendMessage(userId, "An unexpected error occurred. Please try again later.");

            }


            await Task.Delay(1);
        }
    }


}
