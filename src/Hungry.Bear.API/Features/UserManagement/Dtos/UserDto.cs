using System.Collections.Generic;
using Hungry.Bear.API.Features.Meal.Dtos;

namespace Hungry.Bear.API.Features.UserManagement.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string UserName { get; set; }

        public IList<EatDto> Runs { get; set; }
    }
}
