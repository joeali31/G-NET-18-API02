using E_Commerce01.Domain.Contract.Repositories;
using E_Commerce01.Domain.Entities.Base;
using E_Commerce01.Domain.Specifications;
using E_Commerce01.Infrastructure.Data;
using E_Commerce01.Infrastructure.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce01.Infrastructure.Repositories
{
    public class GenericRepository<TEntity, Tkey>(StoreDbContext context) : IGenericRepository<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {

        public async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken ct = default)
        => await context.Set<TEntity>().ToListAsync(ct);

        public async Task<TEntity?> GetByIdAsync(Tkey id, CancellationToken ct = default)
        => await context.Set<TEntity>().FindAsync(id, ct);

        public void Add(TEntity entity) => context.Set<TEntity>().Add(entity);

        public void Update(TEntity entity) => context.Set<TEntity>().Update(entity);

        public void Delete(TEntity entity) => context.Set<TEntity>().Remove(entity);

        public async Task<IReadOnlyList<TEntity>> GetAllAsync(ISpecifications<TEntity, Tkey> specs, CancellationToken ct = default)
        {
            return await SpecificationEvaluator.CreateQuery(context.Set<TEntity>() , specs).ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(ISpecifications<TEntity, Tkey> specs, CancellationToken ct = default)
        {
            return await SpecificationEvaluator.CreateQuery(context.Set<TEntity>() , specs).FirstOrDefaultAsync(ct);
        }
    }
}
