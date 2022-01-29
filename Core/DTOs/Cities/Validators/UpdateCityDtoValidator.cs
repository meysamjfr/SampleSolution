using FluentValidation;

namespace Project.DTOs.Cities.Validators
{
    public class UpdateCityDtoValidator : AbstractValidator<CityDTO>
    {
        public UpdateCityDtoValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} اجباری است")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} باید حداکثر {ComparisonValue} کاراکتر باشد.");

            RuleFor(p => p.Id).NotNull().WithMessage("{PropertyName} اجباری است");
        }
    }
}
