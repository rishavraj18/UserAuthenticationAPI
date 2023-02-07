using UserAuthentication.API.Models;

namespace UserAuthentication.API.Handlers
{
    public interface ITokenHandler
    {
        Task<string> CreateTokenAsync(User user);
    }
}
