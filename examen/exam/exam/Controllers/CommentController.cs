using exam.Data;
using exam.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace exam.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private InterfaceCampingContext _data;
        public CommentController(InterfaceCampingContext data)
        {
            _data = data;
        }


        [HttpGet]
        public ActionResult<IEnumerable<Admin>> GetCity()
        {
            return Ok(_data.GetCities());
        }


        [HttpPost]
        public void AddCity([FromBody] City city)
        {
            _data.AddCity(city);
        }
    }
}
