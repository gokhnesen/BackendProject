using BackendProject.Entity.DomainModels;
using System.Threading.Tasks;

namespace BackendProject.Data.Abstract
{
    public interface IAuthRepository
    {
        Task<User> Register(User user, string password);
        Task<User> Login(string userName, string password);
        Task<bool> UserExists(string userName);
    }
}
