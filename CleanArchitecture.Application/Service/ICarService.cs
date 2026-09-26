using CleanArchitecture.Application.Common;
using CleanArchitecture.Application.Features.CarFeatures.Commands.CreateCar;
using CleanArchitecture.Application.Features.CarFeatures.Queries.GetAllCar;
using CleanArchitecture.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Service
{
    public interface ICarService
    {
        Task CreateAsync(CreateCarCommand request, CancellationToken cancellationToken);
        Task<PaginationResult<Car>> GetAllAsync(GetAllCarQuery request,CancellationToken cancellationToken);


    }
}
