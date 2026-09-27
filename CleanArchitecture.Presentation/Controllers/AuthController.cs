using CleanArchitecture.Application.Features.AuthFeatures.Commands.Register;
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
    public sealed  class AuthController : ApiController
    {
        public AuthController(IMediator mediator) : base(mediator)
        {
        }
        [HttpPost("login")]
        public async Task<IActionResult> Register(RegisterCommand command, CancellationToken cancellationToken)
        {
            MessageResponse response = await _mediator.Send(command, cancellationToken);
            return Ok(response);
        }
    }
}
