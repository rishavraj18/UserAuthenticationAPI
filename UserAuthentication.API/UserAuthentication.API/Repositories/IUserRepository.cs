using UserAuthentication.API.Models;

namespace UserAuthentication.API.Repositories
{
    public interface IUserRepository
    {
        Task<User> AuthenticateAsync(string username, string password);
    }
}
