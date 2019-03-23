using AutoMapper;
using Hungry.Bear.API.Configuration.Auth;
using Hungry.Bear.API.Features.UserManagement.ApiInput;
using Hungry.Bear.API.Features.UserManagement.Dtos;
using Hungry.Bear.API.Filters;
using Hungry.Bear.API.Shared;
using Hungry.Bear.Domain.Features.MealManagement.Repositories;
using Hungry.Bear.Domain.Features.UserManagement.Commands.Create;
using Hungry.Bear.Domain.Features.UserManagement.Commands.Delete;
using Hungry.Bear.Domain.Features.UserManagement.Commands.Update;
using Hungry.Bear.Domain.Features.UserManagement.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Hungry.Bear.API.Features.UserManagement.Controllers
{
    [Produces("application/json")]
    public class UsersController : Controller
    {
        private readonly IMealRepository _mealRepository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly UserManager<HungryBearUser> _userManager;

        public UsersController(
            IMealRepository mealRepository,
            IMediator mediator,
            IMapper mapper,
            UserManager<HungryBearUser> userManager)
        {
            _mealRepository = mealRepository;
            _mediator = mediator;
            _mapper = mapper;
            _userManager = userManager;
        }

        [OpenIddictAuthorize(Policy = PolicyNames.Users.Read)]
        public Task<List<HungryBearUser>> Get()
        {
            throw new NotImplementedException();
        }

        [OpenIddictAuthorize(Policy = PolicyNames.Users.ReadSingle)]
        [PreventNormalUserToAccessOthersData]
        public UserDto Get(string id)
        {
            throw new NotImplementedException();
        }

        [AllowAnonymous]
        [ValidateModelState]
        public async Task<IActionResult> Post([FromBody] CreateUserApiInput input)
        {
            var createUserResult = await _mediator.Send(_mapper.Map<CreateUserCommand>(input));

            if (createUserResult.IsFailure())
            {
                return createUserResult.ToActionResult();
            }

            var createdUser = await _userManager.FindByIdAsync(createUserResult.Value);
            return Json(_mapper.Map<UserDto>(createdUser));
        }

        [OpenIddictAuthorize(Policy = PolicyNames.Users.Update)]
        [PreventNormalUserToAccessOthersData]
        [ValidateModelState]
        public Task<IActionResult> Put([Required] string id, [FromBody] PutUserApiInput input)
        {
            throw new NotImplementedException();
        }

        [OpenIddictAuthorize(Policy = PolicyNames.Users.Update)]
        [PreventNormalUserToAccessOthersData]
        [ValidateModelState]
        public Task<IActionResult> Patch([Required] int id, [FromBody] PatchUserApiInput input)
        {
            throw new NotImplementedException();
        }

        private async Task<IActionResult> Update(UpdateUserCommand command)
        {
            var updateUserResult = await _mediator.Send(command);

            if (updateUserResult.IsFailure())
            {
                return updateUserResult.ToActionResult();
            }

            throw new NotImplementedException();
        }


        [OpenIddictAuthorize(Policy = PolicyNames.Users.Delete)]
        [PreventNormalUserToAccessOthersData]
        public async Task<IActionResult> Delete([Required] string id)
        {
            var deleteUserResult = await _mediator.Send(new DeleteUserCommand
            {
                Id = id,
                CurrentUserId = User.GetId()
            });

            return deleteUserResult.ToActionResult();
        }
    }
}