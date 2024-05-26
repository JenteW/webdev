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
        public IEnumerable<Comment> GetComments()
        {
            return _data.GetComments();
        }


        [HttpPost]
        public IActionResult AddComment(Comment comment)
        {
            _data.AddComment(comment);
            return Ok();
        }
        // get comments by id
        [HttpGet("{id}")]
        public Comment GetCommentById(int id)
        {
            return _data.GetCommentById(id);
        }
        // get comments by camping spot id
        [HttpGet("campingSpot/{id}")]
        public IEnumerable<Comment> GetCommentsByCampingSpotId(int id)
        {
            return _data.GetCommentsByCampingSpotId(id);
        }
        // delete comment by id
        [HttpDelete("{id}")]
        public IActionResult DeleteComment(int id)
        {
            _data.DeleteComment(id);
            return Ok();
        }
        // update comment by id
        [HttpPut("{id}")]
        public IActionResult UpdateComment(int id, Comment comment)
        {
            _data.UpdateComment(id, comment);
            return Ok();
        }
    }
}
