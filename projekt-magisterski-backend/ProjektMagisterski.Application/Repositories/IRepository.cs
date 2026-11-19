using ProjektMagisterski.Core.Abstract;

namespace ProjektMagisterski.Application.Repositories;

public interface IRepository<TEntity> where TEntity : BaseEntity {
	Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

	Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

	Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);

	Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

	void Update(TEntity entity);

	void Delete(TEntity entity);

	Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}