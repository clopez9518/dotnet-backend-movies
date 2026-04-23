

using Movies.Application.DTOs.Profile;
using Movies.Domain.Entities;

namespace Movies.Application.Mappings
{
    public class ProfileProfile : AutoMapper.Profile
    {
        public ProfileProfile() { 
        
            CreateMap<Profile, ProfileDto>();
            CreateMap<CreateProfileDto, Profile>();
        }
    }
}
