using FluentValidation;

namespace FMDApi.Validators
{
    public static class PhoneRuleBuilderExtensions
    {
        /// <summary>
        /// Valida se o número de telefone está no formato brasileiro: (11) 91234-5678, 11912345678, etc.
        /// </summary>
        public static IRuleBuilderOptions<T, string> ValidPhone<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty()
                .NotNull()
                .MaximumLength(20)
                .Matches(@"^\(?\d{2}\)?[\s-]?\d{4,5}-?\d{4}$")
                .WithMessage("Please provide a valid phone number, ex: (11) 91234-5678 ou 11912345678.");
        }
    }
}
