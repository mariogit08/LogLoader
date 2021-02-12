using System;
using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using TReuters.LogLoader.Infra.IOC;

namespace TReuters.LogLoader.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler(config =>
            {
                config.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    var error = context.Features.Get<IExceptionHandlerFeature>();
                    if (error != null)
                    {
                        var ex = error.Error;
                        await context.Response.WriteAsync(new
                        {
                            StatusCode = StatusCodes.Status500InternalServerError,
                            ErrorMessage = ex.Message
                        }.ToString()); ; //ToString() is overridden to Serialize object
                    }
                    else
                    {
                        var ex = error.Error;
                        await context.Response.WriteAsync(new
                        {
                            StatusCode = StatusCodes.Status500InternalServerError,
                            ErrorMessage = "An application error has occurred, for more information contact the support team."
                        }.ToString()); ; //ToString() is overridden to Serialize object
                    }
                });
            });

            app.UseCors(builder =>
                    builder.WithOrigins("http://localhost:55957")
                           .AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod()
                    );

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            AddSwaggerUI(app);

        }

        private static void AddSwaggerUI(IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson();
            services.AddCors();
            ConfigSwaggerAPIInfo(services);

            IOCBootstrapper.RegisterAllIOCModules(services);
        }

        private static void ConfigSwaggerAPIInfo(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "LogLoader API",
                    Description = "A REST ASP.NET Core WebAPI exposing LogLoader Features",
                    TermsOfService = new Uri("https://github.com/mariogit08"),
                    Contact = new OpenApiContact
                    {
                        Name = "Mário Chaves",
                        Email = "mario.chaves@live.com",
                        Url = new Uri("https://www.linkedin.com/in/mariodeveloper/"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://github.com/mariogit08"),
                    }
                });
            });
        }
    }
}
