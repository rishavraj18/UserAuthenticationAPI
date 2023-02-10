using Microsoft.EntityFrameworkCore;
using System.Data;
using UserAuthentication.API.DataAccessLayer;
using UserAuthentication.API.Models;

namespace UserAuthentication.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDBContext dbContext;

        public UserRepository(UserDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<User> AuthenticateAsync(string username, string password)
        {
            var user = await dbContext.Users
                                       .FirstOrDefaultAsync(
                                        x => x.UserName.ToLower() == username.ToLower() && 
                                        x.Password == password);

            if(user == null) {return null;}

            var userRoles = await dbContext.User_Roles.Where(x => x.UserId == user.Id).ToListAsync();

            if(userRoles.Any())
            {
                user.Roles = new List<string>();
                foreach(var Urole in userRoles)
                {
                  var role = await dbContext.Roles.FirstOrDefaultAsync(x => x.Id == Urole.RoleId);
                  if(role != null)
                  {
                    user.Roles.Add(role.Name);
                  }
                }
            }

            user.Password = null;
            return user;
        }
    }
}
