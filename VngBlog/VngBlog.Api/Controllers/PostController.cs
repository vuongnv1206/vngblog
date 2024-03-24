using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Logging;
using VngBlog.Contract.Shared.Dtos.Posts;
using VngBlog.Domain.Entities;
using VngBlog.Domain.Entities.Systems;
using VngBlog.Infrastructure.EntityFrameworkCore;

namespace VngBlog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly VngBlogDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        public PostController(VngBlogDbContext context, UserManager<AppUser> userManager, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var data = await _context.Posts
                .Include(p => p.Author)
                .Include(x => x.PostCategories).ThenInclude(x => x.Category)
                .Include(x => x.PostTags).ThenInclude(x => x.Tag)
                .OrderByDescending(x => x.CreatedTime)
                .ToListAsync();

            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(int id)
        {
            var post = await _context.Posts
                .Include(p => p.Author)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUpdatePostDto postDto)
        {

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                var post = _mapper.Map<Post>(postDto);
                post.AuthorId = user.Id;
                post.Status = PostStatus.Published;
                _context.Add(post);

                if (postDto.CategoryIds != null)
                {
                    foreach (var cateId in postDto.CategoryIds)
                    {
                        _context.Add(new PostCategory()
                        {
                            CategoryId = cateId,
                            Post = post
                        });
                    }
                }

                await _context.SaveChangesAsync();
                return Ok(post); 
            }

            return BadRequest(ModelState);
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound(); 
            }

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return Ok(); 
        }
    }
}
    

