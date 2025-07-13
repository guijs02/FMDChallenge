using FluentValidation;
using FMDApplication.Dtos;
using FMDApplication.Dtos.Lecture;
using FMDApplication.Dtos.Participant;
using FMDApplication.Services;
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

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddContext(builder.Configuration);

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddRepositories();

            builder.Services.AddScoped<ILectureService, LectureService>();
            builder.Services.AddScoped<IParticipantService, ParticipantService>();
            builder.Services.AddScoped<IValidator<CreateLectureInputDto>, CreateLectureDtoValidator>();
            builder.Services.AddScoped<IValidator<CreateParticipantInputDto>, CreateParticipantDtoValidator>();
            builder.Services.AddScoped<IValidator<UpdateParticipantInputDto>, UpdateParticipantDtoValidator>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
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
