using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using StudentsApp.BLL;
using StudentsApp.Core;
using StudentsApp.Core.Services;
using StudentsApp.DAL;
using StudentsApp.API.Resources;
using StudentsApp.API.Validators;
using Swashbuckle.AspNetCore.Swagger;

namespace StudentsApp.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            
            services.AddDbContext<StudentsAppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default"),
                                                     x => x.MigrationsAssembly("StudentsApp.DAL")));
            
            services.AddTransient<IMarkService, MarkService>();
            services.AddTransient<IStudentService, StudentService>();

            services.AddScoped<AbstractValidator<SaveMarkResource>, SaveMarkResourceValidator>();
            services.AddScoped<AbstractValidator<SaveStudentResource>, SaveStudentResourceValidator>();
            
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Student Application", Version = "v.1.0" });
            });
            
            services.AddAutoMapper(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Student Application v.1.0");
            });
        }
    }
}