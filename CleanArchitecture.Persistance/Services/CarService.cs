using AutoMapper;
using CleanArchitecture.Application.Common;
using CleanArchitecture.Application.Features.CarFeatures.Commands.CreateCar;
using CleanArchitecture.Application.Features.CarFeatures.Queries.GetAllCar;
using CleanArchitecture.Application.Service;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Repositories;
using CleanArchitecture.Persistence.Context;
using CleanArchitecture.Persistence.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Persistence.Services
{
    public class CarService : ICarService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICarRepository _carRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CarService(AppDbContext context, IMapper mapper, ICarRepository carRepository, IUnitOfWork unitOfWork)
        {
            _context = context;
            _mapper = mapper;
            _carRepository = carRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task CreateAsync(CreateCarCommand request, CancellationToken cancellationToken)
        {
            var car = _mapper.Map<Car>(request);
            //_context.Set<Car>().Add(car);
            //await _context.SaveChangesAsync(cancellationToken);
            _carRepository.Add(car);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task<PaginationResult<Car>> GetAllAsync(GetAllCarQuery request, CancellationToken cancellationToken)
        {
            var query = _carRepository.GetAll();

            if (!string.IsNullOrWhiteSpace(request.Search))
                query = query.Where(p => p.Name.Contains(request.Search));

            return await query
                .OrderBy(p => p.Name)
                .ToPagedListAsync(request.PageNumber, request.PageSize, cancellationToken);
        }
    }
}
