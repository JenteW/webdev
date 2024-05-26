using Microsoft.AspNetCore.Mvc;


namespace exam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadImage : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            // Get the current directory of the API project
            var currentDirectory = Directory.GetCurrentDirectory();

            // Compute the path to the target directory relative to the current directory
            var targetDirectory = Path.Combine(currentDirectory, @"..\..\..\..\webdev\examen\frontend\frontend-exam\src\assets\campingspots");

            // Resolve the full absolute path
            var absoluteTargetDirectory = Path.GetFullPath(targetDirectory);

            // Ensure the target directory exists
            if (!Directory.Exists(absoluteTargetDirectory))
            {
                return BadRequest("Target directory does not exist. " + absoluteTargetDirectory );
                //Directory.CreateDirectory(absoluteTargetDirectory);
            }

            // Compute the full file path
            var filePath = Path.Combine(absoluteTargetDirectory, file.FileName);

            // Save the file
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Ok(new { path = filePath });
        }

    }
}
