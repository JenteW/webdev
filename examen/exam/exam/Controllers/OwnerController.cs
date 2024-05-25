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
        public IActionResult AddOwner([FromBody] Owner owner)
        {
            owner.Password = PasswordHasher.Hash(owner.Password);
            return _data.AddOwner(owner);
        }
        // get owner by username if password is correct
        [HttpGet("login/{username}")]

        public IActionResult GetOwnerByUsernameAndPassword(string username, string password)
        {
            var owner = _data.GetOwnerByUsernameAndPassword(username);
            if (owner == null)
            {
                return NotFound("No user exists");
            }
            else if (PasswordHasher.VerifyPassword(password, owner.Password))
            {
                return Ok(owner);
            }
            return NotFound("No user found");
        }

        //check if ownername is available
        [HttpGet("username/{username}")]
        public bool CheckOwnername(string username)
        {
            return _data.CheckOwnername(username);
        }

        // get owner by id
        [HttpGet("{id}")]
        public Owner GetOwnerById(int id)
        {
            return _data.GetOwnerById(id);
        }

        // update owner
        [HttpPut("{id}")]
        public IActionResult UpdateOwner(int id, [FromBody] Owner owner)
        {
            owner.Password = PasswordHasher.Hash(owner.Password);
            return _data.UpdateOwner(id, owner);
        }
        // delete owner
        [HttpDelete("{id}")]
        public void DeleteOwner(int id)
        {
            _data.DeleteOwner(id);
        }
    }
}
