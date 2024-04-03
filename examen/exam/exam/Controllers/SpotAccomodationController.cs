using exam.Data;
using exam.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace exam.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SpotAccomodationController : ControllerBase
    {
        private InterfaceCampingContext _data;
        public SpotAccomodationController(InterfaceCampingContext data)
        {
            _data = data;
        }

        [HttpGet]
        public ActionResult<IEnumerable<SpotAccomodation>> GetSpotAccomodation()
        {
            return Ok(_data.GetSpotAccomodations());
        }

        [HttpPost]
        public void AddSpotAccomodation([FromBody] SpotAccomodation spotAccomodation)
        {
            _data.AddSpotAccomodation(spotAccomodation);
        }

        //get all spots for a specific accomodation
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<SpotAccomodation>> GetSpotByAccomodation(int id)
        {
            return Ok(_data.GetSpotsByAccomodation(id));
        }
    }
}
