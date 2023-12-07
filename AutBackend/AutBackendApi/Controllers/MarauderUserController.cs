using Data.DbContexts;
using Microsoft.AspNetCore.Mvc;
using Data.Entities;
using Common.Models;
using Newtonsoft.Json;

namespace AutBackendApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class MarauderUserController : Controller
    {
        private readonly MaraudersMapDbContext context;
        private readonly string stringData;
        
        public MarauderUserController(MaraudersMapDbContext context)
        {
            this.context = context;
            // get the jsondata from file
            stringData = System.IO.File.ReadAllText("D:\\Msc\\Önlab1\\AR-room\\AutBackend\\AutBackend\\AutBackendApi\\data.json");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MarauderDto>>> GetCourses()
        {
            try
            {
                //var courses = await context.MarauderUsers.ToListAsync
                //if (courses == null) return NotFound();
                return Ok(stringData);
            }
            catch (Exception)
            {   
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MarauderDto>> GetCourse(int id)
        {
            //var course = await context.MarauderUsers.FirstOrDefaultAsync(c => c.Id == id);
            var jsonData = JsonConvert.DeserializeObject<List<MarauderDto>>(stringData);
            if(id > jsonData.Count)
                return NotFound();

            return Ok(jsonData[id]);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCourse(int id)
        {
            if (context.MarauderUsers == null)
            {
                return NotFound();
            }
            var course = await context.MarauderUsers.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            context.MarauderUsers.Remove(course);
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<MarauderUser>> PostCourse(MarauderUser user)
        {
            if (context.MarauderUsers == null)
            {
                return Problem("Course list is null.");
            }
            var userToAdd = new MarauderUser()
            {
                Name = user.Name,
                Coordinates = user.Coordinates,
                IsActivated = user.IsActivated,
            };
            context.MarauderUsers.Add(userToAdd);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetCourse", new { id = userToAdd.Id }, userToAdd);
        }
    }
}
