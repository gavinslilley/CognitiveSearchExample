using GL.CSE.Interfaces.AzureSearch;
using GL.CSE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Net;
using System.Threading.Tasks;

namespace GL.CSE.Functions.Functions
{
    public class ExampleSearch
    {
        private readonly ILogger<ExampleSearch> _logger;
        private readonly IExampleQueryService _queryService;

        public ExampleSearch(ILogger<ExampleSearch> log, IExampleQueryService queryService)
        {
            _logger = log;
            _queryService = queryService;
        }

        [FunctionName("ExampleSearch")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "name" })]
        [OpenApiParameter(name: "name", In = ParameterLocation.Query, Required = true, Type = typeof(string), Description = "The **Name** parameter")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            var searchModel = new SearchModel() { SearchTerm = name };

            var results = await _queryService.SearchIndexAsync(searchModel);

            return new OkObjectResult(results);
        }
    }
}

