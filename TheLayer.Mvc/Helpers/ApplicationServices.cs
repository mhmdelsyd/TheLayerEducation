using Microsoft.AspNetCore.Identity.UI.Services;
using TheLayer.Core.Helpers.Classes;

namespace TheLayer.Mvc.Helpers
{
    public static class ApplicationServices
    {
        public static IServiceCollection AddApplicationDI(this IServiceCollection services)
        {
            services.AddScoped<IEmailSender, EmailSender>();
            return services;
        }
    }
}
