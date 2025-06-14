using AutoInsureBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace AutoInsureBot.Services
{
    public class GenereateInsurePolicyText
    {
        public static InsurancePolicyModel BuildPolicyModel(UserSession session)
        {
            Random random = new Random();
            return new InsurancePolicyModel
            {
                Surname = session.passportInformation?.surname?.value ?? string.Empty,
                Name = session.passportInformation?.name?.value ?? string.Empty,
                Patronymic = session.passportInformation?.patronymic?.value ?? string.Empty,
                DateOfBirth = TryFormatDate(session.passportInformation?.date_of_birth?.value),
                Sex = session.passportInformation?.sex?.value ?? string.Empty,
                Nationality = session.passportInformation?.nationality?.value ?? string.Empty,
                DocumentNumber = session.passportInformation?.document_no?.value ?? string.Empty,
                RecordNumber = session.passportInformation?.record_no?.value ?? string.Empty,
                DateOfExpiry = TryFormatDate(session.passportInformation?.date_of_expiry?.value),

                TechPassportSurname = session.techPassportInformation?.surname_or_company?.value ?? string.Empty,
                TechPassportAddress = session.techPassportInformation?.address?.value ?? string.Empty,
                RegistrationNumber = session.techPassportInformation?.registration_number?.value ?? string.Empty,

                InsuranceAmount = "100 USD",
                PolicyIssueDate = DateTime.Now.ToString("yyyy-MM-dd"),
                PolicyNumber = random.Next(100000).ToString()
            };
        }

        private static string TryFormatDate(string dateStr)
        {
            if (DateTime.TryParse(dateStr, out var date))
                return date.ToString("yyyy-MM-dd");
            return dateStr ?? string.Empty;
        }
    }
}
