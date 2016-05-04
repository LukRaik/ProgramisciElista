using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Core.Enums;
using Core.Interfaces;
using Core.Transfer;
using Core.Transfer.User;
using Data;
using Data.Converters;
using ProgramisciElista.Filters;
using ProgramisciElista.Session;

namespace ProgramisciElista.Controllers
{
    [Role("Admin")]
    public class AdminController:LoggingApiController
    {
        private readonly IUserService _userService;

        private readonly ISessionService _sessionService;

        public AdminController(IUserService userService, ISessionService sessionService, ILogger logger) : base(logger)
        {
            _userService = userService;
            _sessionService = sessionService;
        }

        public JsonResult<List<UserLoggedDto>> GetUsers()
        {
            Log("Get users");
            return _sessionService.GetUsersWithActivity().Select(x => x.MapToDto()).ToList().AsResult(Status.Ok);
        }

        [HttpPut]
        public JsonResult<object> ActivateUser(int id)
        {
            Log($"Activate user:{id}");
            if (_userService.Get(id) != null)
            {
                _userService.Activate(id);
                return ResultHelper.EmptyResult(Status.Ok);
            }
            LogError("Failed");
            return ResultHelper.ErrorResult("Wrong user id");
        }

        [HttpPut]
        public JsonResult<object> DeactivateUser(int id)
        {
            Log($"Deactivate user:{id}");
            if (_userService.Get(id) != null)
            {
                _userService.Deactivate(id);
                _sessionService.LogOut(id);
                return ResultHelper.EmptyResult(Status.Ok);
            }
            LogError("Failed");
            return ResultHelper.ErrorResult("Wrong user id");
        }

        [HttpPost]
        public JsonResult<UserDto> Create(UserCreateDto dto)
        {
            Log($"Creating user by {User.Identity.Name}");
            var user = _userService.Create(new User()
            {
                Email = dto.Email,
                Firstname = dto.Firstname,
                TechDate = DateTime.Now,
                IsActive = true,
                Lastname = dto.Lastname,
                Password = dto.Password,
            });
            return user.MapToDto().AsResult(Status.Ok);
        }

        [HttpPost]
        public JsonResult<UserDto> Update(UserDto dto)
        {
            Log($"Update user by {User.Identity.Name}");
            
            _userService.Update(dto.Id, x =>
            {
                x.Email = dto.Email;
                x.Firstname = dto.Firstname;
                x.Lastname = dto.Lastname;
                return x;
            });

            return _userService.Get(dto.Id).MapToDto().AsOkResult();
        }
    }
}
