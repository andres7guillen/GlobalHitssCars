using CSharpFunctionalExtensions;
using MediatR;
using SparePartsServiceDomain.Exceptions;
using SparePartsServiceDomain.Repositories;

namespace SparePartServiceApplication.Commands
{
    public class LessStockSparePartCommand : IRequest<Result<bool>>
    {
        public Guid Id { get; set; }
        public int StockQuantity { get; set; }
        public LessStockSparePartCommand(Guid id, int stockQuantity)
        {
            Id = id;
            StockQuantity = stockQuantity;
        }

        public class LessStockSparePartCommandHandler : IRequestHandler<LessStockSparePartCommand, Result<bool>>
        {
            private readonly ISparePartRepository _sparePartRepository;

            public LessStockSparePartCommandHandler(ISparePartRepository sparePartRepository)
            {
                _sparePartRepository = sparePartRepository;
            }

            public async Task<Result<bool>> Handle(LessStockSparePartCommand request, CancellationToken cancellationToken)
            {
                var spare = await _sparePartRepository.GetSparePartById(request.Id);
                if (spare.HasValue) 
                {
                    var result = spare.Value.LessStock(request.StockQuantity);
                    if (result.IsSuccess)
                    {
                        var stockUpdated = await _sparePartRepository.UpdatateSpare(spare.Value);
                        if (stockUpdated)
                        {
                            return Result.Success(stockUpdated);
                        }
                    }
                    else 
                    {
                        return Result.Failure<bool>(result.Error);
                    }                    
                }
                return Result.Failure<bool>(SparePartContextExceptionEnum.ErrorTryingToLessStock.GetErrorMessage());

            }
        }
    }
}
