using System;
using System.Collections.Generic;
using System.Linq;
using Users.DAL;

using DtoUser = Users.Dtos.User;
using DALUser = Users.DAL.User;

using DtoRole = Users.Dtos.Role;
using DALRole = Users.DAL.Role;

using AutoMapper;
using Users.UserServices.Infrastructure;

namespace Users.UserServices
{
    [AutomapServiceBehaviour]
    public class UserService : IUserService
    {
        public void AddUser(DtoUser user)
        {
            using (UserContext userContext = new UserContext())
            {
                var dalUser = Mapper.Map<DALUser>(user);
                userContext.Users.Add(dalUser);
                userContext.SaveChanges();
            }
        }

        public void DeleteUser(string userId)
        {
            int iUserId;
            if (!Int32.TryParse(userId, out iUserId))
                return;

            using (UserContext userContext = new UserContext())
            {
                var entity = userContext.Users.Find(userId);
                userContext.Users.Remove(entity);
                userContext.SaveChanges();
            }
        }

        public List<DtoUser> GetAllUsers()
        {
            using (UserContext userContext = new UserContext())
            {
                var users = userContext.Users.ToList();
                return Mapper.Map<List<DtoUser>>(users);
            }
        }

        public DtoUser GetUser(string userId)
        {
            int iUserId;
            if (!Int32.TryParse(userId, out iUserId))
                return null;

            using (UserContext userContext = new UserContext())
            {
                var user = userContext.Users.Find(iUserId);
                return Mapper.Map<DtoUser>(user);
            }
        }

        public void UpdateUser(DtoUser user)
        {
            using (UserContext userContext = new UserContext())
            {
                var entity = userContext.Users.Find(user.Id);
                userContext.Entry(entity).CurrentValues.SetValues(user);
                userContext.SaveChanges();
            }
        }
    }
}
