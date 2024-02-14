using VngBlog.Domain.Abstractions.Interfaces;

namespace VngBlog.Domain.Abstractions
{
    public abstract class EntityBase<TKey> : IEntityBase<TKey>
    {
        public TKey Id { get; set; }
    }
}
