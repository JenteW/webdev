﻿using LiteDB;
using exam.Models;
using exam.Data;
using Microsoft.AspNetCore.Mvc;

namespace exam.Data
{
    public class CampingDatabase : InterfaceCampingContext
    {
        LiteDatabase db = new LiteDatabase(@"data.db");


        /******/
        /*ACCOMODATION*/
        /******/
        public void AddAccomodation(Accomodation accomodation)
        {
            db.GetCollection<Accomodation>("accomodations").Insert(accomodation);
        }

        public IEnumerable<Accomodation> GetAccomodations()
        {
            return db.GetCollection<Accomodation>("accomodations").FindAll();
        }
        public IEnumerable<Accomodation> GetAccomodationsByCampingSpotId(int id)
        {
            return db.GetCollection<Accomodation>("accomodations").Find(x => x.CampingSpotId == id);
        }





        /******/
        /*ADMIN*/
        /******/

        public IActionResult AddAdmin(Admin admin)
        {
            try
            {
                db.GetCollection<Admin>("admins").Insert(admin);
                Console.WriteLine("Admin added");
                return new OkResult();
            }
            catch
            {
                Console.WriteLine("could not add admin");
                return new BadRequestResult();
            }
        }
        public IEnumerable<Admin> GetAdmins()
        {
            return db.GetCollection<Admin>("admins").FindAll();
        }

        public Admin GetAdminByUsernameAndPassword(string username)
        {
            return db.GetCollection<Admin>("admins").FindOne(x => x.Username == username);
        }
        public Admin GetAdminById(int id)
        {
            return db.GetCollection<Admin>("admins").FindById(id);
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
        public CampingSpot GetCampingSpotById(int id)
        {
            return db.GetCollection<CampingSpot>("campingspots").FindById(id);
        }
        public IEnumerable<CampingSpot> GetCampingSpotsByOwnerId(int id)
        {
            return db.GetCollection<CampingSpot>("campingspots").Find(x => x.OwnerId == id);
        }
        public IEnumerable<CampingSpot> GetCampingSpotsByLocationId(int id)
        {
            return db.GetCollection<CampingSpot>("campingspots").Find(x => x.LocationId == id);
        }
        

        /*******/
        /*CITY*/
        /*******/

        public void AddCity(City city)
        {
            db.GetCollection<City>("cities").Insert(city);
        }
        public IEnumerable<City> GetCities()
        {
            return db.GetCollection<City>("cities").FindAll();
        }
        public void UpdateCity(int id,  City city)
        {
            db.GetCollection<City>("cities").Update(id, city);
        }
        public IEnumerable<City> GetCitiesByCountry(int countryId)
        {
            return db.GetCollection<City>("cities").Find(x => x.CountryId == countryId);
        }

        /******/
        /*COUNTRY*/
        /*******/
        public void AddCountry(Country country)
        {
            db.GetCollection<Country>("countries").Insert(country);
        }

        public IEnumerable<Country> GetCountries()
        {
            return db.GetCollection<Country>("countries").FindAll();
        }

        /********/
        /*LOCATION*/
        /********/

        public void AddLocation(Location location)
        {
            db.GetCollection<Location>("locations").Insert(location);
        }
        public IEnumerable<Location> GetLocations()
        {
            return db.GetCollection<Location>("locations").FindAll();
        }
        public IEnumerable<Location> GetLocationsByCountryId(int countryId)
        {
            return db.GetCollection<Location>("locations").Find(x => x.CountryId == countryId);
        }



        /******/
        /*OWNER*/
        /******/

        public IActionResult AddOwner(Owner owner)
        {
            try
            {
                db.GetCollection<Owner>("owners").Insert(owner);
                return new OkResult();
            }
            catch
            {
                Console.WriteLine("could not add owner");
                return new BadRequestResult();
            }
        }
        public IEnumerable<Owner> GetOwners()
        {
           return db.GetCollection<Owner>("owners").FindAll();
        }
      /*  public Owner GetOwnerByUsername(string username)
        {
         return db.GetCollection<Owner>("owners").FindOne(x => x.Username == username);
        }*/
        public IActionResult UpdateOwner(int id, Owner owner)
        {
            try
            {
                db.GetCollection<Owner>("owners").Update(id, owner);
                return new OkResult();

            }
            catch
            {
                return new BadRequestResult();
            }
        }
        public void DeleteOwner(int id)
        {
            db.GetCollection<Owner>("owners").Delete(id);
        }
        public Owner GetOwnerByUsernameAndPassword(string username)
        {
            return db.GetCollection<Owner>("owners").FindOne(x => x.Username == username);
        }
        public Owner GetOwnerById(int id)
        {
            return db.GetCollection<Owner>("owners").FindById(id);
        }


        /********/
        /*SPOTACCOMODATION*/
        /********/
        public void AddSpotAccomodation(SpotAccomodation spotAccomodation)
        {
            db.GetCollection<SpotAccomodation>("spotaccomodations").Insert(spotAccomodation);
        }

        public IEnumerable<SpotAccomodation> GetSpotAccomodations()
        {
            return db.GetCollection<SpotAccomodation>("spotaccomodations").FindAll();
        }
        public IEnumerable<SpotAccomodation> GetSpotsByAccomodation(int accomodation)
        {
            return db.GetCollection<SpotAccomodation>("spotaccomodations").Find(x => x.AccomodationId == accomodation);
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
        public IEnumerable<SpotTag> GetTagsBySpotId(int spotId)
        {
            return db.GetCollection<SpotTag>("spottags").Find(x => x.SpotId == spotId);
        }
        public IEnumerable<SpotTag> GetSpotsByTagId(int tagId)
        {
            return db.GetCollection<SpotTag>("spottags").Find(x => x.TagId == tagId);
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
        public Tag GetTagById(int id)
        {
            return db.GetCollection<Tag>("tags").FindById(id);
        }

        /******/
        /*USER*/
        /******/

        public IActionResult AddUser(User user)
        {
            try
            {
                db.GetCollection<User>("users").Insert(user);
                Console.WriteLine("User added");
                return new OkResult();
            }
            catch
            {
                Console.WriteLine("could not add user");
                return new BadRequestResult();
            }
        }
        public IEnumerable<User> GetUsers()
        {
            return db.GetCollection<User>("users").FindAll();
        }

       /* public User GetUserByUsername(string username)
        {
           return db.GetCollection<User>("users").FindOne(x => x.Username == username);
        }*/


        public void UpdateUser(int id, User user)
        {
            db.GetCollection<User>("users").Update(id, user);   
        }

        public void DeleteUser(int id)
        {
            db.GetCollection<User>("users").Delete(id);
        }

        public User GetUserByUsernameAndPassword(string username)
        {
           return db.GetCollection<User>("users").FindOne(x => x.Username == username);
        }
        public User GetUserById(int id)
        {
            return db.GetCollection<User>("users").FindById(id);
        }
    }
}
