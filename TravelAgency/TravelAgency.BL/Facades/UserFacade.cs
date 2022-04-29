using AutoMapper;
using TravelAgency.BL.Models;
using TravelAgency.DAL.Entities;
using TravelAgency.DAL.UnitOfWork;


namespace TravelAgency.BL.Facades
{
    public class UserFacade : CRUDFacade<UserEntity, UserListModel, UserDetailModel>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public UserFacade(IUnitOfWorkFactory unitOfWorkFactory, IMapper mapper) : base(unitOfWorkFactory, mapper)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
            _mapper = mapper;
        }



        public async Task<bool> SignUpForShareRideAsPassenger(UserDetailModel user, ShareRideDetailModel model)
        {
            await using var uow = _unitOfWorkFactory.Create();
            var query = uow
                .GetRepository<ShareRideEntity>()
                .Get()
                .Where(e=> e.Id == model.Id);

            bool collisionOccured = false;
            foreach (var shareRideId in user.PassengerShareRides)
            {
                var shareRide = uow
                    .GetRepository<ShareRideEntity>()
                    .Get()
                    .Where(e => e.Id == shareRideId.ShareRideId);

                if (((query.First().LeaveTime < shareRide.First().LeaveTime && query.First().ArriveTime < shareRide.First().LeaveTime)
                    || (query.First().LeaveTime > shareRide.First().ArriveTime) && (query.First().LeaveTime < query.First().ArriveTime)))
                {
                    //This is state when no collision occured
                }
                else
                {
                    collisionOccured = true;
                }
            }

            foreach (var shareRide in user.DriverShareRides)
            {

                if (((query.First().LeaveTime < shareRide.LeaveTime && query.First().ArriveTime < shareRide.LeaveTime)
                     || (query.First().LeaveTime > shareRide.ArriveTime) && (query.First().LeaveTime < query.First().ArriveTime)))
                {
                    //This is state when no collision occured
                }
                else
                {
                    collisionOccured = true;
                }
            }

            if (!collisionOccured)
            {
                var newPassengerAndShareRideRelation = new PassengerOfShareRideDetailModel(
                    
                    PassengerId: user.Id,
                    ShareRideId: query.First().Id
                )
                {
                    Id = Guid.NewGuid()
                };
                

                user.PassengerShareRides.Add(newPassengerAndShareRideRelation);

                await uow
                    .GetRepository<UserEntity>()
                    .InsertOrUpdateAsync(user, _mapper)
                    .ConfigureAwait(false);
                await uow.CommitAsync();

                await uow
                    .GetRepository<PassengerOfShareRideEntity>()
                    .InsertOrUpdateAsync(newPassengerAndShareRideRelation, _mapper)
                    .ConfigureAwait(false);
                await uow.CommitAsync();



            }
            
            
            return !collisionOccured;
            
        }
    }


}
