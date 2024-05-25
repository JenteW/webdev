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

        //get camping spot by id
        [HttpGet("{id}")]
        public ActionResult<CampingSpot> GetCampingSpotById(int id)
        {
            return Ok(_data.GetCampingSpotById(id));
        }


        //get camingspot by owner id
        [HttpGet("Owner/{id}")]
        public ActionResult<IEnumerable<CampingSpot>> GetCampingSpotByOwnerId(int id)
        {
            return Ok(_data.GetCampingSpotsByOwnerId(id));
        }
        //get campingspot by location id
        [HttpGet("Location/{id}")]
        public ActionResult<IEnumerable<CampingSpot>> GetCampingSpotByLocationId(int id)
        {
            return Ok(_data.GetCampingSpotsByLocationId(id));
        }
        //delete camping spot and all accomodations
        [HttpDelete("{id}")]
        public void DeleteCampingSpotAndAccomodations(int id)
        {
            _data.DeleteAccomodationBySpotId(id);
            _data.DeleteCampingSpot(id);
        }
        // update camping spot
        [HttpPut("{id}")]
        public void UpdateCampingSpot(int id, [FromBody] CampingSpot campingspot)
        {
            _data.UpdateCampingSpot(id, campingspot);
        }

    }
}
