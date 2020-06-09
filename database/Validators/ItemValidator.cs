using FluentValidation;
using Store.Database.Models;

namespace Store.Database.Validators
{
    public class ItemValidator : AbstractValidator<Item>
    {
        public ItemValidator()
        {
            RuleFor(item => item.Name).NotNull().MinimumLength(3).MaximumLength(16).WithMessage("Name property has invalid length!").WithErrorCode("101");

            RuleFor(item => item.Amount).NotNull().GreaterThanOrEqualTo(0).WithMessage("Amount propety has invalid value!").WithErrorCode("102");

            RuleFor(item => item.Price).NotNull().GreaterThanOrEqualTo(0).WithMessage("Price property has invalid value!").WithErrorCode("103");
        }
    }
}