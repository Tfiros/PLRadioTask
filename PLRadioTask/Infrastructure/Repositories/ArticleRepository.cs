using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ArticleRepository : IArticleRepository
{
    private readonly AppDbContext _db;

    public ArticleRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task AddAsync(Article article)
    {
        _db.Articles.Add(article);
        await _db.SaveChangesAsync();
    }

    public async Task<Article?> GetByIdAsync(Guid id)
    {
        return await _db.Articles.Include(a => a.Category).FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<IEnumerable<Article>> GetAllAsync(ArticleStatus? status = null)
    {
        var query = _db.Articles.Include(a => a.Category).AsQueryable();
        if (status.HasValue)
            query = query.Where(a => a.Status == status.Value);
        return await query.ToListAsync();
    }

    public async Task UpdateAsync(Article article)
    {
        _db.Articles.Update(article);
        await _db.SaveChangesAsync();
    }

    public async Task<bool> SlugExistsAsync(string slug)
    {
        return await _db.Articles.AnyAsync(a => a.Slug == slug);
    }
}