using FluentValidation;
using LetterApp.Api.DTOs;

namespace LetterApp.Api.Validators
{
    public class UserDTOValidator:AbstractValidator<UserDTO>
    {
        public UserDTOValidator()
        {
            RuleFor(x => x.Username)
                .NotNull().NotEmpty().WithMessage("Kullanıcı Adı boş bırakılamaz");
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifre boş bırakılamaz")
                .MinimumLength(5).WithMessage("Şifre minimum 5 karakter olmalı");
            RuleFor(x => x.PasswordConfirmed).NotEmpty().WithMessage("Şifre Tekrar boş bırakılamaz")
           .Equal(x => x.Password).WithMessage("Şifreler uyuşmuyor");

        }
    }
}
