using ClientService.Tests.Config;
using ClientServiceDomain.Entities;
using ClientServiceInfrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientService.Tests.Repositories
{
    public class ClientRepositoryTests
    {
        [Fact]
        public async void CreateClientShouldWorks()
        {
            //Arrange
            var context = ApplicationClientDbContextInMemory.Get();
            var repository = new ClientRepository(context);
            var clientTest = Client.Build(
                withEmail: "client1@test.com",
                withSurName: "Test surname",
                withName: "Test name");
            //Act
            var result = await repository.Create(clientTest.Value);

            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async void DeleteClientShouldWorks()
        {
            //Arrange
            var context = ApplicationClientDbContextInMemory.Get();
            var repository = new ClientRepository(context);
            var idClient = Guid.NewGuid();
            var clientTest = Client.Build(
                withEmail: "client1@test.com",
                withSurName: "Test surname",
                withName: "Test name");

            //Act
            var clientCreated = await repository.Create(clientTest.Value);
            var resultDeletedOk = await repository.Delete(clientCreated.Id);
            var resultGetById = await repository.GetById(clientCreated.Id);

            //Assert
            Assert.True(resultDeletedOk);
            Assert.True(resultGetById.HasNoValue);
        }

        [Fact]
        public async void DeleteClientShouldFails_WhenIdNotExists()
        {
            //Arrange
            var context = ApplicationClientDbContextInMemory.Get();
            var repository = new ClientRepository(context);
            var clientTest = Client.Build(
                withEmail: "client1@test.com",
                withSurName: "Test surname",
                withName: "Test name");
            var clientCreated = await repository.Create(clientTest.Value);

            //Act
            var resultDeletedFail = await repository.Delete(Guid.NewGuid());

            //Assert
            Assert.False(resultDeletedFail);
        }

        [Fact]
        public async void GetAllShouldWorks()
        {
            //Arrange
            var context = ApplicationClientDbContextInMemory.Get();
            var repository = new ClientRepository(context);
            var idClient = Guid.NewGuid();
            var clientTest = Client.Build(
                withEmail: "client1@test.com",
                withSurName: "Test surname",
                withName: "Test name");

            //Act
            await repository.Create(clientTest.Value);
            var list = await repository.GetAll();

            //Assert
            Assert.NotNull(list);
        }

        [Fact]
        public async void GetClientByIdShouldWorks()
        {
            //Arrange
            var context = ApplicationClientDbContextInMemory.Get();
            var repository = new ClientRepository(context);

            var clientTest = Client.Build(
                withEmail: "client1@test.com",
                withSurName: "Test surname",
                withName: "Test name");

            //Act
            await repository.Create(clientTest.Value);
            var ClientById = await repository.GetById(clientTest.Value.Id);

            //Assert
            Assert.NotNull(ClientById.Value);
            Assert.True(ClientById.HasValue);
        }

        [Fact]
        public async void GetClientByIdShouldFails_WhenIdGivenNotExists()
        {
            //Arrange
            var context = ApplicationClientDbContextInMemory.Get();
            var idClient = Guid.NewGuid();
            var repository = new ClientRepository(context);

            var clientTest = Client.Build(
                withEmail: "client1@test.com",
                withSurName: "Test surname",
                withName: "Test name");

            //Act
            await repository.Create(clientTest.Value);
            var ClientById = await repository.GetById(Guid.NewGuid());

            //Assert
            Assert.True(ClientById.HasNoValue);
        }

        [Fact]
        public async void UpdateClientShouldWorks()
        {
            //Arrange
            var context = ApplicationClientDbContextInMemory.Get();
            var repository = new ClientRepository(context);
            var newEmail = "NewEmailUpdated";
            var clientTest = Client.Build(
                withEmail: "client1@test.com",
                withSurName: "Test surname",
                withName: "Test name");
            //Act
            await repository.Create(clientTest.Value);
            var ClientById = await repository.GetById(clientTest.Value.Id);
            var ClientToUpdate = ClientById.Value;
            ClientToUpdate.Email = newEmail;
            await repository.Update(ClientToUpdate);

            var ClientByIdUpdated = await repository.GetById(clientTest.Value.Id);

            //Assert
            Assert.True(ClientToUpdate.Email == newEmail);
        }
    }
}
