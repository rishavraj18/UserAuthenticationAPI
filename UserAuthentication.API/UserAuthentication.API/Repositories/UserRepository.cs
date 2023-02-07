using System.Data;
using UserAuthentication.API.Models;

namespace UserAuthentication.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private List<User> Users = new List<User>() {
          new User()
           {
              FirstName = "Rishav", LastName = "Raj",
              Email="rj.rishav94@gmail.com", UserName="rishavraj",
              Password="Test@1234", Roles=new List<string> {"user"}
           },
          new User()
           {
              FirstName = "RishavAdmin", LastName = "Raj",
              Email="rj.rishv@gmail.com", UserName="rishavraj_adm",
              Password="Test@12345", Roles=new List<string> {"user", "admin"}
           }
        };

        public async Task<User> AuthenticateAsync(string username, string password)
        {
            var user = Users.Find(x => x.UserName.Equals(username, StringComparison.InvariantCultureIgnoreCase) &&
            x.Password == password);

            return user;
        }
    }
}
