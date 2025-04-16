using Discount.API.Services;
using Discount.Application.Handlers;
using Discount.Core.Repositories;
using Discount.Infrastructure.Extensions;
using Discount.Infrastructure.Repositories;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Discount.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Register AutoMapper
            builder.Services.AddAutoMapper(typeof(Program).Assembly);

            //Register Mediatr
            var assemblies = new Assembly[]
            {
                Assembly.GetExecutingAssembly(),
                typeof(CreateDiscountCommandHandler).Assembly
            };

            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));
            builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();
            builder.Services.AddGrpc();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            _ = app.UseEndpoints(endpoints =>
            {
                _ = endpoints.MapGrpcService<DiscountService>();
                _ = endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with grpc endpoints must be made through a grpc client");
                });
            });

            // Following will be used for migration database. It will drop and create the coupon table if exist and insert pre-defined values
            // Commenting following as it will be called each time when Discount.API project is executed
            // app.MigrateDatabase<Program>();

            app.Run();
        }
    }
}