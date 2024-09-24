using back_end.Data;
using back_end.Repositories.MovementRepositories;
using back_end.Repositories.ProductRepositories;
using back_end.Repositories.UserRepositories;
using back_end.Services.MovementServices;
using back_end.Services.ProductServices;
using back_end.Services.UserServices;
using Microsoft.EntityFrameworkCore;

namespace back_end;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
            });

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DesafioConnectionString"), sqlOptions =>
            {
                sqlOptions.EnableRetryOnFailure();
            }));
        builder.Services.AddAutoMapper(typeof(Program));
        
        // Registro dos Repositories
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IProductRepository, ProductRepository>();
        builder.Services.AddScoped<IMovementRepository, MovementRepository>();
        
        // Registro dos Services
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IProductService, ProductService>();
        builder.Services.AddScoped<IMovementService, MovementService>();
        
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();

        app.MapControllers();
        
        app.Run();
    }
}