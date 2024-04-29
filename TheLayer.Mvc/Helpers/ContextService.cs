using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TheLayer.Core.Models.Identities;
using TheLayer.InfraStructure.Context;

namespace TheLayer.Mvc.Helpers
{
    public static class ContextService
    {
        public static IServiceCollection AddContextServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TheLayerContext>(x =>
            {
                x.UseSqlServer(configuration.GetConnectionString("TheLayerContextConnection"));
            });

            services.AddIdentity<Consumer, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddSignInManager<SignInManager<Consumer>>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<TheLayerContext>()
                .AddTokenProvider<DataProtectorTokenProvider<Consumer>>(TokenOptions.DefaultProvider);


            services.AddIdentityCore<Admin>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddSignInManager<SignInManager<Admin>>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<TheLayerContext>()
                .AddTokenProvider<DataProtectorTokenProvider<Admin>>(TokenOptions.DefaultProvider);

            services.AddIdentityCore<Student>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddSignInManager<SignInManager<Student>>()
                .AddRoles<IdentityRole>()
                .AddTokenProvider<DataProtectorTokenProvider<Student>>(TokenOptions.DefaultProvider)
                .AddEntityFrameworkStores<TheLayerContext>();

            services.AddIdentityCore<Teacher>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddSignInManager<SignInManager<Teacher>>()
                .AddRoles<IdentityRole>()
                .AddTokenProvider<DataProtectorTokenProvider<Teacher>>(TokenOptions.DefaultProvider)
                .AddEntityFrameworkStores<TheLayerContext>();

            return services;
        }
    }
}
