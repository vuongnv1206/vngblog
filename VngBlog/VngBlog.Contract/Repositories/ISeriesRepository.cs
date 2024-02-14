using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VngBlog.Contract.Common;
using VngBlog.Contract.SeedWork;
using VngBlog.Contract.Shared.Dtos.Posts;
using VngBlog.Contract.Shared.Dtos.Series;
using VngBlog.Domain.Entities.Systems;

namespace VngBlog.Contract.Repositories
{
    public interface ISeriesRepository : IGenericRepository<Series, int>
    {
        Task<PagedList<SeriesDto>> GetAllPaging(string? keyword, int pageIndex = 1, int pageSize = 10);
        Task AddPostToSeries(Guid seriesId, Guid postId, int sortOrder);
        Task RemovePostToSeries(Guid seriesId, Guid postId);
        Task<List<PostInListDto>> GetAllPostsInSeries(Guid seriesId);
        Task<PagedList<PostInListDto>> GetAllPostsInSeries(string slug, int pageIndex = 1, int pageSize = 10);
        Task<SeriesDto> GetBySlug(string slug);

        Task<bool> IsPostInSeries(Guid seriesId, Guid postId);
        Task<bool> HasPost(Guid seriesId);
    }
}
