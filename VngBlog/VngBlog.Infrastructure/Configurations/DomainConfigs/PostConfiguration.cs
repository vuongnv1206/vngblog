using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VngBlog.Domain.Entities.Systems;

namespace VngBlog.Infrastructure.Configurations.DomainConfigs
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.Property(x => x.Status)
                .HasDefaultValue(PostStatus.WaitingForApproval)
                .IsRequired();
            builder.Property(x => x.ViewCount)
               .HasDefaultValue(0)
               .IsRequired();
        }
    }
}
