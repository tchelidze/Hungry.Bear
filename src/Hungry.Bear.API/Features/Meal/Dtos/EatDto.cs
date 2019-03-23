using System;

namespace Hungry.Bear.API.Features.Meal.Dtos
{
    public class EatDto
    {
        public int Id { get; set; }

        public int OwnerId { get; set; }

        public DateTime Date { get; set; }

        public double Distance { get; set; }

        public int Time { get; set; }
    }
}