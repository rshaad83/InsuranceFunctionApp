
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using System;

namespace InsuranceFuncApp
{
    public static class HandleQuotes
    {
        [FunctionName("HandleQuotes")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequest req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            string jsonBody = new StreamReader(req.Body).ReadToEnd();

            log.Info("json req body: " + jsonBody);
            VehicleQuoteRequest vehicleQuoteRequest = null;
            try
            {
                 vehicleQuoteRequest = JsonConvert.DeserializeObject< VehicleQuoteRequest>(jsonBody);
            } catch(Exception e)
            {
                log.Error(e.Message);
            }

            Boolean quoteSubmitted = dao.QuotesDAO.addQuoteRequest(vehicleQuoteRequest);
            return quoteSubmitted == true
                ? (ActionResult)new OkObjectResult($"Request for Quote Successfully Submitted !")
                : new BadRequestObjectResult("Error occurred while submitting request for quote");
        }
    }
}
