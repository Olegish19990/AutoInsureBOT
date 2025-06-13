using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoInsureBot.Models
{
    public class Root
    {
        public Document document { get; set; }
    }

    public class Document
    {
        public Inference inference { get; set; }
    }

    public class Inference
    {
        public Prediction prediction { get; set; }
    }

    public class Prediction
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
