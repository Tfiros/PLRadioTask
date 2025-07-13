using Domain.Entities;
using Domain.Enums;

namespace Application.Interfaces;

public interface IArticleRepository
{
    Task AddAsync(Article article);
    Task<Article?> GetByIdAsync(Guid id);
    Task<IEnumerable<Article>> GetAllAsync(ArticleStatus? status = null);
    Task UpdateAsync(Article article);
    Task<bool> SlugExistsAsync(string slug);
}