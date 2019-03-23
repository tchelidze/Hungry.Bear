using AspNet.Security.OpenIdConnect.Primitives;
using Hungry.Bear.DataAccess;
using Hungry.Bear.Domain.Features.UserManagement.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using OpenIddict.Abstractions;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Hungry.Bear.API.Configuration.Auth
{
    public static class OpenIdDictConfiguration
    {
        public static IServiceCollection AddOpenIdAuth(this IServiceCollection services, IConfiguration configuration, IHostingEnvironment environment)
        {
            var authOptionsSection = configuration.GetSection(nameof(AuthOptions));
            services.Configure<AuthOptions>(authOptionsSection);
            var authOptions = authOptionsSection.Get<AuthOptions>();

            services
                .AddIdentity<HungryBearUser, HungryBearUserRole>()
                .AddEntityFrameworkStores<HungryBearIdentityDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.ClaimsIdentity.UserNameClaimType = OpenIdConnectConstants.Claims.Name;
                options.ClaimsIdentity.UserIdClaimType = OpenIdConnectConstants.Claims.Subject;
                options.ClaimsIdentity.RoleClaimType = OpenIdConnectConstants.Claims.Role;
            });

            services
                .AddOpenIddict()
                .AddCore(options =>
                {
                    options
                        .UseEntityFrameworkCore()
                        .UseDbContext<HungryBearIdentityDbContext>();
                })
                .AddServer(options =>
                {
                    options.UseMvc();
                    options
                        .EnableTokenEndpoint("/connect/token");
                    options
                        .AllowPasswordFlow()
                        .AllowRefreshTokenFlow();

                    options.SetAccessTokenLifetime(TimeSpan.FromMinutes(authOptions.AccessTokenLifetime));

                    options.RegisterScopes(
                        OpenIdConnectConstants.Scopes.Email,
                        OpenIdConnectConstants.Scopes.Profile,
                        OpenIddictConstants.Scopes.Roles);

                    if (!authOptions.RequireHttpsMetadata)
                    {
                        options.DisableHttpsRequirement();
                    }

                    options.UseJsonWebTokens();
                    options.AddSigningKey(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authOptions.SigningKey)));
                });

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();

            services
                .AddAuthentication()
                .AddJwtBearer(options =>
                {
                    options.Authority = authOptions.Authority;
                    options.Audience = authOptions.Audience;
                    options.RequireHttpsMetadata = authOptions.RequireHttpsMetadata;

                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authOptions.SigningKey)),
                        NameClaimType = OpenIdConnectConstants.Claims.Subject,
                        RoleClaimType = OpenIdConnectConstants.Claims.Role
                    };
                });

            services.AddPolicies();

            return services;
        }

    }
}