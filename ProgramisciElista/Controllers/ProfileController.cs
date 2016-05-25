﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Core.Enums;
using Core.Interfaces;
using Core.Transfer;
using Core.Transfer.User;
using Data.Converters;
using ProgramisciElista.Interfaces;
using ProgramisciElista.Session;

namespace ProgramisciElista.Controllers
{
    public class ProfileController:LoggingApiController
    {
        private readonly ISessionService _sessionService;
        private readonly IUserService _userService;
        private readonly IWorkLoggingService _workLoggingService;

        public ProfileController(ILogger logger, IUserService userService, ISessionService sessionService, IWorkLoggingService workLoggingService) : base(logger)
        {
            _userService = userService;
            _sessionService = sessionService;
            _workLoggingService = workLoggingService;
        }

        [HttpPost]
        public JsonResult<UserBasicDto> Update(UserBasicDto dto)
        {
            Log($"{User.Identity.Name} updating profile");
            var id = ((Identity) User.Identity).User.Id;
            _userService.Update(id, x =>
            {
                x.Email = dto.Email;
                x.Firstname = dto.Firstname;
                x.Lastname = dto.Lastname;
                return x;
            });

            return _userService.Get(id).MapToBasicDto().AsOkResult();
        }

        [HttpPost]
        public JsonResult<object> ChangePassword(PasswordDto passwordDto)
        {
            Log($"{User.Identity.Name} change password");
            if (passwordDto.OldPassword == passwordDto.OldPasswordSecond)
            {
                var user = _userService.Login(((Identity) User.Identity).User.Email, passwordDto.OldPassword);
                if (user != null)
                {
                    _userService.Update(user.Value, x => x.Password, passwordDto.NewPassword);
                    Log($"Password change ok!");
                    return ResultHelper.EmptyOk();
                }
                return ResultHelper.ErrorResult("Password is not valid!");
            }
            return ResultHelper.ErrorResult("Password match failed!");
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult<UserLoginResultDto> Login(UserLoginDto dto)
        {
            Log($"{dto.Email} trying to login");
            var token = _sessionService.Login(dto.Email, dto.Password);

            if (token == null)
            {
                return new UserLoginResultDto().AsResult(Status.Error);
            }
            var user = _sessionService.SessionCheck(token);
            if (user.UserGroup.GroupName=="User") _workLoggingService.StartLogWork(user.Id);
            return (new UserLoginResultDto()
            {
                Token = token,
                User = user.MapToDto(),
                Group = user.UserGroup.GroupName
            }).AsResult(Status.Ok);
        }
        [HttpPost]
        public JsonResult<object> Logout(string msg)
        {
            Log($"Logging out {User.Identity.Name}");
            if(User.IsInRole("User")) _workLoggingService.EndLogWork(((Identity)User.Identity).User.Id,msg);
            _sessionService.LogOut(((Identity)User.Identity).User.Id);

            return new object().AsResult(Status.Ok);
        }
    }
}
