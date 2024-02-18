using Bogus;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VngBlog.Domain.Entities;
using VngBlog.Domain.Entities.Systems;
using VngBlog.Infrastructure.EntityFrameworkCore;

namespace VngBlog.Infrastructure.Seedings
{
    public class SeedingDataPostCategory
    {
       
        public async Task SeedData(VngBlogDbContext _dbContext)
        {

         
             _dbContext.Posts.RemoveRange(_dbContext.Posts.Where(p => p.Content.Contains("[fakeData]")));

            await _dbContext.SaveChangesAsync();

            var fakerCategory = new Faker<Category>();
            int cm = 1;
            fakerCategory.RuleFor(c => c.Name, fk => $"CM{cm++} " + fk.Lorem.Sentence(1, 2).Trim('.'));
            fakerCategory.RuleFor(x => x.IsActive ,true);
            fakerCategory.RuleFor(c => c.Slug, fk => fk.Lorem.Slug());



            var cate1 = fakerCategory.Generate();
            var cate11 = fakerCategory.Generate();
            var cate12 = fakerCategory.Generate();
            var cate2 = fakerCategory.Generate();
            var cate21 = fakerCategory.Generate();
            var cate211 = fakerCategory.Generate();


            cate11.CategoryParent = cate1;
            cate12.CategoryParent = cate1;
            cate21.CategoryParent = cate2;
            cate211.CategoryParent = cate21;

            var categories = new Category[] { cate1, cate2, cate12, cate11, cate21, cate211 };
            _dbContext.Categories.AddRange(categories);



            // POST
            var rCateIndex = new Random();
            int bv = 1;

            var fakerPost = new Faker<Post>();
            fakerPost.RuleFor(p => p.AuthorId, "140fb889-6709-4b0e-863b-bd66b530d290");
            fakerPost.RuleFor(p => p.CreatedBy, "Admin");
            fakerPost.RuleFor(p => p.Content, f => f.Lorem.Paragraphs(7) + "[fakeData]");

            fakerPost.RuleFor(p => p.CreatedTime, f => f.Date.Between(new DateTime(2024, 1, 1), new DateTime(2024, 7, 1)));
            fakerPost.RuleFor(p => p.Description, f => f.Lorem.Sentences(3));
            fakerPost.RuleFor(p => p.Status, PostStatus.Published);
            fakerPost.RuleFor(p => p.Slug, f => f.Lorem.Slug());
            fakerPost.RuleFor(p => p.Name, f => $"Bài {bv++} " + f.Lorem.Sentence(3, 4).Trim('.'));

            List<Post> posts = new List<Post>();
            List<PostCategory> post_categories = new List<PostCategory>();


            for (int i = 0; i < 40; i++)
            {
                var post = fakerPost.Generate();
                post.LastModifiedTime = post.CreatedTime;
                posts.Add(post);
                post_categories.Add(new PostCategory()
                {
                    Post = post,
                    Category = categories[rCateIndex.Next(5)]
                });
            }

            await _dbContext.AddRangeAsync(posts);
            await _dbContext.AddRangeAsync(post_categories);
            // END POST



            await _dbContext.SaveChangesAsync();

        }
    }
}
