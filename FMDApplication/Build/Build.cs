using FMDApplication.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FMDApplication.Build
{
    public static class Build
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<ILectureService, LectureService>();
            services.AddScoped<IParticipantService, ParticipantService>();
        }
    }
}
