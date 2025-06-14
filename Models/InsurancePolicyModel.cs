using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoInsureBot.Models
{
    public class InsurancePolicyModel
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string DateOfBirth { get; set; }
        public string Sex { get; set; }
        public string Nationality { get; set; }
        public string DocumentNumber { get; set; }
        public string RecordNumber { get; set; }
        public string DateOfExpiry { get; set; }

        public string TechPassportSurname { get; set; }
        public string TechPassportAddress { get; set; }
        public string RegistrationNumber { get; set; }

        public string InsuranceAmount { get; set; }
        public string PolicyIssueDate { get; set; }
        public string PolicyNumber { get; set; }
    }


}
