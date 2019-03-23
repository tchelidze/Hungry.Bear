using System.Collections.Generic;
using Hungry.Bear.Domain.Features.UserManagement.Entities;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Hungry.Bear.API.Configuration.Auth
{
    public static class PoliciesDefinition
    {
        public static void AddPolicies(this IServiceCollection services)
        {
            services
                .AddAuthorization(opts =>
                {
                    opts.AddPolicy(PolicyNames.Runs.Read, policyOptions =>
                    {
                        policyOptions.AddRequirements(new RolesAuthorizationRequirement(new List<string>
                        {
                            RoleNames.NormalUser,
                            RoleNames.Admin,
                        }));
                    });

                    opts.AddPolicy(PolicyNames.Runs.Create, policyOptions =>
                   {
                       policyOptions.AddRequirements(new RolesAuthorizationRequirement(new List<string>
                       {
                            RoleNames.NormalUser,
                            RoleNames.Admin
                       }));
                   });

                    opts.AddPolicy(PolicyNames.Runs.Update, policyOptions =>
                    {
                        policyOptions.AddRequirements(new RolesAuthorizationRequirement(new List<string>
                        {
                            RoleNames.NormalUser,
                            RoleNames.Admin
                        }));
                    });

                    opts.AddPolicy(PolicyNames.Runs.Delete, policyOptions =>
                    {
                        policyOptions.AddRequirements(new RolesAuthorizationRequirement(new List<string>
                        {
                            RoleNames.NormalUser,
                            RoleNames.Admin
                        }));
                    });

                    opts.AddPolicy(PolicyNames.Report.AverageSpeedAndDistance, policyOptions =>
                    {
                        policyOptions.AddRequirements(new RolesAuthorizationRequirement(new List<string>
                        {
                            RoleNames.NormalUser,
                            RoleNames.Admin
                        }));
                    });

                    opts.AddPolicy(PolicyNames.Users.ReadSingle, policyOptions =>
                    {
                        policyOptions.AddRequirements(new RolesAuthorizationRequirement(new List<string>
                        {
                            RoleNames.UserManager,
                            RoleNames.Admin,
                            RoleNames.NormalUser
                        }));
                    });

                    opts.AddPolicy(PolicyNames.Users.Read, policyOptions =>
                    {
                        policyOptions.AddRequirements(new RolesAuthorizationRequirement(new List<string>
                        {
                            RoleNames.UserManager,
                            RoleNames.Admin
                        }));
                    });

                    opts.AddPolicy(PolicyNames.Users.Update, policyOptions =>
                    {
                        policyOptions.AddRequirements(new RolesAuthorizationRequirement(new List<string>
                        {
                            RoleNames.UserManager,
                            RoleNames.Admin,
                            RoleNames.NormalUser,
                        }));
                    });

                    opts.AddPolicy(PolicyNames.Users.Delete, policyOptions =>
                    {
                        policyOptions.AddRequirements(new RolesAuthorizationRequirement(new List<string>
                        {
                            RoleNames.UserManager,
                            RoleNames.Admin
                        }));
                    });
                });
        }
    }
}
