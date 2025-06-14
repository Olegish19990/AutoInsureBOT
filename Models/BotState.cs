using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoInsureBot.Models
{
    public enum BotState
    {
        None,
        AwaitingPassport,        
        AwaitingTechPassport,    
        AwaitingDataConfirmation,
        AwaitingPriceAgreement,  
        Completed,
        GenereatePolicy,
    }
}

