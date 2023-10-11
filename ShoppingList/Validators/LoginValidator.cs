using FluentValidation;
using ShoppingList.ViewModels;

namespace ShoppingList.Validators
{
    public class LoginValidator : AbstractValidator<LoginViewModel>
    {
        public LoginValidator() 
        {
            RuleFor(a => a.Email)
                    .NotEmpty()
                    .WithMessage("Email alanı boş bırakılamaz.")
                    .EmailAddress()
                    .WithMessage("Geçerli bir email adresi giriniz.");

            RuleFor(a => a.Password)
                .NotEmpty()
                .WithMessage("Şifre alanı boş bırakılamaz.")
                .MinimumLength(8)
                .WithMessage("Şifreniz en az 8 karakter uzunluğunda olmalıdır.")
                .Matches(".*[A-Z].*")
                .WithMessage("Şifreniz en az bir büyük harf içermelidir.")
                .Matches(".*[a-z].*")
                .WithMessage("Şifreniz en az bir küçük harf içermelidir.")
                .Matches(".*[0-9].*")
                .WithMessage("Şifreniz en az bir rakam içermelidir.");
        }
    }
}
