using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VngBlog.Contract.Shared.Dtos.Series
{
    public class SeriesDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        [MaxLength(250)]
        public string? Description { get; set; }

        public string? Slug { get; set; }
        public string? Image { set; get; }
        public string? Content { get; set; }
        public DateTimeOffset? CreatedTime { get; set; }
        public DateTimeOffset? LastModifiedTime { get; set; }
        public string? CreatedBy { get; set; }
        public string? LastModifiedBy { get; set; }
    }
}
