using Domain.Entities;
using Domain.Enums;

namespace Domain.Entities;

public class Article
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string Author { get; set; }
    public string Slug { get; set; }
    public ArticleStatus Status { get; set; } = ArticleStatus.Draft;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Guid? CategoryId { get; set; }
    public Category? Category { get; set; }

    public void Publish()
    {
        Status = ArticleStatus.Published;
    }
}