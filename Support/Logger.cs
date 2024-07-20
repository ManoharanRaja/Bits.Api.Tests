using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bits.Api.Tests.Support
{
    public static class Logger
    {
        public static void LogRequest(RestClient client, RestRequest request)
        {
            Console.WriteLine("Request:");
            Console.WriteLine($"{request.Method} {client.BuildUri(request)}");
            Console.WriteLine("Headers:");
            foreach (var header in request.Parameters.Where(p => p.Type == ParameterType.HttpHeader))
            {
                Console.WriteLine($"{header.Name}: {header.Value}");
            }

            if (request.Parameters.Any(p => p.Type == ParameterType.RequestBody))
            {
                var bodyParam = request.Parameters.First(p => p.Type == ParameterType.RequestBody);
                Console.WriteLine($"Body: {JsonConvert.SerializeObject(bodyParam.Value, Formatting.Indented)}");
            }
            Console.WriteLine();
        }

        public static void LogResponse(RestResponse response)
        {
            Console.WriteLine("Response:");
            Console.WriteLine($"Status Code: {response.StatusCode}");
            Console.WriteLine($"Content: {JsonConvert.DeserializeObject(response.Content)}");
            Console.WriteLine();
        }
    }
}
