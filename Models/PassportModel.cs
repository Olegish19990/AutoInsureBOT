using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoInsureBot.Models
{

    public class PredictionPassport
    {
        public Field date_of_birth { get; set; }
        public Field date_of_expiry { get; set; }
        public Field document_no { get; set; }
        public Field name { get; set; }
        public Field nationality { get; set; }
        public Field patronymic { get; set; }
        public Field record_no { get; set; }
        public Field sex { get; set; }
        public Field surname { get; set; }
    }

    public class Field
    {
        public string value { get; set; }
    }

}
