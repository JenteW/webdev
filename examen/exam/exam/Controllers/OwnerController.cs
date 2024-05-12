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
            if (user.Password == password)
            {
                return Ok(user);
            }
            return NotFound();
        }

        //get owner by username
        [HttpGet("{username}")]
        public void GetOwnerByUsername(string username)
        {
            _data.GetOwnerByUsername(username);
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
