using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.Implementations;
using Xunit;

namespace UrlShortener.Tests.ImplementationsTests
{
    public class RepositoryTests
    {
        [Fact]
        public void Repository_AddAndGet_ReturnValid()
        {
            // Arrange
            var repository = new Repository();

            var shortUrl = "abcd";
            var actualUrl = "efgh";

            // Act
            repository.Add(shortUrl, actualUrl);

            // Assert
            repository.GetDecodedUrl(shortUrl).Should().Be(actualUrl);
            repository.GetEncodedUrl(actualUrl).Should().Be(shortUrl);
        }
    }
}
