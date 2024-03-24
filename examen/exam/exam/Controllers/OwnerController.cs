using exam.Data;
using exam.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace exam.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private InterfaceCampingContext _data;
        public OwnerController(InterfaceCampingContext data)
        {
            _data = data;
        }



        [HttpGet]
        public ActionResult<IEnumerable<Owner>> GetOwner()
        {
            return Ok(_data.GetOwners());
        }

        [HttpPost]
        public void AddOwner([FromBody] Owner owner)
        {
            _data.AddOwner(owner);
        }
    }
}
