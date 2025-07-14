namespace Application.DTOs;

using Domain.Enums;

public class ArticleDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public ArticleStatus Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? Category { get; set; }
}
