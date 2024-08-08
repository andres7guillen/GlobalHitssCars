using CSharpFunctionalExtensions;
using MediatR;
using PurchaseServiceDomain.Entities;
using PurchaseServiceDomain.Exceptions;
using PurchaseServiceDomain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseApplication.Queries
{
    public class GetAllPurchasesQuery : IRequest<Result<IEnumerable<Purchase>>> 
    {
        public int Offset { get; set; }
        public int Limit { get; set; }
    }

    public class GetAllPurchasesQueryHandler : IRequestHandler<GetAllPurchasesQuery, Result<IEnumerable<Purchase>>>
    {
        private readonly IPurchaseRepository _purchaseRepository;

        public GetAllPurchasesQueryHandler(IPurchaseRepository purchaseRepository)
        {
            _purchaseRepository = purchaseRepository;
        }

        public async Task<Result<IEnumerable<Purchase>>> Handle(GetAllPurchasesQuery request, CancellationToken cancellationToken)
        {
            var list = await _purchaseRepository.GetAll(request.Offset, request.Limit);
            return list.Count() > 0
                ? Result.Success(list)
                : Result.Failure<IEnumerable<Purchase>>(PurchaseContextExceptionEnum.NoPurchasesFound.GetErrorMessage());
        }
    }

}
