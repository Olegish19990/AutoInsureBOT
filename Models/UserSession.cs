using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoInsureBot.Models
{
    public class UserSession
    {

        public string passportInformation { get; set; } = string.Empty;
        public string techPasportInformation { get; set; } = string.Empty;
        public BotState botState { get; set; } = BotState.None;
      
    }
}
