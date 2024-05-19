using exam.Models;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace exam.Data
{
    public interface InterfaceCampingContext
    {

        /******/
        /*ACCOMODATION*/
        /*******/

        void AddAccomodation(Accomodation accomodation);
        IEnumerable<Accomodation> GetAccomodations();

        /******/
        /*ADMIN*/
        /******/

        IActionResult AddAdmin(Admin admin);
        IEnumerable<Admin> GetAdmins();
        Admin GetAdminByUsernameAndPassword(string username);
        Admin GetAdminById(int id);


        /*******/
        /*BOOKING*/
        /*******/


        void AddBooking(Booking booking);
        IEnumerable<Booking> GetBookings();

        /*******/
        /*CAMPINGSPOT*/
        /*******/

        void AddCampingSpot(CampingSpot campingspot);
        IEnumerable<CampingSpot> GetCampingSpots();

        /*******/
        /*City*/
        /*******/

        void AddCity(City city);
        IEnumerable<City> GetCities();
        void UpdateCity(int id, City city);


        /*******/
        /*COUNTRY*/
        /*******/

        void AddCountry(Country country);
        IEnumerable<Country> GetCountries();

        /*******/
        /*LOCATION*/
        /*******/

        void AddLocation(Location location);
        IEnumerable<Location> GetLocations();
        /******/
        /*OWNER*/
        /******/
        
        IActionResult AddOwner(Owner owner);
        IEnumerable<Owner> GetOwners();
        //Owner GetOwnerByUsername(string username);
        IActionResult UpdateOwner(int id, Owner owner);
        void DeleteOwner(int id);
        Owner GetOwnerByUsernameAndPassword(string username);
        Owner GetOwnerById(int id);



        /*******/
        /*SPOTACCOMODATION*/
        /*******/

        void AddSpotAccomodation(SpotAccomodation spotaccomodation);
        IEnumerable<SpotAccomodation> GetSpotAccomodations();
        IEnumerable<SpotAccomodation> GetSpotsByAccomodation(int spotId);

        /******/
        /*SPOTTAG*/
        /******/

        void AddSpotTag(SpotTag spottag);
        IEnumerable<SpotTag> GetSpotTags();

        /******/
        /*TAG*/
        /******/

        void AddTag(Tag tag);
        IEnumerable<Tag> GetTags();

        /******/
        /*USER*/
        /******/
       
        IActionResult AddUser(User user);
        IEnumerable<User> GetUsers();
        //User GetUserByUsername(string username);
        void UpdateUser(int id, User user);
        void DeleteUser(int id);
        User GetUserByUsernameAndPassword(string username);
        User GetUserById(int id);
    }
}
