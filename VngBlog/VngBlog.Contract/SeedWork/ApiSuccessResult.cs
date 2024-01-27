﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VngBlog.Contract.SeedWork
{
    public class ApiSuccessResult<T> : ApiResult<T>
    {
        public ApiSuccessResult(T data) : base(true, data, "Success")
        {

        }
        public ApiSuccessResult(T data, string message) : base(true, data, message)
        {

        }

    }
}
