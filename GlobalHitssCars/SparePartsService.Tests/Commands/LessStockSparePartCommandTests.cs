using CSharpFunctionalExtensions;
using Moq;
using SparePartServiceApplication.Commands;
using SparePartsServiceDomain.Entities;
using SparePartsServiceDomain.Exceptions;
using SparePartsServiceDomain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparePartsService.Tests.Commands
{
    public class LessStockSparePartCommandHandlerTests
    {
        [Fact]
        public async Task ShouldReturnFailure_WhenThereIsNotEnoughStock()
        {
            // Arrange FAIIIL
            var sparePartRepositoryMock = new Mock<ISparePartRepository>();
            var sparePart = SparePart.Build("SampleSparePart", "BrandSpareTest", "BrandCarTest", 2000, "referenceTest", true, 10).Value;
            var command = new LessStockSparePartCommand(Guid.NewGuid(), 50);
            var handler = new LessStockSparePartCommand.LessStockSparePartCommandHandler(sparePartRepositoryMock.Object);

            sparePartRepositoryMock
                .Setup(repo => repo.GetSparePartById(It.IsAny<Guid>()))
                .ReturnsAsync(sparePart);
            sparePartRepositoryMock
                .Setup(repo => repo.UpdatateSpare(It.IsAny<SparePart>()))
                .ReturnsAsync(true);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(SparePartContextExceptionEnum.IsNotEnoughStockToDelete.GetErrorMessage(), result.Error);
        }

        [Fact]
        public async Task ShouldReturnSuccess_WhenThereIsEnoughStock()
        {
            // Arrange
            var sparePartRepositoryMock = new Mock<ISparePartRepository>();
            var sparePart = SparePart.Build("SampleSparePart", "BrandSpareTest", "BrandCarTest", 2000, "referenceTest", true, 10).Value;
            var command = new LessStockSparePartCommand(Guid.NewGuid(), 5);
            var handler = new LessStockSparePartCommand.LessStockSparePartCommandHandler(sparePartRepositoryMock.Object);

            sparePartRepositoryMock
                .Setup(repo => repo.GetSparePartById(It.IsAny<Guid>()))
                .ReturnsAsync(sparePart);
            sparePartRepositoryMock
                .Setup(repo => repo.UpdatateSpare(It.IsAny<SparePart>()))
                .ReturnsAsync(true);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.True(result.Value);
            sparePartRepositoryMock.Verify(repo => repo.GetSparePartById(It.IsAny<Guid>()), Times.Once);
        }


    }
}
