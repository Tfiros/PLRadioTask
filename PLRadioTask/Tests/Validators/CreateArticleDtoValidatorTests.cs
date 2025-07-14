using Xunit;
using Application.DTOs;
using Application.Validators;
namespace Tests.Validators;

public class CreateArticleDtoValidatorTests
{
    [Fact]
    public void Validator_ShouldFail_WhenTitleIsEmpty()
    {
        // Arrange
        var validator = new CreateArticleDtoValidator();
        var dto = new CreateArticleDto { Title = "", Content = "abc", Author = "Piotr" };

        // Act
        var result = validator.Validate(dto);

        // Assert
        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "Title");
    }
}