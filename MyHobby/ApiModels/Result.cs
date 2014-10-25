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

    public class Result<T>
    {        
        public Result(bool success, T item)
        {
            this.Success = success;
            this.Item = item;
        }

        public Result(bool success, string message)         
        {
            this.Success = success;
            this.Message = message;
        }

        public bool Success { get; set; }
        public string Message { get; set; }
        public T Item { get; set; }
    }
}