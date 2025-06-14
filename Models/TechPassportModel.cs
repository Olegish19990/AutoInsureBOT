using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoInsureBot.Models
{
    public class PredictionTechPassport
    {
        public Field address { get; set; }
        public Field registration_number { get; set; }
        public Field surname_or_company { get; set; }
    }

    public class FieldTech
    {
        public string value { get; set; }
    }
}
