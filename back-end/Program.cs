using back_end.Data;
using back_end.Repositories;
using back_end.Repositories.UserRepositories;
using back_end.Services;
using back_end.Services.UserServices;
using Microsoft.EntityFrameworkCore;

namespace back_end;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();

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
        
        // Registro dos Services
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IProductService, ProductService>();
        
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