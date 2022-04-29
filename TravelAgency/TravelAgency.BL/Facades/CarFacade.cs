using AutoMapper;
using TravelAgency.BL.Models;
using TravelAgency.DAL.Entities;
using TravelAgency.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace TravelAgency.BL.Facades
{
    public class CarFacade : CRUDFacade<CarEntity, CarListModel, CarDetailModel>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public CarFacade(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper) : base(unitOfWorkFactory, mapper)
        {

            _unitOfWorkFactory = unitOfWorkFactory;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CarListModel>> GetAllUserCars(Guid UserId)
        {
            await using var uow = _unitOfWorkFactory.Create();
            var query = uow
                .GetRepository<CarEntity>()
                .Get()
                .Where(e => e.OwnerId == UserId);
            
            return await _mapper.ProjectTo<CarListModel>(query).ToArrayAsync().ConfigureAwait(false);
        }

        public async Task<bool> CanIAddNewCar(Guid userId)
        {
            
            //Need one await to prevent Warning about async function without await inside
            await using var uow = _unitOfWorkFactory.Create();
            
            var query = uow
                .GetRepository<CarEntity>()
                .Get()
                .Where(e => e.OwnerId == userId);

            //Max supported cars per user is set to 3
            if (query.Count() < 3)
            {
                return true;
            }
            return false;
        }
    }

}