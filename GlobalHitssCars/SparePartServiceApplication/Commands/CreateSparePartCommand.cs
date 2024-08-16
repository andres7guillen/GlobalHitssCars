using CSharpFunctionalExtensions;
using MediatR;
using SparePartsServiceDomain.Entities;
using SparePartsServiceDomain.Exceptions;
using SparePartsServiceDomain.Repositories;

namespace SparePartServiceApplication.Commands
{
    public class CreateSparePartCommand : IRequest<Result<SparePart>>
    {
        public string SpareName { get; set; } = string.Empty;
        public string BrandSpare { get; set; } = string.Empty;
        public string BrandCar { get; set; } = string.Empty;
        public int ModelCar { get; set; }
        public string ReferenceCar { get; set; } = string.Empty;
        public bool IsInStock { get; set; }
        public int Stock { get; set; }

        public CreateSparePartCommand(string spareName, string brandSpare, string brandCar, int modelCar, string referenceCar, bool isInStock, int stock)
        {
            SpareName = spareName;
            BrandSpare = brandSpare;
            BrandCar = brandCar;
            ModelCar = modelCar;
            ReferenceCar = referenceCar;
            IsInStock = isInStock;
            Stock = stock;
        }
        public class CreateSparePartCommandHandler : IRequestHandler<CreateSparePartCommand, Result<SparePart>>
        {
            ISparePartRepository _sparePartRepository;

            public CreateSparePartCommandHandler(ISparePartRepository sparePartRepository)
            {
                _sparePartRepository = sparePartRepository;
            }
            public async Task<Result<SparePart>> Handle(CreateSparePartCommand request, CancellationToken cancellationToken)
            {
                var spareToPart = SparePart.Build(request.SpareName, request.BrandSpare, request.BrandCar, request.ModelCar, request.ReferenceCar, request.IsInStock, request.Stock);
                var createdSpare = await _sparePartRepository.Create(spareToPart.Value);
                return createdSpare == null
                    ? Result.Failure<SparePart>(SparePartContextExceptionEnum.ErrorCreatingSparePart.GetErrorMessage())
                    : Result.Success(createdSpare);
            }
        }
    }
}
