namespace TravelAgency.DAL.Entities
{
    public record PassengerOfShareRideEntity(
        Guid Id,
        Guid PassengerId,
        Guid ShareRideId
        ) : IEntity
    {
        public UserEntity? Passenger { get; set; }
        public ShareRideEntity? ShareRide { get; set; }

    }

    /*
     * Tuto entitu sme vytvorili kvoli potrebnemu vytahu v DB many-to-many medzi UserEntity a ShareRideEntity
     * Tento vztah mal znacit pasaziera danej jazdy. Z pohladu navrhu databaze sme museli vytvorit tuto entitu,
     * ktora obsahuje len ID pasaziera(instancia usera) a ID shareRide(instancia shareRide).
     * Pre kompatibilitu v DbContexte a naslednych fasadach a modeloch sme museli zdedit interface IEntity, avsak toto ID
     * nie je podstatne a nepracuje sa s nim. Nasledne sme pre interakciu v buducom ViewModele museli vyvorit aj fasadu a model pre tuto entitu.
     * Kvoli tomuto vztahu sme museli zmenit a pociatocne ERD. Tento problem sa nedal dopredu ocakavat a preto sme to museli vyriesit az v tejto fazi projektu.
     */
}