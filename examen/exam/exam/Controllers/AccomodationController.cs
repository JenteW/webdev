using exam.Data;
using exam.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace exam.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccomodationController : ControllerBase
    {
        private InterfaceCampingContext _data;
        public AccomodationController(InterfaceCampingContext data)
        {
            _data = data;
        }


        [HttpGet]
        public ActionResult<IEnumerable<Accomodation>> GetAccomodation()
        {
            return Ok(_data.GetAccomodations());
        }


        [HttpPost]
        public void AddAccomodation([FromBody] Accomodation accomodation)
        {
            _data.AddAccomodation(accomodation);
        }
        [HttpGet("campingSpotId/{id}")]
        public ActionResult<IEnumerable<Accomodation>> GetAccomodationByCampingSpotId(int id)
        {
            return Ok(_data.GetAccomodationsByCampingSpotId(id));
        }
        
    }
}
