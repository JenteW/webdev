﻿using exam.Data;
using exam.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace exam.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private InterfaceCampingContext _data;
        public BookingController(InterfaceCampingContext data)
        {
            _data = data;
        }


        [HttpGet]
        public ActionResult<IEnumerable<Booking>> GetBooking()
        {
            return Ok(_data.GetBookings());
        }

        //get booking by UserId
        [HttpGet("userId/{id}")]
        public ActionResult<IEnumerable<Booking>> GetBookingByUserId(int id)
        {
            return Ok(_data.GetBookingsByUserId(id));
        }
        //get booking by id
        [HttpGet("{id}")]
        public ActionResult<Booking> GetBookingById(int id)
        {
            return Ok(_data.GetBookingById(id));
        }
        //get booking by campingSpotId
        [HttpGet("campingSpotId/{id}")]
        public ActionResult<IEnumerable<Booking>> GetBookingByCampingSpotId(int id)
        {
            return Ok(_data.GetBookingsByCampingSpotId(id));
        }
        //get booking by owner id
        [HttpGet("ownerId/{id}")]
        public ActionResult<IEnumerable<Booking>> GetBookingByOwnerId(int id)
        {
            return Ok(_data.GetBookingsByOwnerId(id));
        }
        [HttpPost]
        public void AddBooking([FromBody] Booking booking)
        {
            _data.AddBooking(booking);
        }
        //update booking
        [HttpPut("{id}")]
        public void UpdateBooking(int id, [FromBody] Booking booking)
        {
            _data.UpdateBooking(id, booking);
        }
        //delete booking
        [HttpDelete("{id}")]
        public void DeleteBooking(int id)
        {
            _data.DeleteBooking(id);
        }
        //delete booking by campingSpotId
        [HttpDelete("campingSpotId/{id}")]
        public void DeleteBookingByCampingSpotId(int id)
        {
            _data.DeleteBookingBySpotId(id);
        }
        //check if booking is available
        [HttpPost("checkavailability")]
        public IActionResult CheckAvailability([FromBody] Booking request)
        {
            Console.WriteLine($"Received startDate: {request.StartDate}, endDate: {request.EndDate}");
            DateTime startDate;
            DateTime endDate;
            if (!DateTime.TryParseExact(request.StartDate, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out startDate) || !DateTime.TryParseExact(request.EndDate, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out endDate))
            {
               return BadRequest("Invalid date format");
            }
            if(endDate <= startDate)
            {
                return BadRequest("End date must be after start date");
            }
            bool isAvailable = _data.IsBookingAvailable(startDate, endDate, request.CampingSpotId, request.AccomodationId);
            return Ok( isAvailable);
        }
        
        
    }
}
