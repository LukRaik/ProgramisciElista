using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Core.Enums;
using Core.Transfer;
using Core.Transfer.User;
using Data.Converters;
using ProgramisciElista.Session;

namespace ProgramisciElista.Controllers
{
    public class UserController:ApiController
    {
        private IUserService _userService;
        private ISessionService _sessionService;

        public UserController(IUserService userService, ISessionService sessionService)
        {
            this._userService = userService;
            _sessionService = sessionService;
        }

        [HttpPost]
        [AllowAnonymous]
        public Result<UserLoginResultDto> Login(UserLoginDto dto)
        {
            var token = _sessionService.Login(dto.Email, dto.Password);

            if (token == null)
            {
                return new UserLoginResultDto().AsResult(Status.Error);
            }
            var user = _sessionService.SessionCheck(token);
            return (new UserLoginResultDto()
            {
                Token = token,
                User = user.MapToDto()
            }).AsResult(Status.Ok);
        }
        [HttpPost]
        public Result<object> Logout()
        {
            _sessionService.LogOut(((Identity)User.Identity).User.Id);

            return new object().AsResult(Status.Ok);
        }
    }
}
