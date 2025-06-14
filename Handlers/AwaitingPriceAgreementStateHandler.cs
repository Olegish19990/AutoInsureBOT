using AutoInsureBot.Models;
using AutoInsureBot.Services;
using AutoInsureBot.Services.DocumentGeneration;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace AutoInsureBot.Handlers
{
    public class AwaitingPriceAgreementStateHandler : IBotStateHandler
    {
        
       
        public async Task HandleAsync(Update update, UserSession userSession, ITelegramBotClient botClient)
        {
            var callbackData = update.CallbackQuery.Data;
            var userId = update.CallbackQuery.From.Id;

            switch (callbackData)
            {
                case "confirm_yes_price":
                    await botClient.SendMessage(userId, "Great! We will proceed to issue your insurance policy.");


                    var policyText = GenereateInsurePolicyText.BuildPolicyModel(userSession);
                    byte[] pdfBytes = InsurancePolicyDocument.CreatePolicy(policyText);

                    using (var stream = new MemoryStream(pdfBytes))
                    {
                        stream.Position = 0;
                        var inputFile = new InputFileStream(stream, "InsurancePolicy.pdf");

                        await botClient.SendDocument(
                            chatId: userId,
                            document: inputFile,
                            caption: "Here is your insurance policy"
                        );
                    }

                    break;

                case "confirm_no_price":
                    await botClient.SendMessage(userId, "Sorry, 100 USD is the only available price at the moment.");
                    break;

                default:
                    await botClient.SendMessage(userId, "Unknown command.");
                    break;
            }
        }

     
    }


}
