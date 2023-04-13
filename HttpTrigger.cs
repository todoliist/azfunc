using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using System;
namespace T.Function
{
    public static class HttpTrigger
    {
        [FunctionName("HttpTrigger")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "test")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("Tim processed a request.");
            var connectionString = "Server=tcp:timdbserver.database.windows.net,1433;Initial Catalog=timsql;Persist Security Info=False;User ID=CloudSA0b4fc139;Password=Welcome54321!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            try
            {
                // Try to open a SQL connection using the connection string
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    log.LogInformation("Database connection successful.");
                    return new OkObjectResult("OKOK");
                }
            }
            catch (Exception ex)
            {
                // Catch any exceptions that might occur and output the error message
                log.LogInformation("Database connection failed: {ex.Message}");
                return new OkObjectResult("Not OK");
            }
        }
    }
}
