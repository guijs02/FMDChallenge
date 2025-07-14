using FluentValidation;
using FMDApplication.Dtos.Lecture;
using FMDApplication.Dtos.Participant;
using FMDApplication.Validators;

namespace FMDApi.Build
{
    public static class Build
    {
        public static void AddValidators(this IServiceCollection services)
        {
            services.AddScoped<IValidator<CreateLectureInputDto>, CreateLectureDtoValidator>();
            services.AddScoped<IValidator<CreateParticipantInputDto>, CreateParticipantDtoValidator>();
            services.AddScoped<IValidator<UpdateParticipantInputDto>, UpdateParticipantDtoValidator>();
        }
        public static void AddSwaggerDocumentation(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

    }
}
