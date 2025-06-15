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
                BotState.SessionStart => _serviceProvider.GetRequiredService<SessionStartHandler>(),
                BotState.AwaitingPassport => _serviceProvider.GetRequiredService<AwaitingPassportStateHandler>(),
                BotState.AwaitingTechPassport => _serviceProvider.GetRequiredService<AwaitingTechPassportStateHandler>(),
                BotState.AwaitingDataConfirmation => _serviceProvider.GetRequiredService<AwaitingDataConfirmationStateHandler>(),
                BotState.AwaitingPriceAgreement => _serviceProvider.GetRequiredService<AwaitingPriceAgreementStateHandler>(),
                BotState.Completed => _serviceProvider.GetRequiredService<CompletedStateHandler>(),
                _ => throw new ArgumentOutOfRangeException(nameof(state), state, null)
            };
        }
    }
}
