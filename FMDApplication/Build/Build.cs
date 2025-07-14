using FMDApplication.Services;
using FMDApplication.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FMDApplication.Build
{
    public static class Build
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ILectureService, LectureService>();
            services.AddScoped<IParticipantService, ParticipantService>();

            var (name, baseUrl) = GetConfiguration(configuration);

            services.AddScoped<ITriviaService, TriviaService>()
                .AddHttpClient(name,
                    c => c.BaseAddress = new Uri(baseUrl));
        }

        private static (string name, string baseUrl) GetConfiguration(IConfiguration configuration)
        {
            var triviaConfig = configuration.GetSection("Trivia");

            if (triviaConfig == null)
            {
                throw new ArgumentNullException("Trivia configuration section is missing.");
            }
            var name = triviaConfig["Name"];
            var baseUrl = triviaConfig["BaseUrl"];

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(baseUrl))
            {
                throw new ArgumentException("Trivia configuration is incomplete.");
            }

            return (name, baseUrl);
        }
    }
}
