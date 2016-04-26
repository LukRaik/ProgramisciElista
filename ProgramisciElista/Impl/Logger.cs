using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;

namespace ProgramisciElista.Impl
{
    public class Logger:ILogger
    {
        public void Log(string log, string logtype = "log")
        {
            Console.WriteLine($"[{logtype}]:{log}");
        }
    }
}
