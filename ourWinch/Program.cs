using AspNetCoreHero.ToastNotification;
using Microsoft.AspNetCore;
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
            // Initialize a new WebApplication builder with the passed arguments.
            var builder = WebApplication.CreateBuilder(args);


            // Retrieve the connection string from the application's configuration
            // and ensure it is not null or empty.



            //removing headers


            builder.WebHost.ConfigureKestrel(serverOptions =>
            {
                serverOptions.AddServerHeader = false;
                
            });



            // App Configuration

            string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string is missing.");
            }


            // Set up the identity system for the application, adding default token providers.

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();


            // Configure logging to include console and debug output.

            //builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();



            // Configure logging services.

            builder.Services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddConsole();
                loggingBuilder.AddDebug();
            });

            // Register an email sender service as a transient dependency.
            builder.Services.AddTransient<IEmailSender, MailJetEmailSender>();

            // Configure identity options, such as password requirements and lockout settings.


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




            // Add essential MVC services to the application's service collection.
            builder.Services.AddControllers();
            builder.Services.AddControllersWithViews();

            // Enable API exploration and Swagger for API documentation.
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Configure toast notifications with specified duration, dismissibility, and position.

            builder.Services.AddNotyf(config =>
            {
                config.DurationInSeconds = 10;
                config.IsDismissable = true;
                config.Position = NotyfPosition.TopCenter;
            });


            // Add Entity Framework context for the application with specific options,
            // such as connection string and command timeout settings.

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(connectionString, sqlServerOptionsAction: sqlOptions =>
                {

                    sqlOptions.CommandTimeout(30); // Set the timeout period for database commands
                })
                .LogTo(Console.WriteLine); // Log EF Core operations to the console.

            });

            // Register a custom service as a scoped dependency for dependency injection.
            builder.Services.AddScoped<ServiceSkjemaService>();

            // Configure the Kestrel web server to listen on all network interfaces on port 5002.
            builder.WebHost.ConfigureKestrel(serverOptions =>
            {
                serverOptions.ListenAnyIP(5002);
                Console.WriteLine("Kestrel is now configured to listen on port 5002 for any IP address.");
            });

            // Build the web application using the configured services and middlewares.
            var app = builder.Build();


            // Configure the HTTP request pipeline based on whether the environment is development or production.



            //adding headers
            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("X-Xss-Protection", "1");
                context.Response.Headers.Add("X-Frame-Options", "DENY");
                context.Response.Headers.Add("Referrer-Policy", "no-referrer");
                context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
              
                await next();
            });
            

            // Configure the HTTP request pipeline.

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API Name v1"));

                app.UseDeveloperExceptionPage();  // Use the developer exception page for detailed error information.

            }
            else
            {
                app.UseExceptionHandler("/Home/Error"); // Use a generic error handler page.
                app.UseHsts(); // Use HTTP Strict Transport Security.
            }


            // Middlewares for handling HTTPS redirection, serving static files, routing, authentication, and authorization.

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
