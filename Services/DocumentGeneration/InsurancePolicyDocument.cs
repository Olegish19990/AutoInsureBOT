using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.IO;
using AutoInsureBot.Models;
namespace AutoInsureBot.Services.DocumentGeneration
{
    public class InsurancePolicyDocument
    {
       
        public static byte[] CreatePolicy(InsurancePolicyModel policy)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(50);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    page.Content().Column(col =>
                    {
                        col.Item().Text("Insurance Policy").FontSize(24).Bold().AlignCenter();
                        col.Item().PaddingVertical(10).LineHorizontal(1);

                        col.Item().Text("Policy Holder Information").Bold().FontSize(16);
                        col.Item().Text($"Surname: {policy.Surname}");
                        col.Item().Text($"Name: {policy.Name}");
                        col.Item().Text($"Document number: {policy.DocumentNumber}");
                        col.Item().Text($"Date of Birth: {policy.DateOfBirth}");
                        col.Item().Text($"Date of expiry passport: {policy.DateOfExpiry}");

                        col.Item().PaddingVertical(10).LineHorizontal(1);
                        col.Item().Text("Vehicle Information").Bold().FontSize(16);
                        col.Item().Text($"Address: {policy.TechPassportAddress}");
                        col.Item().Text($"Reg Number: {policy.RegistrationNumber}");

                        col.Item().PaddingVertical(10).LineHorizontal(1);
                        col.Item().Text("Policy Information").Bold().FontSize(16);
                        col.Item().Text($"Policy No: {policy.PolicyNumber}");
                        col.Item().Text($"Issue Date: {policy.PolicyIssueDate}");
                        col.Item().Text($"Amount: {policy.InsuranceAmount}");
                    });
                });
            });

            using (var stream = new MemoryStream())
            {
                document.GeneratePdf(stream);
                return stream.ToArray();
            }
        }

    }
}

