using Discount.Grpc.Mapper;
using Discount.Grpc.Repositories;
using Discount.Grpc.Services;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();

        builder.Services.AddControllers();

        // Registering AutoMapper object with DiscountProfile class
        builder.Services.AddAutoMapper(typeof(DiscountProfile));

        // Add services to the container.
        builder.Services.AddGrpc();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();
        _ = app.UseEndpoints(endpoints =>
        {
            _ = endpoints.MapGrpcService<DiscountService>();
            _ = endpoints.MapControllers();
            _ = endpoints.MapGet("/", async context =>
            {
                await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
            });

        });

        // Following will be used for migration database. It will drop and create the coupon table if exist and insert pre-defined values
        // Commenting following as it will be called each time when Discount.API project is executed
        // app.MigrateDatabase<Program>();

        app.Run();
    }
}