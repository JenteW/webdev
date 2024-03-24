using exam.Data;
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

        [HttpPost]
        public void AddBooking([FromBody] Booking booking)
        {
            _data.AddBooking(booking);
        }
    }
}
