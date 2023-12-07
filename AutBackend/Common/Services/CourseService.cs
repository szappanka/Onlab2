using Common.Models;
using Common.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Common.Services
{
    public class CourseService : ICourseService
    {
        private readonly HttpClient httpClient;

        public CourseService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<IEnumerable<EscapeRoomCourseDto>> GetCourses()
        {
            try
            {
                var courses = await httpClient.GetFromJsonAsync<IEnumerable<EscapeRoomCourseDto>>("api/Courses");
                return courses;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
