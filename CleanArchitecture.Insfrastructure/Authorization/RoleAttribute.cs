using CleanArchitecture.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;
using CleanArchitecture.Domain.Repositories;

namespace CleanArchitecture.Infrastructure.Authorization;

public sealed class RoleAttribute : IAsyncAuthorizationFilter
{
    private readonly string _role;
    private readonly IGenericRepository<UserRole> _userRoleRepository;

    public RoleAttribute(string role, IGenericRepository<UserRole> userRoleRepository)
    {
        _role = role;
        _userRoleRepository = userRoleRepository;
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var userIdClaim = context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(userIdClaim, out var userId))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var userHasRole = await _userRoleRepository
            .GetAll()
            .AnyAsync(p => p.UserId == userId && p.Role.Name == _role,
                      context.HttpContext.RequestAborted);

        if (!userHasRole)
            context.Result = new ForbidResult();
    }
}