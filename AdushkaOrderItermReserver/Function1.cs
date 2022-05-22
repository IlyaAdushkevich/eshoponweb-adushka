using System;
using System.IO;
using System.Text;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace AdushkaOrderItermReserver
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static void Run(
            [ServiceBusTrigger("adushkaservicebusqueue", Connection = "ConnectionStrings:ServiceBusConnStr")]
        string myQueueItem,
            Int32 deliveryCount,
            DateTime enqueuedTimeUtc,
            string messageId,
            [Blob("reserved-order-storage/{rand-guid}.json", FileAccess.Write, Connection = "AzureWebJobsStorage")]
        Stream outputBlob,
            ILogger log)
        {
            outputBlob.WriteAsync(Encoding.Default.GetBytes(myQueueItem));
        }
    }
}
