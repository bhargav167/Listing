using System.Threading.Tasks;
using ListingApi.Model.Auth;

namespace ListingApi.Repository.AuthenticationRepo
{
    public interface IAuthRepo
    {
        Task<bool> IsAuthUserExist (string email);
        Task<AuthUser> AddAuth (AuthUser user);
        Task<AuthUser> getAuthById (int id);
        Task<AuthUser> getAuthUserByEmail (string email);  
    }
}