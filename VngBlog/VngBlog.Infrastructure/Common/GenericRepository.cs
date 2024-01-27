
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Tls;
using VngBlog.Contract.Common;
using VngBlog.Contract.Domains;
using VngBlog.Infrastructure.EntityFrameworkCore;

namespace VngBlog.Infrastructure.Common
{
	public class GenericRepository<T, K> : IGenericRepository<T, K> where T : EntityBase<K>
	{
		private readonly VngBlogDbContext _dbContext;
		private readonly IUnitOfWork _unitOfWork;
		public GenericRepository(VngBlogDbContext dbContext, IUnitOfWork unitOfWork)
		{
			_dbContext = dbContext;
			_unitOfWork = unitOfWork;
		}

		public IQueryable<T> FindAll(bool trackChanges = false) =>
		 !trackChanges ? _dbContext.Set<T>().AsNoTracking() : _dbContext.Set<T>();

		public IQueryable<T> FindAll(bool trackChanges = false, params Expression<Func<T, object>>[] includeProperties)
		{
			var items = FindAll(trackChanges);
			items = includeProperties.Aggregate(items, (current, includeProperty) => current.Include(includeProperty));
			return items;
		}

		public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false) =>
		!trackChanges ? _dbContext.Set<T>().Where(expression).AsNoTracking() : _dbContext.Set<T>().Where(expression);
		public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false, params Expression<Func<T, object>>[] includeProperties)
		{
			var items = FindByCondition(expression, trackChanges);
			items = includeProperties.Aggregate(items, (current, includeProperty) => current.Include(includeProperty));
			return items;
		}
		public async Task<T?> GetByIdAsync(K id) => await FindByCondition(x => x.Id.Equals(id)).FirstOrDefaultAsync();

		public async Task<T?> GetByIdAsync(K id, params Expression<Func<T, object>>[] includeProperties) =>
		await FindByCondition(x => x.Id.Equals(id), trackChanges: false, includeProperties).FirstOrDefaultAsync();


		public async Task<K> CreateAsync(T entity)
		{
			await _dbContext.Set<T>().AddAsync(entity);
			return entity.Id;
		}

		public async Task<IList<K>> CreateListAsync(IEnumerable<T> entities)
		{
			await _dbContext.Set<T>().AddRangeAsync(entities);
			return entities.Select(x => x.Id).ToList();
		}

		public Task UpdateAsync(T entity)
		{
			if (_dbContext.Entry(entity).State == EntityState.Unchanged) return Task.CompletedTask;
			T exist = _dbContext.Set<T>().Find(entity.Id);
			_dbContext.Entry(exist).CurrentValues.SetValues(entity);
			return Task.CompletedTask;
		}
		public Task UpdateListAsync(IEnumerable<T> entities) => _dbContext.Set<T>().AddRangeAsync(entities);

		public Task DeleteAsync(T entity)
		{
			_dbContext.Set<T>().Remove(entity);
			return Task.CompletedTask;
		}
		public Task DeleteListAsync(IEnumerable<T> entities)
		{
			_dbContext.Set<T>().RemoveRange(entities);
			return Task.CompletedTask;
		}

		public Task<int> SaveChangesAsync()
		{
			return _unitOfWork.CommitAsync();
		}
	}
}
