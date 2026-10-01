using CleanArchitecture.Application.Features.RoleFeatures.Commands.CreateRole;
using CleanArchitecture.Application.Service;
using CleanArchitecture.Domain.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Persistence.Services;

public sealed class RoleService : IRoleService
{
    private readonly RoleManager<Role> _roleManager;

    public RoleService(RoleManager<Role> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task CreateAsync(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var result = await _roleManager.CreateAsync(new Role { Name = request.Name });

        if (!result.Succeeded)
        {
            var failures = result.Errors
                .Select(e => new ValidationFailure(e.Code, e.Description));

            throw new ValidationException(failures);
        }
    }
}