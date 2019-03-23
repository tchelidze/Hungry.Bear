using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using Hungry.Bear.API.Filters;
using Hungry.Bear.Domain.Features.UserManagement.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Moq;
using Shouldly;
using Xunit;

namespace Hungry.Bear.Tests.Api
{
    public class PreventNormalUserToAccessOthersDataAttributeTest
    {
        private PreventNormalUserToAccessOthersDataAttribute _sut;

        private PreventNormalUserToAccessOthersDataAttribute Sut =>
            _sut ?? (_sut = new PreventNormalUserToAccessOthersDataAttribute());

        [Fact]
        public void when_normal_user_tries_to_access_others_data_403_should_be_returned()
        {
            const int CurrentUserId = 32;
            const int OtherUsersId = CurrentUserId + 1;

            var sampleCurrentUser = new Mock<ClaimsPrincipal>();

            sampleCurrentUser
                .Setup(user => user.IsInRole(RoleNames.NormalUser))
                .Returns(true);

            sampleCurrentUser
                .Setup(it => it.FindFirst(JwtRegisteredClaimNames.Sub))
                .Returns(new Claim(JwtRegisteredClaimNames.Sub, CurrentUserId.ToString()));

            var currentRouteData = new RouteData();
            typeof(RouteData)
                .GetField("_values", BindingFlags.Instance | BindingFlags.NonPublic)
                .SetValue(currentRouteData, new RouteValueDictionary
                {
                    {"key", OtherUsersId}
                });

            var input = new AuthorizationFilterContext(new ActionContext(new DefaultHttpContext
            {
                User = sampleCurrentUser.Object,
                Features =
                {
                    [typeof(IRoutingFeature)] = new RoutingFeature
                    {
                        RouteData = currentRouteData
                    }
                }
            }, currentRouteData, new ActionDescriptor(), new ModelStateDictionary()), new List<IFilterMetadata>());

            Sut.OnAuthorization(input);

            input.Result.ShouldBeOfType(typeof(StatusCodeResult));
            ((StatusCodeResult)input.Result).StatusCode.ShouldBe(403);
        }
    }
}