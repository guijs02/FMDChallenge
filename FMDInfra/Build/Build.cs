using FMDCore.Interfaces;
using FMDInfra.Data;
using FMDInfra.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMDInfra.Build
{
    public static class Build
    {
        public static void AddContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ILectureRepository, LectureRepository>();
            services.AddScoped<IParticipantRepository, ParticipantRepository>();
        }
    }
}
