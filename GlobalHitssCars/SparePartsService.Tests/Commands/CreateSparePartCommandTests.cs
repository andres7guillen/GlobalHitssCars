using Common.Logging.Interfaces;
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
    public class CreateSparePartCommandTests
    {
        [Fact]
        public async void CreateSparePartShouldWorks()
        {
            //arrange
            var mockSparePartRepository = new Mock<ISparePartRepository>();
            var mockLogger = new Mock<ILogger>();

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
            mockSparePartRepository.Setup(repo => repo.Create(It.IsAny<SparePart>()))
                .ReturnsAsync(SparePart1);

            var command = new CreateSparePartCommand(SparePart1);
            var handler = new CreateSparePartCommand.CreateSparePartCommandHandler(mockSparePartRepository.Object);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(SparePart1, result.Value);
            mockSparePartRepository.Verify(repo => repo.Create(It.IsAny<SparePart>()), Times.Once);
        }

        [Fact]
        public async void CreateSparePartShouldFails()
        {
            //arrange
            var mockSparePartRepository = new Mock<ISparePartRepository>();
            var mockLogger = new Mock<ILogger>();

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
            mockSparePartRepository.Setup(repo => repo.Create(It.IsAny<SparePart>()))
                .ReturnsAsync((SparePart)null);

            var command = new CreateSparePartCommand(SparePart1);
            var handler = new CreateSparePartCommand.CreateSparePartCommandHandler(mockSparePartRepository.Object);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("4003: Error creating SparePart", result.Error);
            mockSparePartRepository.Verify(repo => repo.Create(It.IsAny<SparePart>()), Times.Once);
        }

    }
}
