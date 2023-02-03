using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using UrlShortener.Dtos;
using UrlShortener.Interfaces;

namespace UrlShortener.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DecodeController : ControllerBase
    {
        private readonly ILogger<DecodeController> _logger;
        private readonly IRepository _repository;

        public DecodeController(ILogger<DecodeController> logger, IRepository repository)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpPost]
        public IActionResult Decode(ShortenedUrlDto request)
        {
            _logger.LogInformation("Started execution of Decode!");

            if (string.IsNullOrEmpty(request?.ShortenedUrl))
            {
                return new BadRequestObjectResult("Bad request!");
            }

            // If exists we return the Url, else we return empty.
            // P.S: Not using 204 code for empty, because 204 is generally used to indicate to client that 
            // "Whatever you told happened, now you continue.". The scenario would be something like save
            // and continue. In this scenario, that doesn't make sense.
            // In this case the client queried for an info and we returned it. We can think this more as
            // Client Posted a search query and we are returning the result of that. And that query execution
            // was successful, hence 200. That has not resulted in any object which is fine. You searched, 
            // didn't find anything.
            return new OkObjectResult(new UrlDto()
            {
                Url = _repository.GetDecodedUrl(request.ShortenedUrl),
            });
        }
    }
}
