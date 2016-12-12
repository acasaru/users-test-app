using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using Users.Dtos;

namespace Users.UserServices
{
    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        [WebGet(UriTemplate = "/GetAllUsers/", ResponseFormat = WebMessageFormat.Json)]
        List<User> GetAllUsers();

        [OperationContract]
        [WebGet(UriTemplate = "/GetUser/{id}", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        User GetUser(string id);

        [OperationContract]
        [WebInvoke(UriTemplate = "/UpdateUser/", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, Method = "POST")]
        void UpdateUser(User user);

        [OperationContract]
        [WebInvoke(UriTemplate = "/AddUser/", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, Method = "POST")]
        void AddUser(User user);

        [OperationContract]
        [WebInvoke(UriTemplate = "/DeleteUser/", ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json, Method = "POST")]
        void DeleteUser(string userId);

    }
}
