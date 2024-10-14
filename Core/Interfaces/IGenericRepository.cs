using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(int id);

        Task<IReadOnlyList<T>> ListAllAsync();

        Task<T?> GetEntityWithSpec(ISpecification<T> spec); // one entity

        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);

        void Add(T entity);

        void Update(T entity);

        void Remove(T entity);

        Task<bool> SaveAllAsync();

        bool Exist(int id);




    }
}