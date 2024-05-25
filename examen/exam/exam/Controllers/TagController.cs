using exam.Data;
using exam.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace exam.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private InterfaceCampingContext _data;
        public TagController(InterfaceCampingContext data)
        {
            _data = data;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Tag>> GetTag()
        {
            return Ok(_data.GetTags());
        }

        [HttpPost]
        public void AddTag([FromBody] Tag tag)
        {
            _data.AddTag(tag);
        }
        [HttpGet("{id}")]
        public ActionResult<Tag> GetTagById(int id)
        {
            return Ok(_data.GetTagById(id));
        }

        // delete tag by id
        [HttpDelete("{id}")]
        public void DeleteTag(int id)
        {
            _data.DeleteTag(id);
        }

    }
}
