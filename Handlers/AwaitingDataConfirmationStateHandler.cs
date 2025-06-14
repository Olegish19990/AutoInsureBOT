using AutoInsureBot.Models;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace AutoInsureBot.Handlers
{
    public class AwaitingDataConfirmationStateHandler : IBotStateHandler
    {
        private const string Price = "100 USD";

        public async Task HandleAsync(Update update, UserSession userSession, ITelegramBotClient botClient)
        {
            var userId = update.CallbackQuery?.From?.Id
                     ?? update.Message?.From?.Id
                     ?? throw new InvalidOperationException("Cannot determine user ID");

            if (update.Type != UpdateType.CallbackQuery || update.CallbackQuery?.Data == null)
            {
                await botClient.SendMessage(userId, "Invalid operation. Please try again.");
                return;
            }

            var callbackData = update.CallbackQuery.Data;

            switch (callbackData)
            {
                case "confirm_yes_passport":
                    await HandlePassportConfirmed(userId, userSession, botClient);
                    break;

                case "confirm_no_passport":
                    await HandlePassportRejected(userId, userSession, botClient);
                    break;

                case "confirm_yes_techpassport":
                    await HandleTechPassportConfirmed(userId, userSession, botClient);
                    break;

                case "confirm_no_techpassport":
                    await HandleTechPassportRejected(userId, userSession, botClient);
                    break;

                default:
                    await botClient.SendMessage(userId, "Unknown action. Please try again.");
                    break;
            }
        }

        private async Task HandlePassportConfirmed(long userId, UserSession userSession, ITelegramBotClient botClient)
        {
            if (userSession.passportInformation == null)
            {
                await botClient.SendMessage(userId, "Error: Passport data is missing. Please send photo again.");
                userSession.botState = BotState.AwaitingPassport;
                return;
            }

            await botClient.SendMessage(userId, "Thank you! The data has been saved.");
            userSession.botState = BotState.AwaitingTechPassport;
            await botClient.SendMessage(userId, "Please send photo of your tech passport (vehicle identification document).");
        }

        private async Task HandlePassportRejected(long userId, UserSession userSession, ITelegramBotClient botClient)
        {
            await botClient.SendMessage(userId, "Please send photo again.");
            userSession.passportInformation = null;
            userSession.botState = BotState.AwaitingPassport;
        }

        private async Task HandleTechPassportConfirmed(long userId, UserSession userSession, ITelegramBotClient botClient)
        {
            if (userSession.techPassportInformation == null)
            {
                await botClient.SendMessage(userId, "Error: Tech passport data is missing. Please send photo again.");
                userSession.botState = BotState.AwaitingTechPassport;
                return;
            }

            await botClient.SendMessage(userId, "Thank you! The data has been saved.");
            userSession.botState = BotState.AwaitingPriceAgreement;

            await SendPriceAgreement(userId, botClient);
        }

        private async Task HandleTechPassportRejected(long userId, UserSession userSession, ITelegramBotClient botClient)
        {
            await botClient.SendMessage(userId, "Please send photo again.");
            userSession.techPassportInformation = null;
            userSession.botState = BotState.AwaitingTechPassport;
        }

        private async Task SendPriceAgreement(long userId, ITelegramBotClient botClient)
        {
            string message = $"The fixed price for your insurance is {Price}.\n\nDo you agree with the price?";

            var inlineKeyboard = new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Yes", "confirm_yes_price"),
                    InlineKeyboardButton.WithCallbackData("No", "confirm_no_price")
                }
            });

            await botClient.SendMessage(
                chatId: userId,
                text: message,
                replyMarkup: inlineKeyboard
            );
        }
    }
}
