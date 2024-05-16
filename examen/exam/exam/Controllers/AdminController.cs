using exam.Data;
using exam.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using static exam.PasswordHasher;
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
        public IActionResult AddAdmin([FromBody] Admin admin)
        {
            //hashing the password
            admin.Password = PasswordHasher.Hash(admin.Password);
           return _data.AddAdmin(admin);
        }

        // get user by username if password is correct
        [HttpGet("login/{username}")]

        public IActionResult GetAdminByUsernameAndPassword(string username, string password)
        {
            var user = _data.GetAdminByUsernameAndPassword(username);
            if (user == null)
            {
                return NotFound();
            }
            else if (PasswordHasher.VerifyPassword(password, user.Password))
            {
                return Ok(user);
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public Admin GetAdminById(int id)
        {
            //only return the id and username
            return _data.GetAdminById(id);
        }





    }
}
