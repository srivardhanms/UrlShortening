using FluentAssertions;
using UrlShortener.Implementations;
using Xunit;

namespace UrlShortener.Tests.ImplementationsTests
{
    public class RandomGeneratorTests
    {
        [Fact]
        public void GetRandomString_CallTwice_ReturnDifferentStrings()
        {
            // Arrange
            var random = new RandomGenerator();

            // Act
            var randomString1 = random.GetRandomString();
            var randomString2 = random.GetRandomString();

            // Assert
            randomString1.Should().NotBeEquivalentTo(randomString2);
        }
    }
}
