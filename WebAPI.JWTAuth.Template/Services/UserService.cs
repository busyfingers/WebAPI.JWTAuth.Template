using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.JWTAuth.Template.Helpers;
using WebAPI.JWTAuth.Template.Models;
using Microsoft.EntityFrameworkCore;
using static WebAPI.JWTAuth.Template.Helpers.AuthenticationUtils;

namespace WebAPI.JWTAuth.Template.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _db;

        public UserService(DataContext db)
        {
            _db = db;
        }

        public async Task<User> Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return null;
            }

            var user = await _db.Users.SingleOrDefaultAsync(x => x.Username == username);

            if (user == null)
            {
                return null;
            }

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }

            return user;
        }

        public async Task<User> Create(User user, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new AppException("Password is required");
            }

            if (await UserNameIsTaken(user))
            {
                throw new AppException($"Username '{user.Username}' is already taken");
            }

            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _db.Add(user);
            await _db.SaveChangesAsync();

            return user;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _db.Users.ToListAsync();
        }

        public async Task<User> GetById(int id)
        {
            var user = await _db.Users.FindAsync(id);

            if (user == null)
            {
                throw new AppException("User not found");
            }

            return user;
        }

        public async Task Update(User userParam, string password = null)
        {

            var user = await GetById(userParam.UserId);

            if (userParam.Username != user.Username)
            {
                // username has changed so check if the new username is already taken
                if (await UserNameIsTaken(user))
                {
                    throw new AppException($"Username '{userParam.Username}' is already taken");
                }
            }

            // update user properties
            user.FirstName = userParam.FirstName;
            user.LastName = userParam.LastName;
            user.Username = userParam.Username;

            // update password if it was entered
            if (!string.IsNullOrWhiteSpace(password))
            {
                CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            _db.Users.Update(user);
            await _db.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var user = await _db.Users.FindAsync(id);

            if (user != null)
            {
                _db.Users.Remove(user);
                var deleted = await _db.SaveChangesAsync();

                if (deleted == 0)
                {
                    throw new AppException("Could not delete user, invalid id");
                }
            }
        }

        private async Task<bool> UserNameIsTaken(User user)
        {
            return await _db.Users.AsQueryable().Where(x => x.Username == user.Username).CountAsync() > 0;
        }
    }
}
