using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using test_bp.Infrastructure.Security.Interfaces;
using test_bp.Infrastructure.Security.Settings;
using System.Text;

namespace test_bp.Infrastructure.Security.Extensions
{
    public static class SecurityExtensions
    {
        public static IServiceCollection AddBpSecurity(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
            

            services.AddSingleton<IPasswordHasher, BcryptPasswordHasher>();
            services.AddSingleton<IJwtProvider, JwtProvider>();

            var jwtSettings = configuration.GetSection(JwtSettings.SectionName).Get<JwtSettings>();

            if (string.IsNullOrEmpty(jwtSettings?.KeyToken))
                throw new InvalidOperationException("JWT KeyToken no está configurado.");

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.KeyToken)),
                        ClockSkew = TimeSpan.Zero 
                    };
                    
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_token"];
                            var path = context.HttpContext.Request.Path;
                            
                            return Task.CompletedTask;
                        }
                    };
                });

            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new Microsoft.AspNetCore.Authorization.AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .RequireRole("admin_platform", "register_transaction", "ClientAdminPolicy")
                    .Build();

                options.AddPolicy("AdminPolicy", policy =>
                    policy.RequireRole("admin_platform"));

                options.AddPolicy("RegisterTransactionPolicy", policy =>
                    policy.RequireRole("register_transaction"));

                options.AddPolicy("ClientAdminPolicy", policy => policy.RequireRole("client_admin"));
            });

            return services;
        }
    }
}
