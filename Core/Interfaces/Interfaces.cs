using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ILogger
    {
        void Log(string log, string logtype = "log");

        void Error(string errorMsg);
    }
}
