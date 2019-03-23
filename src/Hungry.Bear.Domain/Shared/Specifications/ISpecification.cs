using System.Threading.Tasks;

namespace Hungry.Bear.Domain.Shared.Specifications
{
    public interface ISpecification<in TInput>
    {
        Task<bool> IsSatisfiedByAsync(TInput input);
    }
}
