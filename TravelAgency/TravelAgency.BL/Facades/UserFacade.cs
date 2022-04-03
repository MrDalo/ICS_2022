using AutoMapper;
using TravelAgency.BL.Models;
using TravelAgency.DAL.Entities;
using TravelAgency.DAL.UnitOfWork;


namespace TravelAgency.BL.Facades
{
    public class UserFacade : CRUDFacade<UserEntity, UserListModel, UserDetailModel>
    {
        public UserFacade(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper) : base(unitOfWorkFactory, mapper)
        {
        }
    }

   
}
