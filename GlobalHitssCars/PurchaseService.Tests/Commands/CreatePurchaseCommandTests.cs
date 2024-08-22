using Common.Logging.Interfaces;
using Moq;
using PurchaseApplication.Commands;
using PurchaseServiceApplication.MessageBus.Interfaces;
using PurchaseServiceDomain.Entities;
using PurchaseServiceDomain.Enum;
using PurchaseServiceDomain.Repository;
using PurchaseServiceDomain.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseService.Tests.Commands
{
    public class CreatePurchaseCommandTests
    {
        [Fact]
        public async void CreatePurchaseShouldWorks()
        {
            //arrange
            var mockPurchaseRepository = new Mock<IPurchaseRepository>();
            var mockLogger = new Mock<ILogger>();
            var messageProducerMock = new Mock<IMessageProducer>();

            var purchaseExpected = Purchase.Build(
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                10,
                70000000,
                TypePurchaseEnum.Car
                );                
            
            mockPurchaseRepository.Setup(repo => repo.Create(It.IsAny<Purchase>()))
                .ReturnsAsync(purchaseExpected.Value);
            messageProducerMock.Setup(m => m.SendingMessage(It.IsAny<Event<object>>(),""));

            var command = new CreatePurchaseCommand(purchaseExpected.Value.ClientId, purchaseExpected.Value.CarId.Value,purchaseExpected.Value.Amount, purchaseExpected.Value.TypePurchase,purchaseExpected.Value.SparePartId,purchaseExpected.Value.Quantity);
            var handler = new CreatePurchaseCommand.CreatePurchaseCommandHandler(mockPurchaseRepository.Object);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(purchaseExpected.Value, result.Value);
            mockPurchaseRepository.Verify(repo => repo.Create(It.IsAny<Purchase>()), Times.Once);
        }

        [Fact]
        public async void CreatePurchaseShouldFails()
        {
            //arrange
            var mockPurchaseRepository = new Mock<IPurchaseRepository>();
            var mockLogger = new Mock<ILogger>();
            var messageProducerMock = new Mock<IMessageProducer>();
            messageProducerMock.Setup(m => m.SendingMessage(It.IsAny<Event<object>>(), ""));

            var purchaseExpected = Purchase.Build(
                Guid.NewGuid(),
                Guid.NewGuid(),
                Guid.NewGuid(),
                10,
                70000000,
                TypePurchaseEnum.Car);
            mockPurchaseRepository.Setup(repo => repo.Create(It.IsAny<Purchase>()))
                .ReturnsAsync((Purchase)null);

            var command = new CreatePurchaseCommand(purchaseExpected.Value.ClientId,purchaseExpected.Value.CarId.Value, 
                purchaseExpected.Value.Amount,purchaseExpected.Value.TypePurchase,purchaseExpected.Value.SparePartId,
                purchaseExpected.Value.Quantity);
            var handler = new CreatePurchaseCommand.CreatePurchaseCommandHandler(mockPurchaseRepository.Object);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("4003: Error creating Purchase", result.Error);
            mockPurchaseRepository.Verify(repo => repo.Create(It.IsAny<Purchase>()), Times.Once);
        }

    }
}
