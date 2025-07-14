using FluentValidation;
using FMDApi.Build;
using FMDApplication.Build;
using FMDApplication.Dtos;
using FMDApplication.Dtos.Lecture;
using FMDApplication.Dtos.Participant;
using FMDApplication.Services;
using FMDApplication.Services.Interfaces;
using FMDApplication.Validators;
using FMDCore.Interfaces;
using FMDInfra.Build;
using FMDInfra.Data;
using FMDInfra.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FMDApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddContext(builder.Configuration);

            builder.Services.AddSwaggerDocumentation();
            builder.Services.AddRepositories();
            builder.Services.AddServices(builder.Configuration);
            builder.Services.AddValidators();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseMiddleware<ExceptionMiddleware>();
            app.MapControllers();

            app.Run();
        }
    }
}
