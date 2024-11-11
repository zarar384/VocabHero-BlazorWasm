using Carter;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Diagnostics;
using MudBlazor.Services;
using System.Reflection;
using VocabHero.Components.Account;
using VocabHero.Data;
using VocabHero.Extensions;
using Microsoft.EntityFrameworkCore;
using VocabHero.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using VocabHero.Data.Repositories;

namespace VocabHero
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCarter();
            //services.AddSqlServer<ApplicationDbContext>(configuration.GetConnectionString("DefaultConnection")!);

            return services;
        }

        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            // add interceptorsto the container.
            //services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();

            services.AddDbContext<ApplicationDbContext>((sp, options) =>
            {
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                options.UseSqlServer(connectionString);
            });

            //add services to the container.
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg =>
            {
                //automatically find and register all handlers for commands, queries from the current assembly
                //where the Program class is located. 
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                //cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
                //cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
            });

            return services;
        }

        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentityCore<User>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddSignInManager()
                .AddDefaultTokenProviders();

            services.AddScoped<IEmailSender<User>, IdentityNoOpEmailSender>();

            return services;
        }

        public static IServiceCollection AddAuthenticationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthorization();
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            })
            .AddJwtBearer(options=>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]!))
                };
            });

            return services;
        }

        public static IServiceCollection AddRazorAndMudServices(this IServiceCollection services)
        {
            services.AddRazorComponents()
                .AddInteractiveWebAssemblyComponents();

            services.AddMudServices();
            services.AddCascadingAuthenticationState();
            services.AddScoped<IdentityUserAccessor>();
            services.AddScoped<IdentityRedirectManager>();
            services.AddScoped<AuthenticationStateProvider, FakeAuthenticationStateProvider>();

            return services;
        }
    }
}