namespace FMDApi.Extension
{
    public static class ValidatorExtension
    {
        public static object FormatValidationErrors(this FluentValidation.Results.ValidationResult result) =>
            result.Errors.Select(e => new
            {
                field = e.PropertyName,
                message = e.ErrorMessage
            });
    }
}