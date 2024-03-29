﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VngBlog.Domain.Abstractions.Interfaces;

namespace VngBlog.Domain.Abstractions
{
    public abstract class EntityAuditBase<T> : EntityBase<T>, IAuditable
    {
        public DateTimeOffset? CreatedTime { get; set; }
        public DateTimeOffset? LastModifiedTime { get; set; }
        public string? CreatedBy { get; set; }
        public string? LastModifiedBy { get; set; }
    }
}
