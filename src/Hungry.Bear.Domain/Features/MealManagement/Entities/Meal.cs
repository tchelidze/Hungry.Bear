using System;
using System.Collections.Generic;
using Hungry.Bear.Domain.Features.UserManagement.Entities;
using Hungry.Bear.Domain.Shared.Entities;

namespace Hungry.Bear.Domain.Features.MealManagement.Entities
{
    public class Meal : IHasId<string>, IHasInsertionDate
    {
        public string Id { get; set; }

        public DateTime InsertionDate { get; set; }

        public HungryBearUser Author { get; set; }

        public int AuthorId { get; set; }

        public DateTime OccuredOn { get; set; }

        public virtual ICollection<Food> Foods { get; set; }
    }
}