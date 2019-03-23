using Hungry.Bear.API.Shared;
using Hungry.Bear.Domain.Features.UserManagement.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;

namespace Hungry.Bear.API.Filters
{
    public class PreventNormalUserToAccessOthersDataAttribute : Attribute, IAuthorizationFilter, IOrderedFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var currentUser = context.HttpContext.User;

            if (!currentUser.IsInRole(RoleNames.NormalUser))
            {
                return;
            }

            var userId = context.HttpContext.GetRouteValue("userId").ToString();

            if (userId != currentUser.GetId())
            {
                context.Result = new StatusCodeResult(403);
            }
        }

        public int Order { get; } = 100;
    }
}