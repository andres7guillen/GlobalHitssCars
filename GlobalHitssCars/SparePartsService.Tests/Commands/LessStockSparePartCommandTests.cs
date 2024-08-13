using CSharpFunctionalExtensions;
using Moq;
using SparePartServiceApplication.Commands;
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
        private readonly Mock<ISparePartRepository> _sparePartRepositoryMock;
        private readonly LessStockSparePartCommand.LessStockSparePartCommandHandler _handler;

        public LessStockSparePartCommandHandlerTests()
        {
            _sparePartRepositoryMock = new Mock<ISparePartRepository>();
            _handler = new LessStockSparePartCommand.LessStockSparePartCommandHandler(_sparePartRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenThereIsEnoughStock()
        {
            // Arrange
            var sparePartId = Guid.NewGuid();
            var stockQuantity = 5;
            var currentStock = 10;

            var command = new LessStockSparePartCommand(sparePartId, stockQuantity);

            _sparePartRepositoryMock
                .Setup(x => x.GetStockBySpareId(sparePartId))
                .ReturnsAsync(Result.Success(currentStock));

            _sparePartRepositoryMock
                .Setup(x => x.LessStock(sparePartId, stockQuantity))
                .ReturnsAsync(true);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.True(result.Value);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenThereIsNotEnoughStock()
        {
            // Arrange
            var sparePartId = Guid.NewGuid();
            var stockQuantity = 15;
            var currentStock = 10;

            var command = new LessStockSparePartCommand(sparePartId, stockQuantity);

            _sparePartRepositoryMock
                .Setup(x => x.GetStockBySpareId(sparePartId))
                .ReturnsAsync(Result.Success(currentStock));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(SparePartContextExceptionEnum.IsNotEnoughStockToDelete.GetErrorMessage(), result.Error);
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenErrorOccursWhileReducingStock()
        {
            // Arrange
            var sparePartId = Guid.NewGuid();
            var stockQuantity = 5;
            var currentStock = 10;

            var command = new LessStockSparePartCommand(sparePartId, stockQuantity);

            _sparePartRepositoryMock
                .Setup(x => x.GetStockBySpareId(sparePartId))
                .ReturnsAsync(Result.Success(currentStock));

            _sparePartRepositoryMock
                .Setup(x => x.LessStock(sparePartId, stockQuantity))
                .ReturnsAsync(false);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(SparePartContextExceptionEnum.ErrorTryingToLessStock.GetErrorMessage(), result.Error);
        }
    }
}
