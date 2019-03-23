using System.Threading.Tasks;

namespace Hungry.Bear.Domain.Shared.Specifications
{
    public static class SpecificationExtensions
    {
        public static async Task<bool> IsNotSatisfiedByAsync<TInput>(this ISpecification<TInput> specification,
            TInput input)
        {
            return !await specification.IsSatisfiedByAsync(input);
        }
    }
}