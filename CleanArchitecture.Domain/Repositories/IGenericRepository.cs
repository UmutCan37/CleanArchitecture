using CleanArchitecture.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Domain.Repositories
{
    public interface IGenericRepository<T> where T : Entity
    {
        IQueryable<T> GetAll();
        Task<List<T>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);

    }
}
