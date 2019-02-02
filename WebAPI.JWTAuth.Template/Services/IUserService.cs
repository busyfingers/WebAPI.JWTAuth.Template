using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.JWTAuth.Template.Models;

namespace WebAPI.JWTAuth.Template.Services
{
    public interface IUserService
    {
        Task<User> Authenticate(string username, string password);
        Task<IEnumerable<User>> GetAll();
        Task<User> GetById(int id);
        Task<User> Create(User user, string password);
        Task Update(User user, string password = null);
        Task Delete(int id);
    }
}
