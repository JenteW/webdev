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
        IEnumerable<Accomodation> GetAccomodationsByCampingSpotId(int id);
        Accomodation GetAccomodationById(int id);
        void DeleteAccomodationBySpotId(int id);

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
        IEnumerable<Booking> GetBookingsByUserId(int userId);
        Booking GetBookingById(int id);
        void UpdateBooking(int id, Booking booking);
        IEnumerable<Booking> GetBookingsByCampingSpotId(int campingSpotId);
        void DeleteBooking(int id);
        bool IsBookingAvailable(DateTime startDate, DateTime endDate, int campingSpotId, int accomodationId);
        /*******/
        /*CAMPINGSPOT*/
        /*******/

        int AddCampingSpot(CampingSpot campingspot);
        IEnumerable<CampingSpot> GetCampingSpots();
        CampingSpot GetCampingSpotById(int id);
        IEnumerable<CampingSpot> GetCampingSpotsByOwnerId(int ownerId);
        IEnumerable<CampingSpot> GetCampingSpotsByLocationId(int locationId);
        void DeleteCampingSpot(int id);
        void UpdateCampingSpot(int id, CampingSpot campingspot);

        /*******/
        /*City*/
        /*******/

        void AddCity(City city);
        IEnumerable<City> GetCities();
        IEnumerable<City> GetCitiesByCountry(int countryId);
        void UpdateCity(int id, City city);
        void DeleteCity(int id);
        void DeleteCityByCountryId(int countryId);


        /*******/
        /*COUNTRY*/
        /*******/

        void AddCountry(Country country);
        IEnumerable<Country> GetCountries();
        void DeleteCountry(int id);

        /*******/
        /*LOCATION*/
        /*******/

        void AddLocation(Location location);
        IEnumerable<Location> GetLocations();
        IEnumerable<Location> GetLocationsByCountryId(int country);
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
        bool CheckOwnername(string username);



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
        IEnumerable<SpotTag> GetTagsBySpotId(int spotId);
        IEnumerable<SpotTag> GetSpotsByTagId(int tagId);



        /******/
        /*TAG*/
        /******/

        void AddTag(Tag tag);
        IEnumerable<Tag> GetTags();
        Tag GetTagById(int id);
        void DeleteTag(int id);


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
        bool CheckUsername(string username);
    }
}
