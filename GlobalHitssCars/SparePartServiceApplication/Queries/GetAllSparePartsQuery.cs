﻿using CSharpFunctionalExtensions;
using MediatR;
using SparePartsServiceDomain.Entities;
using SparePartsServiceDomain.Exceptions;
using SparePartsServiceDomain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparePartServiceApplication.Queries
{
    public class GetAllSparePartsQuery : IRequest<Result<Tuple<int, IEnumerable<SparePart>>>>
    {
        public int Offset { get; set; }
        public int Limit { get; set; }
        public GetAllSparePartsQuery(int offset, int limit)
        {
            Offset = offset;
            Limit = limit;
        }

        public class GetAllSparePartsQueryHandler : IRequestHandler<GetAllSparePartsQuery, Result<Tuple<int,IEnumerable<SparePart>>>>
        {
            private ISparePartRepository _repository;

            public GetAllSparePartsQueryHandler(ISparePartRepository repository)
            {
                _repository = repository;
            }

            public async Task<Result<Tuple<int, IEnumerable<SparePart>>>> Handle(GetAllSparePartsQuery request, CancellationToken cancellationToken)
            {
                var list = await _repository.GetAllSpareParts(request.Offset, request.Limit);
                return list.Item1 > 0
                    ? Result.Success(list)
                    : Result.Failure<Tuple<int, IEnumerable<SparePart>>>(SparePartContextExceptionEnum.NoSparePartsFound.GetErrorMessage());
            }
        }

    }
}
