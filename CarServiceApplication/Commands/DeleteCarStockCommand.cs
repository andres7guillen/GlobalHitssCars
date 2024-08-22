using CarServiceDomain.Exceptions;
using CarServiceDomain.Repositories;
using CSharpFunctionalExtensions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarServiceApplication.Commands
{
    public class DeleteCarStockCommand : IRequest<Result<bool>>
    {
        public Guid Id { get; set; }

        public DeleteCarStockCommand(Guid id)
        {
            Id = id;
        }

        public class DeleteCarCommandHandler : IRequestHandler<DeleteCarStockCommand, Result<bool>>
        {
            private readonly ICarStockRepository _carStockRepository;
            public DeleteCarCommandHandler(ICarStockRepository carRepository)
            {
                _carStockRepository = carRepository;
            }
            public async Task<Result<bool>> Handle(DeleteCarStockCommand request, CancellationToken cancellationToken)
            {
                var wasDeleted = await _carStockRepository.Delete(request.Id);
                return wasDeleted
                    ? Result.Success(true)
                    : Result.Failure<bool>(CarContextExceptionEnum.ErrorDeletingCar.GetErrorMessage());
            }
        }

    }    

}
        

