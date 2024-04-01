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

        void AddAdmin(Admin admin);
        IEnumerable<Admin> GetAdmins();


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


        /*******/
        /*COUNTRY*/
        /*******/

        void AddCountry(Country country);
        IEnumerable<Country> GetCountries();

        /******/
        /*OWNER*/
        /******/
        
        void AddOwner(Owner owner);
        IEnumerable<Owner> GetOwners();




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
        void AddUser(User user);
        IEnumerable<User> GetUsers();
        void GetUserByUsername(string username);
        void UpdateUser(int id, User user);

        
    }
}
