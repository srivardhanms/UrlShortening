using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using UrlShortener.Controllers;
using UrlShortener.Dtos;
using UrlShortener.Interfaces;
using Xunit;

namespace UrlShortener.Tests.ControllerTests
{
    public class EncoderTests
    {
        private static readonly Mock<ILogger<EncodeController>> _mockLogger = new();
        private readonly Mock<IRepository> _mockRepository = new();
        private readonly Mock<IRandomGenerator> _mockRandomGenerator = new();
        private readonly Mock<IConfiguration> _mockConfiguration = new();

        [Fact]
        public void Constructor_LoggerNull_ArgumentNullException()
        {
            // Arrange
            var act = () => new EncodeController(null, null, null, null);

            // Act
            act

            // Assert
            .Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Constructor_RepositoryNull_ArgumentNullException()
        {
            // Arrange
            var act = () => new EncodeController(_mockLogger.Object, null, null, null);

            // Act
            act

            // Assert
            .Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Constructor_RandomGeneratorNull_ArgumentNullException()
        {
            // Arrange
            var act = () => new EncodeController(_mockLogger.Object, _mockRepository.Object, null, null);

            // Act
            act

            // Assert
            .Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Constructor_ConfigurationNull_ArgumentNullException()
        {
            // Arrange
            var act = () => new EncodeController(_mockLogger.Object, _mockRepository.Object, _mockRandomGenerator.Object, null);

            // Act
            act

            // Assert
            .Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Encode_RequestNull_ReturnBadRequest()
        {
            // Arrange
            var encoder = new EncodeController(_mockLogger.Object, _mockRepository.Object, _mockRandomGenerator.Object, _mockConfiguration.Object);

            // Act
            var result = encoder.Encode(null);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public void Encode_UrlNull_ReturnBadRequest()
        {
            // Arrange
            var encoder = new EncodeController(_mockLogger.Object, _mockRepository.Object, _mockRandomGenerator.Object, _mockConfiguration.Object);

            // Act
            var result = encoder.Encode(new UrlDto());

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public void Encode_ExistingUrl_Return200()
        {
            // Arrange
            var testConfiguration = new Dictionary<string, string>()
            {
                { "Shortner:BaseUrl", "https://localhost:3001" }
            };
            var configuration = new ConfigurationBuilder().AddInMemoryCollection(testConfiguration).Build();
            string shortUrl = "https://localhost:3001/abcdefg";
            _mockRepository.Setup(r => r.GetEncodedUrl(It.IsAny<string>())).Returns(shortUrl);

            var encoder = new EncodeController(_mockLogger.Object, _mockRepository.Object, _mockRandomGenerator.Object, configuration);

            // Act
            var result = encoder.Encode(new UrlDto() { Url = "https://google.com" });

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.StatusCode.Should().Be(200);
            var payload = okObjectResult.Value as ShortenedUrlDto;
            payload.ShortenedUrl.Should().Be(shortUrl);
        }

        [Fact]
        public void Encode_NewUrl_Return201()
        {
            // Arrange
            var testConfiguration = new Dictionary<string, string>()
            {
                { "Shortner:BaseUrl", "https://localhost:3001" }
            };
            var configuration = new ConfigurationBuilder().AddInMemoryCollection(testConfiguration).Build();
            _mockRepository.Setup(r => r.GetEncodedUrl(It.IsAny<string>())).Returns(string.Empty);
            _mockRandomGenerator.Setup(r => r.GetRandomString()).Returns("abdc");

            var encoder = new EncodeController(_mockLogger.Object, _mockRepository.Object, _mockRandomGenerator.Object, configuration);

            // Act
            var result = encoder.Encode(new UrlDto() { Url = "https://google.com" });

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            okObjectResult.StatusCode.Should().Be(201);
            var payload = okObjectResult.Value as ShortenedUrlDto;
            payload.ShortenedUrl.Should().Be("https://localhost:3001/abdc");
        }
    }
}
