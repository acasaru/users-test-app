using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
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
    [ServiceContract]
    public class UserService : IUserService
    {
        [OperationContract]
        [WebInvoke(UriTemplate = "/AddUser/", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, Method = "POST")]
        public void AddUser(DtoUser user)
        {
            using (UserContext userContext = new UserContext())
            {
                var dalUser = Mapper.Map<DALUser>(user);
                userContext.Users.Add(dalUser);
                userContext.SaveChanges();
            }
        }

        [OperationContract]
        [WebInvoke(UriTemplate = "/DeleteUser/{userId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, Method = "POST")]
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

        [OperationContract]
        [WebGet(UriTemplate = "/GetAllUsers/", ResponseFormat = WebMessageFormat.Json)]
        public List<DtoUser> GetAllUsers()
        {
            using (UserContext userContext = new UserContext())
            {
                var users = userContext.Users.ToList();
                return Mapper.Map<List<DtoUser>>(users);
            }
        }

        [OperationContract]
        [WebGet(UriTemplate = "/GetUser/{userId}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
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

        [OperationContract]
        [WebInvoke(UriTemplate = "/UpdateUser/", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, Method = "POST")]
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
