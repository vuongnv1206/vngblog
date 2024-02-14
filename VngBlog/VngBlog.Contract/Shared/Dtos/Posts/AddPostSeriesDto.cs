using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VngBlog.Contract.Shared.Dtos.Posts
{
    public class AddPostSeriesDto
    {
        public int PostId { get; set; }
        public int SeriesId { get; set; }
    }
}
