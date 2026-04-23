

using Movies.Application.DTOs.Auth;
using Movies.Application.DTOs.User;
using Movies.Application.DTOs.UserAdmin;
using Movies.Domain.Entities;

namespace Movies.Application.Mappings
{
    public class UserProfile : AutoMapper.Profile
    {
        public UserProfile() {

            CreateMap<User, UserDto>();
            CreateMap<CreateUserDto, User>();
            CreateMap<UpdateUserDto, User>();
            CreateMap<RegisterDto, User>();
            CreateMap<UserAdminDto, User>();
            CreateMap<User, UserAdminDto>();
        }
    }
}
