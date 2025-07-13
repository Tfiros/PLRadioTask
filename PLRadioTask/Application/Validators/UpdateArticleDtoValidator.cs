using Application.DTOs;

namespace Application.Validators;
using FluentValidation;
public class UpdateArticleDtoValidator : AbstractValidator<UpdateArticleDto>
{
    public UpdateArticleDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.");

        RuleFor(x => x.Content)
            .MinimumLength(10).WithMessage("Content must be at least 10 characters.");
    }
}