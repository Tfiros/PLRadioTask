using Xunit;
using Domain.Services;

namespace Tests.Services;

public class SlugGeneratorServiceTests
{
    [Fact]
    public void Generate_ShouldReturnSlugInCorrectFormat()
    {
        // Arrange
        var title = "Testowy Tytuł Artykułu!";

        // Act
        var slug = SlugGeneratorService.GenerateSlug(title);

        // Assert
        Assert.Equal("testowy-tytul-artykulu", slug);
    }
}