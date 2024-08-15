using ClientServiceApplication.Commands;
using ClientServiceDomain.Entities;
using ClientServiceDomain.Repositories;
using Moq;

namespace ClientService.Tests.Commands
{
    public class UpdateClientCommandTests
    {
        [Fact]
        public async void UpdateClientShouldWorks()
        {
            //Arrange
            var clientExpected = Client.Build(
                withEmail: "client1@test.com",
                withSurName: "Test surname",
                withName: "Test name");
            var mockClientRepository = new Mock<IClientRepository>();
            mockClientRepository.Setup(repo => repo.Update(It.IsAny<Client>()))
                .ReturnsAsync(true);
            var command = new UpdateClientCommand(clientExpected.Value.Name, clientExpected.Value.SurName, clientExpected.Value.Email, clientExpected.Value.Id);
            var handler = new UpdateClientCommand.UpdateClientCommandHandler(mockClientRepository.Object);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.True(result.Value);
        }

        [Fact]
        public async void UpdateClientShouldFails()
        {
            //Arrange
            var clientExpected = Client.Build(
                withEmail: "client1@test.com",
                withSurName: "Test surname",
                withName: "Test name");
            var mockClientRepository = new Mock<IClientRepository>();
            mockClientRepository.Setup(repo => repo.Update(It.IsAny<Client>()))
                .ReturnsAsync(false);
            var command = new UpdateClientCommand(clientExpected.Value.Name, clientExpected.Value.SurName, clientExpected.Value.Email, clientExpected.Value.Id);
            var handler = new UpdateClientCommand.UpdateClientCommandHandler(mockClientRepository.Object);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("4002: Error updating Client", result.Error);
            mockClientRepository.Verify(repo => repo.Update(It.IsAny<Client>()), Times.Once);
        }

    }
}
