using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicantTracking.Domain.Models;

namespace ApplicantTracking.Domain.Interfaces
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        /// <summary>
        /// Retrieves all entities of type <typeparamref name="TEntity"/> asynchronously.
        /// </summary>
        /// <returns>A task representing the asynchronous operation, containing an enumerable of all entities.</returns>
        Task<IEnumerable<TEntity>> GetAllAsync();

        /// <summary>
        /// Retrieves an entity of type <typeparamref name="TEntity"/> by its identifier asynchronously.
        /// </summary>
        /// <param name="id">The identifier of the entity to retrieve.</param>
        /// <returns>A task representing the asynchronous operation, containing the entity if found; otherwise, null.</returns>
        Task<TEntity> GetByIdAsync(int id);

        /// <summary>
        /// Adds a new entity of type <typeparamref name="TEntity"/> asynchronously.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task AddAsync(TEntity entity);

        /// <summary>
        /// Updates an existing entity of type <typeparamref name="TEntity"/> asynchronously.
        /// </summary>
        /// <param name="entity">The entity with updated values.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task UpdateAsync(TEntity entity);

        /// <summary>
        /// Deletes an entity of type <typeparamref name="TEntity"/> by its identifier asynchronously.
        /// </summary>
        /// <param name="id">The identifier of the entity to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task DeleteAsync(int id);
    }
}
