using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using UrlShortener.Controllers;
using UrlShortener.Dtos;
using UrlShortener.Interfaces;
using Xunit;

namespace UrlShortener.Tests.ControllerTests
{
    public class DecoderTests
    {
        private Mock<ILogger<DecodeController>> _mockLogger = new();
        private Mock<IRepository> _mockRepository = new();

        [Fact]
        public void Constructor_LoggerNull_ArgumentNullException()
        {
            // Arrange
            var act = () => new DecodeController(null, null);

            // Act
            act

            // Assert
            .Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Constructor_RepositoryNull_ArgumentNullException()
        {
            // Arrange
            var act = () => new DecodeController(_mockLogger.Object, null);

            // Act
            act

            // Assert
            .Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Decode_RequestNull_BadRequest()
        {
            // Arrange
            var decoder = new DecodeController(_mockLogger.Object, _mockRepository.Object);

            // Act
            var result = decoder.Decode(null);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public void Decode_ShortUrlNull_BadRequest()
        {
            // Arrange
            var decoder = new DecodeController(_mockLogger.Object, _mockRepository.Object);

            // Act
            var result = decoder.Decode(new ShortenedUrlDto());

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public void Decode_ValidRequest_BadRequest()
        {
            // Arrange
            var url = "https://google.com";
            _mockRepository.Setup(r => r.GetDecodedUrl(It.IsAny<string>())).Returns(url);
            var decoder = new DecodeController(_mockLogger.Object, _mockRepository.Object);

            // Act
            var result = decoder.Decode(new ShortenedUrlDto() { ShortenedUrl = "https://localhost:3001:/abcd" });

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okObjectResult = result as OkObjectResult;
            var payload = okObjectResult.Value as UrlDto;
            payload.Url.Should().Be(url);
        }
    }
}
