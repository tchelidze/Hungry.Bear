using AutoMapper;
using Hungry.Bear.API.Features.Meal.ApiInput;
using Hungry.Bear.Domain.Features.MealManagement.Commands.Eat;

namespace Hungry.Bear.API.Features.Meal.Mapping
{
    public class RunMappingProfile : Profile
    {
        public static class ItemKeys
        {
            public const string OwnerId = nameof(OwnerId);

            public const string RunId = nameof(RunId);
        }

        public RunMappingProfile()
        {
            var createJogApiInputCreateJogCommandMap = CreateMap<EatApiInput, EatCommand>();
            createJogApiInputCreateJogCommandMap.ForMember(it => it.AuthorId,
                opts => opts.ResolveUsing(
                    (jog, command, arg3, context) => (int)context.Options.Items[ItemKeys.OwnerId]));
        }
    }
}