using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Services;

namespace Application.Services;

public class ArticleService : IArticleService
{
     private readonly IArticleRepository _repository;
     private readonly ICategoryRepository _categoryRepository;

    public ArticleService(IArticleRepository repository, ICategoryRepository categoryRepository)
    {
        _repository = repository;
        _categoryRepository = categoryRepository;
    }

    public async Task<Guid> CreateArticleAsync(CreateArticleDto dto)
    {
        var baseSlug = SlugGeneratorService.GenerateSlug(dto.Title);
        var uniqueSlug = await GenerateUniqueSlugAsync(baseSlug);

        var article = new Article
        {
            Id = Guid.NewGuid(),
            Title = dto.Title,
            Content = dto.Content,
            Author = dto.Author,
            Slug = uniqueSlug,
            CategoryId = dto.CategoryId,
            Status = ArticleStatus.Draft,
            CreatedAt = DateTime.UtcNow
        };

        await _repository.AddAsync(article);
        return article.Id;
    }

    public async Task<IEnumerable<ArticleDto>> GetAllAsync(ArticleStatus? status = null)
    {
        var articles = await _repository.GetAllAsync(status);
        return articles.Select(MapToDto);
    }

    public async Task<ArticleDto?> GetByIdAsync(Guid id)
    {
        var article = await _repository.GetByIdAsync(id);
        return article == null ? null : MapToDto(article);
    }

    public async Task<bool> UpdateArticleAsync(Guid id, UpdateArticleDto dto)
    {
        var article = await _repository.GetByIdAsync(id);
        if (article == null) return false;

        article.Title = dto.Title;
        article.Content = dto.Content;
        article.Author = dto.Author;
        article.CategoryId = dto.CategoryId;
        await _repository.UpdateAsync(article);
        return true;
    }

    public async Task<bool> PublishAsync(Guid id)
    {
        var article = await _repository.GetByIdAsync(id);
        if (article == null) return false;

        article.Publish();
        await _repository.UpdateAsync(article);
        return true;
    }
    public async Task<ArticleStatsDto> GetStatisticsAsync()
    {
        var articles = await _repository.GetAllAsync();

        var stats = new ArticleStatsDto
        {
            DraftCount = articles.Count(a => a.Status == ArticleStatus.Draft),
            PublishedCount = articles.Count(a => a.Status == ArticleStatus.Published),
            MostUsedCategory = articles
                .Where(a => a.Category != null)
                .GroupBy(a => a.Category!.Name)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .FirstOrDefault()
        };

        return stats;
    }
    private async Task<string> GenerateUniqueSlugAsync(string baseSlug)
    {
        var slug = baseSlug;
        var counter = 1;
        while (await _repository.SlugExistsAsync(slug))
        {
            slug = $"{baseSlug}-{counter}";
            counter++;
        }
        return slug;
    }

    private ArticleDto MapToDto(Article article)
    {
        return new ArticleDto
        {
            Id = article.Id,
            Title = article.Title,
            Content = article.Content,
            Author = article.Author,
            Slug = article.Slug,
            Status = article.Status,
            CreatedAt = article.CreatedAt,
            Category = article.Category?.Name
        };
    }
}