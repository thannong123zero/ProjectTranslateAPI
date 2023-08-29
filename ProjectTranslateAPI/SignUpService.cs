using Microsoft.Extensions.Configuration;
using ProjectTranslateAPI.Model;

namespace ProjectTranslateAPI
{
    public static class SignUpService
    {
        public static IServiceCollection WebbAppSignUpService(this IServiceCollection services)
        {
           // services.AddSingleton<WebAppAPIConfiguration>();
            services.AddSingleton<GoogleTranslateHelper>();
            return services;
        }
    }
}
