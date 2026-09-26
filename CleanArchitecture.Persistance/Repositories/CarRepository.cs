using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Repositories;
using CleanArchitecture.Persistence.Context;

namespace CleanArchitecture.Persistence.Repositories;

public sealed class CarRepository : GenericRepository<Car>, ICarRepository
{
    public CarRepository(AppDbContext context) : base(context) { }
}