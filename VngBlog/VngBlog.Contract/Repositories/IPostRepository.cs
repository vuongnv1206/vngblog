using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VngBlog.Contract.Common;
using VngBlog.Contract.SeedWork;
using VngBlog.Contract.Shared.Dtos.PostActivities;
using VngBlog.Contract.Shared.Dtos.Posts;
using VngBlog.Contract.Shared.Dtos.Series;
using VngBlog.Contract.Shared.Dtos.Tags;
using VngBlog.Domain.Entities.Systems;

namespace VngBlog.Contract.Repositories
{
    public interface IPostRepository : IGenericRepository<Post, int>
    {
        Task<PagedList<PostInListDto>> GetAllPaging(string? keyword, Guid currentUserId, Guid? categoryId, int pageIndex = 1, int pageSize = 10);
        Task<List<SeriesDto>> GetAllSeries(Guid postId);
        Task Approve(Guid id, Guid currentUserId);
        Task SendToApprove(Guid id, Guid currentUserId);
        Task ReturnBack(Guid id, Guid currentUserId, string note);
        Task<string> GetReturnReason(Guid id);
        Task<bool> HasPublishInLast(Guid id);
        Task<List<PostActivityLogDto>> GetActivityLogs(Guid id);
        Task<List<Post>> GetListUnpaidPublishPosts(Guid userId);

        Task<List<PostInListDto>> GetLatestPublishPost(int top);

        Task<PagedList<PostInListDto>> GetPostByCategoryPaging(string categorySlug, int pageIndex = 1, int pageSize = 10);

        Task<PostDto> GetBySlug(string slug);

        Task<List<string>> GetAllTags();

        Task AddTagToPost(Guid postId, Guid tagId);

        Task<List<string>> GetTagsByPostId(Guid postId);

        Task<List<TagDto>> GetTagObjectsByPostId(Guid postId);

        Task<PagedList<PostInListDto>> GetPostByTagPaging(string tagSlug, int pageIndex = 1, int pageSize = 10);
        Task<PagedList<PostInListDto>> GetPostByUserPaging(string keyword, Guid userId, int pageIndex = 1, int pageSize = 10);


    }
}
