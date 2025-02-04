using Saal.Restaurant.Application.Interfaces;
using Saal.Restaurant.Application;
using Saal.Restaurant.Infrastructure;
using Saal.Restaurant.Application.Validators;
using FluentValidation;

namespace Saal.Restaurant.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDatabaseServices(builder.Configuration, builder.Environment.IsDevelopment());

            builder.Services.AddScoped<IMenuService, MenuService>();
            builder.Services.AddScoped<ITableService, TableService>();
            builder.Services.AddScoped<IOrderService, OrderService>();
            builder.Services.AddScoped<IBillService, BillService>();

            builder.Services.AddValidatorsFromAssemblyContaining<OrderDtoValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<GenerateBillRequestValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<UpdateBillStatusRequestValidator>();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
            });

            var app = builder.Build();

            // Using In-Memory Database because F1 tier does not support database instance
            //if (app.Environment.IsDevelopment())
            //{
            using var scope = app.Services.CreateScope();
            var serviceProvider = scope.ServiceProvider;
            serviceProvider.SeedData();
            ///}

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}