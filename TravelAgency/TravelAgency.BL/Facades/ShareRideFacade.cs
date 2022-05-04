
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

        public async Task<IEnumerable<ShareRideDetailModel>> GetFilteredShareRidesAsync(
            DateTime ? startTimeFrom = null, DateTime ? finishTimeFrom = null,
            DateTime? startTimeTo = null, DateTime? finishTimeTo = null,
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

            if(startTimeFrom != null)
            {                                                   
                query = query.Where(ride =>DateTime.Compare(ride.LeaveTime, (DateTime)(startTimeFrom)) >= 0 );
            }

            if (startTimeTo != null)
            {
                query = query.Where(ride => DateTime.Compare(ride.LeaveTime, (DateTime)(startTimeTo)) <= 0);
            }

            if (finishTimeFrom != null)
            {
                query = query.Where(ride => DateTime.Compare(ride.ArriveTime, (DateTime)(finishTimeFrom)) >= 0);
            }

            if (finishTimeTo != null)
            {
                query = query.Where(ride => DateTime.Compare(ride.ArriveTime, (DateTime)(finishTimeTo)) <= 0);
            }


            return await _mapper.ProjectTo<ShareRideDetailModel>(query).ToArrayAsync().ConfigureAwait(false);
        }

        public async Task<IEnumerable<ShareRideListModel>> GetDriverShareRides(Guid DriverId)
        {
            await using var uow = _unitOfWorkFactory.Create();
            var query = uow
                .GetRepository<ShareRideEntity>()
                .Get()
                .Where(e=>e.DriverId == DriverId);

            


            return await _mapper.ProjectTo<ShareRideListModel>(query).ToArrayAsync().ConfigureAwait(false);
        }


        public async Task<IEnumerable<ShareRideListModel>> GetUserPassengerShareRides(Guid PassengerId)
        {
            await using var uow = _unitOfWorkFactory.Create();
            var query = uow
                .GetRepository<PassengerOfShareRideEntity>()
                .Get()
                .Where(e => e.PassengerId == PassengerId);

            
            var shareRideQuery = uow.GetRepository<ShareRideEntity>()
                .Get();

            if (!query.Any())
            {
                shareRideQuery = shareRideQuery.Where(e => e.Id == Guid.Empty);
            }
            else
            {

                foreach (var value in query)
                {

                    shareRideQuery = shareRideQuery.Where(e => e.Id == value.ShareRideId);
                }
            }
            

            return await _mapper.ProjectTo<ShareRideListModel>(shareRideQuery).ToArrayAsync().ConfigureAwait(false);
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

            var Passengersquery = uow
                .GetRepository<PassengerOfShareRideEntity>()
                .Get()
                .Where(e => e.ShareRideId == model.Id);

            // -1 because driver is not counted in Passengers 
            if (Passengersquery.Count() == query.First().Capacity - 1)
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
