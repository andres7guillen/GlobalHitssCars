using CSharpFunctionalExtensions;
using MediatR;
using SparePartsServiceDomain.Exceptions;
using SparePartsServiceDomain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparePartServiceApplication.Commands
{
    public class AddSpareStockCommand : IRequest<Result<bool>>
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public AddSpareStockCommand(Guid id, int quantity)
        {
            Id = id;
            Quantity = quantity;
        }

        public class AddSpareStockCommandHandler : IRequestHandler<AddSpareStockCommand, Result<bool>>
        {
            private readonly ISparePartRepository _sparePartRepository;

            public AddSpareStockCommandHandler(ISparePartRepository sparePartRepository)
            {
                _sparePartRepository = sparePartRepository;
            }

            public async Task<Result<bool>> Handle(AddSpareStockCommand request, CancellationToken cancellationToken)
            {
                var spare = await _sparePartRepository.GetSparePartById(request.Id);
                if (spare.HasValue)
                {
                    var result = spare.Value.AddStock(request.Quantity);
                    if (result.IsSuccess) 
                    {
                        var stockUpdated = await _sparePartRepository.UpdatateSpare(spare.Value);
                        if (stockUpdated)
                        {
                            return Result.Success(stockUpdated);
                        }
                    }                    
                    return Result.Failure<bool>(result.Error);                   
                }
                else
                {
                    return Result.Failure<bool>(SparePartContextExceptionEnum.ErrorTryingToAddStock.GetErrorMessage());
                }
            }
        }
    }
}
