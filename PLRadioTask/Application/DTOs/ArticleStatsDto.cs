namespace Application.DTOs;


public class ArticleStatsDto
{
    public int DraftCount { get; set; }
    public int PublishedCount { get; set; }
    public string? MostUsedCategory { get; set; }
}