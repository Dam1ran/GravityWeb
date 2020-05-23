using Domain.Entities;
using GravityDAL.Interfaces;
using GravityDTO;
using GravityServices.Interfaces;
using GravityWeb.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GravityWebTest
{
    public class PersonalInfoControllerFixture
    {

        [SetUp]
        public void Setup()
        {
            

        }

        [Test]
        public async Task WhenSave_ShouldReturnOKWithResponse()
        {
            //arange 
            var mockContext = new Mock<HttpContext>(MockBehavior.Strict);

            mockContext.SetupGet(hc => hc.User).Returns(new ClaimsPrincipal());

            var _userManagerMock = GetMockUserManager();
            
            _userManagerMock.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(new ApplicationUser { Id = 1L, Email = "test.gmail.com", UserName = "Testograph" });

            var personalInfoDTO = new PersonalInfoDTO
            { 
                Id=1, Age=40,
                FirstName="Mario",
                LastName="Luigi",
                FitnessExperienceInMonths=22,
                Gender="Male",
                HeightInCm=180,
                WeightInKg=75               
            };

            var _personalinfoServiceMock = new Mock<IPersonalInfoService>();
            _personalinfoServiceMock.Setup(o=>o.SavePersonalInfoAsync(personalInfoDTO,1L))
                .ReturnsAsync(personalInfoDTO);

            
            var cabinetController = new PersonalInfoController(
                _userManagerMock.Object,
                _personalinfoServiceMock.Object);

            cabinetController.ControllerContext = new ControllerContext()
            {
                HttpContext = mockContext.Object
            };

            //act
            var result = await cabinetController.Post(personalInfoDTO);

            var response = result as OkObjectResult;

            //assert
            mockContext.VerifyGet(u => u.User, Times.Once());
            _personalinfoServiceMock.Verify(pi=>pi.SavePersonalInfoAsync(It.IsAny<PersonalInfoDTO>(),It.IsAny<long>()),Times.Once);

            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Value);
            Assert.IsNotNull(response.Value as PersonalInfoDTO);
            Assert.IsTrue((response.Value as PersonalInfoDTO).Age==40);
            Assert.IsTrue((response.Value as PersonalInfoDTO).FirstName== "Mario");
            Assert.IsTrue((response.Value as PersonalInfoDTO).LastName=="Luigi");
            Assert.IsTrue((response.Value as PersonalInfoDTO).Id==1L);
            Assert.IsTrue((response.Value as PersonalInfoDTO).HeightInCm==180);
            Assert.IsTrue((response.Value as PersonalInfoDTO).WeightInKg==75);
            Assert.IsTrue((response.Value as PersonalInfoDTO).Gender=="Male");
            Assert.IsTrue((response.Value as PersonalInfoDTO).FitnessExperienceInMonths==22);

        }

        public static Mock<UserManager<ApplicationUser>> GetMockUserManager()
        {
            var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
            return new Mock<UserManager<ApplicationUser>>(
                userStoreMock.Object, null, null, null, null, null, null, null, null);
        }
    }
}
