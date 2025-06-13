using Mindee;
using Mindee.Http;
using Mindee.Input;
using Mindee.Product.Generated;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace AutoInsureBot.Services
{
    public class MindeeService
    {
        private MindeeClient _mindeeCleint;
        public MindeeService(IConfiguration configuration)
        {
            var token = configuration["Mindee:Token"];
            _mindeeCleint = new MindeeClient(token);
        }

        public async Task<string> ExtractPassportDataAsync(Stream fileStream , string fileName)
        {           
            var inputSource = new LocalInputSource(fileStream,fileName);

            CustomEndpoint endpoint = new CustomEndpoint(
                endpointName: "ukraine_passport",
                accountName: "OlehIshenko",
                version: "1"
            );

            var response = await _mindeeCleint
                .EnqueueAndParseAsync<GeneratedV1>(inputSource, endpoint);

            return response.RawResponse; //return json response from api

        }

    }
}
