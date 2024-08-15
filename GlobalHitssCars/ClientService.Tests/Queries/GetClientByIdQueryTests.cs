using ClientServiceApplication.Queries;
using ClientServiceDomain.Entities;
using ClientServiceDomain.Repositories;
using Common.Logging.Interfaces;
using CSharpFunctionalExtensions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientService.Tests.Queries
{
    public class GetClientByIdQueryTests
    {
        [Fact]
        public async void GetClientByIdShouldWorks_WhenAClientIsFoundById()
        {
            //Arrange
            var mockClientRepository = new Mock<IClientRepository>();
            var mockLogger = new Mock<ILogger>();
            var newGuid = Guid.NewGuid();
            var client1 = Client.Build(
                withEmail: "client1@test.com",
                withSurName: "Test surname",
                withName: "Test name");

            mockClientRepository.Setup(repo => repo.GetById(It.IsAny<Guid>()))
                .ReturnsAsync(client1.Value);

            var handler = new GetClientByIdQuery.GetClientByIdQueryHandler(mockClientRepository.Object);
            var query = new GetClientByIdQuery(newGuid);

            //Act
            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(client1, result.Value);
            mockClientRepository.Verify(repo => repo.GetById(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async void GetClientByIdShouldFails_WhenAClientIsNotFoundById()
        {
            //Arrange
            var mockClientRepository = new Mock<IClientRepository>();
            var mockLogger = new Mock<ILogger>();
            var newGuid = Guid.NewGuid();

            mockClientRepository.Setup(repo => repo.GetById(It.IsAny<Guid>()))
                .ReturnsAsync(Maybe<Client>.None);

            var handler = new GetClientByIdQuery.GetClientByIdQueryHandler(mockClientRepository.Object);
            var query = new GetClientByIdQuery(newGuid);

            //Act
            var result = await handler.Handle(query, CancellationToken.None);

            //Assert
            Assert.False(result.IsSuccess);
            mockClientRepository.Verify(repo => repo.GetById(It.IsAny<Guid>()), Times.Once);
        }
    }
}
