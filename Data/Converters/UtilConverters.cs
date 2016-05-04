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
        public static JsonResult<T> AsResult<T>(this T data, Status status)
        {
            return new JsonResult<T>(data,status);
        }

        public static JsonResult<T> AsOkResult<T>(this T data)
        {
            return new JsonResult<T>(data, Status.Ok);
        }
    }
}
