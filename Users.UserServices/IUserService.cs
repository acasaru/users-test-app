using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using Users.Dtos;

namespace Users.UserServices
{
    public interface IUserService
    {
        List<User> GetAllUsers();

        User GetUser(string id);

        void UpdateUser(User user);

        void AddUser(User user);

        void DeleteUser(string userId);

    }
}
