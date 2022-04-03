using AutoMapper;
using TravelAgency.BL.Models;
using TravelAgency.DAL.Entities;
using TravelAgency.DAL.UnitOfWork;

namespace TravelAgency.BL.Facades
{
    public class ShareRideFacade : CRUDFacade<ShareRideEntity, ShareRideListModel, ShareRideDetailModel>
    {
        public ShareRideFacade(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper) : base(unitOfWorkFactory, mapper)
        {
        }
    }

    
}
