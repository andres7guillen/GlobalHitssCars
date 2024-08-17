using CSharpFunctionalExtensions;
using Moq;
using SparePartServiceApplication.Commands;
using SparePartsServiceDomain.Entities;
using SparePartsServiceDomain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparePartsService.Tests.Commands
{
    public class AddSpareStockCommandTests
    {
        [Fact]
        public async void AddSpareStockShouldWorks()
        {
            //arrange
            var mockSparePartRepository = new Mock<ISparePartRepository>();
            var sparePart = SparePart.Build("SampleSparePart","BrandSpareTest","BrandCarTest",2000,"referenceTest",true,10).Value;

            mockSparePartRepository
                .Setup(repo => repo.GetSparePartById(It.IsAny<Guid>()))
                .ReturnsAsync(Maybe<SparePart>.From(sparePart));
            mockSparePartRepository
                .Setup(repo => repo.UpdatateSpare(It.IsAny<SparePart>()))
                .ReturnsAsync(true);


            //Act
            var command = new AddSpareStockCommand(Guid.NewGuid(), 10);
            var handler = new AddSpareStockCommand.AddSpareStockCommandHandler(mockSparePartRepository.Object);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            Assert.True(result.Value);
            mockSparePartRepository.Verify(repo => repo.GetSparePartById(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async void AddSpareStockShouldFails()
        {
            //arrange
            var mockSparePartRepository = new Mock<ISparePartRepository>();

            var sparePart = SparePart.Build("SampleSparePart", "BrandSpareTest", "BrandCarTest", 2000, "referenceTest", true, 10).Value;
            mockSparePartRepository.Setup(repo => repo.GetSparePartById(It.IsAny<Guid>()))
                .ReturnsAsync(Maybe<SparePart>.From(sparePart));
            mockSparePartRepository.Setup(repo => repo.UpdatateSpare(It.IsAny<SparePart>()))
                .ReturnsAsync(true);

            //Act
            var command = new AddSpareStockCommand(Guid.NewGuid(), 10);
            var handler = new AddSpareStockCommand.AddSpareStockCommandHandler(mockSparePartRepository.Object);

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(result.IsSuccess);
            mockSparePartRepository.Verify(repo => repo.GetSparePartById(It.IsAny<Guid>()), Times.Once);
            mockSparePartRepository.Verify(repo => repo.UpdatateSpare(It.IsAny<SparePart>()), Times.Once);
        }
    }
}
