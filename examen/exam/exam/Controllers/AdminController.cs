using exam.Data;
using exam.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace exam.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private InterfaceCampingContext _data;
        public AdminController(InterfaceCampingContext data)
        {
            _data = data;
        }


        [HttpGet]
        public ActionResult<IEnumerable<Admin>> GetAdmin()
        {
            return Ok(_data.GetAdmins());
        }


        [HttpPost]
        public void AddAdmin([FromBody] Admin admin)
        {
            _data.AddAdmin(admin);
        }







    }
}
