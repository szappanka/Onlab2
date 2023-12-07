using Common.Models;
using Common.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace GameMaster.Pages
{
    public class CoursesBase : ComponentBase
    {
        [Inject]
        public ICourseService CourseService { get; set; }
        public IEnumerable<EscapeRoomCourseDto> Courses { get; set; }
        protected override async Task OnInitializedAsync()
        {
            Courses = await CourseService.GetCourses();
        }
    }
}
