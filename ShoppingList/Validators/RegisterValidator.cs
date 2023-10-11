using FluentValidation;
using ShoppingList.ViewModels;

namespace ShoppingList.Validators
{
    public class RegisterValidator : AbstractValidator<RegisterViewModel>
    {
        public RegisterValidator() 
        {
            RuleFor(a => a.Email)
                .NotEmpty()
                .WithMessage("Email alanı boş bırakılamaz.")
                .EmailAddress()
                .WithMessage("Geçerli bir email adresi giriniz.");

            RuleFor(a => a.FirstName)
                .NotEmpty()
                .WithMessage("İsim alanı boş bırakılamaz.")
                .Length(2, 50)
                .WithMessage("İsim alanı en az 2 karakter içermelidir.");

            RuleFor(a => a.LastName)
                .NotEmpty()
                .WithMessage("Soyisim alanı boş bırakılamaz.")
                .Length(2, 50)
                .WithMessage("Soyisim alanı en az 2 karakter içermelidir.");

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

            RuleFor(a => a.PasswordConfirm)
                .NotEmpty()
                .WithMessage("Şifre tekrarı alanı boş bırakılamaz.")
                .Equal(x => x.Password)
                .WithMessage("Şifre tekrarı aynı olmalıdır.");
        }

    }
}
