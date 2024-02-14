using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VngBlog.Domain.Entities;
using VngBlog.Domain.Entities.Systems;

namespace VngBlog.Infrastructure.Configurations.DomainConfigs
{
    public class PostCommentConfiguration : IEntityTypeConfiguration<PostComment>
    {
        public void Configure(EntityTypeBuilder<PostComment> builder)
        {
            builder.Property(x => x.Level)
               .HasDefaultValue(1)
               .IsRequired();
        }
    }
}
