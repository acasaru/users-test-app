using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users.DAL
{
    public class UserContextInitializer: System.Data.Entity.DropCreateDatabaseIfModelChanges<UserContext>
    {
        protected override void Seed(UserContext context)
        {
            var userList = new List<User>()
            {
                new User() { EmailAddress = "admin@users.com", FirstName = "Admin", LastName = "Admin", Password = "admin", Role = new Role() { Name = "admin"}, Username = "admin"},
                new User() { EmailAddress = "user@users.com", FirstName = "User", LastName = "User", Password = "user", Role = new Role() { Name = "user"},Username = "user"}
            };

           userList.ForEach(user => context.Users.Add(user));
           context.SaveChanges();
           
        }
    }
}
