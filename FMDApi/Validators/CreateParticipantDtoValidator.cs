using FluentValidation;
using FMDApplication.Dtos;
using FMDApplication.Dtos.Participant;

namespace FMDApplication.Validators;
public class CreateParticipantDtoValidator : AbstractValidator<CreateParticipantInputDto>
{
    public CreateParticipantDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Phone).NotEmpty().MaximumLength(20);
        RuleFor(x => x.LectureId).NotEmpty().WithMessage("LectureId is required.");
    }
}
public class UpdateParticipantDtoValidator : AbstractValidator<UpdateParticipantInputDto>
{
    public UpdateParticipantDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Phone).NotEmpty().MaximumLength(20);
        RuleFor(x => x.LectureId).NotEmpty().WithMessage("LectureId is required.");
    }
}