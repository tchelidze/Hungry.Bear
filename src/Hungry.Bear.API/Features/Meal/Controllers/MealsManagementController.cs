using AutoMapper;
using Hungry.Bear.API.Configuration.Auth;
using Hungry.Bear.API.Features.Meal.ApiInput;
using Hungry.Bear.API.Features.Meal.Dtos;
using Hungry.Bear.API.Features.Meal.Mapping;
using Hungry.Bear.API.Filters;
using Hungry.Bear.API.Shared;
using Hungry.Bear.Domain.Features.MealManagement.Commands.Delete;
using Hungry.Bear.Domain.Features.MealManagement.Commands.Edit;
using Hungry.Bear.Domain.Features.MealManagement.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hungry.Bear.API.Features.Meal.Controllers
{
    [Produces("application/json")]
    public class MealsManagementController : Controller
    {
        private readonly IMealRepository _mealRepository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public MealsManagementController(
            IMealRepository mealRepository,
            IMediator mediator,
            IMapper mapper)
        {
            _mealRepository = mealRepository;
            _mediator = mediator;
            _mapper = mapper;
        }

        [OpenIddictAuthorize(Policy = PolicyNames.Runs.Read)]
        [PreventNormalUserToAccessOthersData]
        public IQueryable<EatDto> Get(int key)
        {
            throw new NotImplementedException();
        }

        [OpenIddictAuthorize(Policy = PolicyNames.Runs.Read)]
        [PreventNormalUserToAccessOthersData]
        public EatDto Get(int key, int runId)
        {
            throw new NotImplementedException();
        }

        [OpenIddictAuthorize(Policy = PolicyNames.Runs.Create)]
        [PreventNormalUserToAccessOthersData]
        [ValidateModelState]
        public async Task<IActionResult> Post([Required] int key, [FromBody] EatApiInput input)
        {
            throw new NotImplementedException();
        }

        [OpenIddictAuthorize(Policy = PolicyNames.Runs.Update)]
        [PreventNormalUserToAccessOthersData]
        [ValidateModelState]
        public Task<IActionResult> Patch([Required] int key, int runId,
            [FromBody] PatchRunApiInput input)
        {
            return Update(_mapper.Map<EditMealCommand>(input,
                opts => opts.Items[RunMappingProfile.ItemKeys.RunId] = runId));
        }

        [NonAction]
        private async Task<IActionResult> Update(EditMealCommand command)
        {
            var updateJogResult = await _mediator.Send(command);

            if (updateJogResult.IsFailure())
            {
                return updateJogResult.ToActionResult();
            }

            throw new NotImplementedException();
        }

        [OpenIddictAuthorize(Policy = PolicyNames.Runs.Delete)]
        [PreventNormalUserToAccessOthersData]
        public async Task<IActionResult> Delete([Required] int key, int runId)
        {
            var deleteJogResult = await _mediator.Send(new DeleteMealCommand { MealId = runId });

            if (deleteJogResult.IsFailure())
            {
                return deleteJogResult.ToActionResult();
            }

            return NoContent();
        }
    }
}