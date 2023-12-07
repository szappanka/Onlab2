//using Common.Models;
//using Data.DataContexts;
//using Data.Entities;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;

//namespace AutBackendApi.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class CoursesController : ControllerBase
//    {
//        private readonly EscapeRoomDbContext context;

//        public CoursesController(EscapeRoomDbContext context)
//        {
//            this.context = context;
            
//        }
//        [HttpGet]
//        public async Task<ActionResult<IEnumerable<EscapeRoomCourseDto>>> GetCourses()
//        {
//            try
//            {
//                var courses = await context.EscapeRoomCourses.Include(c => c.EscapeRoomTasks).ToListAsync();
//                if(courses == null) return NotFound();
//                return Ok(courses);
//            }
//            catch (Exception)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from the database.");
//            }
            
//        }

//        [HttpGet("{id}")]
//        public async Task<ActionResult<EscapeRoomCourseDto>> GetCourse(int id)
//        {
//            var course = await context.EscapeRoomCourses.Include(c => c.EscapeRoomTasks).FirstOrDefaultAsync(c => c.Id == id);
//            return Ok(course);
//        }

//        [HttpDelete("{id}")]
//        public async Task<ActionResult> DeleteCourse(int id)
//        {
//            if (context.EscapeRoomCourses == null)
//            {
//                return NotFound();
//            }
//            var course = await context.EscapeRoomCourses.FindAsync(id);
//            if (course == null)
//            {
//                return NotFound();
//            }

//            context.EscapeRoomCourses.Remove(course);
//            await context.SaveChangesAsync();

//            return NoContent();
//        }

//        [HttpPost]
//        public async Task<ActionResult<EscapeRoomCourseDto>> PostCourse(EscapeRoomCourseWithoutTasksDto course)
//        {
//            if (context.EscapeRoomCourses == null)
//            {
//                return Problem("Course list is null.");
//            }
//            var courseToAdd = new EscapeRoomCourse
//            {
//                Name = course.Name,
//                EscapeRoomTasks = new List<EscapeRoomTask>()
//            };
//            context.EscapeRoomCourses.Add(courseToAdd);
//            await context.SaveChangesAsync();

//            return CreatedAtAction("GetCourse", new { id = courseToAdd.Id }, courseToAdd);
//        }


//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutCourse(int id, EscapeRoomCourseWithoutTasksDto course)
//        {

//            //context.Entry(course).State = EntityState.Modified;

//            try
//            {
//                var courseToModify = await context.EscapeRoomCourses.FindAsync(id);
//                courseToModify.Name = course.Name;
//                await context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!CourseExists(id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return NoContent();
//        }

//        private bool CourseExists(int id)
//        {
//            return (context.EscapeRoomCourses?.Any(e => e.Id == id)).GetValueOrDefault();
//        }
//    }
//}
