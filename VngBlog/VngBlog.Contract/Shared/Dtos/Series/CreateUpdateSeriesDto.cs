using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace VngBlog.Contract.Shared.Dtos.Series
{
    public class CreateUpdateSeriesDto
    {

        [MaxLength(250)]
        public required string Name { get; set; }

        public required string Description { get; set; }

        [MaxLength(250)]
        public required string Slug { get; set; }

        public string? Image { set; get; }
        public string? Content { get; set; }

    }
}