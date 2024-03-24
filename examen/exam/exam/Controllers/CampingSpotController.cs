using exam.Data;
using exam.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace exam.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CampingSpotController : ControllerBase
    {
        private InterfaceCampingContext _data;
        public CampingSpotController(InterfaceCampingContext data)
        {
            _data = data;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CampingSpot>> GetCampingSpot()
        {
            return Ok(_data.GetCampingSpots());
        }

        [HttpPost]
        public void AddCampingSpot([FromBody] CampingSpot campingspot)
        {
            _data.AddCampingSpot(campingspot);
        }
    }
}
