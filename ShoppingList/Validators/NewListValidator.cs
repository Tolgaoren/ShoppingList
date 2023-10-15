using FluentValidation;
using Shopping.Models;

namespace Shopping.Validators
{
    public class NewListValidator : AbstractValidator<ShoppingLists>
    {
        public NewListValidator()
        {
            RuleFor(a => a.UserId)
                .NotEmpty()
                .WithMessage("Oluşturulamadı.");


            RuleFor(a => a.ListName)
                .NotEmpty()
                .WithMessage("Listenizin adı olmak zorundadır.")
                .MinimumLength(1)
                .WithMessage("Listenizin adı olmak zorundadır.");

        }

    }
}
