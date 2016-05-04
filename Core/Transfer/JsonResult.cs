using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Enums;

namespace Core.Transfer
{
    public class JsonResult<T>
    {
        public T Data { get; set; }
        public Status Status { get; set; }

        public JsonResult(T data,Status status)
        {
            Data = data;
            Status = status;
        }
    }
}
