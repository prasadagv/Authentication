using Authentication.Domain.Models;

namespace Authentication.Domain.Repository
{
    public class UserRepository
    {
        private static readonly List<User> _users = new()
        {
            new() 
            { 
                Username = "admin1",
                Password = "password1",
                Email = "admin1@email.com",                 
                FirstName = "Admin1 FirstName", 
                LastName = "Admin1 LastName", 
                Role = "Administrator" 
            },

            new()
            {
                Username = "admin2",
                Password = "password2",
                Email = "admin2@email.com",
                FirstName = "Admin2 FirstName",
                LastName = "Admin2 LastName",
                Role = "Administrator"
            },

            new()
            {
                Username = "user1",
                Password = "password1",
                Email = "user1@email.com",
                FirstName = "User1 FirstName",
                LastName = "User1 LastName",
                Role = "User"
            },
        };

        public static List<User> Users = _users;
    }
}
