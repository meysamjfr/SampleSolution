using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DNTPersianUtils.Core;
using Project.Helpers;

namespace Project.DTOs.ApplicationUsers.Validators
{
    public class LoginApplicationUserDtoValidator : AbstractValidator<LoginApplicationUserDTO>
    {
        public LoginApplicationUserDtoValidator()
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
        }
    }
}
