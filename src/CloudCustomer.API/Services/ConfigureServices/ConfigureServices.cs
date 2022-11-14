using CloudCustomer.API.Config;

namespace CloudCustomer.API.Services.ConfigureServices
{
    public class ConfigureServices
    {
        public static void AddServices(IServiceCollection service, IConfiguration configuration)
        {
            service.Configure<UsersApiOptions>(configuration.GetSection("UsersApiOptions"));
            service.AddTransient<IUserService, UserService>();
            service.AddHttpClient<IUserService, UserService>();
        }
    }
}
