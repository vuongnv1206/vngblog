using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VngBlog.Contract.Common;
using VngBlog.Contract.SeedWork;
using VngBlog.Contract.Shared.Dtos.Categories;
using VngBlog.Domain.Entities.Systems;

namespace VngBlog.Contract.Repositories
{
    public interface IPostCategoryRepository : IGenericRepository<PostCategory, int>
    {
        Task<PagedList<PostCategoryDto>> GetAllPaging(string? keyword, int pageIndex = 1, int pageSize = 10);
        Task<bool> HasPost(Guid categoryId);

        Task<PostCategoryDto> GetBySlug(string slug);

    }
}
