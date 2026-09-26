using CleanArchitecture.Application.Features.CarFeatures.Commands.CreateCar;
using CleanArchitecture.Application.Features.CarFeatures.Queries.GetAllCar;
using CleanArchitecture.Domain.Dtos;
using CleanArchitecture.Presentation.Abstraction;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Presentation.Controllers
{
    public sealed class CarsController : ApiController
    {
        public CarsController(IMediator mediator) : base(mediator)
        {
        }
        [HttpPost]
        public async Task<IActionResult> CreateCar(CreateCarCommand command, CancellationToken cancellationToken)
        {
            MessageResponse response = await _mediator.Send(command, cancellationToken);
            return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllCarQuery request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);
        }
    }
}
