using CleanArchitecture.Application.Features.RoleFeatures.Commands.CreateRole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Service
{
    public interface IRoleService
    {
        Task CreateAsync(CreateRoleCommand request, CancellationToken cancellationToken);
    }
}
