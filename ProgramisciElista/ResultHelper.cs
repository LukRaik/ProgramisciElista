using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Enums;
using Core.Transfer;

namespace ProgramisciElista
{
    public static class ResultHelper
    {
        public static JsonResult<object> EmptyResult(Status status)
        {
            return new JsonResult<object>(null,status);
        }

        public static JsonResult<object> EmptyOk()
        {
            return new JsonResult<object>(null, Status.Ok);
        }

        public static JsonResult<object> ErrorResult(string msg = null)
        {
            return new JsonResult<object>(msg,Status.Ok);
        }
    }
}
