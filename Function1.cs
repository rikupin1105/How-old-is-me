using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace How_old_is_me
{
    public static class Howoldisme
    {
        [FunctionName("How_old_is_me")]
        public static async Task<IActionResult> RunAsync([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequestMessage req, ILogger log)
        {
            var data = await req.Content.ReadAsAsync<DialogFlowRequest>();
            var Birthday = data.queryResult.parameters.Birthday;
            
            var today = DateTime.Today;
            int age = today.Year - Birthday.Year;
            if (Birthday > today.AddYears(-age)) age--;

            var ResponceObject = new DialogFlowResponce();
            ResponceObject.fulfillmentText = age + "çŒÇ≈Ç∑";
            string json = JsonConvert.SerializeObject(ResponceObject);

            //JSONÇ≈ÉäÉ^Å[Éì
            var ReturnObject = new ObjectResult(json);
            return ReturnObject;
        }
    }
    class DialogFlowResponce
    {
        public string fulfillmentText { get; set; }
    }
}
