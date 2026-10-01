using CleanArchitecture.Application.Features.UserRoleFeatures.Commands.CreateUserRole;
using CleanArchitecture.Presentation.Abstraction;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Presentation.Controllers;

public sealed class UserRolesController : ApiController
{
    public UserRolesController(IMediator mediator) : base(mediator) { }

    [HttpPost]
    public async Task<IActionResult> Create(CreateUserRoleCommand request, CancellationToken cancellationToken)
        => Ok(await _mediator.Send(request, cancellationToken));
}