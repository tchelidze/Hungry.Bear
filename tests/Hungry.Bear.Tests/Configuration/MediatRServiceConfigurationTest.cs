using Hungry.Bear.API.Configuration.MediatR;
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
using Moq;
using Shouldly;
using Xunit;

namespace Hungry.Bear.Tests.Configuration
{
    public class MediatRServiceConfigurationTest
    {
        private readonly Mock<ServiceCollection> _serviceCollection = new Mock<ServiceCollection>();

        [Fact]
        public void should_configure_specifications()
        {
            _serviceCollection.Object.AddConfiguredMediatR();

            _serviceCollection.Object.ShouldContain(it => it.ImplementationType == typeof(UserHasPermissionToGrantRoleSpecification) && it.Lifetime == ServiceLifetime.Singleton);
            _serviceCollection.Object.ShouldContain(it => it.ImplementationType == typeof(UserHasPermissionToUpdateSpecification) && it.Lifetime == ServiceLifetime.Scoped);
        }

        [Fact]
        public void should_configure_create_run_command_handling_pipeline()
        {
            _serviceCollection.Object.AddConfiguredMediatR();

            _serviceCollection.Object.ShouldContain(it => it.ServiceType == typeof(IPipelineBehavior<EatCommand, ExecutionResult<int>>)
                                                          && it.ImplementationType == typeof(CreateRunCommandValidator)
                                                          && it.Lifetime == ServiceLifetime.Transient);

            _serviceCollection.Object.ShouldContain(it => it.ServiceType == typeof(IRequestHandler<EatCommand, ExecutionResult<int>>)
                                                          && it.ImplementationType == typeof(EatCommandHandler)
                                                          && it.Lifetime == ServiceLifetime.Transient);
        }

        [Fact]
        public void should_configure_update_run_command_handling_pipeline()
        {
            _serviceCollection.Object.AddConfiguredMediatR();

            _serviceCollection.Object.ShouldContain(it => it.ServiceType == typeof(IPipelineBehavior<EditMealCommand, ExecutionResult>)
                                                          && it.ImplementationType == typeof(EditMealCommandValidator)
                                                          && it.Lifetime == ServiceLifetime.Transient);

            _serviceCollection.Object.ShouldContain(it => it.ServiceType == typeof(IRequestHandler<EditMealCommand, ExecutionResult>)
                                                          && it.ImplementationType == typeof(EditMealCommandHandler)
                                                          && it.Lifetime == ServiceLifetime.Transient);
        }

        [Fact]
        public void should_configure_delete_run_command_handling_pipeline()
        {
            _serviceCollection.Object.AddConfiguredMediatR();

            _serviceCollection.Object.ShouldContain(it => it.ServiceType == typeof(IPipelineBehavior<DeleteMealCommand, ExecutionResult>)
                                                          && it.ImplementationType == typeof(DeleteMealCommandValidator)
                                                          && it.Lifetime == ServiceLifetime.Transient);

            _serviceCollection.Object.ShouldContain(it => it.ServiceType == typeof(IRequestHandler<DeleteMealCommand, ExecutionResult>)
                                                          && it.ImplementationType == typeof(DeleteMealCommandHandler)
                                                          && it.Lifetime == ServiceLifetime.Transient);
        }

        [Fact]
        public void should_configure_create_user_command_handling_pipeline()
        {
            _serviceCollection.Object.AddConfiguredMediatR();

            _serviceCollection.Object.ShouldContain(it => it.ServiceType == typeof(IRequestHandler<CreateUserCommand, ExecutionResult<string>>)
                                                          && it.ImplementationType == typeof(CreateUserCommandHandler)
                                                          && it.Lifetime == ServiceLifetime.Transient);
        }

        [Fact]
        public void should_configure_update_user_command_handling_pipeline()
        {
            _serviceCollection.Object.AddConfiguredMediatR();

            _serviceCollection.Object.ShouldContain(it => it.ServiceType == typeof(IPipelineBehavior<UpdateUserCommand, ExecutionResult>)
                                                          && it.ImplementationType == typeof(UpdateUserCommandValidator)
                                                          && it.Lifetime == ServiceLifetime.Transient);

            _serviceCollection.Object.ShouldContain(it => it.ServiceType == typeof(IRequestHandler<UpdateUserCommand, ExecutionResult>)
                                                          && it.ImplementationType == typeof(UpdateUserCommandHandler)
                                                          && it.Lifetime == ServiceLifetime.Transient);
        }

        [Fact]
        public void should_configure_delete_user_command_handling_pipeline()
        {
            _serviceCollection.Object.AddConfiguredMediatR();

            _serviceCollection.Object.ShouldContain(it => it.ServiceType == typeof(IRequestHandler<DeleteUserCommand, ExecutionResult>)
                                                          && it.ImplementationType == typeof(DeleteUserCommandHandler)
                                                          && it.Lifetime == ServiceLifetime.Transient);
        }
    }
}
