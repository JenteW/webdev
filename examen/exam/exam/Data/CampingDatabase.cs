using LiteDB;
using exam.Models;
using exam.Data;


namespace exam.Data
{
    public class CampingDatabase : InterfaceCampingContext
    {
        LiteDatabase db = new LiteDatabase(@"data.db");


        /******/
        /*ADMIN*/
        /******/

        public void AddAdmin(Admin admin)
        {
            db.GetCollection<Admin>("admins").Insert(admin);
        }
        public IEnumerable<Admin> GetAdmins()
        {
            return db.GetCollection<Admin>("admins").FindAll();
        }


        /*******/
        /*BOOKING*/
        /*******/


        public void AddBooking(Booking booking)
        {
            db.GetCollection<Booking>("bookings").Insert(booking);
        }
        public IEnumerable<Booking> GetBookings()
        {
            return db.GetCollection<Booking>("bookings").FindAll();
        }

        /*******/
        /*CAMPINGSPOT*/
        /*******/

        public void AddCampingSpot(CampingSpot campingspot)
        {
            db.GetCollection<CampingSpot>("campingspots").Insert(campingspot);
        }
        public IEnumerable<CampingSpot> GetCampingSpots()
        {
            return db.GetCollection<CampingSpot>("campingspots").FindAll();
        }

        /******/
        /*OWNER*/
        /******/

        public void AddOwner(Owner owner)
        {
            db.GetCollection<Owner>("owners").Insert(owner);
        }
        public IEnumerable<Owner> GetOwners()
        {
           return db.GetCollection<Owner>("owners").FindAll();
        }
        public void GetOwnerByUsername(string username)
        {
            db.GetCollection<Owner>("owners").FindOne(x => x.Username == username);
        }


        /******/
        /*SPOTTAG*/
        /******/

        public void AddSpotTag(SpotTag spottag)
        {
            db.GetCollection<SpotTag>("spottags").Insert(spottag);
        }
        public IEnumerable<SpotTag> GetSpotTags()
        {
            return db.GetCollection<SpotTag>("spottags").FindAll();
        }

        /******/
        /*TAG*/
        /******/

        public void AddTag(Tag tag)
        {
            db.GetCollection<Tag>("tags").Insert(tag);
        }
        public IEnumerable<Tag> GetTags()
        {
            return db.GetCollection<Tag>("tags").FindAll();
        }

        /******/
        /*USER*/
        /******/

        public void AddUser(User user)
        {
            db.GetCollection<User>("users").Insert(user);
        }
        public IEnumerable<User> GetUsers()
        {
            return db.GetCollection<User>("users").FindAll();
        }

        public void GetUserByUsername(string username)
        {
            db.GetCollection<User>("users").FindOne(x => x.Username == username);
        }

        public void UpdateUser(int id, User user)
        {
            db.GetCollection<User>("users").Update(id, user);   
        }



    }
}
