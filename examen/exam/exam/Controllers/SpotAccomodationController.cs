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
        public SpotAccomodation(InterfaceCampingContext data)
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
    }
}
