using AutoMapper;
using ListingApi.Dtos;
using ListingApi.Model.Auth;

namespace ListingApi.Helper
{
    public class Automapperprofiling:Profile
    {
          public Automapperprofiling () {
            CreateMap<UserDtos, AuthUser> ();
            CreateMap<AuthUser,UserDtos> ();
        } 
    }
}