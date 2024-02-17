using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VngBlog.Domain.Abstractions;

namespace VngBlog.Domain.Entities
{
    public class Contact : EntityAuditBase<int>
    {
        [StringLength(50)]
        public string FullName { get; set; }

        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }
        public string Message { get; set; }
        [Phone]
        public string Phone { get; set; }
    }
}
