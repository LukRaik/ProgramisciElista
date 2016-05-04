using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Core.Interfaces;

namespace ProgramisciElista.Controllers
{
    public class LoggingApiController:ApiController
    {
        private readonly ILogger _logger;


        public LoggingApiController(ILogger logger)
        {
            _logger = logger;
        }

        [NonAction]
        public void Log(string msg)
        {
            _logger.Log(msg);
        }
        [NonAction]
        public void LogError(string msg)
        {
            _logger.Error(msg);
        }
    }
}
