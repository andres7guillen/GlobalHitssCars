using CSharpFunctionalExtensions;
using MediatR;
using SparePartsServiceDomain.Entities;
using SparePartsServiceDomain.Exceptions;
using SparePartsServiceDomain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparePartServiceApplication.Commands
{
    public class UpdateSparePartCommand : IRequest<Result<bool>>
    {
        public Guid Id { get; set; }
        public string SpareName { get; set; } = string.Empty;
        public string BrandSpare { get; set; } = string.Empty;
        public string BrandCar { get; set; } = string.Empty;
        public int ModelCar { get; set; }
        public string ReferenceCar { get; set; } = string.Empty;
        public bool IsInStock { get; set; }
        public int Stock { get; set; }

        public UpdateSparePartCommand(Guid id, string spareName, string brandSpare, string brandCar, int modelCar, string referenceCar, bool isInStock, int stock)
        {
            Id = id;
            SpareName = spareName;
            BrandSpare = brandSpare;
            BrandCar = brandCar;
            ModelCar = modelCar;
            ReferenceCar = referenceCar;
            IsInStock = isInStock;
            Stock = stock;
        }

        
        public class UpdateSparePartCommandHandler : IRequestHandler<UpdateSparePartCommand, Result<bool>>
        {
            private readonly ISparePartRepository _sparePartRepository;

            public UpdateSparePartCommandHandler(ISparePartRepository sparePartRepository)
            {
                _sparePartRepository = sparePartRepository;
            }

            public async Task<Result<bool>> Handle(UpdateSparePartCommand request, CancellationToken cancellationToken)
            {
                var spare = await _sparePartRepository.GetSparePartById(request.Id);
                spare.Value.BrandSpare = request.BrandSpare;
                spare.Value.SpareName = request.SpareName;
                spare.Value.BrandCar = request.BrandCar;
                spare.Value.ModelCar = request.ModelCar;
                spare.Value.IsInStock = request.IsInStock;
                spare.Value.Stock = request.Stock;
                spare.Value.ReferenceCar = request.ReferenceCar;
                spare.Value.UpdateSpare();

                var updated = await _sparePartRepository.UpdatateSpare(spare.Value);
                return updated
                    ? Result.Success(updated)
                    : Result.Failure<bool>(SparePartContextExceptionEnum.ErrorUpdatingSparePart.GetErrorMessage());
            }
        }
    }    
}
