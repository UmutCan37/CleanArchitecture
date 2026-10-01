using CleanArchitecture.Application.Features.UserRoleFeatures.Commands.CreateUserRole;
using CleanArchitecture.Application.Service;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Repositories;
using CleanArchitecture.Persistence.Context;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Persistence.Services;

public sealed class UserRoleService : IUserRoleService
{
    private readonly AppDbContext _context;
    private readonly IGenericRepository<UserRole> _userRoleRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UserRoleService(AppDbContext context, IGenericRepository<UserRole> userRoleRepository, IUnitOfWork unitOfWork)
    {
        _context = context;
        _userRoleRepository = userRoleRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task CreateAsync(CreateUserRoleCommand request, CancellationToken cancellationToken)
    {
        var userExists = await _context.Users.AnyAsync(u => u.Id == request.UserId, cancellationToken);
        if (!userExists)
            throw new ValidationException([new ValidationFailure(nameof(request.UserId), "Kullanıcı bulunamadı.")]);

        var roleExists = await _context.Roles.AnyAsync(r => r.Id == request.RoleId, cancellationToken);
        if (!roleExists)
            throw new ValidationException([new ValidationFailure(nameof(request.RoleId), "Rol bulunamadı.")]);

        var alreadyAssigned = await _context.Set<UserRole>()
            .AnyAsync(ur => ur.UserId == request.UserId && ur.RoleId == request.RoleId, cancellationToken);
        if (alreadyAssigned)
            throw new ValidationException([new ValidationFailure(nameof(request.RoleId), "Bu rol kullanıcıya zaten atanmış.")]);

        _userRoleRepository.Add(new UserRole
        {
            UserId = request.UserId,
            RoleId = request.RoleId
        });

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}