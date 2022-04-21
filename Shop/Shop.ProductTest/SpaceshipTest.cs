using Moq;
using Shop.Controllers;
using Shop.Core.Dtos;
using Shop.Core.ServiceInterface;
using Shop.ProductTest;
using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace Shop.SpaceshipTest
{
    public class SpaceshipTest : TestBase
    {
        [Fact]
        public async Task Should_AddNewSpaseship_WhenReturnsResult()
        {
            string guid = "3185fe69-a25c-4f1c-b439-d47aebe2a6ef";
            SpaceshipDto spaceship = new SpaceshipDto();

            spaceship.Id = Guid.Parse(guid);
            spaceship.Company = "Space";
            spaceship.Country = "Estonia";
            spaceship.Model = "Cargo";
            spaceship.Name = "asd";
            spaceship.EnginePower = 123;
            spaceship.LaunchDate = DateTime.Now;
            spaceship.CreatedAt = DateTime.Now;
            spaceship.ModifieAt = DateTime.Now;

            var result = await Svc<ISpaceshipService>().Add(spaceship);
            Assert.Empty((System.Collections.IEnumerable)result);
        }

        [Fact]
        public async Task Should_GetByIdSpaseship_WhenReturnsResultAsync()
        {
            string guid = "da6973d7-3fdf-4d5c-9d76-9c29d071e268";
            var insertGuid = Guid.Parse(guid);
            var result = await Svc<ISpaceshipService>().GetAsync(insertGuid);
            Assert.NotEmpty((System.Collections.IEnumerable)result);
            Assert.Single((System.Collections.IEnumerable)result);
        }

        [Fact]
        public async Task Should_DeleteByIdSpaseship_WhenDeleteSpaceship()
        {
            string guid = "c4943f9d-6f53-48fe-904d-fb281d996bd6";
            var insertGuid = Guid.Parse(guid);
            var result = await Svc<ISpaceshipService>().Delete(insertGuid);
            Assert.NotEmpty((System.Collections.IEnumerable)result);
            Assert.Single((System.Collections.IEnumerable)result);
        }

        [Fact]
        public async Task Should_UpdateByIdSpaceship_WhenUpdateSpaceship()
        {
            string guid = "accf1408-3076-4752-86cb-2ceff35a2d2e";
            SpaceshipDto spaceship = new SpaceshipDto();

            spaceship.Id = Guid.Parse(guid);
            spaceship.Company = "Space";
            spaceship.Country = "Estonia";
            spaceship.Model = "Cargo";
            spaceship.Name = "asd";
            spaceship.EnginePower = 123;
            spaceship.LaunchDate = DateTime.Now;
            spaceship.CreatedAt = DateTime.Now;
            spaceship.ModifieAt = DateTime.Now;

            await Svc<ISpaceshipService>().Update(spaceship);
            Assert.NotEmpty((System.Collections.IEnumerable)spaceship);
        }

        [Fact]
        public void Test1()
        {
            //Arrange
            var repo = new Mock<IProductService>();
            var controller = new ProductController(null, repo.Object, null);
            var newGuid = Guid.NewGuid();
            string uploadsFolder = Path.Combine("wwwroot/", "multipleFileUpload");
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + "test.jpg";
            //Act

            //Assert
        }
    }
}
