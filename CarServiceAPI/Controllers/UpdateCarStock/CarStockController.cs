﻿using AutoMapper;
using CarServiceAPI.MiddleWare;
using CarServiceAPI.Models;
using CarServiceApplication.Commands;
using CarServiceDomain.Entities;
using CarServiceDomain.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarServiceAPI.Controllers.UpdateCar
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarStockController : ControllerBase
    {
        private readonly ICarStockService _carService;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        public CarStockController(ICarStockService carService, IMapper mapper, IMediator mediator)
        {
            _carService = carService;
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPut]
        [Route("UpdateCar")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomResponse<bool>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CustomResponse<object>))]
        public async Task<IActionResult> UpdateCar([FromBody] CarStockModel model)
        {
            var carUpdated = await _mediator.Send(new UpdateCarStockCommand(
                Guid.Parse(model.Id),
                model.Colour));

            if (carUpdated.IsFailure)
                return BadRequest(carUpdated.Error);
            return Ok(CustomResponse<bool>.BuildSuccess(carUpdated.Value));
        }
    }
}
