using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using UrlShortener.Dtos;
using UrlShortener.Interfaces;
using UrlShortener.Settings;

namespace UrlShortener.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EncodeController : ControllerBase
    {
        private readonly ILogger<EncodeController> _logger;
        private readonly IRepository _repository;
        private readonly IRandomGenerator _randomGenerator;
        private readonly IConfiguration _configuration;

        public EncodeController(
            ILogger<EncodeController> logger, 
            IRepository repository, 
            IRandomGenerator randomGenerator,
            IConfiguration configuration)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _randomGenerator = randomGenerator ?? throw new ArgumentNullException(nameof(randomGenerator));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        [HttpPost]
        public IActionResult Encode(UrlDto request)
        {
            _logger.LogInformation("Started execution of Encode!");

            if (string.IsNullOrEmpty(request?.Url))
                return new BadRequestObjectResult("Bad Request!");

            var shortner = new Shortner();
            _configuration.GetSection(nameof(Shortner)).Bind(shortner);

            string shortenedUrl = _repository.GetEncodedUrl(request.Url);
            if (!string.IsNullOrEmpty(shortenedUrl))
                return new OkObjectResult(new ShortenedUrlDto() { ShortenedUrl = shortenedUrl });

            do
            {
                string randomString = _randomGenerator.GetRandomString();
                shortenedUrl = $"{shortner.BaseUrl}/{randomString}";
            } while (!string.IsNullOrEmpty(_repository.GetDecodedUrl(shortenedUrl)));
            
            _repository.Add(shortenedUrl, request.Url);

            return new OkObjectResult(new ShortenedUrlDto() { ShortenedUrl = shortenedUrl })
            {
                StatusCode = (int?)HttpStatusCode.Created
            };
        }
    }
}
