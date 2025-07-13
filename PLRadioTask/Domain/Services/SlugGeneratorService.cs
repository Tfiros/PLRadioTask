using System.Text.RegularExpressions;

namespace Domain.Services;

public class SlugGeneratorService
{
    public static string GenerateSlug(string inputTitle)
    {
        inputTitle = inputTitle.ToLowerInvariant();
        inputTitle = Regex.Replace(inputTitle, @"[^a-z0-9\s-]","");
        inputTitle = Regex.Replace(inputTitle, @"\s+", "-").Trim('-');
        return inputTitle;
    }
}