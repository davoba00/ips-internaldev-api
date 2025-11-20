using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using NLog.Web;
using ODataResourceApi.Services.OData;
using RebuildProject.MediatR;
using RebuildProject.Middleware;
using RebuildProject.Models;
using RebuildProject.Service;
using static RebuildProject.Common.Constants;

namespace RebuildProject
{
    public static class HostingExtensions
    {
        #region Public Methods

        public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.AddLoggingSystem();

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddTransient(typeof(IRequestExceptionHandler<,,>), typeof(GlobalRequestExceptionHandler<,,>));

            builder.Services
                 .AddODataRoutes()
                 .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddTransient(typeof(IPipelineBehavior<,>),
                                          typeof(RequestCancellationBehavior<,>));

            builder.Services.Configure<DbLoggingSettings>(builder.Configuration.GetSection("DbLogging"));

            builder.Services.Scan(scan => scan
               .FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
                .AddClasses(classes => classes.Where(c => c.Name.EndsWith("Service")))
                .AsSelfWithInterfaces()
                .WithScopedLifetime());

            builder.Services.AddTransient<RequestMiddleware>();
            builder.Services.AddTransient<ApiLoggingMiddleware>();

            return builder;
        }

        public static WebApplication ConfigurePipeline(this WebApplicationBuilder builder)
        {
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();
            app.UseRequestMiddleware();
            app.UseApiLogging();
            app.UseAuthorization();

            app.MapControllers();

            return app;
        }

        #endregion

        #region Internal Methods

        internal static IServiceCollection AddODataRoutes(this IServiceCollection services)
        {
            services.AddControllers().AddOData(
               options =>
               {
                   options.AddRouteComponents(ApiRoutes.Default, ApiODataModelBuilder.GetEdmModel());
                   options.EnableQueryFeatures(100);
                   options.EnableContinueOnErrorHeader = true;
               }
           );

            return services;
        }

        internal static WebApplicationBuilder AddLoggingSystem(this WebApplicationBuilder builder)
        {
            // Add logging
            // - https://nlog-project.org/
            // - https://github.com/NLog/NLog.Web/tree/master/examples/ASP.NET.Core7
            builder.Logging.ClearProviders();
            builder.Host.UseNLog();

            return builder;
        }

        #endregion
    }
}
