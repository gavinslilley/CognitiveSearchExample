using GL.CSE.Interfaces.AzureSearch;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace GL.CSE.Functions.Functions
{
    public class RebuildIndexes
    {
        private readonly ILogger<RebuildIndexes> _logger;
        private readonly IEnumerable<IIndexService> _indexServices;
        public RebuildIndexes(
        IEnumerable<IIndexService> indexServices,
        ILogger<RebuildIndexes> logger)
        {
            _indexServices = indexServices;
            _logger = logger;
        }

        [FunctionName("RebuildIndexes")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "name" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "text/plain", bodyType: typeof(string), Description = "The OK response")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req)
        {
            _logger.LogInformation("RebuildIndexes has been triggered at: {DateTime.Now}");

            foreach (var indexService in _indexServices)
            {
                await indexService.DeleteIndexAsync(_logger);
                await indexService.CreateIndexAsync(_logger);
                await indexService.PopulateIndexAsync(_logger);
            }

            string responseMessage = $"Eiir RebuildIndexes completed successfully at: {DateTime.Now}";

            return new OkObjectResult(responseMessage);
        }
    }
}

