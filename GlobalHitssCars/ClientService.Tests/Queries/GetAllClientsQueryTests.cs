using CarServiceDomain.Exceptions;
using ClientServiceApplication.Queries;
using ClientServiceDomain.Entities;
using ClientServiceDomain.Exceptions;
using ClientServiceDomain.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientService.Tests.Queries
{
    public class GetAllClientsQueryTests
    {
        [Fact]
        public async void GetAllClientsShouldWorks_WhenClientsAreFound() 
        {
            //Arrange
            List<Client> list = new List<Client>();
            var client1= Client.Build(
                withEmail: "client1@test.com",
                withSurName: "Test surname",
                withName: "Test name");
            var client2 = Client.Build(
                withEmail: "client2@test.com",
                withSurName: "Test surname2",
                withName: "Test name2");
            list.Add(client1.Value);
            list.Add(client2.Value);

            var mockClientRepository = new Mock<IClientRepository>();
            mockClientRepository.Setup(repo => repo.GetAll(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new Tuple<int,IEnumerable<Client>>(list.Count(),list));
            var query = new GetAllClientsQuery(0,10);
            var handler = new GetAllClientsQuery.GetAllClientsQueryHandler(mockClientRepository.Object);

            //Act
            var result = await handler.Handle(query, CancellationToken.None);
            //Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(list, result.Value.Item2);
            mockClientRepository.Verify(repo => repo.GetAll(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async void GetAllClientsShouldFails_WhenNoClientsAreFound() 
        { 
            //Arrange
            var mockClientRepository = new Mock<IClientRepository>();
            List<Client> list = new List<Client>();
            mockClientRepository.Setup(repo => repo.GetAll(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(new Tuple<int, IEnumerable<Client>>(list.Count(), list));

            var query = new GetAllClientsQuery(0, 10);
            var handler = new GetAllClientsQuery.GetAllClientsQueryHandler(mockClientRepository.Object);
            //Act
            var result = await handler.Handle(query, CancellationToken.None);
            //Assert
            Assert.Equal(ClientContextExceptionEnum.NoClientsFound.GetErrorMessage(), result.Error);
            mockClientRepository.Verify(repo => repo.GetAll(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }

    }
}
