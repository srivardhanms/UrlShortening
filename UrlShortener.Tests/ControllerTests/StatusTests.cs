using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using UrlShortener.Controllers;
using UrlShortener.Interfaces;
using Xunit;

namespace UrlShortener.Tests.ControllerTests
{
    public class StatusTests
    {
        private Mock<ILogger<StatusController>> _mockLogger = new();
        private Mock<IRepository> _mockRepository = new();
        private Mock<IConfiguration> _mockConfiguration = new();

        [Fact]
        public void Constructor_LoggerNull_ArgumentNullException()
        {
            // Arrange
            var act = () => new StatusController(null, null, null);

            // Act
            act

            // Assert
            .Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Constructor_RepositoryNull_ArgumentNullException()
        {
            // Arrange
            var act = () => new StatusController(_mockLogger.Object, null, null);

            // Act
            act

            // Assert
            .Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Constructor_ConfigurationNull_ArgumentNullException()
        {
            // Arrange
            var act = () => new StatusController(_mockLogger.Object, _mockRepository.Object, null);

            // Act
            act

            // Assert
            .Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Get_InvalidUrl_NotFound()
        {
            // Arrange
            var testConfiguration = new Dictionary<string, string>()
            {
                { "Shortner:BaseUrl", "https://localhost:3001" }
            };
            var configuration = new ConfigurationBuilder().AddInMemoryCollection(testConfiguration).Build();
            _mockRepository.Setup(r => r.GetDecodedUrl(It.IsAny<string>())).Returns(string.Empty);
            var status = new StatusController(_mockLogger.Object, _mockRepository.Object, configuration);

            // Act
            var result = status.Get("abds");

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public void Get_ValidUrl_Redirect301()
        {
            // Arrange
            var testConfiguration = new Dictionary<string, string>()
            {
                { "Shortner:BaseUrl", "https://localhost:3001" }
            };
            var configuration = new ConfigurationBuilder().AddInMemoryCollection(testConfiguration).Build();
            var actualUrl = "https://google.com";
            _mockRepository.Setup(r => r.GetDecodedUrl(It.IsAny<string>())).Returns(actualUrl);
            var status = new StatusController(_mockLogger.Object, _mockRepository.Object, configuration);

            // Act
            var result = status.Get("asdfa");

            // Assert
            var redirect = result as RedirectResult;
            redirect.Permanent.Should().BeTrue();
            redirect.Url.Should().Be(actualUrl);
        }
    }
}
