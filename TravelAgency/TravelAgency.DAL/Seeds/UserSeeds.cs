using TravelAgency.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace TravelAgency.DAL.Seeds;

public static class UserSeeds
{
    public static readonly UserEntity Driver1 = new(
        Id: Guid.Parse(input: "dc5df605-d676-4c25-98d8-5b795c7b6503"),
        Name: "David",
        Login: "xsmith00",
        Surname: "Smith",
        Email: "davidsmith@gmail.com",
        PhoneNumber: "+421789564111",
        PhotoUrl: "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQ2l3ICDx95MWJd7nvoOej3VtuY6_NRp2knsPYQOjqn25ddXDrD2kZvXUVbwSL8CCF4knY&usqp=CAU");

    public static readonly UserEntity Passenger1 = new(
        Id: Guid.Parse(input: "061f3338-0d39-4549-8b7d-ec74f76ba4b1"),
        Name: "Emanuel",
        Login: "xtaylo01",
        Surname: "Taylor",
        Email: "emanueltaylor@gmail.com",
        PhoneNumber: "+421045256789",
        PhotoUrl: "https://1884403144.rsc.cdn77.org/foto/isaiah-webb-vousy-obrazem-sochy/Zml0LWluLzk3OHg5OTk5L2ZpbHRlcnM6cXVhbGl0eSg4NSk6bm9fdXBzY2FsZSgpL2ltZw/2212462.jpg?v=0&st=GNp19ktMcBCOScTY-0DRhI5CEgdVtDqNRvYt2Yw945M&ts=1600812000&e=0");

    public static readonly UserEntity Passenger2 = new(
        Id: Guid.Parse(input: "05553338-0d39-4549-8b7d-ec74f76ba4b1"),
        Name: "Emily",
        Login: "xgreen12",
        Surname: "Green",
        Email: "emilygreen@gmail.com",
        PhoneNumber: "+421124569785",
        PhotoUrl: "https://pbs.twimg.com/media/FBfpjhsX0AQqBBH.png");

    static UserSeeds()
    {
        Driver1.Cars.Add(CarSeeds.FiatMultipla);
        Driver1.DriverShareRides.Add(ShareRideSeeds.ShareRide1);
        Passenger1.PassengerShareRides.Add(PassengerOfShareRideSeeds.PassengerOfShareRide1);
        Passenger2.PassengerShareRides.Add(PassengerOfShareRideSeeds.PassengerOfShareRide2 );
    }


    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>().HasData(
            Driver1 with
            {
                DriverShareRides = Array.Empty<ShareRideEntity>(),
                PassengerShareRides = Array.Empty<PassengerOfShareRideEntity>(),
                Cars = Array.Empty<CarEntity>()
            },
            Passenger1 with
            {
                DriverShareRides = Array.Empty<ShareRideEntity>(),
                PassengerShareRides = Array.Empty<PassengerOfShareRideEntity>(),
                Cars = Array.Empty<CarEntity>()
            },
            Passenger2 with
            {
                DriverShareRides = Array.Empty<ShareRideEntity>(),
                PassengerShareRides = Array.Empty<PassengerOfShareRideEntity>(),
                Cars = Array.Empty<CarEntity>()
            }
        );
    }
}

