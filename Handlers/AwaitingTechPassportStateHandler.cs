using AutoInsureBot.Models;
using AutoInsureBot.Services;
using Newtonsoft.Json.Linq;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace AutoInsureBot.Handlers
{
    public class AwaitingTechPassportStateHandler : IBotStateHandler
    {
        private readonly MindeeService _mindeeService;

        public AwaitingTechPassportStateHandler(MindeeService mindeeService)
        {
            _mindeeService = mindeeService;
        }

        public async Task HandleAsync(Update update, UserSession userSession, ITelegramBotClient botClient)
        {
            try
            {
                var userId = update.CallbackQuery?.From?.Id
                          ?? update.Message?.From?.Id
                          ?? throw new InvalidOperationException("Cannot determine user ID");

                if (update.Message?.Photo == null)
                {
                    await botClient.SendMessage(
                        chatId: userId,
                        text: "Please send a photo of the technical passport."
                    );
                    return;
                }

                var bestPhoto = update.Message.Photo.OrderByDescending(p => p.FileSize).First();

                var file = await botClient.GetFile(bestPhoto.FileId);

                using (var memoryStream = new MemoryStream())
                {
                    await botClient.DownloadFile(file.FilePath, memoryStream);
                    memoryStream.Position = 0;

                    var response = await _mindeeService.ExtractTechPassportDataAsync(memoryStream, file.FilePath);
                    if (string.IsNullOrEmpty(response))
                    {
                        await SendErrorMessage(botClient, userId, "Failed to extract data from the image.");
                        return;
                    }

                    PredictionTechPassport? prediction = null;

                    try
                    {
                        var jObject = JObject.Parse(response);
                        var predictionToken = jObject["document"]?["inference"]?["prediction"];
                        prediction = predictionToken?.ToObject<PredictionTechPassport>();

                        if (prediction == null)
                        {
                            await SendErrorMessage(botClient, userId, "Failed to parse extracted data.");
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error parsing prediction: {ex}");
                        await SendErrorMessage(botClient, userId, "Error occurred while processing the extracted data.");
                        return;
                    }

                    userSession.techPassportInformation = prediction;

                    string message = $"Tech Passport Information:\n" +
                                     $"Surname or Company: {prediction.surname_or_company?.value ?? "N/A"}\n" +
                                     $"Address: {prediction.address?.value ?? "N/A"}\n" +
                                     $"Registration Number: {prediction.registration_number?.value ?? "N/A"}\n";

                    var inlineKeyboard = new InlineKeyboardMarkup(new[]
                    {
                        new[]
                        {
                            InlineKeyboardButton.WithCallbackData("Yes", "confirm_yes_techpassport"),
                            InlineKeyboardButton.WithCallbackData("No", "confirm_no_techpassport")
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
            catch (Exception ex)
            {
                Console.WriteLine($"Unhandled error in AwaitingTechPassportStateHandler: {ex}");
                if (update?.Message?.Chat != null)
                {
                    await SendErrorMessage(botClient, update.Message.Chat.Id, "An unexpected error occurred. Please try again later.");
                }
            }
        }

        private static async Task SendErrorMessage(ITelegramBotClient botClient, long chatId, string message)
        {
            await botClient.SendMessage(
                chatId: chatId,
                text: message
            );
        }
    }
}
