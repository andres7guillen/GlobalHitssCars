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
    public class LessStockSparePartCommand : IRequest<Result<bool>>
    {
        public Guid Id { get; set; }
        public int stockQuantity { get; set; }
    }

    public class UpdateSpareStockCommandHandler : IRequestHandler<LessStockSparePartCommand, Result<bool>>
    {
        private readonly ISparePartRepository _sparePartRepository;
        public async Task<Result<bool>> Handle(LessStockSparePartCommand request, CancellationToken cancellationToken)
        {
            var totalStock = await _sparePartRepository.GetStockBySpareId(request.Id);
            if (request.stockQuantity > totalStock.Value)
                return Result.Failure<bool>(SparePartContextExceptionEnum.IsNotEnoughStockToDelete.GetErrorMessage());
            var result = await _sparePartRepository.LessStock(request.Id, request.stockQuantity);
            return result
                ? Result.Success(result)
                : Result.Failure<bool>(SparePartContextExceptionEnum.ErrorTryingToLessStock.GetErrorMessage());
        }
    }

}
