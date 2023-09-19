using ourWinch.Services;

namespace ourWinch
{
    public class Startup
    {

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<UserService>();

            // ... other services ...
        }

    }
}
