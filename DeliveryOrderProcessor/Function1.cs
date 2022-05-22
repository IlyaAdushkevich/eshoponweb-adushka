using System;
using System.IO;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DeliveryOrderProcessor
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            [CosmosDB(
                databaseName: "adushkasqldatabase",
                collectionName: "OrderDetailsContainer",
                ConnectionStringSetting = "ConnectionStrings:CosmosDbConnStr")]
            IAsyncCollector<dynamic> toDoItemsOut,
            ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            if (string.IsNullOrEmpty(requestBody))
            {
                return new BadRequestObjectResult("Cannot find order items");
            }

            try
            {
                dynamic data = JsonConvert.DeserializeObject(requestBody);
                await toDoItemsOut.AddAsync(data);
            }
            catch (Exception e)
            {
                log.LogError(e.Message);
                return new InternalServerErrorResult();
            }

            return new OkObjectResult("order items has been successfully reserved");
        }
    }
}
