using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Enums;

namespace Core.Transfer
{
    public class Result<T>
    {
        public T Data { get; set; }
        public Status Status { get; set; }

        public Result(T data,Status status)
        {
            Data = data;
            Status = status;
        }
    }
}
