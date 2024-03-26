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

        // get user
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUser()
        {
            return Ok(_data.GetUsers());
        }

        // add user
        [HttpPost]
        public void AddUser([FromBody] User user)
        {
            _data.AddUser(user);
        }



        //get user by username

        [HttpGet("{username}")]

        public void GetUserByUsername(string username)
        {
            _data.GetUserByUsername(username);
        }


        // update user

        [HttpPut("{id}")]

        public void UpdateUser(int id, [FromBody] User user)
        {
            _data.UpdateUser(id, user);
        }


    }
}
