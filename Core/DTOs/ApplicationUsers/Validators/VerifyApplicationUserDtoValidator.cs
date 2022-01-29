using FluentValidation;
using DNTPersianUtils.Core;
using Project.Helpers;

namespace Project.DTOs.ApplicationUsers.Validators
{
    public class VerifyApplicationUserDtoValidator : AbstractValidator<VerifyApplicationUserDTO>
    {
        public VerifyApplicationUserDtoValidator()
        {
            RuleFor(p => p.Phone)
                .NotEmpty().WithMessage("{PropertyName} اجباری است")
                .NotNull()
                .MaximumLength(11).WithMessage("{PropertyName} باید حداکثر {ComparisonValue} کاراکتر باشد.")
                .Must(x =>
                {
                    return x.IsValidIranianPhoneNumber();
                }).WithMessage("{PropertyName} نامعتبر است")
                .WithName(x => x.Phone.GetDisplayName());

            RuleFor(p => p.VerificationCode)
                .NotEmpty().WithMessage("{PropertyName} اجباری است")
                .NotNull()
                .WithName(x => x.VerificationCode.GetDisplayName());
        }
    }
}
