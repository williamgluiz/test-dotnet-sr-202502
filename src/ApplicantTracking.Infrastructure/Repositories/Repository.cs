using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicantTracking.Domain.Interfaces;
using ApplicantTracking.Domain.Models;
using ApplicantTracking.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace ApplicantTracking.Infrastructure.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected readonly AppDbContext Db;
        protected readonly DbSet<TEntity> DbSet;

        /// <summary>
        /// Initializes a new instance of the generic <see cref="Repository{TEntity}"/> class,
        /// setting up the provided <see cref="AppDbContext"/> and the corresponding <see cref="DbSet{TEntity}"/> for data operations.
        /// </summary>
        /// <param name="db">The <see cref="AppDbContext"/> used to access the database.</param>
        protected Repository(AppDbContext db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
        }

        /// <summary>
        /// Asynchronously retrieves all entities of type <typeparamref name="TEntity"/> from the database.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of all entities.</returns>
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
            => await DbSet.ToListAsync();

        /// <summary>
        /// Asynchronously retrieves an entity of type <typeparamref name="TEntity"/> by its primary key.
        /// </summary>
        /// <param name="id">The identifier of the entity to retrieve.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the entity if found; otherwise, <c>null</c>.</returns>
        public virtual async Task<TEntity> GetByIdAsync(int id)
            => await DbSet.FindAsync(id);

        /// <summary>
        /// Asynchronously adds a new entity of type <typeparamref name="TEntity"/> to the database context.
        /// </summary>
        /// <param name="entity">The entity to be added.</param>
        /// <returns>A task that represents the asynchronous add operation.</returns>
        public virtual async Task AddAsync(TEntity entity)
        {
            DbSet.Add(entity);
        }

        /// <summary>
        /// Asynchronously updates an existing entity of type <typeparamref name="TEntity"/> in the database context.
        /// </summary>
        /// <param name="entity">The entity to be updated.</param>
        /// <returns>A task that represents the asynchronous update operation.</returns>
        public virtual async Task UpdateAsync(TEntity entity)
        {
            DbSet.Update(entity);
        }

        /// <summary>
        /// Asynchronously deletes an entity of type <typeparamref name="TEntity"/> from the database context by its identifier.
        /// </summary>
        /// <param name="id">The identifier of the entity to be deleted.</param>
        /// <returns>A task that represents the asynchronous delete operation.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the entity is not found.</exception>
        public virtual async Task DeleteAsync(int id)
        {
            var entity = await DbSet.FindAsync(id);

            if (entity == null)
                throw new InvalidOperationException("Entity not found");

            DbSet.Remove(entity);
        }

        /// <summary>
        /// Disposes the database context, releasing all managed resources.
        /// </summary>
        public void Dispose()
            => Db?.Dispose();
    }
}
