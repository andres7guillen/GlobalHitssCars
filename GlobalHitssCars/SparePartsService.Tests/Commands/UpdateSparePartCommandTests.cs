using Moq;
using SparePartServiceApplication.Commands;
using SparePartsServiceDomain.Entities;
using SparePartsServiceDomain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparePartsService.Tests.Commands
{
    public class UpdateSparePartCommandTests
    {
        [Fact]
        public async void UpdateSparePartShouldWorks()
        {
            //Arrange
            var newGuid = Guid.NewGuid();
            SparePart SparePart1 = new SparePart()
            {
                BrandCar = "Brand car test",
                BrandSpare = "Brand spare test",
                Id = newGuid,
                IsInStock = true,
                ModelCar = 2000,
                ReferenceCar = "Reference test",
                SpareName = "Spare test",
                Stock = 0
            };
            var mockSparePartRepository = new Mock<ISparePartRepository>();
            mockSparePartRepository.Setup(repo => repo.UpdatateSpare(It.IsAny<SparePart>()))
                .ReturnsAsync(true);
            var command = new UpdateSparePartCommand(SparePart1);
            var handler = new UpdateSparePartCommand.UpdateSparePartCommandHandler(mockSparePartRepository.Object);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.True(result.Value);
        }

        [Fact]
        public async void UpdateSparePartShouldFails()
        {
            //Arrange
            var newGuid = Guid.NewGuid();
            SparePart SparePart1 = new SparePart()
            {
                BrandCar = "Brand car test",
                BrandSpare = "Brand spare test",
                Id = newGuid,
                IsInStock = true,
                ModelCar = 2000,
                ReferenceCar = "Reference test",
                SpareName = "Spare test",
                Stock = 0
            };
            var mockSparePartRepository = new Mock<ISparePartRepository>();
            mockSparePartRepository.Setup(repo => repo.UpdatateSpare(It.IsAny<SparePart>()))
                .ReturnsAsync(false);
            var command = new UpdateSparePartCommand(SparePart1);
            var handler = new UpdateSparePartCommand.UpdateSparePartCommandHandler(mockSparePartRepository.Object);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("4002: Error updating SparePart", result.Error);
            mockSparePartRepository.Verify(repo => repo.UpdatateSpare(It.IsAny<SparePart>()), Times.Once);
        }
    }
}
