using ClientServiceApplication.Commands;
using ClientServiceDomain.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientService.Tests.Commands
{
    public class DeleteClientCommandTests
    {
        [Fact]
        public async void DeleteClientShouldWorks()
        {
            //arrange
            var mockClientRepository = new Mock<IClientRepository>();
            var newGuid = Guid.NewGuid();

            mockClientRepository.Setup(repo => repo.Delete(It.IsAny<Guid>()))
                .ReturnsAsync(true);
            //Act
            var command = new DeleteClientCommand(newGuid);
            var handler = new DeleteClientCommand.DeleteClientCommandHandler(mockClientRepository.Object);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.True(result.Value);
            mockClientRepository.Verify(repo => repo.Delete(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async void DeleteClientShouldFails()
        {
            //arrange
            var mockClientRepository = new Mock<IClientRepository>();
            var newGuid = Guid.NewGuid();

            mockClientRepository.Setup(repo => repo.Delete(It.IsAny<Guid>()))
                .ReturnsAsync(false);
            //Act
            var command = new DeleteClientCommand(newGuid);
            var handler = new DeleteClientCommand.DeleteClientCommandHandler(mockClientRepository.Object);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("4004: Error deleting Client", result.Error);
            mockClientRepository.Verify(repo => repo.Delete(It.IsAny<Guid>()), Times.Once);
        }
    }
}
