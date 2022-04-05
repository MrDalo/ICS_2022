using AutoMapper;
using TravelAgency.BL.Models;
using TravelAgency.DAL.Entities;
using TravelAgency.DAL.UnitOfWork;


namespace TravelAgency.BL.Facades
{
    public class PassengerOfShareRideFacade : CRUDFacade<PassengerOfShareRideEntity, PassengerOfShareRideListModel, PassengerOfShareRideDetailModel>
    {
        public PassengerOfShareRideFacade(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper) : base(unitOfWorkFactory, mapper)
        {
        }
    }


}