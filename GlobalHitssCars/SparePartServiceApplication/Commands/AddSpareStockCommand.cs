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
            var newStock = await _sparePartRepository.AddStock(request.Id, request.Quantity);
            return newStock
                ? Result.Success(newStock)
                : Result.Failure<bool>(SparePartContextExceptionEnum.ErrorUpdatingSparePart.GetErrorMessage());
        }
    }
}
