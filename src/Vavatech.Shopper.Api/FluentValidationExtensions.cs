public static class FluentValidationExtensions
{
    public static IDictionary<string, string[]> ToDictionary(this FluentValidation.Results.ValidationResult validationResult)
    {
        return validationResult.Errors
            .GroupBy(p => p.PropertyName)
            .ToDictionary(
            g => g.Key, 
            g => g.Select(x => x.ErrorMessage).ToArray()
            );
    }
}
