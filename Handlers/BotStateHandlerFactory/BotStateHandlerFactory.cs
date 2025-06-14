using AutoInsureBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoInsureBot.Handlers.BotStateHandlerFactory
{
    public class BotStateHandlerFactory : IBotStateHandlerFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public BotStateHandlerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IBotStateHandler GetHandler(BotState state)
        {
            return state switch
            {
                BotState.None => _serviceProvider.GetRequiredService<NoneStateHandler>(),
                BotState.AwaitingPassport => _serviceProvider.GetRequiredService<AwaitingPassportStateHandler>(),
                BotState.AwaitingTechPassport => _serviceProvider.GetRequiredService<AwaitingTechPassportStateHandler>(),
                BotState.AwaitingDataConfirmation => _serviceProvider.GetRequiredService<AwaitingDataConfirmationStateHandler>(),
                BotState.AwaitingPriceAgreement => _serviceProvider.GetRequiredService<AwaitingPriceAgreementStateHandler>(),
                _ => throw new ArgumentOutOfRangeException(nameof(state), state, null)
            };
        }
    }
}
