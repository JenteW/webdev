using exam.Data;
using exam.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace exam.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private InterfaceCampingContext _data;
        public CountryController(InterfaceCampingContext data)
        {
            _data = data;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Country>> GetCountry()
        {
            return Ok(_data.GetCountries());
        }

        [HttpPost]
        public void AddCountry([FromBody] Country country)
        {
            _data.AddCountry(country);
        }
        // delete country by id
        [HttpDelete("{id}")]
        public void DeleteCountry(int id)
        {
            _data.DeleteCityByCountryId(id);
            _data.DeleteCountry(id);
        }

    }
}
