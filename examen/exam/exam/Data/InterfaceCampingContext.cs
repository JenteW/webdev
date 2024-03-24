using exam.Models;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace exam.Data
{
    public interface InterfaceCampingContext
    {

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
    }
}
