using AutoInsureBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoInsureBot.Handlers.BotStateHandlerFactory
{
    public interface IBotStateHandlerFactory
    {
        public IBotStateHandler GetHandler(BotState state);
    }

}
