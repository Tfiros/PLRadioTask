using Xunit;
using Moq;
using Application.Services;
using Application.Interfaces;
using Domain.Entities;
namespace Tests.Services;

public class CategoryServiceTests
{
    [Fact]
    public async Task CreateAsync_ShouldReturnNull_WhenCategoryAlreadyExists()
    {
        // Arrange
        var mockRepo = new Mock<ICategoryRepository>();
        mockRepo.Setup(r => r.GetByNameAsync("Tech")).ReturnsAsync(new Category { Name = "Tech" });

        var service = new CategoryService(mockRepo.Object);

        // Act
        var result = await service.CreateAsync("Tech");

        // Assert
        Assert.Null(result);
    }
}