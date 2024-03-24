using exam.Data;
using exam.Models;
using Microsoft.AspNetCore.Mvc;

namespace exam.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class UserController : ControllerBase
    {
        private InterfaceCampingContext _data;
        public UserController(InterfaceCampingContext data)
        {
            _data = data;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUser()
        {
            return Ok(_data.GetUsers());
        }

        [HttpPost]
        public void AddUser([FromBody] User user)
        {
            _data.AddUser(user);
        }



    }
}
