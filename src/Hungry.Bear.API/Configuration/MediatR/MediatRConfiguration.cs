using Hungry.Bear.Common.ExecutionResult;
using Hungry.Bear.Domain.Features.MealManagement.Commands.Delete;
using Hungry.Bear.Domain.Features.MealManagement.Commands.Eat;
using Hungry.Bear.Domain.Features.MealManagement.Commands.Edit;
using Hungry.Bear.Domain.Features.UserManagement.Commands.Create;
using Hungry.Bear.Domain.Features.UserManagement.Commands.Delete;
using Hungry.Bear.Domain.Features.UserManagement.Commands.Specifications;
using Hungry.Bear.Domain.Features.UserManagement.Commands.Update;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Hungry.Bear.API.Configuration.MediatR
{
    public static class MediatRConfiguration
    {
        public static IServiceCollection AddConfiguredMediatR(this IServiceCollection services)
        {
            /*NOTICE: To avoid automatic from assembly registration, pass empty assemblies list.*/
            services.AddMediatR(new Assembly[0]);

            services.AddTransient<IPipelineBehavior<EatCommand, ExecutionResult<int>>, CreateRunCommandValidator>();
            services.AddTransient<IRequestHandler<EatCommand, ExecutionResult<int>>, EatCommandHandler>();

            services.AddTransient<IPipelineBehavior<EditMealCommand, ExecutionResult>, EditMealCommandValidator>();
            services.AddTransient<IRequestHandler<EditMealCommand, ExecutionResult>, EditMealCommandHandler>();

            services.AddTransient<IPipelineBehavior<DeleteMealCommand, ExecutionResult>, DeleteMealCommandValidator>();
            services.AddTransient<IRequestHandler<DeleteMealCommand, ExecutionResult>, DeleteMealCommandHandler>();

            services.AddSingleton<UserHasPermissionToGrantRoleSpecification>();
            services.AddScoped<UserHasPermissionToUpdateSpecification>();

            services
                .AddTransient<IRequestHandler<CreateUserCommand, ExecutionResult<string>>, CreateUserCommandHandler>();

            services.AddTransient<IPipelineBehavior<UpdateUserCommand, ExecutionResult>, UpdateUserCommandValidator>();
            services.AddTransient<IRequestHandler<UpdateUserCommand, ExecutionResult>, UpdateUserCommandHandler>();

            services.AddTransient<IPipelineBehavior<DeleteUserCommand, ExecutionResult>, DeleteUserCommandValidator>();
            services.AddTransient<IRequestHandler<DeleteUserCommand, ExecutionResult>, DeleteUserCommandHandler>();

            return services;
        }
    }
}