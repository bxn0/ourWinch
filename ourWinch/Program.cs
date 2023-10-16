using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;
using Microsoft.AspNetCore.Builder;


namespace ourWinch
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // App Configuration
            string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string is missing.");
            }
            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddControllersWithViews();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Add the DbContext to the DI container.
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));

            // Set the server to listen on localhost:5002
            builder.WebHost.ConfigureKestrel(serverOptions =>
            {
                serverOptions.ListenAnyIP(5002);
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API Name v1"));
            }

            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id?}");

            app.Run();
        }
    }
}
