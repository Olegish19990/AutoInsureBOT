using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoInsureBot.Services
{
    internal interface IMindeeService
    {
        public Task<string> ExtractPassportDataAsync(Stream fileStream, string fileName);
    }
}
