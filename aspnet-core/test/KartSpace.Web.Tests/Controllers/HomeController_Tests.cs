using System.Threading.Tasks;
using KartSpace.Models.TokenAuth;
using KartSpace.Web.Controllers;
using Shouldly;
using Xunit;

namespace KartSpace.Web.Tests.Controllers
{
    public class HomeController_Tests: KartSpaceWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}