﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VngBlog.Contract.Domains;

namespace VngBlog.Contract.Common
{
	public interface IGenericRepository<T, K> where T : EntityBase<K>
	{
		//Query
		IQueryable<T> FindAll(bool trackChanges = false);

		IQueryable<T> FindAll(bool trackChanges = false, params Expression<Func<T, object>>[] includeProperties);

		IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false);

		IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges = false, params Expression<Func<T, object>>[] includeProperties);

		Task<T?> GetByIdAsync(K id);
		Task<T?> GetByIdAsync(K id, params Expression<Func<T, object>>[] includeProperties);

		Task<K> CreateAsync(T entity);

		Task<IList<K>> CreateListAsync(IEnumerable<T> entities);

		Task UpdateAsync(T entity);

		Task UpdateListAsync(IEnumerable<T> entities);

		Task DeleteAsync(T entity);

		Task DeleteListAsync(IEnumerable<T> entities);

		Task<int> SaveChangesAsync();

	}
}
