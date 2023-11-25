using AspNetCoreHero.ToastNotification;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using ourWinch.Models.Account;
using ourWinch.Services;




namespace ourWinch

{


    /// <summary>
    /// The main entry point for the ASP.NET Core application.
    /// This class configures services, the request pipeline, and starts the web application.
    /// </summary>
    public class Program
    {



        /// <summary>
        /// The Main method is the entry point of the application.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        public static void Main(string[] args)
        {

            


            var builder = WebApplication.CreateBuilder(args);

            

            // App Configuration
            string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string is missing.");
            }

            // Configure Identity with custom ApplicationUser and IdentityRole.
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            //builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();



            // Configure logging services.
            builder.Services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddConsole();
                loggingBuilder.AddDebug();
            });


            // Configure email sending service.
            builder.Services.AddTransient<IEmailSender, MailJetEmailSender>();

            // Set options for Identity.
            builder.Services.Configure<IdentityOptions>(opt =>
            {
                opt.Password.RequiredLength = 5;
                opt.Password.RequireLowercase = true;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(15);
                opt.Lockout.MaxFailedAccessAttempts = 6;

            });




            // Register controllers and views.
            builder.Services.AddControllers();
            builder.Services.AddControllersWithViews();

            // Register API documentation generator (Swagger).
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Configure notification service.
            builder.Services.AddNotyf(config =>
            {
                config.DurationInSeconds = 10;
                config.IsDismissable = true;
                config.Position = NotyfPosition.TopCenter;
            });

            // Configure the database context with SQL Server.
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(connectionString, sqlServerOptionsAction: sqlOptions =>
                {

                    sqlOptions.CommandTimeout(50);
                })
                .LogTo(Console.WriteLine);

            });

            // Register ServiceSkjemaService for DI.
            builder.Services.AddScoped<ServiceSkjemaService>();

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
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }


            // Middleware configurations for HTTPS, static files, routing, etc.
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();


            // Define the default route for the application.
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id?}");

            // Run the application.
            app.Run();
        }
    }
}
