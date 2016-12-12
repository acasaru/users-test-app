using AutoMapper;

using DtoUser = Users.Dtos.User;
using DALUser = Users.DAL.User;

using DtoRole = Users.Dtos.Role;
using DALRole = Users.DAL.Role;


namespace Users.UserServices.Infrastructure
{
    public class AutomapBoostrap
    {
        public static void InitializeMap()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<DALUser, DtoUser>();
                cfg.CreateMap<DtoUser, DALUser>();
                cfg.CreateMap<DALRole, DtoRole>();
                cfg.CreateMap<DtoRole, DALRole>();
            });
        }
    }
}
