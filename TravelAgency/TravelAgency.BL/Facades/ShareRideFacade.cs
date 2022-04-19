
using AutoMapper;
using TravelAgency.BL.Models;
using TravelAgency.DAL.Entities;
using TravelAgency.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace TravelAgency.BL.Facades
{
    public class ShareRideFacade : CRUDFacade<ShareRideEntity, ShareRideListModel, ShareRideDetailModel>
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public ShareRideFacade(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper) : base(unitOfWorkFactory, mapper)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ShareRideListModel>> GetFilteredShareRidesAsync(
            DateTime ? startTime = null, DateTime ? finishTime = null,
            string ? startLocation = null, string ? destinationLocation = null
            )
        {
            await using var uow = _unitOfWorkFactory.Create();
            var query = uow
                .GetRepository<ShareRideEntity>()
                .Get();

            if(startLocation != null)
            {
                query = query.Where(ride => ride.FromPlace.ToUpper() == startLocation.ToUpper());
            }

            if(destinationLocation != null)
            {
                query = query.Where(ride => ride.ToPlace.ToUpper() == destinationLocation.ToUpper());
            }

            if(startTime != null)
            {                                                   
                query = query.Where(ride =>DateTime.Compare(ride.LeaveTime, (DateTime)(startTime)) >= 0 );
            }
            
            if(finishTime != null)
            {
                query = query.Where(ride => DateTime.Compare(ride.ArriveTime, (DateTime)(finishTime)) <= 0);
            }


            return await _mapper.ProjectTo<ShareRideListModel>(query).ToArrayAsync().ConfigureAwait(false);
        }


        public async Task<int> IsShareRideFull(ShareRideDetailModel model)
        {
            if (model.CarId == null)
            {
                    //Error
                return -1;
            }

            await using var uow = _unitOfWorkFactory.Create();
            var query = uow
                .GetRepository<CarEntity>()
                .Get()
                .Where(e => e.Id == model.CarId);
            

                // -1 because driver is not counted in Passengers 
            if (model.Passengers.Count == query.First().Capacity - 1)
            {
                //True
                return 1;
            }
            else
            {
                    //False
                return 0;
            }
            

        }


    }

    
}
