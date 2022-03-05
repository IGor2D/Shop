using Moq;
using Shop.Controllers;
using Shop.Core.ServiceInterface;
using System;
using System.IO;
using Xunit;

namespace Shop.ProductTest
{
    public class ProductTest : TestBase
    {
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
