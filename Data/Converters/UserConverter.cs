using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Transfer.User;

namespace Data.Converters
{
    public static class UserConverter
    {
        public static UserDto MapToDto(this User user)
        {
            return new UserDto()
            {
                Email = user.Email,
                Id = user.Id,
                Lastname = user.Lastname,
                TechDate = user.TechDate,
                Firstname = user.Firstname,
                IsActivated = user.IsActive
            };
        }

        public static UserBasicDto MapToBasicDto(this User user)
        {
            return new UserBasicDto()
            {
                Email = user.Email,
                Lastname = user.Lastname,
                TechDate = user.TechDate,
                Firstname = user.Firstname,
                IsActivated = user.IsActive
            };
        }

        public static UserLoggedDto MapToDto(this KeyValuePair<User, bool> user)
        {
            return new UserLoggedDto()
            {
                Email = user.Key.Email,
                Lastname = user.Key.Lastname,
                TechDate = user.Key.TechDate,
                Firstname = user.Key.Firstname,
                IsActivated = user.Key.IsActive,
                IsActive = user.Value,
                Id = user.Key.Id
            };
        }
    }
}
