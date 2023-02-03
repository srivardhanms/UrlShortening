using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using UrlShortener.Interfaces;
using UrlShortener.Settings;

namespace UrlShortener.Controllers
{
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly ILogger<StatusController> _logger;
        private readonly IRepository _repository;
        private readonly IConfiguration _configuration;

        public StatusController(ILogger<StatusController> logger, IRepository repository, IConfiguration configuration)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _repository = repository?? throw new ArgumentNullException(nameof(repository));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var shortner = new Shortner();
            _configuration.GetSection(nameof(Shortner)).Bind(shortner);

            var actualUri = _repository.GetDecodedUrl($"{shortner.BaseUrl}/{id}");
            if (string.IsNullOrEmpty(actualUri))
                return new NotFoundObjectResult(string.Empty);

            return new RedirectResult(actualUri, true);
        }
    }
}
