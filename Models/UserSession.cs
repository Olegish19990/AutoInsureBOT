using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoInsureBot.Models
{
    public class UserSession
    {

        public PredictionPassport? passportInformation { get; set; }
        public PredictionTechPassport? techPassportInformation { get; set; }
        public BotState botState { get; set; } = BotState.None;
      
    }
}
