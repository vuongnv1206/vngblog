using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using VngBlog.Domain.Entities.Systems;

namespace VngBlog.Contract.Shared.Dtos.PostActivities
{
    public class PostActivityLogDto
    {
        public PostStatus FromStatus { set; get; }
        public PostStatus ToStatus { set; get; }
        public string? Note { set; get; }
        public DateTimeOffset? CreatedTime { get; set; }
        public string? CreatedBy { get; set; }

    }
}
