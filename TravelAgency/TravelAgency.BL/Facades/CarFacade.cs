using AutoMapper;
using TravelAgency.BL.Models;
using TravelAgency.DAL.Entities;
using TravelAgency.DAL.UnitOfWork;

namespace TravelAgency.BL.Facades
{
    public class CarFacade : CRUDFacade<CarEntity, CarListModel, CarDetailModel>
    {

        public CarFacade(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper) : base(unitOfWorkFactory, mapper)
        {
        }
    }

}