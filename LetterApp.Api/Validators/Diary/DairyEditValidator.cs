using FluentValidation;
using LetterApp.Api.DTOs.Diary;

namespace LetterApp.Api.Validators.Diary
{
    public class DairyEditValidator:AbstractValidator<DiaryEditDTO>
    {
        public DairyEditValidator()
        {
            RuleFor(x=>x.Id).NotEmpty().WithMessage("Geçerli bir Id giriniz");
            RuleFor(x => x.Header).NotEmpty().NotNull().WithMessage($"Başlık boş bırakılamaz").MaximumLength(100).WithMessage("En fazla 100 Karakter Girilmeli");
            RuleFor(x => x.Description).MaximumLength(500).WithMessage("En fazla 500 karakter Girilmeli");
        }
    }
}
