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
    public class DeleteCompanyCommand : IRequest<Result<bool>>
    {
        public Guid Id { get; set; }
    }

    public class DeleteCompanyCommandHandler : IRequestHandler<DeleteCompanyCommand, Result<bool>>
    {
        private readonly ICarRepository _carRepository;
        public DeleteCompanyCommandHandler(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }
        public async Task<Result<bool>> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
        {
            var wasDeleted = await _carRepository.Delete(request.Id);
            return wasDeleted
                ? Result.Success(true)
                : Result.Failure<bool>(CarContextExceptionEnum.ErrorDeleteingCar.GetErrorMessage());
        }
    }

}
        

