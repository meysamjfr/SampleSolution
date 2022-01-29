using FluentValidation;

namespace Project.DTOs.Cities.Validators
{
    public class CreateCityDtoValidator : AbstractValidator<CityDTO>
    {
        public CreateCityDtoValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("{PropertyName} اجباری است")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} باید حداکثر {ComparisonValue} کاراکتر باشد.");
        }
    }
}
