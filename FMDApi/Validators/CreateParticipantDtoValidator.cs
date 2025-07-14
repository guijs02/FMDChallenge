using FluentValidation;
using FMDApi.Validators;
using FMDApplication.Dtos;
using FMDApplication.Dtos.Lecture;
using FMDApplication.Dtos.Participant;

namespace FMDApplication.Validators;
public class CreateParticipantDtoValidator : AbstractValidator<CreateParticipantInputDto>
{
    public CreateParticipantDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().NotNull().MaximumLength(40);
        RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(60);
        RuleFor(x => x.Phone).ValidPhone();
        RuleFor(x => x.LectureId).NotEmpty().WithMessage("LectureId is required.");
    }
}
public class CreateParticipantLectureDtoValidator : AbstractValidator<CreateParticipantLectureInputDto>
{
    public CreateParticipantLectureDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().NotNull().MaximumLength(40);
        RuleFor(x => x.Email).NotEmpty().NotNull().EmailAddress().MaximumLength(60);
        RuleFor(x => x.Phone).ValidPhone();
    }
}
public class UpdateParticipantDtoValidator : AbstractValidator<UpdateParticipantInputDto>
{
    public UpdateParticipantDtoValidator()
    {
        RuleFor(x => x.Name).NotEmpty().NotNull().MaximumLength(40);
        RuleFor(x => x.Email).NotEmpty().NotNull().EmailAddress().MaximumLength(60);
        RuleFor(x => x.Phone).ValidPhone();
        RuleFor(x => x.LectureId).NotEmpty().WithMessage("LectureId is required.");
    }
}