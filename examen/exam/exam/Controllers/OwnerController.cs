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
        // get owner by username if password is correct
        [HttpGet("login/{username}")]

        public IActionResult GetOwnerByUsernameAndPassword(string username, string password)
        {
            var user = _data.GetOwnerByUsernameAndPassword(username);
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

        //get owner by username
       /* [HttpGet("{username}")]
        public Owner GetOwnerByUsername(string username)
        {
           return _data.GetOwnerByUsername(username);
        }*/

        // get owner by id
        [HttpGet("{id}")]
        public Owner GetOwnerById(int id)
        {
            return _data.GetOwnerById(id);
        }

        // update owner
        [HttpPut("{id}")]
        public void UpdateOwner(int id, [FromBody] Owner owner)
        {
            _data.UpdateOwner(id, owner);
        }
        // delete owner
        [HttpDelete("{id}")]
        public void DeleteOwner(int id)
        {
            _data.DeleteOwner(id);
        }
    }
}
