using Xunit;
using Moq;
using Application.Services;
using Xunit;
using Moq;
using Application.Services;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;

namespace Tests.Services;

public class ArticleServiceTests
{
    [Fact]
    public async Task GetStatisticsAsync_ShouldReturnCorrectCounts()
    {
        // Arrange
        var mockRepo = new Mock<IArticleRepository>();
        mockRepo.Setup(r => r.GetAllAsync(null)).ReturnsAsync(new List<Article>
        {
            new Article { Status = ArticleStatus.Draft },
            new Article { Status = ArticleStatus.Published },
            new Article { Status = ArticleStatus.Published, Category = new Category { Name = "Tech" } },
            new Article { Status = ArticleStatus.Published, Category = new Category { Name = "Tech" } }
        });

        var service = new ArticleService(mockRepo.Object, Mock.Of<ICategoryRepository>());

        // Act
        var stats = await service.GetStatisticsAsync();

        // Assert
        Assert.Equal(1, stats.DraftCount);
        Assert.Equal(3, stats.PublishedCount);
        Assert.Equal("Tech", stats.MostUsedCategory);
    }
}

