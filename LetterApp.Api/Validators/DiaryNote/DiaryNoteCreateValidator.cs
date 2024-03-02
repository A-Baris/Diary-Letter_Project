using FluentValidation;
using LetterApp.Api.DTOs.DiaryNote;

namespace LetterApp.Api.Validators.DiaryNote
{
    public class DiaryNoteCreateValidator:AbstractValidator<DiaryNoteCreateDTO>
    {
        public DiaryNoteCreateValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Başlık boş bırakılamaz");
            RuleFor(x => x.DiaryId).NotEmpty().WithMessage("Başlık boş bırakılamaz");
            RuleFor(x => x.Body).NotEmpty().WithMessage("Paragraf boş bırakılamaz")
                .MaximumLength(3000).WithMessage("Maksimum 3000 karakterden oluşmalıdır");
            RuleFor(x => x.ReceiverEmail).EmailAddress().WithMessage("Email : abc@abc şeklinde girilmelidir ");
                
        }

    }
}
