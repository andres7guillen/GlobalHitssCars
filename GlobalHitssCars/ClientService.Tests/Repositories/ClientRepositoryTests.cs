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
            var idClient = Guid.NewGuid();
            var ClientTest = new Client()
            {
                Id = idClient,
                Email = "test@test.com",
                Name = "Name Test",
                SurName = "Surname Test"
            };
            //Act
            var result = await repository.Create(ClientTest);

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
            var ClientTest = new Client()
            {
                Id = idClient,
                Email = "test@test.com",
                Name = "Name Test",
                SurName = "Surname Test"
            };

            //Act
            await repository.Create(ClientTest);
            var resultDeletedOk = await repository.Delete(idClient);
            var resultGetById = await repository.GetById(idClient);

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
            var idClient = Guid.NewGuid();
            var ClientTest = new Client()
            {
                Id = idClient,
                Email = "test@test.com",
                Name = "Name Test",
                SurName = "Surname Test"
            };
            await repository.Create(ClientTest);

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
            var ClientTest = new Client()
            {
                Id = idClient,
                Email = "test@test.com",
                Name = "Name Test",
                SurName = "Surname Test"
            };

            //Act
            await repository.Create(ClientTest);
            var list = await repository.GetAll();

            //Assert
            Assert.NotNull(list);
        }

        [Fact]
        public async void GetClientByIdShouldWorks()
        {
            //Arrange
            var context = ApplicationClientDbContextInMemory.Get();
            var idClient = Guid.NewGuid();
            var repository = new ClientRepository(context);

            var ClientTest = new Client()
            {
                Id = idClient,
                Email = "test@test.com",
                Name = "Name Test",
                SurName = "Surname Test"
            };

            //Act
            await repository.Create(ClientTest);
            var ClientById = await repository.GetById(idClient);

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

            var ClientTest = new Client()
            {
                Id = idClient,
                Email = "test@test.com",
                Name = "Name Test",
                SurName = "Surname Test"
            };

            //Act
            await repository.Create(ClientTest);
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
            var idClient = Guid.NewGuid();
            var newEmail = "NewEmailUpdated";
            var ClientTest = new Client()
            {
                Id = idClient,
                Email = "test@test.com",
                Name = "Name Test",
                SurName = "Surname Test"
            };
            //Act
            await repository.Create(ClientTest);
            var ClientById = await repository.GetById(idClient);
            var ClientToUpdate = ClientById.Value;
            ClientToUpdate.Email = newEmail;
            await repository.Update(ClientToUpdate);

            var ClientByIdUpdated = await repository.GetById(idClient);

            //Assert
            Assert.True(ClientToUpdate.Email == newEmail);
        }
    }
}
