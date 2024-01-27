
using VngBlog.Contract.Domains.Interfaces;

namespace VngBlog.Contract.Domains
{
	public abstract class EntityBase<TKey> : IEntityBase<TKey>
	{
		public TKey Id { get; set; }
	}
}
