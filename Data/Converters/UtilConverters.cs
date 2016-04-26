using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Enums;
using Core.Transfer;

namespace Data.Converters
{
    public static class UtilConverters
    {
        public static Result<T> AsResult<T>(this T data, Status status)
        {
            return new Result<T>(data,status);
        }
    }
}
