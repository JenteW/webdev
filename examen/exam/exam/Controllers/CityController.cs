using exam.Data;
using exam.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace exam.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private InterfaceCampingContext _data;
        public CityController(InterfaceCampingContext data)
        {
            _data = data;
        }


        [HttpGet]
        public ActionResult<IEnumerable<City>> GetCity()
        {
            return Ok(_data.GetCities());
        }
        // get city by country id
        [HttpGet("country/{id}")]
        public IEnumerable<City> GetCityByCountryId(int id)
        {
            return _data.GetCities().Where(c => c.CountryId == id);
        }

        [HttpPost]
        public void AddCity([FromBody] City city)
        {
            _data.AddCity(city);
        }

        [HttpPut("{id}")]
        public void UpdateCity(int id, [FromBody] City city)
        {
            _data.UpdateCity(id, city);
        }
        //delete city by country id
        [HttpDelete("Country/{id}")]
        public void DeleteCityByCountryId(int id)
        {
            _data.DeleteCityByCountryId(id);
        }
        //delete city by id
        [HttpDelete("{id}")]
        public void DeleteCity(int id)
        {
            _data.DeleteCity(id);
        }
    }
}
