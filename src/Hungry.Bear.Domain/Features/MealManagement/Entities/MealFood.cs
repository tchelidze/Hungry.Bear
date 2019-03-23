using Hungry.Bear.Domain.Shared.Entities;

namespace Hungry.Bear.Domain.Features.MealManagement.Entities
{
    public class MealFood : IHasId<string>
    {
        public string Id { get; set; }

        public decimal Amount { get; set; }

        public Food Food { get; set; }

        public string FoodId { get; set; }

        public Meal Meal { get; set; }

        public string MealId { get; set; }
    }
}