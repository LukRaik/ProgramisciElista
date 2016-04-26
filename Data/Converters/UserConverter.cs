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
                Firstname = user.Firstname
            };
        }
    }
}
