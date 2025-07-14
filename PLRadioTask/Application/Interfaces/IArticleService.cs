namespace Application.Interfaces;

using Application.DTOs;
using Domain.Enums;

public interface IArticleService
{
    Task<Guid> CreateArticleAsync(CreateArticleDto dto);
    Task<IEnumerable<ArticleDto>> GetAllAsync(ArticleStatus? status = null);
    Task<ArticleDto?> GetByIdAsync(Guid id);
    Task<bool> UpdateArticleAsync(Guid id, UpdateArticleDto dto);
    Task<bool> PublishAsync(Guid id);
    Task<ArticleStatsDto> GetStatisticsAsync();

}