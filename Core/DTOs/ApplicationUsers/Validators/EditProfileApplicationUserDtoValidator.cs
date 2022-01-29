using FluentValidation;
using Project.Helpers;

namespace Project.DTOs.ApplicationUsers.Validators
{
    public class EditProfileApplicationUserDtoValidator : AbstractValidator<EditProfileApplicationUserDTO>
    {
        public EditProfileApplicationUserDtoValidator()
        {
            RuleFor(p => p.FirstName)
                .NotEmpty().WithMessage("{PropertyName} اجباری است")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} باید حداکثر {ComparisonValue} کاراکتر باشد.")
                .WithName(x => x.FirstName.GetDisplayName());

            RuleFor(p => p.LastName)
                .NotEmpty().WithMessage("{PropertyName} اجباری است")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} باید حداکثر {ComparisonValue} کاراکتر باشد.")
                .WithName(x => x.LastName.GetDisplayName());

            RuleFor(p => p.LastName)
                .NotEmpty().WithMessage("{PropertyName} اجباری است")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} باید حداکثر {ComparisonValue} کاراکتر باشد.")
                .WithName(x => x.LastName.GetDisplayName());
        }
    }
}
