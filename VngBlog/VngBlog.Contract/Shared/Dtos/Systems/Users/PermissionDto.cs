using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VngBlog.Contract.Shared.Dtos.Systems.Roles;

namespace VngBlog.Domain.Entities.Systems
{
    public class PermissionDto
    {
        public string RoleId { get; set; }
        public IList<RoleClaimDto> RoleClaims { get; set; }
    }
}
