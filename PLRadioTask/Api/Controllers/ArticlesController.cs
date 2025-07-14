using Application.DTOs;
using Application.Interfaces;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ArticlesController : ControllerBase
{
    private readonly IArticleService _articleService;

    public ArticlesController(IArticleService articleService)
    {
        _articleService = articleService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateArticleDto dto)
    {
        var id = await _articleService.CreateArticleAsync(dto);
        return CreatedAtAction(nameof(GetById), new { articleId = id }, null);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] ArticleStatus? status = null)
    {
        var articles = await _articleService.GetAllAsync(status);
        if (articles.IsNullOrEmpty()) return NotFound();
        return Ok(articles);
    }

    [HttpGet("{articleId:guid}")]
    public async Task<IActionResult> GetById(Guid articleId)
    {
        var article = await _articleService.GetByIdAsync(articleId);
        if (article == null)
            return NotFound();

        return Ok(article);
    }

    [HttpPut("{articleId:guid}")]
    public async Task<IActionResult> Update(Guid articleId, [FromBody] UpdateArticleDto dto)
    {
        var updated = await _articleService.UpdateArticleAsync(articleId, dto);
        if (!updated)
            return NotFound();

        return Ok();
    }
    
    [HttpPost("{articleId:guid}/publish")]
    public async Task<IActionResult> Publish(Guid articleId)
    {
        var result = await _articleService.PublishAsync(articleId);
        if (!result)
            return NotFound();

        return Ok();
    }
    [HttpGet("stats")]
    public async Task<IActionResult> GetStats()
    {
        var stats = await _articleService.GetStatisticsAsync();
        return Ok(stats);
    }
}