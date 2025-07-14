using FluentValidation;
using FMDApplication.Dtos.Lecture;

namespace FMDApplication.Validators;
public class CreateLectureDtoValidator : AbstractValidator<CreateLectureInputDto>
{
    public CreateLectureDtoValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(30);
        RuleFor(x => x.Description).NotEmpty().MaximumLength(100);
        RuleFor(x => x.DateTime).NotEmpty();
        RuleFor(x => x.Participants).NotNull();
        RuleForEach(x => x.Participants).SetValidator(new CreateParticipantLectureDtoValidator());
    }
}