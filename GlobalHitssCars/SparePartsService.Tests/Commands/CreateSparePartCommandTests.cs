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

            var SparePartExpected = SparePart.Build("Spare test", 
                "Brand spare test", 
                "Brand car test", 
                2000, 
                "Reference test", 
                true, 
                10);
            mockSparePartRepository.Setup(repo => repo.Create(It.IsAny<SparePart>()))
                .ReturnsAsync(SparePartExpected.Value);

            var command = new CreateSparePartCommand(SparePartExpected.Value);
            var handler = new CreateSparePartCommand.CreateSparePartCommandHandler(mockSparePartRepository.Object);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(SparePartExpected.Value, result.Value);
            mockSparePartRepository.Verify(repo => repo.Create(It.IsAny<SparePart>()), Times.Once);
        }

        [Fact]
        public async void CreateSparePartShouldFails()
        {
            //arrange
            var mockSparePartRepository = new Mock<ISparePartRepository>();
            var mockLogger = new Mock<ILogger>();

            var SparePartExpected = SparePart.Build("Spare test",
                "Brand spare test",
                "Brand car test",
                2000,
                "Reference test",
                true,
                10);
            mockSparePartRepository.Setup(repo => repo.Create(It.IsAny<SparePart>()))
                .ReturnsAsync((SparePart)null);

            var command = new CreateSparePartCommand(SparePartExpected.Value);
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
