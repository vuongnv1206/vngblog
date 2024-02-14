using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VngBlog.Contract.Common;
using VngBlog.Contract.Shared.Dtos.Tags;
using VngBlog.Domain.Entities.Systems;

namespace VngBlog.Contract.Repositories
{
    public interface ITagRepository : IGenericRepository<Tag, int>
    {
        Task<TagDto> GetByName(string name);
    }
}
