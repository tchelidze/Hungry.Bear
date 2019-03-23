namespace Hungry.Bear.Domain.Shared.Entities
{
    public interface IHasId<T>
    {
        T Id { get; set; }
    }
}