using exam.Data;
using exam.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace exam.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private InterfaceCampingContext _data;
        public LocationController(InterfaceCampingContext data)
        {
            _data = data;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Location>> GetLocation()
        {
            return Ok(_data.GetLocations());
        }

        [HttpPost]
        public Location AddLocation([FromBody] Location location)
        {
            _data.AddLocation(location);
            return location;
        }
    }
}
