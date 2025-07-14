using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;

    public CategoryService(ICategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<CategoryDto>> GetAllAsync()
    {
        var categories = await _repository.GetAllAsync();
        return categories.Select(c => new CategoryDto { Id = c.Id, Name = c.Name });
    }

    public async Task<Guid?> CreateAsync(string name)
    {
        var existing = await _repository.GetByNameAsync(name);
        if (existing != null) return null;

        var category = new Category { Id = Guid.NewGuid(), Name = name };
        await _repository.AddAsync(category);
        return category.Id;
    }
}