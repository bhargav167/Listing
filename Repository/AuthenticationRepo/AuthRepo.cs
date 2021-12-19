using System.Threading.Tasks;
using ListingApi.Data;
using ListingApi.Model.Auth;
using Microsoft.EntityFrameworkCore;

namespace ListingApi.Repository.AuthenticationRepo {
    public class AuthRepo : IAuthRepo {
        private readonly DataContext _context;
        public AuthRepo (DataContext context) {
            _context = context;
        }
        public async Task<AuthUser> AddAuth (AuthUser user) {
            await _context.AuthUsers.AddAsync (user);
            await _context.SaveChangesAsync ();

            return user;
        }

        public async Task<AuthUser> getAuthById (int id) {
            var AuthUser = await _context.AuthUsers.FirstOrDefaultAsync (u => u.Id == id);
            return AuthUser;
        }

        public async Task<AuthUser> getAuthUserByEmail (string email) {
            var AuthUser = await _context.AuthUsers.FirstOrDefaultAsync (u => u.Email == email);
            return AuthUser;
        }
        public async Task<bool> IsAuthUserExist (string email) {
            if (await _context.AuthUsers.AnyAsync (e => e.Email == email))
                return true;

            return false;
        }
    }
}