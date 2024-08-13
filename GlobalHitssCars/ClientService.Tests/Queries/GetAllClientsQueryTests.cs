﻿using CarServiceDomain.Exceptions;
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
            Client client1 = new Client()
            {
                Email = "client1@test.com",
                Id = Guid.NewGuid(),
                Name = "Test name",
                SurName = "Test surname"
            };
            Client client2 = new Client()
            {
                Email = "client2@test.com",
                Id = Guid.NewGuid(),
                Name = "Test2 name",
                SurName = "Test2 surname"
            };
            list.Add(client1);
            list.Add(client2);

            var mockClientRepository = new Mock<IClientRepository>();
            mockClientRepository.Setup(repo => repo.GetAll(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(list);
            var query = new GetAllClientsQuery(0,10);
            var handler = new GetAllClientsQuery.GetAllClientsQueryHandler(mockClientRepository.Object);

            //Act
            var result = await handler.Handle(query, CancellationToken.None);
            //Assert
            Assert.True(result.IsSuccess);
            Assert.Equal(list, result.Value);
            mockClientRepository.Verify(repo => repo.GetAll(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async void GetAllClientsShouldFails_WhenNoClientsAreFound() 
        { 
            //Arrange
            var mockClientRepository = new Mock<IClientRepository>();
            List<Client> list = new List<Client>();
            mockClientRepository.Setup(repo => repo.GetAll(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(list);

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