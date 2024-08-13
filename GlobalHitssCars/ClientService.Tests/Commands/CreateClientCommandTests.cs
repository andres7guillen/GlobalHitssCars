using ClientServiceApplication.Commands;
using ClientServiceDomain.Entities;
using ClientServiceDomain.Repositories;
using Common.Logging.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientService.Tests.Commands
{    
    public class CreateClientCommandTests
    {
        [Fact]
        public async void CreateClientShouldWorks()
        {
            //arrange
            var mockClientRepository = new Mock<IClientRepository>();
            var mockLogger = new Mock<ILogger>();

            var newGuid = Guid.NewGuid();
            Client client1 = new Client()
            {
                Email = "client1@test.com",
                Id = Guid.NewGuid(),
                Name = "Test name",
                SurName = "Test surname"
            };
            mockClientRepository.Setup(repo => repo.Create(It.IsAny<Client>()))
                .ReturnsAsync(client1);

            var handler = new CreateClientCommand.CreateClientCommandHandler(mockClientRepository.Object);
            var command = new CreateClientCommand(client1);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(client1, result.Value);
            mockClientRepository.Verify(repo => repo.Create(It.IsAny<Client>()), Times.Once);
        }

        [Fact]
        public async void CreateClientShouldFails()
        {
            //arrange
            var mockClientRepository = new Mock<IClientRepository>();
            var mockLogger = new Mock<ILogger>();

            var newGuid = Guid.NewGuid();
            Client client1 = new Client()
            {
                Email = "client1@test.com",
                Id = Guid.NewGuid(),
                Name = "Test name",
                SurName = "Test surname"
            };
            mockClientRepository.Setup(repo => repo.Create(It.IsAny<Client>()))
                .ReturnsAsync((Client)null);

            var command = new CreateClientCommand(client1);
            var handler = new CreateClientCommand.CreateClientCommandHandler(mockClientRepository.Object);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("4003: Error creating Client", result.Error);
            mockClientRepository.Verify(repo => repo.Create(It.IsAny<Client>()), Times.Once);
        }

    }
}
