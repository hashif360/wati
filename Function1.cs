using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Wati
{
    public static class Function1
    {
        [FunctionName("add")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
           

            int num1 = Int32.Parse(data?.num1.ToString());
            int num2 = Int32.Parse(data?.num2.ToString());

            var sum = Calculator.Add(num1, num2);

            Database.InsertNumbers(num1, num2, sum);


            return new OkObjectResult(sum);
        }
    }
}
