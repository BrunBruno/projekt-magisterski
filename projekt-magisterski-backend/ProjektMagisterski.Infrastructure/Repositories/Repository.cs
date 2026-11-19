using Microsoft.EntityFrameworkCore;
using ProjektMagisterski.Application.Repositories;
using ProjektMagisterski.Core.Abstract;
using ProjektMagisterski.Infrastructure.Contexts;

namespace ProjektMagisterski.Infrastructure.Repositories;

public class Repository<TEntity> : IRepository<TEntity>
	where TEntity : BaseEntity {
	protected readonly AppDbContext Context;
	protected readonly DbSet<TEntity> DbSet;

	public Repository(AppDbContext context) {
		Context = context;
		DbSet = context.Set<TEntity>();
	}

	public virtual async Task<TEntity?> GetByIdAsync(
		Guid id,
		CancellationToken cancellationToken = default) {
		return await DbSet.FirstOrDefaultAsync(
			x => x.Id == id,
			cancellationToken);
	}

	public virtual async Task<IReadOnlyList<TEntity>> GetAllAsync(
		CancellationToken cancellationToken = default) {
		return await DbSet.AsNoTracking()
			.ToListAsync(cancellationToken);
	}

	public virtual async Task<bool> ExistsAsync(
		Guid id,
		CancellationToken cancellationToken = default) {
		return await DbSet.AnyAsync(
			x => x.Id == id,
			cancellationToken);
	}

	public virtual async Task AddAsync(
		TEntity entity,
		CancellationToken cancellationToken = default) {
		await DbSet.AddAsync(entity, cancellationToken);
	}

	public virtual void Update(TEntity entity) {
		DbSet.Update(entity);
	}

	public virtual void Delete(TEntity entity) {
		DbSet.Remove(entity);
	}

	public virtual Task<int> SaveChangesAsync(
		CancellationToken cancellationToken = default) {
		return Context.SaveChangesAsync(cancellationToken);
	}
}
