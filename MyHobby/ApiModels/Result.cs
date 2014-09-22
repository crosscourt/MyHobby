using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyHobby.ApiModels
{
    public class Result
    {
        public static Result Succeed = new Result(true);

        public Result(bool success)
        {
            this.Success = success;
        }

        public Result(bool success, string message)
            : this(success)
        {
            this.Message = message;
        }

        public bool Success { get; set; }
        public string Message { get; set; }

    }
}