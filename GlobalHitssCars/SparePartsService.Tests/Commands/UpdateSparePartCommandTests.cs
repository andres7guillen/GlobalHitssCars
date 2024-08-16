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
            var sparePartExpected = SparePart.Build("Spare test",
                "Brand spare test",
                "Brand car test",
                2000,
                "Reference test",
                true,
                10);
            var mockSparePartRepository = new Mock<ISparePartRepository>();
            mockSparePartRepository.Setup(repo => repo.GetSparePartById(It.IsAny<Guid>()))
                .ReturnsAsync(sparePartExpected.Value);
            mockSparePartRepository.Setup(repo => repo.UpdatateSpare(It.IsAny<SparePart>()))
                .ReturnsAsync(true);
            var command = new UpdateSparePartCommand(sparePartExpected.Value.Id,
                sparePartExpected.Value.SpareName,
                sparePartExpected.Value.BrandSpare,
                sparePartExpected.Value.BrandCar,
                sparePartExpected.Value.ModelCar,
                sparePartExpected.Value.ReferenceCar,
                sparePartExpected.Value.IsInStock,
                sparePartExpected.Value.Stock);
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
            var sparePartExpected = SparePart.Build("Spare test",
                "Brand spare test",
                "Brand car test",
                2000,
                "Reference test",
                true,
                10);
            var mockSparePartRepository = new Mock<ISparePartRepository>();
            mockSparePartRepository.Setup(repo => repo.GetSparePartById(It.IsAny<Guid>()))
                .ReturnsAsync(sparePartExpected.Value);
            mockSparePartRepository.Setup(repo => repo.UpdatateSpare(It.IsAny<SparePart>()))
                .ReturnsAsync(false);
            var command = new UpdateSparePartCommand(
                sparePartExpected.Value.Id,
                sparePartExpected.Value.SpareName,
                sparePartExpected.Value.BrandSpare,
                sparePartExpected.Value.BrandCar,
                sparePartExpected.Value.ModelCar,
                sparePartExpected.Value.ReferenceCar,
                sparePartExpected.Value.IsInStock,
                sparePartExpected.Value.Stock);
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
