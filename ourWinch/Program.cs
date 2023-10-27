using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;
using Microsoft.AspNetCore.Authentication.Cookies; // Gerekli using direktifi
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
<<<<<<< HEAD
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
=======

using Microsoft.AspNetCore.Identity;

>>>>>>> 3b38bed9ac7cc7b38e03067d988e7f82654a7da6

namespace ourWinch
{
    public class Program
    {
        public static void Main(string[] args)
        {
<<<<<<< HEAD
            CreateHostBuilder(args).Build().Run();
=======
            var builder = WebApplication.CreateBuilder(args);

            // App Configuration
            string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string is missing.");
            }

            builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>();

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddControllersWithViews();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Add the DbContext to the DI container.
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));

            // Set the server to listen on 0.0.0.0:5002
            builder.WebHost.ConfigureKestrel(serverOptions =>
            {
                serverOptions.ListenAnyIP(5002);
                Console.WriteLine("Kestrel is now configured to listen on port 5002 for any IP address.");
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API Name v1"));
                app.UseDeveloperExceptionPage();  // Bu satırı ekledik.
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id?}");

            app.Run();
>>>>>>> 3b38bed9ac7cc7b38e03067d988e7f82654a7da6
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureAppConfiguration((hostingContext, config) =>
                    {
                        config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                    });
                    webBuilder.UseStartup<Startup>();
                });
    }
}

public class Startup
{
    private IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        // Kimlik doğrulama ve diğer servis eklemeleri
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/Account/Login"; // Giriş yapılacak sayfanın yolu
                options.LogoutPath = "/Account/Logout"; // Çıkış yapılacak sayfanın yolu
            });

        // Diğer servislerin eklenmesi
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

        services.AddControllers();
        services.AddControllersWithViews();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    public void Configure(IApplicationBuilder app)
    {
        // Middleware ve diğer konfigürasyonlar burada tanımlanır
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();
        app.UseAuthentication();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id?}");
        });
    }
}
